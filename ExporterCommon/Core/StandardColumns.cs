using System;
using System.Collections.Generic;
using System.Text;

namespace ExporterCommon.Core.StandardColumns
{
    public class Organisation
    {
        public const string HashCode = "OrganisationHashCode";
    }
    public class Patient
    {
        public const string HashCode = "PatientHashCode";
        // for using grhanite
        public const string Sex = "Sex";
        public const string Surname = "Surname";
        public const string FirstName = "FirstName";
        public const string DoB = "DoB";
        public const string Medicare = "Medicare";
        public const string PostCode = "PostCode";
        
    }
    public class GRHANITE
    {
        public const string HashType = "HashType";
        public const string Hash = "Hash";
        public const string AgrWeight = "AgrWeight";
        public const string GUID = "GUID";
    }
    public class Gene
    {
        public const string GeneName = "GeneName";
        public const string RefSeqName = "RefSeqName";
        public const string RefSeqVer = "RefSeqVer";
    }
    public class VariantInstance
    {
        public const string HashCode = "VariantHashCode";

        // status of the instance: New, Updated or Deleted
        public const string Status = "Status";

        // Variant name
        public const string VariantClass = "VariantClass";
        public const string cDNA = "cDNA";
        public const string mRNA = "mRNA";
        public const string Genomic = "Genomic";
        public const string Protein = "Protein";

        // Assessment
        public const string InstanceDate = "InstanceDate";
        public const string Pathogenicity = "Pathogenicity";

        // Optionals
        public const string Location = "Location";
        public const string GenomicRefSeq = "GenomicRefSeq";
        public const string GenomicRefSeqVer = "GenomicRefSeqVer";
        public const string PatientAge = "PatientAge";
        public const string TestMethod = "TestMethod";
        public const string SampleTissue = "SampleTissue";
        public const string SampleSource = "SampleSource";
        public const string Justification = "Justification";
        public const string PubMed = "PubMed";
        public const string RecordedInDatabase = "RecordedInDatabase";
        public const string SampleStored = "SampleStored";
        public const string PedigreeAvailable = "PedigreeAvailable";
        public const string VariantSegregatesWithDisease = "VariantSegregatesWithDisease";
        public const string HistologyStored = "HistologyStored";
        public const string SIFTScore = "SIFTScore";

        public const string DateSubmitted = "DateSubmitted";
    }
}
