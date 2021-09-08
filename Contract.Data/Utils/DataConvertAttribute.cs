using System;

namespace Contract.Data.Utils
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class DataConvertAttribute : Attribute
    {
        public string Source { get; set; }
        public object DefaultValue { get; set; }
        public bool ThrowExceptionIfSourceNotExist { get; set; }

        public string DateFomat { get; set; }

        public string NumberFomat { get; set; }

        public object DefaultValueWhenNull { get; set; }

        public object ValueNotApprove { get; set; }
        public DataConvertAttribute(string source, 
                                      object defaultValue = null,
                                      bool throwExceptionIfSourceNotExist = true,
                                      object defaultValueWhenNull = null,
                                      string dateFomat = null,
                                      string numberFomat = null,
                                      object valueNotApprove = null
                                    )
        {
            this.Source = source;
            this.DefaultValue = defaultValue;
            this.ThrowExceptionIfSourceNotExist = throwExceptionIfSourceNotExist;
            this.DateFomat = dateFomat;
            this.NumberFomat = numberFomat;
            this.DefaultValueWhenNull = defaultValueWhenNull;
            this.ValueNotApprove = valueNotApprove;
        }
    }
}
