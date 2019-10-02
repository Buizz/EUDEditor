Imports System.IO
Imports System.Text
Imports System.Threading

Namespace ProgramSet
    Module ProgramSettingModule
        Public DatEditName As String = "DatEdit in EUDEditor"
        Public FireGraftName As String = "FireGraft in EUDEditor"


        'Public Version As String = "vTEST 0.13"
        Public Version As String = "0.17.9.0"
        Public DatEditVersion As String = "v0.3"
        Public SCDBSerial As UInteger

        Public ErrorFormMessage As String = "EUDEditor Error"
        Public AlterFormMessage As String = "EUDEditor Warning"

        Public StarVersion As String = ""

        Public StarDirec As String = ""
        Public euddraftDirec As String = ""

        Public isAutoCompile As Boolean


        Public DatMPQDirec(3) As String


        'Public ID As String
        'Public Password As String
        'Public AutoLogin As Boolean
        'Public Remember As Boolean



        Public FORECOLOR As Color

        Public BACKCOLOR As Color
        Public CHANGECOLOR As Color

        Public LISTCOLOR As Color
    End Module

End Namespace
Namespace ProjectSet
    Module ProjectSettingModule
        Public saveStatus As Boolean
        Public Sub saveStatusChange()
            saveStatus = False
            Main.nameResetting()
        End Sub

        Public SCDBSerial As UInteger
        Public SCDBUse As Boolean = False

        Public EUDEditorDebug As Boolean
        Public epTraceDebug As Boolean

        Public filename As String


        Public scdbLoingStatus As Boolean
        Public isload As Boolean
        Public loading As Boolean

        Public InputMap As String
        Public OutputMap As String
        Public euddraftuse As Boolean
        Public UsedSetting(8) As Boolean



        Public LoadFromCHK As Boolean
        Public TriggerSetTouse As Boolean
        Public TriggerPlayer As UInteger



        Public PlayerRace As Byte
        Public CHKSTRING As New List(Of String)
        Public CHKSWITCHNAME As New List(Of UInteger)
        Public CHKLOCATIONNAME As New List(Of UInteger)
        Public CHKWAVLIST As New List(Of UInteger)
        Public CHKUNITNAME As New List(Of UInteger)
        Public CHKFORCEDATA As New List(Of List(Of String))
        Public UNITSTR(227) As UInteger



        Public Enum Settingtype
            DatEdit = 0
            FireGraft = 1
            BinEditor = 2
            TileSet = 3
            BtnSet = 4
            GRP = 5
            Struct = 6
            Plugin = 7
            filemanager = 8
        End Enum
        '뭐든지 가능
        '1. DatEdit
        '2. FireGraft

        'eudplib, tep혼용
        '3. BinEditor
        '5. TileSet
        '6. BtnSet

        'eudplib
        '4. GRP
        '7. Struct
        '8. Plugin

        '상관 없음

        Private Function SearchCHK(chkname As String, buffer() As Byte) As UInteger
            Dim _name As String
            Dim _size As UInteger


            Dim mem As MemoryStream = New MemoryStream(buffer)
            Dim binary As BinaryReader = New BinaryReader(mem)
            While (True)
                _name = ""
                _name = _name & ChrW(binary.ReadByte)
                _name = _name & ChrW(binary.ReadByte)
                _name = _name & ChrW(binary.ReadByte)
                _name = _name & ChrW(binary.ReadByte)

                If _name = chkname Then
                    Dim returnval As UInteger = mem.Position
                    binary.Close()
                    mem.Close()
                    Return returnval
                Else

                    _size = binary.ReadUInt32()
                    mem.Position += _size
                End If
            End While
            binary.Close()
            mem.Close()
            Return 0
        End Function







        Public Sub LoadCHKdata()
            LoadTILEDATA()

            CHKSTRING.Clear()
            CHKSWITCHNAME.Clear()
            CHKLOCATIONNAME.Clear()
            CHKWAVLIST.Clear()
            CHKUNITNAME.Clear()
            CHKFORCEDATA.Clear()

            Dim key As String



            For i = 0 To DatEditDATA.Count - 1
                For j = 0 To DatEditDATA(i).mapdata.Count - 1
                    For p = 0 To DatEditDATA(i).mapdata(j).Count - 1
                        DatEditDATA(i).mapdata(j)(p) = 0
                    Next
                Next
            Next





            Dim hmpq As UInteger
            Dim hfile As UInteger
            Dim buffer() As Byte
            Dim filesize As UInteger
            Dim size As Integer

            Dim pdwread As IntPtr

            StormLib.SFileOpenArchive(InputMap, 0, 0, hmpq)


            Dim openFilename As String = "staredit\scenario.chk"

            StormLib.SFileOpenFileEx(hmpq, openFilename, 0, hfile)

            If hfile <> 0 Then
                filesize = StormLib.SFileGetFileSize(hfile, filesize)
                ReDim buffer(filesize)

                StormLib.SFileReadFile(hfile, buffer, filesize, pdwread, 0)

                Dim mem As MemoryStream = New MemoryStream(buffer)
                Dim binary As BinaryReader = New BinaryReader(mem)
                Dim stream As StreamReader = New StreamReader(mem, Text.Encoding.ASCII)

                Try
                    mem.Position = SearchCHK("TYPE", buffer)
                Catch ex As Exception
                    LoadFromCHK = False
                    Exit Sub
                End Try

                size = binary.ReadUInt32

                Dim value As UInteger = binary.ReadUInt32
                If (value <> 1113014610) Then
                    LoadFromCHK = False
                    Exit Sub
                End If




                mem.Position = SearchCHK("SIDE", buffer)

                    size = binary.ReadUInt32
                    Try
                        PlayerRace = 2 - binary.ReadByte()
                        For i = 0 To 6
                            If (2 - binary.ReadByte()) <> PlayerRace Then

                                Throw New ArgumentException("Exception Occured")
                            End If
                        Next
                    Catch ex As Exception
                        PlayerRace = 255
                    End Try

                    If LoadFromCHK = False Then
                        stream.Close()
                        binary.Close()
                        mem.Close()

                        StormLib.SFileCloseFile(hfile)


                        StormLib.SFileCloseArchive(hmpq)
                        Exit Sub
                    End If


                    mem.Position = SearchCHK("WAV ", buffer)

                    size = binary.ReadUInt32
                    For i = 0 To size / 4 - 1
                        'binary.ReadUInt32()
                        '    binary.ReadUInt32()
                        '    binary.ReadUInt32()
                        '    binary.ReadUInt32()

                        CHKWAVLIST.Add(binary.ReadUInt32())
                        '    binary.ReadUInt16()
                    Next

                    Dim _playerFlag(8) As Boolean
                    mem.Position = SearchCHK("OWNR", buffer)

                    size = binary.ReadUInt32
                    '03 = 구조가능
                    '05 = 컴퓨터
                    '06 = 사람 
                    '07 = 중립
                    For i = 0 To 7
                        Dim flag As Byte = binary.ReadByte()
                        If flag = 3 Or flag = 5 Or flag = 6 Or flag = 7 Then
                            _playerFlag(i) = True
                        Else
                            _playerFlag(i) = False
                        End If
                    Next



                    mem.Position = SearchCHK("FORC", buffer)
                    size = binary.ReadUInt32


                    CHKFORCEDATA.Add(New List(Of String))
                    CHKFORCEDATA.Add(New List(Of String))
                    CHKFORCEDATA.Add(New List(Of String))
                    CHKFORCEDATA.Add(New List(Of String))

                    For i = 0 To 3
                        CHKFORCEDATA(i).Add("")
                    Next
                    '플레이어소속 존재하는 플레이어인지 판단!
                    For i = 0 To 7
                        Dim forcenum As Byte = binary.ReadByte()
                        If _playerFlag(i) = True Then
                            CHKFORCEDATA(forcenum).Add(i)
                        End If
                    Next
                    '포스 문자열
                    For i = 0 To 3
                        CHKFORCEDATA(i)(0) = binary.ReadUInt16()
                    Next


                    mem.Position = SearchCHK("MRGN", buffer)

                    size = binary.ReadUInt32

                    For i = 0 To 255
                        binary.ReadUInt32()
                        binary.ReadUInt32()
                        binary.ReadUInt32()
                        binary.ReadUInt32()

                        CHKLOCATIONNAME.Add(binary.ReadUInt16())
                        binary.ReadUInt16()
                    Next

                    mem.Position = SearchCHK("SWNM", buffer)

                    size = binary.ReadUInt32
                    For i = 0 To 255
                        CHKSWITCHNAME.Add(binary.ReadUInt32)
                    Next


                    mem.Position = SearchCHK("UPGx", buffer)

                    size = binary.ReadUInt32

                    Dim TEMPIsUPGDefault() As Byte
                    '## (61개) = 각 업그레이드의 허용 상태
                    '   - 00 = 변화값을 따름
                    '   - 01 = 기본값을 따름
                    TEMPIsUPGDefault = binary.ReadBytes(62)


                    Dim TEMPUPGMin(60) As UInteger
                    '#### (61개) = 첫 업그레이드 미네랄 비용
                    For i = 0 To 60
                        TEMPUPGMin(i) = binary.ReadUInt16()
                    Next

                    Dim TEMPUPGADDMin(60) As UInteger
                    '#### (61개) = 추가 업그레이드 미네랄 비용
                    For i = 0 To 60
                        TEMPUPGADDMin(i) = binary.ReadUInt16()
                    Next

                    Dim TEMPUPGGas(60) As UInteger
                    '#### (61개) = 첫 업그레이드 가스 비용
                    For i = 0 To 60
                        TEMPUPGGas(i) = binary.ReadUInt16()
                    Next

                    Dim TEMPUPGADDGas(60) As UInteger
                    '#### (61개) = 추가 업그레이드 가스 비용
                    For i = 0 To 60
                        TEMPUPGADDGas(i) = binary.ReadUInt16()
                    Next

                    Dim TEMPUPGTime(60) As UInteger
                    '#### (61개) = 첫 업그레이드 시간
                    For i = 0 To 60
                        TEMPUPGTime(i) = binary.ReadUInt16()
                    Next

                    Dim TEMPUPGADDTime(60) As UInteger
                    '#### (61개) = 추가 업그레이드 시간
                    For i = 0 To 60
                        TEMPUPGADDTime(i) = binary.ReadUInt16()
                    Next



                    For i = 0 To 60
                        If TEMPIsUPGDefault(i) = 0 Then
                            key = "Mineral Cost Base"
                            DatEditDATA(DTYPE.upgrades).mapdata(DatEditDATA(DTYPE.upgrades).keyDic(key))(i) = CLng(TEMPUPGMin(i)) - DatEditDATA(DTYPE.upgrades).data(DatEditDATA(DTYPE.upgrades).keyDic(key))(i)

                            key = "Mineral Cost Factor"
                            DatEditDATA(DTYPE.upgrades).mapdata(DatEditDATA(DTYPE.upgrades).keyDic(key))(i) = CLng(TEMPUPGADDMin(i)) - DatEditDATA(DTYPE.upgrades).data(DatEditDATA(DTYPE.upgrades).keyDic(key))(i)

                            key = "Vespene Cost Base"
                            DatEditDATA(DTYPE.upgrades).mapdata(DatEditDATA(DTYPE.upgrades).keyDic(key))(i) = CLng(TEMPUPGGas(i)) - DatEditDATA(DTYPE.upgrades).data(DatEditDATA(DTYPE.upgrades).keyDic(key))(i)

                            key = "Vespene Cost Factor"
                            DatEditDATA(DTYPE.upgrades).mapdata(DatEditDATA(DTYPE.upgrades).keyDic(key))(i) = CLng(TEMPUPGADDGas(i)) - DatEditDATA(DTYPE.upgrades).data(DatEditDATA(DTYPE.upgrades).keyDic(key))(i)

                            key = "Research Time Base"
                            DatEditDATA(DTYPE.upgrades).mapdata(DatEditDATA(DTYPE.upgrades).keyDic(key))(i) = CLng(TEMPUPGTime(i)) - DatEditDATA(DTYPE.upgrades).data(DatEditDATA(DTYPE.upgrades).keyDic(key))(i)

                            key = "Research Time Factor"
                            DatEditDATA(DTYPE.upgrades).mapdata(DatEditDATA(DTYPE.upgrades).keyDic(key))(i) = CLng(TEMPUPGADDTime(i)) - DatEditDATA(DTYPE.upgrades).data(DatEditDATA(DTYPE.upgrades).keyDic(key))(i)

                        End If
                    Next




                    mem.Position = SearchCHK("TECx", buffer)
                    size = binary.ReadUInt32



                    Dim TEMPIsTECHDefault() As Byte
                    '0# = 기술의 허용 상태 (00 사용불가, 01 사용가능)
                    TEMPIsTECHDefault = binary.ReadBytes(44)


                    Dim TEMPTECHMin(43) As UInteger
                    '#### = 미네랄 비용
                    For i = 0 To 43
                        TEMPTECHMin(i) = binary.ReadUInt16()
                    Next

                    Dim TEMPTECHGas(43) As UInteger
                    '#### = 가스 비용
                    For i = 0 To 43
                        TEMPTECHGas(i) = binary.ReadUInt16()
                    Next

                    Dim TEMPTECHTime(43) As UInteger
                    '#### = 걸리는 시간
                    For i = 0 To 43
                        TEMPTECHTime(i) = binary.ReadUInt16()
                    Next

                    Dim TEMPTECHADDEnerge(43) As UInteger
                    '##00 = 필요 마나
                    For i = 0 To 43
                        TEMPTECHADDEnerge(i) = binary.ReadUInt16()
                    Next

                    For i = 0 To 43
                        If TEMPIsTECHDefault(i) = 0 Then
                            key = "Mineral Cost"
                            DatEditDATA(DTYPE.techdata).mapdata(DatEditDATA(DTYPE.techdata).keyDic(key))(i) = CLng(TEMPTECHMin(i)) - DatEditDATA(DTYPE.techdata).data(DatEditDATA(DTYPE.techdata).keyDic(key))(i)

                            key = "Vespene Cost"
                            DatEditDATA(DTYPE.techdata).mapdata(DatEditDATA(DTYPE.techdata).keyDic(key))(i) = CLng(TEMPTECHGas(i)) - DatEditDATA(DTYPE.techdata).data(DatEditDATA(DTYPE.techdata).keyDic(key))(i)

                            key = "Resarch Time"
                            DatEditDATA(DTYPE.techdata).mapdata(DatEditDATA(DTYPE.techdata).keyDic(key))(i) = CLng(TEMPTECHTime(i)) - DatEditDATA(DTYPE.techdata).data(DatEditDATA(DTYPE.techdata).keyDic(key))(i)

                            key = "Energy Required"
                            DatEditDATA(DTYPE.techdata).mapdata(DatEditDATA(DTYPE.techdata).keyDic(key))(i) = CLng(TEMPTECHADDEnerge(i)) - DatEditDATA(DTYPE.techdata).data(DatEditDATA(DTYPE.techdata).keyDic(key))(i)
                        End If
                    Next






                    'Dim UNIxpara() As Byte
                    mem.Position = SearchCHK("UNIx", buffer)

                    size = binary.ReadUInt32
                    'UNIxpara = binary.ReadBytes(size)

                    Dim TEMPIsUnitDefault() As Byte
                    '## (228개) = 유닛의 순서에 맞게 배열
                    '- 00 (변화값을 따름)
                    '- 01 (기본값을 따름)
                    TEMPIsUnitDefault = binary.ReadBytes(228)

                    Dim TEMPUnitHP(227) As UInteger
                    '00## ##00 (228개) = ????부분은 유닛의 체력
                    For i = 0 To 227
                        TEMPUnitHP(i) = binary.ReadUInt32()
                    Next

                    Dim TEMPUnitSh(227) As UShort
                    '#### (228개) = 쉴드
                    For i = 0 To 227
                        TEMPUnitSh(i) = binary.ReadUInt16()
                    Next

                    Dim TEMPUnitAp() As Byte
                    '## (228개) = 방어력
                    TEMPUnitAp = binary.ReadBytes(228)

                    Dim TEMPUnitBt(227) As UShort
                    '#### (228개) = 생산시간
                    For i = 0 To 227
                        TEMPUnitBt(i) = binary.ReadUInt16()
                    Next

                    Dim TEMPMinCost(227) As UShort
                    '#### (228개) = 미네랄 비용
                    For i = 0 To 227
                        TEMPMinCost(i) = binary.ReadUInt16()
                    Next

                    Dim TEMPGasCost(227) As UShort
                    '#### (228개) = 가스 비용
                    For i = 0 To 227
                        TEMPGasCost(i) = binary.ReadUInt16()
                    Next

                    '#### (228개) = 문자열 번호
                    For i = 0 To 227
                        UNITSTR(i) = binary.ReadUInt16()
                        CHKUNITNAME.Add(UNITSTR(i))
                    Next

                    Dim TEMPWeaDmg(129) As UShort
                    '#### (130개) = 각 무기의 기본 공격력
                    For i = 0 To 129
                        TEMPWeaDmg(i) = binary.ReadUInt16()
                    Next

                    Dim TEMPWeaUmg(129) As UShort
                    '#### (130개) = 업그레이드시 올라가는 공격력
                    For i = 0 To 129
                        TEMPWeaUmg(i) = binary.ReadUInt16()
                    Next


                    For i = 0 To 227
                        If TEMPIsUnitDefault(i) = 0 Then
                            key = "Hit Points"
                            DatEditDATA(DTYPE.units).mapdata(DatEditDATA(DTYPE.units).keyDic(key))(i) = CLng(TEMPUnitHP(i)) - DatEditDATA(DTYPE.units).data(DatEditDATA(DTYPE.units).keyDic(key))(i)

                            key = "Shield Amount"
                            DatEditDATA(DTYPE.units).mapdata(DatEditDATA(DTYPE.units).keyDic(key))(i) = CLng(TEMPUnitSh(i)) - DatEditDATA(DTYPE.units).data(DatEditDATA(DTYPE.units).keyDic(key))(i)

                            key = "Armor"
                            DatEditDATA(DTYPE.units).mapdata(DatEditDATA(DTYPE.units).keyDic(key))(i) = CLng(TEMPUnitAp(i)) - DatEditDATA(DTYPE.units).data(DatEditDATA(DTYPE.units).keyDic(key))(i)

                            key = "Build Time"
                            DatEditDATA(DTYPE.units).mapdata(DatEditDATA(DTYPE.units).keyDic(key))(i) = CLng(TEMPUnitBt(i)) - DatEditDATA(DTYPE.units).data(DatEditDATA(DTYPE.units).keyDic(key))(i)

                            key = "Mineral Cost"
                            DatEditDATA(DTYPE.units).mapdata(DatEditDATA(DTYPE.units).keyDic(key))(i) = CLng(TEMPMinCost(i)) - DatEditDATA(DTYPE.units).data(DatEditDATA(DTYPE.units).keyDic(key))(i)

                            key = "Vespene Cost"
                            DatEditDATA(DTYPE.units).mapdata(DatEditDATA(DTYPE.units).keyDic(key))(i) = CLng(TEMPGasCost(i)) - DatEditDATA(DTYPE.units).data(DatEditDATA(DTYPE.units).keyDic(key))(i)

                            key = "Unit Map String"
                            DatEditDATA(DTYPE.units).mapdata(DatEditDATA(DTYPE.units).keyDic(key))(i) = CLng(UNITSTR(i)) - DatEditDATA(DTYPE.units).data(DatEditDATA(DTYPE.units).keyDic(key))(i)


                            key = "Ground Weapon"
                            Dim j As Integer = DatEditDATA(DTYPE.units).data(DatEditDATA(DTYPE.units).keyDic(key))(i) '유닛 i의 지상무기와 공중무기 번호를 준다.

                            If j <> 130 Then
                                key = "Damage Amount"
                                DatEditDATA(DTYPE.weapons).mapdata(DatEditDATA(DTYPE.weapons).keyDic(key))(j) = CLng(TEMPWeaDmg(j)) - DatEditDATA(DTYPE.weapons).data(DatEditDATA(DTYPE.weapons).keyDic(key))(j)
                                key = "Damage Bonus"
                                DatEditDATA(DTYPE.weapons).mapdata(DatEditDATA(DTYPE.weapons).keyDic(key))(j) = CLng(TEMPWeaUmg(j)) - DatEditDATA(DTYPE.weapons).data(DatEditDATA(DTYPE.weapons).keyDic(key))(j)
                            End If


                            key = "Air Weapon"
                            j = DatEditDATA(DTYPE.units).data(DatEditDATA(DTYPE.units).keyDic(key))(i) '유닛 i의 지상무기와 공중무기 번호를 준다.

                            If j <> 130 Then
                                key = "Damage Amount"
                                DatEditDATA(DTYPE.weapons).mapdata(DatEditDATA(DTYPE.weapons).keyDic(key))(j) = CLng(TEMPWeaDmg(j)) - DatEditDATA(DTYPE.weapons).data(DatEditDATA(DTYPE.weapons).keyDic(key))(j)
                                key = "Damage Bonus"
                                DatEditDATA(DTYPE.weapons).mapdata(DatEditDATA(DTYPE.weapons).keyDic(key))(j) = CLng(TEMPWeaUmg(j)) - DatEditDATA(DTYPE.weapons).data(DatEditDATA(DTYPE.weapons).keyDic(key))(j)
                            End If
                        Else
                            UNITSTR(i) = 0
                        End If
                    Next
                    'key = "Ground Weapon"
                    'key = "Air Weapon"
                    'DatEditDAT(DTYPE.units).mapdata(DatEditDAT(DTYPE.units).keyDic(key))(i)



                    Dim STRpara() As Byte
                    mem.Position = SearchCHK("STR ", buffer)

                    size = binary.ReadUInt32 '문자열 수
                    STRpara = binary.ReadBytes(size)
                    stream.Close()
                    binary.Close()
                    mem.Close()



                    Dim strmem As MemoryStream = New MemoryStream(STRpara)
                    Dim strstream As StreamReader = New StreamReader(strmem, Text.Encoding.GetEncoding("ks_c_5601-1987"))
                    Dim strbinary As BinaryReader = New BinaryReader(strmem, Text.Encoding.GetEncoding("ks_c_5601-1987"))

                    Dim tempindex As UInteger
                    Dim tempindex2 As Char
                    Dim tempstring As String = ""
                    Dim strcount As Integer = 0

                    size = strbinary.ReadUInt16
                    For i = 0 To size - 1 '문자열 갯수
                        strmem.Position = 2 + i * 2

                        tempindex = strbinary.ReadUInt16()

                        strmem.Position = tempindex

                        strcount = 0
                        tempindex2 = strbinary.ReadChar
                        While (AscW(tempindex2) <> &H0)
                            tempindex2 = strbinary.ReadChar
                            strcount += 1
                        End While
                        strmem.Position = tempindex



                        tempstring = strbinary.ReadChars(strcount)
                        tempstring = tempstring.Replace(ChrW(0), "")

                        If tempstring <> "" Then
                            CHKSTRING.Add(tempstring)
                        Else
                            CHKSTRING.Add("Null")
                        End If
                    Next



                    strbinary.Close()
                    strstream.Close()
                    strmem.Close()
                    StormLib.SFileCloseFile(hfile)
                Else
                    Exit Sub
            End If

            StormLib.SFileCloseArchive(hmpq)
        End Sub


        Public Sub Reset()
            filename = ""

            InputMap = ""
            OutputMap = ""
            euddraftuse = True
            scdbLoingStatus = False
            For i = 0 To 227
                UNITSTR(i) = 0
            Next

            CHKSTRING.Clear()
            For i = 0 To 8
                UsedSetting(i) = True
            Next
            TriggerPlayer = 7
            TriggerSetTouse = True
            EUDEditorDebug = False
            epTraceDebug = False

            For i = 0 To 7
                DatEditDATA(i).Reset()
            Next
            DatEditForm.MainTAB.SelectedIndex = 0
            DatEditForm.TAB_INDEX = 0

            Soundlist.Clear()
            Soundinterval = 3

            GRPEditorDATA.Clear()
            For i = 0 To 998
                GRPEditorUsingDATA(i) = ""
            Next
            For i = 0 To 4999
                GRPEditorUsingindexDATA(i) = ""
            Next


            soundstopper = False
            scmloader = False
            noAirCollision = False
            unlimiter = False
            keepSTR = False
            eudTurbo = False
            LoadFromCHK = True

            iscriptPatcher = ""
            iscriptPatcheruse = False

            nqcuse = False
            nqcunit = ""
            nqclocs = ""
            nqccommands = ""

            unpatcheruse = False
            unpatcher = ""

            grpinjectoruse = False
            grpinjector_arrow = ""
            grpinjector_drag = ""
            grpinjector_illegal = ""


            dataDumperuse = False
            dataDumper_user = ""
            dataDumper_grpwire = ""
            dataDumper_tranwire = ""
            dataDumper_wirefram = ""
            dataDumper_cmdicons = ""
            dataDumper_stat_txt = ""
            dataDumper_AIscript = ""
            dataDumper_iscript = ""

            dataDumper_grpwire_f = 0
            dataDumper_tranwire_f = 0
            dataDumper_wirefram_f = 0
            dataDumper_cmdicons_f = 0
            dataDumper_stat_txt_f = 0
            dataDumper_AIscript_f = 0
            dataDumper_iscript_f = 0

            extraedssetting = ""

            stattextdic.Clear()
            statlang = 0

            SCDBDeath.Clear()
            SCDBVariable.Clear()
            SCDBLoc.Clear()
            SCDBLocLoad.Clear()
            SCDBMaker = "Noname"
            SCDBMapName = "Noname"
            SCDBUse = False
            SCDBDataSize = 4
            SCDBSerial = 0

            For i = 0 To 227
                grpwireData(i) = i
                tranwireData(i) = i
                wireframData(i) = i

                ProjectUnitStatusFn1(i) = 0
                ProjectUnitStatusFn2(i) = 0
                ProjectDebugID(i) = 0
            Next

            For i = 0 To ProjectBtnUSE.Count - 1
                ProjectBtnUSE(i) = False
                ProjectBtnData(i).Clear()
            Next


            For i = 0 To ProjectRequireDataUSE.Count - 1
                For j = 0 To ProjectRequireDataUSE(i).Count - 1
                    ProjectRequireDataUSE(i)(j) = False
                Next
            Next


            For i = 0 To 25
                DefaultBinBitmap(i).SetEnable(0)
                PjcutData.Clear()

                PjcutData.Add(New CsceneData)
            Next


            ProjectTileSetData.Clear()

            If TilebitDATA IsNot Nothing Then
                ReDim ProjectTIleMSet(TilebitDATA.Count)
                ReDim ProjectMTXMDATA(MapSize.Width * MapSize.Height - 1)
            End If
            ProjectTileUseFile = False
            ProjectTileSetFileName = ""

            NewTriggerFile()


            '폼 리셋
            DatEditForm.Close()



            isload = False

            saveStatus = True
        End Sub

        Public Function Close() As Boolean
            Dim ise2s As Boolean = False
            Try
                If Mid(ProjectSet.filename, ProjectSet.filename.Length - 3) <> ".e2s" Then
                    ise2s = True
                End If
            Catch ex As Exception

            End Try
            Dim Dialog As DialogResult
            If saveStatus = False And ProjectSet.isload = True Then
                Dialog = MsgBox(Lan.GetText("MsgBox", "SaveMsg").Replace("$S$", ProjectSet.filename.Split("\").Last), MsgBoxStyle.YesNoCancel, "EUD Editor")


                If Dialog = DialogResult.Cancel Then
                    Return False '닫는 걸 취소함.
                ElseIf Dialog = DialogResult.Yes Then
                    If ProjectSet.filename = "" Or ise2s Then
                        Dim Dialog2 As DialogResult
                        Dialog2 = Main.SaveFileDialog1.ShowDialog()

                        If Dialog2 = DialogResult.Cancel Then
                            Return False
                        Else
                            ProjectSet.Save(Main.SaveFileDialog1.FileName)
                        End If
                    Else
                        ProjectSet.Save(ProjectSet.filename)
                    End If
                End If
            End If
            ProjectSet.Reset()

            RemasterTile = Nothing
            Main.buttonResetting()
            Return True
        End Function

        Private Sub loadingoformshow(MapName As String)
            ProjectLoadingForm.Label1.Text = MapName
            ProjectLoadingForm.ShowDialog()
        End Sub
        Public Sub Load(MapName As String)
            For i = 0 To 7
                UsedSetting(i) = False
            Next
            Dim fileinfo As New FileInfo(MapName)
            Main.LastData = fileinfo.LastWriteTime
            Dim extension As String = Mid(MapName, MapName.Length - 3)
            Select Case extension
                Case ".e2s", ".e2p"
                    Dim iszipfile As Boolean = False

                    '파일이 존재할 경우 파일의 확장자를 확인해라
                    If extension = ".e2p" Then
                        iszipfile = True
                    End If

                    Dim file As FileStream = Nothing

                    file = New FileStream(MapName, FileMode.Open, FileAccess.Read)

                    Dim stream As StreamReader = New StreamReader(file)

                    Dim savefileVersion As String = ""
                    Dim text As String = stream.ReadToEnd()
                    stream.Close()
                    file.Close()

                    Try
                        Dim Section_ProjectSET As String = FindSection(text, "ProjectSET")

                        savefileVersion = FindSetting(Section_ProjectSET, "Version")
                        If savefileVersion = "0" Then
                            MsgBox(Lan.GetText("MsgBox", "Invalide2s"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                            Exit Sub
                        End If
                        If ProgramSet.Version.Contains("TEST") Then
                            If savefileVersion <> ProgramSet.Version Then
                                MsgBox(Lan.GetText("MsgBox", "TESTVMsg"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                                Exit Sub
                            End If
                        End If
                        If savefileVersion.Contains("TEST") And ProgramSet.Version.Contains("TEST") = False Then
                            MsgBox(Lan.GetText("MsgBox", "TESTVMsg"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                            Exit Sub
                        End If



                        InputMap = FindSetting(Section_ProjectSET, "InputMap")
                        OutputMap = FindSetting(Section_ProjectSET, "OutputMap")
                        euddraftuse = FindSetting(Section_ProjectSET, "euddraftuse")
                        LoadFromCHK = FindSetting(Section_ProjectSET, "loadfromCHK")
                        For i = 0 To UsedSetting.Count - 1
                            UsedSetting(i) = FindSetting(Section_ProjectSET, "UsedSetting" & i)
                        Next

                        EUDEditorDebug = FindSetting(Section_ProjectSET, "EUDEditorDebug")
                        epTraceDebug = FindSetting(Section_ProjectSET, "epTraceDebug")
                        TriggerSetTouse = FindSetting(Section_ProjectSET, "triggerSetTouse")
                        TriggerPlayer = FindSetting(Section_ProjectSET, "triggerPlayer")
                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "Setting").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try


                    Try
                        Dim Section_DatEditSET As String = FindSection(text, "DatEditSET")

                        Dim i, j, k As Integer

                        Dim Setdata() As String = Section_DatEditSET.Split(vbCrLf)
                        For p = 0 To Setdata.Count - 1
                            If Setdata(p).Trim <> "" Then
                                Dim temp() As String = Setdata(p).Trim.Split(",")
                                i = temp(0)
                                j = temp(1)
                                k = temp(2)
                                DatEditDATA(i).projectdata(j)(k) = temp(3)
                            End If
                        Next
                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "DatEdit").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try


                    Try
                        Dim Section_FireGraftSET As String = FindSection(text, "FireGraftSET")


                        For i = 0 To 227
                            If FindSetting(Section_FireGraftSET, "FireGraft" & i) <> "0" Then
                                ProjectUnitStatusFn1(i) = FindSetting(Section_FireGraftSET, "FireGraft" & i).Split(",")(0)
                                ProjectUnitStatusFn2(i) = FindSetting(Section_FireGraftSET, "FireGraft" & i).Split(",")(1)
                                ProjectDebugID(i) = FindSetting(Section_FireGraftSET, "FireGraft" & i).Split(",")(2)
                            End If
                        Next


                        Dim Section_BtnSET As String = FindSection(text, "BtnSET")


                        For i = 0 To 249
                            If FindSetting(Section_BtnSET, "BtnUse" & i) = True Then
                                ProjectBtnUSE(i) = FindSetting(Section_BtnSET, "BtnUse" & i)


                                Dim Setdata() As String = FindSetting(Section_BtnSET, "BtnData" & i).Split(",")
                                For p = 0 To ((Setdata.Count - 1) \ 8) - 1
                                    ProjectBtnData(i).Add(New SBtnDATA)
                                    ProjectBtnData(i)(p).pos = Setdata(0 + p * 8)
                                    ProjectBtnData(i)(p).icon = Setdata(1 + p * 8)
                                    ProjectBtnData(i)(p).con = Setdata(2 + p * 8)
                                    ProjectBtnData(i)(p).act = Setdata(3 + p * 8)
                                    ProjectBtnData(i)(p).conval = Setdata(4 + p * 8)
                                    ProjectBtnData(i)(p).actval = Setdata(5 + p * 8)
                                    ProjectBtnData(i)(p).enaStr = Setdata(6 + p * 8)
                                    ProjectBtnData(i)(p).disStr = Setdata(7 + p * 8)
                                Next


                                'tstr = "BtnData" & i & " : "
                                'For k = 0 To ProjectBtnData(i).Count - 1
                                '    tstr = tstr & ProjectBtnData(i)(k).pos & ","
                                '    tstr = tstr & ProjectBtnData(i)(k).icon & ","
                                '    tstr = tstr & ProjectBtnData(i)(k).con & ","
                                '    tstr = tstr & ProjectBtnData(i)(k).act & ","
                                '    tstr = tstr & ProjectBtnData(i)(k).conval & ","
                                '    tstr = tstr & ProjectBtnData(i)(k).actval & ","
                                '    tstr = tstr & ProjectBtnData(i)(k).enaStr & ","
                                '    tstr = tstr & ProjectBtnData(i)(k).disStr
                                'Next
                                '  ProjectBtnData(i)(0).act
                            End If
                        Next

                        Dim Section_ReqSET As String = FindSection(text, "ReqSET")
                        For i = 0 To ProjectRequireDataUSE.Count - 1
                            For j = 0 To ProjectRequireDataUSE(i).Count - 1
                                ProjectRequireDataUSE(i)(j) = FindSetting(Section_ReqSET, "ReqUse" & i & "," & j)
                                If FindSetting(Section_ReqSET, "ReqUse" & i & "," & j) = 3 Then '존재한다면



                                    ProjectRequireData(i)(j).pos = FindSetting(Section_ReqSET, "ReqPos" & i & "," & j)

                                    ProjectRequireData(i)(j).Code.Clear()

                                    For p As Integer = 0 To FindSetting(Section_ReqSET, "ReqCount" & i & "," & j) - 1
                                        ProjectRequireData(i)(j).Code.Add(FindSetting(Section_ReqSET, "ReqData" & i & "," & j & "," & p))
                                    Next
                                End If



                                'If ProjectRequireDataUSE(i)(j) = False Then
                                '    stream.Write("ReqUse" & i & "," & j & " : " & ProjectRequireDataUSE(i)(j) & vbCrLf)
                                '    stream.Write("ReqPos" & i & "," & j & " : " & ProjectRequireData(i)(j).pos & vbCrLf)
                                '    stream.Write("ReqCount" & i & "," & j & " : " & ProjectRequireData(i)(j).Code.Count & vbCrLf)

                                '    For p = 0 To ProjectRequireData(i)(j).Code.Count - 1
                                '        stream.Write("ReqCount" & i & "," & j & "," & p & " : " & ProjectRequireData(i)(j).Code(p) & vbCrLf)

                                '    Next

                                'End If
                            Next

                        Next
                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "FireGraft").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try



                    Try
                        Dim Section_BinEdittSET As String = FindSection(text, "BinEditSET")
                        Dim projectsceneDataCount As UInteger = FindSetting(Section_BinEdittSET, "projectsceneDataCount")



                        For j = 0 To projectsceneDataCount - 1
                            If j <> 0 Then
                                PjcutData.Add(New CsceneData)

                            End If
                            'projectsceneData(j).scenename
                            Dim projectsceneDatabinDataCount As UInteger = FindSetting(Section_BinEdittSET, "projectsceneData " & j & "binDataCount")

                            For i = 0 To projectsceneDatabinDataCount - 1
                                Dim isDefacult As Boolean = FindSetting(Section_BinEdittSET, ".(" & j & ")(" & i & ")isDefault")

                                If isDefacult = True Then

                                    PjcutData(j).binData(i).imagename = FindSetting(Section_BinEdittSET, "." & "(" & j & ")(" & i & ")imagename")

                                    PjcutData(j).binData(i).pos.X = FindSetting(Section_BinEdittSET, ".(" & j & ")(" & i & ")posx")
                                    PjcutData(j).binData(i).pos.Y = FindSetting(Section_BinEdittSET, ".(" & j & ")(" & i & ")posy")
                                    PjcutData(j).binData(i).size.Width = FindSetting(Section_BinEdittSET, ".(" & j & ")(" & i & ")sizew")
                                    PjcutData(j).binData(i).size.Height = FindSetting(Section_BinEdittSET, ".(" & j & ")(" & i & ")sizeh")


                                    Dim projectsceneDatabinDataObjectdiaCount As UInteger = FindSetting(Section_BinEdittSET, ".(" & j & ")(" & i & ")sizeh")
                                    For k = 0 To projectsceneDatabinDataObjectdiaCount - 1

                                        Dim isDefacultD As Boolean = FindSetting(Section_BinEdittSET, ".(" & j & ")(" & i & ")(" & k & ")isDefault")
                                        If isDefacultD Then
                                            PjcutData(j).binData(i).ObjDlg(k).pos.X = FindSetting(Section_BinEdittSET, ".(" & j & ")(" & i & ")(" & k & ")posx")
                                            PjcutData(j).binData(i).ObjDlg(k).pos.Y = FindSetting(Section_BinEdittSET, ".(" & j & ")(" & i & ")(" & k & ")posy")
                                        End If

                                    Next
                                End If


                            Next
                        Next
                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "BinEdit").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try









                    Try
                        Dim Section_TileSET As String = FindSection(text, "TileSET")

                        ProjectTileUseFile = FindSetting(Section_TileSET, "ProjectTileUseFile")

                        ProjectTileSetFileName = FindSetting(Section_TileSET, "ProjectTileSetFileName")

                        '데이터들
                        Dim ProjectTIleMSetArray() As String = FindSetting(Section_TileSET, "ProjectTIleMSetArray").Split(",")

                        Dim ProjectTIleMSetCount As UInteger = FindSetting(Section_TileSET, "ProjectTIleMSetCount")

                        ReDim ProjectTIleMSet(ProjectTIleMSetCount - 1)
                        For i = 0 To ProjectTIleMSetArray.Count - 2
                            Dim tnum As UInteger = ProjectTIleMSetArray(i)
                            ProjectTIleMSet(tnum) = FindSetting(Section_TileSET, "ProjectTIleMSet" & tnum)
                        Next

                        Dim ProjectTileSetDataCount As UInteger = FindSetting(Section_TileSET, "ProjectTileSetDataCount")

                        For i = 0 To ProjectTileSetDataCount - 1
                            ProjectTileSetData.Add(New CTileSet(FindSetting(Section_TileSET, "ProjectTileSetDataTileSetData" & i).Split(","), FindSetting(Section_TileSET, "ProjectTileSetDataTileSetNum" & i), FindSetting(Section_TileSET, "ProjectTileSetDataisMaker" & i)))
                        Next

                        '데이터들
                        Dim ProjectMTXMDATAArray() As String = FindSetting(Section_TileSET, "ProjectMTXMDATAArray").Split(",")


                        Dim ProjectMTXMDATACount As UInteger = FindSetting(Section_TileSET, "ProjectMTXMDATACount")


                        ReDim ProjectMTXMDATA(ProjectMTXMDATACount - 1)
                        For i = 0 To ProjectMTXMDATAArray.Count - 2
                            Dim tnum As UInteger = ProjectMTXMDATAArray(i)
                            ProjectMTXMDATA(tnum) = FindSetting(Section_TileSET, "ProjectMTXMDATA" & tnum)
                        Next


                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "TileSet").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try


                    Try
                        Dim Section_SoundPlayerSET As String = FindSection(text, "SoundPlayerSET")
                        Soundinterval = FindSetting(Section_SoundPlayerSET, "Soundinterval")


                        Soundlist.AddRange(Section_SoundPlayerSET.Split({vbCrLf}, StringSplitOptions.RemoveEmptyEntries))
                        If Soundlist.Count <> 0 Then
                            Soundlist.RemoveAt(0)
                        End If
                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "BGMPlayer").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try



                    Try
                        Dim Section_GRPEditorSET As String = FindSection(text, "GRPEditorSET")

                        Dim GRPEditorUsingDATCount As Integer = FindSetting(Section_GRPEditorSET, "GRPEditorUsingDATCount")
                        Dim GRPEditorUsingindexDATCount As Integer = FindSetting(Section_GRPEditorSET, "GRPEditorUsingindexDATCount")



                        For i = 0 To FindSetting(Section_GRPEditorSET, "GRPEditorDATCount") - 1
                            Dim grpdata As New GRPDATA

                            grpdata.IsExternal = FindSetting(Section_GRPEditorSET, "GRPEditorDATIsExternal" & i)
                            grpdata.Filename = FindSetting(Section_GRPEditorSET, "GRPEditorDATFilename" & i)
                            grpdata.SafeFilename = FindSetting(Section_GRPEditorSET, "GRPEditorDATSafeFilename" & i)
                            grpdata.Remapping = FindSetting(Section_GRPEditorSET, "GRPEditorDATRemapping" & i)
                            grpdata.Palett = FindSetting(Section_GRPEditorSET, "GRPEditorDATPalett" & i)


                            grpdata.usingimage = New List(Of Integer)
                            For j = 0 To FindSetting(Section_GRPEditorSET, "GRPEditorDATusingimageCount" & i) - 1
                                grpdata.usingimage.Add(0)
                                grpdata.usingimage(j) = FindSetting(Section_GRPEditorSET, "GRPEditorDATusingimage" & i & "," & j)
                            Next

                            GRPEditorDATA.Add(grpdata)
                        Next


                        For i = 0 To GRPEditorUsingDATCount - 1
                            Dim index As Integer = FindSetting(Section_GRPEditorSET, "GRPEditorUsingDAT" & i).Split(",")(0)
                            Dim name As String = FindSetting(Section_GRPEditorSET, "GRPEditorUsingDAT" & i).Split(",")(1)

                            GRPEditorUsingDATA(index) = name
                        Next


                        For i = 0 To GRPEditorUsingindexDATCount - 1
                            Dim index As Integer = FindSetting(Section_GRPEditorSET, "GRPEditorUsingindexDAT" & i).Split(",")(0)
                            Dim name As String = FindSetting(Section_GRPEditorSET, "GRPEditorUsingindexDAT" & i).Split(",")(1)

                            GRPEditorUsingindexDATA(index) = name
                        Next
                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "GRPEditor").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try




                    Try
                        Dim Section_FileManagerSET As String = FindSection(text, "FileManagerSET")


                        statlang = FindSetting(Section_FileManagerSET, "statlang")
                        For i = 0 To FindSetting(Section_FileManagerSET, "stattextdicCount") - 1
                            Try
                                stattextdic.Add(FindSetting(Section_FileManagerSET, "stattextdickey" & i), FindSetting(Section_FileManagerSET, "stattextdicvalue" & i))
                            Catch ex As Exception
                                MsgBox(i & "," & FindSetting(Section_FileManagerSET, "stattextdickey" & i) & "," & FindSetting(Section_FileManagerSET, "stattextdicvalue" & i))
                            End Try
                        Next

                        If FindSetting(Section_FileManagerSET, "wireuse") Then
                            If savefileVersion = "v 0.1" Or savefileVersion = "v TEST.z" Then
                                For i = 0 To 227
                                    wireframData(i) = FindSetting(Section_FileManagerSET, "wireframData" & i)
                                    grpwireData(i) = FindSetting(Section_FileManagerSET, "grpwireData" & i)
                                    tranwireData(i) = FindSetting(Section_FileManagerSET, "tranwireData" & i)
                                Next
                            Else
                                For i = 0 To 227
                                    If FindSetting(Section_FileManagerSET, "wireframData" & i) <> 0 Then
                                        wireframData(i) = FindSetting(Section_FileManagerSET, "wireframData" & i) - 1
                                    End If

                                    If FindSetting(Section_FileManagerSET, "grpwireData" & i) <> 0 Then
                                        grpwireData(i) = FindSetting(Section_FileManagerSET, "grpwireData" & i) - 1
                                    End If

                                    If FindSetting(Section_FileManagerSET, "tranwireData" & i) <> 0 Then
                                        tranwireData(i) = FindSetting(Section_FileManagerSET, "tranwireData" & i) - 1
                                    End If

                                Next
                            End If
                        End If


                    Catch ex As Exception
                        MsgBox(ex.ToString)
                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "FileManager").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try




                    Try
                        Dim Section_PluginSET As String = FindSection(text, "PluginSET")
                        soundstopper = FindSetting(Section_PluginSET, "soundstopper")
                        scmloader = FindSetting(Section_PluginSET, "scmloader")
                        noAirCollision = FindSetting(Section_PluginSET, "noAirColsion")
                        unlimiter = FindSetting(Section_PluginSET, "unlimiter")
                        keepSTR = FindSetting(Section_PluginSET, "keepSTR")
                        eudTurbo = FindSetting(Section_PluginSET, "eudTurbo")
                        iscriptPatcher = FindSetting(Section_PluginSET, "iscriptPatcher")
                        iscriptPatcheruse = FindSetting(Section_PluginSET, "iscriptPatcheruse")
                        unpatcher = FindSetting(Section_PluginSET, "unpatcher")
                        unpatcheruse = FindSetting(Section_PluginSET, "unpatcheruse")
                        nqcuse = FindSetting(Section_PluginSET, "nqcuse")
                        nqcunit = FindSetting(Section_PluginSET, "nqcunit")
                        nqclocs = FindSetting(Section_PluginSET, "nqclocs", True)
                        nqccommands = FindSetting(Section_PluginSET, "nqccommands", True)


                        grpinjectoruse = FindSetting(Section_PluginSET, "grpinjectoruse")

                        grpinjector_arrow = FindSetting(Section_PluginSET, "grpinjector_arrow")
                        grpinjector_drag = FindSetting(Section_PluginSET, "grpinjector_drag")
                        grpinjector_illegal = FindSetting(Section_PluginSET, "grpinjector_illegal")


                        dataDumperuse = FindSetting(Section_PluginSET, "dataDumperuse")
                        dataDumper_user = FindSetting(Section_PluginSET, "dataDumper_user")
                        dataDumper_grpwire = FindSetting(Section_PluginSET, "dataDumper_grpwire")
                        dataDumper_tranwire = FindSetting(Section_PluginSET, "dataDumper_tranwire")
                        dataDumper_wirefram = FindSetting(Section_PluginSET, "dataDumper_wirefram")
                        dataDumper_cmdicons = FindSetting(Section_PluginSET, "dataDumper_cmdicons")
                        dataDumper_stat_txt = FindSetting(Section_PluginSET, "dataDumper_stat_txt")
                        dataDumper_AIscript = FindSetting(Section_PluginSET, "dataDumper_AIscript")
                        dataDumper_iscript = FindSetting(Section_PluginSET, "dataDumper_iscript")

                        dataDumper_grpwire_f = FindSetting(Section_PluginSET, "dataDumper_grpwire_f")
                        dataDumper_tranwire_f = FindSetting(Section_PluginSET, "dataDumper_tranwire_f")
                        dataDumper_wirefram_f = FindSetting(Section_PluginSET, "dataDumper_wirefram_f")
                        dataDumper_cmdicons_f = FindSetting(Section_PluginSET, "dataDumper_cmdicons_f")
                        dataDumper_stat_txt_f = FindSetting(Section_PluginSET, "dataDumper_stat_txt_f")
                        dataDumper_AIscript_f = FindSetting(Section_PluginSET, "dataDumper_AIscript_f")
                        dataDumper_iscript_f = FindSetting(Section_PluginSET, "dataDumper_iscript_f")



                        extraedssetting = FindSetting(Section_PluginSET, "extraedssetting")

                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "Plugin").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try

                    Try
                        Dim Section_SCDBSET As String = FindSection(text, "SCDBSet")
                        SCDBDeath = FindSetting(Section_SCDBSET, "SCDBDeath").Split({","}, StringSplitOptions.RemoveEmptyEntries).ToList
                        SCDBLoc = FindSetting(Section_SCDBSET, "SCDBLoc").Split({","}, StringSplitOptions.RemoveEmptyEntries).ToList
                        SCDBLocLoad = FindSetting(Section_SCDBSET, "SCDBLocLoad").Split({","}, StringSplitOptions.RemoveEmptyEntries).ToList
                        SCDBVariable = FindSetting(Section_SCDBSET, "SCDBVariable").Split({","}, StringSplitOptions.RemoveEmptyEntries).ToList
                        SCDBMaker = FindSetting(Section_SCDBSET, "SCDBMaker")
                        SCDBMapName = FindSetting(Section_SCDBSET, "SCDBMapName")

                        '호환성
                        If SCDBLoc.Count <> SCDBLocLoad.Count Then
                            SCDBLoc.Clear()
                            SCDBLocLoad.Clear()
                        End If

                        SCDBUse = FindSetting(Section_SCDBSET, "SCDBUse")
                        SCDBSerial = FindSetting(Section_SCDBSET, "SCDBSerial")
                        SCDBDataSize = FindSetting(Section_SCDBSET, "SCDBDataSize")
                        'If SCDBUse Then
                        '    If SCDBLoginForm.ShowDialog() <> DialogResult.Yes Then
                        '        SCDBUse = False
                        '    End If
                        'End If



                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "SCDB").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try




                    Try
                        Dim Section As String = FindSection(text, "TriggerEditorSET")
                        LoadTriggerFile(Section, True)
                    Catch ex As Exception
                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "TriggerEditor").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try





                    Try
                        LoadCHKdata()
                    Catch ex As Exception
                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "scenario.chk").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)


                        Exit Sub
                    End Try


                    If savefileVersion <> ProgramSet.Version Then
                        MsgBox(Lan.GetText("MsgBox", "SaveFileDifferent").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Information, ProgramSet.AlterFormMessage)
                    End If

                    ProjectSet.filename = MapName
                    ProjectSet.saveStatus = True
                    ProjectSet.isload = True
                Case ".ees"

                    euddraftuse = True

                    Dim savefileVersion As String = ""
                    TriggerSetTouse = True
                    TriggerPlayer = 7

                    Dim filestream As New FileStream(MapName, FileMode.Open, FileAccess.Read)
                    Dim StrReader As New StreamReader(filestream)
                    Dim text As String = StrReader.ReadToEnd()
                    Dim ctext As String = text.Replace("Start", "S_").Replace("End", "E_")


                    Dim textarr() As String = text.Split(vbCrLf)

                    StrReader.Close()
                    filestream.Close()

                    Try
                        savefileVersion = textarr(0).Trim 'Ver00.8.72

                        UsedSetting(Settingtype.filemanager) = True
                        UsedSetting(Settingtype.Plugin) = True
                        UsedSetting(Settingtype.Struct) = True
                        If textarr(1).Trim = True Then
                            UsedSetting(Settingtype.DatEdit) = True
                            UsedSetting(Settingtype.FireGraft) = True
                        Else
                            UsedSetting(Settingtype.DatEdit) = False
                            UsedSetting(Settingtype.FireGraft) = False
                        End If 'True	EUD트리거


                        UsedSetting(Settingtype.BtnSet) = True ' StrReader.ReadLine() 'True	버튼셋

                        UsedSetting(Settingtype.GRP) = textarr(3).Trim 'True	GRP
                        'StrReader.ReadLine() 'True	NoAirCollsion
                        'StrReader.ReadLine() 'True	언리미터
                        'StrReader.ReadLine() 'True	WireFrame


                        If textarr(7).Trim > 0 Then
                            UsedSetting(Settingtype.BinEditor) = True
                        Else
                            UsedSetting(Settingtype.BinEditor) = False
                        End If


                        'StrReader.ReadLine() '1   binEditor
                        InputMap = textarr(9).Trim 'D:\Game\Starcraft 1.161\maps\EUD에디터실험\EUD에디터 실험맵.scx
                        OutputMap = textarr(10).Trim 'D:\Game\Starcraft 1.161\maps\EUD에디터실험\EUD에디터 실험맵Out.scx
                        If InputMap = "" Or OutputMap = "" Then
                            ProjectSet.loading = True

                            SettingForm.PreSizeSet()
                            SettingForm.ShowDialog()

                            If ProjectSet.loading <> True Then
                                ProjectSet.Close()
                                Exit Sub
                            End If

                            ProjectSet.loading = False
                        End If
                    Catch ex As Exception
                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "Setting").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)

                        Exit Sub
                    End Try









                    'Memory 읽기시작
                    Dim mfilestream As FileStream = New FileStream(My.Application.Info.DirectoryPath & "\Data\Memory.mem", FileMode.Open, FileAccess.Read)


                    ReDim ProjectLoadingForm.Buffer(mfilestream.Length)
                    mfilestream.Read(ProjectLoadingForm.Buffer, 0, mfilestream.Length)

                    mfilestream.Close()



                    Dim _strBuilder As New StringBuilder
                    Dim startoffset As Long
                    Dim endoffset As Long
                    Dim oldaction As String

                    _strBuilder.Append(text)

                    Dim memstr As New MemoryStream(ProjectLoadingForm.Buffer)
                    Dim binread As New BinaryReader(memstr)
                    Dim binwriter As New BinaryWriter(memstr)
                    While InStr(_strBuilder.ToString, "SetMemory") <> 0
                        startoffset = InStr(_strBuilder.ToString, "SetMemory") - 1
                        endoffset = InStr(Mid(_strBuilder.ToString, startoffset), vbCrLf)
                        oldaction = Mid(_strBuilder.ToString, startoffset, endoffset).Trim

                        _strBuilder.Remove(1, startoffset)

                        Dim Textoffset As String = oldaction.Split(",")(0).Replace("SetMemory(", "").Trim
                        Dim Textmodifi As String = oldaction.Split(",")(1).Trim
                        Dim Textvalue As Long = oldaction.Split(",")(2).Replace(");", "").Trim

                        Dim value As UInt32
                        value = Textvalue
                        'If Textmodifi = "Subtract" Then
                        'value = Textvalue * -1
                        'End If


                        Dim offsetuint As UInteger = Val("&H" & Textoffset.Replace("0x", ""))
                        offsetuint -= &H50C000


                        memstr.Position = offsetuint
                        binwriter.Write(value)


                    End While
                    binread.Close()
                    binwriter.Close()
                    memstr.Close()



                    ProjectLoadingForm.Text = """" & MapName & """에서 메모리 읽어오는 중..."


                    ProjectLoadingForm.ShowDialog()


                    'wirefram불러오기
                    Try
                        Dim Section As String = FindSection(ctext, "wire")
                        If Section <> "" Then
                            Dim wiredata() As String = Section.Split(vbCrLf)
                            For i = 0 To (wiredata.Count) \ 4 - 1
                                Dim unitnum As Byte = wiredata(i * 4)

                                wireframData(unitnum) = wiredata(i * 4 + 1)
                                grpwireData(unitnum) = wiredata(i * 4 + 2)
                                tranwireData(unitnum) = wiredata(i * 4 + 3)
                            Next
                        End If
                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "WireFrame").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try



                    'GRP불러오기
                    Try
                        Dim Section As String = FindSection(ctext, "GRP")


                        Dim content() As String = Section.Split(vbCrLf, Integer.MaxValue, StringSplitOptions.RemoveEmptyEntries)

                        For i = 0 To content.Count - 1 Step 2
                            Dim index As Integer = Mid(content(i), 2)
                            Dim address As String = content(i + 1)



                            '만약 주소가 같다면 이미지 추가만 할 것.


                            Dim isoverlap As Boolean = False
                            For j = 0 To GRPEditorDATA.Count - 1
                                If GRPEditorDATA(j).Filename = address Then '주소가 같다. 즉 같은 GRP사용 중
                                    isoverlap = True
                                    If Mid(content(i), 1, 1) = "C" Then '인덱싱
                                        GRPEditorUsingindexDATA(index) = address
                                    Else
                                        GRPEditorDATA(j).usingimage.Add(index)
                                        GRPEditorUsingDATA(index) = address
                                    End If
                                    Exit For
                                End If
                            Next


                            If isoverlap = False Then
                                Dim grpdata As New GRPDATA

                                grpdata.IsExternal = True
                                grpdata.Filename = address
                                grpdata.SafeFilename = address.Split("\").Last
                                grpdata.Remapping = 0
                                grpdata.Palett = 4


                                grpdata.usingimage = New List(Of Integer)

                                If Mid(content(i), 1, 1) = "C" Then '인덱싱
                                    GRPEditorUsingindexDATA(index) = address
                                Else
                                    grpdata.usingimage.Add(index)
                                    GRPEditorUsingDATA(index) = address
                                End If

                                GRPEditorDATA.Add(grpdata)
                            End If


                        Next



                    Catch ex As Exception
                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "GRP").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try

                    'edsText불러오기
                    Try
                        Dim Section As String = FindSection(ctext, "edsText")
                        extraedssetting = Section

                        noAirCollision = textarr(4).Trim
                        unlimiter = textarr(5).Trim
                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "Plugin").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try


                    'ButtonSet불러오기
                    Try
                        Dim Section As String = FindSection(ctext, "ButtonSet")

                        Dim content() As String = Section.Split(vbCrLf, Integer.MaxValue, StringSplitOptions.RemoveEmptyEntries)
                        For i = 0 To content.Count - 1 Step 3
                            Dim btnnum As Integer = content(i).Trim
                            Dim btncount As Integer = content(i + 1).Trim
                            Dim btndata As String = content(i + 2).Trim

                            ProjectBtnUSE(btnnum) = True

                            ProjectBtnData(btnnum).Clear()
                            For j = 0 To btncount - 1
                                ProjectBtnData(btnnum).Add(New SBtnDATA(Mid(btndata, 60 * j + 1, 60)))
                            Next
                        Next
                    Catch ex As Exception
                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "Buttonset").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try



                    'bin불러오기
                    Try
                        Dim Section As String = FindSection(ctext, "bin")
                        If Section <> "" Then
                            Dim content() As String = Section.Split(vbCrLf)

                            PjcutData.RemoveAt(0)
                            PjcutData.Insert(0, New CsceneData)
                            For i = 0 To content.Count - 1 Step 7
                                Dim bfilenum As Integer = content(i).Trim
                                Dim objnum As Integer = content(i + 1).Trim
                                Dim oleft As Integer = content(i + 2).Trim
                                Dim oright As Integer = content(i + 3).Trim
                                Dim owidth As Integer = content(i + 4).Trim


                                Dim oheight As Integer = content(i + 5).Trim
                                Dim ifilename As String = content(i + 6).Trim


                                If objnum = 0 Then
                                    PjcutData(0).binData(bfilenum).imagename = ifilename

                                    PjcutData(0).binData(bfilenum).pos = New Point(oleft, oright)
                                    PjcutData(0).binData(bfilenum).size = New Size(owidth, oheight)
                                Else
                                    PjcutData(0).binData(bfilenum).ObjDlg(objnum - 1).pos = New Point(oleft, oright)
                                    PjcutData(0).binData(bfilenum).ObjDlg(objnum - 1).size = New Size(owidth, oheight)
                                End If

                            Next

                            '파일 번호
                            '폼 오브젝트 번호
                            '왼쪽
                            '위
                            '넓이
                            '높이
                            '파일이름
                        End If



                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "Bin").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try



                    Try
                        LoadCHKdata()
                    Catch ex As Exception
                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "scenario.chk").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try



                    ProjectSet.filename = MapName
                    ProjectSet.saveStatus = True
                    ProjectSet.isload = True
                Case ".mem"
                    Dim savefileVersion As String = "mem"

                    euddraftuse = True
                    LoadFromCHK = False
                    For i = 0 To UsedSetting.Count - 1
                        UsedSetting(i) = False
                    Next

                    UsedSetting(0) = True
                    UsedSetting(1) = True


                    TriggerSetTouse = True
                    TriggerPlayer = 7

                    ProjectSet.loading = True

                    SettingForm.PreSizeSet()
                    SettingForm.ShowDialog()

                    If ProjectSet.loading <> True Then
                        ProjectSet.Close()
                        Exit Sub
                    End If

                    ProjectSet.loading = False




                    Dim filestream As FileStream = New FileStream(MapName, FileMode.Open, FileAccess.Read)


                    ReDim ProjectLoadingForm.Buffer(filestream.Length)
                    filestream.Read(ProjectLoadingForm.Buffer, 0, filestream.Length)

                    filestream.Close()

                    ProjectLoadingForm.Text = """" & MapName & """에서 메모리 읽어오는 중..."


                    ProjectLoadingForm.ShowDialog()



                    Try
                        LoadCHKdata()
                    Catch ex As Exception

                        MsgBox(Lan.GetText("MsgBox", "LodingError").Replace("$S0$", "scenario.chk").Replace("$S1$", ProgramSet.Version).Replace("$S2$", savefileVersion), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        Exit Sub
                    End Try



                    ProjectSet.filename = MapName
                    ProjectSet.saveStatus = True
                    ProjectSet.isload = True

            End Select

            If extension = ".e2p" Then
                RenameFileAll()
            End If

            LoadFileimportable()
        End Sub



        Sub DeleteFilesFromFolder(Folder As String)

            mciSendString("close all", Nothing, 0, 0)
            If Directory.Exists(Folder) Then
                For Each _file As String In Directory.GetFiles(Folder)
                    File.Delete(_file)
                Next
                For Each _folder As String In Directory.GetDirectories(Folder)

                    DeleteFilesFromFolder(_folder)
                Next
            End If
        End Sub

        Public Sub Save(MapName As String)
            Dim issavefilezip As Boolean = False

            If MapName.EndsWith(".e2p") Then
                '집 파일이면
                issavefilezip = True
            End If



            Dim isnewfile As Boolean = False


            If CheckFileExist(MapName) Then
                isnewfile = True
            End If




            Dim _stringbdl As New StringBuilder


            DeleteFilesFromFolder(My.Application.Info.DirectoryPath & "\Data\temp")
            Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\Data\temp\saveFile")


            Dim count As Integer = 0

            If issavefilezip And isnewfile Then
                ProjectSet.filename = MapName.Replace(GetSafeName(MapName), "") & GetSafeName(MapName).Split(".").First & "\" & GetSafeName(MapName)
            Else
                ProjectSet.filename = MapName
            End If
            ProjectSet.saveStatus = True
            ProjectSet.isload = True

            Dim file As FileStream

            Dim savefilename As String
            If issavefilezip = True And isnewfile = True Then
                Directory.CreateDirectory(MapName.Replace(".e2p", ""))
                savefilename = MapName.Replace(".e2p", "") & "\" & GetSafeName(MapName)
            Else
                savefilename = MapName
            End If

            file = New FileStream(savefilename, FileMode.Create, FileAccess.Write)

            'Dim file As FileStream = New FileStream(MapName, FileMode.Create, FileAccess.Write)
            Dim stream As StreamWriter = New StreamWriter(file)



            _stringbdl.Append("S_ProjectSET" & vbCrLf) 'ProjectSET Start
            _stringbdl.Append("Version : " & ProgramSet.Version & vbCrLf)
            _stringbdl.Append("InputMap : " & InputMap & vbCrLf)
            _stringbdl.Append("OutputMap : " & OutputMap & vbCrLf)
            _stringbdl.Append("euddraftuse : " & euddraftuse & vbCrLf)
            _stringbdl.Append("loadfromCHK : " & LoadFromCHK & vbCrLf)
            For i = 0 To UsedSetting.Count - 1
                _stringbdl.Append("UsedSetting" & i & " : " & UsedSetting(i) & vbCrLf)
            Next
            _stringbdl.Append("EUDEditorDebug : " & EUDEditorDebug & vbCrLf)
            _stringbdl.Append("epTraceDebug : " & epTraceDebug & vbCrLf)

            _stringbdl.Append("triggerSetTouse : " & TriggerSetTouse & vbCrLf)
            _stringbdl.Append("triggerPlayer : " & TriggerPlayer & vbCrLf)

            _stringbdl.Append("E_ProjectSET" & vbCrLf)



            _stringbdl.Append("S_DatEditSET" & vbCrLf) 'DatEditSET Start
            For i = 0 To DatEditDATA.Count - 1
                For j = 0 To DatEditDATA(i).projectdata.Count - 1
                    For k = 0 To DatEditDATA(i).projectdata(j).Count - 1
                        If DatEditDATA(i).projectdata(j)(k) <> 0 Then
                            _stringbdl.Append(i & "," & j & "," & k & "," & DatEditDATA(i).projectdata(j)(k) & vbCrLf)
                        End If
                    Next
                Next
            Next
            _stringbdl.Append("E_DatEditSET" & vbCrLf)

            _stringbdl.Append("S_FireGraftSET" & vbCrLf) 'DatEditSET Start
            For i = 0 To 227
                If (ProjectUnitStatusFn1(i) <> 0) Or (ProjectUnitStatusFn2(i) <> 0) Or (ProjectDebugID(i) <> 0) Then
                    _stringbdl.Append("FireGraft" & i & " : " & ProjectUnitStatusFn1(i) & "," & ProjectUnitStatusFn2(i) & "," & ProjectDebugID(i) & vbCrLf)
                End If
            Next
            _stringbdl.Append("E_FireGraftSET" & vbCrLf)
            _stringbdl.Append("S_BtnSET" & vbCrLf) 'DatEditSET Start
            For i = 0 To 249
                If ProjectBtnUSE(i) = True Then
                    Dim tstr As String = ""

                    _stringbdl.Append("BtnUse" & i & " : " & ProjectBtnUSE(i) & vbCrLf)

                    tstr = "BtnData" & i & " : "
                    For k = 0 To ProjectBtnData(i).Count - 1
                        tstr = tstr & ProjectBtnData(i)(k).pos & ","
                        tstr = tstr & ProjectBtnData(i)(k).icon & ","
                        tstr = tstr & ProjectBtnData(i)(k).con & ","
                        tstr = tstr & ProjectBtnData(i)(k).act & ","
                        tstr = tstr & ProjectBtnData(i)(k).conval & ","
                        tstr = tstr & ProjectBtnData(i)(k).actval & ","
                        tstr = tstr & ProjectBtnData(i)(k).enaStr & ","
                        tstr = tstr & ProjectBtnData(i)(k).disStr & ","
                    Next



                    _stringbdl.Append(tstr & vbCrLf)
                End If
            Next
            _stringbdl.Append("E_BtnSET" & vbCrLf)

            _stringbdl.Append("S_ReqSET" & vbCrLf) 'DatEditSET Start
            For i = 0 To ProjectRequireDataUSE.Count - 1
                For j = 0 To ProjectRequireDataUSE(i).Count - 1
                    If ProjectRequireDataUSE(i)(j) <> 0 Then
                        _stringbdl.Append("ReqUse" & i & "," & j & " : " & ProjectRequireDataUSE(i)(j) & vbCrLf)
                        _stringbdl.Append("ReqPos" & i & "," & j & " : " & ProjectRequireData(i)(j).pos & vbCrLf)
                        _stringbdl.Append("ReqCount" & i & "," & j & " : " & ProjectRequireData(i)(j).Code.Count & vbCrLf)

                        For p = 0 To ProjectRequireData(i)(j).Code.Count - 1
                            _stringbdl.Append("ReqData" & i & "," & j & "," & p & " : " & ProjectRequireData(i)(j).Code(p) & vbCrLf)

                        Next

                    End If
                Next

            Next
            _stringbdl.Append("E_ReqSET" & vbCrLf)

            _stringbdl.Append("S_BinEditSET" & vbCrLf)
            _stringbdl.Append("projectsceneDataCount : " & PjcutData.Count & vbCrLf)
            For j = 0 To PjcutData.Count - 1
                'projectsceneData(j).scenename


                _stringbdl.Append("projectsceneData " & j & "binDataCount" & " : " & PjcutData(j).binData.Count & vbCrLf)
                For i = 0 To PjcutData(j).binData.Count - 1
                    Dim isDefacult As Boolean = False

                    '기본값인지 검사
                    If PjcutData(j).binData(i).imagename = BinfileData.binData(i).imagename And
                      PjcutData(j).binData(i).pos.X = BinfileData.binData(i).pos.X And
                      PjcutData(j).binData(i).pos.Y = BinfileData.binData(i).pos.Y And
                      PjcutData(j).binData(i).size.Width = BinfileData.binData(i).size.Width And
                      PjcutData(j).binData(i).size.Height = BinfileData.binData(i).size.Height Then
                        Dim ObjectCheck As Boolean = True
                        For k = 0 To PjcutData(j).binData(i).ObjDlg.Count - 1
                            If PjcutData(j).binData(i).ObjDlg(k).pos.X <> BinfileData.binData(i).ObjDlg(k).pos.X Or
                                PjcutData(j).binData(i).ObjDlg(k).pos.Y <> BinfileData.binData(i).ObjDlg(k).pos.Y Then
                                ObjectCheck = False
                            End If
                        Next
                        If ObjectCheck Then
                            isDefacult = True
                        End If
                    End If


                    If isDefacult = False Then '다르다
                        _stringbdl.Append(".(" & j & ")(" & i & ")isDefault : " & True & vbCrLf)


                        _stringbdl.Append(".(" & j & ")(" & i & ")imagename : " & PjcutData(j).binData(i).imagename & vbCrLf)
                        _stringbdl.Append(".(" & j & ")(" & i & ")posx : " & PjcutData(j).binData(i).pos.X & vbCrLf)
                        _stringbdl.Append(".(" & j & ")(" & i & ")posy : " & PjcutData(j).binData(i).pos.Y & vbCrLf)
                        _stringbdl.Append(".(" & j & ")(" & i & ")sizew : " & PjcutData(j).binData(i).size.Width & vbCrLf)
                        _stringbdl.Append(".(" & j & ")(" & i & ")sizeh : " & PjcutData(j).binData(i).size.Height & vbCrLf)

                        _stringbdl.Append("projectsceneData " & j & "binData" & i & "ObjectdiaCount" & " : " & PjcutData(j).binData(i).ObjDlg.Count & vbCrLf)
                        For k = 0 To PjcutData(j).binData(i).ObjDlg.Count - 1

                            If PjcutData(j).binData(i).ObjDlg(k).pos.X <> BinfileData.binData(i).ObjDlg(k).pos.X Or
                               PjcutData(j).binData(i).ObjDlg(k).pos.Y <> BinfileData.binData(i).ObjDlg(k).pos.Y Then
                                _stringbdl.Append(".(" & j & ")(" & i & ")(" & k & ")isDefault : " & True & vbCrLf)
                                _stringbdl.Append(".(" & j & ")(" & i & ")(" & k & ")posx : " & PjcutData(j).binData(i).ObjDlg(k).pos.X & vbCrLf)
                                _stringbdl.Append(".(" & j & ")(" & i & ")(" & k & ")posy : " & PjcutData(j).binData(i).ObjDlg(k).pos.Y & vbCrLf)




                            End If

                        Next
                    End If







                Next
            Next
            _stringbdl.Append("E_BinEditSET" & vbCrLf)


            _stringbdl.Append("S_SoundPlayerSET" & vbCrLf) 'GRPEditorSET Start

            _stringbdl.Append("Soundinterval : " & Soundinterval & vbCrLf)
            For i = 0 To Soundlist.Count - 1
                _stringbdl.Append(Soundlist(i) & vbCrLf)
            Next


            'GRPEditorSET Start


            _stringbdl.Append("E_SoundPlayerSET" & vbCrLf)

            'Public GRPEditorDAT As New List(Of GRPDATA)
            'Public GRPEditorUsingDAT(998) As String
            'Public GRPEditorUsingindexDAT(4999) As String
            'Class GRPDATA
            'Public IsExternal As Boolean
            'Public Filename As String
            'Public SafeFilename As String
            'Public Remapping As Integer
            'Public Palett As Integer
            'Public usingimage As List(Of Integer)
            'End Class
            _stringbdl.Append("S_GRPEditorSET" & vbCrLf) 'GRPEditorSET Start

            _stringbdl.Append("GRPEditorDATCount : " & GRPEditorDATA.Count & vbCrLf)
            For i = 0 To GRPEditorDATA.Count - 1
                _stringbdl.Append("GRPEditorDATIsExternal" & i & " : " & GRPEditorDATA(i).IsExternal & vbCrLf)
                _stringbdl.Append("GRPEditorDATFilename" & i & " : " & GRPEditorDATA(i).Filename & vbCrLf)
                _stringbdl.Append("GRPEditorDATSafeFilename" & i & " : " & GRPEditorDATA(i).SafeFilename & vbCrLf)
                _stringbdl.Append("GRPEditorDATRemapping" & i & " : " & GRPEditorDATA(i).Remapping & vbCrLf)
                _stringbdl.Append("GRPEditorDATPalett" & i & " : " & GRPEditorDATA(i).Palett & vbCrLf)

                _stringbdl.Append("GRPEditorDATusingimageCount" & i & " : " & GRPEditorDATA(i).usingimage.Count & vbCrLf)
                For j = 0 To GRPEditorDATA(i).usingimage.Count - 1
                    _stringbdl.Append("GRPEditorDATusingimage" & i & "," & j & " : " & GRPEditorDATA(i).usingimage(j) & vbCrLf)
                Next
            Next


            count = 0
            For i = 0 To GRPEditorUsingDATA.Count - 1
                If GRPEditorUsingDATA(i) <> "" Then
                    _stringbdl.Append("GRPEditorUsingDAT" & count & " : " & i & "," & GRPEditorUsingDATA(i) & vbCrLf)
                    count += 1
                End If
            Next
            _stringbdl.Append("GRPEditorUsingDATCount : " & count & vbCrLf)


            count = 0
            For i = 0 To GRPEditorUsingindexDATA.Count - 1
                If GRPEditorUsingindexDATA(i) <> "" Then
                    _stringbdl.Append("GRPEditorUsingindexDAT" & count & " : " & i & "," & GRPEditorUsingindexDATA(i) & vbCrLf)
                    count += 1
                End If
            Next
            _stringbdl.Append("GRPEditorUsingindexDATCount : " & count & vbCrLf)


            _stringbdl.Append("E_GRPEditorSET" & vbCrLf)



            _stringbdl.Append("S_TileSET" & vbCrLf)
            _stringbdl.Append("ProjectTileUseFile : " & ProjectTileUseFile & vbCrLf)
            _stringbdl.Append("ProjectTileSetFileName : " & ProjectTileSetFileName & vbCrLf)
            _stringbdl.Append("ProjectTIleMSetCount : " & ProjectTIleMSet.Count & vbCrLf)

            Dim ProjectTIleMSetArray As String = ""
            For i = 0 To ProjectTIleMSet.Count - 1
                If ProjectTIleMSet(i) <> 0 Then
                    _stringbdl.Append("ProjectTIleMSet" & i & " : " & ProjectTIleMSet(i) & vbCrLf)
                    ProjectTIleMSetArray = ProjectTIleMSetArray & i & ","
                End If
            Next
            _stringbdl.Append("ProjectTIleMSetArray : " & ProjectTIleMSetArray & vbCrLf) 'ProjectTIleMSet.Count & vbCrLf)


            _stringbdl.Append("ProjectTileSetDataCount : " & ProjectTileSetData.Count & vbCrLf)
            For i = 0 To ProjectTileSetData.Count - 1
                Dim bytearray As String = ""
                For j = 0 To 1023
                    bytearray = bytearray & ProjectTileSetData(i).TileSetData(j) & ","
                Next
                _stringbdl.Append("ProjectTileSetDataTileSetData" & i & " : " & bytearray & vbCrLf)
                _stringbdl.Append("ProjectTileSetDataTileSetNum" & i & " : " & ProjectTileSetData(i).TileSetNum & vbCrLf)
                _stringbdl.Append("ProjectTileSetDataisMaker" & i & " : " & ProjectTileSetData(i).isMaker & vbCrLf)
            Next


            _stringbdl.Append("ProjectMTXMDATACount : " & ProjectMTXMDATA.Count & vbCrLf)

            Dim ProjectMTXMDATAArray As String = ""
            For i = 0 To ProjectMTXMDATA.Count - 1
                If ProjectMTXMDATA(i) <> 0 Then
                    _stringbdl.Append("ProjectMTXMDATA" & i & " : " & ProjectMTXMDATA(i) & vbCrLf)
                    ProjectMTXMDATAArray = ProjectMTXMDATAArray & i & ","
                End If
            Next
            _stringbdl.Append("ProjectMTXMDATAArray : " & ProjectMTXMDATAArray & vbCrLf) ' ProjectMTXMDATA.Count & vbCrLf)


            _stringbdl.Append("E_TileSET" & vbCrLf)





            _stringbdl.Append("S_SCDBSet" & vbCrLf) 'FileManagerSET Start

            Dim Ststr As String = ""
            If SCDBDeath.Count <> 0 Then
                Ststr = SCDBDeath(0)
                For i = 1 To SCDBDeath.Count - 1
                    Ststr = Ststr & "," & SCDBDeath(i)
                Next
            End If
            _stringbdl.Append("SCDBDeath : " & Ststr & vbCrLf)

            Ststr = ""
            If SCDBLoc.Count <> 0 Then
                Ststr = SCDBLoc(0)
                For i = 1 To SCDBLoc.Count - 1
                    Ststr = Ststr & "," & SCDBLoc(i)
                Next
            End If

            _stringbdl.Append("SCDBLoc : " & Ststr & vbCrLf)

            Ststr = ""
            If SCDBVariable.Count <> 0 Then
                Ststr = SCDBVariable(0)
                For i = 1 To SCDBVariable.Count - 1
                    Ststr = Ststr & "," & SCDBVariable(i)
                Next
            End If

            _stringbdl.Append("SCDBVariable : " & Ststr & vbCrLf)




            Ststr = ""
            If SCDBLocLoad.Count <> 0 Then
                Ststr = SCDBLocLoad(0)
                For i = 1 To SCDBLocLoad.Count - 1
                    Ststr = Ststr & "," & SCDBLocLoad(i)
                Next
            End If
            _stringbdl.Append("SCDBLocLoad : " & Ststr & vbCrLf)

            _stringbdl.Append("SCDBUse : " & SCDBUse & vbCrLf)
            _stringbdl.Append("SCDBSerial : " & SCDBSerial & vbCrLf)
            _stringbdl.Append("SCDBMaker : " & SCDBMaker & vbCrLf)
            _stringbdl.Append("SCDBMapName : " & SCDBMapName & vbCrLf)
            _stringbdl.Append("SCDBDataSize : " & SCDBDataSize & vbCrLf)


            _stringbdl.Append("E_SCDBSet" & vbCrLf)








            _stringbdl.Append("S_FileManagerSET" & vbCrLf) 'FileManagerSET Start


            _stringbdl.Append("statlang : " & statlang & vbCrLf)
            _stringbdl.Append("stattextdicCount : " & stattextdic.Count & vbCrLf)
            For i = 0 To stattextdic.Count - 1
                _stringbdl.Append("stattextdickey" & i & " : " & stattextdic.Keys(i) & vbCrLf)
                _stringbdl.Append("stattextdicvalue" & i & " : " & stattextdic.Values(i) & vbCrLf)
            Next
            _stringbdl.Append("wireuse : True" & vbCrLf)
            For i = 0 To 227
                If wireframData(i) <> i Then
                    _stringbdl.Append("wireframData" & i & " : " & wireframData(i) + 1 & vbCrLf)
                End If
                If grpwireData(i) <> i Then
                    _stringbdl.Append("grpwireData" & i & " : " & grpwireData(i) + 1 & vbCrLf)
                End If
                If grpwireData(i) <> i Then
                    _stringbdl.Append("tranwireData" & i & " : " & tranwireData(i) + 1 & vbCrLf)
                End If

            Next

            _stringbdl.Append("E_FileManagerSET" & vbCrLf)



            _stringbdl.Append("S_TriggerEditorSET" & vbCrLf)
            _stringbdl.Append(SaveTrigger() & vbCrLf)
            _stringbdl.Append("E_TriggerEditorSET" & vbCrLf)





            _stringbdl.Append("S_PluginSET" & vbCrLf) 'PluginSET Start
            _stringbdl.Append("soundstopper : " & soundstopper & vbCrLf)
            _stringbdl.Append("scmloader : " & scmloader & vbCrLf)
            _stringbdl.Append("noAirColsion : " & noAirCollision & vbCrLf)
            _stringbdl.Append("unlimiter : " & unlimiter & vbCrLf)
            _stringbdl.Append("keepSTR : " & keepSTR & vbCrLf)
            _stringbdl.Append("eudTurbo : " & eudTurbo & vbCrLf)
            _stringbdl.Append("iscriptPatcher : " & iscriptPatcher & vbCrLf)
            _stringbdl.Append("iscriptPatcheruse : " & iscriptPatcheruse & vbCrLf)
            _stringbdl.Append("unpatcher : " & unpatcher & vbCrLf)
            _stringbdl.Append("unpatcheruse : " & unpatcheruse & vbCrLf)
            _stringbdl.Append("nqcuse : " & nqcuse & vbCrLf)
            _stringbdl.Append("nqcunit : " & nqcunit & vbCrLf)
            _stringbdl.Append("nqclocs : " & nqclocs & vbCrLf)
            _stringbdl.Append("nqccommands : " & nqccommands & vbCrLf)

            _stringbdl.Append("grpinjectoruse : " & grpinjectoruse & vbCrLf)

            _stringbdl.Append("grpinjector_arrow : " & grpinjector_arrow & vbCrLf)
            _stringbdl.Append("grpinjector_drag : " & grpinjector_drag & vbCrLf)
            _stringbdl.Append("grpinjector_illegal : " & grpinjector_illegal & vbCrLf)

            _stringbdl.Append("dataDumperuse : " & dataDumperuse & vbCrLf)

            _stringbdl.Append("dataDumper_user : " & dataDumper_user & vbCrLf)

            _stringbdl.Append("dataDumper_grpwire : " & dataDumper_grpwire & vbCrLf)
            _stringbdl.Append("dataDumper_tranwire : " & dataDumper_tranwire & vbCrLf)
            _stringbdl.Append("dataDumper_wirefram : " & dataDumper_wirefram & vbCrLf)
            _stringbdl.Append("dataDumper_cmdicons : " & dataDumper_cmdicons & vbCrLf)
            _stringbdl.Append("dataDumper_stat_txt : " & dataDumper_stat_txt & vbCrLf)
            _stringbdl.Append("dataDumper_AIscript : " & dataDumper_AIscript & vbCrLf)
            _stringbdl.Append("dataDumper_iscript : " & dataDumper_iscript & vbCrLf)

            _stringbdl.Append("dataDumper_grpwire_f : " & dataDumper_grpwire_f & vbCrLf)
            _stringbdl.Append("dataDumper_tranwire_f : " & dataDumper_tranwire_f & vbCrLf)
            _stringbdl.Append("dataDumper_wirefram_f : " & dataDumper_wirefram_f & vbCrLf)
            _stringbdl.Append("dataDumper_cmdicons_f : " & dataDumper_cmdicons_f & vbCrLf)
            _stringbdl.Append("dataDumper_stat_txt_f : " & dataDumper_stat_txt_f & vbCrLf)
            _stringbdl.Append("dataDumper_AIscript_f : " & dataDumper_AIscript_f & vbCrLf)
            _stringbdl.Append("dataDumper_iscript_f : " & dataDumper_iscript_f & vbCrLf)

            _stringbdl.Append("extraedssetting : " & extraedssetting & vbCrLf)

            _stringbdl.Append("E_PluginSET" & vbCrLf)



            stream.Write(_stringbdl.ToString)


            stream.Close()
            file.Close()

            If issavefilezip = True Then
                If isnewfile = True Then
                    Dim foldername As String = MapName.Replace(".e2p", "")
                    '세이브파일 폴더에 파일들을 몽땅 넣어버린다.
                    Directory.CreateDirectory(foldername & "\Resource")
                    Directory.CreateDirectory(foldername & "\Map")
                    Directory.CreateDirectory(foldername & "\eudplibdata")
                    Directory.CreateDirectory(foldername & "\Grp")
                    Directory.CreateDirectory(foldername & "\Sound")
                    Directory.CreateDirectory(foldername & "\temp")

                    '이름을 모두 상대주소로 저장해 버린다.
                    '우선 맵 먼저
                    MoveFileAll(foldername)
                Else
                    Dim foldername As String = MapName.Replace("\" & GetSafeName(MapName), "")
                    Directory.CreateDirectory(foldername & "\Resource")
                    Directory.CreateDirectory(foldername & "\Map")
                    Directory.CreateDirectory(foldername & "\eudplibdata")
                    Directory.CreateDirectory(foldername & "\Grp")
                    Directory.CreateDirectory(foldername & "\Sound")
                    Directory.CreateDirectory(foldername & "\temp")
                    '일단 할건 없는 거로...
                    MoveFileAll(foldername)

                    DeleteDumpFileAll()
                End If
            End If

            DeleteFilesFromFolder(My.Application.Info.DirectoryPath & "\Data\temp")
        End Sub
    End Module
End Namespace