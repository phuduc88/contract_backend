using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.DAO
{
    public interface IEmployeeSignRepository : IRepository<EmployeeSign>
    {
        /// <summary>
        /// Get an IEnumerable FileUpload 
        /// </summary>
        /// <returns><c>IEnumerable FileUpload</c> if FileUpload not Empty, <c>null</c> otherwise</returns>
        IEnumerable<EmployeeSign> Filter();

        IEnumerable<EmployeeSign> Filter(int documnetId);

        EmployeeSign GetDetail(int id);

    }
}
