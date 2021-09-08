using Contract.Business.Utils;
using Newtonsoft.Json;
using System;

namespace Contract.Business.Models
{
    public class ContractMaster
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("companyId")]
        public int? CompanyId { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("taxCode")]
        public string  TaxtCode { get; set; }

        [JsonProperty("no")]
        public string ContractNo { get; set; }

        [JsonProperty("numberInvoice")]
        public decimal? NumberInvoice { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("downloaded")]
        public bool Downloaded { get; set; }

        [JsonIgnore]
        public int CustomerId { get; set; }


        [JsonProperty("createdDate")]
        [JsonConverter(typeof(JsonDateConverterString))]
        public DateTime? DateCreate { get; set; }

        [JsonProperty("agencyName")]
        public string AgencyName { get; set; }

        [JsonProperty("agencyMobile")]
        public string AgencyMobile { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("priceName")]
        public string PriceName { get; set; }

        [JsonProperty("numberMonth")]
        public int NumberMonth { get; set; }

        [JsonProperty("hasToken")]
        public bool HasToken { get; set; }

        public ContractMaster()
        {

        }
    }
}
