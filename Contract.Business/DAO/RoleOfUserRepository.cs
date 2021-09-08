using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class RoleOfUserRepository : GenericRepository<RoleOfUser>, IRoleOfUserRepository
    {
        public RoleOfUserRepository(IDbContext context)
            : base(context)
        {
        }

        // <summary>
        /// Get list RoleOfUser by level.
        /// </summary>
        /// <param name="level">The condition get RoleOfUser.</param>
        /// <returns><c>list RoleOfUser </c> if Levels on database, <c>null</c> otherwise</returns>
        public IEnumerable<RoleOfUser> FilterByUserId(int userId)
        {
            return dbSet.Where(p => p.UserSID == userId);
        }

        public RoleOfUser GetRoleOfUser(int userRoleDetailId, int userId)
        {
            return dbSet.FirstOrDefault(p => p.UserSID == userId && p.UserRoleDetailId == userRoleDetailId);
        }
    }
}
