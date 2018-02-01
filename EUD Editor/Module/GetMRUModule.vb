Imports System.IO

Module GetMRUModule

    'Public Function GetMostRecentDocs(fileSpec As String) As List(Of String)


    '    Dim recentFiles As List(Of String) = New List(Of String)

    '    Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.Recent)

    '    Dim di As DirectoryInfo = New DirectoryInfo(path)
    '    Dim files() As FileInfo = di.GetFiles(fileSpec + ".lnk")
    '    .OrderByDescending(fi >= fi.LastWriteTimeUtc)
    '    .ToList()
    '    If (files.Count < 1) Then
    '        Return recentFiles
    '    End If


    '    Dim script As Dynamic = ReflectionUtils.CreateComInstance("Wscript.Shell");

    'foreach(var file In files)
    '{
    '    Dynamic sc = script.CreateShortcut(File.FullName);
    '    recentFiles.Add(sc.TargetPath);
    '        Marshal.FinalReleaseComObject(sc);
    '    }
    '    Marshal.FinalReleaseComObject(script);

    '    Return recentFiles
    'End Function
End Module
'Public Static List<String> GetMostRecentDocs(String fileSpec)
'{
'    var recentFiles = New List < String > ();

'    var path = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

'    var di = New DirectoryInfo(path);
'    var files = di.GetFiles(fileSpec + ".lnk")
'.OrderByDescending(fi >= fi.LastWriteTimeUtc)
'.ToList();
'    If (files.Count < 1)
'Return recentFiles;

'    dynamic script = ReflectionUtils.CreateComInstance("Wscript.Shell");

'    foreach(var file In files)
'    {
'        dynamic sc = script.CreateShortcut(File.FullName);
'        recentFiles.Add(sc.TargetPath);
'        Marshal.FinalReleaseComObject(sc);
'    }
'    Marshal.FinalReleaseComObject(script);

'    Return recentFiles;
'}