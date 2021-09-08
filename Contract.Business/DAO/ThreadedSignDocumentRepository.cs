using Contract.Business.Models;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class ThreadedSignDocumentRepository : GenericRepository<ThreadedSignDocument>, IThreadedSignDocumentRepository
    {
        public ThreadedSignDocumentRepository(IDbContext context)
            : base(context)
        {
        }

        public IEnumerable<ThreadedSignDocument> Filter()
        {
            return this.dbSet.Where(p => 1 == 1);
        }

        public IEnumerable<EmployeeSignInfo> Filter(int documentId)
        {
            var document = from thread in dbSet
                           join empSing in this.context.Set<EmployeeSign>()
                           on thread.Id equals empSing.ThreadedSignDocumentId
                           where empSing.DocumentSingId == documentId
                           select new EmployeeSignInfo {
                            Id = empSing.Id,
                            DocumentSingId = documentId,
                            ThreadedSignDocumentId = thread.Id,
                            OrderSign = thread.Orders,
                            ReceptionEmail = (thread.ReceptionEmail ?? false),
                            ReceptionFileCopy = (thread.ReceptionFileCopy ?? false),
                            GroupName = thread.GroupName,
                            GroupType = thread.GroupType,
                            TaxCode = thread.TaxCode,
                            Name = thread.Name,
                            Adrress = thread.Adrress,
                            Email = thread.Email,
                           };
            return document;
        }

        public ThreadedSignDocument GetDetail(int id)
        {
            return this.dbSet.FirstOrDefault(p => p.Id == id);
        }

    }
}
