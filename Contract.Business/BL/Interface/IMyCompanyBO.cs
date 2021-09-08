using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.BL
{
    public interface IMyCompanyBO
    {
        /// <summary>
        /// Get info of the Company
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="userLevel"></param>
        /// <returns></returns>
        CompanyInfo GetCompanyInfo(int companyId, string userLevel);

        IEnumerable<MyCompany> GetList();

        IEnumerable<MasterCompanyInfo> FilterCompany(ConditionSearchCompany condition, int skip = int.MinValue, int take = int.MaxValue);
        int CountFillterCompany(ConditionSearchCompany condition);

        ResultCode Create(CompanyInfo companyInfo);

        ResultCode Update(int companyId, CompanyInfo companyInfo);

        ResultCode Delete(int id, string userLevel);

        MyCompanyInfo GetCompanyOfUser(int id, int? companyId);

        ResultCode UpdateMyCompany(int id, int? companyId, MyCompanyInfo companyInfo, string userLevel);

        CompanyInfo GetCompanyInfo(int id);

        ResultCode UpdateToken(int id, int?companyId, TokenInfo tokenInfo);
    }
}
