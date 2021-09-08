using Contract.Common;
using Contract.Data.DBAccessor;

namespace Contract.Business.DAO
{
    public class DbTransactionManager : IDbTransactionManager
    {
        private readonly IDbContext context;

        public DbTransactionManager(IDbContext context)
        {            
            Ensure.Argument.NotNull(context, "context");

            this.context = context;
        }

        public void BeginTransaction()
        {
            this.context.BeginTransaction();
        }

        public void Commit()
        {
            this.context.Commit();
        }

        public void Rollback()
        {
            this.context.Rollback();
        }
    }
}
