Public Class CreateValForm

    Dim lastval As Long
    Private Sub CreateValForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lastval = NumericUpDown1.Value
        Button5.Enabled = True
        If TextBox1.Text <> "" Then
            For i = 0 To GlobalVar.GetElementsCount - 1
                If TextBox1.Text = GlobalVar.GetElementList(i).Values(0) Then
                    Button5.Enabled = False
                    Exit For
                End If
            Next
        Else
            Button5.Enabled = False
        End If

        If EasyCompletionComboBox1.Visible = True Then
            CheckBox1.Visible = False
        Else
            CheckBox1.Visible = True
            If CheckBox1.Checked = True Then
                NumericUpDown1.Enabled = False
            Else
                NumericUpDown1.Enabled = True
            End If
        End If
    End Sub


    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox1.Text = TextBox1.Text.Replace(" ", "_")
        If TextBox1.Text <> "" Then
            Button5.Enabled = True
            For i = 0 To GlobalVar.GetElementsCount - 1
                If TextBox1.Text = GlobalVar.GetElementList(i).Values(0) Then
                    If lastval = NumericUpDown1.Value Then
                        Button5.Enabled = False
                    End If
                    Exit For
                End If
            Next
            Dim isvla As Byte
            Try
                isvla = CByte(Mid(TextBox1.Text, 1, 1))
                Button5.Enabled = False
            Catch ex As Exception
            End Try
        Else
            Button5.Enabled = False
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            NumericUpDown1.Enabled = False
        Else
            NumericUpDown1.Enabled = True
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If TextBox1.Text <> "" Then
            Button5.Enabled = True
            For i = 0 To GlobalVar.GetElementsCount - 1
                If TextBox1.Text = GlobalVar.GetElementList(i).Values(0) Then
                    If lastval = NumericUpDown1.Value Then
                        Button5.Enabled = False
                    End If
                    Exit For
                End If
            Next
            Dim isvla As Byte
            Try
                isvla = CByte(Mid(TextBox1.Text, 1, 1))
                Button5.Enabled = False
            Catch ex As Exception
            End Try
        Else
            Button5.Enabled = False
        End If
    End Sub
End Class