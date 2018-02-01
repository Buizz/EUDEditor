Imports System.IO

Public Class DebugViewUnitForm
    Private Sub DebugViewUnitForm_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        DataGridView1.EndEdit()
        Timer1.Enabled = False
    End Sub


    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        If e.ColumnIndex = 2 Then
            Dim filestream As New FileStream(My.Application.Info.DirectoryPath & "\Data\CUnit.txt", FileMode.Open)
            Dim streamReader As New StreamReader(filestream)

            Dim offsetNames() As String = streamReader.ReadToEnd.Split(vbCrLf)

            streamReader.Close()
            filestream.Close()


            Dim offset As UInteger = Val("&H" & Mid(DataGridView1.Rows(e.RowIndex).Cells(0).Value, 3))
            'MsgBox(Hex(offset))


            Dim value As String = DataGridView1.Rows(e.RowIndex).Cells(2).Value

            If Mid(value, 1, 2) = "0x" Then
                value = Val("&H" & Mid(value, 3))
            End If

            Try
                Select Case offsetNames(e.RowIndex).Split(",")(1)
                    Case "BW::Path*" '4
                        WinAPI.Write(offset, CUInt(value))
                    Case "BW::CUnit*" '4
                        WinAPI.Write(offset, CUInt(value))
                    Case "BW::COrder*" '4
                        WinAPI.Write(offset, CUInt(value))
                    Case "BW::CSprite*" '4
                        WinAPI.Write(offset, CUInt(value))
                    Case "s32"
                        WinAPI.Write(offset, CInt(value))
                    Case "u8"
                        WinAPI.Write(offset, CByte(value))
                    Case "u16"
                        WinAPI.Write(offset, CUShort(value))
                    Case "u32"
                        WinAPI.Write(offset, CUInt(value))
                    Case "bool"
                        WinAPI.Write(offset, CByte(value))
                    Case "UnitMovementState"
                        WinAPI.Write(offset, CByte(value))
                    Case "void*"
                        WinAPI.Write(offset, CUInt(value))
                End Select

            Catch ex As Exception

            End Try



        End If
    End Sub

    'Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    '    If e.ColumnIndex = 2 Then
    '        Dim filestream As New FileStream(My.Application.Info.DirectoryPath & "\Data\CUnit.txt", FileMode.Open)
    '        Dim streamReader As New StreamReader(filestream)

    '        Dim offsetNames() As String = streamReader.ReadToEnd.Split(vbCrLf)

    '        streamReader.Close()
    '        filestream.Close()


    '        Dim offset As UInteger = Val("&H" & Mid(DataGridView1.Rows(e.RowIndex).Cells(0).Value, 3, 6))



    '        With DataGridView1.Rows(e.RowIndex).Cells(2)
    '            Select Case offsetNames(e.RowIndex).Split(",")(1)
    '                Case "BW::CUnit*" '4
    '                    WinAPI.Write(offset, CUInt(.Value))
    '                Case "BW::COrder*" '4
    '                    WinAPI.Write(offset, CUInt(.Value))
    '                Case "BW::CSprite*" '4
    '                    WinAPI.Write(offset, CUInt(.Value))
    '                Case "s32"
    '                    WinAPI.Write(offset, CInt(.Value))
    '                Case "u8"
    '                    WinAPI.Write(offset, CByte(.Value))
    '                Case "u16"
    '                    WinAPI.Write(offset, CUShort(.Value))
    '                Case "u32"
    '                    WinAPI.Write(offset, CUInt(.Value))
    '                Case "bool"
    '                    WinAPI.Write(offset, CByte(.Value))
    '                Case "UnitMovementState"
    '                    WinAPI.Write(offset, CByte(.Value))
    '                Case "void*"
    '                    WinAPI.Write(offset, CUInt(.Value))
    '            End Select
    '        End With
    '    End If
    'End Sub

    Private Sub DebugViewUnitForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True


        DataGridView1.Rows.Clear()

        Dim filestream As New FileStream(My.Application.Info.DirectoryPath & "\Data\CUnit.txt", FileMode.Open)
        Dim streamReader As New StreamReader(filestream)

        Dim offsetNames() As String = streamReader.ReadToEnd.Split(vbCrLf)


        streamReader.Close()
        filestream.Close()

        For i = 0 To offsetNames.Count - 1
            Dim name As String = offsetNames(i).Split(",")(2)
            DataGridView1.Rows.Add("", name, "")
        Next




        ComboBox1.Items.Clear()
        For i = 0 To 255
            ComboBox1.Items.Add("Player" & i + 1)
        Next

        With DebugForm.GameData
            ListBox1.Items.Clear()

            Dim memstr As New MemoryStream(.UnitNodeBuffer)
            Dim binreader As New BinaryReader(memstr)


            For i = 0 To .SelectUnits.Count - 1
                memstr.Position = .SelectUnits(i) * 336 + &H64

                Dim UnitID As UInt16 = binreader.ReadUInt16
                Dim strnum As Integer = DatEditDATA(DTYPE.units).ReadValue("Unit Map String", UnitID) - 1

                Dim listname As String = ""
                If strnum >= 0 Then
                    Try
                        listname = ProjectSet.CHKSTRING(strnum)
                    Catch ex As Exception

                    End Try
                Else
                    listname = CODE(DTYPE.units)(UnitID)
                End If
                Dim index As UInt16 = 1700 - .SelectUnits(i)

                If index = 1700 Then
                    index = 0
                End If
                listname = "[" & index.ToString.PadLeft(4, "0") & "]" & listname


                ListBox1.Items.Add(listname)
            Next


            ListBox1.SelectedIndex = 0


            binreader.Close()
            memstr.Close()
        End With
    End Sub

    Private Sub LoadData()
        If TabControl1.SelectedIndex = 0 Then
            With DebugForm.GameData
                Dim memstr As New MemoryStream(.UnitNodeBuffer)
                Dim binreader As New BinaryReader(memstr)

                Dim selectindex As UInt16 = Mid(ListBox1.Items(ListBox1.SelectedIndex), 2, 4)

                If selectindex = 0 Then
                    selectindex = 1700
                End If

                selectindex = 1700 - selectindex



                If CheckedListBox1.Focused = False Then
                    memstr.Position = selectindex * 336 + &HDC
                    Dim statusflag As UInt32 = binreader.ReadUInt32()

                    For i = 0 To CheckedListBox1.Items.Count - 1
                        CheckedListBox1.SetItemChecked(i, statusflag And 2 ^ i)
                    Next


                    CheckedListBox1.BackColor = ProgramSet.BACKCOLOR
                    CheckedListBox1.ForeColor = ProgramSet.FORECOLOR
                Else
                    CheckedListBox1.BackColor = ProgramSet.FORECOLOR
                    CheckedListBox1.ForeColor = ProgramSet.BACKCOLOR
                End If




                memstr.Position = selectindex * 336 + &H28
                TextBox5.Text = binreader.ReadUInt16 / 32
                TextBox6.Text = binreader.ReadUInt16 / 32
                TextBox5.BackColor = ProgramSet.BACKCOLOR
                TextBox5.ForeColor = ProgramSet.FORECOLOR
                TextBox6.BackColor = ProgramSet.BACKCOLOR
                TextBox6.ForeColor = ProgramSet.FORECOLOR

                '/*0x13C*
                '        struct
                '{             // Official names are "posSortXL, posSortXR, posSortYT, posSortYB"
                '  u32 Left, Right, Top, Bottom; // Ordering for unit boundries in unit finder for binary search
                '} finder;

                '/*0x108*/ rect      contourBounds;    // a rect that specifies the closest contour (collision) points

                memstr.Position = selectindex * 336 + &H64
                Dim UnitID As UInt16 = binreader.ReadUInt16

                If TabControl1.SelectedIndex = 0 Then
                    If ComboBox1.Focused = False Then
                        memstr.Position = selectindex * 336 + &H4C
                        ComboBox1.SelectedIndex = binreader.ReadByte()
                        ComboBox1.BackColor = ProgramSet.BACKCOLOR
                        ComboBox1.ForeColor = ProgramSet.FORECOLOR
                    Else
                        ComboBox1.BackColor = ProgramSet.FORECOLOR
                        ComboBox1.ForeColor = ProgramSet.BACKCOLOR
                    End If






                    memstr.Position = selectindex * 336 + &H8
                    Dim hp As Integer = binreader.ReadInt32()
                    Dim maxhp As Integer = WinAPI.ReadValue(&H662350 + 4 * UnitID, 4)

                    If TextBox1.Focused = False Then
                        TextBox1.BackColor = ProgramSet.BACKCOLOR
                        TextBox1.ForeColor = ProgramSet.FORECOLOR
                        TextBox1.Text = hp \ 256
                    Else
                        TextBox1.BackColor = ProgramSet.FORECOLOR
                        TextBox1.ForeColor = ProgramSet.BACKCOLOR
                    End If

                    If NumericUpDown1.Focused = False Then
                        Try
                            NumericUpDown1.Value = (hp / maxhp) * 100
                        Catch ex As Exception
                            NumericUpDown1.Value = 0
                        End Try
                        NumericUpDown1.BackColor = ProgramSet.BACKCOLOR
                        NumericUpDown1.ForeColor = ProgramSet.FORECOLOR
                    Else
                        NumericUpDown1.BackColor = ProgramSet.FORECOLOR
                        NumericUpDown1.ForeColor = ProgramSet.BACKCOLOR
                    End If

                    Try
                        NumericUpDown1.Maximum = (Integer.MaxValue / maxhp) * 100
                        NumericUpDown1.Minimum = (Integer.MinValue / maxhp) * 100
                    Catch ex As Exception
                        NumericUpDown1.Maximum = 0
                        NumericUpDown1.Minimum = 0
                    End Try





                    memstr.Position = selectindex * 336 + &H60
                    Dim Shield As Integer = binreader.ReadUInt32()
                    Dim maxShield As Integer = WinAPI.ReadValue(&H660E00 + 2 * UnitID, 2) * 256

                    If TextBox2.Focused = False Then
                        TextBox2.Text = Shield \ 256
                        TextBox2.BackColor = ProgramSet.BACKCOLOR
                        TextBox2.ForeColor = ProgramSet.FORECOLOR
                    Else
                        TextBox2.BackColor = ProgramSet.FORECOLOR
                        TextBox2.ForeColor = ProgramSet.BACKCOLOR
                    End If

                    If NumericUpDown2.Focused = False Then
                        Try
                            NumericUpDown2.Value = (Shield / maxShield) * 100
                        Catch ex As Exception
                            NumericUpDown2.Value = 0
                        End Try
                        NumericUpDown2.BackColor = ProgramSet.BACKCOLOR
                        NumericUpDown2.ForeColor = ProgramSet.FORECOLOR
                    Else
                        NumericUpDown2.BackColor = ProgramSet.FORECOLOR
                        NumericUpDown2.ForeColor = ProgramSet.BACKCOLOR
                    End If
                    Try
                        NumericUpDown2.Maximum = (Integer.MaxValue / maxShield) * 100
                        NumericUpDown2.Minimum = (Integer.MinValue / maxShield) * 100
                    Catch ex As Exception
                        NumericUpDown2.Maximum = 0
                        NumericUpDown2.Minimum = 0
                    End Try





                    memstr.Position = selectindex * 336 + &HA2

                    Dim Energy As UInt16 = binreader.ReadUInt16()
                    If TextBox3.Focused = False Then
                        TextBox3.Text = Energy \ 256
                        TextBox3.BackColor = ProgramSet.BACKCOLOR
                        TextBox3.ForeColor = ProgramSet.FORECOLOR
                    Else
                        TextBox3.BackColor = ProgramSet.FORECOLOR
                        TextBox3.ForeColor = ProgramSet.BACKCOLOR
                    End If

                    If NumericUpDown3.Focused = False Then
                        Dim maxEnergy As Byte

                        If (WinAPI.ReadValue(&H664080 + UnitID * 4, 4) And &H40) > 0 Then
                            maxEnergy = 250
                        Else
                            maxEnergy = 200
                        End If


                        Try
                            NumericUpDown3.Value = (Energy / (maxEnergy * 256)) * 100
                        Catch ex As Exception
                            NumericUpDown3.Value = 0
                        End Try
                        NumericUpDown3.BackColor = ProgramSet.BACKCOLOR
                        NumericUpDown3.ForeColor = ProgramSet.FORECOLOR
                    Else
                        NumericUpDown3.BackColor = ProgramSet.FORECOLOR
                        NumericUpDown3.ForeColor = ProgramSet.BACKCOLOR
                    End If




                    If TextBox4.Focused = False Then
                        memstr.Position = selectindex * 336 + &HD0
                        TextBox4.Text = binreader.ReadUInt16()
                        TextBox4.BackColor = ProgramSet.BACKCOLOR
                        TextBox4.ForeColor = ProgramSet.FORECOLOR
                    Else
                        TextBox4.BackColor = ProgramSet.FORECOLOR
                        TextBox4.ForeColor = ProgramSet.BACKCOLOR
                    End If
                    '플레이어ID,/* 0x04C*/ u8
                    '현재 체력량,/* 0x008*/ s32
                    '현재 쉴드량,/* 0x060*/ u32
                    '에너지,/* 0x0A2*/ u16
                    '/*0x0D0*/\\\\u16\\\\BUILDING:RESOURCE:resourceCount;
                Else

                End If
                binreader.Close()
                memstr.Close()
            End With
        Else
            With DebugForm.GameData
                Dim filestream As New FileStream(My.Application.Info.DirectoryPath & "\Data\CUnit.txt", FileMode.Open)
                Dim streamReader As New StreamReader(filestream)

                Dim offsetNames() As String = streamReader.ReadToEnd.Split(vbCrLf)

                streamReader.Close()
                filestream.Close()


                Dim memstr As New MemoryStream(.UnitNodeBuffer)
                Dim binreader As New BinaryReader(memstr)

                Dim selectindex As UInt16 = Mid(ListBox1.Items(ListBox1.SelectedIndex), 2, 4)

                If selectindex = 0 Then
                    selectindex = 1700
                End If

                selectindex = 1700 - selectindex


                '+ &H59CCA8

                For i = 0 To offsetNames.Count - 1
                    Dim offset As UInt16 = Val("&H" & offsetNames(i).Split(",")(0))
                    '/*0x000*/
                    memstr.Position = selectindex * 336 + offset
                    DataGridView1.Rows(i).Cells(0).Value = "0x" & Hex(memstr.Position + &H59CCA8)

                    Select Case offsetNames(i).Split(",")(1)
                        Case "BW::Path*" '4
                            DataGridView1.Rows(i).Cells(2).Value = "0x" & Hex(binreader.ReadUInt32)
                        Case "BW::CUnit*" '4
                            DataGridView1.Rows(i).Cells(2).Value = "0x" & Hex(binreader.ReadUInt32)
                        Case "BW::COrder*" '4
                            DataGridView1.Rows(i).Cells(2).Value = "0x" & Hex(binreader.ReadUInt32)
                        Case "BW::CSprite*" '4
                            DataGridView1.Rows(i).Cells(2).Value = "0x" & Hex(binreader.ReadUInt32)
                        Case "s32"
                            DataGridView1.Rows(i).Cells(2).Value = binreader.ReadInt32
                        Case "u8"
                            DataGridView1.Rows(i).Cells(2).Value = binreader.ReadByte
                        Case "u16"
                            DataGridView1.Rows(i).Cells(2).Value = binreader.ReadUInt16
                        Case "u32"
                            DataGridView1.Rows(i).Cells(2).Value = binreader.ReadUInt32
                        Case "bool"
                            DataGridView1.Rows(i).Cells(2).Value = binreader.ReadByte
                        Case "UnitMovementState"
                            DataGridView1.Rows(i).Cells(2).Value = binreader.ReadByte
                        Case "void*"
                            DataGridView1.Rows(i).Cells(2).Value = binreader.ReadUInt32
                    End Select
                    ' DataGridView1.Rows.Add("0x" & Hex(memstr.Position + &H59CCA8), name, offsetNames)
                Next




                binreader.Close()
                memstr.Close()
            End With
        End If
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        LoadData()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LoadData()
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            Dim value As Integer = TextBox1.Text
        Catch ex As Exception
            Exit Sub
        End Try

        If TextBox1.Focused Then
            Try
                Dim selectindex As UInt16 = Mid(ListBox1.Items(ListBox1.SelectedIndex), 2, 4)

                If selectindex = 0 Then
                    selectindex = 1700
                End If

                selectindex = 1700 - selectindex


                WinAPI.Write(CUInt(&H59CCA8 + selectindex * 336 + &H8), CInt(TextBox1.Text * 256))
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If NumericUpDown1.Focused Then
            Try






                Dim selectindex As UInt16 = Mid(ListBox1.Items(ListBox1.SelectedIndex), 2, 4)

                If selectindex = 0 Then
                    selectindex = 1700
                End If

                selectindex = 1700 - selectindex


                Dim memstr As New MemoryStream(DebugForm.GameData.UnitNodeBuffer)
                Dim binreader As New BinaryReader(memstr)

                memstr.Position = selectindex * 336 + &H64
                Dim UnitID As UInt16 = binreader.ReadUInt16

                binreader.Close()
                memstr.Close()


                Dim maxhp As Integer = WinAPI.ReadValue(&H662350 + 4 * UnitID, 4)




                WinAPI.Write(CUInt(&H59CCA8 + selectindex * 336 + &H8), CInt(NumericUpDown1.Value / 100 * maxhp))
            Catch ex As Exception

            End Try
        End If
    End Sub


    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Try
            Dim value As Integer = TextBox2.Text
        Catch ex As Exception
            Exit Sub
        End Try

        If TextBox2.Focused Then
            Try
                Dim selectindex As UInt16 = Mid(ListBox1.Items(ListBox1.SelectedIndex), 2, 4)

                If selectindex = 0 Then
                    selectindex = 1700
                End If

                selectindex = 1700 - selectindex


                WinAPI.Write(CUInt(&H59CCA8 + selectindex * 336 + &H60), CInt(TextBox2.Text * 256))
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        If NumericUpDown2.Focused Then
            Try
                Dim selectindex As UInt16 = Mid(ListBox1.Items(ListBox1.SelectedIndex), 2, 4)

                If selectindex = 0 Then
                    selectindex = 1700
                End If

                selectindex = 1700 - selectindex


                Dim memstr As New MemoryStream(DebugForm.GameData.UnitNodeBuffer)
                Dim binreader As New BinaryReader(memstr)

                memstr.Position = selectindex * 336 + &H64
                Dim UnitID As UInt16 = binreader.ReadUInt16

                binreader.Close()
                memstr.Close()


                Dim maxShield As Integer = WinAPI.ReadValue(&H660E00 + 2 * UnitID, 2) * 256




                WinAPI.Write(CUInt(&H59CCA8 + selectindex * 336 + &H60), CUInt(NumericUpDown2.Value / 100 * maxShield))
            Catch ex As Exception

            End Try
        End If
    End Sub


    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Try
            Dim value As Integer = TextBox3.Text
        Catch ex As Exception
            Exit Sub
        End Try

        If TextBox3.Focused Then
            Try
                Dim selectindex As UInt16 = Mid(ListBox1.Items(ListBox1.SelectedIndex), 2, 4)

                If selectindex = 0 Then
                    selectindex = 1700
                End If

                selectindex = 1700 - selectindex


                WinAPI.Write(CUInt(&H59CCA8 + selectindex * 336 + &HA2), CUShort(TextBox3.Text * 256))
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub NumericUpDown3_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown3.ValueChanged
        If NumericUpDown3.Focused Then
            Try
                Dim selectindex As UInt16 = Mid(ListBox1.Items(ListBox1.SelectedIndex), 2, 4)

                If selectindex = 0 Then
                    selectindex = 1700
                End If

                selectindex = 1700 - selectindex


                Dim memstr As New MemoryStream(DebugForm.GameData.UnitNodeBuffer)
                Dim binreader As New BinaryReader(memstr)

                memstr.Position = selectindex * 336 + &H64
                Dim UnitID As UInt16 = binreader.ReadUInt16

                binreader.Close()
                memstr.Close()

                Dim maxEnergy As Byte

                If (WinAPI.ReadValue(&H664080 + UnitID * 4, 4) And &H40) > 0 Then
                    maxEnergy = 250
                Else
                    maxEnergy = 200
                End If


                'NumericUpDown3.Value/ 100 * (maxEnergy * 256) = (Energy / ) 
                WinAPI.Write(CUInt(&H59CCA8 + selectindex * 336 + &HA2), CUShort(NumericUpDown3.Value / 100 * (maxEnergy * 256)))
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Try
            Dim value As Integer = TextBox4.Text
        Catch ex As Exception
            Exit Sub
        End Try

        If TextBox4.Focused Then
            Try
                Dim selectindex As UInt16 = Mid(ListBox1.Items(ListBox1.SelectedIndex), 2, 4)

                If selectindex = 0 Then
                    selectindex = 1700
                End If

                selectindex = 1700 - selectindex


                WinAPI.Write(CUInt(&H59CCA8 + selectindex * 336 + &HD0), CUShort(TextBox4.Text))
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Focused Then
            Try
                Dim selectindex As UInt16 = Mid(ListBox1.Items(ListBox1.SelectedIndex), 2, 4)

                If selectindex = 0 Then
                    selectindex = 1700
                End If

                selectindex = 1700 - selectindex

                WinAPI.Write(CUInt(&H59CCA8 + selectindex * 336 + &H4C), CByte(ComboBox1.SelectedIndex))
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.ItemCheck, CheckedListBox1.SelectedIndexChanged
        If CheckedListBox1.Focused Then
            Try
                Dim selectindex As UInt16 = Mid(ListBox1.Items(ListBox1.SelectedIndex), 2, 4)

                If selectindex = 0 Then
                    selectindex = 1700
                End If

                selectindex = 1700 - selectindex

                Dim statusflag As UInt32 = 0

                For i = 0 To CheckedListBox1.Items.Count - 1
                    If CheckedListBox1.GetItemChecked(i) Then
                        statusflag += 2 ^ i
                    End If
                Next

                WinAPI.Write(CUInt(&H59CCA8 + selectindex * 336 + &HDC), statusflag)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        LoadData()
    End Sub
End Class