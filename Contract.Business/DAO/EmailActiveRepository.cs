using Contract.Business.Models;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
using Contract.Common.Extensions;
namespace Contract.Business.DAO
{
    public class EmailActiveRepository : GenericRepository<EmailActive>, IEmailActiveRepository
    {
        public EmailActiveRepository(IDbContext context)
            : base(context)
        {
        }

        public IEnumerable<EmailActive> Filter(ConditionSearchEmailActive condition, int skip = 0, int take = int.MaxValue)
        {
            var emailActives = this.dbSet.Where(p => p.CompanyID == condition.CompanyId.Value);
            if (condition.DateFrom.HasValue)
            {
                emailActives = emailActives.Where(p => p.CreatedDate >= condition.DateFrom.Value);
            }

            if (condition.DateTo.HasValue)
            {
                emailActives = emailActives.Where(p => p.CreatedDate <= condition.DateTo.Value);
            }

            if (condition.Status.HasValue)
            {
                emailActives = emailActives.Where(p => p.StatusSend == condition.Status.Value);
            }

            if (condition.EmailTo.IsNotNullOrEmpty())
            {
                emailActives = emailActives.Where(p => p.EmailTo.Contains(condition.EmailTo) || p.Title.Contains(condition.EmailTo));
            }

            return emailActives;
        }

        public EmailActive GetById(int id)
        {
           return this.dbSet.FirstOrDefault(p => p.Id == id);
        }

        public EmailActive GetByAccountId(int id)
        {
            return this.dbSet.FirstOrDefault(p => p.AccountId == id);
        }
    }
}
