using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;

namespace Contract.Business.BL
{
    public interface IEmployeeSignDetailBO
    {
        EmployeeSignDetailInfo GetDetail(int id);

        EmployeeSignDetailInfo Create(EmployeeSignDetailInfo fileSign);

        EmployeeSignDetailInfo Update(int id, EmployeeSignDetailInfo fileSign);

        ResultCode DeleteByDocumentId(int documentId);

        ResultCode Delete(int id);

    }
}
