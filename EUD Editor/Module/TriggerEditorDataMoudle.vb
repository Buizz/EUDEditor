Imports System.IO
Imports Newtonsoft.Json

'To serialize MyPersonClass using XML serialisation, you will need instances of XmlSerializer And StreamWriter (in System.IO):

'XmlSerializer serializer = New XmlSerializer(TypeOf (MyPersonClass));
'StreamWriter xmlFile = New StreamWriter(@"InsertFileName");
'serializer.Serialize(xmlFile, classInstance);
'xmlFile.Close();

Module TriggerEditorDataMoudle
    Public Const PadWidth As Byte = 4
    Public Const Separater As String = "ஐ"

    Public WaitCounter As Integer
    Public VarialbeName As String


    Public GlobalVar As Element


    Public Actions As List(Of Action)
    Public Condictions As List(Of Condiction)


    Public functions As Element
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


    Public Sub NewTriggerFile()
        GlobalVar = New Element(GlobalVar, ElementType.main)
        functions = New Element(functions, ElementType.Functions)
        StartElement = New Element(StartElement, ElementType.main)
        BeforeElement = New Element(BeforeElement, ElementType.main)
        AfterElement = New Element(AfterElement, ElementType.main)
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

    Public Sub LoadTriggerFile(datas As String)
        If datas = "" Then
            NewTriggerFile()
            Exit Sub
        End If

        Try
            functions = New Element(functions, ElementType.Functions)
            GlobalVar = New Element(GlobalVar, ElementType.main)
            StartElement = New Element(StartElement, ElementType.main)
            BeforeElement = New Element(BeforeElement, ElementType.main)
            AfterElement = New Element(AfterElement, ElementType.main)

            functions.LoadFile(datas, findSection(datas, "&functions&"))
            GlobalVar.LoadFile(datas, findSection(datas, "&GlobalVar&"))
            StartElement.LoadFile(datas, findSection(datas, "&onPluginStart&"))
            BeforeElement.LoadFile(datas, findSection(datas, "&beforeTriggerExec&"))
            AfterElement.LoadFile(datas, findSection(datas, "&afterTriggerExec&"))
        Catch ex As Exception
            MsgBox(Lan.GetText("Msgbox", "tfError"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
        End Try

    End Sub

    Public Sub LoadTriggerFileKeepFile(datas As String)
        If datas = "" Then
            Exit Sub
        End If

        Try
            functions.LoadFile(datas, findSection(datas, "&functions&"))
            GlobalVar.LoadFile(datas, findSection(datas, "&GlobalVar&"))
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
                    strb.Append(name & Factors.GetElementList(0).Values(0))
                    For j = 1 To Factors.GetElementsCount - 1
                        strb.Append(", " & name & Factors.GetElementList(j).Values(0))
                    Next
                End If



                strb.AppendLine(");")
            End If
        Next


        Return strb.ToString
    End Function


    Public Function TriggerToEPS() As String
        Dim str As String = ""
        If ProjectSet.UsedSetting(ProjectSet.Settingtype.BtnSet) Then
            str = "import BGMPlayer as BGM;" & vbCrLf
        End If
        str = str & "import punitloop as lp;" & vbCrLf
        str = str & "import tempcustomText as tct;" & vbCrLf

        str = str & vbCrLf & "var txtPtr;" & vbCrLf
        str = str & "const trgk = $T('Artanis & safhfh');" & vbCrLf



        str = str & GlobalVar.ToCode(-1) & vbCrLf


        For Each funcs As Element In functions.GetElementList
            Dim arugments As String = ""

            If funcs.GetElementList(0).GetElementList.Count <> 0 Then
                arugments = funcs.GetElementList(0).GetElementList(0).Values(0)
                For i = 1 To funcs.GetElementList(0).GetElementList.Count - 1
                    arugments = arugments & ", " & funcs.GetElementList(0).GetElementList(i).Values(0)
                Next
            End If



            str = str & "function " & funcs.Values(0) & "(" & arugments & ");" & vbCrLf
        Next



        str = str & functions.ToCode(-1) & vbCrLf
        str = str & "function WaitableTrigger() {" & vbCrLf & GetWaitAbleTrigger() & "}" & vbCrLf



        str = str & "function onPluginStart() {" & vbCrLf & GetIntend(1) & "randomize();" & vbCrLf &
               GetIntend(1) & "tct.init();" & vbCrLf &
        StartElement.ToCode(0) & "}" & vbCrLf



        str = str & "function beforeTriggerExec() {" & vbCrLf &
                         GetIntend(1) & "WaitableTrigger();" & vbCrLf

        If ProjectSet.UsedSetting(ProjectSet.Settingtype.BtnSet) = True Then
            str = str & GetIntend(1) & "BGM.Player();" & vbCrLf
        End If


        str = str & BeforeElement.ToCode(0) & "}" & vbCrLf
        str = str & "function afterTriggerExec() {" & vbCrLf & AfterElement.ToCode(0) & "}" & vbCrLf

        Return str
    End Function


    Public Function SaveTrigger() As String
        Dim str As New Text.StringBuilder


        str.AppendLine("&functions&")
        str.AppendLine(functions.ToSaveFile)
        str.AppendLine("&GlobalVar&")
        str.AppendLine(GlobalVar.ToSaveFile)
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


        '컨디션 로딩해볼까?
        _filestream = New FileStream(My.Application.Info.DirectoryPath & "\Data\TriggerEditor\condition.json", FileMode.Open)
        _streamreader = New StreamReader(_filestream, System.Text.Encoding.Default)

        jsonString = _streamreader.ReadToEnd

        _streamreader.Close()
        _filestream.Close()

        '
        'Condictions
        Condictions = JsonConvert.DeserializeObject(Of List(Of Condiction))(jsonString)




        'Condictions.Add(obj(0))
        'MsgBox(obj(0).Name)
        'For Each onecon As Condiction In conList
        '    Console.WriteLine("id:" & onename.id)
        '    Console.WriteLine("name:" & onename.Name)
        'Next


        'Condictions.Add(New Condiction("CountdownTimer", "카운트 타이머가 Time보다 Modifier할 경우", "CountdownTimer(Comparison, Time)", {"Comparison", "Time"}))
        'Condictions.Add(New Condiction("Command", "Payer의 Unit을 Number보다 Comparison을 경우", "Command(Player, Comparison, Number, Unit)", {"Player", "Comparison", "Number", "Unit"}))

        '        Function CountdownTimer(Comparison, Time)
        '        Comparison = ParseComparison(Comparison)
        '        Return Condition(0, 0, Time, 0, Comparison, 1, 0, 0)
        '        End


        'Function Command(Player, Comparison, Number, Unit)
        '        Player = ParsePlayer(Player)
        '        Comparison = ParseComparison(Comparison)
        '        Unit = ParseUnit(Unit)
        '        Return Condition(0, Player, Number, Unit, Comparison, 2, 0, 0)
        '        End





    End Sub
End Module
