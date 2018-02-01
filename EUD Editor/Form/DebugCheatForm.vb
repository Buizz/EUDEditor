Public Class DebugCheatForm
    Dim cheatvalue() As UInteger = {&H20000, &H2000, &H800, &H4, &H2}
    Dim cheatFlag As UInteger
    Private Sub DebugCheatForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Location = MousePosition
        Focus()

        cheatFlag = WinAPI.ReadValue(&H6D5A6C, 4)


        For i = 0 To 4
            If (cheatFlag And cheatvalue(i)) = 0 Then
                CheckedListBox1.SetItemChecked(i, False)
            Else
                CheckedListBox1.SetItemChecked(i, True)
            End If
        Next



        '00020000 Food For Thought  인구치트

        '00002000 Modify The Phase Variance  모든 요구사항 무시

        '00000800 The Gathering 마나무한

        '00000004 Power Overwhelming 무적

        '00000002 Operation CWAL 생산 속도
    End Sub

    Private Sub DebugCheatForm_LostFocus(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        Close()
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        cheatFlag = WinAPI.ReadValue(&H6D5A6C, 4)
        If e.NewValue Then
            WinAPI.Write(CUInt(&H6D5A6C), CUInt(cheatFlag Or cheatvalue(e.Index)))
        Else
            WinAPI.Write(CUInt(&H6D5A6C), CUInt((cheatFlag Or cheatvalue(e.Index)) - cheatvalue(e.Index)))
        End If
    End Sub


End Class