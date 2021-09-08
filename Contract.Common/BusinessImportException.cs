using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Common
{
   public class BusinessImportException : BusinessLogicException
    {
        public string SheetName { get; private set; }
        public string ColumnName { get; private set; }
        public int RowIndex { get; set; }
        public BusinessImportException(ResultCode errorCode, string message, string sheetName, string columnName, int rowIndex)
            : base(errorCode, message)
        {
            this.SheetName = sheetName;
            this.ColumnName = columnName;
            this.RowIndex = rowIndex;
        }
    }
}
