using System;
using System.Collections.Generic;
using System.Text;
using System.Deployment.Application;
using System.Reflection;

using ExporterCommon.Conf;

namespace ExporterCommon.Plugins
{
    public class PluginLoader
    {
        public static IExtractor LoadExtractor(string ddlPath)
        {
            string assemblyPath = "";

            if (ApplicationDeployment.IsNetworkDeployed)
                assemblyPath = ApplicationDeployment.CurrentDeployment.DataDirectory + "\\Plugins\\" + ddlPath;
            else
                assemblyPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Plugins\\" + ddlPath;
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
                throw new PluginNotValidException(assemblyPath);
            }
            IExtractor result = (IExtractor)Activator.CreateInstance(pluginType);

            return result;
        }
    }
}
