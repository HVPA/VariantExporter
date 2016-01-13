using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExporterCommon
{
    public static class RestAPI
    {
        // REST url

        /// <summary>
        ///  The URL to the node.
        ///  We recommend using the domain name of the server for production
        /// </summary>
        //public const string URL = @"http://the.website.com/";
        public const string URL = @"http://144.6.232.180/";

        /// <summary>
        /// The username to access SiteConf setttings.
        /// Please set according to instructions for setting up SiteConf
        /// </summary>
        public const string API_USER = "siteconf";
        /// <summary>
        /// API Hash key for programmability.
        /// Please set according to instructions for setting up SiteConf
        /// </summary>
        public const string API_HASH = "3020577c5bd111b889bb5cdd0cda80aee376fd2c";


        public const string API = @"siteconf/api/v1/";
        public static string api_key
        {
            get
            {
                return "?username=" + API_USER + "&api_key=" + API_HASH;
            }
        }
    }
}
