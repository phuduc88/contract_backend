using Contract.Business.BL;
using Contract.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace Contract.API.Business
{
    public class CityBusiness : BaseBusiness
    {
        #region Fields, Properties

        private ICityBO cityBO;

        #endregion Fields, Properties

        #region Contructor

        public CityBusiness(IBOFactory boFactory)
        {
            this.cityBO = boFactory.GetBO<ICityBO>();
        }

        #endregion Contructor

        #region Methods

        public IEnumerable<CityInfo> Filter()
        {
           return this.cityBO.GetList();
        }

        public CityInfo GetById(int id)
        {
            return this.cityBO.GetById(id);
        }
        #endregion
    }
}