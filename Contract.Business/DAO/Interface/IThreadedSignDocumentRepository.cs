using Contract.Business.Models;
using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.DAO
{
    public interface IThreadedSignDocumentRepository : IRepository<ThreadedSignDocument>
    {
        /// <summary>
        /// Get an IEnumerable DocumentType 
        /// </summary>
        /// <returns><c>IEnumerable DocumentType</c> if DocumentType not Empty, <c>null</c> otherwise</returns>
        IEnumerable<ThreadedSignDocument> Filter();

        IEnumerable<EmployeeSignInfo> Filter(int documentId);

        ThreadedSignDocument GetDetail(int id);

    }
}
