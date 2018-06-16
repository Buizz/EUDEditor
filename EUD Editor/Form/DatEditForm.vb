Imports System.IO
Imports System.Drawing
Imports System.Threading


Public Enum DTYPE
    units = 0
    weapons = 1
    flingy = 2
    sprites = 3
    images = 4
    upgrades = 5
    techdata = 6
    orders = 7
    sfxdata = 8
    portdata = 9
    grpfile = 10
    btnunit = 11
    icon = 12
End Enum

Public Class DatEditForm
    Private mpq As New SFMpq
    Public loadSTATUS As Boolean
    Public TAB_INDEX As Integer = 0

    Private frameNum As UInteger
    Private LISTFILTER As String
    Private Const Stringisnot As String = "FailToLoadString"

    Public TabSelectindex As New List(Of Integer)
    Public Tabfilfer As New List(Of String)
    Private LastSelectTab As Integer

    Private _unusedColor As Color = Color.LightGray
    Public _OBJECTNUM As Integer


    Private Sub SELECTLIST(index As Integer)
        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(i)(1) = index Then
                ListBox1.SelectedIndex = i
                Exit Sub
            End If
        Next
        If CheckBox5.Checked = True And ListBox1.Items.Count <> 0 Then
            ListBox1.SelectedIndex = 0
            Exit Sub
        End If

        _OBJECTNUM = index
        LoadData()
    End Sub
    Public Function GetUnitIndex(index As Integer)
        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(i)(1) = index Then
                Return i
            End If
        Next
        Return 0
    End Function


    Private Sub DatEditForm_Closed(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        Timer1.Enabled = False
        IScriptPlayer.Enabled = False
        e.Cancel = True
        Me.Hide()

        Main.Activate()

        My.Forms.Main.Visible = True
    End Sub


    Public FristRun As Boolean = False
    Public FristRunOpenOrders As Boolean = False

    Public Sub ReloadCHK()
        If FristRun = True Then
            ComboBox23.Items.Clear()
            ComboBox23.Items.Add("Default")
            ComboBox23.Items.AddRange(ProjectSet.CHKSTRING.ToArray)
            LoadData()
            ListDraw()
            PaletDraw()
        End If
    End Sub

    Public Sub ColorReset()
        ListBox9.BackColor = ProgramSet.BACKCOLOR
        ListBox9.ForeColor = ProgramSet.FORECOLOR


        ListView9.BackColor = ProgramSet.BACKCOLOR
        ListView10.BackColor = ProgramSet.BACKCOLOR

        NumericUpDown1.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown2.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown3.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown4.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown5.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown6.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown7.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown8.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown9.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown10.ForeColor = ProgramSet.FORECOLOR


        NumericUpDown13.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown14.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown15.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown16.ForeColor = ProgramSet.FORECOLOR



        With ListBox8
            .ForeColor = ProgramSet.FORECOLOR
            .BackColor = ProgramSet.BACKCOLOR
        End With
        With ListBox2
            .ForeColor = ProgramSet.FORECOLOR
            .BackColor = ProgramSet.BACKCOLOR
        End With
        With ListBox3
            .ForeColor = ProgramSet.FORECOLOR
            .BackColor = ProgramSet.BACKCOLOR
        End With
        With ListBox4
            .ForeColor = ProgramSet.FORECOLOR
            .BackColor = ProgramSet.BACKCOLOR
        End With
        With ListBox5
            .ForeColor = ProgramSet.FORECOLOR
            .BackColor = ProgramSet.BACKCOLOR
        End With
    End Sub
    Private Sub DatEditForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)
        Lan.SetMenu(Me, MenuStrip1)
        Lan.SetMenu(Me, ListMenu)

        ColorReset()


        If FristRun = False Then

            For i = 0 To MainTAB.TabCount - 1
                TabSelectindex.Add(0)
                Tabfilfer.Add("")
            Next

            'loadthread = New Thread(AddressOf showloadform)
            'loadthread.Start()


            NumericUpDown1.Maximum = Integer.MaxValue \ 256
            NumericUpDown1.Minimum = Integer.MinValue \ 256
            LoadComboBoxAndList()

            ListDraw()
            PaletDraw()

            If ListBox1.Items.Count <> 0 Then
                ListBox1.SelectedIndex = 0
            End If


            'loadthread.Abort()
            Activate()
            FristRun = True

        End If
        LoadData()
    End Sub


    Public Sub SoundPlay(name As String)
        Try
            If ListBox6.Items.Contains(name.ToLower) Then
                SoundPlayer(MPQlib.ReadFile(name))
            Else
                SoundPlayer(mpq.ReaddatFile(name))
            End If
        Catch ex As Exception
            MsgBox(Lan.GetText(Me.Name, "FailPlayWave"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
        End Try
    End Sub
    Public Function GRPHock(fileNum As Integer)

        If GRPEditorUsingDATA(fileNum) <> "" Then
            If InStr(GRPEditorUsingDATA(fileNum), "unit\") = 1 Then
                Dim filebuff() As Byte
                filebuff = mpq.ReaddatFile(GRPEditorUsingDATA(fileNum))
                Return filebuff
            Else
                Dim filebuff As String
                filebuff = GRPEditorUsingDATA(fileNum)
                Return filebuff
            End If
        Else
            Dim grpnum As Integer = DatEditDATA(DTYPE.images).ReadValue("GRP File", fileNum)
            Dim filebuff() As Byte
            filebuff = mpq.ReaddatFile("unit\" & CODE(DTYPE.grpfile)(grpnum).Replace("<0>", ""))
            Return filebuff
        End If
    End Function


    Private Sub 초기화ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 초기화ToolStripMenuItem.Click
        Dim index As Integer = _OBJECTNUM
        For i = 0 To DatEditDATA(MainTAB.SelectedIndex).projectdata.Count - 1
            Try
                If (index - DatEditDATA(MainTAB.SelectedIndex).keyINFO(i).VarStart) >= 0 Then
                    DatEditDATA(MainTAB.SelectedIndex).projectdata(i)(index - DatEditDATA(MainTAB.SelectedIndex).keyINFO(i).VarStart) = 0

                End If
            Catch ex As Exception
            End Try
        Next

        If MainTAB.SelectedIndex = 0 Then
            Dim value As Integer = -1 + DatEditDATA(DTYPE.units).ReadValue("Unit Map String", _OBJECTNUM)
            If value >= 0 Then
                ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & ProjectSet.CHKSTRING(value)
            Else
                ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & CODE(DTYPE.units)(_OBJECTNUM)
            End If
        End If

        LoadData()
        ListBox1.Refresh()
    End Sub


    Private Sub ObjectCopy()
        Dim datasource As String = ""

        Dim index As Integer = _OBJECTNUM
        For i = 0 To DatEditDATA(TAB_INDEX).projectdata.Count - 1
            Try
                Try
                    If (index - DatEditDATA(MainTAB.SelectedIndex).keyINFO(i).VarStart) >= 0 Then
                        datasource = datasource & DatEditDATA(TAB_INDEX).ReadValueNum(i, index) & ","
                    Else
                        datasource = datasource & "Null" & ","
                    End If
                Catch ex As Exception
                    datasource = datasource & "Null" & ","
                End Try




            Catch ex As Exception
                MsgBox(DatEditDATA(TAB_INDEX).keyDic.Values(i))
            End Try
        Next


        My.Computer.Clipboard.SetText(datasource)

        'My.Computer.Clipboard.SetText(MainTAB.SelectedIndex & "," & index)
    End Sub

    Private Sub ObjectLoad(_text As String)
        Dim cliptext As String = _text

        Dim newvalue() As String = cliptext.Split(",")

        Dim index As Integer = _OBJECTNUM

        Dim Toindex = cliptext.Split(",")(1)
        'Dim i =
        For i = 0 To DatEditDATA(MainTAB.SelectedIndex).projectdata.Count - 1
            Try

                If (index - DatEditDATA(MainTAB.SelectedIndex).keyINFO(i).VarStart) >= 0 And newvalue(i) <> "Null" Then 'And (Toindex - DatEditDAT(MainTAB.SelectedIndex).keyINFO(i).VarStart) >= 0 Then
                    Try
                        DatEditDATA(MainTAB.SelectedIndex).WriteValueNum(i, index, newvalue(i))
                    Catch ex As Exception

                    End Try


                    'DatEditDAT(MainTAB.SelectedIndex).WriteValueNum(i, index, DatEditDAT(MainTAB.SelectedIndex).ReadValueNum(i, Toindex))


                End If
            Catch ex As Exception
                MsgBox(DatEditDATA(MainTAB.SelectedIndex).keyDic.Values(i))
            End Try
        Next

        If MainTAB.SelectedIndex = 0 Then
            Dim value As Long = -1 + DatEditDATA(DTYPE.units).ReadValue("Unit Map String", _OBJECTNUM)
            If value >= 0 Then
                Try
                    ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & ProjectSet.CHKSTRING(value)
                Catch ex As Exception
                    ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & Stringisnot
                End Try
            Else
                ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & CODE(DTYPE.units)(_OBJECTNUM)
            End If
        End If
        LoadData()
        ListBox1.Refresh()
    End Sub
    Private Sub ObjectPaste()
        ObjectLoad(My.Computer.Clipboard.GetText())
    End Sub

    Private Sub 복사ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 복사ToolStripMenuItem.Click
        ObjectCopy()
    End Sub
    Private Sub 붙여넣기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 붙여넣기ToolStripMenuItem.Click
        ObjectPaste()
    End Sub

    Private Sub 초기화ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem1.Click
        Try
            Dim key As String = ActiveControl.Tag

            If key = "OrdersFlag:" Then
                'ListView8.Items(0).Checked
                For i = 0 To 11

                    DatEditDATA(DTYPE.orders).projectdata(i + 1) _
                    (_OBJECTNUM - DatEditDATA(DTYPE.orders).keyINFO(i).VarStart) = 0
                Next
                'DatEditDAT(TAB_INDEX).WriteToCHECKBOXLIST(ListView8, _OBJECTNUM)
            Else
                DatEditDATA(MainTAB.SelectedIndex).projectdata(DatEditDATA(MainTAB.SelectedIndex).keyDic(key))(_OBJECTNUM - DatEditDATA(MainTAB.SelectedIndex).keyINFO(DatEditDATA(MainTAB.SelectedIndex).keyDic(key)).VarStart) = 0
            End If

            LoadData()
            ListBox1.Refresh()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub 오브젝트초기화ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObjectResetToolStripMenuItem.Click
        Dim index As Integer = _OBJECTNUM
        'MsgBox()
        For i = 0 To DatEditDATA(MainTAB.SelectedIndex).projectdata.Count - 1
            Try
                If (index - DatEditDATA(MainTAB.SelectedIndex).keyINFO(i).VarStart) >= 0 Then
                    DatEditDATA(MainTAB.SelectedIndex).projectdata(i)(index - DatEditDATA(MainTAB.SelectedIndex).keyINFO(i).VarStart) = 0

                End If
            Catch ex As Exception
            End Try
        Next

        If MainTAB.SelectedIndex = 0 Then
            Dim value As Integer = -1 + DatEditDATA(DTYPE.units).ReadValue("Unit Map String", _OBJECTNUM)
            If value >= 0 Then
                ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & ProjectSet.CHKSTRING(value)
            Else
                ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & CODE(DTYPE.units)(_OBJECTNUM)
            End If
        End If

        LoadData()
        ListBox1.Refresh()
    End Sub

    Private Sub 오브젝트복사ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObjectCopyToolStripMenuItem.Click
        ObjectCopy()
    End Sub

    Private Sub 오브젝트붙여넣기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObjectPasteToolStripMenuItem.Click
        Dim cliptext As String = My.Computer.Clipboard.GetText()

        Dim valuecount As Integer = cliptext.Split(",").Count - 1
        Try
            If valuecount = DatEditDATA(MainTAB.SelectedIndex).projectdata.Count Then
                ObjectPaste()
            End If
        Catch ex As Exception
        End Try
    End Sub





    Private Sub ListMenuShow()
        Dim cliptext As String = My.Computer.Clipboard.GetText()

        Dim valuecount As Integer = cliptext.Split(",").Count - 1
        Dim temp As String = cliptext.Split(",")(0)
        Try
            If valuecount = DatEditDATA(MainTAB.SelectedIndex).projectdata.Count Then
                붙여넣기ToolStripMenuItem.Enabled = True
            Else
                붙여넣기ToolStripMenuItem.Enabled = False
            End If
        Catch ex As Exception
            붙여넣기ToolStripMenuItem.Enabled = False
        End Try

        ListMenu.Show()
        ListMenu.Location = MousePosition
    End Sub

    Private Sub 편집ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.DropDownOpened
        Dim cliptext As String = My.Computer.Clipboard.GetText()

        Dim valuecount As Integer = cliptext.Split(",").Count - 1
        Dim temp As String = cliptext.Split(",")(0)
        Try
            If valuecount = DatEditDATA(MainTAB.SelectedIndex).projectdata.Count Then
                ObjectPasteToolStripMenuItem.Enabled = True
            Else
                ObjectPasteToolStripMenuItem.Enabled = False
            End If
        Catch ex As Exception
            ObjectPasteToolStripMenuItem.Enabled = False
        End Try
    End Sub
    Private Sub 편집ToolStripMenuItem_Close(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.DropDownClosed
        ObjectPasteToolStripMenuItem.Enabled = True
    End Sub

    Private Sub 데이터로내보내기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 데이터로내보내기ToolStripMenuItem.Click
        ToData()
    End Sub

    Private Sub 데이터불러오기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 데이터불러오기ToolStripMenuItem.Click
        DataLoad()
    End Sub

    Private Sub 데이터로내보내기ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DataExportToolStripMenuItem1.Click
        ToData()
    End Sub

    Private Sub 데이터불러오기ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DataImportToolStripMenuItem1.Click
        DataLoad()
    End Sub
    Private Sub ToData()
        Dim dialog As DialogResult
        SaveFileDialog1.FileName = ListBox1.SelectedItem(0)

        dialog = SaveFileDialog1.ShowDialog()
        If dialog = DialogResult.OK Then
            Dim filestream As New FileStream(SaveFileDialog1.FileName, FileMode.Create)
            Dim streamwriter As New StreamWriter(filestream)


            Dim datasource As String = ""

            Dim index = _OBJECTNUM
            For i = 0 To DatEditDATA(TAB_INDEX).projectdata.Count - 1
                Try
                    Try
                        If (index - DatEditDATA(MainTAB.SelectedIndex).keyINFO(i).VarStart) >= 0 Then
                            datasource = datasource & DatEditDATA(TAB_INDEX).ReadValueNum(i, index) & ","
                        Else
                            datasource = datasource & "Null" & ","
                        End If
                    Catch ex As Exception
                        datasource = datasource & "Null" & ","
                    End Try
                Catch ex As Exception
                    MsgBox(DatEditDATA(TAB_INDEX).keyDic.Values(i))
                End Try
            Next


            streamwriter.Write(datasource)


            streamwriter.Close()
            filestream.Close()
        End If
    End Sub
    Private Sub DataLoad()
        Dim dialog As DialogResult
        dialog = OpenFileDialog1.ShowDialog()
        If dialog = DialogResult.OK Then
            Dim filestream As New FileStream(OpenFileDialog1.FileName, FileMode.Open)
            Dim streamreader As New StreamReader(filestream)
            Dim tempstring As String = streamreader.ReadToEnd
            Try
                If DatEditDATA(MainTAB.SelectedIndex).projectdata.Count = (tempstring.Split(",").Count - 1) Then
                    ObjectLoad(tempstring)
                Else
                    Throw New Exception
                End If
            Catch ex As Exception
                MsgBox(Lan.GetText(Me.Name, "invalidData"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            End Try



            streamreader.Close()
            filestream.Close()
        End If
    End Sub








    Public Sub RefreshForm()
        Dim oldselectindex As Integer = _OBJECTNUM
        ListDraw()
        PaletDraw()

        SELECTLIST(oldselectindex)



    End Sub
    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        RefreshForm()
        ListSelected()
    End Sub


    Private Sub LoadComboBoxFromFile(ByRef Combobox As ComboBox, filename As String)
        filename = My.Application.Info.DirectoryPath & "\Data\Langage\" & My.Settings.Langage & "\" & filename
        Dim file As FileStream = New FileStream(filename, FileMode.Open, FileAccess.Read)
        Dim stream As StreamReader = New StreamReader(file, System.Text.Encoding.Default)

        Combobox.Items.Clear()

        While (stream.EndOfStream = False)
            Combobox.Items.Add(stream.ReadLine)
        End While

        stream.Close()
        file.Close()
    End Sub
    Private Sub LoadListviewFromFile(ByRef Listview As ListView, filename As String)
        filename = My.Application.Info.DirectoryPath & "\Data\Langage\" & My.Settings.Langage & "\" & filename
        Dim file As FileStream = New FileStream(filename, FileMode.Open, FileAccess.Read)
        Dim stream As StreamReader = New StreamReader(file, System.Text.Encoding.Default)

        Listview.Items.Clear()
        While (stream.EndOfStream = False)
            Listview.Items.Add(stream.ReadLine)
        End While

        stream.Close()
        file.Close()
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MainTAB.SelectedIndexChanged
        loadSTATUS = False
        'TabSelectindex(MainTAB.selecti)


        TabSelectindex(LastSelectTab) = _OBJECTNUM
        Tabfilfer(LastSelectTab) = LISTFILTER

        'TabSelectindex.Add(0)
        'Tabfilfer.Add("")
        LISTFILTER = Tabfilfer(MainTAB.SelectedIndex)



        TAB_INDEX = MainTAB.SelectedIndex
        ListDraw()
        PaletDraw()

        Dim IME = TextBox2.ImeMode
        TextBox2.ImeMode = ImeMode.Off
        TextBox2.Text = ""
        TextBox2.ImeMode = IME
        Application.DoEvents()

        'Try
        SELECTLIST(TabSelectindex(MainTAB.SelectedIndex))

        'Catch ex As Exception
        '_OBJECTNUM = 0
        'End Try

        TextBox2.Text = Tabfilfer(MainTAB.SelectedIndex)


        LastSelectTab = MainTAB.SelectedIndex
        loadSTATUS = True
    End Sub

    Private Sub Dat파일불러오기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatFileLoadToolStripMenuItem.Click
        Dim dialog As DialogResult

        dialog = DatFileLoad.ShowDialog

        If dialog = DialogResult.OK Then
            'Dim num As Integer = 
            For i = 0 To DatFileLoad.FileNames.Count - 1
                MainTAB.SelectedIndex = ReadDATAFileFromDat(DatFileLoad.FileNames(i))
            Next


            ListDraw()
            PaletDraw()
        End If
    End Sub

    Private Sub Dat파일저장SToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatFileSaveSToolStripMenuItem.Click
        Dim dialog As DialogResult

        If DatFileSave.FileName = "" Then
            DatFileSave.FileName = DatEditDATA(TAB_INDEX).typeName
        End If

        dialog = DatFileSave.ShowDialog

        If dialog = DialogResult.OK Then
            WriteDATAFileFromDat(DatFileSave.FileName, TAB_INDEX)
        End If
    End Sub

    Private Sub Dat파일모두저장ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatFileAllSaveToolStripMenuItem.Click
        Dim dialog As DialogResult



        dialog = DatFileSaveAll.ShowDialog

        If dialog = DialogResult.OK Then
            For i = 0 To DatEditDATA.Count - 1
                Dim filename As String = Replace(DatFileSaveAll.FileName & "_" & DatEditDATA(i).typeName, ".dat", "")
                WriteDATAFileFromDat(filename & ".dat", i)
            Next
        End If
    End Sub

    Private Sub Dat파일리셋RToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatFileResetRToolStripMenuItem.Click
        Dim Filename As String = My.Application.Info.DirectoryPath & "\Data\" & DatEditDATA(TAB_INDEX).typeName & ".dat"

        ReadDATAFileFromDat(Filename)

        ListDraw()
        PaletDraw()
    End Sub
    Private Sub Dat파일모두리셋ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatFileAllResetToolStripMenuItem.Click
        Dim Filename As String

        For i = 0 To DatEditDATA.Count - 1
            Filename = My.Application.Info.DirectoryPath & "\Data\" & DatEditDATA(i).typeName & ".dat"
            ReadDATAFileFromDat(Filename)
        Next


        ListDraw()
        PaletDraw()
    End Sub

    Private Sub 사용된데이터ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsedDataToolStripMenuItem.Click
        SelectChangeForm.ShowDialog()

        LoadData()
        refreshchangeAll()
    End Sub

    Private Sub 테마설정TToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ThameSetTToolStripMenuItem.Click
        ThemeSetForm.ShowDialog()
        ColorReset()
        LoadData()
    End Sub


    Private Sub 프로젝트저장ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProjectSaveToolStripMenuItem.Click
        MainTAB.Focus()
        Main.저장()
    End Sub
    Private Sub PaletDraw()
        ListView1.BeginUpdate()
        ListView1.Items.Clear()
        Dim flingyNum, SpriteNum, ImageNum As Integer
        Dim size As Integer = ListBox1.Items.Count - 1
        If MainTAB.SelectedIndex = DTYPE.weapons Or MainTAB.SelectedIndex = DTYPE.techdata Or
            MainTAB.SelectedIndex = DTYPE.orders Then
            size -= 1
        End If
        For i = 0 To size
            Dim index As Integer = ListBox1.Items(i)(1)

            ListView1.Items.Add("")
            Dim itemindex As Integer = ListView1.Items.Count - 1
            Select Case MainTAB.SelectedIndex
                Case DTYPE.units
                    flingyNum = DatEditDATA(DTYPE.units).ReadValue("Graphics", index)
                    SpriteNum = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", flingyNum)
                    ImageNum = DatEditDATA(DTYPE.sprites).ReadValue("Image File", SpriteNum)

                    ListView1.LargeImageList = IMAGELIST
                    ListView1.Items(itemindex).ImageIndex = ImageNum
                Case DTYPE.weapons
                    ListView1.LargeImageList = ICONILIST
                    Try
                        ListView1.Items(itemindex).ImageIndex = DatEditDATA(DTYPE.weapons).ReadValue("Icon", index)
                    Catch ex As Exception
                        ListView1.Items(itemindex).ImageIndex = 4
                    End Try
                Case DTYPE.flingy
                    SpriteNum = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", index)
                    ImageNum = DatEditDATA(DTYPE.sprites).ReadValue("Image File", SpriteNum)
                    ListView1.LargeImageList = IMAGELIST
                    ListView1.Items(itemindex).ImageIndex = ImageNum
                Case DTYPE.sprites
                    ImageNum = DatEditDATA(DTYPE.sprites).ReadValue("Image File", index)
                    ListView1.LargeImageList = IMAGELIST
                    ListView1.Items(itemindex).ImageIndex = ImageNum
                Case DTYPE.images
                    ListView1.LargeImageList = IMAGELIST
                    ListView1.Items(itemindex).ImageIndex = index
                Case DTYPE.upgrades
                    ListView1.LargeImageList = ICONILIST
                    Try
                        ListView1.Items(itemindex).ImageIndex = DatEditDATA(DTYPE.upgrades).ReadValue("Icon", index)
                    Catch ex As Exception
                        ListView1.Items(itemindex).ImageIndex = 4
                    End Try
                Case DTYPE.techdata
                    ListView1.LargeImageList = ICONILIST
                    Try
                        ListView1.Items(itemindex).ImageIndex = DatEditDATA(DTYPE.techdata).ReadValue("Icon", index)
                    Catch ex As Exception
                        ListView1.Items(itemindex).ImageIndex = 4
                    End Try
                Case DTYPE.orders
                    ListView1.LargeImageList = ICONILIST
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

    Private Sub refreshchangeAll()
        For i = 0 To ListBox1.Items.Count - 1
            ListBox1.Items(i)(2) = DatEditDATA(TAB_INDEX).CheckChangeAll(i)
        Next

        ListBox1.Refresh()
    End Sub
    Private Sub ListDraw()
        ListBox1.BeginUpdate()

        Dim listNum As Integer = MainTAB.SelectedIndex
        Dim index As Integer = 0


        ListBox1.Items.Clear()

        For i = 0 To CODE(listNum).Count - 1
            index = i

            Dim temp, stra, strb As String
            Dim temp2(2) As String

            temp = CODE(listNum)(i)
            If temp <> "None" Then
                If listNum = DTYPE.units Then
                    If DatEditDATA(DTYPE.units).ReadValue("Unit Map String", index) = 0 Then
                        temp2(0) = temp
                    Else
                        Try
                            temp2(0) = ProjectSet.CHKSTRING(-1 + DatEditDATA(DTYPE.units).ReadValue("Unit Map String", i)) & " (" & temp & ")" 'ProjectSet.UNITSTR(index)
                        Catch ex As Exception
                            temp2(0) = Stringisnot 'ProjectSet.UNITSTR(index)
                        End Try

                    End If
                    temp2(1) = index
                    temp2(2) = DatEditDATA(DTYPE.units).CheckChangeAll(i)
                Else
                    temp2(0) = temp
                    temp2(1) = index
                    temp2(2) = DatEditDATA(listNum).CheckChangeAll(i)
                End If
                temp2(0) = "[" & Format(i, "000") & "]- " & temp2(0)


                Select Case TAB_INDEX
                    Case DTYPE.sfxdata
                        Dim value As Integer = DatEditDATA(DTYPE.sfxdata).ReadValue("Sound File", i)

                        If ListBox6.Items.Contains("sound\" & ComboBox53.Items(value).ToString.ToLower) Then

                            temp2(2) = 1
                        End If
                    Case DTYPE.images
                        If GRPEditorUsingDATA(i) <> "" Then
                            temp2(2) = 1
                        End If
                End Select



                If TAB_INDEX = DTYPE.portdata Then
                    MPQlib.ReadListfile(ListBox7)
                    readlistsmk()

                    Dim value As Integer = DatEditDATA(DTYPE.portdata).ReadValue("Portrait File", i)

                    For k = 0 To ListBox7.Items.Count - 1
                        If InStr(ListBox7.Items(k), "portrait\" & ComboBox54.Items(value).ToString.ToLower) <> 0 Then
                            temp2(2) = 1
                        End If
                    Next
                End If


                stra = temp2(0).ToLower
                If LISTFILTER <> "" Then
                    strb = LISTFILTER.ToLower
                Else
                    strb = ""
                End If
                If InStr(stra, strb) <> 0 Then
                    If CheckBox5.Checked = False Then
                        ListBox1.Items.Add(temp2)
                    Else
                        If temp2(2) = 1 Then
                            ListBox1.Items.Add(temp2)
                        End If
                    End If
                End If
            End If
        Next

        'SELECTLIST(_OBJECTNUM)
        If ListBox1.SelectedIndex = -1 And ListBox1.Items.Count <> 0 Then
            ListBox1.SelectedIndex = 0
        End If



        ListBox1.EndUpdate()
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

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.Click, ListView1.ItemSelectionChanged
        Try
            SELECTLIST(ListView1.SelectedItems(0).Tag)
        Catch ex As Exception
        End Try
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

    Private Sub ListSelected()
        If ListBox1.Items.Count <> 0 Then
            _OBJECTNUM = ListBox1.SelectedItem(1)


            LoadData()
        Else
            _OBJECTNUM = 0
        End If
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        ListSelected()
    End Sub
    Private Sub ListBox1_DrawItem(ByVal sender As Object,
 ByVal e As System.Windows.Forms.DrawItemEventArgs) _
 Handles ListBox1.DrawItem
        If (e.Index < 0) Then Exit Sub
        ' Draw the background of the ListBox control for each item.
        e.DrawBackground()

            ' Define the default color of the brush as black.
            Dim myBrush As Brush

            ' Determine the color of the brush to draw each item based on   
            ' the index of the item to draw.
            myBrush = Brushes.White
            'rect.Height -= 1
            If ListBox1.Items(e.Index)(2) = 1 Then
                'ToolStripStatusLabel1.Text = e.Index
                myBrush = Brushes.IndianRed
            End If


            e.Graphics.DrawString(ListBox1.Items(e.Index)(0).ToString,
        e.Font, myBrush, e.Bounds, StringFormat.GenericDefault)


        e.DrawFocusRectangle()
    End Sub


    Private Sub ListBox8_DrawItem(ByVal sender As Object,
 ByVal e As System.Windows.Forms.DrawItemEventArgs) _
 Handles ListBox8.DrawItem
        If ListBox8.SelectedIndex <> -1 Then
            ' Draw the background of the ListBox control for each item.
            e.DrawBackground()

            ' Define the default color of the brush as black.
            Dim myBrush As New SolidBrush(ListBox8.ForeColor)

            ' Determine the color of the brush to draw each item based on   
            ' the index of the item to draw.
            'rect.Height -= 1

            If ListBox7.Items.Contains("portrait\" & ListBox8.Items(e.Index).ToString) Then
                myBrush = Brushes.IndianRed
                ListBox1.Items(ListBox1.SelectedIndex)(2) = 1
            End If


            e.Graphics.DrawString(ListBox8.Items(e.Index).ToString,
        e.Font, myBrush, e.Bounds, StringFormat.GenericDefault)


            e.DrawFocusRectangle()
        End If
    End Sub



    Public Sub Loadstattxt()
        If ProjectSet.UsedSetting(8) = True Then
            For i = 0 To stattextdic.Count - 1
                stat_txt(stattextdic.Keys(i)) = stattextdic(stattextdic.Keys(i))
            Next

        End If


        ComboBox22.Items.Clear()
        ComboBox22.Items.AddRange(stat_txt)
        For i = 0 To 1300
            ComboBox22.Items.RemoveAt(0)
        Next
        ComboBox32.Items.Clear()
        ComboBox32.Items.Add("None")
        ComboBox32.Items.AddRange(stat_txt)
        ComboBox33.Items.Clear()
        ComboBox33.Items.Add("None")
        ComboBox33.Items.AddRange(stat_txt)
        ComboBox42.Items.Clear()
        ComboBox42.Items.Add("None")
        ComboBox42.Items.AddRange(stat_txt)
        ComboBox44.Items.Clear()
        ComboBox44.Items.Add("None")
        ComboBox44.Items.AddRange(stat_txt)
        ComboBox50.Items.Clear()
        ComboBox50.Items.Add("None")
        ComboBox50.Items.AddRange(stat_txt)




        FireGraftForm.ComboBox10.Items.Clear()
        FireGraftForm.ComboBox9.Items.Clear()

        FireGraftForm.ComboBox10.Items.Add("None")
        FireGraftForm.ComboBox9.Items.Add("None")

        FireGraftForm.ComboBox10.Items.AddRange(stat_txt)
        FireGraftForm.ComboBox9.Items.AddRange(stat_txt)
    End Sub
    Private Sub LoadComboBoxAndList()
        Loadstattxt()


        ComboBox1.Items.Clear()
        ComboBox1.Items.AddRange(CODE(DTYPE.upgrades).ToArray)


        ComboBox4.Items.Clear()
        ComboBox4.Items.AddRange(CODE(DTYPE.weapons).ToArray)
        ComboBox5.Items.Clear()
        ComboBox5.Items.AddRange(CODE(DTYPE.weapons).ToArray)


        LoadComboBoxFromFile(ComboBox6, "UnitSize.txt")
        LoadComboBoxFromFile(ComboBox20, "ElevationLevels.txt")
        LoadComboBoxFromFile(ComboBox21, "UnitDirection.txt")

        LoadListviewFromFile(ListView2, "SpecialAbilityFlags.txt")
        LoadListviewFromFile(ListView3, "Races.txt")
        LoadListviewFromFile(ListView4, "FlingyFlags.txt")
        LoadListviewFromFile(ListView5, "StareditAvailabilityFlags.txt")
        LoadListviewFromFile(ListView6, "StareditGroupFlags.txt")

        LoadListviewFromFile(ListView9, "OrdersFlag.txt")

        ComboBox23.Items.Clear()
        ComboBox23.Items.Add("Default")
        ComboBox23.Items.AddRange(ProjectSet.CHKSTRING.ToArray)


        ComboBox7.Items.Clear()
        ComboBox7.Items.AddRange(CODE(DTYPE.units).ToArray)
        ComboBox7.Items.Add("None")
        ComboBox8.Items.Clear()
        ComboBox8.Items.AddRange(CODE(DTYPE.units).ToArray)
        ComboBox8.Items.Add("None")
        ComboBox9.Items.Clear()
        ComboBox9.Items.AddRange(CODE(DTYPE.units).ToArray)
        ComboBox9.Items.Add("None")



        ComboBox10.Items.Clear()
        ComboBox10.Items.AddRange(CODE(DTYPE.sfxdata).ToArray)

        ComboBox11.Items.Clear()
        ComboBox11.Items.AddRange(CODE(DTYPE.sfxdata).ToArray)
        ComboBox12.Items.Clear()
        ComboBox12.Items.AddRange(CODE(DTYPE.sfxdata).ToArray)

        ComboBox13.Items.Clear()
        ComboBox13.Items.AddRange(CODE(DTYPE.sfxdata).ToArray)
        ComboBox14.Items.Clear()
        ComboBox14.Items.AddRange(CODE(DTYPE.sfxdata).ToArray)

        ComboBox15.Items.Clear()
        ComboBox15.Items.AddRange(CODE(DTYPE.sfxdata).ToArray)
        ComboBox16.Items.Clear()
        ComboBox16.Items.AddRange(CODE(DTYPE.sfxdata).ToArray)



        ComboBox17.Items.Clear()
        ComboBox17.Items.AddRange(CODE(DTYPE.portdata).ToArray)
        ComboBox18.Items.Clear()
        ComboBox18.Items.AddRange(CODE(DTYPE.images).ToArray)
        ComboBox19.Items.Clear()
        ComboBox19.Items.AddRange(CODE(DTYPE.flingy).ToArray)





        ComboBox24.Items.Clear()
        ComboBox24.Items.AddRange(CODE(DTYPE.orders).ToArray)
        ComboBox25.Items.Clear()
        ComboBox25.Items.AddRange(CODE(DTYPE.orders).ToArray)
        ComboBox26.Items.Clear()
        ComboBox26.Items.AddRange(CODE(DTYPE.orders).ToArray)
        ComboBox27.Items.Clear()
        ComboBox27.Items.AddRange(CODE(DTYPE.orders).ToArray)
        ComboBox28.Items.Clear()
        ComboBox28.Items.AddRange(CODE(DTYPE.orders).ToArray)

        LoadComboBoxFromFile(ComboBox29, "Rightclick.txt")
        LoadListviewFromFile(ListView7, "AIInternal.txt")


        LoadComboBoxFromFile(ComboBox3, "DamTypes.txt")
        LoadComboBoxFromFile(ComboBox30, "Explosions.txt")
        ComboBox31.Items.Clear()
        ComboBox31.Items.AddRange(CODE(DTYPE.techdata).ToArray)
        ComboBox2.Items.Clear()
        ComboBox2.Items.AddRange(CODE(DTYPE.upgrades).ToArray)


        LoadListviewFromFile(ListView8, "TargetType.txt")
        LoadComboBoxFromFile(ComboBox34, "Behaviours.txt")


        ComboBox35.Items.Clear()
        ComboBox35.Items.AddRange(CODE(DTYPE.flingy).ToArray)
        LoadComboBoxFromFile(ComboBox36, "Icon.txt")

        ComboBox37.Items.Clear()
        ComboBox37.Items.AddRange(CODE(DTYPE.sprites).ToArray)


        LoadComboBoxFromFile(ComboBox38, "FlingyControl.txt")

        ComboBox39.Items.Clear()
        ComboBox39.Items.AddRange(CODE(DTYPE.images).ToArray)

        ComboBox40.Items.Clear()
        ComboBox40.Items.AddRange(CODE(DTYPE.images).ToArray)
        For i = 0 To 560
            ComboBox40.Items.RemoveAt(0)
        Next
        While (ComboBox40.Items.Count > 256)
            ComboBox40.Items.RemoveAt(256)
        End While

        LoadComboBoxFromFile(ComboBox41, "Icon.txt")



        LoadComboBoxFromFile(ComboBox45, "Icon.txt")



        LoadComboBoxFromFile(ComboBox43, "Races.txt")
        ComboBox43.Items.Add("All")
        LoadComboBoxFromFile(ComboBox46, "Races.txt")
        ComboBox46.Items.Add("All")

        ComboBox47.Items.Clear()
        ComboBox47.Items.AddRange(CODE(DTYPE.weapons).ToArray)
        ComboBox48.Items.Clear()
        ComboBox48.Items.AddRange(CODE(DTYPE.techdata).ToArray)
        ComboBox49.Items.Clear()
        ComboBox49.Items.AddRange(CODE(DTYPE.orders).ToArray)


        LoadComboBoxFromFile(ComboBox51, "Animations.txt")
        LoadComboBoxFromFile(ComboBox52, "Icon.txt")



        ComboBox53.Items.Clear()
        ComboBox53.Items.Add("None")
        ComboBox53.Items.AddRange(sfxdata)


        ComboBox54.Items.Clear()
        ComboBox54.Items.Add("None")
        ComboBox54.Items.AddRange(portdata)

        LoadComboBoxFromFile(ComboBox55, "DrawList.txt")
        LoadComboBoxFromFile(ComboBox56, "Remapping.txt")


        LoadComboBoxFromFile(ComboBox57, "GRPfile.txt")
        LoadComboBoxFromFile(ComboBox58, "GRPfile.txt")
        LoadComboBoxFromFile(ComboBox59, "GRPfile.txt")
        LoadComboBoxFromFile(ComboBox60, "GRPfile.txt")
        LoadComboBoxFromFile(ComboBox61, "GRPfile.txt")
        LoadComboBoxFromFile(ComboBox62, "GRPfile.txt")

        LoadListviewFromFile(ListView10, "ImagesFlags.txt")

        LoadComboBoxFromFile(ComboBox65, "GRPfile.txt")
        LoadComboBoxFromFile(ComboBox64, "IscriptIDList.txt")
    End Sub


    Public Sub LoadData()
        'Timer1.Enabled = False
        loadSTATUS = False
        Select Case MainTAB.SelectedIndex
            Case DTYPE.units
                UnitDataLOAD()
            Case DTYPE.weapons
                WeaponDataLOAD()
            Case DTYPE.flingy
                FlingyDataLOAD()
            Case DTYPE.sprites
                SpritesDataLOAD()
            Case DTYPE.images
                ImagesDataLOAD()
            Case DTYPE.upgrades
                UpgradeDataLOAD()
            Case DTYPE.techdata
                TechDataLOAD()
            Case DTYPE.orders
                OrdersDataLOAD()
                FristRunOpenOrders = True
            Case DTYPE.sfxdata
                SfxdataDataLOAD()
            Case DTYPE.portdata
                PortdataDataLOAD()
        End Select

        loadSTATUS = True
        HPloadSTATUS = True
    End Sub

    Private Sub UnitDataLOAD()
        MPQlib.ReadListfile(ListBox6)
        readlist()

        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.SelectedItem(2) = DatEditDATA(DTYPE.units).CheckChangeAll(_OBJECTNUM)
            Label24.Text = ListBox1.SelectedItem(0) & " (" & CODE(DTYPE.units)(_OBJECTNUM) & ")"
        End If



        Dim t_Iconnum As Integer



        Try
            PictureBox4.Image = ICONILIST.Images(_OBJECTNUM)
        Catch ex As Exception

        End Try


        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox4, _OBJECTNUM) 'HP
        If TextBox4.Text > Integer.MaxValue Then
            NumericUpDown1.Value = (TextBox4.Text - 4294967296) \ 256
        Else
            NumericUpDown1.Value = TextBox4.Text \ 256
        End If
        NumericUpDown1.BackColor = TextBox4.BackColor

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox6, _OBJECTNUM) '쉴드
        DatEditDATA(DTYPE.units).ReadToCHECKBOX(CheckBox1, _OBJECTNUM)
        If CheckBox1.Checked = False Then
            TextBox6.BackColor = _unusedColor
        End If

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox7, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox8, _OBJECTNUM) '방어구

        Try
            t_Iconnum = DatEditDATA(DTYPE.upgrades).ReadValue("Icon", TextBox8.Text)
            PictureBox1.Image = ICONILIST.Images(t_Iconnum) '방어구 아이콘
        Catch ex As Exception
            PictureBox1.Image = ICONILIST.Images(4)
        End Try
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox1, _OBJECTNUM) '방어구 콤보박스


        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox5, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox9, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox10, _OBJECTNUM)
        NumericUpDown2.Value = TextBox10.Text / 24
        NumericUpDown2.BackColor = TextBox10.BackColor

        DatEditDATA(DTYPE.units).ReadToCHECKBOX(CheckBox2, _OBJECTNUM)



        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox11, _OBJECTNUM)
        Try
            t_Iconnum = DatEditDATA(DTYPE.weapons).ReadValue("Icon", TextBox11.Text)
            PictureBox2.Image = ICONILIST.Images(t_Iconnum) '지상공격 아이콘
        Catch ex As Exception
            PictureBox2.Image = ICONILIST.Images(4)
        End Try
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox4, _OBJECTNUM) '지상공격 콤보박스


        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox12, _OBJECTNUM)
        Try
            t_Iconnum = DatEditDATA(DTYPE.weapons).ReadValue("Icon", TextBox12.Text)
            PictureBox3.Image = ICONILIST.Images(t_Iconnum) '공중공격 아이콘
        Catch ex As Exception
            PictureBox3.Image = ICONILIST.Images(4)
        End Try
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox5, _OBJECTNUM) '공중공격 콤보박스


        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox14, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox13, _OBJECTNUM)



        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox15, _OBJECTNUM) '인구수
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox16, _OBJECTNUM)

        DatEditDATA(DTYPE.units).ReadToCHECKBOXLIST(ListView3, _OBJECTNUM)


        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox17, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox18, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox19, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox20, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox21, _OBJECTNUM) '유닛크기
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox6, _OBJECTNUM) '유닛크기 콤보박스

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox22, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox23, _OBJECTNUM)




        '고급정보
        DatEditDATA(DTYPE.units).ReadToCHECKBOXLIST(ListView2, _OBJECTNUM)

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox24, _OBJECTNUM)
        Try
            t_Iconnum = TextBox24.Text
            PictureBox5.Image = ICONILIST.Images(t_Iconnum) '감염유닛
        Catch ex As Exception
            PictureBox5.Image = ICONILIST.Images(4)
        End Try
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox7, _OBJECTNUM) '감염유닛 콤보박스

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox25, _OBJECTNUM)
        Try
            t_Iconnum = TextBox25.Text
            PictureBox6.Image = ICONILIST.Images(t_Iconnum) '부가유닛1 아이콘
        Catch ex As Exception
            PictureBox6.Image = ICONILIST.Images(4)
        End Try
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox8, _OBJECTNUM) '부가유닛1 콤보박스

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox26, _OBJECTNUM)
        Try
            t_Iconnum = TextBox26.Text
            PictureBox7.Image = ICONILIST.Images(t_Iconnum) '부가유닛2 아이콘
        Catch ex As Exception
            PictureBox7.Image = ICONILIST.Images(4)
        End Try
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox9, _OBJECTNUM) '부가유닛2 콤보박스

        DatEditDATA(DTYPE.units).ReadToCHECKBOXLIST(ListView4, _OBJECTNUM)


        '사운드
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox27, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox10, _OBJECTNUM)

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox28, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox11, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox29, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox12, _OBJECTNUM)

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox30, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox13, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox31, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox14, _OBJECTNUM)

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox32, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox15, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox33, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox16, _OBJECTNUM)


        If MainTAB.SelectedIndex = 0 And TabControl2.SelectedIndex = 2 Then
            LoadSoundlist()
        End If
        '그래픽
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox36, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox19, _OBJECTNUM)

        frameNum = 0
        drawUnitGRP()


        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox35, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox18, _OBJECTNUM)

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox34, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox17, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox37, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox20, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox38, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox21, _OBJECTNUM)
        'Unit Direction




        DatEditDATA(DTYPE.units).ReadToNUMERIC(NumericUpDown3, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToNUMERIC(NumericUpDown4, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToNUMERIC(NumericUpDown5, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToNUMERIC(NumericUpDown6, _OBJECTNUM)


        DatEditDATA(DTYPE.units).ReadToNUMERIC(NumericUpDown7, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToNUMERIC(NumericUpDown8, _OBJECTNUM)

        DatEditDATA(DTYPE.units).ReadToNUMERIC(NumericUpDown10, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToNUMERIC(NumericUpDown9, _OBJECTNUM)

        DatEditDATA(DTYPE.units).ReadToCHECKBOXLIST(ListView5, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCHECKBOXLIST(ListView6, _OBJECTNUM)


        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox45, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox22, _OBJECTNUM)

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox46, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox23, _OBJECTNUM)


        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox53, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox28, _OBJECTNUM)
        Try
            t_Iconnum = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox53.Text)
            PictureBox11.Image = ICONILIST.Images(t_Iconnum) '지상공격 아이콘
        Catch ex As Exception
            PictureBox11.Image = ICONILIST.Images(4)
        End Try

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox52, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox27, _OBJECTNUM)
        Try
            t_Iconnum = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox52.Text)
            PictureBox12.Image = ICONILIST.Images(t_Iconnum) '지상공격 아이콘
        Catch ex As Exception
            PictureBox12.Image = ICONILIST.Images(4)
        End Try

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox51, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox26, _OBJECTNUM)
        Try
            t_Iconnum = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox51.Text)
            PictureBox13.Image = ICONILIST.Images(t_Iconnum) '지상공격 아이콘
        Catch ex As Exception
            PictureBox13.Image = ICONILIST.Images(4)
        End Try

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox50, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox25, _OBJECTNUM)
        Try
            t_Iconnum = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox50.Text)
            PictureBox14.Image = ICONILIST.Images(t_Iconnum) '지상공격 아이콘
        Catch ex As Exception
            PictureBox14.Image = ICONILIST.Images(4)
        End Try

        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox49, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox24, _OBJECTNUM)
        Try
            t_Iconnum = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox49.Text)
            PictureBox15.Image = ICONILIST.Images(t_Iconnum) '지상공격 아이콘
        Catch ex As Exception
            PictureBox15.Image = ICONILIST.Images(4)
        End Try


        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox54, _OBJECTNUM)
        DatEditDATA(DTYPE.units).ReadToCOMBOBOX(ComboBox29, _OBJECTNUM)


        DatEditDATA(DTYPE.units).ReadToCHECKBOXLIST(ListView7, _OBJECTNUM)


        DatEditDATA(DTYPE.units).ReadToTEXTBOX(TextBox107, _OBJECTNUM)

        LoadUnitGRP()
        drawUnitGRP()
    End Sub

    Private Sub WeaponDataLOAD()
        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.SelectedItem(2) = DatEditDATA(DTYPE.weapons).CheckChangeAll(_OBJECTNUM)
        End If

        Dim t_Iconnum As Integer

        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox1, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox3, _OBJECTNUM)

        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox39, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox40, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox43, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox44, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox55, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox56, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox57, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox64, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox65, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox66, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox67, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox62, _OBJECTNUM)

        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox41, _OBJECTNUM)
        Try
            t_Iconnum = DatEditDATA(DTYPE.upgrades).ReadValue("Icon", TextBox41.Text)
            PictureBox16.Image = ICONILIST.Images(t_Iconnum) '지상공격 아이콘
        Catch ex As Exception
            PictureBox16.Image = ICONILIST.Images(4)
        End Try
        DatEditDATA(DTYPE.weapons).ReadToCOMBOBOX(ComboBox2, _OBJECTNUM) '지상공격 콤보박스


        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox63, _OBJECTNUM)
        Try
            t_Iconnum = TextBox63.Text
            PictureBox18.Image = ICONILIST.Images(t_Iconnum) '지상공격 아이콘
        Catch ex As Exception
            PictureBox18.Image = ICONILIST.Images(4)
        End Try
        DatEditDATA(DTYPE.weapons).ReadToCOMBOBOX(ComboBox36, _OBJECTNUM) '지상공격 콤보박스
        '39,40,43,44,55,56,57,64,65,66,67,62



        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox42, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToCOMBOBOX(ComboBox3, _OBJECTNUM)

        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox47, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToCOMBOBOX(ComboBox30, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox48, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToCOMBOBOX(ComboBox31, _OBJECTNUM)

        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox58, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToCOMBOBOX(ComboBox32, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox59, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToCOMBOBOX(ComboBox33, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox60, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToCOMBOBOX(ComboBox34, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox61, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToCOMBOBOX(ComboBox35, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToTEXTBOX(TextBox63, _OBJECTNUM)
        DatEditDATA(DTYPE.weapons).ReadToCOMBOBOX(ComboBox36, _OBJECTNUM)


        DatEditDATA(DTYPE.weapons).ReadToCHECKBOXLIST(ListView8, _OBJECTNUM)

        LoadWeaponGRP()
        drawWeaponGRP()
    End Sub

    Private Sub FlingyDataLOAD()
        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.SelectedItem(2) = DatEditDATA(DTYPE.flingy).CheckChangeAll(_OBJECTNUM)
        End If


        DatEditDATA(DTYPE.flingy).ReadToTEXTBOX(TextBox68, _OBJECTNUM)
        DatEditDATA(DTYPE.flingy).ReadToCOMBOBOX(ComboBox37, _OBJECTNUM)

        DatEditDATA(DTYPE.flingy).ReadToTEXTBOX(TextBox69, _OBJECTNUM)
        DatEditDATA(DTYPE.flingy).ReadToTEXTBOX(TextBox70, _OBJECTNUM)
        DatEditDATA(DTYPE.flingy).ReadToTEXTBOX(TextBox71, _OBJECTNUM)
        DatEditDATA(DTYPE.flingy).ReadToTEXTBOX(TextBox72, _OBJECTNUM)


        DatEditDATA(DTYPE.flingy).ReadToTEXTBOX(TextBox73, _OBJECTNUM)
        DatEditDATA(DTYPE.flingy).ReadToCOMBOBOX(ComboBox38, _OBJECTNUM)



        DatEditDATA(DTYPE.flingy).ReadToTEXTBOX(TextBox108, _OBJECTNUM)

        LoadflingyGRP()
        drawflingyGRP()
    End Sub

    Private Sub SpritesDataLOAD()
        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.SelectedItem(2) = DatEditDATA(DTYPE.sprites).CheckChangeAll(_OBJECTNUM)
        End If




        DatEditDATA(DTYPE.sprites).ReadToTEXTBOX(TextBox74, _OBJECTNUM)
        DatEditDATA(DTYPE.sprites).ReadToCOMBOBOX(ComboBox39, _OBJECTNUM)

        DatEditDATA(DTYPE.sprites).ReadToTEXTBOX(TextBox75, _OBJECTNUM)
        DatEditDATA(DTYPE.sprites).ReadToCOMBOBOX(ComboBox40, _OBJECTNUM)


        DatEditDATA(DTYPE.sprites).ReadToCHECKBOX(CheckBox6, _OBJECTNUM)


        DatEditDATA(DTYPE.sprites).ReadToTEXTBOX(TextBox77, _OBJECTNUM)


        DatEditDATA(DTYPE.sprites).ReadToTEXTBOX(TextBox76, _OBJECTNUM)
        NumericUpDown13.Value = TextBox76.Text / 3
        NumericUpDown13.BackColor = TextBox76.BackColor
        NumericUpDown13.Visible = TextBox76.Visible

        LoadSpriteGRP()
        drawSpriteGRP()
    End Sub

    Private Sub ImagesDataLOAD()
        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.SelectedItem(2) = DatEditDATA(DTYPE.images).CheckChangeAll(_OBJECTNUM)
        End If




        DatEditDATA(DTYPE.images).ReadToTEXTBOX(TextBox112, _OBJECTNUM)
        DatEditDATA(DTYPE.images).ReadToCOMBOBOX(ComboBox55, _OBJECTNUM)

        DatEditDATA(DTYPE.images).ReadToTEXTBOX(TextBox113, _OBJECTNUM)
        DatEditDATA(DTYPE.images).ReadToCOMBOBOX(ComboBox56, _OBJECTNUM)



        DatEditDATA(DTYPE.images).ReadToTEXTBOX(TextBox114, _OBJECTNUM)
        DatEditDATA(DTYPE.images).ReadToCOMBOBOX(ComboBox57, _OBJECTNUM)

        DatEditDATA(DTYPE.images).ReadToTEXTBOX(TextBox115, _OBJECTNUM)
        DatEditDATA(DTYPE.images).ReadToCOMBOBOX(ComboBox58, _OBJECTNUM)

        DatEditDATA(DTYPE.images).ReadToTEXTBOX(TextBox116, _OBJECTNUM)
        DatEditDATA(DTYPE.images).ReadToCOMBOBOX(ComboBox59, _OBJECTNUM)

        DatEditDATA(DTYPE.images).ReadToTEXTBOX(TextBox117, _OBJECTNUM)
        DatEditDATA(DTYPE.images).ReadToCOMBOBOX(ComboBox60, _OBJECTNUM)

        DatEditDATA(DTYPE.images).ReadToTEXTBOX(TextBox118, _OBJECTNUM)
        DatEditDATA(DTYPE.images).ReadToCOMBOBOX(ComboBox61, _OBJECTNUM)

        DatEditDATA(DTYPE.images).ReadToTEXTBOX(TextBox119, _OBJECTNUM)
        DatEditDATA(DTYPE.images).ReadToCOMBOBOX(ComboBox62, _OBJECTNUM)

        DatEditDATA(DTYPE.images).ReadToTEXTBOX(TextBox121, _OBJECTNUM)
        DatEditDATA(DTYPE.images).ReadToCOMBOBOX(ComboBox65, _OBJECTNUM)
        DatEditDATA(DTYPE.images).ReadToTEXTBOX(TextBox120, _OBJECTNUM)
        DatEditDATA(DTYPE.images).ReadToCOMBOBOX(ComboBox64, _OBJECTNUM)

        For i = 0 To 3
            Dim strings As String = DatEditDATA(DTYPE.images).keyDic.Keys(i + 1)
            ListView10.Items(i).Checked = DatEditDATA(DTYPE.images).ReadValue(strings, _OBJECTNUM)


            DatEditDATA(DTYPE.images).CheckChange(strings, _OBJECTNUM, ListView10.Items(i))

            Dim value As Boolean = DatEditDATA(DTYPE.images).ReadValue(strings, _OBJECTNUM)
            If value = True And ListView10.Items(i).BackColor = ProgramSet.BACKCOLOR Then
                ListView10.Items(i).BackColor = ProgramSet.LISTCOLOR
            End If
        Next


        TextBox121.Enabled = False
        ComboBox65.Enabled = False

        TextBox121.BackColor = Color.GhostWhite
        ComboBox65.BackColor = Color.GhostWhite




        If ListView10.Items(0).Checked Then
            TrackBar1.Enabled = True
        Else
            TrackBar1.Value = 0
            TrackBar1.Enabled = False
        End If


        If GRPEditorUsingDATA(_OBJECTNUM) <> "" Then
            ComboBox65.BackColor = Color.IndianRed
            If ListBox1.Items.Count <> 0 Then
                ListBox1.Items(ListBox1.SelectedIndex)(2) = 1
            End If
        End If


        Dim iscriptID As Integer = DatEditDATA(DTYPE.images).ReadValue("Iscript ID", _OBJECTNUM)


        ListBox9.Items.Clear()

        '        ListBox9.Items.Add(iscript.iscriptEntry(iscript.key(iscriptID)).EntryType)
        Try
            For i = 0 To iscript.iscriptEntry(iscript.key(iscriptID)).EntryType - 1
                ListBox9.Items.Add(Format(iscript.iscriptEntry(iscript.key(iscriptID)).AnimHeader(i), "00000") & " " & HEADERNAME(i))
                'ListBox9.Items.Add(iscript.iscriptEntry(iscript.key(iscriptID)).headeroffset)
            Next
        Catch ex As KeyNotFoundException

        End Try

        If GRPEditorControl.Visible = True Then
            GRPEditorControlload()
        End If



        If ListBox9.Items.Count <> 0 Then
            ListBox9.SelectedIndex = 0
        End If
        RichTextBox1.ResetText()
        'PictureBox26
        LoadImageGRP()
        drawImageGRP(0, False)

        IScriptPlayer.Enabled = True
        iscript.curretgrpMaxFrame = ImageGRP.framecount
        iscript.currentScriptID = iscriptID
    End Sub

    Private Sub UpgradeDataLOAD()
        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.SelectedItem(2) = DatEditDATA(DTYPE.upgrades).CheckChangeAll(_OBJECTNUM)
        End If


        Dim t_Iconnum As Integer

        DatEditDATA(DTYPE.upgrades).ReadToTEXTBOX(TextBox78, _OBJECTNUM) '방어구

        Try
            t_Iconnum = TextBox78.Text
            PictureBox23.Image = ICONILIST.Images(t_Iconnum) '방어구 아이콘
        Catch ex As Exception
            PictureBox23.Image = ICONILIST.Images(4)
        End Try
        DatEditDATA(DTYPE.upgrades).ReadToCOMBOBOX(ComboBox41, _OBJECTNUM) '방어구 콤보박스


        DatEditDATA(DTYPE.upgrades).ReadToTEXTBOX(TextBox79, _OBJECTNUM)
        DatEditDATA(DTYPE.upgrades).ReadToCOMBOBOX(ComboBox42, _OBJECTNUM)

        DatEditDATA(DTYPE.upgrades).ReadToTEXTBOX(TextBox82, _OBJECTNUM)
        DatEditDATA(DTYPE.upgrades).ReadToTEXTBOX(TextBox81, _OBJECTNUM)
        DatEditDATA(DTYPE.upgrades).ReadToTEXTBOX(TextBox80, _OBJECTNUM)
        NumericUpDown14.Value = TextBox80.Text / 24
        NumericUpDown14.BackColor = TextBox80.BackColor



        DatEditDATA(DTYPE.upgrades).ReadToTEXTBOX(TextBox85, _OBJECTNUM)
        DatEditDATA(DTYPE.upgrades).ReadToTEXTBOX(TextBox84, _OBJECTNUM)
        DatEditDATA(DTYPE.upgrades).ReadToTEXTBOX(TextBox83, _OBJECTNUM)
        NumericUpDown15.Value = TextBox83.Text / 24
        NumericUpDown15.BackColor = TextBox83.BackColor



        DatEditDATA(DTYPE.upgrades).ReadToTEXTBOX(TextBox87, _OBJECTNUM)


        DatEditDATA(DTYPE.upgrades).ReadToTEXTBOX(TextBox88, _OBJECTNUM)
        DatEditDATA(DTYPE.upgrades).ReadToCOMBOBOX(ComboBox43, _OBJECTNUM)



        DatEditDATA(DTYPE.upgrades).ReadToCHECKBOX(CheckBox8, _OBJECTNUM)


        DatEditDATA(DTYPE.upgrades).ReadToTEXTBOX(TextBox109, _OBJECTNUM)
    End Sub

    Private Sub TechDataLOAD()
        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.SelectedItem(2) = DatEditDATA(DTYPE.techdata).CheckChangeAll(_OBJECTNUM)
        End If


        Dim t_Iconnum As Integer

        DatEditDATA(DTYPE.techdata).ReadToTEXTBOX(TextBox92, _OBJECTNUM) '방어구

        Try
            t_Iconnum = TextBox92.Text
            PictureBox24.Image = ICONILIST.Images(t_Iconnum) '방어구 아이콘
        Catch ex As Exception
            PictureBox24.Image = ICONILIST.Images(4)
        End Try
        DatEditDATA(DTYPE.techdata).ReadToCOMBOBOX(ComboBox45, _OBJECTNUM) '방어구 콤보박스


        DatEditDATA(DTYPE.techdata).ReadToTEXTBOX(TextBox91, _OBJECTNUM)
        DatEditDATA(DTYPE.techdata).ReadToCOMBOBOX(ComboBox44, _OBJECTNUM)


        DatEditDATA(DTYPE.techdata).ReadToTEXTBOX(TextBox90, _OBJECTNUM)
        DatEditDATA(DTYPE.techdata).ReadToTEXTBOX(TextBox89, _OBJECTNUM)
        DatEditDATA(DTYPE.techdata).ReadToTEXTBOX(TextBox86, _OBJECTNUM)
        NumericUpDown16.Value = TextBox86.Text / 24
        NumericUpDown16.BackColor = TextBox86.BackColor

        DatEditDATA(DTYPE.techdata).ReadToTEXTBOX(TextBox94, _OBJECTNUM)



        DatEditDATA(DTYPE.techdata).ReadToTEXTBOX(TextBox93, _OBJECTNUM)
        DatEditDATA(DTYPE.techdata).ReadToCOMBOBOX(ComboBox46, _OBJECTNUM)



        DatEditDATA(DTYPE.techdata).ReadToCHECKBOX(CheckBox9, _OBJECTNUM)

        DatEditDATA(DTYPE.techdata).ReadToTEXTBOX(TextBox110, _OBJECTNUM)
        DatEditDATA(DTYPE.techdata).ReadToCHECKBOX(CheckBox10, _OBJECTNUM)
    End Sub

    Private Sub OrdersDataLOAD()
        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.SelectedItem(2) = DatEditDATA(DTYPE.orders).CheckChangeAll(_OBJECTNUM)
        End If


        Dim t_Iconnum As Integer

        DatEditDATA(DTYPE.orders).ReadToTEXTBOX(TextBox100, _OBJECTNUM) '방어구

        Try
            t_Iconnum = TextBox100.Text
            PictureBox25.Image = ICONILIST.Images(t_Iconnum) '방어구 아이콘
        Catch ex As Exception
            PictureBox25.Image = ICONILIST.Images(4)
        End Try
        DatEditDATA(DTYPE.orders).ReadToCOMBOBOX(ComboBox52, _OBJECTNUM) '방어구 콤보박스


        DatEditDATA(DTYPE.orders).ReadToTEXTBOX(TextBox95, _OBJECTNUM)
        DatEditDATA(DTYPE.orders).ReadToCOMBOBOX(ComboBox47, _OBJECTNUM)
        DatEditDATA(DTYPE.orders).ReadToTEXTBOX(TextBox96, _OBJECTNUM)
        DatEditDATA(DTYPE.orders).ReadToCOMBOBOX(ComboBox48, _OBJECTNUM)
        DatEditDATA(DTYPE.orders).ReadToTEXTBOX(TextBox97, _OBJECTNUM)
        DatEditDATA(DTYPE.orders).ReadToCOMBOBOX(ComboBox49, _OBJECTNUM)
        DatEditDATA(DTYPE.orders).ReadToTEXTBOX(TextBox98, _OBJECTNUM)
        DatEditDATA(DTYPE.orders).ReadToCOMBOBOX(ComboBox50, _OBJECTNUM)
        DatEditDATA(DTYPE.orders).ReadToTEXTBOX(TextBox99, _OBJECTNUM)
        DatEditDATA(DTYPE.orders).ReadToCOMBOBOX(ComboBox51, _OBJECTNUM)



        For i = 0 To 11
            Dim strings As String = DatEditDATA(DTYPE.orders).keyDic.Keys(i + 1)
            Try
                ListView9.Items(i).Checked = DatEditDATA(DTYPE.orders).ReadValue(strings, _OBJECTNUM)


                DatEditDATA(DTYPE.orders).CheckChange(strings, _OBJECTNUM, ListView9.Items(i))

                Dim value As Boolean = DatEditDATA(DTYPE.orders).ReadValue(strings, _OBJECTNUM)
                If value = True And ListView9.Items(i).BackColor = ProgramSet.BACKCOLOR Then
                    ListView9.Items(i).BackColor = ProgramSet.LISTCOLOR
                End If
            Catch ex As Exception
                ListView9.Items(i).Checked = False
            End Try

        Next

        DatEditDATA(DTYPE.orders).ReadToTEXTBOX(TextBox111, _OBJECTNUM)
    End Sub

    Private Sub SfxdataDataLOAD()
        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.SelectedItem(2) = DatEditDATA(DTYPE.sfxdata).CheckChangeAll(_OBJECTNUM)
        End If

        DatEditDATA(DTYPE.sfxdata).ReadToTEXTBOX(TextBox101, _OBJECTNUM)
        DatEditDATA(DTYPE.sfxdata).ReadToCOMBOBOX(ComboBox53, _OBJECTNUM)

        DatEditDATA(DTYPE.sfxdata).ReadToTEXTBOX(TextBox102, _OBJECTNUM)
        DatEditDATA(DTYPE.sfxdata).ReadToTEXTBOX(TextBox103, _OBJECTNUM)
        DatEditDATA(DTYPE.sfxdata).ReadToTEXTBOX(TextBox104, _OBJECTNUM)
        DatEditDATA(DTYPE.sfxdata).ReadToTEXTBOX(TextBox105, _OBJECTNUM)


        TextBox101.Enabled = False
        ComboBox53.Enabled = False

        TextBox101.BackColor = Color.GhostWhite
        ComboBox53.BackColor = Color.GhostWhite



        MPQlib.ReadListfile(ListBox6)
        readlist()

        If ListBox6.Items.Contains("sound\" & ComboBox53.SelectedItem.ToString.ToLower) Then
            ComboBox53.BackColor = Color.IndianRed
            If ListBox1.Items.Count <> 0 Then
                ListBox1.Items(ListBox1.SelectedIndex)(2) = 1
            End If
        End If
    End Sub

    Private Sub PortdataDataLOAD()
        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.SelectedItem(2) = DatEditDATA(DTYPE.portdata).CheckChangeAll(_OBJECTNUM)
        End If

        DatEditDATA(DTYPE.portdata).ReadToTEXTBOX(TextBox106, _OBJECTNUM)
        DatEditDATA(DTYPE.portdata).ReadToCOMBOBOX(ComboBox54, _OBJECTNUM)



        TextBox106.Enabled = False
        ComboBox54.Enabled = False

        TextBox106.BackColor = Color.GhostWhite
        ComboBox54.BackColor = Color.GhostWhite


        ListBox8.Items.Clear()

        Dim filmstream As New FileStream(My.Application.Info.DirectoryPath & "\Data\" & "Portdatalist.txt", FileMode.Open)
        Dim streamreader As New StreamReader(filmstream)
        Dim templist As New List(Of String)

        While (streamreader.EndOfStream = False)
            Dim temp As String = streamreader.ReadLine()
            templist.Add(Mid(temp, InStr(temp, "\") + 1))
        End While
        Dim filenames() As String
        filenames = templist.ToArray 'readtblfile(My.Application.Info.DirectoryPath & "\Data\" & "portdata.tbl")

        streamreader.Close()
        filmstream.Close()


        For i = 0 To filenames.Count - 1
            If InStr(filenames(i), ComboBox54.Items(TextBox106.Text).ToString.ToLower) <> 0 Then
                If ListBox8.Items.Contains(filenames(i)) = False Then
                    ListBox8.Items.Add(filenames(i))
                End If
            End If
        Next

        If ListBox8.Items.Count <> 0 Then
            ListBox8.SelectedIndex = 0
        End If


        MPQlib.ReadListfile(ListBox7)
        readlistsmk()

        'If ListBox7.Items.Contains("sound\" & ComboBox53.SelectedItem.ToString.ToLower) Then
        'ComboBox54.BackColor = Color.IndianRed
        'End If
    End Sub


    Private Sub ListBox9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox9.SelectedIndexChanged
        If ListBox9.SelectedIndex <> -1 Then
            RichTextBox1.Text = HEADERINFOR(ListBox9.SelectedIndex)

            Try
                Dim iscriptID As Integer = DatEditDATA(DTYPE.images).ReadValue("Iscript ID", _OBJECTNUM)
                Dim animheader As UInt16 = iscript.iscriptEntry(iscript.key(iscriptID)).AnimHeader(ListBox9.SelectedIndex)

                iscript.gfxturn = True
                iscirpt_wait = 1
                IScriptPlayer.Interval = 1
                iscript.currentFrame = 0
                iscript.currentHeader = animheader
                iscript.currentAnimHeaderID = ListBox9.SelectedIndex
                iscript.x = 0
                iscript.y = 0
                IScriptPlayer.Enabled = True
            Catch ex As KeyNotFoundException

            End Try


        End If
    End Sub

    Public iscirpt_wait As Integer
    Public iscirpt_direction As Integer
    Private Sub IScriptPlayer_Tick(sender As Object, e As EventArgs) Handles IScriptPlayer.Tick

        If ListBox9.SelectedIndex <> -1 And TAB_INDEX = DTYPE.images Then
            iscirpt_direction = TrackBar1.Value

            iscirpt_wait = 0
            iscript.playScript()
            If iscirpt_wait = 0 Then
                IScriptPlayer.Interval = 1
            Else
                IScriptPlayer.Interval = iscirpt_wait * 30
            End If

        Else
            IScriptPlayer.Enabled = False
        End If
    End Sub
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        If ListBox9.SelectedIndex <> -1 Then
            Try
                Dim iscriptID As Integer = DatEditDATA(DTYPE.images).ReadValue("Iscript ID", _OBJECTNUM)
                Dim animheader As UInt16 = iscript.iscriptEntry(iscript.key(iscriptID)).AnimHeader(ListBox9.SelectedIndex)

                iscript.gfxturn = True
                iscirpt_wait = 1
                IScriptPlayer.Interval = 1
                iscript.currentHeader = animheader
                IScriptPlayer.Enabled = True



                Dim gfxturn As Boolean = DatEditDATA(DTYPE.images).ReadValue("Gfx Turns", _OBJECTNUM)
                If gfxturn = True Then
                    If TrackBar1.Value > 16 Then
                        drawImageGRP(33 - TrackBar1.Value, True)
                    Else
                        drawImageGRP(TrackBar1.Value, False)
                    End If
                Else
                    drawImageGRP(0, False)
                End If

            Catch ex As KeyNotFoundException

            End Try

        End If
    End Sub

    Private Sub LoadSoundlist()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        ListBox4.Items.Clear()
        ListBox5.Items.Clear()


        Dim ready As Integer = Val(TextBox27.Text)
        Dim yes1 As Integer = Val(TextBox28.Text)
        Dim yes2 As Integer = Val(TextBox29.Text)
        Dim what1 As Integer = Val(TextBox31.Text)
        Dim what2 As Integer = Val(TextBox30.Text)
        Dim piss1 As Integer = Val(TextBox33.Text)
        Dim piss2 As Integer = Val(TextBox32.Text)

        Try
            Dim value As String = sfxdata(DatEditDATA(DTYPE.sfxdata).ReadValue("Sound File", ready) - 1)
            ListBox2.Items.Add(value)
        Catch ex As Exception
            ListBox2.Items.Clear()
        End Try


        Try
            If Math.Abs(yes1 - yes2) < 1144 Then
                If yes1 <= yes2 Then
                    For i = yes1 To yes2
                        Dim value As String = sfxdata(DatEditDATA(DTYPE.sfxdata).ReadValue("Sound File", i) - 1)
                        ListBox3.Items.Add(value)
                    Next
                Else
                    For i = yes2 To yes1
                        Dim value As String = sfxdata(DatEditDATA(DTYPE.sfxdata).ReadValue("Sound File", i) - 1)
                        ListBox3.Items.Add(value)
                    Next
                End If
            End If
        Catch ex As Exception
            ListBox3.Items.Clear()
        End Try


        Try
            If Math.Abs(what1 - what2) < 1144 Then
                If what1 <= what2 Then
                    For i = what1 To what2
                        Dim value As String = sfxdata(DatEditDATA(DTYPE.sfxdata).ReadValue("Sound File", i) - 1)
                        ListBox4.Items.Add(value)
                    Next
                Else
                    For i = what2 To what1
                        Dim value As String = sfxdata(DatEditDATA(DTYPE.sfxdata).ReadValue("Sound File", i) - 1)
                        ListBox4.Items.Add(value)
                    Next
                End If
            End If
        Catch ex As Exception
            ListBox4.Items.Clear()
        End Try


        Try
            If Math.Abs(piss1 - piss2) < 1144 Then
                If piss1 <= piss2 Then
                    For i = piss1 To piss2
                        Dim value As String = sfxdata(DatEditDATA(DTYPE.sfxdata).ReadValue("Sound File", i) - 1)
                        ListBox5.Items.Add(value)
                    Next
                Else
                    For i = piss2 To piss1
                        Dim value As String = sfxdata(DatEditDATA(DTYPE.sfxdata).ReadValue("Sound File", i) - 1)
                        ListBox5.Items.Add(value)
                    Next
                End If
            End If
        Catch ex As Exception
            ListBox5.Items.Clear()
        End Try

    End Sub
    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.MouseDoubleClick
        Dim mpq As New SFMpq
        If ListBox2.SelectedIndex <> -1 Then
            If ListBox2.SelectedItem <> "No sound" Then
                SoundPlay("sound\" & Replace(ListBox2.SelectedItem, "(1)", "").Trim)
            End If
        End If
    End Sub

    Private Sub ListBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox3.MouseDoubleClick
        Dim mpq As New SFMpq
        If ListBox3.SelectedIndex <> -1 Then
            If ListBox3.SelectedItem <> "No sound" Then
                SoundPlay("sound\" & Replace(ListBox3.SelectedItem, "(1)", "").Trim)
            End If
        End If
    End Sub

    Private Sub ListBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox4.MouseDoubleClick
        Dim mpq As New SFMpq
        If ListBox4.SelectedIndex <> -1 Then
            If ListBox4.SelectedItem <> "No sound" Then
                SoundPlay("sound\" & Replace(ListBox4.SelectedItem, "(1)", "").Trim)
            End If
        End If
    End Sub

    Private Sub ListBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox5.MouseDoubleClick
        Dim mpq As New SFMpq
        If ListBox5.SelectedIndex <> -1 Then
            If ListBox5.SelectedItem <> "No sound" Then
                SoundPlay("sound\" & Replace(ListBox5.SelectedItem, "(1)", "").Trim)
            End If
        End If
    End Sub


    Private Sub TabControl2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl2.SelectedIndexChanged
        If MainTAB.SelectedIndex = 0 And TabControl2.SelectedIndex = 2 Then
            LoadSoundlist()
        End If
    End Sub



    Dim ImageGRP As New GRP
    Public Sub drawImageGRP(fnum As Integer, flip As Boolean, Optional x As Integer = 0, Optional y As Integer = 0)
        Label146.Text = "Frame " & fnum

        Dim remapping As Integer
        Dim drawfunc As Integer

        Try
            drawfunc = DatEditDATA(DTYPE.images).ReadValue("Draw Function", _OBJECTNUM) + 1
            remapping = DatEditDATA(DTYPE.images).ReadValue("Remapping", _OBJECTNUM) + 1

            ImageGRP.LoadPalette(0, drawfunc, remapping)
            'Select Case remapping
            '    Case 0
            '        ImageGRP.LoadPalette(PalettType.install)
            '    Case 1
            '        ImageGRP.LoadPalette(PalettType.ofire)
            '    Case 2
            '        ImageGRP.LoadPalette(PalettType.gfire)
            '    Case 3
            '        ImageGRP.LoadPalette(PalettType.bfire)
            '    Case 4
            '        ImageGRP.LoadPalette(PalettType.bexpl)
            'End Select

            Try
                If ImageGRP.GRPFrame.Count > fnum Then
                    ImageGRP.DrawToPictureBox(PictureBox26, fnum, 0, False, flip, x, y)
                Else
                    IScriptPlayer.Enabled = False
                    ListBox9.SelectedIndex = -1

                    Dim bitmap As New Bitmap(256, 256)
                    Dim grptool As Graphics
                    grptool = Graphics.FromImage(bitmap)
                    grptool.DrawString(Lan.GetText(Me.Name, "invalidData").Replace("$V$", fnum), Font, Brushes.Red, New Point(0, 96))

                    PictureBox26.Image = bitmap
                End If
            Catch ex As Exception
                PictureBox26.Image = ICONILIST.Images(4)
            End Try
        Catch ex As Exception
            PictureBox26.Image = PictureBox26.ErrorImage
        End Try
    End Sub
    Private Sub LoadImageGRP()
        Dim remapping As Integer

        Try
            remapping = DatEditDATA(DTYPE.images).ReadValue("Remapping", _OBJECTNUM)
        Catch ex As Exception

        End Try

        ImageGRP.Reset()

        ImageGRP.LoadGRP(GRPHock(_OBJECTNUM)) 'mpq.ReaddatFile("unit\" & CODE(DTYPE.grpfile)(grpfile).Replace("<0>", ""))) 'unit\protoss\dragoo

        TextBox122.Text = ImageGRP.GRPFrame.Count
    End Sub

    Dim SpritGRP As New GRP
    Private Sub drawSpriteGRP()
        Dim Image, remapping, point, size As Integer
        If CheckBox7.Checked = False Then
            frameNum = 13 + 17
        End If

        Try
            Image = DatEditDATA(DTYPE.sprites).ReadValue("Image File", _OBJECTNUM)
            remapping = DatEditDATA(DTYPE.images).ReadValue("Remapping", Image)


            Select Case remapping
                Case 0
                    SpritGRP.LoadPalette(PalettType.install)
                Case 1
                    SpritGRP.LoadPalette(PalettType.ofire)
                Case 2
                    SpritGRP.LoadPalette(PalettType.gfire)
                Case 3
                    SpritGRP.LoadPalette(PalettType.bfire)
                Case 4
                    SpritGRP.LoadPalette(PalettType.bexpl)
            End Select


            Try
                SpritGRP.DrawToPictureBox(PictureBox22, frameNum, 12, True)
            Catch ex As Exception
                PictureBox22.Image = ICONILIST.Images(4)
            End Try





            Dim tempgrp As New GRP
            Try
                size = DatEditDATA(DTYPE.sprites).ReadValue("Health Bar", _OBJECTNUM)
                point = DatEditDATA(DTYPE.sprites).ReadValue("Sel.Circle Offset", _OBJECTNUM)
                Image = DatEditDATA(DTYPE.sprites).ReadValue("Sel.Circle Image", _OBJECTNUM) + 561
                remapping = DatEditDATA(DTYPE.images).ReadValue("Remapping", Image)


                Select Case remapping
                    Case 0
                        tempgrp.LoadPalette(PalettType.install)
                    Case 1
                        tempgrp.LoadPalette(PalettType.ofire)
                    Case 2
                        tempgrp.LoadPalette(PalettType.gfire)
                    Case 3
                        tempgrp.LoadPalette(PalettType.bfire)
                    Case 4
                        tempgrp.LoadPalette(PalettType.bexpl)
                End Select


                tempgrp.LoadGRP(GRPHock(Image)) 'mpq.ReaddatFile("unit\" & CODE(DTYPE.grpfile)(temp2).Replace("<0>", ""))) 'unit\protoss\dragoo


                tempgrp.DrawToPictureBoxBackG(PictureBox22, frameNum, 12, point)

                Panel2.Visible = True
                Panel2.Location = New Point(10 + 126 - size \ 2, 21 + 128 + point + tempgrp.GetFrameSize(frameNum).Height \ 2 + 4)
                Panel2.Width = ((size + 4) \ 3) * 3

                tempgrp.Reset()
            Catch ex As Exception
                Panel2.Visible = False
                PictureBox22.BackgroundImage = Nothing
            End Try
        Catch ex As Exception
            PictureBox22.Image = PictureBox22.ErrorImage
        End Try

    End Sub
    Private Sub LoadSpriteGRP()
        Dim Image, remapping As Integer

        Try
            Image = DatEditDATA(DTYPE.sprites).ReadValue("Image File", _OBJECTNUM)
            remapping = DatEditDATA(DTYPE.images).ReadValue("Remapping", Image)
        Catch ex As Exception

        End Try


        SpritGRP.Reset()
        Try
            SpritGRP.LoadGRP(GRPHock(Image))
        Catch ex As Exception

        End Try

    End Sub

    Dim flingyGRP As New GRP
    Private Sub drawflingyGRP()
        Dim sprite, Image, remapping As Integer

        Try
            sprite = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", _OBJECTNUM)
            Image = DatEditDATA(DTYPE.sprites).ReadValue("Image File", sprite)
            remapping = DatEditDATA(DTYPE.images).ReadValue("Remapping", Image)


            Select Case remapping
                Case 0
                    flingyGRP.LoadPalette(PalettType.install)
                Case 1
                    flingyGRP.LoadPalette(PalettType.ofire)
                Case 2
                    flingyGRP.LoadPalette(PalettType.gfire)
                Case 3
                    flingyGRP.LoadPalette(PalettType.bfire)
                Case 4
                    flingyGRP.LoadPalette(PalettType.bexpl)
            End Select


            Try
                flingyGRP.DrawToPictureBox(PictureBox21, frameNum, 12)
            Catch ex As Exception
                PictureBox21.Image = ICONILIST.Images(4)
            End Try
        Catch ex As Exception
            PictureBox21.Image = PictureBox21.ErrorImage
        End Try

    End Sub
    Private Sub LoadflingyGRP()
        Dim sprite, Image, remapping As Integer

        Try
            sprite = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", _OBJECTNUM)
            Image = DatEditDATA(DTYPE.sprites).ReadValue("Image File", sprite)
            remapping = DatEditDATA(DTYPE.images).ReadValue("Remapping", Image)
        Catch ex As Exception

        End Try

        flingyGRP.Reset()
        flingyGRP.LoadGRP(GRPHock(Image)) 'mpq.ReaddatFile("unit\" & CODE(DTYPE.grpfile)(temp2).Replace("<0>", ""))) 'unit\protoss\dragoo

    End Sub

    Dim WeaponGRP As New GRP
    Private Sub drawWeaponGRP()
        Dim weaponsgrp, weaponsprite, weaponimage, grpfile, remapping As Integer


        Try
            weaponsgrp = DatEditDATA(DTYPE.weapons).ReadValue("Graphics", _OBJECTNUM)
            weaponsprite = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", weaponsgrp)
            weaponimage = DatEditDATA(DTYPE.sprites).ReadValue("Image File", weaponsprite)
            grpfile = DatEditDATA(DTYPE.images).ReadValue("GRP File", weaponimage)
            remapping = DatEditDATA(DTYPE.images).ReadValue("Remapping", weaponimage)


            Select Case remapping
                Case 0
                    WeaponGRP.LoadPalette(PalettType.install)
                Case 1
                    WeaponGRP.LoadPalette(PalettType.ofire)
                Case 2
                    WeaponGRP.LoadPalette(PalettType.gfire)
                Case 3
                    WeaponGRP.LoadPalette(PalettType.bfire)
                Case 4
                    WeaponGRP.LoadPalette(PalettType.bexpl)
            End Select

            Try
                WeaponGRP.DrawToPictureBox(PictureBox17, frameNum)
            Catch ex As Exception
                PictureBox17.Image = ICONILIST.Images(4)
            End Try
        Catch ex As Exception
            PictureBox17.Image = PictureBox17.ErrorImage
        End Try


        Try
            Dim dir As Integer = DatEditDATA(DTYPE.weapons).ReadValue("Attack Angle", _OBJECTNUM)
            Dim Attackdir As Integer = DatEditDATA(DTYPE.weapons).ReadValue("Launch Spin", _OBJECTNUM)

            Dim bit As New Bitmap(32, 32)
            Dim grp As Graphics
            grp = Graphics.FromImage(bit)
            '((dir + 1) / 32 * 360 * Math.PI/180)
            Dim dergee As Double
            Dim point As Point
            dergee = (dir / 256) * 360


            grp.FillPie(Brushes.LimeGreen, New Rectangle(0, 0, 31, 31), 270 - dergee, dergee * 2)



            dergee = (((Attackdir) / 256) * 360 - 90) * (Math.PI / 180)
            point = New Point(Math.Cos(dergee) * 20 + 16, Math.Sin(dergee) * 20 + 16)
            grp.DrawLine(New Pen(Color.Red, 2), New Point(16, 16), point)

            point = New Point(Math.Cos(dergee) * -20 + 16, Math.Sin(dergee) * 20 + 16)
            grp.DrawLine(New Pen(Color.Red, 2), New Point(16, 16), point)


            PictureBox20.Image = bit



            'Dim ICONGRP As New GRP


            'grpfile = DatEditDATA(DTYPE.images).ReadValue("GRP File", NumericUpDown12.Value)
            'ICONGRP.LoadPalette(PalettType.install)
            'ICONGRP.LoadGRP(GRPHock(NumericUpDown12.Value)) 'mpq.ReaddatFile("unit\" & CODE(DTYPE.grpfile)(grpfile).Replace("<0>", "")))
            'PictureBox19.BackgroundImage = ICONGRP.DrawGRP(8)





            bit = New Bitmap(96, 96)
            grp = Graphics.FromImage(bit)


            Dim x, y As Integer
            x = DatEditDATA(DTYPE.weapons).ReadValue("Forward Offset", _OBJECTNUM)
            y = DatEditDATA(DTYPE.weapons).ReadValue("Upward Offset", _OBJECTNUM)

            Dim redpen As New Pen(Brushes.Red, 1)
            grp.DrawLine(redpen, 48, 0, 48, 96)
            grp.DrawLine(redpen, 0, 48, 96, 48)

            'redpen = New Pen(Brushes.Red, 1)
            For i = 0 To 12
                grp.DrawLine(redpen, i * 8, 44, i * 8, 52)
            Next
            For i = 0 To 12
                grp.DrawLine(redpen, 44, i * 8, 52, i * 8)
            Next

            grp.DrawLine(Pens.LimeGreen, 48 + x, 0, 48 + x, 96)
            grp.DrawLine(Pens.LimeGreen, 0, 48 - y, 96, 48 - y)


            PictureBox19.Image = bit
        Catch ex As Exception
            PictureBox19.Image = PictureBox19.ErrorImage
        End Try
    End Sub
    Private Sub LoadWeaponGRP()
        Dim weaponsgrp, weaponsprite, weaponimage, grpfile, remapping As Integer

        Try
            weaponsgrp = DatEditDATA(DTYPE.weapons).ReadValue("Graphics", _OBJECTNUM)
            weaponsprite = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", weaponsgrp)
            weaponimage = DatEditDATA(DTYPE.sprites).ReadValue("Image File", weaponsprite)
            grpfile = DatEditDATA(DTYPE.images).ReadValue("GRP File", weaponimage)
            remapping = DatEditDATA(DTYPE.images).ReadValue("Remapping", weaponimage)
        Catch ex As Exception

        End Try

        WeaponGRP.Reset()
        WeaponGRP.LoadGRP(GRPHock(weaponimage)) 'mpq.ReaddatFile("unit\" & CODE(DTYPE.grpfile)(grpfile).Replace("<0>", ""))) 'unit\protoss\dragoo
    End Sub

    Dim UnitGRP As New GRP
    Dim ConStructGRP As New GRP
    Private Sub drawUnitGRP()
        If CheckBox4.Checked = False Then
            frameNum = 0
        End If

        Dim ICONGRP As New GRP
        Dim temp1, temp2, temp3, temp4, l, r, u, d, addx, addy, conx, cony, dir As Integer
        Try

            temp1 = DatEditDATA(DTYPE.units).ReadValue("Graphics", _OBJECTNUM)
            temp2 = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", temp1)
            temp4 = DatEditDATA(DTYPE.sprites).ReadValue("Image File", temp2)
            temp2 = DatEditDATA(DTYPE.images).ReadValue("GRP File", temp4)
            temp3 = DatEditDATA(DTYPE.images).ReadValue("Remapping", temp4)

            l = DatEditDATA(DTYPE.units).ReadValue("Unit Size Left", _OBJECTNUM)
            r = DatEditDATA(DTYPE.units).ReadValue("Unit Size Right", _OBJECTNUM)
            u = DatEditDATA(DTYPE.units).ReadValue("Unit Size Up", _OBJECTNUM)
            d = DatEditDATA(DTYPE.units).ReadValue("Unit Size Down", _OBJECTNUM)
            conx = DatEditDATA(DTYPE.units).ReadValue("StarEdit Placement Box Width", _OBJECTNUM)
            cony = DatEditDATA(DTYPE.units).ReadValue("StarEdit Placement Box Height", _OBJECTNUM)
            dir = DatEditDATA(DTYPE.units).ReadValue("Unit Direction", _OBJECTNUM)
            Try
                addx = DatEditDATA(DTYPE.units).ReadValue("Addon Horizontal (X) Position", _OBJECTNUM)
                addy = DatEditDATA(DTYPE.units).ReadValue("Addon Vertical (Y) Position", _OBJECTNUM)
            Catch ex As Exception
                addx = 0
                addy = 0
            End Try



            Select Case temp3
                Case 0
                    UnitGRP.LoadPalette(PalettType.install)
                Case 1
                    UnitGRP.LoadPalette(PalettType.ofire)
                Case 2
                    UnitGRP.LoadPalette(PalettType.gfire)
                Case 3
                    UnitGRP.LoadPalette(PalettType.bfire)
                Case 4
                    UnitGRP.LoadPalette(PalettType.bexpl)
            End Select



            'mpq.ReaddatFile("unit\" & CODE(DTYPE.grpfile)(temp2).Replace("<0>", ""))) 'unit\protoss\dragoo

            Try
                If CheckBox3.Checked = True Then
                    UnitGRP.DrawToPictureBoxUnitGRP(PictureBox8, frameNum, l, r, u, d, addx, addy, conx, cony, NumericUpDown11.Value + 1)
                Else
                    If temp3 = 0 Then
                        UnitGRP.DrawToPictureBox(PictureBox8, frameNum, NumericUpDown11.Value + 1)
                    Else
                        UnitGRP.DrawToPictureBox(PictureBox8, frameNum, 0)
                    End If
                End If
            Catch ex As Exception
                PictureBox8.Image = ICONILIST.Images(4)
            End Try

        Catch ex As Exception
            PictureBox8.Image = PictureBox8.ErrorImage
        End Try



        Try
            Dim bit As New Bitmap(32, 32)
            Dim grp As Graphics
            grp = Graphics.FromImage(bit)
            '((dir + 1) / 32 * 360 * Math.PI/180)

            Dim dergee As Double = (((dir) / 32) * 360 - 90) * (Math.PI / 180)
            Dim point As New Point(Math.Cos(dergee) * 16 + 16, Math.Sin(dergee) * 16 + 16)
            If dir <> 32 Then
                grp.DrawLine(Pens.Orange, New Point(16, 16), point)
            Else
                grp.DrawString("R", Me.Font, Brushes.LimeGreen, New Point(10, 6))
            End If

            PictureBox10.Image = bit





            temp1 = DatEditDATA(DTYPE.units).ReadValue("Construction Animation", _OBJECTNUM)

            temp2 = DatEditDATA(DTYPE.images).ReadValue("GRP File", temp1)
            temp3 = DatEditDATA(DTYPE.images).ReadValue("Remapping", temp1)



            Select Case temp3
                Case 0
                    ConStructGRP.LoadPalette(PalettType.install)
                Case 1
                    ConStructGRP.LoadPalette(PalettType.ofire)
                Case 2
                    ConStructGRP.LoadPalette(PalettType.gfire)
                Case 3
                    ConStructGRP.LoadPalette(PalettType.bfire)
                Case 4
                    ConStructGRP.LoadPalette(PalettType.bexpl)
            End Select


            ConStructGRP.DrawToPictureBox(PictureBox9, frameNum)
        Catch ex As Exception
            PictureBox9.Image = PictureBox9.ErrorImage
        End Try
    End Sub
    Private Sub LoadUnitGRP()
        Dim temp1, temp2, temp3, temp4 As Integer

        Try
            temp1 = DatEditDATA(DTYPE.units).ReadValue("Graphics", _OBJECTNUM)
            temp2 = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", temp1)
            temp4 = DatEditDATA(DTYPE.sprites).ReadValue("Image File", temp2)
        Catch ex As Exception

        End Try


        UnitGRP.Reset()
        UnitGRP.LoadGRP(GRPHock(temp4))



        Try
            temp1 = DatEditDATA(DTYPE.units).ReadValue("Construction Animation", _OBJECTNUM)

            temp2 = DatEditDATA(DTYPE.images).ReadValue("GRP File", temp1)
            temp3 = DatEditDATA(DTYPE.images).ReadValue("Remapping", temp1)
        Catch ex As Exception

        End Try


        ConStructGRP.Reset()
        ConStructGRP.LoadGRP(GRPHock(temp1)) 'mpq.ReaddatFile("unit\" & CODE(DTYPE.grpfile)(temp2).Replace("<0>", ""))) 'unit\protoss\dragoo

    End Sub




    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Select Case TAB_INDEX
            Case 0
                If TabControl2.SelectedIndex = 3 Then
                    drawUnitGRP()
                End If
            Case 1
                drawWeaponGRP()
            Case 2
                drawflingyGRP()
            Case 3
                drawSpriteGRP()
        End Select



        If frameNum < &HFFFFFFFE& Then
            frameNum += 1
        Else
            frameNum = 0
        End If
    End Sub




    Private Sub comboandtext(ByRef Textbox As TextBox, ByRef Combobox As ComboBox)
        loadSTATUS = False
        DatEditDATA(TAB_INDEX).WriteToTEXTBOX(Textbox, _OBJECTNUM)
        Try
            Combobox.SelectedIndex = Textbox.Text
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(Combobox, _OBJECTNUM)

            Combobox.Enabled = True
            Combobox.Visible = True
        Catch ex As Exception
            Combobox.SelectedIndex = -1
            Combobox.Enabled = False
            Combobox.Visible = False
            loadSTATUS = True
        End Try
        loadSTATUS = True
    End Sub


    Private Sub readlist()
        Dim k As Integer = 0
        For i = 0 To ListBox6.Items.Count - 1
            If InStr(ListBox6.Items(k), ".wav") = 0 Then
                ListBox6.Items.RemoveAt(k)
            Else
                k += 1
            End If
        Next
    End Sub
    Private Sub readlistsmk()
        Dim k As Integer = 0
        For i = 0 To ListBox7.Items.Count - 1
            If InStr(ListBox7.Items(k), ".smk") = 0 Then
                ListBox7.Items.RemoveAt(k)
            Else
                k += 1
            End If
        Next
    End Sub
    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        If MPQEditorControl.Visible Then
            MPQEditorControl.Visible = False
        Else
            If MPQlib.ReadListfile(ListBox6) Then
                readlist()


                MPQEditorControl.Visible = True
            Else
                MsgBox(Lan.GetText(Me.Name, "invalidMap"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            End If


        End If
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Dim dialog As DialogResult
        Dim infliename As String = ""
        soundLoad.Filter = "wav|*.wav"
        dialog = soundLoad.ShowDialog

        If dialog = DialogResult.OK Then



            infliename = "sound\" & ComboBox53.Items(TextBox101.Text).ToString.ToLower


            MPQlib.AddFileSound(soundLoad.FileName, infliename)
            If ListBox6.Items.Contains(infliename) = False Then
                ListBox6.Items.Add(infliename)
            End If
        End If

        LoadData()
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Dim temp As Integer = ListBox6.SelectedIndex
        If ListBox6.SelectedIndex <> -1 Then
            MPQlib.RemoveFile(ListBox6.SelectedItem)
            ListBox6.Items.RemoveAt(ListBox6.SelectedIndex)


        End If

        LoadData()

        If ListBox6.Items.Count <> 0 Then
            If ListBox6.Items.Count > temp Then
                ListBox6.SelectedIndex = temp
            Else
                ListBox6.SelectedIndex = ListBox6.Items.Count - 1
            End If
        End If
    End Sub



    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Dim dialog As DialogResult
        Dim infliename As String = ""
        soundLoad.Filter = "smk|*.smk"
        dialog = soundLoad.ShowDialog

        If dialog = DialogResult.OK Then



            infliename = "portrait\" & ListBox8.SelectedItem


            MPQlib.AddFile(soundLoad.FileName, infliename)
            If ListBox7.Items.Contains(infliename) = False Then
                ListBox7.Items.Add(infliename)
            End If
        End If

        LoadData()
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        Dim temp As Integer = ListBox7.SelectedIndex
        If ListBox7.SelectedIndex <> -1 Then

            MPQlib.RemoveFile(ListBox7.SelectedItem)
            ListBox7.Items.RemoveAt(ListBox7.SelectedIndex)


        End If
        LoadData()

        If ListBox7.Items.Count <> 0 Then
            If ListBox7.Items.Count > temp Then
                ListBox7.SelectedIndex = temp
            Else
                ListBox7.SelectedIndex = ListBox7.Items.Count - 1
            End If
        End If
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        If MPQEditorControl2.Visible Then
            MPQEditorControl2.Visible = False
        Else
            If MPQlib.ReadListfile(ListBox7) Then
                readlistsmk()


                MPQEditorControl2.Visible = True
            Else
                MsgBox(Lan.GetText(Me.Name, "invalidMap"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            End If
        End If
    End Sub



    'HP 설정
    Private Sub HitPoint_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If loadSTATUS = True And HPloadSTATUS = True Then
            HPloadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox4, _OBJECTNUM)

            Dim value As UInteger = DatEditDATA(TAB_INDEX).ReadValue(TextBox4.Tag, _OBJECTNUM)
            If NumericUpDown1.Maximum > value \ 256 Then
                NumericUpDown1.Value = value \ 256
            Else
                NumericUpDown1.Value = (value - 4294967296) \ 256
            End If


            NumericUpDown1.BackColor = TextBox4.BackColor
            HPloadSTATUS = True
        End If
    End Sub

    Public HPloadSTATUS As Boolean = True
    Private Sub HitPoint_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged, NumericUpDown1.KeyUp
        If loadSTATUS = True And HPloadSTATUS = True Then
            HPloadSTATUS = False
            If NumericUpDown1.Value < 0 Then
                TextBox4.Text = 4294967296 + (NumericUpDown1.Value) * 256
                'Integer.MaxValue
                '4294967295  '(Not (NumericUpDown1.Value) * -256) + 1
            Else
                TextBox4.Text = NumericUpDown1.Value * 256
            End If
            '-1 = 4294967295
            '-2 = 4294967294

            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox4, _OBJECTNUM)

            NumericUpDown1.BackColor = TextBox4.BackColor
            HPloadSTATUS = True
        End If
    End Sub

    Private Sub ShieldAmount_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox6, _OBJECTNUM)
            If CheckBox1.Checked = False Then
                TextBox6.BackColor = _unusedColor
            End If
        End If
    End Sub

    Private Sub ShieldEnable_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.MouseClick, CheckBox1.CheckedChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOX(CheckBox1, _OBJECTNUM)

            If CheckBox1.Checked = False Then
                TextBox6.BackColor = _unusedColor
            Else
                DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox6, _OBJECTNUM)
            End If
        End If
    End Sub


    Private Sub Armor_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox7, _OBJECTNUM)
        End If
    End Sub

    Private Sub ArmorUpgrade_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox8, _OBJECTNUM)
            Try
                ComboBox1.SelectedIndex = TextBox8.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox1, _OBJECTNUM)
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.upgrades).ReadValue("Icon", TextBox8.Text)
                PictureBox1.Image = ICONILIST.Images(tempinteger2) '방어구 아이콘
                ComboBox1.Enabled = True
                ComboBox1.Visible = True
            Catch ex As Exception
                PictureBox1.Image = ICONILIST.Images(4)
                ComboBox1.SelectedIndex = -1
                ComboBox1.Enabled = False
                ComboBox1.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub ArmorUpgrade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox1, _OBJECTNUM)
            TextBox8.Text = ComboBox1.SelectedIndex
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.upgrades).ReadValue("Icon", TextBox8.Text)
                PictureBox1.Image = ICONILIST.Images(tempinteger2) '방어구 아이콘
            Catch ex As Exception
                PictureBox1.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub

    Private Sub StareditGroupFlags2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView3.ItemChecked
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOXLIST(ListView3, _OBJECTNUM)
        End If
    End Sub

    Private Sub MineralCost_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox5, _OBJECTNUM)
        End If
    End Sub

    Private Sub VespeneCost_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox9, _OBJECTNUM)
        End If
    End Sub

    Private Sub BuildTime_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox10, _OBJECTNUM)
            Try
                NumericUpDown2.Value = TextBox10.Text / 24
            Catch ex As Exception

            End Try
            NumericUpDown2.BackColor = TextBox10.BackColor
            loadSTATUS = True
        End If
    End Sub
    Private Sub BuildTime_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        If loadSTATUS = True Then
            TextBox10.Text = NumericUpDown2.Value * 24
            NumericUpDown2.BackColor = TextBox10.BackColor
        End If
    End Sub

    Private Sub BroodwarUnitFlag_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOX(CheckBox2, _OBJECTNUM)
        End If
    End Sub

    Private Sub ListView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView2.ItemChecked
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOXLIST(ListView2, _OBJECTNUM)
        End If
    End Sub

    Private Sub Search_TextChanged(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyUp
        LISTFILTER = TextBox2.Text
        ListDraw()
        PaletDraw()
    End Sub

    Private Sub AddonHorizontalXPosition_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown8.ValueChanged, NumericUpDown8.KeyUp
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToNUMERIC(NumericUpDown8, _OBJECTNUM)
        End If
    End Sub

    Private Sub AddonVerticalYPosition_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown7.ValueChanged, NumericUpDown7.KeyUp
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToNUMERIC(NumericUpDown7, _OBJECTNUM)
        End If
    End Sub


    Private Sub UnitDirection_TextChanged(sender As Object, e As EventArgs) Handles TextBox38.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox38, ComboBox21)
        End If
    End Sub

    Private Sub UnitDirection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox21.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox21, _OBJECTNUM)
            TextBox38.Text = ComboBox21.SelectedIndex
        End If
    End Sub

    Private Sub ReadySound_TextChanged(sender As Object, e As EventArgs) Handles TextBox27.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox27, ComboBox10)
            LoadSoundlist()
        End If
    End Sub

    Private Sub ReadySound_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox10.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox10, _OBJECTNUM)
            TextBox27.Text = ComboBox10.SelectedIndex
            LoadSoundlist()
        End If
    End Sub

    Private Sub YesSoundStart_TextChanged(sender As Object, e As EventArgs) Handles TextBox28.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox28, ComboBox11)
            LoadSoundlist()
        End If
    End Sub

    Private Sub YesSoundStart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox11.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox11, _OBJECTNUM)
            TextBox28.Text = ComboBox11.SelectedIndex
            LoadSoundlist()
        End If
    End Sub

    Private Sub YesSoundEnd_TextChanged(sender As Object, e As EventArgs) Handles TextBox29.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox29, ComboBox12)
            LoadSoundlist()
        End If
    End Sub

    Private Sub YesSoundEnd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox12.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox12, _OBJECTNUM)
            TextBox29.Text = ComboBox12.SelectedIndex
            LoadSoundlist()
        End If
    End Sub

    Private Sub WhatSoundStart_TextChanged(sender As Object, e As EventArgs) Handles TextBox31.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox31, ComboBox14)
            LoadSoundlist()
        End If
    End Sub

    Private Sub WhatSoundStart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox14.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox14, _OBJECTNUM)
            TextBox31.Text = ComboBox14.SelectedIndex
            LoadSoundlist()
        End If
    End Sub

    Private Sub WhatSoundEnd_TextChanged(sender As Object, e As EventArgs) Handles TextBox30.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox30, ComboBox13)
            LoadSoundlist()
        End If
    End Sub

    Private Sub WhatSoundEnd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox13.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox13, _OBJECTNUM)
            TextBox30.Text = ComboBox13.SelectedIndex
            LoadSoundlist()
        End If
    End Sub

    Private Sub PissSoundStart_TextChanged(sender As Object, e As EventArgs) Handles TextBox33.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox33, ComboBox16)
            LoadSoundlist()
        End If
    End Sub

    Private Sub PissSoundStart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox16.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox16, _OBJECTNUM)
            TextBox33.Text = ComboBox16.SelectedIndex
            LoadSoundlist()
        End If
    End Sub

    Private Sub PissSoundEnd_TextChanged(sender As Object, e As EventArgs) Handles TextBox32.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox32, ComboBox15)
            LoadSoundlist()
        End If
    End Sub

    Private Sub PissSoundEnd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox15.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox15, _OBJECTNUM)
            TextBox32.Text = ComboBox15.SelectedIndex
            LoadSoundlist()
        End If
    End Sub

    Private Sub MaxGroundHits_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox14, _OBJECTNUM)
        End If
    End Sub

    Private Sub MaxAirHits_TextChanged(sender As Object, e As EventArgs) Handles TextBox13.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox13, _OBJECTNUM)
        End If
    End Sub

    Private Sub SupplyRequired_TextChanged(sender As Object, e As EventArgs) Handles TextBox15.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox15, _OBJECTNUM)
        End If
    End Sub

    Private Sub SupplyProvided_TextChanged(sender As Object, e As EventArgs) Handles TextBox16.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox16, _OBJECTNUM)
        End If
    End Sub

    Private Sub SpaceRequired_TextChanged(sender As Object, e As EventArgs) Handles TextBox17.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox17, _OBJECTNUM)
        End If
    End Sub

    Private Sub SpaceProvided_TextChanged(sender As Object, e As EventArgs) Handles TextBox18.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox18, _OBJECTNUM)
        End If
    End Sub

    Private Sub BuildScore_TextChanged(sender As Object, e As EventArgs) Handles TextBox19.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox19, _OBJECTNUM)
        End If
    End Sub

    Private Sub DestroyScore_TextChanged(sender As Object, e As EventArgs) Handles TextBox20.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox20, _OBJECTNUM)
        End If
    End Sub

    Private Sub TargetAcquisitionRange_TextChanged(sender As Object, e As EventArgs) Handles TextBox23.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox23, _OBJECTNUM)
        End If
    End Sub

    Private Sub SightRange_TextChanged(sender As Object, e As EventArgs) Handles TextBox22.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox22, _OBJECTNUM)
        End If
    End Sub

    Private Sub UnitSize_TextChanged(sender As Object, e As EventArgs) Handles TextBox21.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox21, ComboBox6)
        End If
    End Sub

    Private Sub UnitSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox6, _OBJECTNUM)
            TextBox21.Text = ComboBox6.SelectedIndex
        End If
    End Sub

    Private Sub GroundWeapon_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox11, _OBJECTNUM)
            Try
                ComboBox4.SelectedIndex = TextBox11.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox4, _OBJECTNUM)
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.weapons).ReadValue("Icon", TextBox11.Text)
                PictureBox2.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
                ComboBox4.Enabled = True
                ComboBox4.Visible = True
            Catch ex As Exception
                PictureBox2.Image = ICONILIST.Images(4)
                ComboBox4.SelectedIndex = -1
                ComboBox4.Enabled = False
                ComboBox4.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub GroundWeapon_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox4, _OBJECTNUM)

            If ComboBox4.SelectedIndex = 130 Then
                loadSTATUS = False
                TextBox11.Text = ComboBox4.SelectedIndex
                loadSTATUS = True
                PictureBox2.Image = ICONILIST.Images(4)
            Else

                TextBox11.Text = ComboBox4.SelectedIndex
                Try
                    Dim tempinteger2 As Integer = DatEditDATA(DTYPE.weapons).ReadValue("Icon", TextBox11.Text)
                    PictureBox2.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
                Catch ex As Exception
                    PictureBox2.Image = ICONILIST.Images(4)
                End Try

            End If
        End If
    End Sub

    Private Sub AirWeapon_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox12, _OBJECTNUM)
            Try
                ComboBox5.SelectedIndex = TextBox12.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox5, _OBJECTNUM)
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.weapons).ReadValue("Icon", TextBox12.Text)
                PictureBox3.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
                ComboBox5.Enabled = True
                ComboBox5.Visible = True
            Catch ex As Exception
                PictureBox3.Image = ICONILIST.Images(4)
                ComboBox5.SelectedIndex = -1
                ComboBox5.Enabled = False
                ComboBox5.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub AirWeapon_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox5, _OBJECTNUM)

            If ComboBox5.SelectedIndex = 130 Then
                loadSTATUS = False
                TextBox12.Text = ComboBox5.SelectedIndex
                loadSTATUS = True
                PictureBox3.Image = ICONILIST.Images(4)
            Else

                TextBox12.Text = ComboBox5.SelectedIndex
                Try
                    Dim tempinteger2 As Integer = DatEditDATA(DTYPE.weapons).ReadValue("Icon", TextBox12.Text)
                    PictureBox3.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
                Catch ex As Exception
                    PictureBox3.Image = ICONILIST.Images(4)
                End Try
            End If
        End If
    End Sub

    Private Sub Infestation_TextChanged(sender As Object, e As EventArgs) Handles TextBox24.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox24, _OBJECTNUM)
            Try
                ComboBox7.SelectedIndex = TextBox24.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox7, _OBJECTNUM)
                Dim tempinteger2 As Integer = TextBox24.Text
                PictureBox5.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
                ComboBox7.Enabled = True
                ComboBox7.Visible = True
            Catch ex As Exception
                PictureBox5.Image = ICONILIST.Images(4)
                ComboBox7.SelectedIndex = -1
                ComboBox7.Enabled = False
                ComboBox7.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub Infestation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox7, _OBJECTNUM)
            TextBox24.Text = ComboBox7.SelectedIndex
            Try
                Dim tempinteger2 As Integer = TextBox24.Text
                PictureBox5.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox5.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub

    Private Sub Subunit1_TextChanged(sender As Object, e As EventArgs) Handles TextBox25.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox25, _OBJECTNUM)
            Try
                ComboBox8.SelectedIndex = TextBox25.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox8, _OBJECTNUM)
                Dim tempinteger2 As Integer = TextBox25.Text
                PictureBox6.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
                ComboBox8.Enabled = True
                ComboBox8.Visible = True
            Catch ex As Exception
                PictureBox6.Image = ICONILIST.Images(4)
                ComboBox8.SelectedIndex = -1
                ComboBox8.Enabled = False
                ComboBox8.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub Subunit1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox8, _OBJECTNUM)
            TextBox25.Text = ComboBox8.SelectedIndex
            Try
                Dim tempinteger2 As Integer = TextBox25.Text
                PictureBox6.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox6.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub

    Private Sub TextBox26_TextChanged(sender As Object, e As EventArgs) Handles TextBox26.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox26, _OBJECTNUM)
            Try
                ComboBox9.SelectedIndex = TextBox26.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox9, _OBJECTNUM)
                Dim tempinteger2 As Integer = TextBox26.Text
                PictureBox7.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
                ComboBox9.Enabled = True
                ComboBox9.Visible = True
            Catch ex As Exception
                PictureBox7.Image = ICONILIST.Images(4)
                ComboBox9.SelectedIndex = -1
                ComboBox9.Enabled = False
                ComboBox9.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub ComboBox9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox9.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox9, _OBJECTNUM)
            TextBox26.Text = ComboBox9.SelectedIndex
            Try
                Dim tempinteger2 As Integer = TextBox26.Text
                PictureBox7.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox7.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub

    Private Sub UnknownoldMovement_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView4.ItemChecked
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOXLIST(ListView4, _OBJECTNUM)
        End If
    End Sub

    Private Sub Graphics_TextChanged(sender As Object, e As EventArgs) Handles TextBox36.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox36, ComboBox19)
            LoadUnitGRP()
            drawUnitGRP()
        End If
    End Sub

    Private Sub Graphics_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox19.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox19, _OBJECTNUM)
            TextBox36.Text = ComboBox19.SelectedIndex
            LoadUnitGRP()
            drawUnitGRP()
        End If
    End Sub

    Private Sub ConstructionAnimation_TextChanged(sender As Object, e As EventArgs) Handles TextBox35.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox35, ComboBox18)
            LoadUnitGRP()
            drawUnitGRP()
        End If
    End Sub

    Private Sub ConstructionAnimation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox18.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox18, _OBJECTNUM)
            TextBox35.Text = ComboBox18.SelectedIndex
            LoadUnitGRP()
            drawUnitGRP()
        End If
    End Sub

    Private Sub Portrait_TextChanged(sender As Object, e As EventArgs) Handles TextBox34.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox34, ComboBox17)
        End If
    End Sub

    Private Sub Portrait_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox17.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox17, _OBJECTNUM)
            TextBox34.Text = ComboBox17.SelectedIndex
        End If
    End Sub

    Private Sub ElevationLevel_TextChanged(sender As Object, e As EventArgs) Handles TextBox37.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox37, ComboBox20)
        End If
    End Sub

    Private Sub ElevationLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox20.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox20, _OBJECTNUM)
            TextBox37.Text = ComboBox20.SelectedIndex
        End If
    End Sub

    Private Sub NumericUpDown3_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown3.ValueChanged, NumericUpDown3.KeyUp
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToNUMERIC(NumericUpDown3, _OBJECTNUM)
        End If
    End Sub

    Private Sub NumericUpDown4_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown4.ValueChanged, NumericUpDown4.KeyUp
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToNUMERIC(NumericUpDown4, _OBJECTNUM)
        End If
    End Sub

    Private Sub NumericUpDown5_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown5.ValueChanged, NumericUpDown5.KeyUp
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToNUMERIC(NumericUpDown5, _OBJECTNUM)
        End If
    End Sub

    Private Sub NumericUpDown6_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown6.ValueChanged, NumericUpDown6.KeyUp
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToNUMERIC(NumericUpDown6, _OBJECTNUM)
        End If
    End Sub

    Private Sub NumericUpDown10_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown10.ValueChanged, NumericUpDown10.KeyUp
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToNUMERIC(NumericUpDown10, _OBJECTNUM)
        End If
    End Sub

    Private Sub NumericUpDown9_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown9.ValueChanged, NumericUpDown9.KeyUp
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToNUMERIC(NumericUpDown9, _OBJECTNUM)
        End If
    End Sub

    Private Sub StareditAvailabilityFlags_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView5.ItemChecked
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOXLIST(ListView5, _OBJECTNUM)
        End If
    End Sub

    Private Sub StareditGroupFlags_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView6.ItemChecked
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOXLIST(ListView6, _OBJECTNUM)
        End If
    End Sub

    Private Sub RankSublabel_TextChanged(sender As Object, e As EventArgs) Handles TextBox45.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox45, ComboBox22)
        End If
    End Sub

    Private Sub RankSublabel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox22.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox22, _OBJECTNUM)
            TextBox45.Text = ComboBox22.SelectedIndex
        End If
    End Sub

    Private Sub UnitMapString_TextChanged(sender As Object, e As EventArgs) Handles TextBox46.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox46, ComboBox23)

            Dim value As Integer = -1 + DatEditDATA(DTYPE.units).ReadValue("Unit Map String", _OBJECTNUM)
            If value >= 0 Then
                Try
                    ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & ProjectSet.CHKSTRING(value)
                Catch ex As Exception
                    ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & Stringisnot
                End Try
                ListBox1.Refresh()
            Else
                ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & CODE(DTYPE.units)(_OBJECTNUM)
                ListBox1.Refresh()
            End If
            Label24.Text = ListBox1.SelectedItem(0) & " (" & CODE(DTYPE.units)(ListBox1.SelectedItem(1)) & ")"
        End If
    End Sub

    Private Sub UnitMapString_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox23.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox23, _OBJECTNUM)
            TextBox46.Text = ComboBox23.SelectedIndex

            Dim value As Integer = -1 + DatEditDATA(DTYPE.units).ReadValue("Unit Map String", _OBJECTNUM)
            If value >= 0 Then
                Try
                    ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & ProjectSet.CHKSTRING(value)
                Catch ex As Exception
                    ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & Stringisnot
                End Try
                ListBox1.Refresh()
            Else
                ListBox1.SelectedItem(0) = "[" & Format(_OBJECTNUM, "000") & "]- " & CODE(DTYPE.units)(_OBJECTNUM)
                ListBox1.Refresh()
            End If
            Label24.Text = ListBox1.SelectedItem(0) & " (" & CODE(DTYPE.units)(ListBox1.SelectedItem(1)) & ")"
        End If
    End Sub

    Private Sub AIInternal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView7.ItemChecked
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOXLIST(ListView7, _OBJECTNUM)
        End If
    End Sub

    Private Sub RightclickAction_TextChanged(sender As Object, e As EventArgs) Handles TextBox54.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox54, ComboBox29)
        End If
    End Sub

    Private Sub RightclickAction_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox29.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox29, _OBJECTNUM)
            TextBox54.Text = ComboBox29.SelectedIndex
        End If
    End Sub

    Private Sub CompAIIdle_TextChanged(sender As Object, e As EventArgs) Handles TextBox53.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox53, _OBJECTNUM)
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox53.Text)
                PictureBox11.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox11.Image = ICONILIST.Images(4)
            End Try
            Try
                ComboBox28.SelectedIndex = TextBox53.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox28, _OBJECTNUM)
                ComboBox28.Enabled = True
                ComboBox28.Visible = True
            Catch ex As Exception
                ComboBox28.SelectedIndex = -1
                ComboBox28.Enabled = False
                ComboBox28.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub CompAIIdle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox28.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox28, _OBJECTNUM)
            TextBox53.Text = ComboBox28.SelectedIndex
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox53.Text)
                PictureBox11.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox11.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub

    Private Sub HumanAIIdle_TextChanged(sender As Object, e As EventArgs) Handles TextBox52.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox52, _OBJECTNUM)
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox52.Text)
                PictureBox12.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox12.Image = ICONILIST.Images(4)
            End Try
            Try
                ComboBox27.SelectedIndex = TextBox52.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox27, _OBJECTNUM)
                ComboBox27.Enabled = True
                ComboBox27.Visible = True
            Catch ex As Exception
                ComboBox27.SelectedIndex = -1
                ComboBox27.Enabled = False
                ComboBox27.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub HumanAIIdle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox27.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox27, _OBJECTNUM)
            TextBox52.Text = ComboBox27.SelectedIndex
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox52.Text)
                PictureBox12.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox12.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub

    Private Sub ReturntoIdle_TextChanged(sender As Object, e As EventArgs) Handles TextBox51.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox51, _OBJECTNUM)
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox51.Text)
                PictureBox13.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox13.Image = ICONILIST.Images(4)
            End Try
            Try
                ComboBox26.SelectedIndex = TextBox51.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox26, _OBJECTNUM)
                ComboBox26.Enabled = True
                ComboBox26.Visible = True
            Catch ex As Exception
                ComboBox26.SelectedIndex = -1
                ComboBox26.Enabled = False
                ComboBox26.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub ReturntoIdle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox26.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox26, _OBJECTNUM)
            TextBox51.Text = ComboBox26.SelectedIndex
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox51.Text)
                PictureBox13.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox13.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub

    Private Sub AttackUnit_TextChanged(sender As Object, e As EventArgs) Handles TextBox50.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox50, _OBJECTNUM)
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox50.Text)
                PictureBox14.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox14.Image = ICONILIST.Images(4)
            End Try
            Try
                ComboBox25.SelectedIndex = TextBox50.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox25, _OBJECTNUM)
                ComboBox25.Enabled = True
                ComboBox25.Visible = True
            Catch ex As Exception
                ComboBox25.SelectedIndex = -1
                ComboBox25.Enabled = False
                ComboBox25.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub AttackUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox25.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox25, _OBJECTNUM)
            TextBox50.Text = ComboBox25.SelectedIndex
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox50.Text)
                PictureBox14.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox14.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub

    Private Sub AttackMove_TextChanged(sender As Object, e As EventArgs) Handles TextBox49.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox49, _OBJECTNUM)
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox49.Text)
                PictureBox15.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox15.Image = ICONILIST.Images(4)
            End Try
            Try
                ComboBox24.SelectedIndex = TextBox49.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox24, _OBJECTNUM)
                ComboBox24.Enabled = True
                ComboBox24.Visible = True
            Catch ex As Exception
                ComboBox24.SelectedIndex = -1
                ComboBox24.Enabled = False
                ComboBox24.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub AttackMove_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox24.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox24, _OBJECTNUM)
            TextBox49.Text = ComboBox24.SelectedIndex
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.orders).ReadValue("Highlight", TextBox49.Text)
                PictureBox15.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox15.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox1, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox3, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox39_TextChanged(sender As Object, e As EventArgs) Handles TextBox39.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox39, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox40_TextChanged(sender As Object, e As EventArgs) Handles TextBox40.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox40, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox43_TextChanged(sender As Object, e As EventArgs) Handles TextBox43.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox43, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox44_TextChanged(sender As Object, e As EventArgs) Handles TextBox44.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox44, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox56_TextChanged(sender As Object, e As EventArgs) Handles TextBox56.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox56, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox55_TextChanged(sender As Object, e As EventArgs) Handles TextBox55.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox55, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox57_TextChanged(sender As Object, e As EventArgs) Handles TextBox57.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox57, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox65_TextChanged(sender As Object, e As EventArgs) Handles TextBox65.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox65, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox64_TextChanged(sender As Object, e As EventArgs) Handles TextBox64.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox64, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox67_TextChanged(sender As Object, e As EventArgs) Handles TextBox67.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox67, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox66_TextChanged(sender As Object, e As EventArgs) Handles TextBox66.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox66, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox62_TextChanged(sender As Object, e As EventArgs) Handles TextBox62.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox62, _OBJECTNUM)
        End If
    End Sub



    Private Sub TextBox42_TextChanged(sender As Object, e As EventArgs) Handles TextBox42.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox42, ComboBox3)
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox3, _OBJECTNUM)
            TextBox42.Text = ComboBox3.SelectedIndex
        End If
    End Sub

    Private Sub TextBox47_TextChanged(sender As Object, e As EventArgs) Handles TextBox47.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox47, ComboBox30)
        End If
    End Sub

    Private Sub ComboBox30_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox30.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox30, _OBJECTNUM)
            TextBox47.Text = ComboBox30.SelectedIndex
        End If
    End Sub

    Private Sub TextBox48_TextChanged(sender As Object, e As EventArgs) Handles TextBox48.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox48, ComboBox31)
        End If
    End Sub

    Private Sub ComboBox31_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox31.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox31, _OBJECTNUM)
            TextBox48.Text = ComboBox31.SelectedIndex
        End If
    End Sub

    Private Sub TextBox58_TextChanged(sender As Object, e As EventArgs) Handles TextBox58.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox58, ComboBox32)
        End If
    End Sub

    Private Sub ComboBox32_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox32.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox32, _OBJECTNUM)
            TextBox58.Text = ComboBox32.SelectedIndex
        End If
    End Sub

    Private Sub TextBox59_TextChanged(sender As Object, e As EventArgs) Handles TextBox59.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox59, ComboBox33)
        End If
    End Sub

    Private Sub ComboBox33_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox33.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox33, _OBJECTNUM)
            TextBox59.Text = ComboBox33.SelectedIndex
        End If
    End Sub

    Private Sub TextBox60_TextChanged(sender As Object, e As EventArgs) Handles TextBox60.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox60, ComboBox34)
        End If
    End Sub

    Private Sub ComboBox34_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox34.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox34, _OBJECTNUM)
            TextBox60.Text = ComboBox34.SelectedIndex
        End If
    End Sub

    Private Sub TextBox61_TextChanged(sender As Object, e As EventArgs) Handles TextBox61.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox61, ComboBox35)
            LoadWeaponGRP()
            drawWeaponGRP()
        End If
    End Sub

    Private Sub ComboBox35_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox35.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox35, _OBJECTNUM)
            TextBox61.Text = ComboBox35.SelectedIndex
            LoadWeaponGRP()
            drawWeaponGRP()
        End If
    End Sub

    Private Sub ListView8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView8.ItemChecked
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOXLIST(ListView8, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox41_TextChanged(sender As Object, e As EventArgs) Handles TextBox41.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox41, _OBJECTNUM)
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.upgrades).ReadValue("Icon", TextBox41.Text)
                PictureBox16.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox16.Image = ICONILIST.Images(4)
            End Try
            Try
                ComboBox2.SelectedIndex = TextBox41.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox2, _OBJECTNUM)
                ComboBox2.Enabled = True
                ComboBox2.Visible = True
            Catch ex As Exception
                ComboBox2.SelectedIndex = -1
                ComboBox2.Enabled = False
                ComboBox2.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub





    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox2, _OBJECTNUM)
            TextBox41.Text = ComboBox2.SelectedIndex
            Try
                Dim tempinteger2 As Integer = DatEditDATA(DTYPE.upgrades).ReadValue("Icon", TextBox41.Text)
                PictureBox16.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox16.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub

    Private Sub TextBox63_TextChanged(sender As Object, e As EventArgs) Handles TextBox63.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox63, _OBJECTNUM)
            Try
                Dim tempinteger2 As Integer = TextBox63.Text
                PictureBox18.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox18.Image = ICONILIST.Images(4)
            End Try
            Try
                ComboBox36.SelectedIndex = TextBox63.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox36, _OBJECTNUM)
                ComboBox36.Enabled = True
                ComboBox36.Visible = True
            Catch ex As Exception
                ComboBox36.SelectedIndex = -1
                ComboBox36.Enabled = False
                ComboBox36.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub ComboBox36_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox36.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox36, _OBJECTNUM)
            TextBox63.Text = ComboBox36.SelectedIndex
            Try
                Dim tempinteger2 As Integer = TextBox63.Text
                PictureBox18.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox18.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub


    Private Sub TextBox68_TextChanged(sender As Object, e As EventArgs) Handles TextBox68.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox68, ComboBox37)
            LoadflingyGRP()
            drawflingyGRP()
        End If
    End Sub

    Private Sub ComboBox37_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox37.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox37, _OBJECTNUM)
            TextBox68.Text = ComboBox37.SelectedIndex
            LoadflingyGRP()
            drawflingyGRP()
        End If
    End Sub

    Private Sub TextBox69_TextChanged(sender As Object, e As EventArgs) Handles TextBox69.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox69, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox70_TextChanged(sender As Object, e As EventArgs) Handles TextBox70.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox70, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox71_TextChanged(sender As Object, e As EventArgs) Handles TextBox71.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox71, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox72_TextChanged(sender As Object, e As EventArgs) Handles TextBox72.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox72, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox73_TextChanged(sender As Object, e As EventArgs) Handles TextBox73.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox73, ComboBox38)
        End If
    End Sub

    Private Sub ComboBox38_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox38.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox38, _OBJECTNUM)
            TextBox73.Text = ComboBox38.SelectedIndex
        End If
    End Sub

    Private Sub TextBox74_TextChanged(sender As Object, e As EventArgs) Handles TextBox74.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox74, ComboBox39)
            LoadSpriteGRP()
            drawSpriteGRP()
        End If
    End Sub

    Private Sub ComboBox39_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox39.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox39, _OBJECTNUM)
            TextBox74.Text = ComboBox39.SelectedIndex
            LoadSpriteGRP()
            drawSpriteGRP()
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOX(CheckBox6, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox75_TextChanged(sender As Object, e As EventArgs) Handles TextBox75.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox75, ComboBox40)
        End If
    End Sub

    Private Sub ComboBox40_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox40.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox40, _OBJECTNUM)
            TextBox75.Text = ComboBox40.SelectedIndex
        End If
    End Sub

    Private Sub TextBox77_TextChanged(sender As Object, e As EventArgs) Handles TextBox77.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox77, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox76_TextChanged(sender As Object, e As EventArgs) Handles TextBox76.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox76, _OBJECTNUM)
            Try
                NumericUpDown13.Value = TextBox76.Text \ 3
            Catch ex As Exception
            End Try
            NumericUpDown13.BackColor = TextBox76.BackColor
            NumericUpDown13.Visible = TextBox76.Visible
        End If
    End Sub

    Private Sub NumericUpDown13_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown13.ValueChanged
        If loadSTATUS = True Then
            TextBox76.Text = NumericUpDown13.Value * 3

            NumericUpDown13.BackColor = TextBox76.BackColor
            NumericUpDown13.Visible = TextBox76.Visible
        End If
    End Sub



    Private Sub TextBox78_TextChanged(sender As Object, e As EventArgs) Handles TextBox78.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox78, _OBJECTNUM)
            Try
                Dim tempinteger2 As Integer = TextBox78.Text
                PictureBox23.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox23.Image = ICONILIST.Images(4)
            End Try
            Try
                ComboBox41.SelectedIndex = TextBox78.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox41, _OBJECTNUM)
                ComboBox41.Enabled = True
                ComboBox41.Visible = True
            Catch ex As Exception
                ComboBox41.SelectedIndex = -1
                ComboBox41.Enabled = False
                ComboBox41.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub ComboBox41_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox41.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox41, _OBJECTNUM)
            TextBox78.Text = ComboBox41.SelectedIndex
            Try
                Dim tempinteger2 As Integer = TextBox78.Text
                PictureBox23.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox23.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub

    Private Sub TextBox79_TextChanged(sender As Object, e As EventArgs) Handles TextBox79.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox79, ComboBox42)
        End If
    End Sub

    Private Sub ComboBox42_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox42.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox42, _OBJECTNUM)
            TextBox79.Text = ComboBox42.SelectedIndex
        End If
    End Sub

    Private Sub TextBox82_TextChanged(sender As Object, e As EventArgs) Handles TextBox82.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox82, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox81_TextChanged(sender As Object, e As EventArgs) Handles TextBox81.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox81, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox80_TextChanged(sender As Object, e As EventArgs) Handles TextBox80.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox80, _OBJECTNUM)
            Try
                NumericUpDown14.Value = TextBox80.Text / 24
            Catch ex As Exception

            End Try
            NumericUpDown14.BackColor = TextBox80.BackColor
            NumericUpDown14.Visible = TextBox80.Visible
        End If
    End Sub

    Private Sub NumericUpDown14_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown14.ValueChanged
        If loadSTATUS = True Then
            TextBox80.Text = NumericUpDown14.Value * 24

            NumericUpDown14.BackColor = TextBox80.BackColor
            NumericUpDown14.Visible = TextBox80.Visible
        End If
    End Sub

    Private Sub TextBox85_TextChanged(sender As Object, e As EventArgs) Handles TextBox85.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox85, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox84_TextChanged(sender As Object, e As EventArgs) Handles TextBox84.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox84, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox83_TextChanged(sender As Object, e As EventArgs) Handles TextBox83.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox83, _OBJECTNUM)
            Try
                NumericUpDown15.Value = TextBox83.Text / 24
            Catch ex As Exception

            End Try
            NumericUpDown15.BackColor = TextBox83.BackColor
            NumericUpDown15.Visible = TextBox83.Visible
        End If
    End Sub

    Private Sub NumericUpDown15_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown15.ValueChanged
        If loadSTATUS = True Then
            TextBox83.Text = NumericUpDown15.Value * 24

            NumericUpDown15.BackColor = TextBox83.BackColor
            NumericUpDown15.Visible = TextBox83.Visible
        End If
    End Sub

    Private Sub TextBox87_TextChanged(sender As Object, e As EventArgs) Handles TextBox87.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox87, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox88_TextChanged(sender As Object, e As EventArgs) Handles TextBox88.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox88, ComboBox43)
        End If
    End Sub

    Private Sub ComboBox43_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox43.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox43, _OBJECTNUM)
            TextBox88.Text = ComboBox43.SelectedIndex
        End If
    End Sub

    Private Sub CheckBox8_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox8.CheckedChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOX(CheckBox8, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox92_TextChanged(sender As Object, e As EventArgs) Handles TextBox92.TextChanged
        If loadSTATUS = True Then
            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox92, _OBJECTNUM)
            Try
                Dim tempinteger2 As Integer = TextBox92.Text
                PictureBox24.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox24.Image = ICONILIST.Images(4)
            End Try
            Try
                ComboBox45.SelectedIndex = TextBox92.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox45, _OBJECTNUM)
                ComboBox45.Enabled = True
                ComboBox45.Visible = True
            Catch ex As Exception
                ComboBox45.SelectedIndex = -1
                ComboBox45.Enabled = False
                ComboBox45.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub ComboBox45_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox45.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox45, _OBJECTNUM)
            TextBox92.Text = ComboBox45.SelectedIndex
            Try
                Dim tempinteger2 As Integer = TextBox92.Text
                PictureBox24.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox24.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub

    Private Sub TextBox91_TextChanged(sender As Object, e As EventArgs) Handles TextBox91.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox91, ComboBox44)
        End If
    End Sub

    Private Sub ComboBox44_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox44.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox44, _OBJECTNUM)
            TextBox91.Text = ComboBox44.SelectedIndex
        End If
    End Sub

    Private Sub TextBox90_TextChanged(sender As Object, e As EventArgs) Handles TextBox90.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox90, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox89_TextChanged(sender As Object, e As EventArgs) Handles TextBox89.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox89, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox86_TextChanged(sender As Object, e As EventArgs) Handles TextBox86.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox86, _OBJECTNUM)
            Try
                NumericUpDown16.Value = TextBox86.Text / 24
            Catch ex As Exception

            End Try
            NumericUpDown16.BackColor = TextBox86.BackColor
            NumericUpDown16.Visible = TextBox86.Visible
        End If
    End Sub

    Private Sub NumericUpDown16_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown16.ValueChanged
        If loadSTATUS = True Then
            TextBox86.Text = NumericUpDown16.Value * 24

            NumericUpDown16.BackColor = TextBox86.BackColor
            NumericUpDown16.Visible = TextBox86.Visible
        End If
    End Sub

    Private Sub TextBox94_TextChanged(sender As Object, e As EventArgs) Handles TextBox94.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox94, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox93_TextChanged(sender As Object, e As EventArgs) Handles TextBox93.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox93, ComboBox46)
        End If
    End Sub

    Private Sub ComboBox46_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox46.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox46, _OBJECTNUM)
            TextBox93.Text = ComboBox46.SelectedIndex
        End If
    End Sub

    Private Sub CheckBox9_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox9.CheckedChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOX(CheckBox9, _OBJECTNUM)
        End If
    End Sub





    Private Sub TextBox95_TextChanged(sender As Object, e As EventArgs) Handles TextBox95.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox95, ComboBox47)
        End If
    End Sub

    Private Sub ComboBox47_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox47.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox47, _OBJECTNUM)
            TextBox95.Text = ComboBox47.SelectedIndex
        End If
    End Sub

    Private Sub TextBox96_TextChanged(sender As Object, e As EventArgs) Handles TextBox96.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox96, ComboBox48)
        End If
    End Sub

    Private Sub ComboBox48_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox48.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox48, _OBJECTNUM)
            TextBox96.Text = ComboBox48.SelectedIndex
        End If
    End Sub

    Private Sub TextBox97_TextChanged(sender As Object, e As EventArgs) Handles TextBox97.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox97, ComboBox49)
        End If
    End Sub

    Private Sub ComboBox49_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox49.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox49, _OBJECTNUM)
            TextBox97.Text = ComboBox49.SelectedIndex
        End If
    End Sub

    Private Sub TextBox98_TextChanged(sender As Object, e As EventArgs) Handles TextBox98.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox98, ComboBox50)
        End If
    End Sub

    Private Sub ComboBox50_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox50.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox50, _OBJECTNUM)
            TextBox98.Text = ComboBox50.SelectedIndex
        End If
    End Sub

    Private Sub TextBox99_TextChanged(sender As Object, e As EventArgs) Handles TextBox99.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox99, ComboBox51)
        End If
    End Sub

    Private Sub ComboBox51_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox51.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox51, _OBJECTNUM)
            TextBox99.Text = ComboBox51.SelectedIndex
        End If
    End Sub

    Private Sub TextBox100_TextChanged(sender As Object, e As EventArgs) Handles TextBox100.TextChanged
        If loadSTATUS = True Then

            loadSTATUS = False
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox100, _OBJECTNUM)
            Try
                Dim tempinteger2 As Integer = TextBox100.Text
                PictureBox25.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox25.Image = ICONILIST.Images(4)
            End Try
            Try
                ComboBox52.SelectedIndex = TextBox100.Text
                DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox52, _OBJECTNUM)
                ComboBox52.Enabled = True
                ComboBox52.Visible = True
            Catch ex As Exception
                ComboBox52.SelectedIndex = -1
                ComboBox52.Enabled = False
                ComboBox52.Visible = False
                loadSTATUS = True
            End Try
            loadSTATUS = True
        End If
    End Sub

    Private Sub ComboBox52_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox52.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox52, _OBJECTNUM)
            TextBox100.Text = ComboBox52.SelectedIndex
            Try
                Dim tempinteger2 As Integer = TextBox100.Text
                PictureBox25.Image = ICONILIST.Images(tempinteger2) '무기 아이콘
            Catch ex As Exception
                PictureBox25.Image = ICONILIST.Images(4)
            End Try
        End If
    End Sub



    Private Sub ListView9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView9.ItemChecked
        If loadSTATUS = True And FristRunOpenOrders = True Then
            'ListView8.Items(0).Checked
            For i = 0 To 11
                Dim strings As String = DatEditDATA(DTYPE.orders).keyDic.Keys(i + 1)

                If ListView9.Items(i).Checked = True Then
                    DatEditDATA(DTYPE.orders).WriteValue(strings, _OBJECTNUM, 1)
                Else
                    DatEditDATA(DTYPE.orders).WriteValue(strings, _OBJECTNUM, 0)
                End If
                DatEditDATA(DTYPE.orders).CheckChange(strings, _OBJECTNUM, ListView9.Items(i))


                Dim value As Boolean = DatEditDATA(DTYPE.orders).ReadValue(strings, _OBJECTNUM)
                If value = True And ListView9.Items(i).BackColor = ProgramSet.BACKCOLOR Then
                    ListView9.Items(i).BackColor = ProgramSet.LISTCOLOR
                End If
            Next

            'DatEditDAT(TAB_INDEX).WriteToCHECKBOXLIST(ListView8, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox101_TextChanged(sender As Object, e As EventArgs) Handles TextBox101.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox101, ComboBox53)
        End If
    End Sub

    Private Sub ComboBox53_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox53.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox53, _OBJECTNUM)
            TextBox101.Text = ComboBox53.SelectedIndex
        End If
    End Sub

    Private Sub TextBox103_TextChanged(sender As Object, e As EventArgs) Handles TextBox103.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox103, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox102_TextChanged(sender As Object, e As EventArgs) Handles TextBox102.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox102, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox105_TextChanged(sender As Object, e As EventArgs) Handles TextBox105.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox105, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox104_TextChanged(sender As Object, e As EventArgs) Handles TextBox104.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox104, _OBJECTNUM)
        End If
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Dim mpq As New SFMpq
        If ComboBox53.SelectedIndex <> -1 Then
            If ComboBox53.SelectedItem <> "No sound" Then
                SoundPlay("sound\" & Replace(ComboBox53.SelectedItem, "(1)", "").Trim)
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button3.Tag, _OBJECTNUM)
            If CODE(DTYPE.weapons).Count > value + 1 Then
                MainTAB.SelectedIndex = DTYPE.weapons
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button4.Tag, _OBJECTNUM)
            If CODE(DTYPE.weapons).Count > value + 1 Then
                MainTAB.SelectedIndex = DTYPE.weapons
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button1.Tag, _OBJECTNUM)
            If CODE(DTYPE.units).Count > value + 1 Then
                MainTAB.SelectedIndex = DTYPE.units
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button2.Tag, _OBJECTNUM)
            If CODE(DTYPE.units).Count > value Then
                MainTAB.SelectedIndex = DTYPE.units
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button6.Tag, _OBJECTNUM)
            If CODE(DTYPE.units).Count > value Then
                MainTAB.SelectedIndex = DTYPE.units
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button12.Tag, _OBJECTNUM)
            If CODE(DTYPE.sfxdata).Count > value Then
                MainTAB.SelectedIndex = DTYPE.sfxdata
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button13.Tag, _OBJECTNUM)
            If CODE(DTYPE.sfxdata).Count > value Then
                MainTAB.SelectedIndex = DTYPE.sfxdata
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button10.Tag, _OBJECTNUM)
            If CODE(DTYPE.sfxdata).Count > value Then
                MainTAB.SelectedIndex = DTYPE.sfxdata
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button11.Tag, _OBJECTNUM)
            If CODE(DTYPE.sfxdata).Count > value Then
                MainTAB.SelectedIndex = DTYPE.sfxdata
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button9.Tag, _OBJECTNUM)
            If CODE(DTYPE.sfxdata).Count > value Then
                MainTAB.SelectedIndex = DTYPE.sfxdata
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button8.Tag, _OBJECTNUM)
            If CODE(DTYPE.sfxdata).Count > value Then
                MainTAB.SelectedIndex = DTYPE.sfxdata
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button7.Tag, _OBJECTNUM)
            If CODE(DTYPE.sfxdata).Count > value Then
                MainTAB.SelectedIndex = DTYPE.sfxdata
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button16.Tag, _OBJECTNUM)
            If CODE(DTYPE.flingy).Count > value Then
                MainTAB.SelectedIndex = DTYPE.flingy
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button15.Tag, _OBJECTNUM)
            If CODE(DTYPE.images).Count > value Then
                MainTAB.SelectedIndex = DTYPE.images
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button14.Tag, _OBJECTNUM)
            If CODE(DTYPE.portdata).Count > value Then
                MainTAB.SelectedIndex = DTYPE.portdata
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub




    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button21.Tag, _OBJECTNUM)
            If CODE(DTYPE.orders).Count > value + 1 Then
                MainTAB.SelectedIndex = DTYPE.orders
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button20.Tag, _OBJECTNUM)
            If CODE(DTYPE.orders).Count > value + 1 Then
                MainTAB.SelectedIndex = DTYPE.orders
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button19.Tag, _OBJECTNUM)
            If CODE(DTYPE.orders).Count > value + 1 Then
                MainTAB.SelectedIndex = DTYPE.orders
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button18.Tag, _OBJECTNUM)
            If CODE(DTYPE.orders).Count > value + 1 Then
                MainTAB.SelectedIndex = DTYPE.orders
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button17.Tag, _OBJECTNUM)
            If CODE(DTYPE.orders).Count > value + 1 Then
                MainTAB.SelectedIndex = DTYPE.orders
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try

    End Sub




    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.weapons).ReadValue(Button22.Tag, _OBJECTNUM)
            If CODE(DTYPE.flingy).Count > value Then
                MainTAB.SelectedIndex = DTYPE.flingy
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.flingy).ReadValue(Button23.Tag, _OBJECTNUM)
            If CODE(DTYPE.sprites).Count > value Then
                MainTAB.SelectedIndex = DTYPE.sprites
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.sprites).ReadValue(Button24.Tag, _OBJECTNUM)
            If CODE(DTYPE.images).Count > value Then
                MainTAB.SelectedIndex = DTYPE.images
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.sprites).ReadValue(Button25.Tag, _OBJECTNUM)
            If CODE(DTYPE.images).Count > value Then
                MainTAB.SelectedIndex = DTYPE.images
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.units).ReadValue(Button51.Tag, _OBJECTNUM)
            If CODE(DTYPE.upgrades).Count > value Then
                MainTAB.SelectedIndex = DTYPE.upgrades
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button52_Click(sender As Object, e As EventArgs) Handles Button52.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.weapons).ReadValue(Button52.Tag, _OBJECTNUM)
            If CODE(DTYPE.upgrades).Count > value Then
                MainTAB.SelectedIndex = DTYPE.upgrades
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.orders).ReadValue(Button26.Tag, _OBJECTNUM)
            If CODE(DTYPE.weapons).Count > value + 1 Then
                MainTAB.SelectedIndex = DTYPE.weapons
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Try
            Dim value As Integer = DatEditDATA(DTYPE.orders).ReadValue(Button27.Tag, _OBJECTNUM)
            If CODE(DTYPE.techdata).Count > value + 1 Then
                MainTAB.SelectedIndex = DTYPE.techdata
                SELECTLIST(value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox107_TextChanged(sender As Object, e As EventArgs) Handles TextBox107.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox107, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox108_TextChanged(sender As Object, e As EventArgs) Handles TextBox108.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox108, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox109_TextChanged(sender As Object, e As EventArgs) Handles TextBox109.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox109, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox110_TextChanged(sender As Object, e As EventArgs) Handles TextBox110.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox110, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox111_TextChanged(sender As Object, e As EventArgs) Handles TextBox111.TextChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToTEXTBOX(TextBox111, _OBJECTNUM)
        End If
    End Sub

    Private Sub CheckBox10_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox10.CheckedChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCHECKBOX(CheckBox10, _OBJECTNUM)
        End If
    End Sub

    Private Sub ListView10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView10.ItemChecked
        If loadSTATUS = True Then
            'ListView8.Items(0).Checked
            For i = 0 To 3
                Dim strings As String = DatEditDATA(DTYPE.images).keyDic.Keys(i + 1)

                If ListView10.Items(i).Checked = True Then
                    DatEditDATA(DTYPE.images).WriteValue(strings, _OBJECTNUM, 1)
                Else
                    DatEditDATA(DTYPE.images).WriteValue(strings, _OBJECTNUM, 0)
                End If
                DatEditDATA(DTYPE.images).CheckChange(strings, _OBJECTNUM, ListView10.Items(i))


                Dim value As Boolean = DatEditDATA(DTYPE.images).ReadValue(strings, _OBJECTNUM)
                If value = True And ListView10.Items(i).BackColor = ProgramSet.BACKCOLOR Then
                    ListView10.Items(i).BackColor = ProgramSet.LISTCOLOR
                End If
            Next

            'DatEditDAT(TAB_INDEX).WriteToCHECKBOXLIST(ListView8, _OBJECTNUM)
        End If
    End Sub

    Private Sub TextBox112_TextChanged(sender As Object, e As EventArgs) Handles TextBox112.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox112, ComboBox55)
            Try
                ListBox9.SelectedIndex = 0
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ComboBox55_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox55.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox55, _OBJECTNUM)
            TextBox112.Text = ComboBox55.SelectedIndex
            Try
                ListBox9.SelectedIndex = 0
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub TextBox113_TextChanged(sender As Object, e As EventArgs) Handles TextBox113.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox113, ComboBox56)
            Try
                ListBox9.SelectedIndex = 0
            Catch ex As Exception

            End Try
            'drawImageGRP(0, False)
        End If
    End Sub

    Private Sub ComboBox56_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox56.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox56, _OBJECTNUM)
            TextBox113.Text = ComboBox56.SelectedIndex
            Try
                ListBox9.SelectedIndex = 0
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub TextBox114_TextChanged(sender As Object, e As EventArgs) Handles TextBox114.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox114, ComboBox57)
        End If
    End Sub

    Private Sub ComboBox57_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox57.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox57, _OBJECTNUM)
            TextBox114.Text = ComboBox57.SelectedIndex
        End If
    End Sub

    Private Sub TextBox115_TextChanged(sender As Object, e As EventArgs) Handles TextBox115.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox115, ComboBox58)
        End If
    End Sub

    Private Sub ComboBox58_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox58.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox58, _OBJECTNUM)
            TextBox115.Text = ComboBox58.SelectedIndex
        End If
    End Sub

    Private Sub TextBox116_TextChanged(sender As Object, e As EventArgs) Handles TextBox116.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox116, ComboBox59)
        End If
    End Sub

    Private Sub ComboBox59_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox59.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox59, _OBJECTNUM)
            TextBox116.Text = ComboBox59.SelectedIndex
        End If
    End Sub

    Private Sub TextBox117_TextChanged(sender As Object, e As EventArgs) Handles TextBox117.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox117, ComboBox60)
        End If
    End Sub

    Private Sub ComboBox60_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox60.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox60, _OBJECTNUM)
            TextBox117.Text = ComboBox60.SelectedIndex
        End If
    End Sub

    Private Sub TextBox118_TextChanged(sender As Object, e As EventArgs) Handles TextBox118.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox118, ComboBox61)
        End If
    End Sub

    Private Sub ComboBox61_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox61.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox61, _OBJECTNUM)
            TextBox118.Text = ComboBox61.SelectedIndex
        End If
    End Sub

    Private Sub TextBox119_TextChanged(sender As Object, e As EventArgs) Handles TextBox119.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox119, ComboBox62)
        End If
    End Sub

    Private Sub ComboBox62_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox62.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox62, _OBJECTNUM)
            TextBox119.Text = ComboBox62.SelectedIndex
        End If
    End Sub

    Private Sub TextBox120_TextChanged(sender As Object, e As EventArgs) Handles TextBox120.TextChanged
        If loadSTATUS = True Then
            comboandtext(TextBox120, ComboBox64)
            Dim iscriptID As Integer = DatEditDATA(DTYPE.images).ReadValue("Iscript ID", _OBJECTNUM)


            ListBox9.Items.Clear()


            Try
                '        ListBox9.Items.Add(iscript.iscriptEntry(iscript.key(iscriptID)).EntryType)
                For i = 0 To iscript.iscriptEntry(iscript.key(iscriptID)).EntryType - 1
                    ListBox9.Items.Add(Format(iscript.iscriptEntry(iscript.key(iscriptID)).AnimHeader(i), "00000") & " " & HEADERNAME(i))
                    'ListBox9.Items.Add(iscript.iscriptEntry(iscript.key(iscriptID)).headeroffset)
                Next
            Catch ex As KeyNotFoundException

            End Try

            If ListBox9.Items.Count <> 0 Then
                ListBox9.SelectedIndex = 0
            End If
            RichTextBox1.ResetText()
            'PictureBox26
            LoadImageGRP()
            drawImageGRP(0, False)

            IScriptPlayer.Enabled = True
            iscript.curretgrpMaxFrame = ImageGRP.framecount
            iscript.currentScriptID = iscriptID
        End If
    End Sub

    Private Sub ComboBox64_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox64.SelectedIndexChanged
        If loadSTATUS = True Then
            DatEditDATA(TAB_INDEX).WriteToCOMBOBOX(ComboBox64, _OBJECTNUM)
            TextBox120.Text = ComboBox64.SelectedIndex
            Dim iscriptID As Integer = DatEditDATA(DTYPE.images).ReadValue("Iscript ID", _OBJECTNUM)


            ListBox9.Items.Clear()
            Try
                '        ListBox9.Items.Add(iscript.iscriptEntry(iscript.key(iscriptID)).EntryType)
                For i = 0 To iscript.iscriptEntry(iscript.key(iscriptID)).EntryType - 1
                    ListBox9.Items.Add(Format(iscript.iscriptEntry(iscript.key(iscriptID)).AnimHeader(i), "00000") & " " & HEADERNAME(i))
                    'ListBox9.Items.Add(iscript.iscriptEntry(iscript.key(iscriptID)).headeroffset)
                Next
            Catch ex As KeyNotFoundException

            End Try

            If ListBox9.Items.Count <> 0 Then
                ListBox9.SelectedIndex = 0
            End If
            RichTextBox1.ResetText()
            'PictureBox26
            LoadImageGRP()
            drawImageGRP(0, False)

            IScriptPlayer.Enabled = True
            iscript.curretgrpMaxFrame = ImageGRP.framecount
            iscript.currentScriptID = iscriptID
        End If
    End Sub

    Private Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click
        If GRPEditorControl.Visible Then
            GRPEditorControl.Visible = False
        Else
            GRPEditorControlload()
        End If
    End Sub
    Private Sub GRPEditorControlload()
        ListBox10.Items.Clear()

        ListBox10.Items.Add("Default")
        If GRPEditorUsingDATA(_OBJECTNUM) <> "" Then
            ' ListBox10.Items.Add(GRPEditorUsingDATA(_OBJECTNUM))
        End If
        For i = 0 To GRPEditorDATA.Count - 1
            ListBox10.Items.Add(GRPEditorDATA(i).Filename)
        Next
        ListBox10.SelectedItem = GRPEditorUsingDATA(_OBJECTNUM)
        If ListBox10.SelectedIndex = -1 Then
            ListBox10.SelectedIndex = 0
        End If





        GRPEditorControl.Visible = True
    End Sub

    Private Sub ExtractGRP(sender As Object, e As EventArgs) Handles Button44.Click
        Dim dialog As DialogResult
        GRPForm_ListForm.Location = Me.Location
        dialog = GRPForm_ListForm.ShowDialog()
        If (dialog = DialogResult.OK) Or (GRPForm_ListForm.returnvalue >= 0) Then


            Dim GrpD As New GRPDATA
            GrpD.IsExternal = False


            'GRPForm_ListForm.returnvalue
            GrpD.Filename = "unit\" & CODE(DTYPE.grpfile)(GRPForm_ListForm.returnvalue).Replace("<0>", "") '"unit\neutral\civilian.grp"
            GrpD.SafeFilename = "unit\" & CODE(DTYPE.grpfile)(GRPForm_ListForm.returnvalue).Replace("<0>", "") '"unit\neutral\civilian.grp"
            GrpD.Remapping = 0
            GrpD.Palett = 4
            GrpD.usingimage = New List(Of Integer)



            If ListBox10.SelectedIndex <> -1 Then
                For i = 0 To GRPEditorDATA.Count - 1
                    If GRPEditorDATA(i).SafeFilename = GrpD.SafeFilename Then

                        MsgBox(Lan.GetText(Me.Name, "DuplicateFile"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)

                        Exit Sub
                    End If
                Next
            End If



            ListBox10.Items.Add(GrpD.SafeFilename)
            GRPEditorDATA.Add(GrpD)



            ListBox10.SelectedIndex = ListBox10.Items.Count - 1
        End If
    End Sub

    Private Sub LoadGRP(sender As Object, e As EventArgs) Handles Button42.Click
        Dim dialog As DialogResult
        dialog = OpenFileDialog2.ShowDialog


        If dialog = DialogResult.OK Then
            Dim GrpD As New GRPDATA
            GrpD.IsExternal = True
            GrpD.Filename = OpenFileDialog2.FileName
            GrpD.SafeFilename = OpenFileDialog2.SafeFileName
            GrpD.Remapping = 0
            GrpD.Palett = 4
            GrpD.usingimage = New List(Of Integer)


            If ListBox1.SelectedIndex <> -1 Then
                For i = 0 To GRPEditorDATA.Count - 1
                    If GRPEditorDATA(i).SafeFilename = GrpD.SafeFilename Then
                        MsgBox(Lan.GetText(Me.Name, "DuplicateFile"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)


                        Exit Sub
                    End If
                Next
            End If



            ListBox10.Items.Add(GrpD.SafeFilename)
            GRPEditorDATA.Add(GrpD)



            ListBox10.SelectedIndex = ListBox10.Items.Count - 1
        End If
    End Sub

    Private Function GetListNum()
        If ListBox1.SelectedIndex <> -1 Then
            For i = 0 To GRPEditorDATA.Count - 1
                If GRPEditorDATA(i).SafeFilename = ListBox10.SelectedItem Then
                    Return i
                End If
            Next
        End If
        Return 0
    End Function
    Private Sub DeleteGRP(sender As Object, e As EventArgs) Handles Button43.Click
        If ListBox10.SelectedIndex > 0 Then
            Dim index As Integer = ListBox10.SelectedIndex

            Dim num As Integer = GetListNum()

            If index <> -1 Then
                For i = 0 To GRPEditorUsingindexDATA.Count - 1
                    If GRPEditorUsingindexDATA(i) = GRPEditorDATA(num).Filename Then
                        GRPEditorUsingindexDATA(i) = ""
                    End If
                Next


                ListBox10.Items.RemoveAt((index))

                For i = 0 To GRPEditorDATA(num).usingimage.Count - 1
                    GRPEditorUsingDATA(GRPEditorDATA(num).usingimage(i)) = ""
                Next

                GRPEditorDATA.RemoveAt(num)
            End If



            If ListBox10.Items.Count > index Then
                ListBox10.SelectedIndex = index
            Else
                If ListBox10.Items.Count > 0 Then
                    ListBox10.SelectedIndex = ListBox10.Items.Count - 1
                End If
            End If



            GRPEditorControlload()
            ListDraw()
        End If
    End Sub

    Private Sub ListBox10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox10.SelectedIndexChanged
        If loadSTATUS = True Then
            If ListBox10.SelectedIndex <> -1 Then

                '일단 해당파일의 기록을 완전히 지운다.
                If GRPEditorUsingDATA(_OBJECTNUM) <> "" Then '지울게 있을 경우
                    For i = 0 To GRPEditorDATA.Count - 1
                        If GRPEditorDATA(i).Filename = GRPEditorUsingDATA(_OBJECTNUM) Then
                            For k = 0 To GRPEditorDATA(i).usingimage.Count - 1
                                If GRPEditorDATA(i).usingimage(k) = _OBJECTNUM Then
                                    GRPEditorDATA(i).usingimage.RemoveAt(k)
                                End If
                            Next
                        End If
                    Next

                    GRPEditorUsingDATA(_OBJECTNUM) = ""
                End If


                '선택한 파일에 해당 이미지를 추가한다.
                If ListBox10.SelectedIndex <> 0 Then '선택한게 None가 아니라면
                    Dim grpfilename As String = ListBox10.SelectedItem


                    For i = 0 To GRPEditorDATA.Count - 1
                        If GRPEditorDATA(i).Filename = grpfilename Then
                            GRPEditorDATA(i).usingimage.Add(_OBJECTNUM)
                        End If
                    Next

                    GRPEditorUsingDATA(_OBJECTNUM) = grpfilename
                End If

                LoadData()
            End If
        End If
    End Sub


    Private Sub 트리거보기TToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TriggerViewerTToolStripMenuItem.Click
        Visible = False
        TriggerViewerForm.ShowDialog()
        Visible = True
        LoadData()
    End Sub


    Private Sub Stat_textSet(key As String)
        Dim value As UInteger = DatEditDATA(TAB_INDEX).ReadValue(key, _OBJECTNUM)
        If value <> 0 Then
            value -= 1
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
    End Sub

    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        Dim value As UInteger = DatEditDATA(TAB_INDEX).ReadValue(TextBox45.Tag, _OBJECTNUM) + 1301
        If value <> 0 Then
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
    End Sub

    Private Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
        Stat_textSet(TextBox58.Tag)
    End Sub

    Private Sub Button47_Click(sender As Object, e As EventArgs) Handles Button47.Click
        Stat_textSet(TextBox59.Tag)
    End Sub

    Private Sub Button48_Click(sender As Object, e As EventArgs) Handles Button48.Click
        Stat_textSet(TextBox79.Tag)
    End Sub

    Private Sub Button49_Click(sender As Object, e As EventArgs) Handles Button49.Click
        Stat_textSet(TextBox91.Tag)
    End Sub

    Private Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        Stat_textSet(TextBox98.Tag)
    End Sub


    Private Sub CodeViewerShow(listN As DTYPE, Button As Object)
        Try
            CodeViewer.listNum = listN
            CodeViewer.Value = DatEditDATA(TAB_INDEX).ReadValue(Button.Tag, _OBJECTNUM)

            CodeViewer.mode = "Dat"
            CodeViewer.ObjectName = Button.Tag
            CodeViewer.ObjectNum = _OBJECTNUM
            CodeViewer.ObjectTab = TAB_INDEX


            CodeViewer.Show()
            CodeViewer.Location = New Point(MousePosition.X - CodeViewer.Size.Width / 2, MousePosition.Y)
        Catch ex As Exception

        End Try

    End Sub


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        CodeViewerShow(DTYPE.upgrades, PictureBox1)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        CodeViewerShow(DTYPE.weapons, PictureBox2)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        CodeViewerShow(DTYPE.weapons, PictureBox3)
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        CodeViewerShow(DTYPE.units, PictureBox5)
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        CodeViewerShow(DTYPE.units, PictureBox6)
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        CodeViewerShow(DTYPE.units, PictureBox7)
    End Sub

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        CodeViewerShow(DTYPE.orders, PictureBox11)
    End Sub

    Private Sub PictureBox12_Click(sender As Object, e As EventArgs) Handles PictureBox12.Click
        CodeViewerShow(DTYPE.orders, PictureBox12)
    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click
        CodeViewerShow(DTYPE.orders, PictureBox13)
    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click
        CodeViewerShow(DTYPE.orders, PictureBox14)
    End Sub

    Private Sub PictureBox15_Click(sender As Object, e As EventArgs) Handles PictureBox15.Click
        CodeViewerShow(DTYPE.orders, PictureBox15)
    End Sub

    Private Sub PictureBox16_Click(sender As Object, e As EventArgs) Handles PictureBox16.Click
        CodeViewerShow(DTYPE.upgrades, PictureBox16)
    End Sub

    Private Sub PictureBox23_Click(sender As Object, e As EventArgs) Handles PictureBox23.Click
        CodeViewerShow(DTYPE.icon, PictureBox23)
    End Sub

    Private Sub PictureBox24_Click(sender As Object, e As EventArgs) Handles PictureBox24.Click
        CodeViewerShow(DTYPE.icon, PictureBox24)
    End Sub

    Private Sub PictureBox18_Click(sender As Object, e As EventArgs) Handles PictureBox18.Click
        CodeViewerShow(DTYPE.icon, PictureBox18)
    End Sub
End Class