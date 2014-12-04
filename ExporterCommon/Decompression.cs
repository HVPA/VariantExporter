using System;
using System.Collections.Generic;
using System.Text;
using Ionic.Zip;

namespace ExporterCommon
{
    public static class Decompression
    {
        // using dotnetzip from codeplex to extract zip file contents to the specified directory.
        public static void Decompress(string zipPath, string extractPath)
        {
            using (ZipFile zip = ZipFile.Read(zipPath))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        //compresses a list of files to the specified zipped file name
        public static void Compress(List<string> fileList, string zippedFile)
        {
            using (ZipFile zip = new ZipFile())
            {
                foreach (string file in fileList)
                {
                    zip.AddFile(file, "");
                }
                
                // save the zip file
                zip.Save(zippedFile + ".zip");
            }
        }

        // compresses the specified directory to the specified zipped file name
        public static void Compress(string sourceDirectory, string zippedFile)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(sourceDirectory);

                // save the zip file
                zip.Save(zippedFile + ".zip");
            }
        }

        public static void ZippUpTransferFiles(string zippedFileName, string encryptedFilePath, 
            string commonAppPath, string orgHashCode, Log log)
        {
            //string zippedFileName = conf.OrganisationHashCode + "_TestEncryption";
            
            // list of files to be zipped up
            List<string> files = new List<string>();

            // create empty file that has org id eg: <org_id>.org
            System.IO.File.Create(commonAppPath + orgHashCode + ".org").Dispose();

            files.Add(commonAppPath  + orgHashCode + ".org");
            files.Add(encryptedFilePath);
            files.Add(encryptedFilePath + ".session");
            files.Add(encryptedFilePath + ".sig");

            if (log != null)log.write("Zipping encrypted files");
            string zippedFilePath = commonAppPath + zippedFileName;

            // zip up files
            Compress(files, zippedFilePath);
            if (log != null)log.write("Zipping files complete");
        }
    }
}
