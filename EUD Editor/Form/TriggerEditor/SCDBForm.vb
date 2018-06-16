Public Class SCDBForm
    Dim units As New List(Of String)
    Dim locs As New List(Of String)

    Private Sub SCDBForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        units.Clear()
        For i = 0 To CODE(0).Count - 1
            If DatEditDATA(DTYPE.units).ReadValue("Unit Map String", i) = 0 Then
                units.Add(CODE(0)(i))
            Else
                Try
                    units.Add(ProjectSet.CHKSTRING(-1 + ProjectSet.CHKUNITNAME(i)) & "(" & CODE(0)(i) & ")")

                Catch ex As Exception
                    units.Add(CODE(0)(i))
                End Try
            End If
        Next
        locs.Clear()
        locs.Add("None")
        For i = 0 To 254
            If ProjectSet.CHKLOCATIONNAME(i) <> 0 Then
                locs.Add(ProjectSet.CHKSTRING(ProjectSet.CHKLOCATIONNAME(i) - 1))
            Else
                locs.Add("Location " & i)
            End If
        Next



        ListBox3.Items.Clear()
        For i = 0 To SCDBDeath.Count - 1
            ListBox3.Items.Add(units(SCDBDeath(i)))
        Next

        ComboBox1.Items.Clear()
        ComboBox1.ResetText()
        ComboBox1.Items.AddRange(units.ToArray)
        ComboBox1.SelectedIndex = 0

        'ListBox3.Items.Clear()
        'For i = 0 To SCDBLoc.Count - 1
        '    ListBox3.Items.Add(locs(SCDBLoc(i)))
        'Next
        isopenBtnDeath = True
    End Sub

    Private Sub BtnSetting_Click(sender As Object, e As EventArgs) Handles BtnSetting.Click
        If SCDBLoginForm.ShowDialog() = DialogResult.No Then
            ProjectSet.SCDBUse = False
            Me.Close()
        End If
    End Sub



    Dim isopenBtnDeath As Boolean = False
    Private Sub BtnDeath_Click(sender As Object, e As EventArgs) Handles BtnDeath.Click
        ComboBox1.Items.Clear()
        ComboBox1.ResetText()
        ComboBox1.Items.AddRange(units.ToArray)
        ComboBox1.SelectedIndex = 0


        ListBox3.Items.Clear()
        For i = 0 To SCDBDeath.Count - 1
            ListBox3.Items.Add(units(SCDBDeath(i)))
        Next
        TableLayoutPanel1.RowStyles(1).Height = 33

        isopenBtnLoc = False
        isopenBtnDeath = True
        BtnDelete.Enabled = False
    End Sub

    Dim isopenBtnLoc As Boolean = False
    Private Sub BtnLoc_Click(sender As Object, e As EventArgs) Handles BtnLoc.Click
        ComboBox1.Items.Clear()
        ComboBox1.ResetText()
        ComboBox1.Items.AddRange(locs.ToArray)
        ComboBox1.SelectedIndex = 0
        EasyCompletionComboBox1.Items.Clear()
        EasyCompletionComboBox1.ResetText()
        EasyCompletionComboBox1.Items.AddRange(locs.ToArray)
        EasyCompletionComboBox1.SelectedIndex = 0


        ListBox3.Items.Clear()
        For i = 0 To SCDBLoc.Count - 1
            ListBox3.Items.Add("Save : " & locs(SCDBLoc(i)) & "   Load : " & locs(SCDBLocLoad(i)))
        Next
        TableLayoutPanel1.RowStyles(1).Height = 66


        isopenBtnDeath = False
        isopenBtnLoc = True
        BtnDelete.Enabled = False
    End Sub


    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Dim lastsel As Integer = ListBox3.SelectedIndex
        If isopenBtnDeath Then
            SCDBDeath.RemoveAt(ListBox3.SelectedIndex)
        ElseIf isopenBtnLoc Then
            SCDBLoc.RemoveAt(ListBox3.SelectedIndex)
            SCDBLocLoad.RemoveAt(ListBox3.SelectedIndex)
        End If
        ListBox3.Items.RemoveAt(ListBox3.SelectedIndex)

        Try
            ListBox3.SelectedIndex = lastsel
        Catch ex As Exception
            ListBox3.SelectedIndex = ListBox3.Items.Count - 1
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If isopenBtnDeath Then
            SCDBDeath.Add(ComboBox1.SelectedIndex)
            ListBox3.Items.Add(units(ComboBox1.SelectedIndex))
        ElseIf isopenBtnLoc Then
            SCDBLoc.Add(ComboBox1.SelectedIndex)
            SCDBLocLoad.Add(EasyCompletionComboBox1.SelectedIndex)
            ListBox3.Items.Add("Save : " & locs(ComboBox1.SelectedIndex) & "   Load : " & locs(EasyCompletionComboBox1.SelectedIndex))
        End If
        ListBox3.SelectedIndex = ListBox3.Items.Count - 1

    End Sub

    Private Sub ListBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox3.SelectedIndexChanged
        If ListBox3.SelectedIndex <> -1 Then
            BtnDelete.Enabled = True
        Else
            BtnDelete.Enabled = False
        End If
    End Sub
End Class