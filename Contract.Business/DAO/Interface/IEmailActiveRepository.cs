using Contract.Business.Models;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Data.Entity;

namespace Contract.Business.DAO
{
    public interface IEmailActiveRepository : IRepository<EmailActive>
    {
        /// <summary>
        /// Get an IEnumerable EmailActive 
        /// </summary>
        /// <returns><c>IEnumerable EmailActive</c> if City not Empty, <c>null</c> otherwise</returns>
        IEnumerable<EmailActive> Filter(ConditionSearchEmailActive condition, int skip = 0, int take = int.MaxValue);

        EmailActive GetById(int id);

        EmailActive GetByAccountId(int id);
    }
}
