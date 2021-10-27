
echo.
echo fo-dicom builder
echo ================
echo. 

timeout /T 5
set GenerateDocumentation=1

@dotnet build ./../FO-DICOM.Core/FO-DICOM.Core.csproj --configuration Release

@dotnet build ./../Platform/FO-DICOM.Imaging.Desktop/FO-DICOM.Imaging.Desktop.csproj --configuration Release

@dotnet build ./../Platform/FO-DICOM.Imaging.ImageSharp/FO-DICOM.Imaging.ImageSharp.csproj --configuration Release

@dotnet build ./../Logging/FO-DICOM.NLog/FO-DICOM.NLog.csproj --configuration Release


echo.
echo fo-dicom NuGet package builder for FO-DICOM.Imaging.Desktop
echo ==============================
echo. 
echo This Windows batch file uses NuGet to automatically
echo build the fo-dicom NuGet packages.
echo. 

timeout /T 5

@nuget pack ./../Platform/FO-DICOM.Imaging.Desktop/FO-DICOM.Imaging.Desktop.nuspec -OutputDirectory ./../Platform/FO-DICOM.Imaging.Desktop/bin
pause
