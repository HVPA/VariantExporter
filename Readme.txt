Prerequisite:
- .NET 3.5 Framework
- Office 2003+

VariantExporter:
Windows application thats been tested to work on Windows XP, Windows 7 and Windows 8 with 
the .NET 3.5 Framework installed. The VariantExporter is designed to be a modular desktop client
that you can write specific plugins to extract the variant data from clinical laboratory data 
systems setup such as traditional DBMS and file structured datasources like spreadsheets and csv files.

Setup:
The Project was developed using Visual Studio 2010, but you can always open the project solution and migrate
the solution to the Visual Studio version you are using.

You will need to ensure that your plugin dll's and xml's are copied to "VariantExporterWinGUI\Plugins"
directory during the build process the VariantExporterWinGUI project will copy any files in the 
Plugins directory to bin directory.

There should be a folder called "Executables" in VariantExporterWinGUI project, you need to compile 2
executables in the folder. 

The first executable you need is the HVP_Encryption.exe, which is a python script that has been compiled 
into an .exe:

To get the HVP_Encryption.exe, you will need to build it from source, get the source from: HVP_Encryptor repo.
Run "python setup.py p2exe" this will create a dist folder with the HVP_Encryption.exe, copy this file to the 
Executables directory in the VariantExporterWinGUI project.

You will also need the Validation.exe, which is also a python script that needs to be compiled into an .exe:

To get the Validation.exe, you will need to build it from source, get the source from: HGVS_NomenclatureParser
repo. 
Run "python setup.py p2exe" this will create a dist folder with the Validator.exe, copy this file to the
Executables directory in the VariantExporterWinGUI project.

NB: In order to run the converted py2exe .exe files the client machine needs to have at least .NET 2.0 and 
VS C++ 2008(or higher) Redistributable Package

Building the project will generate all the necessary files and folders in the VariantExporterWinGUI 
bin debug/release directory. Once you have successfully built the project running the VariantExporterWinGUI.exe from the bin directory 
should start the application.

