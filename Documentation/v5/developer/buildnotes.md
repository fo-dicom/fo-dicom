### Prerequisites for *development* branch

Fellow Oak Dicom is developed with Visual Studio 2017 or later. 

*fo-dicom.core* targets .Net Standard 2.0 and uses C# 8 as language version. This requires NetCore3 SDK to be installed on the machine.

*fo-dicom.Tests* are targeting .Net Framework 4.6.2, .Net Core 2.1 and .Net Core 3.1. In order to run the unittests you need to have those SDKs installed.


### Getting the source code

- Fork the repository [fo-dicom/fo-dicom](https://github.com/fo-dicom/fo-dicom) into your repository and clone it to your workstation using your favorite git client. This is the prefered way if you plan to contribute.
- Or simply download the sources as ZIP file from github.

The repository contains several project and solution files. The current solution is **FO-DICOM.Full.sln**, so open this solution file. *Dicom.Full.sln* should be opened if you need to open the old version for bugfixing.

After opening the solution file you will find several projects:

Project | Description
----------- | ------------
FO-DICOM.Core | This project contains the core logic for handling DICOM files, and working with DICOM services
Tools/FO-DICOM.Dump | Is is some sample project that can be used to reproduce some isses and debug into the code. The application opens files, shows all tags and values, can anonymize the file and render the image.
Tests/FO-DICOM.Tests | The xUnit based unit test project
Tests/FO-DICOM.Benchmark | This project executes a benchmark test for some typical actions like opening and parsing a dataset or sending a file via store scu and scp.
Platform/* | Within this folder there are several projects, that contain some platform specific code and can be then registered in DI container.
Logging/* | Projects integrating logging frameworks
Serialization/* | Projects integrationg serialization into Json, Xml or any other format



