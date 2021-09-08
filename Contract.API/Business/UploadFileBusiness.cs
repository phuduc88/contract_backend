using Contract.API.Constants;
using Contract.Business;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using Contract.Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Contract.API.Business
{
    public class UploadFileBusiness : BaseBusiness
    {
        public ResultCode SaveFile(List<FileUploadInfo> fileUploadInfo, string filePath)
        {
            ////var file = fileUploadInfo.File;
            //if (file == null || file.InputStream == null || string.IsNullOrWhiteSpace(filePath))
            //{
            //    return ResultCode.RequestDataInvalid;
            //}

            //ResultCode resultCode = ResultCode.WaitNextRequest;
            //using (var fs = new FileStream(filePath,FileMode.Create))
            //{
            //    var buffer = new byte[Config.ApplicationSetting.Instance.MaxLengthBuffer];
            //    int bytesRead;
            //    while ((bytesRead = file.InputStream.Read(buffer, 0, buffer.Length)) > 0)
            //    {
            //        fs.Write(buffer, 0, bytesRead);
            //    }
            //}


            return ResultCode.NoError;
        }

        public string CreateFilePathByDocId(int releaseId, FileUploadInfo fileUploadInfo)
        {
            int companyId = GetCompanyIdOfUser();
            string path = Path.Combine(Config.ApplicationSetting.Instance.FolderAssetOfCompany, companyId.ToString(), AssetSignXML.Release, AssetSignXML.TempSignFile, releaseId.ToString());
            string folderPath = HttpContext.Current.Server.MapPath(path);
            ResetFolder(folderPath);
            DeleteFolderSignFileAndDe(releaseId, fileUploadInfo, companyId);
            return Path.Combine(folderPath, fileUploadInfo.FileName);
        }

        private void DeleteFolderSignFileAndDe(int releaseId, FileUploadInfo fileUploadInfo, int companyId)
        {
            string rootSignFile = Path.Combine(Config.ApplicationSetting.Instance.FolderAssetOfCompany, companyId.ToString(), AssetSignXML.Release, AssetSignXML.SignFile, releaseId.ToString());
            string folderSignFile = HttpContext.Current.Server.MapPath(rootSignFile);
            ResetFolder(folderSignFile);
            string rootDeliver = Path.Combine(Config.ApplicationSetting.Instance.FolderAssetOfCompany, companyId.ToString(), AssetSignXML.Release, AssetSignXML.DeliverBHXH, releaseId.ToString());
            string folderDeliver = HttpContext.Current.Server.MapPath(rootDeliver);
            ResetFolder(folderDeliver);
        }

        private void ResetFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }

            Directory.CreateDirectory(folderPath);
        }

        public string CreateFilePathByDocId(int releaseId, int companyId, FileUploadInfo fileUploadInfo)
        {
            string path = Path.Combine(Config.ApplicationSetting.Instance.FolderAssetOfCompany, companyId.ToString(), AssetSignXML.Release, AssetSignXML.TempSignFile, releaseId.ToString());
            string folderPath = HttpContext.Current.Server.MapPath(path);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return Path.Combine(folderPath, fileUploadInfo.FileName);
        }

        public bool IsValidFile(FileUploadInfo fileUploadInfo)
        {
            if (fileUploadInfo == null)
            {
                return false;
            }

            bool fileSizeValid = fileUploadInfo.File.ContentLength > 0 && fileUploadInfo.File.ContentLength <= Config.ApplicationSetting.Instance.MaxSizeFileUpload;
            bool extensionValid = Utility.Equals(Path.GetExtension(fileUploadInfo.FileName), FileUpload.FileExtension);
            return fileSizeValid && extensionValid;
        }


        public string StandardFileUpload(string sourceFile, int declarationId)
        {
            string newFilePath = string.Empty;
            try
            {
                FileInfo fileInfo = new FileInfo(sourceFile);

                var extension = fileInfo.Extension;
                var nameWithoutExtension = fileInfo.Name.Remove(fileInfo.Name.LastIndexOf(extension));

                string suffix = DateTime.Now.ToString(Formatter.DateTimeFormat);
                string newName = nameWithoutExtension + Characters.Underscore + suffix + extension;

                string folderPath = fileInfo.DirectoryName;
                string destFile = Path.Combine(folderPath, newName);

                fileInfo.MoveTo(destFile);
                int companyId = GetCompanyIdOfUser();
                string path = Path.Combine(Config.ApplicationSetting.Instance.FolderAssetOfCompany, companyId.ToString(), AssetSignXML.Release, AssetSignXML.SignFile, declarationId.ToString());
                string tagetFullPath = HttpContext.Current.Server.MapPath(path);
                if (!Directory.Exists(tagetFullPath))
                {
                    Directory.CreateDirectory(tagetFullPath);
                }
                FileProcess.ExtractFile(destFile, tagetFullPath);
                newFilePath = destFile;
                DeleteFolderTempl(folderPath);
            }
            catch
            {
                // Don't need handle exception. 
                // When exception, will set file path is source file
                // TODO: Maybe, write log to trace exception.
                newFilePath = string.Empty;
            }

            return newFilePath;

        }
        private void DeleteFolderTempl(string destFile)
        {

            if (destFile.IsNotNullOrEmpty() && Directory.Exists(destFile))
            {
                Directory.Delete(destFile, true);
            }
        }
        //public string StandardFileUpload(string sourceFile, int companyId)
        //{
        //    string newFilePath = string.Empty;
        //    try
        //    {
        //        FileInfo fileInfo = new FileInfo(sourceFile);

        //        var extension = fileInfo.Extension;
        //        var nameWithoutExtension = fileInfo.Name.Remove(fileInfo.Name.LastIndexOf(extension));

        //        string suffix = DateTime.Now.ToString(Formatter.DateTimeFormat);
        //        string newName = nameWithoutExtension + Characters.Underscore + suffix + extension;

        //        string folderPath = fileInfo.DirectoryName;
        //        string destFile = Path.Combine(folderPath, newName);

        //        fileInfo.MoveTo(destFile);

        //        string path = Path.Combine(Config.ApplicationSetting.Instance.FolderAssetOfCompany, companyId.ToString(), AssetSignXML.Release, AssetSignXML.SignFile);
        //        string tagetFullPath = HttpContext.Current.Server.MapPath(path);
        //        if (!Directory.Exists(tagetFullPath))
        //        {
        //            Directory.CreateDirectory(tagetFullPath);
        //        }
        //        FileProcess.ExtractFile(destFile, tagetFullPath);
        //        newFilePath = destFile;
        //        DeleteFolderTempl(folderPath);
        //    }
        //    catch
        //    {
        //        // Don't need handle exception. 
        //        // When exception, will set file path is source file
        //        // TODO: Maybe, write log to trace exception.
        //        newFilePath = string.Empty;
        //    }

        //    return newFilePath;

        //}
    }
}