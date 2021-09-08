using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business.Email
{
   public interface IProcessEmail
    {
       bool SendEmail(IEmail email);
    }
}
