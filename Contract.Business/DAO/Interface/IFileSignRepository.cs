using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.DAO
{
    public interface IFileSignRepository : IRepository<FileSign>
    {
        /// <summary>
        /// Get an IEnumerable FileUpload 
        /// </summary>
        /// <returns><c>IEnumerable FileUpload</c> if FileUpload not Empty, <c>null</c> otherwise</returns>
        IEnumerable<FileSign> Filter();

        IEnumerable<FileSign> Filter(int documentId);

        FileSign GetDetail(int id);

    }
}
