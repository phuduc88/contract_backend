using Contract.API.Config;
using Contract.Business.BL;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contract.API.Business
{
    public class EmailActiveBusiness : BaseBusiness
    {
        #region Fields, Properties

        private IEmailActiveBO emailActiveBO;

        #endregion Fields, Properties

        #region Contructor

        public EmailActiveBusiness(IBOFactory boFactory)
        {
            EmailConfig emailConfig = new EmailConfig()
            {
                FolderEmailTemplate = HttpContext.Current.Server.MapPath(ApplicationSetting.Instance.EmailTemplateFilePath)
            };

            this.emailActiveBO = boFactory.GetBO<IEmailActiveBO>(emailConfig);
        }

        #endregion Contructor

        #region Methods

        public IEnumerable<EmailActiveInfo> Filter(out int totalRecords, string keyword = null, int? status = null, string dateFrom = null, string dateTo = null, string orderType = null, string orderby = null, int skip = int.MinValue, int take = int.MaxValue)
        {
            ConditionSearchEmailActive condition = new ConditionSearchEmailActive(this.CurrentUser, keyword, status, dateFrom, dateTo, orderType, orderby);
            totalRecords = this.emailActiveBO.CountFilter(condition);
            return this.emailActiveBO.Filter(condition, skip, take);
        }

        public EmailActiveInfo GetEmailActive(int id)
        {
            int companyIdOfUser = GetCompanyIdOfUser();
            return this.emailActiveBO.GetEmailActive(companyIdOfUser, id);
        }

        public ResultCode SendEmailActive(int id, EmailActiveInfo emailActive)
        {
            if (emailActive == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            return this.emailActiveBO.SendEmail(id, emailActive, this.SmtpClientOfCompany);
        }

        #endregion
    }
}