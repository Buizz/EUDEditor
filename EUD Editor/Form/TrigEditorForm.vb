Imports System.IO

Public Class TrigEditorForm
    Private Sub TrigEditorForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)
        Lan.SetMenu(Me, ContextMenuStrip1)
        Lan.SetMenu(Me, MenuStrip1)
        'LoadTriggerFile()
        CheckBox1.Checked = ProjectSet.SCDBUse
        Button8.Enabled = ProjectSet.SCDBUse

        UndoRedoBtnRefresh()
        ReDrawTriggerList()
        ReDrawList()
        RedrawCode()
    End Sub


    Private Sub UndoRedoBtnRefresh()
        Button18.Enabled = TaskManager.Isundoable
        Button19.Enabled = TaskManager.Isredoable
        실행취소ToolStripMenuItem.Enabled = Button18.Enabled
        다시실행ToolStripMenuItem.Enabled = Button19.Enabled
    End Sub

    Private Sub WorkSpace_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles WorkSpace.NodeMouseClick
        Dim xpos As Integer = e.X - e.Node.FullPath.Split("ஐ").Count * 19
        If (3 <= xpos) And (xpos <= 18) Then
            Dim _selectElement As Element = CType(e.Node.Tag, Element)

            Select Case _selectElement.GetTypeV
                Case ElementType.액션, ElementType.함수, ElementType.Wait, ElementType.RawTrigger, ElementType.Foluder, ElementType.조건문if, ElementType.조건문ifelse, ElementType.포, ElementType.와일
                    _selectElement.isdisalbe = Not _selectElement.isdisalbe
                    If _selectElement.isdisalbe Then
                        e.Node.BackColor = Color.Gray
                        e.Node.ImageIndex = 3
                        e.Node.SelectedImageIndex = 3
                    Else
                        e.Node.BackColor = Nothing
                        e.Node.ImageIndex = 1
                        e.Node.SelectedImageIndex = 1
                    End If

                    RedrawCode()
                Case ElementType.조건
                    If _selectElement.isdisalbe Then
                        _selectElement.isdisalbe = False
                        _selectElement.isNotcon = False
                    Else
                        If _selectElement.isNotcon Then
                            _selectElement.isdisalbe = True
                            _selectElement.isNotcon = False
                        Else
                            _selectElement.isNotcon = True
                        End If
                    End If




                    If _selectElement.isdisalbe Then
                        e.Node.BackColor = Color.Gray
                        e.Node.ImageIndex = 3
                        e.Node.SelectedImageIndex = 3
                    Else
                        If _selectElement.isNotcon Then
                            e.Node.BackColor = Nothing
                            e.Node.ImageIndex = 4
                            e.Node.SelectedImageIndex = 4
                        Else
                            e.Node.BackColor = Nothing
                            e.Node.ImageIndex = 1
                            e.Node.SelectedImageIndex = 1
                        End If
                    End If
                    RedrawCode()
            End Select
        End If
        If e.Node Is Nothing Then
            ButtonRefresh()
        End If

    End Sub

    Private Sub WorkSpace_AfterExpand(sender As Object, e As TreeViewEventArgs) Handles WorkSpace.AfterExpand
        Dim _selectElement As Element = CType(e.Node.Tag, Element)

        If _selectElement IsNot Nothing Then
            _selectElement.isFloding = False
        End If

    End Sub

    Private Sub WorkSpace_AfterCollapse(sender As Object, e As TreeViewEventArgs) Handles WorkSpace.AfterCollapse
        Dim _selectElement As Element = CType(e.Node.Tag, Element)
        If _selectElement IsNot Nothing Then
            _selectElement.isFloding = True
        End If
    End Sub

    Private Sub WorkSpace_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles WorkSpace.AfterSelect
        ButtonRefresh()
    End Sub

    'Dim _flag As Boolean = True
    'Private Sub WorkSpace_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles WorkSpace.AfterCheck
    '    If _flag Then
    '        _flag = False
    '        Dim _selectElement As Element = CType(e.Node.Tag, Element)

    '        Select Case _selectElement.GetTypeV
    '            Case ElementType.조건, ElementType.액션, ElementType.함수, ElementType.Wait, ElementType.RawTrigger, ElementType.Foluder, ElementType.조건문if, ElementType.조건문ifelse, ElementType.포, ElementType.와일
    '                _selectElement.isdisalbe = Not e.Node.Checked
    '                RedrawCode()
    '            Case Else
    '                e.Node.Checked = True
    '        End Select
    '        _flag = True
    '    End If
    'End Sub

    Private Sub TreeView1_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles WorkSpace.NodeMouseDoubleClick
        Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)

        If e.Button = MouseButtons.Left Then
            If _selectElement.GetTypeV = ElementType.조건 Or
            _selectElement.GetTypeV = ElementType.액션 Or
            _selectElement.GetTypeV = ElementType.포 Or
            _selectElement.GetTypeV = ElementType.Wait Or
            _selectElement.GetTypeV = ElementType.Foluder Or
            _selectElement.GetTypeV = ElementType.Switch Or
            _selectElement.GetTypeV = ElementType.Switchcase Or
            _selectElement.GetTypeV = ElementType.RawTrigger Then
                Edit()
            ElseIf _selectElement.GetTypeV = ElementType.함수 And functions.GetElementsCount <> 0 Then
                Edit()
            ElseIf _selectElement.GetTypeV = ElementType.와일조건 Or _selectElement.GetTypeV = ElementType.조건절 Then
                Edit()
            End If
        End If
    End Sub

    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles WorkSpace.NodeMouseClick

        If e.Button = MouseButtons.Right Then
            WorkSpace.SelectedNode = e.Node
        End If
    End Sub



    'SetHotkey
    Private Sub TreeView1_Hotkey(sender As Object, e As KeyEventArgs) Handles WorkSpace.KeyDown
        Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)

        If e.Control = True Then
            Select Case e.KeyCode
                Case Keys.C
                    If CheckDeleteable(_selectElement) = True Then
                        Copy(WorkSpace.SelectedNode)
                    End If
                Case Keys.X
                    If CheckDeleteable(_selectElement) = True Then
                        Cut(WorkSpace.SelectedNode)
                    End If
                Case Keys.V
                    If CheckNewFile(_selectElement) = True Then
                        If CheckPaste(_selectElement) = True Then
                            Paste(WorkSpace.SelectedNode)
                        End If
                    End If
            End Select
        End If
        If e.KeyCode = Keys.Enter Then
            If _selectElement.GetTypeV = ElementType.조건 Or
            _selectElement.GetTypeV = ElementType.액션 Or
            _selectElement.GetTypeV = ElementType.포 Or
            _selectElement.GetTypeV = ElementType.Wait Or
            _selectElement.GetTypeV = ElementType.Foluder Or
            _selectElement.GetTypeV = ElementType.Switch Or
            _selectElement.GetTypeV = ElementType.Switchcase Or
            _selectElement.GetTypeV = ElementType.RawTrigger Then
                Edit()
            ElseIf _selectElement.GetTypeV = ElementType.함수 And functions.GetElementsCount <> 0 Then
                Edit()
            ElseIf _selectElement.GetTypeV = ElementType.와일조건 Or _selectElement.GetTypeV = ElementType.조건절 Then
                Edit()
            End If
        End If
        If e.KeyCode = Keys.Delete Then
            Delete(WorkSpace.SelectedNode)
        End If
    End Sub

    'Private Sub TreeView1_KeyDown(sender As Object, e As KeyEventArgs) Handles WorkSpace.KeyDown
    '    If e.Control = True And e.Shift = True Then
    '        Select Case e.KeyCode
    '            Case Keys.C
    '                If 조건Btn.Visible = True Then
    '                    Dim _tempele As Element = CType(WorkSpace.SelectedNode.Tag, Element)
    '                    CondicitonFormShow(True, _tempele)
    '                    WorkSpace.Focus()
    '                End If
    '            Case Keys.A
    '                If 액션Btn.Visible = True Then
    '                    Dim _tempele As Element = CType(WorkSpace.SelectedNode.Tag, Element)
    '                    ActionFormShow(True, _tempele)
    '                    WorkSpace.Focus()
    '                End If
    '            Case Keys.W
    '                If 대기하기Btn.Visible = True Then
    '                    AddWait(True)
    '                    WorkSpace.Focus()
    '                End If
    '            Case Keys.F
    '                If 함수Btn.Visible = True Then
    '                    Func()
    '                    WorkSpace.Focus()
    '                End If
    '            Case Keys.I
    '                If IfBtn.Visible = True Then
    '                    NewEle(WorkSpace.SelectedNode, ElementType.조건문if)
    '                    WorkSpace.Focus()
    '                End If
    '            Case Keys.E
    '                If IfElseBtn.Visible = True Then
    '                    NewEle(WorkSpace.SelectedNode, ElementType.조건문ifelse)
    '                    WorkSpace.Focus()
    '                End If
    '            Case Keys.R
    '                If ForBtn.Visible = True Then
    '                    formaker()
    '                    WorkSpace.Focus()
    '                End If
    '            Case Keys.H
    '                If WhileBtn.Visible = True Then
    '                    NewEle(WorkSpace.SelectedNode, ElementType.와일)
    '                    WorkSpace.Focus()
    '                End If
    '            Case Keys.N
    '                If 함수정의Btn.Visible = True Then
    '                    FuncNew()
    '                    WorkSpace.Focus()
    '                End If
    '            Case Keys.T
    '                If 인수Btn.Visible = True Then
    '                    Factor()
    '                    WorkSpace.Focus()
    '                End If
    '            Case Keys.S
    '                If 함수저장Btn.Visible = True Then
    '                    FuncSave()
    '                    WorkSpace.Focus()
    '                End If
    '            Case Keys.L
    '                If 함수불러오기Btn.Visible = True Then
    '                    FuncLoad()
    '                    WorkSpace.Focus()
    '                End If
    '        End Select
    '    End If
    'End Sub








    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        TaskManager.Clear()
        UndoRedoBtnRefresh()

        ReDrawTriggerList()
        ReDrawList()
        RedrawCode()
    End Sub


    Public Sub refreshScreen()
        ReDrawTriggerList()
        ReDrawList()
        RedrawCode()
    End Sub



    Dim playerlist As List(Of List(Of Integer))
    Dim listboxdata As New List(Of Byte)
    Private Sub ReDrawTriggerList()
        ListControl1.Clear()
        ListBox2.Items.Clear()
        listboxdata.Clear()
        playerlist = New List(Of List(Of Integer))
        For i = 0 To 12
            playerlist.Add(New List(Of Integer))
        Next
        '플레이어별로 분배해야 되는데 임시로 이렇게 해두자.
        For i = 0 To RawTriggers.GetElementsCount - 1

            Dim playerflag As UInteger = RawTriggers.GetElements(i).Values(0)
            For j = 0 To 12
                If ((playerflag And Math.Pow(2, j)) > 0) Then
                    playerlist(j).Add(i)
                End If
            Next
        Next

        For i = 0 To 12
            If playerlist(i).Count <> 0 Then
                Select Case i
                    Case 0 To 7
                        ListBox2.Items.Add("Player " & i + 1)
                        listboxdata.Add(i)
                    Case 8 To 11
                        ListBox2.Items.Add("Force " & i - 7)
                        listboxdata.Add(i)
                    Case 12
                        ListBox2.Items.Add("AllPayers")
                        listboxdata.Add(i)
                End Select
            End If
        Next
        If ListBox2.Items.Count <> 0 Then
            ListBox2.SelectedIndex = 0
        End If
    End Sub

    Private Sub ReDrawList()
        WorkSpace.BeginUpdate()
        WorkSpace.Nodes.Clear()

        Tempindex_Element = 0
        ElementINDEX = New List(Of Element)

        globalvarRefresh()

        WorkSpace.Nodes.Add(functions.ToTreeNode())
        WorkSpace.Nodes.Add(RawTriggers.ToTreeNode())
        WorkSpace.Nodes.Add(StartElement.ToTreeNode())
        WorkSpace.Nodes.Add(BeforeElement.ToTreeNode())
        WorkSpace.Nodes.Add(AfterElement.ToTreeNode())


        WorkSpace.Nodes(0).Text = "functions"
        WorkSpace.Nodes(0).Tag = functions
        WorkSpace.Nodes(1).Text = "ClassicTriggers"
        WorkSpace.Nodes(1).Tag = RawTriggers
        WorkSpace.Nodes(2).Text = "onPluginStart"
        WorkSpace.Nodes(2).Tag = StartElement
        WorkSpace.Nodes(3).Text = "beforeTriggerExec"
        WorkSpace.Nodes(3).Tag = BeforeElement
        WorkSpace.Nodes(4).Text = "afterTriggerExec"
        WorkSpace.Nodes(4).Tag = AfterElement
        TextBox1.Text = AddText.Values(0)

        WorkSpace.SelectedNode = WorkSpace.Nodes(0)
        'Dim Tabcount As Byte = 0

        'For i = 0 To MinaElement.GetElementsCount - 1
        '    GListView1.Items.Add("Element" & i)

        '    If MinaElement.GetElements(i).GetElementsCount <> 0 Then
        '        GListView1.Items.Add("    Element" & i)
        '    End If

        'Next

        WorkSpace.EndUpdate()
    End Sub








    Private Function CheckDeleteable(_ele As Element) As Boolean
        If _ele IsNot Nothing Then
            If _ele.GetTypeV <> ElementType.만족 And
                _ele.GetTypeV <> ElementType.만족안함 And
                _ele.GetTypeV <> ElementType.조건절 And
                _ele.GetTypeV <> ElementType.와일만족 And
                _ele.GetTypeV <> ElementType.와일조건 And
                _ele.GetTypeV <> ElementType.main And
                _ele.GetTypeV <> ElementType.포만족 And
                _ele.GetTypeV <> ElementType.인수 And
                _ele.GetTypeV <> ElementType.코드 And
                _ele.GetTypeV <> ElementType.Functions And
                _ele.GetTypeV <> ElementType.FoluderAction And
                _ele.GetTypeV <> ElementType.RawTriggers And
                _ele.GetTypeV <> ElementType.TriggerAct And
                _ele.GetTypeV <> ElementType.TriggerCond Then
                Return True
            End If
        End If
        Return False
    End Function
    Private Function CheckNewFile(_ele As Element) As Boolean
        If _ele IsNot Nothing Then
            If _ele.GetTypeV = ElementType.main Or
                _ele.GetTypeV = ElementType.포 Or
                _ele.GetTypeV = ElementType.포만족 Or
                _ele.GetTypeV = ElementType.만족 Or
                _ele.GetTypeV = ElementType.만족안함 Or
                _ele.GetTypeV = ElementType.조건절 Or
                _ele.GetTypeV = ElementType.와일 Or
                _ele.GetTypeV = ElementType.와일조건 Or
                _ele.GetTypeV = ElementType.와일만족 Or
                _ele.GetTypeV = ElementType.액션 Or
                _ele.GetTypeV = ElementType.조건 Or
                _ele.GetTypeV = ElementType.Switch Or
                _ele.GetTypeV = ElementType.Switchcase Or
                _ele.GetTypeV = ElementType.조건문if Or
                _ele.GetTypeV = ElementType.조건문ifelse Or
                _ele.GetTypeV = ElementType.Functions Or
                _ele.GetTypeV = ElementType.함수정의 Or
                _ele.GetTypeV = ElementType.인수 Or
                _ele.GetTypeV = ElementType.코드 Or
                _ele.GetTypeV = ElementType.함수 Or
                _ele.GetTypeV = ElementType.Wait Or
                _ele.GetTypeV = ElementType.Foluder Or
                _ele.GetTypeV = ElementType.FoluderAction Or
                _ele.GetTypeV = ElementType.RawTriggers Or
                _ele.GetTypeV = ElementType.RawTrigger Or
                _ele.GetTypeV = ElementType.TriggerAct Or
                _ele.GetTypeV = ElementType.TriggerCond Then
                Return True
            End If
        End If
        Return False
    End Function
    Private Function CheckPaste(_ele As Element) As Boolean
        If CopyData IsNot Nothing Then
            If _ele IsNot Nothing Then
                '선택된 곳이...
                If _ele.GetTypeV = ElementType.인수 Then
                    '복사된 내용이 변수일 경우 판단
                    If CopyData.GetTypeV = ElementType.액션 Then
                        If CopyData.act.Name = "CreateVariableWithNoini" Then
                            Return True
                        End If
                    End If
                    Return False
                End If
                If _ele.Parrent IsNot Nothing Then
                    If _ele.Parrent.GetTypeV = ElementType.인수 Then
                        '복사된 내용이 변수일 경우 판단
                        If CopyData.GetTypeV = ElementType.액션 Then
                            If CopyData.act.Name = "CreateVariableWithNoini" Then
                                Return True
                            End If
                        End If
                        Return False
                    End If
                End If

                '케이스를 복사중이면
                If CopyData.GetTypeV = ElementType.Switchcase Then
                    '선택중인 함수가 Switch이거나 Case일 경우
                    If _ele.GetTypeV = ElementType.Switch Or _ele.GetTypeV = ElementType.Switchcase Then
                        Return True
                    Else
                        Return False
                    End If
                End If


                '만약 함수를 복사중 이라면
                If CopyData.GetTypeV = ElementType.함수정의 Then
                    '선택중인 함수가 Funcitons 또는 
                    If _ele.GetTypeV = ElementType.Functions Or _ele.GetTypeV = ElementType.함수정의 Then
                        Return True
                    Else
                        Return False
                    End If
                End If
                If _ele.GetTypeV = ElementType.Functions Or _ele.GetTypeV = ElementType.함수정의 Then
                    If CopyData.GetTypeV = ElementType.함수정의 Then
                        Return True
                    Else
                        Return False
                    End If
                End If
                If _ele.GetTypeV = ElementType.RawTriggers Or _ele.GetTypeV = ElementType.RawTrigger Then
                    If CopyData.GetTypeV = ElementType.RawTrigger Then
                        Return True
                    Else
                        Return False
                    End If
                End If

                '대기하기를 복사중이면
                If CopyData.GetTypeV = ElementType.Wait Then
                    If _ele.Parrent IsNot Nothing Then
                        If _ele.Parrent.GetTypeV = ElementType.코드 Or _ele.Parrent.GetTypeV = ElementType.TriggerAct Then
                            Return True
                        Else
                            If _ele.GetTypeV = ElementType.코드 Or _ele.GetTypeV = ElementType.TriggerAct Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    Else
                        If _ele.GetTypeV = ElementType.코드 Or _ele.GetTypeV = ElementType.TriggerAct Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If


                If CopyData.GetTypeV = ElementType.RawTrigger Then
                    If _ele.GetTypeV = ElementType.RawTriggers Then
                        Return True
                    Else
                        Return False
                    End If
                End If

                '함수가 조건형 함수 일 경우 조건부 넣을 수 있음
                If _ele.GetTypeV = ElementType.함수 Then
                    If _ele.Parrent.GetTypeV = ElementType.조건절 Or _ele.Parrent.GetTypeV = ElementType.와일조건 Or _ele.Parrent.GetTypeV = ElementType.TriggerCond Then
                        Select Case CopyData.GetTypeV
                            Case ElementType.조건
                                Return True
                            Case ElementType.액션
                                Return False
                        End Select
                    End If
                End If


                If _ele.GetTypeV = ElementType.조건 Or _ele.GetTypeV = ElementType.조건절 Or _ele.GetTypeV = ElementType.와일조건 Or _ele.GetTypeV = ElementType.TriggerCond Then
                    If CopyData.GetTypeV = ElementType.조건 Or CopyData.GetTypeV = ElementType.함수 Then
                        Return True
                    Else
                        Return False
                    End If
                Else '액션부일 경우.
                    If CopyData.GetTypeV <> ElementType.조건 Then
                        Return True
                    Else
                        Return False
                    End If
                End If

            End If
        End If

        Return False
    End Function


    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        MenuRefresh()
    End Sub

    Private Sub MenuRefresh()
        Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)
        'Dim _ParrentElement As Element = _selectElement.Parrent
        If _selectElement.GetTypeV = ElementType.조건 Or
            _selectElement.GetTypeV = ElementType.액션 Or
            _selectElement.GetTypeV = ElementType.포 Or
            _selectElement.GetTypeV = ElementType.Wait Or
            _selectElement.GetTypeV = ElementType.Foluder Or
            _selectElement.GetTypeV = ElementType.RawTrigger Or
            _selectElement.GetTypeV = ElementType.Switch Or
            _selectElement.GetTypeV = ElementType.Switchcase Then
            수정ToolStripMenuItem.Enabled = True
        ElseIf _selectElement.GetTypeV = ElementType.함수 And functions.GetElementsCount <> 0 Then
            수정ToolStripMenuItem.Enabled = True
        Else
            함수저장ToolStripMenuItem.Enabled = False

            If _selectElement.GetTypeV = ElementType.와일조건 Or _selectElement.GetTypeV = ElementType.조건절 Then
                수정ToolStripMenuItem.Enabled = True
            Else
                수정ToolStripMenuItem.Enabled = False
            End If
        End If


        If CheckDeleteable(_selectElement) = True Then
            삭제DToolStripMenuItem.Enabled = True
            복사VToolStripMenuItem.Enabled = True
            잘라내기XToolStripMenuItem.Enabled = True
        Else
            삭제DToolStripMenuItem.Enabled = False
            복사VToolStripMenuItem.Enabled = False
            잘라내기XToolStripMenuItem.Enabled = False
        End If


        If _selectElement.GetTypeV = ElementType.함수정의 Then
            수정ToolStripMenuItem.Enabled = True
            함수저장ToolStripMenuItem.Enabled = True
        Else
            함수저장ToolStripMenuItem.Enabled = False
        End If


        If CheckNewFile(_selectElement) = True Then
            새로만들기NToolStripMenuItem1.Enabled = True
            함수FToolStripMenuItem.Enabled = True
            액션ToolStripMenuItem.Enabled = False
            ToolStripMenuItem1.Enabled = False
            함수ToolStripMenuItem.Enabled = False
            함수불러오기ToolStripMenuItem.Enabled = False
            If문ToolStripMenuItem.Enabled = False
            IfElse문ToolStripMenuItem.Enabled = False
            For문ToolStripMenuItem.Enabled = False
            While문ToolStripMenuItem.Enabled = False
            스위치ToolStripMenuItem.Enabled = False
            케이스ToolStripMenuItem.Enabled = False
            조건ToolStripMenuItem.Enabled = False
            ToolStripMenuItem2.Enabled = False
            함수정의ToolStripMenuItem.Enabled = False
            인수ToolStripMenuItem.Enabled = False


            Select Case _selectElement.GetTypeV
                Case ElementType.main, ElementType.포만족, ElementType.만족, ElementType.만족안함, ElementType.액션, ElementType.Foluder, ElementType.조건문if, ElementType.조건문ifelse, ElementType.와일, ElementType.와일만족, ElementType.포, ElementType.코드, ElementType.Wait, ElementType.FoluderAction, ElementType.TriggerAct
                    액션ToolStripMenuItem.Enabled = True
                    ToolStripMenuItem1.Enabled = True
                    If문ToolStripMenuItem.Enabled = True
                    IfElse문ToolStripMenuItem.Enabled = True
                    For문ToolStripMenuItem.Enabled = True
                    While문ToolStripMenuItem.Enabled = True
                    스위치ToolStripMenuItem.Enabled = True
                    If functions.GetElementsCount <> 0 Then
                        함수ToolStripMenuItem.Enabled = True
                    End If
                Case ElementType.조건절, ElementType.조건, ElementType.와일조건, ElementType.TriggerCond
                    조건ToolStripMenuItem.Enabled = True
                    If functions.GetElementsCount <> 0 Then
                        함수ToolStripMenuItem.Enabled = True
                    End If
                Case ElementType.함수
                    If _selectElement.Parrent.GetTypeV = ElementType.조건절 Or _selectElement.Parrent.GetTypeV = ElementType.와일조건 Or _selectElement.Parrent.GetTypeV = ElementType.TriggerCond Then
                        조건ToolStripMenuItem.Enabled = True
                        If functions.GetElementsCount <> 0 Then
                            함수ToolStripMenuItem.Enabled = True
                        End If
                    Else
                        액션ToolStripMenuItem.Enabled = True
                        ToolStripMenuItem1.Enabled = True
                        If문ToolStripMenuItem.Enabled = True
                        IfElse문ToolStripMenuItem.Enabled = True
                        For문ToolStripMenuItem.Enabled = True
                        While문ToolStripMenuItem.Enabled = True
                        스위치ToolStripMenuItem.Enabled = True
                        If functions.GetElementsCount <> 0 Then
                            함수ToolStripMenuItem.Enabled = True
                        End If
                    End If
                Case ElementType.Functions, ElementType.함수정의
                    함수정의ToolStripMenuItem.Enabled = True
                    함수불러오기ToolStripMenuItem.Enabled = True
                Case ElementType.인수
                    인수ToolStripMenuItem.Enabled = True
                Case ElementType.인수
                    인수ToolStripMenuItem.Enabled = True
                Case ElementType.RawTriggers, ElementType.RawTrigger
                    ToolStripMenuItem2.Enabled = True
                Case ElementType.Switch, ElementType.Switchcase
                    액션ToolStripMenuItem.Enabled = True
                    ToolStripMenuItem1.Enabled = True
                    If문ToolStripMenuItem.Enabled = True
                    IfElse문ToolStripMenuItem.Enabled = True
                    For문ToolStripMenuItem.Enabled = True
                    While문ToolStripMenuItem.Enabled = True
                    스위치ToolStripMenuItem.Enabled = True
                    케이스ToolStripMenuItem.Enabled = True
                    If functions.GetElementsCount <> 0 Then
                        함수ToolStripMenuItem.Enabled = True
                    End If
            End Select



            If CheckPaste(_selectElement) = True Then
                붙혀넣기CToolStripMenuItem.Enabled = True
            Else
                붙혀넣기CToolStripMenuItem.Enabled = False
            End If
        Else
            새로만들기NToolStripMenuItem1.Enabled = False
            붙혀넣기CToolStripMenuItem.Enabled = False
        End If



        If _selectElement.GetTypeV = ElementType.인수 Then
            '복사VToolStripMenuItem.Enabled = False
            '잘라내기XToolStripMenuItem.Enabled = False
            액션ToolStripMenuItem.Enabled = False
            ToolStripMenuItem1.Enabled = False
            함수ToolStripMenuItem.Enabled = False
            함수불러오기ToolStripMenuItem.Enabled = False
            If문ToolStripMenuItem.Enabled = False
            IfElse문ToolStripMenuItem.Enabled = False
            For문ToolStripMenuItem.Enabled = False
            While문ToolStripMenuItem.Enabled = False
            스위치ToolStripMenuItem.Enabled = False
            케이스ToolStripMenuItem.Enabled = False
            조건ToolStripMenuItem.Enabled = False
            함수정의ToolStripMenuItem.Enabled = False
            ToolStripMenuItem2.Enabled = False
            인수ToolStripMenuItem.Enabled = True
        End If
        If _selectElement.Parrent IsNot Nothing Then
            If _selectElement.Parrent.GetTypeV = ElementType.인수 Then
                '복사VToolStripMenuItem.Enabled = False
                '잘라내기XToolStripMenuItem.Enabled = False
                액션ToolStripMenuItem.Enabled = False
                ToolStripMenuItem1.Enabled = False
                함수ToolStripMenuItem.Enabled = False
                함수불러오기ToolStripMenuItem.Enabled = False
                If문ToolStripMenuItem.Enabled = False
                IfElse문ToolStripMenuItem.Enabled = False
                For문ToolStripMenuItem.Enabled = False
                While문ToolStripMenuItem.Enabled = False
                스위치ToolStripMenuItem.Enabled = False
                케이스ToolStripMenuItem.Enabled = False
                조건ToolStripMenuItem.Enabled = False
                함수정의ToolStripMenuItem.Enabled = False
                ToolStripMenuItem2.Enabled = False
                인수ToolStripMenuItem.Enabled = True
            End If
            If _selectElement.Parrent.GetTypeV = ElementType.코드 Or _selectElement.Parrent.GetTypeV = ElementType.TriggerAct Then
                대기하기ToolStripMenuItem.Enabled = True
            Else
                If _selectElement.GetTypeV = ElementType.코드 Or _selectElement.GetTypeV = ElementType.TriggerAct Then
                    대기하기ToolStripMenuItem.Enabled = True
                Else
                    대기하기ToolStripMenuItem.Enabled = False
                End If
            End If
        Else
            If _selectElement.GetTypeV = ElementType.코드 Or _selectElement.GetTypeV = ElementType.TriggerAct Then
                대기하기ToolStripMenuItem.Enabled = True
            Else
                대기하기ToolStripMenuItem.Enabled = False
            End If
        End If
    End Sub

    Private Sub ButtonRefresh()
        MenuRefresh()
        Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)
        'Dim _ParrentElement As Element = _selectElement.Parrent
        If _selectElement.GetTypeV = ElementType.조건 Or
            _selectElement.GetTypeV = ElementType.액션 Or
            _selectElement.GetTypeV = ElementType.포 Then
        Else
            함수저장Btn.Visible = False

        End If




        If _selectElement.GetTypeV = ElementType.함수정의 Then
            함수저장Btn.Visible = True
        Else
            함수저장Btn.Visible = False
        End If


        If CheckNewFile(_selectElement) = True Then
            액션Btn.Visible = False
            Button5.Visible = False
            함수Btn.Visible = False
            함수불러오기Btn.Visible = False
            IfBtn.Visible = False
            IfElseBtn.Visible = False
            ForBtn.Visible = False
            WhileBtn.Visible = False
            Button7.Visible = False
            Button20.Visible = False
            조건Btn.Visible = False
            Button17.Visible = False
            함수정의Btn.Visible = False
            인수Btn.Visible = False






            Select Case _selectElement.GetTypeV
                Case ElementType.main, ElementType.포만족, ElementType.만족, ElementType.만족안함, ElementType.액션, ElementType.Foluder, ElementType.조건문if, ElementType.조건문ifelse, ElementType.와일, ElementType.와일만족, ElementType.포, ElementType.코드, ElementType.Wait, ElementType.FoluderAction, ElementType.TriggerAct
                    액션Btn.Visible = True
                    Button5.Visible = True
                    IfBtn.Visible = True
                    IfElseBtn.Visible = True
                    ForBtn.Visible = True
                    WhileBtn.Visible = True
                    Button7.Visible = True
                    If functions.GetElementsCount <> 0 Then
                        함수Btn.Visible = True
                    End If
                Case ElementType.조건절, ElementType.조건, ElementType.와일조건, ElementType.TriggerCond
                    조건Btn.Visible = True
                    If functions.GetElementsCount <> 0 Then
                        함수Btn.Visible = True
                    End If
                Case ElementType.함수
                    If _selectElement.Parrent.GetTypeV = ElementType.조건절 Or _selectElement.Parrent.GetTypeV = ElementType.와일조건 Or _selectElement.Parrent.GetTypeV = ElementType.TriggerCond Then
                        조건Btn.Visible = True
                        If functions.GetElementsCount <> 0 Then
                            함수Btn.Visible = True
                        End If
                    Else
                        액션Btn.Visible = True
                        Button5.Visible = True
                        IfBtn.Visible = True
                        IfElseBtn.Visible = True
                        ForBtn.Visible = True
                        WhileBtn.Visible = True
                        Button7.Visible = True
                        If functions.GetElementsCount <> 0 Then
                            함수Btn.Visible = True
                        End If
                    End If
                Case ElementType.Functions, ElementType.함수정의
                    함수정의Btn.Visible = True
                    함수불러오기Btn.Visible = True
                Case ElementType.인수
                    인수Btn.Visible = True
                Case ElementType.RawTrigger, ElementType.RawTriggers
                    Button17.Visible = True
                Case ElementType.Switch, ElementType.Switchcase
                    액션Btn.Visible = True
                    Button5.Visible = True
                    IfBtn.Visible = True
                    IfElseBtn.Visible = True
                    ForBtn.Visible = True
                    WhileBtn.Visible = True
                    Button7.Visible = True
                    Button20.Visible = True
                    If functions.GetElementsCount <> 0 Then
                        함수Btn.Visible = True
                    End If
            End Select
        End If
        If _selectElement.GetTypeV = ElementType.인수 Then
            액션Btn.Visible = False
            함수Btn.Visible = False
            Button5.Visible = False
            함수불러오기Btn.Visible = False
            IfBtn.Visible = False
            IfElseBtn.Visible = False
            ForBtn.Visible = False
            WhileBtn.Visible = False
            Button7.Visible = False
            Button20.Visible = False
            조건Btn.Visible = False
            Button17.Visible = False
            함수정의Btn.Visible = False
            인수Btn.Visible = True
        End If
        If _selectElement.Parrent IsNot Nothing Then
            If _selectElement.Parrent.GetTypeV = ElementType.인수 Then
                액션Btn.Visible = False
                Button5.Visible = False
                함수Btn.Visible = False
                함수불러오기Btn.Visible = False
                IfBtn.Visible = False
                IfElseBtn.Visible = False
                ForBtn.Visible = False
                WhileBtn.Visible = False
                Button7.Visible = False
                Button20.Visible = False
                조건Btn.Visible = False
                함수정의Btn.Visible = False
                인수Btn.Visible = True
            End If
            If _selectElement.Parrent.GetTypeV = ElementType.코드 Or _selectElement.Parrent.GetTypeV = ElementType.TriggerAct Then
                대기하기Btn.Visible = True
            Else
                If _selectElement.GetTypeV = ElementType.코드 Or _selectElement.GetTypeV = ElementType.TriggerAct Then
                    대기하기Btn.Visible = True
                Else
                    대기하기Btn.Visible = False
                End If
            End If
        Else
            If _selectElement.GetTypeV = ElementType.코드 Or _selectElement.GetTypeV = ElementType.TriggerAct Then
                대기하기Btn.Visible = True
            Else
                대기하기Btn.Visible = False
            End If
        End If
    End Sub







    Private Sub 삭제DToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 삭제DToolStripMenuItem.Click
        Delete(WorkSpace.SelectedNode)
    End Sub



    Private Sub If문ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles If문ToolStripMenuItem.Click
        NewEle(WorkSpace.SelectedNode, ElementType.조건문if)
    End Sub

    Private Sub IfElse문ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IfElse문ToolStripMenuItem.Click
        NewEle(WorkSpace.SelectedNode, ElementType.조건문ifelse)
    End Sub

    Private Sub SwitchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 스위치ToolStripMenuItem.Click
        AddSwitch(True)
    End Sub

    Private Sub CaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 케이스ToolStripMenuItem.Click
        AddCase(True)
    End Sub

    Private Sub While문ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles While문ToolStripMenuItem.Click
        NewEle(WorkSpace.SelectedNode, ElementType.와일)
    End Sub

    Private Sub For문ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles For문ToolStripMenuItem.Click
        formaker()
    End Sub





    Private CopyData As Element

    Private Sub 복사VToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 복사VToolStripMenuItem.Click
        If CheckDeleteable(CType(WorkSpace.SelectedNode.Tag, Element)) = True Then
            Copy(WorkSpace.SelectedNode)
        End If
    End Sub



    Private Sub 잘라내기XToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 잘라내기XToolStripMenuItem.Click
        Cut(WorkSpace.SelectedNode)
    End Sub

    Private Sub 붙혀넣기CToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 붙혀넣기CToolStripMenuItem.Click
        Paste(WorkSpace.SelectedNode)
    End Sub







    Private Sub Delete(ByRef selectnode As TreeNode)
        Dim _tempele As Element = CType(selectnode.Tag, Element)
        If CheckDeleteable(_tempele) = True Then
            TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.delete, _tempele, selectnode)
            UndoRedoBtnRefresh()

            _tempele.Delete()
            selectnode.Remove()
        End If
        RedrawCode()
    End Sub

    Private Sub Copy(ByRef selectnode As TreeNode)
        If CheckDeleteable(CType(selectnode.Tag, Element)) = True Then
            Dim _tempele As Element = CType(selectnode.Tag, Element)
            CopyData = _tempele.Clone(Nothing)
        End If
    End Sub

    Private Sub Cut(ByRef selectnode As TreeNode)
        Copy(selectnode)
        Delete(selectnode)
    End Sub

    Private Sub Paste(ByRef selectnode As TreeNode)
        Dim _tempele As Element = CType(selectnode.Tag, Element)
        If CheckNewFile(_tempele) = True Then
            Select Case _tempele.GetTypeV
                Case ElementType.main, ElementType.포만족, ElementType.만족, ElementType.만족안함, ElementType.조건절, ElementType.와일조건, ElementType.와일만족, ElementType.FoluderAction, ElementType.Functions, ElementType.코드, ElementType.인수, ElementType.TriggerAct, ElementType.TriggerCond, ElementType.RawTriggers


                    CopyData.Parrent = _tempele
                    _tempele.AddElements(0, CopyData)
                    selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                    'selectnode.FirstNode.Expand()
                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                    UndoRedoBtnRefresh()

                Case ElementType.액션, ElementType.조건, ElementType.조건문if, ElementType.조건문ifelse, ElementType.와일, ElementType.포, ElementType.함수정의, ElementType.함수, ElementType.Wait, ElementType.Foluder, ElementType.RawTrigger
                    Dim _index As Integer = selectnode.Index + 1
                    CopyData.Parrent = _tempele.Parrent
                    _tempele.Parrent.AddElements(_index, CopyData)
                    selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)
                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                    UndoRedoBtnRefresh()
                Case ElementType.Switch
                    If CopyData.GetTypeV = ElementType.Switchcase Then
                        CopyData.Parrent = _tempele
                        _tempele.AddElements(0, CopyData)
                        selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                        'selectnode.FirstNode.Expand()
                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                        UndoRedoBtnRefresh()
                    Else
                        Dim _index As Integer = selectnode.Index + 1
                        CopyData.Parrent = _tempele.Parrent
                        _tempele.Parrent.AddElements(_index, CopyData)
                        selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)
                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                        UndoRedoBtnRefresh()
                    End If

                Case ElementType.Switchcase
                    If CopyData.GetTypeV = ElementType.Switchcase Then
                        Dim _index As Integer = selectnode.Index + 1
                        CopyData.Parrent = _tempele.Parrent
                        _tempele.Parrent.AddElements(_index, CopyData)
                        selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)
                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                        UndoRedoBtnRefresh()

                    Else
                        CopyData.Parrent = _tempele
                        _tempele.AddElements(0, CopyData)
                        selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                        'selectnode.FirstNode.Expand()
                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                        UndoRedoBtnRefresh()
                    End If
            End Select
        End If
        RedrawCode()
    End Sub

    Private Sub NewEle(ByRef selectnode As TreeNode, _type As ElementType)
        Dim _tempele As Element = CType(selectnode.Tag, Element)
        If CheckNewFile(_tempele) = True Then
            Select Case _tempele.GetTypeV
                Case ElementType.main, ElementType.포만족, ElementType.만족, ElementType.만족안함, ElementType.와일만족, ElementType.코드, ElementType.FoluderAction, ElementType.TriggerAct
                    If _type = ElementType.액션 Or _type = ElementType.조건 Then

                    Else
                        _tempele.AddElements(0, _type)
                    End If


                    selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                    'selectnode.LastNode.Expand()

                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                    UndoRedoBtnRefresh()
                Case ElementType.조건절, ElementType.와일조건, ElementType.TriggerCond
                    _tempele.AddElements(_type, 0)
                    selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                    'selectnode.FirstNode.Expand()

                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                    UndoRedoBtnRefresh()
                Case ElementType.Switchcase
                    If _type = ElementType.Switchcase Then
                        Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                        _tempele.Parrent.AddElements(_index, _type)
                        selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)
                        'selectnode.Parent.Expand()
                        'selectnode.Parent.Nodes(_index).Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                        UndoRedoBtnRefresh()
                    Else
                        _tempele.AddElements(0, _type)

                        selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                        'selectnode.LastNode.Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                        UndoRedoBtnRefresh()
                    End If
                Case ElementType.Switch
                    If _type = ElementType.Switchcase Then
                        _tempele.AddElements(0, _type)
                        selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                        'selectnode.LastNode.Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                        UndoRedoBtnRefresh()
                    Else
                        Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                        _tempele.Parrent.AddElements(_index, _type)
                        selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)
                        'selectnode.Parent.Expand()
                        'selectnode.Parent.Nodes(_index).Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                        UndoRedoBtnRefresh()
                    End If


                Case ElementType.액션, ElementType.조건문if, ElementType.조건문ifelse, ElementType.포, ElementType.와일, ElementType.함수, ElementType.Wait, ElementType.Foluder
                    Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                    _tempele.Parrent.AddElements(_index, _type)
                    selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)
                    'selectnode.Parent.Expand()
                    'selectnode.Parent.Nodes(_index).Expand()

                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                    UndoRedoBtnRefresh()
                Case ElementType.조건
                    Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                    _tempele.Parrent.AddElements(_index, _type, 0)
                    selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)

                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                    UndoRedoBtnRefresh()
                Case ElementType.Functions
                    _tempele.AddElements(0, _type)
                    selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                    'selectnode.FirstNode.Expand()

                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                    UndoRedoBtnRefresh()
                Case ElementType.함수정의
                    Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                    _tempele.Parrent.AddElements(_index, _type)
                    selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)
                    'selectnode.Parent.Expand()
                    'selectnode.Parent.Nodes(_index).Expand()

                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                    UndoRedoBtnRefresh()
            End Select
        End If

        RedrawCode()
    End Sub

    Private Sub NewEle(ByRef selectnode As TreeNode, _element As Element)
        Dim _tempele As Element = CType(selectnode.Tag, Element)
        If CheckNewFile(_tempele) = True Then
            Select Case _tempele.GetTypeV
                Case ElementType.main, ElementType.포만족, ElementType.만족, ElementType.만족안함, ElementType.와일만족, ElementType.코드, ElementType.FoluderAction, ElementType.TriggerAct
                    If _element.GetTypeV = ElementType.액션 Or _element.GetTypeV = ElementType.조건 Then

                    Else
                        _tempele.AddElements(0, _element)
                    End If


                    selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                    'selectnode.LastNode.Expand()

                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                    UndoRedoBtnRefresh()
                Case ElementType.조건절, ElementType.와일조건, ElementType.TriggerCond
                    _tempele.AddElements(_element)
                    selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                    'selectnode.FirstNode.Expand()

                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                    UndoRedoBtnRefresh()

                Case ElementType.Switchcase
                    If _element.GetTypeV = ElementType.Switchcase Then
                        Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                        _tempele.Parrent.AddElements(_index, _element)
                        selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)
                        'selectnode.Parent.Expand()
                        'selectnode.Parent.Nodes(_index).Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                        UndoRedoBtnRefresh()
                    Else
                        _tempele.AddElements(0, _element)

                        selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                        'selectnode.LastNode.Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                        UndoRedoBtnRefresh()
                    End If

                Case ElementType.Switch
                    If _element.GetTypeV = ElementType.Switchcase Then
                        _tempele.AddElements(0, _element)
                        selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                        'selectnode.LastNode.Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                        UndoRedoBtnRefresh()
                    Else
                        Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                        _tempele.Parrent.AddElements(_index, _element)
                        selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)
                        'selectnode.Parent.Expand()
                        'selectnode.Parent.Nodes(_index).Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                        UndoRedoBtnRefresh()
                    End If


                Case ElementType.액션, ElementType.조건문if, ElementType.조건문ifelse, ElementType.포, ElementType.와일, ElementType.함수, ElementType.Wait, ElementType.Foluder
                    Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                    _tempele.Parrent.AddElements(_index, _element)
                    selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)
                    'selectnode.Parent.Expand()
                    'selectnode.Parent.Nodes(_index).Expand()

                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                    UndoRedoBtnRefresh()
                Case ElementType.조건
                    Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                    _tempele.Parrent.AddElements(_index, _element)
                    selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)

                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                    UndoRedoBtnRefresh()
                Case ElementType.Functions
                    _tempele.AddElements(0, _element)
                    selectnode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)
                    'selectnode.FirstNode.Expand()

                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), selectnode.Nodes(0))
                    UndoRedoBtnRefresh()
                Case ElementType.함수정의
                    Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                    _tempele.Parrent.AddElements(_index, _element)
                    selectnode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)
                    'selectnode.Parent.Expand()
                    'selectnode.Parent.Nodes(_index).Expand()

                    TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), selectnode.NextNode)
                    UndoRedoBtnRefresh()
            End Select
        End If

        RedrawCode()
    End Sub

    Private Sub ActionFormShow(_isNewAct As Boolean, _targetEle As Element)
        Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)
        ActionForm._varele = _selectElement
        ActionForm.isNewAct = _isNewAct

        If _isNewAct = True Then
            Select Case _selectElement.GetTypeV
                Case ElementType.main, ElementType.포만족, ElementType.만족, ElementType.만족안함, ElementType.조건절, ElementType.와일조건, ElementType.Switchcase, ElementType.와일만족, ElementType.FoluderAction, ElementType.코드, ElementType.TriggerAct
                    If _selectElement.GetElementsCount <> 0 Then
                        ActionForm._varele = _selectElement.GetElementList(_selectElement.GetElementsCount - 1)
                    End If

                    ActionForm._ele = New Element(_selectElement, ElementType.액션, 0)
                Case ElementType.액션, ElementType.조건, ElementType.Switch, ElementType.조건문if, ElementType.조건문ifelse, ElementType.와일, ElementType.포, ElementType.함수, ElementType.Wait, ElementType.Foluder
                    ActionForm._ele = New Element(_selectElement.Parrent, ElementType.액션, 0)
            End Select
        Else
            ActionForm._ele = _targetEle.Clone(Nothing)
        End If



        If ActionForm.ShowDialog() = DialogResult.OK Then
            If _isNewAct = False Then
                _selectElement.act = ActionForm._ele.act
                _selectElement.SetValue(ActionForm._ele.Values.ToArray)

                WorkSpace.SelectedNode.Text = _selectElement.GetText
                '벨류 넣는거 까먹지말아라
            Else
                Select Case _selectElement.GetTypeV
                    Case ElementType.main, ElementType.포만족, ElementType.만족, ElementType.만족안함, ElementType.Switchcase, ElementType.와일만족, ElementType.FoluderAction, ElementType.코드, ElementType.TriggerAct
                        _selectElement.AddElements(0, ActionForm._ele)

                        WorkSpace.SelectedNode.Nodes.Insert(0, _selectElement.GetElementList.First.ToTreeNode)

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _selectElement.GetElements(0), WorkSpace.SelectedNode.Nodes(0))
                        UndoRedoBtnRefresh()
                    Case ElementType.액션, ElementType.Switch, ElementType.조건문if, ElementType.조건문ifelse, ElementType.와일, ElementType.포, ElementType.함수, ElementType.Wait, ElementType.Foluder
                        Dim _index As Integer = WorkSpace.SelectedNode.Index + 1

                        _selectElement.Parrent.AddElements(_index, ActionForm._ele)

                        WorkSpace.SelectedNode.Parent.Nodes.Insert(_index, _selectElement.Parrent.GetElementList(_index).ToTreeNode)

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _selectElement.Parrent.GetElements(_index), WorkSpace.SelectedNode.NextNode)
                        UndoRedoBtnRefresh()
                End Select
            End If

        End If

        If ActionForm._ele.GetTypeV = ElementType.액션 Then
            If ActionForm._ele.act.Name = "Comment" Then
                If _selectElement.Parrent.GetTypeV = ElementType.TriggerAct Then
                    WorkSpace.SelectedNode.Parent.Parent.Text = _selectElement.Parrent.Parrent.GetText
                ElseIf _selectElement.Parrent.GetTypeV = ElementType.RawTrigger Then
                    WorkSpace.SelectedNode.Parent.Text = _selectElement.Parrent.GetText
                End If
            End If
        End If


        RedrawCode()
        'WorkSpace.SelectedNode.Expand()
    End Sub

    Private Sub CondicitonFormShow(_isNewCon As Boolean, _targetEle As Element)
        Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)
        CondictionForm._varele = _selectElement
        CondictionForm.isNewCon = _isNewCon

        If _isNewCon = True Then
            Select Case _selectElement.GetTypeV
                Case ElementType.main, ElementType.포만족, ElementType.만족, ElementType.만족안함, ElementType.조건절, ElementType.와일조건, ElementType.Switchcase, ElementType.와일만족, ElementType.FoluderAction, ElementType.TriggerAct, ElementType.TriggerCond
                    'CondictionForm._varele = _selectElement.GetElementList(_selectElement.GetElementsCount)
                    CondictionForm._ele = New Element(_selectElement, ElementType.조건, 0)
                Case ElementType.액션, ElementType.Switch, ElementType.조건, ElementType.조건문if, ElementType.조건문ifelse, ElementType.와일, ElementType.포, ElementType.함수
                    CondictionForm._ele = New Element(_selectElement.Parrent, ElementType.조건, 0)
            End Select
        Else
            CondictionForm._ele = _targetEle.Clone(Nothing)
        End If



        If CondictionForm.ShowDialog() = DialogResult.OK Then
            If _isNewCon = False Then
                _selectElement.con = CondictionForm._ele.con
                _selectElement.SetValue(CondictionForm._ele.Values.ToArray)

                WorkSpace.SelectedNode.Text = _selectElement.GetText
                '벨류 넣는거 까먹지말아라
            Else
                Select Case _selectElement.GetTypeV
                    Case ElementType.와일조건, ElementType.조건절, ElementType.TriggerCond
                        _selectElement.AddElements(0, CondictionForm._ele)

                        WorkSpace.SelectedNode.Nodes.Insert(0, _selectElement.GetElementList.First.ToTreeNode)

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _selectElement.GetElements(0), WorkSpace.SelectedNode.Nodes(0))
                        UndoRedoBtnRefresh()
                    Case ElementType.조건, ElementType.함수
                        Dim _index As Integer = WorkSpace.SelectedNode.Index + 1

                        _selectElement.Parrent.AddElements(_index, CondictionForm._ele)

                        WorkSpace.SelectedNode.Parent.Nodes.Insert(_index, _selectElement.Parrent.GetElementList(_index).ToTreeNode)

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _selectElement.Parrent.GetElements(_index), WorkSpace.SelectedNode.NextNode)
                        UndoRedoBtnRefresh()
                End Select
            End If

        End If

        RedrawCode()
        'WorkSpace.SelectedNode.Expand()
    End Sub

    Private Sub ForEditingShow(_isNewfor As Boolean, _targetEle As Element)
        Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)

        If _isNewfor = False Then
            ForEditing._Element = _targetEle.Clone(Nothing)
        Else
            ForEditing._Element = New Element(Nothing, ElementType.포)
        End If


        If ForEditing.ShowDialog() = DialogResult.OK Then
            If _isNewfor = True Then
                NewEle(WorkSpace.SelectedNode, ForEditing._Element.Clone(_selectElement))
            Else
                _targetEle.SetValue(ForEditing._Element.Values.ToArray)
                WorkSpace.SelectedNode.Text = _targetEle.GetText
            End If
        End If

        RedrawCode()
    End Sub
    Private Sub Factor()
        'DeclareVariable
        Dim _index As Integer
        For i = 0 To Actions.Count - 1
            If Actions(i).Name = "CreateVariableWithNoini" Then
                _index = i
            End If
        Next
        CreateValForm.NumericUpDown1.Visible = False
        CreateValForm.EasyCompletionComboBox1.Visible = True
        CreateValForm.Label2.Text = "인수형식"

        CreateValForm.EasyCompletionComboBox1.Items.Clear()
        For i = 0 To ValueDefiniction.Count - 1
            CreateValForm.EasyCompletionComboBox1.Items.Add(ValueDefiniction(i).Name(0))
        Next
        CreateValForm.EasyCompletionComboBox1.SelectedIndex = 0


        CreateValForm.TextBox1.Text = ""
        CreateValForm.NumericUpDown1.Value = 0
        If CreateValForm.ShowDialog = DialogResult.OK Then
            Dim _tempele As Element = CType(WorkSpace.SelectedNode.Tag, Element)

            If _tempele.GetTypeV = ElementType.액션 Then
                _tempele.Parrent.AddElements(New Element(GlobalVar, ElementType.액션, _index, {CreateValForm.TextBox1.Text, CreateValForm.EasyCompletionComboBox1.SelectedIndex}))
                WorkSpace.SelectedNode.Parent.Nodes.Add(_tempele.Parrent.GetElementList.Last.ToTreeNode)
                'WorkSpace.SelectedNode.Parent.LastNode.Expand()

                TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElementList.Last, WorkSpace.SelectedNode.NextNode)
                UndoRedoBtnRefresh()
            Else
                _tempele.AddElements(New Element(GlobalVar, ElementType.액션, _index, {CreateValForm.TextBox1.Text, CreateValForm.EasyCompletionComboBox1.SelectedIndex}))
                WorkSpace.SelectedNode.Nodes.Add(_tempele.GetElementList.Last.ToTreeNode)
                'WorkSpace.SelectedNode.LastNode.Expand()

                TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), WorkSpace.SelectedNode.Nodes(0))
                UndoRedoBtnRefresh()
            End If
        End If

        RedrawCode()
    End Sub

    Private Sub Func()
        FunctionForm.FunEle = New Element(Nothing, ElementType.함수, {"Name"})
        Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)
        My.Forms.FunctionForm.isNew = True
        FunctionForm._varele = _selectElement
        If FunctionForm.ShowDialog() = DialogResult.OK Then


            NewEle(WorkSpace.SelectedNode, FunctionForm.FunEle)
        End If

        RedrawCode()
    End Sub

    Private Sub FuncNew()
        FuctionNameForm.TextBox1.Text = ""
        FuctionNameForm.CheckBox1.Checked = False
        If FuctionNameForm.ShowDialog = DialogResult.OK Then
            Dim _tempele As Element = CType(WorkSpace.SelectedNode.Tag, Element)
            If CheckNewFile(_tempele) = True Then
                Select Case _tempele.GetTypeV
                    Case ElementType.Functions
                        _tempele.AddElements(0, ElementType.함수정의)
                        _tempele.GetElementList.First.SetValue({FuctionNameForm.TextBox1.Text, FuctionNameForm.CheckBox1.Checked})
                        WorkSpace.SelectedNode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)


                        'WorkSpace.SelectedNode.FirstNode.Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), WorkSpace.SelectedNode.Nodes(0))
                        UndoRedoBtnRefresh()
                    Case ElementType.함수정의
                        Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                        _tempele.Parrent.AddElements(_index, ElementType.함수정의)
                        _tempele.Parrent.GetElementList(_index).SetValue({FuctionNameForm.TextBox1.Text, FuctionNameForm.CheckBox1.Checked})
                        WorkSpace.SelectedNode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)


                        'WorkSpace.SelectedNode.Parent.Expand()
                        'WorkSpace.SelectedNode.Parent.Nodes(_index).Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), WorkSpace.SelectedNode.NextNode)
                        UndoRedoBtnRefresh()
                End Select
            End If
        End If

        RedrawCode()
    End Sub
    Private Sub FuncLoad()
        OpenFileDialog2.InitialDirectory = My.Application.Info.DirectoryPath & "\TEFunction"

        Dim dialog As DialogResult = OpenFileDialog2.ShowDialog()

        If dialog = DialogResult.OK Then
            For Each filename As String In OpenFileDialog2.FileNames
                Dim _tempele As Element = CType(WorkSpace.SelectedNode.Tag, Element)
                Dim newElement As New Element(Nothing, ElementType.main)



                Dim _filestream As New FileStream(filename, FileMode.Open)
                Dim _streamreader As New StreamReader(_filestream)

                newElement.LoadFile(_streamreader.ReadToEnd(), 0)

                Select Case _tempele.GetTypeV
                    Case ElementType.Functions
                        _tempele.AddElements(0, newElement)
                        WorkSpace.SelectedNode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)


                        'WorkSpace.SelectedNode.FirstNode.Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), WorkSpace.SelectedNode.Nodes(0))
                        UndoRedoBtnRefresh()
                    Case ElementType.함수정의
                        Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                        _tempele.Parrent.AddElements(_index, newElement)
                        WorkSpace.SelectedNode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)


                        'WorkSpace.SelectedNode.Parent.Expand()
                        'WorkSpace.SelectedNode.Parent.Nodes(_index).Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), WorkSpace.SelectedNode.NextNode)
                        UndoRedoBtnRefresh()
                End Select
                _streamreader.Close()
                _filestream.Close()
            Next

        End If
        RedrawCode()
    End Sub

    Private Sub TriggerNew()
        With TriggerPlayerSelectDialog.CheckedListBox1
            For i = 0 To .Items.Count - 1
                .SetItemChecked(0, False)
            Next
        End With

        If TriggerPlayerSelectDialog.ShowDialog = DialogResult.OK Then
            Dim value As Integer = 0
            With TriggerPlayerSelectDialog.CheckedListBox1
                For i = 0 To .Items.Count - 1
                    If .GetItemChecked(i) = True Then
                        value += Math.Pow(2, i)
                    End If
                Next
            End With


            Dim _tempele As Element = CType(WorkSpace.SelectedNode.Tag, Element)
            If CheckNewFile(_tempele) = True Then
                Select Case _tempele.GetTypeV
                    Case ElementType.RawTriggers
                        _tempele.AddElements(0, ElementType.RawTrigger)
                        _tempele.GetElementList.First.SetValue({value})
                        WorkSpace.SelectedNode.Nodes.Insert(0, _tempele.GetElementList.First.ToTreeNode)


                        'WorkSpace.SelectedNode.FirstNode.Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.GetElements(0), WorkSpace.SelectedNode.Nodes(0))
                        UndoRedoBtnRefresh()
                    Case ElementType.RawTrigger
                        Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                        _tempele.Parrent.AddElements(_index, ElementType.RawTrigger)
                        _tempele.Parrent.GetElementList(_index).SetValue({value})
                        WorkSpace.SelectedNode.Parent.Nodes.Insert(_index, _tempele.Parrent.GetElementList(_index).ToTreeNode)


                        'WorkSpace.SelectedNode.Parent.Expand()
                        'WorkSpace.SelectedNode.Parent.Nodes(_index).Expand()

                        TaskManager.AddTask(CTaskManager.Trigtask.Tasktype.create, _tempele.Parrent.GetElements(_index), WorkSpace.SelectedNode.NextNode)
                        UndoRedoBtnRefresh()
                End Select
            End If
        End If

        RedrawCode()
    End Sub







    Private Sub 조건ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 조건ToolStripMenuItem.Click
        Dim _tempele As Element = CType(WorkSpace.SelectedNode.Tag, Element)
        CondicitonFormShow(True, _tempele)

    End Sub

    Private Sub 액션ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 액션ToolStripMenuItem.Click
        '액션 새로 넣기
        Dim _tempele As Element = CType(WorkSpace.SelectedNode.Tag, Element)
        ActionFormShow(True, _tempele)

    End Sub



    Private Sub 수정ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 수정ToolStripMenuItem.Click
        Edit()
    End Sub

    Private Sub Edit()
        'ProjectSet.LoadCHKdata()
        Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)

        If _selectElement.Parrent.GetTypeV = ElementType.인수 Then
            CreateValForm.NumericUpDown1.Visible = False
            CreateValForm.EasyCompletionComboBox1.Visible = True
            CreateValForm.Label2.Text = "인수형식"

            CreateValForm.EasyCompletionComboBox1.Items.Clear()
            For i = 0 To ValueDefiniction.Count - 1
                CreateValForm.EasyCompletionComboBox1.Items.Add(ValueDefiniction(i).Name(0))
            Next
            CreateValForm.EasyCompletionComboBox1.SelectedIndex = _selectElement.Values(1)

            CreateValForm.TextBox1.Text = _selectElement.Values(0)
            CreateValForm.NumericUpDown1.Value = 0
            If CreateValForm.ShowDialog = DialogResult.OK Then
                _selectElement.SetValue({CreateValForm.TextBox1.Text, CreateValForm.EasyCompletionComboBox1.SelectedIndex})
                WorkSpace.SelectedNode.Text = _selectElement.GetText
            End If
        Else
            Select Case _selectElement.GetTypeV
                Case ElementType.Switch
                    AddSwitch(False)
                Case ElementType.Switchcase
                    AddCase(False)
                Case ElementType.조건
                    CondicitonFormShow(False, _selectElement)
                Case ElementType.액션
                    ActionFormShow(False, _selectElement)
                Case ElementType.포
                    ForEditingShow(False, _selectElement)
                Case ElementType.함수
                    My.Forms.FunctionForm.FunEle = _selectElement.Clone(Nothing)
                    My.Forms.FunctionForm._varele = _selectElement
                    My.Forms.FunctionForm.isNew = False
                    If My.Forms.FunctionForm.ShowDialog = DialogResult.OK Then
                        _selectElement.SetValue(My.Forms.FunctionForm.FunEle.Values.ToArray)

                        WorkSpace.SelectedNode.Text = _selectElement.GetText
                    End If
                Case ElementType.함수정의
                    FuctionNameForm.TextBox1.Text = _selectElement.Values(0)
                    FuctionNameForm.CheckBox1.Checked = _selectElement.Values(1)
                    If FuctionNameForm.ShowDialog = DialogResult.OK Then

                        _selectElement.SetValue({FuctionNameForm.TextBox1.Text, FuctionNameForm.CheckBox1.Checked})


                        WorkSpace.SelectedNode.Text = _selectElement.GetText
                    End If
                Case ElementType.와일조건, ElementType.조건절
                    If _selectElement.Values(0) = "And" Then
                        _selectElement.Values(0) = "Or"
                    Else
                        _selectElement.Values(0) = "And"
                    End If
                    WorkSpace.SelectedNode.Text = _selectElement.GetText
                Case ElementType.Wait
                    AddWait(False)
                Case ElementType.Foluder
                    AddFolduer(False)
                Case ElementType.RawTrigger
                    Dim value As Integer = _selectElement.Values(0)
                    With TriggerPlayerSelectDialog.CheckedListBox1
                        For i = 0 To .Items.Count - 1
                            If (value And Math.Pow(2, i)) > 0 Then
                                .SetItemChecked(i, True)
                            Else
                                .SetItemChecked(i, False)
                            End If
                        Next
                    End With
                    If TriggerPlayerSelectDialog.ShowDialog = DialogResult.OK Then
                        value = 0
                        With TriggerPlayerSelectDialog.CheckedListBox1
                            For i = 0 To .Items.Count - 1
                                If .GetItemChecked(i) = True Then
                                    value += Math.Pow(2, i)
                                End If
                            Next
                        End With
                        _selectElement.SetValue({value})


                        WorkSpace.SelectedNode.Text = _selectElement.GetText
                    End If
            End Select
        End If

        RedrawCode()
    End Sub









    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        Dim filename As String = ProjectSet.filename.Split("\").Last

        SaveFileDialog1.FileName = filename.Remove(InStr(filename, ".") - 1)
        Dim dialog As DialogResult = SaveFileDialog1.ShowDialog

        If dialog = DialogResult.OK Then
            Dim _filesteram As New FileStream(SaveFileDialog1.FileName, FileMode.Create)
            Dim _streamWriter As New StreamWriter(_filesteram)


            _streamWriter.Write(SaveTrigger)



            _streamWriter.Close()
            _filesteram.Close()
        End If
        'Dim ise2s As Boolean = False
        'Try
        '    If Mid(ProjectSet.filename, ProjectSet.filename.Length - 3) <> ".e2s" Then
        '        ise2s = True
        '    End If
        'Catch ex As Exception

        'End Try


        'If ProjectSet.filename = "" Or ise2s Then
        '    Dim Dialog As DialogResult


        '    If ProjectSet.filename = "" Then
        '        SaveFileDialog1.FileName = "제목 없음"
        '    Else
        '        SaveFileDialog1.FileName = Mid(ProjectSet.filename, 1, ProjectSet.filename.Length - 4)
        '    End If


        '    Dialog = SaveFileDialog1.ShowDialog()
        '    If Dialog = DialogResult.OK Then
        '        Dim _filesteram As New FileStream(SaveFileDialog1.FileName, FileMode.Create)
        '        Dim _streamWriter As New StreamWriter(_filesteram)


        '        _streamWriter.Write(SaveTrigger)



        '        _streamWriter.Close()
        '        _filesteram.Close()
        '    End If
        'End If


        ''Dim filename As String = ProjectSet.filename.Split("\").Last

        ''SaveFileDialog1.FileName = filename.Remove(InStr(filename, ".") - 1)
        ''Dim dialog As DialogResult = SaveFileDialog1.ShowDialog

        ''If dialog = DialogResult.OK Then
        ''    Dim _filesteram As New FileStream(SaveFileDialog1.FileName, FileMode.Create)
        ''    Dim _streamWriter As New StreamWriter(_filesteram)


        ''    _streamWriter.Write(SaveTrigger)



        ''    _streamWriter.Close()
        ''    _filesteram.Close()
        ''End If

        RedrawCode()
    End Sub

    Private Sub btn_OpenFile_Click(sender As Object, e As EventArgs) Handles btn_OpenFile.Click
        Dim dialog As DialogResult = OpenFileDialog1.ShowDialog

        If dialog = DialogResult.OK Then
            Dim _filesteram As New FileStream(OpenFileDialog1.FileName, FileMode.Open)
            Dim _streamReader As New StreamReader(_filesteram)


            LoadTriggerFile(_streamReader.ReadToEnd())



            _streamReader.Close()
            _filesteram.Close()

            ReDrawList()
        End If

        UndoRedoBtnRefresh()
        ReDrawList()
        RedrawCode()
    End Sub


    Private Sub 열기OToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 열기OToolStripMenuItem.Click
        Dim dialog As DialogResult = OpenFileDialog1.ShowDialog

        If dialog = DialogResult.OK Then
            Dim _filesteram As New FileStream(OpenFileDialog1.FileName, FileMode.Open)
            Dim _streamReader As New StreamReader(_filesteram)


            LoadTriggerFile(_streamReader.ReadToEnd())



            _streamReader.Close()
            _filesteram.Close()

            ReDrawList()
        End If

        UndoRedoBtnRefresh()
        ReDrawList()
        RedrawCode()
    End Sub

    Private Sub 파일로저장AToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 파일로저장AToolStripMenuItem.Click
        Dim filename As String = ProjectSet.filename.Split("\").Last

        SaveFileDialog1.FileName = filename.Remove(InStr(filename, ".") - 1)
        Dim dialog As DialogResult = SaveFileDialog1.ShowDialog

        If dialog = DialogResult.OK Then
            Dim _filesteram As New FileStream(SaveFileDialog1.FileName, FileMode.Create)
            Dim _streamWriter As New StreamWriter(_filesteram)


            _streamWriter.Write(SaveTrigger)



            _streamWriter.Close()
            _filesteram.Close()
        End If

        RedrawCode()
    End Sub



    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Main.저장()
    End Sub

    Private Sub 프로젝트저장ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 프로젝트저장ToolStripMenuItem.Click
        Main.저장()
    End Sub


    Private Sub 새로만들기NToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 새로만들기NToolStripMenuItem.Click
        If MsgBox(Lan.GetMsgText("SaveWaring"), MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            NewTriggerFile()
            UndoRedoBtnRefresh()
            ReDrawTriggerList()
            ReDrawList()
            RedrawCode()
        End If
    End Sub

    Private Sub btn_NewFile_Click(sender As Object, e As EventArgs) Handles btn_NewFile.Click
        If MsgBox(Lan.GetMsgText("SaveWaring"), MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            NewTriggerFile()
            UndoRedoBtnRefresh()
            ReDrawTriggerList()
            ReDrawList()
            RedrawCode()
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        LoadTILEDATA(False, True)


        For i = 0 To DebugDic.Count - 1
            Try
                DebugDic.Values(i).ReDrawColor()
            Catch ex As Exception

            End Try
        Next


        eudplib.Toflie(False, True)
        ErrorDialog.Show()
    End Sub

    Private Sub globalvarRefresh()
        ListBox1.Items.Clear()
        For i = 0 To GlobalVar.GetElementsCount - 1
            ListBox1.Items.Add(GlobalVar.GetElementList(i).Values(0))
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'DeclareVariable
        Dim _index As Integer

        CreateValForm.NumericUpDown1.Visible = True
        My.Forms.CreateValForm.EasyCompletionComboBox1.Visible = False
        CreateValForm.Label2.Text = "초기값"

        CreateValForm.TextBox1.Text = ""
        CreateValForm.NumericUpDown1.Value = 0
        CreateValForm.CheckBox1.Checked = False
        CreateValForm.CheckBox1.Enabled = True
        If CreateValForm.ShowDialog = DialogResult.OK Then
            Dim valuename As String
            If CreateValForm.CheckBox1.Checked = True Then
                valuename = "CreatePlayerVariable"
            Else
                valuename = "CreateVariable"
            End If
            For i = 0 To Actions.Count - 1
                If Actions(i).Name = valuename Then
                    _index = i
                End If
            Next


            GlobalVar.AddElements(New Element(GlobalVar, ElementType.액션, _index, {CreateValForm.TextBox1.Text, CreateValForm.NumericUpDown1.Value}))
            ListBox1.Items.Add(CreateValForm.TextBox1.Text)
        End If

        RedrawCode()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ListBox1.SelectedIndex <> -1 Then
            CreateValForm.NumericUpDown1.Visible = True
            My.Forms.CreateValForm.EasyCompletionComboBox1.Visible = False
            CreateValForm.Label2.Text = "초기값"

            CreateValForm.TextBox1.Text = GlobalVar.GetElementList(ListBox1.SelectedIndex).Values(0)
            CreateValForm.NumericUpDown1.Value = GlobalVar.GetElementList(ListBox1.SelectedIndex).Values(1)

            If GlobalVar.GetElementList(ListBox1.SelectedIndex).act.Name = "CreateVariable" Then
                CreateValForm.CheckBox1.Checked = False
            Else
                CreateValForm.CheckBox1.Checked = True
            End If
            CreateValForm.CheckBox1.Enabled = False

            If CreateValForm.ShowDialog = DialogResult.OK Then
                GlobalVar.GetElementList(ListBox1.SelectedIndex).SetValue({CreateValForm.TextBox1.Text, CreateValForm.NumericUpDown1.Value})
                ListBox1.Items(ListBox1.SelectedIndex) = CreateValForm.TextBox1.Text
            End If
        End If

        RedrawCode()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ListBox1.SelectedIndex <> -1 Then
            GlobalVar.GetElementList.RemoveAt(ListBox1.SelectedIndex)

            Dim lastSelect As Integer = ListBox1.SelectedIndex
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            Try
                ListBox1.SelectedIndex = lastSelect
            Catch ex As Exception
                ListBox1.SelectedIndex = ListBox1.Items.Count - 1
            End Try
        End If
        RedrawCode()
    End Sub

    Private Sub RedrawCode()
        If SplitContainer1.Width < SplitContainer1.SplitterDistance + 40 Then
        Else
            Dim _val As Integer = FastColoredTextBox1.VerticalScroll.Value

            FastColoredTextBox1.Text = TriggerToEPS()

            FastColoredTextBox1.VerticalScroll.Value = _val
            FastColoredTextBox1.UpdateScrollbars()
        End If
    End Sub


    Private Sub 함수ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 함수ToolStripMenuItem.Click
        Func()
    End Sub

    Private Sub 함수정의ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 함수정의ToolStripMenuItem.Click
        FuncNew()
    End Sub

    Private Sub 인수ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 인수ToolStripMenuItem.Click
        Factor()
    End Sub

    Private Sub 함수저장ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 함수저장ToolStripMenuItem.Click
        FuncSave()
    End Sub
    Private Sub FuncSave()
        Dim _Selectelement As Element = CType(WorkSpace.SelectedNode.Tag, Element)
        SaveFileDialog2.FileName = _Selectelement.Values(0)

        Dim dialog As DialogResult = SaveFileDialog2.ShowDialog()
        If dialog = DialogResult.OK Then
            Dim _filestream As New FileStream(SaveFileDialog2.FileName, FileMode.Create)
            Dim _streamwriter As New StreamWriter(_filestream)



            _streamwriter.Write(_Selectelement.ToSaveFile)

            _streamwriter.Close()
            _filestream.Close()
        End If

        RedrawCode()
    End Sub


    Private Sub 함수불러오기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 함수불러오기ToolStripMenuItem.Click
        FuncLoad()
    End Sub

    Private Sub formaker()
        Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)
        ForEditingShow(True, _selectElement)
    End Sub







    Private Sub 조건Btn_Click(sender As Object, e As EventArgs) Handles 조건Btn.Click
        Dim _tempele As Element = CType(WorkSpace.SelectedNode.Tag, Element)
        CondicitonFormShow(True, _tempele)
        WorkSpace.Focus()
    End Sub

    Private Sub 액션Btn_Click(sender As Object, e As EventArgs) Handles 액션Btn.Click
        Dim _tempele As Element = CType(WorkSpace.SelectedNode.Tag, Element)
        ActionFormShow(True, _tempele)
        WorkSpace.Focus()
    End Sub

    Private Sub 함수Btn_Click(sender As Object, e As EventArgs) Handles 함수Btn.Click
        Func()
        WorkSpace.Focus()
    End Sub

    Private Sub IfBtn_Click(sender As Object, e As EventArgs) Handles IfBtn.Click
        NewEle(WorkSpace.SelectedNode, ElementType.조건문if)
        WorkSpace.Focus()
    End Sub

    Private Sub IfElseBtn_Click(sender As Object, e As EventArgs) Handles IfElseBtn.Click
        NewEle(WorkSpace.SelectedNode, ElementType.조건문ifelse)
        WorkSpace.Focus()
    End Sub

    Private Sub SwitchBtn_Click(sender As Object, e As EventArgs) Handles Button7.Click
        AddSwitch(True)
        WorkSpace.Focus()
    End Sub

    Private Sub CaseBtn_Click(sender As Object, e As EventArgs) Handles Button20.Click
        AddCase(True)
        WorkSpace.Focus()
    End Sub

    Private Sub ForBtn_Click(sender As Object, e As EventArgs) Handles ForBtn.Click
        formaker()
        WorkSpace.Focus()
    End Sub

    Private Sub WhileBtn_Click(sender As Object, e As EventArgs) Handles WhileBtn.Click
        NewEle(WorkSpace.SelectedNode, ElementType.와일)
        WorkSpace.Focus()
    End Sub

    Private Sub 함수정의Btn_Click(sender As Object, e As EventArgs) Handles 함수정의Btn.Click
        FuncNew()
        WorkSpace.Focus()
    End Sub

    Private Sub 인수Btn_Click(sender As Object, e As EventArgs) Handles 인수Btn.Click
        Factor()
        WorkSpace.Focus()
    End Sub

    Private Sub 함수저장Btn_Click(sender As Object, e As EventArgs) Handles 함수저장Btn.Click
        FuncSave()
        WorkSpace.Focus()
    End Sub

    Private Sub 함수불러오기Btn_Click(sender As Object, e As EventArgs) Handles 함수불러오기Btn.Click
        FuncLoad()
        WorkSpace.Focus()
    End Sub

    Private Sub 대기하기Btn_Click(sender As Object, e As EventArgs) Handles 대기하기Btn.Click
        AddWait(True)
        WorkSpace.Focus()
    End Sub




    Private Sub 대기하기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 대기하기ToolStripMenuItem.Click
        AddWait(True)
    End Sub
    Private Sub AddWait(isnew As Boolean)
        If isnew Then
            My.Forms.WaitDailog.NumericUpDown1.Value = 0
            If WaitDailog.ShowDialog = DialogResult.OK Then
                NewEle(WorkSpace.SelectedNode, New Element(Nothing, ElementType.Wait, {WaitDailog.NumericUpDown1.Value}))
            End If
        Else
            Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)
            My.Forms.WaitDailog.NumericUpDown1.Value = _selectElement.Values(0)
            If WaitDailog.ShowDialog = DialogResult.OK Then
                _selectElement.SetValue({WaitDailog.NumericUpDown1.Value})


                WorkSpace.SelectedNode.Text = _selectElement.GetText
            End If
        End If

        RedrawCode()
    End Sub

    Private Sub AddSwitch(isnew As Boolean)
        SwitchDialog._varele = CType(WorkSpace.SelectedNode.Tag, Element)
        If isnew Then
            My.Forms.SwitchDialog.TextBox1.Text = ""
            If SwitchDialog.ShowDialog = DialogResult.OK Then
                NewEle(WorkSpace.SelectedNode, New Element(Nothing, ElementType.Switch, {SwitchDialog.TextBox1.Text}))
            End If
        Else
            Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)
            My.Forms.SwitchDialog.TextBox1.Text = _selectElement.Values(0)
            If SwitchDialog.ShowDialog = DialogResult.OK Then
                _selectElement.SetValue({SwitchDialog.TextBox1.Text})


                WorkSpace.SelectedNode.Text = _selectElement.GetText
            End If
        End If

        RedrawCode()
    End Sub


    Private Sub AddCase(isnew As Boolean)
        If isnew Then
            My.Forms.CaseDialog.NumericUpDown1.Value = 0
            My.Forms.CaseDialog.CheckBox1.Checked = True
            If CaseDialog.ShowDialog = DialogResult.OK Then
                NewEle(WorkSpace.SelectedNode, New Element(Nothing, ElementType.Switchcase, {CaseDialog.NumericUpDown1.Value, CaseDialog.CheckBox1.Checked}))
            End If
        Else
            Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)
            My.Forms.CaseDialog.NumericUpDown1.Value = _selectElement.Values(0)
            My.Forms.CaseDialog.CheckBox1.Checked = _selectElement.Values(1)
            If CaseDialog.ShowDialog = DialogResult.OK Then
                _selectElement.SetValue({CaseDialog.NumericUpDown1.Value, CaseDialog.CheckBox1.Checked})


                WorkSpace.SelectedNode.Text = _selectElement.GetText
            End If
        End If

        RedrawCode()
    End Sub


    Private Sub Btn_OpenCont_Click(sender As Object, e As EventArgs) Handles Btn_OpenCont.Click
        Dim dialog As DialogResult = OpenFileDialog1.ShowDialog

        If dialog = DialogResult.OK Then
            Dim _filesteram As New FileStream(OpenFileDialog1.FileName, FileMode.Open)
            Dim _streamReader As New StreamReader(_filesteram)


            LoadTriggerFileKeepFile(_streamReader.ReadToEnd())



            _streamReader.Close()
            _filesteram.Close()

            ReDrawList()
        End If

        UndoRedoBtnRefresh()
        RedrawCode()
    End Sub

    Private Sub OpenCont_Click(sender As Object, e As EventArgs) Handles OpenCont.Click
        Dim dialog As DialogResult = OpenFileDialog1.ShowDialog

        If dialog = DialogResult.OK Then
            Dim _filesteram As New FileStream(OpenFileDialog1.FileName, FileMode.Open)
            Dim _streamReader As New StreamReader(_filesteram)


            LoadTriggerFileKeepFile(_streamReader.ReadToEnd())



            _streamReader.Close()
            _filesteram.Close()

            ReDrawList()
        End If

        UndoRedoBtnRefresh()
        RedrawCode()
    End Sub


    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        TriggerNew()
        WorkSpace.Focus()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        TriggerNew()
        WorkSpace.Focus()
    End Sub


    Private Sub AddFolduer(isnew As Boolean)
        If isnew Then
            My.Forms.FoudlerNamedialog.TextBox1.Text = ""
            My.Forms.FoudlerNamedialog.TextBox2.Text = ""
            My.Forms.FoudlerNamedialog.CheckBox1.Checked = False


            If FoudlerNamedialog.ShowDialog = DialogResult.OK Then
                NewEle(WorkSpace.SelectedNode, New Element(Nothing, ElementType.Foluder, {FoudlerNamedialog.TextBox1.Text, FoudlerNamedialog.TextBox2.Text, FoudlerNamedialog.CheckBox1.Checked}))
            End If
        Else
            Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)
            My.Forms.FoudlerNamedialog.TextBox1.Text = _selectElement.Values(0)
            My.Forms.FoudlerNamedialog.TextBox2.Text = _selectElement.Values(1)
            My.Forms.FoudlerNamedialog.CheckBox1.Checked = _selectElement.Values(2)


            If FoudlerNamedialog.ShowDialog = DialogResult.OK Then
                _selectElement.SetValue({FoudlerNamedialog.TextBox1.Text, FoudlerNamedialog.TextBox2.Text, FoudlerNamedialog.CheckBox1.Checked})


                WorkSpace.SelectedNode.Text = _selectElement.GetText
            End If
        End If

        RedrawCode()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        AddFolduer(True)
        WorkSpace.Focus()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        AddFolduer(True)
        WorkSpace.Focus()
    End Sub

    Private Sub FoldMenuItem_Click(sender As Object, e As EventArgs) Handles FoldMenuItem.Click
        WorkSpace.BeginUpdate()
        WorkSpace.SelectedNode.Collapse()
        WorkSpace.EndUpdate()
    End Sub

    Private Sub UnFoldMenuItem_Click(sender As Object, e As EventArgs) Handles UnFoldMenuItem.Click
        WorkSpace.BeginUpdate()
        WorkSpace.SelectedNode.ExpandAll()
        WorkSpace.EndUpdate()
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        AddText.Values(0) = TextBox1.Text
        RedrawCode()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        SCDBForm.ShowDialog()
        CheckBox1.Checked = ProjectSet.SCDBUse
        Button8.Enabled = ProjectSet.SCDBUse
        RedrawCode()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            ProjectSet.SCDBUse = False
            Button8.Enabled = False
        Else
            ProjectSet.SCDBUse = True
            Button8.Enabled = True

            'If ProjectSet.SCDBUse = False And ProjectSet.scdbLoingStatus = False Then
            '    If SCDBLoginForm.ShowDialog() = DialogResult.Yes Then
            '        ProjectSet.SCDBUse = True
            '        Button8.Enabled = True
            '    Else
            '        CheckBox1.Checked = False
            '    End If
            'ElseIf ProjectSet.SCDBUse = False And ProjectSet.scdbLoingStatus = True Then
            '    ProjectSet.SCDBUse = True
            '    Button8.Enabled = True
            'End If
        End If
        RedrawCode()
    End Sub


    Dim playerlistload As Boolean = False
    Private Sub ReDrawPlayerList()
        playerlistload = True
        '마지막으로 클릭하고 있던 값을 기억
        Dim lastdata As SByte
        If ListBox2.SelectedIndex <> -1 Then
            lastdata = listboxdata(ListBox2.SelectedIndex)
        Else
            lastdata = -1
        End If

        ListBox2.Items.Clear()

        listboxdata.Clear()
        playerlist = New List(Of List(Of Integer))
        For i = 0 To 12
            playerlist.Add(New List(Of Integer))
        Next
        '플레이어별로 분배해야 되는데 임시로 이렇게 해두자.
        For i = 0 To RawTriggers.GetElementsCount - 1

            Dim playerflag As UInteger = RawTriggers.GetElements(i).Values(0)
            For j = 0 To 12
                If ((playerflag And Math.Pow(2, j)) > 0) Then
                    playerlist(j).Add(i)
                End If
            Next
        Next

        For i = 0 To 12
            If playerlist(i).Count <> 0 Then
                Select Case i
                    Case 0 To 7
                        ListBox2.Items.Add("Player " & i + 1)
                        listboxdata.Add(i)
                    Case 8 To 11
                        ListBox2.Items.Add("Force " & i - 7)
                        listboxdata.Add(i)
                    Case 12
                        ListBox2.Items.Add("AllPayers")
                        listboxdata.Add(i)
                End Select
            End If
        Next

        For i = 0 To listboxdata.Count - 1
            If listboxdata(i) = lastdata Then
                ListBox2.SelectedIndex = i
            End If
        Next

        playerlistload = False
        If ListBox2.SelectedIndex = -1 Then '플레이어가 아에 사라짐.
            If ListBox2.Items.Count <> 0 Then
                ListBox2.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub ReDrawPlayerTriggerList()
        ListControl1.Clear()
        Button10.Enabled = False
        Button11.Enabled = False
        Button12.Enabled = False
        Button13.Enabled = False
        Button15.Enabled = False

        If ListBox2.SelectedIndex <> -1 Then
            For i = 0 To playerlist(listboxdata(ListBox2.SelectedIndex)).Count - 1
                ListControl1.Add(RawTriggers.GetElements(playerlist(listboxdata(ListBox2.SelectedIndex))(i)))
            Next
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If playerlistload = False Then
            ReDrawPlayerTriggerList()
        End If
    End Sub

    Private Function GetSelectTrigger(Optional index As SByte = 0) As Element
        If ListControl1.SelectedIndex <> -1 Then
            Return RawTriggers.GetElements(playerlist(listboxdata(ListBox2.SelectedIndex))(ListControl1.SelectedIndex + index))
        Else
            Return Nothing
        End If
    End Function

    Private Sub ClassbuttonRefresh(Optional index As Integer = -1)
        If index = -1 Then
            index = ListControl1.SelectedIndex
        End If
        If index <> -1 Then
            Button10.Enabled = True
            Button11.Enabled = True
            Button12.Enabled = True

            If (index <> 0) Then
                Button13.Enabled = True
            Else
                Button13.Enabled = False
            End If
            If (index <> ListControl1.Count - 1) Then
                Button15.Enabled = True
            Else
                Button15.Enabled = False
            End If

        Else
            Button10.Enabled = False
            Button11.Enabled = False
            Button12.Enabled = False
            Button13.Enabled = False
            Button15.Enabled = False
        End If
    End Sub

    Private Sub ListControl1_ItemClick(sender As Object, index As Integer) Handles ListControl1.ItemClick
        ClassbuttonRefresh(index)
    End Sub
    Private Sub ListControl1_DoubleClick(sender As Object) Handles ListControl1.ItemDoubleClick
        TriggerForm.MainTrigger = RawTriggers.GetElements(playerlist(listboxdata(ListBox2.SelectedIndex))(ListControl1.SelectedIndex)).Clone
        If TriggerForm.ShowDialog() = DialogResult.OK Then
            RawTriggers.GetElements(playerlist(listboxdata(ListBox2.SelectedIndex))(ListControl1.SelectedIndex)).Copy(TriggerForm.MainTrigger)

            Dim playerflag As Integer = TriggerForm.MainTrigger.Values(0)

            If (playerflag And Math.Pow(2, listboxdata(ListBox2.SelectedIndex))) = 0 Then
                ListControl1.Remove(ListControl1.SelectedIndex)
            End If

            ListControl1.Refresh()
            ClassbuttonRefresh()
            ReDrawPlayerList()
        End If
    End Sub


    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean
        If TabControl1.SelectedIndex = 0 Then
            Select Case keyData
                Case Keys.N
                    If Button9.Enabled = True Then
                        _NewTrigger()
                    End If
                Case Keys.M
                    If Button10.Enabled = True Then
                        _Modify()
                    End If
                Case Keys.D
                    If Button11.Enabled = True Then
                        _Delete()
                    End If
                Case Keys.C
                    If Button12.Enabled = True Then
                        _Copy()
                    End If
                Case Keys.U
                    If Button13.Enabled = True Then
                        _MoveUp()
                    End If
                Case Keys.O
                    If Button15.Enabled = True Then
                        _MoveDown()
                    End If
                Case Keys.F
                    FuncManagerDialog.ShowDialog()
            End Select
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function


    Private Sub NewTriggerbtn(sender As Object, e As EventArgs) Handles Button9.Click
        _NewTrigger()
    End Sub

    Private Sub Modifybtn(sender As Object, e As EventArgs) Handles Button10.Click
        _Modify()
    End Sub

    Private Sub DeleteBtn(sender As Object, e As EventArgs) Handles Button11.Click
        _Delete()
    End Sub

    Private Sub CopyBtn(sender As Object, e As EventArgs) Handles Button12.Click
        _Copy()
    End Sub

    Private Sub MoveUpBtn(sender As Object, e As EventArgs) Handles Button13.Click
        _MoveUp()
    End Sub

    Private Sub MoveDownBtn(sender As Object, e As EventArgs) Handles Button15.Click
        _MoveDown()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        FuncManagerDialog.ShowDialog()
    End Sub


    Private Sub _NewTrigger()
        'If ListControl1.SelectedIndex = -1 Then
        '    ListControl1.SelectedIndex = 0
        'End If
        TriggerForm.MainTrigger = New Element(RawTriggers, ElementType.RawTrigger)
        TriggerForm.MainTrigger.SetValue({0})
        If TriggerForm.ShowDialog() = DialogResult.OK Then


            Dim orgele As Element = TriggerForm.MainTrigger.Clone(RawTriggers)
            Dim orgparrent As Element = RawTriggers
            Dim index As Integer
            If GetSelectTrigger() Is Nothing Then
                index = 0
            Else
                index = GetSelectTrigger.Getindex()
            End If

            orgparrent.AddElements(index, orgele)


            '만약 해당 화면에 추가되었다면
            Dim playerflag As Integer = TriggerForm.MainTrigger.Values(0)

            If ListBox2.SelectedIndex <> -1 Then
                If (playerflag And Math.Pow(2, listboxdata(ListBox2.SelectedIndex))) > 0 Then
                    If ListControl1.SelectedIndex <> -1 Then
                        Dim lastindex As Integer = ListControl1.SelectedIndex

                        ListControl1.Add(ListControl1.Items(ListControl1.Count - 1).Trigger)
                        For i = ListControl1.Count - 1 To lastindex + 1 Step -1
                            ListControl1.Items(i).Trigger = ListControl1.Items(i - 1).Trigger
                        Next

                        ListControl1.Items(lastindex).Trigger = orgparrent.GetElements(index)
                    Else
                        If playerflag <> 0 Then
                            ListControl1.Add(Nothing)
                            For i = ListControl1.Count - 1 To 0 + 1 Step -1
                                ListControl1.Items(i).Trigger = ListControl1.Items(i - 1).Trigger
                            Next

                            ListControl1.Items(0).Trigger = orgparrent.GetElements(index)
                        End If
                    End If
                End If
            Else
                If playerflag <> 0 Then
                    ListControl1.Add(orgparrent.GetElements(index))
                End If
            End If




            ListControl1.Refresh()
            ReDrawPlayerList()
        End If
        ClassbuttonRefresh()
    End Sub

    Private Sub _Modify()
        TriggerForm.MainTrigger = RawTriggers.GetElements(playerlist(listboxdata(ListBox2.SelectedIndex))(ListControl1.SelectedIndex)).Clone
        If TriggerForm.ShowDialog() = DialogResult.OK Then
            RawTriggers.GetElements(playerlist(listboxdata(ListBox2.SelectedIndex))(ListControl1.SelectedIndex)).Copy(TriggerForm.MainTrigger)

            Dim playerflag As Integer = TriggerForm.MainTrigger.Values(0)

            If (playerflag And Math.Pow(2, listboxdata(ListBox2.SelectedIndex))) = 0 Then
                ListControl1.Remove(ListControl1.SelectedIndex)
            End If

            ListControl1.Refresh()
            ClassbuttonRefresh()
            ReDrawPlayerList()
        End If
    End Sub


    Private Sub _Delete()
        Dim lastindex As Integer = ListControl1.SelectedIndex
        GetSelectTrigger.Delete()
        ListControl1.Remove(lastindex)
        If ListControl1.Count - 1 >= lastindex Then
            ListControl1.SelectedIndex = lastindex
        Else
            ListControl1.SelectedIndex = ListControl1.Count - 1
            If ListControl1.Count = 0 Then
                Button10.Enabled = False
                Button11.Enabled = False
                Button12.Enabled = False
                Button13.Enabled = False
                Button15.Enabled = False
            End If
        End If
        ReDrawPlayerList()
    End Sub

    Private Sub _Copy()
        Dim orgele As Element = GetSelectTrigger.Clone(GetSelectTrigger.Parrent)
        Dim orgparrent As Element = GetSelectTrigger.Parrent
        Dim index As Integer = GetSelectTrigger.Getindex()
        orgparrent.AddElements(index, orgele)

        Dim lastindex As Integer = ListControl1.SelectedIndex

        ListControl1.Add(ListControl1.Items(ListControl1.Count - 1).Trigger)
        For i = ListControl1.Count - 1 To lastindex + 1 Step -1
            ListControl1.Items(i).Trigger = ListControl1.Items(i - 1).Trigger
        Next

        ListControl1.Items(lastindex).Trigger = orgparrent.GetElementList(index) 'orgele


        ListControl1.SelectedIndex = lastindex + 1

        ReDrawPlayerList()
    End Sub

    Private Sub _MoveUp()
        Dim orgele As Element = GetSelectTrigger.Clone(GetSelectTrigger.Parrent)
        Dim orgparrent As Element = GetSelectTrigger.Parrent

        Dim index As Integer = GetSelectTrigger.Getindex()

        GetSelectTrigger.Delete()
        orgparrent.AddElements(index - 1, orgele)

        If (ListControl1.SelectedIndex > 1) Then
            Button13.Enabled = True
        Else
            Button13.Enabled = False
        End If
        Button15.Enabled = True


        Dim lastindex As Integer = ListControl1.SelectedIndex
        Dim tempele As Element = ListControl1.Items(lastindex - 1).Trigger
        ListControl1.Items(lastindex - 1).Trigger = orgparrent.GetElements(index - 1)
        ListControl1.Items(lastindex).Trigger = tempele


        ListControl1.SelectedIndex = lastindex - 1
    End Sub

    Private Sub _MoveDown()
        Dim orgele As Element = GetSelectTrigger.Clone(GetSelectTrigger.Parrent)
        Dim orgparrent As Element = GetSelectTrigger.Parrent

        Dim index As Integer = GetSelectTrigger.Getindex()

        GetSelectTrigger.Delete()
        orgparrent.AddElements(index + 1, orgele)

        Button13.Enabled = True
        If (ListControl1.SelectedIndex < ListControl1.Count - 2) Then
            Button15.Enabled = True
        Else
            Button15.Enabled = False
        End If

        Dim lastindex As Integer = ListControl1.SelectedIndex
        Dim tempele As Element = ListControl1.Items(lastindex + 1).Trigger
        ListControl1.Items(lastindex + 1).Trigger = orgparrent.GetElements(index + 1)
        ListControl1.Items(lastindex).Trigger = tempele


        ListControl1.SelectedIndex = lastindex + 1
    End Sub





    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        If ListBox1.SelectedIndex <> -1 Then
            CreateValForm.NumericUpDown1.Visible = True
            My.Forms.CreateValForm.EasyCompletionComboBox1.Visible = False
            CreateValForm.Label2.Text = "초기값"

            CreateValForm.TextBox1.Text = GlobalVar.GetElementList(ListBox1.SelectedIndex).Values(0)
            CreateValForm.NumericUpDown1.Value = GlobalVar.GetElementList(ListBox1.SelectedIndex).Values(1)

            If GlobalVar.GetElementList(ListBox1.SelectedIndex).act.Name = "CreateVariable" Then
                CreateValForm.CheckBox1.Checked = False
            Else
                CreateValForm.CheckBox1.Checked = True
            End If
            CreateValForm.CheckBox1.Enabled = False

            If CreateValForm.ShowDialog = DialogResult.OK Then
                GlobalVar.GetElementList(ListBox1.SelectedIndex).SetValue({CreateValForm.TextBox1.Text, CreateValForm.NumericUpDown1.Value})
                ListBox1.Items(ListBox1.SelectedIndex) = CreateValForm.TextBox1.Text
            End If
        End If

        RedrawCode()
    End Sub

    Private Sub ListBox1_KeyPress(sender As Object, e As KeyEventArgs) Handles ListBox1.KeyDown
        If e.KeyCode = Keys.Delete Then
            If ListBox1.SelectedIndex <> -1 Then
                GlobalVar.GetElementList.RemoveAt(ListBox1.SelectedIndex)

                Dim lastSelect As Integer = ListBox1.SelectedIndex
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
                Try
                    ListBox1.SelectedIndex = lastSelect
                Catch ex As Exception
                    ListBox1.SelectedIndex = ListBox1.Items.Count - 1
                End Try
            End If
        End If
        RedrawCode()
    End Sub

    Private Sub Undobtn(sender As Object, e As EventArgs) Handles Button18.Click
        Undo()
    End Sub

    Private Sub Redobtn(sender As Object, e As EventArgs) Handles Button19.Click
        Redo()
    End Sub

    Private Sub Undo()
        TaskManager.Undo()
        UndoRedoBtnRefresh()
        'ReDrawTriggerList()
        'ReDrawList()
        RedrawCode()
    End Sub

    Private Sub Redo()
        TaskManager.Redo()
        UndoRedoBtnRefresh()
        'ReDrawTriggerList()
        'ReDrawList()
        RedrawCode()
    End Sub

    Private Sub 실행취소ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 실행취소ToolStripMenuItem.Click
        Undo()
    End Sub

    Private Sub 다시실행ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 다시실행ToolStripMenuItem.Click
        Redo()
    End Sub

    Private Sub 모두접기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 모두접기ToolStripMenuItem.Click
        WorkSpace.BeginUpdate()
        WorkSpace.CollapseAll()
        WorkSpace.EndUpdate()
        WorkSpace.SelectedNode = WorkSpace.Nodes(0)
    End Sub

    Private Sub 모두펼치기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 모두펼치기ToolStripMenuItem.Click
        WorkSpace.BeginUpdate()
        WorkSpace.ExpandAll()
        WorkSpace.EndUpdate()
    End Sub

    Private Sub 편집ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 편집ToolStripMenuItem.DropDownOpened
        실행취소ToolStripMenuItem.Enabled = Button18.Enabled
        다시실행ToolStripMenuItem.Enabled = Button19.Enabled
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        StringAnimaterForm.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Try
            Dim _selectElement As Element = CType(WorkSpace.SelectedNode.Tag, Element)
            My.Computer.Clipboard.SetText(_selectElement.ToCode(0, True))
        Catch ex As Exception

        End Try
    End Sub
End Class