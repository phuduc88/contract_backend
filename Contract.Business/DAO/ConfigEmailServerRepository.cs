using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class ConfigEmailServerRepository : GenericRepository<EmailServer>, IConfigEmailServerRepository
    {
        public ConfigEmailServerRepository(IDbContext context)
            : base(context)
        {
        }

        public EmailServer GetByCompany(int companyId)
        {
            return this.dbSet.FirstOrDefault(p => p.CompanyID == companyId);
        }
        public IEnumerable<EmailServer> GetList(int companyId)
        {
            return this.dbSet.Where(p => p.CompanyID == companyId);
        }
    }
}
