using Contract.Business.Utils;
using Contract.Common;
using Contract.Data.Utils;
using Newtonsoft.Json;
using System;

namespace Contract.Business.Models
{
    public class UserSessionInfo
    {
        private static ulong LastSessionId = 0;

        #region Fields, Properties

        [JsonIgnore]
        public readonly ulong SessionId;

        [JsonIgnore]
        public bool IsOverwriteLogin { get; set; }

        [DataConvert("UserSID")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [DataConvert("UserID")]
        [StringTrim]
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [DataConvert("UserName")]
        [StringTrim]
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        [DataConvert("Email")]
        [StringTrim]
        public string Email { get; set; }

        [StringTrim]
        [DataConvert("Password")]
        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        [DataConvert("AccountDefault")]
        [StringTrim]
        public bool AccountDefault { get; set; }

        [JsonProperty("token")]
        [StringTrim]
        public string Token { get; set; }

        [JsonProperty("role")]
        public Role RoleUser { get; set; }

        [JsonProperty("company")]
        public CompanyInfo Company { get; set; }

        [JsonConverter(typeof(JsonDateConverterString))]
        [JsonProperty("currentDate")]
        public DateTime CurrentDate
        {
            get
            {
                return DateTime.Now;
            }
        }

        [JsonProperty("slidingExpiration")]
        public TimeSpan SlidingExpiration { get; set; }

        [JsonProperty("emailServer", NullValueHandling = NullValueHandling.Ignore)]
        public EmailServerInfo EmailServer { get; set; }

        [JsonProperty("signatureImage")]
        public string SignatureImage { get; set; }


        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserSessionInfo()
        {
            SessionId = ++LastSessionId;
        }

        /// <summary>
        /// Constructor current Client object by copying data in the specified object
        /// </summary>
        /// <param name="srcUser">Source object</param>
        public UserSessionInfo(object srcObject, string token)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, UserSessionInfo>(srcObject, this);
            }

            this.Token = token;
        }


        public UserSessionInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, UserSessionInfo>(srcObject, this);
            }
        }

        public UserSessionInfo(object srcObject, object srcRole, string token)
            : this(srcObject)
        {
            this.RoleUser = srcRole != null ? new Role(srcRole) : new Role();
            this.Token = token;
        }

        public UserSessionInfo(object srcObject, object srcRole, object srcCompany, string token)
            : this(srcObject, srcRole, token)
        {
            this.Company = srcCompany != null ? new CompanyInfo(srcCompany) : new CompanyInfo();
            this.Token = token;
        }
        #endregion
    }
}
