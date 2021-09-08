using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Data.Entity;

namespace Contract.Business.DAO
{
    public interface IEmployeesRepository : IRepository<Employee>
    {
        /// <summary>
        /// Get an IEnumerable Employeer 
        /// </summary>
        /// <returns><c>IEnumerable Employeer</c> if Employeer not Empty, <c>null</c> otherwise</returns>
        IEnumerable<Employee> GetList();

        /// <summary>
        /// Get an Employeer by CompanySID.
        /// </summary>
        /// <param name="id">The condition get Employeer.</param>
        /// <returns><c>Operator</c> if CompanySID on database, <c>null</c> otherwise</returns>
        Employee GetById(int id);

        /// <summary>
        /// Search Company by the filter conditions
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>

        IEnumerable<Employee> Filter(ConditionSearchEmployeer condition);

        IEnumerable<Employee> FilterGroup(ConditionSearchEmployeer condition);

        IEnumerable<Employee> Filter(int companyId);

        Employee Filter(EmployeeSearch condition);

    }
}
