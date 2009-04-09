[Setup]
AppName=Incremental File Opener for Visual Studio 2005/2008
AppVerName=IncrementalFileOpener v1.0
AppCopyright=Copyright Martin Knafve 2009
DefaultDirName={pf}\IncrementalFileOpener
PrivilegesRequired=admin
OutputBaseFilename=IncrementalFileOpener
SolidCompression=yes
Uninstallable=true
DirExistsWarning=no
CreateAppDir=true

[Components]
Name: "VisualStudio2005"; Description: "Visual Studio 2005"; Types: full; Check: DirExists(ExpandConstant('{userdocs}\Visual Studio 2005'))
Name: "VisualStudio2008"; Description: "Visual Studio 2008"; Types: full; Check: DirExists(ExpandConstant('{userdocs}\Visual Studio 2008'))

[Files]
Source: ..\Source\bin\IncrementalOpener.dll; DestDir: {app};  AfterInstall: WriteAddinFile

[Dirs]
Name: "{userdocs}\Visual Studio 2005\Addins"; Check: DirExists(ExpandConstant('{userdocs}\Visual Studio 2005'))
Name: "{userdocs}\Visual Studio 2008\Addins"; Check: DirExists(ExpandConstant('{userdocs}\Visual Studio 2005'))

[Messages]
BeveledLabel=Incremental File Opener

[Code]
procedure WriteAddinFile();
var
  output: String;
begin

   output := output + '<?xml version="1.0" standalone="no"?>' + #13#10;
   output := output + '<Extensibility xmlns="http://schemas.microsoft.com/AutomationExtensibility">' + #13#10;
   output := output + '	<HostApplication>' + #13#10;
   output := output + '		<Name>Microsoft Visual Studio</Name>' + #13#10;
   output := output + '		<Version>9.0</Version>' + #13#10;
   output := output + '	</HostApplication>' + #13#10;
   output := output + '	<HostApplication>' + #13#10;
   output := output + '		<Name>Microsoft Visual Studio</Name>' + #13#10;
   output := output + '		<Version>8.0</Version>' + #13#10;
   output := output + '	</HostApplication>' + #13#10;
   output := output + '	<Addin>' + #13#10;
   output := output + '		<FriendlyName>IncrementalOpener</FriendlyName>' + #13#10;
   output := output + '		<Description>Incremental file opener for Visual Studio 2005/2008.</Description>' + #13#10;
   output := output + '		<Assembly>' + ExpandConstant('{app}') + '\IncrementalOpener.dll</Assembly>' + #13#10;
   output := output + '		<FullClassName>IncrementalOpener.Connect</FullClassName>' + #13#10;
   output := output + '		<LoadBehavior>1</LoadBehavior>' + #13#10;
   output := output + '		<CommandPreload>1</CommandPreload>' + #13#10;
   output := output + '		<CommandLineSafe>0</CommandLineSafe>' + #13#10;
   output := output + '	</Addin>' + #13#10;
   output := output + '</Extensibility>' + #13#10;

   if (IsComponentSelected('VisualStudio2005')) then
   begin
     SaveStringToFile(ExpandConstant('{userdocs}') + '\Visual Studio 2005\Addins\IncrementalOpener.AddIn', output, false);
   end;
   
   if (IsComponentSelected('VisualStudio2008')) then
   begin
    SaveStringToFile(ExpandConstant('{userdocs}') + '\Visual Studio 2008\Addins\IncrementalOpener.AddIn', output, false);
   end;
end;


