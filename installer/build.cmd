@echo off
rem ---------------------------------------------------------------------------------------------
rem Builds the Windows installer from a checkout: `dist`, then Inno Setup.
rem
rem   installer\build.cmd 1.2.3
rem
rem Needs the .NET 8 SDK, a built CSharp.lua compiler (`factorio-mod bootstrap`, once) and
rem Inno Setup 6 (https://jrsoftware.org/isdl.php). The release output is:
rem
rem   release\FactorioCSharpModder-<version>-win64.exe
rem
rem CI runs the same two commands; see docs\RELEASING.md.
rem ---------------------------------------------------------------------------------------------
setlocal

set "VERSION=%~1"
if "%VERSION%"=="" (
  set /p VERSION=Version (1.2.3): 
)
if "%VERSION%"=="" (
  echo error: a version is required.
  exit /b 1
)
rem A tag is often v1.2.3; Inno wants the number.
set "VERSION=%VERSION:v=%"

set "ROOT=%~dp0.."
pushd "%ROOT%"

rem The Rider plugin is optional.
set "PLUGIN="
if exist "plugin\*.zip" set "PLUGIN=--plugin plugin"

set "ISCC=%ProgramFiles(x86)%\Inno Setup 6\ISCC.exe"
if not exist "%ISCC%" set "ISCC=%ProgramFiles%\Inno Setup 6\ISCC.exe"
if not exist "%ISCC%" (
  echo error: Inno Setup 6 was not found. Install it, or set ISCC to the compiler.
  popd
  exit /b 1
)

echo.
echo === dist ===
call factorio-mod.cmd dist --output dist --version %VERSION% --runtime win-x64 %PLUGIN%
if errorlevel 1 goto :error

echo.
echo === installer ===
"%ISCC%" /DAppVersion=%VERSION% /DPayloadDir="%ROOT%\dist" installer\FactorioCSharpModder.iss
if errorlevel 1 goto :error

popd
echo.
echo release\FactorioCSharpModder-%VERSION%-win64.exe
exit /b 0

:error
popd
echo error: the installer was not built.
exit /b 1
