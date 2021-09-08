using Contract.Business;
using Contract.Business.BL;
using Contract.Business.Config;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;
using System.IO;
using Contract.Common.Extensions;
using System.Linq;

namespace Contract.API.Business
{
    public class DocumentSignBusiness : BaseBusiness
    {
        #region Fields, Properties

        private IDocumentSignBO documentSignBO;
        private IFileSignBO fileSignBO;
        private IThreadedSignDocumentBO threadedSignDocumentBO;
        private IEmployeeSignBO employeeSignBO;
        private IEmployeeSignDetailBO employeeSignDetailBO;
        private PrintConfig printConfig;
        #endregion Fields, Properties

        #region Contructor

        public DocumentSignBusiness(IBOFactory boFactory)
        {
            printConfig = GetPrintConfig();
            this.documentSignBO = boFactory.GetBO<IDocumentSignBO>();
            this.fileSignBO = boFactory.GetBO<IFileSignBO>();
            this.threadedSignDocumentBO = boFactory.GetBO<IThreadedSignDocumentBO>();
            this.employeeSignBO = boFactory.GetBO<IEmployeeSignBO>();
            this.employeeSignDetailBO = boFactory.GetBO<IEmployeeSignDetailBO>();
        }

        #endregion Contructor

        #region Methods
        public IEnumerable<DocumentSignInfo> Filter(out int totalRecords, string dateFrom = null, string dateTo = null, int? status = null, string orderType = null, string orderby = null, int skip = 0, int take = int.MaxValue)
        {
            ConditionSearchDocument condition = new ConditionSearchDocument(this.CurrentUser, status, dateFrom, dateTo, orderType, orderby);
            totalRecords = this.documentSignBO.CountFilter(condition);
            return this.documentSignBO.Filter(condition, skip, take);
        }

        public DocumentSignInfo GetDetail(int id)
        {
            return this.documentSignBO.GetDetail(id);
        }

        public DocumentSignInfo Create(List<FileUploadInfo> files, int documentType)
        {
            DocumentSignInfo documentSign = CreateDocument(documentType);
            documentSign.FilesSign = CreateFileSign(files, documentSign.Id);
            return documentSign;
        }

        public DocumentSignInfo Update(int id, List<FileUploadInfo> files)
        {
            List<FileSignInfo> fileSignAppen = CreateFileSign(files, id);
            var documentSing = GetDetail(id);
            if (documentSing.FilesSign == null)
            {
                documentSing.FilesSign.AddRange(fileSignAppen);
            }
            else
            {
                documentSing.FilesSign = fileSignAppen;
            }
            return documentSing;
        }

        public DocumentSignInfo CreateThreadSignDocument(int id, DocumentSignInfo fileSign)
        {
            if (fileSign == null || fileSign.EmployeesSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            int step = fileSign.CurrentStep;
            if (fileSign.CurrentStep == (int)DocumentStep.New)
            {
                step = (int)DocumentStep.AddCustomer;
            }
            fileSign.CurrentStep = step;
            UpdateNextStepDocument(id, fileSign);
            fileSign.EmployeesSign = ProcessThreadEmployeeSignDocument(id, fileSign);
            fileSign.CurrentStep = (int)DocumentStep.Finish;
            return fileSign;
        }

        public DocumentSignInfo CreateSignaturPositionDocument(int documentId, DocumentSignInfo fileSign)
        {
            if (fileSign == null || fileSign.EmployeesSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            fileSign.CurrentStep = (int)DocumentStep.Finish;
            UpdateNextStepDocument(documentId, fileSign);
            DeleteEmployeeSingDetail(documentId);
            fileSign.EmployeesSign = CreateSignaturPosition(fileSign.EmployeesSign);
            return fileSign;
        }

        public ResultCode Delete(int id)
        {
            return this.documentSignBO.Delete(id);
        }

        public FileExport SignDocument(int documentId)
        {
            PrintConfig  printConfig = GetPrintConfig();
            SignDocumentInfo signDocumentInfo = new SignDocumentInfo();
            signDocumentInfo.DocumentId = documentId;
            signDocumentInfo.UserSignId = this.CurrentUser.Id;
            signDocumentInfo.FullPathFileOfCompany = printConfig.FullFolderAssetOfCompany;
            return documentSignBO.SignDocument(signDocumentInfo);
        }

        #endregion

        private List<FileSignInfo> CreateFileSign(List<FileUploadInfo> files, int documentId)
        {
            string folderContain = CreateFolder(documentId);
            List<FileSignInfo> result = new List<FileSignInfo>();
            int identity = 1;
            foreach (var item in files)
            {
                string extension = Path.GetExtension(item.File.FileName);
                string fileName = Path.GetFileNameWithoutExtension(item.File.FileName).StandardizeFileName();
                string fullPathFile = Path.Combine(folderContain, string.Format("{0}{1}", fileName, extension));
                item.File.SaveAs(fullPathFile);
                FileSignInfo fileSign = BuildFileSign(item, fullPathFile, extension, fileName, identity, documentId);
                result.Add(fileSign);
                identity++;
            }
            return result;
        }

        private FileSignInfo BuildFileSign(FileUploadInfo fileUpload, string fullPathFileUpload, string extension, string fileName, int identity, int documentId)
        {
            FileSignInfo fileSign = new FileSignInfo();
            fileSign.FileName = fileUpload.FileName;
            fileSign.FileConvert = File2Pdf(fullPathFileUpload, extension);
            fileSign.FileSize = fileUpload.File.ContentLength;
            fileSign.Data = FileProcess.GetBase64StringFile(fileSign.FileConvert);
            fileSign.FileSourceType = FileExtension.TypeFile[extension];
            fileSign.FileConvertType = FileExtension.TypeFile[FileExtension.Pdf];
            fileSign.FileNameSave = fileName;
            fileSign.CompanyId = GetCompanyIdOfUser();
            fileSign.Orders = identity;
            fileSign.UserCreate = this.CurrentUser.Id;
            fileSign.DocumentSignId = documentId;
            var fileSignCreate = this.fileSignBO.Create(fileSign);
            fileSignCreate.Data = fileSign.Data;
            return fileSignCreate;
        }

        private DocumentSignInfo CreateDocument(int documentType)
        {
            DocumentSignInfo documentSign = new DocumentSignInfo();
            documentSign.CompanyId = GetCompanyIdOfUser();
            documentSign.UserCreate = this.CurrentUser.Id;
            documentSign.DocumentType = documentType;
            documentSign.Email = this.CurrentUser.Email;
            documentSign.SenderName = this.CurrentUser.UserName;
            return this.documentSignBO.Create(documentSign);
        }

        private string CreateFolder(int documentId)
        {
            string pathFileAssetOfCompany = printConfig.FullFolderAssetOfCompany;
            string folderContain = Path.Combine(pathFileAssetOfCompany, documentId.ToString());
            if (!Directory.Exists(folderContain))
            {
                Directory.CreateDirectory(folderContain);
            }

            return folderContain;
        }

        private string File2Pdf(string fullPathFileUpload, string extension)
        {
            string fileConvert = string.Empty;
            switch (extension)
            {
                case FileExtension.Doc:
                case FileExtension.DocX:
                case FileExtension.Xls:
                case FileExtension.Xlsx:
                    fileConvert = FileProcess.ConverFile2Pdf(Config.ApplicationSetting.Instance.FileFullPathOpenOffice, fullPathFileUpload);
                    break;
                case FileExtension.Jpg:
                case FileExtension.Jpeg:
                case FileExtension.Png:
                    fileConvert = FileProcess.ConverImages2Pdf(fullPathFileUpload);
                    break;
                default:
                    fileConvert = fullPathFileUpload;
                    break;
            }

            return fileConvert;
        }

        private List<EmployeeSignInfo> ProcessThreadEmployeeSignDocument(int documentId, DocumentSignInfo fileSign)
        {
            List<EmployeeSignInfo> result = new List<EmployeeSignInfo>();
            DeleteEmployeeSing(documentId, fileSign.EmployeesSign);
            var threadedSignDocumentEmployeeSign = CreateThreadedEmployeeSign(fileSign.EmployeesSign);
            result = ProcessEmployeeSign(threadedSignDocumentEmployeeSign, documentId);
            return result;
        }

        private List<EmployeeSignInfo> CreateThreadedEmployeeSign(List<EmployeeSignInfo> employeesSign)
        {
            List<EmployeeSignInfo> result = new List<EmployeeSignInfo>();
            int userAction = this.CurrentUser.Id;
            foreach (var employee in employeesSign)
            {
                ThreadedSignDocumentInfo theadSign = new ThreadedSignDocumentInfo(employee);
                theadSign.UserActionId = userAction;
                theadSign.CompanyId = GetCompanyIdOfUser();
                ThreadedSignDocumentInfo threadSignNew = this.threadedSignDocumentBO.Create(theadSign);
                EmployeeSignInfo employeeSingInfo = new EmployeeSignInfo(threadSignNew);
                employeeSingInfo.ThreadedSignDocumentId = threadSignNew.Id;
                employeeSingInfo.Id = employee.Id;
                result.Add(employeeSingInfo);
            }
            return result;
        }

        private List<EmployeeSignInfo> ProcessEmployeeSign(List<EmployeeSignInfo> employeesSign, int documentId)
        {
            List<EmployeeSignInfo> result = new List<EmployeeSignInfo>();
            int identity = 1;
            foreach (var item in employeesSign)
            {
                item.ThreadedSignDocumentId = item.ThreadedSignDocumentId;
                item.DocumentSingId = documentId;
                item.OrderSign = identity;
                if (item.Id == 0)
                {
                    CreateEmployeeSign(item);
                }
                else
                {
                    UpdateEmployeeSign(item.Id, item);
                }
                result.Add(item);
            }

            return result;
        }

        private EmployeeSignInfo CreateEmployeeSign(EmployeeSignInfo employeeSingInfo)
        {
            var employeerSign = this.employeeSignBO.Create(employeeSingInfo);
            employeeSingInfo.Id = employeerSign.Id;
            return employeeSingInfo;
        }

        private EmployeeSignInfo UpdateEmployeeSign(int id, EmployeeSignInfo employeeSingInfo)
        {
            var employeerSign = this.employeeSignBO.Update(id, employeeSingInfo);
            return employeerSign;
        }

        private List<EmployeeSignInfo> CreateSignaturPosition(List<EmployeeSignInfo> employeesSign)
        {
            List<EmployeeSignInfo> result = new List<EmployeeSignInfo>();
            foreach (var employee in employeesSign)
            {
                if (employee.EmployeesSignDetail == null || employee.EmployeesSignDetail.Count == 0)
                {
                    employee.EmployeesSignDetail = new List<EmployeeSignDetailInfo>();
                }
                else
                {
                    employee.EmployeesSignDetail = CreateEmployeeSignDetail(employee.Id, employee.EmployeesSignDetail);
                }

                result.Add(employee);
            }

            return result;
        }

        private List<EmployeeSignDetailInfo> CreateEmployeeSignDetail(int employeeSignId, List<EmployeeSignDetailInfo> employeeSignDetail)
        {
            List<EmployeeSignDetailInfo> result = new List<EmployeeSignDetailInfo>();
            foreach (var item in employeeSignDetail)
            {
                item.EmployeeSignId = employeeSignId;
                EmployeeSignDetailInfo employeeSignDetailCreated = this.employeeSignDetailBO.Create(item);
                result.Add(employeeSignDetailCreated);
            }

            return result;
        }

        private void UpdateNextStepDocument(int documentId, DocumentSignInfo documentSign)
        {
            this.documentSignBO.UpdateStep(documentId, documentSign);
        }

        private void DeleteEmployeeSing(int documentId, List<EmployeeSignInfo> employeesSign)
        {
            if (employeesSign == null || employeesSign.Count() == 0)
            {
                return;
            }

            List<int> employeeSingWillBeUpdate = employeesSign.Select(p => p.Id).ToList();
            this.employeeSignBO.DeleteEployeeSing(documentId, employeeSingWillBeUpdate);
        }


        private void DeleteEmployeeSingDetail(int documentId)
        {
            this.employeeSignDetailBO.DeleteByDocumentId(documentId);
        }
    }
}