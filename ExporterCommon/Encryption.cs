// TODO: URL of where this comes from Melvin!

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

namespace ExporterCommon
{
    public static class Encryption
    {
        /// <summary>
        /// Encrypts a string
        /// </summary>
        /// <param name="PlainText">Text to be encrypted</param>
        /// <param name="Password">Password to encrypt with</param>
        /// <param name="Salt">Salt to encrypt with</param>
        /// <param name="HashAlgorithm">Can be either SHA1 or MD5</param>
        /// <param name="PasswordIterations">Number of iterations to do</param>
        /// <param name="InitialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="KeySize">Can be 128, 192, or 256</param>
        /// <returns>An encrypted string</returns>
        public static string Encrypt(string PlainText, string Password,
            string Salt = "Kosher", string HashAlgorithm = "SHA1",
            int PasswordIterations = 2, string InitialVector = "OFRna73m*aze01xY",
            int KeySize = 256)
        {
            if (string.IsNullOrEmpty(PlainText))
                return "";

            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] CipherTextBytes = null;
            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
                        CryptoStream.FlushFinalBlock();
                        CipherTextBytes = MemStream.ToArray();
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();

            return Convert.ToBase64String(CipherTextBytes);
        }

        /// <summary>
        /// Decrypts a string
        /// </summary>
        /// <param name="CipherText">Text to be decrypted</param>
        /// <param name="Password">Password to decrypt with</param>
        /// <param name="Salt">Salt to decrypt with</param>
        /// <param name="HashAlgorithm">Can be either SHA1 or MD5</param>
        /// <param name="PasswordIterations">Number of iterations to do</param>
        /// <param name="InitialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="KeySize">Can be 128, 192, or 256</param>
        /// <returns>A decrypted string</returns>
        public static string Decrypt(string CipherText, string Password,
            string Salt = "Kosher", string HashAlgorithm = "SHA1",
            int PasswordIterations = 2, string InitialVector = "OFRna73m*aze01xY",
            int KeySize = 256)
        {
            if (string.IsNullOrEmpty(CipherText))
                return "";

            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] CipherTextBytes = Convert.FromBase64String(CipherText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
            int ByteCount = 0;

            using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                    {
                        ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();

            return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);
        }

        /// <summary>
        /// Encrypts file using the HVP_Encryption.exe, must specify the path of where
        /// the HVP_Encryption.exe is located.
        /// </summary>
        /// <param name="HVPEncryptionExePath">Path of HVP_Encryption.exe</param>
        /// <param name="filePath">File path of file to encrypt</param>
        /// <param name="encryptedFilePath">Output path of encrypted file</param>
        /// <param name="keyPath">Path of where user specific files are</param>
        /// <param name="privateKey">Private Key name</param>
        /// <param name="publicKey">Public Key name</param>
        /// <param name="log">Logger object used to log the status, set null if not in use</param>
        public static void HVP_EncryptFile(string HVPEncryptionExePath, string filePath, string encryptedFilePath, 
            string keyPath, string privateKey, string publicKey, Log log)
        {
            // Check to see if the HVP_Encryption.exe file exist
            if (!System.IO.File.Exists(HVPEncryptionExePath))
            {
                if (log != null) log.write("Could not locate the HVP_Encryption.exe file, please check file path: " + HVPEncryptionExePath);
                throw new Exception("Can not find HVP_Encryption.exe file");
            }

            if (log != null) log.write("Encrypting files");
            
            //Encrypt test file
            Process process = new Process();
            
            //string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            //process.StartInfo.FileName = appPath + "\\Executables\\HVP_Encryption.exe";
            process.StartInfo.FileName = HVPEncryptionExePath;

            // encryption parameters goes here
            process.StartInfo.Arguments = "-e --public \"" + keyPath + "\\" + publicKey +
                "\" --private \"" + keyPath + "\\" + privateKey +
                "\" --input \"" + filePath +
                "\" --output \"" + encryptedFilePath + "\"";

            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            process.Close();

            // check if files exist, if no files are missing then the encryption process 
            // has failed silently
            if (!System.IO.File.Exists(encryptedFilePath) ||
                !System.IO.File.Exists(encryptedFilePath + ".session") ||
                !System.IO.File.Exists(encryptedFilePath + ".sig"))
            {
                if (log != null) log.write("Failed to Encrypt files!!!");
                throw new Exception("Encryption failed! Failed to encrypt the files properly, check public and private keys are correct.");
            }
            if (log != null)  log.write("Encrypting files complete");
        }
    }
}
