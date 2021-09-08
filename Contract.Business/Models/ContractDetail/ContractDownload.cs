using Contract.Data.Utils;
using System;

namespace Contract.Business.Models
{
    public class ContractDownload
    {
        [DataConvert("Id")]
        public int ContractId { get; set; }

        [DataConvert("CustomerId")]
        public int CustomerId { get; set; }

        [DataConvert("No")]
        public string ContractNo { get; set; }

        [DataConvert("DatePayment")]
        public DateTime ContractDate { get; set; }

        public CompanyInfo CompanyInfo { get; set; }

        public CustomerInfo Customer { get; set; }

        public Decimal NumberInvoice { get; set; }

        public string ItemInvoiceRegister { get; set; }
        public ContractDownload()
        {

        }
        public ContractDownload(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, ContractDownload>(srcObject, this);
            }
        }

        public ContractDownload(object srcObject, object srcCustomer)
            : this(srcObject)
        {
            if (srcCustomer != null)
            {
                this.Customer = srcCustomer != null ? new CustomerInfo(srcCustomer) : new CustomerInfo();
            }
        }
    }
}
