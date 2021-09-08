using Contract.Business.Models;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class DocumentSignRepository : GenericRepository<DocumentSign>, IDocumentSignRepository
    {
        public DocumentSignRepository(IDbContext context)
            : base(context)
        {
        }


        public IEnumerable<DocumentSign> Filter()
        {
            return this.dbSet.Where(p => 1 == 1);
        }

        public IEnumerable<DocumentSign> Filter(ConditionSearchDocument condition)
        {
            var documentsSign = this.dbSet.Where(p => p.CompanyId == condition.CompanyId);
            if (condition.UserId.HasValue)
            {
                documentsSign = documentsSign.Where(p => p.UserCreate == condition.UserId.Value);
            }
            if (condition.Status.HasValue)
            {
                documentsSign = documentsSign.Where(p => p.Status == condition.Status.Value);
            }

            return documentsSign;
          
        }
        public DocumentSign GetDetail(int id)
        {
            return this.dbSet.FirstOrDefault(p => p.Id == id);
        }

    }
}
