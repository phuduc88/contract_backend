using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class EmployeeSignDetailRepository : GenericRepository<EmployeeSignDetail>, IEmployeeSignDetailRepository
    {
        public EmployeeSignDetailRepository(IDbContext context)
            : base(context)
        {
        }

        public IEnumerable<EmployeeSignDetail> Filter()
        {
            return this.dbSet.Where(p => 1 == 1);
        }

        public IEnumerable<EmployeeSignDetail> Filter(int documentId)
        {
            var document = from empDetail in dbSet
                           join empSing in this.context.Set<EmployeeSign>()
                           on empDetail.EmployeeSignId equals empSing.Id
                           where empSing.DocumentSingId == documentId
                           select empDetail;
            return document;
        }

        public IEnumerable<EmployeeSignDetail> FilterByEmployeeSing(int employeeSingId)
        {
            return this.dbSet.Where(p => p.EmployeeSignId == employeeSingId);
        }

        public IEnumerable<EmployeeSignDetail> FilterByFileSign(int fileId)
        {
            return this.dbSet.Where(p => p.FileSignId == fileId);
        }

        public EmployeeSignDetail GetDetail(int id)
        {
            return this.dbSet.FirstOrDefault(p => p.Id == id);
        }

    }
}
