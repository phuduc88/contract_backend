using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class EmployeeSignRepository : GenericRepository<EmployeeSign>, IEmployeeSignRepository
    {
        public EmployeeSignRepository(IDbContext context)
            : base(context)
        {
        }

        public IEnumerable<EmployeeSign> Filter()
        {
            return this.dbSet.Where(p => 1 == 1);
        }

        public EmployeeSign GetDetail(int id)
        {
            return this.dbSet.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<EmployeeSign> Filter(int documnetId)
        {
            return this.dbSet.Where(p => p.DocumentSingId == documnetId);
        }
    }
}
