Public Class SwitchViewerForm
    Private Sub SwitchViewerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckedListBox1.BackColor = ProgramSet.BACKCOLOR
        CheckedListBox1.ForeColor = ProgramSet.FORECOLOR


        '58DC40
        Dim buffer() As Byte = WinAPI.ReadValue(&H58DC40, 32)



        CheckedListBox1.Items.Clear()
        Dim switchCount As UInt16 = 0
        For i = 0 To 31
            For k = 0 To 7
                Dim isTrue As Boolean = buffer(i) And 2 ^ k

                If ProjectSet.CHKSWITCHNAME.Count <> 0 Then
                    If ProjectSet.CHKSWITCHNAME(switchCount) = 0 Then
                        CheckedListBox1.Items.Add("Switch " & switchCount + 1, isTrue)
                    Else
                        CheckedListBox1.Items.Add(ProjectSet.CHKSTRING(ProjectSet.CHKSWITCHNAME(switchCount) - 1), isTrue)

                    End If
                Else
                    CheckedListBox1.Items.Add("Switch " & switchCount + 1, isTrue)
                End If


                switchCount += 1
            Next
        Next
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If CheckedListBox1.Focused = False Then
            Dim buffer() As Byte = WinAPI.ReadValue(&H58DC40, 32)


            Dim switchCount As UInt16 = 0
            For i = 0 To 31
                For k = 0 To 7
                    Dim isTrue As Boolean = buffer(i) And 2 ^ k
                    If CheckedListBox1.GetItemChecked(switchCount) <> isTrue Then
                        CheckedListBox1.SetItemChecked(switchCount, isTrue)
                    End If

                    switchCount += 1
                Next
            Next
        End If
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        If CheckedListBox1.Focused Then
            Dim originValue As Byte = WinAPI.ReadValue(&H58DC40 + e.Index \ 8, 1)


            originValue = originValue - (originValue And (2 ^ (e.Index Mod 8)))
            If e.NewValue Then
                originValue += 2 ^ (e.Index Mod 8)
            End If



            WinAPI.Write(CUInt(&H58DC40 + e.Index \ 8), originValue)

            TextBox1.Focus()
        End If
    End Sub
End Class