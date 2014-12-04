using System;
using System.Collections.Generic;
using System.Text;

namespace ExporterCommon.Plugins
{
    public class PluginNotValidException : Exception
    {
        public PluginNotValidException(string assemblyPath) :
            base("Plugin in path '" + assemblyPath + "' not a valid extractor plugin")
        {

        }
    }
}
