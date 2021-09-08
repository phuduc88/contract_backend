using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Data.Entity;

namespace Contract.Business.DAO
{
    public interface IMyCompanyRepository : IRepository<MyCompany>
    {
        /// <summary>
        /// Get an IEnumerable MyCompany 
        /// </summary>
        /// <returns><c>IEnumerable MyCompany</c> if MyCompany not Empty, <c>null</c> otherwise</returns>
        IEnumerable<MyCompany> GetList();

        /// <summary>
        /// Get an MyCompany by CompanySID.
        /// </summary>
        /// <param name="id">The condition get MyCompany.</param>
        /// <returns><c>Operator</c> if CompanySID on database, <c>null</c> otherwise</returns>
        MyCompany GetById(int id);

        /// <summary>
        /// Get an MyCompany by CompanySID.
        /// </summary>
        /// <param name="id">The condition get MyCompany.</param>
        /// <returns><c>Operator</c> if CompanySID on database, <c>null</c> otherwise</returns>

        /// <summary>
        /// Search Company by the filter conditions
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>

        IEnumerable<MyCompany> FilterCompany(ConditionSearchCompany condition);

        bool ContainEmail(string email);
        bool ContainTaxCode(string taxCode);

        bool ContainEmail(int companyId, string email, string levelCustomer);
        bool ContainTaxCode(int companyId, string taxCode, string levelCustomer);

        IEnumerable<MyCompany> FilterAllCustomer(ConditionSearchCustomer condition);

        IEnumerable<MyCompany> FilterCustomer(ConditionSearchCustomer condition);

        IEnumerable<MyCompany> FilterCustomer(ConditionSearchCustomer condition, string customerType);

        IEnumerable<MyCompany> FilterCustomerManagement(ConditionSearchCustomer condition);

       
    }
}
