using Contract.Business.Constants;
using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System;

namespace Contract.Business.BL
{
  public class Email
    {
       private readonly IEmailActiveRepository emailRepository;
       public Email(IRepositoryFactory repoFactory)
      {
          Ensure.Argument.NotNull(repoFactory, "repoFactory");
          this.emailRepository = repoFactory.GetRepository<IEmailActiveRepository>();
      }

       public int CreateEmailActive(EmailInfo emailInfo, int companyId, int typeEmail = 1)
       {
           var emailActive = new EmailActive();
           emailActive.CopyData(emailInfo);
           emailActive.CompanyID = companyId;
           emailActive.StatusSend = (int)StatusSendEmail.New;
           emailActive.TypeEmail = typeEmail;
           emailActive.CreatedDate = DateTime.Now;
           this.emailRepository.Insert(emailActive);
           return emailActive.Id;
       }
      
    }
}
