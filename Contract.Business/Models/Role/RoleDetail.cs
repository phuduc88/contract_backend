using Contract.Data.Utils;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class RoleDetail
    {
        [JsonProperty("id")]
        [DataConvert("UserRoleSID")]
        public int Id { get; set; }

        [JsonProperty("name")]
        [DataConvert("RoleName")]
        public string Name { get; set; }

        public RoleDetail()
        {

        }

        public RoleDetail(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, RoleDetail>(srcObject, this);
            }
        }
    }
}
