using Contract.Business.Utils;
using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class EmployeeInfomation
    {
        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("maxEmployee")]
        public int MaxEmployee { get; set; }
        
        public EmployeeInfomation()
        {
        }

        public EmployeeInfomation(int order)
             : this()
        {
            this.Order = (order + 1);
        }

    }
}
