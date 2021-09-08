using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Contract.Data.DBAccessor
{
    public interface IDbContext : IDisposable
    {
        DbChangeTracker ChangeTracker { get; }
        IDbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T t) where T : class;
        IEnumerable<T> ExecWithStoreProcedure<T>(string query, params object[] parameters);

        int SaveChanges();

        int ExecuteSqlCommmant(string sql, object[] parameters);
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
