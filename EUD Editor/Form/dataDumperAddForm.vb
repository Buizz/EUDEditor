Imports System.IO

Public Class dataDumperAddForm
    Public datadumperstring As String = ""


    Private Filepath As String
    Private insertType As Byte
    Private Fileptr As String

    Private Sub CheckOk()
        If Filepath <> "" And Fileptr <> "" Then
            Select Case insertType
                Case 1
                    datadumperstring = Filepath.Replace(":", "\:") & " : " & Fileptr & ", copy"
                Case 2
                    datadumperstring = Filepath.Replace(":", "\:") & " : " & Fileptr & ", unpatchable"
                Case Else

                    datadumperstring = Filepath.Replace(":", "\:") & " : " & Fileptr
            End Select
            Button5.Enabled = True
        Else
            Button5.Enabled = False
        End If
    End Sub

    Dim loadcomp As Boolean
    Private Sub dataDumperAddForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        loadcomp = True
        LoadData()
        If datadumperstring = "" Then
            Filepath = ""
            insertType = 0
            Fileptr = ""
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox2.Enabled = True
            ListBox2.SelectedIndex = 0
            ListBox1.SelectedIndex = 0
        Else
            TextBox2.Clear()
            Filepath = datadumperstring.Split(" : ")(0).Replace("\:", ":").Trim
            TextBox1.Text = Filepath
            If datadumperstring.Split(" : ").Count = 3 Then
                Fileptr = datadumperstring.Split(" : ")(2).Trim()
                ListBox2.SelectedIndex = 0
                insertType = 0
            Else
                Fileptr = datadumperstring.Split(" : ")(2).Replace(",", "").Trim()


                Select Case datadumperstring.Split(" : ")(3).Trim
                    Case "copy"
                        ListBox2.SelectedIndex = 1
                        insertType = 1
                    Case "unpatchable"
                        ListBox2.SelectedIndex = 2
                        insertType = 2
                End Select

            End If
            For i = 0 To ListBox1.Items.Count - 1
                If Mid(ListBox1.Items(i), 1, 8).Trim = Fileptr Then
                    ListBox1.SelectedIndex = i
                    TextBox2.Enabled = False
                    TextBox2.Clear()
                    Exit For
                End If
            Next
            If ListBox1.SelectedIndex = -1 Then
                ListBox1.SelectedIndex = 0
                TextBox2.Text = Fileptr
                TextBox2.Enabled = True
            End If
            '
            'datadumperstring.Split(" : ")(3).Trim()



            CheckOk()
        End If

        loadcomp = False
    End Sub


    Private Sub LoadData()
        Dim filename As String = My.Application.Info.DirectoryPath & "\Data\Pointer.txt"
        Dim filestream As New FileStream(filename, FileMode.Open)
        Dim streamreader As New StreamReader(filestream)

        Dim text() As String = streamreader.ReadToEnd.Split(vbCrLf)

        ListBox1.Items.Clear()
        ListBox1.Items.Add("직접 입력")

        For i = 0 To text.Count - 1
            If text(i) <> "" Then
                Try
                    ListBox1.Items.Add(text(i).Split(",")(0).Trim & " " & text(i).Split(",")(1).Trim)
                Catch ex As Exception

                End Try
            End If
        Next



        streamreader.Close()
        filestream.Close()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If loadcomp = False Then
            If ListBox1.SelectedIndex = 0 Then
                TextBox2.Enabled = True
            Else
                Fileptr = Mid(ListBox1.Items(ListBox1.SelectedIndex), 1, 8).Trim
                TextBox2.Enabled = False
            End If
            CheckOk()
        End If

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If loadcomp = False Then
            insertType = ListBox2.SelectedIndex
            CheckOk()
        End If

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If loadcomp = False Then
            If ListBox1.SelectedIndex = 0 Then
                Fileptr = TextBox2.Text
            End If
            CheckOk()
        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim dialog As DialogResult
        dialog = OpenFileDialog1.ShowDialog()
        If dialog = DialogResult.OK Then
            Filepath = OpenFileDialog1.FileName
            TextBox1.Text = OpenFileDialog1.FileName
        End If
        CheckOk()
    End Sub
End Class