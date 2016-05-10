@echo off

echo.
echo fo-dicom builder
echo ================
echo. 
echo This Windows batch file will use Visual Studio 2015 to
echo compile the Release versions of fo-dicom.
echo. 

timeout /T 5

@if "%VS140COMNTOOLS%"=="" goto error_no_VS140COMNTOOLSDIR
@call "%VS140COMNTOOLS%VsDevCmd.bat"

@cd ..
@.\Setup\nuget.exe restore Dicom.Full.sln
@msbuild "DICOM.Full.sln" /t:Rebuild /p:Configuration=Release;Platform="Any CPU"
@msbuild "DICOM.Full.sln" /t:Rebuild /p:Configuration=Release;Platform="x86"
@msbuild "DICOM.Full.sln" /t:Rebuild /p:Configuration=Release;Platform="x64"
@msbuild "DICOM.Full.sln" /t:Rebuild /p:Configuration=Release;Platform="ARM"

@goto end

@REM -----------------------------------------------------------------------
:error_no_VS140COMNTOOLSDIR
@echo ERROR: Cannot determine the location of the VS Common Tools folder.
@goto end

:end
pause
