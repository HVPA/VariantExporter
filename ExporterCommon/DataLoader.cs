using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using DBConnLib;
using AuditLogDB;
using SiteConf;
using RestSharp;

namespace ExporterCommon
{
    public static class DataLoader
    {
        private static IWebProxy _proxy;

        // sets the webproxy
        public static void SetProxy(IWebProxy proxy)
        {
            _proxy = proxy;
        }
        
        /// <summary>
        /// Sets up a basic Rest request with header, url, api and api_key params.
        /// NB: is set to "public" for the DeleteRestObject function in DataSaver.
        /// </summary>
        /// <param name="restMeth"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static RestRequest RestRequest(Method restMeth, SiteConf.Model model)
        {
            RestRequest request = new RestRequest(RestAPI.API + "{model}/", restMeth);
            request.AddParameter("model", model, ParameterType.UrlSegment);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("username", "siteconf");
            request.AddParameter("api_key", "3020577c5bd111b889bb5cdd0cda80aee376fd2c");

            return request;
        }

        /// <summary>
        /// Returns a Gene obj from specified gene ID, data is retrieved via Rest.
        /// </summary>
        /// <param name="geneID"></param>
        /// <returns></returns>
        public static SiteConf.Gene.Object GetGene(int geneID)
        {
            RestClient client = new RestClient(RestAPI.URL);
            client.AddHandler("application/json", new RestSharp.Deserializers.JsonDeserializer());
            client.Proxy = _proxy;

            // setup request params
            RestRequest request = RestRequest(Method.GET, SiteConf.Model.gene);
            request.AddParameter("ID", geneID.ToString());
            
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Could not get resource from REST service.");

            // Deserialize json object
            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            List<SiteConf.Gene.RootObject> resultList = deserial.Deserialize<List<SiteConf.Gene.RootObject>>(response);

            if (resultList.Count != 1)
                throw new Exception("Error!!!");

            return resultList[0].objects[0];
        }

        /// <summary>
        /// Returns a Gene obj from specified upload ID and RefSeqName, data is retrieved via Rest.
        /// </summary>
        /// <param name="uploadID"></param>
        /// <param name="RefSeq"></param>
        /// <returns></returns>
        public static SiteConf.Gene.Object GetGeneRefSeqUploadID(int uploadID, string RefSeq)
        {
            RestClient client = new RestClient(RestAPI.URL);
            client.AddHandler("application/json", new RestSharp.Deserializers.JsonDeserializer());
            client.Proxy = _proxy;

            // setup request params
            RestRequest request = RestRequest(Method.GET, SiteConf.Model.gene);
            request.AddParameter("upload", uploadID.ToString());
            request.AddParameter("RefSeqName", RefSeq);
            
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Could not get resource from REST service.");

            // Deserialize json object
            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            List<SiteConf.Gene.RootObject> resultList = deserial.Deserialize<List<SiteConf.Gene.RootObject>>(response);

            if (resultList.Count != 1)
                throw new Exception("Error!!!");

            if (resultList[0].objects.Count < 1)
                return null;

            return resultList[0].objects[0];
        }

        /// <summary>
        /// Returns a List of Gene obj from specified upload ID, data is retrieved via Rest.
        /// </summary>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        public static List<SiteConf.Gene.Object> GetGeneList(int uploadID)
        {
            RestClient client = new RestClient(RestAPI.URL);
            client.AddHandler("application/json", new RestSharp.Deserializers.JsonDeserializer());
            client.Proxy = _proxy;

            // setup request params
            RestRequest request = RestRequest(Method.GET, SiteConf.Model.gene);
            request.AddParameter("upload", uploadID.ToString());
            
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Could not get resource from REST service.");

            // Deserialize json object
            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            List<SiteConf.Gene.RootObject> resultList = deserial.Deserialize<List<SiteConf.Gene.RootObject>>(response);

            if (resultList.Count != 1)
                throw new Exception("Error!!!");

            return resultList[0].objects;
        }

        /// <summary>
        /// Returns a OrgSite obj from specified OrgHashCode, data is retrieved via Rest.
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="ParamValue"></param>
        /// <returns></returns>
        public static SiteConf.OrgSite.Object GetOrgSite(string paramName, string paramValue)
        {
            RestClient client = new RestClient(RestAPI.URL);
            client.AddHandler("application/json", new RestSharp.Deserializers.JsonDeserializer());
            client.Proxy = _proxy;

            // setup request params
            RestRequest request = RestRequest(Method.GET, SiteConf.Model.orgsite);
            //request.AddParameter("OrgHashCode", OrgHashCode);
            request.AddParameter(paramName, paramValue);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Could not get resource from REST service.");
                
            // Deserialize json object
            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            List<SiteConf.OrgSite.RootObject> resultList = deserial.Deserialize<List<SiteConf.OrgSite.RootObject>>(response);
            
            if (resultList.Count != 1)
                throw new Exception("Error!!!");

            if (resultList[0].objects.Count < 1)
                return null;
            
            return resultList[0].objects[0];
        }

        /// <summary>
        /// Returns a list of OrgSite from specified AdminOrgSiteID, data is retrieved via REST.
        /// </summary>
        /// <param name="AdminSiteID"></param>
        /// <returns></returns>
        public static List<SiteConf.OrgSite.Object> GetAdminOrgSites(int AdminSiteID)
        {
            RestClient client = new RestClient(RestAPI.URL);
            client.AddHandler("application/json", new RestSharp.Deserializers.JsonDeserializer());
            client.Proxy = _proxy;

            // setup request params
            RestRequest request = RestRequest(Method.GET, SiteConf.Model.orgsite);
            request.AddParameter("AdminOrgSiteID", AdminSiteID.ToString());

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Could not get resource from REST service.");

            // Deserialize json object
            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            List<SiteConf.OrgSite.RootObject> resultList = deserial.Deserialize<List<SiteConf.OrgSite.RootObject>>(response);

            if (resultList.Count != 1)
                throw new Exception("Error");

            return resultList[0].objects;
        }

        /// <summary>
        /// Returns a List of Upload obj from specified OrgHashCode, data is retrieved via Rest.
        /// </summary>
        /// <param name="OrgHashCode"></param>
        /// <returns></returns>
        public static List<SiteConf.Upload.Object> GetUploadList(string OrgHashCode)
        {
            RestClient client = new RestClient(RestAPI.URL);
            client.AddHandler("application/json", new RestSharp.Deserializers.JsonDeserializer());
            client.Proxy = _proxy;

            // setup request params
            RestRequest request = RestRequest(Method.GET, SiteConf.Model.upload);
            request.AddParameter("orgsite__OrgHashCode", OrgHashCode);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Could not get resource from REST service.");

            // Deserialize json object
            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            List<SiteConf.Upload.RootObject> resultList = deserial.Deserialize<List<SiteConf.Upload.RootObject>>(response);

            if (resultList.Count != 1)
                throw new Exception("Error!!!");

            return resultList[0].objects;
        }

        /// <summary>
        /// Returns a list of Upload obj where DataSourceType is Spreadsheet, data is retrieved via Rest.
        /// </summary>
        /// <param name="OrgHashCode"></param>
        /// <returns></returns>
        public static List<SiteConf.Upload.Object> GetSpreadSheetUploadList(string OrgHashCode)
        {
            RestClient client = new RestClient(RestAPI.URL);
            client.AddHandler("application/json", new RestSharp.Deserializers.JsonDeserializer());
            client.Proxy = _proxy;

            // setup request params
            RestRequest request = RestRequest(Method.GET, SiteConf.Model.upload);
            request.AddParameter("DataSourceType", SiteConf.DataSourceType.Spreadsheet);
            request.AddParameter("orgsite__OrgHashCode", OrgHashCode);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Could not get resource from REST service.");

            // Deserialize json object
            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            List<SiteConf.Upload.RootObject> resultList = deserial.Deserialize<List<SiteConf.Upload.RootObject>>(response);

            if (resultList.Count != 1)
                throw new Exception("Error!!!");

            return resultList[0].objects;
        }

        /// <summary>
        /// Returns a Upload obj from specified upload ID, data is retrieved via Rest.
        /// </summary>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        public static SiteConf.Upload.Object GetUpload(int uploadID)
        {
            RestClient client = new RestClient(RestAPI.URL);
            client.AddHandler("application/json", new RestSharp.Deserializers.JsonDeserializer());
            client.Proxy = _proxy;

            // setup request params
            RestRequest request = RestRequest(Method.GET, SiteConf.Model.upload);
            request.AddParameter("ID", uploadID);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Could not get resource from REST service.");

            // Deserialize json object
            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            List<SiteConf.Upload.RootObject> resultList = deserial.Deserialize<List<SiteConf.Upload.RootObject>>(response);

            if (resultList.Count != 1)
                throw new Exception("Error!!!");

            if (resultList.Count < 1)
                return null;

            return resultList[0].objects[0];
        }

        /// <summary>
        /// Tries to get an Instance based on the encryptedhashcode and upload id.
        /// Returns a null if instance not found, data is retrieved from sqlite DB via nhiberante
        /// </summary>
        /// <param name="iSession"></param>
        /// <param name="variantHashCode"></param>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        public static Instance GetInstance(ISession iSession, string variantHashCode, int uploadID)
        {
             IList<Instance> resultList = iSession.CreateCriteria(typeof(Instance))
                .Add(Expression.Eq("EncryptedHashCode", variantHashCode))
                .Add(Expression.Eq("UploadID", uploadID))
                .List<Instance>();

            // none found then just return a null
            if (resultList.Count == 0)
                return null;

            // if more than one through error
            if (resultList.Count > 1)
                throw new Exception("There seems to be more than 1 or could not find instance with the UploadID: "
                    + uploadID.ToString() + " and the Instance hashcode of: " + variantHashCode);

            return resultList[0];
        }

        /// <summary>
        /// Get a list of all instances, data is retrieved from sqlite DB via nhiberante
        /// </summary>
        /// <param name="iSession"></param>
        /// <returns></returns>
        public static IList<Instance> GetInstanceList(ISession iSession)
        {
            IList<Instance> resultList = iSession.CreateCriteria(typeof(Instance))
                .List<Instance>();

            return resultList;
        }

        /// <summary>
        /// Get a list of all instance for a specific Upload, data is retrieved from sqlite DB via nhiberante
        /// </summary>
        /// <param name="iSession"></param>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        public static IList<Instance> GetInstanceList(ISession iSession, int uploadID)
        {
            IList<Instance> resultList = iSession.CreateCriteria(typeof(Instance))
                .Add(Expression.Eq("UploadID", uploadID))
                .List<Instance>();

            return resultList;
        }

        /// <summary>
        /// Get the details of the instance based on the last date that it was sent, data is retrieved from sqlite DB via nhiberante
        /// </summary>
        /// <param name="iSession"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static Details GetDetails(ISession iSession, Instance instance)
        {
            IList<Details> resultList = iSession.CreateCriteria(typeof(Details))
                .Add(Expression.Eq("Instance", instance))
                .AddOrder(Order.Desc("ID"))
                .List<Details>();

            // return the first result as that would be the latest date recorded
            return resultList[0];
        }

        /// <summary>
        /// Returns a DiseaseTag obj from specified diseasetag ID, data is retrieved via Rest
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static SiteConf.DiseaseTag.Object GetDiseaseTag(int ID)
        {
            RestClient client = new RestClient(RestAPI.URL);
            client.AddHandler("application/json", new RestSharp.Deserializers.JsonDeserializer());
            client.Proxy = _proxy;

            // setup request params
            RestRequest request = RestRequest(Method.GET, SiteConf.Model.diseasetag);
            request.AddParameter("ID", ID.ToString());

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Could not get resource from REST service.");

            // Deserialize json object
            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            List<SiteConf.DiseaseTag.RootObject> resultList = deserial.Deserialize<List<SiteConf.DiseaseTag.RootObject>>(response);

            if (resultList.Count != 1)
                throw new Exception("Error!!!");

            if (resultList.Count < 1)
                return null;

            return resultList[0].objects[0];
        }

        /// <summary>
        /// Returns a list of DiseaseTag obj from specified gene ID, data is retrieved via Rest
        /// </summary>
        /// <param name="geneID"></param>
        /// <returns></returns>
        public static List<SiteConf.DiseaseTag.Object> GetDiseaseTagList(int geneID)
        {
            RestClient client = new RestClient(RestAPI.URL);
            client.AddHandler("application/json", new RestSharp.Deserializers.JsonDeserializer());
            client.Proxy = _proxy;

            // setup request params
            RestRequest request = RestRequest(Method.GET, SiteConf.Model.diseasetag);
            request.AddParameter("gene__ID", geneID.ToString());

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Could not get resource from REST service.");

            // Deserialize json object
            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            List<SiteConf.DiseaseTag.RootObject> resultList = deserial.Deserialize<List<SiteConf.DiseaseTag.RootObject>>(response);

            if (resultList.Count != 1)
                throw new Exception("Error!!!");

            return resultList[0].objects;
        }
    }
}
