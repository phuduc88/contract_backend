using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class DocumentTypeRepository : GenericRepository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(IDbContext context)
            : base(context)
        {
        }

        public IEnumerable<DocumentType> Filter()
        {
            return this.dbSet.Where(p => 1 == 1);
        }

        public DocumentType GetDetail(int id)
        {
            return this.dbSet.FirstOrDefault(p => p.Id == id);
        }

    }
}
