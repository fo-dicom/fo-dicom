@echo off

@set "MONOBINDIR=%ProgramFiles(x86)%\Mono\bin\"
@if NOT EXIST "%MONOBINDIR%" goto error_mono_notfound

echo.
echo fo-dicom builder for Mono
echo =========================
echo. 
echo This Windows batch file will use Mono xbuild
echo to compile the Release version of fo-dicom for Mono.
echo.
echo Path to Mono bin folder is set as follows:
echo %MONOBINDIR%
echo.

timeout /T 5

@set PATH=%MONOBINDIR%;%PATH%

@cd ..
@.\Setup\nuget.exe restore Dicom.Mono.sln -source https://www.nuget.org/api/v2
@msbuild Platform\Mono\Dicom.Mono.csproj /target:Rebuild /property:Configuration=Release;Platform=AnyCPU

@goto end

@REM -----------------------------------------------------------------------
:error_mono_notfound
@echo ERROR: 
@echo Path to the Mono bin folder is invalid.
@echo Update MONOBINDIR to a valid path and run again.
@goto end

:end
@pause
