using System;
using System.Collections.Generic;
using System.Text;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using DBConnLib;
using AuditLogDB;

namespace VariantExporterWinGUI.Util
{
    public static class DataLoader
    {
        
        /// <summary>
        /// Gets a Gene hibernate object based on specified ID of that gene.
        /// </summary>
        /// <param name="iSession"></param>
        /// <param name="geneID"></param>
        /// <returns></returns>
        public static Gene GetGene(ISession iSession, int geneID)
        {
            IList<Gene> resultList = iSession.CreateCriteria(typeof(Gene))
                .Add(Expression.Eq("ID", geneID))
                .List<Gene>();

            if (resultList.Count != 1)
                throw new Exception("Can not find an instance of this gene, GeneID: " + geneID.ToString());

            return resultList[0];
        }

        /// <summary>
        /// Get a IList of Gene.
        /// </summary>
        /// <param name="iSession"></param>
        /// <returns></returns>
        public static IList<Gene> GetGeneList(ISession iSession, Upload upload)
        {
            IList<Gene> resultList = iSession.CreateCriteria(typeof(Gene))
                .Add(Restrictions.Eq("Upload", upload))
                .List<Gene>();

            return resultList;
        }

        /// <summary>
        /// Get all the List of Uploads.
        /// </summary>
        /// <param name="iSession"></param>
        /// <returns></returns>
        public static IList<Upload> GetUploadList(ISession iSession)
        {
            IList<Upload> resultList = iSession.CreateCriteria(typeof(Upload))
                .List<Upload>();

            return resultList;
        }

        /// <summary>
        /// Only gets the Uploads that has a SpreadSheet DataSourceType.
        /// </summary>
        /// <param name="iSession"></param>
        /// <returns></returns>
        public static IList<Upload> GetSpreadSheetUploadList(ISession iSession)
        {
            IList<Upload> resultList = iSession.CreateCriteria(typeof(Upload))
                .Add(Expression.Eq("DataSourceType", DataSourceType.Spreadsheet))
                .List<Upload>();

            return resultList;
        }

        public static Upload GetUpload(ISession iSession, int uploadID)
        {
            IList<Upload> resultList = iSession.CreateCriteria(typeof(Upload))
                .Add(Expression.Eq("ID", uploadID))
                .List<Upload>();

            if (resultList.Count != 1)
                throw new Exception("Can not find an instance of this Upload, UploadID: "
                    + uploadID.ToString());

            return resultList[0];
        }

        public static Upload GetUpload(ISession iSession, string dll)
        {
            IList<Upload> resultList = iSession.CreateCriteria(typeof(Upload))
                .Add(Expression.Eq("Plugin", dll))
                .List<Upload>();

            if (resultList.Count == 0)
                return null;

            if (resultList.Count > 1)
                throw new Exception("There are multiple instances of this upload, dll plugin : "
                    + dll);

            return resultList[0];
        }

        /// <summary>
        /// Tries to get an Instance based on the raw instanceID and gene id.
        /// Returns a null if instance not found.
        /// </summary>
        /// <param name="iSession"></param>
        /// <param name="encryptedHashCode"></param>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        public static Instance GetInstance(ISession iSession, int variantInstanceID, int uploadID)
        {
            Upload upload = GetUpload(iSession, uploadID);
            
            IList<Instance> resultList = iSession.CreateCriteria(typeof(Instance))
                .Add(Expression.Eq("VariantInstanceID", variantInstanceID))
                .Add(Expression.Eq("Upload", upload))
                .List<Instance>();

            // none found return then just return a null
            if (resultList.Count == 0)
                return null;
            if (resultList.Count > 1)
                throw new Exception("There seems to be more than 1 instance with the UploadID: " 
                    + uploadID.ToString() + " and the InstanceID of: " + variantInstanceID.ToString());

            return resultList[0];
        }

        /// <summary>
        /// Tries to get an Instance based on the encryptedhashcode and upload id.
        /// Returns a null if instance not found.
        /// </summary>
        /// <param name="iSession"></param>
        /// <param name="variantHashCode"></param>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        public static Instance GetInstance(ISession iSession, string variantHashCode, int uploadID)
        {
            Upload upload = GetUpload(iSession, uploadID);

            IList<Instance> resultList = iSession.CreateCriteria(typeof(Instance))
                .Add(Expression.Eq("EncryptedHashCode", variantHashCode))
                .Add(Expression.Eq("Upload", upload))
                .List<Instance>();

            // none found then just return a null
            if (resultList.Count != 1)
                throw new Exception("There seems to be more than 1 or could not find instance with the UploadID: "
                    + uploadID.ToString() + " and the Instance hashcode of: " + variantHashCode);

            return resultList[0];
        }

        /// <summary>
        /// Get a list of all instances
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
        /// Get the details of the instance based on the last date that it was sent.
        /// </summary>
        /// <param name="iSession"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static Details GetDetails(ISession iSession, Instance instance)
        {
            IList<Details> resultList = iSession.CreateCriteria(typeof(Details))
                .CreateAlias("HVPTransaction", "HVPTransaction")
                .Add(Expression.Eq("Instance", instance))
                .AddOrder(Order.Desc("HVPTransaction.Date"))
                .List<Details>();

            // return the first result as that would be the latest date recorded
            return resultList[0];
        }
    }
}
