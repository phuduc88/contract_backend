using Contract.Business.Models;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Data.Entity;

namespace Contract.Business.DAO
{
    public interface IEmailActiveFileAttachRepository : IRepository<EmailActiveFileAttach>
    {
        /// <summary>
        /// Get an IEnumerable EmailActive 
        /// </summary>
        /// <returns><c>IEnumerable EmailActive</c> if City not Empty, <c>null</c> otherwise</returns>
        IEnumerable<EmailActiveFileAttach> Filter(int emailActiveId);
    }
}
