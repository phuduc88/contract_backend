using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.DAO
{
    public interface IDocumentTypeRepository : IRepository<DocumentType>
    {
        /// <summary>
        /// Get an IEnumerable DocumentType 
        /// </summary>
        /// <returns><c>IEnumerable DocumentType</c> if DocumentType not Empty, <c>null</c> otherwise</returns>
        IEnumerable<DocumentType> Filter();

        DocumentType GetDetail(int id);

    }
}
