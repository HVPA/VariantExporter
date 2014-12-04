using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VariantExporterCmdLn
{
    public class Configuration
    {
        public string Output { get; set; } // Temporary directory to output encrypted files and zipped files to.
        public string OrganisationHashCode { get; set; } // ID that is assigned to a lab
        public string PublicKey { get; set; } // destination server public key file for encrypting contents before sending
        public string PrivateKey { get; set; } // labs private key file for encrypting contents before sending
        public string ServerAddress { get; set; } // the server address to send variant data to.
        public string TestServerAddress { get; set; } // the test server address to do test sends.

        // Custom Proxy settings
        public string ProxyAddress { get; set; } // proxy server
        public string ProxyPort { get; set; } // port
        public string ProxyUser { get; set; } // user
        public string ProxyPassword { get; set; } // password
        public string ProxyUserDomain { get; set; } // user domain
    }
}
