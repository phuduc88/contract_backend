using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business
{
    public class SpireXls
    {
        public Workbook Workbook;
        public Worksheet Worksheet;

        public SpireXls(string fullPathFile)
        {
            Active(fullPathFile);
        }


        private void Active(string fullPathFile)
        {
            Workbook = new Workbook();
            Workbook.LoadFromFile(fullPathFile);
            Worksheet = Workbook.Worksheets[0];
        }
    }
}
