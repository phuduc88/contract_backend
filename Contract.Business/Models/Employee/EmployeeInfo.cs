using Contract.Business.Utils;
using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Contract.Common.Extensions;

namespace Contract.Business.Models
{
    public class EmployeeInfo
    {
        [JsonProperty("id")]
        [DataConvert("Id")]
        public int Id { get; set; }

        [JsonProperty("employeeId")]
        [DataConvert("EmployeeId")]
        public int? EmployeeId { get; set; }

        [JsonProperty("companyId")]
        [DataConvert("CompanyId")]
        public int? CompanyId { get; set; }

        [JsonProperty("code")]
        [DataConvert("Code")]
        public string Code { get; set; }

        [JsonProperty("fullName")]
        [DataConvert("FullName")]
        public string FullName { get; set; }

        [JsonProperty("gender")]
        [DataConvert("Gender")]
        public int Gender { get; set; }

        [JsonProperty("email")]
        [DataConvert("Email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        [DataConvert("Mobile")]
        public string Mobile { get; set; }

        [JsonProperty("usingHSM")]
        [DataConvert("UsingHSM")]
        public bool? UsingHSM { get; set; }

        [JsonProperty("active")]
        [DataConvert("Active")]
        public bool? Active { get; set; }

        [JsonProperty("children")]
        public List<EmployeeInfo> ChildrenEmployee { get; set; }

       
        public EmployeeInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public EmployeeInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, EmployeeInfo>(srcObject, this);
            }
        }
        
    }
}
