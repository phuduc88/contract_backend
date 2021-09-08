using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class CityInfo
    {
        [JsonProperty("identity")]
        [DataConvert("Id")]
        public int Id { get; set; }


        [StringTrim]
        [JsonProperty("id")]
        [DataConvert("Code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        [DataConvert("Name")]
        public string Name { get; set; }

        [JsonProperty("shortName")]
        [DataConvert("ShortName")]
        public string ShortName { get; set; }

        public CityInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public CityInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, CityInfo>(srcObject, this);
            }
        }
    }
}
