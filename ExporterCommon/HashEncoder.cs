using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using ExporterCommon.Core.StandardColumns;

namespace ExporterCommon
{
    public static class HashEncoder
    {
        /// <summary>
        /// Converts a string to a Base64 string.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string Encode(string inputString)
        {
            byte[] input = Encoding.UTF8.GetBytes(inputString);
            return Convert.ToBase64String(input);
        }

        /// <summary>
        /// Decodes a Base64 string back into a readable string.
        /// NB: This function is not really needed, but nice to have just in case ;)
        /// </summary>
        /// <param name="encodedString"></param>
        /// <returns></returns>
        public static string Decode(string encodedString)
        {
            byte[] input = Convert.FromBase64String(encodedString);
            return Encoding.UTF8.GetString(input);
        }

        /// <summary>
        /// Appends all columns in a datarow and generates a hash string using
        /// Base64 conversion.
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string EncodeDataRow(DataRow dr)
        {
            // string to store all the values in the columns of a data row
            string columnAppend = "";
            
            DataTable dt = dr.Table;
            foreach (DataColumn dc in dt.Columns)
            {
                // ignore the status field as this field is dynamically 
                // modified by the exporter.
                if (dr[dc] != dr[VariantInstance.Status])
                    columnAppend = columnAppend + dr[dc].ToString();
            }

            // return hash encoded string
            return Encode(columnAppend);
        }
    }
}
