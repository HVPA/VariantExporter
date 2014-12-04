using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;

namespace ExporterCommon
{
    public static class Transmit
    {
        /// <summary>
        /// Sends file to server set from the conf file.
        /// </summary>
        /// <param name="zippedFile">Path of zipped file to be sent</param>
        /// <returns>Response from server</returns>
        public static string SendData(string zippedFile, string serverAddress, WebClient client, Log log)
        {
            //WebClient client = new WebClient();
            string sent = null;

            try
            {
                byte[] b = client.UploadFile(serverAddress, "POST", zippedFile);
                System.Text.Encoding enc = System.Text.Encoding.ASCII;
                sent = enc.GetString(b);
            }
            catch (WebException ex)
            {
                // if error is '407' proxy error then display message to user
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    if ((int)response.StatusCode == 407)
                    {
                        MessageBox.Show("Could not authenticate proxy server, please check your username and password.", "Error!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (log != null) log.write("Unable to connect due to proxy, check your proxy settings");
                        if (log != null) log.write(ex.ToString());

                        return "PROXY_AUTH_ERROR";
                    }
                }
                return "Could not connect";
            }
            finally
            {
                client.Dispose();
            }

            return sent;
        }
    }
}
