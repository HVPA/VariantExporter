using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using NUnit.Framework;

using ExporterCommon.Core;
using ExporterCommon.Core.StandardColumns;
using ExporterCommon.Output;

namespace ExporterCommonTest
{
    [TestFixture]
    public class XmlOutputTest
    {
        private bool valid;

        private XmlDocument ReadXml(string file) {
            TextReader xml = new StreamReader(file);
            XmlReader xsd = XmlReader.Create(new StreamReader("HVP_Transaction.xsd"));

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, xsd);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += new ValidationEventHandler(validationEventHandler);

            XmlReader reader = XmlReader.Create(xml, settings);
            XmlDocument document = new XmlDocument();

            valid = true;
            try
            {
                document.Load(reader);
                document.Validate(new ValidationEventHandler(validationEventHandler));
            }
            finally
            {
                reader.Close();
                xml.Close();
                xsd.Close();
                
            }

            return valid ? document : null;
        }
        private void validationEventHandler(object sender, ValidationEventArgs e)
        {
            this.valid = false; // Problem encountered so this read is invalid
            Console.WriteLine(e.Exception.Message);
            throw e.Exception;
        }



        [Test]
        public void TestSampleXml()
        {
            // Tests the schema based on Sample.xml so that you have a reference for what works

            XmlDocument doc = ReadXml("Sample.xml");
            Assert.IsTrue(doc != null);
            Assert.IsTrue(doc.HasChildNodes);

            XmlNodeList nodes = doc.GetElementsByTagName("HVP_Transaction");
            Assert.IsTrue(nodes.Count == 1);

            XmlNode node = nodes[0];
            Console.WriteLine(node.Attributes["destination"].Value);
            Assert.AreEqual("https://portal.hvpaustralia.org.au", node.Attributes["destination"].Value);
            Assert.IsTrue(node.HasChildNodes);

            node = node.ChildNodes[0];
            Console.WriteLine(node.ChildNodes[0].Name);
            Console.WriteLine(node.ChildNodes[0].InnerText);
            Assert.AreEqual("OrganisationHashCode", node.ChildNodes[0].Name);
            Assert.AreEqual("XYZ123", node.ChildNodes[0].InnerText);
        }


        [Test]
        public void TestGenerateXmlAllFields()
        {
            DataTable dt = HVP_Standard.NewVariantInstanceTable();

            // === Make dummy data ===
            DataRow row = dt.NewRow();
            row[Organisation.HashCode] = "XYZ123";

            row[Patient.HashCode] = "XYZ123";

            row[Gene.GeneName] = "BRCA1";

            row[VariantInstance.Status] = "New";
            row[VariantInstance.HashCode] = "XYZ123";
            row[VariantInstance.VariantClass] = "cDNA";
            row[VariantInstance.cDNA] = "c.1G>T";
            row[VariantInstance.mRNA] = "r.1G>U";
            row[VariantInstance.Genomic] = "g.100G>T";
            row[VariantInstance.Protein] = "p.33Ser>Cys";
            row[VariantInstance.InstanceDate] = new DateTime(2011, 4, 14, 17, 22, 0);
            row[VariantInstance.Pathogenicity] = "1";

            row[VariantInstance.PatientAge] = 36;
            row[VariantInstance.TestMethod] = "1";
            row[VariantInstance.SampleTissue] = "1";
            row[VariantInstance.SampleSource] = "1";
            row[VariantInstance.Justification] = "Justification";
            row[VariantInstance.PubMed] = "1234";
            row[VariantInstance.RecordedInDatabase] = true;
            row[VariantInstance.SampleStored] = true;
            row[VariantInstance.PedigreeAvailable] = true;
            row[VariantInstance.VariantSegregatesWithDisease] = true;
            row[VariantInstance.HistologyStored] = true;

            dt.Rows.Add(row);

            // === Output the data ===
            XmlOutput output = new XmlOutput();
            //output.Generate("testAll.xml", dt);

            // === Test read-back ===
            XmlDocument doc = ReadXml("testAll.xml");
            Assert.IsTrue(doc != null);
            Assert.IsTrue(doc.HasChildNodes);

            XmlNodeList nodes = doc.GetElementsByTagName("HVP_Transaction");
            Assert.IsTrue(nodes.Count == 1);

            XmlNode node = nodes[0];
            Console.WriteLine(node.Attributes["destination"].Value);
            Assert.AreEqual("https://portal.hvpaustralia.org.au", node.Attributes["destination"].Value);
            Assert.NotNull(node.Attributes["dateCreated"].Value);
            Assert.IsTrue(node.HasChildNodes);
            Assert.IsTrue(node.ChildNodes.Count == 1);

            node = node.ChildNodes[0];
            Assert.AreEqual("XYZ123", node.ChildNodes[0].InnerText);

            Assert.AreEqual("XYZ123", node.ChildNodes[1].InnerText);

            Assert.AreEqual("BRCA1", node.ChildNodes[2].InnerText);
            Assert.AreEqual("NM001732", node.ChildNodes[3].InnerText);
            Assert.AreEqual("1", node.ChildNodes[4].InnerText);

            Assert.AreEqual("New", node.ChildNodes[5].InnerText);
            Assert.AreEqual("XYZ123", node.ChildNodes[6].InnerText);
            Assert.AreEqual("cDNA", node.ChildNodes[7].InnerText);
            Assert.AreEqual("c.1G>T", node.ChildNodes[8].InnerText);
            Assert.AreEqual("r.1G>U", node.ChildNodes[9].InnerText);
            Assert.AreEqual("g.100G>T", node.ChildNodes[10].InnerText);
            Assert.AreEqual("p.33Ser>Cys", node.ChildNodes[11].InnerText);
            Assert.AreEqual("2011-04-14-17:22", node.ChildNodes[12].InnerText);
            Assert.AreEqual("1", node.ChildNodes[13].InnerText);

            Assert.AreEqual("Male", node.ChildNodes[14].InnerText);
            Assert.AreEqual("36", node.ChildNodes[15].InnerText);
            Assert.AreEqual("1", node.ChildNodes[16].InnerText);
            Assert.AreEqual("1", node.ChildNodes[17].InnerText);
            Assert.AreEqual("1", node.ChildNodes[18].InnerText);
            Assert.AreEqual("Justification", node.ChildNodes[19].InnerText);
            Assert.AreEqual("1234", node.ChildNodes[20].InnerText);
            Assert.AreEqual("true", node.ChildNodes[21].InnerText);
            Assert.AreEqual("true", node.ChildNodes[22].InnerText);
            Assert.AreEqual("true", node.ChildNodes[23].InnerText);
            Assert.AreEqual("true", node.ChildNodes[24].InnerText);
            Assert.AreEqual("true", node.ChildNodes[25].InnerText);
            

        }
    }
}
