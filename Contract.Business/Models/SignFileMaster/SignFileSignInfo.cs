using Contract.Data.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class SignFileSignInfo
    {
        [JsonProperty("documentType")]
        public string DocumentType { get; set; }

        
        [JsonProperty("filesSign")]
        public List<FileSignInfo> FilesSign { get; set; }


        public SignFileSignInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public SignFileSignInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, SignFileSignInfo>(srcObject, this);
            }
        }
    }
}
