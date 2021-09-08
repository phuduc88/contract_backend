using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.DAO
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(IDbContext context)
            : base(context)
        {
        }

        public IEnumerable<City> Fillter()
        {
            return this.dbSet.Where(p => !(p.Deleleted ?? false));
        }

        public City GetById(int id)
        {
            return this.dbSet.FirstOrDefault(p => !(p.Deleleted ?? false) && p.Id == id);
        }
        public City GetByCode(string code)
        {
            return this.dbSet.FirstOrDefault(p => !(p.Deleleted ?? false) && p.Code.Equals(code));
        }

    }
}
