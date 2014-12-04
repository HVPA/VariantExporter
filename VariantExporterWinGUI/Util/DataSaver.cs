using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using DBConnLib;
using AuditLogDB;
using ExporterCommon;
using ExporterCommon.Core.StandardColumns;

namespace VariantExporterWinGUI.Util
{
    public static class DataSaver
    {
        /// <summary>
        /// Simple generic save for hibernate objects.
        /// Note that the ISession will close if saving fails.
        /// </summary>
        /// <param name="iSession"></param>
        /// <param name="hbObj"></param>
        public static void SaveHibernateObject(ISession iSession, object hbObj)
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
                iSession.Close();
                throw ex;
            }
        }

        public static void DeleteHibernateObject(ISession iSession, object hbObj)
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
                iSession.Close();
                throw ex;
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
        public static void SaveInstanceAudit(ISession iSession, int uploadID, DataRow dr, HVPTransaction trans)
        {
            // get gene
            //AuditLogDB.Gene gene = DataLoader.GetGene(iSession, uploadID);
            // get upload
            Upload upload = DataLoader.GetUpload(iSession, uploadID);

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
                instance.VariantInstanceID = int.Parse(dr[0].ToString()); // it is assume the raw variant instance id is kept in the first column
                instance.EncryptedHashCode = dr[VariantInstance.HashCode].ToString();
                instance.GeneName = dr[ExporterCommon.Core.StandardColumns.Gene.GeneName].ToString();
                instance.Upload = upload;
                //instance.Gene = gene;
            }

            // create new details
            Details details = new Details();
            details.CheckSum = HashEncoder.EncodeDataRow(dr);
            details.Status = dr[VariantInstance.Status].ToString();
            details.Instance = instance;
            details.HVPTransaction = trans;

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
                iSession.Close();
                throw ex;
            }
        }
    }
}
