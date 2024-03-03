unit Main;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants,
  System.Classes, Vcl.Graphics, Vcl.Controls, Vcl.Forms, Vcl.Dialogs,
  Vcl.ComCtrls, Vcl.StdCtrls, Vcl.ExtCtrls;

type
  TForm_HandleDll = class(TForm)
    PageControl_Functions: TPageControl;
    TabSheet_TransformXML: TTabSheet;
    TabSheet_SaveXMLToFile: TTabSheet;
    Panel_Buttons: TPanel;
    Button_LoadXMLFile: TButton;
    RichEdit_ResultXML: TRichEdit;
    OpenDialogXML: TOpenDialog;
    Panel_Buttons2: TPanel;
    Button_LoadSaveXMLFile: TButton;
    RichEdit_SavedXML: TRichEdit;
    SaveDialogXML: TSaveDialog;
    procedure Button_LoadXMLFileClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button_LoadSaveXMLFileClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form_HandleDll: TForm_HandleDll;

implementation

{$R *.dfm}

uses
  UFEBSTransform_2;

procedure TForm_HandleDll.Button_LoadSaveXMLFileClick(Sender: TObject);
var
  InputFileName: WideString;
  OutputFileName: WideString;
  Result: Boolean;
begin
  if not OpenDialogXML.Execute then
  begin
    ShowMessage('User has cancelled, exiting!');
    exit;
  end;

  if not SaveDialogXML.Execute then
  begin
    ShowMessage('User has cancelled, exiting!');
    exit;
  end;

  InputFileName := OpenDialogXML.FileName;
  OutputFileName := SaveDialogXML.FileName;

  SaveXMLToFileDelphi(InputFileName,OutputFileName);
end;

procedure TForm_HandleDll.Button_LoadXMLFileClick(Sender: TObject);
var
  InputFileName: WideString;
  Result: WideString;
begin
  if not OpenDialogXML.Execute then
  begin
    ShowMessage('User has cancelled, exiting!');
    exit;
  end;

  InputFileName := OpenDialogXML.FileName;

  Result := TransformXMLDelphi(InputFileName);
  RichEdit_ResultXML.Lines.Text := Result;
end;

procedure TForm_HandleDll.FormCreate(Sender: TObject);
begin
  OpenDialogXML.InitialDir := GetCurrentDir;
  SaveDialogXML.InitialDir := GetCurrentDir;
end;

end.

