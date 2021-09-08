using Contract.Business.Cache;
using Contract.Business.Constants;
using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using Contract.Common.Extensions;
using Contract.Business.Config;
using System.IO;
namespace Contract.Business.BL
{
    public class SessionManagerBO : ISessionManagerBO
    {
        #region Fields, Properties

        private static Logger logger = new Logger();

        private readonly string authenSecretKey;
        private readonly string folderAsset;
        private readonly TimeSpan loginSessionTimeout;
        private readonly ISignOfUserRepository signOfUserRepository;
        private readonly TimeSpan tokenResetPasswordExpire;
        private readonly bool createTokenRandom;

        private readonly ILoginUserRepository loginUserRepository;

        #endregion

        #region Contructor

        public SessionManagerBO(IRepositoryFactory repoFactory, SessionConfig config)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            Ensure.Argument.NotNull(config, "config");
            this.loginUserRepository = repoFactory.GetRepository<ILoginUserRepository>();
            this.authenSecretKey = config.LoginSecretKey;
            this.loginSessionTimeout = config.SessionTimeout;
            this.createTokenRandom = config.CreateTokenRandom;
            this.tokenResetPasswordExpire = config.SessionResetPasswordExpire;
            this.folderAsset = config.FolderAsset;
            this.signOfUserRepository = repoFactory.GetRepository<ISignOfUserRepository>();
        }

        #endregion

        #region ISessionManagerBO Methods

        public UserSessionInfo Login(LoginInfo loginInfo )
        {
            ResultCode errorCode;
            string errorMessage;
            if (!loginInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            // Get User information
            var loginUser = this.loginUserRepository.Login(loginInfo.UserId, loginInfo.Password);
            if (loginUser.MyCompany != null && ((loginUser.MyCompany.Deleted ?? false)  || !(loginUser.MyCompany.Active ?? false)))
            {
                throw new BusinessLogicException(ResultCode.LoginUserIdNotExist,
                                  string.Format("Login faild because userId [{0}] not found.", loginInfo.UserId));
            }

            if (loginUser != null && Utility.IsTrue(loginUser.Deleted))
            {
                throw new BusinessLogicException(ResultCode.LoginUserIdNotExist,
                                    string.Format("Login failed failed because user with USER_ID = [{0}] has been deleted.", loginInfo.UserId));
            }

            
            // Cache login information
            UserSessionInfo userSessionInfo = GetSessionInfo(loginUser, false);
            userSessionInfo.SignatureImage = SignatureImage(loginUser.UserSID, (loginUser.CompanySID ?? 0));
            UserSessionCache.Instance.SaveUserSession(userSessionInfo, this.loginSessionTimeout);
            return userSessionInfo;
        }

        public UserSessionInfo Logout(string token)
        {
            // Delete token in session
            var sessionInfo = UserSessionCache.Instance.RemoveUserSession(token);
            return sessionInfo;
        }

        public UserSessionInfo GetUserSession(string token)
        {
            return UserSessionCache.Instance.GetUserSession(token);
        }

        public UserSessionInfo ResetPassword(ResetPassword resetPasswordInfo)
        {
            // Validate data
            if (resetPasswordInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode resultCode;
            string errorDetail;
            if (!resetPasswordInfo.IsValid(out resultCode, out errorDetail))
            {
                throw new BusinessLogicException(resultCode, errorDetail);
            }

            var loginUser = GetUserByEmail(resetPasswordInfo.Email);
            UserSessionInfo userSessionInfo = GetSessionInfo(loginUser);
            UserSessionCache.Instance.SaveUserSession(userSessionInfo, this.tokenResetPasswordExpire);

            return userSessionInfo;
        }

        public ResultCode UpdatePassword(int userId, ChangePassword updatePasswordInfo)
        {
            // Validate data
            if (updatePasswordInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode resultCode;
            string errorDetail;
            if (!updatePasswordInfo.IsValid(out resultCode, out errorDetail))
            {
                throw new BusinessLogicException(resultCode, errorDetail);
            }

            var loginUser = GetUserById(userId);

            // Update Pasword
            var currentTime = DateTime.Now;
            loginUser.Password = updatePasswordInfo.NewPassword;
            loginUser.UpdatedDate = currentTime;
            loginUser.LastChangedPasswordTime = currentTime;
            return loginUserRepository.Update(loginUser) ? ResultCode.NoError : ResultCode.UnknownError;
        }

        /// <summary>
        /// A user has been changed (updated/deleted).
        /// </summary>
        /// <param name="user">The user changed</param>
        /// <param name="isDeleted">true: user is deleted, false: user is updated.</param>

        #endregion ISessionManagerBO Methods

        #region Private methods

        private UserSessionInfo GetSessionInfo(LoginUser loginUser, bool isConnection = false)
        {
            string token = this.createTokenRandom ? Guid.NewGuid().ToString("N") : loginUser.UserID;;
            if (isConnection)
            {
                token = string.Format("{0};{1}", loginUser.UserID, loginUser.Password);
            }
             
            UserSessionInfo userSessionInfo  = new UserSessionInfo(loginUser, loginUser.UserRole, loginUser.MyCompany, token);;
            userSessionInfo.RoleUser.Permissions = GetPermissions(loginUser.RoleOfUsers);
            return userSessionInfo;
        }

        private List<string> GetPermissions(IEnumerable<RoleOfUser> roleOfUsers)
        {
            List<string> permissionOfUser = new List<string>();
            if (roleOfUsers == null)
            {
                return permissionOfUser;
            }

            roleOfUsers.ForEach(p => {

                if (p.Action.IsNotNullOrEmpty())
                {
                    foreach (var item in p.Action.ToArray())
                    {
                        permissionOfUser.Add(string.Format("{0}_{1}", p.FunctionName, item));
                    }
                }
            });

            return permissionOfUser;
        }


        private List<string> CreatePermissions(string masterData, string permission)
        {
            List<string> permissions = new List<string>();
            foreach (var item in permission.ToCharArray())
            {
                permissions.Add(string.Format("{0}_{1}", masterData, item));
            }

            return permissions;
        }

        private LoginUser GetUserByEmail(string email)
        {
            var loginUser = this.loginUserRepository.GetByEmail(email);
            if (loginUser == null)
            {
                throw new BusinessLogicException(ResultCode.LoginEmailNotExist,
                                   string.Format("Email does not exist: {0}", email));
            }

            return loginUser;
        }

        private LoginUser GetUserById(int userId)
        {
            var loginUser = this.loginUserRepository.GetById(userId);
            if (loginUser == null)
            {
                throw new BusinessLogicException(ResultCode.LoginUserIdNotExist,
                                   string.Format("User id does not exist: {0}", userId));
            }

            return loginUser;
        }

        private string SignatureImage(int userId, int compnayId)
        {
            SignOfUser singOfUse = this.signOfUserRepository.GetSignOfUserDefault(userId);
            if (singOfUse == null || this.folderAsset.IsNullOrEmpty())
            {
                return string.Empty;
            }

            string folderContain = Path.Combine(this.folderAsset, compnayId.ToString(), userId.ToString(), singOfUse.FileName);
            return FileProcess.GetBase64StringFile(folderContain);
        }

        #endregion
    }
}