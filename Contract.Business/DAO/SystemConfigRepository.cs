using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class SystemConfigRepository : GenericRepository<SystemConfig>, ISystemConfigRepository
    {
        public SystemConfigRepository(IDbContext context)
            : base(context)
        {
        }

        public SystemConfig GetSystemConfig()
        {
            return this.dbSet.FirstOrDefault();
        }
    }
}
