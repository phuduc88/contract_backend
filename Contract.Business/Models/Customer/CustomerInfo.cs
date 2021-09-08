using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class CustomerInfo
    {
        [DataConvert("CompanySID")]
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("salaryAreaId")]
        [DataConvert("SalaryAreaId")]
        public int? SalaryAreaId { get; set; }

        [DataConvert("SalaryAreaCode")]
        [JsonProperty("salaryAreaCode")]
        public string SalaryAreaCode { get; set; }

        [StringTrim]
        [DataConvert("ContractCode")]
        [JsonProperty("ContractCode")]
        public string ContractCode { get; set; }

        [DataConvert("CompanyName")]
        [StringTrim]
        [JsonProperty("name")]
        public string CompanyName { get; set; }

        [DataConvert("TaxCode")]
        [JsonProperty("tax")]
        public string TaxCode { get; set; }

        [DataConvert("Address")]
        [JsonProperty("address")]
        public string Address { get; set; }

        [DataConvert("AddressRegister")]
        [JsonProperty("addressRegister")]
        public string AddressRegister { get; set; }

        [JsonProperty("delegate")]
        [DataConvert("Delegate")]
        public string Delegate { get; set; }

        [JsonProperty("submissionType")]
        [DataConvert("SubmissionType")]
        public int? SubmissionType { get; set; }

        [JsonProperty("tel")]
        [DataConvert("Tel1")]
        public string Tel { get; set; }

        [JsonProperty("fax")]
        [DataConvert("Fax")]
        public string Fax { get; set; }

        [DataConvert("BankAccount")]
        [JsonProperty("bankAccount")]
        public string BankAccount { get; set; }

        [DataConvert("AccountHolder")]
        [JsonProperty("accountHolder")]
        public string AccountHolder { get; set; }

        [JsonProperty("bankName")]
        [DataConvert("BankName")]
        public string BankName { get; set; }

        [JsonProperty("ContractDepartmentCode")]
        [DataConvert("ContractDepartmentCode")]
        public string ContractDepartmentCode { get; set; }

        [JsonProperty("ContractDepartmentName")]
        [DataConvert("ContractDepartmentName")]
        public string ContractDepartmentName { get; set; }

        [StringTrim]
        [JsonProperty("personContact")]
        [DataConvert("PersonContact")]
        public string PersonContact { get; set; }

        [DataConvert("Position")]
        [StringTrim]
        [JsonProperty("position")]
        public string Position { get; set; }

        [StringTrim]
        [JsonProperty("mobile")]
        [DataConvert("Mobile")]
        public string Mobile { get; set; }

        [DataConvert("Email")]
        [StringTrim]
        [JsonProperty("email")]
        public string Email { get; set; }

        [DataConvert("Active")]
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("website")]
        [DataConvert("WebSite")]
        public string WebSite { get; set; }

        [DataConvert("HasAccount")]
        [JsonProperty("hasAccount")]
        public bool HasAccount { get; set; }

        [JsonIgnore]
        [DataConvert("Level_Customer")]
        public string Level { get; set; }

        [JsonIgnore]
        public UserSessionInfo SessionInfo { get; set; }

        [JsonProperty("cityId")]
        [DataConvert("CityId")]
        public int? CityId { get; set; }

        [JsonProperty("cityCode")]
        [DataConvert("CityCode")]
        public string CityCode { get; set; }

        [JsonProperty("city")]
        public CityInfo City { get; set; }

        [JsonProperty("paymentMethodCode")]
        [DataConvert("PaymentMethodCode")]
        public string PaymentMethodCode { get; set; }

        [JsonIgnore]
        [DataConvert("CompanyID")]
        public int CompanyId { get; set; }


        public CustomerInfo()
        {

        }
        public CustomerInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, CustomerInfo>(srcObject, this);
            }
        }

        public CustomerInfo(object srcObject, object srcCity)
            : this(srcObject)
        {
            this.City = srcCity != null ? new CityInfo(srcCity) : new CityInfo();
        }

    }
}
