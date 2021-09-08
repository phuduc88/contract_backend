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
    public class MyCompanyInfo
    {
        [JsonProperty("id")]
        [DataConvert("CompanySID")]
        public int Id { get; set; }

        [DataConvert("SalaryAreaId")]
        [JsonProperty("salaryAreaId")]
        public int? SalaryAreaId { get; set; }

        [JsonProperty("salaryAreaCode")]
        [DataConvert("SalaryAreaCode")]
        public string SalaryAreaCode { get; set; }

        [StringTrim]
        [JsonProperty("ContractCode")]
        [DataConvert("ContractCode")]
        public string ContractCode { get; set; }

        [JsonProperty("issued")]
        [DataConvert("Issued")]
        public string Issued { get; set; }

        [DataConvert("Careers")]
        [JsonProperty("careers")]
        public string Careers { get; set; }

        [JsonProperty("license")]
        [DataConvert("License")]
        public string License { get; set; }

        [StringTrim]
        [JsonProperty("name")]
        [DataConvert("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("address")]
        [DataConvert("Address")]
        public string Address { get; set; }

        [DataConvert("AddressRegister")]
        [JsonProperty("addressRegister")]
        public string AddressRegister { get; set; }

        [DataConvert("TaxCode")]
        [JsonProperty("taxCode")]
        public string TaxCode { get; set; }

        [JsonProperty("email")]
        [StringTrim]
        [DataConvert("Email")]
        public string Email { get; set; }

        [JsonProperty("personContact")]
        [StringTrim]
        [DataConvert("PersonContact")]
        public string PersonContact { get; set; }

        [StringTrim]
        [DataConvert("Position")]
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("tel")]
        [DataConvert("Tel1")]
        public string Tel { get; set; }

        [DataConvert("Mobile")]
        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [DataConvert("Fax")]
        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("delegate")]
        [DataConvert("Delegate")]
        public string Delegate { get; set; }

         
        [JsonProperty("bankAccount")]
        [DataConvert("BankAccount")]
        public string BankAccount { get; set; }

        [DataConvert("AccountHolder")]
        [JsonProperty("accountHolder")]
        public string AccountHolder { get; set; }

        [DataConvert("BankName")]
        [JsonProperty("bankName")]
        public string BankName { get; set; }

        [JsonProperty("ContractDepartmentCode")]
        [DataConvert("ContractDepartmentCode")]
        public string ContractDepartmentCode { get; set; }

        [JsonProperty("ContractDepartmentName")]
        [DataConvert("ContractDepartmentName")]
        public string ContractDepartmentName { get; set; }

        [DataConvert("Description")]
        [JsonProperty("description")]
        [StringTrim]
        public string Description { get; set; }

        [JsonProperty("logo")]
        [DataConvert("LogoFileName")]
        public string Logo { get; set; }

        [DataConvert("WebSite")]
        [JsonProperty("webSite")]
        public string WebSite { get; set; }

        [JsonProperty("account", NullValueHandling = NullValueHandling.Ignore)]
        public CompanyAccount Account { get; set; }

        [DataConvert("Level_Agencies")]
        [JsonProperty("level_Agencies")]
        public int Level_Agencies { get; set; }

        [DataConvert("EmailOfContract")]
        [JsonProperty("emailOfContract")]
        public string EmailOfContract { get; set; }

        public MyCompanyInfo()
        {

        }

        public MyCompanyInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, MyCompanyInfo>(srcObject, this);
            }
        }

        public MyCompanyInfo(object srcObject, object srcAccount)
            : this(srcObject)
        {
            this.Account = srcAccount != null ? new CompanyAccount(srcAccount) : new CompanyAccount();
        }

    }
}
