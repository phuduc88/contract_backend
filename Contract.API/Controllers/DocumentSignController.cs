using Contract.API.Business;
using Contract.API.Constants;
using Contract.Business;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Contract.API.Controllers
{
    /// <summary>
    /// This class provides APIs which handle user session: login, logout, check session alive
    /// </summary>
    [RoutePrefix("document-sign")]
    public class DocumentSignController : BaseController
    {
        #region Fields, Properties

        private static readonly Logger logger = new Logger();
        private readonly DocumentSignBusiness business;

        #endregion // #region Fields, Properties

        #region Contructor

        public DocumentSignController()
        {
            business = new DocumentSignBusiness(GetBOFactory());
        }

        #endregion Contructor

        #region API methods
        [HttpGet]
        [Route("{dateFrom?}/{dateTo?}/{status?}/{orderby?}/{orderType?}/{skip?}/{take?}")]
        //[CustomAuthorize(Roles = UserPermission.CompanyManagement_Read)]
        public IHttpActionResult Fillter(string dateFrom = null, string dateTo = null, int? status = null, string orderType = null, string orderby = null, int skip = 0, int take = int.MaxValue)
        {
            var response = new ApiResultList<DocumentSignInfo>();
            try
            {
                int totalRecords = 0;
                response.Code = ResultCode.NoError;
                response.Data = business.Filter(out totalRecords, dateFrom, dateTo, status, orderby, orderType, skip, take);
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
        //[CustomAuthorize(Roles = UserPermission.MyCompany_Read)]
        public IHttpActionResult GetDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult<DocumentSignInfo>();
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
        [Route("employee-sing/{id}")]
        //[CustomAuthorize(Roles = UserPermission.CompanyManagement_Update)]
        public IHttpActionResult Update(int id, DocumentSignInfo documentSign)
        {
            if (documentSign == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult<DocumentSignInfo>();

            try
            {
                response.Code = ResultCode.NoError;
                response.Data = this.business.CreateThreadSignDocument(id, documentSign);
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
        [Route("signature-position/{id}")]
        //[CustomAuthorize(Roles = UserPermission.CompanyManagement_Update)]
        public IHttpActionResult CreateCreateSignaturPosition(int id, DocumentSignInfo documentSign)
        {
            if (documentSign == null)
            {
                return Error(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var response = new ApiResult<DocumentSignInfo>();

            try
            {
                response.Code = ResultCode.NoError;
                response.Data = this.business.CreateSignaturPositionDocument(id, documentSign);
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


        [HttpPost]
        [Route("upload/{documentType}")]
        //[CustomAuthorize(Roles = UserPermission.MyCompany_Update)]
        public IHttpActionResult UploadFileSign(int documentType)
        {
            var response = new ApiResult<DocumentSignInfo>();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var fileUploadInfo = FileUploadInfo.FromRequest(httpRequest);
                response.Data = this.business.Create(fileUploadInfo, documentType);
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

        [HttpPost]
        [Route("upload-appen/{documentId}")]
        //[CustomAuthorize(Roles = UserPermission.MyCompany_Update)]
        public IHttpActionResult UploadAppenFileSign(int documentId)
        {
            var response = new ApiResult<DocumentSignInfo>();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var fileUploadInfo = FileUploadInfo.FromRequest(httpRequest);
                response.Data = this.business.Update(documentId, fileUploadInfo);
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

        //[HttpPost]
        //[Route("sign/{documentId}")]
        ////[CustomAuthorize(Roles = UserPermission.MyCompany_Update)]
        //public IHttpActionResult SingDocument(int documentId)
        //{
        //    var response = new ApiResult();
        //    try
        //    {
        //        response.Code = this.business.SignDocument(documentId);
        //        response.Message = MsgApiResponse.ExecuteSeccessful;
        //    }
        //    catch (BusinessLogicException ex)
        //    {
        //        response.Code = ex.ErrorCode;
        //        response.Message = ex.Message;
        //        logger.Error(this.CurrentUser.UserId, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Code = ResultCode.UnknownError;
        //        response.Message = MsgInternalServerError;
        //        logger.Error(this.CurrentUser.UserId, ex);
        //    }

        //    return Ok(response);
        //}

        [HttpGet]
        [Route("dowload-sign-temp/{id}")]
        [ResponseType(typeof(DocumentFile))]
        //[CustomAuthorize(Roles = UserPermission.InvoiceManagement_Read)]
        public IHttpActionResult DownloadSignTem(int id)
        {
            try
            {
                FileExport fileInfo = this.business.SignDocument(id);
                if (!File.Exists(fileInfo.FullPathFileName))
                {
                    return NotFound();
                }

                DocumentFile file = new DocumentFile()
                {
                    FilePath = fileInfo.FullPathFileName,
                    FileName = fileInfo.FileName,
                };

                SetResponseHeaders("Content-Disposition", "inline; filename=" + fileInfo.FileName);
                return Ok(file);
            }
            catch (Exception ex)
            {
                logger.Error(this.CurrentUser.UserId, ex);
                return Error(HttpStatusCode.InternalServerError, "Error occured while read file");
            }
        }

        #endregion API methods
    }
}