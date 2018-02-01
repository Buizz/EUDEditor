Module RegModule
    '    Public Structure mnuCommands
    '        Public Captions As Collection
    '        Public Commands As Collection
    '    End Structure

    '    Public Structure filetype
    '        Public Commands As mnuCommands
    '        Public Extension As String
    '        Public ProperName As String
    '        Public FullName As String
    '        Public ContentType As String
    '        Public IconPath As String
    '        Public IconIndex As Integer
    '    End Structure

    '    Public Const REG_SZ = 1
    '    Public Const HKEY_CLASSES_ROOT = &H80000000

    '    Public Declare Function RegCloseKey Lib "advapi32.dll" (ByVal hKey As Long) As Long
    '    Public Declare Function RegCreateKey Lib "advapi32" Alias "RegCreateKeyA" (ByVal _
    'hKey As Long, ByVal lpszSubKey As String, phkResult As Long) As Long
    '    Public Declare Function RegSetValueEx Lib "advapi32" Alias "RegSetValueExA" (ByVal _
    'hKey As Long, ByVal lpszValueName As String, ByVal dwReserved As Long, ByVal fdwType As Long, lpbData As Any, ByVal cbData As Long) As Long


    '    Public Sub CreateExtension(newfiletype As filetype)

    '        Dim IconString As String
    '        Dim Result As Long, Result2 As Long, ResultX As Long
    '        Dim ReturnValue As Long, HKeyX As Long
    '        Dim cmdloop As Integer

    '        IconString = newfiletype.IconPath & "," & newfiletype.IconIndex

    '        If Left$(newfiletype.Extension, 1) <> "." Then newfiletype.Extension = "." & newfiletype.Extension

    '        RegCreateKey(HKEY_CLASSES_ROOT, newfiletype.Extension, Result)
    '        ReturnValue = RegSetValueEx(Result, "", 0, REG_SZ, ByVal newfiletype.ProperName, LenB(StrConv(newfiletype.ProperName, vbFromUnicode)))

    '        ' Set up content type
    '        If newfiletype.ContentType <> "" Then
    '            ReturnValue = RegSetValueEx(Result, "Content Type", 0, REG_SZ, ByVal CStr(newfiletype.ContentType), LenB(StrConv(newfiletype.ContentType, vbFromUnicode)))
    '    End If

    '        RegCreateKey HKEY_CLASSES_ROOT, newfiletype.ProperName, Result

    '    If Not IconString = ",0" Then
    '            RegCreateKey Result, "DefaultIcon", Result2 'Create The Key of "ProperNameDefaultIcon"
    '            ReturnValue = RegSetValueEx(Result2, "", 0, REG_SZ, ByVal IconString, LenB(StrConv(IconString, vbFromUnicode)))
    '            'Set The Default Value for the Key
    '        End If

    '        ReturnValue = RegSetValueEx(Result, "", 0, REG_SZ, ByVal newfiletype.FullName, LenB(StrConv(newfiletype.FullName, vbFromUnicode)))
    '        RegCreateKey Result, ByVal "Shell", ResultX

    '    ' Create neccessary subkeys for each command
    '        For cmdloop = 1 To newfiletype.Commands.Captions.Count
    '            RegCreateKey ResultX, ByVal _
    '        newfiletype.Commands.Captions(cmdloop), Result
    '        RegCreateKey Result, ByVal "Command", Result2
    '        Dim CurrentCommand$
    '            CurrentCommand = newfiletype.Commands.Commands(cmdloop)
    '            ReturnValue = RegSetValueEx(Result2, "", 0, REG_SZ, ByVal CurrentCommand$, LenB(StrConv(CurrentCommand$, vbFromUnicode)))
    '            RegCloseKey Result
    '        RegCloseKey Result2
    '    Next

    '        RegCloseKey Result2
    'End Sub








    '    Public Function oprogram(ProperName As String, FullName As String, ContentType As String, Extension As String, Captions As String, Commands As String, IconPath As String, IconIndex As Long)

    '        Dim myfiletype As filetype

    '        myfiletype.ProperName = ProperName
    '        myfiletype.FullName = FullName
    '        myfiletype.ContentType = ContentType
    '        myfiletype.Extension = Extension
    '        myfiletype.Commands.Captions.Add Captions
    'myfiletype.Commands.Commands.Add Commands
    'myfiletype.IconPath = IconPath
    '        myfiletype.IconIndex = IconIndex
    '        CreateExtension myfiletype

    'End Function
End Module
