Imports System.IO
Imports System.Drawing.Image
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Module ProgramData
    Public grpwireData(227) As Byte
    Public tranwireData(227) As Byte
    Public wireframData(227) As Byte




    Public reqOpcode() As String



    'Public TestText As String = ""

    Public stat_txt() As String


    Public statusFn1 As New List(Of UInteger)
    Public statusFn2 As New List(Of UInteger)

    Public conbtnFnc As New List(Of btnType)
    Public actbtnFnc As New List(Of btnType)
    Structure btnType
        Public FucOffset As UInteger
        Public Name As String
        Public Code As Integer
    End Structure



    Public ReadOnly ANIMTYPE() As Byte = {2, 2, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             14, 14, 16, 16, 0, 0, 0, 0, 22, 22, 0, 24, 26, 0, 28, 28, 28, 28}
    Public HEADERNAME() As String
    Public HEADERINFOR() As String


    Public binfilename() As String
    Public binfileptr() As String

    Public portdata() As String
    Public sfxdata() As String


    Public Soundlist As New List(Of String)
    Public Soundinterval As Integer
    Public iscript As New CIscript

    Public SCDBLoc As New List(Of String)
    Public SCDBDeath As New List(Of String)

    Public DatEditDATA As New List(Of CDatEdit)
    Public GRPEditorDATA As New List(Of GRPDATA)
    Public GRPEditorUsingDATA(998) As String
    Public GRPEditorUsingindexDATA(4999) As String
    Class GRPDATA
        Public IsExternal As Boolean
        Public Filename As String
        Public SafeFilename As String
        Public Remapping As Integer
        Public Palett As Integer
        Public usingimage As List(Of Integer)
    End Class



    Public stattextdic As New Dictionary(Of Integer, String)
    Public Sub StatTextAdd(index As Integer, value As String)
        ProjectSet.saveStatus = False
        My.Forms.Main.nameResetting()
        If value <> "" Then
            If stattextdic.ContainsKey(index) Then
                stattextdic.Remove(index)
            End If
            stattextdic.Add(index, value)
        Else
            If stattextdic.ContainsKey(index) Then
                stattextdic.Remove(index)
            End If
        End If
    End Sub


    Public soundstopper As Boolean
    Public scmloader As Boolean
    Public noAirCollision As Boolean
    Public unlimiter As Boolean
    Public keepSTR As Boolean
    Public eudTurbo As Boolean
    Public iscriptPatcher As String
    Public iscriptPatcheruse As Boolean


    Public unpatcheruse As Boolean
    Public unpatcher As String

    Public grpinjectoruse As Boolean
    Public grpinjector_arrow As String
    Public grpinjector_drag As String
    Public grpinjector_illegal As String
    'arrow.grp
    'drag.grp
    'illegal.grp
    Public nqcuse As Boolean
    Public nqcunit As String
    Public nqclocs As String
    Public nqccommands As String

    Public dataDumperuse As Boolean
    Public dataDumper_user As String
    Public dataDumper_grpwire As String
    Public dataDumper_tranwire As String
    Public dataDumper_wirefram As String
    Public dataDumper_cmdicons As String
    Public dataDumper_stat_txt As String
    Public dataDumper_AIscript As String
    Public dataDumper_iscript As String

    Public dataDumper_grpwire_f As Byte
    Public dataDumper_tranwire_f As Byte
    Public dataDumper_wirefram_f As Byte
    Public dataDumper_cmdicons_f As Byte
    Public dataDumper_stat_txt_f As Byte
    Public dataDumper_AIscript_f As Byte
    Public dataDumper_iscript_f As Byte


    Public extraedssetting As String


    Public UnitStatusFn1(227) As Byte
    Public UnitStatusFn2(227) As Byte
    Public DebugID(227) As UInt32
    Public ProjectUnitStatusFn1(227) As Integer
    Public ProjectUnitStatusFn2(227) As Integer
    Public ProjectDebugID(227) As Integer

    Public RequireData(4) As List(Of SReqDATA)
    Public ProjectRequireData(4) As List(Of SReqDATA)
    '5개가 있고 각각 
    Public Class SReqDATA
        '요구사항 각 내용.
        Public pos As UInt16

        Public Code As List(Of UShort)
    End Class
    Public ProjectRequireDataUSE(4) As List(Of UInteger)






    Public DefaultBinBitmap As New List(Of binimage)
    Public Class binimage
        Private number As Integer

        Public pbmp As Bitmap
        Public tbmp As Bitmap
        Public zbmp As Bitmap

        Public bmp As Bitmap
        Public objtimage As List(Of Bitmap)
        Public objtimageEnable As List(Of Boolean)

        Public Sub New()
            number = DefaultBinBitmap.Count
        End Sub
        Public Sub SetEnable(fliterNum As Integer)

            Select Case number
                Case 0 '미네랄
                    Select Case fliterNum
                        Case 0
                            For k = 0 To 4
                                objtimageEnable(k) = True
                            Next
                            objtimageEnable(6) = True
                        Case 1
                            objtimageEnable(5) = True
                            objtimageEnable(7) = True
                        Case 2
                            For i = 0 To objtimageEnable.Count - 1
                                objtimageEnable(i) = True
                            Next
                        Case 3
                            For i = 0 To objtimageEnable.Count - 1
                                objtimageEnable(i) = False
                            Next
                    End Select
                Case 1 'statdata
                    '    ListBox4.Items.Add("일반 유닛") 0,1,2,3 + 19,20,21,22,23,24
                    '    ListBox4.Items.Add("인구보급 건물") 15,16,17,18
                    '    ListBox4.Items.Add("생산 중 건물") 4,5,6,7,8,9,10
                    '    ListBox4.Items.Add("건설 중 건물") 25,26
                    '    ListBox4.Items.Add("애드온 건설") 27,28,29
                    '    ListBox4.Items.Add("업그레이드 중") 12,13,14
                    '    ListBox4.Items.Add("유닛 부대 선택")
                    '    ListBox4.Items.Add("탑승 유닛 선택")

                    For k = 0 To 3
                        objtimageEnable(k) = True
                    Next

                    Select Case fliterNum
                        Case 0
                            For k = 19 To 24
                                objtimageEnable(k) = True
                            Next
                        Case 1
                            For k = 15 To 18
                                objtimageEnable(k) = True
                            Next
                        Case 2
                            For k = 4 To 10
                                objtimageEnable(k) = True
                            Next
                        Case 3
                            For k = 25 To 26
                                objtimageEnable(k) = True
                            Next
                        Case 4
                            For k = 27 To 29
                                objtimageEnable(k) = True
                            Next
                        Case 5
                            For k = 12 To 14
                                objtimageEnable(k) = True
                            Next

                        Case 6
                            For k = 30 To 32
                                objtimageEnable(k) = True
                            Next
                        Case 7
                            For k = 33 To 36
                                objtimageEnable(k) = True
                            Next
                        Case 8
                            For k = 37 To 44
                                objtimageEnable(k) = True
                            Next


                        Case 9
                            For k = 0 To 3
                                objtimageEnable(k) = False
                            Next
                            For k = 45 To 56
                                objtimageEnable(k) = True
                            Next
                        Case 10
                            For i = 0 To objtimageEnable.Count - 1
                                objtimageEnable(i) = True
                            Next
                        Case 11
                            For i = 0 To objtimageEnable.Count - 1
                                objtimageEnable(i) = False
                            Next
                    End Select

                Case 4 '미니맵

                    objtimageEnable(0) = True
                    Select Case fliterNum
                        Case 0
                            For k = 1 To 2
                                objtimageEnable(k) = True
                            Next
                        Case 1
                            For k = 3 To 3
                                objtimageEnable(k) = True
                            Next
                        Case 2
                            For i = 0 To objtimageEnable.Count - 1
                                objtimageEnable(i) = True
                            Next
                        Case 3
                            For i = 0 To objtimageEnable.Count - 1
                                objtimageEnable(i) = False
                            Next
                    End Select

                Case Else
                    Select Case fliterNum
                        Case 0
                            For i = 0 To objtimageEnable.Count - 1
                                objtimageEnable(i) = True
                            Next
                        Case 1
                            For i = 0 To objtimageEnable.Count - 1
                                objtimageEnable(i) = False
                            Next


                    End Select

            End Select
        End Sub
    End Class


    Public BinfileData As CsceneData
    Public PjcutData As List(Of CsceneData)
    Public Class CbinFile
        Public imagename As String
        Public pos As Point
        Public pos2 As Point
        Public size As Size


        Public ObjDlg As List(Of Cbinobject)
    End Class
    Public Class Cbinobject
        Public pos As Point
        Public pos2 As Point
        Public size As Size
        Public controltype As Integer

    End Class

    Public Class CsceneData
        Public binData(25) As CbinFile
        Public scenename As String

        Public Sub New()
            Dim file As FileStream
            scenename = "Default"
            For i = 0 To 25
                file = New FileStream(My.Application.Info.DirectoryPath & "\Data\bin\" & binfilename(i) & ".bin", FileMode.Open)
                Dim binreader As New BinaryReader(file)

                binData(i) = New CbinFile
                file.Position = 4
                binData(i).pos = New Point(binreader.ReadInt16, binreader.ReadInt16)
                binData(i).pos2 = New Point(binreader.ReadInt16, binreader.ReadInt16)
                file.Position = 12
                binData(i).size = New Size(binreader.ReadInt16, binreader.ReadInt16)



                binData(i).ObjDlg = New List(Of Cbinobject)


                '작은 오브젝트 읽어오기
                For k = 1 To file.Length \ 86 - 1
                    binData(i).ObjDlg.Add(New Cbinobject)
                    file.Position = 4 + k * 86
                    binData(i).ObjDlg(k - 1).pos = New Point(binreader.ReadInt16, binreader.ReadInt16)
                    binData(i).ObjDlg(k - 1).pos2 = New Point(binreader.ReadInt16, binreader.ReadInt16)
                    file.Position = 12 + k * 86
                    binData(i).ObjDlg(k - 1).size = New Size(binreader.ReadInt16, binreader.ReadInt16)
                    file.Position = 34 + k * 86
                    binData(i).ObjDlg(k - 1).controltype = binreader.ReadInt16
                Next


                binreader.Close()
                file.Close()
            Next
        End Sub
    End Class
    Public binfileobjectname(14) As String

    'Unit
    'Mairn
    '1
    '2
    '...
    'Ghost
    'Tech


    '버튼 데이터
    '위치, 아이콘, 사용가능, 사용뷸가, 조건, 조건값, 액션, 액션값
    Public Class SBtnDATA
        Public pos As UInt16
        Public icon As UInt16
        Public enaStr As UInt16
        Public disStr As UInt16
        Public con As UInt32
        Public conval As UInt16
        Public act As UInt32
        Public actval As UInt16

        Public Sub New(Optional data As String = "")
            If data <> "" Then
                Dim poss As Integer = 1
                pos = extratext(2, poss, data)
                icon = extratext(2, poss, data)
                con = extratext(4, poss, data)
                act = extratext(4, poss, data)
                conval = extratext(2, poss, data)
                actval = extratext(2, poss, data)
                enaStr = extratext(2, poss, data)
                disStr = extratext(2, poss, data)
            End If
        End Sub
        Private Function extratext(size As Integer, ByRef ptr As Integer, str As String)
            Dim returnvalue As ULong = 0
            For i = 0 To size - 1
                returnvalue += +Mid(str, ptr + 3 * i, 3) * 256 ^ i
            Next


            ptr += size * 3
            Return returnvalue
        End Function
    End Class


    Public BtnData() As List(Of SBtnDATA)
    Public Btnoffset() As UInteger

    Public ProjectBtnData() As List(Of SBtnDATA)
    Public ProjectBtnUSE() As Boolean
    '버튼셋 데이터
    '버튼 데이터 리스트




    'grpwire.grp
    'tranwire.grp
    'wirefram.grp
    'cmdicons.grp
    'stat_txt.tbl

    'AIscript.bin
    'iscript.bin




    'DatEdit
    '데이터 타입, 키, 인덱스별 밸류
    Private index As Dictionary(Of String, Integer)



    Public CODE As New List(Of List(Of String))



    Public Sub DataLoad()
        'DatEdit읽기 시작
        ReadDATAFile("units")
        ReadDATAFile("weapons") '무기
        ReadDATAFile("flingy") '비행정보
        ReadDATAFile("sprites") '스프라이트
        ReadDATAFile("images") '이미지
        ReadDATAFile("upgrades") '업그레이드
        ReadDATAFile("techdata") '기술
        ReadDATAFile("orders") '명령
        ReadDATAFile("sfxdata") '기술
        ReadDATAFile("portdata") '명령



        LoadCodeLIST("Units")
        LoadCodeLIST("Weapons")
        LoadCodeLIST("Flingy")
        LoadCodeLIST("Sprites")
        LoadCodeLIST("Images")
        LoadCodeLIST("Upgrades")
        LoadCodeLIST("Techdata")
        LoadCodeLIST("Orders")
        LoadCodeLIST("Sfxdata")
        LoadCodeLIST("Portdata")
        LoadCodeLIST("GRPfile")
        LoadCodeLIST("UnitBtn")
        LoadCodeLIST("Icon")

        LoadFiregraft()
        LoadbinEditor()
        '임시 데이터######################################
        'Dim filereader As New FileStream("C:\Users\skslj\Desktop\Memory.mem", FileMode.Open)
        'Dim filewriter As New FileStream("C:\Users\skslj\Desktop\require.dat", FileMode.Create)
        'Dim binreader As New BinaryReader(filereader)
        'Dim binwriter As New BinaryWriter(filewriter)
        ''50C000


        ''660A70시작 위치
        ''514178코드 시작 위치

        'binwriter.Write(CUShort(&HFFFF))
        'For i = 0 To CODE(DTYPE.units).Count - 1
        '    filereader.Position = &H660A70 + i * &H2 - &H50C000

        '    Dim StartPos As UInt16 = binreader.ReadUInt16()

        '    If StartPos <> 0 Then
        '        filereader.Position = StartPos * 2 + &H514176 - &H50C000


        '        Dim copcode As UInt16 = binreader.ReadUInt16()
        '        binwriter.Write(copcode)

        '        While copcode <> &HFFFF
        '            copcode = binreader.ReadUInt16()
        '            binwriter.Write(copcode)
        '        End While
        '    End If
        'Next



        'binwriter.Close()
        'binreader.Close()
        'filewriter.Close()
        'filereader.Close()
        '##################################################

        'MsgBox("종료")
        'End

        portdata = Readtblfile(My.Application.Info.DirectoryPath & "\Data\" & "portdata.tbl")
        sfxdata = Readtblfile(My.Application.Info.DirectoryPath & "\Data\" & "sfxdata.tbl")

        LoadTriggerData()
        LoadValueDef()

        LoadAnimHeader()
        readOpcodes()
    End Sub
    Private Sub LoadFiregraft()
        Dim FileStream As New FileStream(My.Application.Info.DirectoryPath & "\Data\" & "statusInfor.dat", FileMode.Open)
        Dim binReader As New BinaryReader(FileStream)

        Dim value As UInteger

        statusFn1.AddRange({4343040, 4344192, 4346240, 4345616, 4344656, 4344560, 4344512, 4348160, 4343072})
        statusFn2.AddRange({4353872, 4356240, 4357264, 4355232, 4355040, 4354656, 4357424, 4353760, 4349664})

        For i = 0 To 227
            FileStream.Position = &HC * i + 4
            value = binReader.ReadUInt32
            UnitStatusFn1(i) = statusFn1.FindIndex(Function(p)
                                                       Return p = value
                                                   End Function)


            FileStream.Position = &HC * i + 8
            value = binReader.ReadUInt32
            UnitStatusFn2(i) = statusFn2.FindIndex(Function(p As UInteger)
                                                       Return p = value
                                                   End Function)


            FileStream.Position = &HC * i
            value = binReader.ReadUInt32
            DebugID(i) = value
        Next

        binReader.Close()
        FileStream.Close()


        FileStream = New FileStream(My.Application.Info.DirectoryPath & "\Data\" & "btnset.dat", FileMode.Open)
        binReader = New BinaryReader(FileStream)




        Dim btncount As UInteger
        Dim btnadress As UInteger


        Dim btnobjectnum As UInteger = CODE(DTYPE.btnunit).Count
        ReDim BtnData(btnobjectnum - 1)
        ReDim Btnoffset(btnobjectnum - 1)

        ReDim ProjectBtnData(btnobjectnum - 1)
        ReDim ProjectBtnUSE(btnobjectnum - 1)

        For j = 0 To btnobjectnum - 1
            btncount = binReader.ReadUInt32() '버튼 수
            btnadress = binReader.ReadUInt32() '버튼 주소



            Btnoffset(j) = btnadress

            BtnData(j) = New List(Of SBtnDATA)
            ProjectBtnData(j) = New List(Of SBtnDATA)
            For i = 0 To btncount - 1
                Dim TBtn As New SBtnDATA
                TBtn.pos = binReader.ReadUInt16() 'pos
                TBtn.icon = binReader.ReadUInt16() 'icon
                TBtn.con = binReader.ReadUInt32() 'con
                TBtn.act = binReader.ReadUInt32() 'act
                TBtn.conval = binReader.ReadUInt16() 'conval
                TBtn.actval = binReader.ReadUInt16() 'actval
                TBtn.enaStr = binReader.ReadUInt16() 'enaStr
                TBtn.disStr = binReader.ReadUInt16() 'disStr

                BtnData(j).Add(TBtn)
            Next

        Next


        'Structure SBtnDATA
        'Public pos As Integer
        'Public icon As Integer
        'Public enaStr As Integer
        'Public disStr As Integer
        'Public con As Long
        'Public conval As Integer
        'Public act As Long
        'Public actval As Integer
        'End Structure

        'Public BtnData() As List(Of SBtnDATA)
        'Public Btnoffset() As UInteger

        'Public ProjectBtnData() As List(Of SBtnDATA)
        'Public ProjectBtnUSE() As Boolean

        binReader.Close()
        FileStream.Close()


        FileStream = New FileStream(My.Application.Info.DirectoryPath & "\Data\" & "FireGraftConFun.txt", FileMode.Open)
        Dim strReader As New StreamReader(FileStream)
        Dim str() As String = strReader.ReadToEnd.Split(vbCrLf)

        For i = 0 To str.Count - 1
            Dim tbtntype As New btnType

            tbtntype.Name = str(i).Split("	")(0).Trim
            tbtntype.FucOffset = "&H" & str(i).Split("	")(1)

            If str(i).Split("	").Count = 3 Then
                tbtntype.Code = str(i).Split("	")(2)
            Else
                tbtntype.Code = -1
            End If

            conbtnFnc.Add(tbtntype)
        Next





        strReader.Close()
        FileStream.Close()


        FileStream = New FileStream(My.Application.Info.DirectoryPath & "\Data\" & "FireGraftActFun.txt", FileMode.Open)
        strReader = New StreamReader(FileStream)
        str = strReader.ReadToEnd.Split(vbCrLf)

        For i = 0 To str.Count - 1
            Dim tbtntype As New btnType

            tbtntype.Name = str(i).Split("	")(0).Trim
            tbtntype.FucOffset = "&H" & str(i).Split("	")(1)

            If str(i).Split("	").Count = 3 Then
                tbtntype.Code = str(i).Split("	")(2)
            Else
                tbtntype.Code = -1
            End If



            actbtnFnc.Add(tbtntype)
        Next


        strReader.Close()
        FileStream.Close()
        Dim file As FileStream = New FileStream(My.Application.Info.DirectoryPath & "\Data\reqopcode.txt", FileMode.Open, FileAccess.Read)
        Dim stream As StreamReader = New StreamReader(file, System.Text.Encoding.Default)

        reqOpcode = stream.ReadToEnd.Split(vbCrLf, Integer.MaxValue, StringSplitOptions.RemoveEmptyEntries)
        reqOpcode(1) = " " & reqOpcode(1)
        'reqOpcode(0) = "  " & reqOpcode(0)
        'reqOpcode(31) = "  " & reqOpcode(31)
        'reqOpcode(32) = "  " & reqOpcode(32)
        'reqOpcode(33) = "  " & reqOpcode(33)


        stream.Close()
        file.Close()

        FileStream = New FileStream(My.Application.Info.DirectoryPath & "\Data\" & "require.dat", FileMode.Open)
        binReader = New BinaryReader(FileStream)

        For i = 0 To 4
            RequireData(i) = New List(Of SReqDATA)
            ProjectRequireData(i) = New List(Of SReqDATA)
            ProjectRequireDataUSE(i) = New List(Of UInteger)
        Next
        For i = 0 To CODE(DTYPE.units).Count - 1
            RequireData(0).Add(New SReqDATA)
            ProjectRequireData(0).Add(New SReqDATA)
            ProjectRequireDataUSE(0).Add(True)

            Dim cot As Integer = RequireData(0).Count - 1
            RequireData(0)(cot).pos = binReader.ReadUInt16()
        Next
        For i = 0 To CODE(DTYPE.upgrades).Count - 1
            RequireData(1).Add(New SReqDATA)
            ProjectRequireData(1).Add(New SReqDATA)
            ProjectRequireDataUSE(1).Add(True)

            Dim cot As Integer = RequireData(1).Count - 1
            RequireData(1)(cot).pos = binReader.ReadUInt16()
        Next
        For i = 0 To CODE(DTYPE.techdata).Count - 2
            RequireData(2).Add(New SReqDATA)
            ProjectRequireData(2).Add(New SReqDATA)
            ProjectRequireDataUSE(2).Add(True)

            Dim cot As Integer = RequireData(2).Count - 1
            RequireData(2)(cot).pos = binReader.ReadUInt16()
        Next
        For i = 0 To CODE(DTYPE.techdata).Count - 2
            RequireData(3).Add(New SReqDATA)
            ProjectRequireData(3).Add(New SReqDATA)
            ProjectRequireDataUSE(3).Add(True)

            Dim cot As Integer = RequireData(3).Count - 1
            RequireData(3)(cot).pos = binReader.ReadUInt16()
        Next
        For i = 0 To CODE(DTYPE.orders).Count - 2
            RequireData(4).Add(New SReqDATA)
            ProjectRequireData(4).Add(New SReqDATA)
            ProjectRequireDataUSE(4).Add(True)

            Dim cot As Integer = RequireData(4).Count - 1
            RequireData(4)(cot).pos = binReader.ReadUInt16()
        Next






        Dim pos() As UInteger = {&H46C, &H8B4, &HBFC, &HD3C, &HFEC}
        Dim tnum As Integer
        Dim codetype() As Integer = {DTYPE.units, DTYPE.upgrades, DTYPE.techdata, DTYPE.techdata, DTYPE.orders}
        For k = 0 To 4
            tnum = k

            Dim count As Integer = CODE(codetype(k)).Count - 1
            If k = 2 Or k = 3 Or k = 4 Then
                count -= 1
            End If


            For i = 0 To count
                RequireData(tnum)(i).Code = New List(Of UInt16)
                ProjectRequireData(tnum)(i).Code = New List(Of UInt16)





                If RequireData(tnum)(i).pos <> 0 Then



                    FileStream.Position = pos(k) + RequireData(tnum)(i).pos * 2

                    Dim opcode As UInt16 = 1
                    While True
                        Dim issubeol As Boolean
                        opcode = binReader.ReadUInt16()
                        If opcode = &HFF1F Or opcode = &HFF20 Then
                            issubeol = True
                        End If

                        If opcode <> &HFFFF Then
                            RequireData(tnum)(i).Code.Add(opcode)
                        End If
                        If opcode = &HFFFF Then
                            If issubeol Then
                                RequireData(tnum)(i).Code.Add(opcode)
                                issubeol = False
                            Else
                                Exit While
                            End If
                        End If
                    End While

                End If

            Next


        Next

        binReader.Close()
        FileStream.Close()
    End Sub
    Public Sub LoadbinEditor()
        Dim file As New FileStream(My.Application.Info.DirectoryPath & "\Data\bin\" & "binFile.txt", FileMode.Open)
        Dim strreader As New StreamReader(file)


        ReDim binfilename(25)
        Dim i As Integer = 0
        While strreader.EndOfStream = False
            binfilename(i) = strreader.ReadLine()
            i += 1
        End While
        strreader.Close()
        file.Close()


        ReDim binfileptr(25)
        file = New FileStream(My.Application.Info.DirectoryPath & "\Data\bin\" & "binFileptr.txt", FileMode.Open)
        strreader = New StreamReader(file)
        i = 0
        While strreader.EndOfStream = False
            binfileptr(i) = strreader.ReadLine()
            i += 1
        End While
        strreader.Close()
        file.Close()



        file = New FileStream(My.Application.Info.DirectoryPath & "\Data\bin\" & "object.txt", FileMode.Open)
        strreader = New StreamReader(file)
        i = 0
        While strreader.EndOfStream = False
            binfileobjectname(i) = strreader.ReadLine()
            i += 1
        End While
        strreader.Close()
        file.Close()







        BinfileData = New CsceneData

        PjcutData = New List(Of CsceneData)
        PjcutData.Add(New CsceneData)


        Dim pbmp As New Bitmap(My.Application.Info.DirectoryPath & "\Data\bin\" & "pconsole.png")
        Dim tebmp As New Bitmap(My.Application.Info.DirectoryPath & "\Data\bin\" & "tconsole.png")
        Dim zbmp As New Bitmap(My.Application.Info.DirectoryPath & "\Data\bin\" & "zconsole.png")

        DefaultBinBitmap = New List(Of binimage)
        For i = 0 To 25
            DefaultBinBitmap.Add(New binimage)

            Dim tbmp As New Bitmap(BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height)


            Dim grp As Graphics = Graphics.FromImage(tbmp)

            Select Case i
                Case 1 To 4

                    grp.DrawImage(pbmp, New Rectangle(0, 0, BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height), New Rectangle(BinfileData.binData(i).pos.X, BinfileData.binData(i).pos.Y, BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height), GraphicsUnit.Pixel)

                    DefaultBinBitmap(i).pbmp = tbmp

                    tbmp = New Bitmap(BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height)
                    grp = Graphics.FromImage(tbmp)
                    grp.DrawImage(tebmp, New Rectangle(0, 0, BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height), New Rectangle(BinfileData.binData(i).pos.X, BinfileData.binData(i).pos.Y, BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height), GraphicsUnit.Pixel)

                    DefaultBinBitmap(i).tbmp = tbmp

                    tbmp = New Bitmap(BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height)
                    grp = Graphics.FromImage(tbmp)
                    grp.DrawImage(zbmp, New Rectangle(0, 0, BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height), New Rectangle(BinfileData.binData(i).pos.X, BinfileData.binData(i).pos.Y, BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height), GraphicsUnit.Pixel)

                    DefaultBinBitmap(i).zbmp = tbmp
                Case 5 To 10
                    grp.DrawImage(pbmp, New Rectangle(0, 0, BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height), New Rectangle(BinfileData.binData(i).pos.X, BinfileData.binData(i).pos.Y, BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height), GraphicsUnit.Pixel)
                    DefaultBinBitmap(i).bmp = tbmp
                Case 11 To 17
                    grp.DrawImage(tebmp, New Rectangle(0, 0, BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height), New Rectangle(BinfileData.binData(i).pos.X, BinfileData.binData(i).pos.Y, BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height), GraphicsUnit.Pixel)
                    DefaultBinBitmap(i).bmp = tbmp
                Case 18 To 25
                    grp.DrawImage(zbmp, New Rectangle(0, 0, BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height), New Rectangle(BinfileData.binData(i).pos.X, BinfileData.binData(i).pos.Y, BinfileData.binData(i).size.Width, BinfileData.binData(i).size.Height), GraphicsUnit.Pixel)
                    DefaultBinBitmap(i).bmp = tbmp


            End Select


            DefaultBinBitmap(i).objtimage = New List(Of Bitmap)
            DefaultBinBitmap(i).objtimageEnable = New List(Of Boolean)
            For k = 0 To BinfileData.binData(i).ObjDlg.Count - 1
                DefaultBinBitmap(i).objtimageEnable.Add(False)

            Next
            '작은 오브젝트 읽어오기
            Select Case i
                Case 0
                    '0  
                    DefaultBinBitmap(i).objtimage.Add(New Bitmap(New Bitmap(My.Application.Info.DirectoryPath & "\Data\bin\" & "statres5.png")))

                    '1
                    DefaultBinBitmap(i).objtimage.Add(New Bitmap(New Bitmap(My.Application.Info.DirectoryPath & "\Data\bin\" & "statres4.png")))

                    '2
                    DefaultBinBitmap(i).objtimage.Add(New Bitmap(New Bitmap(My.Application.Info.DirectoryPath & "\Data\bin\" & "statres3.png")))

                    '3
                    DefaultBinBitmap(i).objtimage.Add(New Bitmap(New Bitmap(My.Application.Info.DirectoryPath & "\Data\bin\" & "statres2.png")))

                    '4
                    DefaultBinBitmap(i).objtimage.Add(New Bitmap(New Bitmap(My.Application.Info.DirectoryPath & "\Data\bin\" & "statres1.png")))

                    '5
                    DefaultBinBitmap(i).objtimage.Add(Nothing)

                    '6 카운트타이머
                    DefaultBinBitmap(i).objtimage.Add(New Bitmap(New Bitmap(My.Application.Info.DirectoryPath & "\Data\bin\" & "statres0.png")))


                Case 1
                    For k = 0 To 56
                        Dim filename As String = My.Application.Info.DirectoryPath & "\Data\bin\" & "statdata" & k & ".png"
                        If CheckFileExist(filename) = False Then
                            Dim nbitmap As New Bitmap(filename)
                            DefaultBinBitmap(i).objtimage.Add(nbitmap)
                        Else
                            DefaultBinBitmap(i).objtimage.Add(Nothing)
                        End If

                    Next






                Case 4
                    DefaultBinBitmap(i).objtimage.Add(Nothing)
                    For k = 1 To 3
                        DefaultBinBitmap(i).objtimage.Add(New Bitmap(My.Application.Info.DirectoryPath & "\Data\bin\" & "minimap" & k & ".png"))
                    Next

                    DefaultBinBitmap(i).objtimage.Add(Nothing)
                    For k = 4 To 6
                        DefaultBinBitmap(i).objtimage.Add(New Bitmap(My.Application.Info.DirectoryPath & "\Data\bin\" & "minimap" & k & ".png"))
                    Next

                    DefaultBinBitmap(i).objtimage.Add(Nothing)
                    For k = 7 To 9
                        DefaultBinBitmap(i).objtimage.Add(New Bitmap(My.Application.Info.DirectoryPath & "\Data\bin\" & "minimap" & k & ".png"))
                    Next
            End Select
            DefaultBinBitmap(i).SetEnable(0)
        Next
    End Sub


    Public Sub DataLoadAfterProgramLoad()
        'LoadFileimportable()
    End Sub


    Public Sub LoadFileimportable()
        Dim mpq As New SFMpq

        Dim icongrp As New GRP

        DatEditForm.ICONILIST.Images.Clear()

        If dataDumper_cmdicons_f <> 0 And CheckFileExist(dataDumper_cmdicons) = False Then
            Dim FileStream As New FileStream(dataDumper_cmdicons, FileMode.Open)
            Dim memsteram As New BinaryReader(FileStream)

            icongrp.LoadGRP(memsteram.ReadBytes(FileStream.Length))
            icongrp.LoadPalette(PalettType.Icons)

            For i = 0 To icongrp.framecount - 1
                Dim bitmap As New Bitmap(36, 34)
                Dim grp As Graphics
                grp = Graphics.FromImage(bitmap)
                grp.Clear(Color.Black)

                grp.DrawImage(icongrp.DrawGRP(i), (36 - icongrp.GRPFrame(i).frameWidth) \ 2, (34 - icongrp.GRPFrame(i).frameHeight) \ 2)

                DatEditForm.ICONILIST.Images.Add(bitmap)
            Next




            memsteram.Close()
            FileStream.Close()
        Else
            icongrp.LoadGRP(mpq.ReaddatFile("unit\cmdbtns\cmdicons.grp"))
            icongrp.LoadPalette(PalettType.Icons)

            For i = 0 To icongrp.framecount - 1
                Dim bitmap As New Bitmap(36, 34)
                Dim grp As Graphics
                grp = Graphics.FromImage(bitmap)
                grp.Clear(Color.Black)

                grp.DrawImage(icongrp.DrawGRP(i), (36 - icongrp.GRPFrame(i).frameWidth) \ 2, (34 - icongrp.GRPFrame(i).frameHeight) \ 2)

                DatEditForm.ICONILIST.Images.Add(bitmap)
                'DatEditForm.ICONILIST.ImageSize = Bitmap.Size
            Next
        End If



        If dataDumper_stat_txt_f <> 0 Then

            Try
                stat_txt = Readstat_txtfile(True)
                ' MsgBox("아")
            Catch ex As Exception
                MsgBox(dataDumper_stat_txt & " 해당 파일이 정상적이지 않습니다. 기본 stat_txt.tbl을 불러옵니다.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)

                stat_txt = Readtblfile(My.Application.Info.DirectoryPath & "\Data\" & "stat_txt.tbl", True)
            End Try

        Else
            stat_txt = Readtblfile(My.Application.Info.DirectoryPath & "\Data\" & "stat_txt.tbl", True)
        End If




        ' DatEditForm.Loadstattxt()

        If iscriptPatcheruse = True Then
            Try
                Dim FileStream As New FileStream(iscriptPatcher, FileMode.Open)
                Dim memsteram As New BinaryReader(FileStream)



                iscript.LoadIscriptToBuff(memsteram.ReadBytes(FileStream.Length))

                memsteram.Close()
                FileStream.Close()
            Catch ex As Exception
                iscript.LoadIscriptToBuff(mpq.ReaddatFile("scripts\iscript.bin"))
            End Try
        ElseIf dataDumper_iscript_f <> 0 Then
            Try
                Dim FileStream As New FileStream(dataDumper_iscript, FileMode.Open)
                Dim memsteram As New BinaryReader(FileStream)



                iscript.LoadIscriptToBuff(memsteram.ReadBytes(FileStream.Length))

                memsteram.Close()
                FileStream.Close()
            Catch ex As Exception
                iscript.LoadIscriptToBuff(mpq.ReaddatFile("scripts\iscript.bin"))
            End Try
        Else
            iscript.LoadIscriptToBuff(mpq.ReaddatFile("scripts\iscript.bin"))
        End If
    End Sub
    Private Sub LoadAnimHeader()
        Dim FileStream As New FileStream(My.Application.Info.DirectoryPath & "\Data\" & "AnimHeader.txt", FileMode.Open)
        Dim strreader As New StreamReader(FileStream)

        Dim text As String = strreader.ReadToEnd

        strreader.Close()
        FileStream.Close()

        Dim line() As String = text.Split(vbCrLf)
        Dim count As Integer = line.Count - 1



        ReDim HEADERNAME(count)
        ReDim HEADERINFOR(count)

        For i = 0 To count
            HEADERNAME(i) = line(i).Split("-")(0).Trim
            HEADERINFOR(i) = line(i).Split("-")(1).Trim
        Next

    End Sub




    Private Function ReadDEF(BaseDEFtext As String, Key As String)
        Try
            Dim temptext2 As String
            Dim start As Integer = InStr(BaseDEFtext, vbCrLf & Key)
            If start > 0 Then
                Dim temptext As String = Mid(BaseDEFtext, start)

                temptext = Mid(temptext, InStr(temptext, "="))
                'Mid(base, InStr(base, Key), 4)
                If InStr(temptext, vbCrLf) = 0 Then
                    temptext2 = Mid(temptext, 2).Trim
                    If InStr(temptext2, ":") = 0 Then
                        Return temptext2
                    Else
                        Return Mid(temptext2, 1, InStr(temptext2, ":"))
                    End If
                Else
                    temptext2 = Mid(temptext, 2, InStr(temptext, vbCrLf) - 1).Trim
                    If InStr(temptext2, ":") = 0 Then
                        Return temptext2
                    Else
                        Return Mid(temptext2, 1, InStr(temptext2, ":"))
                    End If
                End If
            Else
                Return "false"
            End If
        Catch ex As Exception
            Return "false"
        End Try
    End Function


    '# DatEdit에서 사용하는 것.
    Public Function ReadDATAFileFromDat(Filename As String) As Integer 'In DatEdt
        Dim tempfilenaem As String = Filename
        Dim tempdata As CDatEdit = New CDatEdit
        tempdata.typeName = Filename




        Dim mpq As New SFMpq
        'Dim memstream As New MemoryStream(mpq.ReaddatFile("arr\" & Filename & ".dat"))

        'MsgBox(memstream.Length & "  파일이름" & Filename)
        Dim File As FileStream = New FileStream(Filename, FileMode.Open)
        Dim byteReader As BinaryReader = New BinaryReader(File)


        Dim DatNum As Integer = -1
        For i = 0 To DatEditDATA.Count - 1
            If DatEditDATA(i).filesze = File.Length Then
                DatNum = i
                Exit For
            End If
        Next
        If DatNum = -1 Then
            MsgBox(Filename & "는(은) 정상적인 dat파일이 아닙니다.!", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            Return 0
        End If




        Dim msgdef As String = My.Application.Info.DirectoryPath & "\Data\" & DatEditDATA(DatNum).typeName & ".def"

        Dim Filedef As FileStream = New FileStream(msgdef, FileMode.Open)
        Dim Reader As StreamReader = New StreamReader(Filedef)
        Dim byteReaderdef As BinaryReader = New BinaryReader(Filedef)
        Dim defFile As String = Reader.ReadToEnd
        Reader.Close()
        byteReaderdef.Close()
        Filedef.Close()



        Dim Varcount As UInteger = ReadDEF(defFile, "Varcount")
        Dim InputEntrycount As UInteger = ReadDEF(defFile, "InputEntrycount") - 1
        Dim OutputEntrycount As UInteger = ReadDEF(defFile, "OutputEntrycount") - 1
        Dim tempkinfo As New CDatEdit.KINFO


        For i = 0 To Varcount - 1
            If ReadDEF(defFile, i & "Name") = "Unknown" Then
                tempdata.keyDic.Add(Filename & ReadDEF(defFile, i & "Name"), i)
            Else
                'TestText = TestText & tempfilenaem & "_" & ReadDEF(defFile, i & "Name") & "=0x" & vbCrLf
                tempdata.keyDic.Add(ReadDEF(defFile, i & "Name"), i)
            End If

            tempkinfo.Size = ReadDEF(defFile, i & "Size")

            If ReadDEF(defFile, i & "VarStart") = "false" Then
                tempkinfo.VarStart = 0
            Else
                tempkinfo.VarStart = ReadDEF(defFile, i & "VarStart")
            End If
            If ReadDEF(defFile, i & "VarEnd") = "false" Then
                tempkinfo.VarEnd = InputEntrycount
            Else
                tempkinfo.VarEnd = ReadDEF(defFile, i & "VarEnd")
            End If
            tempdata.keyINFO.Add(tempkinfo)

            Dim Entrycount As Integer = 0
            If tempkinfo.VarStart = 0 And tempkinfo.VarEnd = InputEntrycount Then
                Entrycount = InputEntrycount
            ElseIf tempkinfo.VarStart <> 0 And tempkinfo.VarEnd = InputEntrycount Then
                Entrycount = tempkinfo.VarEnd - tempkinfo.VarStart
            Else
                Entrycount = tempkinfo.VarEnd - tempkinfo.VarStart
            End If

            '총 사이즈는 VarArray * Size
            '현재 읽을 것은 (VarArrayIndex - 1) * Size

            '위치 = (VarArrayIndex - 1) * Size
            '위치가 2면 포지션을 2만큼 넘기고 2만큼 읽는다.

            '총 사이즈가 16이고 위치가 12이면 그리고 사이즈가 4이면
            '12(위치) 만큼 넘기고 4(사이즈)만큼 읽고 0(총사이즈 - 위치 - 사이즈)만큼 넘긴다.

            Dim VarArray As Integer = 1
            Dim VarArrayIndex As Integer = 1
            Dim FilePos As UInteger
            If ReadDEF(defFile, i & "VarArray") <> "false" Then
                VarArray = ReadDEF(defFile, i & "VarArray")
                FilePos = File.Position

                VarArrayIndex = ReadDEF(defFile, i & "VarArrayIndex")
            End If



            For j = 0 To Entrycount
                Dim value As UInteger


                File.Position += (VarArrayIndex - 1) * tempkinfo.Size
                Select Case tempkinfo.Size
                    Case 1
                        value = byteReader.ReadByte()
                    Case 2
                        value = byteReader.ReadUInt16()
                    Case 4
                        value = byteReader.ReadUInt32()
                End Select
                File.Position += VarArray * tempkinfo.Size - (VarArrayIndex - 1) * tempkinfo.Size - tempkinfo.Size



                'If ReadDEF(defFile, i & "Name") = "Target Acquisition Range" And value = 0 Then
                '    Dim TargetRange() As Byte = {4, 7, 5, 6, 6, 8, 8, 1, 5, 0, 3, 0, 6, 3, 0, 0, 6, 5, 5, 5, 5,
                '        5, 0, 7, 7, 12, 12, 6, 6, 6, 12, 12, 3, 0, 9, 0, 0, 3, 4, 3, 3, 1, 0, 3, 8, 8, 0, 3, 3,
                '        8, 3, 3, 0, 5, 3, 3, 8, 0, 6, 0, 9, 3, 7, 7, 1, 3, 4, 3, 3, 0, 4, 5, 8, 4, 3, 3, 3, 3,
                '        4, 3, 4, 8, 8, 8, 0, 4, 5, 3, 4, 0, 0, 8, 4, 0, 0, 0, 0, 0, 9, 6, 6, 0, 6, 6, 6, 0, 0,
                '        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                '        0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0,
                '        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                '        0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 5, 5, 2, 5, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                '        0, 0, 0, 0, 0}

                '    value = TargetRange(j)
                'End If


                DatEditDATA(DatNum).projectdata(i)(j) = CLng(value) - DatEditDATA(DatNum).data(i)(j) - DatEditDATA(DatNum).mapdata(i)(j)
            Next



            If VarArray <> VarArrayIndex And VarArray <> 0 Then
                File.Position = FilePos
            End If
        Next


        'value들을 미리 읽어서 저장해 두자.

        '시작은 Varcount=59에서 'Varcount'을 이용해 뒤에 값을 읽는 함수를 만든다.
        'Varcount를 먼저 읽고 input과 output을 이용해 최대 값을 구해준다.


        '미리 정의된 키와 연결된 밸류 배열을 만든다. 배열의 크기는 각각 계산한다.
        '2차원 배열로 정의하고 몇번째 밸류인지를 알기 위해서는 사전을 이용한다.



        'Def읽기


        'Mid(defFile, InStr(defFile, "key"))


        '파일읽기


        byteReader.Close()
        File.Close()



        'DatEditDAT.Add(tempdata)
        Return DatNum
    End Function
    Public Sub WriteDATAFileFromDat(Filename As String, DatNum As Integer)  'In DatEdt
        Dim File As FileStream = New FileStream(Filename, FileMode.Create)
        Dim bytewriter As BinaryWriter = New BinaryWriter(File)


        For i = 0 To DatEditDATA(DatNum).data.Count - 1
            For j = 0 To DatEditDATA(DatNum).data(i).Count - 1
                Dim value As UInteger = DatEditDATA(DatNum).data(i)(j) +
                    DatEditDATA(DatNum).projectdata(i)(j) +
                    DatEditDATA(DatNum).mapdata(i)(j)
                Dim key As String = DatEditDATA(DatNum).keyDic.Keys(i)


                If key = "StarEdit Placement Box Width" Or
                   key = "Addon Horizontal (X) Position" Then

                    bytewriter.Write(CUShort(value))

                    value = DatEditDATA(DatNum).data(i + 1)(j) +
                    DatEditDATA(DatNum).projectdata(i + 1)(j) +
                    DatEditDATA(DatNum).mapdata(i + 1)(j)

                    bytewriter.Write(CUShort(value))
                End If
                If key = "Unit Size Left" Then

                    bytewriter.Write(CUShort(value))

                    value = DatEditDATA(DatNum).data(i + 1)(j) +
                    DatEditDATA(DatNum).projectdata(i + 1)(j) +
                    DatEditDATA(DatNum).mapdata(i + 1)(j)

                    bytewriter.Write(CUShort(value))

                    value = DatEditDATA(DatNum).data(i + 2)(j) +
                    DatEditDATA(DatNum).projectdata(i + 2)(j) +
                    DatEditDATA(DatNum).mapdata(i + 2)(j)

                    bytewriter.Write(CUShort(value))

                    value = DatEditDATA(DatNum).data(i + 3)(j) +
                    DatEditDATA(DatNum).projectdata(i + 3)(j) +
                    DatEditDATA(DatNum).mapdata(i + 3)(j)

                    bytewriter.Write(CUShort(value))
                End If



                If key <> "StarEdit Placement Box Width" And
                    key <> "StarEdit Placement Box Height" And
                    key <> "Addon Horizontal (X) Position" And
                    key <> "Addon Vertical (Y) Position" And
                    key <> "Unit Size Left" And
                    key <> "Unit Size Up" And
                    key <> "Unit Size Right" And
                    key <> "Unit Size Down" Then

                    Select Case DatEditDATA(DatNum).keyINFO(i).Size
                        Case 1
                            bytewriter.Write(CByte(value))
                        Case 2
                            bytewriter.Write(CUShort(value))
                        Case 4
                            bytewriter.Write(CUInt(value))
                    End Select
                End If

            Next
        Next

        bytewriter.Close()
        File.Close()
    End Sub
    Private Function ReadDATAFile(Filename As String) As String 'In DatEdt
        Dim tempfilenaem As String = Filename
        Dim tempdata As CDatEdit = New CDatEdit
        tempdata.typeName = Filename

        Filename = My.Application.Info.DirectoryPath & "\Data\" & Filename

        Dim File As FileStream = New FileStream(Filename & ".def", FileMode.Open)
        Dim Reader As StreamReader = New StreamReader(File)
        Dim byteReader As BinaryReader = New BinaryReader(File)
        Dim defFile As String = Reader.ReadToEnd
        Reader.Close()
        byteReader.Close()
        File.Close()


        Dim mpq As New SFMpq
        'Dim memstream As New MemoryStream(mpq.ReaddatFile("arr\" & Filename & ".dat"))

        'MsgBox(memstream.Length & "  파일이름" & Filename)
        File = New FileStream(Filename & ".dat", FileMode.Open)
        tempdata.filesze = File.Length

        byteReader = New BinaryReader(File)

        Dim Varcount As UInteger = ReadDEF(defFile, "Varcount")
        Dim InputEntrycount As UInteger = ReadDEF(defFile, "InputEntrycount") - 1
        Dim OutputEntrycount As UInteger = ReadDEF(defFile, "OutputEntrycount") - 1
        Dim tempkinfo As New CDatEdit.KINFO


        For i = 0 To Varcount - 1
            tempdata.data.Add(New List(Of UInteger))
            tempdata.projectdata.Add(New List(Of Long))
            tempdata.mapdata.Add(New List(Of Long))



            If ReadDEF(defFile, i & "Name") = "Unknown" Then
                tempdata.keyDic.Add(Filename & ReadDEF(defFile, i & "Name"), i)
            Else
                'TestText = TestText & tempfilenaem & "_" & ReadDEF(defFile, i & "Name") & "=0x" & vbCrLf
                tempdata.keyDic.Add(ReadDEF(defFile, i & "Name"), i)
            End If

            If ReadDEF(defFile, i & "VarArray") <> False Then
                tempkinfo.realSize = ReadDEF(defFile, i & "VarArray") * ReadDEF(defFile, i & "Size")
            Else
                tempkinfo.realSize = ReadDEF(defFile, i & "Size")
            End If

            tempkinfo.Size = ReadDEF(defFile, i & "Size")

            If ReadDEF(defFile, i & "VarStart") = "false" Then
                tempkinfo.VarStart = 0
            Else
                tempkinfo.VarStart = ReadDEF(defFile, i & "VarStart")
            End If
            If ReadDEF(defFile, i & "VarEnd") = "false" Then
                tempkinfo.VarEnd = InputEntrycount
            Else
                tempkinfo.VarEnd = ReadDEF(defFile, i & "VarEnd")
            End If
            tempdata.keyINFO.Add(tempkinfo)


            Dim Entrycount As Integer = 0
            If tempkinfo.VarStart = 0 And tempkinfo.VarEnd = InputEntrycount Then
                Entrycount = InputEntrycount
            ElseIf tempkinfo.VarStart <> 0 And tempkinfo.VarEnd = InputEntrycount Then
                Entrycount = tempkinfo.VarEnd - tempkinfo.VarStart
            Else
                Entrycount = tempkinfo.VarEnd - tempkinfo.VarStart
            End If


            '총 사이즈는 VarArray * Size
            '현재 읽을 것은 (VarArrayIndex - 1) * Size

            '위치 = (VarArrayIndex - 1) * Size
            '위치가 2면 포지션을 2만큼 넘기고 2만큼 읽는다.

            '총 사이즈가 16이고 위치가 12이면 그리고 사이즈가 4이면
            '12(위치) 만큼 넘기고 4(사이즈)만큼 읽고 0(총사이즈 - 위치 - 사이즈)만큼 넘긴다.

            Dim VarArray As Integer = 1
            Dim VarArrayIndex As Integer = 1
            Dim FilePos As UInteger
            If ReadDEF(defFile, i & "VarArray") <> "false" Then
                VarArray = ReadDEF(defFile, i & "VarArray")
                FilePos = File.Position

                VarArrayIndex = ReadDEF(defFile, i & "VarArrayIndex")
            End If



            For j = 0 To Entrycount
                Dim value As UInteger


                File.Position += (VarArrayIndex - 1) * tempkinfo.Size
                Select Case tempkinfo.Size
                    Case 1
                        value = byteReader.ReadByte()
                    Case 2
                        value = byteReader.ReadUInt16()
                    Case 4
                        value = byteReader.ReadUInt32()
                End Select
                File.Position += VarArray * tempkinfo.Size - (VarArrayIndex - 1) * tempkinfo.Size - tempkinfo.Size


                tempdata.mapdata(tempdata.mapdata.Count - 1).Add(0)
                tempdata.projectdata(tempdata.projectdata.Count - 1).Add(0)


                tempdata.data(tempdata.data.Count - 1).Add(value)
            Next


            If VarArray <> VarArrayIndex And VarArray <> 0 Then
                File.Position = FilePos
            End If
        Next
        'value들을 미리 읽어서 저장해 두자.

        '시작은 Varcount=59에서 'Varcount'을 이용해 뒤에 값을 읽는 함수를 만든다.
        'Varcount를 먼저 읽고 input과 output을 이용해 최대 값을 구해준다.


        '미리 정의된 키와 연결된 밸류 배열을 만든다. 배열의 크기는 각각 계산한다.
        '2차원 배열로 정의하고 몇번째 밸류인지를 알기 위해서는 사전을 이용한다.



        'Def읽기


        'Mid(defFile, InStr(defFile, "key"))


        '파일읽기


        byteReader.Close()
        File.Close()



        DatEditDATA.Add(tempdata)
        Return 1
    End Function






    Private Sub LoadCodeLIST(filename As String)
        Dim filepath As String = My.Application.Info.DirectoryPath & "\Data\Langage\" & My.Settings.Langage & "\" & filename & ".txt"

        Dim File As FileStream = New FileStream(filepath, FileMode.Open)
        Dim Reader As StreamReader = New StreamReader(File)

        Dim index As Integer = 0

        CODE.Add(New List(Of String))

        While (Reader.EndOfStream = False)
            Dim temp As String

            temp = Reader.ReadLine()

            CODE(CODE.Count - 1).Add(temp)

            index += 1
        End While

        Reader.Close()
        File.Close()
    End Sub



    Public Class CDatEdit
        Public typeName As String 'units, weapons 등등
        Public filesze As Integer 'units, weapons 등등
        Public data As New List(Of List(Of UInteger)) 'HP 값
        Public projectdata As New List(Of List(Of Long)) '프로젝트 값.
        Public mapdata As New List(Of List(Of Long)) '프로젝트 값.

        Public keyDic As New Dictionary(Of String, UInteger)
        Public keyINFO As New List(Of KINFO)

        Structure KINFO
            Public realSize As Integer
            Public Size As Integer
            Public VarStart As Integer
            Public VarEnd As Integer
        End Structure


        Public Sub Reset()
            MapReset()
            projectReset()

        End Sub
        Public Sub MapReset()
            'data
            'Public projectdata As New List(Of List(Of Long)) 이거 두개를 초기화한다.
            'Public mapdata As New List(Of List(Of Long))
            mapdata.Clear()
            For i = 0 To data.Count - 1
                mapdata.Add(New List(Of Long))
                For j = 0 To data(i).Count - 1
                    mapdata(i).Add(0)
                Next
            Next
        End Sub
        Public Sub projectReset()
            'data
            'Public projectdata As New List(Of List(Of Long)) 이거 두개를 초기화한다.
            'Public mapdata As New List(Of List(Of Long))
            projectdata.Clear()
            For i = 0 To data.Count - 1
                projectdata.Add(New List(Of Long))
                For j = 0 To data(i).Count - 1
                    projectdata(i).Add(0)
                Next
            Next
        End Sub



        Public Function ReadValue(key As String, index As UInteger)

            Return data(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart) + projectdata(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart) + mapdata(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart)

        End Function
        Public Function ReadValueNum(key As Integer, index As UInteger)
            Return data(key)(index - keyINFO(key).VarStart) + projectdata(key)(index - keyINFO(key).VarStart) + mapdata(key)(index - keyINFO(key).VarStart)
        End Function


        Public Function WriteValue(key As String, index As UInteger, value As Long)
            ProjectSet.saveStatusChange()
            Dim tempvalue As Long = value - data(keyDic(key))(index - keyINFO(keyDic(key)).VarStart)


            If value >= Math.Pow(256, keyINFO(keyDic(key)).Size) Then
                projectdata(keyDic(key))(index - keyINFO(keyDic(key)).VarStart) = Math.Pow(256, keyINFO(keyDic(key)).Size) - 1 - data(keyDic(key))(index - keyINFO(keyDic(key)).VarStart) - mapdata(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart)

                If DatEditForm.ListBox1.Items.Count <> 0 Then
                    DatEditForm.ListBox1.SelectedItem(2) = CheckChangeAll(index)
                End If
                Return True
            Else
                projectdata(keyDic(key))(index - keyINFO(keyDic(key)).VarStart) = value - data(keyDic(key))(index - keyINFO(keyDic(key)).VarStart) - mapdata(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart)

                If DatEditForm.ListBox1.Items.Count <> 0 Then
                    DatEditForm.ListBox1.SelectedItem(2) = CheckChangeAll(index)
                End If
            End If
            Return False
        End Function
        Public Function WriteValueNum(key As Integer, index As UInteger, value As Long, Optional Loading As Boolean = False)
            Dim restrict As Boolean = True
            If (typeName = "sfxdata" And key = 0) Or typeName = "portdata" Or (typeName = "images" And key = 0) Then
                restrict = False
            End If


            If restrict Then

                ProjectSet.saveStatusChange()
                Dim tempvalue As Long = value - data(key)(index - keyINFO(key).VarStart)


                If value >= Math.Pow(256, keyINFO(key).Size) Then
                    projectdata(key)(index - keyINFO(key).VarStart) = Math.Pow(256, keyINFO(key).Size) - 1 - data(key)(index - keyINFO(key).VarStart) - mapdata(key)(index - keyINFO(key).VarStart)

                    If DatEditForm.ListBox1.Items.Count <> 0 Then
                        DatEditForm.ListBox1.SelectedItem(2) = CheckChangeAll(index)
                    End If
                    Return True
                Else
                    projectdata(key)(index - keyINFO(key).VarStart) = value - data(key)(index - keyINFO(key).VarStart) - mapdata(key)(index - keyINFO(key).VarStart)

                    If Loading = False Then
                        If DatEditForm.ListBox1.Items.Count <> 0 Then
                            DatEditForm.ListBox1.SelectedItem(2) = CheckChangeAll(index)
                        End If
                    End If
                End If
            End If
            Return False
        End Function
        'projectdata(keyDic(key))(index - keyINFO(keyDic(key)).VarStart)
        ' projectdata(데이터넘버)(index - keyINFO(데이터넘버).VarStart)
        ' projectdata(데이터넘버)(유닛넘버)

        '즉  projectdata(데이터넘버)(유닛넘버) 에서 데이터넘버를 바꿔가며 그 값이 0인지 확인.

        Public Function CheckChangeAll(index As Long) As Integer
            For i = 0 To projectdata.Count - 1
                Try
                    If (index - keyINFO(i).VarStart) >= 0 Then
                        If projectdata(i)(index - keyINFO(i).VarStart) <> 0 Then ' 하나라도 0이 아니라면. 즉 하나라도 수정되어있다면.
                            Return 1
                        End If
                    End If
                Catch ex As Exception
                End Try

            Next
            Return 0
        End Function


        Public Sub CheckChange(key As String, index As UInteger, obj As Object)
            obj.ForeColor = ProgramSet.FORECOLOR
            Try
                If projectdata(keyDic(key))(index - keyINFO(keyDic(key)).VarStart) = 0 Then
                    obj.BackColor = ProgramSet.BACKCOLOR
                    'Return False
                Else
                    obj.BackColor = ProgramSet.CHANGECOLOR
                    'Return True
                End If
            Catch ex As Exception
            End Try
        End Sub


        Public Sub ChecklistChange(key As String, index As UInteger, checkedlistBox As ListView)
            checkedlistBox.ForeColor = ProgramSet.FORECOLOR

            Try
                checkedlistBox.BackColor = ProgramSet.BACKCOLOR

                Dim provalue As Long = projectdata(keyDic(key))(index - keyINFO(keyDic(key)).VarStart)
                Dim mapvalue As Long = data(keyDic(key))(index - keyINFO(keyDic(key)).VarStart)



                Dim oldvalue As Long = mapvalue
                Dim newvalue As Long = mapvalue + provalue


                For i = 0 To checkedlistBox.Items.Count - 1
                    'MsgBox(value & vbCrLf & i & " 번째 버튼 " & (value And (2 ^ i)))

                    '만약 value 가 00111
                    '만약 value 가 00110
                    '다음 index 가 00001

                    'and시 0이 아니면 현재 수치가 존재한다.
                    If (oldvalue And (2 ^ i)) <> (newvalue And (2 ^ i)) Then
                        checkedlistBox.Items(i).BackColor = ProgramSet.CHANGECOLOR
                        'Return False
                    Else
                        If checkedlistBox.Items(i).Checked = True Then
                            checkedlistBox.Items(i).BackColor = ProgramSet.LISTCOLOR
                        Else
                            checkedlistBox.Items(i).BackColor = ProgramSet.BACKCOLOR
                        End If
                        'Return True
                    End If
                Next
            Catch ex As Exception
            End Try
        End Sub

        'Public Sub DrawIcon(ByRef pictureBox As PictureBox, index As UInteger)
        '    Dim tempgrp As New GRP
        '    Dim key As String = pictureBox.Tag
        '    Dim mpq As New SFMpq

        '    Dim value As UInteger '방어구 종류.


        '    tempgrp.LoadPalette(PalettType.Icons)
        '    tempgrp.LoadGRP(mpq.ReaddatFile("unit\protoss\dragoon.grp")) 'unit\cmdbtns\cmdicons.grp

        '    Try
        '        value = data(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart) + projectdata(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart)

        '        tempgrp.DrawToPictureBox(pictureBox, index)


        '        pictureBox.Enabled = True
        '        pictureBox.Visible = True
        '    Catch ex As Exception
        '        pictureBox.Image = New Bitmap(32, 32, Imaging.PixelFormat.Format8bppIndexed)
        '        pictureBox.Enabled = False
        '        pictureBox.Visible = False
        '    End Try


        'End Sub

        Public Sub ReadToTEXTBOX(ByRef textbox As TextBox, index As UInteger)
            Dim key As String = textbox.Tag


            CheckChange(key, index, textbox)
            Try
                textbox.Text = ReadValue(key, index) 'data(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart) + projectdata(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart)
                textbox.Enabled = True
                textbox.Visible = True
            Catch ex As Exception
                textbox.Text = "0"
                textbox.Enabled = False
                textbox.Visible = False
            End Try
        End Sub

        Public Sub WriteToTEXTBOX(ByRef textbox As TextBox, index As UInteger)
            Dim key As String = textbox.Tag
            Dim value As Long
            Try
                value = textbox.Text
                textbox.Text = value
                If WriteValue(key, index, value) Then
                    textbox.Text = Math.Pow(256, keyINFO(keyDic(key)).Size) - 1
                End If
                CheckChange(key, index, textbox)
            Catch ex As Exception
            End Try           'projectdata(KEYV)(index - keyINFO(KEYV).VarStart) = textbox.Text - data(KEYV)(index - keyINFO(KEYV).VarStart)
        End Sub

        Public Sub ReadToNUMERIC(ByRef numericupdown As NumericUpDown, index As UInteger)
            Dim key As String = numericupdown.Tag
            numericupdown.Maximum = Math.Pow(256, keyINFO(keyDic(key)).Size) - 1

            CheckChange(key, index, numericupdown)
            Try
                numericupdown.Value = ReadValue(key, index) 'data(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart) + projectdata(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart)
                numericupdown.Enabled = True
                numericupdown.Visible = True
            Catch ex As Exception
                numericupdown.Value = 0
                numericupdown.Enabled = False
                numericupdown.Visible = False
            End Try
        End Sub

        Public Sub WriteToNUMERIC(ByRef numericupdown As NumericUpDown, index As UInteger)
            Dim key As String = numericupdown.Tag
            Dim value As Long

            Try
                value = numericupdown.Value
                numericupdown.Value = value
                If WriteValue(key, index, value) Then
                    numericupdown.Value = Math.Pow(256, keyINFO(keyDic(key)).Size) - 1
                End If
                CheckChange(key, index, numericupdown)
            Catch ex As Exception
            End Try           'projectdata(KEYV)(index - keyINFO(KEYV).VarStart) = textbox.Text - data(KEYV)(index - keyINFO(KEYV).VarStart)
        End Sub

        Public Sub ReadToCOMBOBOX(ByRef combobox As ComboBox, index As UInteger)
            Dim key As String = combobox.Tag

            CheckChange(key, index, combobox)
            Try
                combobox.SelectedIndex = ReadValue(key, index) 'data(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart) + projectdata(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart)
                combobox.Enabled = True
                combobox.Visible = True
            Catch ex As Exception
                combobox.SelectedIndex = -1
                combobox.Enabled = False
                combobox.Visible = False
            End Try
        End Sub

        Public Sub WriteToCOMBOBOX(ByRef combobox As ComboBox, index As UInteger)
            Dim key As String = combobox.Tag
            Dim value As Long
            Try
                value = combobox.SelectedIndex
                If WriteValue(key, index, value) Then
                    combobox.SelectedIndex = Math.Pow(256, keyINFO(keyDic(key)).Size) - 1
                End If
                CheckChange(key, index, combobox)
            Catch ex As Exception
            End Try
            combobox.Enabled = True
            'projectdata(KEYV)(index - keyINFO(KEYV).VarStart) = textbox.Text - data(KEYV)(index - keyINFO(KEYV).VarStart)
        End Sub


        Public Sub ReadToCHECKBOX(ByRef checkbox As CheckBox, index As UInteger)
            Dim key As String = checkbox.Tag

            CheckChange(key, index, checkbox)
            Try
                checkbox.Checked = ReadValue(key, index)
                checkbox.Enabled = True
                checkbox.Visible = True
            Catch ex As Exception
                checkbox.Checked = False
                checkbox.Enabled = False
                checkbox.Visible = False
            End Try
        End Sub

        Public Sub WriteToCHECKBOX(ByRef checkbox As CheckBox, index As UInteger)
            Dim key As String = checkbox.Tag
            If checkbox.Checked Then
                WriteValue(key, index, 1)
            Else
                WriteValue(key, index, 0)
            End If


            CheckChange(checkbox.Tag, index, checkbox)
        End Sub

        Public Sub ReadToCHECKBOXLIST(ByRef checkedlistBox As ListView, index As UInteger)
            Dim key As String = checkedlistBox.Tag

            Try
                For i = 0 To checkedlistBox.Items.Count - 1
                    checkedlistBox.Items(i).Checked = ReadValue(key, index) And (2 ^ i)
                Next
                ' checkedlistBox.Text = ReadValue(key, index) 'data(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart) + projectdata(keyDic.Item(key))(index - keyINFO(keyDic(key)).VarStart)
                checkedlistBox.Enabled = True
                checkedlistBox.Visible = True
            Catch ex As Exception
                checkedlistBox.Enabled = False
                checkedlistBox.Visible = False
            End Try
            ChecklistChange(key, index, checkedlistBox)
        End Sub

        Public Sub WriteToCHECKBOXLIST(ByRef checkedlistBox As ListView, index As UInteger)
            Dim key As String = checkedlistBox.Tag
            Try
                Dim value As UInteger = ReadValue(key, index)
                For i = 0 To checkedlistBox.Items.Count - 1
                    If checkedlistBox.Items(i).Checked = True Then
                        value = value Or 2 ^ i
                    Else
                        value = (value Or (2 ^ i)) - 2 ^ i
                    End If
                Next
                WriteValue(key, index, value)

                ChecklistChange(checkedlistBox.Tag, index, checkedlistBox)
            Catch ex As Exception

            End Try
        End Sub
    End Class
End Module
