using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business.Models
{
    public class UploadEmployee
    {
        [JsonProperty("declarationCode")]
        public string DeclarationCode { get; set; }

        [JsonProperty("file")]
        public string Data { get; set; }

        public int UserUpload { get; set; }

        [JsonIgnore]
        public CompanyInfo CompanyInfo { get; set; }


        public UploadEmployee()
        {

        }
    }
}
