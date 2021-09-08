using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using Contract.Common;
using Contract.Business.Extensions;

namespace Contract.Business
{
    public class ImportExcel
    {
        private const string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=YES;'";
        private readonly OleDbConnection oleDbConnection = null;
        private static Logger logger = new Logger();
        public Dictionary<string, DataTable> DicMasterData = null;

        public ImportExcel(string fullPathToExcel, IEnumerable<string> sheetNames)
        {
            oleDbConnection = new OleDbConnection(string.Format(ConnectionString, fullPathToExcel));
            DicMasterData = LoadData(sheetNames);
        }

        public DataTable GetBySheetName(string sheetName)
        {
            if (!DicMasterData.ContainsKey(sheetName))
            {
                return null;
            }

            return DicMasterData[sheetName];
        }

        public DataTable GetBySheetName(string sheetName, List<string> columnImport)
        {
            DataTable dataBysheetName = GetBySheetName(sheetName);
            if(dataBysheetName == null)
            {
                return null;
            }

            dataBysheetName.TableName = sheetName;
            return dataBysheetName.RemoveRowSpace();

        }

        private Dictionary<string, DataTable> LoadData(IEnumerable<string> sheetNames)
        {
            Dictionary<string, DataTable> masterData = new Dictionary<string, DataTable>();
            try
            {
                using (oleDbConnection)
                {
                    oleDbConnection.Open();
                    DataTable dtSheet = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    foreach (DataRow item in dtSheet.Rows)
                    {
                        string sheetName = item["TABLE_NAME"].ToString().Replace("$", "");
                        if(!sheetNames.Contains(sheetName))
                        {
                            continue;
                        }

                        DataTable dataOfSheet = GetDataBySheetName(sheetName, oleDbConnection);
                        if (masterData.ContainsKey(sheetName))
                        {
                            masterData[sheetName] = dataOfSheet;
                        }
                        else
                        {
                            masterData.Add(sheetName, dataOfSheet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("File import format invalid", ex);
                //throw new BusinessImportException(ResultCode.ImportFileFormatInvalid, ex.Message, string.Empty, string.Empty, 0);
            }

            return masterData;
        }

        private DataTable GetDataBySheetName(string sheetName, OleDbConnection oleDbConnection)
        {
            string queryData = string.Format("SELECT * from [{0}$]", sheetName);
            DataTable data = new DataTable();
            using (OleDbCommand oleDbCommand = new OleDbCommand(queryData, oleDbConnection))
            {
                using (OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader())
                {
                    data.Load(oleDbDataReader);
                    return data;
                }
            }
        }
    }
}
