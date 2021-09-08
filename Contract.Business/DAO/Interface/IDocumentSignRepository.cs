using Contract.Business.Models;
using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.DAO
{
    public interface IDocumentSignRepository : IRepository<DocumentSign>
    {
        /// <summary>
        /// Get an IEnumerable FileUpload 
        /// </summary>
        /// <returns><c>IEnumerable FileUpload</c> if FileUpload not Empty, <c>null</c> otherwise</returns>
        IEnumerable<DocumentSign> Filter();

        IEnumerable<DocumentSign> Filter(ConditionSearchDocument  condition);

        DocumentSign GetDetail(int id);

    }
}
