using Contract.Business.Utils;
using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;

namespace Contract.Business.Models
{
    public class ContractOfCompany
    {
        [JsonProperty("id")]
        [DataConvert("ID")]
        public int Id { get; set; }

        [JsonProperty("companyId")]
        [DataConvert("CompanyID")]
        public int? CompanyId { get; set; }

        [StringTrim]
        [JsonProperty("contractNo")]
        [DataConvert("No")]
        public string ContractNo { get; set; }

        [StringTrim]
        [JsonProperty("numberInvoice")]
        [DataConvert("NumberInvoice")]
        public decimal? NumberInvoice { get; set; }

        [JsonProperty("contractDate")]
        [DataConvert("CreatedDate")]
        [JsonConverter(typeof(JsonDateConverterString))]
        public string ContractDate { get; set; }

        public ContractOfCompany()
        {

        }
        public ContractOfCompany(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, ContractOfCompany>(srcObject, this);
            }
        }
    }
}
