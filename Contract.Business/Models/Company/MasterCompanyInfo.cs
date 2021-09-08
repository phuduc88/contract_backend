using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class MasterCompanyInfo
    {
        [JsonProperty("id")]
        [DataConvert("CompanySID")]
        public int Id { get; set; }

        [StringTrim]
        [JsonProperty("name")]
        [DataConvert("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("tax")]
        [DataConvert("TaxCode")]
        public string TaxCode { get; set; }

        [StringTrim]
        [JsonProperty("email")]
        [DataConvert("Email")]
        public string Email { get; set; }

        [StringTrim]
        [JsonProperty("personContact")]
        [DataConvert("PersonContact")]
        public string PersonContact { get; set; }

        [StringTrim]
        [JsonProperty("delegate")]
        [DataConvert("Delegate")]
        public string Delegate { get; set; }

        [StringTrim]
        [JsonProperty("website")]
        [DataConvert("WebSite")]
        public string WebSite { get; set; }

        [StringTrim]
        [JsonProperty("tel")]
        [DataConvert("Tel1")]
        public string Tel1 { get; set; }

        public MasterCompanyInfo()
        {

        }

        /// <summary>
        /// Constructor current Company object by copying data in the specified object
        /// </summary>
        /// <param name="srcObject">Source object</param>
        public MasterCompanyInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, MasterCompanyInfo>(srcObject, this);
            }
        }
    }
}
