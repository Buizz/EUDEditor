Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Threading.Thread


Public Class binEditorForm

    Dim racetype() As Integer = {0, 6, 13}
    Dim race As Integer
    Dim selectedsceneNum As Integer
    Dim selecteddialog As Integer = -1
    Dim selectedobject As Integer = -1
    Private Sub refreshBtn()
        GroupBox3.Enabled = False
        ListBox4.Items.Clear()
        ListBox3.Items.Clear()
        GroupBox5.Enabled = False
        selectedobject = -1
        ListBox2.Items.Clear()
        For i = 0 To 4
            ListBox2.Items.Add(binfilename(i))
        Next

        Select Case race
            Case 0
                For i = 5 To 10
                    ListBox2.Items.Add(binfilename(i))
                Next
            Case 1
                For i = 11 To 17
                    ListBox2.Items.Add(binfilename(i))
                Next
            Case 2
                For i = 18 To 25
                    ListBox2.Items.Add(binfilename(i))
                Next
        End Select
    End Sub
    Private Sub DrawConsole()
        Dim bmp As New Bitmap(640, 480) 'My.Application.Info.DirectoryPath & "\Data\bin\" & "pconsole.bmp")

        Dim grp As Graphics = Graphics.FromImage(bmp)

        'grp.DrawRectangle(Pens.Red, mousepos.X, mousepos.Y, 100, 100)


        For i = 0 To ListBox2.Items.Count - 1
            Dim dialogNum As Integer = i


            If dialogNum > 4 Then
                Select Case race
                    Case 0
                        dialogNum += racetype(race)
                    Case 1
                        dialogNum += racetype(race)
                    Case 2
                        dialogNum += racetype(race)
                End Select
            End If
            Dim tpos As Point = PjcutData(selectedsceneNum).binData(dialogNum).pos
            Dim tsize As Size = PjcutData(selectedsceneNum).binData(dialogNum).size

            If CheckFileExist(PjcutData(selectedsceneNum).binData(dialogNum).imagename) Then
                Select Case dialogNum
                    Case 0
                    Case 1 To 4
                        Select Case race
                            Case 0
                                grp.DrawImage(DefaultBinBitmap(dialogNum).pbmp, tpos.X, tpos.Y)
                            Case 1
                                grp.DrawImage(DefaultBinBitmap(dialogNum).tbmp, tpos.X, tpos.Y)
                            Case 2
                                grp.DrawImage(DefaultBinBitmap(dialogNum).zbmp, tpos.X, tpos.Y)
                        End Select
                    Case Else
                        grp.DrawImage(DefaultBinBitmap(dialogNum).bmp, tpos.X, tpos.Y)

                End Select
            Else
                'tsize = New Bitmap(projectsceneData(selectedsceneNum).binData(dialogNum).imagename).Size
                Dim mybitmap As New Bitmap(PjcutData(selectedsceneNum).binData(dialogNum).imagename)
                mybitmap.MakeTransparent(Color.Black)
                grp.DrawImage(mybitmap, tpos.X, tpos.Y)

            End If



            Dim brushs1 As New SolidBrush(Color.FromArgb(&H221DDB16))
            Dim brushs2 As New SolidBrush(Color.FromArgb(&H44FF00DD))


            For k = 0 To PjcutData(selectedsceneNum).binData(dialogNum).ObjDlg.Count - 1
                Dim ttpos As Point = PjcutData(selectedsceneNum).binData(dialogNum).ObjDlg(k).pos
                Dim ttsize As Size = PjcutData(selectedsceneNum).binData(dialogNum).ObjDlg(k).size
                If DefaultBinBitmap(dialogNum).objtimageEnable(k) = True Then
                    Try
                        Select Case i'오브젝트 그리기
                            Case 4
                                Select Case race
                                    Case 0
                                        grp.DrawImage(DefaultBinBitmap(dialogNum).objtimage(k), ttpos.X + tpos.X, ttpos.Y + tpos.Y)
                                    Case 1
                                        grp.DrawImage(DefaultBinBitmap(dialogNum).objtimage(k + 4), ttpos.X + tpos.X, ttpos.Y + tpos.Y)
                                    Case 2
                                        grp.DrawImage(DefaultBinBitmap(dialogNum).objtimage(k + 8), ttpos.X + tpos.X, ttpos.Y + tpos.Y)
                                End Select

                            Case Else
                                grp.DrawImage(DefaultBinBitmap(dialogNum).objtimage(k), ttpos.X + tpos.X, ttpos.Y + tpos.Y)
                        End Select
                    Catch ex As Exception

                    End Try

                End If

                If ListBox2.SelectedIndex = i Then
                    If selectedobject = k Then
                        grp.FillRectangle(brushs2, ttpos.X + tpos.X, ttpos.Y + tpos.Y, ttsize.Width, ttsize.Height)
                    End If
                End If

                If DefaultBinBitmap(dialogNum).objtimageEnable(k) = True Then
                    grp.DrawRectangle(Pens.LimeGreen, ttpos.X + tpos.X, ttpos.Y + tpos.Y, ttsize.Width, ttsize.Height)
                End If
            Next

            If ListBox2.SelectedIndex = i Then
                grp.FillRectangle(brushs1, tpos.X, tpos.Y, tsize.Width, tsize.Height) '다이얼로그 선택되었을 경우
            End If
            grp.DrawRectangle(Pens.Red, tpos.X, tpos.Y, tsize.Width, tsize.Height) '다이얼로그 외각선
        Next
        bmp.MakeTransparent(Color.Black)

        PictureBox1.Image = bmp
    End Sub

    Private Sub binEditorForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        PictureBox1.Image = New Bitmap(640, 480) 'Format8bppIndexed


        ResetList()

        If ListBox1.SelectedIndex = -1 Then
            ListBox1.SelectedIndex = 0
        End If

        refreshBtn()
        DrawConsole()
    End Sub

    Dim isShiftDown As Boolean
    Private Sub binEditorForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Shift = True Then
            isShiftDown = True
        End If

    End Sub
    Private Sub binEditorForm_Keyup(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        If e.Shift = True Then
            isShiftDown = False
        End If

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        race = TabControl1.SelectedIndex


        refreshBtn()
        DrawConsole()
    End Sub

    Dim oldmoustpos As Point
    Dim olddialogpos As Point
    Dim oldobjectpos As Point
    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        Dim ispickin As Boolean = False
        oldmoustpos = mousepos
        '오브젝트 선택


        For i = 0 To ListBox2.Items.Count - 1
            Dim dialogNum As Integer = i
            If dialogNum > 4 Then
                Select Case race
                    Case 0
                        dialogNum += racetype(race)
                    Case 1
                        dialogNum += racetype(race)
                    Case 2
                        dialogNum += racetype(race)
                End Select
            End If

            Dim tpos As Point = PjcutData(selectedsceneNum).binData(dialogNum).pos
            Dim tsize As Size = PjcutData(selectedsceneNum).binData(dialogNum).size



            If (tpos.X <= mousepos.X And mousepos.X <= tpos.X + tsize.Width And
               tpos.Y <= mousepos.Y And mousepos.Y <= tpos.Y + tsize.Height) Then
                olddialogpos.X = mousepos.X - tpos.X
                olddialogpos.Y = mousepos.Y - tpos.Y
                If ListBox2.SelectedIndex = i Then

                    '안에 있는 것 클릭하기.
                    For k = 0 To ListBox3.Items.Count - 1
                        If DefaultBinBitmap(dialogNum).objtimageEnable(k) = True Then
                            Dim ttpos As Point = PjcutData(selectedsceneNum).binData(dialogNum).ObjDlg(k).pos
                            Dim ttsize As Size = PjcutData(selectedsceneNum).binData(dialogNum).ObjDlg(k).size

                            If tpos.X + ttpos.X <= mousepos.X And mousepos.X <= tpos.X + ttpos.X + ttsize.Width And
                               tpos.Y + ttpos.Y <= mousepos.Y And mousepos.Y <= tpos.Y + ttpos.Y + ttsize.Height Then
                                oldobjectpos.X = mousepos.X - ttpos.X
                                oldobjectpos.Y = mousepos.Y - ttpos.Y
                                If ListBox3.SelectedIndex <> k Then
                                    isloadfinsish = False
                                    ListBox3.SelectedIndex = k
                                    isloadfinsish = True
                                    ispickin = True
                                    'Exit For
                                Else
                                    'ListBox3.SelectedIndex = lastselect
                                    ispickin = True
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    LoadinterObject()

                    If ispickin = False Then
                        ListBox3.SelectedIndex = -1
                        ispickin = True

                    End If
                Else
                    ispickin = True
                    ListBox2.SelectedIndex = i
                    Exit For
                End If

            Else
                If ListBox2.SelectedIndex = i Then
                    '안에 있는 것 클릭하기.
                    For k = 0 To ListBox3.Items.Count - 1
                        If DefaultBinBitmap(dialogNum).objtimageEnable(k) = True Then
                            Dim ttpos As Point = PjcutData(selectedsceneNum).binData(dialogNum).ObjDlg(k).pos
                            Dim ttsize As Size = PjcutData(selectedsceneNum).binData(dialogNum).ObjDlg(k).size

                            If tpos.X + ttpos.X <= mousepos.X And mousepos.X <= tpos.X + ttpos.X + ttsize.Width And
                               tpos.Y + ttpos.Y <= mousepos.Y And mousepos.Y <= tpos.Y + ttpos.Y + ttsize.Height Then
                                oldobjectpos.X = mousepos.X - ttpos.X
                                oldobjectpos.Y = mousepos.Y - ttpos.Y
                                If ListBox3.SelectedIndex <> k Then
                                    ListBox3.SelectedIndex = k
                                    ispickin = True
                                    Exit For
                                Else
                                    ispickin = True
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Next



        If ispickin = True Then
            Timer1.Enabled = True
            isclick = True
        Else
            ListBox2.SelectedIndex = -1
            ListBox3.SelectedIndex = -1
        End If
    End Sub
    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        isclick = False
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        mousepos = e.Location
    End Sub

    Dim isclick As Boolean
    Public mousepos As New Point
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


        If isclick Then
            If selecteddialog <> -1 Then
                If selectedobject = -1 Then
                    Dim x As Integer = mousepos.X - olddialogpos.X
                    Dim y As Integer = mousepos.Y - olddialogpos.Y
                    If isShiftDown Then
                        x = (x \ 5) * 5
                        y = (y \ 5) * 5
                    End If
                    PjcutData(selectedsceneNum).binData(selecteddialog).pos.X = x
                    PjcutData(selectedsceneNum).binData(selecteddialog).pos.Y = y


                    NumericUpDown1.Value = PjcutData(selectedsceneNum).binData(selecteddialog).pos.X
                    NumericUpDown2.Value = PjcutData(selectedsceneNum).binData(selecteddialog).pos.Y
                Else
                    Dim x As Integer = mousepos.X - oldobjectpos.X
                    Dim y As Integer = mousepos.Y - oldobjectpos.Y
                    If isShiftDown Then
                        x = (x \ 5) * 5
                        y = (y \ 5) * 5
                    End If
                    PjcutData(selectedsceneNum).binData(selecteddialog).ObjDlg(selectedobject).pos.X _
                    = x
                    PjcutData(selectedsceneNum).binData(selecteddialog).ObjDlg(selectedobject).pos.Y _
                    = y



                    NumericUpDown5.Value = PjcutData(selectedsceneNum).binData(selecteddialog).ObjDlg(selectedobject).pos.X
                    NumericUpDown6.Value = PjcutData(selectedsceneNum).binData(selecteddialog).ObjDlg(selectedobject).pos.Y

                End If

            End If

            DrawConsole()
        Else
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub 다이얼로그선택(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        ListBox3.Items.Clear()
        GroupBox5.Enabled = False
        ListBox4.Items.Clear()

        selectedobject = -1
        selecteddialog = ListBox2.SelectedIndex
        If ListBox2.SelectedIndex <> -1 Then
            GroupBox3.Enabled = True

            If selecteddialog > 4 Then
                Select Case race
                    Case 0
                        selecteddialog += racetype(race)
                    Case 1
                        selecteddialog += racetype(race)
                    Case 2
                        selecteddialog += racetype(race)
                End Select
            End If
            If CheckFileExist(PjcutData(selectedsceneNum).binData(selecteddialog).imagename) Then
                Button2.Enabled = False
            Else
                Button2.Enabled = True
            End If
            For i = 0 To PjcutData(selectedsceneNum).binData(selecteddialog).ObjDlg.Count - 1
                ListBox3.Items.Add(binfileobjectname(PjcutData(selectedsceneNum).binData(selecteddialog).ObjDlg(i).controltype))
            Next


            NumericUpDown1.Value = PjcutData(selectedsceneNum).binData(selecteddialog).pos.X
            NumericUpDown2.Value = PjcutData(selectedsceneNum).binData(selecteddialog).pos.Y

            'NumericUpDown3.Value = projectsceneData(selectedsceneNum).binData(selecteddialog).size.Width
            'NumericUpDown4.Value = projectsceneData(selectedsceneNum).binData(selecteddialog).size.Height

            'ListBox3.SelectedIndex = 0

            Select Case selecteddialog
                Case 0 '미네랄
                    For i = 0 To 1
                        ListBox4.Items.Add(Lan.GetText(Me.Name, "Min" & i))
                    Next
                Case 1 'statdata
                    For i = 0 To 9
                        ListBox4.Items.Add(Lan.GetText(Me.Name, "statdata" & i))
                    Next
                Case 2'포트레잇
                Case 3'메뉴
                Case 4 '미니맵
                    For i = 0 To 1
                        ListBox4.Items.Add(Lan.GetText(Me.Name, "Minimap" & i))
                    Next
            End Select
            For i = 0 To 1
                ListBox4.Items.Add(Lan.GetText(Me.Name, "Always" & i))
            Next
            ListBox4.SelectedIndex = 0
        Else
            GroupBox3.Enabled = False
        End If




        DrawConsole()
    End Sub

    Private Sub Secne(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        selectedsceneNum = ListBox1.SelectedIndex
        ListBox2.SelectedIndex = -1
        ListBox3.SelectedIndex = -1
        ListBox4.SelectedIndex = -1

        DrawConsole()
    End Sub

    Dim isloadfinsish As Boolean = True
    Private Sub interObject(sender As Object, e As EventArgs) Handles ListBox3.SelectedIndexChanged
        If isloadfinsish Then
            LoadinterObject()
        End If

    End Sub
    Private Sub LoadinterObject()

        selectedobject = ListBox3.SelectedIndex


        If selectedobject <> -1 And selecteddialog <> -1 Then
            GroupBox5.Enabled = True
            NumericUpDown5.Value = PjcutData(selectedsceneNum).binData(selecteddialog).ObjDlg(selectedobject).pos.X
            NumericUpDown6.Value = PjcutData(selectedsceneNum).binData(selecteddialog).ObjDlg(selectedobject).pos.Y
        Else
            GroupBox5.Enabled = False
        End If
        DrawConsole()
    End Sub



    Private Sub 필터(sender As Object, e As EventArgs) Handles ListBox4.SelectedIndexChanged
        If selecteddialog <> -1 Then
            For i = 0 To DefaultBinBitmap(selecteddialog).objtimageEnable.Count - 1
                DefaultBinBitmap(selecteddialog).objtimageEnable(i) = False
            Next

            Dim fliterNum As Integer = ListBox4.SelectedIndex

            DefaultBinBitmap(selecteddialog).SetEnable(fliterNum)


            DrawConsole()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PjcutData.Add(New CsceneData)
        ListBox1.Items.Add(Lan.GetText(Me.Name, "Scene") & " " & PjcutData.Count - 1)
        DrawConsole()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ListBox1.SelectedIndex <> 0 Then

            PjcutData.RemoveAt(ListBox1.SelectedIndex)
            ResetList()
            DrawConsole()
        Else
            PjcutData.RemoveAt(ListBox1.SelectedIndex)
            PjcutData.Add(New CsceneData)
            ResetList()
            DrawConsole()
        End If

    End Sub
    Private Sub ResetList()
        ListBox1.Items.Clear()
        ListBox1.Items.Add(Lan.GetText(Me.Name, "DefaultScene"))
        For i = 0 To PjcutData.Count - 2
            ListBox1.Items.Add(Lan.GetText(Me.Name, "Scene") & i + 1)
        Next
        ListBox1.SelectedIndex = 0
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        PjcutData(selectedsceneNum).binData(selecteddialog).pos.X = NumericUpDown1.Value
        DrawConsole()
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        PjcutData(selectedsceneNum).binData(selecteddialog).pos.Y = NumericUpDown2.Value
        DrawConsole()
    End Sub

    Private Sub NumericUpDown5_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown5.ValueChanged
        PjcutData(selectedsceneNum).binData(selecteddialog).ObjDlg(selectedobject).pos.X = NumericUpDown5.Value
        DrawConsole()
    End Sub

    Private Sub NumericUpDown6_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown6.ValueChanged
        PjcutData(selectedsceneNum).binData(selecteddialog).ObjDlg(selectedobject).pos.Y = NumericUpDown6.Value
        DrawConsole()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dialog As DialogResult

        dialog = OpenFileDialog1.ShowDialog

        If dialog = DialogResult.OK Then
            Dim tbitmap As Bitmap = New Bitmap(OpenFileDialog1.FileName)

            If (tbitmap.Size.Width * tbitmap.Size.Height) > (BinfileData.binData(selecteddialog).size.Width * BinfileData.binData(selecteddialog).size.Height) Then
                Dim tempstr As String = Lan.GetText("Msgbox", "binEditbmpCau")

                MsgBox(tempstr & (tbitmap.Size.Width * tbitmap.Size.Height) - (BinfileData.binData(selecteddialog).size.Width * BinfileData.binData(selecteddialog).size.Height) & " byte", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            End If
            PjcutData(selectedsceneNum).binData(selecteddialog).imagename = OpenFileDialog1.FileName
            PjcutData(selectedsceneNum).binData(selecteddialog).size = tbitmap.Size
            Button2.Enabled = True
        End If
        DrawConsole()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PjcutData(selectedsceneNum).binData(selecteddialog).imagename = ""
        PjcutData(selectedsceneNum).binData(selecteddialog).size = BinfileData.binData(selecteddialog).size
        Button2.Enabled = False
        DrawConsole()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        PjcutData(selectedsceneNum).binData(selecteddialog).pos.X = BinfileData.binData(selecteddialog).pos.X
        PjcutData(selectedsceneNum).binData(selecteddialog).pos.Y = BinfileData.binData(selecteddialog).pos.Y
        DrawConsole()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        PjcutData(selectedsceneNum).binData(selecteddialog).ObjDlg(selectedobject).pos.X = BinfileData.binData(selecteddialog).ObjDlg(selectedobject).pos.X
        PjcutData(selectedsceneNum).binData(selecteddialog).ObjDlg(selectedobject).pos.Y = BinfileData.binData(selecteddialog).ObjDlg(selectedobject).pos.Y
        DrawConsole()
    End Sub

    'Private Sub 위치초기화ToolStripMenuItem_Click(sender As Object, e As EventArgs)

    '    'Dim selectedsceneNum As Integer
    '    'Dim selecteddialog As Integer = -1
    '    'Dim selectedobject As Integer = -1

    '    If selectedobject = -1 Then
    '        projectsceneData(selectedsceneNum).binData(selecteddialog).pos.X = BinfileData.binData(selecteddialog).pos.X
    '        projectsceneData(selectedsceneNum).binData(selecteddialog).pos.Y = BinfileData.binData(selecteddialog).pos.Y
    '    Else
    '        projectsceneData(selectedsceneNum).binData(selecteddialog).Objectdia(selectedobject).pos.X = BinfileData.binData(selecteddialog).Objectdia(selectedobject).pos.X
    '        projectsceneData(selectedsceneNum).binData(selecteddialog).Objectdia(selectedobject).pos.Y = BinfileData.binData(selecteddialog).Objectdia(selectedobject).pos.Y
    '    End If
    '    DrawConsole()
    'End Sub
End Class