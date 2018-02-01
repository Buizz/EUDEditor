Imports System.IO

Public Class MPQForm
    Private Sub MPQForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        If MPQlib.ReadListfile(ListBox1) = False Then
            MsgBox("정상적인 맵 파일이 아닙니다." & vbCrLf & "1. 프로텍트가 걸린 맵." & vbCrLf & "2. 정상적이지 않은 맵." & vbCrLf & "3. 현재 실행 중인 맵.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            Me.Close()
        End If
    End Sub




    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex = -1 Then
            Button3.Enabled = False
            Button2.Enabled = False
        Else
            'My.Computer.Audio.Play(ReadFile(ListBox1.SelectedItem), AudioPlayMode.Background)

            Button3.Enabled = True
            Button2.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim temp As Integer = ListBox1.SelectedIndex
        MPQlib.RemoveFile(ListBox1.SelectedItem)
        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)

        If ListBox1.Items.Count <> 0 Then
            If ListBox1.Items.Count > temp Then
                ListBox1.SelectedIndex = temp
            Else
                ListBox1.SelectedIndex = ListBox1.Items.Count - 1
            End If
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim inputtext As String
        inputtext = InputBox("변경할 이름을 입력하세요.", "EUD Editor", ListBox1.SelectedItem)
        If inputtext <> "" Then
            Rename(ListBox1.SelectedItem, inputtext)
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            ListBox1.Items.Add(inputtext)
        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dialog As DialogResult
        Dim infliename As String = ""
        dialog = OpenFileDialog1.ShowDialog

        If dialog = DialogResult.OK Then
            If OpenFileDialog1.FilterIndex = 1 Then
                Dim extension As String = Mid(OpenFileDialog1.FileName, OpenFileDialog1.FileName.Count - 2)



                Select Case extension.ToLower
                    Case "wav"
                        MPQForm_ListForm.Listtype = 0
                    Case "smk"
                        MPQForm_ListForm.Listtype = 1
                    Case "bin"
                        MPQForm_ListForm.Listtype = 2
                End Select
                MPQForm_ListForm.ShowDialog()

                If MPQForm_ListForm.okay = True Then
                    infliename = MPQForm_ListForm.ListValue

                    Select Case extension.ToLower
                        Case "wav"
                            MPQlib.AddFileSound(OpenFileDialog1.FileName, infliename)
                        Case "smk"
                            MPQlib.AddFile(OpenFileDialog1.FileName, infliename)
                        Case "bin"
                            MPQlib.AddFile(OpenFileDialog1.FileName, infliename) 'MPQForm_ListForm.Listtype = 2
                    End Select

                    If ListBox1.Items.Contains(MPQForm_ListForm.ListValue) = False Then
                        ListBox1.Items.Add(infliename)
                    End If
                End If
            Else
                Dim inputtext As String
                inputtext = InputBox("이름을 입력하세요.", "EUD Editor", ListBox1.SelectedItem)
                If inputtext <> "" Then


                    MPQlib.AddFile(OpenFileDialog1.FileName, inputtext)

                    If ListBox1.Items.Contains(MPQForm_ListForm.ListValue) = False Then
                        ListBox1.Items.Add(inputtext)
                    End If
                End If
            End If
        End If
    End Sub
End Class