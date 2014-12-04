using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExporterCommon.Input
{
    public class CSVInput
    {
        public string CSV_Source { get; set; }
        public string GetConnectionString()
        {
            FileInfo file = new FileInfo(CSV_Source);
            
            return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file.DirectoryName +
                ";Extended Properties='text;HDR=Yes;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text;FMT=Delimited(,);'";
        }

        private OleDbConnection myConnection = null;

        /// <summary>
        /// Gets the all the data from the csv and renders it to a DataTable as it is exactly
        /// in the csv format.
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataFromRows()
        {
            StreamReader str = new StreamReader(CSV_Source);
            // get the column count
            int columnCount = str.ReadLine().Split(',').Length;
            str.Dispose();

            StreamReader sr = new StreamReader(CSV_Source);

            DataTable dt = new DataTable();

            //create the number of columns for that table
            for (int i = 0; i < columnCount; i++)
            {
                DataColumn dc = new DataColumn(i.ToString(), typeof(string));
                dt.Columns.Add(dc);
            }

            string line;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();

                // convert line to string array
                string[] values = line.Split(',');
                DataRow dr = dt.NewRow();

                // copy data across to dt
                for (int i = 0; i < values.Length; i++)
                {
                    dr[i] = values[i];
                }

                dt.Rows.Add(dr);
            }

            // release all resources held by sr
            sr.Dispose();

            return dt;
        }
        
        /// <summary>
        /// Gets the headers from csv, which will form the the datatable column headers and align all the 
        /// data from the specified start column into the corresponding data columns.
        /// </summary>
        /// <param name="headerRow">The row number that contains the heards from the csv. Index start at 0</param>
        /// <param name="startColumn">The starting column number from the csv. Index start at 0</param>
        /// <returns></returns>
        public DataSet GetDataFromRows(int headerRow, int startColumn)
        {
            DataSet ds = new DataSet();

            // populate the datatable with data from csv
            DataTable dt = GetDataFromRows();

            // table to store the filtered results from dt
            DataTable dtResults = new DataTable();

            // get header columns for the results table
            DataRow drHeaders = dt.Rows[headerRow];

            // create the headers for our result table
            for (int i = startColumn; i < dt.Columns.Count; i++)
            {
                // add the header columns to the result table
                DataColumn dc = new DataColumn(drHeaders[i].ToString(), typeof(string));
                dtResults.Columns.Add(dc);   
            }

            // get the raw data from dt to the result table with the proper column headers
            // Assumption: The data should be 1 row below the headers so we increment it by 1
            for (int rows = headerRow + 1; rows < dt.Rows.Count; rows++)
            {
                DataRow dr = dtResults.NewRow();
                int drColCount = 0;
                for (int i = startColumn; i < dt.Columns.Count; i++)
                {
                    dr[drColCount] = dt.Rows[rows][i];
                    drColCount++;
                }

                dtResults.Rows.Add(dr);
            }

            ds.Tables.Add(dtResults);

            return ds;
        }

        public CSVInput(string CSV_Source)
        {
            this.CSV_Source = CSV_Source;
        }

        public void Open()
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

        public DataSet Get(string sql)
        {
            OleDbDataAdapter da = new OleDbDataAdapter(sql, myConnection);

            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
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
