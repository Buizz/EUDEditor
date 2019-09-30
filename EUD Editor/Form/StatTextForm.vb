Imports System.IO
Imports System.Text.RegularExpressions


Public Class StatTextForm
    Public stringNum As Integer
    Public RawText As String
    Public OrginText As String
    Dim loadned As Boolean = False
    Dim lastcolor As Color

    Private Sub StatTextForm_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        If OrginText = RawText Then
            RawText = ""
        End If
    End Sub

    Private Sub StatTextForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox2.Items.Clear()
        Dim File As FileStream = New FileStream(My.Application.Info.DirectoryPath & "\Data\Langage\" & My.Settings.Langage & "\StatText.txt", FileMode.Open, FileAccess.Read)
        Dim Stream As StreamReader = New StreamReader(File, System.Text.Encoding.Default)

        ComboBox2.Items.AddRange(Stream.ReadToEnd.Split(vbCrLf))

        Stream.Close()
        File.Close()

        Lan.SetLangage(Me)




        loadned = False
        LoadFileimportable()

        OrginText = stat_txt(stringNum)

        DataGridView1.Rows.Clear()
        For i = 0 To stat_txt.Count - 1
            If stattextdic.ContainsKey(i) Then
                DataGridView1.Rows.Add(stat_txt(i), stattextdic(i))
                DataGridView1.Rows(i).Tag = i
            Else
                DataGridView1.Rows.Add(stat_txt(i), "")
                DataGridView1.Rows(i).Tag = i
            End If
        Next
        If ProjectSet.UsedSetting(8) = True Then
            For i = 0 To stattextdic.Count - 1
                stat_txt(stattextdic.Keys(i)) = stattextdic(stattextdic.Keys(i))
            Next
        End If

        DataGridView1.ClearSelection()

        DataGridView1.FirstDisplayedScrollingRowIndex = stringNum

        DataGridView1.Rows(stringNum).Selected = True


        loadned = True
        LoadText()
    End Sub

    Private Sub LoadText()
        loadned = False
        TextBox1.ResetText()
        RichTextBox2.ResetText()

        RawText = stat_txt(stringNum)

        TextBox1.Text = RawText
        CodeRead()
        loadned = True
    End Sub

    Private Function CheckStringlen() As Integer
        Dim pattern As String = "<\d+>"
        Dim rgx As New Regex(pattern, RegexOptions.IgnoreCase)



        Dim currentpos As Integer = 0
        Dim currentindex As Integer = 0

        While currentpos < RawText.Count

            If rgx.Match(Mid(RawText, currentpos + 1)).Success = True And rgx.Match(Mid(RawText, currentpos + 1)).Index = 0 Then
                currentpos += rgx.Match(Mid(RawText, currentpos + 1)).Value.Count
            Else
                'MsgBox(rgx.Match(RawText).Success & ", " & rgx.Match(RawText).Index)
                currentpos += 1
            End If



            currentindex += 1
        End While


        Return currentindex
    End Function
    Private Function GetChar(index As Integer) As String
        Dim pattern As String = "<\d+>"
        Dim rgx As New Regex(pattern, RegexOptions.IgnoreCase)



        Dim rstring As String = ""
        Dim currentpos As Integer = 0
        Dim currentindex As Integer = 0

        While currentindex <= index

            If rgx.Match(Mid(RawText, currentpos + 1)).Success = True And rgx.Match(Mid(RawText, currentpos + 1)).Index = 0 Then
                rstring = rgx.Match(Mid(RawText, currentpos + 1)).Value
                currentpos += rgx.Match(Mid(RawText, currentpos + 1)).Value.Count


            Else
                rstring = RawText(currentpos)
                currentpos += 1
            End If

            'MsgBox(currentindex & ", " & index)


            currentindex += 1
        End While


        Return rstring
    End Function
    Dim ishotkey As Boolean = False
    Private Sub CodeRead()
        loadned = False
        ishotkey = False
        'ComboBox1.SelectedIndex = 0
        'ComboBox2.SelectedIndex = 0

        RichTextBox2.ResetText()
        RichTextBox2.SuspendLayout()
        RichTextBox2.SelectionColor = Color.FromArgb(184, 184, 232)
        PictureBox2.Visible = False


        For i = 0 To CheckStringlen() - 1
            Dim pass As Boolean = False

            If i = 1 Then
                If GetChar(i).Length > 1 Then
                    Dim opcode As Integer = Replace(Replace(GetChar(i), "<", ""), ">", "")
                    Select Case opcode
                        Case 0
                            PictureBox2.Visible = False
                        Case 1
                            PictureBox2.Visible = True
                            PictureBox2.Image = My.Resources.Create
                        Case 2
                            PictureBox2.Visible = True
                            PictureBox2.Image = My.Resources.NUpgrade
                        Case 3
                            PictureBox2.Visible = True
                            PictureBox2.Image = My.Resources.SKill
                        Case 4
                            PictureBox2.Visible = True
                            PictureBox2.Image = My.Resources.Tech
                        Case 5
                            PictureBox2.Visible = True
                            PictureBox2.Image = My.Resources.modifi

                    End Select


                    Try
                        ComboBox2.SelectedIndex = opcode + 1
                    Catch ex As Exception
                        ComboBox2.SelectedIndex = -1
                    End Try

                    If GetChar(0).Length > 1 Then
                        Dim arc As Byte = Replace(Replace(GetChar(0), "<", ""), ">", "")
                        If arc = 27 Then
                            ComboBox1.SelectedIndex = 26 + 1
                        End If

                    Else
                        If 97 <= Asc(GetChar(0)) And Asc(GetChar(0)) <= 122 Then
                            ComboBox1.SelectedIndex = Asc(GetChar(0)) - 97 + 1
                        End If
                        '97 ~ 122

                        '65 ~ 90
                        If 65 <= Asc(GetChar(0)) And Asc(GetChar(0)) <= 90 Then
                            ComboBox1.SelectedIndex = Asc(GetChar(0)) - 65 + 1
                            loadned = False
                            RawText = GetChar(0).ToLower & Mid(RawText, 1 + GetChar(0).Length)
                            TextBox1.Text = RawText
                            loadned = True
                        End If
                    End If

                    ishotkey = True
                    pass = True
                    RichTextBox2.Clear()
                    RichTextBox2.SelectionColor = Color.FromArgb(184, 184, 232)
                Else
                    ComboBox1.SelectedIndex = 0
                    ComboBox2.SelectedIndex = 0
                    'RichTextBox2.AppendText(GetChar(0) & GetChar(1))
                    pass = False
                End If
            End If


            If pass = False Then
                If GetChar(i).Length < 2 Then
                    RichTextBox2.AppendText(GetChar(i))
                Else
                    Dim opcode As Integer = Replace(Replace(GetChar(i), "<", ""), ">", "")

                    '12 = 기본
                    '3 = 노랑
                    '4 = 하양
                    '5 = 회색(뒷 내용 다 회색됨)
                    '6 = 진한 빨강
                    '7 = 밝은 초록
                    '8 = 빨강
                    '9 = ?
                    '10 = 개행
                    '11 = 투명
                    '12 = 옆을 지운다
                    '13
                    '14 = 파랑
                    '15 = 초록(P3)
                    '16 = 퍼플
                    '17 = 주황
                    '18 =오른쪽
                    '19 = 가운대
                    '20 = 투명
                    '21 = 갈색
                    '22 = 하양
                    '23 = 노랑
                    '24 = 초록
                    '25 = P10
                    '26 = 시안
                    '27= P11
                    '28 = P12
                    '29 - GrayGeen
                    '30 = BlueGray
                    '31 = Turquoise
                    Select Case opcode
                        Case 0
                            Exit For
                        Case 1
                            RichTextBox2.SelectionColor = Color.FromArgb(184, 184, 232)
                        Case 2
                            RichTextBox2.SelectionColor = Color.FromArgb(184, 184, 232)
                        Case 3
                            RichTextBox2.SelectionColor = Color.FromArgb(220, 220, 60)
                        Case 4
                            RichTextBox2.SelectionColor = Color.FromArgb(255, 255, 255)
                        Case 5
                            RichTextBox2.SelectionColor = Color.FromArgb(132, 116, 116)
                        Case 6
                            RichTextBox2.SelectionColor = Color.FromArgb(200, 24, 24)
                        Case 7
                            RichTextBox2.SelectionColor = Color.FromArgb(16, 252, 24)
                        Case 8
                            RichTextBox2.SelectionColor = Color.FromArgb(244, 4, 4)
                        Case 14
                            RichTextBox2.SelectionColor = Color.FromArgb(12, 74, 204)
                        Case 15
                            RichTextBox2.SelectionColor = Color.FromArgb(44, 140, 148)
                        Case 16
                            RichTextBox2.SelectionColor = Color.FromArgb(136, 64, 156)
                        Case 17
                            RichTextBox2.SelectionColor = Color.FromArgb(248, 140, 20)
                        Case 21
                            RichTextBox2.SelectionColor = Color.FromArgb(112, 48, 20)
                        Case 22
                            RichTextBox2.SelectionColor = Color.FromArgb(204, 224, 208)
                        Case 23
                            RichTextBox2.SelectionColor = Color.FromArgb(252, 252, 56)
                        Case 24
                            RichTextBox2.SelectionColor = Color.FromArgb(8, 128, 8)
                        Case 25
                            RichTextBox2.SelectionColor = Color.FromArgb(252, 252, 8)
                        Case 26
                            RichTextBox2.SelectionColor = Color.FromArgb(184, 184, 232)
                        Case 27
                            RichTextBox2.SelectionColor = Color.FromArgb(236, 196, 176)
                        Case 28
                            RichTextBox2.SelectionColor = Color.FromArgb(64, 104, 212)
                        Case 29
                            RichTextBox2.SelectionColor = Color.FromArgb(116, 164, 124)
                        Case 30
                            RichTextBox2.SelectionColor = Color.FromArgb(144, 144, 184)
                        Case 31
                            RichTextBox2.SelectionColor = Color.FromArgb(0, 228, 252)
                        Case 10

                            lastcolor = RichTextBox2.SelectionColor
                            RichTextBox2.AppendText(vbCrLf)
                            RichTextBox2.SelectionColor = lastcolor

                    End Select
                End If
            End If
        Next


        PictureBox2.Location = New Point(0, RichTextBox2.Lines.Count * 16)

        RichTextBox2.ResumeLayout()
        loadned = True
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If loadned = True Then
            RichTextBox2.Text = ""


            Dim stringtemp As String = TextBox1.Text
            If InStr(TextBox1.Text.ToString, Chr(10)) <> 0 Then
                Dim j As Integer = TextBox1.SelectionStart
                TextBox1.Text = Replace(TextBox1.Text, vbCrLf, "<10>")
                TextBox1.SelectionStart = j + 2

                RawText = TextBox1.Text
            End If

            RawText = TextBox1.Text
            CodeRead()
        End If

    End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
    '    If ComboBox1.SelectedIndex = -1 Then
    '        If ishotkey = True Then
    '            loadned = False
    '            RawText = Mid(RawText, 1 + GetChar(0).Length + GetChar(1).Length)
    '            TextBox1.Text = RawText
    '            loadned = True
    '            ishotkey = False
    '        End If


    '    Else
    '        If RadioButton6.Checked = True Then
    '            RadioButton1.Checked = True

    '            RawText = "<" & ComboBox1.SelectedIndex & "><1>" & RawText
    '        Else
    '            loadned = False
    '            If ComboBox1.SelectedItem = "ESC" Then
    '                RawText = "<27>" & Mid(RawText, 1 + GetChar(0).Length)
    '            Else
    '                RawText = Chr(ComboBox1.SelectedIndex + 97) & Mid(RawText, 1 + GetChar(0).Length)
    '            End If
    '            TextBox1.Text = RawText
    '            loadned = True
    '        End If
    '    End If
    'End Sub



    Private Sub DataGridView1_CellContentClick(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If loadned = True Then
            stringNum = DataGridView1.SelectedRows(0).Tag
            LoadText()
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If loadned = True Then
            loadned = False
            Dim selectvalue As Integer = ComboBox2.SelectedIndex

            CodeRead()

            If selectvalue <> 0 Then
                If ishotkey = True Then '값 변경
                    TextBox1.Text = Chr(ComboBox1.SelectedIndex + 97 - 1) & "<" & selectvalue - 1 & ">" & Mid(TextBox1.Text, 1 + GetChar(0).Length + GetChar(1).Length)
                Else '값 추가
                    TextBox1.Text = "a<" & selectvalue - 1 & ">" & TextBox1.Text
                End If

            Else '사용 안함을 했을 경우.
                If ishotkey = True Then '값 변경
                    TextBox1.Text = Mid(TextBox1.Text, 1 + GetChar(0).Length + GetChar(1).Length)
                End If
            End If

            CodeRead()
            loadned = True
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If loadned = True Then
            loadned = False
            Dim selectvalue As Integer = ComboBox1.SelectedIndex

            CodeRead()

            If selectvalue <> 0 Then
                If selectvalue = 27 Then
                    If ishotkey = True Then '값 변경
                        TextBox1.Text = "<27><" & ComboBox2.SelectedIndex - 1 & ">" & Mid(TextBox1.Text, 1 + GetChar(0).Length + GetChar(1).Length)
                    Else '값 추가
                        TextBox1.Text = "<27><" & ComboBox2.SelectedIndex - 1 & ">" & TextBox1.Text
                    End If


                Else
                    If ishotkey = True Then '값 변경
                        TextBox1.Text = Chr(selectvalue + 97 - 1) & "<" & ComboBox2.SelectedIndex - 1 & ">" & Mid(TextBox1.Text, 1 + GetChar(0).Length + GetChar(1).Length)
                    Else '값 추가
                        TextBox1.Text = Chr(selectvalue + 97 - 1) & "<0>" & TextBox1.Text
                    End If
                End If

            Else '값을 사용안함으로 했을 경우
                If ishotkey = True Then '값 변경
                    TextBox1.Text = Mid(TextBox1.Text, 1 + GetChar(0).Length + GetChar(1).Length)
                End If
            End If

            CodeRead()
            loadned = True
        End If
    End Sub
End Class