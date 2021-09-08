using Contract.Business.Constants;
using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System;
using System.Collections.Generic;
using Contract.Common.Extensions;
using System.Linq;
using System.IO;

namespace Contract.Business.BL
{
    public class DocumentSignBO : IDocumentSignBO
    {
        #region Fields, Properties

        private readonly IDocumentSignRepository documentSignRepository;
        private readonly IFileSignRepository fileSignRepository;
        private readonly IThreadedSignDocumentRepository threadedSignDocumentRepository;
        private readonly IEmployeeSignDetailRepository employeeSignDetailRepository;
        private readonly ISignOfUserRepository signOfUserRepository;
        #endregion

        #region Contructor

        public DocumentSignBO(IRepositoryFactory repoFactory)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.documentSignRepository = repoFactory.GetRepository<IDocumentSignRepository>();
            this.fileSignRepository = repoFactory.GetRepository<IFileSignRepository>();
            this.threadedSignDocumentRepository = repoFactory.GetRepository<IThreadedSignDocumentRepository>();
            this.employeeSignDetailRepository = repoFactory.GetRepository<IEmployeeSignDetailRepository>();
            this.signOfUserRepository = repoFactory.GetRepository<ISignOfUserRepository>();
        }

        #endregion
       
        #region Methods

        public IEnumerable<DocumentSignInfo> Filter(ConditionSearchDocument condition, int skip, int take)
        {
            var documentSigns = this.documentSignRepository.Filter(condition).AsQueryable()
                .OrderBy(condition.ColumnOrder, condition.Order_Type.Equals(OrderType.Desc)).Skip(skip).Take(take).ToList();

            return documentSigns.Select(p => new DocumentSignInfo(p));
        }

        public int CountFilter(ConditionSearchDocument condition)
        {
            return this.documentSignRepository.Filter(condition).Count();
        }

        public DocumentSignInfo GetDetail(int id)
        {
            var documentSign = this.documentSignRepository.GetDetail(id);
            DocumentSignInfo documentSingInfo = new DocumentSignInfo(documentSign);
            documentSingInfo.FilesSign = this.GetFileSign(id);
            documentSingInfo.EmployeesSign = this.GetEmployeesSign(id);
            return documentSingInfo;
        }

        public DocumentSignInfo Create(DocumentSignInfo documentSignInfo)
        {
            if (documentSignInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!documentSignInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            DocumentSign documentSignNew = new DocumentSign();
            documentSignNew.CopyData(documentSignInfo);
            documentSignNew.Status = (int)DocumentStatus.New;
            documentSignNew.UserCreate = documentSignInfo.UserCreate;
            documentSignNew.Email = documentSignInfo.Email;
            documentSignNew.SenderName = documentSignInfo.SenderName;
            documentSignNew.DateCreate = DateTime.Now;
            bool result = this.documentSignRepository.Insert(documentSignNew);
            return new DocumentSignInfo(documentSignNew);

        }

        public DocumentSignInfo Update(int id, DocumentSignInfo documentSignInfo)
        {
            if (documentSignInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!documentSignInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            DocumentSign currentDocumentSign = GetDocumentSign(id);
            currentDocumentSign.CopyData(documentSignInfo);
            bool result = this.documentSignRepository.Update(currentDocumentSign);
            return new DocumentSignInfo(currentDocumentSign);
        }

        public DocumentSignInfo UpdateStep(int id, DocumentSignInfo documentSign)
        {
            var currentDocument = this.GetDocumentSign(id);
            currentDocument.CurrentStep = documentSign.CurrentStep;
            currentDocument.MyselfSign = documentSign.MyselfSign;
            this.documentSignRepository.Update(currentDocument);
            return new DocumentSignInfo(currentDocument);

        }

        public FileExport SignDocument(SignDocumentInfo signDocument)
        {
            var filesSign = GetFileSignInfo(signDocument.DocumentId);
            ImageSignInfo signOfUser = GetSignOfUse(signDocument.UserSignId, signDocument.FullPathFileOfCompany);
            List<string> filePdfDraw = new List<string>();
            foreach (var item in filesSign)
            {
               List<ImageSignInfo> ImageSigns =  GetImageSign(signOfUser, signDocument.FullPathFileOfCompany, item.Id);
               string fileDraw = PdfProcess.DrawImageToPdf(item.FileConvert, ImageSigns,  item.Id);
               filePdfDraw.Add(fileDraw);
            }
            string fileInvoicePdf = PdfProcess.MergeFile(filePdfDraw);
            FileExport fileInfo = new FileExport(fileInvoicePdf);
            return fileInfo;
        }

        public ResultCode Delete(int id)
        {
            DocumentSign currentDocumentSign = GetDocumentSign(id);
            bool result = this.documentSignRepository.Delete(currentDocumentSign);
            return result ? ResultCode.NoError : ResultCode.UnknownError;
        }
        #endregion


        private List<ImageSignInfo> GetImageSign(ImageSignInfo signOfUser, string fullPathFileAsset, int fileId)
        {
            List<ImageSignInfo> result = new List<ImageSignInfo>();
            List<EmployeeSignDetail> employeesSignDetail = this.employeeSignDetailRepository.FilterByFileSign(fileId).ToList();
            employeesSignDetail.ForEach(p => {
                result.Add(new ImageSignInfo(p, signOfUser.FullPathFile, signOfUser.Extension));
            });

            return result;
        }
        private DocumentSign GetDocumentSign(int id)
        {
            DocumentSign currentDocumentSign = this.documentSignRepository.GetDetail(id);
            if (currentDocumentSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, "Get DocumentSign fail width id");
            }

            return currentDocumentSign;
        }

        private List<FileSignInfo> GetFileSign(int documentId)
        {
            List<FileSignInfo> result = new List<FileSignInfo>();
            var filesSign = this.fileSignRepository.Filter(documentId).ToList();
            foreach (var item in filesSign)
            {
                FileSignInfo fileSignInfo = new FileSignInfo(item);
                fileSignInfo.Data = FileProcess.GetBase64StringFile(item.FileConvert);
                result.Add(fileSignInfo);
            }
            return result;
        }

        private List<EmployeeSignInfo> GetEmployeesSign(int documentId)
        {
            List<EmployeeSignInfo> result = new List<EmployeeSignInfo>();
            var employeeSignInfo = this.threadedSignDocumentRepository.Filter(documentId).ToList();
            var employeeSignDetailInfo = this.employeeSignDetailRepository.Filter(documentId).ToList();
            foreach (var item in employeeSignInfo)
            {
                item.EmployeesSignDetail = employeeSignDetailInfo.Where(p => p.EmployeeSignId == item.Id).Select(p => new EmployeeSignDetailInfo(p)).ToList();
                result.Add(item);
            }

            return result;
        }

        private List<FileSignInfo> GetFileSignInfo(int documentId)
        {
            var filesSign = this.fileSignRepository.Filter(documentId);
            var filesSignInfo = filesSign.Select(p => new FileSignInfo(p)).ToList();
            return filesSignInfo;
        }

        private ImageSignInfo GetSignOfUse(int userId, string fullPathFileAsst)
        {
            SignOfUser signOfUse = this.signOfUserRepository.GetSignOfUserDefault(userId);
            
            if (signOfUse == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, "Get Sign of user fail width id");
            }

            string fullPathFile = Path.Combine(fullPathFileAsst, userId.ToString(), signOfUse.FileName);
            if (!File.Exists(fullPathFile))
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, "Not found full path file Sing of employee");
            }

            ImageSignInfo result = new ImageSignInfo();
            result.FullPathFile = fullPathFile;
            result.Extension = signOfUse.Extension;
            return result;
        }

    }
}