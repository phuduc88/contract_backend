using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IDbContext context)
            : base(context)
        {
        }

        // <summary>
        /// Get list UserRole by level.
        /// </summary>
        /// <param name="level">The condition get UserRole.</param>
        /// <returns><c>list UserRole </c> if Levels on database, <c>null</c> otherwise</returns>
        public IEnumerable<UserRole> FillterUserRole(string level)
        {
            return dbSet.Where(p => p.Levels.Equals(level));
        }


        public UserRole FillterUserRoleByLevel(string level)
        {
            return dbSet.FirstOrDefault(p => p.Levels.Equals(level));
        }

        public UserRole GetById(int id)
        {
            return dbSet.FirstOrDefault(p => p.UserRoleSID == id);
        }
    }
}
