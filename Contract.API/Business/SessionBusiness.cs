using Contract.Business;
using Contract.Business.BL;
using Contract.Business.Config;
using Contract.Business.Constants;
using Contract.Business.Email;
using Contract.Business.Models;
using Contract.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
namespace Contract.API.Business
{
    public class SessionBusiness : BaseBusiness
    {
        #region Fields, Properties

        private ISessionManagerBO sessionManagerBO;
        private readonly EmailConfig emailConfig;
        #endregion Fields, Properties

        #region Methods

        public SessionBusiness(IBOFactory boFactory)
        {
            var config = new SessionConfig()
            {
                LoginSecretKey = Config.ApplicationSetting.Instance.LoginSecretKey,
                SessionTimeout = TimeSpan.FromMinutes(Config.ApplicationSetting.Instance.SessionTimeout),
                CreateTokenRandom = Config.ApplicationSetting.Instance.EnableCreateTokenRandom,
                SessionResetPasswordExpire = TimeSpan.FromMinutes(Config.ApplicationSetting.Instance.ResetPasswordTimeOut),
                FolderAsset = HttpContext.Current.Server.MapPath(Config.ApplicationSetting.Instance.FolderAssetOfCompany),
            };

            emailConfig = new EmailConfig()
            {
                FolderEmailTemplate = HttpContext.Current.Server.MapPath(Config.ApplicationSetting.Instance.EmailTemplateFilePath),
            };

            this.sessionManagerBO = boFactory.GetBO<ISessionManagerBO>(config);
        }

        /// <summary>
        /// Process user login flow
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public UserSessionInfo Login(LoginInfo loginInfo)
        {
            var userSessionInfo = this.sessionManagerBO.Login(loginInfo);
            return userSessionInfo;
        }

        /// <summary>
        /// Process: user logout flow
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ResultCode Logout(string token)
        {
            this.sessionManagerBO.Logout(token);
            return ResultCode.NoError;
        }

        /// <summary>
        /// Process: checking user session is alive
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ResultCode CheckUserSession(string token)
        {
            if (Config.ApplicationSetting.Instance.TokenKeyResearch.Equals(token))
            {
                return ResultCode.SessionAlive;
            }

            var sessionInfo = this.sessionManagerBO.GetUserSession(token);
            return sessionInfo != null ? ResultCode.SessionAlive : ResultCode.SessionEnded;
        }

        public ResultCode CheckUserPassword(string token)
        {
            if (Config.ApplicationSetting.Instance.TokenKeyResearch.Equals(token))
            {
                return ResultCode.SessionAlive;
            }

            var sessionInfo = this.sessionManagerBO.GetUserSession(token);
            return sessionInfo != null ? ResultCode.SessionAlive : ResultCode.SessionEnded;
        }

        public UserSessionInfo GetUserSession(string token)
        {
            if (Config.ApplicationSetting.Instance.TokenKeyResearch.Equals(token))
            {
                return new UserSessionInfo()
                {
                    UserId = "MBHXH",
                    UserName = "MBHXH",
                    RoleUser = new Role() { Permissions = new List<string>()},
                };
            }
            return this.sessionManagerBO.GetUserSession(token);
        }

        public bool ResetPassword(ResetPassword resetPasswordInfo)
        {
            if (resetPasswordInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var userSessionInfo = this.sessionManagerBO.ResetPassword(resetPasswordInfo);
            var receiverInfo = new ReceiverInfo(userSessionInfo, EmailType.ResetPassword);
            receiverInfo.UrlResetPassword = string.Format("{0}{1}/", Config.ApplicationSetting.Instance.UrlResetPassword, userSessionInfo.Token);

            EmailInfo email = EmailTemplate.GetEmail(emailConfig, EmailType.ResetPassword, receiverInfo);
            ProcessEmail processEmail = new ProcessEmail();
            Task.Run(() => processEmail.SendEmail(new SendGmail(email, GetSmtpClientOfCompany(userSessionInfo.EmailServer))));
            return true;
        }

        public ResultCode UpdatePassword(ChangePassword passwordInfo)
        {
            if (passwordInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            return this.sessionManagerBO.UpdatePassword(this.CurrentUser.Id, passwordInfo);
        }

        public UserSessionInfo GetAccountInfo(string token)
        {
            return this.sessionManagerBO.GetUserSession(token);
        }
        #endregion Methods
    }
}