using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class AccountInfo
    {
        [JsonProperty("id")]
        [DataConvert("UserSID")]
        public int UserSID { get; set; }

        [DataConvert("UserName")]
        [JsonProperty("name")]
        [StringTrim]
        public string UserName { get; set; }

        [JsonProperty("loginId")]
        [DataConvert("UserID")]
        public string UserID { get; set; }

        [DataConvert("CompanySID")]
        [JsonProperty("companyId")]
        public int? CompanyId { get; set; }

        [JsonProperty("customerId")]
        public int? CustomerId { get; set; }

        [JsonProperty("email")]
        [StringTrim]
        [DataConvert("Email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        [DataConvert("Password")]
        [StringTrim]
        public string Password { get; set; }

        [JsonProperty("mobile")]
        [DataConvert("Mobile")]
        public string Mobile { get; set; }

        [JsonProperty("accountDefault")]
        [DataConvert("AccountDefault")]
        public bool? AccountDefault { get; set; }

        [JsonProperty("roleLevel")]
        public string RoleLevel { get; set; }

        [JsonProperty("active")]
        [DataConvert("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("roles")]
        public List<FunctionInfo> Roles { get; set; }

        public AccountInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public AccountInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, AccountInfo>(srcObject, this);
            }
        }
    }
}
