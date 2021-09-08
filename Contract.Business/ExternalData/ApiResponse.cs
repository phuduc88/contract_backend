using Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business.ExternalData
{
    public class ApiResponse
    {
        public ResultCode Code { get; set; }
        public string Message { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
        where T : class
    {
        public T Data { get; set; }
    }

    public class ApiResponseList<T> : ApiResponse
        where T : class
    {
        public List<T> Data { get; set; }
    }
}
