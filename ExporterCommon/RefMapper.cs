using System;
using System.Collections.Generic;
using System.Text;
using ExporterCommon.Conf;

namespace ExporterCommon
{
    /// <summary>
    /// Used to remap refdata fields collected from data source and remap those fields
    /// back into HVP database.
    /// </summary>
    public class RefMapper
    {
        private string _refMapXmlPath;

        public RefMapper(string refMapXmlPath)
        {
            _refMapXmlPath = refMapXmlPath;
        }
        
        /// <summary>
        /// Input parent element node name and the field value you want remapped 
        /// and it will return you what the remap value should be.
        /// </summary>
        /// <param name="fieldname"></param>
        /// <param name="fieldvalue"></param>
        /// <returns></returns>
        public string FieldReMapper(string fieldname, string fieldvalue)
        {
            // deserialise and get list remapped fields from xml confs.
            RefMappings refMap = XmlDeserialiser<RefMappings>.Deserialise(_refMapXmlPath);

            // iterate through all the different refdata fields for the one specified
            foreach (RefData rd in refMap.RefDatas)
            {
                if (rd.Name != null)
                {
                    if (rd.Name.Trim().ToLower() == fieldname.Trim().ToLower())
                    {
                        // if we found the field name then value should be remapped to something
                        // iterate through the field values to find the matching one
                        foreach (RefDataField rdf in rd.RefDataFields)
                        {
                            if (rdf.Value != null)
                            {
                                if (rdf.Value.Trim().ToLower() == fieldvalue.Trim().ToLower())
                                {
                                    return rdf.Name;
                                }
                            }
                            else
                            {
                                // if field is null and refdata value is empty string
                                // they are considered the same
                                if (fieldvalue == string.Empty)
                                    return rdf.Name;
                            }
                        }
                        break;
                    }
                }
            }

            // if nothing is found we return the orginal fieldvalue back
            return fieldvalue;
        }

        public bool ValidateFields(string fieldname, string fieldvalue)
        {
            bool found = false;

            // deserialise and get list remapped fields from xml confs.
            RefMappings refMap = XmlDeserialiser<RefMappings>.Deserialise(_refMapXmlPath);

            // iterate through all the different refdata fields for the one specified
            foreach (RefData rd in refMap.RefDatas)
            {
                if (rd.Name.Trim().ToLower() == fieldname.Trim().ToLower())
                {
                    // if we found the field name then value should be remapped to something
                    // iterate through the field values to find the matching one
                    foreach (RefDataField rdf in rd.RefDataFields)
                    {
                        if (rdf.Value != null)
                        {
                            if (rdf.Value.Trim().ToLower() == fieldvalue.Trim().ToLower())
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    break;
                }
            }

            return found;
        }
    }
}
