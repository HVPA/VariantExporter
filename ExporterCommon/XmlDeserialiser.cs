using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using ExporterCommon.Conf;

namespace ExporterCommon
{
    public static class XmlDeserialiser<T>
    {
        public static T Deserialise(string filepath)
        {
            T result;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamReader reader = null;

            try
            {
                reader = new StreamReader(filepath);
                result = (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return result;
        }

        // This is not needed it yet, at least it won't be needed for the configs
        // as we never need to write to the config xml's. This function is only meant for
        // testing and reference should remove no longer needed.
        public static void Serialise(T input, string filepath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            TextWriter writer = null;

            try
            {
                writer = new StreamWriter(filepath);
                xmlSerializer.Serialize(writer, input);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}
