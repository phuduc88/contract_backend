using Newtonsoft.Json;
namespace Contract.Business.Models
{
    public class ChangePassword
    {
        [JsonProperty("password")]
        public string NewPassword { get; set; }         
    }
}
