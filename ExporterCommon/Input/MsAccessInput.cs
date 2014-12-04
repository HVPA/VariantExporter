using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text;

namespace ExporterCommon.Input
{
    public class MsAccessInput
    {
        public string DatabaseSource { get; set; }
        public string GetConnectionString()
        {
            return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DatabaseSource + ";";
        }

        private OleDbConnection myConnection = null;

        public MsAccessInput(string databaseSource) 
        {
            DatabaseSource = databaseSource;
        }

        public void Open()
        {
            if (myConnection == null) {
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

        public DataSet Get(string sql)
        {
            DataSet result = null;

            OleDbDataAdapter objAdapter = new OleDbDataAdapter(sql, myConnection);
            result = new DataSet();
            objAdapter.Fill(result, "results");

            return result;
        }

        public DataSet OpenGetClose(string sql)
        {
            DataSet result = null;

            Open();
            result = Get(sql);
            Close();

            return result;
        }
    }
}
