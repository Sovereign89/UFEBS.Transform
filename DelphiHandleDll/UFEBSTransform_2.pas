unit UFEBSTransform_2;

interface

uses System.SysUtils;

type
  PUTF8Char = ^UTF8Char;

function TransformXML(FileNameIn: PUTF8Char): PUTF8Char; stdcall; external 'UFEBS.Transform.dll';
procedure SaveXMLToFile(FileNameIn: PUTF8Char; FileNameOut: PUTF8Char); stdcall; external 'UFEBS.Transform.dll';

function TransformXMLDelphi(const FileNameIn: string): string;
procedure SaveXMLToFileDelphi(const FileNameIn, FileNameOut: string);

implementation

function TransformXMLDelphi(const FileNameIn: string): string;
var
  InputFileUTF8: UTF8String;
  OutputFileUTF8: PUTF8Char;
begin
  InputFileUTF8 := UTF8String(FileNameIn);
  OutputFileUTF8 := TransformXML(PUTF8Char(InputFileUTF8));
  Result := string(OutputFileUTF8);
end;

procedure SaveXMLToFileDelphi(const FileNameIn, FileNameOut: string);
var
  InputFileUTF8, OutputFileUTF8: UTF8String;
  ResultBool: Boolean;
begin
  InputFileUTF8 := UTF8String(FileNameIn);
  OutputFileUTF8 := UTF8String(FileNameOut);
  SaveXMLToFile(PUTF8Char(InputFileUTF8), PUTF8Char(OutputFileUTF8));
end;

end.
