using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ExporterCommon.Conf
{   
    public class RefMappings
    {
        public List<RefData> RefDatas;

        public RefMappings()
        {
            RefDatas = new List<RefData>();
        }
    }

    public class RefData
    {
        [XmlAttribute]
        public string Name { get; set; }
        
        public List<RefDataField> RefDataFields;

        public RefData()
        {
            RefDataFields = new List<RefDataField>();
        }
    }

    public class RefDataField
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
