using Contract.Data.Utils;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class SystemConfigInfo
    {
        [JsonProperty("filePathToken")]
        [DataConvert("FilePathToken")]
        public string FilePathToken { get; set; }

        [JsonProperty("passWord")]
        [DataConvert("PassWord")]        
        public string PassWord { get; set; }

        [JsonProperty("ivanCode")]
        [DataConvert("IVANCode")]
        public string IVANCode { get; set; }

        [JsonProperty("ivanName")]
        [DataConvert("IVANName")]
        public string IVANName { get; set; }

        [JsonProperty("ivanPass")]
        [DataConvert("IVANPass")]
        public string IVANPass { get; set; }

        [JsonProperty("modeSign")]
        [DataConvert("ModeSign")]
        public string ModeSign { get; set; }


        public SystemConfigInfo()
        {

        }

        public SystemConfigInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, SystemConfigInfo>(srcObject, this);
            }
        }

    }
}
