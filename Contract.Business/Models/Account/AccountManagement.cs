using Contract.Business.Utils;
using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class AccountManagement
    {

        [JsonProperty("loginId")]
        [DataConvert("UserID")]
        public string UserID { get; set; }

        [StringTrim]
        [JsonProperty("password")]
        [DataConvert("Password")]
        public string Password { get; set; }

        
        public AccountManagement()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public AccountManagement(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, AccountManagement>(srcObject, this);
            }
        }

    }
}
