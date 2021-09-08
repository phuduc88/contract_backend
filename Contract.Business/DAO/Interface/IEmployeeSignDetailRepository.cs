using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.DAO
{
    public interface IEmployeeSignDetailRepository : IRepository<EmployeeSignDetail>
    {
        /// <summary>
        /// Get an IEnumerable FileUpload 
        /// </summary>
        /// <returns><c>IEnumerable FileUpload</c> if FileUpload not Empty, <c>null</c> otherwise</returns>
        IEnumerable<EmployeeSignDetail> Filter();

        IEnumerable<EmployeeSignDetail> Filter(int documentId);

        IEnumerable<EmployeeSignDetail> FilterByEmployeeSing(int employeeSingId);

        IEnumerable<EmployeeSignDetail> FilterByFileSign(int fileId);

        EmployeeSignDetail GetDetail(int id);
    }
}
