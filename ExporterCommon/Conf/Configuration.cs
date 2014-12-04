using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using System.Collections;

namespace ExporterCommon.Conf
{
    public class Configuration
    {
        public string PortalWebSite { get; set; } // URL address of the Portal website
        public string OrganisationHashCode { get; set; } // ID that is assigned to a lab
        public string PassKey { get; set;  } // encryption pass key used to encrypt data identifiers
        public string PublicKey { get; set; } // destination server public key file for encrypting contents before sending
        public string PrivateKey { get; set; } // labs private key file for encrypting contents before sending
        public string ServerAddress { get; set; } // the server address to send variant data to.
        public string TestServerAddress { get; set; } // the test server address to do test sends.
        public string VerboseLogEnable { get; set; } // turn on/off debugging log
        public string ClearCache { get; set; } // Clears the "Temp" and "Raw" directory after data is sent.
        public string AutoUpdateLink { get; set; } // url link to the xml auto update 

        // Custom Proxy settings
        public string ProxyAddress { get; set; } // proxy server
        public string ProxyPort { get; set; } // port
        public string ProxyUser { get; set; } // user
        public string ProxyPassword { get; set; } // password
        public string ProxyUserDomain { get; set; } // user domain

        // Grhanite settings
        public bool EnableGRHANITE { get; set; } // enables grhanite for site
        public string LinkageKey { get; set; }
        public bool NameTranspositionAllowed { get; set; }
        public bool TightSexMatching { get; set; }
        public bool YOBFatherSonChecking { get; set; }
        public bool AdditionalYOBPCodeHash { get; set; }
        public bool ExcludeMedicare { get; set; }
        public bool SuppliesMedicareDigits5to9Only { get; set; }
        public bool ValidateMedicareChecksum { get; set; }
    }

    public enum DatabaseType
    {
        MsAccess,
        MsSQLServer,
        Excel
    }

}
