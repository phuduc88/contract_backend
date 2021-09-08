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
    public class TokenInfo
    {
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

        public TokenInfo()
        {

        }

        public TokenInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, TokenInfo>(srcObject, this);
            }
        }
    }
}
