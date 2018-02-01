Public Class GRPFormUseIndex
    Public grpname As String

    Private Sub GRPFormUseIndex_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        TextBox1.Clear()

        For i = 0 To GRPEditorUsingindexDATA.Count - 1
            If GRPEditorUsingindexDATA(i) = grpname Then
                TextBox1.AppendText(i & ", ")
            End If
        Next
    End Sub


    Private Sub GRPFormUseIndex_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        Dim text As String = "," & Replace(TextBox1.Text, " ", "") & ","
        For i = 0 To GRPEditorUsingindexDATA.Count - 1


            If InStr(text, "," & i & ",") = 0 Then
                If GRPEditorUsingindexDATA(i) = grpname Then
                    GRPEditorUsingindexDATA(i) = ""
                End If
            Else
                GRPEditorUsingindexDATA(i) = grpname
            End If
        Next

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Close()
    End Sub
End Class