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
    [RoutePrefix("document-type")]
    public class DocumentTypeController : BaseController
    {
        #region Fields, Properties

        private static readonly Logger logger = new Logger();
        private readonly DocumentTypeBusiness business;

        #endregion // #region Fields, Properties

        #region Contructor

        public DocumentTypeController()
        {
            business = new DocumentTypeBusiness(GetBOFactory());
        }

        #endregion Contructor

        #region API methods
        [Route("{code?}/{name?}/{orderType?}/{orderby?}/{skip?}/{take?}")]
        [HttpGet]
        public IHttpActionResult Filter(string code = null, string name = null, string orderType = null, string orderby = null, int skip = int.MinValue, int take = int.MaxValue)
        {
            var response = new ApiResultList<DocumentTypeInfo>();
            try
            {
                response.Code = ResultCode.NoError;
                response.Data = business.Filter();
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
        [Route("{id}")]
        //[CustomAuthorize(Roles = UserPermission.MyCompany_Read)]
        public IHttpActionResult GetDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult<DocumentTypeInfo>();
            try
            {
                response.Code = ResultCode.NoError;
                response.Message = MsgApiResponse.ExecuteSeccessful;
                response.Data = this.business.GetDetail(id);
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
        //[CustomAuthorize(Roles = UserPermission.CompanyManagement_Create)]
        public IHttpActionResult Create(DocumentTypeInfo productInfo)
        {
            if (productInfo == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult();

            try
            {
                response.Code = this.business.Create(productInfo);
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
        [Route("{id}")]
        //[CustomAuthorize(Roles = UserPermission.CompanyManagement_Update)]
        public IHttpActionResult Update(int id, DocumentTypeInfo productInfo)
        {
            if (!ModelState.IsValid || productInfo == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult();

            try
            {
                response.Code = this.business.Update(id, productInfo);
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
        //[CustomAuthorize(Roles = UserPermission.CompanyManagement_Delete)]
        public IHttpActionResult Delete(int id)
        {
            var response = new ApiResult();
            try
            {
                response.Code = this.business.Delete(id);
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