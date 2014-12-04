using System;
using System.Collections.Generic;
using System.Text;
using SiteConf;
using System.Data;

namespace ExporterCommon.Plugins
{
    public interface IExtractor
    {
        DataTable GetData(IExtractorContext context, SiteConf.Upload.Object upload);

        Boolean Validate(DataRow dr, string refMapperPath);

        // only needed for DB server to test the connectivity between client and server
        Boolean TestSqlConnection(SiteConf.Upload.Object upload);
    }
}
