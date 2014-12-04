using System;
using System.Collections.Generic;
using System.Text;

namespace SiteConf
{
    namespace HVPTran
    {
        /// <summary>
        /// Root of json obj
        /// </summary>
        public class RootObject
        {
            public Meta meta { get; set; }
            public List<Object> objects { get; set; }
        }

        /// <summary>
        /// HVPTrans class
        /// </summary>
        public class Object : ObjectAbstract
        {
            public DateTime Date { get; set; }
            public string Who { get; set; }
            public string Log { get; set; }
            public string Location { get; set; }
            public string Byte { get; set; }
            public string orgsite { get; set; }
        }
    }
}
