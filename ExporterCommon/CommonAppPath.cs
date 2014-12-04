using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExporterCommon
{
    public static class CommonAppPath
    {
        /// <summary>
        /// Sets the version of the application by checking application directory for a "debug" file.
        /// If file exist then this version is a developer version, else then this is a release.
        /// </summary>
        public static bool IsRelease()
        {
            string assemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if (System.IO.File.Exists(assemblyPath + "\\debug"))
                return false;
            else
                return true;
        }

        public static string GetCommonAppPath()
        {
            if (IsRelease())
                return System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\VariantExporter";
            else
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        }

        /// <summary>
        /// Creates a new directory, in the common app. It won't create a directory if directory already exist.
        /// </summary>
        /// <param name="dirName"></param>
        public static void CreateDirectory(string dirName)
        {
            System.IO.Directory.CreateDirectory(GetCommonAppPath() + "\\" + dirName + "\\");
        }

        /// <summary>
        /// Checks for user app data folder, and creates one if one doesn't exist
        /// </summary>
        private static void CheckUserAppDataFolder()
        {
            if (IsRelease())
            {
                if (!System.IO.Directory.Exists(System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.LocalApplicationData) + "\\VariantExporter"))
                {
                    System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.LocalApplicationData) + "\\VariantExporter");
                }
            }
        }
    }
}
