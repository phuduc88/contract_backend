using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class CustomerManagementInfo
    {
        [JsonProperty("id")]
        [DataConvert("CompanySID")]
        public int? Id { get; set; }

        [StringTrim]
        [JsonProperty("name")]
        [DataConvert("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("tax")]
        [DataConvert("TaxCode")]
        public string TaxCode { get; set; }

        [JsonProperty("address")]
        [DataConvert("Address")]
        public string Address { get; set; }
        
        [JsonProperty("delegate")]
        [DataConvert("Delegate")]
        public string Delegate { get; set; }


        [JsonProperty("tel")]
        [DataConvert("Tel1")]
        public string Tel { get; set; }

        [JsonProperty("fax")]
        [DataConvert("Fax")]
        public string Fax { get; set; }

        [JsonProperty("bankAccount")]
        [DataConvert("BankAccount")]
        public string BankAccount { get; set; }

        [JsonProperty("accountHolder")]
        [DataConvert("AccountHolder")]
        public string AccountHolder { get; set; }


        [JsonProperty("bankName")]
        [DataConvert("BankName")]
        public string BankName { get; set; }

        [JsonIgnore()]
        [DataConvert("TaxDepartmentID")]
        public int? TaxDepartmentID { get; set; }

        [StringTrim]
        [JsonProperty("personContact")]
        [DataConvert("PersonContact")]
        public string PersonContact { get; set; }

        [StringTrim]
        [JsonProperty("position")]
        [DataConvert("Position")]
        public string Position { get; set; }

        [StringTrim]
        [JsonProperty("mobile")]
        [DataConvert("Mobile")]
        public string Mobile { get; set; }

        [StringTrim]
        [JsonProperty("email")]
        [DataConvert("Email")]
        public string Email { get; set; }


        [JsonProperty("active")]
        [DataConvert("Active")]
        public bool Active { get; set; }

        [JsonProperty("website")]
        [DataConvert("WebSite")]
        public string WebSite { get; set; }

        [JsonIgnore]
        [DataConvert("Level_Customer")]
        public string Level { get; set; }

        [JsonIgnore]
        public UserSessionInfo SessionInfo { get; set; }

        [JsonProperty("cityId")]
        [DataConvert("CityId")]
        public int? CityId { get; set; }

        [JsonProperty("city")]
        public CityInfo City { get; set; }

        [JsonIgnore]
        [DataConvert("CompanyID")]
        public int CompanyId { get; set; }


        [StringTrim]
        [JsonProperty("emailContract")]
        [DataConvert("EmailOfContract")]
        public string EmailContract { get; set; }

        [JsonProperty("account")]
        public List<AccountManagement> Accounts { get; set; }

        public CustomerManagementInfo()
        {

        }
        public CustomerManagementInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, CustomerManagementInfo>(srcObject, this);
            }
        }

        public CustomerManagementInfo(object srcObject, object srcCity)
            : this(srcObject)
        {
            this.City = srcCity != null ? new CityInfo(srcCity) : new CityInfo();
        }
        
    }
}
