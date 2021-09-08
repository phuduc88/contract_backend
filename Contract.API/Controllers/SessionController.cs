using Contract.API.Business;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using System;
using System.Web.Http;

namespace Contract.API.Controllers
{
    /// <summary>
    /// This class provides APIs which handle user session: login, logout, check session alive
    /// </summary>
    [RoutePrefix("session")]
    public class SessionController : BaseController
    {
        #region Fields, Properties

        private static readonly Logger logger = new Logger();
        private readonly SessionBusiness business;

        #endregion // #region Fields, Properties

        #region Contructor

        public SessionController()
        {
            business = new SessionBusiness(GetBOFactory());
        }

        #endregion Contructor

        #region API methods

        /// <summary>
        /// Request login to new session
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IHttpActionResult Login(LoginInfo loginInfo)
        {

            if (!ModelState.IsValid || loginInfo == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult<UserSessionInfo>();

            try
            {
                response.Code = ResultCode.NoError;
                response.Message = Authentication.LoginSuccessful;
                response.Data = this.business.Login(loginInfo);
            }
            catch (BusinessLogicException ex)
            {
                response.Code = ex.ErrorCode;
                response.Message = ex.Message;
                logger.Error(loginInfo.UserId, ex);
            }
            catch (Exception ex)
            {
                response.Code = ResultCode.UnknownError;
                response.Message = MsgInternalServerError;
                logger.Error(loginInfo.UserId, ex);
            }

            return Ok(response);
        }

        /// <summary>
        /// Request logout current session
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            string authorizeToken = GetAuthenticatedToken();
            if (string.IsNullOrWhiteSpace(authorizeToken))
            {
                return Error(ResultCode.TokenInvalid, Authentication.TokenInvalid);
            }

            var response = new ApiResult();

            try
            {
                ResultCode resultCode = this.business.Logout(authorizeToken);
                response.Code = resultCode;
                response.Message = Authentication.LogoutSuccessful;
            }
            catch (BusinessLogicException ex)
            {
                response.Code = ex.ErrorCode;
                response.Message = ex.Message;
                logger.Error(this.CurrentUser.UserId, ex);
            }
            catch (Exception ex)
            {
                response.Code = ResultCode.UnknownError;
                response.Message = MsgInternalServerError;
                logger.Error(this.CurrentUser.UserId, ex);
            }

            return Ok(response);
        }

        /// <summary>
        /// Check a session alive or not
        /// </summary>
        /// <param name="token">Session token</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{token}")]
        [AllowAnonymous]
        public IHttpActionResult CheckSessionStatus(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest();
            }

            var response = new ApiResult();

            var resultCode = ResultCode.NoError;
            try
            {
                resultCode = this.business.CheckUserSession(token);
            }
            catch (Exception ex)
            {
                response.Code = ResultCode.UnknownError;
                response.Message = ex.Message;
                logger.Error(string.Empty, ex);
            }

            switch (resultCode)
            {
                case ResultCode.SessionAlive:
                    {
                        response.Code = ResultCode.NoError;
                        response.Message = MsgApiResponse.ExecuteSeccessful;
                        break;
                    }
                case ResultCode.SessionEnded:
                    {
                        response.Code = ResultCode.TokenInvalid;
                        response.Message = "Session is timeout";
                        break;
                    }
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("reset-password")]
        [AllowAnonymous]
        public IHttpActionResult ResetPassword(ResetPassword resetPasswordInfo)
        {
            if (!ModelState.IsValid || resetPasswordInfo == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult();
            try
            {
                response.Code = this.business.ResetPassword(resetPasswordInfo) ? ResultCode.NoError : ResultCode.UnknownError;
                response.Message = MsgApiResponse.ExecuteSeccessful;
            }
            catch (BusinessLogicException ex)
            {
                response.Code = ex.ErrorCode;
                response.Message = ex.Message;
                logger.Error(this.CurrentUser.UserId, ex);
            }
            catch (Exception ex)
            {
                response.Code = ResultCode.UnknownError;
                response.Message = MsgInternalServerError;
                logger.Error(this.CurrentUser.UserId, ex);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("new-password")]
        [AllowAnonymous]
        public IHttpActionResult UpdatePassword(ChangePassword updatePasswordInfo)
        {
            if (!ModelState.IsValid || updatePasswordInfo == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult();
            try
            {
                this.business.UpdatePassword(updatePasswordInfo);
                response.Code = ResultCode.NoError;
                response.Message = MsgApiResponse.ExecuteSeccessful;
            }
            catch (BusinessLogicException ex)
            {
                response.Code = ex.ErrorCode;
                response.Message = ex.Message;
                logger.Error(this.CurrentUser.UserId, ex);
            }
            catch (Exception ex)
            {
                response.Code = ResultCode.UnknownError;
                response.Message = MsgInternalServerError;
                logger.Error(this.CurrentUser.UserId, ex);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("account-info/{token}")]
        [AllowAnonymous]
        public IHttpActionResult GetAccountInfo(string token)
        {

            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest();
            }

            var response = new ApiResult<UserSessionInfo>();

            try
            {
                response.Code = ResultCode.NoError;
                response.Message = Authentication.LoginSuccessful;
                response.Data = this.business.GetAccountInfo(token);
            }
            catch (BusinessLogicException ex)
            {
                response.Code = ex.ErrorCode;
                response.Message = ex.Message;
                logger.Error(token, ex);
            }
            catch (Exception ex)
            {
                response.Code = ResultCode.UnknownError;
                response.Message = MsgInternalServerError;
                logger.Error(token, ex);
            }

            return Ok(response);
        }


        #endregion API methods
    }
}