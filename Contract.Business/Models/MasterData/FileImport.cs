using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class FileImport
    {
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonIgnore()]
        public int UserActionId { get; set; }
    }
}
