using Contract.Business;
using Contract.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace Contract.API.MediaFormatter
{
    public class PdfFileFormatter : FileFormatter<DocumentFile>
    {
        public PdfFileFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/pdf"));
        }

        protected override string GetFilePath(DocumentFile t)
        {
            return t != null ? t.FilePath : string.Empty;
        }
    }
}