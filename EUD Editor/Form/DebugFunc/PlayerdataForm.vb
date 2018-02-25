Public Class PlayerdataForm
    Dim PlayerNum As Byte
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Select Case WinAPI.ReadValue(&H57EEE0 + 36 * PlayerNum + 8, 1)
            Case 0
                TextBox1.Text = "Inactive"
            Case 1
                TextBox1.Text = "Computer"
            Case 2
                TextBox1.Text = "Human"
            Case 3
                TextBox1.Text = "Rescuable"
            Case 7
                TextBox1.Text = "Neutral"
            Case Else
                TextBox1.Text = "Null"
        End Select


        '57F0F0 미네랄
        If NumericUpDown1.Focused Then
            NumericUpDown1.ForeColor = ProgramSet.BACKCOLOR
            NumericUpDown1.BackColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown1.ForeColor = ProgramSet.FORECOLOR
            NumericUpDown1.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown1.Value = WinAPI.ReadValue(&H57F0F0 + PlayerNum * 4, 4)
        End If

        If NumericUpDown2.Focused Then
            NumericUpDown2.ForeColor = ProgramSet.BACKCOLOR
            NumericUpDown2.BackColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown2.ForeColor = ProgramSet.FORECOLOR
            NumericUpDown2.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown2.Value = WinAPI.ReadValue(&H57F120 + PlayerNum * 4, 4)
        End If

        If CheckedListBox3.Focused Then
            CheckedListBox3.ForeColor = ProgramSet.BACKCOLOR
            CheckedListBox3.BackColor = ProgramSet.FORECOLOR
        Else
            CheckedListBox3.ForeColor = ProgramSet.FORECOLOR
            CheckedListBox3.BackColor = ProgramSet.BACKCOLOR

            Dim buffer() As Byte = WinAPI.ReadValue(&H58D634 + PlayerNum * 12, 12)
            For i = 0 To 11
                CheckedListBox3.SetItemChecked(i, buffer(i))
            Next
        End If


        If CheckedListBox1.Focused Then
            CheckedListBox1.ForeColor = ProgramSet.BACKCOLOR
            CheckedListBox1.BackColor = ProgramSet.FORECOLOR
        Else
            CheckedListBox1.ForeColor = ProgramSet.FORECOLOR
            CheckedListBox1.BackColor = ProgramSet.BACKCOLOR

            Dim buffer As UInteger = WinAPI.ReadValue(&H57F1EC + PlayerNum * 4, 4)
            For i = 0 To 7
                CheckedListBox1.SetItemChecked(i, buffer And (2 ^ i))
            Next
        End If

        '57F1EC	1.16.1	Win	Shared Vision
        '58D634	1.16.1	Win	Player Alliances	12	12
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        PlayerNum = TabControl1.SelectedIndex
    End Sub

    Private Sub PlayerdataForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PlayerNum = 0


        TextBox1.BackColor = Color.GhostWhite
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If NumericUpDown1.Focused Then
            WinAPI.Write(CUInt(&H57F0F0 + PlayerNum * 4), CUInt(NumericUpDown1.Value))
        End If
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        If NumericUpDown2.Focused Then
            WinAPI.Write(CUInt(&H57F120 + PlayerNum * 4), CUInt(NumericUpDown2.Value))
        End If
    End Sub

    Private Sub CheckedListBox3_SelectedIndexChanged(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox3.ItemCheck
        If CheckedListBox3.Focused Then
            If e.NewValue = CheckState.Checked Then

                WinAPI.Write(CUInt(&H58D634 + PlayerNum * 12 + e.Index), CByte(2))
            Else
                WinAPI.Write(CUInt(&H58D634 + PlayerNum * 12 + e.Index), CByte(0))
            End If
        End If

    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        If CheckedListBox1.Focused Then
            Dim buffer As UInteger = WinAPI.ReadValue(&H57F1EC + PlayerNum * 4, 4)

            buffer = buffer - (buffer And (2 ^ e.Index))

            If e.NewValue = CheckState.Checked Then
                buffer += 2 ^ e.Index
            End If

            WinAPI.Write(CUInt(&H57F1EC + PlayerNum * 4), buffer)
        End If
    End Sub

    '0057EEE0	1.16.1	Win	Active Player Structures 	36	12
    '+0x00 = HumanID (4 bytes) 
    '+0x04 = StormID (4 bytes) 
    '+0x08 = Type (1 byte; 0 = inactive, 1 = computer, 2 = human, 3 = rescuable, 7 = neutral) 
    '+0x09 = Race (1 byte; 0 = zerg, 1 = terran, 2 = protoss) 
    '+0x0A = Team (1 byte) 
    '+0x0B = Name (25 bytes)


    '시야
    '동맹
    '이름
    '미네랄
    '가스
End Class