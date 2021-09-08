using Contract.Common;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class ResetPassword
    {
        [StringTrim]
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
