using Contract.Data.Utils;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class DocumentTypeInfo
    {
        [JsonProperty("id")]
        [DataConvert("Id")]
        public int Id { get; set; }

        [JsonProperty("documentName")]
        [DataConvert("DocumentName")]
        public string DocumentName { get; set; }

        [JsonProperty("docType")]
        [DataConvert("DocType")]
        public int? DocType { get; set; }

        [JsonProperty("orders")]
        [DataConvert("Orders")]
        public int Orders { get; set; }


        public DocumentTypeInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public DocumentTypeInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, DocumentTypeInfo>(srcObject, this);
            }
        }
    }
}
