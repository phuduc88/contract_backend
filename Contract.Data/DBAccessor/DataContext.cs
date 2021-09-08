using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFRSCaseSearchDBServer.Data.DBAccessor
{
    public sealed class DataContext
    {

        private static readonly IDbContext context = new DataClassesDataContext();
        
        static DataContext()
        {
        }

        private DataContext()
        {
        }


        public static IDbContext CurrentContext
        {
            get
            {
                return context;
            }
        }
    }
}
