using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;

namespace Contract.Business.Models
{
    public class EmailServerInfo
    {

        [StringTrim]
        [JsonProperty("id")]
        [DataConvert("Id")]
        public int Id { get; set; }

        [StringTrim]
        [JsonProperty("companyId")]
        [DataConvert("CompanyID")]
        public int CompanyID { get; set; }

        [StringTrim]
        [JsonProperty("autoSendEmail")]
        [DataConvert("AutoSendEmail")]
        public bool AutoSendEmail { get; set; }

        [StringTrim]
        [JsonProperty("methodSendSSL")]
        [DataConvert("MethodSendSSL")]
        public bool MethodSendSSL { get; set; }

        [StringTrim]
        [JsonProperty("smtpServer")]
        [DataConvert("SMTPServer")]
        public string SMTPServer { get; set; }

        [StringTrim]
        [JsonProperty("port")]
        [DataConvert("Port")]
        public int Port { get; set; }

        [StringTrim]
        [JsonProperty("emailServer")]
        [DataConvert("EmailServer")]
        public string EmailServer { get; set; }

        [StringTrim]
        [JsonProperty("password")]
        [DataConvert("Password")]
        public string Password { get; set; }
        
        public EmailServerInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public EmailServerInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, EmailServerInfo>(srcObject, this);
            }
        }
    }
}
