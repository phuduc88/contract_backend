using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Contract.Data.DBAccessor
{
    public partial class DataClassesDataContext : DbContext, IDbContext
    {
        private DbContextTransaction transaction;
            
        IDbSet<T> IDbContext.Set<T>()
        {
            return base.Set<T>();
        }

        //new void Dispose()
        //{
        //    base.Dispose();
        //}

        int IDbContext.SaveChanges()
        {
            return base.SaveChanges();
        }      

        public DataClassesDataContext(string sqlConnection)
            : base(sqlConnection)
        {
        }

        DbEntityEntry<T> IDbContext.Entry<T>(T t)
        {
            return base.Entry<T>(t);
        }

        public void BeginTransaction()
        {
            this.transaction = this.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
        }
        public int ExecuteSqlCommmant(string sql, object[] parameters)
        {
            return this.Database.ExecuteSqlCommand(sql, parameters);
        }

        public IEnumerable<T> ExecWithStoreProcedure<T>(string query, params object[] parameters)
        {
            return this.Database.SqlQuery<T>(query, parameters);
        }

        public void Commit()
        {
            if (this.transaction != null)
            {
                this.transaction.Commit();
                this.transaction = null;
            }
        }

        public void Rollback()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
                this.transaction = null;
            }
        }
    }
}
