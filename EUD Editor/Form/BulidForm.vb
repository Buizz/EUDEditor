Public Class BulidForm
    Dim process As New Process
    Dim startInfo As New ProcessStartInfo
    Public isotherWindows As Boolean
    Private Sub BulidForm_close(sender As Object, e As EventArgs) Handles MyBase.Closed
        If process.HasExited = False Then
            process.Close()
        End If
    End Sub



    Public Sub CompileStart()
        RichTextBox1.Text = ""
        RichTextBox2.Text = "빌드 중입니다."

        Dim filename As String = My.Application.Info.DirectoryPath & "\Data\eudplibdata\EUDEditor.eds"

        startInfo.FileName = ProgramSet.euddraftDirec
        startInfo.Arguments = """" & filename & """"

        startInfo.RedirectStandardOutput = True
        startInfo.RedirectStandardError = True
        startInfo.RedirectStandardInput = True
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        startInfo.CreateNoWindow = True

        startInfo.UseShellExecute = False



        process.StartInfo = startInfo


        ''process.
        Try
            process.Start() ' 여기서 프로그램이 실행됩니다.
        Catch ex As System.ComponentModel.Win32Exception
            MsgBox("euddraft실행 파일이 누락되었습니다.! 다시 설정해 주세요.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            SettingForm.ShowDialog()
            Exit Sub
        End Try

        Timer1.Enabled = True
        'Process.WaitForExit()
    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        RichTextBox1.Text = RichTextBox1.Text & process.StandardOutput.ReadToEnd()
        Dim Errormsg As String = process.StandardError.ReadToEnd()

        If process.HasExited Then
            Timer1.Enabled = False
            If InStr(Errormsg, "zipimport.ZipImportError: can't decompress data; zlib not available") <> 0 Then
                CompileStart()
            ElseIf Errormsg <> "" Then
                '에러
                'CompileStart()
                '에러문구 출력

                RichTextBox2.Text = "빌드에 실패했습니다. 자세한 상황은 좌측 상단의 로그를 참고하세요." & vbCrLf & Errormsg
                Me.Activate()
                MsgBox(RichTextBox2.Text, MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)

            Else
                Me.Hide()
            End If
        End If
    End Sub
End Class