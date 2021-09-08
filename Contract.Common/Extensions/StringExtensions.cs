using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Serialization;

namespace Contract.Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="System.String"/>
    /// </summary>
    public static class StringExtensions
    {
        private static readonly DateTime BaseUtcTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>
        /// A nicer way of calling <see cref="System.String.IsNullOrEmpty(string)"/>
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// A nicer way of calling the inverse of <see cref="System.String.IsNullOrEmpty(string)"/>
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is not null or an empty string (""); otherwise, false.</returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !value.IsNullOrEmpty();
        }

        public static bool IsNotNullOrEmpty(this string value, string valueReplace)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            return !value.Replace(valueReplace,"").IsNullOrEmpty();
        }

        public static bool IsNotNullOrEmptyTrim(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            return !value.Trim().IsNullOrEmpty();
        }


        public static string IsReplace(this string value, string valueReplace)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }
            return value.Replace(valueReplace, "").Trim();
        }

        public static bool IsContanin(this string value, string valueCompare)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            return value.Contains(valueCompare);
        }

        /// <summary>
        /// A nicer way of calling <see cref="System.String.Format(string, object[])"/>
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the string representation of the corresponding objects in args.</returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string GetExtensionFile(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }
            List<string> array = value.SplitAndTrim('.').ToList();
            int lenghtOf = array.Count();
            return array[(lenghtOf - 1)];
        }

        /// <summary>
        /// Returns a string array containing the trimmed substrings in this <paramref name="value"/>
        /// that are delimited by the provided <paramref name="separators"/>.
        /// </summary>
        public static IEnumerable<string> SplitAndTrim(this string value, params char[] separators)
        {
            Ensure.Argument.NotNull(value, "source");

            return value.Trim().Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
        }

        /// <summary>
        /// Checks if the <paramref name="source"/> contains the <paramref name="input"/> based on the provided <paramref name="comparison"/> rules.
        /// </summary>
        public static bool Contains(this string source, string input, StringComparison comparison)
        {
            if (source.IsNullOrEmpty())
            {
                return false;
            }

            return source.IndexOf(input, comparison) >= 0;
        }

        /// <summary>
        /// Limits the length of the <paramref name="source"/> to the specified <paramref name="maxLength"/>.
        /// </summary>
       
        public static bool IsOverLength(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return str.Length > maxLength;
            }
        }

        public static bool IsEmail(this string str)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(str);
                return mailAddress.Address == str;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNumeric(this string str)
        {
            double valueConvert = 0;
            return double.TryParse(str, out valueConvert);
        }


        public static string EmptyNull(this string str)
        {
            return str ?? "";
        }

        public static string IsToUpper(this string str)
        {
            if (str.IsNullOrEmpty())
            {
                return string.Empty;
            }
            return str.ToUpper();
        }

        public static bool IsEquals(this string str, string value)
        {
            return str.EmptyNull().Equals(value);
        }

        public static bool IsEqualUpper(this string str, string value)
        {
            return str.EmptyNull().ToUpper().Equals(value.EmptyNull().ToUpper());
        }

        public static T ParseEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str, true);
        }

        public static bool IsEmumDefined<T>(this string str)
        {
            //return (T)Enum.Parse(typeof(T), str, true);
            return Enum.IsDefined(typeof(T), str);
        }

        public static bool IsEnumType<T>(this string str)
        {
            bool isEnumType = true;
            try
            {
                ParseEnum<T>(str);
            }
            catch
            {
                isEnumType = false;
            }

            return isEnumType;
        }

        public static bool IsPhoneNumber(this string str)
        {
            try
            {
                return Regex.Match(str, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").Success;
            }
            catch
            {
                return false;
            }
        }

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public static bool ToBoolean(this string value)
        {
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
                    throw new InvalidCastException("You can't cast a weird value to a bool!");
            }
        }

        public static decimal? ToDecimal(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return null;
            }

            decimal outValue = 0;
            decimal.TryParse(value, out outValue);
            return outValue;
        }


        public static string ToUpperAndStrim(this string value)
        {
            return value.EmptyNull().Trim().ToUpper();
        }

        public static string ToStrim(this string value)
        {
            return value.EmptyNull().Trim();
        }
        
        public static DateTime? ConvertDateTime(this string timeStamp)
        {
            try
            {
                if (timeStamp.IsNullOrEmpty())
                {
                    return null;
                }

                return DateTime.ParseExact(timeStamp, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime? ConvertDateTimeFull(this string timeStamp)
        {
            try
            {
                if (timeStamp.IsNullOrEmpty())
                {
                    return null;
                }

                return DateTime.ParseExact(timeStamp, "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return null;
            }
        }        

        public static string DecodeUrl(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return null;
            }

            return HttpUtility.UrlDecode(value.IsReplace("null"), Encoding.UTF8);
        }
        public static int? ToInt(this string value)
        {
            if (value == null)
            {
                return null;
            }

            int outValue = 0;
            int.TryParse(value, out outValue);
            return outValue;
        }

        public static int ToInt(this string value, int valueDefault)
        {
            if (value == null)
            {
                return valueDefault;
            }

            int outValue = 0;
            int.TryParse(value.ToString(), out outValue);
            return outValue;
        }        

        public static string ConvertToString(this string value)
        {
            if (value == null)
            {
                return "";
            }

            return value.ToString();

        }

        public static string IsPathLeft(this string value, int totalWidth, char paddingChar)
        {
            if (value == null)
            {
                return "";
            }

            return value.PadLeft(totalWidth, paddingChar);

        }

        public static string IsPathRight(this string value, int totalWidth, char paddingChar)
        {
            if (value == null)
            {
                return "";
            }

            return value.PadRight(totalWidth, paddingChar);

        }

        public static List<string> ConvertToList(this string value, char character)
        {
            if (value.IsNullOrEmpty())
            {
                return new List<string>();
            }

            List<string> listData = value.Split(character).ToList();
            return listData;
        }

        public static string IsSubString(this string value, char character, int indexOf)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            string[] arrayCharacter = value.Split(character);
            if (arrayCharacter.Length == 0 || (arrayCharacter.Length - 1) < indexOf)
            {
                return string.Empty;
            }

            return arrayCharacter[indexOf];
        }

        public static string IsSubString(this string value, int lenght)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }
            if (value.Length < lenght)
            {
                return value;
            }

            return value.Substring(0, lenght);
        }

        public static string GetAccount(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }
            string[] arrayCharacter = value.Split(' ');
            if (arrayCharacter.Length == 0)
            {
                return null;
            }
            string accountId = arrayCharacter[arrayCharacter.Length - 1];
            for (int i = 0; i < arrayCharacter.Length - 1; i++)
            {
                string item = arrayCharacter[i];
                if (item.IsNullOrEmpty())
                {
                    continue;
                }

                accountId = accountId + arrayCharacter[i].Substring(0, 1).ToLower();
            }

            return accountId.ToAscii();
        }

        public static string ToAscii(this string unicode)
        {
            if (string.IsNullOrEmpty(unicode)) return "";
            string result = unicode.ToLower().Trim();
            string[] arrSrc = new string[] { " ", "&", "'", ">","<","!",":","#",".","+",
                "~","@","$","%","^","*","(",")",",","}","{","]","[",";","?","/","\\","\"","“","”",
                "Đ","đ", "ê", "â", "ư", "ơ", "ă","ô",
                    "ế", "ấ", "ứ", "ớ", "ắ","á","ú","ó","ố","í","ý","é",
                    "ề", "ầ", "ừ", "ờ", "ằ","à","ù","ò","ồ","ì","ỳ","è",
                    "ể", "ẩ", "ử", "ở", "ẳ","ả","ủ","ỏ","ổ","ỉ","ỷ","ẻ",
                    "ễ", "ẫ", "ữ", "ỡ", "ẵ","ã","ũ","õ","ỗ","ĩ","ỹ","ẽ",
                    "ệ", "ậ", "ự", "ợ", "ặ","ạ","ụ","ọ","ộ","ị","ỵ","ẹ"};
            string[] arrDest = new string[] { "-", "", "", "", "", "","","","","","","",
                "","","","","","","","","","","","","","","","","","",
                "D","d", "e", "a", "u", "o", "a","o",
                    "e", "a", "u", "o", "a","a","u","o","o","i","y","e",
                    "e", "a", "u", "o", "a","a","u","o","o","i","y","e",
                    "e", "a", "u", "o", "a","a","u","o","o","i","y","e",
                    "e", "a", "u", "o", "a","a","u","o","o","i","y","e",
                    "e", "a", "u", "o", "a","a","u","o","o","i","y","e"};
            for (int ct = 0; ct < arrSrc.Length; ct++)
            {
                result = result.Replace(arrSrc[ct].ToString(), arrDest[ct].ToString());
            }
            return result;
        }

        public static int FindNumber(this string str)
        {
            if (str == null) return 0;
            String number = new String(str.Where(Char.IsDigit).ToArray());
            return ToInt(number, 0);
        }

         

        public static byte[] ToHexBytes(this string hex)
        {
            if (hex == null) return null;
            if (hex.Length == 0) return new byte[0];

            int l = hex.Length / 2;
            var b = new byte[l];
            for (int i = 0; i < l; ++i)
            {
                b[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return b;
        }

        public static T Deserialize<T>(this string xmlStr)
        {
            var serializer = new XmlSerializer(typeof(T));
            T result;
            using (TextReader reader = new StringReader(xmlStr))
            {
                result = (T)serializer.Deserialize(reader);
            }
            return result;
        }

        public static string RemoveAllZero(this string value, char specter, char charactorWillBeRemove)
        {
            var phanle = value.SpliptData(specter, 1);
            if (phanle.IsNullOrEmpty())
            {
                return value;
            }

            return value.RemoveCharactorEndString(charactorWillBeRemove);
        }

        public static string RemoveCharactorEndString(this string value, char charactorWillBeRemove)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }

            var valueAfterRemove = value.Reverse().SkipWhile(i => i == charactorWillBeRemove).Reverse();
            return new String(valueAfterRemove.ToArray());
        }

        public static string SpliptData(this string value, char separator, int indexof)
        {
            string[] array = value.Split(separator);
            if (array.Length > indexof)
            {
                return array[indexof];
            }
            else
            {
                return string.Empty;
            }
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static string Encrypt(this string value, string keyCompare)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(keyCompare, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    value = Convert.ToBase64String(ms.ToArray());
                }
            }
            return value;
        }
        public static string Decrypt(this string value, string keyCompare)
        {
            value = value.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(keyCompare, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    value = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return value;
        }

        public static string DefaulValueWhenNullOrEmpty(this string value, string defaultValue)
        {

            if (value.IsNullOrEmpty())
            {
                return defaultValue;
            }

            return value;
        }

        public static string StandardizeFileName(this string fileName)
        {
            string fileNameStandardize = fileName.ToAscii();
            Regex r = new Regex(@"[^\w\.-]");
            return Path.GetInvalidFileNameChars().Aggregate(r.Replace(fileNameStandardize, @""), (current, c) => current.Replace(c.ToString(), string.Empty));
        }
       
    }
}

