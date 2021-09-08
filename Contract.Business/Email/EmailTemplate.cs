using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using Contract.Common.Extensions;
using System;
using System.IO;
using System.Text;

namespace Contract.Business.Email
{
    public static class EmailTemplate
    {
        private static readonly Logger logger = new Logger();
        private const string DefaulCultureInfo = "vi-VN";
        public static EmailInfo GetEmail(EmailConfig emailConfig, int typeEmail,ReceiverInfo receiverInfo)
        {
            string templateEmail = GetTemplateFile(typeEmail);
            EmailInfo emailInfo = GetEmailInfo(emailConfig.FolderEmailTemplate, templateEmail);
            StandardizedContentEmail(emailInfo, receiverInfo);
            return emailInfo;
        }

        public static EmailInfo GetEmail(EmailConfig emailConfig, int typeEmail, ReceiverAccountActiveInfo receiverInfo)
        {
            string templateEmail = GetTemplateFile(typeEmail);
            EmailInfo emailInfo = GetEmailInfo(emailConfig.FolderEmailTemplate, templateEmail);
            StandardizedContentEmail(emailInfo, receiverInfo);
            return emailInfo;
        }
        public static EmailInfo GetEmail(EmailConfig emailConfig, string  typeEmail, ReceiverInfo receiverInfo)
        {
            EmailInfo emailInfo = GetEmailInfo(emailConfig.FolderEmailTemplate, string.Format("{0}.txt", typeEmail));
            StandardizedContentEmail(emailInfo, receiverInfo);
            return emailInfo;
        }


        #region Private methods

        private static string GetTemplateFile(int emailType)
        {
            string template = string.Empty;
            switch (emailType)
            {
                case (int)Email_Type.NoticeAdjustInfomation:
                    template = "Notice_AdjustmentInfomation_Invoice.txt";
                    break;
                case (int)Email_Type.NoticeAdjustDown:
                    template = "Notice_Down_Invoice.txt";
                    break;
                case (int)Email_Type.NoticeAdjustUp:
                    template = "Notice_Up_Invoice.txt";
                    break;
                case (int)Email_Type.NoticeAdjustSubstitute:
                    template = "Notice_Substitute_Invoice.txt";
                    break;
                case (int)Email_Type.NoticeAccountCustomer:
                    template = "Customer_Active_account.txt";
                    break;
                case (int)Email_Type.NoticeAccountSeller:
                    template = "Seller_Active_account.txt";
                    break;
                case (int)Email_Type.SendVerificationCode:
                    template = "Notice_VerificationCode.txt";
                    break;
                default:
                    template = "Notice_Release_Invoice.txt";
                    break;
            }

            return template;
        } 
        private static EmailInfo GetEmailInfo(string fullFilePathTempale, string templateEmail)
        {
            try
            {
                var emailContent = new EmailInfo();
                var fullFilePath = GetFullFilePath(fullFilePathTempale, templateEmail);
                if (!File.Exists(fullFilePath))
                {
                    throw new Exception("File not exist" + fullFilePath);
                }

                string[] contentTemplates = System.IO.File.ReadAllLines(fullFilePath);
                StringBuilder contentEmail = new StringBuilder();
                int numberOfDocument = 0;
                foreach (var line in contentTemplates)
                {
                    if (numberOfDocument == 0)
                    {
                        emailContent.Subject = line;
                    }
                    else
                    {
                        contentEmail.AppendLine(line);
                    }

                    numberOfDocument++;
                }

                if (contentEmail.Length == 0 || emailContent.Subject.IsNullOrEmpty())
                {
                    throw new Exception("Template file Invalid format");
                }

                emailContent.Content = contentEmail.ToString();

                return emailContent;
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message, fullFilePathTempale, templateEmail);
                return null;
            }
        }

        private static string GetFullFilePath(string fullFilePathTempale, string fileName)
        {
            return Path.Combine(fullFilePathTempale, fileName);
        }

        private static void StandardizedContentEmail(EmailInfo emailInfo, ReceiverInfo receiverInfo)
        {
            emailInfo.Name = receiverInfo.UserName;
            emailInfo.EmailTo = receiverInfo.Email;
            emailInfo.Content = emailInfo.Content.Replace(PlaceHolder.PlaceHolderEmail, receiverInfo.Email)
                .Replace(PlaceHolder.PlaceHolderUrl, receiverInfo.UrlResetPassword)
                .Replace(PlaceHolder.PlaceHolderUserId, receiverInfo.UserId);
        }


        private static void StandardizedContentEmail(EmailInfo emailInfo, ReceiverAccountActiveInfo receiverInfo)
        {
            emailInfo.Name = receiverInfo.UserName;
            emailInfo.EmailTo = receiverInfo.Email;
            emailInfo.Content = emailInfo.Content.Replace(PlaceHolder.PlaceHolderEmail, receiverInfo.Email)
                .Replace(PlaceHolder.PlaceHolderCompanyName, receiverInfo.CompanyName)
                .Replace(PlaceHolder.PlaceHolderCustomerName, receiverInfo.CustomerName)
                .Replace(PlaceHolder.PlaceHolderUserId, receiverInfo.UserId)
                .Replace(PlaceHolder.PlaceHolderPassword, receiverInfo.Password);
        }
        #endregion
    }
}
