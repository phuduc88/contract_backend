using Contract.Business.Constants;
using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using Contract.Common.Extensions;
using Contract.Business.Utils;
using System;

namespace Contract.Business.Models
{
    public class CompanyInfo
    {
        [JsonProperty("id")]
        [DataConvert("CompanySID")]
        public int? Id { get; set; }

        [StringTrim]
        [JsonProperty("name")]
        [DataConvert("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("address")]
        [DataConvert("Address")]
        public string Address { get; set; }

        [JsonProperty("addressRegister")]
        [DataConvert("AddressRegister")]
        public string AddressRegister { get; set; }

        
        [JsonProperty("taxCode")]
        [DataConvert("TaxCode")]
        public string TaxCode { get; set; }

        [JsonProperty("email")]
        [StringTrim]
        [DataConvert("Email")]
        public string Email { get; set; }

        [JsonProperty("personContact")]
        [StringTrim]
        [DataConvert("PersonContact")]
        public string PersonContact { get; set; }

        [JsonProperty("tel")]
        [DataConvert("Tel1")]
        public string Tel { get; set; }

        [JsonProperty("mobile")]
        [DataConvert("Mobile")]
        public string Mobile { get; set; }

        [JsonProperty("fax")]
        [DataConvert("Fax")]
        public string Fax { get; set; }

        [JsonProperty("delegate")]
        [DataConvert("Delegate")]
        public string Delegate { get; set; }

        [DataConvert("BankAccount")]
        [JsonProperty("bankAccount")]
        public string BankAccount { get; set; }

        [JsonProperty("accountHolder")]
        [DataConvert("AccountHolder")]
        public string AccountHolder { get; set; }

        [DataConvert("BankName")]
        [JsonProperty("bankName")]
        public string BankName { get; set; }

        [StringTrim]
        [JsonProperty("description")]
        [DataConvert("Description")]
        public string Description { get; set; }

        [JsonProperty("logo")]
        [DataConvert("LogoFileName")]
        public string Logo { get; set; }

        [DataConvert("WebSite")]
        [JsonProperty("webSite")]
        public string WebSite { get; set; }

        [DataConvert("Position")]
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("account", NullValueHandling = NullValueHandling.Ignore)]
        public CompanyAccount Account { get; set; }

        [JsonProperty("level_Agencies")]
        [DataConvert("Level_Agencies")]
        public int Level_Agencies { get; set; }

        [JsonProperty("emailOfContract")]
        [DataConvert("EmailOfContract")]
        public string EmailOfContract { get; set; }


        public CompanyInfo()
        {

        }

        public CompanyInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, CompanyInfo>(srcObject, this);
            }
        }

        public CompanyInfo(object srcObject, object srcAccount)
            : this(srcObject)
        {
            this.Account = srcAccount != null ? new CompanyAccount(srcAccount) : new CompanyAccount();
        }

    }
}
