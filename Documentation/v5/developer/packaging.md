### Visual Studio build tools

**Setup/00_BuildAndPack5.cmd** can help you to build and package the projects, that are also available on nuget.

The path of *dotnet.exe* has to be included in `path` environment variable, so that the script can run successfully.

The projects, that are targeting .Net Standard or .Net Core are built in release confguration and automatically create nuget packages. This feature is not available in projects that target Net Framework. So for `FO-DICOM.Imaging.Desktop` a call to *nuget.exe* has to be done to create a package for this project.
