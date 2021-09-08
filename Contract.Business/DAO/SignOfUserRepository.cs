using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class SignOfUserRepository : GenericRepository<SignOfUser>, ISignOfUserRepository
    {
        public SignOfUserRepository(IDbContext context)
            : base(context)
        {
        }

        public IEnumerable<SignOfUser> Filter()
        {
            return this.dbSet.Where(p => 1 == 1);
        }

        public SignOfUser GetDetail(int id)
        {
            return this.dbSet.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<SignOfUser> FilterUseDefault(int id, int companyId)
        {
            return this.dbSet.Where(p => p.CompanyId == companyId && p.Id != id);
        }

        public SignOfUser GetSignOfUserDefault(int userId)
        {
            return this.dbSet.FirstOrDefault(p => (p.UseDefault ?? false) && p.UserId == userId);
        }
    }
}
