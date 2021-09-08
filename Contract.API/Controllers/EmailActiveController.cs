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
    [RoutePrefix("emailActive")]
    public class EmailActiveController : BaseController
    {
        #region Fields, Properties

        private static readonly Logger logger = new Logger();
        private readonly EmailActiveBusiness business;

        #endregion

        #region Contructor

        public EmailActiveController()
        {
            business = new EmailActiveBusiness(GetBOFactory());
        }

        #endregion

        #region API methods
        [HttpGet]
        [Route("{emailTo?}/{status?}/{dateFrom?}/{dateTo?}/{orderType?}/{orderby?}/{skip?}/{take?}")]
        //[CustomAuthorize(Roles = UserPermission.AgenciesManagement_Read)]
        public IHttpActionResult FillterEmailActive(string emailTo = null, int? status = null, string dateFrom = null, string dateTo = null, string orderType = null, string orderby = null, int skip = int.MinValue, int take = int.MaxValue)
        {
            var response = new ApiResultList<EmailActiveInfo>();
            try
            {
                int totalRecords = 0;
                response.Code = ResultCode.NoError;
                response.Data = business.Filter(out totalRecords, emailTo, status, dateFrom, dateTo, orderby, orderType, skip, take);
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
        //[CustomAuthorize(Roles = UserPermission.AgenciesManagement_Read)]
        public IHttpActionResult GetEmailActiveInfo(int id)
        {
            if (!ModelState.IsValid)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult<EmailActiveInfo>();

            try
            {
                response.Code = ResultCode.NoError;
                response.Message = MsgApiResponse.ExecuteSeccessful;
                response.Data = this.business.GetEmailActive(id);
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
        [Route("sendemail/{id}")]
        //[CustomAuthorize(Roles = UserPermission.AgenciesManagement_Create)]
        public IHttpActionResult SendEmailActive(int id, EmailActiveInfo emailActive)
        {
            //if (!ModelState.IsValid || sellerInfo == null)
            //{
            //    return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            //}

            var response = new ApiResult();

            try
            {
                response.Code = business.SendEmailActive(id, emailActive);
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

        #endregion
    }
}