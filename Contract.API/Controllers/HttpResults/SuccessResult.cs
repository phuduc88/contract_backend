using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;

namespace Contract.API.Controllers.Results
{
    public class SuccessResult<T> : CommonResult 
        where T : class
    {
        #region  Fields, Properties

        public T Object { get; set; }
        
        #endregion

        #region Contructor

        public SuccessResult(HttpRequestMessage request, HttpStatusCode httpStatusCode, T t) 
            : base(request, httpStatusCode)
        {
            this.Object = t;
        }
        
        #endregion

        #region Methods

        protected override HttpResponseMessage CreateResponse()
        {
            var response = base.CreateResponse();
            response.Content = new StringContent(GetJsonContent());

            return response;
        }

       private string GetJsonContent()
        {
            try
            {
                var content = JsonConvert.SerializeObject(this.Object);

                return content;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        
        #endregion
    }
}