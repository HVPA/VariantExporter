using System;
using System.Collections.Generic;
using System.Text;

namespace ExporterCommon.Plugins.StandardColumns
{
    public class Organisation
    {
        public const string HashCode = "OrganisationHashCode";
    }
    public class Patient
    {
        public const string HashCode = "PatientHashCode";
        public const string Gender = "Gender";
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
    }
}
