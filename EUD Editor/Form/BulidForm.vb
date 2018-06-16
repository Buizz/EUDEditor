Public Class BulidForm
    Dim process As New Process
    Dim startInfo As New ProcessStartInfo
    Public isotherWindows As Boolean
    Private Sub BulidForm_close(sender As Object, e As EventArgs) Handles MyBase.Closed
        If process.HasExited = False Then
            process.Close()
        End If
        DeledtDebugpy()
        count = 0
    End Sub

    Dim base As String
    Dim Errormsg As String = ""
    Public Sub CompileStart(basefolder As String)
        base = basefolder
        Errormsg = ""
        RichTextBox1.Text = ""
        RichTextBox2.Text = Lan.GetMsgText("build")

        Dim filename As String = basefolder & "\eudplibdata\EUDEditor.eds"

        startInfo.FileName = ProgramSet.euddraftDirec
        startInfo.Arguments = """" & filename & """"

        'startInfo.StandardOutputEncoding = System.Text.Encoding.ASCII 'GetEncoding("ks_c_5601-1987")
        'startInfo.StandardErrorEncoding = System.Text.Encoding.ASCII 'GetEncoding("ks_c_5601-1987")

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
            MsgBox(Lan.GetText("Msgbox", "neeuddraft"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            SettingForm.ShowDialog()
            Exit Sub
        End Try

        If BackgroundWorker1.IsBusy = False Then
            BackgroundWorker1.RunWorkerAsync()
        End If
        'Process.WaitForExit()
    End Sub



    Dim count As Integer

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        While (True)
            process.StandardInput.Write(vbCrLf)
            count += 1
            Errormsg = Errormsg & process.StandardError.ReadToEnd()
            'Me.Text = count & "번째 시도 중"
            RichTextBox1.Text = RichTextBox1.Text & process.StandardOutput.ReadToEnd()
            If process.HasExited Then
                If InStr(Errormsg, "zipimport.ZipImportError: can't decompress data; zlib not available") <> 0 Then
                    'RichTextBox2.Text = "재시도 합니다."
                    'Me.Text = count & "번째 재시도 합니다"
                    CompileStart(base)
                ElseIf Errormsg <> "" Or InStr(RichTextBox1.Text, "[Error]") <> 0 Then
                    '에러
                    'CompileStart()
                    '에러문구 출력

                    TEErrorText = Errormsg
                    TEErrorText2 = RichTextBox1.Text

                    RichTextBox2.Text = Lan.GetMsgText("buildError") & vbCrLf & Errormsg
                    Me.Activate()
                    MsgBox(RichTextBox2.Text, MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                    count = 0
                    Exit Sub
                Else
                    My.Computer.Audio.Play(My.Resources.successBulid, AudioPlayMode.Background)
                    Me.Hide()
                    count = 0
                    Exit Sub
                End If
            End If
            'Me.Text = count & "번째 시도 완료"
        End While
    End Sub
End Class