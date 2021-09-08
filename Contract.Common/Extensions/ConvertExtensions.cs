using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Common.Extensions
{
    public static class ConvertExtensions
    {
        public static bool AsBool(this string value)
        {
            bool result;
            return Boolean.TryParse(value, out result) ? result : false;
        }

        public static int AsInt(this string value)
        {
            int result;
            return Int32.TryParse(value, out result) ? result : 0;
        }

        /// <summary>
        /// Convert a string value to another data type.
        /// </summary>
        /// <param name="propertyType">Destination data type</param>
        /// <param name="valueStr">Source value</param>
        /// <returns>New converted data in destination data type</returns>
        public static object ConvertDataToType(this string valueStr, Type propertyType)
        {
            object value = null;
            if (valueStr != null && typeof(string) != propertyType)
            {
                Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                value = System.Convert.ChangeType(valueStr, underlyingType);
            }
            else
            {
                value = valueStr;
            }

            return value;
        }
    }
}
