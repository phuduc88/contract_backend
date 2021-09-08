using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business.DAO
{
    public interface IDbExecuteSqlCommant 
    {
       int ExcecuteCommant(string sql, object[] parameters);
    }
}
