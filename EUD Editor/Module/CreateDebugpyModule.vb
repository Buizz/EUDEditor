Imports System.IO

Module CreateDebugpyModule
    Public Sub CreateDebugpy()
        Dim fileCreator As New FileStream(My.Application.Info.DirectoryPath & "\Data\eudplibdata\" & "EUDEditorDebug.py", FileMode.Create)
        Dim StreamWriter = New StreamWriter(fileCreator)




        StreamWriter.Write(Replace(My.Resources.EUDEditorDEBUG, "STRINGDATA", StringToBinary(ProjectSet.OutputMap, ",")))


        StreamWriter.Close()
        fileCreator.Close()
    End Sub

    Public Function StringToBinary(ByVal Text As String, Optional ByVal Separator As String = " ") As String
        Dim oReturn As New System.Text.StringBuilder
        For Each Character As Byte In System.Text.ASCIIEncoding.UTF8.GetBytes(Text)

            oReturn.Append(Convert.ToString(Character, 10))
            oReturn.Append(Separator)
        Next
        oReturn.Remove(oReturn.Length - 1, 1)

        Return oReturn.ToString
    End Function


    Public Sub DeledtDebugpy()
        Try
            My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\Data\eudplibdata\" & "EUDEditorDebug.py")
        Catch ex As Exception

        End Try
    End Sub
End Module
