Public Class SoundPlayerForm
    Private Function getPureName(str As String) As String
        Return str.Split("\").Last
    End Function

    Private Sub SoundPlayerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        ListBox1.Items.Clear()

        For i = 0 To Soundlist.Count - 1
            ListBox1.Items.Add(getPureName(Soundlist(i)))
        Next
        'ListBox1.Items.AddRange(Soundlist.ToArray)

        Try
            ComboBox1.SelectedIndex = Soundinterval - 1
        Catch ex As Exception
            ComboBox1.SelectedIndex = 3
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Soundinterval = ComboBox1.SelectedIndex + 1
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            If Soundlist.Contains(OpenFileDialog1.FileName) Then
                MsgBox("이미 존재하는 파일입니다.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            Else
                Soundlist.Add(OpenFileDialog1.FileName)
                ListBox1.Items.Add(getPureName(OpenFileDialog1.FileName))
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim lastselect As Integer = ListBox1.SelectedIndex
        Soundlist.RemoveAt(ListBox1.SelectedIndex)
        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)

        Try
            ListBox1.SelectedIndex = lastselect
        Catch ex As Exception
            ListBox1.SelectedIndex = ListBox1.Items.Count - 1
        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex <> -1 Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        BGMPlayerdialog.ShowDialog()
    End Sub
End Class