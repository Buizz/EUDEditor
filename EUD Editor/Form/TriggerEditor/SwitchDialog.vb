Public Class SwitchDialog
    Public _varele As Element

    Private Function GetVariablesWithoutoverlap() As List(Of String)
        Dim returnValues As New List(Of String)

        Dim values As New List(Of String)
        If GlobalVar.GetElementsCount <> 0 Then
            values.AddRange(GlobalVar.GetElementList(GlobalVar.GetElementsCount - 1).GetVariables(Nothing))
        End If

        values.AddRange(_varele.GetVariables(Nothing))

        For i = 0 To values.Count - 1
            If returnValues.Contains(values(i)) = False Then
                returnValues.Add(values(i))
            End If
        Next

        Return returnValues
    End Function


    Private Sub SwitchDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)


        EasyCompletionComboBox1.Items.Clear()
        EasyCompletionComboBox1.Items.AddRange(GetVariablesWithoutoverlap.ToArray)

        If EasyCompletionComboBox1.Items.Contains(TextBox1.Text) Then
            RadioButton1.Checked = True
            TextBox1.Text = ""
        Else
            RadioButton2.Checked = True
        End If
    End Sub
    Private Sub SwitchDialog_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        If EasyCompletionComboBox1.Enabled = True Then
            TextBox1.Text = EasyCompletionComboBox1.SelectedItem
        End If
    End Sub
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            EasyCompletionComboBox1.Enabled = True
            TextBox1.Enabled = False
        Else
            EasyCompletionComboBox1.Enabled = False
            TextBox1.Enabled = True
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            TextBox1.Enabled = True
            EasyCompletionComboBox1.Enabled = False
        Else
            TextBox1.Enabled = False
            EasyCompletionComboBox1.Enabled = True
        End If
    End Sub

End Class