using Contract.Common;
using Contract.Common.Utils;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class ResultImportSheet
    {
        [JsonProperty("rowError", NullValueHandling = NullValueHandling.Ignore)]
        public ListError<ImportRowError> RowError { get; set; }

        [JsonProperty("rowSuccess")]
        public int? RowSuccess { get; set; }

        [JsonIgnore()]
        public ResultCode ErrorCode { get; set; }

         [JsonIgnore()]
        public string Message { get; set; }

        public ResultImportSheet()
        {
            RowError = new ListError<ImportRowError>();
        }

    }
}
