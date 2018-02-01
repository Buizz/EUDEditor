Public Class ForEditing
    Public _Element As Element

    'Counting
    'Custom
    'Player
    'AllUnit

    Private Sub Refreshcont()
        isloading = True
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        NumericUpDown1.Enabled = False
        CheckedListBox1.Enabled = False
        ComboBox1.Enabled = False
        Select Case _Element.Values(0)
            Case "Counting"
                TextBox2.Enabled = True
                NumericUpDown1.Enabled = True

                TextBox2.Text = _Element.Values(1)
                NumericUpDown1.Value = _Element.Values(2)
                RadioButton1.Checked = True
            Case "Custom"
                TextBox1.Enabled = True

                TextBox1.Text = _Element.Values(1)
                RadioButton2.Checked = True
            Case "AllUnit"
                RadioButton4.Checked = True

                ComboBox1.Enabled = True

                ComboBox1.SelectedIndex = _Element.Values(1)
            Case "PlayerLoop"
                CheckedListBox1.Enabled = True
                RadioButton3.Checked = True

                Dim temparray() As String = _Element.Values(1).Split(",")
                For i = 0 To 11
                    CheckedListBox1.SetItemChecked(i, temparray(i))
                Next
        End Select
        isloading = False
    End Sub
    Dim isloading As Boolean = False
    Private Sub ForEditing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isloading = True
        CheckedListBox1.Items.Clear()
        For i = 1 To 8
            CheckedListBox1.Items.Add("Player " & i)
        Next
        For i = 0 To 3
            CheckedListBox1.Items.Add(ProjectSet.CHKSTRING(ProjectSet.CHKFORCEDATA(i)(0) - 1) & "(Force " & i + 1 & ")")
        Next

        isloading = False

        Refreshcont()
    End Sub



    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If isloading = False Then
            _Element.Values(1) = TextBox2.Text
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If isloading = False Then
            _Element.Values(2) = NumericUpDown1.Value
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If isloading = False Then
            _Element.Values(1) = TextBox1.Text
        End If
    End Sub


    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If isloading = False Then
            _Element.Values(0) = "Custom"
            _Element.Values(1) = ""
            Refreshcont()
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If isloading = False Then
            _Element.Values(0) = "Counting"
            _Element.Values(1) = "i"
            _Element.Values(2) = "10"
            Refreshcont()
        End If
    End Sub


    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If isloading = False Then
            _Element.Values(0) = "AllUnit"
            _Element.Values(1) = 0
            Refreshcont()
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If isloading = False Then
            _Element.Values(0) = "PlayerLoop"
            _Element.Values(1) = "True,True,True,True,True,True,True,True,False,False,False,False"
            Refreshcont()
        End If
    End Sub


    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        If isloading = False Then

            _Element.Values(1) = CheckedListBox1.GetItemCheckState(0)
            For i = 1 To 11
                _Element.Values(1) = _Element.Values(1) & "," & CheckedListBox1.GetItemCheckState(i)
            Next
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If isloading = False Then
            _Element.Values(1) = ComboBox1.SelectedIndex
        End If
    End Sub
End Class