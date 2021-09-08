
namespace Contract.Business.Models
{
    public class CustomerOfCompany
    {
        public int CompanySID { get; set; }

        public int? CustomerIdOfCompany { get; set; }
        public string CompanyName { get; set; }

        public string CustomerName { get; set; }

        public string TaxCode { get; set; }

        public string Email { get; set; }

        public string PersonContact { get; set; }

        public string Tel { get; set; }

        public string Level_Customer { get; set; }


        public CustomerOfCompany()
        {

        }
    }
}
