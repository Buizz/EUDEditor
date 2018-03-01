Imports System.IO
Imports System.Text.RegularExpressions

Public Class FireGraftForm
    Public TAB_INDEX As Integer = 0
    Public _OBJECTNUM As Integer
    Private LISTFILTER As String


    Public TabSelectindex As New List(Of Integer)
    Public Tabfilfer As New List(Of String)
    Private LastSelectTab As Integer

    Public FristRun As Boolean = False


    Public Sub RefreshForm()
        Dim oldselectindex As Integer = _OBJECTNUM
        LoadList()
        PaletDraw()

        SELECTLIST(oldselectindex)
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        ReqList.DrawMode = DrawMode.OwnerDrawVariable
        MyBase.OnLoad(e)
    End Sub

    Private Sub ListBox3_MeasureItem(sender As Object, e As MeasureItemEventArgs) Handles ReqList.MeasureItem
        e.ItemHeight = e.Graphics.MeasureString(ReqList.Items(e.Index).ToString, ReqList.Font).Height
    End Sub

    Private Sub ListBox3_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ReqList.DrawItem
        e.DrawBackground()
        Dim brush As SolidBrush
        If ReqList.Enabled = False Then
            brush = New SolidBrush(Color.FromArgb(&HFF606451))
        Else
            brush = New SolidBrush(ReqList.ForeColor)
        End If
        If e.Index <> -1 Then
            e.Graphics.DrawString(ReqList.Items(e.Index).ToString, ReqList.Font, brush, e.Bounds)
        End If


    End Sub

    Private Sub FireGraftForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim func As String = Lan.GetText(Me.Name, "Func")
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()

        For i = 1 To 9
            ComboBox1.Items.Add(func & i)
            ComboBox2.Items.Add(func & i)
        Next


        Lan.SetMenu(Me, BtnlistMenu, "BtnlistMenu")
        Lan.SetMenu(Me, ListMenu, "ListMenu")
        Lan.SetMenu(Me, ReqMenu, "ReqMenu")
        Lan.SetMenu(Me, MenuStrip1)
        Lan.SetLangage(Me)
        If FristRun = False Then
            CheckBox2.Checked = True
            Dim file As FileStream = New FileStream(My.Application.Info.DirectoryPath & "\Data\Langage\" & My.Settings.Langage & "\Icon.txt", FileMode.Open, FileAccess.Read)
            Dim stream As StreamReader = New StreamReader(file, System.Text.Encoding.Default)


            ComboBox4.Items.Clear()

            While (stream.EndOfStream = False)
                ComboBox4.Items.Add(stream.ReadLine)
            End While



            stream.Close()
            file.Close()

            ComboBox3.Items.Clear()
            file = New FileStream(My.Application.Info.DirectoryPath & "\Data\Langage\" & My.Settings.Langage & "\FireGraftStatus.txt", FileMode.Open, FileAccess.Read)
            stream = New StreamReader(file, System.Text.Encoding.Default)


            While (stream.EndOfStream = False)
                ComboBox3.Items.Add(stream.ReadLine)
            End While

            stream.Close()
            file.Close()





            Loadstattxt()



            ComboBox6.Items.Clear()
            ComboBox7.Items.Clear()
            For i = 0 To conbtnFnc.Count - 1
                ComboBox6.Items.Add(conbtnFnc(i).Name)
            Next
            For i = 0 To actbtnFnc.Count - 1
                ComboBox7.Items.Add(actbtnFnc(i).Name)
            Next

            For i = 0 To 6
                TabSelectindex.Add(0)
                Tabfilfer.Add("")
            Next


            StatusDic.Add("2 1")
            StatusDic.Add("1 0")
            StatusDic.Add("4 3")
            StatusDic.Add("3 2")
            StatusDic.Add("7 6")
            StatusDic.Add("8 7")
            StatusDic.Add("6 5")
            StatusDic.Add("5 4")
            StatusDic.Add("0 8")
            StatusDic.Add("1 8")
            StatusDic.Add("2 8")


            LoadList()
            PaletDraw()
            Try
                ListBox1.SelectedIndex = 0

            Catch ex As Exception

            End Try

            LoadData()
            ColorReset()
            FristRun = True
        Else
            LoadList()
            PaletDraw()
        End If
    End Sub
    Private Sub FireGraftForm_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing

        e.Cancel = True
        Me.Hide()

        Main.Activate()

        My.Forms.Main.Visible = True
    End Sub

    Private LastSize As Integer
    Private Sub PaletteBtn(sender As Object, e As EventArgs) Handles Button5.Click
        If SplitContainer1.SplitterDistance = 24 Then
            SplitContainer1.Panel1MinSize = 93
            SplitContainer1.IsSplitterFixed = False
            SplitContainer1.SplitterDistance = LastSize '244
            Button5.Text = Lan.GetText(Me.Name, "Fold")
        Else
            LastSize = SplitContainer1.SplitterDistance
            SplitContainer1.Panel1MinSize = 24
            SplitContainer1.IsSplitterFixed = True
            SplitContainer1.SplitterDistance = 24
            Button5.Text = Lan.GetText(Me.Name, "UnFold")
        End If
    End Sub
    Private Sub SELECTLIST(index As Integer)
        ListBox1.SelectedIndex = -1

        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(i)(LITEM.index) = index Then
                ListBox1.SelectedIndex = i
                _OBJECTNUM = index
                Exit Sub
            End If
        Next


        If ListBox1.SelectedIndex = -1 Then
            If ListBox1.Items.Count <> 0 Then
                ListBox1.SelectedIndex = 0
                _OBJECTNUM = ListBox1.Items(0)(LITEM.index)
                Exit Sub
            End If
        End If

        _OBJECTNUM = 0
    End Sub
    Private Sub LoadList()
        Dim lastSELECT As Integer = _OBJECTNUM


        ListBox1.BeginUpdate()

        ListBox1.Items.Clear()



        Select Case TAB_INDEX
            Case 0
                For i = 0 To CODE(DTYPE.units).Count - 1
                    Dim list(2) As String

                    list(LITEM.index) = i
                    list(LITEM.ischange) = False
                    list(LITEM.Name) = "[" & Format(i, "000") & "]- " & CODE(DTYPE.units)(i)




                    If ProjectUnitStatusFn1(i) <> 0 Or ProjectUnitStatusFn2(i) <> 0 Or ProjectDebugID(i) <> 0 Then
                        list(LITEM.ischange) = True
                    End If




                    Dim stra, strb As String
                    stra = list(LITEM.Name).ToLower
                    If LISTFILTER <> "" Then
                        strb = LISTFILTER.ToLower
                    Else
                        strb = ""
                    End If


                    If CheckBox5.Checked = True Then
                        If list(LITEM.ischange) = True Then
                            ListBox1.Items.Add(list)
                        End If
                    Else
                        If InStr(stra, strb) <> 0 Then
                            ListBox1.Items.Add(list)
                        End If
                    End If
                Next
            Case 1
                For i = 0 To CODE(DTYPE.btnunit).Count - 1
                    Dim list(2) As String

                    list(LITEM.index) = i
                    list(LITEM.ischange) = False
                    list(LITEM.Name) = "[" & Format(i, "000") & "]- " & CODE(DTYPE.btnunit)(i)

                    If ProjectBtnUSE(i) = True Then
                        list(LITEM.ischange) = True
                    End If


                    Dim stra, strb As String
                    stra = list(LITEM.Name).ToLower
                    If LISTFILTER <> "" Then
                        strb = LISTFILTER.ToLower
                    Else
                        strb = ""
                    End If


                    If CheckBox5.Checked = True Then
                        If list(LITEM.ischange) = True Then
                            ListBox1.Items.Add(list)
                        End If
                    Else
                        If InStr(stra, strb) <> 0 Then
                            ListBox1.Items.Add(list)
                        End If
                    End If
                Next
            Case 2
                For i = 0 To CODE(DTYPE.units).Count - 1
                    Dim list(2) As String

                    list(LITEM.index) = i
                    list(LITEM.ischange) = False
                    list(LITEM.Name) = "[" & Format(i, "000") & "]- " & CODE(DTYPE.units)(i)

                    If ProjectRequireDataUSE(TAB_INDEX - 2)(i) <> 0 Then
                        list(LITEM.ischange) = True
                    End If


                    Dim stra, strb As String
                    stra = list(LITEM.Name).ToLower
                    If LISTFILTER <> "" Then
                        strb = LISTFILTER.ToLower
                    Else
                        strb = ""
                    End If


                    If CheckBox5.Checked = True Then
                        If list(LITEM.ischange) = True Then
                            ListBox1.Items.Add(list)
                        End If
                    Else
                        If InStr(stra, strb) <> 0 Then
                            ListBox1.Items.Add(list)
                        End If
                    End If
                Next
            Case 3
                For i = 0 To CODE(DTYPE.upgrades).Count - 1
                    Dim list(2) As String

                    list(LITEM.index) = i
                    list(LITEM.ischange) = False
                    list(LITEM.Name) = "[" & Format(i, "000") & "]- " & CODE(DTYPE.upgrades)(i)

                    If ProjectRequireDataUSE(TAB_INDEX - 2)(i) <> 0 Then
                        list(LITEM.ischange) = True
                    End If


                    Dim stra, strb As String
                    stra = list(LITEM.Name).ToLower
                    If LISTFILTER <> "" Then
                        strb = LISTFILTER.ToLower
                    Else
                        strb = ""
                    End If

                    If CheckBox5.Checked = True Then
                        If list(LITEM.ischange) = True Then
                            ListBox1.Items.Add(list)
                        End If
                    Else
                        If InStr(stra, strb) <> 0 Then
                            ListBox1.Items.Add(list)
                        End If
                    End If
                Next
            Case 4
                For i = 0 To CODE(DTYPE.techdata).Count - 2
                    Dim list(2) As String

                    list(LITEM.index) = i
                    list(LITEM.ischange) = False
                    list(LITEM.Name) = "[" & Format(i, "000") & "]- " & CODE(DTYPE.techdata)(i)


                    If ProjectRequireDataUSE(TAB_INDEX - 2)(i) <> 0 Then
                        list(LITEM.ischange) = True
                    End If

                    Dim stra, strb As String
                    stra = list(LITEM.Name).ToLower
                    If LISTFILTER <> "" Then
                        strb = LISTFILTER.ToLower
                    Else
                        strb = ""
                    End If

                    If CheckBox5.Checked = True Then
                        If list(LITEM.ischange) = True Then
                            ListBox1.Items.Add(list)
                        End If
                    Else
                        If InStr(stra, strb) <> 0 Then
                            ListBox1.Items.Add(list)
                        End If
                    End If
                Next
            Case 5
                For i = 0 To CODE(DTYPE.techdata).Count - 2
                    Dim list(2) As String

                    list(LITEM.index) = i
                    list(LITEM.ischange) = False
                    list(LITEM.Name) = "[" & Format(i, "000") & "]- " & CODE(DTYPE.techdata)(i)

                    If ProjectRequireDataUSE(TAB_INDEX - 2)(i) <> 0 Then
                        list(LITEM.ischange) = True
                    End If


                    Dim stra, strb As String
                    stra = list(LITEM.Name).ToLower
                    If LISTFILTER <> "" Then
                        strb = LISTFILTER.ToLower
                    Else
                        strb = ""
                    End If

                    If CheckBox5.Checked = True Then
                        If list(LITEM.ischange) = True Then
                            ListBox1.Items.Add(list)
                        End If
                    Else
                        If InStr(stra, strb) <> 0 Then
                            ListBox1.Items.Add(list)
                        End If
                    End If
                Next
            Case 6
                For i = 0 To CODE(DTYPE.orders).Count - 2
                    Dim list(2) As String

                    list(LITEM.index) = i
                    list(LITEM.ischange) = False
                    list(LITEM.Name) = "[" & Format(i, "000") & "]- " & CODE(DTYPE.orders)(i)

                    If ProjectRequireDataUSE(TAB_INDEX - 2)(i) <> 0 Then
                        list(LITEM.ischange) = True
                    End If

                    Dim stra, strb As String
                    stra = list(LITEM.Name).ToLower
                    If LISTFILTER <> "" Then
                        strb = LISTFILTER.ToLower
                    Else
                        strb = ""
                    End If

                    If CheckBox5.Checked = True Then
                        If list(LITEM.ischange) = True Then
                            ListBox1.Items.Add(list)
                        End If
                    Else
                        If InStr(stra, strb) <> 0 Then
                            ListBox1.Items.Add(list)
                        End If
                    End If
                Next
        End Select


        SELECTLIST(lastSELECT)


        ListBox1.EndUpdate()
    End Sub
    Private Sub PaletDraw()
        ListView1.BeginUpdate()
        ListView1.Items.Clear()
        Dim flingyNum, SpriteNum, ImageNum As Integer
        Dim size As Integer = ListBox1.Items.Count - 1
        For i = 0 To size
            Dim index As Integer = ListBox1.Items(i)(LITEM.index)

            ListView1.Items.Add("")
            Dim itemindex As Integer = ListView1.Items.Count - 1
            Select Case TAB_INDEX
                Case 0
                    flingyNum = DatEditDATA(DTYPE.units).ReadValue("Graphics", index)
                    SpriteNum = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", flingyNum)
                    ImageNum = DatEditDATA(DTYPE.sprites).ReadValue("Image File", SpriteNum)

                    ListView1.LargeImageList = DatEditForm.IMAGELIST
                    ListView1.Items(itemindex).ImageIndex = ImageNum
                Case 1
                    Try

                        flingyNum = DatEditDATA(DTYPE.units).ReadValue("Graphics", index)
                        SpriteNum = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", flingyNum)
                        ImageNum = DatEditDATA(DTYPE.sprites).ReadValue("Image File", SpriteNum)

                        ListView1.LargeImageList = DatEditForm.IMAGELIST
                        ListView1.Items(itemindex).ImageIndex = ImageNum
                    Catch ex As Exception
                        ListView1.LargeImageList = DatEditForm.IMAGELIST
                        ListView1.Items(itemindex).ImageIndex = 0
                    End Try
                Case 2
                    flingyNum = DatEditDATA(DTYPE.units).ReadValue("Graphics", index)
                    SpriteNum = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", flingyNum)
                    ImageNum = DatEditDATA(DTYPE.sprites).ReadValue("Image File", SpriteNum)

                    ListView1.LargeImageList = DatEditForm.IMAGELIST
                    ListView1.Items(itemindex).ImageIndex = ImageNum


                Case 3
                    ListView1.LargeImageList = DatEditForm.ICONILIST
                    Try
                        ListView1.Items(itemindex).ImageIndex = DatEditDATA(DTYPE.upgrades).ReadValue("Icon", index)
                    Catch ex As Exception
                        ListView1.Items(itemindex).ImageIndex = 4
                    End Try

                Case 4
                    ListView1.LargeImageList = DatEditForm.ICONILIST
                    Try
                        ListView1.Items(itemindex).ImageIndex = DatEditDATA(DTYPE.techdata).ReadValue("Icon", index)
                    Catch ex As Exception
                        ListView1.Items(itemindex).ImageIndex = 4
                    End Try
                Case 5
                    ListView1.LargeImageList = DatEditForm.ICONILIST
                    Try
                        ListView1.Items(itemindex).ImageIndex = DatEditDATA(DTYPE.techdata).ReadValue("Icon", index)
                    Catch ex As Exception
                        ListView1.Items(itemindex).ImageIndex = 4
                    End Try

                Case 6
                    ListView1.LargeImageList = DatEditForm.ICONILIST
                    Try
                        ListView1.Items(itemindex).ImageIndex = DatEditDATA(DTYPE.orders).ReadValue("Highlight", index)
                        If DatEditDATA(DTYPE.orders).ReadValue("Highlight", index) > 390 Then
                            ListView1.Items(itemindex).ImageIndex = 4
                        End If
                    Catch ex As Exception
                        ListView1.Items(itemindex).ImageIndex = 4
                    End Try
            End Select
            ListView1.Items(itemindex).Tag = index
        Next
        ListView1.EndUpdate()


        'ListView1.Clear()
        'ListView1.Items.Add(New ListView.ListViewItemCollection())
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.Click, ListView1.ItemSelectionChanged
        Try
            SELECTLIST(ListView1.SelectedItems(0).Tag)
        Catch ex As Exception
        End Try
    End Sub



    Private Sub MainTab_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MainTab.SelectedIndexChanged
        TAB_INDEX = MainTab.SelectedIndex
        'If TAB_INDEX = 2 Then
        '    MsgBox(Lan.GetText("Msgbox", "NotArrow"), MsgBoxStyle.Information, ProgramSet.ErrorFormMessage)
        '    MainTab.SelectedIndex = 1
        '    TAB_INDEX += TabControl2.SelectedIndex
        'End If
        '수정수정수정


        'Public TabSelectindex As New List(Of Integer)
        'Public Tabfilfer As New List(Of String)
        'Private LastSelectTab As Integer


        TabSelectindex(LastSelectTab) = _OBJECTNUM
        Tabfilfer(LastSelectTab) = LISTFILTER
        'MsgBox(LISTFILTER)


        LISTFILTER = Tabfilfer(TAB_INDEX)
        _OBJECTNUM = TabSelectindex(TAB_INDEX)

        Dim IME = TextBox2.ImeMode
        TextBox2.ImeMode = ImeMode.Off
        TextBox2.Text = ""
        TextBox2.ImeMode = IME
        Application.DoEvents()


        TextBox2.Text = Tabfilfer(TAB_INDEX)

        LastSelectTab = TAB_INDEX




        LoadList()
        PaletDraw()
    End Sub
    Private Sub TabControl2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl2.SelectedIndexChanged
        TAB_INDEX = 2 + TabControl2.SelectedIndex



        TabSelectindex(LastSelectTab) = _OBJECTNUM
        Tabfilfer(LastSelectTab) = LISTFILTER
        'MsgBox(LISTFILTER)


        LISTFILTER = Tabfilfer(TAB_INDEX)
        _OBJECTNUM = TabSelectindex(TAB_INDEX)

        Dim IME = TextBox2.ImeMode
        TextBox2.ImeMode = ImeMode.Off
        TextBox2.Text = ""
        TextBox2.ImeMode = IME
        Application.DoEvents()


        TextBox2.Text = Tabfilfer(TAB_INDEX)

        LastSelectTab = TAB_INDEX


        LoadList()
        PaletDraw()
    End Sub

    Dim StatusDic As New List(Of String)
    Dim isload As Boolean
    Private Sub changeColor(ByRef _object As Object, _value As Integer)
        If _value = 0 Then
            With _object
                .ForeColor = ProgramSet.FORECOLOR
                .BackColor = ProgramSet.BACKCOLOR
            End With
        Else
            If ListBox1.SelectedIndex <> -1 Then
                ListBox1.SelectedItem(LITEM.ischange) = True
            End If
            With _object
                .ForeColor = ProgramSet.FORECOLOR
                .BackColor = ProgramSet.CHANGECOLOR
            End With
        End If
    End Sub
    Private Sub CheckChange()
        Select Case TAB_INDEX
            Case 0
                If ProjectUnitStatusFn1(_OBJECTNUM) <> 0 Or ProjectUnitStatusFn2(_OBJECTNUM) <> 0 Or ProjectDebugID(_OBJECTNUM) <> 0 Then
                    ListBox1.SelectedItem(LITEM.ischange) = True
                Else
                    ListBox1.SelectedItem(LITEM.ischange) = False
                End If
            Case 1
                If ProjectBtnUSE(_OBJECTNUM) = True Then
                    ListBox1.SelectedItem(LITEM.ischange) = True
                Else
                    ListBox1.SelectedItem(LITEM.ischange) = False
                End If
            Case 2, 3, 4, 5, 6
                If ProjectRequireDataUSE(TAB_INDEX - 2)(_OBJECTNUM) <> 0 Then
                    ListBox1.SelectedItem(LITEM.ischange) = True
                Else
                    ListBox1.SelectedItem(LITEM.ischange) = False
                End If
        End Select
    End Sub
    Private Sub GetFilesize()
        Dim MaxSize() As UInt16 = {1096, 840, 320, 688, 1316}

        Dim CurretSize As UInt16 = 2


        For i = 0 To RequireData(TAB_INDEX - 2).Count - 1
            If ProjectRequireDataUSE(TAB_INDEX - 2)(i) = 3 Then
                CurretSize += (ProjectRequireData(TAB_INDEX - 2)(i).Code.Count) * 2
                CurretSize += 4
            ElseIf ProjectRequireDataUSE(TAB_INDEX - 2)(i) = 0 Then
                CurretSize += (RequireData(TAB_INDEX - 2)(i).Code.Count) * 2
                If RequireData(TAB_INDEX - 2)(i).pos <> 0 Then
                    CurretSize += 4
                End If
            ElseIf ProjectRequireDataUSE(TAB_INDEX - 2)(i) = 2 Then
                CurretSize += 4
            End If
        Next
        CurretSize += 2


        Label13.Text = Lan.GetText(Me.Name, "Label13") & " : " & CurretSize & "/" & MaxSize(TAB_INDEX - 2)

        If CurretSize > MaxSize(TAB_INDEX - 2) Then
            Label13.ForeColor = Color.Red
        Else
            Label13.ForeColor = Color.Black
        End If
    End Sub
    Private Sub LoadData()
        RepDataToFile()

        isload = False
        Select Case TAB_INDEX
            Case 0
                CheckChange()

                If ProjectUnitStatusFn1(_OBJECTNUM) = 0 And ProjectUnitStatusFn2(_OBJECTNUM) = 0 Then
                    With ComboBox3
                        .ForeColor = ProgramSet.FORECOLOR
                        .BackColor = ProgramSet.BACKCOLOR
                    End With
                Else
                    With ComboBox3
                        .ForeColor = ProgramSet.FORECOLOR
                        .BackColor = ProgramSet.CHANGECOLOR
                    End With
                End If

                changeColor(ComboBox1, ProjectUnitStatusFn1(_OBJECTNUM))
                ComboBox1.SelectedIndex = UnitStatusFn1(_OBJECTNUM) + ProjectUnitStatusFn1(_OBJECTNUM)


                changeColor(ComboBox2, ProjectUnitStatusFn2(_OBJECTNUM))
                ComboBox2.SelectedIndex = UnitStatusFn2(_OBJECTNUM) + ProjectUnitStatusFn2(_OBJECTNUM)


                changeColor(TextBox1, ProjectDebugID(_OBJECTNUM))
                TextBox1.Text = DebugID(_OBJECTNUM) + ProjectDebugID(_OBJECTNUM)


                LoadCombobox3()
            Case 1
                CheckChange()




                CheckBox1.Checked = Not ProjectBtnUSE(_OBJECTNUM)
                Panel2.Enabled = ProjectBtnUSE(_OBJECTNUM)

                Dim file As FileStream = New FileStream(My.Application.Info.DirectoryPath & "\Data\Langage\" & My.Settings.Langage & "\Icon.txt", FileMode.Open, FileAccess.Read)
                Dim stream As StreamReader = New StreamReader(file, System.Text.Encoding.Default)

                Dim Icon() As String

                Icon = stream.ReadToEnd.Split(vbCrLf)

                stream.Close()
                file.Close()


                Dim lastsel As Integer = ListBox2.SelectedIndex
                ListBox2.Items.Clear()

                If ProjectBtnUSE(_OBJECTNUM) = True Then
                    For i = 0 To ProjectBtnData(_OBJECTNUM).Count - 1
                        ListBox2.Items.Add(Icon(ProjectBtnData(_OBJECTNUM)(i).icon))
                    Next
                    If ListBox2.Items.Count <> 0 Then
                        ListBox2.SelectedIndex = 0
                    End If
                Else
                    GroupBox4.Visible = False
                    GroupBox5.Visible = False
                    GroupBox6.Visible = False
                    GroupBox7.Visible = False

                    Panel3.Visible = False

                    For i = 0 To BtnData(_OBJECTNUM).Count - 1
                        ListBox2.Items.Add(Icon(BtnData(_OBJECTNUM)(i).icon))
                    Next
                    If ListBox2.Items.Count <> 0 Then
                        ListBox2.SelectedIndex = 0
                    End If
                End If


                If lastsel < ListBox2.Items.Count Then
                    ListBox2.SelectedIndex = lastsel
                End If
                If ListBox2.SelectedIndex = -1 And ListBox2.Items.Count <> 0 Then
                    ListBox2.SelectedIndex = 0
                End If

                PreviewDraw()
            Case 2, 3, 4, 5, 6
                GetFilesize()



                Select Case ProjectRequireDataUSE(TAB_INDEX - 2)(_OBJECTNUM)
                    Case 0
                        GroupBox8.Enabled = False
                        RadioButton1.Checked = True
                        ReadReqData(RequireData(TAB_INDEX - 2)(_OBJECTNUM))
                        ComboBox11.Visible = False
                        ComboBox12.Visible = False
                    Case 1
                        ReqList.Items.Clear()
                        GroupBox8.Enabled = False
                        RadioButton2.Checked = True
                        ComboBox11.Visible = False
                        ComboBox12.Visible = False
                    Case 2
                        ReqList.Items.Clear()
                        GroupBox8.Enabled = False
                        RadioButton3.Checked = True
                        ComboBox11.Visible = False
                        ComboBox12.Visible = False
                    Case 3
                        GroupBox8.Enabled = True
                        RadioButton4.Checked = True
                        ReadReqData(ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM))
                        '+ Environment.NewLine +
                End Select

                'MsgBox(ProjectRequireDataUSE(TAB_INDEX - 2)(_OBJECTNUM))
                'Environment.NewLine 
        End Select

        isload = True
    End Sub

    Private ReqListData As New List(Of List(Of UInt16))

    Private Sub ReadReqData(Reqdata As SReqDATA, Optional isDrawList As Boolean = True)
        Dim index As UInteger = 0
        Dim tempstr As String
        Dim opcodevtype As Byte
        Dim outdent As String = ""

        Dim list As New List(Of String)

        ReqList.BeginUpdate()
        If isDrawList Then
            ReqList.Items.Clear()
        End If
        ReqListData.Clear()

        While index < Reqdata.Code.Count
            opcodevtype = 0
            If Reqdata.Code(index) > &HFF Then 'OPcode일 경우
                Dim opcode As Integer = Reqdata.Code(index) - &HFF00
                Select Case opcode
                    Case 2, 3, 4, 37
                        ReqListData.Add(New List(Of UInt16))
                        If opcode = 37 Then
                            opcodevtype = DTYPE.techdata
                        Else
                            opcodevtype = 0
                        End If

                        '다음 오피코드를 연결한다.
                        tempstr = outdent & parsereqCode(Reqdata.Code(index))
                        ReqListData(ReqListData.Count - 1).Add(index)
                        index += 1

                        If isDrawList Then
                            ReqList.Items.Add(tempstr & Environment.NewLine & outdent & "    " & parsereqCode(Reqdata.Code(index), opcodevtype))
                        End If
                        list.Add(tempstr & Environment.NewLine & outdent & "    " & parsereqCode(Reqdata.Code(index), opcodevtype))

                        ReqListData(ReqListData.Count - 1).Add(index)

                    Case 31, 32, 33
                        ReqListData.Add(New List(Of UInt16))
                        ReqListData(ReqListData.Count - 1).Add(index)
                        outdent = "    "
                        If isDrawList Then
                            ReqList.Items.Add(parsereqCode(Reqdata.Code(index)))
                        End If
                        list.Add(parsereqCode(Reqdata.Code(index)))
                    Case 255
                        outdent = ""
                        ReqListData.Add(New List(Of UInt16))
                        ReqListData(ReqListData.Count - 1).Add(index)
                        If isDrawList Then
                            ReqList.Items.Add(outdent & parsereqCode(Reqdata.Code(index)))
                        End If
                        list.Add(outdent & parsereqCode(Reqdata.Code(index)))
                    Case Else
                        ReqListData.Add(New List(Of UInt16))
                        ReqListData(ReqListData.Count - 1).Add(index)
                        If isDrawList Then
                            ReqList.Items.Add(outdent & parsereqCode(Reqdata.Code(index)))
                        End If
                        list.Add(outdent & parsereqCode(Reqdata.Code(index)))
                End Select
            Else
                ReqListData.Add(New List(Of UInt16))


                '다음 오피코드를 연결한다.
                tempstr = outdent & "(" & reqOpcode(3) & ")"
                If isDrawList Then
                    ReqList.Items.Add(tempstr & Environment.NewLine & outdent & "    " & parsereqCode(Reqdata.Code(index), opcodevtype))
                End If
                list.Add(tempstr & Environment.NewLine & outdent & "    " & parsereqCode(Reqdata.Code(index), opcodevtype))

                ReqListData(ReqListData.Count - 1).Add(index)

            End If


            index += 1
        End While

        If isDrawList = False Then
            comboban = True
            changeban = True

            While list.Count > ReqList.Items.Count
                ReqList.Items.Add("")
            End While

            For i = 0 To list.Count - 1
                ReqList.Items(i) = list(i)
            Next
            changeban = False
            comboban = False
        End If


        Try
            If ReqList.SelectedIndex = -1 Then
                ReqList.SelectedIndex = 0

            End If
        Catch ex As Exception
            ComboBox11.Visible = False
            ComboBox12.Visible = False
        End Try
        ReqList.EndUpdate()
        If ReqList.SelectedIndex <> -1 Then
            Buttonrefresh()
        End If
    End Sub


    Dim namechange As Boolean
    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedIndex <> -1 Then
            If ProjectBtnUSE(_OBJECTNUM) = True Then
                GroupBox4.Visible = True
                GroupBox5.Visible = True
                GroupBox6.Visible = True
                GroupBox7.Visible = True

                Panel3.Visible = True
            Else
                GroupBox4.Visible = False
                GroupBox5.Visible = False
                GroupBox6.Visible = False
                GroupBox7.Visible = False

                Panel3.Visible = False
            End If

            btndataLoad()

            PreviewDraw()
        Else
            If namechange = False Then
                GroupBox4.Visible = False
                GroupBox5.Visible = False
                GroupBox6.Visible = False
                GroupBox7.Visible = False

                Panel3.Visible = False
            End If

        End If
    End Sub

    '
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

    Private Sub btndataLoad()
        isload = False
        If ListBox2.SelectedIndex <> -1 Then



            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ProjectBtnUSE(_OBJECTNUM) = True Then
                Panel3.Visible = True

                With ProjectBtnData(_OBJECTNUM)(btnnum)
                    CodeRead()

                    TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                     & ValueTostring(.con) & ValueTostring(.act) _
                     & ValueTostring(.conval) & ValueTostring(.actval) _
                     & ValueTostring(.enaStr) & ValueTostring(.disStr)

                    TextBox3.Text = .pos
                    TextBox5.Text = .icon
                    Try
                        PictureBox1.Image = DatEditForm.ICONILIST.Images(.icon) '방어구 아이콘
                    Catch ex As Exception
                        PictureBox1.Image = DatEditForm.ICONILIST.Images(4)
                    End Try

                    Try
                        ComboBox4.SelectedIndex = .icon
                    Catch ex As Exception
                        ComboBox4.SelectedIndex = -1
                    End Try

                    TextBox10.Text = .enaStr

                    Try
                        ComboBox10.SelectedIndex = .enaStr
                    Catch ex As Exception
                        ComboBox10.SelectedIndex = -1
                    End Try

                    TextBox11.Text = .disStr
                    Try
                        ComboBox9.SelectedIndex = .disStr
                    Catch ex As Exception
                        ComboBox9.SelectedIndex = -1
                    End Try

                    TextBox7.Clear()
                    ComboBox6.SelectedIndex = -1
                    ComboBox5.Items.Clear()
                    ComboBox5.ResetText()
                    ComboBox5.Text = ""
                    For i = 0 To conbtnFnc.Count - 1
                        If conbtnFnc(i).FucOffset = .con Then
                            Try
                                ComboBox6.SelectedIndex = i
                            Catch ex As Exception
                                ComboBox6.SelectedIndex = -1
                            End Try

                            TextBox7.Text = i


                            If conbtnFnc(i).Code <> -1 Then
                                ComboBox5.Items.AddRange(CODE(conbtnFnc(i).Code).ToArray)
                                Try
                                    ComboBox5.SelectedIndex = .conval
                                Catch ex As Exception
                                    ComboBox5.SelectedIndex = -1
                                End Try
                            End If
                            Exit For
                        End If
                    Next

                    TextBox6.Text = .conval

                    TextBox9.Clear()
                    ComboBox7.SelectedIndex = -1
                    ComboBox8.Items.Clear()
                    ComboBox8.Text = ""
                    ComboBox8.ResetText()
                    For i = 0 To actbtnFnc.Count - 1
                        If actbtnFnc(i).FucOffset = .act Then
                            Try
                                ComboBox7.SelectedIndex = i
                            Catch ex As Exception
                                ComboBox7.SelectedIndex = -1
                            End Try
                            TextBox9.Text = i


                            If actbtnFnc(i).Code <> -1 Then
                                ComboBox8.Items.AddRange(CODE(actbtnFnc(i).Code).ToArray)
                                Try
                                    ComboBox8.SelectedIndex = .actval
                                Catch ex As Exception
                                    ComboBox8.SelectedIndex = -1
                                End Try
                            End If
                            Exit For
                        End If
                    Next

                    TextBox8.Text = .actval
                End With
            Else
                Panel3.Visible = False
                With BtnData(_OBJECTNUM)(btnnum)


                    TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                    & ValueTostring(.con) & ValueTostring(.act) _
                    & ValueTostring(.conval) & ValueTostring(.actval) _
                    & ValueTostring(.enaStr) & ValueTostring(.disStr)
                End With
            End If



        End If
        isload = True
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

    Private Sub PreviewDraw()
        Dim bitmap As New Bitmap(120, 120)
        Dim grptool As Graphics
        grptool = Graphics.FromImage(bitmap)


        '
        If ProjectBtnUSE(_OBJECTNUM) = True Then
            For i = 0 To ProjectBtnData(_OBJECTNUM).Count - 1
                Dim pos As Integer = ProjectBtnData(_OBJECTNUM)(i).pos - 1

                Dim tpos As New Point((pos Mod 3) * 38, Math.Floor(pos / 3) * 36)

                Try
                    grptool.DrawImage(My.Forms.DatEditForm.ICONILIST.Images(ProjectBtnData(_OBJECTNUM)(i).icon), tpos)
                Catch ex As Exception
                    grptool.DrawImage(My.Forms.DatEditForm.ICONILIST.Images(4), tpos)
                End Try
            Next
            If ListBox2.SelectedIndex <> -1 Then
                Dim pos As Integer = ProjectBtnData(_OBJECTNUM)(ListBox2.SelectedIndex).pos - 1

                Dim tpos As New Point((pos Mod 3) * 38, Math.Floor(pos / 3) * 36)

                Dim brush As New SolidBrush(Color.FromArgb(&HAA1DDB16))
                grptool.FillRectangle(brush, New Rectangle(tpos.X, tpos.Y, 36, 34))
            End If
        Else
            For i = 0 To BtnData(_OBJECTNUM).Count - 1
                Dim pos As Integer = BtnData(_OBJECTNUM)(i).pos - 1

                Dim tpos As New Point((pos Mod 3) * 38, Math.Floor(pos / 3) * 36)

                Try
                    grptool.DrawImage(My.Forms.DatEditForm.ICONILIST.Images(BtnData(_OBJECTNUM)(i).icon), tpos)
                Catch ex As Exception
                    grptool.DrawImage(My.Forms.DatEditForm.ICONILIST.Images(4), tpos)
                End Try
            Next
            If ListBox2.SelectedIndex <> -1 Then
                Dim pos As Integer = BtnData(_OBJECTNUM)(ListBox2.SelectedIndex).pos - 1

                Dim tpos As New Point((pos Mod 3) * 38, Math.Floor(pos / 3) * 36)

                Dim brush As New SolidBrush(Color.FromArgb(&HAA1DDB16))
                grptool.FillRectangle(brush, New Rectangle(tpos.X, tpos.Y, 36, 34))
            End If
        End If




        BtnPreview.Image = bitmap
    End Sub

    Private Sub LoadCombobox3()
        isload = False
        If StatusDic.Contains(ComboBox1.SelectedIndex & " " & ComboBox2.SelectedIndex) Then
            ComboBox3.SelectedIndex = StatusDic.FindIndex(Function(p As String)
                                                              Return p = ComboBox1.SelectedIndex & " " & ComboBox2.SelectedIndex
                                                          End Function)

        Else
            ComboBox3.SelectedIndex = -1
        End If
        isload = True
    End Sub



    Private Sub ListBox1_DrawItem(ByVal sender As Object,
ByVal e As System.Windows.Forms.DrawItemEventArgs) _
Handles ListBox1.DrawItem
        If (e.Index < 0) Then Exit Sub

        Dim myBrush As Brush
        myBrush = Brushes.White

        If ListBox1.Items(e.Index)(LITEM.ischange) = True Then
            myBrush = Brushes.IndianRed
        End If



        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            e = New DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State Xor DrawItemState.Selected, e.ForeColor,
        Color.DarkGreen)
        End If


        e.DrawBackground()


        e.Graphics.DrawString(ListBox1.Items(e.Index)(LITEM.Name),
        e.Font, myBrush, e.Bounds, StringFormat.GenericDefault)


        e.DrawFocusRectangle()

    End Sub
    Enum LITEM
        ischange = 0
        index = 1
        Name = 2
    End Enum


    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex <> -1 Then
            _OBJECTNUM = ListBox1.SelectedItem(1)

            LoadData()
        End If
    End Sub


    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.KeyUp
        LISTFILTER = TextBox2.Text

        LoadList()
        PaletDraw()
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        LoadList()
        PaletDraw()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If isload = True Then
            Dim v1, v2 As Integer
            v1 = StatusDic(ComboBox3.SelectedIndex).Split(" ")(0).Trim
            v2 = StatusDic(ComboBox3.SelectedIndex).Split(" ")(1).Trim
            ComboBox1.SelectedIndex = v1
            ComboBox2.SelectedIndex = v2
            ProjectSet.saveStatus = False
            Main.nameResetting()


            If ProjectUnitStatusFn1(_OBJECTNUM) = 0 And ProjectUnitStatusFn2(_OBJECTNUM) = 0 Then
                With ComboBox3
                    .ForeColor = ProgramSet.FORECOLOR
                    .BackColor = ProgramSet.BACKCOLOR
                End With
            Else
                With ComboBox3
                    .ForeColor = ProgramSet.FORECOLOR
                    .BackColor = ProgramSet.CHANGECOLOR
                End With
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If isload = True Then
            ProjectUnitStatusFn1(_OBJECTNUM) = ComboBox1.SelectedIndex - UnitStatusFn1(_OBJECTNUM)
            ProjectSet.saveStatus = False
            Main.nameResetting()
            changeColor(ComboBox1, ProjectUnitStatusFn1(_OBJECTNUM))
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If isload = True Then
            ProjectUnitStatusFn2(_OBJECTNUM) = ComboBox2.SelectedIndex - UnitStatusFn2(_OBJECTNUM)
            ProjectSet.saveStatus = False
            Main.nameResetting()
            changeColor(ComboBox2, ProjectUnitStatusFn2(_OBJECTNUM))
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If isload = True Then
            Try
                ProjectDebugID(_OBJECTNUM) = TextBox1.Text - DebugID(_OBJECTNUM)
                ProjectSet.saveStatus = False
                Main.nameResetting()
                changeColor(TextBox1, ProjectDebugID(_OBJECTNUM))
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub 프로젝트저장ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PSaveToolStripMenuItem.Click
        MainTab.Focus()
        Main.저장()
    End Sub

    Private Sub ListBox1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles ListBox1.MouseUp
        If e.Button = MouseButtons.Right Then

            Dim n As Integer = ListBox1.IndexFromPoint(e.X, e.Y)
            If n <> ListBox.NoMatches Then
                ListBox1.SelectedIndex = n
            End If

            ListMenuShow()
        End If
    End Sub



    Private Sub ListBox2_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles ListBox2.MouseUp
        If e.Button = MouseButtons.Right Then

            Dim n As Integer = ListBox2.IndexFromPoint(e.X, e.Y)
            If n <> ListBox.NoMatches Then
                ListBox2.SelectedIndex = n
            End If

            ListBtnMenuShow()
        End If
    End Sub

    Private Sub ColorReset()

        TextBox4.BackColor = ProgramSet.BACKCOLOR
        TextBox4.ForeColor = ProgramSet.FORECOLOR

        TextBox3.BackColor = ProgramSet.BACKCOLOR
        TextBox3.ForeColor = ProgramSet.FORECOLOR
        TextBox5.BackColor = ProgramSet.BACKCOLOR
        TextBox5.ForeColor = ProgramSet.FORECOLOR
        ComboBox4.BackColor = ProgramSet.BACKCOLOR
        ComboBox4.ForeColor = ProgramSet.FORECOLOR

        TextBox10.BackColor = ProgramSet.BACKCOLOR
        TextBox10.ForeColor = ProgramSet.FORECOLOR
        ComboBox10.BackColor = ProgramSet.BACKCOLOR
        ComboBox10.ForeColor = ProgramSet.FORECOLOR

        TextBox11.BackColor = ProgramSet.BACKCOLOR
        TextBox11.ForeColor = ProgramSet.FORECOLOR
        ComboBox9.BackColor = ProgramSet.BACKCOLOR
        ComboBox9.ForeColor = ProgramSet.FORECOLOR


        TextBox7.BackColor = ProgramSet.BACKCOLOR
        TextBox7.ForeColor = ProgramSet.FORECOLOR
        TextBox6.BackColor = ProgramSet.BACKCOLOR
        TextBox6.ForeColor = ProgramSet.FORECOLOR
        TextBox9.BackColor = ProgramSet.BACKCOLOR
        TextBox9.ForeColor = ProgramSet.FORECOLOR
        TextBox8.BackColor = ProgramSet.BACKCOLOR
        TextBox8.ForeColor = ProgramSet.FORECOLOR



        ComboBox6.BackColor = ProgramSet.BACKCOLOR
        ComboBox6.ForeColor = ProgramSet.FORECOLOR
        ComboBox5.BackColor = ProgramSet.BACKCOLOR
        ComboBox5.ForeColor = ProgramSet.FORECOLOR
        ComboBox7.BackColor = ProgramSet.BACKCOLOR
        ComboBox7.ForeColor = ProgramSet.FORECOLOR
        ComboBox8.BackColor = ProgramSet.BACKCOLOR
        ComboBox8.ForeColor = ProgramSet.FORECOLOR

        ComboBox11.BackColor = ProgramSet.BACKCOLOR
        ComboBox11.ForeColor = ProgramSet.FORECOLOR
        ComboBox12.BackColor = ProgramSet.BACKCOLOR
        ComboBox12.ForeColor = ProgramSet.FORECOLOR

        ListBox2.BackColor = ProgramSet.BACKCOLOR
        ListBox2.ForeColor = ProgramSet.FORECOLOR

        ReqList.BackColor = ProgramSet.BACKCOLOR
        ReqList.ForeColor = ProgramSet.FORECOLOR
    End Sub
    Private Sub 테마설정TToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ThemeSetTToolStripMenuItem.Click
        ThemeSetForm.ShowDialog()
        ColorReset()
        LoadData()
    End Sub

    Private Sub 초기화ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem1.Click
        Select Case ActiveControl.Name
            Case "ComboBox3"
                ProjectUnitStatusFn1(_OBJECTNUM) = 0
                ProjectUnitStatusFn2(_OBJECTNUM) = 0
            Case "ComboBox1"
                ProjectUnitStatusFn1(_OBJECTNUM) = 0
            Case "ComboBox2"
                ProjectUnitStatusFn2(_OBJECTNUM) = 0
            Case "TextBox1"
                ProjectDebugID(_OBJECTNUM) = 0
        End Select
        LoadData()
    End Sub


    Private Sub 오브젝트초기화ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObjResetToolStripMenuItem.Click
        reset()
    End Sub

    Private Sub 초기화ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem.Click
        reset()
    End Sub
    Private Sub reset()
        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.SelectedItem(LITEM.ischange) = False
        End If
        Select Case TAB_INDEX
            Case 0
                ProjectUnitStatusFn1(_OBJECTNUM) = 0
                ProjectUnitStatusFn2(_OBJECTNUM) = 0
                ProjectDebugID(_OBJECTNUM) = 0
            Case 1
                ProjectBtnUSE(_OBJECTNUM) = False
            Case 2, 3, 4, 5, 6
                ProjectRequireDataUSE(TAB_INDEX - 2)(_OBJECTNUM) = 0

        End Select


        GroupBox4.Visible = False
        GroupBox5.Visible = False
        GroupBox6.Visible = False
        GroupBox7.Visible = False

        Panel3.Visible = False

        ProjectSet.saveStatus = False
        Main.nameResetting()
        LoadData()
    End Sub

    Private Sub 오브젝트복사ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObjectCopyToolStripMenuItem.Click
        TCopy()
    End Sub
    Private Sub 복사ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        TCopy()
    End Sub
    Private Sub TCopy()
        Dim str As String = ""
        Select Case TAB_INDEX
            Case 0
                str = ProjectUnitStatusFn1(_OBJECTNUM) + UnitStatusFn1(_OBJECTNUM) & ","
                str = str & ProjectUnitStatusFn2(_OBJECTNUM) + UnitStatusFn2(_OBJECTNUM) & ","
                str = str & ProjectDebugID(_OBJECTNUM) + DebugID(_OBJECTNUM) & ","
            Case 1
                If ProjectBtnUSE(_OBJECTNUM) = True Then
                    Dim tstr As String = ""

                    For i = 0 To ProjectBtnData(_OBJECTNUM).Count - 1
                        With ProjectBtnData(_OBJECTNUM)(i)
                            tstr = tstr & ValueTostring(.pos) & ValueTostring(.icon) _
                                & ValueTostring(.con) & ValueTostring(.act) _
                                & ValueTostring(.conval) & ValueTostring(.actval) _
                                & ValueTostring(.enaStr) & ValueTostring(.disStr) & vbCrLf
                        End With
                    Next
                    str = tstr
                Else
                    Dim tstr As String = ""

                    For i = 0 To BtnData(_OBJECTNUM).Count - 1
                        With BtnData(_OBJECTNUM)(i)
                            tstr = tstr & ValueTostring(.pos) & ValueTostring(.icon) _
                                & ValueTostring(.con) & ValueTostring(.act) _
                                & ValueTostring(.conval) & ValueTostring(.actval) _
                                & ValueTostring(.enaStr) & ValueTostring(.disStr) & vbCrLf
                        End With
                    Next
                    str = tstr
                End If

            Case 2, 3, 4, 5, 6
                Select Case ProjectRequireDataUSE(TAB_INDEX - 2)(_OBJECTNUM)
                    Case 0
                        str = "3"

                        With RequireData(TAB_INDEX - 2)(_OBJECTNUM)
                            If .pos = 0 Then
                                str = "1"
                            Else

                                For i = 0 To .Code.Count - 1
                                    str = str & "," & .Code(i)
                                Next
                            End If


                        End With
                    Case 1
                        str = "1"
                    Case 2
                        str = "2"
                    Case 3
                        str = "3"

                        With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                            For i = 0 To .Code.Count - 1
                                str = str & "," & .Code(i)
                            Next


                        End With
                End Select



                'TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                '    & ValueTostring(.con) & ValueTostring(.act) _
                '    & ValueTostring(.conval) & ValueTostring(.actval) _
                '    & ValueTostring(.enaStr) & ValueTostring(.disStr)

        End Select

        Try
            My.Computer.Clipboard.SetText(str)
        Catch ex As Exception
            My.Computer.Clipboard.Clear()
        End Try
    End Sub

    Private Sub 오브젝트붙여넣기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObjPasteToolStripMenuItem.Click
        TPaste()
    End Sub
    Private Sub 붙여넣기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        TPaste()
    End Sub
    Private Sub TPaste()
        Dim cliptext As String = My.Computer.Clipboard.GetText



        Select Case TAB_INDEX
            Case 0
                Dim valuecount As Integer = cliptext.Split(",").Count - 1
                If valuecount = 3 Then
                    ProjectUnitStatusFn1(_OBJECTNUM) = cliptext.Split(",")(0) - UnitStatusFn1(_OBJECTNUM)
                    ProjectUnitStatusFn2(_OBJECTNUM) = cliptext.Split(",")(1) - UnitStatusFn2(_OBJECTNUM)
                    ProjectDebugID(_OBJECTNUM) = cliptext.Split(",")(2) - DebugID(_OBJECTNUM)


                    LoadData()
                    CheckChange()
                End If
            Case 1

                ProjectBtnUSE(_OBJECTNUM) = True
                ProjectBtnData(_OBJECTNUM).Clear()

                For i = 0 To cliptext.Split(vbCrLf).Count - 1
                    If cliptext.Split(vbCrLf)(i).Trim <> "" Then
                        If cliptext.Split(vbCrLf)(i).Trim.Length = 60 Then
                            ProjectBtnData(_OBJECTNUM).Add(New SBtnDATA)

                            Dim pos As Integer = 1
                            ProjectBtnData(_OBJECTNUM)(i).pos = extratext(2, pos, cliptext.Split(vbCrLf)(i).Trim)
                            ProjectBtnData(_OBJECTNUM)(i).icon = extratext(2, pos, cliptext.Split(vbCrLf)(i).Trim)
                            ProjectBtnData(_OBJECTNUM)(i).con = extratext(4, pos, cliptext.Split(vbCrLf)(i).Trim)
                            ProjectBtnData(_OBJECTNUM)(i).act = extratext(4, pos, cliptext.Split(vbCrLf)(i).Trim)
                            ProjectBtnData(_OBJECTNUM)(i).conval = extratext(2, pos, cliptext.Split(vbCrLf)(i).Trim)
                            ProjectBtnData(_OBJECTNUM)(i).actval = extratext(2, pos, cliptext.Split(vbCrLf)(i).Trim)
                            ProjectBtnData(_OBJECTNUM)(i).enaStr = extratext(2, pos, cliptext.Split(vbCrLf)(i).Trim)
                            ProjectBtnData(_OBJECTNUM)(i).disStr = extratext(2, pos, cliptext.Split(vbCrLf)(i).Trim)


                        End If
                    End If
                Next

                LoadData()
            Case 2, 3, 4, 5, 6
                Dim codes() As String = cliptext.Split(",")

                Dim type As Integer = cliptext.Split(",")(0)


                ProjectRequireDataUSE(TAB_INDEX - 2)(_OBJECTNUM) = type

                If type = 0 Or type = 3 Then
                    With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                        .Code.Clear()

                        For i = 1 To codes.Count - 1
                            .Code.Add(codes(i))
                        Next
                    End With

                End If

                LoadData()
        End Select
        ProjectSet.saveStatus = False
        Main.nameResetting()
    End Sub

    Private Sub ListBtnMenuShow()
        Dim cliptext As String = My.Computer.Clipboard.GetText()
        '복사된 데이터가 옳은 데이터일 경우.

        If cliptext.Length = 60 Then
            btnmenuPASTE.Enabled = True
        Else
            btnmenuPASTE.Enabled = False
        End If



        If ListBox2.SelectedIndex <> -1 Then
            btnmenuCUT.Enabled = True
            btnmenuCOPY.Enabled = True
            btnmenuDELETE.Enabled = True


            If cliptext.Length = 60 Then
                btnmenuDUMP.Enabled = True
            Else
                btnmenuDUMP.Enabled = False
            End If
        Else
            btnmenuCUT.Enabled = False
            btnmenuCOPY.Enabled = False
            btnmenuDELETE.Enabled = False
            btnmenuDUMP.Enabled = False
        End If

        BtnlistMenu.Show()
        BtnlistMenu.Location = MousePosition
    End Sub
    Private Sub ListMenuShow()
        Dim cliptext As String = My.Computer.Clipboard.GetText()


        Try
            PasteToolStripMenuItem.Enabled = False
            Select Case TAB_INDEX
                Case 0
                    Dim valuecount As Integer = cliptext.Split(",").Count - 1
                    If valuecount = 3 Then
                        PasteToolStripMenuItem.Enabled = True
                    End If
                Case 1
                    Dim valuecount As Integer = cliptext.Length


                    For i = 0 To cliptext.Split(vbCrLf).Count - 1
                        If cliptext.Split(vbCrLf)(i).Trim <> "" Then
                            If cliptext.Split(vbCrLf)(i).Trim.Length <> 60 Then
                                PasteToolStripMenuItem.Enabled = False
                                Exit For
                            Else
                                PasteToolStripMenuItem.Enabled = True
                            End If
                        End If

                    Next

                    'If valuecount Mod 62 = 0 Then

                    'End If
                Case 2 To 6
                    Dim values() As String = cliptext.Split(",")

                    For i = 0 To values.Count - 2
                        Try
                            Dim temp As Integer = CInt(values(i))
                            PasteToolStripMenuItem.Enabled = True
                        Catch ex As Exception
                            PasteToolStripMenuItem.Enabled = False
                            Exit For
                        End Try
                    Next
            End Select
        Catch ex As Exception
            PasteToolStripMenuItem.Enabled = False
        End Try

        ListMenu.Show()
        ListMenu.Location = MousePosition
    End Sub

    Private Sub 편집ToolStripMenuItem_DropDownOpening(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.DropDownOpening
        Dim cliptext As String = My.Computer.Clipboard.GetText()


        Try
            ObjPasteToolStripMenuItem.Enabled = False
            Select Case TAB_INDEX
                Case 0
                    Dim valuecount As Integer = cliptext.Split(",").Count - 1
                    If valuecount = 3 Then
                        ObjPasteToolStripMenuItem.Enabled = True
                    End If
                Case 1
                    Dim valuecount As Integer = cliptext.Length


                    For i = 0 To cliptext.Split(vbCrLf).Count - 1
                        If cliptext.Split(vbCrLf)(i).Trim <> "" Then
                            If cliptext.Split(vbCrLf)(i).Trim.Length <> 60 Then
                                ObjPasteToolStripMenuItem.Enabled = False
                                Exit For
                            Else
                                ObjPasteToolStripMenuItem.Enabled = True
                            End If
                        End If

                    Next
                Case 2 To 6
                    Dim values() As String = cliptext.Split(",")

                    For i = 0 To values.Count - 2
                        Try
                            Dim temp As Integer = CInt(values(i))
                            ObjPasteToolStripMenuItem.Enabled = True
                        Catch ex As Exception
                            ObjPasteToolStripMenuItem.Enabled = False
                            Exit For
                        End Try
                    Next
            End Select
        Catch ex As Exception
            ObjPasteToolStripMenuItem.Enabled = False
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If isload = True Then
            ProjectBtnUSE(_OBJECTNUM) = Not CheckBox1.Checked
            Panel2.Enabled = ProjectBtnUSE(_OBJECTNUM)

            'GroupBox4.Visible = ProjectBtnUSE(_OBJECTNUM)
            'GroupBox5.Visible = ProjectBtnUSE(_OBJECTNUM)
            'GroupBox6.Visible = ProjectBtnUSE(_OBJECTNUM)
            'GroupBox7.Visible = ProjectBtnUSE(_OBJECTNUM)


            ProjectBtnData(_OBJECTNUM).Clear()

            For i = 0 To BtnData(_OBJECTNUM).Count - 1
                Dim tbtn As New SBtnDATA



                With BtnData(_OBJECTNUM)(i)
                    tbtn.icon = .icon
                    tbtn.pos = .pos
                    tbtn.con = .con
                    tbtn.act = .act
                    tbtn.conval = .conval
                    tbtn.actval = .actval
                    tbtn.enaStr = .enaStr
                    tbtn.disStr = .disStr
                End With
                ProjectBtnData(_OBJECTNUM).Add(tbtn)
            Next

            LoadData()

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then

                    Try
                        ProjectBtnData(_OBJECTNUM)(btnnum).pos = TextBox3.Text
                    Catch ex As Exception
                        TextBox3.Text = 65535
                        ProjectBtnData(_OBJECTNUM)(btnnum).pos = 65535
                    End Try
                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr)
                    End With

                End If
                PreviewDraw()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub
    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then
                    Try
                        Dim test As UInteger = TextBox5.Text

                        Try
                            ProjectBtnData(_OBJECTNUM)(btnnum).icon = TextBox5.Text
                        Catch ex As Exception
                            TextBox5.Text = 65535
                            ProjectBtnData(_OBJECTNUM)(btnnum).icon = 65535
                        End Try


                        With ProjectBtnData(_OBJECTNUM)(btnnum)
                            Try
                                ComboBox4.SelectedIndex = TextBox5.Text
                                PictureBox1.Image = DatEditForm.ICONILIST.Images(.icon) '방어구 아이콘
                            Catch ex As Exception
                                ComboBox4.SelectedIndex = -1
                                PictureBox1.Image = DatEditForm.ICONILIST.Images(4)
                            End Try

                            TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                            & ValueTostring(.con) & ValueTostring(.act) _
                            & ValueTostring(.conval) & ValueTostring(.actval) _
                            & ValueTostring(.enaStr) & ValueTostring(.disStr)
                        End With

                        namechange = True
                        Try
                            ListBox2.Items(ListBox2.SelectedIndex) = ComboBox4.SelectedItem
                        Catch ex As Exception
                            ListBox2.Items(ListBox2.SelectedIndex) = "None"
                        End Try
                        namechange = False
                    Catch ex As Exception
                    End Try


                End If
                PreviewDraw()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then

                    Try
                        ProjectBtnData(_OBJECTNUM)(btnnum).icon = ComboBox4.SelectedIndex 'TextBox5.Text
                    Catch ex As Exception
                        ComboBox4.SelectedIndex = -1
                        ProjectBtnData(_OBJECTNUM)(btnnum).icon = 65535
                    End Try


                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        Try
                            TextBox5.Text = ComboBox4.SelectedIndex
                            PictureBox1.Image = DatEditForm.ICONILIST.Images(.icon) '방어구 아이콘
                        Catch ex As Exception
                            ComboBox4.SelectedIndex = -1
                            PictureBox1.Image = DatEditForm.ICONILIST.Images(4)
                        End Try

                        TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr)
                    End With

                    namechange = True
                    ListBox2.Items(ListBox2.SelectedIndex) = ComboBox4.SelectedItem
                    namechange = False
                End If

                PreviewDraw()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub



    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then
                    Try
                        Dim test As UInteger = TextBox10.Text
                        Try
                            ProjectBtnData(_OBJECTNUM)(btnnum).enaStr = TextBox10.Text
                        Catch ex As Exception
                            TextBox10.Text = 65535
                            ProjectBtnData(_OBJECTNUM)(btnnum).enaStr = 65535
                        End Try

                        With ProjectBtnData(_OBJECTNUM)(btnnum)
                            Try
                                ComboBox10.SelectedIndex = TextBox10.Text
                            Catch ex As Exception
                                ComboBox10.SelectedIndex = -1
                            End Try

                            TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                            & ValueTostring(.con) & ValueTostring(.act) _
                            & ValueTostring(.conval) & ValueTostring(.actval) _
                            & ValueTostring(.enaStr) & ValueTostring(.disStr)

                        End With
                    Catch ex As Exception

                    End Try
                End If
                PreviewDraw()
                CodeRead()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub ComboBox10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox10.SelectedIndexChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then

                    Try
                        ProjectBtnData(_OBJECTNUM)(btnnum).enaStr = ComboBox10.SelectedIndex 'TextBox5.Text
                    Catch ex As Exception
                        ComboBox10.SelectedIndex = -1
                        ProjectBtnData(_OBJECTNUM)(btnnum).enaStr = 65535
                    End Try


                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        Try
                            TextBox10.Text = ComboBox10.SelectedIndex
                        Catch ex As Exception
                            ComboBox10.SelectedIndex = -1
                        End Try

                        TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr)
                    End With

                End If
                PreviewDraw()
                CodeRead()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub




    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then
                    Try
                        Dim test As UInteger = TextBox11.Text
                        Try
                            ProjectBtnData(_OBJECTNUM)(btnnum).disStr = TextBox11.Text
                        Catch ex As Exception
                            TextBox11.Text = 65535
                            ProjectBtnData(_OBJECTNUM)(btnnum).disStr = 65535
                        End Try

                        With ProjectBtnData(_OBJECTNUM)(btnnum)
                            Try
                                ComboBox9.SelectedIndex = TextBox11.Text
                            Catch ex As Exception
                                ComboBox9.SelectedIndex = -1
                            End Try

                            TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                            & ValueTostring(.con) & ValueTostring(.act) _
                            & ValueTostring(.conval) & ValueTostring(.actval) _
                            & ValueTostring(.enaStr) & ValueTostring(.disStr)
                        End With
                    Catch ex As Exception

                    End Try
                End If
                PreviewDraw()
                CodeRead()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub ComboBox9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox9.SelectedIndexChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then

                    Try
                        ProjectBtnData(_OBJECTNUM)(btnnum).disStr = ComboBox9.SelectedIndex 'TextBox5.Text
                    Catch ex As Exception
                        ComboBox9.SelectedIndex = -1
                        ProjectBtnData(_OBJECTNUM)(btnnum).disStr = 65535
                    End Try


                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        Try
                            TextBox11.Text = ComboBox9.SelectedIndex
                        Catch ex As Exception
                            ComboBox9.SelectedIndex = -1
                        End Try

                        TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr)
                    End With

                End If
                PreviewDraw()
                CodeRead()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub



    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then

                    Try
                        ProjectBtnData(_OBJECTNUM)(btnnum).con = conbtnFnc(TextBox7.Text).FucOffset
                    Catch ex As Exception
                    End Try

                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        Try
                            ComboBox6.SelectedIndex = TextBox7.Text
                        Catch ex As Exception
                            ComboBox6.SelectedIndex = -1
                        End Try

                        TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr)



                        ComboBox5.Items.Clear()
                        ComboBox5.Items.Clear()
                        ComboBox5.ResetText()
                        ComboBox5.Text = ""
                        For i = 0 To conbtnFnc.Count - 1
                            If conbtnFnc(i).FucOffset = .con Then
                                If conbtnFnc(i).Code <> -1 Then
                                    ComboBox5.Items.AddRange(CODE(conbtnFnc(i).Code).ToArray)
                                    Try
                                        ComboBox5.SelectedIndex = .conval
                                    Catch ex As Exception
                                        ComboBox5.SelectedIndex = -1
                                    End Try
                                End If
                                Exit For
                            End If
                        Next
                    End With

                End If
                PreviewDraw()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then

                    Try
                        ProjectBtnData(_OBJECTNUM)(btnnum).con = conbtnFnc(ComboBox6.SelectedIndex).FucOffset 'TextBox5.Text
                    Catch ex As Exception
                        'MsgBox("시발?")
                    End Try


                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        Try
                            TextBox7.Text = ComboBox6.SelectedIndex
                        Catch ex As Exception
                            ComboBox6.SelectedIndex = -1
                        End Try

                        TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr)



                        ComboBox5.Items.Clear()
                        ComboBox5.ResetText()
                        ComboBox5.Text = ""
                        For i = 0 To conbtnFnc.Count - 1
                            If conbtnFnc(i).FucOffset = .con Then
                                If conbtnFnc(i).Code <> -1 Then
                                    ComboBox5.Items.AddRange(CODE(conbtnFnc(i).Code).ToArray)
                                    Try
                                        ComboBox5.SelectedIndex = .conval
                                    Catch ex As Exception
                                        ComboBox5.SelectedIndex = -1
                                    End Try
                                End If
                                Exit For
                            End If
                        Next
                    End With



                End If
                PreviewDraw()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then

                    Try
                        ProjectBtnData(_OBJECTNUM)(btnnum).act = actbtnFnc(TextBox9.Text).FucOffset
                    Catch ex As Exception
                    End Try

                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        Try
                            ComboBox7.SelectedIndex = TextBox9.Text
                        Catch ex As Exception
                            ComboBox7.SelectedIndex = -1
                        End Try

                        TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr)



                        ComboBox8.Items.Clear()
                        ComboBox8.Text = ""
                        ComboBox8.ResetText()
                        For i = 0 To actbtnFnc.Count - 1
                            If actbtnFnc(i).FucOffset = .act Then
                                If actbtnFnc(i).Code <> -1 Then
                                    ComboBox8.Items.AddRange(CODE(actbtnFnc(i).Code).ToArray)

                                    Try
                                        ComboBox8.SelectedIndex = .actval
                                    Catch ex As Exception
                                        ComboBox8.SelectedIndex = -1
                                    End Try
                                End If
                                Exit For
                            End If
                        Next
                    End With

                End If
                PreviewDraw()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then

                    Try
                        ProjectBtnData(_OBJECTNUM)(btnnum).act = actbtnFnc(ComboBox7.SelectedIndex).FucOffset 'TextBox5.Text
                    Catch ex As Exception
                        'MsgBox("시발?")
                    End Try


                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        Try
                            TextBox9.Text = ComboBox7.SelectedIndex
                        Catch ex As Exception
                            ComboBox7.SelectedIndex = -1
                        End Try

                        TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr)


                        ComboBox8.Items.Clear()
                        ComboBox8.Text = ""
                        ComboBox8.ResetText()
                        For i = 0 To actbtnFnc.Count - 1
                            If actbtnFnc(i).FucOffset = .act Then
                                If actbtnFnc(i).Code <> -1 Then
                                    ComboBox8.Items.AddRange(CODE(actbtnFnc(i).Code).ToArray)

                                    Try
                                        ComboBox8.SelectedIndex = .actval
                                    Catch ex As Exception
                                        ComboBox8.SelectedIndex = -1
                                    End Try
                                End If
                                Exit For
                            End If
                        Next
                    End With

                End If
                PreviewDraw()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub




    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then

                    Try
                        ProjectBtnData(_OBJECTNUM)(btnnum).conval = TextBox6.Text
                    Catch ex As Exception
                        TextBox6.Text = 65535
                        ProjectBtnData(_OBJECTNUM)(btnnum).conval = 65535
                    End Try

                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        Try
                            ComboBox5.SelectedIndex = TextBox6.Text
                        Catch ex As Exception
                            ComboBox5.SelectedIndex = -1
                        End Try

                        TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr)
                    End With

                End If
                PreviewDraw()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then

                    Try
                        ProjectBtnData(_OBJECTNUM)(btnnum).conval = ComboBox5.SelectedIndex 'TextBox5.Text
                    Catch ex As Exception
                        ComboBox5.SelectedIndex = -1
                        ProjectBtnData(_OBJECTNUM)(btnnum).conval = 65535
                    End Try


                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        Try
                            TextBox6.Text = ComboBox5.SelectedIndex
                        Catch ex As Exception
                            ComboBox5.SelectedIndex = -1
                        End Try

                        TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr)
                    End With

                End If
                PreviewDraw()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then

                    Try
                        ProjectBtnData(_OBJECTNUM)(btnnum).actval = TextBox8.Text
                    Catch ex As Exception
                        TextBox8.Text = 65535
                        ProjectBtnData(_OBJECTNUM)(btnnum).actval = 65535
                    End Try

                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        Try
                            ComboBox8.SelectedIndex = TextBox8.Text
                        Catch ex As Exception
                            ComboBox8.SelectedIndex = -1
                        End Try

                        TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr)
                    End With

                End If
                PreviewDraw()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            If ListBox2.SelectedIndex <> -1 Then
                If ProjectBtnUSE(_OBJECTNUM) = True Then

                    Try
                        ProjectBtnData(_OBJECTNUM)(btnnum).actval = ComboBox8.SelectedIndex 'TextBox5.Text
                    Catch ex As Exception
                        ComboBox8.SelectedIndex = -1
                        ProjectBtnData(_OBJECTNUM)(btnnum).actval = 65535
                    End Try


                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        Try
                            TextBox8.Text = ComboBox8.SelectedIndex
                        Catch ex As Exception
                            ComboBox8.SelectedIndex = -1
                        End Try

                        TextBox4.Text = ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr)
                    End With

                End If
                PreviewDraw()
            End If
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If isload = True Then
            isload = False
            Dim btnnum As Integer = ListBox2.SelectedIndex

            Dim pos As Integer = 1
            ProjectBtnData(_OBJECTNUM)(btnnum).pos = extratext(2, pos, TextBox4.Text)
            ProjectBtnData(_OBJECTNUM)(btnnum).icon = extratext(2, pos, TextBox4.Text)
            ProjectBtnData(_OBJECTNUM)(btnnum).con = extratext(4, pos, TextBox4.Text)
            ProjectBtnData(_OBJECTNUM)(btnnum).act = extratext(4, pos, TextBox4.Text)
            ProjectBtnData(_OBJECTNUM)(btnnum).conval = extratext(2, pos, TextBox4.Text)
            ProjectBtnData(_OBJECTNUM)(btnnum).actval = extratext(2, pos, TextBox4.Text)
            ProjectBtnData(_OBJECTNUM)(btnnum).enaStr = extratext(2, pos, TextBox4.Text)
            ProjectBtnData(_OBJECTNUM)(btnnum).disStr = extratext(2, pos, TextBox4.Text)


            btndataLoad()
            isload = True

            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub
    Private Function extratext(size As Integer, ByRef ptr As Integer, str As String)
        Dim returnvalue As Integer

        For i = 0 To size - 1
            returnvalue += Mid(str, ptr + 3 * i, 3) * 256 ^ i
        Next


        ptr += size * 3
        Return returnvalue
    End Function

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Or e.KeyChar = vbCr Then 'Char.IsControl(e.KeyChar)
            e.Handled = True
        End If
        'IsDigit : 유니코드문자 10진수 확인
        'e.KeyChar : 누른 키 값 획득
        'IsControl : 유니코드 문자가 제어키인지 확인 (Enter, Escape 등)
        'e.Handled : 이벤트 처리
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


    'Dim cliptext As String = My.Computer.Clipboard.GetText()
    ''복사된 데이터가 옳은 데이터일 경우.

    'If cliptext.Length = 60 Then
    '        btnmenuPASTE.Enabled = True
    '    Else
    '        btnmenuPASTE.Enabled = False
    '    End If



    'If ListBox2.SelectedIndex <> -1 Then
    '        btnmenuCUT.Enabled = True
    '        btnmenuCOPY.Enabled = True
    '        btnmenuDELETE.Enabled = True


    '        If cliptext.Length = 60 Then
    '            btnmenuDUMP.Enabled = True
    '        Else
    '            btnmenuDUMP.Enabled = False
    '        End If
    'Else
    '        btnmenuCUT.Enabled = False
    '        btnmenuCOPY.Enabled = False
    '        btnmenuDELETE.Enabled = False
    '        btnmenuDUMP.Enabled = False
    '    End If

    '    BtnlistMenu.Show()
    '    BtnlistMenu.Location = MousePosition


    Private Sub ListBox2_KeyPress(sender As Object, e As KeyEventArgs) Handles ListBox2.KeyDown
        Dim cliptext As String = My.Computer.Clipboard.GetText()
        If e.Control = True Then
            Select Case e.KeyCode
                Case Keys.C
                    If ListBox2.SelectedIndex <> -1 Then
                        btnCOPY()
                    End If
                Case Keys.X
                    If ListBox2.SelectedIndex <> -1 Then
                        btnCUT()
                    End If
                Case Keys.V
                    If cliptext.Length = 60 Then
                        btnPASTE()
                    End If
                Case Keys.N
                    btnNEW()
                Case Keys.D
                    If cliptext.Length = 60 Then
                        btnDUMP()
                    End If
            End Select
        ElseIf e.KeyCode = Keys.Delete Then
            If ListBox2.SelectedIndex <> -1 Then
                btnDELETE()
            End If
        End If
        'MsgBox(e.KeyCode)
    End Sub

    Private Sub btnDELETE()
        ProjectBtnData(_OBJECTNUM).RemoveAt(ListBox2.SelectedIndex)


        Dim lastsel As Integer = ListBox2.SelectedIndex

        ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)

        If lastsel < ListBox2.Items.Count Then
            ListBox2.SelectedIndex = lastsel
        End If

        PreviewDraw()

        ProjectSet.saveStatus = False
        Main.nameResetting()
    End Sub

    Private Sub btnmenuDELETE_Click(sender As Object, e As EventArgs) Handles btnmenuDELETE.Click
        btnDELETE()
    End Sub


    Private Sub btnCOPY()
        With ProjectBtnData(_OBJECTNUM)(ListBox2.SelectedIndex)
            My.Computer.Clipboard.SetText(ValueTostring(.pos) & ValueTostring(.icon) _
                        & ValueTostring(.con) & ValueTostring(.act) _
                        & ValueTostring(.conval) & ValueTostring(.actval) _
                        & ValueTostring(.enaStr) & ValueTostring(.disStr))
        End With

        ProjectSet.saveStatus = False
        Main.nameResetting()
    End Sub
    Private Sub btnmenuCOPY_Click(sender As Object, e As EventArgs) Handles btnmenuCOPY.Click
        btnCOPY()
    End Sub


    Private Sub btnDUMP()
        Dim cliptext As String = My.Computer.Clipboard.GetText()


        Dim btnnum As Integer = ListBox2.SelectedIndex

        Dim pos As Integer = 1
        ProjectBtnData(_OBJECTNUM)(btnnum).pos = extratext(2, pos, cliptext)
        ProjectBtnData(_OBJECTNUM)(btnnum).icon = extratext(2, pos, cliptext)
        ProjectBtnData(_OBJECTNUM)(btnnum).con = extratext(4, pos, cliptext)
        ProjectBtnData(_OBJECTNUM)(btnnum).act = extratext(4, pos, cliptext)
        ProjectBtnData(_OBJECTNUM)(btnnum).conval = extratext(2, pos, cliptext)
        ProjectBtnData(_OBJECTNUM)(btnnum).actval = extratext(2, pos, cliptext)
        ProjectBtnData(_OBJECTNUM)(btnnum).enaStr = extratext(2, pos, cliptext)
        ProjectBtnData(_OBJECTNUM)(btnnum).disStr = extratext(2, pos, cliptext)

        LoadData()

        ProjectSet.saveStatus = False
        Main.nameResetting()
    End Sub
    Private Sub btnmenuDUMP_Click(sender As Object, e As EventArgs) Handles btnmenuDUMP.Click
        btnDUMP()
    End Sub

    Private Sub btnPASTE()
        Dim cliptext As String = My.Computer.Clipboard.GetText()


        Dim btnnum As Integer = ListBox2.SelectedIndex
        If btnnum = -1 Then
            btnnum = 0
        End If



        Dim pos As Integer = 1

        Dim tempbtn As New SBtnDATA
        tempbtn.pos = extratext(2, pos, cliptext)
        tempbtn.icon = extratext(2, pos, cliptext)
        tempbtn.con = extratext(4, pos, cliptext)
        tempbtn.act = extratext(4, pos, cliptext)
        tempbtn.conval = extratext(2, pos, cliptext)
        tempbtn.actval = extratext(2, pos, cliptext)
        tempbtn.enaStr = extratext(2, pos, cliptext)
        tempbtn.disStr = extratext(2, pos, cliptext)


        ProjectBtnData(_OBJECTNUM).Insert(btnnum, tempbtn)

        LoadData()

        ProjectSet.saveStatus = False
        Main.nameResetting()
    End Sub
    Private Sub btnmenuPASTE_Click(sender As Object, e As EventArgs) Handles btnmenuPASTE.Click
        btnPASTE()
    End Sub


    Private Sub btnCUT()
        btnCOPY()
        btnDELETE()
    End Sub
    Private Sub btnmenuCUT_Click(sender As Object, e As EventArgs) Handles btnmenuCUT.Click
        btnCUT()
    End Sub

    Private Sub btnNEW()
        Dim btnnum As Integer = ListBox2.SelectedIndex
        Dim dialog As DialogResult


        BtnCreateHelperForm.ListBox1.SelectedIndex = 0
        dialog = BtnCreateHelperForm.ShowDialog()


        If dialog = DialogResult.OK And BtnCreateHelperForm.isbtnset = False Then
            Dim tempbtn As New SBtnDATA
            tempbtn.pos = BtnCreateHelperForm.dpos
            tempbtn.icon = BtnCreateHelperForm.dicon
            tempbtn.con = BtnCreateHelperForm.dcon
            tempbtn.act = BtnCreateHelperForm.dact
            tempbtn.conval = BtnCreateHelperForm.dconval
            tempbtn.actval = BtnCreateHelperForm.dactval
            tempbtn.enaStr = BtnCreateHelperForm.denaStr
            tempbtn.disStr = BtnCreateHelperForm.ddisStr



            If btnnum = -1 Then
                ProjectBtnData(_OBJECTNUM).Add(tempbtn)
            Else
                ProjectBtnData(_OBJECTNUM).Insert(btnnum, tempbtn)
            End If
        End If

        LoadData()

        ProjectSet.saveStatus = False
        Main.nameResetting()
    End Sub
    Private Sub btnmenuNEW_Click(sender As Object, e As EventArgs) Handles btnmenuNEW.Click
        btnNEW()
    End Sub


    Dim loadned As Boolean = False
    Dim ishotkey As Boolean = False
    Public RawText As String
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
    Private Sub CodeRead()
        Dim btnnum As Integer = ListBox2.SelectedIndex

        If btnnum <> -1 Then


            If ProjectBtnUSE(_OBJECTNUM) = True Then
                With ProjectBtnData(_OBJECTNUM)(btnnum)
                    If CheckBox2.Checked Then
                        If .enaStr <> 0 Then
                            Try
                                RawText = stat_txt(.enaStr - 1)
                            Catch ex As Exception
                                RawText = "Error"
                            End Try
                        Else
                            RawText = "Error"
                        End If
                    Else
                        If .disStr <> 0 Then
                            Try
                                RawText = stat_txt(.disStr - 1)
                            Catch ex As Exception
                                RawText = "Error"
                            End Try
                        Else
                            RawText = "Error"
                        End If
                    End If
                End With

            End If

            loadned = False
            ishotkey = False
            'ComboBox1.SelectedIndex = 0
            'ComboBox2.SelectedIndex = 0

            RichTextBox2.ResetText()
            RichTextBox2.SuspendLayout()
            RichTextBox2.SelectionColor = Color.FromArgb(184, 184, 232)
            PictureBox2.Visible = False


            For i = 0 To CheckStringlen() - 1


                If i = 1 Then
                    If GetChar(i).Length > 1 Then
                        Dim opcode As Byte = Replace(Replace(GetChar(i), "<", ""), ">", "")
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



                        If GetChar(0).Length > 1 Then
                            Dim arc As Byte = Replace(Replace(GetChar(0), "<", ""), ">", "")


                        Else

                            '97 ~ 122

                            '65 ~ 90
                            If 65 <= Asc(GetChar(0)) And Asc(GetChar(0)) <= 90 Then
                                loadned = False
                                RawText = GetChar(0).ToLower & Mid(RawText, 1 + GetChar(0).Length)

                                loadned = True
                            End If
                        End If

                        ishotkey = True

                    Else
                        RichTextBox2.AppendText(GetChar(0) & GetChar(1))
                    End If
                End If


                If i > 1 Then
                    If GetChar(i).Length < 2 Then
                        RichTextBox2.AppendText(GetChar(i))
                    Else
                        Dim opcode As Byte = Replace(Replace(GetChar(i), "<", ""), ">", "")

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

        End If
    End Sub
    Dim lastcolor As Color

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

        CodeRead()
    End Sub

    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        Dim btnnum As Integer = ListBox2.SelectedIndex
        If ProjectBtnData(_OBJECTNUM)(btnnum).enaStr <> 0 Then

            Dim value As UInteger = ProjectBtnData(_OBJECTNUM)(btnnum).enaStr - 1

            If value >= 0 Then
                Dim dialog As DialogResult
                StatTextForm.stringNum = value
                dialog = StatTextForm.ShowDialog()
                If dialog = DialogResult.OK Then

                    StatTextAdd(value, StatTextForm.RawText)
                    Loadstattxt()
                    LoadData()

                    'ComboBox32.Items(TextBox58.Text) = StatTextForm.RawText
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim btnnum As Integer = ListBox2.SelectedIndex

        If ProjectBtnData(_OBJECTNUM)(btnnum).disStr <> 0 Then
            Dim value As UInteger = ProjectBtnData(_OBJECTNUM)(btnnum).disStr - 1

            If value >= 0 Then
                Dim dialog As DialogResult
                StatTextForm.stringNum = value
                dialog = StatTextForm.ShowDialog()
                If dialog = DialogResult.OK Then

                    StatTextAdd(value, StatTextForm.RawText)
                    Loadstattxt()
                    LoadData()

                    'ComboBox32.Items(TextBox58.Text) = StatTextForm.RawText
                End If
            End If
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If isload = True Then
            ProjectRequireDataUSE(TAB_INDEX - 2)(_OBJECTNUM) = 0

            LoadData()
            CheckChange()
            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If isload = True Then
            ProjectRequireDataUSE(TAB_INDEX - 2)(_OBJECTNUM) = 1

            LoadData()
            CheckChange()
            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If isload = True Then
            ProjectRequireDataUSE(TAB_INDEX - 2)(_OBJECTNUM) = 2

            LoadData()
            CheckChange()
            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If isload = True Then
            ProjectRequireDataUSE(TAB_INDEX - 2)(_OBJECTNUM) = 3


            ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM).Code.Clear()
            For i = 0 To RequireData(TAB_INDEX - 2)(_OBJECTNUM).Code.Count - 1
                ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM).Code.Add(RequireData(TAB_INDEX - 2)(_OBJECTNUM).Code(i))
            Next

            LoadData()
            CheckChange()
            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub BtnPreview_Click(sender As Object, e As MouseEventArgs) Handles BtnPreview.MouseClick
        '38,36
        Dim btnpos As Integer
        For i = 0 To 2
            If (i * 38 <= e.X) And (e.X < (i * 38 + 38)) Then
                btnpos = i
            End If
        Next
        For i = 0 To 2
            If (i * 38 <= e.Y) And (e.Y < (i * 36 + 36)) Then
                btnpos += i * 3
            End If
        Next
        btnpos += 1
        For i = 0 To ProjectBtnData(_OBJECTNUM).Count - 1
            If btnpos = ProjectBtnData(_OBJECTNUM)(i).pos And ListBox2.SelectedIndex <> i Then
                ListBox2.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

    Dim changeban As Boolean = False
    Dim comboban As Boolean = False

    Private Sub LoadReqData()
        If ReqList.SelectedIndex <> -1 Then
            comboban = True
            ComboBox11.Visible = True
            With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                Select Case ReqListData(ReqList.SelectedIndex).Count
                    Case 1
                        If .Code(ReqListData(ReqList.SelectedIndex)(0)) > &HFF Then
                            ComboBox12.Visible = False
                            Dim opcode As UInt16 = .Code(ReqListData(ReqList.SelectedIndex)(0)) - &HFF00
                            If opcode <> &HFF Then
                                ComboBox11.SelectedIndex = opcode - 1
                            Else
                                ComboBox11.SelectedIndex = 39
                            End If
                        Else
                            ComboBox12.Visible = True
                            ComboBox11.SelectedIndex = 38
                            ComboBox12.Items.Clear()
                            ComboBox12.Items.AddRange(CODE(DTYPE.units).ToArray)
                            ComboBox12.SelectedIndex = .Code(ReqListData(ReqList.SelectedIndex)(0))
                        End If
                    Case 2
                        Dim opcode As UInt16 = .Code(ReqListData(ReqList.SelectedIndex)(0)) - &HFF00

                        ComboBox12.Visible = True

                        ComboBox11.SelectedIndex = opcode - 1

                        ComboBox12.Items.Clear()

                        If opcode = 37 Then
                            ComboBox12.Items.AddRange(CODE(DTYPE.techdata).ToArray)
                            ComboBox12.Items.RemoveAt(44)
                        Else
                            ComboBox12.Items.AddRange(CODE(DTYPE.units).ToArray)
                        End If

                        ComboBox12.SelectedIndex = .Code(ReqListData(ReqList.SelectedIndex)(1))

                End Select
            End With
            comboban = False
        Else
            ComboBox11.Visible = False
            ComboBox12.Visible = False

        End If
    End Sub
    Private Sub ListBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ReqList.SelectedIndexChanged
        If changeban = False Then
            comboban = True
            If ReqList.SelectedIndex <> -1 Then
                LoadReqData()
            Else
                ComboBox11.Visible = False
                ComboBox12.Visible = False
            End If




            comboban = False

            Buttonrefresh()
        End If

        'Try
        '    ComboBox11.SelectedItem = 0
        '    RichTextBox1.Text = ""
        '    For i = 0 To ReqListData(ReqList.SelectedIndex).Count - 1
        '        RichTextBox1.AppendText(parsereqCode(ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM))) & vbCrLf)
        '    Next
        'Catch ex As Exception

        'End Try
        'ListData.Add(New List(Of UInt16))
        'ListData(ReqList.Items.Count - 1).Add(Reqdata.Code(index))
    End Sub

    Private Sub Buttonrefresh()
        If ReqList.SelectedIndex <> -1 Then
            If ReqList.SelectedIndex = 0 Then
                Button6.Enabled = False
                Button3.Enabled = True
            Else
                Button6.Enabled = True
                Button3.Enabled = True
            End If

            If ReqList.SelectedIndex >= ReqList.Items.Count - 1 Then
                Button7.Enabled = False
            Else
                Button7.Enabled = True
            End If

        Else
            Button6.Enabled = False
            Button3.Enabled = False
            Button7.Enabled = False
        End If



    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Delete()
    End Sub
    Private Sub Delete()
        changeban = True
        For i = 0 To ReqListData(ReqList.SelectedIndex).Count - 1
            With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                .Code.RemoveAt((ReqListData(ReqList.SelectedIndex)(0)))
            End With
        Next
        Dim selectindex As UInteger = ReqList.SelectedIndex
        ReqList.Items.RemoveAt(ReqList.SelectedIndex)



        Try
            ReqList.SelectedIndex = selectindex
        Catch ex As Exception
            ReqList.SelectedIndex = ReqList.Items.Count - 1
        End Try
        changeban = False


        ReadReqData(ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM), False)

        ProjectSet.saveStatus = False
        Main.nameResetting()
        Buttonrefresh()
        LoadReqData()
    End Sub

    Private Sub up_Click(sender As Object, e As EventArgs) Handles Button6.Click
        changeban = True
        '코드 픽킹.
        Dim selectindex As UInteger = ReqList.SelectedIndex

        Dim tcode As New List(Of UInt16)
        For i = 0 To ReqListData(selectindex).Count - 1
            With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                tcode.Add(.Code(ReqListData(selectindex)(0)))
                .Code.RemoveAt((ReqListData(selectindex)(0)))
            End With
        Next

        Dim tstring As String = ReqList.Items(ReqList.SelectedIndex)
        ReqList.Items.RemoveAt(ReqList.SelectedIndex)

        With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
            .Code.InsertRange(ReqListData(selectindex - 1)(0), tcode)
        End With
        ReqList.Items.Insert(selectindex - 1, tstring)


        Try
            ReqList.SelectedIndex = selectindex - 1
        Catch ex As Exception
            ReqList.SelectedIndex = ReqList.Items.Count - 1
        End Try


        ReadReqData(ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM), False)
        changeban = False

        ProjectSet.saveStatus = False
        Main.nameResetting()
        Buttonrefresh()
    End Sub

    Private Sub down_Click(sender As Object, e As EventArgs) Handles Button7.Click
        changeban = True
        Dim selectindex As UInteger = ReqList.SelectedIndex + 1

        Dim tcode As New List(Of UInt16)
        For i = 0 To ReqListData(selectindex).Count - 1
            With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                tcode.Add(.Code(ReqListData(selectindex)(0)))
                .Code.RemoveAt((ReqListData(selectindex)(0)))
            End With
        Next

        Dim tstring As String = ReqList.Items(selectindex)
        ReqList.Items.RemoveAt(selectindex)

        With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
            .Code.InsertRange(ReqListData(selectindex - 1)(0), tcode)
        End With
        ReqList.Items.Insert(selectindex - 1, tstring)


        Try
            ReqList.SelectedIndex = selectindex
        Catch ex As Exception
            ReqList.SelectedIndex = ReqList.Items.Count - 1
        End Try


        ReadReqData(ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM), False)
        changeban = False

        ProjectSet.saveStatus = False
        Main.nameResetting()
        Buttonrefresh()
    End Sub


    'ReqListData(ReqList.SelectedIndex)의 수를 파악한다.
    '2, 3, 4, 37으로 교체 할 경우
    '그 외
    '39로 교체
    '1번 문장 OldStr
    '2번 문장 Oldstr

    '원본 문장
    Private Sub ModifyReqCode(Opcode As Integer, Value As Integer)
        Dim selectindex As Integer = ReqList.SelectedIndex

        With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
            Dim oldisze As Byte
            Dim newsize As Byte
            Dim oldOpcode As UInt16
            Dim oldValue As UInt16

            If .Code(ReqListData(selectindex)(0)) <= &HFF Then
                oldOpcode = 39
                oldValue = .Code(ReqListData(selectindex)(0))
            Else
                oldOpcode = .Code(ReqListData(selectindex)(0)) - &HFF00
            End If

            oldisze = ReqListData(selectindex).Count

            Select Case Opcode
                Case 2, 3, 4, 37
                    newsize = 2
                Case Else
                    newsize = 1
            End Select

            If Opcode = 40 Then
                Opcode = &HFF
            End If

            If Opcode = 39 Then
                .Code(ReqListData(selectindex)(0)) = 0
            Else
                .Code(ReqListData(selectindex)(0)) = Opcode + &HFF00
            End If


            If oldisze <> newsize Then
                If oldisze = 1 Then
                    If newsize = 2 Then '용량 증가

                        .Code.Insert(ReqListData(selectindex)(0) + 1, 0)

                    End If
                ElseIf oldisze = 2 Then
                    If newsize = 1 Then '용량 감소
                        .Code.RemoveAt(ReqListData(selectindex)(0) + 1)

                    End If
                End If
            End If

            If newsize = 2 Then
                If Opcode = 37 Then
                    If CODE(DTYPE.techdata).Count - 1 > Value Then
                        .Code(ReqListData(selectindex)(0) + 1) = Value
                    Else
                        .Code(ReqListData(selectindex)(0) + 1) = CODE(DTYPE.techdata).Count - 2
                    End If

                Else
                    .Code(ReqListData(selectindex)(0) + 1) = Value
                End If
            ElseIf Opcode = 39 Then
                .Code(ReqListData(selectindex)(0)) = Value

            End If
            'oldValue = .Code(ReqListData(selectindex)(1))









            'changeban = True
            'ReqList.Items(ReqList.SelectedIndex) = ReqList.Items(ReqList.SelectedIndex).ToString.Replace(oldstr, newstr)

            'changeban = False
            GetFilesize()
        End With

    End Sub

    Private Sub ComboBox11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox11.SelectedIndexChanged
        If comboban = False Then



            ModifyReqCode(ComboBox11.SelectedIndex + 1, ComboBox12.SelectedIndex)

            ReadReqData(ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM), False)

            comboban = True
            LoadReqData()
            comboban = False
            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub

    Private Sub ComboBox12_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox12.SelectedIndexChanged
        If comboban = False Then



            ModifyReqCode(ComboBox11.SelectedIndex + 1, ComboBox12.SelectedIndex)

            ReadReqData(ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM), False)

            comboban = True
            LoadReqData()
            comboban = False
            ProjectSet.saveStatus = False
            Main.nameResetting()
        End If
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        NewCode()
    End Sub
    Private Sub NewCode()
        Dim dialog As DialogResult

        dialog = FiregraftRepAddForm.ShowDialog

        If dialog = DialogResult.OK Then
            changeban = True
            Dim selectindex As UInteger
            Dim insertPos As UInteger

            If ReqList.SelectedIndex = -1 Then
                selectindex = 0
                insertPos = 0
            Else
                selectindex = ReqList.SelectedIndex
                insertPos = ReqListData(selectindex)(0)
            End If


            Dim tcode As New List(Of UInt16)
            With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)

                If FiregraftRepAddForm.Opcode = 38 Then
                    tcode.Add(FiregraftRepAddForm.Value)
                ElseIf FiregraftRepAddForm.Opcode = 39 Then
                    tcode.Add(&HFFFF)
                Else
                    tcode.Add(FiregraftRepAddForm.Opcode + &HFF01)
                    If FiregraftRepAddForm.ishavevalue = True Then
                        tcode.Add(FiregraftRepAddForm.Value)
                    End If
                End If
            End With



            With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                .Code.InsertRange(insertPos, tcode)
            End With

            Try
                ReqList.SelectedIndex = selectindex
            Catch ex As Exception
                ReqList.SelectedIndex = ReqList.Items.Count - 1
            End Try


            ReadReqData(ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM), False)
            changeban = False

            LoadReqData()

            ProjectSet.saveStatus = False
            Main.nameResetting()
            Buttonrefresh()
        End If
    End Sub

    Dim drag As Boolean = False
    Private Sub ReqList_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles ReqList.MouseUp
        If e.Button = MouseButtons.Right Then

            Dim n As Integer = ReqList.IndexFromPoint(e.X, e.Y)
            If n <> ListBox.NoMatches Then
                Try
                    ReqList.SelectedIndex = n
                Catch ex As Exception
                    ReqList.SelectedIndex = -1
                End Try
            End If

            ListreqMenuShow()
        ElseIf e.Button = MouseButtons.Left Then
            drag = False
            If dragList <> ReqList.SelectedIndex Then
                changeban = True
                Dim selectindex As UInteger
                Dim destindex As UInteger



                If dragList > ReqList.SelectedIndex Then '이건 잘됨.
                    selectindex = dragList
                    destindex = ReqList.SelectedIndex

                    Dim tcode As New List(Of UInt16)
                    For i = 0 To ReqListData(selectindex).Count - 1
                        With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                            tcode.Add(.Code(ReqListData(selectindex)(0)))
                            .Code.RemoveAt((ReqListData(selectindex)(0)))
                        End With
                    Next

                    Dim tstring As String = ReqList.Items(selectindex)
                    ReqList.Items.RemoveAt(selectindex)

                    With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                        .Code.InsertRange(ReqListData(destindex)(0), tcode)
                    End With
                    ReqList.Items.Insert(destindex, tstring)


                    Try
                        ReqList.SelectedIndex = destindex
                    Catch ex As Exception
                        ReqList.SelectedIndex = ReqList.Items.Count - 1
                    End Try


                    ReadReqData(ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM), False)
                    changeban = False
                Else
                    Dim delcount As Byte = ReqListData(selectindex).Count

                    selectindex = dragList
                    destindex = ReqList.SelectedIndex - ReqListData(selectindex).Count + 1

                    Dim tcode As New List(Of UInt16)
                    For i = 0 To ReqListData(selectindex).Count - 1
                        With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                            tcode.Add(.Code(ReqListData(selectindex)(0)))
                            .Code.RemoveAt((ReqListData(selectindex)(0)))
                        End With
                    Next

                    Dim tstring As String = ReqList.Items(selectindex)
                    ReqList.Items.RemoveAt(selectindex)

                    With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                        .Code.InsertRange(ReqListData(destindex)(0) + ReqListData(destindex).Count - 1, tcode)
                    End With
                    ReqList.Items.Insert(destindex, tstring)


                    Try
                        ReqList.SelectedIndex = destindex
                    Catch ex As Exception
                        ReqList.SelectedIndex = ReqList.Items.Count - 1
                    End Try


                    ReadReqData(ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM), False)
                    changeban = False
                End If



                ProjectSet.saveStatus = False
                Main.nameResetting()
                Buttonrefresh()

            End If
        End If

    End Sub





    Dim dragList As Integer
    Private Sub ReqList_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles ReqList.MouseDown
        If e.Button = MouseButtons.Left Then
            drag = True


            dragList = ReqList.SelectedIndex
        End If
    End Sub

    Private Sub ListreqMenuShow()
        Dim cliptext As String = My.Computer.Clipboard.GetText()
        '복사된 데이터가 옳은 데이터일 경우.

        'If cliptext.Length = 60 Then
        '    btnmenuPASTE.Enabled = True
        'Else
        '    btnmenuPASTE.Enabled = False
        'End If



        If ReqList.SelectedIndex <> -1 Then
            ToolStripMenuItem6.Enabled = True
            ToolStripMenuItem4.Enabled = True
            ToolStripMenuItem3.Enabled = True
        Else
            ToolStripMenuItem6.Enabled = False
            ToolStripMenuItem4.Enabled = False
            ToolStripMenuItem3.Enabled = False
        End If


        Dim iscanpaste As Boolean = True
        For i = 0 To cliptext.Split(",").Count - 1
            Try
                Dim a = CUShort(cliptext.Split(",")(i))
            Catch ex As Exception
                iscanpaste = False
                Exit For
            End Try
        Next
        If iscanpaste Then
            ToolStripMenuItem5.Enabled = True
        Else
            ToolStripMenuItem5.Enabled = False
        End If

        'ToolStripMenuItem5.Enabled = True



        ReqMenu.Show()
        ReqMenu.Location = MousePosition
    End Sub



    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        Delete()
    End Sub



    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        NewCode()
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        reqCOPY()
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        reqCOPY()
        Delete()
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        reqPASTE()
    End Sub

    Private Sub reqCOPY()
        Dim str As String = ""

        With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
            If ReqListData(ReqList.SelectedIndex).Count = 1 Then
                str = .Code(ReqListData(ReqList.SelectedIndex)(0))
            Else
                str = .Code(ReqListData(ReqList.SelectedIndex)(0)) & "," & .Code(ReqListData(ReqList.SelectedIndex)(1))
            End If



        End With



        Try
            My.Computer.Clipboard.SetText(str)
        Catch ex As Exception
            My.Computer.Clipboard.Clear()
        End Try
    End Sub

    Private Sub reqPASTE()
        Dim cliptext As String = My.Computer.Clipboard.GetText


        Try
            With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                Dim tcode As New List(Of UInt16)


                Dim selectindex As Integer = ReqList.SelectedIndex
                Dim insertPos As UInteger

                insertPos = ReqListData(selectindex)(0)

                For i = 0 To cliptext.Split(",").Count - 1

                    tcode.Add(cliptext.Split(",")(i))


                Next

                With ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM)
                    .Code.InsertRange(insertPos, tcode)
                End With


            End With

        Catch ex As Exception
        End Try
        changeban = True
        ReadReqData(ProjectRequireData(TAB_INDEX - 2)(_OBJECTNUM), False)
        changeban = False
        LoadReqData()

        ProjectSet.saveStatus = False
        Main.nameResetting()
    End Sub

    Private Sub ReqList_KeyPress(sender As Object, e As KeyEventArgs) Handles ReqList.KeyDown
        'Dim cliptext As String = My.Computer.Clipboard.GetText()
        ''복사된 데이터가 옳은 데이터일 경우.

        ''If cliptext.Length = 60 Then
        ''    btnmenuPASTE.Enabled = True
        ''Else
        ''    btnmenuPASTE.Enabled = False
        ''End If



        'If ReqList.SelectedIndex <> -1 Then
        '    ToolStripMenuItem6.Enabled = True
        '    ToolStripMenuItem4.Enabled = True
        '    ToolStripMenuItem3.Enabled = True
        'Else
        '    ToolStripMenuItem6.Enabled = False
        '    ToolStripMenuItem4.Enabled = False
        '    ToolStripMenuItem3.Enabled = False
        'End If


        'Dim iscanpaste As Boolean = True
        'For i = 0 To cliptext.Split(",").Count - 1
        '    Try
        '        Dim a = CUShort(cliptext.Split(",")(i))
        '    Catch ex As Exception
        '        iscanpaste = False
        '        Exit For
        '    End Try
        'Next
        'If iscanpaste Then
        '    ToolStripMenuItem5.Enabled = True
        'Else
        '    ToolStripMenuItem5.Enabled = False
        'End If



        Dim cliptext As String = My.Computer.Clipboard.GetText()
        If e.Control = True Then
            Select Case e.KeyCode
                Case Keys.C
                    If ReqList.SelectedIndex <> -1 Then
                        reqCOPY()
                    End If
                Case Keys.X
                    If ReqList.SelectedIndex <> -1 Then
                        reqCOPY()
                        Delete()
                    End If
                Case Keys.V
                    Dim iscanpaste As Boolean = True
                    For i = 0 To cliptext.Split(",").Count - 1
                        Try
                            Dim a = CUShort(cliptext.Split(",")(i))
                        Catch ex As Exception
                            iscanpaste = False
                            Exit For
                        End Try
                    Next
                    If iscanpaste Then
                        reqPASTE()
                    End If

                Case Keys.N
                    NewCode()
            End Select
        ElseIf e.KeyCode = Keys.Delete Then
            If ReqList.SelectedIndex <> -1 Then
                Delete()
            End If
        End If
        'MsgBox(e.KeyCode)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        For i = 0 To ProjectRequireDataUSE(TAB_INDEX - 2).Count - 1
            ProjectRequireDataUSE(TAB_INDEX - 2)(i) = 0
        Next
        LoadList()
        'LoadReqData()

        ProjectSet.saveStatus = False
        Main.nameResetting()
        Buttonrefresh()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        For i = 0 To ProjectRequireDataUSE(TAB_INDEX - 2).Count - 1
            ProjectRequireDataUSE(TAB_INDEX - 2)(i) = 1
        Next
        LoadList()
        'LoadReqData()

        ProjectSet.saveStatus = False
        Main.nameResetting()
        Buttonrefresh()

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        For i = 0 To ProjectRequireDataUSE(TAB_INDEX - 2).Count - 1
            ProjectRequireDataUSE(TAB_INDEX - 2)(i) = 2
        Next
        LoadList()
        'LoadReqData()

        ProjectSet.saveStatus = False
        Main.nameResetting()
        Buttonrefresh()
    End Sub
    Private Sub CodeViewerShow(listN As DTYPE, Button As Object)
        Try
            CodeViewer.listNum = listN
            Dim btnnum As Integer = ListBox2.SelectedIndex
            CodeViewer.Value = ProjectBtnData(_OBJECTNUM)(btnnum).icon

            CodeViewer.mode = "Fire"
            CodeViewer.ObjectName = Button.Tag
            CodeViewer.ObjectNum = _OBJECTNUM
            CodeViewer.ObjectTab = TAB_INDEX
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