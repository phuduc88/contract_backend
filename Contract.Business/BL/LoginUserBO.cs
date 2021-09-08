using Contract.Business.Cache;
using Contract.Business.Constants;
using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using Contract.Common.Extensions;
using Contract.Data.DBAccessor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Business.BL
{
    public class LoginUserBO : ILoginUserBO
    {
        #region Fields, Properties
        // <summary>
        /// Last userId in the table
        /// </summary>
        private static Logger logger = new Logger();
        private readonly SessionConfig config;
        private readonly ILoginUserRepository loginUserRepository;
        private readonly IRepositoryFactory repoFactory;
        private readonly Permission permission;
        private readonly IDbTransactionManager transaction;
        private readonly IMyCompanyRepository myCompanyRepository;


        private ResultCode errorCode;
        private string errorMessage;
        #endregion

        #region Contructor

        public LoginUserBO(IRepositoryFactory repoFactory, SessionConfig webConfig)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");

            this.loginUserRepository = repoFactory.GetRepository<ILoginUserRepository>();
            this.repoFactory = repoFactory;
            this.permission = new Permission(repoFactory);
            this.transaction = repoFactory.GetRepository<IDbTransactionManager>();
            this.myCompanyRepository = repoFactory.GetRepository<IMyCompanyRepository>();
            this.config = webConfig;
        }
        #endregion

        #region LoginUserBO Methods
        public IEnumerable<AccountDetail> FillterUser(ConditionSearchUser condition, int skip = int.MinValue, int take = int.MaxValue)
        {
            if (condition == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var loginUsers = this.loginUserRepository.FilterUser(condition).AsQueryable()
                .OrderBy(condition.Order_By, condition.Order_Type.Equals(OrderType.Desc)).Skip(skip).Take(take).ToList();

            return loginUsers.Select(p => new AccountDetail(p));
        }

        public int CountFillterUser(ConditionSearchUser condition)
        {
            if (condition == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            var listAccount = this.loginUserRepository.FilterUser(condition).ToList();
            return listAccount.Count();
        }

        public AccountInfo GetAccountInfo(int id, int? companyId, string level)
        {
            var currentLoginUser = GetById(id, companyId);
            AccountInfo accountInfo = new AccountInfo(currentLoginUser);
            accountInfo.Roles = this.permission.GetPermissionOfUser(currentLoginUser);
            return accountInfo;
        }

        public AccountInfo GetAccountDefaultCompany(int companyId)
        {
            AccountInfo accountDefault = new AccountInfo();
            LoginUser userDefault = this.loginUserRepository.GetUserDefault(companyId);
            if (userDefault != null)
            {
                accountDefault = new AccountInfo(userDefault);
            }

            return accountDefault;
        }


        public ResultCode Create(AccountInfo userInfo)
        {
            if (userInfo == null || !userInfo.CompanyId.HasValue)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            try
            {
                var companyOfUser = GetCompanyById(userInfo.CompanyId.Value);
                transaction.BeginTransaction();
                var loginUser = CreateLoginUser(userInfo, companyOfUser);
                this.permission.CreatePermission(loginUser, userInfo.Roles);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return ResultCode.NoError;
        }

        public ResultCode CreateDefault(AccountInfo userInfo)
        {
            if (userInfo == null || !userInfo.CompanyId.HasValue)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            try
            {
                var companyOfUser = GetCompanyById(userInfo.CompanyId.Value);
                var roles = permission.FunctionByLevel(userInfo.RoleLevel);
                transaction.BeginTransaction();
                var loginUser = CreateLoginUser(userInfo, companyOfUser, userInfo.RoleLevel);
                this.permission.CreatePermission(loginUser, roles);
                UpdateCompany(companyOfUser, userInfo.RoleLevel);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return ResultCode.NoError;
        }

        public ResultCode Update(int id, AccountInfo userInfo)
        {
            if (userInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            try
            {
                var currentUser = GetById(id, userInfo.CompanyId);
                transaction.BeginTransaction();
                currentUser = UpdateLoginUser(currentUser, userInfo);
                this.permission.UpdatePermission(currentUser, userInfo.Roles);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return ResultCode.NoError;
        }

        public ResultCode ChangePassword(int id, PasswordInfo passowrdInfo)
        {
            if (passowrdInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!passowrdInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            var currentUser = GetById(id, null);
            if (!currentUser.Password.IsEquals(passowrdInfo.CurrentPassword))
            {
                throw new BusinessLogicException(ResultCode.LoginEmailNotExist,
                                    string.Format("Change password faild because passwrod not match with current password = [{0}].", passowrdInfo.CurrentPassword));
            }

            var currentTime = DateTime.Now;
            currentUser.Password = passowrdInfo.NewPassword;
            currentUser.UpdatedDate = currentTime;
            currentUser.LastChangedPasswordTime = currentTime;
            return this.loginUserRepository.Update(currentUser) ? ResultCode.NoError : ResultCode.UnknownError;
        }

        public ResultCode Delete(int id, int? companyId, string roleOfUserDelete)
        {
            var currentUser = GetById(id, companyId);
            //if (!currentUser.UserRole.Levels.Contains(RoleInfo.SYSTEMADMIN))
            //{
            //    throw new BusinessLogicException(ResultCode.UserAccountMgtNotPermissionDelete,
            //                     "Delete User failed because current user not permission deleted.");
            //}
            if (currentUser.AccountDefault.HasValue && currentUser.AccountDefault.Value)
            {
                throw new BusinessLogicException(ResultCode.UserAccountMgtIsDefault,
                         "Delete User failed because current user is account default of company.");
            }

            currentUser.Deleted = true;
            currentUser.UpdatedDate = DateTime.Now;
            UserSessionCache.Instance.RemoveSessionByUserId(id);
            return loginUserRepository.Update(currentUser) ? ResultCode.NoError : ResultCode.UnknownError;
        }

        public ResultCode UpdateCurrentUser(LoginUser loginUser)
        {
            return this.loginUserRepository.Update(loginUser) ? ResultCode.NoError : ResultCode.UnknownError;
        }

        #endregion LoginUserBO Methods

        #region Private methods
        private UserRole GetUserRole(string level)
        {
            var userRole = this.repoFactory.GetRepository<IUserRoleRepository>().FillterUserRole(level).FirstOrDefault();
            if (userRole == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            return userRole;
        }

        private UserRole GetUserRole(int roleId)
        {
            var userRole = this.repoFactory.GetRepository<IUserRoleRepository>().FirstOrDefault(p => p.UserRoleSID == roleId);
            if (userRole == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            return userRole;
        }

        private LoginUser GetUserByEmail(string email)
        {
            var currentUser = this.loginUserRepository.GetByEmail(email);
            if (currentUser == null)
            {
                throw new BusinessLogicException(ResultCode.LoginEmailNotExist,
                                   string.Format("Email does not exist: {0}", email));
            }

            return currentUser;
        }

        private MyCompany GetCompanyById(int companyId)
        {
            var company = this.repoFactory.GetRepository<IMyCompanyRepository>().GetById(companyId);
            if (company == null)
            {
                throw new BusinessLogicException(ResultCode.NotFoundResourceId,
                                   string.Format("Get MyCompany info failed because Country with CompanySID =[{0}] not found.", companyId));
            }

            return company;
        }

        private LoginUser CreateLoginUser(AccountInfo userInfo, MyCompany companyInfo, string level = RoleInfo.CUSTOMER)
        {
            bool isExitUserId = this.loginUserRepository.ContainUserId(userInfo.UserID);
            if (isExitUserId)
            {
                throw new BusinessLogicException(ResultCode.UserAccountMgtConflictResourceUserId,
                                  string.Format("Create User failed because user with UserId = [{0}] is exist.", userInfo.UserID));
            }

            bool isExitEmail = this.loginUserRepository.ContainEmail(userInfo.Email);
            if (isExitEmail)
            {
                throw new BusinessLogicException(ResultCode.UserAccountMgtConflictResourceEmail,
                                  string.Format("Create User failed because user with Email = [{0}] is exist.", userInfo.Email));
            }

            var loginUserInfo = new LoginUser();
            var userRole = GetUserRole(level);
            loginUserInfo.CopyData(userInfo);
            loginUserInfo.CompanySID = companyInfo.CompanySID;
            loginUserInfo.UserRoleSID = userRole.UserRoleSID;
            loginUserInfo.CreatedDate = DateTime.Now;

            if (!loginUserInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            loginUserRepository.Insert(loginUserInfo);
            loginUserInfo.UserRole = userRole;
            return loginUserInfo;
        }

        private LoginUser UpdateLoginUser(LoginUser currentUser, AccountInfo userInfo)
        {
            if (!currentUser.UserID.IsEquals(userInfo.UserID)        // UserId is changed
                   && this.loginUserRepository.ContainUserId(userInfo.UserID))   // New UserId is existed
            {
                throw new BusinessLogicException(ResultCode.UserAccountMgtConflictResourceUserId,
                                string.Format("Update UserLogin with userId [{0}] exist", userInfo.UserID));
            }

            if (!currentUser.Email.IsEquals(userInfo.Email)        // Email is changed
               && this.loginUserRepository.ContainEmail(userInfo.Email)) // New Email is existed
            {
                throw new BusinessLogicException(ResultCode.UserAccountMgtConflictResourceEmail,
                                string.Format("Update UserLogin with Email [{0}] exist", userInfo.Email));
            }

            var currentTime = DateTime.Now;
            currentUser.CopyData(userInfo);
            var userRole = GetUserRole(userInfo.RoleLevel);
            currentUser.UserRoleSID = userRole.UserRoleSID;
            currentUser.UpdatedDate = DateTime.Now;
            currentUser.IsActive = userInfo.IsActive;
            currentUser.UpdatedDate = currentTime;
            if (!currentUser.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            this.loginUserRepository.Update(currentUser);
            currentUser.UserRole = userRole;
            return currentUser;
        }

        private LoginUser GetById(int id, int? companyId)
        {
            var currentUser = this.loginUserRepository.GetById(id);
            if (currentUser == null)
            {
                throw new BusinessLogicException(ResultCode.NotFoundResourceId,
                                  string.Format("Get LoginUser failed because LoginUser with ID =[{0}] not found.", id));
            }

            if (companyId.HasValue && companyId.Value != currentUser.CompanySID)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid,
                                 string.Format("Get LoginUser failed because LoginUser with company =[{0}] not mapping.", id));
            }

            return currentUser;
        }

        private void UpdateCompany(MyCompany companyOfUser, string roleLevel)
        {
            if (roleLevel.IsEquals(RoleInfo.CUSTOMER))
            {
            }
            this.myCompanyRepository.Update(companyOfUser);
        }
        #endregion

    }
}