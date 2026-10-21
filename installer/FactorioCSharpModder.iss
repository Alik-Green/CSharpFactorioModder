; ---------------------------------------------------------------------------------------------
; Factorio C# Modder - Windows installer (Inno Setup 6)
;
; It installs the tree `factorio-mod dist` assembles: the tool, its API documents, bindings and
; compiler, the templates and the example mods. Then it offers two integrations:
;
;   * `factorio-mod` on PATH, so a console, MSBuild and the Rider plugin all find it;
;   * the "Factorio mod (C#)" project template, installed into `dotnet new` - which is the list
;     Rider's New Solution dialog shows.
;
; Build it with:
;
;   factorio-mod dist --output dist --version 1.2.3 --runtime win-x64
;   iscc /DAppVersion=1.2.3 /DPayloadDir=<absolute-or-relative dist folder> installer\FactorioCSharpModder.iss
;
; or just `installer\build.cmd 1.2.3`, which does both.
;
; Switches the installer understands, on top of Inno's own (/SILENT, /DIR=, /TASKS=, /ALLUSERS):
;
;   /TEMPLATE     install the Rider / dotnet new template without asking - for scripted installs
;   /NOTEMPLATE   never install it
; ---------------------------------------------------------------------------------------------

#ifndef AppVersion
  #define AppVersion "0.0.0-dev"
#endif

#ifndef PayloadDir
  #define PayloadDir "..\dist"
#endif

#define AppName "Factorio C# Modder"
#define AppExe "factorio-mod.exe"
#define AppPublisher "factorio-csharp"
#define AppUrl "https://github.com/Alik-Green/CSharpFactorioModder"

; The payload is the thing that gets installed; fail here rather than shipping an empty installer.
#if !FileExists(PayloadDir + "\VERSION") && !FileExists(AddBackslash(SourcePath) + PayloadDir + "\VERSION")
  #error The payload folder is missing. Run `factorio-mod dist --output dist --runtime win-x64` first, or pass /DPayloadDir=<folder>.
#endif

[Setup]
AppId={{9C5F3C3A-6C2B-4E7E-9E7B-1B6B3C4B7A11}
AppName={#AppName}
AppVersion={#AppVersion}
AppVerName={#AppName} {#AppVersion}
AppPublisher={#AppPublisher}
AppPublisherURL={#AppUrl}
AppSupportURL={#AppUrl}
AppUpdatesURL={#AppUrl}
VersionInfoVersion={#AppVersion}
VersionInfoCompany={#AppPublisher}
VersionInfoDescription={#AppName} installer

; A per-user install by default (no UAC); the user can choose "install for all users" on the
; first page, and {autopf} follows that choice.
PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
DefaultDirName={autopf}\Factorio C# Modder
DisableProgramGroupPage=yes
DefaultGroupName={#AppName}

; The tool is win-x64 only: the vendored compiler and the .NET runtime it needs are x64.
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible

OutputDir=..\release
OutputBaseFilename=FactorioCSharpModder-{#AppVersion}-win64
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
ShowLanguageDialog=no
ChangesEnvironment=yes
CloseApplications=no
UninstallDisplayName={#AppName} {#AppVersion}
UninstallDisplayIcon={app}\bin\{#AppExe}
DisableWelcomePage=no

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "addtopath"; Description: "Add factorio-mod to PATH (needed by the project template and the Rider plugin)"; GroupDescription: "Integration:"

[Files]
; Everything `factorio-mod dist` wrote. The layout is the repository's, because the tool finds its
; own root by walking up to config\toolchain.json.
Source: "{#PayloadDir}\*"; DestDir: "{app}"; Excludes: "build\*,build\*.*"; \
    Flags: ignoreversion recursesubdirs createallsubdirs

[Registry]
; HKA is HKLM for an all-users install and HKCU for a per-user one, decided by the first page.
Root: HKA; Subkey: "Environment"; ValueType: expandsz; ValueName: "Path"; ValueData: "{olddata};{app}\bin"; \
    Tasks: addtopath; Check: NeedsAddPath(ExpandConstant('{app}\bin'))

[Icons]
Name: "{group}\Uninstall {#AppName}"; Filename: "{uninstallexe}"
Name: "{group}\Factorio mod documentation"; Filename: "{app}\docs\RIDER.md"

[Run]
; Nothing to launch. The template question and the PATH cleanup live in [Code], because both need to
; explain themselves when they fail - a [Run] entry that flashes a console window does not.

[UninstallDelete]
Type: filesandordirs; Name: "{app}\build"

[Code]

{ Whether a specific switch was passed on the Setup command line. }
function CmdLineParamExists(const Value: string): Boolean;
var
  I: Integer;
begin
  Result := False;
  for I := 1 to ParamCount do
  begin
    if CompareText(ParamStr(I), Value) = 0 then
    begin
      Result := True;
      exit;
    end;
  end;
end;

{ Whether `dotnet` can be run at all. Called once, because it starts a process. }
function HasDotnet(): Boolean;
var
  ResultCode: Integer;
begin
  if FileExists(ExpandConstant('{%USERPROFILE|}\.dotnet\dotnet.exe')) then
  begin
    Result := True;
    exit;
  end;
  if not Exec(ExpandConstant('{cmd}'), '/C dotnet --version >nul 2>&1', '',
              SW_HIDE, ewWaitUntilTerminated, ResultCode) then
  begin
    Result := False;
    exit;
  end;
  Result := ResultCode = 0;
end;

{ Whether PATH already contains the folder, so installing twice does not add it twice. }
function NeedsAddPath(Param: string): Boolean;
var
  Root: Integer;
  Path: string;
begin
  if IsAdminInstallMode then Root := HKLM else Root := HKCU;
  if not RegQueryStringValue(Root, 'Environment', 'Path', Path) then
  begin
    Result := True;
    exit;
  end;
  { The comparison has to be case-insensitive and tolerant of a trailing separator. }
  Result := Pos(';' + Uppercase(Param) + ';', ';' + Uppercase(Path) + ';') = 0;
end;

{ Removes one entry from PATH on uninstall, keeping everything else. }
procedure RemoveFromPath(Entry: string);
var
  Root, P: Integer;
  Path, Rest, Piece, Updated: string;
begin
  if IsAdminInstallMode then Root := HKLM else Root := HKCU;
  if not RegQueryStringValue(Root, 'Environment', 'Path', Path) then exit;

  Rest := Path;
  Updated := '';
  while Rest <> '' do
  begin
    P := Pos(';', Rest);
    if P > 0 then
    begin
      Piece := Copy(Rest, 1, P - 1);
      Delete(Rest, 1, P);
    end
    else
    begin
      Piece := Rest;
      Rest := '';
    end;

    if (Trim(Piece) <> '') and (CompareText(Trim(Piece), Entry) <> 0) then
    begin
      if Updated <> '' then Updated := Updated + ';';
      Updated := Updated + Piece;
    end;
  end;

  if Updated <> Path then
    RegWriteExpandStringValue(Root, 'Environment', 'Path', Updated);
end;

{ Runs `factorio-mod template` in the installation, which installs the "Factorio mod (C#)"
  project template into dotnet new - the list Rider's New Solution dialog reads. }
procedure InstallTemplate();
var
  ResultCode: Integer;
begin
  if WizardForm <> nil then
    WizardForm.StatusLabel.Caption := 'Installing the Factorio mod template...';

  if not Exec(ExpandConstant('{cmd}'),
              '/C ""' + ExpandConstant('{app}\bin\factorio-mod.cmd') + '" template"',
              ExpandConstant('{app}'),
              SW_HIDE, ewWaitUntilTerminated, ResultCode) or (ResultCode <> 0) then
  begin
    MsgBox('The project template could not be installed (exit code ' + IntToStr(ResultCode) + ').' + #13#10 + #13#10 +
           'You can install it at any time with:' + #13#10 + #13#10 +
           '    factorio-mod template', mbError, MB_OK);
  end;
end;

{ The question itself, asked once the files are in place - which is the point at which it makes
  sense: the template comes from the installation. }
procedure OfferTemplate();
var
  Answer: Integer;
begin
  if not HasDotnet() then
  begin
    MsgBox('The Factorio mod template cannot be installed yet: the .NET 8 SDK is not on PATH.' + #13#10 + #13#10 +
           'Mods are compiled with it, so it is needed anyway. Install it from ' +
           'https://dotnet.microsoft.com/download/dotnet/8.0 and then run:' + #13#10 + #13#10 +
           '    factorio-mod template', mbInformation, MB_OK);
    exit;
  end;

  Answer := MsgBox('Add the "Factorio mod (C#)" project template to Rider and dotnet new?' + #13#10 + #13#10 +
                   'It puts the template in Rider''s New Solution dialog, and adds' + #13#10 +
                   '`dotnet new factorio-mod` to the command line.' + #13#10 + #13#10 +
                   'You can remove it later with `factorio-mod template --uninstall`.',
                   mbConfirmation, MB_YESNO);
  if Answer = IDYES then InstallTemplate();
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    { A silent install must not stop on a dialog. /TEMPLATE opts in without asking, so a script can
      still get the template; without it, silent means "files only". }
    if CmdLineParamExists('/NOTEMPLATE') then
      exit
    else if CmdLineParamExists('/TEMPLATE') then
      InstallTemplate()
    else if not WizardSilent() then
      OfferTemplate();
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  ResultCode: Integer;
begin
  if CurUninstallStep = usUninstall then
  begin
    { Take the template out again, but only if it can be done quietly: an uninstall must not fail
      because dotnet disappeared since the install. }
    if HasDotnet() then
    begin
      Exec('dotnet', 'new uninstall "' + ExpandConstant('{app}\templates\dotnet\factorio-mod') + '"',
           '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
    end;

    RemoveFromPath(ExpandConstant('{app}\bin'));
  end;
end;

{ A last look at the machine before the user is told it is done. }
procedure CurPageChanged(CurPageID: Integer);
begin
  if CurPageID = wpFinished then
  begin
    if not HasDotnet() then
      WizardForm.FinishedLabel.Caption := WizardForm.FinishedLabel.Caption + #13#10#13#10 +
        'Note: the .NET 8 SDK was not found. Install it before building a mod: ' +
        'https://dotnet.microsoft.com/download/dotnet/8.0';
  end;
end;
