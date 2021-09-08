using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Contract.Common.Extensions;
using Contract.Business.Extensions;
using Contract.Data.Utils;
using System.Xml.Serialization;
using System.IO;
using Contract.Business.Models;

namespace Contract.Business.Extensions
{
    public static class ObjectExtensions
    {
        private static readonly Dictionary<string, PropertyInfo[]> DataTypeProperties =
                           new Dictionary<string, PropertyInfo[]>();

        public static decimal? ToDecimal(this object value)
        {
            if (value == null)
            {
                return null;
            }
            
            decimal outValue = 0;
            decimal.TryParse(value.ToString(),out outValue);
            return outValue;
        }

        public static string DefaulValueWhenNullOrEmpty(this object value, string defaultValue)
        {

            if (value.IsNullOrEmpty())
            {
                return defaultValue;
            }

            return (string)value;
        }

        public static decimal? ToDecimal(this object value, IFormatProvider provider, string format, string specter)
        {
            var numberFomat = value.FomatMonney(provider, format, specter);
            if (numberFomat == null)
            {
                return null;
            }

            decimal outValue = 0;
            decimal.TryParse(numberFomat, out outValue);
            return outValue;
        }

        public static string IsOrderToString(this int? value)
        {
            if (!value.HasValue)
            {
                return string.Empty;
            }

            return value.Value.ToString();
        }

        public static string FomatMonney(this object value, IFormatProvider provider, string format, string specter)
        {
            if (value == null)
            {
                return null;
            }
            decimal number = value.ToDecimal(0);
            string endDecimal = format.SpliptData('.', 1);
            string numberAfter = number.ToString(format, provider);
            char charspecter = specter.Equals(".") ? '.' : ',';
            string fristNumber = numberAfter.SpliptData(charspecter, 0);
            if (fristNumber.IsNullOrEmpty())
            {
                numberAfter = string.Format("{0}{1}", "0", numberAfter);
            }

            return numberAfter.Replace(String.Format("{0}{1}", specter, endDecimal), "").RemoveAllZero(charspecter, '0');
        }

        public static string FomatMonney(this object value, IFormatProvider provider, string format, string specter, bool isChangeInvoice)
        {
            if (value == null)
            {
                return null;
            }

            decimal number = value.ToDecimal(0);
            if (isChangeInvoice && number == 0)
            {
                return @"...\....";
            }

            string endDecimal = format.SpliptData('.', 1);
            string numberAfter = number.ToString(format, provider);
            char charspecter = specter.Equals(".") ? '.' : ',';
            string fristNumber = numberAfter.SpliptData(charspecter, 0);
            if (fristNumber.IsNullOrEmpty())
            {
                numberAfter = string.Format("{0}{1}", "0", numberAfter);
            }

            return numberAfter.Replace(String.Format("{0}{1}", specter, endDecimal), "").RemoveAllZero(charspecter,'0');
        }


        public static bool IsEquals(this object value, object valueCompare)
        {
            if (value == null || valueCompare == null)
            {
                return false;
            }

            return value.ToString().Equals(valueCompare.ToString());
        }

        public static bool IsEquals(this object value, string valueCompare)
        {
            if (value == null || valueCompare == null)
            {
                return false;
            }

            return value.ToString().Equals(valueCompare);
        }

        public static decimal ToDecimal(this object value, int defaultValue)
        {
            if (value == null)
            {
                return defaultValue;
            }

            decimal outValue = 0;
            decimal.TryParse(value.ToString(), out outValue);
            return outValue;
        }

        public static decimal ToDecimal(this object value, int defaultValue, IFormatProvider provider, string format, string specter)
        {
            var numberFomat = value.FomatMonney(provider, format, specter);
            if (numberFomat == null)
            {
                return defaultValue;
            }
          
            decimal outValue = 0;
            decimal.TryParse(numberFomat, out outValue);
            return outValue;
        }


        public static double ToDouble(this object value, int defaultValue)
        {
            if (value == null)
            {
                return defaultValue;
            }

            double outValue = 0;
            double.TryParse(value.ToString(), out outValue);
            return outValue;
        }

        public static int? ToInt(this object value)
        {
            if (value == null)
            {
                return null;
            }

            int outValue = 0;
            int.TryParse(value.ToString(), out outValue);
            return outValue;
        }

        public static int? ToInt(this object value, int valueDefault)
        {
            if (value == null)
            {
                return valueDefault;
            }

            int outValue = 0;
            int.TryParse(value.ToString(), out outValue);
            return outValue;
        }

        public static double? ToDouble(this object value)
        {
            if (value == null)
            {
                return null;
            }

            double outValue = 0;
            double.TryParse(value.ToString(), out outValue);
            return outValue;
        }

        public static double? ToDouble(this object value, double valueDefault)
        {
            if (value == null)
            {
                return valueDefault;
            }

            double outValue = 0;
            double.TryParse(value.ToString(), out outValue);
            return outValue;
        }

        public static bool? ToBoolean(this object value)
        {
            if (value == null)
            {
                return null;
            }

            switch (value.EmptyNull().ToLower())
            {
                case "true":
                    return true;
                case "t":
                    return true;
                case "1":
                    return true;
                case "0":
                    return false;
                case "false":
                    return false;
                case "f":
                    return false;
                case "":
                    return false;
                default:
                    return false;
            }
        }

        public static string EmptyNull(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString();
        }

        public static bool IsNullOrEmpty(this object value)
        {
            return string.IsNullOrEmpty(value.EmptyNull());
        }

        public static string Trim(this object value)
        {
            return value.EmptyNull().Trim();
        }

        public static string IsToString(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString().Trim();
        }


        public static string ToUpperAndTrim(this object value)
        {
            return value.EmptyNull().ToUpper().Trim();
        }

        public static DateTime? ConvertDateTime(this object value)
        {
            try
            {
                if (value.IsNullOrEmpty())
                {
                    return null;
                }

                return Convert.ToDateTime(value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int GetQuarter(this DateTime dateTime)
        {
            if (dateTime.Month <= 3)
                return 1;

            if (dateTime.Month <= 6)
                return 2;

            if (dateTime.Month <= 9)
                return 3;

            return 4;
        }

        public static string ConvertDateTime(this object value, string fomat)
        {
            try
            {
                if (value.IsNullOrEmpty())
                {
                    return string.Empty;
                }

                return Convert.ToDateTime(value).ToString(fomat);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ConvertToString(this decimal? value, string fomat)
        {
            try
            {
                if (!value.HasValue)
                {
                    return string.Empty;
                }

                return value.Value.ToString(fomat);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ConvertToString(this decimal value, string fomat)
        {
            try
            {
                return value.ToString(fomat);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string ConvertToString(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString();
        }

        public static string ConvertToString(this object value, string fomat)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return Convert.ToDateTime(value).ToString(fomat);
        }     

        public static string SpliptData(this object value, char separator, int indexof)
        {
            string valueOfTex = value.ConvertToString();
            string[] array = valueOfTex.Split(separator);
            if (array.Length > indexof)
            {
                return array[indexof].Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        //public static Dictionary<string, string> ConvertDataToDic(this object srcObj, Dictionary<string, PlaceHolderConfig> placehoder)
        //{
        //    Dictionary<string, string> dicResult = new Dictionary<string, string>();
        //    var srcProperties = GetProperties(srcObj.GetType());
        //    foreach (var item in placehoder)
        //    {
        //        PropertyInfo property = GetPropertyInfo(item.Key, srcProperties);
        //        if (!dicResult.ContainsKey(item.Key))
        //        {
        //            if (property == (PropertyInfo)null)
        //            {
        //                dicResult.Add(item.Key, item.Value.ValueDefault);
        //            }
        //            else
        //            {
        //                string retValue = property.GetValue(srcObj).FomatData(property.PropertyType.FullName, item.Value.ValueDefault);
        //                dicResult.Add(item.Key, retValue);
        //            }
        //        }
        //    }
        //    return dicResult;
        //}

        public static string SerializeXML<T>(this T t)
        {
            if (t == null)
            {
                return string.Empty;
            }
            StringBuilder xml = new StringBuilder();
            if (t == null)
            {
                throw new ArgumentNullException("Source object");
            }


            // Get objects's properties from memory cache
            var srcProperties = GetProperties(t.GetType());

            foreach (var property in srcProperties)
            {
                var attr = property.GetCustomAttribute<XMLConvertAttribute>();
                if (attr == null)
                {
                    continue;
                }

                var srcPropertyName = attr.Source;
                var defaultValue = attr.DefaultValue;
                var throwExpIfSourceNotExist = attr.ThrowExceptionIfSourceNotExist;
                bool ignoreWhenNull = attr.IgnoreWhenNull;
                object values = property.GetValue(t);
                if (values == null  && ignoreWhenNull)
                {
                    continue;
                }
                xml.AppendLine(string.Format("<{0}>{1}</{0}>", srcPropertyName, values));
            }

            return xml.ToString();
        }


        private static PropertyInfo[] GetProperties(Type type)
        {
            // Prevent unnecessary locks
            if (DataTypeProperties.ContainsKey(type.FullName))
            {
                return DataTypeProperties[type.FullName];
            }

            PropertyInfo[] props = null;
            lock (DataTypeProperties)
            {
                if (DataTypeProperties.ContainsKey(type.FullName))
                {
                    props = DataTypeProperties[type.FullName];
                }
                else
                {
                    props = type.GetProperties();
                    DataTypeProperties.Add(type.FullName, props);
                }
            }
            return props;
        }


        private static PropertyInfo GetPropertyInfo(string srcPropertyName, PropertyInfo[] srcPropertyList)
        {
            var srcProperty = srcPropertyList.FirstOrDefault(p =>
                         p.Name.Equals(srcPropertyName, StringComparison.InvariantCultureIgnoreCase));
            return srcProperty;

        }

        public static void XmlSerializerNoNamespace<T>(this T value, string fullPathFileSave)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            TextWriter txtWriter = new StreamWriter(fullPathFileSave);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            xs.Serialize(txtWriter, value, ns);
            txtWriter.Close();
        }

        public static void XmlSerializer<T>(this T value, string fullPathFileSave)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            TextWriter txtWriter = new StreamWriter(fullPathFileSave);
            xs.Serialize(txtWriter, value);
            txtWriter.Close();
           
        }

        private static string FomatData(this object data, string fullName, string defaultValue)
        {
            if (data.IsNullOrEmpty())
            {
                return defaultValue;
            }

            if (fullName.Contains("System.Decimal"))
            {
                return data.ToDecimal(0).ConvertToString("#,##0");
            }
                
            if (fullName.Contains("System.DateTime")) {
                return data.ConvertDateTime("dd/MM/yyyy");
            }
                
            return data.ToString();
        }
         
    }
}
