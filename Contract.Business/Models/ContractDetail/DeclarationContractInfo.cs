using Contract.Business.Constants;

namespace Contract.Business.Models
{
    public class DeclarationContractInfo
    {
        public string DeclarationCode { get; set; }

        public string DeclararionName { get; set; }

        public DeclarationContractInfo()
        {

        }

        public DeclarationContractInfo(bool isSwichVendor)
            : this()
        {
             
        }
    }
}
