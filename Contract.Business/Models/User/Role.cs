using Contract.Business.Constants;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class Role
    {
        [JsonProperty("roleName")]
        [DataConvert("RoleName")]
        public string RoleName { get; set; }

        [JsonProperty("defaultUrl")]
        [DataConvert("DefaultPage")]
        public string DefaultPage { get; set; }

        [JsonProperty("permission")]
        public List<string> Permissions { get; set; }

        [JsonProperty("level")]
        [DataConvert("Levels")]
        public string Level { get; set; }

         /// <summary>
        /// Default constructor
        /// </summary>
        public Role()
        {
           
        }

        public Role(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, Role>(srcObject, this);
            }
        }

        public Role(object srcObject, List<string> permissions)
            : this(srcObject)
        {
            if (permissions != null)
            {
                this.Permissions = permissions;
            }
        }
    }

}
