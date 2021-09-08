using Newtonsoft.Json;

namespace Contract.Business.Models
{
   public class ValidatorResult
    {
        [JsonProperty("result")]
        public string Result { get; set; }

    }
}
