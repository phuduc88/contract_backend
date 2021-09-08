
namespace Contract.Business.Email
{
    public class ProcessEmail : IProcessEmail
    {
        public bool SendEmail(IEmail email)
        {
             return email.Send();
        }
    }
}
