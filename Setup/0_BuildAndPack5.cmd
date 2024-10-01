
echo.
echo fo-dicom builder
echo ================
echo. 

timeout /T 5
set GenerateDocumentation=1

@dotnet build ./../FO-DICOM.Core/FO-DICOM.Core.csproj --configuration Release

@dotnet build ./../Platform/FO-DICOM.Imaging.Desktop/FO-DICOM.Imaging.Desktop.csproj --configuration Release

@dotnet build ./../Platform/FO-DICOM.Imaging.ImageSharp/FO-DICOM.Imaging.ImageSharp.csproj --configuration Release

@dotnet build ./../Platform/FO-DICOM.Imaging.ImageSharp.NetStandard/FO-DICOM.Imaging.ImageSharp.NetStandard.csproj --configuration Release

@dotnet build ./../Serialization/FO-DICOM.Json/FO-DICOM.Json.csproj --configuration Release


echo.
