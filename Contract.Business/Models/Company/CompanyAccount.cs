using Contract.Data.Utils;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class CompanyAccount
    {
        [JsonProperty("id")]
        [DataConvert("UserSID")]
        public int? id { get; set; }

        [JsonProperty("userId")]
        [DataConvert("UserID")]
        public string UserId { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("email")]
        [DataConvert("Email")]
        public string Email { get; set; }

        [JsonProperty("active")]
        [DataConvert("IsActive")]
        public bool IsActive { get; set; }
        public CompanyAccount()
        {

        }

        public CompanyAccount(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, CompanyAccount>(srcObject, this);
            }
        }
    }
}
