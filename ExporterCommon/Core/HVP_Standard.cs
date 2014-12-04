using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using ExporterCommon.Core.StandardColumns;

namespace ExporterCommon.Core
{
    public class HVP_Standard
    {
        public static DataTable NewVariantInstanceTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn(VariantInstance.Status, typeof(string)));
            dt.Columns.Add(new DataColumn(Organisation.HashCode, typeof(string)));

            dt.Columns.Add(new DataColumn(Patient.HashCode, typeof(string)));

            // for use in grhanite only these fields will not be sent to HVP
            dt.Columns.Add(new DataColumn(Patient.Sex, typeof(string)));
            dt.Columns.Add(new DataColumn(Patient.Surname, typeof(string)));
            dt.Columns.Add(new DataColumn(Patient.FirstName, typeof(string)));
            dt.Columns.Add(new DataColumn(Patient.DoB, typeof(DateTime)));
            dt.Columns.Add(new DataColumn(Patient.Medicare, typeof(string)));
            dt.Columns.Add(new DataColumn(Patient.PostCode, typeof(string)));

            // gene
            dt.Columns.Add(new DataColumn(Gene.GeneName, typeof(string)));
            dt.Columns.Add(new DataColumn(Gene.RefSeqName, typeof(string)));
            dt.Columns.Add(new DataColumn(Gene.RefSeqVer, typeof(string)));

            // variant instance
            dt.Columns.Add(new DataColumn(VariantInstance.HashCode, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.VariantClass, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.cDNA, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.mRNA, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.Genomic, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.Protein, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.InstanceDate, typeof(DateTime)));
            dt.Columns.Add(new DataColumn(VariantInstance.Pathogenicity, typeof(string)));

            dt.Columns.Add(new DataColumn(VariantInstance.Location, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.GenomicRefSeq, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.GenomicRefSeqVer, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.PatientAge, typeof(int)));
            dt.Columns.Add(new DataColumn(VariantInstance.TestMethod, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.SampleTissue, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.SampleSource, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.Justification, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.PubMed, typeof(string)));
            dt.Columns.Add(new DataColumn(VariantInstance.RecordedInDatabase, typeof(bool)));
            dt.Columns.Add(new DataColumn(VariantInstance.SampleStored, typeof(bool)));
            dt.Columns.Add(new DataColumn(VariantInstance.PedigreeAvailable, typeof(bool)));
            dt.Columns.Add(new DataColumn(VariantInstance.VariantSegregatesWithDisease, typeof(bool)));
            dt.Columns.Add(new DataColumn(VariantInstance.HistologyStored, typeof(bool)));
            dt.Columns.Add(new DataColumn(VariantInstance.SIFTScore, typeof(string)));
            
            dt.Columns.Add(new DataColumn(VariantInstance.DateSubmitted, typeof(DateTime)));

            return dt;

        }
    }
}
