using Newtonsoft.Json;

namespace Contract.Business.Models
{
   public class PasswordInfo
    {
        [JsonProperty("currentPassword")]
        public string CurrentPassword { get; set; }

        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
    }
}
