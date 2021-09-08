using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business.Models
{
    public class GroupEmployeerInfo
    {
        [JsonProperty("id")]
        public string Code { get; set; }

        [JsonProperty("groupCode")]
        public string GroupCode { get; set; }

        [JsonProperty("groupName")]
        public string GroupName { get; set; }

        [JsonProperty("employees")]
        public List<EmployeeInfo> Employees { get; set; }

        public GroupEmployeerInfo()
        {
        }
        public GroupEmployeerInfo(string code, string groupName, List<EmployeeInfo> employees)
            : this()
        {
            this.Code = code;
            this.GroupCode = code;
            this.GroupName = groupName;
            this.Employees = employees;
        }

    }
}
