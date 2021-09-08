using Contract.Business.BL;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;
using System.Web;

namespace Contract.API.Business
{
    public class CompanyBusiness : BaseBusiness
    {
        #region Fields, Properties

        private IMyCompanyBO companyBo;

        #endregion Fields, Properties

        #region Contructor

        public CompanyBusiness(IBOFactory boFactory)
        {
            var uploadImageConfig = new UpdateloadImageConfig
            {
                MaxSizeImage =Config.ApplicationSetting.Instance.MaxSizeImage,
                RootFolderUpload=HttpContext.Current.Server.MapPath(Config.ApplicationSetting.Instance.FolderAssetOfCompany)
            };
            this.companyBo = boFactory.GetBO<IMyCompanyBO>(uploadImageConfig);
        }

        #endregion Contructor

        #region Methods

        public IEnumerable<MasterCompanyInfo> FillterCompanies(out int totalRecords,string keyword = null, string orderType = null, string orderby = null, int skip = 0, int take = int.MaxValue)
        {
            var condition = new ConditionSearchCompany(this.CurrentUser, keyword, orderType, orderby);
            totalRecords = this.companyBo.CountFillterCompany(condition);
            var companies = this.companyBo.FilterCompany(condition, skip, take);
            return companies;
        }

        public CompanyInfo GetCompanyInfo(int companyId)
        {
            return this.companyBo.GetCompanyInfo(companyId, RoleInfo.SALE);
        }

        public ResultCode CreateCompanyInfo(CompanyInfo companyInfo)
        {
            if (companyInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode result = this.companyBo.Create(companyInfo);
            return result;
        }

        public ResultCode UpdateCompanyInfo(int companyId, CompanyInfo companyInfo)
        {
            if (companyInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            return this.companyBo.Update(companyId, companyInfo);
        }

        public ResultCode Delete(int id)
        {
            string roleOfCurrentUser = this.CurrentUser.RoleUser.Level;
            return this.companyBo.Delete(id, roleOfCurrentUser);
        }

        public MyCompanyInfo GetMyCompanyInfo(int id)
        {
            int? companyid =  this.CurrentUser.Company.Id;
            return this.companyBo.GetCompanyOfUser(id, companyid);
        }

        public ResultCode UpdateMyCompany(int id, MyCompanyInfo companyInfo)
        {
            if (companyInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            int? companyid = this.CurrentUser.Company.Id;
            string roleLevel =  this.CurrentUser.RoleUser.Level;
            return this.companyBo.UpdateMyCompany(id, companyid, companyInfo, roleLevel);

        }

        public ResultCode UpdateToken(int id, TokenInfo tokenInfo)
        {
            if (tokenInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            int? companyid = this.CurrentUser.Company.Id;
            return this.companyBo.UpdateToken(id, companyid, tokenInfo);
        }
        #endregion
    }
}