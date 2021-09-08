using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class FileSignRepository : GenericRepository<FileSign>, IFileSignRepository
    {
        public FileSignRepository(IDbContext context)
            : base(context)
        {
        }

        public IEnumerable<FileSign> Filter()
        {
            return this.dbSet.Where(p => 1 == 1);
        }

        public IEnumerable<FileSign> Filter(int documentId)
        {
            return this.dbSet.Where(p => p.DocumentSignId == documentId);
        }

        public FileSign GetDetail(int id)
        {
            return this.dbSet.FirstOrDefault(p => p.Id == id);
        }

    }
}
