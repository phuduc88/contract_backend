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
    [RoutePrefix("employees")]
    public class EmployeeController : BaseController
    {
        #region Fields, Properties

        private static readonly Logger logger = new Logger();
        private readonly EmployeeBusiness business;

        #endregion

        #region Contructor

        public EmployeeController()
        {
            business = new EmployeeBusiness(GetBOFactory());
        }

        #endregion

        #region API methods
        [HttpGet]
        [Route("{orderby?}/{orderType?}/{skip?}/{take?}")]
        //[CustomAuthorize(Roles = UserPermission.CompanyManagement_Read)]
        public IHttpActionResult Fillter(string fullName = null, string birthday = null, string code = null, string ContractCode = null, string ContractNo = null, string identityCar = null, int? gender = null, string hospitalFirstRegistName = null, string orderType = null, string orderby = null, int skip = 0, int take = int.MaxValue)
        {
            var response = new ApiResultList<EmployeeInfo>();
            try
            {
                int totalRecords = 0;
                response.Code = ResultCode.NoError;
                response.Data = business.Filter(out totalRecords, orderby, orderType, skip, take);
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
        [Route("tree")]
        //[CustomAuthorize(Roles = UserPermission.CompanyManagement_Read)]
        public IHttpActionResult GetTreeEmployee()
        {
            var response = new ApiResultList<EmployeeInfo>();
            try
            {
                 
                response.Code = ResultCode.NoError;
                response.Data = business.GetEmployeeTree();
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
        //[CustomAuthorize(Roles = UserPermission.CompanyManagement_Read)]
        public IHttpActionResult GetEmployeeInfo(int id)
        {
            if (!ModelState.IsValid)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult<EmployeeInfo>();

            try
            {
                response.Code = ResultCode.NoError;
                response.Message = MsgApiResponse.ExecuteSeccessful;
                response.Data = this.business.GetById(id);
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
        public IHttpActionResult Create(EmployeeInfo employeerInfo)
        {
            if (employeerInfo == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult<EmployeeInfo>();

            try
            {
                response.Code = ResultCode.NoError;
                response.Message = MsgApiResponse.ExecuteSeccessful;
                response.Data = this.business.Create(employeerInfo);
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
        public IHttpActionResult Update(int id, EmployeeInfo employeerInfo)
        {
            if (employeerInfo == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult<EmployeeInfo>();
            try
            {
                response.Code = ResultCode.NoError;
                response.Message = MsgApiResponse.ExecuteSeccessful;
                response.Data = this.business.Update(id, employeerInfo);
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
       // [CustomAuthorize(Roles = UserPermission.CompanyManagement_Delete)]
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
        #endregion
    }
}