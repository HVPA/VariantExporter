using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SiteConf
{
    namespace Upload
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
        /// Upload class
        /// </summary>
        public class Object : ObjectAbstract
        {
            public string Name { get; set; }
            public DataSourceType DataSourceType { get; set; }
            public string DataSourceName { get; set; }
            public string DatabaseName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Plugin { get; set; }
            public string RefMapper { get; set; }
            public string orgsite { get; set; }

            public int? GetOrgSiteID()
            {
                // retrieves the ID from the string.
                // e.g: /api/v1/orgsite/12/
                string[] split_str = orgsite.Split('/');
                try
                {
                    for (int i = 0; i < split_str.Length; i++)
                    {
                        if (split_str[i] == "orgsite")
                        {
                            return int.Parse(split_str[i + 1]);
                        }
                    }

                    return null;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
