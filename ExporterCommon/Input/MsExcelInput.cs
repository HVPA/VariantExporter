using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text;

namespace ExporterCommon.Input
{
    public class MsExcelInput
    {
        public string SpreadSheetSource { get; set; }
        public string GetConnectionString()
        {
            string extension = System.IO.Path.GetExtension(SpreadSheetSource);

            if (extension == ".xls")
            {
                // NB: Jet can not handle .xlsm(macro spreadsheets) and .xlsx files. You will need to use Ace see below
                return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SpreadSheetSource + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text;\";";
            }
            else
            {
                // In order to use this connection string you must have the access 2007 database engine installed on the client machine
                // can be downloaded from here: http://www.microsoft.com/download/en/details.aspx?id=23734
                return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SpreadSheetSource + @";Extended Properties=""Excel 12.0 Macro;HDR=YES"";";
            }
        }

        private OleDbConnection myConnection = null;

        public MsExcelInput(string spreadSheetSource)
        {
            SpreadSheetSource = spreadSheetSource;
        }

        public void open()
        {
            if (myConnection == null)
            {
                myConnection = new OleDbConnection(GetConnectionString());
                myConnection.Open();
            }
        }

        public void Close()
        {
            if (myConnection != null)
            {
                myConnection.Close();
                myConnection = null;
            }
        }

        /// <summary>
        /// Gets the list of sheetnames from the spreadsheet
        /// </summary>
        /// <returns></returns>
        public DataTable GetListOfSheetNames()
        {
            myConnection = new OleDbConnection(GetConnectionString());
            myConnection.Open();
            DataTable dt = myConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            myConnection.Close();

            return dt;
        }

        /// <summary>
        /// For spreadsheet sql you need to specify the workbook as the table to select from.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet Get(string sql)
        {
            //OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Book1.xls;Extended Properties=Excel 8.0");
            //OleDbDataAdapter da = new OleDbDataAdapter("select * from MyObject", con);
            OleDbDataAdapter da = new OleDbDataAdapter(sql, myConnection);

            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }


        /// <summary>
        /// For spreadsheet sql you need to specify the workbook as the table to select from.
        /// To specify the workbook as a table it must be in this format: [Sheet1$].
        /// Eg: SELECT * FROM [WorkBook_Name$]
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet OpenGetClose(string sql)
        {
            DataSet result = null;

            open();
            result = Get(sql);
            Close();

            return result;
        }
    }
}
