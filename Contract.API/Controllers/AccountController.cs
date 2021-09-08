using Contract.API.Business;
using Contract.API.Constants;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Contract.API.Controllers
{
    /// <summary>
    /// This class provides APIs which handle user session: login, logout, check session alive
    /// </summary>
    [RoutePrefix("accounts")]
    public class AccountController : BaseController
    {
        #region Fields, Properties

        private static readonly Logger logger = new Logger();
        private readonly AccountBusiness business;

        #endregion // #region Fields, Properties

        #region Contructor

        public AccountController()
        {
            business = new AccountBusiness(GetBOFactory());
        }

        #endregion Contructor

        #region API methods

        /// <summary>
        /// Request login to new session
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id:int}/change-password")]
        [AllowAnonymous]
        public IHttpActionResult ChangePassword(int id, PasswordInfo passwordInfo)
        {
            if (!ModelState.IsValid || passwordInfo == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult();

            try
            {
                response.Code = this.business.ChangePassword(id, passwordInfo);
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
        [Route("{keyword?}/{skip?}/{take?}")]
        //[CustomAuthorize(Roles = UserPermission.AccountManagement_Read)]
        public IHttpActionResult FillterUser(string keyword = null, string orderby = null, string orderType = null, int skip = 0, int take = int.MaxValue)
        {
            var response = new ApiResultList<AccountDetail>();
            try
            {
                int totalRecords = 0;
                response.Code = ResultCode.NoError;
                response.Data = business.FillterUser(out totalRecords, keyword, orderby, orderType, skip, take);
                response.Message = MsgApiResponse.ExecuteSeccessful;
                Dictionary<string, string> responHeaders
                     = new Dictionary<string, string>(){
                     {CustomHttpRequestHeader.AccessControlExposeHeaders, "X-Collection-Total, X-Collection-Skip, X-Collection-Take"},
                     {CustomHttpRequestHeader.CollectionTotal, totalRecords.ToString()},
                     {CustomHttpRequestHeader.CollectionSkip, skip.ToString()},
                     {CustomHttpRequestHeader.CollectionTake, take.ToString()},
                };

                SetResponseHeaders(responHeaders);
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
        [Route("{id}")]
        //[CustomAuthorize(Roles = UserPermission.AccountManagement_Read)]
        public IHttpActionResult GetAccoutInfo(int id)
        {
            if (!ModelState.IsValid)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult<AccountInfo>();

            try
            {
                response.Code = ResultCode.NoError;
                response.Message = MsgApiResponse.ExecuteSeccessful;
                response.Data = this.business.GetAccoutInfo(id);
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
        [Route("account-default/{companyId}")]
        //[CustomAuthorize(Roles = UserPermission.AccountManagement_Read)]
        public IHttpActionResult GetAccoutDefaultOfCompany(int companyId)
        {
            if (!ModelState.IsValid)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult<AccountInfo>();

            try
            {
                response.Code = ResultCode.NoError;
                response.Message = MsgApiResponse.ExecuteSeccessful;
                response.Data = this.business.GetAccoutDefaultOfCompany(companyId);
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
        [Route("agency-account")]
        //[CustomAuthorize(Roles = UserPermission.AccountManagement_Create)]
        public IHttpActionResult CreateUserAgency(AccountInfo userInfo)
        {
            if (!ModelState.IsValid || userInfo == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult();

            try
            {
                response.Code = this.business.CreateUserDefault(userInfo, RoleInfo.SALE);
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
        [Route("customer-account")]
        //[CustomAuthorize(Roles = UserPermission.AccountManagement_Create)]
        public IHttpActionResult CreateUserCustomer(AccountInfo userInfo)
        {
            if (!ModelState.IsValid || userInfo == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult();

            try
            {
                response.Code = this.business.CreateUserDefault(userInfo, RoleInfo.CUSTOMER);
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
        [Route("")]
        //[CustomAuthorize(Roles = UserPermission.AccountManagement_Create)]
        public IHttpActionResult CreateUser(AccountInfo userInfo)
        {
            if (!ModelState.IsValid || userInfo == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult();

            try
            {
                response.Code = this.business.CreateUser(userInfo);
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
        [Route("{id:int}")]
        //[CustomAuthorize(Roles = UserPermission.AccountManagement_Update)]
        public IHttpActionResult UpdateUser(int id, AccountInfo userInfo)
        {
            if (!ModelState.IsValid || userInfo == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult();
            try
            {
                response.Code = this.business.UpdateUser(id, userInfo);
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

        [HttpDelete]
        [Route("{id:int}")]
        //[CustomAuthorize(Roles = UserPermission.AccountManagement_Delete)]
        public IHttpActionResult DeleteUser(int id)
        {
            var response = new ApiResult();
            try
            {
                response.Code = this.business.DeleteUser(id);
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


        [Route("send-mail-account")]
        [HttpPost]
        public IHttpActionResult SendVerificationCode(AccountInfo accountInfo)
        {
            ApiResult response = new ApiResult();
            try
            {
                response.Code = this.business.SendEmailAccount(accountInfo);
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
        #endregion API methods
    }
}