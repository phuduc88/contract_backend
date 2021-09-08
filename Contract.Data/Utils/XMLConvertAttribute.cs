using System;

namespace Contract.Data.Utils
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class XMLConvertAttribute : Attribute
    {
        public string Source { get; set; }
        public object DefaultValue { get; set; }
        public bool ThrowExceptionIfSourceNotExist { get; set; }

        public bool IgnoreWhenNull { get; set; }


        public XMLConvertAttribute(string source,
                                      object defaultValue = null,
                                      bool throwExceptionIfSourceNotExist = true,
                                      bool ignoreWhenNull = false
                                    )
        {
            this.Source = source;
            this.DefaultValue = defaultValue;
            this.ThrowExceptionIfSourceNotExist = throwExceptionIfSourceNotExist;
            this.IgnoreWhenNull = ignoreWhenNull;
        }
    }
}
