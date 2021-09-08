using Contract.Business;
using Contract.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace Contract.API.MediaFormatter
{
    public class ZipFileFormatter : FileFormatter<DocumentFile>
    {
        public ZipFileFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/zip"));
        }

        protected override string GetFilePath(DocumentFile t)
        {
            return t != null ? t.FilePath : string.Empty;
        }
    }
}