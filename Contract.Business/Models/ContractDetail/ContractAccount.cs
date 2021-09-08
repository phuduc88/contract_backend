
namespace Contract.Business.Models
{
    public class ContractAccount
    {
        public string UserId { get; set; }

        public string UserName{ get; set; }

        public int? CompanyId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Level_Role { get; set; }

        public int? ClientId { get; set; }

        public ContractAccount()
        {

        }
    }
}
