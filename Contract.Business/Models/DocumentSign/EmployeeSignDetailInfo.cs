using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class EmployeeSignDetailInfo
    {
        [JsonProperty("id")]
        [DataConvert("Id")]
        public int Id { get; set; }

        [JsonProperty("employeeSignId")]
        [DataConvert("EmployeeSignId")]
        public int? EmployeeSignId { get; set; }

        [JsonProperty("fileSignId")]
        [DataConvert("FileSignId")]
        public int? FileSignId { get; set; }

        [JsonProperty("signType")]
        [DataConvert("SignType")]
        public int? SignType { get; set; }

        [JsonProperty("page")]
        [DataConvert("Page")]
        public int? Page { get; set; }

        [JsonProperty("coordinateX")]
        [DataConvert("CoordinateX")]
        public decimal CoordinateX { get; set; }

        [JsonProperty("coordinateY")]
        [DataConvert("CoordinateY")]
        public decimal CoordinateY { get; set; }

        [JsonProperty("scale")]
        [DataConvert("Scale")]
        public decimal Scale { get; set; }

        [JsonProperty("width")]
        [DataConvert("Width")]
        public decimal Width { get; set; }

        [JsonProperty("height")]
        [DataConvert("Height")]
        public decimal Height { get; set; }

        [JsonProperty("orderLink")]
        [DataConvert("OrderLink")]
        public int? OrderLink { get; set; }


        public EmployeeSignDetailInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public EmployeeSignDetailInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, EmployeeSignDetailInfo>(srcObject, this);
            }
        }
    }
}
