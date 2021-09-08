using Contract.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business
{
   public interface IExportFile
    {
       FileExport ExportFile();
    }
}
