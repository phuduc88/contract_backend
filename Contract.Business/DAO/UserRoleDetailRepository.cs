using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class UserRoleDetailRepository : GenericRepository<UserRoleDetail>, IUserRoleDetailRepository
    {
        public UserRoleDetailRepository(IDbContext context)
            : base(context)
        {
        }

        // <summary>
        /// Get list UserRole by level.
        /// </summary>
        /// <param name="level">The condition get UserRole.</param>
        /// <returns><c>list UserRole </c> if Levels on database, <c>null</c> otherwise</returns>
        public UserRoleDetail GetById(int id)
        {
            return dbSet.FirstOrDefault(p => p.Id == id);
        }
    }
}
