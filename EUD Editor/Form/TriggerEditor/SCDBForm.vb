Imports System.IO

Public Class SCDBForm
    Public units As New List(Of String)
    Public locs As New List(Of String)

    Private Sub SCDBForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        TextBox2.Text = SCDBMaker
        TextBox3.Text = SCDBMapName

        NumericUpDown1.Value = SCDBDataSize

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


        TableLayoutPanel1.RowStyles(1).Height = 0
        TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Absolute
        TableLayoutPanel1.RowStyles(2).Height = 0
        TableLayoutPanel1.RowStyles(3).Height = 72

        isopenBtnLoc = False
        isopenBtnVarb = False
        isopenBtnDeath = False
        BtnDelete.Enabled = False



        'ListBox3.Items.Clear()
        'For i = 0 To SCDBDeath.Count - 1
        '    ListBox3.Items.Add(units(SCDBDeath(i)))
        'Next

        'ComboBox1.Items.Clear()
        'ComboBox1.ResetText()
        'ComboBox1.Items.AddRange(units.ToArray)
        'ComboBox1.SelectedIndex = 0

        'TableLayoutPanel1.RowStyles(1).Height = 33
        ''ListBox3.Items.Clear()
        ''For i = 0 To SCDBLoc.Count - 1
        ''    ListBox3.Items.Add(locs(SCDBLoc(i)))
        ''Next
        'isopenBtnDeath = True
        'isopenBtnLoc = False
        'isopenBtnVarb = False
    End Sub

    Private Sub BtnSetting_Click(sender As Object, e As EventArgs) Handles BtnSetting.Click

        TableLayoutPanel1.RowStyles(1).Height = 0
        TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Absolute
        TableLayoutPanel1.RowStyles(2).Height = 0
        TableLayoutPanel1.RowStyles(3).Height = 72

        isopenBtnLoc = False
        isopenBtnVarb = False
        isopenBtnDeath = False
        BtnDelete.Enabled = False
        'SCDBLoginForm.ShowDialog()
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
        TableLayoutPanel1.RowStyles(2).SizeType = SizeType.AutoSize
        TableLayoutPanel1.RowStyles(2).Height = 100
        TableLayoutPanel1.RowStyles(3).Height = 0

        isopenBtnLoc = False
        isopenBtnVarb = False
        isopenBtnDeath = True
        BtnDelete.Enabled = False
    End Sub

    Dim isopenBtnVarb As Boolean = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ComboBox1.Items.Clear()
        ComboBox1.ResetText()

        For i = 0 To GlobalVar.GetElementsCount - 1
            If GlobalVar.GetElementList(i).act.Name = "CreateVariable" Then
                ComboBox1.Items.Add(GlobalVar.GetElementList(i).Values(0))
            Else
                ComboBox1.Items.Add(GlobalVar.GetElementList(i).Values(0) & "[getcurpl()]")
            End If
        Next
        If ComboBox1.Items.Count <> 0 Then
            ComboBox1.SelectedIndex = 0
            TextBox1.Text = ComboBox1.Text
        Else
            TextBox1.Text = "Nothing"
        End If
        EasyCompletionComboBox1.Hide()


        ListBox3.Items.Clear()
        For i = 0 To SCDBVariable.Count - 1
            ListBox3.Items.Add(SCDBVariable(i))
        Next
        TableLayoutPanel1.RowStyles(1).Height = 66
        TableLayoutPanel3.RowStyles(1).Height = 0
        TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Percent
        TableLayoutPanel1.RowStyles(2).Height = 100
        TableLayoutPanel1.RowStyles(3).Height = 0

        isopenBtnLoc = False
        isopenBtnVarb = True
        isopenBtnDeath = False
        BtnDelete.Enabled = False
    End Sub

    Dim isopenBtnLoc As Boolean = False
    Private Sub BtnLoc_Click(sender As Object, e As EventArgs) Handles BtnLoc.Click
        ComboBox1.Items.Clear()
        ComboBox1.ResetText()
        ComboBox1.Items.AddRange(locs.ToArray)
        ComboBox1.SelectedIndex = 0
        EasyCompletionComboBox1.Show()
        EasyCompletionComboBox1.Items.Clear()
        EasyCompletionComboBox1.ResetText()
        EasyCompletionComboBox1.Items.AddRange(locs.ToArray)
        EasyCompletionComboBox1.SelectedIndex = 0


        ListBox3.Items.Clear()
        For i = 0 To SCDBLoc.Count - 1
            ListBox3.Items.Add("Save : " & locs(SCDBLoc(i)) & "   Load : " & locs(SCDBLocLoad(i)))
        Next
        TableLayoutPanel1.RowStyles(1).Height = 66
        TableLayoutPanel3.RowStyles(1).Height = 33
        TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Percent
        TableLayoutPanel1.RowStyles(2).Height = 100
        TableLayoutPanel1.RowStyles(3).Height = 0


        isopenBtnDeath = False
        isopenBtnLoc = True
        isopenBtnVarb = False
        BtnDelete.Enabled = False
    End Sub


    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Dim lastsel As Integer = ListBox3.SelectedIndex
        If isopenBtnDeath Then
            SCDBDeath.RemoveAt(ListBox3.SelectedIndex)
        ElseIf isopenBtnLoc Then
            SCDBLoc.RemoveAt(ListBox3.SelectedIndex)
            SCDBLocLoad.RemoveAt(ListBox3.SelectedIndex)
        ElseIf isopenBtnVarb Then
            SCDBVariable.RemoveAt(ListBox3.SelectedIndex)
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
        ElseIf isopenBtnVarb Then
            SCDBVariable.Add(TextBox1.Text)
            ListBox3.Items.Add(TextBox1.Text)
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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If isopenBtnVarb Then
            TextBox1.Text = ComboBox1.Text
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        SCDBMaker = TextBox2.Text
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        SCDBMapName = TextBox3.Text
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged, NumericUpDown1.TextChanged

        SCDBDataSize = NumericUpDown1.Value
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SCDBDataCreater.ShowDialog()
    End Sub
End Class