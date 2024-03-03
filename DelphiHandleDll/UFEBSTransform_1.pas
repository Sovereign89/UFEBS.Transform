unit UFEBSTransform_1;

interface

uses
  Windows;

type
  PUTF8Char = ^UTF8Char;
  TTransformXMLFunc = function(FileNameIn: PUTF8Char): PUTF8Char; stdcall;
  TSaveXMLToFileFunc = procedure(FileNameIn: PUTF8Char; FileNameOut: PUTF8Char); stdcall;

var
  TransformXML: TTransformXMLFunc;
  SaveXMLToFile: TSaveXMLToFileFunc;
  DllHandle: THandle;

function IsLibraryLoaded: Boolean;
function InitializeLibrary: Boolean;
procedure UninitializeLibrary;
function TransformXMLDelphi(const FileNameIn: string): string;
function SaveXMLToFileDelphi(const FileNameIn, FileNameOut: string): boolean;

implementation

function IsLibraryLoaded: Boolean;
begin
  Result := DllHandle <> 0;
end;

function InitializeLibrary: Boolean;
begin
  DllHandle := LoadLibrary('UFEBS.Transform.dll');
  if DllHandle = 0 then
  begin
    Result := False;
    Exit;
  end;

  @TransformXML := GetProcAddress(DllHandle, 'TransformXML');
  if @TransformXML = nil then
  begin
    FreeLibrary(DllHandle);
    Result := False;
    Exit;
  end;

  @SaveXMLToFile := GetProcAddress(DllHandle, 'SaveXMLToFile');
  if @SaveXMLToFile = nil then
  begin
    FreeLibrary(DllHandle);
    Result := False;
    Exit;
  end;

  Result := True;
end;

procedure UninitializeLibrary;
begin
  if IsLibraryLoaded then
    FreeLibrary(DllHandle);
end;

function TransformXMLDelphi(const FileNameIn: string): string;
var
  InputFileUTF8: UTF8String;
  OutputFileUTF8: PUTF8Char;
begin
  if not IsLibraryLoaded then begin
    if not InitializeLibrary then
    begin
      Result := '';
      Exit;
    end;
  end;

  InputFileUTF8 := UTF8String(FileNameIn);
  OutputFileUTF8 := TransformXML(PUTF8Char(InputFileUTF8));
  Result := string(OutputFileUTF8);
end;

function SaveXMLToFileDelphi(const FileNameIn, FileNameOut: string): boolean;
var
  InputFileUTF8, OutputFileUTF8: UTF8String;
begin
  Result := false;
  if not IsLibraryLoaded then begin
    if not InitializeLibrary then
    begin
      Exit;
    end;
  end;

  InputFileUTF8 := UTF8String(FileNameIn);
  OutputFileUTF8 := UTF8String(FileNameOut);
  SaveXMLToFile(PUTF8Char(InputFileUTF8), PUTF8Char(OutputFileUTF8));
  Result := true;
end;

end.
