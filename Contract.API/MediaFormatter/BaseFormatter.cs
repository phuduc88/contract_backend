using System;
using System.Net.Http.Formatting;

namespace Contract.API.MediaFormatter
{
    public abstract class BaseFormatter<T> : MediaTypeFormatter
        where T : class
    {
        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(T);
        }
    }
}