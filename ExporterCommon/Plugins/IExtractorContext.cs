using System;
using System.Collections.Generic;
using System.Text;

using ExporterCommon.Conf;

namespace ExporterCommon.Plugins
{
    public interface IExtractorContext
    {
        Configuration GetConfiguration();
    }
}
