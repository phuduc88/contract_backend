using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Contract.API.MediaFormatter
{
    public abstract class FileFormatter<T> : BaseFormatter<T>
        where T : class
    {
        protected abstract string GetFilePath(T t);

        public override async Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
                                               TransportContext transportContext)
        {      
            var file = value as T;
            if (file == null || !File.Exists(GetFilePath(file)))
            {
                await base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
                return;
            }

            using (var stream = new FileStream(GetFilePath(file), FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(writeStream);
            }
        }        
    }
}