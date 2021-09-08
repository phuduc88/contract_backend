using Contract.Common;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class LoginInfo
    {
        [StringTrim]
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
