using Contract.Common.Extensions;
using System.Data;
namespace Contract.Business.Models
{
    public class EmployeeSearch
    {
        public string FullName { get; set; }

        public string ContractCode { get; set; }


        public string IdentityCar { get; set; }


        public int? Gender { get; set; }

        public CompanyInfo CompanyInfo { get; set; }
       
         public EmployeeSearch(CompanyInfo companyInfo, string fullName, string ContractCode, string identityCar, int? gender)
         {
            this.FullName = fullName.DecodeUrl();
            this.CompanyInfo = companyInfo;
            this.ContractCode = ContractCode.DecodeUrl();
            this.IdentityCar = identityCar.DecodeUrl();
            this.Gender = gender;
        }
    }
}
