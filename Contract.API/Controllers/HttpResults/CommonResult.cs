using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Contract.API.Controllers.Results
{
    public class CommonResult : IHttpActionResult
    {
        #region  Fields, Properties

        private HttpStatusCode _statusCode;
        protected HttpStatusCode StatusCode
        {
            get { return this._statusCode; }
            set { this._statusCode = value; }
        }

        private HttpRequestMessage _request;
        protected HttpRequestMessage Request
        {
            get { return this._request; }
            set { this._request = value; }
        }
        
        #endregion

        #region Contructor

        public CommonResult(HttpRequestMessage request, HttpStatusCode httpStatusCode)
        {
            this.Request = request;
            this.StatusCode = httpStatusCode;
        }
        
        #endregion

        #region Methods

        protected virtual HttpResponseMessage CreateResponse()
        {
            return this.Request.CreateResponse(this.StatusCode);
        }

        public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = this.CreateResponse();
            return Task.FromResult(response);
        }
        
        #endregion
    }
}