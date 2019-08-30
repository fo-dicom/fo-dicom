@echo off

@set "VS17COMMONTOOLS=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\"
@if NOT EXIST "%VS17COMMONTOOLS%" goto error_VS17_notfound

echo.
echo fo-dicom builder
echo ================
echo. 
echo This Windows batch file will use Visual Studio 2017 Professional
echo to compile the Release versions of fo-dicom.
echo.
echo Path to VS 2017 common tools folder is set as follows:
echo %VS17COMMONTOOLS%
echo.

timeout /T 5

@call "%VS17COMMONTOOLS%VsDevCmd.bat"

@cd ..
@.\Setup\nuget.exe restore Dicom.Full.sln -source https://www.nuget.org/api/v2
@msbuild "DICOM.Full.sln" /t:Rebuild /p:Configuration=Release;Platform="Any CPU"
@msbuild "DICOM.Full.sln" /t:Rebuild /p:Configuration=Release;Platform="x86"
@msbuild "DICOM.Full.sln" /t:Rebuild /p:Configuration=Release;Platform="x64"
@msbuild "DICOM.Full.sln" /t:Rebuild /p:Configuration=Release;Platform="ARM"

@goto end

@REM -----------------------------------------------------------------------
:error_VS17_notfound
@echo ERROR: 
@echo Path to the Visual Studio 2017 common tools folder is invalid.
@echo Update VS17COMMONTOOLS to a valid path and run again.
@goto end

:end
pause
