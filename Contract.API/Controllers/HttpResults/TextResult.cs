using System.Net;
using System.Net.Http;
using System.Text;

namespace Contract.API.Controllers.Results
{
    public class TextResult<T> : CommonResult
    {
        #region  Fields, Properties

        public T Content { get; set; }
        
        #endregion

        #region Contructor

        public TextResult(HttpRequestMessage request, HttpStatusCode httpStatusCode, T content)
            : base(request, httpStatusCode)
        {
            this.Content = content;
        }
        
        #endregion

        #region Methods

        protected override HttpResponseMessage CreateResponse()
        {
            var response = base.CreateResponse();
            string content = string.Empty;
            if (this.Content != null)
            {
                content = this.Content.ToString();
            }
            response.Content = new StringContent(content);
            return response;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{")
                .AppendFormat("HttpCode={0},", this.StatusCode)
                .AppendFormat("Content={0}", this.Content)
                .Append("}");

            return sb.ToString();
        }
        
        #endregion
    }

    public class TextResult : TextResult<string>
    {
        #region  Fields, Properties

        private object[] arguments { get; set; }
        
        #endregion

        #region Contructor

        public TextResult(HttpRequestMessage request, HttpStatusCode httpStatusCode, string message, params object[] args)
            : base(request, httpStatusCode, message)
        {
            this.arguments = args;
        }
        
        #endregion

        #region Methods

        protected override HttpResponseMessage CreateResponse()
        {
            var response = this.Request.CreateResponse(this.StatusCode);
            var message = string.Format(this.Content, this.arguments);

            response.Content = new StringContent(message, System.Text.Encoding.Unicode);

            return response;
        }
        
        #endregion

    }
}