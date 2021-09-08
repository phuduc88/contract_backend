using Contract.Business.Utils;
using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class SignOfUserInfo
    {
        [JsonProperty("id")]
        [DataConvert("Id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        [DataConvert("UserId")]
        public int UserId { get; set; }

        [JsonProperty("isDraw")]
        [DataConvert("IsDraw")]
        public bool IsDraw { get; set; }

        [JsonProperty("companyId")]
        [DataConvert("CompanyId")]
        public int? CompanyId { get; set; }

        [JsonProperty("useDefault")]
        [DataConvert("UseDefault")]
        public bool? UseDefault { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("fileName")]
        [DataConvert("FileName")]
        public string FileName { get; set; }

        [JsonProperty("dateCreate")]
        [JsonConverter(typeof(JsonDateConverterStringFull))]
        [DataConvert("DateCreate")]
        public DateTime DateCreate { get; set; }

        public SignOfUserInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public SignOfUserInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, SignOfUserInfo>(srcObject, this);
            }
        }
    }
}
