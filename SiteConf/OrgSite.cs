using System;
using System.Collections.Generic;
using System.Text;

namespace SiteConf
{
    namespace OrgSite
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
        /// OrgSite class
        /// </summary>
        public class Object : ObjectAbstract
        {
            public string OrgHashCode { get; set; }
            public bool? HVPAdmin { get; set; }
            public string OrgSite { get; set; }
        }
    }
}
