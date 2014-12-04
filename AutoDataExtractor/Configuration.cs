using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoDataExtractor
{
    public class Configuration
    {
        public string OrgHashCode { get; set; }
        public string PassKey { get; set; }
        public string Plugin { get; set;} // dll plugin to extract data
        public string RefMapper { get; set; } // RefmMapper xml to remap fields
        public string DataSourceDirectory { get; set; } // Location to where folder containing datasource files are
        public string XmlOutputDirectory { get; set; } // Location of xml output
        public string LogDirectory { get; set; } // Location of where log files are written to
    }
}
