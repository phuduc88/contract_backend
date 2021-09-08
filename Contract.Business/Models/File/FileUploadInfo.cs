using Contract.Business.Constants;
using Contract.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Contract.Business.Models
{
    public class FileUploadInfo
    {
        public HttpPostedFile File { get; set; }
        public string FileName { get; set; }

        public static List<FileUploadInfo> FromRequest(HttpRequest request)
        {
            if (request.Files == null || request.Files.Count == 0)
            {
                return null;
            }
            List<FileUploadInfo> result = new List<FileUploadInfo>();
            for (int i = 0; i < request.Files.Count; i++)
            {
                if (request.Files[i].InputStream == null)
                {
                    continue;
                }
                string extension = Path.GetExtension(request.Files[i].FileName);
                if (!FileExtension.ExtenFileAllowUpload.Contains(extension))
                {
                    throw new BusinessLogicException(ResultCode.RequestDataInvalid, MsgApiResponse.FileUploadIvalid);
                }

                result.Add(new FileUploadInfo
                  {
                      File = request.Files[i],
                      FileName = request.Files.AllKeys[i],
                  });
            }
            return result;
        }
    }
}
