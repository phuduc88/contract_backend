using Contract.Business.Constants;
using Contract.Business.DAO;
using Contract.Business.Email;
using Contract.Business.Models;
using Contract.Common;
using Contract.Common.Extensions;
using Contract.Data.DBAccessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;


namespace Contract.Business.BL
{
    public class EmailActiveBO: IEmailActiveBO
    {
        #region Fields, Properties

        private readonly IEmailActiveRepository emailRepository;
        private readonly EmailConfig emailConfig;
        private readonly IMyCompanyRepository myCompanyRepository;
        #endregion

        #region Contructor

        public EmailActiveBO(IRepositoryFactory repoFactory, EmailConfig emailConfig)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.emailRepository = repoFactory.GetRepository<IEmailActiveRepository>();
            this.myCompanyRepository = repoFactory.GetRepository<IMyCompanyRepository>();
            this.emailConfig = emailConfig;
        }

        #endregion
       
        #region Methods
        public IEnumerable<EmailActiveInfo> Filter(ConditionSearchEmailActive condition, int skip = 0, int take = int.MaxValue)
        {
            if (condition == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            var emailActives = this.emailRepository.Filter(condition).Skip(skip).Take(take).ToList();
            return emailActives.Select(p => new EmailActiveInfo(p));
        }
        public int CountFilter(ConditionSearchEmailActive condition)
        {
            if (condition == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var emailActives = this.emailRepository.Filter(condition).ToList();
            return emailActives.Count();
        }

        public EmailActiveInfo GetEmailActive(int companyId, int id)
        {
            var emailActives = this.emailRepository.GetById(id);
            return new EmailActiveInfo(emailActives);
        }

        public ResultCode SendEmail(int id, EmailActiveInfo emailActiveInfo, SmtpClient smtpClientOfCompany)
        {
            if (emailActiveInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            EmailActive curentEmailActive = GetEmailActive(id, emailActiveInfo);
            Task.Run(() => SendEmail(emailActiveInfo, smtpClientOfCompany, curentEmailActive));
            return ResultCode.NoError;
        }

        public ResultCode SendEmail(int id, SmtpClient smtpClientOfCompany)
        {
            EmailActive curentEmailActive = GetEmailNotice(id);
            bool isSuccess = SendEmail(curentEmailActive, smtpClientOfCompany);
            StatusSendEmail statusSendEmail = isSuccess ? StatusSendEmail.Successfull : StatusSendEmail.Error;
            UpdateStatusSendEmail(id, statusSendEmail);
            return isSuccess ? ResultCode.NoError : ResultCode.UnknownError;
        }

        public ResultCode SendEmail(AccountInfo accountInfo, SmtpClient smtpClientOfCompany)
        {
            EmailActive curentEmailActive = this.GetEmailActiveByAccount(accountInfo.UserSID);
            if (curentEmailActive == null)
            {
                curentEmailActive = this.CreateEmailActive(accountInfo);
            }
            Task.Run(() => SendEmail(accountInfo.Email, smtpClientOfCompany, curentEmailActive));   
            return ResultCode.NoError;
        }

        public EmailActive CreateEmailActive(EmailInfo emailInfo)
        {
            if (emailInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            EmailActive emailActive = new EmailActive();
            emailActive.CopyData(emailInfo);
            emailActive.CreatedDate = DateTime.Now;
            this.emailRepository.Insert(emailActive);
            return emailActive;
        }
        #endregion

        private EmailActive GetEmailActive(int id)
        {
            EmailActive currentEmailActive = this.emailRepository.GetById(id);
            if (currentEmailActive == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, "Get EmailActive fail width id");
            }

            return currentEmailActive;
        }

        private EmailActive GetEmailActive(int id, EmailActiveInfo emailActiveInfo)
        {
            EmailActive currentEmailActive = GetEmailActive(id);
            if (currentEmailActive.Id != emailActiveInfo.Id)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, "Data information is invalid.");
            }
            currentEmailActive.EmailTo = emailActiveInfo.EmailTo;
            return currentEmailActive;
        }

        private bool SendEmail(EmailActiveInfo emailActiveInfo, SmtpClient smtpClientOfCompany, EmailActive curentEmailActive)
        {
            bool isSuccess = SendEmail(curentEmailActive, smtpClientOfCompany);
            if (isSuccess)
            {
                isSuccess = UpdateEmailActive(curentEmailActive, emailActiveInfo, StatusSendEmail.Successfull);
            }
            return isSuccess;
        }

        private bool SendEmail(string emailTo, SmtpClient smtpClientOfCompany, EmailActive curentEmailActive)
        {
            bool isSuccess = this.SendEmail(curentEmailActive, smtpClientOfCompany);
            if (isSuccess)
            {
                isSuccess = this.UpdateEmailActive(curentEmailActive, emailTo, StatusSendEmail.Successfull);
            }
            return isSuccess;
        }

        private bool SendEmail(EmailActive emailActive, SmtpClient smtpClientOfCompany)
        {
            var email = new EmailInfo()
            {
                Name = emailActive.Title,
                Subject = emailActive.Title,
                EmailTo = emailActive.EmailTo,
                Content = emailActive.ContentEmail,
            };

            email.Files = GetFileAttach(emailActive);
            ProcessEmail processEmail = new ProcessEmail();
            return processEmail.SendEmail(new SendGmail(email, smtpClientOfCompany));
        }


        private bool UpdateStatusSendEmail(int emailId, StatusSendEmail statusSendEmail)
        {
            var emailNotice = GetById(emailId);
            emailNotice.StatusSend = (int)statusSendEmail;
            emailNotice.SendtedDate = DateTime.Now;
            return this.emailRepository.Update(emailNotice);
        }

        private bool UpdateEmailActive(EmailActive currentEmailActive, string emailTo, StatusSendEmail status)
        {
            currentEmailActive.EmailTo = emailTo;
            currentEmailActive.SendtedDate = DateTime.Now;
            currentEmailActive.StatusSend = (int)status;
            return this.emailRepository.Update(currentEmailActive);
        }

        private List<File_Info> GetFileAttach(EmailActive emailActive)
        {
            List<File_Info> fileAttach = new List<File_Info>();
            if (emailActive.EmailActiveFileAttaches == null)
            {
                return fileAttach;
            }

            emailActive.EmailActiveFileAttaches.ForEach(p => {
                if (File.Exists(p.FullPathFileAttach))
                {
                    fileAttach.Add( new File_Info(p.FileName, p.FullPathFileAttach));
                }
            });

            return fileAttach;
        }

        private EmailActive GetById(int id)
        {
            var emailActive = this.emailRepository.GetById(id);
            if (emailActive == null)
            {
                throw new BusinessLogicException(ResultCode.NotFoundResourceId, MsgApiResponse.DataNotFound);
            }

            return emailActive;
        }

        private bool UpdateEmailActive(EmailActive currentEmailActive, EmailActiveInfo emailActiveInfo, StatusSendEmail status)
        {
            currentEmailActive.EmailTo = emailActiveInfo.EmailTo;
            currentEmailActive.SendtedDate = DateTime.Now;
            currentEmailActive.StatusSend = (int)status;
            return this.emailRepository.Update(currentEmailActive);
        }

        private EmailActive GetEmailNotice(int id)
        {
            var currentEmailActive = GetById(id);
            return currentEmailActive;
        }

        private EmailActive GetEmailActiveByAccount(int accountId)
        {
            return this.emailRepository.GetByAccountId(accountId);
        }

        private EmailActive CreateEmailActive(AccountInfo accountInfo)
        {
            EmailInfo emailInfo = this.BuildEmailConfig(accountInfo);
            emailInfo.AccountId = accountInfo.UserSID;
            emailInfo.CompanyId = accountInfo.CompanyId;
            return this.CreateEmailActive(emailInfo);
        }

        private EmailInfo BuildEmailConfig(AccountInfo accountInfo)
        {
            return EmailTemplate.GetEmail(this.emailConfig, (int)this.GetEmailType(accountInfo.RoleLevel), this.GetAccountActiveInfo(accountInfo));
        }

        private Email_Type GetEmailType(string accountLevel)
        {
            return accountLevel.IsEquals(RoleInfo.SALE) ? Email_Type.NoticeAccountSeller : Email_Type.NoticeAccountCustomer;
        }

        private ReceiverAccountActiveInfo GetAccountActiveInfo(AccountInfo accountInfo)
        {
            MyCompany currentCompany = this.myCompanyRepository.GetById(accountInfo.CustomerId ?? 0);
            if (currentCompany == null)
            {
                throw new BusinessLogicException(ResultCode.NotFoundResourceId, MsgApiResponse.DataInvalid);
            }
                
            return new ReceiverAccountActiveInfo()
            {
                CompanyName = currentCompany.CompanyName,
                CustomerName = currentCompany.CompanyName,
                UserId = accountInfo.UserID,
                Password = accountInfo.Password,
                Email = accountInfo.Email
            };
        }
    }
}