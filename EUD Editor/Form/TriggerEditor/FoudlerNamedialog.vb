Public Class FoudlerNamedialog
    Private Sub FoudlerNamedialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)
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
        If CheckBox1.Checked = False Then
            TextBox2.Text = TextBox1.Text
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        TextBox2.Enabled = CheckBox1.Checked
    End Sub
End Class