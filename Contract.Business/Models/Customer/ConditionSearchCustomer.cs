using Contract.Business.Constants;
using Contract.Common.Extensions;

namespace Contract.Business.Models
{
    public class ConditionSearchCustomer
    {
        public string CustomerName { get; private set; }

        public string Delegate { get; private set; }
        public string TaxCode { get; private set; }
        public string Tel { get; private set; }
        public int? Status { get; private set; }
        public int? CompanyID { get; private set; }
        public int? ParentCompanyId { get; private set; }
        public string Order_By { get; set; }
        public string Order_Type { get; set; }

        public ConditionSearchCustomer(UserSessionInfo currentUser, string customerName, string taxCode, string nameDelegate, string tel, int? status, string orderBy, string orderType)
        {
            this.CustomerName = customerName.DecodeUrl();
            this.TaxCode = taxCode.DecodeUrl();
            this.Delegate = nameDelegate.DecodeUrl();
            this.Tel = tel.DecodeUrl();
            this.Status = status;
            this.CompanyID = currentUser.Company.Id;

            string macthOrderBy;
            string macthOrderType;
            CustomerSortColumn.OrderByColumn.TryGetValue(orderBy.EmptyNull().ToUpperInvariant(), out macthOrderBy);
            OrderType.DcType.TryGetValue(orderType.EmptyNull().ToUpperInvariant(), out macthOrderType);

            macthOrderBy = macthOrderBy ?? CustomerSortColumn.OrderByColumnDefault;
            macthOrderType = macthOrderType ?? OrderType.Desc;
            this.Order_By = macthOrderBy;
            this.Order_Type = macthOrderType;
        }
    }
}
