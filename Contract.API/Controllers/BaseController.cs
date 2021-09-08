using Contract.API.Business;
using Contract.API.Constants;
using Contract.API.Controllers.Results;
using Contract.Business.BL;
using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Contract.API.Controllers
{
    /// <summary>
    /// The abstract base class for all controllers
    /// </summary>
    [Authorize]
    public abstract class BaseController : ApiController
    {
        #region  Fields, Properties
        protected const string MsgInternalServerError = "An error occurred in server.";
        protected const string MsgRequestDataInvalid = "The request data is invalid.";

        private UserSessionInfo currentUser;
        public UserSessionInfo CurrentUser
        {
            get
            {
                if (this.currentUser == null)
                {
                    this.currentUser = GetCurrentUser();
                }

                return this.currentUser != null ? this.currentUser : new UserSessionInfo();
            }
        }

        private readonly IDbContext dbContext = DbContextManager.GetContext();

        #endregion

        #region Base methods

        protected IBOFactory GetBOFactory()
        {
            return new BOFactory(this.dbContext);
        }

        protected IBOFactory GetBOFactory(string nameOrSqlConnectionString)
        {
            return new BOFactory(DbContextManager.GetContext(nameOrSqlConnectionString));
        }

        protected string GetAuthenticatedToken()
        {
            return HttpUtils.GetRequestHeaderValue(this.Request, CustomHttpRequestHeader.AuthorizationToken);
        }

        #endregion

        #region Customize IHttpActionResult

        protected IHttpActionResult NoContent(HttpStatusCode code)
        {
            return new CommonResult(this.Request, code);
        }

        protected IHttpActionResult Success<T>(HttpStatusCode statusCode, T t) where T : class
        {
            return new SuccessResult<T>(this.Request, statusCode, t);
        }

        protected IHttpActionResult Error(ResultCode code, string message)
        {
            return Ok(new ApiResult(code, message));
        }

        protected IHttpActionResult Error(HttpStatusCode statusCode, string message, IDictionary<string, object> data = null)
        {
            return new ErrorResult(this.Request, statusCode, message, data);
        }

        protected IHttpActionResult Text(HttpStatusCode statusCode, string message, params object[] args)
        {
            return new TextResult(this.Request, statusCode, message, args);
        }
        #endregion

        #region Static methods

        protected static void SetResponseHeaders(Dictionary<string, string> responseHeaders)
        {
            foreach (var kvp in responseHeaders)
            {
                HttpContext.Current.Response.AppendHeader(kvp.Key, kvp.Value);
            }
        }

        protected static void SetResponseHeaders(string key, string value)
        {
            HttpContext.Current.Response.AppendHeader(key, value);
        }

        protected static IEnumerable<T> GetEmptyList<T>()
        {
            return new List<T>();
        }

        #endregion

        #region Private methods

        private UserSessionInfo GetCurrentUser()
        {
            string token = string.Empty;
            if (Request != null)
            {
                token = GetAuthenticatedToken();
            }
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            var sessionBusiness = new SessionBusiness(this.GetBOFactory());
            return sessionBusiness.GetUserSession(token);
        }

        #endregion

    }
}