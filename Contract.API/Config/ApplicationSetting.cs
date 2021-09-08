using Contract.Business.Cache;
using Contract.Common;
using Contract.Common.Extensions;
using Contract.Data.DBAccessor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Contract.API.Config
{
    public class ApplicationSetting
    {
        #region Web.app config keys

        /// <summary>
        /// Secret key to append to login information when calculate hash string
        /// </summary>
        [Config("LoginSecretKey")]
        public string LoginSecretKey { get; private set; }

        /// <summary>
        /// Login session timeout in minutes (default 30 minutes)
        /// </summary>
        [Config("SessionTimeout", "30", false)]
        public int SessionTimeout { get; private set; }

        [Config("Enable CreateTokenRandom", "true", true)]
        public bool EnableCreateTokenRandom { get; private set; }

        #region Key Config For Email

        [Config("UrlResetPassword")]
        public string UrlResetPassword { get; private set; }

        [Config("ResetPasswordTimeOut", "15", false)]
        public int ResetPasswordTimeOut { get; private set; }

        [Config("EmailTemplateFilePath")]
        public string EmailTemplateFilePath { get; private set; }

       
        #endregion

        #region Key Config For Schedule Job
        /// <summary>
        /// Max size of image upload
        /// </summary>
        [Config("MaxSizeImage", "1000", false)]
        public int MaxSizeImage { get; set; }

        /// <summary>
        /// Folder to save image uploaded
        /// </summary>
        [Config("FolderAssetOfCompany", "~/Data/Asset", false)]
        public string FolderAssetOfCompany { get; set; }

        /// Folder to save image uploaded
        /// </summary>

        #endregion

        
        [Config("MaxLengthBuffer", "1024", false)]
        public int MaxLengthBuffer { get; private set; }

        [Config("MaxSizeFileUpload", "62914560", false)]
        public int MaxSizeFileUpload { get; set; }

        [Config("Folder UploadTemp")]
        public string UploadTempFolder { get; private set; }

        [Config("UploadFolderImport", "~/Data/MasterData", false)]
        public string ImportFolder { get; set; }

        [Config("UploadValidator", "~/Data/ValidatorInvoice", false)]
        public string UploadValidator { get; set; }

        /// </summary>
        [Config("MessageSendEmailSuccess", "Gửi email cho khách hàng thành công", false)]
        public string MessageSendEmailSuccess { get; set; }

        [Config("MessageSendEmailError", "Có lỗi trong quá trình gửi email cho khách hàng", false)]
        public string MessageSendEmailError { get; set; }

        [Config("TemplateInvoiceFolder", "~/Data/InvoiceTemplate", false)]
        public string TemplateInvoiceFolder { get; set; }

        [Config("TokenKeyResearch", "NewInvoice", false)]
        public string TokenKeyResearch { get; private set; }

        [Config("UrlSearchInvoice", "http://login.new-invoice.com/api/", false)]
        public string UrlSearchInvoice { get; private set; }

        [Config("AcceptionImport", "true", false)]
        public bool AcceptionImport { get; private set; }

        [Config("Cache Namespace: Session", "newinvoice:session", false)]
        public string CacheNamespaceSession { get; private set; }
        /// <summary>
        /// Cache timeout in minutes (default 1 day)
        /// </summary>
        [Config("CacheResponse Timeout", "120", false)]
        public int CacheResponseTimeout { get; private set; }

        [Config("ImportFileMaxSizeImport", "512", false)]
        public int ImportFileMaxSizeImport { get; private set; }

        [Config("FileFullPathOpenOffice", "512", false)]
        public string FileFullPathOpenOffice { get; private set; }

        #endregion Web.app config keys

        #region Fields, Properties

        private static Logger logger = new Logger();
        private static readonly ApplicationSetting instance = new ApplicationSetting();

        public static ApplicationSetting Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion Fields, Properties

        #region Methods

        /// <summary>
        /// Prevent new object of this class from outside
        /// </summary>
        private ApplicationSetting()
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            try
            {
                // Get objects's properties from memory cache
                var configProperties = typeof(ApplicationSetting).GetProperties();

                foreach (var property in configProperties)
                {
                    var attr = property.GetCustomAttribute<ConfigAttribute>();
                    if (attr == null)
                    {
                        continue;
                    }

                    var configKey = attr.Key;
                    var configValueStr = attr.DefaultValue;

                    if (ConfigurationManager.AppSettings.AllKeys.Contains(configKey))
                    {
                        configValueStr = ConfigurationManager.AppSettings[configKey];
                    }

                    var configValue = configValueStr.ConvertDataToType(property.PropertyType);
                    property.SetValue(this, configValue);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Get configuration in Web.config failed.");
                throw;
            }
        }

        #endregion Methods
    }
}