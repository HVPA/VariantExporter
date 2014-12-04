using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Net;

using Newtonsoft.Json.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using DBConnLib;
using AuditLogDB;
using SiteConf;
using ExporterCommon;
using ExporterCommon.Core.StandardColumns;
using RestSharp;

namespace ExporterCommon
{
    public static class DataSaver
    {
        private static IWebProxy _proxy;

        // sets the webproxy
        public static void SetProxy(IWebProxy proxy)
        {
            _proxy = proxy;
        }
        
        /// <summary>
        /// Returns the enum SiteConf.Model based on object type.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static SiteConf.Model GetModelFromObj(ObjectAbstract obj)
        {
            Type type = obj.GetType();

            if (type == typeof(SiteConf.DiseaseTag.Object))
                return Model.diseasetag;

            if (type == typeof(SiteConf.Gene.Object))
                return Model.gene;

            if (type == typeof(SiteConf.HVPTran.Object))
                return Model.hvptran;

            if (type == typeof(SiteConf.OrgSite.Object))
                return Model.orgsite;

            if (type == typeof(SiteConf.Upload.Object))
                return Model.upload;
            else
                throw new Exception("Can not find object within SiteConf.Model");
        }
        
        /// <summary>
        /// Serializes standard object to JObject
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static JObject JObjectSerializer(ObjectAbstract obj)
        {
            JObject jObj = new JObject();

            Type type = obj.GetType();
            IList<System.Reflection.PropertyInfo> props =
                new List<System.Reflection.PropertyInfo>(type.GetProperties());
            foreach (System.Reflection.PropertyInfo prop in props)
            {
                string propName = prop.Name;
                // ignore "ID" and "resource_uri" fields
                if (propName == "ID" || propName == "resource_uri")
                    continue;
                else
                {
                    object propValue = prop.GetValue(obj, null);
                    // do not append to json if value is null
                    if (propValue != null)
                        jObj.Add(propName, propValue.ToString());
                }
            }

            return jObj;
        }

        /// <summary>
        /// Saves a new Object via Rest POST
        /// </summary>
        /// <param name="obj"></param>
        public static void SaveNewRestObject(ObjectAbstract obj)
        {
            JObject jObj = JObjectSerializer(obj);

            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Proxy = _proxy;

            try
            {
                client.UploadString(RestAPI.URL + RestAPI.API + GetModelFromObj(obj) + "/" + RestAPI.api_key, 
                    "POST", jObj.ToString());
            }
            catch (WebException ex)
            {
                // failed to save data
            }
        }

        /// <summary>
        /// Updates an existing Object via Rest PUT
        /// </summary>
        /// <param name="obj"></param>
        public static void UpdateRestObject(ObjectAbstract obj)
        {
            JObject jObj = JObjectSerializer(obj);

            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Proxy = _proxy;

            try
            {
                client.UploadString(RestAPI.URL + RestAPI.API + GetModelFromObj(obj) +
                    "/" + obj.ID.ToString() + "/" + RestAPI.api_key, "PUT", jObj.ToString());
            }
            catch (WebException ex)
            {
                // failed to update data
            }
        }

        /// <summary>
        /// Delete an existing Object via Rest DELETE
        /// </summary>
        /// <param name="obj"></param>
        public static void DeleteRestObject(ObjectAbstract obj)
        {
            RestClient client = new RestClient(RestAPI.URL);
            client.Proxy = _proxy;

            // setup request params
            RestRequest request = DataLoader.RestRequest(Method.DELETE, GetModelFromObj(obj));
            request.AddParameter("ID", obj.ID.ToString());

            client.Execute(request);
        }

        /// <summary>
        /// Simple generic save for hibernate objects.
        /// Note that the ISession will close if saving fails.
        /// </summary>
        /// <param name="iSession"></param>
        /// <param name="hbObj"></param>
        public static void SaveHibernateObject(ISession iSession, object hbObj)
        {
            try
            {
                ITransaction iTrans = iSession.BeginTransaction();
                try
                {
                    iSession.Save(hbObj);
                    iTrans.Commit();
                }
                catch (Exception ex)
                {
                    iTrans.Rollback();
                    throw ex;
                }
            }
            catch
            {
                MessageBox.Show("Error could not save changes made. Check database connection or try again.", "Error Saving data");
            }
        }

        public static void DeleteHibernateObject(ISession iSession, object hbObj)
        {
            try
            {
                ITransaction iTrans = iSession.BeginTransaction();
                try
                {
                    iSession.Delete(hbObj);
                    iTrans.Commit();
                }
                catch (Exception ex)
                {
                    iTrans.Rollback();
                    throw ex;
                }
            }
            catch
            {
                MessageBox.Show("Error could not save changes made. Check database connection or try again.", "Error Saving data");
            }
        }

        /// <summary>
        /// Saves an Instance for the audit database, This will also go on to save into details 
        /// and the HVPTransaction table as well.
        /// </summary>
        /// <param name="iSession"></param>
        /// <param name="instanceID"></param>
        /// <param name="uploadID"></param>
        /// <param name="passKey"></param>
        public static void SaveInstanceAudit(ISession iSession, int uploadID, DataRow dr, SiteConf.HVPTran.Object trans)
        {
            // get upload
            //Upload upload = DataLoader.GetUpload(iSession, uploadID);

            Instance instance = null;
            // if this is an update we try and get the previous
            if (dr[VariantInstance.Status].ToString() == "Update" ||
                dr[VariantInstance.Status].ToString() == "Delete")
            {
                // get existing instance from audit db using the variant instance hashcode
                instance = DataLoader.GetInstance(iSession, dr[VariantInstance.HashCode].ToString(), uploadID);
            }
            else
            {
                // create new instance
                instance = new Instance();
                instance.VariantInstanceID = dr[0].ToString(); // it is assume the raw variant instance id is kept in the first column
                instance.EncryptedHashCode = dr[VariantInstance.HashCode].ToString();
                instance.GeneName = dr[ExporterCommon.Core.StandardColumns.Gene.GeneName].ToString();
                instance.RecordedDate = DateTime.Now;
                instance.UploadID = uploadID;
                //instance.Gene = gene;
            }

            // create new details
            Details details = new Details();
            details.CheckSum = HashEncoder.EncodeDataRow(dr);
            details.Status = dr[VariantInstance.Status].ToString();
            details.Instance = instance;
            details.TransactionID = trans.ID;

            ITransaction iTrans = iSession.BeginTransaction();
            try
            {
                iSession.Save(instance);
                iSession.Save(details);
                iTrans.Commit();
            }
            catch (Exception ex)
            {
                iTrans.Rollback();
                throw ex;
            }
        }
    }
}
