Imports System.Text.RegularExpressions

Public Class CTextEditor
    Public _varele As Element

    Dim normalsize As New Size(646, 529) '480 382
    Dim widesize As New Size(858, 529)

    '$로 감싸기

    Dim lastcolor As Color
    Public RawText As String
    Public realText As String

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Me.Size = widesize
            PictureBox2.Image = My.Resources.ConsoleWide
            'MsgBox(TextBox1.Size.ToString)
        Else
            Me.Size = normalsize
            PictureBox2.Image = My.Resources.Console
            'MsgBox(TextBox1.Size.ToString)
        End If
    End Sub



    Private Function LoadCodeDecoder(mainstr As String)
        Dim returnstr As String = mainstr



        Return returnstr
    End Function

    Private Function SaveCodeParser(mainstr As String)
        Dim returnstr As String = mainstr
        '$를 변환시킨다



        Return returnstr
    End Function

    Private Sub TextEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        RawText = RawText.Replace("\n", vbCrLf)
        RawText = RawText.Replace("\""", """")



        RawText = LoadCodeDecoder(RawText)


        For i = 0 To 31
            Dim code As String = Hex(i)


            If code.Count = 1 Then
                code = "0" & code
            End If

            RawText = RawText.Replace("\x" & code, "<" & code & ">")
        Next

        TextBox1.Text = RawText
        Textrefresh()
        Textrefresh()
    End Sub

    Private Sub TextEditor_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing

    End Sub


    Private Sub TextBox1_KeyPress(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.Control = True And e.KeyCode = Keys.A Then
            TextBox1.SelectAll()
        End If
    End Sub


    Private Sub Textrefresh()
        If TextBox1.Lines.Count > 11 Then
            TextBox1.Text = lasttext.Text
        End If

        RawText = TextBox1.Text

        For i = 0 To TextBox1.Lines.Count - 1
            Try
                If lasttext.Lines(i) <> TextBox1.Lines(i) Then
                    lasttext.Text = TextBox1.Text
                End If
            Catch ex As Exception
                lasttext.Text = TextBox1.Text
            End Try

        Next




        DrawPreview()
    End Sub


    Public lasttext As New TextBox
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Textrefresh()
    End Sub

    Private Sub DrawPreview()
        PreViewer.Text = vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf


        Try
            Dim lines As String() = PreViewer.Lines

            For i = 0 To TextBox1.Lines.Count - 1
                lines(11 - TextBox1.Lines.Count + i) = "　" & TextBox1.Lines(i) & "　"
            Next

            PreViewer.Lines = lines
        Catch ex As Exception
        End Try


        MakeColor()
    End Sub



    Private Function GetstrColor(opcode As Byte)
        Dim stringcolor As Color = Color.FromArgb(184, 184, 232)
        Select Case opcode
            Case 1
                stringcolor = Color.FromArgb(184, 184, 232)
            Case 2
                stringcolor = Color.FromArgb(184, 184, 232)
            Case 3
                stringcolor = Color.FromArgb(220, 220, 60)
            Case 4
                stringcolor = Color.FromArgb(255, 255, 255)
            Case 5
                stringcolor = Color.FromArgb(132, 116, 116)
            Case 6
                stringcolor = Color.FromArgb(200, 24, 24)
            Case 7
                stringcolor = Color.FromArgb(16, 252, 24)
            Case 8
                stringcolor = Color.FromArgb(244, 4, 4)
            Case 14
                stringcolor = Color.FromArgb(12, 74, 204)
            Case 15
                stringcolor = Color.FromArgb(44, 140, 148)
            Case 16
                stringcolor = Color.FromArgb(136, 64, 156)
            Case 17
                stringcolor = Color.FromArgb(248, 140, 20)
            Case 21
                stringcolor = Color.FromArgb(112, 48, 20)
            Case 22
                stringcolor = Color.FromArgb(204, 224, 208)
            Case 23
                stringcolor = Color.FromArgb(252, 252, 56)
            Case 24
                stringcolor = Color.FromArgb(8, 128, 8)
            Case 25
                stringcolor = Color.FromArgb(252, 252, 8)
            Case 26
                stringcolor = Color.FromArgb(184, 184, 232)
            Case 27
                stringcolor = Color.FromArgb(236, 196, 176)
            Case 28
                stringcolor = Color.FromArgb(64, 104, 212)
            Case 29
                stringcolor = Color.FromArgb(116, 164, 124)
            Case 30
                stringcolor = Color.FromArgb(144, 144, 184)
            Case 31
                stringcolor = Color.FromArgb(0, 228, 252)
        End Select
        Return stringcolor
    End Function
    Private Sub MakeColor()
        Dim str As String
        str = PreViewer.Text

        Dim deletist As New List(Of String)
        Dim colorLocation As New List(Of Integer)
        Dim colorlen As New List(Of Integer)
        Dim colors As New List(Of Byte)
        Dim linesType As New List(Of String)
        Dim removerChars As Integer = 0
        While (GetCheckExist(str) = True)
            Dim isexception As Boolean = False
            Dim opcode As Byte
            Try
                opcode = Val("&H" & GetChar(str).Replace("<", "").Replace(">", ""))
            Catch ex As Exception
                removerChars += GetChar(str).Count
                str = Mid(str, GetChar(str).Count)
                isexception = True
            End Try
            Select Case opcode
                Case 1 To 8
                Case 14 To 17
                Case 18 To 19
                Case 21 To 25
                Case 27 To 31
                Case Else
                    removerChars += GetChar(str).Count
                    str = Mid(str, GetChar(str).Count)
                    isexception = True
            End Select

            If isexception = False Then
                deletist.Add(GetChar(str))
                'MsgBox(Getindex(str))
                removerChars += Getindex(str)
                str = Mid(str, Getindex(str))

                colorLocation.Add(Getindex(str) + removerChars - 1)
                colorlen.Add(GetLF(str) - 1 - GetChar(str).Count)


                colors.Add(opcode)


                removerChars += -1
                str = Mid(str, GetChar(str).Count + 1)
            End If
        End While

        'For i = 0 To PreViewer.Lines.Count - 1
        '    If PreViewer.Lines(i).Contains("<12>") Then
        '        linesType.Add("R")
        '    ElseIf PreViewer.Lines(i).Contains("<13>") Then
        '        linesType.Add("C")
        '    Else
        '        linesType.Add("N")
        '    End If
        'Next


        For i = 0 To deletist.Count - 1
            PreViewer.Text = PreViewer.Text.Replace(deletist(i), "")
        Next


        realText = TextBox1.Text
        For i = 0 To deletist.Count - 1
            Dim codestring As String = deletist(i).Replace("<", "").Replace(">", "")

            If codestring.Count = 1 Then
                codestring = "0" & codestring
            End If

            realText = realText.Replace(deletist(i), "\x" & codestring)

            str = PreViewer.Text

            Dim stringcolor As Color = Color.FromArgb(184, 184, 232)
            Select Case colors(i)
                Case 1 To 8
                    stringcolor = GetstrColor(colors(i))
                Case 14 To 17
                    stringcolor = GetstrColor(colors(i))
                Case 21 To 25
                    stringcolor = GetstrColor(colors(i))
                Case 27 To 31
                    stringcolor = GetstrColor(colors(i))
            End Select

            str = Mid(str, colorLocation(i) + deletist(i).Count)

            If GetLF(str) <> -1 Then
                PreViewer.Select(colorLocation(i), GetLF(str) + deletist(i).Count)
            Else
                PreViewer.Select(colorLocation(i), str.Count + deletist(i).Count - 1)
            End If
            Select Case colors(i)
                Case 18
                    '오른쪽
                    PreViewer.SelectionAlignment = HorizontalAlignment.Right
                Case 19
                    '가운대
                    PreViewer.SelectionAlignment = HorizontalAlignment.Center
            End Select


            PreViewer.SelectionColor = stringcolor
        Next
        realText = realText.Replace(vbCrLf, "\n")
        realText = realText.Replace("""", "\""")
        realText = realText.Replace("\$", "$")
        realText = SaveCodeParser(realText)


    End Sub


    Private Function GetChar(str As String) As String
        Dim pattern As String = "<\w+>"
        Dim rgx As New Regex(pattern, RegexOptions.IgnoreCase)

        If rgx.Match(str).Success = True Then
            Return rgx.Match(str).Value
        End If
        Return rgx.Match(str).Index
    End Function

    Private Function Getindex(str As String) As Integer
        Dim pattern As String = "<\w+>"
        Dim rgx As New Regex(pattern, RegexOptions.IgnoreCase)


        Return rgx.Match(str).Index
    End Function
    Private Function GetLF(str As String) As Integer
        'Dim pattern As String = "\x0A"
        'Dim rgx As New Regex(pattern, RegexOptions.IgnoreCase)


        Return str.IndexOf(vbLf)
    End Function

    Private Function GetCheckExist(str As String) As Boolean
        Dim pattern As String = "<\w+>"
        Dim rgx As New Regex(pattern, RegexOptions.IgnoreCase)


        Return rgx.Match(str).Success
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddTectDialog._varele = _varele
        If AddTectDialog.ShowDialog() = DialogResult.OK Then
            TextBox1.SelectedText = AddTectDialog.returnstring
        End If
    End Sub
End Class