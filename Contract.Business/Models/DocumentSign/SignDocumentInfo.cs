using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business.Models
{
    public class SignDocumentInfo
    {

        public int DocumentId { get; set; }

        public int UserSignId { get; set; }

        public string FullPathFileOfCompany { get; set; }
    }
}
