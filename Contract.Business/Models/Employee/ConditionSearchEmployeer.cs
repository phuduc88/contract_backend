using Contract.Business.Constants;
using Contract.Common.Extensions;
using System.Collections.Generic;
using System.Linq;
namespace Contract.Business.Models
{
    public class ConditionSearchEmployeer
    {
        public string FullName { get; private set; }

        public string Code { get; set; }
        public string ContractCode { get; set; }

        public string ContractNo { get; set; }

        public string IdentityCar { get; set; }

        public string Birthday { get; set; }

        public int? Gender { get; set; }

        public string HospitalFirstRegistName { get; set; }

        public List<string> Keywords { get; private set; }

        public int Type { get; private set; }

        public string Order_By { get; set; }

        public string Order_Type { get; set; }

        public int CompanyId { get; set; }

        public UserSessionInfo CurrentUser { get; set; }

        public ConditionSearchEmployeer(UserSessionInfo currentUser, string fullName,string birthday, string code, string ContractCode, string ContractNo, string identityCar, int? gender, string hospitalFirstRegistName,string orderBy, string orderType)
            : this(currentUser, fullName, string.Empty, orderBy, orderType)
        {
            this.Code = code.DecodeUrl();
            this.Birthday = birthday.DecodeUrl();
            this.ContractCode = ContractCode.DecodeUrl();
            this.ContractNo = ContractNo.DecodeUrl();
            this.IdentityCar = identityCar.DecodeUrl();
            this.HospitalFirstRegistName = hospitalFirstRegistName.DecodeUrl();
            this.Gender = gender;
        }

        public ConditionSearchEmployeer(UserSessionInfo currentUser, string fullName, string type, string orderBy, string orderType)
        {
            this.FullName = StringExtensions.DecodeUrl(fullName);
            this.Keywords = this.GetKeyWords();
            string macthOrderBy;
            string macthOrderType;
            EmployeerSortColumn.OrderByColumn.TryGetValue(orderBy.EmptyNull().ToUpperInvariant(), out macthOrderBy);
            OrderType.DcType.TryGetValue(orderType.EmptyNull().ToUpperInvariant(), out macthOrderType);

            macthOrderBy = macthOrderBy ?? EmployeerSortColumn.OrderByColumnDefault;
            macthOrderType = macthOrderType ?? OrderType.Desc;
            this.Order_By = macthOrderBy;
            this.Order_Type = macthOrderType;
            this.CurrentUser = currentUser;
            this.CompanyId = GetCompanyIdOfUser(currentUser);
        }

        private int GetCompanyIdOfUser(UserSessionInfo currentUser)
        {
            int conpanyId = 0;
            if (currentUser.Company.Id.HasValue)
            {
                conpanyId = currentUser.Company.Id.Value;
            }

            return conpanyId;
        }

        private List<string> GetKeyWords()
        {
            if (this.FullName.IsNullOrEmpty())
            {
                return new List<string>();
            }

            return this.FullName.Split(';').ToList();
        }
    }
}
