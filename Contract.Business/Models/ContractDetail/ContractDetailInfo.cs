using Contract.Business.Utils;
using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;

namespace Contract.Business.Models
{
    public class ContractDetailInfo
    {
        [JsonProperty("id")]
        [DataConvert("Id")]
        public int Id { get; set; }

        [JsonProperty("companyId")]
        [DataConvert("CompanyID")]
        public int? CompanyId { get; set; }

        [JsonProperty("customerId")]
        [DataConvert("CustomerId")]
        public int? CustomerId { get; set; }

        [StringTrim]
        [JsonProperty("productId")]
        [DataConvert("ProductId")]
        public int? ProductId { get; set; }

        [StringTrim]
        [JsonProperty("priceId")]
        [DataConvert("PriceId")]
        public int? PriceId { get; set; }

        [StringTrim]
        [JsonProperty("month")]
        [DataConvert("Month")]
        public int? Month { get; set; }

        [StringTrim]
        [JsonProperty("typePayment")]
        [DataConvert("TypePayment")]
        public int? TypePayment { get; set; }

        [StringTrim]
        [JsonProperty("bonusMonth")]
        [DataConvert("BonusMonth")]
        public int? BonusMonth { get; set; }

        [StringTrim]
        [JsonProperty("status")]
        [DataConvert("Status")]
        public int? Status { get; set; }

        [JsonProperty("total")]
        [DataConvert("Total")]
        public decimal? Total { get; set; }

        [StringTrim]
        [JsonProperty("no")]
        [DataConvert("No")]
        public string No { get; set; }

        public ContractDetailInfo()
        {

        }

        public ContractDetailInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, ContractDetailInfo>(srcObject, this);
            }
        }

    }
}
