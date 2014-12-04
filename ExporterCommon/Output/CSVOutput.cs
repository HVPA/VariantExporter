using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Office.Interop.Excel;

namespace ExporterCommon.Output
{
    public class CSVOutput
    {
        /// <summary>
        /// Generates an excel spreadsheet worksheet to an csv.
        /// </summary>
        /// <param name="spreadSheetPath">Location path of the spreadsheet</param>
        /// <param name="worksheet">Worksheet tab that will be rendered to csv</param>
        /// <param name="destination">Destination path to where new csv file will saved</param>
        public void Generate(string spreadSheetPath, string worksheet, string destination)
        {
            // get file name from path
            string fileName = Path.GetFileNameWithoutExtension(spreadSheetPath);
            string path = Path.GetFullPath(spreadSheetPath).Replace(Path.GetFileName(spreadSheetPath), "");
            
            Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            ObjExcel.Visible = false;
            ObjExcel.DisplayAlerts = false;

            Workbook ObjWorkBook = null;
            Worksheet ObjWorkSheet = null;

            try
            {
                ObjWorkBook = ObjExcel.Workbooks.Open(spreadSheetPath, Type.Missing, Type.Missing, Type.Missing, 
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // get the worksheet from the workbook
                foreach (Worksheet sheet in ObjWorkBook.Worksheets)
                {
                    if (sheet.Name == worksheet)
                    {
                        ObjWorkSheet = sheet;
                        break;
                    }
                }

                if (ObjWorkSheet == null)
                    throw new Exception("Could not locate worksheet: " + worksheet + ", check that the name is correct.");
            
                // when overwriting a file the alert dialog always appear, checking if the file exist
                // and then removing the file is the only way to overwrite the file.
                if (File.Exists(destination + fileName + ".csv"))
                    File.Delete(destination + fileName + ".csv");

                ObjWorkSheet.SaveAs(destination + fileName + ".csv", XlFileFormat.xlCSV, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                //throw new Exception("Can not write to app's directory, check user permissions for app directory");
            }
            finally
            {
                //ObjWorkBook.Close(XlSaveAction.xlSaveChanges, Type.Missing, Type.Missing);
                ObjWorkBook.Close(false, false, Type.Missing);
                ObjExcel.Quit();
            }
        }
    }
}
