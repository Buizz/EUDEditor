Public Class FoudlerNamedialog
    Private Sub FoudlerNamedialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If TextBox1.Text <> "" Then
            Button5.Enabled = True
        Else
            Button5.Enabled = False
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text <> "" Then
            Button5.Enabled = True
        Else
            Button5.Enabled = False
        End If
    End Sub
End Class