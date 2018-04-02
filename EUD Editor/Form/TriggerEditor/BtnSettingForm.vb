Imports System.IO
Imports SergeUtils

Public Class BtnSettingForm
    Public RawBtnCode As String

    Private D_Pos As UShort
    Private D_icon As UShort
    Private D_con As UInteger
    Private D_act As UInteger
    Private D_conval As UShort
    Private D_actval As UShort
    Private D_enaStr As UShort
    Private D_disStr As UShort


    Private isload As Boolean = False



    Private Sub BtnSettingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        isload = False

        Try
            ComboBox4.Items.Clear()
            ComboBox4.Items.AddRange(CODE(12).ToArray)
            Loadstattxt()

            ComboBox6.Items.Clear()
            ComboBox7.Items.Clear()
            For i = 0 To conbtnFnc.Count - 1
                ComboBox6.Items.Add(conbtnFnc(i).Name)
            Next
            For i = 0 To actbtnFnc.Count - 1
                ComboBox7.Items.Add(actbtnFnc(i).Name)
            Next

            TextBox4.Text = RawBtnCode

            Dim Pos As Integer = 1
            D_Pos = extratext(2, Pos, TextBox4.Text)
            D_icon = extratext(2, Pos, TextBox4.Text)
            D_con = extratext(4, Pos, TextBox4.Text)
            D_act = extratext(4, Pos, TextBox4.Text)
            D_conval = extratext(2, Pos, TextBox4.Text)
            D_actval = extratext(2, Pos, TextBox4.Text)
            D_enaStr = extratext(2, Pos, TextBox4.Text)
            D_disStr = extratext(2, Pos, TextBox4.Text)
            btndataLoad()
        Catch ex As Exception
            Me.Close()
            isload = True
        End Try

        isload = True
    End Sub
    Private Sub BtnSettingForm_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        RawBtnCode = TextBox4.Text

    End Sub

    Public Sub Loadstattxt()
        LoadFileimportable()

        If ProjectSet.UsedSetting(8) = True Then
            For i = 0 To stattextdic.Count - 1
                stat_txt(stattextdic.Keys(i)) = stattextdic(stattextdic.Keys(i))
            Next

        End If

        ComboBox10.Items.Clear()
        ComboBox9.Items.Clear()
        ComboBox10.Items.Add("None")
        ComboBox10.Items.AddRange(stat_txt)
        ComboBox9.Items.Add("None")
        ComboBox9.Items.AddRange(stat_txt)
    End Sub

    Private Function extratext(size As Integer, ByRef ptr As Integer, str As String)
        Dim returnvalue As Integer

        For i = 0 To size - 1
            returnvalue += Mid(str, ptr + 3 * i, 3) * 256 ^ i
        Next


        ptr += size * 3
        Return returnvalue
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If isload = True Then
            isload = False

            Dim Pos As Integer = 1
            D_Pos = extratext(2, Pos, TextBox4.Text)
            D_icon = extratext(2, Pos, TextBox4.Text)
            D_con = extratext(4, Pos, TextBox4.Text)
            D_act = extratext(4, Pos, TextBox4.Text)
            D_conval = extratext(2, Pos, TextBox4.Text)
            D_actval = extratext(2, Pos, TextBox4.Text)
            D_enaStr = extratext(2, Pos, TextBox4.Text)
            D_disStr = extratext(2, Pos, TextBox4.Text)
            btndataLoad()

            isload = True
        End If

    End Sub


    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If isload = True Then
            isload = False
            Try
                D_Pos = TextBox3.Text
            Catch ex As Exception
                TextBox3.Text = 65535
                D_Pos = 65535
            End Try
            WriteText()
            isload = True
        End If
    End Sub
    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If isload = True Then
            isload = False
            Try
                D_icon = TextBox5.Text
                Try
                    ComboBox4.SelectedIndex = TextBox5.Text
                    PictureBox1.Image = DatEditForm.ICONILIST.Images(D_icon) '방어구 아이콘
                Catch ex As Exception
                    ComboBox4.SelectedIndex = -1
                    PictureBox1.Image = DatEditForm.ICONILIST.Images(4)
                End Try
            Catch ex As Exception
            End Try
            WriteText()


            isload = True
        End If
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If isload = True Then
            isload = False

            Try
                D_icon = ComboBox4.SelectedIndex 'TextBox5.Text
            Catch ex As Exception
                ComboBox4.SelectedIndex = -1
                D_icon = 65535
            End Try


            Try
                TextBox5.Text = ComboBox4.SelectedIndex
                PictureBox1.Image = DatEditForm.ICONILIST.Images(D_icon) '방어구 아이콘
            Catch ex As Exception
                ComboBox4.SelectedIndex = -1
                PictureBox1.Image = DatEditForm.ICONILIST.Images(4)
            End Try
            WriteText()

            isload = True

        End If
    End Sub



    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        If isload = True Then
            isload = False
            Try
                D_enaStr = TextBox10.Text
                Try
                    ComboBox10.SelectedIndex = TextBox10.Text
                Catch ex As Exception
                    ComboBox10.SelectedIndex = -1
                End Try
            Catch ex As Exception
            End Try


            WriteText()

            isload = True
        End If
    End Sub

    Private Sub ComboBox10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox10.SelectedIndexChanged
        If isload = True Then
            isload = False

            Try
                D_enaStr = ComboBox10.SelectedIndex 'TextBox5.Text
            Catch ex As Exception
                ComboBox10.SelectedIndex = -1
                D_enaStr = 65535
            End Try

            Try
                TextBox10.Text = ComboBox10.SelectedIndex
            Catch ex As Exception
                ComboBox10.SelectedIndex = -1
            End Try

            WriteText()

            isload = True
        End If
    End Sub




    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        If isload = True Then
            isload = False

            Try
                D_disStr = TextBox11.Text
                Try
                    ComboBox9.SelectedIndex = TextBox11.Text
                Catch ex As Exception
                    ComboBox9.SelectedIndex = -1
                End Try
            Catch ex As Exception
            End Try


            WriteText()

            isload = True
        End If
    End Sub

    Private Sub ComboBox9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox9.SelectedIndexChanged
        If isload = True Then
            isload = False

            Try
                D_disStr = ComboBox9.SelectedIndex 'TextBox5.Text
            Catch ex As Exception
                ComboBox9.SelectedIndex = -1
                D_disStr = 65535
            End Try

            Try
                TextBox11.Text = ComboBox9.SelectedIndex
            Catch ex As Exception
                ComboBox9.SelectedIndex = -1
            End Try

            WriteText()

            isload = True
        End If
    End Sub



    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        If isload = True Then
            isload = False
            Try
                D_con = conbtnFnc(TextBox7.Text).FucOffset
            Catch ex As Exception
            End Try

            Try
                ComboBox6.SelectedIndex = TextBox7.Text
            Catch ex As Exception
                ComboBox6.SelectedIndex = -1
            End Try

            WriteText()


            ComboBox5.Items.Clear()
            For i = 0 To conbtnFnc.Count - 1
                If conbtnFnc(i).FucOffset = D_con Then
                    If conbtnFnc(i).Code <> -1 Then
                        ComboBox5.Items.AddRange(CODE(conbtnFnc(i).Code).ToArray)
                        Try
                            ComboBox5.SelectedIndex = D_conval
                        Catch ex As Exception
                            ComboBox5.SelectedIndex = -1
                        End Try
                    End If
                    Exit For
                End If
            Next

            isload = True
        End If
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        If isload = True Then
            isload = False
            Try
                D_con = conbtnFnc(ComboBox6.SelectedIndex).FucOffset 'TextBox5.Text
            Catch ex As Exception
                'MsgBox("시발?")
            End Try

            Try
                TextBox7.Text = ComboBox6.SelectedIndex
            Catch ex As Exception
                ComboBox6.SelectedIndex = -1
            End Try

            WriteText()


            ComboBox5.Items.Clear()
            For i = 0 To conbtnFnc.Count - 1
                If conbtnFnc(i).FucOffset = D_con Then
                    If conbtnFnc(i).Code <> -1 Then
                        ComboBox5.Items.AddRange(CODE(conbtnFnc(i).Code).ToArray)
                        Try
                            ComboBox5.SelectedIndex = D_conval
                        Catch ex As Exception
                            ComboBox5.SelectedIndex = -1
                            ComboBox5.Text = ""
                        End Try
                    End If
                    Exit For
                End If
            Next

            isload = True
        End If
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        If isload = True Then
            isload = False
            Try
                D_act = actbtnFnc(TextBox9.Text).FucOffset
            Catch ex As Exception
            End Try

            Try
                ComboBox7.SelectedIndex = TextBox9.Text
            Catch ex As Exception
                ComboBox7.SelectedIndex = -1
            End Try

            WriteText()



            ComboBox8.Items.Clear()
            For i = 0 To actbtnFnc.Count - 1
                If actbtnFnc(i).FucOffset = D_act Then
                    If actbtnFnc(i).Code <> -1 Then
                        ComboBox8.Items.AddRange(CODE(actbtnFnc(i).Code).ToArray)

                        Try
                            ComboBox8.SelectedIndex = D_actval
                        Catch ex As Exception
                            ComboBox8.SelectedIndex = -1
                        End Try
                    End If
                    Exit For
                End If
            Next


            isload = True
        End If
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        If isload = True Then
            isload = False
            Try
                D_act = actbtnFnc(ComboBox7.SelectedIndex).FucOffset 'TextBox5.Text
            Catch ex As Exception
                'MsgBox("시발?")
            End Try


            Try
                TextBox9.Text = ComboBox7.SelectedIndex
            Catch ex As Exception
                ComboBox7.SelectedIndex = -1
            End Try

            WriteText()


            ComboBox8.Items.Clear()
            For i = 0 To actbtnFnc.Count - 1
                If actbtnFnc(i).FucOffset = D_act Then
                    If actbtnFnc(i).Code <> -1 Then
                        ComboBox8.Items.AddRange(CODE(actbtnFnc(i).Code).ToArray)

                        Try
                            ComboBox8.SelectedIndex = D_actval
                        Catch ex As Exception
                            ComboBox8.SelectedIndex = -1
                            ComboBox8.Text = ""
                        End Try
                    End If
                    Exit For
                End If
            Next

            isload = True
        End If
    End Sub




    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        If isload = True Then
            isload = False

            Try
                D_conval = TextBox6.Text
            Catch ex As Exception
                TextBox6.Text = 65535
                D_conval = 65535
            End Try

            Try
                ComboBox5.SelectedIndex = TextBox6.Text
            Catch ex As Exception
                ComboBox5.SelectedIndex = -1
            End Try

            WriteText()

            isload = True
        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        If isload = True Then
            isload = False
            Try
                D_conval = ComboBox5.SelectedIndex 'TextBox5.Text
            Catch ex As Exception
                ComboBox5.SelectedIndex = -1
                D_conval = 65535
            End Try

            Try
                TextBox6.Text = ComboBox5.SelectedIndex
            Catch ex As Exception
                ComboBox5.SelectedIndex = -1
            End Try

            WriteText()

            isload = True
        End If
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        If isload = True Then
            isload = False

            Try
                D_actval = TextBox8.Text
            Catch ex As Exception
                TextBox8.Text = 65535
                D_actval = 65535
            End Try

            Try
                ComboBox8.SelectedIndex = TextBox8.Text
            Catch ex As Exception
                ComboBox8.SelectedIndex = -1
            End Try

            WriteText()

            isload = True
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        If isload = True Then
            isload = False

            Try
                D_actval = ComboBox8.SelectedIndex 'TextBox5.Text
            Catch ex As Exception
                ComboBox8.SelectedIndex = -1
                D_actval = 65535
            End Try

            Try
                TextBox8.Text = ComboBox8.SelectedIndex
            Catch ex As Exception
                ComboBox8.SelectedIndex = -1
            End Try

            WriteText()

            isload = True
        End If
    End Sub


    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If isload = True Then
            isload = False
            Try

                While TextBox4.TextLength < 60
                    TextBox4.SelectedText = 0
                    TextBox4.SelectionStart -= 1
                End While
                While TextBox4.TextLength > 60
                    If TextBox4.SelectionStart > 60 Then
                        TextBox4.SelectionStart -= 1
                        TextBox4.SelectionLength = 1
                        TextBox4.SelectedText = ""
                    Else
                        TextBox4.SelectionLength = 1
                        TextBox4.SelectedText = ""
                    End If
                End While

                If TextBox4.TextLength = 60 Then
                    Dim k, t As Integer
                    k = TextBox4.SelectionStart
                    t = TextBox4.SelectionLength
                    For i = 0 To 19
                        If Mid(TextBox4.Text, i * 3 + 1, 3) > 255 Then
                            TextBox4.Text = Mid(TextBox4.Text, 1, i * 3) & 255 & Mid(TextBox4.Text, i * 3 + 4, 57 - i * 3)
                        End If
                    Next

                    TextBox4.SelectionStart = k
                    TextBox4.SelectionLength = t
                End If

            Catch ex As Exception
                btndataLoad()
            End Try

            isload = True
        End If
    End Sub
    Private Function ValueTostring(value As Object) As String
        Dim rstr As String = ""

        Dim memstr As New MemoryStream(4)
        Dim binwriter As New BinaryWriter(memstr)

        binwriter.Write(value)

        memstr.Position = 0
        Select Case value.GetType.ToString
            Case "System.UInt16"
                rstr = rstr & Format(memstr.ReadByte, "000")
                rstr = rstr & Format(memstr.ReadByte, "000")
            Case "System.UInt32"
                rstr = rstr & Format(memstr.ReadByte, "000")
                rstr = rstr & Format(memstr.ReadByte, "000")
                rstr = rstr & Format(memstr.ReadByte, "000")
                rstr = rstr & Format(memstr.ReadByte, "000")
        End Select



        binwriter.Close()
        memstr.Close()
        Return rstr
    End Function


    Private Sub WriteText()
        TextBox4.Text = ValueTostring(D_Pos) & ValueTostring(D_icon) _
         & ValueTostring(D_con) & ValueTostring(D_act) _
         & ValueTostring(D_conval) & ValueTostring(D_actval) _
         & ValueTostring(D_enaStr) & ValueTostring(D_disStr)
    End Sub

    Private Sub btndataLoad()


        WriteText()

        TextBox3.Text = D_Pos
        TextBox5.Text = D_icon
        Try
            PictureBox1.Image = DatEditForm.ICONILIST.Images(D_icon) '방어구 아이콘
        Catch ex As Exception
            PictureBox1.Image = DatEditForm.ICONILIST.Images(4)
        End Try

        Try
            ComboBox4.SelectedIndex = D_icon
        Catch ex As Exception
            ComboBox4.SelectedIndex = -1
        End Try

        TextBox10.Text = D_enaStr

        Try
            ComboBox10.SelectedIndex = D_enaStr
        Catch ex As Exception
            ComboBox10.SelectedIndex = -1
        End Try

        TextBox11.Text = D_disStr
        Try
            ComboBox9.SelectedIndex = D_disStr
        Catch ex As Exception
            ComboBox9.SelectedIndex = -1
        End Try

        TextBox7.Clear()
        ComboBox6.SelectedIndex = -1
        ComboBox5.Items.Clear()
        For i = 0 To conbtnFnc.Count - 1
            If conbtnFnc(i).FucOffset = D_con Then
                Try
                    ComboBox6.SelectedIndex = i
                Catch ex As Exception
                    ComboBox6.SelectedIndex = -1
                End Try

                TextBox7.Text = i


                If conbtnFnc(i).Code <> -1 Then
                    ComboBox5.Items.AddRange(CODE(conbtnFnc(i).Code).ToArray)
                    Try
                        ComboBox5.SelectedIndex = D_conval
                    Catch ex As Exception
                        ComboBox5.SelectedIndex = -1
                    End Try
                End If
                Exit For
            End If
        Next

        TextBox6.Text = D_conval

        TextBox9.Clear()
        ComboBox7.SelectedIndex = -1
        ComboBox8.Items.Clear()
        For i = 0 To actbtnFnc.Count - 1
            If actbtnFnc(i).FucOffset = D_act Then
                Try
                    ComboBox7.SelectedIndex = i
                Catch ex As Exception
                    ComboBox7.SelectedIndex = -1
                End Try
                TextBox9.Text = i


                If actbtnFnc(i).Code <> -1 Then
                    ComboBox8.Items.AddRange(CODE(actbtnFnc(i).Code).ToArray)
                    Try
                        ComboBox8.SelectedIndex = D_actval
                    Catch ex As Exception
                        ComboBox8.SelectedIndex = -1
                    End Try
                End If
                Exit For
            End If
        Next

        TextBox8.Text = D_actval

    End Sub

    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        If D_enaStr <> 0 Then
            Dim value As UInteger = D_enaStr - 1

            If value <> 0 Then
                Dim dialog As DialogResult
                StatTextForm.stringNum = value
                dialog = StatTextForm.ShowDialog()
                If dialog = DialogResult.OK Then

                    StatTextAdd(value, StatTextForm.RawText)
                    Loadstattxt()

                    btndataLoad()
                    'ComboBox32.Items(TextBox58.Text) = StatTextForm.RawText
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If D_disStr <> 0 Then
            Dim value As UInteger = D_disStr - 1

            If value <> 0 Then
                Dim dialog As DialogResult
                StatTextForm.stringNum = value
                dialog = StatTextForm.ShowDialog()
                If dialog = DialogResult.OK Then

                    StatTextAdd(value, StatTextForm.RawText)
                    Loadstattxt()
                    btndataLoad()

                    'ComboBox32.Items(TextBox58.Text) = StatTextForm.RawText
                End If
            End If
        End If
    End Sub
    Private Sub CodeViewerShow(listN As DTYPE, Button As Object)
        Try
            CodeViewer.listNum = listN
            Dim btnnum As Integer = 0
            CodeViewer.Value = D_icon

            CodeViewer.mode = "BtnSet"
            CodeViewer.ObjectName = Button.Tag
            CodeViewer.ObjectNum = 0
            CodeViewer.ObjectTab = 0
            CodeViewer.BtnCount = btnnum


            CodeViewer.Show()
            CodeViewer.Location = New Point(MousePosition.X - CodeViewer.Size.Width / 2, MousePosition.Y)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        CodeViewerShow(DTYPE.icon, PictureBox1)
    End Sub
End Class