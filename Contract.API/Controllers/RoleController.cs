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
    [RoutePrefix("roles")]
    public class RoleController : BaseController
    {
        #region Fields, Properties

        private static readonly Logger logger = new Logger();
        private readonly RoleBusiness business;

        #endregion // #region Fields, Properties

        #region Contructor

        public RoleController()
        {
            business = new RoleBusiness(GetBOFactory());
        }

        #endregion Contructor

        #region API methods
        [HttpGet]
        [Route("")]
        //[CustomAuthorize(Roles = UserPermission.LoginUser_Read)]
        public IHttpActionResult GetRole()
        {
            var response = new ApiResultList<FunctionInfo>();
            try
            {
                response.Code = ResultCode.NoError;
                response.Data = business.GetList();
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
        [Route("employee")]
        //[CustomAuthorize(Roles = UserPermission.LoginUser_Read)]
        public IHttpActionResult GetFunctionOfEmployee()
        {
            var response = new ApiResultList<FunctionInfo>();
            try
            {
                response.Code = ResultCode.NoError;
                response.Data = business.GetFunctionByLevel(RoleInfo.SALE_EMPLOYER);
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