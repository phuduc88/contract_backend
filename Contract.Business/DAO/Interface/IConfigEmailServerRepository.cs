using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.DAO
{
    public interface IConfigEmailServerRepository : IRepository<EmailServer>
    {
        /// Get an config email server by company Id.
        /// </summary>
        /// <param name="id">The condition get email server.</param>
        /// <returns><c>Operator</c> if Id on database, <c>null</c> otherwise</returns>
        EmailServer GetByCompany(int companyId);

        IEnumerable<EmailServer> GetList(int companyId);
    }
}
