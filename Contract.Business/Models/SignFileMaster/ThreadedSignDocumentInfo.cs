using Contract.Data.Utils;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class ThreadedSignDocumentInfo
    {
        [JsonProperty("id")]
        [DataConvert("Id", ThrowExceptionIfSourceNotExist = false)]
        public int Id { get; set; }

        [JsonProperty("companyId")]
        [DataConvert("CompanyId", ThrowExceptionIfSourceNotExist = false)]
        public int? CompanyId { get; set; }

        [JsonProperty("documentTypeId")]
        [DataConvert("DocumentTypeId", ThrowExceptionIfSourceNotExist = false)]
        public int DocumentTypeId { get; set; }

        [JsonProperty("groupType")]
        [DataConvert("GroupType")]
        public int? GroupType { get; set; }

        [JsonProperty("receptionEmail")]
        [DataConvert("ReceptionEmail")]
        public bool? ReceptionEmail { get; set; }

        [JsonProperty("receptionFileCopy")]
        [DataConvert("ReceptionFileCopy")]
        public bool? ReceptionFileCopy { get; set; }

        [JsonProperty("groupName")]
        [DataConvert("GroupName")]
        public string GroupName { get; set; }

        [JsonProperty("email")]
        [DataConvert("Email")]
        public string Email { get; set; }

        [JsonProperty("taxCode")]
        [DataConvert("TaxCode")]
        public string TaxCode { get; set; }

        [JsonProperty("orders")]
        [DataConvert("Orders")]
        public int Orders { get; set; }

        [JsonProperty("adrress")]
        [DataConvert("Adrress")]
        public string Adrress { get; set; }

        [JsonProperty("name")]
        [DataConvert("Name")]
        public string Name { get; set; }

        [JsonIgnore()]
        public int UserActionId { get; set; }

        public ThreadedSignDocumentInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public ThreadedSignDocumentInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, ThreadedSignDocumentInfo>(srcObject, this);
            }
        }
    }
}
