using Contract.Business.Constants;
using Contract.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class ConditionSearchEmailActive
    {
        public int? CompanyId { get; set; }
        public DateTime? DateFrom { get; private set; }
        public DateTime? DateTo { get; private set; }

        public int? Status { get; private set; }


        public string EmailTo { get; set; }
        public string Order_Type { get; set; }
        public string ColumnOrder { get; set; }

        public ConditionSearchEmailActive(UserSessionInfo currentUser, string emailTo,int? status, string dateFrom, string dateTo, string orderType, string orderBy)
        {
            this.EmailTo = emailTo.DecodeUrl();
            this.CompanyId = currentUser.Company.Id;
            this.DateFrom = dateFrom.DecodeUrl().ConvertDateTime();
            this.DateTo = dateTo.DecodeUrl().ConvertDateTime();
            this.Status = status;

            string macthOrderBy;
            string macthOrderType;

            ActiveEmailSortColumn.OrderByColumn.TryGetValue(orderBy.EmptyNull().ToUpperInvariant(), out macthOrderBy);
            OrderType.DcType.TryGetValue(orderType.EmptyNull().ToUpperInvariant(), out macthOrderType);

            macthOrderBy = macthOrderBy ?? ActiveEmailSortColumn.OrderByColumnDefault;
            macthOrderType = macthOrderType ?? OrderType.Desc;
            this.ColumnOrder = macthOrderBy;
            this.Order_Type = macthOrderType;
           
           
        }
    }
}
