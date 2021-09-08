using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Business.BL
{
    public class CityBO: ICityBO
    {
        #region Fields, Properties

        private readonly ICityRepository cityRepository;
        #endregion

        #region Contructor

        public CityBO(IRepositoryFactory repoFactory)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.cityRepository = repoFactory.GetRepository<ICityRepository>();
        }

        #endregion
       
        #region Methods
        public IEnumerable<CityInfo> GetList()
        {
            var cities = this.cityRepository.GetAll();
            return cities.Select(p => new CityInfo(p));
        }


        public CityInfo GetById(int id)
        {
            var city = this.cityRepository.GetById(id);
            return new CityInfo(city);
        }
        public CityInfo GetByCode(string code)
        {
            var city = this.cityRepository.GetByCode(code);
            return new CityInfo(city);
        }

        #endregion
       
    }
}