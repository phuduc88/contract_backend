using Contract.Business.Utils;
using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class AccountDetail
    {
        [JsonProperty("id")]
        [DataConvert("UserSID")]
        public int UserSID { get; set; }

        [JsonProperty("loginId")]
        [DataConvert("UserID")]
        public string UserID { get; set; }

        [StringTrim]
        [JsonProperty("name")]
        [DataConvert("UserName")]
        public string UserName { get; set; }

        [JsonProperty("companyId")]
        [DataConvert("CompanySID")]
        public int? CompanyId { get; set; }

        [StringTrim]
        [JsonProperty("email")]
        [DataConvert("Email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        [DataConvert("Mobile")]
        public string Mobile { get; set; }

        [StringTrim]
        [JsonProperty("active")]
        [DataConvert("IsActive")]
        public bool IsActive { get; set; }

        [StringTrim]
        [JsonProperty("deleted")]
        [DataConvert("Deleted")]
        public bool Deleted { get; set; }

        [StringTrim]
        [JsonProperty("created")]
        [JsonConverter(typeof(JsonDateConverterString))]
        [DataConvert("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [StringTrim]
        [JsonProperty("roleId")]
        [DataConvert("UserRoleSID")]
        public int UserRoleSID { get; set; }

        public AccountDetail()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public AccountDetail(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, AccountDetail>(srcObject, this);
            }
        }

    }
}
