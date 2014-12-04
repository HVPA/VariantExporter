using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteConf
{
    namespace DiseaseTag
    {
        /// <summary>
        /// Root of json obj
        /// </summary>
        public class RootObject
        {
            public Meta meta { get; set; }
            public List<Object> objects { get; set; }
        }

        public class Object : ObjectAbstract
        {
            public string Tag { get; set; }
            public string gene { get; set; }
        }
    }
}
