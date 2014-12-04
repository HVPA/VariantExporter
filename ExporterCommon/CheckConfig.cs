using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ExporterCommon.Conf;

namespace ExporterCommon
{
    /// <summary>
    /// Sanity check, validate the config file to ensure everything is all ok
    /// </summary>
    public static class CheckConfig
    {
        /// <summary>
        /// Calls all sanity checks in the class. The one check to rule them all!
        /// </summary>
        /// <returns></returns>
        public static bool CheckAllConfig(Configuration conf)
        {
            if (
                CheckOrganisationHash(conf.OrganisationHashCode) &&
                CheckValidPassKey(conf.PassKey)
            )
                return true;
            else
                return false;
        }
        
        /// <summary>
        /// Checks that Organisation HashCode is not null and not empty space string.
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public static bool CheckOrganisationHash(string orgHash)
        {
            bool valid = false;

            if (orgHash != null)
            {
                if (orgHash.Trim() != "")
                    valid = true;
            }

            return valid;
        }

        /// <summary>
        /// Checks that Pass key is not null and is at least 12 characters long.
        /// </summary>
        /// <param name="passKey"></param>
        /// <returns></returns>
        public static bool CheckValidPassKey(string passKey)
        {
            bool valid = true;

            if (passKey != null)
            {
                if (passKey.Trim().Length < 12)
                    valid = false;
            }
            else
                valid = false;

            return valid;
        }
    }
}
