using System;
using System.Collections.Generic;
using System.Text;
using SiteConf;

namespace ExporterCommon.Conf
{
    public class UploadConf
    {
        public string UploadName { get; set; }
        public DataSourceType DataSourceType { get; set; }
        public string PluginDll { get; set; }
        public string RefMapper { get; set; }
    }
}
