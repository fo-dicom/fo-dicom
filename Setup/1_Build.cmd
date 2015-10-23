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
@msbuild "DICOM.Full.sln" /t:Rebuild /p:Configuration=Release;Platform="Any CPU"

@goto end

@REM -----------------------------------------------------------------------
:error_no_VS140COMNTOOLSDIR
@echo ERROR: Cannot determine the location of the VS Common Tools folder.
@goto end

:end