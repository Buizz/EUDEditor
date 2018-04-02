Imports System.IO
'변수 관련 함수.
'변수 싹 정리하고,
'함수 인수에 벨류정의선택을 넣는거야
'함수

Module ValueDefsModule
    Public ValueDefiniction As New List(Of ValueDefs)
    Public Class ValueDefs
        Public Sub New(_name As String, _type As OutPutType)
            Name.Add(_name)
            type = _type
        End Sub
        Public Sub New(_name As String, _type As OutPutType, _values() As String)
            Name.Add(_name)
            type = _type
            values.AddRange(_values)
        End Sub
        Public Sub New(_name() As String, _type As OutPutType)
            Name.AddRange(_name)
            type = _type
        End Sub
        Public Sub New(_name() As String, _type As OutPutType, _values() As String)
            Name.AddRange(_name)
            type = _type
            values.AddRange(_values)
        End Sub

        '정의 이름
        Public Name As New List(Of String)
        '출력 컨트롤
        '리스트 박스, 뉴머리, 텍스트박스, 콤보박스, 체크리스트박스, 유닛프로퍼티

        Public type As OutPutType
        Public Enum OutPutType
            Combobox = 0
            Number = 1
            Text = 2
            List = 3
            CheckList = 4
            UnitProperty = 5
            Variable = 6
            RawString = 7
            ComboboxString = 8
            ListNum = 9
            UnitBtn = 10
            BtnData = 11
            ComboboxNum = 12
            CText = 13
        End Enum

        Public values As New List(Of String)

        '만약정의 이름이 OffsetName이라면
        '배열을 유동적으로 만들어서 내보낸다.
        Public Function GetValues(Optional Flag As Boolean = False, Optional index As Integer = 0) As String()
            Dim _values As New List(Of String)
            Try


                For k = 0 To Name.Count - 1
                    Select Case Name(k)
                        Case "BGM"
                            For Each str As String In Soundlist
                                _values.Add(str.Split("\").Last)
                            Next
                        Case "DestLocation", "StartLocation", "Location", "Where"
                            _values.Add("None")
                            For i = 0 To 254
                                If ProjectSet.CHKLOCATIONNAME(i) <> 0 Then
                                    _values.Add(ProjectSet.CHKSTRING(ProjectSet.CHKLOCATIONNAME(i) - 1))
                                Else
                                    _values.Add("Location " & i)
                                End If
                            Next
                        Case "Switch"
                            '_values.Add("None")
                            For i = 0 To 255
                                If ProjectSet.CHKSWITCHNAME(i) <> 0 Then
                                    _values.Add(ProjectSet.CHKSTRING(ProjectSet.CHKSWITCHNAME(i) - 1))
                                Else
                                    _values.Add("Switch " & i + 1)
                                End If
                            Next
                        Case "WAVName"
                            For i = 0 To ProjectSet.CHKWAVLIST.Count - 1
                                If ProjectSet.CHKWAVLIST(i) <> 0 Then
                                    _values.Add(ProjectSet.CHKSTRING(ProjectSet.CHKWAVLIST(i) - 1))
                                End If
                            Next
                            For i = 0 To CODE(8).Count - 1
                                _values.Add(CODE(8)(i).ToLower)

                            Next
                            '스타에딧웨이브파일 넣어보자.
                        Case "Unit", "UnitType", "OnUnit"
                            For i = 0 To CODE(0).Count - 1
                                If DatEditDATA(DTYPE.units).ReadValue("Unit Map String", i) = 0 Then
                                    _values.Add(CODE(0)(i))
                                Else
                                    Try
                                        If Flag Then
                                            _values.Add(ProjectSet.CHKSTRING(-1 + ProjectSet.CHKUNITNAME(i)))
                                        Else
                                            _values.Add(ProjectSet.CHKSTRING(-1 + ProjectSet.CHKUNITNAME(i)) & "(" & CODE(0)(i) & ")")
                                        End If
                                    Catch ex As Exception
                                        _values.Add(CODE(0)(i))
                                    End Try
                                End If
                            Next
                            _values.Add("None")
                            _values.Add("Any unit")
                            _values.Add("Men")
                            _values.Add("Buildings")
                            _values.Add("Factories")
                        Case "OffsetName"
                            _values.AddRange(DatEditDATA(index).keyDic.Keys.ToArray)
                        Case "stat_txt"
                            _values.Add("None")
                            For i = 0 To stat_txt.Count - 1
                                If stattextdic.ContainsKey(i) Then
                                    _values.Add(stattextdic(i))
                                Else
                                    _values.Add(stat_txt(i))
                                End If
                            Next

                        Case "Rank/Sublabelstat_txt"
                            _values.AddRange(stat_txt)
                            For i = 0 To 1300
                                _values.RemoveAt(0)
                            Next
                        Case "CircleImage"
                            _values.AddRange(CODE(DTYPE.images).ToArray)
                            For i = 0 To 560
                                _values.RemoveAt(0)
                            Next
                            While (_values.Count > 256)
                                _values.RemoveAt(256)
                            End While
                        Case "StructOffset"
                            For i = 0 To CUnitData.Count - 1
                                _values.Add(CUnitData(i)(2))
                            Next

                    End Select

                    Dim filename As String = My.Application.Info.DirectoryPath & "\Data\Langage\" & My.Settings.Langage & "\" & Name(k) & ".txt"
                    If CheckFileExist(filename) = False Then
                        Dim filestream As New FileStream(filename, FileMode.Open)
                        Dim strreader As New StreamReader(filestream, Text.Encoding.Default)

                        While (strreader.EndOfStream = False)
                            _values.Add(strreader.ReadLine)
                        End While


                        strreader.Close()
                        filestream.Close()
                    End If
                    If _values.Count <> 0 Then
                        Exit For
                    End If
                Next
                If _values.Count = 0 Then
                    _values.AddRange(values)
                End If




                _values.Add("Empty")
                Return _values.ToArray
            Catch ex As Exception

            End Try
            Return _values.ToArray
        End Function
    End Class

    Public CUnitData As New List(Of String())
    Public Sub LoadCUnitData()
        Dim filename As String = My.Application.Info.DirectoryPath & "\Data\CUnitToTriggerEditor.txt"
        Dim filestream As New FileStream(filename, FileMode.Open)
        Dim strreader As New StreamReader(filestream, Text.Encoding.Default)

        While (strreader.EndOfStream = False)
            CUnitData.Add(strreader.ReadLine.Split(","))
        End While


        strreader.Close()
        filestream.Close()
    End Sub


    '모든 벨류들을 추가한다.
    Public Sub LoadValueDef()
        ValueDefiniction.Add(New ValueDefs("RawString", ValueDefs.OutPutType.RawString))


        ' "Time", "Count", "Percent", "NewValue", "Add", "Goal", "Amount", "Number",
        ValueDefiniction.Add(New ValueDefs({"Number", "Count", "Percent", "NewValue", "Add", "Goal", "Amount", "Time", "Line"}, ValueDefs.OutPutType.Number))

        ' "Script", "Unit", "UnitType", "OnUnit", "WAVName", "Switch", "StartLocation", "DestLocation", "Location", "Where",
        ValueDefiniction.Add(New ValueDefs("Script", ValueDefs.OutPutType.ComboboxString))
        ValueDefiniction.Add(New ValueDefs({"Unit", "UnitType", "OnUnit"}, ValueDefs.OutPutType.Combobox))
        ValueDefiniction.Add(New ValueDefs("WAVName", ValueDefs.OutPutType.ComboboxString))
        ValueDefiniction.Add(New ValueDefs("Switch", ValueDefs.OutPutType.Combobox))
        ValueDefiniction.Add(New ValueDefs({"Location", "DestLocation", "StartLocation", "Where"}, ValueDefs.OutPutType.Combobox))
        ValueDefiniction.Add(New ValueDefs("DatFile", ValueDefs.OutPutType.Combobox, {"units.dat", "weapons.dat", "flingy.dat", "sprites.dat", "images.dat", "upgrades.dat", "techdata.dat", "orders.dat"}))

        ValueDefiniction.Add(New ValueDefs("OffsetName", ValueDefs.OutPutType.Combobox))
        ValueDefiniction.Add(New ValueDefs("Weapon", ValueDefs.OutPutType.Combobox, CODE(1).ToArray))
        ValueDefiniction.Add(New ValueDefs("Flingy", ValueDefs.OutPutType.Combobox, CODE(2).ToArray))
        ValueDefiniction.Add(New ValueDefs("Sprite", ValueDefs.OutPutType.Combobox, CODE(3).ToArray))
        ValueDefiniction.Add(New ValueDefs("Image", ValueDefs.OutPutType.Combobox, CODE(4).ToArray))
        ValueDefiniction.Add(New ValueDefs("Upgrade", ValueDefs.OutPutType.Combobox, CODE(5).ToArray))
        ValueDefiniction.Add(New ValueDefs("Techdata", ValueDefs.OutPutType.Combobox, CODE(6).ToArray))
        ValueDefiniction.Add(New ValueDefs("Order", ValueDefs.OutPutType.Combobox, CODE(7).ToArray))
        ValueDefiniction.Add(New ValueDefs("Sfxdata", ValueDefs.OutPutType.Combobox, CODE(8).ToArray))
        ValueDefiniction.Add(New ValueDefs("Portdata", ValueDefs.OutPutType.Combobox, CODE(9).ToArray))
        ValueDefiniction.Add(New ValueDefs("stat_txt", ValueDefs.OutPutType.Combobox))



        '"State", "ResourceType", "ScoreType", "Modifier", "OrderType", "TimeModifier", "AlwaysDisplay", "ForPlayer", "Owner", "Player", "NewOwner", "VariableModifier",


        ValueDefiniction.Add(New ValueDefs({"PlayerX", "Player", "NewOwner", "Owner", "ForPlayer"}, ValueDefs.OutPutType.ListNum, {"Player 1", "Player 2", "Player 3", "Player 4", "Player 5", "Player 6",
                                           "Player 7", "Player 8", "Player 9", "Player 10", "Player 11", "Player 12", "Unknown",
                                           "CurrentPlayer", "Foes", "Allies", "NeutralPlayers", "AllPlayers", "Force1", "Force2",
                                           "Force3", "Force4", "Unknown", "Unknown", "Unknown", "Unknown", "NonAlliedVictoryPlayers"}))
        ValueDefiniction.Add(New ValueDefs("VariableModifier", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("VariableComparison", ValueDefs.OutPutType.ListNum))


        ValueDefiniction.Add(New ValueDefs("DisplayOption", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("AlwaysDisplay", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("State", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("SState", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("ResourceType", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("Comparison", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("ScoreType", ValueDefs.OutPutType.ListNum, {"Total", "Units", "Buildings", "UnitsAndBuildings", "Kills", "Razings", "KillsAndRazings", "Custom"}))
        ValueDefiniction.Add(New ValueDefs({"Modifier", "TimeModifier"}, ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("OrderType", ValueDefs.OutPutType.ListNum))



        '"Label", "Text", "ScenarioName",
        ValueDefiniction.Add(New ValueDefs({"Text", "Label"}, ValueDefs.OutPutType.Text, CODE(11).ToArray))
        ValueDefiniction.Add(New ValueDefs("CText", ValueDefs.OutPutType.CText, CODE(11).ToArray))
        ValueDefiniction.Add(New ValueDefs("ScenarioName", ValueDefs.OutPutType.Text))

        '"Properties",
        ValueDefiniction.Add(New ValueDefs("Properties", ValueDefs.OutPutType.UnitProperty))

        '"Variable"
        ValueDefiniction.Add(New ValueDefs({"Variable", "UnitPTR", "UnitEPD"}, ValueDefs.OutPutType.Variable))


        '"button",
        ValueDefiniction.Add(New ValueDefs("UnitBtn", ValueDefs.OutPutType.UnitBtn))
        ValueDefiniction.Add(New ValueDefs("BtnData", ValueDefs.OutPutType.BtnData))



        ValueDefiniction.Add(New ValueDefs("Status", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("UnitDirection", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("Boolean", ValueDefs.OutPutType.ListNum, {"NotUse", "Use"}))
        ValueDefiniction.Add(New ValueDefs("ElevationLevels", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("FlingyFlags", ValueDefs.OutPutType.CheckList))
        ValueDefiniction.Add(New ValueDefs("Rank/Sublabelstat_txt", ValueDefs.OutPutType.Combobox))
        ValueDefiniction.Add(New ValueDefs("AIInternal", ValueDefs.OutPutType.CheckList))
        ValueDefiniction.Add(New ValueDefs("SpecialAbilityFlags", ValueDefs.OutPutType.CheckList))
        ValueDefiniction.Add(New ValueDefs("UnitSize", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("Rightclick", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("StareditGroupFlags", ValueDefs.OutPutType.CheckList))
        ValueDefiniction.Add(New ValueDefs("StareditAvailabilityFlags", ValueDefs.OutPutType.CheckList))
        ValueDefiniction.Add(New ValueDefs("TargetType", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("DamTypes", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("Behaviours", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("Explosions", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("Icon", ValueDefs.OutPutType.Combobox))
        ValueDefiniction.Add(New ValueDefs("FlingyControl", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("CircleImage", ValueDefs.OutPutType.Combobox))
        ValueDefiniction.Add(New ValueDefs("GRPfile", ValueDefs.OutPutType.Combobox))
        ValueDefiniction.Add(New ValueDefs("Races", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("Animations", ValueDefs.OutPutType.ListNum))


        ValueDefiniction.Add(New ValueDefs("StructOffset", ValueDefs.OutPutType.Combobox))




        ValueDefiniction.Add(New ValueDefs("orderState", ValueDefs.OutPutType.CheckList, {"Moving/ Following Order", "No collide(Larva)?", "Harvesting? Working?", "Constructing Stationary"}))
        ValueDefiniction.Add(New ValueDefs("orderSignal", ValueDefs.OutPutType.CheckList, {"Update building graphic/state", "Casting spell", "Reset collision? Always enabled for hallucination...", "Unknown", "Lift/Land state"}))
        ValueDefiniction.Add(New ValueDefs("userActionFlags", ValueDefs.OutPutType.CheckList, {"Unknown", "Unknown", "issued an order", "interrupted an order", "self destructing"}))
        ValueDefiniction.Add(New ValueDefs("UnitMovementState", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("statusFlags", ValueDefs.OutPutType.CheckList))
        ValueDefiniction.Add(New ValueDefs("SresourceType", ValueDefs.OutPutType.ListNum, {"Unknown", "Gas", "Ore"}))
        ValueDefiniction.Add(New ValueDefs("visibilityStatus", ValueDefs.OutPutType.CheckList))

        'ValueDefiniction.Add(New ValueDefs("DValue", ValueDefs.OutPutType.Combobox))
        'ValueDefiniction.Add(New ValueDefs("SValue", ValueDefs.OutPutType.Combobox))


        ValueDefiniction.Add(New ValueDefs("KeyCode", ValueDefs.OutPutType.Combobox))


        ValueDefiniction.Add(New ValueDefs("BGM", ValueDefs.OutPutType.Combobox))
        ValueDefiniction.Add(New ValueDefs("BGMFlag", ValueDefs.OutPutType.ListNum))


        ValueDefiniction.Add(New ValueDefs("BtnEnableTxt", ValueDefs.OutPutType.UnitBtn))
        ValueDefiniction.Add(New ValueDefs("BtnUnEnableTxt", ValueDefs.OutPutType.UnitBtn))

        ValueDefiniction.Add(New ValueDefs("CState", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("UnitBtnlist", ValueDefs.OutPutType.Combobox, CODE(11).ToArray))
        ValueDefiniction.Add(New ValueDefs("ScoreOffset", ValueDefs.OutPutType.ListNum))


        ValueDefiniction.Add(New ValueDefs("SupplyType", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("SCDBMsgType", ValueDefs.OutPutType.ListNum))
        ValueDefiniction.Add(New ValueDefs("SCDBConnectStatus", ValueDefs.OutPutType.ListNum))


        LoadCUnitData()
    End Sub

    '정의 번호를 역으로 넣는다.
    Public Function GetDefValueDefs(_name As String) As ValueDefs
        Dim valuedef As String
        Try
            valuedef = _name.Split(".")(1)
        Catch ex As Exception
            valuedef = _name
        End Try


        For i = 0 To ValueDefiniction.Count - 1
            For j = 0 To ValueDefiniction(i).Name.Count - 1
                If valuedef = ValueDefiniction(i).Name(j) Then
                    Return ValueDefiniction(i)
                End If
            Next

        Next
        Return ValueDefiniction(0)
    End Function






End Module
