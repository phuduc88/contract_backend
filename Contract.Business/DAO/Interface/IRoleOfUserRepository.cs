using Contract.Business.Models;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Data.Entity;

namespace Contract.Business.DAO
{
    public interface IRoleOfUserRepository : IRepository<RoleOfUser>
    {
        /// <summary>
        /// Get list RoleOfUser by level.
        /// </summary>
        /// <param name="level">The condition get UserRole.</param>
        /// <returns><c>list RoleOfUser </c> if Levels on database, <c>null</c> otherwise</returns>
        IEnumerable<RoleOfUser> FilterByUserId(int userId);
        RoleOfUser GetRoleOfUser(int userRoleDetailId, int userId);
    }
}
