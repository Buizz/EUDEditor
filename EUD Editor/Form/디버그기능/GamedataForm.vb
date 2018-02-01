Imports System.IO

Public Class GamedataForm
    Public TriggerExecTimer As Boolean
    Private Sub ColorReset()
        TextBox1.BackColor = Color.GhostWhite

        TextBox2.BackColor = Color.GhostWhite

        TextBox3.BackColor = Color.GhostWhite

        NumericUpDown1.BackColor = Color.GhostWhite

        NumericUpDown2.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown2.ForeColor = ProgramSet.FORECOLOR

        NumericUpDown3.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown3.ForeColor = ProgramSet.FORECOLOR

        NumericUpDown4.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown4.ForeColor = ProgramSet.FORECOLOR

        CheckBox1.BackColor = ProgramSet.BACKCOLOR
        CheckBox1.ForeColor = ProgramSet.FORECOLOR

        CheckBox2.BackColor = ProgramSet.BACKCOLOR
        CheckBox2.ForeColor = ProgramSet.FORECOLOR

        CheckBox3.BackColor = ProgramSet.BACKCOLOR
        CheckBox3.ForeColor = ProgramSet.FORECOLOR

        CheckBox4.BackColor = ProgramSet.BACKCOLOR
        CheckBox4.ForeColor = ProgramSet.FORECOLOR

        CheckBox5.BackColor = ProgramSet.BACKCOLOR
        CheckBox5.ForeColor = ProgramSet.FORECOLOR

        ComboBox1.BackColor = ProgramSet.BACKCOLOR
        ComboBox1.ForeColor = ProgramSet.FORECOLOR

        ComboBox2.BackColor = ProgramSet.BACKCOLOR
        ComboBox2.ForeColor = ProgramSet.FORECOLOR

    End Sub
    '0057FD3C	1.16.1	Win	Map File Name	260	1	The current map's file name.
    '0057FE40	1.16.1	Win	Map Title	32	1	The current map's title.
    '006D0F48	1.16.1	Win	Game Name	24	1

    '0058D6F4	1.16.1	Win	Countdown Timer	4	1
    '0058F04C	1.16.1	Win	Time Pause State	4	1

    '0057F23C	1.16.1	Win	Elapsed Time	4	1


    '006509A0	1.16.1	Win	Trigger Execution Counter	4	1
    '006556E0	1.16.1	Win	Accept Commands	4	1


    '0057F0B4	1.16.1	Win	Multiplayer Mode

    '51CE88 1,2,3,4,5
    '5124F0 42, 36, 29, 21, 12, 1



    '0x6509C4 0 = 일반 1 = 퍼즈
    Private Sub GamedataForm_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        WinAPI.Write(&H6509A0, 0)
    End Sub


    Private Sub GamedataForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ColorReset()

        CheckBox4.Checked = True

        Dim buffer() As Byte = WinAPI.ReadValue(&H57FD3C, 260)
        Dim memoryStream As New MemoryStream(buffer)

        Dim strstream As StreamReader = New StreamReader(memoryStream, System.Text.Encoding.GetEncoding("ks_c_5601-1987"))


        TextBox3.Text = strstream.ReadToEnd()
        strstream.Close()

        buffer = WinAPI.ReadValue(&H57FE40, 32)
        memoryStream = New MemoryStream(buffer)

        strstream = New StreamReader(memoryStream, System.Text.Encoding.GetEncoding("ks_c_5601-1987"))


        TextBox2.Text = strstream.ReadToEnd()
        strstream.Close()


        buffer = WinAPI.ReadValue(&H6D0F48, 24)
        memoryStream = New MemoryStream(buffer)

        strstream = New StreamReader(memoryStream, System.Text.Encoding.GetEncoding("ks_c_5601-1987"))


        TextBox1.Text = strstream.ReadToEnd()
        strstream.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If TriggerExecTimer = False Then
            WinAPI.Write(&H6509A0, 100)
        End If


        NumericUpDown1.Value = WinAPI.ReadValue(&H57F23C, 4)

        If NumericUpDown4.Focused = False And ComboBox1.Focused = False Then
            Try
                ComboBox1.SelectedIndex = WinAPI.ReadValue(&H51CE88, 4) - 1
            Catch ex As Exception
                ComboBox1.SelectedIndex = -1
            End Try
            ComboBox1.BackColor = ProgramSet.BACKCOLOR
            ComboBox1.ForeColor = ProgramSet.FORECOLOR

            NumericUpDown4.Value = WinAPI.ReadValue(&H51CE88, 4)
            NumericUpDown4.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown4.ForeColor = ProgramSet.FORECOLOR
        Else
            ComboBox1.BackColor = ProgramSet.FORECOLOR
            ComboBox1.ForeColor = ProgramSet.BACKCOLOR

            NumericUpDown4.BackColor = ProgramSet.FORECOLOR
            NumericUpDown4.ForeColor = ProgramSet.BACKCOLOR
        End If


        '5124F0 42, 36, 29, 21, 12, 1
        If ComboBox2.Focused = False And NumericUpDown3.Focused = False Then
            Dim value As UInteger = WinAPI.ReadValue(&H5124F0, 4)
            Select Case value
                Case 42
                    ComboBox2.SelectedIndex = 0
                Case 36
                    ComboBox2.SelectedIndex = 1
                Case 29
                    ComboBox2.SelectedIndex = 2
                Case 21
                    ComboBox2.SelectedIndex = 3
                Case 12
                    ComboBox2.SelectedIndex = 4
                Case 1
                    ComboBox2.SelectedIndex = 5
                Case Else
                    ComboBox2.SelectedIndex = -1
            End Select
            ComboBox2.BackColor = ProgramSet.BACKCOLOR
            ComboBox2.ForeColor = ProgramSet.FORECOLOR

            NumericUpDown3.Value = WinAPI.ReadValue(&H5124F0, 4)
            NumericUpDown3.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown3.ForeColor = ProgramSet.FORECOLOR
        Else
            ComboBox2.BackColor = ProgramSet.FORECOLOR
            ComboBox2.ForeColor = ProgramSet.BACKCOLOR

            NumericUpDown3.BackColor = ProgramSet.FORECOLOR
            NumericUpDown3.ForeColor = ProgramSet.BACKCOLOR
        End If








        If CheckBox1.Focused = False Then
            CheckBox1.Checked = WinAPI.ReadValue(&H57F0B4, 1)
            CheckBox1.BackColor = ProgramSet.BACKCOLOR
            CheckBox1.ForeColor = ProgramSet.FORECOLOR
        Else
            CheckBox1.BackColor = ProgramSet.FORECOLOR
            CheckBox1.ForeColor = ProgramSet.BACKCOLOR
        End If

        If CheckBox2.Focused = False Then
            CheckBox2.Checked = WinAPI.ReadValue(&H6509C4, 1)
            CheckBox2.BackColor = ProgramSet.BACKCOLOR
            CheckBox2.ForeColor = ProgramSet.FORECOLOR
        Else
            CheckBox2.BackColor = ProgramSet.FORECOLOR
            CheckBox2.ForeColor = ProgramSet.BACKCOLOR
        End If

        If CheckBox3.Focused = False Then
            CheckBox3.Checked = WinAPI.ReadValue(&H6556E0, 4)
            CheckBox3.BackColor = ProgramSet.BACKCOLOR
            CheckBox3.ForeColor = ProgramSet.FORECOLOR
        Else
            CheckBox3.BackColor = ProgramSet.FORECOLOR
            CheckBox3.ForeColor = ProgramSet.BACKCOLOR
        End If


        If NumericUpDown2.Focused = False Then
            NumericUpDown2.Value = WinAPI.ReadValue(&H58D6F4, 4)
            NumericUpDown2.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown2.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown2.BackColor = ProgramSet.FORECOLOR
            NumericUpDown2.ForeColor = ProgramSet.BACKCOLOR
        End If

        If CheckBox5.Focused = False Then
            CheckBox5.Checked = WinAPI.ReadValue(&H58F04C, 4)
            CheckBox5.BackColor = ProgramSet.BACKCOLOR
            CheckBox5.ForeColor = ProgramSet.FORECOLOR
        Else
            CheckBox5.BackColor = ProgramSet.FORECOLOR
            CheckBox5.ForeColor = ProgramSet.BACKCOLOR
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Focused Then
            WinAPI.Write(&H57F0B4, CByte(CheckBox1.Checked))
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Focused Then
            WinAPI.Write(&H6509C4, CByte(CheckBox2.Checked))
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Focused Then
            WinAPI.Write(CUInt(&H6556E0), CUInt(CheckBox3.Checked))
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Focused Then
            WinAPI.Write(CUInt(&H58F04C), CUInt(CheckBox5.Checked))
        End If
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        If NumericUpDown2.Focused Then
            WinAPI.Write(CUInt(&H58D6F4), CUInt(NumericUpDown2.Value))

        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        TriggerExecTimer = CheckBox4.Checked
        If TriggerExecTimer Then
            WinAPI.Write(&H6509A0, 0)
        End If
    End Sub


    '51CE88 1,2,3,4,5
    '5124F0 42, 36, 29, 21, 12, 1
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.Focused Then

            Select Case ComboBox2.SelectedIndex
                Case 0
                    WinAPI.Write(CUInt(&H5124F0), CUInt(42))
                    NumericUpDown3.Value = 42
                Case 1
                    WinAPI.Write(CUInt(&H5124F0), CUInt(36))
                    NumericUpDown3.Value = 36
                Case 2
                    WinAPI.Write(CUInt(&H5124F0), CUInt(29))
                    NumericUpDown3.Value = 29
                Case 3
                    WinAPI.Write(CUInt(&H5124F0), CUInt(21))
                    NumericUpDown3.Value = 21
                Case 4
                    WinAPI.Write(CUInt(&H5124F0), CUInt(12))
                    NumericUpDown3.Value = 12
                Case 5
                    WinAPI.Write(CUInt(&H5124F0), CUInt(1))
                    NumericUpDown3.Value = 1

            End Select
        End If
    End Sub

    Private Sub NumericUpDown3_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown3.ValueChanged
        If NumericUpDown3.Focused Then
            WinAPI.Write(CUInt(&H5124F0), CUInt(NumericUpDown3.Value))

            Select Case NumericUpDown3.Value
                Case 42
                    ComboBox2.SelectedIndex = 0
                Case 36
                    ComboBox2.SelectedIndex = 1
                Case 29
                    ComboBox2.SelectedIndex = 2
                Case 21
                    ComboBox2.SelectedIndex = 3
                Case 12
                    ComboBox2.SelectedIndex = 4
                Case 1
                    ComboBox2.SelectedIndex = 5
                Case Else
                    ComboBox2.SelectedIndex = -1
            End Select
        End If
    End Sub



    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Focused Then
            WinAPI.Write(CUInt(&H51CE88), CUInt(ComboBox1.SelectedIndex + 1))

            NumericUpDown4.Value = ComboBox1.SelectedIndex + 1
        End If
    End Sub

    Private Sub NumericUpDown4_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown4.ValueChanged
        If NumericUpDown4.Focused Then
            WinAPI.Write(CUInt(&H51CE88), CUInt(NumericUpDown4.Value))

            ComboBox1.SelectedIndex = NumericUpDown4.Value - 1
        End If
    End Sub
End Class