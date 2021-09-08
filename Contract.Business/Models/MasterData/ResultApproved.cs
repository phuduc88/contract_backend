using Contract.Business.Constants;
using Contract.Common;
using Contract.Common.Utils;
using Newtonsoft.Json;

namespace Contract.Business.Models
{
    public class ResultApproved
    {
        [JsonProperty("rowError", NullValueHandling = NullValueHandling.Ignore)]
        public ListError<ApprovedRowError> RowError { get; set; }

        [JsonIgnore()]
        public ResultCode ErrorCode { get; set; }

        [JsonIgnore()]
        public string Message { get; set; }
        public ResultApproved()
        {
            RowError = new ListError<ApprovedRowError>();
            ErrorCode = ResultCode.NoError;
            Message = MsgApiResponse.ExecuteSeccessful;
        }

    }
}
