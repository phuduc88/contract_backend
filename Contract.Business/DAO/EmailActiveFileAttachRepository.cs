using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class EmailActiveFileAttachRepository : GenericRepository<EmailActiveFileAttach>, IEmailActiveFileAttachRepository
    {
        public EmailActiveFileAttachRepository(IDbContext context)
            : base(context)
        {
        }

        public IEnumerable<EmailActiveFileAttach> Filter(int emailActiveId)
        {
            var emailActives = this.dbSet.Where(p => p.EmailActiveId == emailActiveId);
            return emailActives;
        }
    }
}
