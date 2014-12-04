using System;
using System.Collections.Generic;
using System.Text;

namespace SiteConf
{
    namespace Gene
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
            public string GeneName { get; set; }
            public string RefSeqName { get; set; }
            public string RefSeqVersion { get; set; }
            public string upload { get; set; }
        }
    }
}
