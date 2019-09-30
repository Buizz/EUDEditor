Imports System.IO
Imports Newtonsoft.Json


'To serialize MyPersonClass using XML serialisation, you will need instances of XmlSerializer And StreamWriter (in System.IO):

'XmlSerializer serializer = New XmlSerializer(TypeOf (MyPersonClass));
'StreamWriter xmlFile = New StreamWriter(@"InsertFileName");
'serializer.Serialize(xmlFile, classInstance);
'xmlFile.Close();

Module TriggerEditorDataMoudle
    Public DebugDic As New Dictionary(Of Integer, Element)



    Public Class CTaskManager
        '노드도 기억해야함. 노드는 좌표로 기억할까

        Public Class Trigtask
            Private pManager As CTaskManager


            Private nodePos As New List(Of Integer)


            Private lastpos As Integer
            Private iscreate As Tasktype
            Public Enum Tasktype
                create
                delete
                edit
            End Enum


            Private parentele As Element
            Private parentnode As TreeNode
            Private parentNodeindex As Integer

            Private ele As Element
            Private node As TreeNode

            Public Sub New(_iscreate As Tasktype, _ele As Element, _node As TreeNode, _lastpos As Integer, _pManager As CTaskManager)
                iscreate = _iscreate
                ele = _ele
                node = _node
                pManager = _pManager
                lastpos = _lastpos
                Dim temp_node As TreeNode = node
                While temp_node.Parent IsNot Nothing
                    nodePos.Add(temp_node.Index)
                    temp_node = temp_node.Parent
                End While
                parentnode = temp_node
                parentNodeindex = temp_node.Index

                parentele = CType(parentnode.Tag, Element)
            End Sub

            Public Sub UndoStart()
                parentnode = pManager.maintreeview.Nodes(parentNodeindex)

                Select Case iscreate
                    Case Tasktype.create
                        Dim temp_node As TreeNode = parentnode
                        Dim temp_ele As Element = parentele
                        For i = nodePos.Count - 1 To 0 Step -1
                            temp_node = temp_node.Nodes(nodePos(i))
                            temp_ele = temp_ele.GetElements(nodePos(i))
                        Next

                        temp_node.Nodes(lastpos).Remove()
                        temp_ele.RemoveAt(lastpos)
                    Case Tasktype.delete
                        Dim temp_node As TreeNode = parentnode
                        Dim temp_ele As Element = parentele
                        For i = nodePos.Count - 1 To 0 Step -1
                            temp_node = temp_node.Nodes(nodePos(i))
                            temp_ele = temp_ele.GetElements(nodePos(i))
                        Next

                        temp_ele.AddElements(lastpos, ele)
                        temp_node.Nodes.Insert(lastpos, temp_ele.GetElements(lastpos).ToTreeNode)
                        temp_node.Nodes(lastpos).Tag = temp_ele.GetElements(lastpos)


                    Case Tasktype.edit
                End Select
            End Sub

            Public Sub RedoStart()
                parentnode = pManager.maintreeview.Nodes(parentNodeindex)

                Select Case iscreate
                    Case Tasktype.create
                        Dim temp_node As TreeNode = parentnode
                        Dim temp_ele As Element = parentele
                        For i = nodePos.Count - 1 To 0 Step -1
                            temp_node = temp_node.Nodes(nodePos(i))
                            temp_ele = temp_ele.GetElements(nodePos(i))
                        Next

                        temp_ele.AddElements(lastpos, ele)
                        temp_node.Nodes.Insert(lastpos, temp_ele.GetElements(lastpos).ToTreeNode)
                        temp_node.Nodes(lastpos).Tag = temp_ele.GetElements(lastpos)
                    Case Tasktype.delete
                        Dim temp_node As TreeNode = parentnode
                        Dim temp_ele As Element = parentele
                        For i = nodePos.Count - 1 To 0 Step -1
                            temp_node = temp_node.Nodes(nodePos(i))
                            temp_ele = temp_ele.GetElements(nodePos(i))
                        Next

                        temp_node.Nodes(lastpos).Remove()
                        temp_ele.RemoveAt(lastpos)


                    Case Tasktype.edit
                End Select
            End Sub
        End Class




        Public maintreeview As TreeView

        Public trigtasklistPtr As Integer
        Public trigtasklist As List(Of Trigtask)


        Public Sub AddTask(_iscreate As Trigtask.Tasktype, _ele As Element, _node As TreeNode)
            trigtasklist.Add(New Trigtask(_iscreate, _ele.Clone, _node.Parent, _ele.Getindex, Me))
            trigtasklist.RemoveRange(trigtasklist.Count - 1 - trigtasklistPtr, trigtasklistPtr)

            trigtasklistPtr = 0
        End Sub

        Public Sub Undo()
            trigtasklist(trigtasklist.Count - 1 - trigtasklistPtr).UndoStart()
            trigtasklistPtr += 1
        End Sub
        Public Sub Redo()
            trigtasklist(trigtasklist.Count - trigtasklistPtr).RedoStart()
            trigtasklistPtr -= 1
        End Sub


        Public Sub New(_maintreeview As TreeView)
            trigtasklist = New List(Of Trigtask)
            trigtasklistPtr = 0
            maintreeview = _maintreeview
        End Sub

        Public Sub Clear()
            trigtasklist.Clear()
            trigtasklistPtr = 0
        End Sub

        Public Function Isundoable() As Boolean
            Dim index As Integer = trigtasklist.Count - 1 - trigtasklistPtr
            If (index >= 0) And (index <= trigtasklist.Count - 1) Then
                Return True
            Else
                Return False
            End If
        End Function
        Public Function Isredoable() As Boolean
            Dim index As Integer = trigtasklist.Count - trigtasklistPtr
            If (index >= 0) And (index <= trigtasklist.Count - 1) Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class

    Public TaskManager As CTaskManager = New CTaskManager(TrigEditorForm.WorkSpace)



    Public ClassicTriggerCounter As Integer

    Public Const PadWidth As Byte = 4
    Public Const Separater As String = "ஐ"

    Public WaitCounter As Integer
    Public VarialbeName As String


    Public GlobalVar As Element


    Public Actions As List(Of Action)
    Public Condictions As List(Of Condiction)


    Public AddText As Element
    Public functions As Element
    Public RawTriggers As Element
    Public StartElement As Element
    Public BeforeElement As Element
    Public AfterElement As Element
    Public ElementINDEX As List(Of Element)
    Public Tempindex_Element As UInteger

    Private Function GetIntend(count As Integer) As String
        Dim tabstring As String = ""
        For i = 0 To count * PadWidth - 1
            tabstring = tabstring & " "
        Next
        Return tabstring
    End Function

    Public Function GetFunc(Funcname As String) As Element
        If functions.GetElementsCount <> 0 Then
            For i = 0 To functions.GetElementsCount - 1
                If Funcname = functions.GetElementList(i).Values(0) Then
                    Return functions.GetElementList(i)
                End If
            Next
        End If

        Return Nothing
    End Function

    Public Function CheckFunc(Funcname As String) As Boolean
        If functions.GetElementsCount <> 0 Then
            For i = 0 To functions.GetElementsCount - 1
                If Funcname = functions.GetElementList(i).Values(0) Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function

    Public Function CheckFuncExist(Funcname As String) As Boolean
        Return My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\TEFunction\" & Funcname & ".tfn")
    End Function


    Public Sub FuncLoadFile(FuncName As String)
        For i = 0 To functions.GetElementsCount - 1
            If functions.GetElements(i).Values(0) = FuncName Then
                Exit Sub
            End If
        Next

        FuncName = My.Application.Info.DirectoryPath & "\TEFunction\" & FuncName & ".tfn"


        Dim _tempele As Element = functions
        Dim newElement As New Element(Nothing, ElementType.main)



        Dim _filestream As New FileStream(FuncName, FileMode.Open)
        Dim _streamreader As New StreamReader(_filestream)

        newElement.LoadFile(_streamreader.ReadToEnd(), 0)

        Select Case _tempele.GetTypeV
            Case ElementType.Functions
                _tempele.AddElements(0, newElement)
            Case ElementType.함수정의
                Dim _index As Integer = _tempele.Parrent.GetElementList().IndexOf(_tempele) + 1

                _tempele.Parrent.AddElements(_index, newElement)
        End Select
        _streamreader.Close()
        _filestream.Close()
    End Sub


    Public Sub FuncDeleteFile(FuncName As String)
        For i = 0 To functions.GetElementsCount - 1
            If functions.GetElements(i).Values(0) = FuncName Then


                functions.RemoveAt(i)
                Exit Sub
            End If
        Next
    End Sub

    Public Sub NewTriggerFile()
        AddText = New Element(GlobalVar, ElementType.RawString)
        GlobalVar = New Element(GlobalVar, ElementType.main)
        functions = New Element(functions, ElementType.Functions)
        RawTriggers = New Element(RawTriggers, ElementType.RawTriggers)
        StartElement = New Element(StartElement, ElementType.main)
        BeforeElement = New Element(BeforeElement, ElementType.main)
        AfterElement = New Element(AfterElement, ElementType.main)
        TaskManager.Clear()
    End Sub


    Private Function findSection(data As String, Key As String) As Integer
        Dim _temp() As String = data.Split(vbCrLf)

        For i = 0 To _temp.Count - 1
            If _temp(i).Trim = Key Then
                Return i + 1
            End If
        Next
        Return 0
    End Function

    Public Sub LoadTriggerFile(datas As String, Optional isfirst As Boolean = False)
        TaskManager.Clear()
        If datas = "" Then
            NewTriggerFile()
            Exit Sub
        End If

        Try
            Try
                AddText = New Element(AddText, ElementType.RawString)
            Catch ex As Exception

            End Try
            functions = New Element(functions, ElementType.Functions)
            GlobalVar = New Element(GlobalVar, ElementType.main)

            StartElement = New Element(StartElement, ElementType.main)
            BeforeElement = New Element(BeforeElement, ElementType.main)
            AfterElement = New Element(AfterElement, ElementType.main)

            Try
                AddText.LoadFile(datas, findSection(datas, "&AddText&"))
            Catch ex As Exception

            End Try
            functions.LoadFile(datas, findSection(datas, "&functions&"), isfirst)
            GlobalVar.LoadFile(datas, findSection(datas, "&GlobalVar&"))
            Try
                RawTriggers.LoadFile(datas, findSection(datas, "&RawTriggers&"))
            Catch ex As Exception
                RawTriggers = New Element(RawTriggers, ElementType.RawTriggers)
            End Try
            StartElement.LoadFile(datas, findSection(datas, "&onPluginStart&"))
            BeforeElement.LoadFile(datas, findSection(datas, "&beforeTriggerExec&"))
            AfterElement.LoadFile(datas, findSection(datas, "&afterTriggerExec&"))
        Catch ex As Exception
            MsgBox(Lan.GetText("Msgbox", "tfError"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
        End Try

    End Sub

    Public Sub LoadTriggerFileKeepFile(datas As String)
        TaskManager.Clear()
        If datas = "" Then
            Exit Sub
        End If

        Try
            Try
                AddText.LoadFile(datas, findSection(datas, "&AddText&"))
            Catch ex As Exception

            End Try
            functions.LoadFile(datas, findSection(datas, "&functions&"))
            GlobalVar.LoadFile(datas, findSection(datas, "&GlobalVar&"))
            Try
                RawTriggers.LoadFile(datas, findSection(datas, "&RawTriggers&"))
            Catch ex As Exception
                RawTriggers = New Element(RawTriggers, ElementType.RawTriggers)
            End Try

            StartElement.LoadFile(datas, findSection(datas, "&onPluginStart&"))
            BeforeElement.LoadFile(datas, findSection(datas, "&beforeTriggerExec&"))
            AfterElement.LoadFile(datas, findSection(datas, "&afterTriggerExec&"))
        Catch ex As Exception
            MsgBox(Lan.GetText("Msgbox", "tfError"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
        End Try


    End Sub

    Public Function GetWaitAbleTrigger() As String
        Dim strb As New System.Text.StringBuilder

        For i = 0 To functions.GetElementsCount - 1
            Dim name As String = functions.GetElementList(i).Values(0)
            Dim flag As Boolean = functions.GetElementList(i).Values(1)

            If flag Then
                Dim Factors As Element = functions.GetElementList(i).GetElementList(0)

                strb.Append(GetIntend(1) & name & "(")

                If Factors.GetElementsCount <> 0 Then
                    strb.Append(name & Factors.GetElementList(0).Values(0) & "[getcurpl()]")
                    For j = 1 To Factors.GetElementsCount - 1
                        strb.Append(", " & name & Factors.GetElementList(j).Values(0) & "[getcurpl()]")
                    Next
                End If



                strb.AppendLine(");")
            End If
        Next


        Return strb.ToString
    End Function


    Public Function GetClassicTrigger() As String
        Dim strb As New System.Text.StringBuilder

        Dim playerlist As New List(Of List(Of Integer))
        For i = 0 To 7
            playerlist.Add(New List(Of Integer))
        Next
        '플레이어별로 분배해야 되는데 임시로 이렇게 해두자.
        For i = 0 To RawTriggers.GetElementsCount - 1

            Dim playerflag As UInteger = RawTriggers.GetElements(i).Values(0)
            For j = 0 To 12
                If ((playerflag And Math.Pow(2, j)) > 0) Then
                    Select Case j
                        Case 0 To 7
                            playerlist(j).Add(i)
                        Case 8 To 11
                            If ProjectSet.CHKFORCEDATA(j - 8).Count > 1 Then
                                For k = 1 To ProjectSet.CHKFORCEDATA(j - 8).Count - 1
                                    If playerlist(ProjectSet.CHKFORCEDATA(j - 8)(k)).Contains(i) = False Then
                                        playerlist(ProjectSet.CHKFORCEDATA(j - 8)(k)).Add(i)
                                    End If
                                Next
                            End If
                        Case 12
                            For k = 0 To 7
                                If playerlist(k).Contains(i) = False Then
                                    playerlist(k).Add(i)
                                End If
                            Next
                    End Select
                End If
            Next
        Next

        For i = 0 To 7
            strb.AppendLine(GetIntend(1) & "//플레이어 " & i + 1)
            strb.AppendLine(GetIntend(1) & "if (playerexist(" & i & ")){")
            strb.AppendLine(GetIntend(2) & "setcurpl(" & i & ");")
            For k = 0 To playerlist(i).Count - 1
                If RawTriggers.GetElements(playerlist(i)(k)).isdisalbe = False Then
                    strb.AppendLine(GetIntend(2) & "ClassicTriggerStarter" & playerlist(i)(k) & "();")
                End If
            Next

            strb.AppendLine(GetIntend(1) & "}")
        Next



        Return strb.ToString
    End Function

    Public LineCount As ULong = 0
    Public Function TriggerToEPS(Optional isbulid As Boolean = False) As String
        LineCount = 0
        If isbulid = True Then
            TEErrorText = ""
            TEErrorText2 = ""
            ErrorDialog.Close()
            DebugDic.Clear()
        End If


        Dim strbulider As New System.Text.StringBuilder

        Dim str As String = ""
        If ProjectSet.UsedSetting(ProjectSet.Settingtype.BtnSet) Then
            strbulider.AppendLine("import BGMPlayer as BGM;")
        End If
        If ProjectSet.SCDBUse Then
            strbulider.AppendLine("import SCDB as scdb;")
        End If
        strbulider.AppendLine("import punitloop as lp;")
        strbulider.AppendLine("import tempcustomText as tct;")

        strbulider.AppendLine(vbCrLf & "var txtPtr;")
        strbulider.AppendLine("const trgk = $T('Artanis & safhfh');")

        LineCount = strbulider.ToString.Split(vbCrLf).Count
        'strbulider.Append(LineCount & " : ")
        strbulider.AppendLine(GlobalVar.ToCode(-1, isbulid))

        LineCount = strbulider.ToString.Split(vbCrLf).Count
        'strbulider.Append(LineCount & " : ")
        strbulider.AppendLine(AddText.ToCode(-1, isbulid))



        For Each funcs As Element In functions.GetElementList
            Dim arugments As String = ""

            If funcs.GetElementList(0).GetElementList.Count <> 0 Then
                arugments = funcs.GetElementList(0).GetElementList(0).Values(0)
                For i = 1 To funcs.GetElementList(0).GetElementList.Count - 1
                    arugments = arugments & ", " & funcs.GetElementList(0).GetElementList(i).Values(0)
                Next
            End If

            If funcs.GetTypeV = ElementType.함수정의 Then
                If funcs.Values(1) = True Then
                    WaitCounter = 1
                    For i = 0 To funcs.GetElements(0).GetElementsCount - 1
                        strbulider.AppendLine("const " & funcs.Values(0) & funcs.GetElements(0).GetElements(i).Values(0) & " = [0, 0, 0, 0, 0, 0, 0, 0];")
                    Next
                    VarialbeName = funcs.Values(0) & "Timer[getcurpl()]"
                    strbulider.AppendLine("const " & funcs.Values(0) & "Timer = [0, 0, 0, 0, 0, 0, 0, 0];")

                End If
            End If

            strbulider.AppendLine("function " & funcs.Values(0) & "(" & arugments & ");")
        Next



        LineCount = strbulider.ToString.Split(vbCrLf).Count
        'strbulider.Append(LineCount & " : ")
        strbulider.AppendLine(functions.ToCode(-1, isbulid))


        'ClassicTriggerExec에 들어갈 내용들을 적는 곳
        For i = 0 To RawTriggers.GetElementsCount - 1
            With RawTriggers.GetElements(i)
                VarialbeName = "ClassicTriggerExecTimer" & i & "[getcurpl()]"
                strbulider.AppendLine("const ClassicTriggerExecTimer" & i & " = [0, 0, 0, 0, 0, 0, 0, 0];")

                'strbulider.Append(LineCount & " : ")
                strbulider.AppendLine("function ClassicTriggerExec" & i & "() {")
                LineCount = strbulider.ToString.Split(vbCrLf).Count
                strbulider.AppendLine(.GetElements(1).ToCode(2, isbulid))
                strbulider.AppendLine("}")
            End With
        Next
        '================================================================


        'ClassicTriggerStarter에 들어갈 내용들을 적는 곳
        For i = 0 To RawTriggers.GetElementsCount - 1
            With RawTriggers.GetElements(i)
                strbulider.AppendLine("function ClassicTriggerStarter" & i & "() {")

                LineCount = strbulider.ToString.Split(vbCrLf).Count
                'strbulider.Append(LineCount & " : ")
                strbulider.Append(.GetElements(0).ToCode(1, isbulid))
                strbulider.AppendLine(GetIntend(1) & "){")

                strbulider.AppendLine(GetIntend(2) & "if (ClassicTriggerExecTimer" & i & "[getcurpl()] == 0){")
                strbulider.AppendLine(GetIntend(3) & "ClassicTriggerExecTimer" & i & "[getcurpl()] = 1;")
                strbulider.AppendLine(GetIntend(3) & "ClassicTriggerExec" & i & "();")
                strbulider.AppendLine(GetIntend(2) & "}")


                strbulider.AppendLine(GetIntend(1) & "}")
                strbulider.AppendLine("}")
            End With
        Next
        '================================================================


        'Wait를 사용하기 위해서
        strbulider.AppendLine("function ClassicTriggerExec() {")
        For i = 0 To RawTriggers.GetElementsCount - 1
            With RawTriggers.GetElements(i)
                LineCount += 1
                strbulider.AppendLine(GetIntend(1) & "ClassicTriggerExec" & i & "();")
            End With
        Next
        strbulider.AppendLine("}")

        LineCount = strbulider.ToString.Split(vbCrLf).Count
        'strbulider.Append(LineCount & " : ")
        strbulider.AppendLine("function ClassicTriggerStarter() {")
        strbulider.AppendLine(GetClassicTrigger() & "}")


        LineCount = strbulider.ToString.Split(vbCrLf).Count
        'strbulider.Append(LineCount & " : ")
        strbulider.AppendLine("function WaitableTriggerExec() {")
        strbulider.AppendLine(GetWaitAbleTrigger() & "}")


        LineCount = strbulider.ToString.Split(vbCrLf).Count
        'strbulider.Append(LineCount & " : ")
        strbulider.AppendLine("function onPluginStart() {")
        If ProjectSet.SCDBUse Then
            strbulider.AppendLine(GetIntend(1) & "scdb.Init();")
        End If
        strbulider.AppendLine(GetIntend(1) & "randomize();")
        LineCount += 2
        'If ProgramSet.StarVersion = "1.16.1" Then
        '    strbulider.AppendLine(GetIntend(1) & "tct.legacySupport();")
        'End If


        strbulider.AppendLine(StartElement.ToCode(0, isbulid))
        strbulider.AppendLine("}")



        LineCount = strbulider.ToString.Split(vbCrLf).Count
        'strbulider.Append(LineCount & " : ")
        strbulider.AppendLine("function beforeTriggerExec() {")
        strbulider.AppendLine(GetIntend(1) & "EUDPlayerLoop()();")
        strbulider.AppendLine(GetIntend(2) & "WaitableTriggerExec();")
        strbulider.AppendLine(GetIntend(2) & "ClassicTriggerExec();")
        strbulider.AppendLine(GetIntend(1) & "EUDEndPlayerLoop();")
        strbulider.AppendLine(GetIntend(1) & "ClassicTriggerStarter();")

        LineCount += 6
        If ProjectSet.UsedSetting(ProjectSet.Settingtype.BtnSet) = True Then
            LineCount += 1
            strbulider.AppendLine(GetIntend(1) & "BGM.Player();")
        End If


        strbulider.AppendLine(BeforeElement.ToCode(0, isbulid) & "}")
        strbulider.AppendLine("function afterTriggerExec() {")
        If ProjectSet.SCDBUse Then
            strbulider.AppendLine(GetIntend(1) & "EUDPlayerLoop()();")
            strbulider.AppendLine(GetIntend(2) & "scdb.SCDBExec();")
            strbulider.AppendLine(GetIntend(1) & "EUDEndPlayerLoop();")
        End If

        LineCount = strbulider.ToString.Split(vbCrLf).Count
        'strbulider.Append(LineCount & " : ")
        strbulider.AppendLine(AfterElement.ToCode(0, isbulid) & "}")


        Return strbulider.ToString
    End Function


    Public Function SaveTrigger() As String
        Dim str As New Text.StringBuilder


        str.AppendLine("&AddText&")
        str.AppendLine(AddText.ToSaveFile)
        str.AppendLine("&functions&")
        str.AppendLine(functions.ToSaveFile)
        str.AppendLine("&GlobalVar&")
        str.AppendLine(GlobalVar.ToSaveFile)
        str.AppendLine("&RawTriggers&")
        str.AppendLine(RawTriggers.ToSaveFile)
        str.AppendLine("&onPluginStart&")
        str.AppendLine(StartElement.ToSaveFile)
        str.AppendLine("&beforeTriggerExec&")
        str.AppendLine(BeforeElement.ToSaveFile)
        str.AppendLine("&afterTriggerExec&")
        str.AppendLine(AfterElement.ToSaveFile)


        Return str.ToString
    End Function




    Public Sub LoadTriggerData()
        Dim _filestream As New FileStream(My.Application.Info.DirectoryPath & "\Data\TriggerEditor\action.json", FileMode.Open)
        Dim _streamreader As New StreamReader(_filestream, System.Text.Encoding.Default)

        Dim jsonString As String = _streamreader.ReadToEnd

        Actions = JsonConvert.DeserializeObject(Of List(Of Action))(jsonString)

        _streamreader.Close()
        _filestream.Close()


        For i = 0 To Actions.Count - 1
            Try
                Actions(i).Text = Actions(i).Texts(Actions(i).Texts.IndexOf(My.Settings.Langage) + 1)
            Catch ex As Exception

            End Try
        Next



        '컨디션 로딩해볼까?
        _filestream = New FileStream(My.Application.Info.DirectoryPath & "\Data\TriggerEditor\condition.json", FileMode.Open)
        _streamreader = New StreamReader(_filestream, System.Text.Encoding.Default)

        jsonString = _streamreader.ReadToEnd

        _streamreader.Close()
        _filestream.Close()

        Condictions = JsonConvert.DeserializeObject(Of List(Of Condiction))(jsonString)

        For i = 0 To Condictions.Count - 1
            Try
                Condictions(i).Text = Condictions(i).Texts(Condictions(i).Texts.IndexOf(My.Settings.Langage) + 1)
            Catch ex As Exception

            End Try
        Next
    End Sub
End Module
