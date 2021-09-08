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
    public class ProductInfo
    {
        [JsonProperty("id")]
        [DataConvert("Id")]
        public int? Id { get; set; }

        [DataConvert("Code")]
        [StringTrim]
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        [DataConvert("Name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        [DataConvert("Type")]
        public string Type { get; set; }


        public ProductInfo()
        {

        }

        public ProductInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, ProductInfo>(srcObject, this);
            }
        }

    }
}
