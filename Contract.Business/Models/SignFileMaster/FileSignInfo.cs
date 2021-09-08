using Contract.Data.Utils;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class FileSignInfo
    {
        [JsonProperty("id")]
        [DataConvert("Id")]
        public int Id { get; set; }

        [JsonProperty("documentSignId")]
        [DataConvert("DocumentSignId")]
        public int? DocumentSignId { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("fileSourceType")]
        [DataConvert("FileSourceType")]
        public string FileSourceType { get; set; }

        [JsonProperty("fileConvertType")]
        [DataConvert("FileConvertType")]
        public string FileConvertType { get; set; }

        [JsonProperty("fileName")]
        [DataConvert("FileName")]
        public string FileName { get; set; }

        [JsonProperty("fileSize")]
        [DataConvert("FileSize")]
        public int FileSize { get; set; }

        [JsonProperty("fileConvert")]
        [DataConvert("FileConvert")]
        public string FileConvert { get; set; }

        [JsonProperty("numberPage")]
        [DataConvert("NumberPage")]
        public int? NumberPage { get; set; }

        [JsonProperty("orders")]
        [DataConvert("Orders")]
        public int? Orders { get; set; }

        [JsonProperty("status")]
        [DataConvert("Status")]
        public int Status { get; set; }

        [JsonProperty("companyId")]
        [DataConvert("CompanyId")]
        public int CompanyId { get; set; }

        [JsonProperty("userCreate")]
        public int UserCreate { get; set; }

        [JsonProperty("fileNameSave")]
        [DataConvert("FileNameSave")]
        public string FileNameSave { get; set; }

        public FileSignInfo()
        {
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public FileSignInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, FileSignInfo>(srcObject, this);
            }
        }
    }
}
