using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;
using System.Web;

namespace Contract.Business.Models
{
    public class CompanyFileUploadInfo
    {
        [JsonProperty("id")]
        [DataConvert("Id")]
        public int Id { get; set; }

        [StringTrim]
        [JsonProperty("companyId")]
        [DataConvert("CompanyId")]
        public int? CompanyId { get; set; }

        [JsonProperty("documentName")]
        [DataConvert("DocumentName")]
        public string DocumentName { get; set; }

        [JsonProperty("fileName")]
        [DataConvert("FileName")]
        public string FileName { get; set; }

        [JsonProperty("fullPathFile")]
        [DataConvert("FullPathFile")]
        public string FullPathFile { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        public CompanyFileUploadInfo()
        {
        }

        /// <summary>
        /// Constructor current DeclarationsInfo object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public CompanyFileUploadInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, CompanyFileUploadInfo>(srcObject, this);
            }
        }
    }
}
