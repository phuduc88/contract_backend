using Contract.Business.Utils;
using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Contract.Common.Extensions;

namespace Contract.Business.Models
{
    public class ContractInfo
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

        [StringTrim]
        [JsonProperty("email")]
        [DataConvert("Email")]
        public string Email { get; set; }

        [JsonProperty("emailOfContract")]
        [StringTrim]
        [DataConvert("EmailOfContract")]
        public string EmailOfContract { get; set; }

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

        //[JsonProperty("logo")]
        [DataConvert("LogoFileName")]
        public string Logo { get; set; }

        [DataConvert("WebSite")]
        [JsonProperty("webSite")]
        public string WebSite { get; set; }

        [DataConvert("Level_Agencies")]
        [JsonProperty("level_Agencies")]
        public int Level_Agencies { get; set; }


        [DataConvert("ResponseResults")]
        [JsonProperty("responseResults")]
        public int ResponseResults { get; set; }

        [JsonProperty("submissionType")]
        [DataConvert("SubmissionType")]
        public int SubmissionType { get; set; }

        [DataConvert("GroupCode")]
        [JsonProperty("groupCode")]
        public string GroupCode { get; set; }

        [DataConvert("GroupName")]
        [JsonProperty("groupName")]
        public string GroupName { get; set; }

        [JsonProperty("cityId")]
        [DataConvert("CityId")]
        public int? CityId { get; set; }

        [StringTrim]
        [JsonProperty("cityCode")]
        [DataConvert("CityCode", ThrowExceptionIfSourceNotExist = false)]
        public string CityCode { get; set; }

        [DataConvert("City.Name", ThrowExceptionIfSourceNotExist = false)]
        [StringTrim]
        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [DataConvert("DistrictId")]
        [JsonProperty("districtId")]
        public int? DistrictId { get; set; }

        [StringTrim]
        [JsonProperty("wardsCode")]
        [DataConvert("WardsCode")]
        public string WardsCode { get; set; }

        [JsonProperty("wardsId")]
        [DataConvert("WardsId")]
        public int? WardsId { get; set; }

        [DataConvert("DistrictCode")]
        [StringTrim]
        [JsonProperty("districtCode")]
        public string DistrictCode { get; set; }

        [StringTrim]
        [JsonProperty("subjectsCard")]
        [DataConvert("SubjectsCard")]
        public int? SubjectsCard { get; set; }

        [JsonProperty("companyId")]
        [DataConvert("CompanyID")]
        public int? CompanyID { get; set; }

        [JsonIgnore]
        [DataConvert("Level_Customer")]
        public string Level { get; set; }

        [StringTrim]
        [JsonProperty("paymentMethodCode")]
        [DataConvert("PaymentMethodCode")]
        public string PaymentMethodCode { get; set; }

        [DataConvert("MaxEmployee", ThrowExceptionIfSourceNotExist = false)]
        [StringTrim]
        [JsonProperty("maxEmployee")]
        public int MaxEmployee { get; set; }

        [DataConvert("PrivateKey")]
        [JsonProperty("privateKey")]
        public string PrivateKey { get; set; }

        [JsonProperty("vendorToken")]
        [DataConvert("VendorToken")]
        public string VendorToken { get; set; }

        [JsonProperty("fromDate")]
        [DataConvert("FromDate")]
        [JsonConverter(typeof(JsonDateConverterString))]
        public DateTime? FromDate { get; set; }

        [DataConvert("Expired")]
        [JsonConverter(typeof(JsonDateConverterString))]
        [JsonProperty("expired")]
        public DateTime? Expired { get; set; }

        [DataConvert("IsFirst")]
        [JsonProperty("isFirst")]
        public bool IsFirst { get; set; }

        [DataConvert("HasToken")]
        [JsonProperty("hasToken")]
        public bool HasToken { get; set; }

        [DataConvert("CompanyType")]
        [JsonProperty("companyType")]
        public string CompanyType { get; set; }

        [DataConvert("Note")]
        [JsonProperty("note")]
        public string Note { get; set; }

        [DataConvert("AddressReception")]
        [JsonProperty("addressReception")]
        public string AddressReception { get; set; }

        [DataConvert("AuthorityNo")]
        [JsonProperty("authorityNo")]
        public string AuthorityNo { get; set; }

        [DataConvert("AuthorityDate")]
        [JsonConverter(typeof(JsonDateConverterString))]
        [JsonProperty("authorityDate")]
        public DateTime? AuthorityDate { get; set; }

        [JsonProperty("isSiwchVendor")]
        public bool IsSiwchVendor { get; set; }

        [JsonProperty("calculationType")]
        [DataConvert("CalculationType")]
        public int CalculationType { get; set; }

        [JsonProperty("objectType")]
        [DataConvert("ObjectType")]
        public string ObjectType { get; set; }

        [JsonProperty("coefficient")]
        [DataConvert("Coefficient")]
        public decimal Coefficient { get; set; }

        [JsonProperty("interestCalculation")]
        [DataConvert("InterestCalculation")]
        public bool InterestCalculation { get; set; }

        [JsonProperty("files")]
        public List<CompanyFileUploadInfo> FilesUpload { get; set; }

        [JsonProperty("contractDetail")]
        public ContractDetailInfo ContractDetail { get; set; }

        public ContractInfo()
        {

        }
        public ContractInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, ContractInfo>(srcObject, this);
            }
        }

    }
}
