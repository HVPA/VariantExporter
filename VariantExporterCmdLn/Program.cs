using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.IO;

using ExporterCommon;
using ExporterCommon.Plugins;

namespace VariantExporterCmdLn
{
    public class Program
    {
        private static Configuration _conf;
        
        private static string xmlFilePath;
        private static string xmlFilePath_NonSending;
        // default values are false unless specified via cmd parameters
        private static bool testSend = false;
        private static bool testConn = false;
        
        private static bool help = false;

        /// <summary>
        /// Command line exit Code Summary:
        /// 0 – Application completed its task with no errors
        /// 1 – Configuration.xml error
        /// 2 – HVP server connection error
        /// 3 – Client proxy settings error
        /// 4 – HVP xml data file read error
        /// 5 – Encryption xml data error
        /// 6 – Zipping encrypted file error
        /// 7 - Error clearing cache data
        /// </summary>

        public static void Main(string[] args)
        {
            // Stops the WebRequest from adding a Expect: 100-Continue" http header when
            // connecting to the web via a proxy setup
            System.Net.ServicePointManager.Expect100Continue = false;

            // path directory where exe is running from
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            // if checks all files are here
            if (!CheckFiles(path))
            {
                System.Environment.Exit(1);
            }

            Arguments arguments = new Arguments(args);

            // get list of arguments
            GetArguments(arguments);
            
            // check xmlfilepath is available to start transmission process
            if (!string.IsNullOrEmpty(xmlFilePath))
            {
                string timestamp = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
                string zippedFileName = _conf.OrganisationHashCode + "_" + timestamp;
                
                EncryptAndZipData(path, xmlFilePath, zippedFileName, timestamp);
                
                // get proxy settings, if any
                WebClient client = new WebClient();
                SetProxy(client);

                // get the url address
                string serverAddress = _conf.ServerAddress;
                if (testSend) // if sending to test server, change address here
                    serverAddress = _conf.TestServerAddress;

                // send data
                string sent = SendData(path, zippedFileName, serverAddress, client);

                // check for errors
                if (sent != "<HTML>POST OK.</HTML>")
                {
                    Console.WriteLine("Send failed, Server may not be responding or check settings and try again");
                    Console.WriteLine("Message from server: " + sent);

                    System.Environment.Exit(2);
                }

                ClearCache(path);
                
                Console.WriteLine("Data sent successfully");

                // program completed with no errors
                System.Environment.Exit(0);
            }

            if (!string.IsNullOrEmpty(xmlFilePath_NonSending))
            {
                string timestamp = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
                string zippedFileName = _conf.OrganisationHashCode + "_" + timestamp;

                EncryptAndZipData(path, xmlFilePath_NonSending, zippedFileName, timestamp);
            }
                
            // test connection
            if (testConn)
            {
                // create a test file to sent server
                string testFilePath = path + "\\" + _conf.Output + "\\testfile";
                System.IO.File.Create(testFilePath).Dispose();
                System.IO.File.AppendAllText(testFilePath, "Test HVP Connectivity");

                string timestamp = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
                string encryptedFile = timestamp + ".txt";
                string zippedFileName = _conf.OrganisationHashCode + "_TestEncryption";

                //Encrypt data
                EncryptData(path, timestamp, encryptedFile, testFilePath);
                    
                // Zip data
                ZipData(path, timestamp, encryptedFile, zippedFileName);

                // get proxy
                WebClient client = new WebClient();
                SetProxy(client);
                    
                // send data
                string sent = SendData(path, zippedFileName, _conf.ServerAddress, client);

                // check for errors
                if (sent != "<HTML>TEST POST SUCCESSFUL.</HTML>")
                {
                    Console.WriteLine("Send failed, Server may not be responding or check settings and try again");
                    Console.WriteLine("Message from server: " + sent);
                        
                    System.Environment.ExitCode = 2;
                }
                else
                    Console.WriteLine("Connection test successful");

                // delete the test file
                System.IO.File.Delete(testFilePath);

                ClearCache(path);
            }

            // display help menu
            if (help || args.Count() == 0)
            {
                DisplayHelpOptions();
            }
            
        }

        private static void EncryptAndZipData(string path, string xmlFilePath, 
            string zippedFileName, string timestamp)
        {
            // check xmlFilePath exist
            if (!System.IO.File.Exists(xmlFilePath))
            {
                Console.WriteLine("Could not load file: " + xmlFilePath);
                System.Environment.Exit(4);
            }

            // current timestamp
            //string timestamp = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
            string encryptedFile = timestamp + ".txt";
            //string zippedFileName = _conf.OrganisationHashCode + "_" + timestamp;

            // encrypted data using HVP_Encryption.exe and public and private keys
            EncryptData(path, timestamp, encryptedFile, xmlFilePath);

            // set zip file name
            ZipData(path, timestamp, encryptedFile, zippedFileName);
        }

        private static void SetProxy(WebClient client)
        {
            try
            {
                if (_conf.ProxyAddress != string.Empty)
                {
                    // if using an auto conf script
                    WebProxy proxy;
                    if (_conf.ProxyPort != string.Empty)
                        proxy = new WebProxy(_conf.ProxyAddress, int.Parse(_conf.ProxyPort));
                    else
                        proxy = new WebProxy(_conf.ProxyAddress);
                    proxy.Credentials = new NetworkCredential(_conf.ProxyUser, _conf.ProxyPassword, _conf.ProxyUserDomain);
                    client.Proxy = proxy;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Proxy setting error, check proxy settings in the Configuration.xml");
                Console.WriteLine(ex.Message.ToString());

                System.Environment.Exit(3);
            }
        }

        private static void EncryptData(string path, string timestamp, string encryptedFile, string xmlFilePath)
        {
            try
            {
                Encryption.HVP_EncryptFile(path + "\\HVP_Encryption.exe", xmlFilePath,
                    path + "\\" + _conf.Output + "\\" + encryptedFile, path,
                    _conf.PrivateKey, _conf.PublicKey, null);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Encrypting data error");
                Console.WriteLine(ex.Message.ToString());

                System.Environment.Exit(5);
            }
        }

        private static void ZipData(string path, string timestamp, string encryptedFile, string zippedFileName)
        {
            try
            {
                Decompression.ZippUpTransferFiles(zippedFileName, path + "\\" + _conf.Output +
                    "\\" + encryptedFile, path + "\\" + _conf.Output + "\\", _conf.OrganisationHashCode, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Compressing data error");
                Console.WriteLine(ex.Message.ToString());

                System.Environment.Exit(6);
            }
        }

        private static string SendData(string path, string zippedFileName, string serverAddress, WebClient client)
        {
            try
            {
                string sent = Transmit.SendData(path + "\\" + _conf.Output + "\\" + zippedFileName + ".zip",
                    serverAddress, client, null);

                return sent;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sending data error");
                Console.WriteLine(ex.Message.ToString());

                System.Environment.ExitCode = 2;
                return "";
            }
        }

        private static void ClearCache(string path)
        {
            try
            {
                System.IO.DirectoryInfo output = new System.IO.DirectoryInfo(path + "\\" + _conf.Output);
                foreach (System.IO.FileInfo file in output.GetFiles())
                {
                    file.Delete();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error clearing files from the cache.");
                Console.WriteLine(ex.Message.ToString());
                System.Environment.Exit(7);
            }
        }

        /// <summary>
        /// Check configuration file, keys and output directory are available
        /// </summary>
        /// <returns></returns>
        private static bool CheckFiles(string path)
        {
            bool checksPassed = true;
 
            // check if configuration.xml file exist
            if (!System.IO.File.Exists(path + "\\Configuration.xml"))
            {
                Console.WriteLine("Could not find the Configuration.xml file, please check if the file is in the directory.");
                return false;
            }

            _conf = XmlDeserialiser<Configuration>.Deserialise(path + "\\Configuration.xml");

            // check output directory exist
            if (!System.IO.Directory.Exists(path + "\\" + _conf.Output + "\\"))
            {
                // create directory if not exist
                System.IO.Directory.CreateDirectory(path + "\\" + _conf.Output + "\\");
            }

            // check OrgHashCode is set in the configuration file
            if (_conf.OrganisationHashCode == string.Empty)
            {
                Console.WriteLine("No Organisation Hash Code has been set in the Configuration.xml");
                checksPassed = false;
            }

            // check private and public keys are set in configuration file
            if (_conf.PrivateKey == String.Empty)
            {
                Console.WriteLine("No Private has been set in the Configuration.xml");
                checksPassed = false;
            }

            if (_conf.PublicKey == String.Empty)
            {
                Console.WriteLine("No Public key has been set in the Configuration.xml");
                checksPassed = false;
            }

            // check private and public key file exist
            if (_conf.PrivateKey != string.Empty)
            {
                if (!System.IO.File.Exists(path + "\\" + _conf.PrivateKey))
                {
                    Console.WriteLine("No Private key file was found");
                    checksPassed = false;
                }
            }

            if (_conf.PublicKey != string.Empty)
            {
                if (!System.IO.File.Exists(path + "\\" + _conf.PublicKey))
                {
                    Console.WriteLine("No Public key file was found");
                    checksPassed = false;
                }
            }

            return checksPassed;
        }


        private static void GetArguments(Arguments arguments)
        {
            // Filename - xml file be encrypted and sent
            bool? argCheck = ArgValueCheck(arguments[ArgumentList.filename]);
            xmlFilePath = ArgCheckReturnFileName(argCheck, arguments[ArgumentList.filename], ArgumentList.filename);

            argCheck = ArgValueCheck(arguments[ArgumentList.f]);
            xmlFilePath = ArgCheckReturnFileName(argCheck, arguments[ArgumentList.f], ArgumentList.f);

            // Send data to test server
            argCheck = ArgValueCheck(arguments[ArgumentList.testsend]);
            testSend = ArgCheckGeneric(argCheck, arguments[ArgumentList.testsend]);

            argCheck = ArgValueCheck(arguments[ArgumentList.t]);
            testSend = ArgCheckGeneric(argCheck, arguments[ArgumentList.t]);

            // Test connection
            argCheck = ArgValueCheck(arguments[ArgumentList.testconn]);
            testConn = ArgCheckGeneric(argCheck, arguments[ArgumentList.testconn]);

            argCheck = ArgValueCheck(arguments[ArgumentList.c]);
            testConn = ArgCheckGeneric(argCheck, arguments[ArgumentList.c]);

            // encrypt and zip data, no send
            argCheck = ArgValueCheck(arguments[ArgumentList.encrypt]);
            xmlFilePath_NonSending = ArgCheckReturnFileName(argCheck, arguments[ArgumentList.encrypt], ArgumentList.encrypt);

            argCheck = ArgValueCheck(arguments[ArgumentList.e]);
            xmlFilePath_NonSending = ArgCheckReturnFileName(argCheck, arguments[ArgumentList.e], ArgumentList.e);

            // help
            argCheck = ArgValueCheck(arguments[ArgumentList.h]);
            if (argCheck != null)
            {
                if (argCheck == true)
                    help = true;
                else
                    Console.WriteLine("Unknown parameter, type -h or -help for a list of accepted parameters.");
            }

            argCheck = ArgValueCheck(arguments[ArgumentList.help]);
            if (argCheck != null)
            {
                if (argCheck == true)
                    help = true;
                else
                    Console.WriteLine("Unknown parameter, type -h or -help for a list of accepted parameters.");
            }
        }

        private static void DisplayHelpOptions()
        {
            Console.WriteLine("");
            Console.WriteLine("===============================");
            Console.WriteLine("List of acceptable parameters: ");
            Console.WriteLine("===============================");
            Console.WriteLine("");
            Console.WriteLine("-f <filename> or -filename <filename>");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("HVP XML file send, the xml file with variant data that will be sent to HVP.");
            Console.WriteLine("e.g: VariantExporterCmdLn.exe -f xmldatasubmission.xml");
            Console.WriteLine("");
            Console.WriteLine("-t or -testsend");
            Console.WriteLine("------------------");
            Console.WriteLine("Sends XML file data to HVP test server. To be used with the file send");
            Console.WriteLine("e.g: VariantExporterCmdLn.exe -t -f xmldatasubmission.xml");
            Console.WriteLine("");
            Console.WriteLine("-e or -encrypt");
            Console.WriteLine("------------------");
            Console.WriteLine("For debugging and testing purpoese, this will only invoke the encryption and compressing data process, encrypted and zipped up data will remain in the temp directory");
            Console.WriteLine("e.g: VariantExporterCmdLn.exe -e xmldatasubmission.xml");
            Console.WriteLine("");
            Console.WriteLine("-c or -testconn");
            Console.WriteLine("------------------");
            Console.WriteLine("Testing connecting with HVP, checks keys whether they can be used to encrypt and decrypt files to HVP server.");
            Console.WriteLine("");
            Console.WriteLine("-h or -help");
            Console.WriteLine("--------------");
            Console.WriteLine("View this help menu.");
        }

        private static bool ArgCheckGeneric(bool? argCheck, string arg)
        {
            if (argCheck != null)
            {
                if (argCheck == true)
                    return true; //value = true;
                else
                {
                    Console.WriteLine("Was not expecting a value after -" + arg);
                    System.Environment.ExitCode = 4;
                    return false;
                }
            }
            else
                return false;
        }

        private static string ArgCheckReturnFileName(bool? argCheck, string argValue, string arg)
        {
            if (argCheck != null)
            {
                if (argCheck == true)
                {
                    Console.WriteLine("Missing filename after -" + arg);
                    System.Environment.ExitCode = 4;
                    return "";
                }
                else
                    return argValue;//xmlFilePath = argValue;
            }
            return "";
        }

        private static bool? ArgValueCheck(string argument)
        {
            if (!string.IsNullOrEmpty(argument))
            {
                if (argument == "true")
                    return true;
                else
                    return false;
            }
            else
                return null;
        }
    }
}
