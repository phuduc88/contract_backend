using Contract.API.Business;
using Contract.API.Constants;
using Contract.Business.BL;
using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Contract.API.MessageHandler
{
    public class AuthenticationHandler : DelegatingHandler
    {
        class ByPassApi : IEquatable<ByPassApi>
        {
            #region Fields, Properties

            public readonly string LocalRequestUrl;
            public readonly HttpMethod HttpMethod;
            
            #endregion

            #region Contructor

            public ByPassApi(string url, HttpMethod httpMethod)
            {
                this.LocalRequestUrl = url;
                this.HttpMethod = httpMethod;
            }
            
            #endregion

            #region Methods

            public bool Equals(ByPassApi other)
            {
                return other.LocalRequestUrl.IndexOf(this.LocalRequestUrl) > -1 && this.HttpMethod.Equals(other.HttpMethod);
            }
            
            #endregion
        }

        #region Fields, Properties

        private readonly SessionBusiness sessionBusiness;
        private static readonly Logger logger = new Logger();
        private static readonly List<ByPassApi> listByPassApi = new List<ByPassApi>()
        {
            new ByPassApi("/login", HttpMethod.Post),
            new ByPassApi("/client-login", HttpMethod.Post),
            new ByPassApi("/reset-password", HttpMethod.Post),
            new ByPassApi("/session", HttpMethod.Get),
            new ByPassApi("/hangfire", HttpMethod.Get),
            new ByPassApi("/favicon.ico", HttpMethod.Get),
        };
        
        #endregion

        #region Contructor

        public AuthenticationHandler()
        {
            sessionBusiness = new SessionBusiness(new BOFactory(DbContextManager.GetContext()));
        }
        
        #endregion

        #region Methods

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            try
            {
                if (CheckAuthorize(requestMessage))
                {
                    string token = HttpUtils.GetRequestHeaderValue(requestMessage, CustomHttpRequestHeader.AuthorizationToken);
                    string tokenConnecion = HttpUtils.GetRequestHeaderValue(requestMessage, CustomHttpRequestHeader.AuthorizationToken_Connection);
                    UserSessionInfo userInfo = null;
                    if (string.IsNullOrWhiteSpace(token) && string.IsNullOrWhiteSpace(tokenConnecion))
                    {
                        return AuthenticationResult(HttpStatusCode.BadRequest);
                    }

                    if (!string.IsNullOrWhiteSpace(tokenConnecion))
                    {
                        LoginInfo loginInfo = getLoginInfo(tokenConnecion);
                        userInfo = this.sessionBusiness.Login(loginInfo);
                    }
                    else
                    {
                        ResultCode resultCode = this.sessionBusiness.CheckUserSession(token);
                        if (resultCode != ResultCode.SessionAlive)
                        {
                            return AuthenticationResult(HttpStatusCode.Unauthorized);
                        }

                        userInfo = this.sessionBusiness.GetUserSession(token);
                        if (userInfo == null)
                        {
                            return AuthenticationResult(HttpStatusCode.Unauthorized);
                        }
                    }

                    IPrincipal principal = new GenericPrincipal(new GenericIdentity(userInfo.UserId), userInfo.RoleUser.Permissions.ToArray());
                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                }

                return base.SendAsync(requestMessage, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An unknown error occurred.");
                return AuthenticationResult(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Authentication result client call API
        /// </summary>
        /// <param name="httpcode"></param>
        /// <returns></returns>
        private static Task<HttpResponseMessage> AuthenticationResult(HttpStatusCode httpcode)
        {
            var httpResponseMessage = new HttpResponseMessage(httpcode);
            var taskCompletionSource = new TaskCompletionSource<HttpResponseMessage>();
            taskCompletionSource.SetResult(httpResponseMessage);
            return taskCompletionSource.Task;
        }

        private static bool CheckAuthorize(HttpRequestMessage requestMessage)
        {
            var requestApi = new ByPassApi(requestMessage.RequestUri.LocalPath, requestMessage.Method);

            return !listByPassApi.Contains(requestApi);
        }

        private LoginInfo getLoginInfo(string tokenConnection)
        {
            String[] tokenInfo = tokenConnection.Split(';');
            if (tokenInfo.Length > 1)
            {
                return new LoginInfo()
                {
                    UserId = tokenInfo[0],
                    Password = tokenInfo[1],
                };
            }

            return new LoginInfo();
        }
        
        #endregion
    }
}

