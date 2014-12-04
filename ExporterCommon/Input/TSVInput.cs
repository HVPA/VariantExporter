using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ExporterCommon.Input
{
    public class TSVInput
    {
        public string TSV_Source { get; set; }

        public TSVInput(string source)
        {
            TSV_Source = source;
        }

        public DataTable GetDataFromRows()
        {
            StreamReader str = new StreamReader(TSV_Source);
            // get the column count
            int columnCount = str.ReadLine().Split('\t').Length;

            str.Dispose();

            StreamReader sr = new StreamReader(TSV_Source);

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
                string[] values = line.Split('\t');

                DataRow dr = dt.NewRow();

                // copy data across to dt
                for (int i = 0; i < columnCount; i++)
                {
                    dr[i] = values[i];
                }

                dt.Rows.Add(dr);
            }

            // release all resources held by sr
            sr.Dispose();

            return dt;
        }
    }
}
