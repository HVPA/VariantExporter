using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Deployment.Application;

using ExporterCommon;
using ExporterCommon.Conf;
using System.Windows.Forms;


namespace ExporterCommon.Plugins
{
    public class DefaultExtractorContext : IExtractorContext
    {
        Configuration conf;

        public DefaultExtractorContext()
        {
            //string path = ApplicationDeployment.CurrentDeployment.DataDirectory + "\\Configuration.xml";
            
            string path = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\debug"))
                path = Application.StartupPath + "\\Configuration.xml";
            else
                path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\VariantExporter\\Configuration.xml";

            /*
            if (ApplicationDeployment.IsNetworkDeployed)
                path = ApplicationDeployment.CurrentDeployment.DataDirectory + "\\Configuration.xml";
            else
                path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Configuration.xml";*/
            
            conf = XmlDeserialiser<Configuration>.Deserialise(path);
        }

        public Configuration GetConfiguration()
        {
            return conf;
        }

        public void SerializeObject()
        {
            //string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Configuration.xml";
            string path = "";

            if (System.IO.File.Exists(Application.StartupPath + "\\debug"))
                path = Application.StartupPath + "\\Configuration.xml";
            else
                path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\VariantExporter\\Configuration.xml";
            
            XmlDeserialiser<Configuration>.Serialise(conf, path);
        }
    }
}
