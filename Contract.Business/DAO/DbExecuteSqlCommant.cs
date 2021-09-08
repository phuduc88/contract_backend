using Contract.Business.Models;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
using Contract.Common.Extensions;
using Contract.Business.Constants;

namespace Contract.Business.DAO
{
    public class DbExecuteSqlCommant : IDbExecuteSqlCommant
    {
        private readonly IDbContext context;

        public DbExecuteSqlCommant(IDbContext context)
        {            
            this.context = context;
        }

        public int ExcecuteCommant(string sql, object[] parameters)
        {
            return this.context.ExecuteSqlCommmant(sql, parameters);
        }
      
    }
}
