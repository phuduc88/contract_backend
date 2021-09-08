using System;
using System.Net.Mail;
using Contract.Business.Models;
using Contract.Business.Email;
using Contract.Common;

namespace Contract.Business
{
    public class SendGmail : IEmail
    {
        private static Logger logger = new Logger();
        private readonly EmailInfo email;
        private readonly SmtpClient smtpClient;
        public SendGmail(EmailInfo email, SmtpClient smtpClient)
        {
            this.email = email;
            this.smtpClient = smtpClient;
        }

        public bool Send()
        {
            return SendEmail();
        }

        private bool SendEmail()
        {
            bool resultSendEmail = true;

            try
            {
                MailMessage mailMessage = new MailMessage();
                string[] emailsTo =  email.EmailTo.Split(',');
                if (emailsTo.Length == 0)
                {
                    return false;
                }

                foreach (var emailTo in emailsTo)
                {
                    mailMessage.To.Add(new MailAddress(emailTo, email.Name));
                }
               
                mailMessage.Subject = email.Subject;
                mailMessage.Body = email.Content;
                mailMessage.IsBodyHtml = true;
                if (email.Files.Count > 0)
                {
                    email.Files.ForEach(p => {
                        mailMessage.Attachments.Add(new Attachment(p.FullPathFileattached));
                    });
                }
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                resultSendEmail = false;
                logger.Error(ex, "Send email error", null);
            }

            return resultSendEmail;
        }
    }
}
