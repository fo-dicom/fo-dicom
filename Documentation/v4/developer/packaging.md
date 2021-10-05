### Visual Studio 2017 build tools
**Setup/01_Build.cmd** is now updated to use Visual Studio 2017 build tools. When running the command file, error messages may occur at the beginning of the run, similar to the following:

> DICOM.Universal.csproj: error MSB4019: The imported project "C:\Program Files (x86)\MSBuild\Microsoft\WindowsXaml\v15.0\Microsoft.Windows.UI.Xaml.CSharp.targets" was not found. Confirm that the path in the <Import> declaration is correct, and that the file exists on disk.

These errors do not seem to have any impact on the actual build, but should nevertheless be avoided.

As far as I have been able to find out, the errors are due to a mismatch in the **msbuild** installation related to Visual Studio 2017. The problem is most easily solved by:

1. Creating a folder *v15.0* in the *C:\Program Files (x86)\MSBuild\Microsoft\WindowsXaml* folder.
2. From the folder *C:\Program Files (x86)\MSBuild\Microsoft\WindowsXaml\v14.0*, copy the following files:
    * Microsoft.Windows.UI.Xaml.Common.Targets
    * Microsoft.Windows.UI.Xaml.CPP.Targets
    * Microsoft.Windows.UI.Xaml.CSharp.Targets