Imports System.IO
Imports System.Text

Public Class SCDBDataCreater
    Private Page As TPage
    Private Enum TPage
        Deaths
        Value
        Location
    End Enum


    Private SCDBDeathData As New List(Of UInteger)
    Private SCDBVariableData As New List(Of UInteger)
    Private SCDBLocationData As New List(Of SCDBUnitData)

    Public Class SCDBUnitData
        Private UnitCount As Byte()

        Public Sub New()
            ReDim UnitCount(227)
        End Sub

        Public Sub SetUnitCount(UnitNum As Byte, count As Byte)
            UnitCount(UnitNum) = count
        End Sub
        Public Function GetUnitCount(UnitNum As Byte) As Byte
            Return UnitCount(UnitNum)
        End Function
    End Class


    Private Sub SCDBDataCreater_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = "플레이어 이름을 입력하세요."


        SCDBDeathData.Clear()
        SCDBVariableData.Clear()
        SCDBLocationData.Clear()
        For i = 0 To SCDBDeath.Count - 1
            SCDBDeathData.Add(0)
        Next
        For i = 0 To SCDBVariable.Count - 1
            SCDBVariableData.Add(0)
        Next
        For i = 0 To SCDBLocLoad.Count - 1
            SCDBLocationData.Add(New SCDBUnitData)
        Next
        SetDataGridView(TPage.Deaths)
    End Sub

    Private Sub SetDataGridView(tpage As TPage)
        Page = tpage
        DataGridView1.Rows.Clear()
        Select Case tpage
            Case TPage.Deaths
                DataGridView1.Columns(0).HeaderText = "데스값"
                DataGridView1.Columns.RemoveAt(1)

                Dim temp As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
                temp.Width = 160
                temp.Resizable = False

                DataGridView1.Columns.Add(temp)
                For i = 0 To SCDBDeath.Count - 1
                    DataGridView1.Rows.Add(SCDBForm.units(SCDBDeath(i)), SCDBDeathData(i))
                Next
            Case TPage.Value
                DataGridView1.Columns(0).HeaderText = "변수"
                DataGridView1.Columns.RemoveAt(1)

                Dim temp As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
                temp.Width = 160
                temp.Resizable = False

                DataGridView1.Columns.Add(temp)
                For i = 0 To SCDBVariable.Count - 1
                    DataGridView1.Rows.Add(SCDBVariable(i), SCDBVariableData(i))
                Next
            Case TPage.Location
                DataGridView1.Columns(0).HeaderText = "로케이션"
                'DataGridView1.Columns(1).ValueType = GetType(DataGridViewButtonColumn)
                DataGridView1.Columns.RemoveAt(1)

                Dim temp As DataGridViewButtonColumn = New DataGridViewButtonColumn()
                temp.Width = 160
                temp.Resizable = False

                DataGridView1.Columns.Add(temp)
                For i = 0 To SCDBLocLoad.Count - 1
                    DataGridView1.Rows.Add(SCDBForm.locs(SCDBLocLoad(i)), "편집")
                Next
        End Select



    End Sub

    Private Sub BtnDeath_Click(sender As Object, e As EventArgs) Handles BtnDeath.Click
        DataSave()
        SetDataGridView(TPage.Deaths)
    End Sub

    Private Sub BtnLoc_Click(sender As Object, e As EventArgs) Handles BtnLoc.Click
        DataSave()
        SetDataGridView(TPage.Location)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataSave()
        SetDataGridView(TPage.Value)
    End Sub

    Private Sub DataSave()
        Select Case Page
            Case TPage.Deaths
                For i = 0 To SCDBDeath.Count - 1
                    Try
                        SCDBDeathData(i) = DataGridView1.Item(1, i).Value
                    Catch ex As Exception

                    End Try

                Next
            Case TPage.Value
                For i = 0 To SCDBVariable.Count - 1
                    Try
                        SCDBVariableData(i) = DataGridView1.Item(1, i).Value
                    Catch ex As Exception

                    End Try
                Next
        End Select
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataSave()

        Dim CRC32 As New CRC32

        Dim playerCode As ULong = 0

        Dim playername As Byte() = System.Text.Encoding.UTF8.GetBytes(TextBox1.Text)



        '4A 50 6F 6B 65 72


        playerCode += playername(0)

        'Dim tempstr As String = ""

        For i = 1 To playername.Length - 1
            'tempstr = tempstr & " " & Hex(playername(i))

            playerCode += (CUInt(playername(i)) << (8 * ((i - 1) Mod 4)))
            '1,2,3,4   5,6,7,8
        Next

        If playerCode > UInteger.MaxValue Then
            playerCode = playerCode Mod UInteger.MaxValue
        End If

        Dim Filename As String = Hex(CRC32.GetCRC32(SCDBMapName)) & Hex(CRC32.GetCRC32(SCDBMaker)) & Hex(playerCode) & (SCDBVariable.Count + SCDBDeath.Count + SCDBLoc.Count)
        Filename = GetMd5Hash(Filename)

        SaveFileDialog1.FileName = Filename
        Dim dialog As DialogResult = SaveFileDialog1.ShowDialog()


        If dialog = DialogResult.OK Then
            Dim DataArray As New List(Of UInteger)

            '데이터추가
            For i = 0 To SCDBDeathData.Count - 1
                If SCDBDeathData(i) <= 999 Then
                    DataArray.Add(SCDBDeathData(i) + i * 1000 + 500000)
                ElseIf SCDBDeathData(i) <= 999999999 Then
                    DataArray.Add(SCDBDeathData(i) \ 1000000 + i * 1000 + 600000)

                    DataArray.Add(SCDBDeathData(i) Mod 1000000)
                Else
                    DataArray.Add(i * 1000 + 700000)
                    DataArray.Add((SCDBDeathData(i) \ 1000000) Mod 1000000)
                    DataArray.Add(SCDBDeathData(i) Mod 1000000)

                End If
            Next
            For i = 0 To SCDBVariableData.Count - 1
                If SCDBVariableData(i) <= 999 Then
                    DataArray.Add(SCDBVariableData(i) + i * 1000)
                ElseIf SCDBVariableData(i) <= 999999999 Then
                    DataArray.Add(SCDBVariableData(i) \ 1000000 + i * 1000 + 100000)

                    DataArray.Add(SCDBVariableData(i) Mod 1000000)
                Else
                    DataArray.Add(i * 1000 + 200000)
                    DataArray.Add((SCDBVariableData(i) \ 1000000) Mod 1000000)
                    DataArray.Add(SCDBVariableData(i) Mod 1000000)

                End If

            Next
            For i = 0 To SCDBLocationData.Count - 1
                For k = 0 To 227
                    If SCDBLocationData(i).GetUnitCount(k) <> 0 Then
                        If SCDBLocationData(i).GetUnitCount(k) = 1 Then
                            DataArray.Add(k + 300000)
                        Else
                            DataArray.Add(k + 400000)

                            DataArray.Add(SCDBLocationData(i).GetUnitCount(k) + k * 1000)

                        End If
                    End If
                Next
            Next





            Dim Strb As New StringBuilder




            Strb.Append(Filename)
            For i = 0 To DataArray.Count - 1
                Strb.Append(Format(DataArray(i), "000000"))
                'MsgBox(Format(DataArray(i), "000000"))
            Next

            Dim str As String = Strb.ToString

            Dim filestream As New FileStream(SaveFileDialog1.FileName, FileMode.Create)
            Dim streamwriter As New StreamWriter(filestream)
            Dim binaryWriter As BinaryWriter

            Try
                binaryWriter = New BinaryWriter(filestream)
                binaryWriter.Write(CUInt(1111769939))
                binaryWriter.Write(CUInt(0))
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
            'MsgBox(str)
            Dim datastr As String = EncryptString128Bit(StrEncrpty(str, CByte(str.Count / 2)), GetKey)
            streamwriter.Write(datastr)


            streamwriter.Close()
            filestream.Close()


        End If
    End Sub

    Private SpecialKey() As String = {"Connect", "Saving", "Loading", "Complete"}

    Private Key As String = "<xmlns:materialDesign=""http://materialdesigninxaml.net/winfx/xaml/themes"">"
    Private Function GetKey() As String
        Dim CRC32 As New CRC32

        Dim playerCode As ULong = 0

        Dim playername As Byte() = System.Text.Encoding.UTF8.GetBytes(TextBox1.Text)



        '4A 50 6F 6B 65 72


        playerCode += playername(0)

        'Dim tempstr As String = ""

        For i = 1 To playername.Length - 1
            'tempstr = tempstr & " " & Hex(playername(i))

            playerCode += (CUInt(playername(i)) << (8 * ((i - 1) Mod 4)))
            '1,2,3,4   5,6,7,8
        Next

        If playerCode > UInteger.MaxValue Then
            playerCode = playerCode Mod UInteger.MaxValue
        End If



        '656B700C

        'MsgBox(Hex(CRC32.GetCRC32(SCDBMapName)) & " " & Hex(CRC32.GetCRC32(SCDBMaker)) & " " & Hex(playerCode) & " " & Hex(SCDBVariable.Count + SCDBDeath.Count + SCDBLoc.Count))
        Dim str As String = Key & SpecialKey(0) & Hex(CRC32.GetCRC32(SCDBMapName)) & SpecialKey(1) & Hex(CRC32.GetCRC32(SCDBMaker)) & SpecialKey(2) & Hex(playerCode) & SpecialKey(3) & Hex(SCDBVariable.Count + SCDBDeath.Count + SCDBLoc.Count)
        Return GetMd5Hash(StrDecrypt(str, CUInt(str.Count / 2)))
    End Function
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        DataSave()

        Dim dialog As DialogResult = OpenFileDialog1.ShowDialog()


        If dialog = DialogResult.OK Then
            Try
                Dim DataArray As New List(Of UInteger)


                Dim Filename As String = OpenFileDialog1.FileName.Split("\").Last

                Dim filestream As New FileStream(OpenFileDialog1.FileName, FileMode.Open)
                Dim streamReader As New StreamReader(filestream)
                Dim binaryReader As BinaryReader


                SCDBDeathData.Clear()
                SCDBVariableData.Clear()
                SCDBLocationData.Clear()
                For i = 0 To SCDBDeath.Count - 1
                    SCDBDeathData.Add(0)
                Next
                For i = 0 To SCDBVariable.Count - 1
                    SCDBVariableData.Add(0)
                Next
                For i = 0 To SCDBLocLoad.Count - 1
                    SCDBLocationData.Add(New SCDBUnitData)
                Next


                Try
                    binaryReader = New BinaryReader(filestream)
                    filestream.Position = 4

                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try

                filestream.Position = 8

                Dim str As String = streamReader.ReadToEnd()


                streamReader.Close()
                filestream.Close()


                str = StrDecrypt(DecryptString128Bit(str, GetKey), CUInt(DecryptString128Bit(str, GetKey).Count / 2))
                'MsgBox(str)
                If Mid(str, 1, Filename.Length) = Filename Then
                    str = str.Replace(Filename, "")
                Else
                    MsgBox("잘못된 플레이어 혹은 잘못된 파일입니다.")
                    Exit Sub
                End If

                For i = 0 To (str.Length / 6) - 1
                    DataArray.Add(CUInt(Mid(str, CInt(6 * i + 1), 6)))
                Next

                For i = 0 To DataArray.Count - 1
                    Select Case Math.Floor(DataArray(i) / 100000)
                        Case 0

                            Dim index As UInteger = Math.Floor(DataArray(i) / 1000)
                            Dim value As UInteger = DataArray(i) Mod 1000

                            SCDBVariableData(index) = value
                        Case 1
                            Dim index As UInteger = Math.Floor(DataArray(i) / 1000) - 100
                            Dim value As UInteger = (DataArray(i) Mod 1000) * 1000000
                            ' MsgBox("변수 값 :" & value & " 데이터 값" & DataArray(i))
                            i += 1

                            value += DataArray(i)
                            ' MsgBox("변수 값 :" & value)

                            SCDBVariableData(index) = value

                            'MsgBox("변수 값 :" & value & " 바뀐 데이터 값 : " & SCDBVariableData(index))
                        Case 2
                            Dim index As UInteger = Math.Floor(DataArray(i) / 1000) - 200
                            Dim value As UInteger = 0
                            i += 1

                            value += DataArray(i) * 1000000
                            i += 1

                            value += DataArray(i)

                            SCDBVariableData(index) = value
                        Case 3
                            Dim index As UInteger = Math.Floor(DataArray(i) / 1000) - 300
                            Dim value As UInteger = DataArray(i) Mod 1000

                            SCDBLocationData(index).SetUnitCount(value, 1)


                        Case 4
                            Dim index As UInteger = Math.Floor(DataArray(i) / 1000) - 400
                            i += 1

                            Dim unitNum As UInteger = DataArray(i) / 1000
                            Dim unitcount As UInteger = DataArray(i) Mod 1000

                            SCDBLocationData(index).SetUnitCount(unitNum, unitcount)

                        Case 5
                            Dim index As UInteger = Math.Floor(DataArray(i) / 1000) - 500
                            Dim value As UInteger = DataArray(i) Mod 1000

                            SCDBDeathData(index) = value
                        Case 6
                            Dim index As UInteger = Math.Floor(DataArray(i) / 1000) - 600
                            Dim value As UInteger = (DataArray(i) Mod 1000) * 1000000
                            i += 1

                            value += DataArray(i)

                            SCDBDeathData(index) = value
                        Case 7
                            Dim index As UInteger = Math.Floor(DataArray(i) / 1000) - 700
                            Dim value As UInteger = 0
                            i += 1

                            value += DataArray(i) * 1000000
                            i += 1

                            value += DataArray(i)

                            SCDBDeathData(index) = value
                    End Select
                Next
                '    EUDSwitchCase()(5);
                '        tIndex = UnitIndex[tDataValue / 1000 - 500];
                '        tValue = tDataValue % 1000;
                '        SetDeaths((13), (7), tValue, (tIndex));
                '    EUDBreak();
                '    EUDSwitchCase()(6);
                '        tIndex = UnitIndex[tDataValue / 1000 - 600];
                '        tValue = (tDataValue % 1000) * 1000000;
                '        i += 1;
                '        tDataValue = DataArray[i + getcurpl() * DataArrayLength];
                '        tValue += tDataValue;
                '        SetDeaths((13), (7), tValue, (tIndex));
                '    EUDBreak();
                '    EUDSwitchCase()(7);
                '        tIndex = UnitIndex[tDataValue / 1000 - 700];
                '        tValue = 0;
                '        i += 1;
                '        tDataValue = DataArray[i + getcurpl() * DataArrayLength];
                '        tValue += tDataValue * 1000000;
                '        i += 1;
                '        tDataValue = DataArray[i + getcurpl() * DataArrayLength];
                '        tValue += tDataValue;
                '        SetDeaths((13), (7), tValue, (tIndex));
                '    EUDBreak();

                SetDataGridView(Page)
            Catch ex As Exception
                MsgBox("잘못된 플레이어 혹은 잘못된 파일입니다.")
            End Try
        End If

    End Sub
    Private Function StrEncrpty(str As String, count As Byte) As String
        Dim strcount As Integer = str.Length

        Dim strbulider As New System.Text.StringBuilder

        'azcd
        If strcount Mod 2 = 0 Then
            For i = 0 To strcount / 2 - 1
                strbulider.Append(str(i))
                strbulider.Append(str(strcount - i - 1))
            Next
        Else
            For i = 0 To strcount / 2 - 1
                strbulider.Append(str(i))
                strbulider.Append(str(strcount - i - 1))
            Next
            strbulider.Append(str(Math.Floor(strcount / 2)))
        End If

        If count <> 0 Then
            Return StrEncrpty(strbulider.ToString, count - 1)
        Else
            Return strbulider.ToString
        End If
    End Function
    Private Function StrDecrypt(str As String, count As Byte) As String
        Dim strcount As Integer = str.Length

        Dim strbulider As New System.Text.StringBuilder

        'azcd
        If strcount Mod 2 = 0 Then
            For i = 0 To strcount / 2 - 1
                strbulider.Append(str(i * 2))
            Next
            For i = 0 To strcount / 2 - 1
                strbulider.Append(str(strcount - i * 2 - 1))
            Next
        Else
            For i = 0 To strcount / 2 - 1
                strbulider.Append(str(i * 2))
            Next
            strbulider.Append(str(strcount - 1))
            For i = 0 To strcount / 2 - 1
                strbulider.Append(str(strcount - (i + 1) * 2))
            Next
        End If

        If count <> 0 Then
            Return StrDecrypt(strbulider.ToString, count - 1)
        Else
            Return strbulider.ToString
        End If
    End Function
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 1 And Page = TPage.Location Then
            UnitSelecter.SCDBLocationData = SCDBLocationData(e.RowIndex)
            UnitSelecter.ShowDialog()
        End If
    End Sub

End Class