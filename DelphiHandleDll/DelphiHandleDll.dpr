program DelphiHandleDll;

uses
  Vcl.Forms,
  System.SysUtils,
  Windows,
  Dialogs,
  Main in 'Main.pas' {Form_HandleDll},
  UFEBSTransform_1 in 'UFEBSTransform_1.pas',
  UFEBSTransform_2 in 'UFEBSTransform_2.pas';

{$R *.res}

begin
  if not FileExists('UFEBS.Transform.dll') then
  begin
    ShowMessage('UFEBS.Transform.dll not found. Cannot start the application.');
    Exit;
  end;

  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TForm_HandleDll, Form_HandleDll);
  Application.Run;
end.
