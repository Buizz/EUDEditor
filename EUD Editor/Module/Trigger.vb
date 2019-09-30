Imports System.Text

Public Enum ElementType
    main = 0
    조건문if = 1
    조건문ifelse = 2
    조건절 = 3
    액션절 = 4
    만족 = 5
    만족안함 = 6
    조건 = 7
    액션 = 8
    와일 = 9
    포 = 10
    스위치 = 11
    와일조건 = 12
    와일만족 = 13
    포만족 = 14
    Functions = 15
    함수정의 = 16
    인수 = 17
    코드 = 18
    함수 = 19
    Wait = 20
    Foluder = 21
    FoluderAction = 22
    RawString = 23
    RawTrigger = 24
    RawTriggers = 25
    TriggerCond = 26
    TriggerAct = 27
    Switch = 28
    Switchcase = 29
End Enum



Public Class Element
    Private Function ElementNames(Et As ElementType) As String
        Dim temp() As String = {
               "",'0
               "If (Conditions) then do (Actions)",'1
               "If (Conditions) then do (Actions) else do (Actions)",'2
               "if : ",'3
               "actions : ",'4
               "then : ",'5
               "else : ",'6
               "Con",'7
               "Act",'8
               "While : ",'9
               "For",'10
               "Switch",'11
               "Condition : ",'12
               "Action : ",'13 
               "Action : ",'14
               "CustomFuncitons : ",'15
               "FuncitonDef : ",'16
               "Argument : ",'17
               "Code : ",'18
               "Fuction : ",'19
               "Wait : ",'20
               "Folder : ",'21
               "actions : ",'22
               "",'23
               "ClassciTrigger",'24
               "Triggers",'25
               "Condition : ",'26
               "Action : ",'27
               "Switch : ",'28
               "Case : "'29
               }

        Return temp(Et)
    End Function


    Public Parrent As Element
    Public isdisalbe As Boolean
    Public isNotcon As Boolean
    Public isFloding As Boolean


    Public CTreeNode As TreeNode


    Public act As Action
    Public con As Condiction
    Private Elements As List(Of Element)
    Public Values As New List(Of String)


    Private Type As ElementType

    Public Function Getindex() As Integer
        For i = 0 To Parrent.GetElementsCount - 1
            If Parrent.GetElements(i).GetHashCode = Me.GetHashCode Then
                Return i
            End If
        Next
        Return -1
    End Function





    Public Function GetValueTodef(valdef As String) As String
        Dim returnstring As String = ""

        Dim valuecount As Integer = valdef.Split(".")(0)
        Dim valuedef As String = valdef.Split(".")(1)

        'MsgBox(valuedef)

        Dim count As Integer = 0



        If act IsNot Nothing Then
            '액션일경우
            For i = 0 To act.ValuesDef.Count - 1
                If act.ValuesDef(i) = valuedef Then
                    count += 1
                    If count = valuecount Then
                        returnstring = Values(i)
                        Exit For
                    End If
                End If
            Next
        ElseIf con IsNot Nothing Then
            '조건일경우
            For i = 0 To con.ValuesDef.Count - 1
                If con.ValuesDef(i) = valuedef Then
                    count += 1
                    If count = valuecount Then
                        returnstring = Values(i)
                        Exit For
                    End If
                End If
            Next
        End If



        Return returnstring
    End Function
    Public Sub SetValueTodef(valdef As String, value As String)
        Dim valuecount As Integer = valdef.Split(".")(0)
        Dim valuedef As String = valdef.Split(".")(1)

        'MsgBox(valuecount)

        Dim count As Integer = 0
        If act IsNot Nothing Then
            '액션일경우
            For i = 0 To act.ValuesDef.Count - 1
                If act.ValuesDef(i) = valuedef Then
                    count += 1
                    If count = valuecount Then
                        Values(i) = value
                        Exit For
                    End If
                End If
            Next
        ElseIf con IsNot Nothing Then
            '조건일경우
            For i = 0 To con.ValuesDef.Count - 1
                If con.ValuesDef(i) = valuedef Then
                    count += 1
                    If count = valuecount Then
                        Values(i) = value
                        Exit For
                    End If
                End If
            Next
        End If


    End Sub




    Public Function ValueParser(Valnum As Integer, Optional isTocode As Boolean = False) As String
        Try
            Dim _value As String = Values(Valnum)
            Dim valdef As String

            If act Is Nothing Then
                valdef = con.ValuesDef(Valnum)
            Else
                valdef = act.ValuesDef(Valnum)
            End If

            Return ValueParser(_value, valdef, isTocode)
        Catch ex As Exception


            Return Valnum
        End Try
    End Function

    Public Function ValueParser(_value As String, valdef As String, Optional isTocode As Boolean = False, Optional isnumber As Boolean = False) As String
        Dim returnstring As String = _value



        Dim defvlaue2 As Integer = 0



        Dim _values As New List(Of String)



        '{"대입", "덧셈", "뺄셈", "곱셈", "나눗셈"}))
        '{"일치", "불일치", "이상", "이하", "초과", "미만"}))
        Select Case valdef
            Case "PlayerX"
                If isTocode Then
                    Select Case _value
                        Case 13
                            returnstring = "getcurpl()"
                    End Select
                End If
            Case "VariableModifier"
                If isTocode Then
                    Select Case _value
                        Case 0
                            returnstring = "="
                        Case 1
                            returnstring = "+="
                        Case 2
                            returnstring = "-="
                        Case 3
                            returnstring = "*="
                        Case 4
                            returnstring = "/="
                    End Select
                    Return returnstring
                End If
            Case "VariableComparison"
                If isTocode Then
                    Select Case _value
                        Case 0
                            returnstring = "=="
                        Case 1
                            returnstring = "!="
                        Case 2
                            returnstring = ">="
                        Case 3
                            returnstring = "<="
                        Case 4
                            returnstring = ">"
                        Case 5
                            returnstring = "<"
                    End Select
                    Return returnstring
                End If
            Case "AlwaysDisplay"
                If isTocode Then
                    Select Case _value
                        Case 0
                            returnstring = "0"
                        Case 1
                            returnstring = "4"
                    End Select
                End If

            Case "Comparison"
                If isTocode Then
                    Select Case _value
                        Case 0
                            returnstring = "0"
                        Case 1
                            returnstring = "1"
                        Case 2
                            returnstring = "10"
                    End Select
                End If
            Case "Modifier", "TimeModifier"
                If isTocode Then
                    Select Case _value
                        Case 0
                            returnstring = "7"
                        Case 1
                            returnstring = "8"
                        Case 2
                            returnstring = "9"
                    End Select
                End If
            Case "SState"
                If isTocode Then
                    Select Case _value
                        Case 0
                            returnstring = "4"
                        Case 1
                            returnstring = "5"
                        Case 2
                            returnstring = "6"
                        Case 3
                            returnstring = "11"
                    End Select
                End If
            Case "State"
                If isTocode Then
                    Select Case _value
                        Case 0
                            returnstring = "4"
                        Case 1
                            returnstring = "5"
                        Case 2
                            returnstring = "6"
                    End Select
                End If
            Case "CState"
                If isTocode Then
                    Select Case _value
                        Case 0
                            returnstring = "2"
                        Case 1
                            returnstring = "3"
                    End Select
                End If
            Case "ScoreOffset"
                Dim offsets() As String = {"0x581DE4", "0x581E14", "0x581E44", "0x581E74", "0x581EA4", "0x581ED4", "0x581F04", "0x581F34", "0x581F64", "0x581F94", "0x581FC4", "0x581FF4", "0x582024", "0x582054", "0x582084", "0x5820B4", "0x5820E4", "0x582114", "0x5822F4"}
                If isTocode Then
                    returnstring = offsets(_value)
                End If
            Case "Properties"
                'UnitProperty(hitpoint = 100, shield = 100, energy = 100, resource = 0, hanger = 0, cloaked = True, burrowed = False, intransit = False, hallucinated = True, invincible = False)
                If isTocode = False Then
                    Return "UnitProperty"
                End If
            Case "DatFile"
                If isTocode = True Then
                    Dim val As Integer = 0
                    Try
                        val = _value

                        Dim offsets() As String = {"0x65FD00", "0x6564E0", "0x6C9858", "0x665AC0", "0x666778", "0x655700", "0x656198", "0x664A40"}
                        returnstring = offsets(val)



                        Return returnstring
                    Catch ex As Exception
                        Return returnstring
                    End Try
                End If
            Case "Object"
                If isTocode = False Then
                    Try
                        Dim num As Integer = CInt(GetValue("DatFile"))
                        Dim strings() As String = {"Unit", "Weapon", "Flingy", "Sprite", "Image", "Upgrade", "Techdata", "Order"}
                        valdef = strings(num)
                    Catch ex As Exception
                        Return returnstring
                    End Try
                End If
            Case "OffsetName"
                If isTocode = False Then
                    Try
                        Dim num As Integer = CInt(GetValue("DatFile"))
                        Dim val As Integer = _value
                        defvlaue2 = num
                        _values.Clear()
                        _values.AddRange(GetDefValueDefs(valdef).GetValues(isTocode, num))

                        returnstring = _values(val)
                        Return returnstring
                    Catch ex As Exception
                        Return returnstring
                    End Try
                Else
                    '수식 대잔치
                    Dim offsets() As String = {"0x65FD00", "0x6564E0", "0x6C9858", "0x665AC0", "0x666778", "0x655700", "0x656198", "0x664A40"}

                    Try
                        Dim num As Integer = CInt(GetValue("DatFile"))
                        Dim Tvalue As Integer = _value
                        Dim Offsetname As String = DatEditDATA(num).typeName & "_" & DatEditDATA(num).keyDic.Keys.ToList(_value)
                        'Dim typeName As String = DatEditDATA(num).keyDic.Keys.ToList(_value)


                        'Dim _size As Integer = DatEditDATA(num).keyINFO(Tvalue).realSize

                        Dim _offset As Long = Val("&H" & ReadOffset(Offsetname))


                        returnstring = _offset - Val("&H" & offsets(num).Replace("0x", ""))
                        Return returnstring
                    Catch ex As Exception
                        Return returnstring
                    End Try

                End If
            Case "DValue"
                If isTocode = False Then
                    Try
                        Dim num As Integer = CInt(GetValue("DatFile"))
                        Dim num2 As Integer = CInt(GetValue("OffsetName"))
                        Dim val As Integer = _value

                        Dim _defstring As String = GetDefValueDefs("OffsetName").GetValues(False, num)(num2)
                        _defstring = DatEditDATA(num).typeName & "_" & _defstring

                        If GetDefValueDefs(ReadValDef(_defstring)).type = ValueDefs.OutPutType.CheckList Then
                            Return "0x" & Hex(returnstring)
                        End If
                        _values.Clear()
                        _values.AddRange(GetDefValueDefs(ReadValDef(_defstring)).GetValues(isTocode, num))

                        returnstring = _values(val)
                        Return returnstring
                    Catch ex As Exception

                    End Try
                End If
            Case "StructOffset"
                If isTocode = True Then
                    Try
                        Dim val As Integer = _value
                        returnstring = "0x" & CUnitData(val)(0)
                        Return returnstring
                    Catch ex As Exception

                    End Try
                End If
            Case "SValue"
                If isTocode = False Then
                    Try
                        Dim num As Integer = CInt(GetValue("StructOffset"))
                        Dim _defstring As String = CUnitData(num)(1)
                        Dim val As Integer = _value

                        If GetDefValueDefs(CUnitData(num)(1)).type = ValueDefs.OutPutType.CheckList Then
                            Return "0x" & Hex(returnstring)
                        End If
                        _values.Clear()
                        _values.AddRange(GetDefValueDefs(CUnitData(num)(1)).GetValues(isTocode, num))

                        returnstring = _values(val)
                        Return returnstring
                    Catch ex As Exception

                    End Try
                End If
            Case "KeyCode"
                If isTocode = True Then
                    Return returnstring
                End If
            Case "BtnEnableTxt"
                If isTocode = False Then
                    Try
                        Dim _tempvalue() As String = GetValue("BtnEnableTxt").Split(":")

                        If ProjectBtnUSE(_tempvalue(0)) = True Then
                            returnstring = ProjectBtnData(_tempvalue(0))(_tempvalue(1)).enaStr - 1
                        Else
                            returnstring = BtnData(_tempvalue(0))(_tempvalue(1)).enaStr - 1
                        End If

                        If stattextdic.ContainsKey(returnstring) Then
                            returnstring = stattextdic(returnstring)
                        Else
                            returnstring = stat_txt(returnstring)
                        End If
                        Return returnstring
                    Catch ex As Exception
                        Return returnstring
                    End Try
                Else
                    Try
                        Dim _tempvalue() As String = GetValue("BtnEnableTxt").Split(":")

                        If ProjectBtnUSE(_tempvalue(0)) = True Then
                            returnstring = ProjectBtnData(_tempvalue(0))(_tempvalue(1)).enaStr
                        Else
                            returnstring = BtnData(_tempvalue(0))(_tempvalue(1)).enaStr
                        End If
                        Return returnstring
                    Catch ex As Exception
                        Return returnstring
                    End Try
                End If
            Case "BtnUnEnableTxt"
                If isTocode = False Then
                    Try
                        Dim _tempvalue() As String = GetValue("BtnUnEnableTxt").Split(":")

                        If ProjectBtnUSE(_tempvalue(0)) = True Then
                            returnstring = ProjectBtnData(_tempvalue(0))(_tempvalue(1)).disStr - 1
                        Else
                            returnstring = BtnData(_tempvalue(0))(_tempvalue(1)).disStr - 1
                        End If

                        If stattextdic.ContainsKey(returnstring) Then
                            returnstring = stattextdic(returnstring)
                        Else
                            returnstring = stat_txt(returnstring)
                        End If
                        Return returnstring
                    Catch ex As Exception
                        Return returnstring
                    End Try
                Else
                    Try
                        Dim _tempvalue() As String = GetValue("BtnUnEnableTxt").Split(":")

                        If ProjectBtnUSE(_tempvalue(0)) = True Then
                            returnstring = ProjectBtnData(_tempvalue(0))(_tempvalue(1)).disStr
                        Else
                            returnstring = BtnData(_tempvalue(0))(_tempvalue(1)).disStr
                        End If
                        Return returnstring
                    Catch ex As Exception
                        Return returnstring
                    End Try
                End If



            Case "UnitBtn"
                If isTocode = False Then
                    Try
                        Dim unitsname() As String = CODE(11).ToArray
                        Dim Icon() As String = CODE(12).ToArray

                        Dim _tempvalue() As String = GetValue("UnitBtn").Split(":")


                        returnstring = unitsname(_tempvalue(0)) & ", Btn : " & _tempvalue(1)
                        Return returnstring
                    Catch ex As Exception
                        Return returnstring
                    End Try

                Else
                    Try
                        Dim _tempvalue() As String = _value.Split(":")

                        Dim i As Integer = _tempvalue(0)
                        '_tempvalue(1)는 위치관련이 아니라 인덱스야.

                        Dim sort As New List(Of UInteger)
                        Dim data As New List(Of UInteger)
                        If ProjectBtnData(i).Count <> 0 Then


                            For j = 0 To ProjectBtnData(i).Count - 1
                                sort.Add(j)
                                data.Add(ProjectBtnData(i)(j).pos)
                            Next
                            With ProjectBtnData(i)
                                For j = 0 To .Count - 1 'pos를 기준으로 정렬.
                                    'j번째 데이터를 선택.
                                    '

                                    For p = j + 1 To .Count - 1  'pos를 기준으로 정렬.
                                        If data(j) > data(p) Then
                                            Dim temp As UInteger = sort(p)
                                            sort(p) = sort(j)
                                            sort(j) = temp

                                            temp = data(p)
                                            data(p) = data(j)
                                            data(j) = temp
                                        End If
                                    Next
                                Next
                            End With
                        End If

                        Dim realpos As Integer
                        For i = 0 To sort.Count - 1
                            If sort(i) = _tempvalue(1) Then
                                realpos = i
                                Exit For
                            End If
                        Next


                        '오프셋 구하기.
                        returnstring = "epdread_epd(EPD(0x5187EC) + " & 3 * _tempvalue(0) & ") + 5 * " & realpos '_tempvalue(1)
                        Return returnstring
                    Catch ex As Exception
                        Return returnstring
                    End Try
                End If
            Case "Location"
                If isTocode = True And isnumber = True Then
                    Try
                        returnstring -= 1
                        Return returnstring
                    Catch ex As Exception

                    End Try
                End If
            Case "CText"
                If isTocode = True Then
                    returnstring = CTextEncode(returnstring)

                    Return returnstring
                End If
            Case "DBText"
                If isTocode = True Then
                    returnstring = "Db(u2utf8(" & returnstring & "))"

                    Return returnstring
                End If
            Case "WAVName"
                If isTocode = True Then
                    returnstring = returnstring.Replace("\", "\\")
                    'If returnstring.IndexOf("staredit") <> -1 Then
                    '    returnstring = returnstring.Replace("\", "\\")
                    'Else
                    '    returnstring = """sound\\" & returnstring.Replace("\", "\\").Replace("""", "") & """"
                    'End If


                    Return returnstring
                End If
        End Select

        If isnumber = False Then
            Select Case GetDefValueDefs(valdef).type
                Case ValueDefs.OutPutType.ListNum
                    If isTocode = False Then
                        _values.Clear()
                        _values.AddRange(GetDefValueDefs(valdef).GetValues(isTocode))

                        Dim val As Integer = 0
                        Try
                            val = _value
                            returnstring = _values(val)


                            Return returnstring
                        Catch ex As Exception
                            Return returnstring
                        End Try
                    End If
                    Return "(" & returnstring & ")"
                Case ValueDefs.OutPutType.CheckList
                    Try
                        Return "0x" & Hex(returnstring)
                    Catch ex As Exception
                        Return returnstring
                    End Try

                Case ValueDefs.OutPutType.ComboboxNum
                    If isTocode = False Then
                        _values.Clear()
                        _values.AddRange(GetDefValueDefs(valdef).GetValues(isTocode))

                        Dim val As Integer = 0
                        Try
                            val = _value
                            returnstring = _values(val)

                            If valdef = "WAVName" And isTocode = True Then
                                returnstring = returnstring.Replace("\", "\\")
                            End If


                            If isTocode = True Then
                                returnstring = """" & returnstring & """"
                            End If

                            Return returnstring
                        Catch ex As Exception
                            Return returnstring
                        End Try
                    Else
                        Return "(" & returnstring & ")"
                    End If
                Case ValueDefs.OutPutType.Combobox
                    _values.Clear()
                    _values.AddRange(GetDefValueDefs(valdef).GetValues(isTocode))

                    Dim val As Integer = 0
                    Try
                        val = _value
                        returnstring = _values(val)

                        If valdef = "WAVName" And isTocode = True Then
                            returnstring = returnstring.Replace("\", "\\")
                        End If


                        If isTocode = True Then
                            returnstring = _value '"""" & returnstring & """"
                        End If

                        If isTocode Then
                            returnstring = "(" & returnstring & ")"
                        End If
                        Return returnstring
                    Catch ex As Exception
                        If isTocode Then
                            returnstring = "(" & returnstring & ")"
                        End If
                        Return returnstring
                    End Try
                Case Else
                    Return returnstring
            End Select
        Else
            Return returnstring
        End If


    End Function



    Private Function NextLine(str As String, ByRef index As Integer) As String
        index += 1
        Return Mid(str, str.IndexOf(":") + 2, str.Length - str.IndexOf(":") + 2)
    End Function

    Private Function SeachAct(name As String) As Action
        For i = 0 To Actions.Count - 1
            If name = Actions(i).Name Then
                Return Actions(i)
            End If
        Next
        Return Actions(0)
    End Function
    Private Function SeachCon(name As String) As Condiction
        For i = 0 To Condictions.Count - 1
            If name = Condictions(i).Name Then
                Return Condictions(i)
            End If
        Next
        Return Condictions(0)
    End Function



    Public Function ToSaveFile() As String
        Dim _stringb As New StringBuilder

        _stringb.AppendLine("Type:" & Type & "," & isdisalbe & "," & isFloding & "," & isNotcon)

        Select Case Type
            Case ElementType.액션
                _stringb.AppendLine("act:" & act.Name)

                Dim temp As New StringBuilder
                temp.Append(Values(0))
                For i = 1 To Values.Count - 1
                    temp.Append(Separater & Values(i))
                Next

                _stringb.AppendLine(temp.ToString)
            Case ElementType.조건
                _stringb.AppendLine("con:" & con.Name)

                Dim temp As New StringBuilder
                temp.Append(Values(0))
                For i = 1 To Values.Count - 1
                    temp.Append(Separater & Values(i))
                Next

                _stringb.AppendLine(temp.ToString)
            Case ElementType.포, ElementType.함수, ElementType.함수정의, ElementType.Foluder, ElementType.Switchcase
                Dim temp As New StringBuilder
                temp.Append(Values(0))
                For i = 1 To Values.Count - 1
                    temp.Append(Separater & Values(i))
                Next

                _stringb.AppendLine(temp.ToString)
            Case ElementType.조건절, ElementType.와일조건, ElementType.Wait, ElementType.RawString, ElementType.RawTrigger, ElementType.TriggerCond, ElementType.Switch
                Dim temp As New StringBuilder
                temp.Append(Values(0))

                _stringb.AppendLine(temp.ToString)
        End Select


        _stringb.AppendLine("ElementsCount:" & Elements.Count)
        For i = 0 To Elements.Count - 1
            _stringb.Append(Elements(i).ToSaveFile)
        Next

        _stringb.AppendLine("END")


        Return _stringb.ToString
    End Function

    Public Function LoadFile(_str As String, index As Integer, Optional isfrist As Boolean = False) As Integer
        Dim tempstr() As String

        Dim actconname As String = ""
        Dim _index As Integer = index
        tempstr = _str.Split(vbCrLf)

        Dim typeflag As String = NextLine(tempstr(_index), _index)


        Select Case typeflag.Split(",").Count
            Case 1
                Type = typeflag.Split(",")(0)
                isdisalbe = False
                isFloding = False
                isNotcon = False
            Case 2
                Type = typeflag.Split(",")(0)
                isdisalbe = typeflag.Split(",")(1)
                isFloding = False
                isNotcon = False
            Case 3
                Type = typeflag.Split(",")(0)
                isdisalbe = typeflag.Split(",")(1)
                isFloding = typeflag.Split(",")(2)
                isNotcon = False
            Case 4
                Type = typeflag.Split(",")(0)
                isdisalbe = typeflag.Split(",")(1)
                isFloding = typeflag.Split(",")(2)
                isNotcon = typeflag.Split(",")(3)
        End Select


        Dim isreadvalue As Boolean = False

        Select Case Type
            Case ElementType.액션
                actconname = NextLine(tempstr(_index), _index)

                act = SeachAct(actconname)
                isreadvalue = True
            Case ElementType.조건
                actconname = NextLine(tempstr(_index), _index)

                con = SeachCon(actconname)
                isreadvalue = True
            Case ElementType.포, ElementType.함수정의, ElementType.함수, ElementType.조건, ElementType.조건절, ElementType.와일조건, ElementType.Wait, ElementType.Foluder, ElementType.RawString, ElementType.RawTrigger, ElementType.TriggerCond, ElementType.Switch, ElementType.Switchcase
                isreadvalue = True
        End Select

        If isreadvalue = True Then
            Values = New List(Of String)
            Dim _valuestring As String = ""
            _valuestring = _valuestring & tempstr(_index).Trim
            _index += 1
            While (tempstr(_index).Trim.IndexOf("ElementsCount") = -1)
                _valuestring = _valuestring & vbCrLf & tempstr(_index).Trim
                _index += 1
            End While

            Values.AddRange(_valuestring.Split(Separater))
        End If


        '함수가 정의되어있지 않는 함수일 경우
        If Type = ElementType.함수 And isfrist = False Then
            If CheckFunc(Values(0)) = False Then
                If CheckFuncExist(Values(0)) Then
                    Try
                        FuncLoadFile(Values(0))
                        TrigEditorForm.refreshScreen()
                    Catch ex As Exception
                    End Try
                End If
            End If
        End If

        Dim Compmsg As String = ""
        '호환성코드
        Select Case Type
            Case ElementType.액션
                Select Case actconname
                    Case "SetUpgradeResearched", "AddUpgradeResearched", "SetUpgradeAvailable", "AddUpgradeAvailable", "SetTechnologiesResearched", "AddTechnologiesResearched", "SetTechnologiesAvailable", "AddTechnologiesAvailable"
                        Dim valuesstr As String = ""

                        For i = 0 To Values.Count - 1
                            If i = 0 Then
                                valuesstr = Values(i)
                            Else
                                valuesstr = valuesstr & ", " & Values(i)
                            End If
                        Next

                        If actconname.Contains("Upgrade") Then
                            act = SeachAct("SetUpgrade")
                        Else
                            act = SeachAct("SetTech")
                        End If

                        '0"PlayerX",
                        '1"Upgrade",
                        '2"Count"

                        'To

                        '0"PlayerX",
                        '1"Upgrade",
                        '2"UTType",
                        '3"Count",
                        '4"Modifier"

                        Dim oldvalue As New List(Of String)
                        oldvalue.AddRange(Values)

                        Values.Clear()
                        Values.Add(oldvalue(0)) '0
                        Values.Add(oldvalue(1)) '1
                        If actconname.Contains("Researched") Then
                            Values.Add(0) '2 현재값:0 최대값:1
                        Else
                            Values.Add(1) '2 현재값:0 최대값:1
                        End If
                        Values.Add(oldvalue(2)) '3
                        If actconname.Contains("Set") Then
                            Values.Add(0) '4 대입:0 덧셈:1
                        Else
                            Values.Add(1) '4 대입:0 덧셈:1
                        End If

                        Compmsg = Compmsg & """" & actconname & "(" & valuesstr & ")" & """" & vbCrLf
                End Select
            Case ElementType.조건
                'con = SeachCon(NextLine(tempstr(_index), _index))
                Select Case actconname
                    Case "TechnologiesAvailable", "TechnologiesResearched", "UpgradesAvailable", "UpgradesResearched"
                        Dim valuesstr As String = ""

                        For i = 0 To Values.Count - 1
                            If i = 0 Then
                                valuesstr = Values(i)
                            Else
                                valuesstr = valuesstr & ", " & Values(i)
                            End If
                        Next

                        If actconname.Contains("Upgrade") Then
                            con = SeachCon("Upgrade")
                        Else
                            con = SeachCon("Tech")
                        End If

                        '0"PlayerX",
                        '1"Upgrade",
                        '2"Count"
                        '3"VariableComparison"

                        'To

                        '0"PlayerX",
                        '1"Upgrade",
                        '2"UTType",
                        '3"Count",
                        '4"VariableComparison"

                        Dim oldvalue As New List(Of String)
                        oldvalue.AddRange(Values)

                        Values.Clear()
                        Values.Add(oldvalue(0)) '0
                        Values.Add(oldvalue(1)) '1
                        If actconname.Contains("Researched") Then
                            Values.Add(0) '2 현재값:0 최대값:1
                        Else
                            Values.Add(1) '2 현재값:0 최대값:1
                        End If
                        Values.Add(oldvalue(2)) '3
                        Values.Add(oldvalue(3)) '4

                        Compmsg = Compmsg & """" & actconname & "(" & valuesstr & ")" & """" & vbCrLf
                End Select
            Case ElementType.Foluder
                If Values.Count = 1 Then
                    Values(0) = "//" & Values(0)
                    Values.Add(Values(0))
                    Values.Add("False")
                End If
        End Select

        If Compmsg.Length <> 0 Then
            MsgBox(Compmsg & "다음 코드들이 호환성에 맞게 변경됩니다.", MsgBoxStyle.Exclamation, "TriggerEditor 자동변환")
        End If


        '호환성코드


        Dim elecount As Integer = NextLine(tempstr(_index), _index)


        For i = 0 To elecount - 1
            Dim _ele As New Element(Me, ElementType.main)
            _index = _ele.LoadFile(_str, _index, isfrist)



            If _ele.GetTypeV = ElementType.함수정의 Then
                Dim flag As Boolean = True
                For k = 0 To functions.GetElementsCount - 1
                    If functions.GetElements(k).Values(0) = _ele.Values(0) Then
                        flag = False
                        Exit For
                    End If
                Next
                If flag Then
                    Elements.Add(_ele)
                End If
            Else
                Elements.Add(_ele)
            End If

        Next
        NextLine(tempstr(_index), _index)

        Return _index
    End Function




    Public Function GetVariables(lastlist As List(Of String)) As List(Of String)
        Dim Variables As New List(Of String)

        If lastlist Is Nothing Then
            lastlist = New List(Of String)
        End If

        If Parrent IsNot Nothing Then
            If Parrent.GetTypeV = ElementType.코드 Then
                '그렇다면...
                For i = 0 To Parrent.Parrent.Elements(0).Elements.Count - 1
                    Variables.Add(Parrent.Parrent.Elements(0).Elements(i).Values(0))
                Next
            ElseIf GetTypeV() = ElementType.코드 Then
                '그렇다면...
                For i = 0 To Parrent.Elements(0).Elements.Count - 1
                    Variables.Add(Parrent.Elements(0).Elements(i).Values(0))
                Next

            End If

            '우선 나에 대해서 내 위치를 찾아라!
            Dim mylocation As Integer = Parrent.Elements.IndexOf(Me)

            '내 위치가 0이 될때 까지 부모에서 조사하라
            For i = mylocation To 0 Step -1
                If Parrent.Elements(i).Type = ElementType.액션 Then
                    If Parrent.Elements(i).act.Name = "CreateVariable" Or Parrent.Elements(i).act.Name = "CreateVariableWithNoini" Then
                        If lastlist.Contains(Parrent.Elements(i).Values(0)) = False Then
                            Variables.Add(Parrent.Elements(i).Values(0))
                        End If
                    End If
                    If Parrent.Elements(i).act.Name = "CreatePlayerVariable" Then
                        If lastlist.Contains(Parrent.Elements(i).Values(0)) = False Then
                            Variables.Add(Parrent.Elements(i).Values(0) & "[getcurpl()]")
                        End If
                    End If
                End If
                If Parrent.Type = ElementType.포 Then
                    If Parrent.Values(0) = "Counting" Then
                        If lastlist.Contains(Parrent.Values(1)) = False Then
                            Variables.Add(Parrent.Values(1))
                        End If
                    End If
                    If Parrent.Values(0) = "AllUnit" Then
                        If lastlist.Contains("epd") = False Then
                            Variables.Add("epd")
                        End If
                        If lastlist.Contains("ptr") = False Then
                            Variables.Add("ptr")
                        End If
                    End If

                End If
            Next

            If Parrent.Type <> ElementType.main Then
                Variables.AddRange(Parrent.GetVariables(Variables))
            End If
        End If

        Return Variables
    End Function




    Public Sub New(tparrent As Element, stype As ElementType)
        Elements = New List(Of Element)
        Type = stype
        Parrent = tparrent

        If Type = ElementType.조건문if Then
            Elements.Add(New Element(Me, ElementType.조건절))
            Elements.Last.Values = New List(Of String)
            Elements.Last.Values.Add("And")
            Elements.Add(New Element(Me, ElementType.만족))
        ElseIf Type = ElementType.조건문ifelse Then
            Elements.Add(New Element(Me, ElementType.조건절))
            Elements.Last.Values = New List(Of String)
            Elements.Last.Values.Add("And")
            Elements.Add(New Element(Me, ElementType.만족))
            Elements.Add(New Element(Me, ElementType.만족안함))
        ElseIf Type = ElementType.와일 Then
            Elements.Add(New Element(Me, ElementType.와일조건))
            Elements.Last.Values = New List(Of String)
            Elements.Last.Values.Add("And")
            Elements.Add(New Element(Me, ElementType.와일만족))
        ElseIf Type = ElementType.포 Then
            Elements.Add(New Element(Me, ElementType.포만족))
            Values = New List(Of String)
            Values.Add("Counting")
            Values.Add("i")
            Values.Add("10")
        ElseIf Type = ElementType.함수정의 Then
            Elements.Add(New Element(Me, ElementType.인수))
            Elements.Add(New Element(Me, ElementType.코드))
        ElseIf Type = ElementType.Foluder Then
            Elements.Add(New Element(Me, ElementType.FoluderAction))
        ElseIf Type = ElementType.RawString Then
            Values.Add("")
        ElseIf Type = ElementType.RawTrigger Then
            Values.Add(0)
            Elements.Add(New Element(Me, ElementType.TriggerCond))
            Elements.Last.Values = New List(Of String)
            Elements.Last.Values.Add("And")
            Elements.Add(New Element(Me, ElementType.TriggerAct))
        End If
    End Sub
    Public Sub New(tparrent As Element, stype As ElementType, actcon As Integer, Optional _value() As String = Nothing)
        Elements = New List(Of Element)
        Type = stype
        Parrent = tparrent

        If stype = ElementType.액션 Then
            act = Actions(actcon)
            If _value IsNot Nothing Then
                Values.AddRange(_value)
            Else
                Values.AddRange(act.ValuesDef)
            End If
        End If
        If stype = ElementType.조건 Then
            con = Condictions(actcon)
            If _value IsNot Nothing Then
                Values.AddRange(_value)
            Else
                Values.AddRange(con.ValuesDef)
            End If
        End If
    End Sub
    Public Sub New(tparrent As Element, stype As ElementType, _act As Action, Optional _value() As String = Nothing)
        Elements = New List(Of Element)
        Type = stype
        Parrent = tparrent

        If stype = ElementType.액션 Then
            act = _act
            If _value IsNot Nothing Then
                Values.AddRange(_value)
            Else
                Values.AddRange(act.ValuesDef)
            End If
        End If
    End Sub
    Public Sub New(tparrent As Element, stype As ElementType, _con As Condiction, Optional _value() As String = Nothing)
        Elements = New List(Of Element)
        Type = stype
        Parrent = tparrent

        If stype = ElementType.조건 Then
            con = _con
            If _value IsNot Nothing Then
                Values.AddRange(_value)
            Else
                Values.AddRange(con.ValuesDef)
            End If
        End If
    End Sub
    Public Sub New(tparrent As Element, stype As ElementType, Optional _value() As String = Nothing)
        Elements = New List(Of Element)
        Type = stype
        Parrent = tparrent

        If _value IsNot Nothing Then
            Values.AddRange(_value)
        End If
        If Type = ElementType.Foluder Then
            Elements.Add(New Element(Me, ElementType.FoluderAction))
        End If
    End Sub

    Public Function GetTypeName() As String
        Return ElementNames(Type)
    End Function
    Public Function GetTypeV() As ElementType
        Return Type
    End Function
    '조건문(If, If else)
    'if의 요소는 각각 1.조건들. 2.조건 만족시 실행할 요소들
    'if else는 각각 1.조건들. 2.조건 만족시 실행할 요소들. 3.조건 불만족시 실행할 요소들

    '조건 벨류와 조건 타입만 소지(내부적으로 요소들을 가지고 있지 않음)
    '액션 벨류와 액션 타입만 소지(내부적으로 요소들을 가지고 있지 않음)


    '제어문
    'While, for, switch등
    Public Sub RemoveAt(index As Integer)
        'trigtasklist.Add(New Trigtask(Trigtask.Tasktype.delete, Elements(index).Clone, Elements(index).Getindex))
        Elements.RemoveAt(index)
    End Sub


    Public Sub Delete()
        'trigtasklist.Add(New Trigtask(Trigtask.Tasktype.delete, Clone, Getindex))
        Parrent.Elements.Remove(Me)
    End Sub


    Public Function Clone(Optional _parrentEle As Element = Nothing) As Element
        If _parrentEle Is Nothing Then
            _parrentEle = Parrent
        End If

        Dim newEle As Element

        If Type = ElementType.액션 Then
            newEle = New Element(_parrentEle, Type, act, Values.ToArray)
            'newEle.Values.AddRange(Values)
        ElseIf Type = ElementType.조건 Then
            newEle = New Element(_parrentEle, Type, con, Values.ToArray)
            'newEle.Values.AddRange(Values)
        Else
            newEle = New Element(_parrentEle, Type)
        End If
        newEle.SetValue(Values.ToArray)


        For i = 0 To Elements.Count - 1
            If newEle.Elements.Count <= i Then
                newEle.Elements.Add(Elements(i).Clone(newEle))
            Else
                newEle.Elements(i) = Elements(i).Clone(newEle)
            End If
        Next
        newEle.isdisalbe = isdisalbe
        newEle.isFloding = isFloding
        newEle.isNotcon = isNotcon

        Return newEle
    End Function
    Public Sub Copy(_copyele As Element)
        Type = _copyele.Type
        act = _copyele.act
        con = _copyele.con

        Values.Clear()
        Values.AddRange(_copyele.Values.ToList)
        Parrent = _copyele.Parrent

        Elements.Clear()

        For i = 0 To _copyele.Elements.Count - 1
            Elements.Add(_copyele.Elements(i).Clone())
        Next
    End Sub


    Public Sub AddElements(stype As ElementType)
        Elements.Add(New Element(Me, stype))
    End Sub
    Public Sub AddElements(stype As ElementType, _actcon As Integer)
        Elements.Add(New Element(Me, stype, _actcon))
    End Sub

    Public Sub AddElements(loc As Integer, stype As ElementType)
        Elements.Insert(loc, New Element(Me, stype))
    End Sub
    Public Sub AddElements(loc As Integer, stype As ElementType, _actcon As Integer)
        Elements.Insert(loc, New Element(Me, stype, _actcon))
    End Sub
    Public Sub AddElements(loc As Integer, _ele As Element)
        Elements.Insert(loc, _ele.Clone(Me))
    End Sub
    Public Sub AddElements(_ele As Element)
        Elements.Add(_ele.Clone(Me))
    End Sub


    Public Sub SetValue(_value() As String)
        Values.Clear()
        Values.AddRange(_value.ToArray)
    End Sub
    Public Sub SetValue(_index As Integer, _value As String)
        Values(_index) = _value
    End Sub
    Public Function GetValues() As List(Of String)
        Return Values
    End Function

    Public Function GetElementsCount() As Integer
        Return Elements.Count
    End Function
    Public Function GetElements(num As UInteger) As Element
        Return Elements(num)
    End Function
    Public Function GetElementList() As List(Of Element)
        Return Elements
    End Function



    Public Sub ReDrawColor()
        If isdisalbe Then
            CTreeNode.BackColor = Color.Gray
        Else
            CTreeNode.BackColor = Nothing
        End If
    End Sub


    Public Function ToTreeNode(Optional _isdisalbe As Boolean = False) As TreeNode
        Dim RTreeNode As TreeNode
        Dim ishiddencheckbox As Boolean = True

        Select Case Type
            Case ElementType.조건, ElementType.액션, ElementType.함수, ElementType.Wait, ElementType.RawTrigger, ElementType.Foluder, ElementType.조건문if, ElementType.조건문ifelse, ElementType.포, ElementType.와일
                ishiddencheckbox = False
        End Select



        If Type <> ElementType.main Then
            Dim text As String = ""
            Try
                text = GetText()
            Catch ex As Exception

            End Try
            RTreeNode = New TreeNode(text)
            If ishiddencheckbox Then
                RTreeNode.ImageIndex = 2
            Else
                RTreeNode.ImageIndex = 1
                RTreeNode.SelectedImageIndex = 1
            End If

            Select Case Type
                Case ElementType.조건, ElementType.액션, ElementType.Functions, ElementType.RawTriggers
                    RTreeNode.ForeColor = Color.White
                Case ElementType.조건문if, ElementType.조건문ifelse, ElementType.와일, ElementType.포, ElementType.함수정의
                    RTreeNode.ForeColor = Color.LightPink
                Case ElementType.Foluder, ElementType.FoluderAction
                    RTreeNode.ForeColor = Color.LightGreen
                Case ElementType.RawTrigger
                    RTreeNode.ForeColor = Color.CadetBlue
                Case ElementType.함수
                    If CheckFunc(Values(0)) Then
                        RTreeNode.ForeColor = Color.DodgerBlue
                    Else
                        RTreeNode.ForeColor = Color.Red
                    End If
                Case ElementType.Switchcase
                    RTreeNode.ForeColor = Color.GreenYellow
                Case ElementType.Switch
                    RTreeNode.ForeColor = Color.GreenYellow
                Case Else
                    RTreeNode.ForeColor = Color.LightBlue
            End Select


            RTreeNode.Tag = Me
        Else
            RTreeNode = New TreeNode
            RTreeNode.ImageIndex = 2
        End If


        'MsgBox(Elements.Count)
        For i = 0 To Elements.Count - 1
            RTreeNode.Nodes.Add(Elements(i).ToTreeNode())
        Next

        RTreeNode.Checked = Not isdisalbe
        If isdisalbe Or _isdisalbe Then
            RTreeNode.BackColor = Color.Gray
            RTreeNode.ImageIndex = 3
            RTreeNode.SelectedImageIndex = 3
        End If
        If isNotcon Then
            RTreeNode.ImageIndex = 4
            RTreeNode.SelectedImageIndex = 4
        End If

        If isFloding = False Then
            RTreeNode.Expand()
        End If
        CTreeNode = RTreeNode
        Return RTreeNode
    End Function


    Public Function GetFuncToolTips() As String()
        Dim returnstr As New List(Of String)
        Dim factors As New List(Of String)

        Dim funcdef As Element = GetFunc(Values(0))
        For i = 0 To funcdef.Elements(0).GetElementsCount - 1
            factors.Add(funcdef.Elements(0).Elements(i).Values(0))
        Next

        If funcdef.Elements(1).GetElementsCount <> 0 Then
            Try
                If (funcdef.Elements(1).Elements(0).GetTypeV = ElementType.Foluder) Then
                    If (funcdef.Elements(1).Elements(0).Values(0) = "/*ToolTip") Then
                        Dim tooltipdef As Element = Nothing

                        '언어선택
                        For i = 0 To funcdef.Elements(1).Elements(0).Elements(0).GetElementsCount - 1
                            If funcdef.Elements(1).Elements(0).Elements(0).GetElements(i).Values(0) = My.Settings.Langage Then
                                tooltipdef = funcdef.Elements(1).Elements(0).Elements(0).GetElements(i).Elements(0)
                                Exit For
                            End If
                        Next
                        If tooltipdef Is Nothing Then
                            Return returnstr.ToArray
                        End If

                        For i = 0 To tooltipdef.GetElementsCount - 1
                            If tooltipdef.Elements(i).GetTypeV = ElementType.액션 Then
                                If tooltipdef.Elements(i).act.Name = "CreateVariableWithNoini" Then
                                    Dim Tooltipstr As String = tooltipdef.Elements(i).Values(0)
                                    If factors.Contains(Tooltipstr) Then '인자를 포함하고 있을 경우
                                        returnstr.Add("$" & factors.IndexOf(Tooltipstr) & "$")
                                    End If
                                ElseIf tooltipdef.Elements(i).act.Name = "RawCode" Then
                                    Dim Tooltipstr As String = tooltipdef.Elements(i).Values(0)
                                    returnstr.Add(Tooltipstr)
                                End If
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                returnstr.Clear()
                Return returnstr.ToArray
            End Try
        Else
            Return returnstr.ToArray
        End If




        Return returnstr.ToArray
    End Function



    Public Function GetText() As String
        Dim _rtext As String = ""


        Select Case Type
            Case ElementType.Switch
                _rtext = ElementNames(Type) & Values(0)
            Case ElementType.Switchcase
                If Values(0) = -1 Then
                    _rtext = ElementNames(Type) & "Default"
                Else
                    _rtext = ElementNames(Type) & Values(0)
                End If


                _rtext = _rtext & Lan.GetText("Trigger", "SwitchCase") & Values(1)

            Case ElementType.Foluder
                _rtext = Values(0) & " : "
            Case ElementType.액션
                _rtext = act.Text

                Dim bexit As Boolean = False
                While (bexit = False)
                    For i = 0 To act.ValuesDef.Count - 1
                        _rtext = Replace(_rtext,  "$" & act.ValuesDef(i) & "$", "(" & ValueParser(i) & ")", , 1)
                    Next
                    bexit = True
                    For i = 0 To act.ValuesDef.Count - 1
                        If _rtext.Contains("$" & act.ValuesDef(i) & "$") Then
                            bexit = False
                        End If
                    Next
                End While


            Case ElementType.조건
                _rtext = con.Text

                Dim bexit As Boolean = False
                While (bexit = False)
                    For i = 0 To con.ValuesDef.Count - 1
                        _rtext = Replace(_rtext, "$" & con.ValuesDef(i) & "$", "(" & ValueParser(i) & ")", , 1)
                    Next
                    bexit = True
                    For i = 0 To con.ValuesDef.Count - 1
                        If _rtext.Contains("$" & con.ValuesDef(i) & "$") Then
                            bexit = False
                        End If
                    Next
                End While

            Case ElementType.포
                Select Case Values(0)
                    Case "Counting"
                        _rtext = Lan.GetText("Trigger", "For").Replace("$1$", Values(1)).Replace("$2$", Values(2))
                    Case "Custom"
                        _rtext = "For(" & Values(1) & ")"
                    Case "AllUnit"
                        Dim _playertext As String
                        Select Case Values(1)
                            Case 0
                                _playertext = Lan.GetText("Trigger", "AllPlayer")
                            Case 13
                                _playertext = Lan.GetText("Trigger", "AllPlayer")
                            Case Else
                                _playertext = "Player " & Values(1)
                        End Select

                        _rtext = Lan.GetText("Trigger", "Foreach").Replace("$1$", _playertext)'"Foreach(" & _playertext & "의 유닛을 epd, ptr변수로 순환합니다.)"
                    Case "PlayerLoop"
                        Dim tname As String = ""

                        Dim _array() As String = Values(1).Split(",")
                        Dim _playerArray As New List(Of Byte)

                        For i = 0 To 7
                            If _array(i) = True Then
                                _playerArray.Add(i)
                            End If
                        Next

                        For i = 8 To 11
                            If _array(i) = True Then
                                If ProjectSet.CHKFORCEDATA(i - 8).Count > 1 Then
                                    For j = 1 To ProjectSet.CHKFORCEDATA(i - 8).Count - 1
                                        _playerArray.Add(ProjectSet.CHKFORCEDATA(i - 8)(j))
                                    Next
                                End If
                            End If
                        Next

                        For i = 0 To 7
                            If _playerArray.Contains(i) Then
                                tname = tname & "P" & i + 1 & " "
                            End If
                        Next

                        _rtext = Lan.GetText("Trigger", "PlayerLoop").Replace("$1$", tname) 'tname & "를 순환합니다."
                End Select
            Case ElementType.함수정의
                _rtext = Lan.GetText("Trigger", "FuncDef").Replace("$1$", Values(0)).Replace("$2$", Values(1)) ' "함수정의 : " & Values(0) & "        대기하기 사용 : " & Values(1)
            Case ElementType.함수
                Dim existFlag As Boolean = False

                If CheckFunc(Values(0)) Then
                    existFlag = True
                Else
                    existFlag = False
                End If



                If existFlag Then
                    '주석 가능한지 체크
                    Dim FToolTip As String() = GetFuncToolTips()

                    If FToolTip.Length <> 0 Then
                        _rtext = Lan.GetText("Trigger", "Func") & " : "

                        Dim tempstr As String = ""
                        For i = 0 To FToolTip.Count - 1
                            Dim num As Integer
                            Try
                                num = FToolTip(i).Replace("$", "")
                                _rtext = _rtext & "(" & ValueParser(Values(1 + num), GetFuncDEf(1 + num)) & ")"
                            Catch ex As Exception
                                _rtext = _rtext & FToolTip(i) & " "
                            End Try
                        Next
                    Else
                            _rtext = Lan.GetText("Trigger", "Func") & " : " & Values(0) & "("
                            Dim valdef As String = ""

                            If Values.Count <> 1 Then
                                _rtext = _rtext & ValueParser(Values(1), GetFuncDEf(1))
                                For i = 2 To Values.Count - 1
                                    _rtext = _rtext & "," & ValueParser(Values(i), GetFuncDEf(i))
                                Next
                            End If
                            _rtext = _rtext & ")"
                    End If
                Else
                    _rtext = Lan.GetText("Trigger", "ExistFunc") & " : " & Values(0)
                End If



            Case ElementType.조건절
                _rtext = ElementNames(Type) & " " & Values(0)
            Case ElementType.와일조건
                _rtext = ElementNames(Type) & " " & Values(0)
            Case ElementType.Wait
                _rtext = Lan.GetText("Trigger", "Wait") & " : " & Values(0)
            Case ElementType.RawTrigger
                _rtext = "ClassicTrigger :"
                For i = 0 To Elements(1).GetElementsCount - 1
                    If Elements(1).GetElements(i).GetTypeV = ElementType.액션 Then
                        If Elements(1).GetElements(i).act.Name = "Comment" Then
                            _rtext = Elements(1).GetElements(i).Values(0) & " :"
                        End If
                    End If
                Next




                For i = 0 To 12
                    If (Values(0) And Math.Pow(2, i)) > 0 Then
                        If (8 <= i) And (i <= 11) Then
                            _rtext = _rtext & " Force" & i - 7
                        ElseIf i = 12 Then
                            _rtext = _rtext & " AllPlayers"
                        Else
                            _rtext = _rtext & " P" & i + 1
                        End If
                    End If
                Next
            Case Else
                _rtext = ElementNames(Type)
        End Select

        Return _rtext
    End Function


    Private Function GetFuncDEf(_index As Integer) As String
        Try
            For i = 0 To functions.GetElementsCount - 1
                If functions.Elements(i).Values(0) = Values(0) Then
                    Return ValueDefiniction(functions.Elements(i).Elements(0).Elements(_index - 1).Values(1)).Name(0)

                End If
            Next
        Catch ex As Exception

        End Try

        Return "Error"
    End Function



    Private Function GetIntend(count As Integer) As String
        Dim tabstring As String = ""
        For i = 0 To count * PadWidth - 1
            tabstring = tabstring & " "
        Next
        Return tabstring
    End Function


    Public Function GetCode() As String
        Dim _rtext As String = ""

        If Type = ElementType.액션 Or Type = ElementType.조건 Then
            If Type = ElementType.액션 Then
                _rtext = act.CodeText


                If act.Name = "SetDatfile" Or act.Name = "SetVariableDatFile" Or act.Name = "AddDatfile" Then
                    Try
                        Dim num As Integer = CInt(GetValue("DatFile"))
                        Dim Tvalue As Integer = CInt(GetValue("OffsetName"))
                        'Dim typeName As String = DatEditDATA(num).keyDic.Keys.ToList(_value)

                        Dim _size As Integer = DatEditDATA(num).keyINFO(Tvalue).realSize

                        Select Case _size
                            Case 4
                                _rtext = _rtext.Replace("$writedef$", "dw")
                            Case 2
                                _rtext = _rtext.Replace("$writedef$", "w")
                            Case 1
                                _rtext = _rtext.Replace("$writedef$", "b")
                        End Select

                        _rtext = _rtext.Replace("&SIZE&", _size)
                    Catch ex As Exception
                        _rtext = _rtext.Replace("$writedef$", "dw")
                    End Try

                ElseIf act.Name = "SetCUnitData" Or act.Name = "SetVariableCUnitData" Or act.Name = "AddCUnitData" Or act.Name = "SetCUnitDataEPD" Or act.Name = "SetVariableCUnitDataEPD" Or act.Name = "AddCUnitDataEPD" Then
                    Try
                        '스트럭쳐 포인터...
                        Dim num As Integer = CInt(GetValue("StructOffset"))
                        Dim num1 As Integer = Val("&H" & CUnitData(num)(0))

                        Dim num2 As Integer = num + 1
                        Try
                            num2 = Val("&H" & CUnitData(num + 1)(0))
                        Catch ex As Exception

                        End Try

                        Dim _size As Integer = num2 - num1

                        If CUnitData(num).Length = 4 Then
                            _size = CUnitData(num)(3)
                        End If




                        'TrigEditorForm.Text = (num & " " & num1 & " " & num2)

                        Select Case _size
                            Case 4
                                _rtext = _rtext.Replace("$writedef$", "dw")
                            Case 2
                                _rtext = _rtext.Replace("$writedef$", "w")
                            Case 1
                                _rtext = _rtext.Replace("$writedef$", "b")
                        End Select

                        Select Case act.Name
                            Case "SetCUnitDataEPD", "AddCUnitDataEPD"
                                _rtext = _rtext.Replace("$Byte$", Math.Pow(256, num1 Mod 4))
                        End Select


                        If (act.Name = "SetCUnitDataEPD" Or act.Name = "SetVariableCUnitDataEPD") And _size = 4 Then
                            _rtext = _rtext.Split("|")(1)
                        Else
                            _rtext = _rtext.Split("|")(0)
                        End If
                    Catch ex As Exception
                        _rtext = _rtext.Replace("$writedef$", "dw")
                    End Try
                ElseIf act.Name = "SetButton" Then
                    Try
                        Dim valuestream As String = GetValue("BtnData")
                        Dim index As Integer = 1

                        Dim value As UInteger
                        For i = 0 To 4
                            value = 0
                            For j = 0 To 3
                                value += Mid(valuestream, index, 3) * Math.Pow(256, j)
                                index += 3
                            Next


                            _rtext = _rtext.Replace("Offset" & i + 1, value)
                        Next
                    Catch ex As Exception

                    End Try
                ElseIf act.Name = "ChangeStarText" Then
                    Try
                        Dim statindex As Integer = Values(0) - 1
                        Dim index As Integer = Values(1)
                        Dim stattext As String
                        If stattextdic.ContainsKey(statindex) Then
                            stattext = stattextdic(statindex)
                        Else
                            stattext = stat_txt(statindex)
                        End If
                        Dim len As Byte = GettblLen(stattext, index)
                        Dim start As Byte = GettblStart(stattext, index)

                        Dim dummystr As String = ""
                        For i = 0 To len - 1
                            dummystr = dummystr & "\x0D"
                        Next

                        _rtext = _rtext.Replace("Dummy", """" & dummystr & """")
                        _rtext = _rtext.Replace("Offset", start)
                        _rtext = _rtext.Replace("len", len)
                    Catch ex As Exception

                    End Try
                ElseIf act.Name = "ChangeButtonEnableMsg" Then
                    Try
                        Dim statindex As Integer = CInt(ValueParser(Values(0), "BtnEnableTxt", True)) - 1
                        Dim index As Integer = Values(1)
                        Dim stattext As String
                        If stattextdic.ContainsKey(statindex) Then
                            stattext = stattextdic(statindex)
                        Else
                            stattext = stat_txt(statindex)
                        End If
                        Dim len As Byte = GettblLen(stattext, index)
                        Dim start As Byte = GettblStart(stattext, index)

                        Dim dummystr As String = ""
                        For i = 0 To len - 1
                            dummystr = dummystr & "\x0D"
                        Next

                        _rtext = _rtext.Replace("Dummy", """" & dummystr & """")
                        _rtext = _rtext.Replace("Offset", start)
                        _rtext = _rtext.Replace("len", len)
                    Catch ex As Exception

                    End Try
                ElseIf act.Name = "ChangeButtonUnEnableMsg" Then
                    Try
                        Dim statindex As Integer = CInt(ValueParser(Values(0), "BtnUnEnableTxt", True)) - 1
                        Dim index As Integer = Values(1)
                        Dim stattext As String
                        If stattextdic.ContainsKey(statindex) Then
                            stattext = stattextdic(statindex)
                        Else
                            stattext = stat_txt(statindex)
                        End If
                        Dim len As Byte = GettblLen(stattext, index)
                        Dim start As Byte = GettblStart(stattext, index)

                        Dim dummystr As String = ""
                        For i = 0 To len - 1
                            dummystr = dummystr & "\x0D"
                        Next

                        _rtext = _rtext.Replace("Dummy", """" & dummystr & """")
                        _rtext = _rtext.Replace("Offset", start)
                        _rtext = _rtext.Replace("len", len)
                    Catch ex As Exception

                    End Try
                ElseIf act.Name = "DisplayCText" Then
                    If Values(1) = "1" Then
                        _rtext = "txtPtr = dwread_epd_safe(EPD(0x640B58));" & vbCrLf & _rtext & ";" & vbCrLf & "SetMemory(0x640B58, SetTo, txtPtr);"
                    End If
                ElseIf act.Name = "DisplaySavedCText" Then
                    If Values(0) = "1" Then
                        _rtext = "txtPtr = dwread_epd_safe(EPD(0x640B58));" & vbCrLf & _rtext & ";" & vbCrLf & "SetMemory(0x640B58, SetTo, txtPtr);"
                    End If
                ElseIf act.Name = "SetUpgrade" Then
                    '0"PlayerX",
                    '1"Upgrade",
                    '2"UTType",
                    '3"Count",
                    '4"Modifier"
                    If Values(4) = 0 Then
                        _rtext = _rtext.Split("|")(1)
                    Else
                        _rtext = _rtext.Split("|")(0)
                    End If


                    If Values(2) = 0 Then '현재 업그레이드 수치일 경우 58D2B0, 58F32C /차이 207C
                        _rtext = _rtext.Replace("$Offset$", "0x58D2B0 + 0x207C * ($Upgrade$ / 46)")
                    Else '아닐 경우 58D088, 58F278 /차이 21F0
                        _rtext = _rtext.Replace("$Offset$", "0x58D088 + 0x21F0 * ($Upgrade$ / 46)")
                    End If
                ElseIf act.Name = "SetTech" Then
                    '0"PlayerX",
                    '1"Upgrade",
                    '2"UTType",
                    '3"Count",
                    '4"Modifier"
                    If Values(4) = 0 Then
                        _rtext = _rtext.Split("|")(1)
                    Else
                        _rtext = _rtext.Split("|")(0)
                    End If


                    If Values(2) = 0 Then '현재 업그레이드 수치일 경우 58CF44, 58F140
                        _rtext = _rtext.Replace("$Offset$", "0x58CF44 + 0x21FC * ($Techdata$ / 24)")
                    Else '아닐 경우 58CE24, 58F050
                        _rtext = _rtext.Replace("$Offset$", "0x58CE24 + 0x222C * ($Techdata$ / 24)")
                    End If
                End If

                Dim bexit As Boolean = False
                While (bexit = False)
                    For i = 0 To act.ValuesDef.Count - 1
                        _rtext = Replace(_rtext, "$" & act.ValuesDef(i) & "$", ValueParser(i, True), , 1) 'ValueParser(i, True)
                    Next
                    bexit = True
                    For i = 0 To act.ValuesDef.Count - 1
                        If _rtext.Contains("$" & act.ValuesDef(i) & "$") Then
                            bexit = False
                        End If
                    Next
                End While


            ElseIf Type = ElementType.조건 Then
                _rtext = con.CodeText

                If isNotcon Then
                    _rtext = "!" & _rtext
                End If

                If con.Name = "Datfile" Then
                    Try
                        Dim num As Integer = CInt(GetValue("DatFile"))
                        Dim Tvalue As Integer = CInt(GetValue("OffsetName"))
                        'Dim typeName As String = DatEditDATA(num).keyDic.Keys.ToList(_value)

                        Dim _size As Integer = DatEditDATA(num).keyINFO(Tvalue).realSize

                        Select Case _size
                            Case 4
                                _rtext = _rtext.Replace("$writedef$", "dw")
                            Case 2
                                _rtext = _rtext.Replace("$writedef$", "w")
                            Case 1
                                _rtext = _rtext.Replace("$writedef$", "b")
                        End Select

                        _rtext = _rtext.Replace("&SIZE&", _size)
                    Catch ex As Exception
                        _rtext = _rtext.Replace("$writedef$", "dw")
                        _rtext = _rtext.Replace("&SIZE&", 4)
                    End Try
                ElseIf con.Name = "CUnitData" Or con.Name = "CUnitDataEPD" Then
                    Try
                        '스트럭쳐 포인터...
                        Dim num As Integer = CInt(GetValue("StructOffset"))
                        Dim num1 As Integer = Val("&H" & CUnitData(num)(0))

                        Dim num2 As Integer = num + 1
                        Try
                            num2 = Val("&H" & CUnitData(num + 1)(0))
                        Catch ex As Exception

                        End Try

                        Dim _size As Integer = num2 - num1


                        'TrigEditorForm.Text = (num & " " & num1 & " " & num2)

                        Select Case _size
                            Case 4
                                _rtext = _rtext.Replace("$writedef$", "dw")
                            Case 2
                                _rtext = _rtext.Replace("$writedef$", "w")
                            Case 1
                                _rtext = _rtext.Replace("$writedef$", "b")
                        End Select

                        If con.Name = "CUnitDataEPD" And _size = 4 Then
                            _rtext = _rtext.Split("|")(1)
                            Dim Comp As Integer = CInt(GetValue("VariableComparison"))
                            Select Case Comp
                                Case 0 '일치
                                    _rtext = _rtext.Replace("$NotFlag$", "")
                                    _rtext = _rtext.Replace("$Comparison$", "Exactly")
                                Case 1 '불일치
                                    _rtext = _rtext.Replace("$NotFlag$", "!")
                                    _rtext = _rtext.Replace("$Comparison$", "Exactly")
                                Case 2 '이상
                                    _rtext = _rtext.Replace("$NotFlag$", "")
                                    _rtext = _rtext.Replace("$Comparison$", "AtLeast")
                                Case 3 '이하
                                    _rtext = _rtext.Replace("$NotFlag$", "")
                                    _rtext = _rtext.Replace("$Comparison$", "AtMost")
                                Case 4 '초과
                                    _rtext = _rtext.Replace("$NotFlag$", "!")
                                    _rtext = _rtext.Replace("$Comparison$", "AtMost")
                                Case 5 '미만
                                    _rtext = _rtext.Replace("$NotFlag$", "!")
                                    _rtext = _rtext.Replace("$Comparison$", "AtLeast")
                            End Select






                        Else
                            _rtext = _rtext.Split("|")(0)
                        End If
                    Catch ex As Exception
                        _rtext = _rtext.Replace("$writedef$", "dw")
                    End Try
                ElseIf con.Name = "Upgrade" Then
                    '0"PlayerX",
                    '1"Upgrade",
                    '2"UTType",
                    '3"Count",
                    '4"Modifier"

                    If Values(2) = 0 Then '현재 업그레이드 수치일 경우 58D2B0, 58F32C /차이 207C
                        _rtext = _rtext.Replace("$Offset$", "0x58D2B0 + 0x207C * ($Upgrade$ / 46)")
                    Else '아닐 경우 58D088, 58F278 /차이 21F0
                        _rtext = _rtext.Replace("$Offset$", "0x58D088 + 0x21F0 * ($Upgrade$ / 46)")
                    End If
                ElseIf con.Name = "Tech" Then
                    '0"PlayerX",
                    '1"Upgrade",
                    '2"UTType",
                    '3"Count",
                    '4"Modifier"

                    If Values(2) = 0 Then '현재 업그레이드 수치일 경우 58CF44, 58F140
                        _rtext = _rtext.Replace("$Offset$", "0x58CF44 + 0x21FC * ($Techdata$ / 24)")
                    Else '아닐 경우 58CE24, 58F050
                        _rtext = _rtext.Replace("$Offset$", "0x58CE24 + 0x222C * ($Techdata$ / 24)")
                    End If
                End If

                Dim bexit As Boolean = False
                While (bexit = False)
                    For i = 0 To con.ValuesDef.Count - 1
                        _rtext = Replace(_rtext, "$" & con.ValuesDef(i) & "$", ValueParser(i, True), , 1)
                    Next
                    bexit = True
                    For i = 0 To con.ValuesDef.Count - 1
                        If _rtext.Contains("$" & con.ValuesDef(i) & "$") Then
                            bexit = False
                        End If
                    Next
                End While
            End If
        Else
            _rtext = ElementNames(Type)
            Select Case Type
                Case ElementType.Switch
                    _rtext = "EUDSwitch(" & Values(0) & ");"
                Case ElementType.Switchcase
                    If Values(0) = -1 Then
                        _rtext = "EUDSwitchDefault()();"
                    Else
                        _rtext = "EUDSwitchCase()(" & Values(0) & ");"
                    End If
                Case ElementType.Foluder
                    _rtext = Values(0)
                Case ElementType.조건문if, ElementType.조건문ifelse, ElementType.와일
                    _rtext = ""
                Case ElementType.조건절, ElementType.TriggerCond
                    _rtext = "if("
                Case ElementType.만족안함
                    _rtext = "else{"
                Case ElementType.만족
                    _rtext = "){"
                Case ElementType.와일조건
                    _rtext = "while("
                Case ElementType.와일만족
                    _rtext = "){"
                Case ElementType.포
                    Select Case Values(0)
                        Case "Counting"
                            _rtext = "for(var " & Values(1) & " = 0 ; " & Values(1) & " < " & Values(2) & " ; " & Values(1) & "++){"
                        Case "Custom"
                            _rtext = "for(" & Values(1) & "){"
                        Case "AllUnit"
                            Dim _playerText As String

                            Select Case Values(1)
                                Case 0
                                    _playerText = "EUDLoopUnit()"
                                Case 13
                                    _playerText = "lp.EUDLoopUnit2()"
                                Case Else
                                    _playerText = "lp.EUDLoopPUnit(" & Values(1) - 1 & ")"
                            End Select


                            _rtext = "foreach(ptr, epd : " & _playerText & " ) {"
                        Case "PlayerLoop"
                            _rtext = "EUDPlayerLoop()();"
                    End Select
                Case ElementType.포만족



                    _rtext = "" '"for(var j = 0 ; j <= MaxPlayer ; j++){"
                Case ElementType.함수정의
                    _rtext = "function" & " " & Values(0) & "("

                    If Elements(0).Elements.Count <> 0 Then
                        _rtext = _rtext & Elements(0).Elements(0).Values(0)
                        For i = 1 To Elements(0).Elements.Count - 1
                            _rtext = _rtext & " ," & Elements(0).Elements(i).Values(0)
                        Next
                    End If
                    _rtext = _rtext & ") {"
                Case ElementType.RawTrigger
                    _rtext = "function" & " ClassicTrigger(){"
                Case ElementType.TriggerAct
                    _rtext = "if (" & VarialbeName & " == 1) {"
                Case ElementType.함수
                    '해당 함수의 정의를 가져와야 한다.
                    Dim fundef As Element = Nothing

                    For i = 0 To functions.GetElementsCount - 1
                        If Values(0) = functions.GetElementList(i).Values(0) Then
                            fundef = functions.GetElementList(i)
                            Exit For
                        End If
                    Next
                    If fundef IsNot Nothing Then
                        If fundef.Values(1) Then 'Wait사용
                            '인수 3개 설정하고, 타이머 1로 설정하고 함수 시작.
                            Dim funname As String = Values(0)

                            _rtext = funname & "Timer[getcurpl()] = 1;"

                            Dim valuedeflist As New List(Of String)
                            If Values.Count <> 1 Then
                                valuedeflist.Add(funname & fundef.GetElementList(0).GetElementList(0).Values(0))
                                _rtext = _rtext & vbCrLf & funname & fundef.GetElementList(0).GetElementList(0).Values(0) & "[getcurpl()] = " & ValueParser(Values(1), GetFuncDEf(1), True, True) & ";"
                                For i = 2 To Values.Count - 1
                                    valuedeflist.Add(funname & fundef.GetElementList(0).GetElementList(i - 1).Values(0))
                                    _rtext = _rtext & vbCrLf & funname & fundef.GetElementList(0).GetElementList(i - 1).Values(0) & "[getcurpl()] = " & ValueParser(Values(i), GetFuncDEf(i), True, True) & ";"
                                Next
                            End If

                            _rtext = _rtext & vbCrLf & Values(0) & "("
                            If Values.Count <> 1 Then
                                _rtext = _rtext & valuedeflist(0) & "[getcurpl()]"
                                For i = 2 To Values.Count - 1
                                    _rtext = _rtext & "," & valuedeflist(i - 1) & "[getcurpl()]"
                                Next
                            End If
                            _rtext = _rtext & ")"
                            Exit Select
                        End If
                    End If
                    _rtext = Values(0) & "("


                    If Values.Count <> 1 Then
                        _rtext = _rtext & ValueParser(Values(1), GetFuncDEf(1), True, True)
                        For i = 2 To Values.Count - 1
                            _rtext = _rtext & "," & ValueParser(Values(i), GetFuncDEf(i), True, True)
                        Next
                    End If
                    _rtext = _rtext & ")"
                    '만약 조건부일 경우를 판단해야 함.
                    '부모가 조건일 경우!
            End Select


        End If

        Return _rtext
    End Function





    Public Function ToCode(intend As Integer, isbulid As Boolean, Optional isLast As Boolean = False) As String
        Dim _stringb As New StringBuilder


        Dim abledflag As Boolean = True

        If isdisalbe Then
            Return ""
        End If
        If Type = ElementType.RawString Then
            _stringb.Append(Values(0))
        End If
        If Type = ElementType.액션 Then
            If act.Name = "Comment" Then
                abledflag = False
            End If
            If act.Name = "BGMPlay" Or act.Name = "BGMResume" Or act.Name = "BGMStop" Then
                If ProjectSet.UsedSetting(ProjectSet.Settingtype.BtnSet) = False Then
                    abledflag = False
                End If
            ElseIf act.Name = "SCDB:Exec" Or act.Name = "SCDB:SaveData" Or act.Name = "SCDB:LoadData" Or act.Name = "SCDB:UseCustomMsg" Or act.Name = "SCDB:GetDataCount" Or act.Name = "SCDB:GetCurrentindex" Or act.Name = "SCDB:LastMsgReset" Then
                If ProjectSet.SCDBUse = False Then
                    abledflag = False
                End If
            End If
        ElseIf Type = ElementType.조건 Then
            If con.Name = "CurrentBGM" Or con.Name = "BGMPlaying" Then
                If ProjectSet.UsedSetting(ProjectSet.Settingtype.BtnSet) = False Then
                    abledflag = False
                End If
            ElseIf con.Name = "SCDB:Msg" Or con.Name = "SCDB:Connect" Or con.Name = "SCDB:Loadable" Then
                If ProjectSet.SCDBUse = False Then
                    abledflag = False
                End If
            End If
        End If


        If Type <> ElementType.main And Type <> ElementType.조건문if And Type <> ElementType.조건문ifelse And Type <> ElementType.와일 And Type <> ElementType.포만족 And Type <> ElementType.Functions And Type <> ElementType.코드 And Type <> ElementType.인수 And Type <> ElementType.FoluderAction And Type <> ElementType.RawTriggers Then
            If abledflag = True Then
                If Type = ElementType.Wait Then 'Wait는 부모의 보모의 2벨류를 판단.
                    If Parrent.Parrent.GetTypeV = ElementType.함수정의 Then
                        If Parrent.Parrent.Values(1) = False Then
                            Return _stringb.ToString
                        Else
                            WaitCounter += Values(0)
                            LineCount += 2
                            _stringb.Append(GetIntend(intend - 1) & "}")
                            _stringb.AppendLine(" else if (" & VarialbeName & " == " & WaitCounter & ") {")
                            Return _stringb.ToString
                        End If
                    Else
                        WaitCounter += Values(0)
                        LineCount += 2
                        _stringb.Append(GetIntend(intend - 1) & "}")
                        _stringb.AppendLine(" else if (" & VarialbeName & " == " & WaitCounter & ") {")
                        Return _stringb.ToString
                    End If
                End If

                If Type = ElementType.함수정의 Then
                    If Values(1) = True Then
                        WaitCounter = 1
                        VarialbeName = Values(0) & "Timer[getcurpl()]"
                    End If
                End If

                If Type = ElementType.TriggerAct Then
                    WaitCounter = 1
                End If


                If Type = ElementType.함수 Then
                    Dim fundef As Element = Nothing

                    For i = 0 To functions.GetElementsCount - 1
                        If Values(0) = functions.GetElementList(i).Values(0) Then
                            fundef = functions.GetElementList(i)
                            Exit For
                        End If
                    Next
                    If fundef IsNot Nothing Then
                        If fundef.Values(1) Then
                            LineCount += 2
                            _stringb.AppendLine(GetIntend(intend) & "if (" & fundef.Values(0) & "Timer[getcurpl()] == 0) {")
                            intend += 1
                        End If
                    End If
                End If

                If Type = ElementType.TriggerAct Then
                    intend -= 1
                End If

                'Code내용============================================================================================
                If GetCode().IndexOf(vbCr) <> -1 Then
                    Dim strs() As String = GetCode().Split(vbCrLf)

                    _stringb.Append(GetIntend(intend) & strs(0).Trim)
                    For i = 1 To strs.Count - 1
                        _stringb.Append(vbCrLf & GetIntend(intend) & strs(i).Trim)
                    Next
                    LineCount += strs.Count
                Else
                    If (Type = ElementType.액션 Or Type = ElementType.조건 Or Type = ElementType.함수) And isbulid = True Then
                        Try
                            DebugDic.Add(LineCount, Me)
                        Catch ex As Exception

                        End Try
                    End If
                    _stringb.Append(GetIntend(intend) & GetCode())

                    LineCount += 1
                End If
                'Code내용=============================================================================================


                If Type = ElementType.함수정의 Then
                    If Values(1) = True Then
                        LineCount += 2
                        intend += 1
                        _stringb.Append(vbCrLf & GetIntend(intend) & "if (" & VarialbeName & " > 0) {")
                        intend += 1
                        _stringb.Append(vbCrLf & GetIntend(intend) & "if (" & VarialbeName & " == 1) {")
                    End If
                End If



                If Type = ElementType.액션 Then
                    If _stringb.Length <> 0 Then
                        If _stringb.Chars(_stringb.Length - 1) <> ";" Then
                            _stringb.Append(";")
                        End If
                    End If
                End If
                If Type = ElementType.조건 Then
                    If isLast = False Then
                        If Parrent.Values(0) = "And" Then
                            _stringb.Append(" && ")
                        Else
                            _stringb.Append(" || ")
                        End If
                    End If
                End If
                If Type = ElementType.함수 Then
                    If Parrent.Type <> ElementType.와일조건 And Parrent.Type <> ElementType.조건절 And
                            Parrent.Type <> ElementType.조건절 And Parrent.Type <> ElementType.TriggerCond Then
                        _stringb.Append(";")
                    Else
                        If isLast = False Then
                            If Parrent.Values(0) = "And" Then
                                _stringb.Append(" && ")
                            Else
                                _stringb.Append(" || ")
                            End If
                        End If
                    End If
                End If


                _stringb.AppendLine()
            End If
        End If

        Select Case Type
            Case ElementType.포
                If Values(0) = "PlayerLoop" Then
                    intend += 1
                    LineCount += 1
                    Dim tempcondition As String = ""

                    Dim _array() As String = Values(1).Split(",")
                    Dim _playerArray As New List(Of Byte)

                    For i = 0 To 7
                        If _array(i) = True Then
                            _playerArray.Add(i)
                        End If
                    Next

                    For i = 8 To 11
                        If _array(i) = True Then
                            If ProjectSet.CHKFORCEDATA(i - 8).Count > 1 Then
                                For j = 1 To ProjectSet.CHKFORCEDATA(i - 8).Count - 1
                                    _playerArray.Add(ProjectSet.CHKFORCEDATA(i - 8)(j))
                                Next
                            End If
                        End If
                    Next

                    For i = 0 To 7
                        If _playerArray.Contains(i) Then
                            tempcondition = tempcondition & "getcurpl() == " & i & " || "
                        End If
                    Next
                    If tempcondition <> "" Then
                        tempcondition = tempcondition.Remove(tempcondition.Length - 4, 4)
                    End If



                    _stringb.Append(GetIntend(intend) & "if (" & tempcondition & ") {" & vbCrLf)



                    'If Values(1) <> "0" Then
                    '    intend += 1
                    '    Dim tempcondition As String = ""

                    '    If ProjectSet.CHKFORCEDATA(Values(1) - 1).Count > 1 Then
                    '        tempcondition = "getcurpl() == " & ProjectSet.CHKFORCEDATA(Values(1) - 1)(1)
                    '        For i = 2 To ProjectSet.CHKFORCEDATA(Values(1) - 1).Count - 1
                    '            tempcondition = tempcondition & " || getcurpl() == " & ProjectSet.CHKFORCEDATA(Values(1) - 1)(i)
                    '        Next
                    '    End If




                    '    _stringb.Append(GetIntend(intend) & "if (" & tempcondition & ") {" & vbCrLf)

                    'End If
                End If
        End Select



        intend += 1
        Select Case Type
            Case ElementType.조건문if, ElementType.조건문ifelse, ElementType.와일, ElementType.포, ElementType.코드
                intend -= 1
            Case ElementType.Foluder
                intend -= 2
        End Select

        If Type <> ElementType.인수 Then
            For i = 0 To Elements.Count - 1
                If i = Elements.Count - 1 Then
                    _stringb.Append(Elements(i).ToCode(intend, isbulid, True))
                Else
                    _stringb.Append(Elements(i).ToCode(intend, isbulid))
                End If
            Next
            If _stringb.Length > 2 And Elements.Count <> 0 Then
                If Mid(_stringb.ToString, _stringb.Length - 4).Trim = "&&" Then
                    _stringb.Remove(_stringb.Length - 5, 2)
                End If
            End If

        End If






            Select Case Type
            Case ElementType.Switchcase
                If Values(1) Then
                    LineCount += 1
                    _stringb.AppendLine(GetIntend(intend - 1) & "EUDBreak();")
                End If
            Case ElementType.Switch
                LineCount += 1
                _stringb.AppendLine(GetIntend(intend - 1) & "EUDEndSwitch();")

            Case ElementType.함수정의
                LineCount += 1
                If Values(1) = True Then
                    intend -= 1
                    LineCount += 4
                    _stringb.AppendLine(GetIntend(intend + 1) & VarialbeName & " = -1;")
                    _stringb.AppendLine(GetIntend(intend) & "}")
                    '_stringb.AppendLine(GetIntend(intend) & "if (" & VarialbeName & " > 0) {")
                    intend -= 1
                    _stringb.AppendLine(GetIntend(intend + 1) & VarialbeName & " += 1;")
                    _stringb.AppendLine(GetIntend(intend) & "}")
                End If
                _stringb.Append(GetIntend(intend - 1) & "}" & vbCrLf)
            Case ElementType.TriggerAct
                intend -= 1

                Dim preserveFlag As Boolean = False
                '프리저브가 있는지 확인한다.
                For i = 0 To Elements.Count - 1
                    If Elements(i).Type = ElementType.액션 Then
                        If Elements(i).act.Name = "PreserveTrigger" Then
                            preserveFlag = True
                        End If
                    End If
                Next




                '만약 프리져브가 없으면 초기화를 시키지 않는다.
                If preserveFlag Then
                    _stringb.AppendLine(GetIntend(intend + 1) & VarialbeName & " = 0;")
                End If




                _stringb.AppendLine(GetIntend(intend) & "}")
                _stringb.AppendLine(GetIntend(intend) & "if (" & VarialbeName & " > 0) {")
                _stringb.AppendLine(GetIntend(intend + 1) & VarialbeName & " += 1;")
                _stringb.AppendLine(GetIntend(intend) & "}")
            Case ElementType.함수
                Dim fundef As Element = Nothing
                For i = 0 To functions.GetElementsCount - 1
                    If Values(0) = functions.GetElementList(i).Values(0) Then
                        fundef = functions.GetElementList(i)
                        Exit For
                    End If
                Next
                If fundef IsNot Nothing Then
                    If fundef.Values(1) Then
                        intend -= 1
                        _stringb.AppendLine(GetIntend(intend - 1) & "}")
                    End If
                End If



            Case ElementType.만족, ElementType.만족안함, ElementType.와일만족, ElementType.RawTrigger
                LineCount += 1
                _stringb.Append(GetIntend(intend - 1) & "}" & vbCrLf)
            Case ElementType.포만족
                LineCount += 1
                If Parrent.Values(0) = "PlayerLoop" Then
                    LineCount += 1
                    If Parrent.Values(1) <> "0" Then
                        _stringb.Append(GetIntend(intend - 1) & "}" & vbCrLf)
                        intend -= 1
                    End If
                    _stringb.Append(GetIntend(intend - 1) & "EUDEndPlayerLoop();" & vbCrLf)
                Else
                    _stringb.Append(GetIntend(intend - 1) & "}" & vbCrLf)
                End If
            Case ElementType.Foluder
                LineCount += 1
                _stringb.AppendLine(GetIntend(intend + 1) & Values(1))
        End Select



        Return _stringb.ToString
    End Function
    Private Function GetValue(def As String) As String
        If act IsNot Nothing Then
            If act.ValuesDef.IndexOf(def) = -1 Then
                Return "None"
            Else
                Return Values(act.ValuesDef.IndexOf(def))
            End If
        Else
            If con.ValuesDef.IndexOf(def) = -1 Then
                Return "None"
            Else
                Return Values(con.ValuesDef.IndexOf(def))
            End If
        End If


    End Function
End Class

Public Class Action
    Public Name As String
    Public Text As String
    Public Texts As New List(Of String)

    Public CodeText As String


    Public Tooltip As String
    Public ValuesDef As New List(Of String)


    'Public Sub New(_name As String, _text As String, _codetext As String, _valuedef() As String, Optional _tooltip As String = "")
    '    Name = _name
    '    Text = _text
    '    CodeText = _codetext
    '    ToolTip = _tooltip

    '    ValuesDef = New List(Of String)
    '    ValuesDef.AddRange(_valuedef)
    'End Sub
End Class



Public Class Condiction
    Public Name As String
    Public Text As String
    Public Texts As New List(Of String)

    Public CodeText As String


    Public Tooltip As String
    Public ValuesDef As New List(Of String)


    'Public Sub New(_name As String, _text As String, _codetext As String, _valuedef() As String, Optional _tooltip As String = "")
    '    Name = _name
    '    Text = _text
    '    CodeText = _codetext
    '    Tooltip = _tooltip

    '    ValuesDef = New List(Of String)
    '    ValuesDef.AddRange(_valuedef)
    'End Sub
End Class