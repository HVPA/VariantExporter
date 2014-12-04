using System;
using System.Collections.Generic;
using System.Text;

namespace SiteConf
{
    public enum DataSourceType
    {
        Database,
        Spreadsheet,
        Server
    }

    public class DataSourceTypeString : NHibernate.Type.EnumStringType
    {
        public DataSourceTypeString()
            : base(typeof(DataSourceType), 64)
        { }
    }

    public enum Model
    {
        diseasetag,
        gene,
        hvptran,
        orgsite,
        upload
    }
}
