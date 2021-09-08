using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Contract.Data.Utils
{
    /// <summary>
    /// This class provides methods to help copying data in one POCO (Plain Old C# Object) to another POCO.
    /// </summary>
    public static class DataObjectConverter
    {
        /// <summary>
        /// Cache properties in a type for using later
        /// </summary>
        private static readonly Dictionary<string, PropertyInfo[]> DataTypeProperties =
                            new Dictionary<string, PropertyInfo[]>();

        /// <summary>
        /// Copy fields/properties data in <c>t</c> object to <c>u</c> object.
        /// Use <c>ObjectMappingAttribute</c> in class U to specify which fields/properties will be copied.
        /// </summary>
        /// <typeparam name="T">Source class</typeparam>
        /// <typeparam name="U">Destination class</typeparam>
        /// <param name="t">Source object</param>
        /// <param name="u">Destination object</param>
        /// <returns>Return <c>true</c> if convert successfully, false if otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">Source object or destination object is null.</exception>
        /// <exception cref="ArgumentException">Cannot convert data in source object to destination object.</exception>
        /// <exception cref="MissingFieldException">Source field/property does not exist.</exception>
        public static bool Convert<T, U>(T t, U u) 
            where T: class
            where U: class
        {
            if (t == null)
            {
                throw new ArgumentNullException("Source object");
            }
            if (u == null)
            {
                throw new ArgumentNullException("Destination object");
            }
            
            bool retValue = true;

            // Get objects's properties from memory cache
            var srcProperties = GetProperties(t.GetType());
            var destProperties = GetProperties(u.GetType());

            foreach (var property in destProperties)
            {
                var attr = property.GetCustomAttribute<DataConvertAttribute>();
                if (attr == null)
                {
                    continue;
                }

                var srcPropertyName = attr.Source;
                var defaultValue = attr.DefaultValue;
                var throwExpIfSourceNotExist = attr.ThrowExceptionIfSourceNotExist;
                var dateFomat = attr.DateFomat;
                var numberFomat = attr.NumberFomat;
                var defaultValueWhenNull = attr.DefaultValueWhenNull;
                var valueNotApprove = attr.ValueNotApprove;

                bool isSrcPropertyExists;
                var srcValue = GetSourceValue(t, srcPropertyName, srcProperties, dateFomat, numberFomat, out isSrcPropertyExists);

                if (!isSrcPropertyExists)
                {
                    if (throwExpIfSourceNotExist)
                        throw new MissingFieldException("Source property is not found: " + srcPropertyName);

                    srcValue = defaultValue;
                }

                if (srcValue == null)
                {
                    srcValue = defaultValueWhenNull;
                }

                if (valueNotApprove != null)
                {
                    if (srcValue.ToString().Equals(valueNotApprove.ToString()))
                    {
                        srcValue = defaultValueWhenNull;
                    }
                    
                }

                SetPropertyValue(u, property, srcValue);
            }

            return retValue;
        }

        /// <summary>
        /// Get all public properties of a data type
        /// </summary>
        /// <param name="type">Data type</param>
        /// <returns>
        /// <para>
        /// An array of PropertyInfo objects representing all public properties of the data type.
        /// -or- 
        /// An empty array of type PropertyInfo, if the data type does not have public properties.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para></para>
        /// </remarks>
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

        /// <summary>
        /// Get data in source object recusively
        /// </summary>
        private static object GetSourceValue(object srcObj, 
                                            string srcPropertyName, 
                                            PropertyInfo[] srcPropertyList, 
                                            string dateFomat,
                                            string numberFomat,
                                            out bool isSrcPropertyExists)
        {
            object retValue = null;
            isSrcPropertyExists = false;

            if (srcObj == null)
            {
                return null;
            }

            if (!srcPropertyName.Contains("."))
            {
                var srcProperty = srcPropertyList.FirstOrDefault(p => 
                            p.Name.Equals(srcPropertyName, StringComparison.InvariantCultureIgnoreCase));
                if (srcProperty != null)
                {
                    isSrcPropertyExists = true;
                    retValue = srcProperty.GetValue(srcObj);
                    if (retValue != null && dateFomat != null)
                    {

                        retValue = FomatObject(retValue, dateFomat);
                    }

                    if (retValue != null && numberFomat != null)
                    {

                        retValue = FomatObjectNumberic(retValue, numberFomat);
                    }

                }
            }
            else
            {
                var srcPropertyLevel1Name = srcPropertyName.Substring(0, srcPropertyName.IndexOf("."));
                var srcPropertyLevel1 = srcPropertyList.FirstOrDefault(p => 
                            p.Name.Equals(srcPropertyLevel1Name, StringComparison.InvariantCultureIgnoreCase));
                if (srcPropertyLevel1 != null)
                {
                    var srcPropertyLevel1Value = srcPropertyLevel1.GetValue(srcObj);
                    if (srcPropertyLevel1Value != null)
                    {
                        var srcPropertyLevel2Name = srcPropertyName.Substring(srcPropertyName.IndexOf(".") + 1);
                        var srcPropertiesLevel2 = GetProperties(srcPropertyLevel1Value.GetType());

                        retValue = GetSourceValue(srcPropertyLevel1Value, 
                                                    srcPropertyLevel2Name, 
                                                    srcPropertiesLevel2, 
                                                    dateFomat,
                                                    numberFomat,
                                                    out isSrcPropertyExists);
                    }
                }
            }
           
            return retValue;
        }

        private static void SetPropertyValue<T>(T t, PropertyInfo prop, object value)
            where T : class
        {
            var destData = ConvertData(prop.PropertyType, value);
            prop.SetValue(t, destData);
        }

        /// <summary>
        /// Convert a value from 1 data type to another data type.
        /// </summary>
        /// <param name="destDataType">Destination data type</param>
        /// <param name="srcDataType">Source data type</param>
        /// <param name="srcData">Source value</param>
        /// <returns>New converted data in destination data type</returns>
        /// <exception cref="ArgumentException">Cannot convert data because:
        /// <list>
        ///     <item>Source data type invalid, no way to convert data from source data type to destination data type </item>
        ///     <item>Source data is invalid format for converting to </item>
        /// </list>
        /// </exception>
        private static object ConvertData(Type destDataType, object srcData)
        {
            try
            {
                object destData = null;
                if (srcData != null && srcData.GetType() != destDataType)
                {
                    Type propertyType = Nullable.GetUnderlyingType(destDataType) ?? destDataType;
                    destData = System.Convert.ChangeType(srcData, propertyType);
                }
                else
                {
                    destData = srcData;
                }

                //var destData = converter.ConvertTo(srcData, destDataType);
                return destData;
            }
            //catch (NotSupportedException ex)
            //{
            //    var msg = string.Format("Source datatype is invalid. Expected: {0}, Actual: {1}", destDataType.Name, srcDataType.Name);
            //    throw new ArgumentException(msg, ex);
            //}
            catch (InvalidCastException ex)
            {
                var msg = string.Format("Source datatype is invalid format of {0}", destDataType.Name);
                throw new ArgumentException(msg, ex);
            }
            catch (FormatException ex)
            {
                var msg = string.Format("Source datatype is invalid format of {0}", destDataType.Name);
                throw new ArgumentException(msg, ex);
            }
        }

        private static object FomatObject(object retValue, string dateFomat)
        {
            try
            {
                DateTime dtValue = (DateTime)retValue;
                return dtValue.ToString(dateFomat);
            }
            catch 
            {

                return retValue;
            }
        }

        private static object FomatObjectNumberic(object retValue, string numberFomat)
        {
            try
            {
                string numberFomayt = String.Format(numberFomat, retValue);
                return numberFomayt;
            }
            catch
            {

                return retValue;
            }
        }
    }
    
}
