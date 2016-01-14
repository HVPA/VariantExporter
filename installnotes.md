# Installation Notes

## Introduction and Notes.
The following document prescribes steps to setup a Developer’s environment to develop VariantExporter and operate deployments for Sites using the HVPA tool chain.

Assumes Windows 7

## Get software tools

1. Microsoft Visual Studio. Please review you are using an appropriate license of the product that meets your purposes.
2. Windows Git
3. Python for Windows
4. Microsoft Office (optional, recommended). Microsoft Office permits the usage of MS Office interop DLL’s which are libraries the Exporter utilises to open Microsoft office files such as MS Excel spreadsheets and MS Access databases. 

## Get source code

```
git clone https://bt_alanlo@bitbucket.org/hvpa/variantexporter.git VariantExporter
git clone https://bt_alanlo@bitbucket.org/hvpa/hvp_encryptor.git HVP_Encryptor
git clone https://bt_alanlo@bitbucket.org/hvpa/hgvs_nomenclatureparser.git HGVS_NomenclatureParser
```

## Build python based tools
Certain components of the HVPA tool chain were developed in python. In order to use the same code, the VariantExporter runs command line versions of the code rather than rewriting them in .NET

```
cd HVP_Encryptor\
python setup.py py2exe
cd ..
```

**! Copy HVP_Encryptor\dist\HVP_Encryptor.exe to VariantExporterWinGUI\Executables\**

```
cd HGVS_NomenclatureParser\
python setup.py py2exe
```

**! Copy HGVS_NomenclatureParser\dist\HGVS_NomenclatureParser to VariantExporterWinGUI\Executables\**

 
## Setting up your development environment

**! Install visual studio and open VariantExpoter\VariantExporter.sln solution**

The main application is VariantExporterWinGUI, which is a native windows application that is installed at the laboratory.

Before you can build a complete binary, sure:

1) Ensure the target framework is .NET 3.5

To set this, go to Project > VariantExporterWinGUI Properties > Application > Target Framework

2) Copy the node’s public key to the VariantExporter\VariantExporterWinGUI\Keys folder, and remember the name of that file

3) Copy the laboratory’s public and private key to the same folder. Instructions for generating the keys later in this document 

4) Edit and review the properties in the VariantExporterWinGUI\Configuration.xml file. In particular:

> OrganisationHashCode: The hash code for this site

> PublicKey: The file name of the Node’s public key file inside the Keys folder

> PrivateKey: The file name of the site’s private key file inside the Keys folder. This should be the hash code for the site with a ‘.private’ extension

> ServerAddress: The url of the production node server’s importer service

> TestServerAddress: The url of the test server’s importer service

> Proxy setting values: This will be dependant on the laboratory’s network setup

> AutoUpdateLink: The url of the server holding the binary’s installation files so that it can check for updates

> PassKey: The password for this laboratory site. Instructions to generate this later in this document


## Generate keys for new Site/Laboratory
This section walks through how you setup a new Laboratory. 

**! Obtain the hashcode created by the Node for this site**

```
cd HVP_Encryptor\
python HVP_Encryptor.py -c —keyname=(hashcode)
```

**! Copy both .private and .public files into VariantExporterWinGUI\Keys\**

**! Put the .public key on the node under VariantImporter/keys**

```
In VisualStudio using the Solution Explorer under the VariantExporterWinGUI project,

   Goto Keys

      Add Existing Items > .public key

   In Properties for this .public key

      Set ‘Copy to Output Directory’ to ‘Copy Always’
```





