using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;

namespace Contract.Business.BL
{
    public interface IEmployeeSignBO
    {
        EmployeeSignInfo GetDetail(int id);

        EmployeeSignInfo Create(EmployeeSignInfo fileSign);

        void DeleteEployeeSing(int documentId, List<int> employeeWillBeUpdate);

        EmployeeSignInfo Update(int id, EmployeeSignInfo fileSign);

        ResultCode Delete(int id);

    }
}
