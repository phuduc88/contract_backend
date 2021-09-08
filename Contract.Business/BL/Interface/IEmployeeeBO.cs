using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.BL
{
    public interface IEmployeeeBO
    {
        /// <summary>
        /// Get info of the Company
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="userLevel"></param>
        /// <returns></returns>
        EmployeeInfo GetEmployeeInfo(int id);

        IEnumerable<EmployeeInfo> Filter(ConditionSearchEmployeer condition, int skip = int.MinValue, int take = int.MaxValue);

        IEnumerable<EmployeeInfo> FilterTree(ConditionSearchEmployeer condition);


        int CountFillter(ConditionSearchEmployeer condition);

        EmployeeInfo Create(EmployeeInfo employeerInfo);


        EmployeeInfo Update(int id, EmployeeInfo employeerInfo);

        ResultCode Delete(int id);

        
    }
}
