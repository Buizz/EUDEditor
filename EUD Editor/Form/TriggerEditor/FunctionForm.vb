Imports System.IO
Imports System.Runtime.InteropServices

Public Class FunctionForm
    Public _varele As Element


    Public FunEle As Element
    Private FunNum As Integer
    Private BaseFun As Element



    Private LinkLabels As New List(Of Label)

    Public isNew As Boolean = False


    Private currentValueDef As String
    Private currentFullValueDef As String




    Public Const MOD_CTRL As Integer = &H2
    Public Const MOD_Shift As Integer = &H4
    Public Const WM_HOTKEY As Integer = &H312       '

    <DllImport("User32.dll")>
    Public Shared Function RegisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    End Function

    <DllImport("User32.dll")>
    Public Shared Function UnregisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    End Function

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case (id.ToString)
                Case "100"
                    DialogResult = DialogResult.OK
                Case "200"
                    If TabControl3.SelectedIndex < TabControl3.TabCount - 1 Then
                        TabControl3.SelectedIndex = TabControl3.SelectedIndex + 1
                    Else
                        TabControl3.SelectedIndex = 0
                    End If
                Case "300"
                    If BaseFun.GetElementList(0).GetElementsCount <> 0 Then
                        If TabControl1.SelectedIndex < TabControl1.TabCount - 1 Then
                            TabControl1.SelectedIndex = TabControl1.SelectedIndex + 1
                        Else
                            TabControl1.SelectedIndex = 0
                        End If
                    End If
                Case "500"
                    SelectFunctionCbb.Select()
                    SelectFunctionCbb.Focus()
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 100)
        UnregisterHotKey(Me.Handle, 200)
        UnregisterHotKey(Me.Handle, 300)
        UnregisterHotKey(Me.Handle, 500)
    End Sub


    Private Sub Form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        RegisterHotKey(Me.Handle, 100, MOD_Shift, Keys.Enter)
        RegisterHotKey(Me.Handle, 200, 0, Keys.Tab)
        RegisterHotKey(Me.Handle, 300, MOD_Shift, Keys.Tab)
        RegisterHotKey(Me.Handle, 500, MOD_CTRL, Keys.Q)
    End Sub


    Private Sub LinkLabel_Click(sender As Object, e As EventArgs)
        Dim label As Label = CType(sender, LinkLabel)
        'MsgBox(CType(sender, LinkLabel).Tag)
        currentValueDef = label.Tag
        currentFullValueDef = BaseFun.GetElementList(0).GetElementList(LinkLabels.IndexOf(label)).Values(0) & "(" & label.Tag.Split(".")(1) & ")"

        TabControl3.SelectedIndex = LinkLabels.IndexOf(label)


        ValueSetting(True)
    End Sub
    '0벨류는 함수 종류다?
    Private Function CheckExist() As Boolean
        If functions.GetElementsCount <> 0 Then

            For i = 0 To functions.GetElementsCount - 1
                If FunEle.Values(0) = functions.GetElementList(i).Values(0) Then
                    Return True
                End If
            Next

        End If
        Return False
    End Function

    Private Function GetVariablesWithoutoverlap() As List(Of String)
        Dim returnValues As New List(Of String)

        Dim values As New List(Of String)
        If GlobalVar.GetElementsCount <> 0 Then
            values.AddRange(GlobalVar.GetElementList(GlobalVar.GetElementsCount - 1).GetVariables(Nothing))
        End If

        values.AddRange(_varele.GetVariables(Nothing))

        For i = 0 To values.Count - 1
            If returnValues.Contains(values(i)) = False Then
                returnValues.Add(values(i))
            End If
        Next

        Return returnValues
    End Function
    Private Sub FunctionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isload = False
        LoadFunctionList()

        If CheckExist() = False And isNew = False Then
            MsgBox("함수가 존재하지 않습니다.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            SelectFunctionCbb.SelectedIndex = 0
            FunEle.Values(0) = SelectFunctionCbb.SelectedItem


            FunEle.Values.RemoveRange(1, FunEle.Values.Count - 1)

            GetBaseFun()
            For i = 0 To BaseFun.GetElementList(0).GetElementsCount - 1
                FunEle.Values.Add(BaseFun.GetElementList(0).GetElementList(i).Values(0) & "(" & Getdef(i) & ")")
            Next
        End If


        EasyCompletionComboBox2.Items.Clear()
        EasyCompletionComboBox2.Items.AddRange(GetVariablesWithoutoverlap.ToArray)



        If isNew = True Then
            SelectFunctionCbb.SelectedIndex = 0
            FunEle.Values(0) = SelectFunctionCbb.SelectedItem


            FunEle.Values.RemoveRange(1, FunEle.Values.Count - 1)

            GetBaseFun()
            For i = 0 To BaseFun.GetElementList(0).GetElementsCount - 1
                FunEle.Values.Add(BaseFun.GetElementList(0).GetElementList(i).Values(0) & "(" & Getdef(i) & ")")
            Next
        Else
            GetBaseFun()
            CheckChange()
        End If



        MakeLable(True)



        If LinkLabels.Count <> 0 Then
            currentValueDef = LinkLabels(TabControl3.SelectedIndex).Tag
            currentFullValueDef = BaseFun.GetElementList(0).GetElementList(TabControl3.SelectedIndex).Values(0) & "(" & currentValueDef.Split(".")(1) & ")"
            ValueSetting(True)
        End If


        isload = True


        SelectFunctionCbb.Select()
        SelectFunctionCbb.Focus()
    End Sub
    Private Sub CheckChange()
        Dim BaseCount As Integer = BaseFun.GetElementList(0).GetElementsCount
        Dim CurrentCount As Integer = FunEle.Values.Count


        If BaseCount < CurrentCount Then
            FunEle.Values.RemoveRange(BaseCount + 1, CurrentCount - BaseCount - 1)
        End If




        For i = 0 To BaseFun.GetElementList(0).GetElementsCount - 1
            If CurrentCount <= i + 1 Then '현재 선택한 함수가 더 적을 경우.
                FunEle.Values.Add(BaseFun.GetElementList(0).GetElementList(i).Values(0) & "(" & Getdef(i) & ")")
            End If


            If FunEle.Values(i + 1) = BaseFun.GetElementList(0).GetElementList(i).Values(0) & "(" & Getdef(i) & ")" Then
                'Button5.Enabled = False
            End If
        Next
    End Sub


    Private Sub GetBaseFun()
        For i = 0 To functions.GetElementsCount - 1
            If FunEle.Values(0) = functions.GetElementList(i).Values(0) Then
                FunNum = i
                BaseFun = functions.GetElementList(i)
            End If
        Next
    End Sub


    Private Sub LoadFunctionList()
        SelectFunctionCbb.Items.Clear()

        SelectFunctionCbb.SelectedIndex = -1
        For i = 0 To functions.GetElementsCount - 1
            SelectFunctionCbb.Items.Add(functions.GetElementList(i).Values(0))
            If FunEle.Values(0) = functions.GetElementList(i).Values(0) Then
                SelectFunctionCbb.SelectedIndex = i
            End If
        Next
    End Sub
    Private Sub TabControl3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl3.SelectedIndexChanged
        Try
            currentValueDef = LinkLabels(TabControl3.SelectedIndex).Tag
            currentFullValueDef = BaseFun.GetElementList(0).GetElementList(TabControl3.SelectedIndex).Values(0) & "(" & currentValueDef.Split(".")(1) & ")"

            ValueSetting(True)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SelectFunctionCbb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SelectFunctionCbb.SelectedIndexChanged
        If isload = True Then
            FunEle.Values.Clear()

            FunEle.Values.Add(SelectFunctionCbb.SelectedItem)


            GetBaseFun()
            For i = 0 To BaseFun.GetElementList(0).GetElementsCount - 1
                FunEle.Values.Add(BaseFun.GetElementList(0).GetElementList(i).Values(0) & "(" & Getdef(i) & ")")
            Next

            MakeLable(True)
        End If

    End Sub

    Private Sub SetSize(little As Boolean)
        If little Then
            TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(2).Height = 0


            TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Percent
            Me.Size = New Size(Me.Width, 207)
        Else
            TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Percent
            TableLayoutPanel1.RowStyles(2).Height = 100

            TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Absolute
            Me.Size = New Size(Me.Width, 500)
        End If
    End Sub


    Private Function Getdef(index As Integer) As String

        'MsgBox("제발살려줘") 
        Return ValueDefiniction(BaseFun.GetElementList(0).GetElementList(index).Values(1)).Name(0)
    End Function


    Private Function CheckValue() As Boolean
        GetBaseFun()

        If BaseFun.GetElementList(0).GetElementsCount = 0 Then
            SetSize(True)
            Return True
        Else
            SetSize(False)
        End If
        Return True
    End Function



    Private Sub MakeLable(Optional isfrist As Boolean = True)
        CheckValue()
        GetBaseFun()

        If isfrist = True Then

            TabControl3.TabPages.Clear()

            FlowLayoutPanel1.Controls.Clear()
            LinkLabels.Clear()


            '함수 인자 갯수 조정
            Dim BaseCount As Integer = BaseFun.GetElementList(0).GetElementsCount




            For i = 0 To BaseCount - 1
                LinkLabels.Add(New LinkLabel)
                LinkLabels.Last.AutoSize = True
                LinkLabels.Last.Margin = New Padding(0, 0, 0, 0)
                LinkLabels.Last.Name = "LL" & LinkLabels.Count

                LinkLabels.Last.Text = BaseFun.GetElementList(0).GetElementList(i).Values(0) & "(" & Getdef(i) & ")"

                Dim overlapcount As Integer = 1
                '중복 인수 체크
                For k = 0 To LinkLabels.Count - 2
                    If LinkLabels(k).Tag.Contains(Getdef(i)) Then
                        overlapcount += 1
                    End If
                Next

                LinkLabels.Last.Tag = overlapcount & "." & Getdef(i)

                '페이지 추가
                TabControl3.TabPages.Add(LinkLabels.Last.Tag)

                AddHandler LinkLabels.Last.Click, AddressOf LinkLabel_Click

                FlowLayoutPanel1.Controls.Add(LinkLabels.Last)
            Next


            If LinkLabels.Count <> 0 Then
                currentValueDef = LinkLabels(0).Tag
                ValueSetting(True)
            End If
        End If

        For i = 0 To LinkLabels.Count - 1
            Dim count As Integer = 0
            For j = 0 To BaseFun.GetElementList(0).GetElementsCount - 1
                Dim valuedef As String = LinkLabels(i).Tag.Split(".")(1)
                Dim valuecount As Integer = LinkLabels(i).Tag.Split(".")(0)


                If valuedef = ValueDefiniction(BaseFun.GetElementList(0).GetElementList(j).Values(1)).Name(0) Then
                    count += 1
                    If count = valuecount Then
                        If BaseFun.GetElementList(0).GetElementList(j).Values(1) = "Properties" Then
                            LinkLabels(i).Text = "Properties"
                        Else
                            LinkLabels(i).Text = FunEle.ValueParser(FunEle.Values(i + 1), ValueDefiniction(BaseFun.GetElementList(0).GetElementList(j).Values(1)).Name(0))
                        End If
                    End If
                End If
            Next
        Next
    End Sub

    'Private Sub MakeLable(Optional isfrist As Boolean = True)
    '    CheckValue()
    '    GetBaseFun()
    '    If isfrist = True Then
    '        TabControl3.TabPages.Clear()

    '        FlowLayoutPanel1.Controls.Clear()
    '        LinkLabels.Clear()




    '        For i = 0 To BaseFun.GetElementList(0).GetElementsCount - 1
    '            LinkLabels.Add(New LinkLabel)
    '            LinkLabels.Last.AutoSize = True
    '            LinkLabels.Last.Margin = New Padding(0, 0, 0, 0)
    '            LinkLabels.Last.Name = "LL" & LinkLabels.Count

    '            LinkLabels.Last.Text = BaseFun.GetElementList(0).GetElementList(i).Values(0) & "(" & Getdef(i) & ")"


    '            LinkLabels.Last.Tag = Getdef(i)

    '            '페이지 추가
    '            TabControl3.TabPages.Add(LinkLabels.Last.Tag)

    '            AddHandler LinkLabels.Last.Click, AddressOf LinkLabel_Click

    '            FlowLayoutPanel1.Controls.Add(LinkLabels.Last)
    '        Next




    '        If LinkLabels.Count <> 0 Then
    '            currentValueDef = LinkLabels(0).Tag
    '            currentFullValueDef = BaseFun.GetElementList(0).GetElementList(0).Values(0) & "(" & currentValueDef & ")"

    '            ValueSetting(True)
    '        End If

    '    End If




    '    For i = 0 To LinkLabels.Count - 1
    '        For j = 0 To BaseFun.GetElementList(0).GetElementsCount - 1
    '            If LinkLabels(i).Tag = ValueDefiniction(BaseFun.GetElementList(0).GetElementList(j).Values(1)).Name(0) Then
    '                If BaseFun.GetElementList(0).GetElementList(j).Values(1) = "Properties" Then
    '                    LinkLabels(i).Text = "Properties"
    '                Else
    '                    LinkLabels(i).Text = FunEle.ValueParser(FunEle.Values(i + 1), ValueDefiniction(BaseFun.GetElementList(0).GetElementList(j).Values(1)).Name(0))
    '                End If
    '            End If
    '        Next
    '    Next
    'End Sub

    Dim isloading As Boolean = False
    Dim isload As Boolean = False
    Private Sub ValueSetting(isLoading As Boolean)
        Me.isloading = True
        Dim value As String = GetValue()


        '올바른 데이터인지
        Dim isDataCollect As Boolean = False
        Dim defvlaue2 As Integer = 0
        '처음인지
        Dim isDefault As Boolean = False
        If value = currentFullValueDef Then
            isDefault = True
        End If

        TextBox2.Text = value

        Dim _valuedef As ValueDefs = GetDefValueDefs(currentValueDef)
        Select Case currentValueDef.Split(".")(1)
            Case "Object"
                If GetValue("DatFile") <> "None" Then
                    Try
                        Dim num As Integer = CInt(GetValue("DatFile"))
                        Dim strings() As String = {"Unit", "Weapon", "Flingy", "Sprite", "Image", "Upgrade", "Techdata", "Order"}
                        _valuedef = GetDefValueDefs(strings(num))
                    Catch ex As Exception

                    End Try
                End If
            Case "OffsetName"
                If GetValue("DatFile") <> "None" Then
                    Try
                        Dim num As Integer = CInt(GetValue("DatFile"))
                        defvlaue2 = num
                        'For i = 0 To DatEditDATA(num).data.Count - 1
                        '    DatEditDATA(num).keyDic.Keys.ToArray(i)
                        'Next


                        'Dim strings() As String = {"Unit", "Weapon", "Flingy", "Sprite", "Image", "Upgrade", "Techdata", "Order"}
                        '_valuedef = GetDefValueDefs(strings(num))
                    Catch ex As Exception

                    End Try
                End If
            Case "DValue"
                If GetValue("OffsetName") <> "None" Then
                    Try
                        Dim num As Integer = CInt(GetValue("DatFile"))
                        Dim num2 As Integer = CInt(GetValue("OffsetName"))

                        Dim _defstring As String = GetDefValueDefs("OffsetName").GetValues(False, num)(num2)
                        _defstring = DatEditDATA(num).typeName & "_" & _defstring


                        _valuedef = GetDefValueDefs(ReadValDef(_defstring))

                    Catch ex As Exception

                    End Try
                End If
            Case "SValue"
                If GetValue("StructOffset") <> "None" Then
                    Try
                        Dim num As Integer = CInt(GetValue("StructOffset"))
                        Dim _defstring As String = CUnitData(num)(1)




                        _valuedef = GetDefValueDefs(_defstring)
                    Catch ex As Exception

                    End Try
                End If
        End Select



        Select Case _valuedef.type
            Case ValueDefs.OutPutType.BtnData

                TableLayoutPanel8.Visible = True
                TableLayoutPanel8.Dock = DockStyle.Fill

                CheckedListBox1.Visible = False
                TableLayoutPanel4.Visible = False
                NumericUpDown1.Visible = False
                ListBox1.Visible = False
                TableLayoutPanel6.Visible = False
                EasyCompletionComboBox1.Visible = False
                TableLayoutPanel7.Visible = False

                For i = 0 To 59
                    Try
                        Dim _bytes As Byte = Val(value(i))
                    Catch ex As Exception
                        Exit Select
                    End Try
                Next
                isDataCollect = True
                TextBox3.Text = value


            Case ValueDefs.OutPutType.UnitBtn

                TableLayoutPanel7.Visible = True
                TableLayoutPanel7.Dock = DockStyle.Fill

                CheckedListBox1.Visible = False
                TableLayoutPanel4.Visible = False
                NumericUpDown1.Visible = False
                ListBox1.Visible = False
                TableLayoutPanel6.Visible = False
                EasyCompletionComboBox1.Visible = False
                TableLayoutPanel8.Visible = False

                UnitBtnSelecter.Items.Clear()
                UnitBtnSelecter.Items.AddRange(_valuedef.GetValues)

                Try
                    Dim v1 As Long = CLng(value.Split(":")(0))
                    Dim v2 As Long = CLng(value.Split(":")(1))
                    UnitBtnSelecter.SelectedIndex = v1



                    Dim _OBJECTNUM As Integer = UnitBtnSelecter.SelectedIndex

                    Dim Icon() As String = CODE(12).ToArray

                    BtnSelector.Items.Clear()
                    If ProjectBtnUSE(_OBJECTNUM) = True Then
                        For i = 0 To ProjectBtnData(_OBJECTNUM).Count - 1
                            BtnSelector.Items.Add(Icon(ProjectBtnData(_OBJECTNUM)(i).icon))
                        Next
                        If BtnSelector.Items.Count <> 0 Then
                            BtnSelector.SelectedIndex = 0
                        End If
                    Else
                        For i = 0 To BtnData(_OBJECTNUM).Count - 1
                            BtnSelector.Items.Add(Icon(BtnData(_OBJECTNUM)(i).icon))
                        Next
                        If BtnSelector.Items.Count <> 0 Then
                            BtnSelector.SelectedIndex = 0
                        End If
                    End If
                    BtnSelector.SelectedIndex = v2

                    isDataCollect = True
                Catch ex As Exception
                End Try
            Case ValueDefs.OutPutType.CheckList

                CheckedListBox1.Visible = True
                CheckedListBox1.Dock = DockStyle.Fill

                TableLayoutPanel4.Visible = False
                NumericUpDown1.Visible = False
                ListBox1.Visible = False
                TableLayoutPanel6.Visible = False
                EasyCompletionComboBox1.Visible = False
                TableLayoutPanel7.Visible = False
                TableLayoutPanel8.Visible = False

                CheckedListBox1.Items.Clear()
                CheckedListBox1.Items.AddRange(_valuedef.GetValues)

                Try
                    For i = 0 To CheckedListBox1.Items.Count - 1
                        CheckedListBox1.SetItemChecked(i, value And Math.Pow(2, i))
                    Next
                    isDataCollect = True

                Catch ex As Exception

                End Try

            Case ValueDefs.OutPutType.ComboboxString
                EasyCompletionComboBox1.Visible = True
                EasyCompletionComboBox1.Dock = DockStyle.Fill

                TableLayoutPanel4.Visible = False
                NumericUpDown1.Visible = False
                ListBox1.Visible = False
                TableLayoutPanel6.Visible = False
                CheckedListBox1.Visible = False
                TableLayoutPanel7.Visible = False
                TableLayoutPanel8.Visible = False

                EasyCompletionComboBox1.Items.Clear()
                EasyCompletionComboBox1.Items.AddRange(_valuedef.GetValues)

                If EasyCompletionComboBox1.Items.Contains(value.Replace("""", "")) Then
                    EasyCompletionComboBox1.SelectedIndex = EasyCompletionComboBox1.Items.IndexOf(value.Replace("""", ""))
                    isDataCollect = True
                End If
            Case ValueDefs.OutPutType.Combobox
                EasyCompletionComboBox1.Visible = True
                EasyCompletionComboBox1.Dock = DockStyle.Fill
                TableLayoutPanel4.Visible = False
                NumericUpDown1.Visible = False
                ListBox1.Visible = False
                TableLayoutPanel6.Visible = False
                CheckedListBox1.Visible = False
                TableLayoutPanel7.Visible = False
                TableLayoutPanel8.Visible = False

                EasyCompletionComboBox1.Items.Clear()
                EasyCompletionComboBox1.Items.AddRange(_valuedef.GetValues(False, defvlaue2))

                Try
                    Dim temp As Long = CLng(value)
                    EasyCompletionComboBox1.SelectedIndex = value
                    isDataCollect = True
                Catch ex As Exception
                End Try
            Case ValueDefs.OutPutType.ListNum
                ListBox1.Visible = True
                ListBox1.Dock = DockStyle.Fill
                TableLayoutPanel4.Visible = False
                NumericUpDown1.Visible = False
                TableLayoutPanel6.Visible = False
                EasyCompletionComboBox1.Visible = False
                CheckedListBox1.Visible = False
                TableLayoutPanel7.Visible = False
                TableLayoutPanel8.Visible = False

                ListBox1.Items.Clear()
                ListBox1.Items.AddRange(_valuedef.GetValues)

                Try
                    Dim temp As Long = CLng(value)
                    ListBox1.SelectedIndex = value
                    isDataCollect = True
                Catch ex As Exception
                End Try

            Case ValueDefs.OutPutType.List
                ListBox1.Visible = True
                ListBox1.Dock = DockStyle.Fill
                TableLayoutPanel4.Visible = False
                NumericUpDown1.Visible = False
                TableLayoutPanel6.Visible = False
                EasyCompletionComboBox1.Visible = False
                CheckedListBox1.Visible = False
                TableLayoutPanel7.Visible = False
                TableLayoutPanel8.Visible = False

                ListBox1.Items.Clear()
                ListBox1.Items.AddRange(_valuedef.GetValues)

                If ListBox1.Items.Contains(value) Then
                    ListBox1.SelectedIndex = ListBox1.Items.IndexOf(value)
                    isDataCollect = True
                End If
            Case ValueDefs.OutPutType.Number
                NumericUpDown1.Visible = True
                NumericUpDown1.Dock = DockStyle.Fill
                TableLayoutPanel4.Visible = False
                ListBox1.Visible = False
                TableLayoutPanel6.Visible = False
                EasyCompletionComboBox1.Visible = False
                CheckedListBox1.Visible = False
                TableLayoutPanel7.Visible = False
                TableLayoutPanel8.Visible = False


                Try
                    NumericUpDown1.Value = CLng(value)
                    isDataCollect = True
                Catch ex As Exception

                End Try
            Case ValueDefs.OutPutType.Text
                TableLayoutPanel6.Visible = True
                TableLayoutPanel6.Dock = DockStyle.Fill
                TableLayoutPanel4.Visible = False
                NumericUpDown1.Visible = False
                ListBox1.Visible = False
                EasyCompletionComboBox1.Visible = False
                CheckedListBox1.Visible = False
                TableLayoutPanel7.Visible = False
                TableLayoutPanel8.Visible = False

                If value.Last = """" And value.First = """" Then
                    TextBox1.Text = Mid(value, 2, value.Length - 2)
                    isDataCollect = True
                End If
            Case ValueDefs.OutPutType.CText
                TableLayoutPanel6.Visible = True
                TableLayoutPanel6.Dock = DockStyle.Fill
                TableLayoutPanel4.Visible = False
                NumericUpDown1.Visible = False
                ListBox1.Visible = False
                EasyCompletionComboBox1.Visible = False
                CheckedListBox1.Visible = False
                TableLayoutPanel7.Visible = False
                TableLayoutPanel8.Visible = False

                If value.Last = """" And value.First = """" Then
                    TextBox1.Text = Mid(value, 2, value.Length - 2)
                    isDataCollect = True
                End If
            Case ValueDefs.OutPutType.UnitProperty
                TableLayoutPanel4.Visible = True
                TableLayoutPanel4.Dock = DockStyle.Fill
                ListBox1.Visible = False
                TableLayoutPanel6.Visible = False
                EasyCompletionComboBox1.Visible = False
                TableLayoutPanel7.Visible = False
                TableLayoutPanel8.Visible = False

                isDataCollect = True
                If SearchProperties(value, "hitpoint") <> "Error" Then
                    NumericUpDown2.Value = SearchProperties(value, "hitpoint")
                Else
                    isDataCollect = False
                End If
                If SearchProperties(value, "shield") <> "Error" Then
                    NumericUpDown3.Value = SearchProperties(value, "shield")
                Else
                    isDataCollect = False
                End If
                If SearchProperties(value, "energy") <> "Error" Then
                    NumericUpDown4.Value = SearchProperties(value, "energy")
                Else
                    isDataCollect = False
                End If
                If SearchProperties(value, "resource") <> "Error" Then
                    NumericUpDown5.Value = SearchProperties(value, "resource")
                Else
                    isDataCollect = False
                End If
                If SearchProperties(value, "hanger") <> "Error" Then
                    NumericUpDown6.Value = SearchProperties(value, "hanger")
                Else
                    isDataCollect = False
                End If
                If SearchProperties(value, "cloaked") <> "Error" Then
                    CheckedListBox2.SetItemChecked(0, SearchProperties(value, "cloaked"))
                Else
                    isDataCollect = False
                End If
                If SearchProperties(value, "burrowed") <> "Error" Then
                    CheckedListBox2.SetItemChecked(1, SearchProperties(value, "burrowed"))
                Else
                    isDataCollect = False
                End If
                If SearchProperties(value, "intransit") <> "Error" Then
                    CheckedListBox2.SetItemChecked(2, SearchProperties(value, "intransit"))
                Else
                    isDataCollect = False
                End If
                If SearchProperties(value, "hallucinated") <> "Error" Then
                    CheckedListBox2.SetItemChecked(3, SearchProperties(value, "hallucinated"))
                Else
                    isDataCollect = False
                End If
                If SearchProperties(value, "invincible") <> "Error" Then
                    CheckedListBox2.SetItemChecked(4, SearchProperties(value, "invincible"))
                Else
                    isDataCollect = False
                End If
            Case ValueDefs.OutPutType.Variable
                TableLayoutPanel4.Visible = False
                CheckedListBox1.Visible = False
                NumericUpDown1.Visible = False
                ListBox1.Visible = False
                TableLayoutPanel6.Visible = False
                EasyCompletionComboBox1.Visible = False
                TableLayoutPanel7.Visible = False
                TableLayoutPanel8.Visible = False

                isDataCollect = False
                SetVariable(isLoading)
                Me.isloading = False
                EasyCompletionComboBox2.Select()
                EasyCompletionComboBox2.Focus()
                Exit Sub
            Case ValueDefs.OutPutType.RawString
                TableLayoutPanel4.Visible = False
                CheckedListBox1.Visible = False
                NumericUpDown1.Visible = False
                ListBox1.Visible = False
                TableLayoutPanel6.Visible = False
                EasyCompletionComboBox1.Visible = False
                TableLayoutPanel7.Visible = False
                TableLayoutPanel8.Visible = False

                TabControl1.SelectedIndex = 2
                isDataCollect = False
                Me.isloading = False
                TextBox2.Select()
                TextBox2.Focus()
                Exit Sub
        End Select


        '추천에 값을 표기 할 수 있을 경우.
        If isDataCollect Then
            '추천에 표기가능 하다면
            If isLoading Then
                TabControl1.SelectedIndex = 0
            End If
            '추천에 맞는 값을 선택
        Else
            '추천에 표기가 불가능 하다면
            '디폴트 값일 경우
            If isDefault = True Then
                '디폴트 일 경우 강제로 선택
                EasyCompletionComboBox1.SelectedIndex = -1
                EasyCompletionComboBox1.Text = ""
                ListBox1.SelectedIndex = -1
                TextBox1.Text = ""
                NumericUpDown1.Value = 0
                CheckedListBox1.ClearSelected()

                CheckedListBox2.ClearSelected()
                NumericUpDown2.Value = 100
                NumericUpDown3.Value = 100
                NumericUpDown4.Value = 50
                NumericUpDown5.Value = 0
                NumericUpDown6.Value = 0

                '처음 로딩(탭 선택이 아닌)된 거라면 강제로 탭을 0으로 고정
                If isLoading Then
                    TabControl1.SelectedIndex = 0
                End If
            Else
                '디폴트가 아니고 표기도 불가능 하면
                If isLoading Then
                    '로딩중이면 자동 표기에 따라 변수로 이동
                    SetVariable(isLoading)
                    '강제로 눌렀으면 기본 값을 출력
                    EasyCompletionComboBox1.SelectedIndex = -1
                    EasyCompletionComboBox1.Text = ""
                    ListBox1.SelectedIndex = -1
                    TextBox1.Text = ""
                    NumericUpDown1.Value = 0
                    CheckedListBox1.ClearSelected()

                    CheckedListBox2.ClearSelected()
                    NumericUpDown2.Value = 100
                    NumericUpDown3.Value = 100
                    NumericUpDown4.Value = 50
                    NumericUpDown5.Value = 0
                    NumericUpDown6.Value = 0
                End If
            End If
        End If

        '포커스 맞추기
        Select Case _valuedef.type
            Case ValueDefs.OutPutType.BtnData
                TextBox3.Select()
                TextBox3.Focus()
            Case ValueDefs.OutPutType.UnitBtn
                UnitBtnSelecter.Select()
                UnitBtnSelecter.Focus()
            Case ValueDefs.OutPutType.CheckList
                CheckedListBox1.Select()
                CheckedListBox1.Focus()
            Case ValueDefs.OutPutType.ComboboxString
                EasyCompletionComboBox1.Select()
                EasyCompletionComboBox1.Focus()
            Case ValueDefs.OutPutType.Combobox
                EasyCompletionComboBox1.Select()
                EasyCompletionComboBox1.Focus()
            Case ValueDefs.OutPutType.ListNum
                ListBox1.Select()
                ListBox1.Focus()
            Case ValueDefs.OutPutType.List
                ListBox1.Select()
                ListBox1.Focus()
            Case ValueDefs.OutPutType.Number
                NumericUpDown1.Select()
                NumericUpDown1.Focus()
            Case ValueDefs.OutPutType.Text
                TextBox1.Select()
                TextBox1.Focus()
            Case ValueDefs.OutPutType.CText
                TextBox1.Select()
                TextBox1.Focus()
        End Select



        Me.isloading = False
    End Sub

    Private Function SearchProperties(mainstr As String, Keyword As String) As String
        Try
            mainstr = mainstr

            Dim startpos As Integer = InStr(mainstr, Keyword) + Keyword.Length
            mainstr = Mid(mainstr, startpos)

            If InStr(mainstr, ",") <> 0 Then
                mainstr = Mid(mainstr, 1, InStr(mainstr, ",") - 1)
            Else
                mainstr = Mid(mainstr, 1, InStr(mainstr, ")") - 1)
            End If

            mainstr = mainstr.Replace("=", "").Trim

            Try
                Dim _temp As Boolean = CBool(mainstr)
            Catch ex As Exception
                Try
                    Dim _temp As Integer = CInt(mainstr)
                Catch a As Exception
                    Return "Error"
                End Try
            End Try



            Return mainstr
        Catch ex As Exception
            Return "Error"
        End Try
    End Function


    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged, NumericUpDown1.TextChanged
        If isloading = False Then
            SetValue(NumericUpDown1.Value)
            MakeLable(False)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If isloading = False Then
            SetValue("""" & TextBox1.Text & """")
            MakeLable(False)
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If isloading = False Then
            SetValue(TextBox2.Text)
            MakeLable(False)
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If isloading = False And isload Then
            ValueSetting(False)
            Select Case TabControl1.SelectedIndex
                Case 0
                Case 1
                    SetVariable(False)
                    EasyCompletionComboBox2.Select()
                    EasyCompletionComboBox2.Focus()
                Case 2
                    TextBox2.Select()
                    TextBox2.Focus()
            End Select

        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If isloading = False And isload Then
            Dim _valuedef As ValueDefs = GetDefValueDefs(currentValueDef)
            If currentValueDef = "DValue" Then

                If GetValue("OffsetName") <> "None" Then
                    Try
                        Dim num As Integer = CInt(GetValue("DatFile"))
                        Dim num2 As Integer = CInt(GetValue("OffsetName"))

                        Dim _defstring As String = GetDefValueDefs("OffsetName").GetValues(False, num)(num2)
                        _defstring = DatEditDATA(num).typeName & "_" & _defstring


                        _valuedef = GetDefValueDefs(ReadValDef(_defstring))

                    Catch ex As Exception

                    End Try
                End If
            End If

            If currentValueDef = "SValue" Then

                If GetValue("StructOffset") <> "None" Then
                    Try
                        Dim num As Integer = CInt(GetValue("StructOffset"))
                        Dim _defstring As String = CUnitData(num)(1)




                        _valuedef = GetDefValueDefs(_defstring)

                    Catch ex As Exception

                    End Try
                End If
            End If


            If _valuedef.type = ValueDefs.OutPutType.ListNum Then
                SetValue(ListBox1.SelectedIndex)
            Else
                SetValue(ListBox1.SelectedItem)
            End If
            MakeLable(False)
        End If
    End Sub

    Private Sub EasyCompletionComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles EasyCompletionComboBox1.SelectedIndexChanged
        If isloading = False And isload Then
            If currentValueDef = "Script" Then
                SetValue("""" & EasyCompletionComboBox1.SelectedItem & """")
            Else
                If currentValueDef = "DestLocation" Or currentValueDef = "StartLocation" Or currentValueDef = "Location" Or currentValueDef = "Where" Then
                    SetValue(EasyCompletionComboBox1.SelectedIndex)
                Else
                    SetValue(EasyCompletionComboBox1.SelectedIndex)
                End If


            End If

            MakeLable(False)
        End If
    End Sub

    Private Function Getproperty() As String
        Dim str As String = "UnitProperty(hitpoint = VHP, shield = VSH, energy = VEG, resource = VRE, hanger = VHAN, cloaked = VCL, burrowed = VBU, intransit = VINT, hallucinated = VHAL, invincible = VINV)"

        str = str.Replace("VHP", NumericUpDown2.Value)
        str = str.Replace("VSH", NumericUpDown3.Value)
        str = str.Replace("VEG", NumericUpDown4.Value)
        str = str.Replace("VRE", NumericUpDown5.Value)
        str = str.Replace("VHAN", NumericUpDown6.Value)
        str = str.Replace("VCL", CheckedListBox2.GetItemChecked(0))
        str = str.Replace("VBU", CheckedListBox2.GetItemChecked(1))
        str = str.Replace("VINT", CheckedListBox2.GetItemChecked(2))
        str = str.Replace("VHAL", CheckedListBox2.GetItemChecked(3))
        str = str.Replace("VINV", CheckedListBox2.GetItemChecked(4))

        Return str
    End Function



    Private Sub CheckedListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox2.SelectedIndexChanged
        If isloading = False And isload Then
            SetValue(Getproperty)
            MakeLable(False)
        End If
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        If isloading = False And isload Then
            SetValue(Getproperty)
            MakeLable(False)
        End If
    End Sub

    Private Sub NumericUpDown5_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown5.ValueChanged
        If isloading = False And isload Then
            SetValue(Getproperty)
            MakeLable(False)
        End If
    End Sub

    Private Sub NumericUpDown6_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown6.ValueChanged
        If isloading = False And isload Then
            SetValue(Getproperty)
            MakeLable(False)
        End If
    End Sub

    Private Sub NumericUpDown3_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown3.ValueChanged
        If isloading = False And isload Then
            SetValue(Getproperty)
            MakeLable(False)
        End If
    End Sub

    Private Sub NumericUpDown4_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown4.ValueChanged
        If isloading = False And isload Then
            SetValue(Getproperty)
            MakeLable(False)
        End If
    End Sub

    Private Sub EasyCompletionComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles EasyCompletionComboBox2.SelectedIndexChanged
        If isloading = False And isload Then
            SetValue(EasyCompletionComboBox2.SelectedItem)
            MakeLable(False)
        End If
    End Sub



    Private Sub SetVariable(isFirst As Boolean)
        If isFirst Then
            TabControl1.SelectedIndex = 1
        End If
        EasyCompletionComboBox2.ResetText()
        Dim value As String = GetValue()
        If EasyCompletionComboBox2.Items.Contains(value) Then
            EasyCompletionComboBox2.SelectedIndex = EasyCompletionComboBox2.Items.IndexOf(value)
        Else
            If isFirst Then
                TabControl1.SelectedIndex = 2
            End If
        End If
    End Sub

    Private Function GetValueNum(valuedef As String)
        Dim valuecount As Integer = valuedef.Split(".")(0)
        Dim valuestring As String = valuedef.Split(".")(1)

        Dim count As Integer = 0
        For i = 0 To BaseFun.GetElementList(0).GetElementsCount - 1
            If valuestring = Getdef(i) Then
                count += 1
                If count = valuecount Then
                    Return i + 1
                End If

            End If
        Next
        Return 1
    End Function
    Private Function GetValue(def As String) As String
        Dim returnstring As String

        Try
            returnstring = FunEle.Values(GetValueNum(currentValueDef))
        Catch ex As Exception
            returnstring = "None"
        End Try
        Return returnstring
    End Function

    Private Function GetValue() As String
        Return FunEle.Values(GetValueNum(currentValueDef))
    End Function
    Private Sub SetValue(_value As String)
        FunEle.Values(GetValueNum(currentValueDef)) = _value
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim _valuedef As ValueDefs = GetDefValueDefs(currentValueDef)
        If _valuedef.type = ValueDefs.OutPutType.CText Then
            CTextEditor.RawText = TextBox1.Text
            CTextEditor._varele = _varele
            If CTextEditor.ShowDialog() = DialogResult.OK Then
                TextBox1.Text = CTextEditor.realText
            End If
        Else
            TextEditor.RawText = TextBox1.Text
            If TextEditor.ShowDialog() = DialogResult.OK Then
                TextBox1.Text = TextEditor.realText
            End If
        End If
    End Sub


    Private Sub UnitBtnSelecter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UnitBtnSelecter.SelectedIndexChanged
        If isloading = False And isload Then
            Dim _OBJECTNUM As Integer = UnitBtnSelecter.SelectedIndex

            Dim Icon() As String = CODE(12).ToArray

            BtnSelector.Items.Clear()
            If ProjectBtnUSE(_OBJECTNUM) = True Then
                For i = 0 To ProjectBtnData(_OBJECTNUM).Count - 1
                    BtnSelector.Items.Add(Icon(ProjectBtnData(_OBJECTNUM)(i).icon))
                Next
                If BtnSelector.Items.Count <> 0 Then
                    BtnSelector.SelectedIndex = 0
                End If
            Else
                For i = 0 To BtnData(_OBJECTNUM).Count - 1
                    BtnSelector.Items.Add(Icon(BtnData(_OBJECTNUM)(i).icon))
                Next
                If BtnSelector.Items.Count <> 0 Then
                    BtnSelector.SelectedIndex = 0
                End If
            End If
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
    Private Sub BtnSelector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles BtnSelector.SelectedIndexChanged
        If isloading = False And isload Then
            If BtnSelector.SelectedIndex <> -1 And UnitBtnSelecter.SelectedIndex <> -1 Then
                Dim _OBJECTNUM As Integer = UnitBtnSelecter.SelectedIndex
                Dim btnnum As Integer = BtnSelector.SelectedIndex

                SetValue(UnitBtnSelecter.SelectedIndex & ":" & BtnSelector.SelectedIndex)


                If ProjectBtnUSE(_OBJECTNUM) = True Then
                    With ProjectBtnData(_OBJECTNUM)(btnnum)
                        FunEle.SetValueTodef("1.BtnData", ValueTostring(.pos) & ValueTostring(.icon) _
                         & ValueTostring(.con) & ValueTostring(.act) _
                         & ValueTostring(.conval) & ValueTostring(.actval) _
                         & ValueTostring(.enaStr) & ValueTostring(.disStr))
                    End With
                Else
                    With BtnData(_OBJECTNUM)(btnnum)
                        FunEle.SetValueTodef("1.BtnData", ValueTostring(.pos) & ValueTostring(.icon) _
                         & ValueTostring(.con) & ValueTostring(.act) _
                         & ValueTostring(.conval) & ValueTostring(.actval) _
                         & ValueTostring(.enaStr) & ValueTostring(.disStr))
                    End With
                End If
                MakeLable(False)
            End If
        End If
    End Sub
    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If isloading = False And isload Then
            SetValue(TextBox3.Text)
            MakeLable(False)
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        BtnSettingForm.RawBtnCode = TextBox3.Text
        If BtnSettingForm.ShowDialog() = DialogResult.OK Then
            TextBox3.Text = BtnSettingForm.RawBtnCode
        End If
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        If isloading = False And isload Then
            Dim value As Long = 0
            For i = 0 To CheckedListBox1.Items.Count - 1
                If CheckedListBox1.GetItemChecked(i) Then
                    value += Math.Pow(2, i)
                End If

            Next
            SetValue(value)
            MakeLable(False)
        End If
    End Sub
End Class