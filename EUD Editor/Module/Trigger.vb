Imports System.Text
Imports System.IO

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
               "Wait : "'20
               }

        Return temp(Et)
    End Function


    Public Parrent As Element


    Public act As Action
    Public con As Condiction
    Private Elements As List(Of Element)
    Public Values As New List(Of String)


    Private Type As ElementType

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



        Select Case valdef
            Case "VariableModifier"
                If isTocode Then
                    Select Case _value
                        Case "대입"
                            returnstring = "="
                        Case "덧셈"
                            returnstring = "+="
                        Case "뺼셈"
                            returnstring = "-="
                        Case "곱셈"
                            returnstring = "*="
                        Case "나눗셈"
                            returnstring = "/="
                    End Select
                End If
            Case "VariableComparison"
                If isTocode Then
                    Select Case _value
                        Case "일치"
                            returnstring = "=="
                        Case "초과"
                            returnstring = ">"
                        Case "미만"
                            returnstring = "<"
                        Case "이상"
                            returnstring = ">="
                        Case "이하"
                            returnstring = "<="
                        Case "불일치"
                            returnstring = "!="
                    End Select
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
            Case "Modifier"
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
                        '오프셋 구하기.
                        returnstring = "epdread_epd(EPD(0x5187EC) + " & 3 * _tempvalue(0) & ") + 5 * " & _tempvalue(1)
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
                    Return returnstring
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
                        Return returnstring
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
                            returnstring = """" & returnstring & """"
                        End If

                        Return returnstring
                    Catch ex As Exception
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

        _stringb.AppendLine("Type:" & Type)
        If Type = ElementType.액션 Then
            _stringb.AppendLine("act:" & act.Name)

            Dim temp As New StringBuilder
            temp.Append(Values(0))
            For i = 1 To Values.Count - 1
                temp.Append(Separater & Values(i))
            Next

            _stringb.AppendLine(temp.ToString)
        End If
        If Type = ElementType.조건 Then
            _stringb.AppendLine("con:" & con.Name)

            Dim temp As New StringBuilder
            temp.Append(Values(0))
            For i = 1 To Values.Count - 1
                temp.Append(Separater & Values(i))
            Next

            _stringb.AppendLine(temp.ToString)
        End If
        If Type = ElementType.포 Or Type = ElementType.함수 Or Type = ElementType.함수정의 Then
            Dim temp As New StringBuilder
            temp.Append(Values(0))
            For i = 1 To Values.Count - 1
                temp.Append(Separater & Values(i))
            Next

            _stringb.AppendLine(temp.ToString)
        End If
        If Type = ElementType.조건절 Or Type = ElementType.와일조건 Or Type = ElementType.Wait Then
            Dim temp As New StringBuilder
            temp.Append(Values(0))

            _stringb.AppendLine(temp.ToString)
        End If

        _stringb.AppendLine("ElementsCount:" & Elements.Count)
        For i = 0 To Elements.Count - 1
            _stringb.Append(Elements(i).ToSaveFile)
        Next

        _stringb.AppendLine("END")


        Return _stringb.ToString
    End Function

    Public Function LoadFile(_str As String, index As Integer)
        Dim tempstr() As String


        Dim _index As Integer = index
        tempstr = _str.Split(vbCrLf)
        Type = NextLine(tempstr(_index), _index)
        'MsgBox(Type)

        Dim isreadvalue As Boolean = False
        If Type = ElementType.액션 Then
            act = SeachAct(NextLine(tempstr(_index), _index))
            isreadvalue = True
        End If
        If Type = ElementType.조건 Then
            con = SeachCon(NextLine(tempstr(_index), _index))
            isreadvalue = True
        End If
        If Type = ElementType.포 Or Type = ElementType.함수정의 Or
                Type = ElementType.함수 Or Type = ElementType.조건절 Or
                Type = ElementType.와일조건 Or Type = ElementType.Wait Then
            isreadvalue = True
        End If

        If isreadvalue = True Then
            Values = New List(Of String)
            Dim _valuestring As String = ""
            '벨류 읽기
            'MsgBox(tempstr(_index + _i).IndexOf("ElementsCount"))
            _valuestring = _valuestring & tempstr(_index).Trim
            _index += 1
            While (tempstr(_index).Trim.IndexOf("ElementsCount") = -1)
                _valuestring = _valuestring & vbCrLf & tempstr(_index).Trim
                _index += 1
            End While

            Values.AddRange(_valuestring.Split(Separater))
        End If



        Dim elecount As Integer = NextLine(tempstr(_index), _index)


        For i = 0 To elecount - 1
            Dim _ele As New Element(Me, ElementType.main)
            _index = _ele.LoadFile(_str, _index)
            Elements.Add(_ele)
        Next
        NextLine(tempstr(_index), _index)

        '_stringb.AppendLine("ElementsCount:" & Elements.Count)
        'For i = 0 To Elements.Count - 1
        '    _stringb.Append(Elements(i).ToSaveFile)
        'Next

        '_stringb.AppendLine("END")

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



    Public Sub Delete()
        Parrent.Elements.Remove(Me)
    End Sub


    Public Function Clone(_parrentEle As Element) As Element
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

        Return newEle
    End Function



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

    Public Function ToTreeNode() As TreeNode
        Dim RTreeNode As New TreeNode


        If Type <> ElementType.main Then
            RTreeNode = New TreeNode(GetText())
            Select Case Type

                Case ElementType.조건, ElementType.액션, ElementType.Functions
                    RTreeNode.ForeColor = Color.White
                Case ElementType.조건문if, ElementType.조건문ifelse, ElementType.와일, ElementType.포, ElementType.함수정의
                    RTreeNode.ForeColor = Color.LightPink
                Case Else
                    RTreeNode.ForeColor = Color.LightBlue
            End Select


            RTreeNode.Tag = Me
        End If

        'MsgBox(Elements.Count)
        For i = 0 To Elements.Count - 1
            RTreeNode.Nodes.Add(Elements(i).ToTreeNode())
        Next


        Return RTreeNode
    End Function

    Public Function GetText() As String
        Dim _rtext As String = ""


        Select Case Type
            Case ElementType.액션
                _rtext = act.Text

                Dim bexit As Boolean = False
                While (bexit = False)
                    For i = 0 To act.ValuesDef.Count - 1
                        _rtext = Replace(_rtext, "$" & act.ValuesDef(i) & "$", "(" & ValueParser(i) & ")", , 1)
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
                        _rtext = "For(변수 " & Values(1) & "를 사용하여 " & Values(2) & "번 반복합니다.)"
                    Case "Custom"
                        _rtext = "For(" & Values(1) & ")"
                    Case "AllUnit"
                        Dim _playertext As String
                        Select Case Values(1)
                            Case 0
                                _playertext = "모든 플레이어"
                            Case Else
                                _playertext = "Player " & Values(1)
                        End Select

                        _rtext = "Foreach(" & _playertext & "의 유닛을 epd, ptr변수로 순환합니다.)"
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

                        _rtext = tname & "를 순환합니다."
                End Select
            Case ElementType.함수정의
                _rtext = "함수정의 : " & Values(0) & "        대기하기 사용 : " & Values(1)
            Case ElementType.함수
                _rtext = "함수 : " & Values(0) & "("

                Dim valdef As String = ""

                If Values.Count <> 1 Then
                    _rtext = _rtext & ValueParser(Values(1), GetFuncDEf(1))
                    For i = 2 To Values.Count - 1
                        _rtext = _rtext & "," & ValueParser(Values(i), GetFuncDEf(i))
                    Next
                End If
                _rtext = _rtext & ")"
            Case ElementType.조건절
                _rtext = ElementNames(Type) & " " & Values(0)
            Case ElementType.와일조건
                _rtext = ElementNames(Type) & " " & Values(0)
            Case ElementType.Wait
                _rtext = "대기하기 : " & Values(0)
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

                End If
                If act.Name = "SetCUnitData" Or act.Name = "SetVariableCUnitData" Or act.Name = "AddCUnitData" Then
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
                    Catch ex As Exception
                        _rtext = _rtext.Replace("$writedef$", "dw")
                    End Try
                End If
                If act.Name = "SetButton" Then
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
                End If
                If act.Name = "DisplayCText" Or act.Name = "DisplaySavedCText" Then
                    If Values(1) = "1" Then
                        _rtext = "txtPtr = dwread_epd_safe(EPD(0x640B58));" & vbCrLf & _rtext & ";" & vbCrLf & "SetMemory(0x640B58, SetTo, txtPtr);"
                    End If
                End If

            ElseIf Type = ElementType.조건 Then
                _rtext = con.CodeText
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
                End If
                If con.Name = "CUnitData" Then
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
                    Catch ex As Exception
                        _rtext = _rtext.Replace("$writedef$", "dw")
                    End Try
                End If
            End If
        Else
            _rtext = ElementNames(Type)
            Select Case Type
                Case ElementType.조건문if, ElementType.조건문ifelse, ElementType.와일
                    _rtext = ""
                Case ElementType.조건절
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
                    _rtext = _rtext & "){"
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

                            _rtext = funname & "Timer = 1;"

                            Dim valuedeflist As New List(Of String)
                            If Values.Count <> 1 Then
                                valuedeflist.Add(funname & fundef.GetElementList(0).GetElementList(0).Values(0))
                                _rtext = _rtext & vbCrLf & funname & fundef.GetElementList(0).GetElementList(0).Values(0) & " = " & ValueParser(Values(1), GetFuncDEf(1), True, True) & ";"
                                For i = 2 To Values.Count - 1
                                    valuedeflist.Add(funname & fundef.GetElementList(0).GetElementList(i - 1).Values(0))
                                    _rtext = _rtext & vbCrLf & funname & fundef.GetElementList(0).GetElementList(i - 1).Values(0) & " = " & ValueParser(Values(i), GetFuncDEf(i), True, True) & ";"
                                Next
                            End If

                            _rtext = _rtext & vbCrLf & Values(0) & "("
                            If Values.Count <> 1 Then
                                _rtext = _rtext & valuedeflist(0)
                                For i = 2 To Values.Count - 1
                                    _rtext = _rtext & "," & valuedeflist(i - 1)
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





    Public Function ToCode(intend As Integer, Optional isLast As Boolean = False) As String
        Dim _stringb As New StringBuilder

        Dim abledflag As Boolean = True


        If Type = ElementType.액션 Then
            If act.Name = "BGMPlay" Or act.Name = "BGMResume" Or act.Name = "BGMStop" Then
                If ProjectSet.UsedSetting(ProjectSet.Settingtype.BtnSet) = False Then
                    abledflag = False
                End If
            End If
        End If
        If Type = ElementType.조건 Then
            If con.Name = "CurrentBGM" Or con.Name = "BGMPlaying" Then
                If ProjectSet.UsedSetting(ProjectSet.Settingtype.BtnSet) = False Then
                    abledflag = False
                End If
            End If
        End If


        If Type <> ElementType.main And Type <> ElementType.조건문if And Type <> ElementType.조건문ifelse And Type <> ElementType.와일 And Type <> ElementType.포만족 And Type <> ElementType.Functions And Type <> ElementType.코드 And Type <> ElementType.인수 Then
            If abledflag = True Then
                If Type = ElementType.Wait Then 'Wait는 부모의 보모의 2벨류를 판단.
                    If Parrent.Parrent.Values(1) = False Then
                        Return _stringb.ToString
                    Else
                        WaitCounter += Values(0)
                        _stringb.AppendLine(GetIntend(intend - 1) & "}")
                        _stringb.AppendLine(GetIntend(intend - 1) & "if (" & VarialbeName & " == " & WaitCounter & ") {")
                        Return _stringb.ToString
                    End If
                End If

                If Type = ElementType.함수정의 Then
                    If Values(1) = True Then
                        WaitCounter = 1
                        For i = 0 To Elements(0).Elements.Count - 1
                            _stringb.AppendLine("var " & Values(0) & Elements(0).Elements(i).Values(0) & ";")
                        Next
                        _stringb.AppendLine("var " & Values(0) & "Timer;")
                        VarialbeName = Values(0) & "Timer"
                    End If
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
                            _stringb.AppendLine(GetIntend(intend) & "if (" & fundef.Values(0) & "Timer == 0) {")
                            intend += 1
                        End If
                    End If
                End If



                'Code내용
                If GetCode().IndexOf(vbCr) <> -1 Then
                    Dim strs() As String = GetCode().Split(vbCrLf)


                    _stringb.Append(GetIntend(intend) & strs(0).Trim)
                    For i = 1 To strs.Count - 1
                        _stringb.Append(vbCrLf & GetIntend(intend) & strs(i).Trim)
                    Next
                Else
                    _stringb.Append(GetIntend(intend) & GetCode())
                End If



                If Type = ElementType.함수정의 Then
                    If Values(1) = True Then
                        _stringb.Append(vbCrLf & GetIntend(intend + 1) & "if (" & VarialbeName & " == 1) {")
                        intend += 1
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
                            Parrent.Type <> ElementType.조건절 Then
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
        End Select

        If Type <> ElementType.인수 Then
            For i = 0 To Elements.Count - 1
                If i = Elements.Count - 1 Then
                    _stringb.Append(Elements(i).ToCode(intend, True))
                Else
                    _stringb.Append(Elements(i).ToCode(intend))
                End If
            Next
        End If

        If Type = ElementType.함수정의 Then
            If Values(1) = True Then
                intend -= 1
                _stringb.AppendLine(GetIntend(intend + 1) & VarialbeName & " = 0;")
                _stringb.AppendLine(GetIntend(intend) & "}")
                _stringb.AppendLine(GetIntend(intend) & "if (" & VarialbeName & " > 0) {")
                _stringb.AppendLine(GetIntend(intend + 1) & VarialbeName & " += 1;")
                _stringb.AppendLine(GetIntend(intend) & "}")
            End If
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
                    intend -= 1
                    _stringb.AppendLine(GetIntend(intend - 1) & "}")
                End If
            End If
        End If


        Select Case Type
            Case ElementType.만족, ElementType.만족안함, ElementType.와일만족, ElementType.함수정의
                _stringb.Append(GetIntend(intend - 1) & "}" & vbCrLf)
            Case ElementType.포만족
                If Parrent.Values(0) = "PlayerLoop" Then
                    If Parrent.Values(1) <> "0" Then
                        _stringb.Append(GetIntend(intend - 1) & "}" & vbCrLf)
                        intend -= 1
                    End If
                    _stringb.Append(GetIntend(intend - 1) & "EUDEndPlayerLoop();" & vbCrLf)
                Else
                    _stringb.Append(GetIntend(intend - 1) & "}" & vbCrLf)
                End If
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

    Public CodeText As String


    Public Tooltip As String
    Public ValuesDef As List(Of String)


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

    Public CodeText As String


    Public Tooltip As String
    Public ValuesDef As List(Of String)


    'Public Sub New(_name As String, _text As String, _codetext As String, _valuedef() As String, Optional _tooltip As String = "")
    '    Name = _name
    '    Text = _text
    '    CodeText = _codetext
    '    Tooltip = _tooltip

    '    ValuesDef = New List(Of String)
    '    ValuesDef.AddRange(_valuedef)
    'End Sub
End Class