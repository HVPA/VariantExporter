using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteConf
{
    /// <summary>
    /// Meta data for json obj
    /// </summary>
    public class Meta
    {
        public int limit { get; set; }
        public object next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total_count { get; set; }
    }
}
