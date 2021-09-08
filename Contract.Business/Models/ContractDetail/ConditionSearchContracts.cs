using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Business.Extensions;
using Contract.Common.Extensions;
using System;

namespace Contract.Business.Models
{
    public class ConditionSearchContracts
    {
        public int? CompanyId { get; set; }
        public string TaxCode { get; set; }
        public DateTime? DateFrom { get; private set; }
        public DateTime? DateTo { get; private set; }
        public DateTime? DatePayment { get; private set; }
        public int? ContactType { get; set; }

        public int? ParentCompanyId { get; set; }

        public int? Status { get; private set; }

        public string Order_By { get; set; }
        public string Order_Type { get; set; }

        public ConditionSearchContracts(UserSessionInfo currentUser, string taxNumber, string dateFrom, string dateTo, string datePayment, string contractType, int? status, string orderBy, string orderType)
        {
            this.CompanyId = currentUser.Company.Id;
            this.TaxCode = taxNumber.DecodeUrl();
            this.DateFrom = dateFrom.DecodeUrl().ConvertDateTime();
            this.DateTo = dateTo.DecodeUrl().ConvertDateTime();
            this.DatePayment = datePayment.DecodeUrl().ConvertDateTime();
            this.ContactType = contractType.ToInt();
            this.Status = status;

            string macthOrderBy;
            string macthOrderType;
            ContractSortColumn.OrderByColumn.TryGetValue(orderBy.EmptyNull().ToUpperInvariant(), out macthOrderBy);
            OrderType.DcType.TryGetValue(orderType.EmptyNull().ToUpperInvariant(), out macthOrderType);

            macthOrderBy = macthOrderBy ?? ContractSortColumn.OrderByColumnDefault;
            macthOrderType = macthOrderType ?? OrderType.Desc;
            this.Order_By = macthOrderBy;
            this.Order_Type = macthOrderType;
        }
    }
}
