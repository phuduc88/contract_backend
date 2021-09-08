using Contract.API.Constants;
using Contract.Business.Cache;
using Contract.Business.Config;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using Contract.Common.Extensions;
using Contract.Business.Extensions;
using System;
using System.IO;
using System.Net.Mail;
using System.Web;

namespace Contract.API.Business
{
    public abstract class BaseBusiness
    {
        private UserSessionInfo _currentUser;
        private SmtpClient smtpClientOfCompany;

        public UserSessionInfo CurrentUser
        {
            get
            {
                lock (this)
                {

                    if (this._currentUser == null)
                    {
                        string token = HttpUtils.GetRequestHeaderValue(HttpContext.Current.Request, CustomHttpRequestHeader.AuthorizationToken);
                        string token_Connection = HttpUtils.GetRequestHeaderValue(HttpContext.Current.Request, CustomHttpRequestHeader.AuthorizationToken_Connection);
                        if (token_Connection.IsNotNullOrEmpty())
                        {
                            token = token_Connection;
                        }

                        this._currentUser = UserSessionCache.Instance.GetUserSession(token);
                    }

                    return this._currentUser;
                }
            }
        }

        public SmtpClient SmtpClientOfCompany
        {
            get
            {
                lock (this)
                {

                    if (this.smtpClientOfCompany == null)
                    {
                        this.smtpClientOfCompany = GetSmtpClientOfCompany(this.CurrentUser.EmailServer);
                    }

                    return this.smtpClientOfCompany;
                }

            }
        }

        public SmtpClient GetSmtpClientOfCompany(EmailServerInfo emailServer)
        {
            SmtpClient smtpClientOfCompany = new SmtpClient();

            if (emailServer == null)
            {
                return smtpClientOfCompany;
            }

            smtpClientOfCompany.Host = emailServer.SMTPServer;
            smtpClientOfCompany.Port = emailServer.Port;
            smtpClientOfCompany.Credentials = new System.Net.NetworkCredential(emailServer.EmailServer, emailServer.Password);
            smtpClientOfCompany.EnableSsl = emailServer.MethodSendSSL;
            return smtpClientOfCompany;
        }

        public int GetCompanyIdOfUser()
        {
            int companyId = 0;
            if (this.CurrentUser.Company.Id.HasValue)
            {
                companyId = this.CurrentUser.Company.Id.Value;
            }

            return companyId;
        }

        public PrintConfig GetPrintConfig()
        {
            string fullPathTemplateFolder = HttpContext.Current.Server.MapPath(Config.ApplicationSetting.Instance.FolderAssetOfCompany);
            PrintConfig config = new PrintConfig(fullPathTemplateFolder);
            config.BuildAssetByCompany(this.CurrentUser.Company);
            return config;
        }

        
        public string ConvertFileToBase64(string fullPathFile)
        {
            if (!File.Exists(fullPathFile))
            {
                return string.Empty;
            }

            var bytes = File.ReadAllBytes(fullPathFile);
            return Convert.ToBase64String(bytes);
        }

        public static string SaveFileImport(string data, string suffix)
        {
            string FolderPath = HttpContext.Current.Server.MapPath(Config.ApplicationSetting.Instance.ImportFolder);

            string fileName = string.Format(@"{0}\declaration{1}.{2}", FolderPath, DateTime.Now.ToString("ddMMyyyy"), suffix);
            try
            {
                File.WriteAllBytes(fileName, Convert.FromBase64String(data));
            }
            catch (Exception ex)
            {

                throw new BusinessLogicException(ResultCode.FileNotFound, ex.Message);
            }

            return fileName;
        }
    }
}
