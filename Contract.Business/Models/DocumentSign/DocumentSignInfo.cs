using Contract.Business.Utils;
using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class DocumentSignInfo
    {
        [JsonProperty("id")]
        [DataConvert("Id")]
        public int Id { get; set; }

        [JsonProperty("companyId")]
        [DataConvert("CompanyId")]
        public int? CompanyId { get; set; }

        [JsonProperty("documentType")]
        [DataConvert("DocumentType")]
        public int? DocumentType { get; set; }

        [JsonProperty("status")]
        [DataConvert("Status")]
        public int? Status { get; set; }

        [JsonProperty("currentStep")]
        [DataConvert("CurrentStep")]
        public int CurrentStep { get; set; }

        [JsonProperty("userCreate")]
        [DataConvert("UserCreate")]
        public int? UserCreate { get; set; }

        [JsonProperty("employeesSign")]
        public List<EmployeeSignInfo> EmployeesSign { get; set; }

        [JsonProperty("filesSign")]
        public List<FileSignInfo> FilesSign { get; set; }

        [JsonProperty("email")]
        [DataConvert("Email")]
        public string Email { get; set; }

        [JsonProperty("senderName")]
        [DataConvert("SenderName")]
        public string SenderName { get; set; }

        [JsonProperty("active")]
        [DataConvert("Active")]
        public bool? Active { get; set; }

        [JsonProperty("myselfSign")]
        [DataConvert("MyselfSign")]
        public bool MyselfSign { get; set; }

        [JsonProperty("dateCreate")]
        [JsonConverter(typeof(JsonDateConverterStringFull))]
        [DataConvert("DateCreate")]
        public DateTime DateCreate { get; set; }

        [JsonIgnore]
        public string ThreadedSignId { get; set; }


        public DocumentSignInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public DocumentSignInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, DocumentSignInfo>(srcObject, this);
            }
        }
    }
}
