using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Contract.API.Controllers.Results
{
    public class ErrorResult : CommonResult
    {
        #region  Fields, Properties

        public string Message { get; set; }
        public object Data { get; set; }
        
        #endregion

        #region Contructor

        public ErrorResult(HttpRequestMessage request, HttpStatusCode httpStatusCode, string message, IDictionary<string, object> data)
            : base(request, httpStatusCode)
        {
            this.Message = message;
            if (data == null || data.Count == 0)
            {
                this.Data = "";
            }
            else
            {
                this.Data = data;
            }
        }
        
        #endregion

        #region Methods

        protected override HttpResponseMessage CreateResponse()
        {
            var response = base.CreateResponse();
            response.Content = new StringContent(GetJsonContent());

            return response;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{")
                .AppendFormat("HttpCode={0},", this.StatusCode)
                .AppendFormat("Content={0}", GetJsonContent())
                .Append("}");

            return sb.ToString();
        }

        private string GetJsonContent()
        {
            try
            {
                var dictonary = new Dictionary<string, object>();
                dictonary.Add("Message", this.Message);
                dictonary.Add("Data", this.Data);

                var content = JsonConvert.SerializeObject(dictonary);

                return content;
            }
            catch
            {
                return this.Message;
            }
        }
        
        #endregion
    }
}