using Contract.Business.BL;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contract.API.Business
{
    public class AccountBusiness : BaseBusiness
    {
        #region Fields, Properties

        private readonly ILoginUserBO loginUseBO;
        private readonly IEmailActiveBO emailActiveBO;
        
        #endregion Fields, Properties

        #region Methods

        public AccountBusiness(IBOFactory boFactory)
        {
            var config = new SessionConfig()
            {
                LoginSecretKey = Config.ApplicationSetting.Instance.LoginSecretKey,
                SessionTimeout = TimeSpan.FromMinutes(Config.ApplicationSetting.Instance.SessionTimeout),
                CreateTokenRandom = Config.ApplicationSetting.Instance.EnableCreateTokenRandom,
                SessionResetPasswordExpire = TimeSpan.FromMinutes(Config.ApplicationSetting.Instance.ResetPasswordTimeOut),
            };

            EmailConfig emailConfig = new EmailConfig()
            {
                FolderEmailTemplate = HttpContext.Current.Server.MapPath(Config.ApplicationSetting.Instance.EmailTemplateFilePath)
            };

            this.loginUseBO = boFactory.GetBO<ILoginUserBO>(config);
            this.emailActiveBO = boFactory.GetBO<IEmailActiveBO>(emailConfig);
        }

        public List<AccountDetail> FillterUser(out int totalRecords, string keyword = null, string orderby = null, string orderType = null, int skip = 0, int take = int.MaxValue)
        {
            var condition = new ConditionSearchUser(this.CurrentUser, keyword, orderby, orderType);
            condition.ChildLevels = new string[] { 
                this.CurrentUser.RoleUser.Level
            };

            totalRecords = this.loginUseBO.CountFillterUser(condition);
            return this.loginUseBO.FillterUser(condition, skip, take).ToList();
        }

        public AccountInfo GetAccoutInfo(int id)
        {
            return this.loginUseBO.GetAccountInfo(id, this.CurrentUser.Company.Id, this.CurrentUser.RoleUser.Level); 
        }

        public AccountInfo GetAccoutDefaultOfCompany(int companyId)
        {
            return this.loginUseBO.GetAccountDefaultCompany(companyId);
        }
         
        public ResultCode ChangePassword(int id, PasswordInfo passwordInfo)
        {
            bool sameUser = id == this.CurrentUser.Id;
            if (!sameUser)
            {
                throw new BusinessLogicException(ResultCode.IdNotMatch,
                                string.Format("Change password faild because parameter Id not match with current Id = [{0}].", id));
            }

            if (passwordInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            return this.loginUseBO.ChangePassword(id, passwordInfo);
        }

        public ResultCode CreateUser(AccountInfo userInfo)
        {
            if (userInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            userInfo.CompanyId = this.CurrentUser.Company.Id;
            return this.loginUseBO.Create(userInfo); 
        }

        public ResultCode UpdateUser(int id, AccountInfo userInfo)
        {
            if (userInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            userInfo.RoleLevel = this.CurrentUser.RoleUser.Level;
            return this.loginUseBO.Update(id, userInfo);
        }

        public ResultCode CreateUserDefault(AccountInfo userInfo, string level)
        {
            if (userInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            userInfo.AccountDefault = true;
            userInfo.RoleLevel = level;
            return this.loginUseBO.CreateDefault(userInfo);
        }

        public ResultCode DeleteUser(int id)
        {
            string roleOfCurrentUser = this.CurrentUser.RoleUser.Level;
            int? companyId = this.CurrentUser.Company.Id;
            return this.loginUseBO.Delete(id,companyId, roleOfCurrentUser);
        }


        public ResultCode SendEmailAccount(AccountInfo accountInfo)
        {
            if (accountInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            accountInfo.CompanyId = new int?(this.GetCompanyIdOfUser());
            return this.emailActiveBO.SendEmail(accountInfo, this.SmtpClientOfCompany);
        }
        private bool IsCompanyOfUser(int companyId)
        {
            bool result = true;
            int? idCompanyOfUser = this.CurrentUser.Company.Id;
            if (!idCompanyOfUser.HasValue ||
                idCompanyOfUser.Value != companyId)
            {
                result = false;
            }

            return result;
        }

        #endregion
    }
}