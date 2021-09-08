using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class ChangeCompanyInfo
    {
        [DataConvert("Id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [DataConvert("CompanyId")]
        [JsonProperty("companyId")]
        public int CompanyId { get; set; }

        [JsonProperty("declarationId")]
        [DataConvert("DeclarationId")]
        public int DeclarationId { get; set; }

        [StringTrim]
        [JsonProperty("ContractDepartmentName")]
        [DataConvert("ContractDepartmentName")]
        public string ContractDepartmentName { get; set; }

        [DataConvert("ContractDepartmentCode")]
        [StringTrim]
        [JsonProperty("ContractDepartmentCode")]
        public string ContractDepartmentCode { get; set; }

        [JsonProperty("issued")]
        [DataConvert("Issued")]
        public string Issued { get; set; }

        [JsonProperty("ContractCode")]
        [StringTrim]
        [DataConvert("ContractCode")]
        public string ContractCode { get; set; }

        [StringTrim]
        [JsonProperty("name")]
        [DataConvert("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("taxCode")]
        [DataConvert("TaxCode")]
        public string TaxCode { get; set; }

       
        [JsonProperty("careers")]
        [DataConvert("Careers")]
        public string Careers { get; set; }

        [DataConvert("License")]
        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("address")]
        [DataConvert("Address")]
        public string Address { get; set; }

        [JsonProperty("addressRegister")]
        [DataConvert("AddressRegister")]
        public string AddressRegister { get; set; }

        [JsonProperty("mobile")]
        [DataConvert("Mobile")]
        public string Mobile { get; set; }

        [JsonProperty("fax")]
        [DataConvert("Fax")]
        public string Fax { get; set; }

        [StringTrim]
        [JsonProperty("email")]
        [DataConvert("Email")]
        public string Email { get; set; }

        [JsonProperty("paymentMethodCode")]
        [DataConvert("PaymentMethodCode")]
        [StringTrim]
        public string PaymentMethodCode { get; set; }

        [JsonProperty("companyType")]
        [DataConvert("CompanyType")]
        [StringTrim]
        public string CompanyType { get; set; }

        
        [StringTrim]
        [JsonProperty("note")]
        [DataConvert("Note")]
        public string Note { get; set; }

        [StringTrim]
        [DataConvert("DocumentAttached")]
        [JsonProperty("documentAttached")]
        public string DocumentAttached { get; set; }

        public ChangeCompanyInfo()
        {
        }

        public ChangeCompanyInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, ChangeCompanyInfo>(srcObject, this);
            }
        }

        public object this[string propertyName]
        {
            get
            {
                if (this.GetType().GetProperty(propertyName) != null)
                {
                    return this.GetType().GetProperty(propertyName).GetValue(this, null);
                }
                return string.Empty;
            }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
    }
}
