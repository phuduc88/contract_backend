using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Net.Mail;

namespace Contract.Business.BL
{
    public interface IEmailActiveBO
    {
        IEnumerable<EmailActiveInfo> Filter(ConditionSearchEmailActive condition, int skip = 0, int take = int.MaxValue);

        int CountFilter(ConditionSearchEmailActive condition);

        EmailActiveInfo GetEmailActive(int companyId, int id);

        ResultCode SendEmail(int id, EmailActiveInfo emailActiveInfo, SmtpClient smtpClientOfCompany);

        ResultCode SendEmail(int id, SmtpClient smtpClientOfCompany);

        ResultCode SendEmail(AccountInfo accountInfo, SmtpClient smtpClientOfCompany);

        EmailActive CreateEmailActive(EmailInfo emailInfo);

    }
}
