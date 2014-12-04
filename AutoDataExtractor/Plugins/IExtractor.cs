using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AutoDataExtractor.Plugins
{
    public interface IExtractor
    {
        DataTable GetData(Configuration conf, string dataSourcePath);

        Boolean Validate(DataRow dr, string refMapperPath);
    }
}
