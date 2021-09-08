using Contract.Business.Utils;
using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;

namespace Contract.Business.Models
{
    public class EmailActiveInfo
    {

        [StringTrim]
        [JsonProperty("companyId")]
        [DataConvert("CompanyID")]
        public int CompanyID { get; set; }

        [StringTrim]
        [JsonProperty("id")]
        [DataConvert("Id")]
        public int Id { get; set; }

        [StringTrim]
        [JsonProperty("title")]
        [DataConvert("Title")]
        public string Title { get; set; }

        [StringTrim]
        [JsonProperty("emailTo")]
        [DataConvert("EmailTo")]
        public string EmailTo { get; set; }

        [StringTrim]
        [JsonProperty("content")]
        [DataConvert("ContentEmail")]
        public string ContentEmail { get; set; }

        [StringTrim]
        [JsonProperty("createdDate")]
        [JsonConverter(typeof(JsonDateConverterString))]
        [DataConvert("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [StringTrim]
        [JsonProperty("sendtedDate")]
        [JsonConverter(typeof(JsonDateConverterString))]
        [DataConvert("SendtedDate")]
        public DateTime? SendtedDate { get; set; }

        [StringTrim]
        [JsonProperty("status")]
        [DataConvert("StatusSend")]
        public int StatusSend { get; set; }

        public EmailActiveInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public EmailActiveInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, EmailActiveInfo>(srcObject, this);
            }
        }
    }
}
