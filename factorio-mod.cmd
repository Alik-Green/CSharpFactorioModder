@echo off
rem ---------------------------------------------------------------------------------------------
rem factorio-mod for Windows, without make.
rem
rem   factorio-mod.cmd setup
rem   factorio-mod.cmd new MyMod
rem   factorio-mod.cmd build mods\HelloFactorio
rem   factorio-mod.cmd template
rem   factorio-mod.cmd plugin
rem
rem Every `make` target in this repository is a one-line wrapper around one of these commands, so
rem this script is the whole tool on a machine without GNU make. It builds the tool first if the
rem Release dll is missing, which is what `make tool` does.
rem ---------------------------------------------------------------------------------------------
setlocal

set "ROOT=%~dp0"
set "PROJECT=%ROOT%tools\Factorio.ModTool\Factorio.ModTool.csproj"
set "DLL=%ROOT%tools\Factorio.ModTool\bin\Release\net8.0\factorio-mod.dll"

where dotnet >nul 2>nul
if errorlevel 1 (
  echo error: 'dotnet' is not on PATH. Install the .NET 8 SDK: https://dotnet.microsoft.com/download/dotnet/8.0
  exit /b 1
)

if not exist "%DLL%" (
  echo building factorio-mod ...
  dotnet build "%PROJECT%" -c Release -v quiet --nologo
  if errorlevel 1 (
    echo error: the tool did not build; see the messages above.
    exit /b 1
  )
)

dotnet "%DLL%" %*
exit /b %errorlevel%
