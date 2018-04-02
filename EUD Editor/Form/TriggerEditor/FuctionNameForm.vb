Public Class FuctionNameForm

    Private Sub CreateValForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)
        Button5.Enabled = True
        If TextBox1.Text <> "" Then
            For i = 0 To functions.GetElementsCount - 1
                If TextBox1.Text = functions.GetElementList(i).Values(0) Then
                    If functions.GetElementList(i).Values(1) = CheckBox1.Checked Then
                        Button5.Enabled = False
                        Exit For
                    End If
                End If
            Next
        Else
            Button5.Enabled = False
        End If


    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox1.Text = TextBox1.Text.Replace(" ", "_")
        If TextBox1.Text <> "" Then
            Button5.Enabled = True
            For i = 0 To functions.GetElementsCount - 1
                If TextBox1.Text = functions.GetElementList(i).Values(0) Then
                    If functions.GetElementList(i).Values(1) = CheckBox1.Checked Then
                        Button5.Enabled = False
                        Exit For
                    End If
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
        If TextBox1.Text <> "" Then
            Button5.Enabled = True
            For i = 0 To functions.GetElementsCount - 1
                If TextBox1.Text = functions.GetElementList(i).Values(0) Then
                    If functions.GetElementList(i).Values(1) = CheckBox1.Checked Then
                        Button5.Enabled = False
                        Exit For
                    End If
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