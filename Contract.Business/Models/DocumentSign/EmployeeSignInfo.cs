using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contract.Business.Models
{
    public class EmployeeSignInfo
    {
        [JsonProperty("id")]
        [DataConvert("Id", ThrowExceptionIfSourceNotExist = false)]
        public int Id { get; set; }

        [JsonProperty("documentSingId")]
        [DataConvert("DocumentSingId", ThrowExceptionIfSourceNotExist = false)]
        public int? DocumentSingId { get; set; }

        [JsonIgnore]
        public int? ThreadedSignDocumentId { get; set; }

        [JsonProperty("orderSign")]
        [DataConvert("OrderSign", ThrowExceptionIfSourceNotExist = false)]
        public int? OrderSign { get; set; }       

        [JsonProperty("receptionEmail")]
        [DataConvert("ReceptionEmail", ThrowExceptionIfSourceNotExist = false)]
        public bool ReceptionEmail { get; set; }

        [JsonProperty("receptionFileCopy")]
        [DataConvert("ReceptionFileCopy", ThrowExceptionIfSourceNotExist = false)]
        public bool ReceptionFileCopy { get; set; }

        [JsonProperty("groupType")]
        [DataConvert("GroupType", ThrowExceptionIfSourceNotExist = false)]
        public int? GroupType { get; set; }

        [JsonProperty("groupName")]
        [DataConvert("GroupName", ThrowExceptionIfSourceNotExist = false)]
        public string GroupName { get; set; }

        [JsonProperty("email")]
        [DataConvert("Email", ThrowExceptionIfSourceNotExist = false)]
        public string Email { get; set; }

        [JsonProperty("taxCode")]
        [DataConvert("TaxCode", ThrowExceptionIfSourceNotExist = false)]
        public string TaxCode { get; set; }

        [JsonProperty("orders")]
        [DataConvert("Orders", ThrowExceptionIfSourceNotExist = false)]
        public int? Orders { get; set; }

        [JsonProperty("adrress")]
        [DataConvert("Adrress", ThrowExceptionIfSourceNotExist = false)]
        public string Adrress { get; set; }

        [JsonProperty("name")]
        [DataConvert("Name", ThrowExceptionIfSourceNotExist = false)]
        public string Name { get; set; }

        [JsonProperty("employeesSignDetail")]
        public List<EmployeeSignDetailInfo> EmployeesSignDetail { get; set; }

        public EmployeeSignInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public EmployeeSignInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, EmployeeSignInfo>(srcObject, this);
            }
        }
    }
}
