using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoDataExtractor.Plugins
{
    public class PluginLoader
    {
        public static IExtractor LoadExtractor(string ddlPath)
        {
            string assemblyPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + ddlPath;
            Assembly assembly = Assembly.LoadFile(assemblyPath);

            Type[] types = assembly.GetTypes();
            Type pluginType = null;
            foreach (Type t in assembly.GetTypes())
            {
                if (typeof(IExtractor).IsAssignableFrom(t))
                {
                    pluginType = t;
                    break;
                }
            }

            if (pluginType == null)
            {
                throw new Exception("Plugin in path '" + assemblyPath + "' not a valid extractor plugin");
            }
            IExtractor result = (IExtractor)Activator.CreateInstance(pluginType);

            return result;
        }
    }
}
