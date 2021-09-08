using Contract.Business.Constants;
using Contract.Common.Extensions;

namespace Contract.Business.Models
{
    public class ConditionSearchCompany
    {
       public string Keyword { get; private set; }
        public string Order_By { get; set; }
        public string Order_Type { get; set; }

        public int CompanyId { get; set; }

        public UserSessionInfo CurrentUser { get; set; }

        public ConditionSearchCompany(UserSessionInfo currentUser, string keyword, string orderBy, string orderType) 
        {
            this.Keyword = keyword.DecodeUrl();
            string macthOrderBy;
            string macthOrderType;
            CompanySortColumn.OrderByColumn.TryGetValue(orderBy.EmptyNull().ToUpperInvariant(), out macthOrderBy);
            OrderType.DcType.TryGetValue(orderType.EmptyNull().ToUpperInvariant(), out macthOrderType);

            macthOrderBy = macthOrderBy ?? CompanySortColumn.OrderByColumnDefault;
            macthOrderType = macthOrderType ?? OrderType.Desc;
            this.Order_By = macthOrderBy;
            this.Order_Type = macthOrderType;
            this.CurrentUser = currentUser;
            this.CompanyId = GetCompanyIdOfUser(currentUser);
          
        }

        private int GetCompanyIdOfUser(UserSessionInfo currentUser)
        {
            int idCompanyOfUser = 0;
            if (currentUser.Company.Id.HasValue)
            {
                idCompanyOfUser = currentUser.Company.Id.Value;
            }

            return idCompanyOfUser;
        }
    }
}
