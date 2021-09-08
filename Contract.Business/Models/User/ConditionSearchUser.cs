using Contract.Business.Constants;
using System.Linq;
using Contract.Common.Extensions;

namespace Contract.Business.Models
{
    public class ConditionSearchUser
    {
        public string Keyword { get; private set; }
        public int? CompanyId { get; private set; }
        public string[] ChildLevels { get; set; }
        public string Order_By { get; set; }
        public string Order_Type { get; set; }
        public string LanguageCode { get; set; }

        public ConditionSearchUser(UserSessionInfo currentUser, string keyword, string orderBy, string orderType) 
        {
            this.Keyword = keyword.DecodeUrl();
            this.CompanyId = currentUser.Company.Id;
            string macthOrderBy;
            string macthOrderType;
            LoginUserInfo.OrderByColumn.TryGetValue(orderBy.EmptyNull().ToUpperInvariant(), out macthOrderBy);
            OrderType.DcType.TryGetValue(orderType.EmptyNull().ToUpperInvariant(), out macthOrderType);

            macthOrderBy = macthOrderBy ?? LoginUserInfo.OrderByColumnDefault;
            macthOrderType = macthOrderType ?? OrderType.Desc;
            this.Order_By = macthOrderBy;
            this.Order_Type = macthOrderType;
        }
    }
}
