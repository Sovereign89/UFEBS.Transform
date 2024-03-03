object Form_HandleDll: TForm_HandleDll
  Left = 0
  Top = 0
  Caption = 'DelphiHandleDll'
  ClientHeight = 689
  ClientWidth = 701
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'Segoe UI'
  Font.Style = []
  Position = poScreenCenter
  OnCreate = FormCreate
  TextHeight = 13
  object PageControl_Functions: TPageControl
    Left = 0
    Top = 0
    Width = 701
    Height = 689
    ActivePage = TabSheet_SaveXMLToFile
    Align = alClient
    TabOrder = 0
    object TabSheet_TransformXML: TTabSheet
      Caption = 'TransformXML'
      object Panel_Buttons: TPanel
        AlignWithMargins = True
        Left = 3
        Top = 3
        Width = 687
        Height = 38
        Align = alTop
        BevelOuter = bvNone
        TabOrder = 0
        object Button_LoadXMLFile: TButton
          AlignWithMargins = True
          Left = 3
          Top = 3
          Width = 206
          Height = 32
          Align = alLeft
          Caption = 'Load and transform XML file'
          TabOrder = 0
          OnClick = Button_LoadXMLFileClick
        end
      end
      object RichEdit_ResultXML: TRichEdit
        AlignWithMargins = True
        Left = 3
        Top = 47
        Width = 687
        Height = 611
        Align = alClient
        BevelInner = bvNone
        BevelOuter = bvNone
        Font.Charset = RUSSIAN_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Segoe UI'
        Font.Style = []
        ParentFont = False
        PlainText = True
        ReadOnly = True
        ScrollBars = ssVertical
        TabOrder = 1
      end
    end
    object TabSheet_SaveXMLToFile: TTabSheet
      Caption = 'SaveXMLToFile'
      ImageIndex = 1
      object Panel_Buttons2: TPanel
        Left = 0
        Top = 0
        Width = 693
        Height = 41
        Align = alTop
        BevelOuter = bvNone
        TabOrder = 0
        ExplicitLeft = 312
        ExplicitTop = 160
        ExplicitWidth = 185
        object Button_LoadSaveXMLFile: TButton
          AlignWithMargins = True
          Left = 3
          Top = 3
          Width = 254
          Height = 35
          Align = alLeft
          Caption = 'Load, transform, and save xml file to output name'
          TabOrder = 0
          OnClick = Button_LoadSaveXMLFileClick
        end
      end
      object RichEdit_SavedXML: TRichEdit
        AlignWithMargins = True
        Left = 3
        Top = 44
        Width = 687
        Height = 614
        Align = alClient
        BevelInner = bvNone
        BevelOuter = bvNone
        Font.Charset = RUSSIAN_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Segoe UI'
        Font.Style = []
        ParentFont = False
        PlainText = True
        ReadOnly = True
        ScrollBars = ssVertical
        TabOrder = 1
        ExplicitTop = 47
        ExplicitHeight = 611
      end
    end
  end
  object OpenDialogXML: TOpenDialog
    DefaultExt = '*.xml'
    Filter = 'XML File|*.xml'
    Options = [ofHideReadOnly, ofPathMustExist, ofFileMustExist, ofEnableSizing]
    Title = 'Load XML'
    Left = 516
    Top = 32
  end
  object SaveDialogXML: TSaveDialog
    DefaultExt = '*.xml'
    Filter = 'XML file|*.xml'
    Title = 'Save XML'
    Left = 620
    Top = 32
  end
end
