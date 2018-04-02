Public Class AddTectDialog
    Public returnstring As String
    Public _varele As Element


    Private Sub AddTectDialog_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        returnstring = ""
        Select Case ListBox1.SelectedIndex
            Case 0 To 4
                returnstring = "{P" & ListBox3.SelectedIndex + 1 & "_"
        End Select

        Select Case ListBox1.SelectedIndex
            Case 0
                returnstring = returnstring & "D_" & UnitSelecter.SelectedIndex & "}"
            Case 1
                returnstring = returnstring & "K_" & UnitSelecter.SelectedIndex & "}"
            Case 2
                returnstring = returnstring & "N}"
            Case 3
                Select Case UnitSelecter.SelectedIndex
                    Case 0
                        returnstring = returnstring & "O}"
                    Case 1
                        returnstring = returnstring & "G}"
                End Select
            Case 4
                returnstring = returnstring & "S_" & UnitSelecter.SelectedIndex & "}"
            Case 5
                returnstring = returnstring & "{GT}"
            Case 6
                returnstring = returnstring & "{C:" & UnitSelecter.Text & "}"
            Case 7
                returnstring = returnstring & "{C:tct.s2u(tct.strptr(" & UnitSelecter.SelectedIndex + 1 & "))}"
            Case 8
                returnstring = returnstring & "{C:tct.s2u(tct.strptr(wread(0x660260 + 2 * " & UnitSelecter.SelectedIndex & ")))}"
            Case 9
                returnstring = "{C:" & TextBox1.Text & "}"
        End Select
    End Sub


    Private Function GetVariablesWithoutoverlap() As List(Of String)
        Dim returnValues As New List(Of String)

        Dim values As New List(Of String)
        If GlobalVar.GetElementsCount <> 0 Then
            values.AddRange(GlobalVar.GetElementList(GlobalVar.GetElementsCount - 1).GetVariables(Nothing))
        End If

        values.AddRange(_varele.GetVariables(Nothing))

        For i = 0 To values.Count - 1
            If returnValues.Contains(values(i)) = False Then
                returnValues.Add(values(i))
            End If
        Next

        Return returnValues
    End Function

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        DialogResult = DialogResult.OK
    End Sub


    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Select Case ListBox1.SelectedIndex
            Case 0, 1
                ListBox3.Enabled = True
                TableLayoutPanel4.ColumnStyles(0).Width = 60
                TableLayoutPanel4.ColumnStyles(1).Width = 30

                TextBox1.Visible = False
                UnitSelecter.Dock = DockStyle.Fill
                UnitSelecter.Visible = True
            Case 2 To 4
                ListBox3.Enabled = True
                TableLayoutPanel4.ColumnStyles(0).Width = 60
                TableLayoutPanel4.ColumnStyles(1).Width = 30

                TextBox1.Visible = False
                UnitSelecter.Dock = DockStyle.Fill
                UnitSelecter.Visible = True
            Case 5, 6, 7, 8
                ListBox3.Enabled = False
                TableLayoutPanel4.ColumnStyles(0).Width = 100
                TableLayoutPanel4.ColumnStyles(1).Width = 0

                TextBox1.Visible = False
                UnitSelecter.Dock = DockStyle.Fill
                UnitSelecter.Visible = True
            Case 9
                ListBox3.Enabled = False
                TableLayoutPanel4.ColumnStyles(0).Width = 100
                TableLayoutPanel4.ColumnStyles(1).Width = 0

                TextBox1.Dock = DockStyle.Fill
                TextBox1.Visible = True
                UnitSelecter.Visible = False
        End Select

        UnitSelecter.Enabled = False

        UnitSelecter.Items.Clear()
        UnitSelecter.ResetText()
        Select Case ListBox1.SelectedIndex
            Case 0, 1
                UnitSelecter.Enabled = True
                For i = 0 To CODE(0).Count - 1
                    If DatEditDATA(DTYPE.units).ReadValue("Unit Map String", i) = 0 Then
                        UnitSelecter.Items.Add(CODE(0)(i))
                    Else
                        Try
                            UnitSelecter.Items.Add(ProjectSet.CHKSTRING(-1 + ProjectSet.CHKUNITNAME(i)) & "(" & CODE(0)(i) & ")")
                        Catch ex As Exception
                            UnitSelecter.Items.Add(CODE(0)(i))
                        End Try
                    End If
                Next

                UnitSelecter.SelectedIndex = 0
            Case 3
                UnitSelecter.Enabled = True
                UnitSelecter.Items.Add("Ore")
                UnitSelecter.Items.Add("Gas")
                UnitSelecter.SelectedIndex = 0
            Case 4
                UnitSelecter.Enabled = True
                UnitSelecter.Items.Add("Zerg Control Available")
                UnitSelecter.Items.Add("Zerg Control Used")
                UnitSelecter.Items.Add("Zerg Control Max")
                UnitSelecter.Items.Add("Terran Supply Available")
                UnitSelecter.Items.Add("Terran Supply Used")
                UnitSelecter.Items.Add("Terran Supply Max")
                UnitSelecter.Items.Add("Protoss Psi Available")
                UnitSelecter.Items.Add("Protoss Psi Used")
                UnitSelecter.Items.Add("Protoss Psi Max")
                UnitSelecter.SelectedIndex = 0
            Case 6
                UnitSelecter.Enabled = True
                UnitSelecter.Items.Clear()
                UnitSelecter.Items.AddRange(GetVariablesWithoutoverlap.ToArray)
                If UnitSelecter.Items.Count <> 0 Then
                    UnitSelecter.SelectedIndex = 0
                End If
            Case 7
                UnitSelecter.Enabled = True
                For i = 0 To ProjectSet.CHKSTRING.Count - 1
                    UnitSelecter.Items.Add(ProjectSet.CHKSTRING(i))
                Next

                UnitSelecter.SelectedIndex = 0
            Case 8
                UnitSelecter.Enabled = True
                For i = 0 To CODE(0).Count - 1
                    If DatEditDATA(DTYPE.units).ReadValue("Unit Map String", i) = 0 Then
                        UnitSelecter.Items.Add(CODE(0)(i))
                    Else
                        Try
                            UnitSelecter.Items.Add(ProjectSet.CHKSTRING(-1 + ProjectSet.CHKUNITNAME(i)) & "(" & CODE(0)(i) & ")")
                        Catch ex As Exception
                            UnitSelecter.Items.Add(CODE(0)(i))
                        End Try
                    End If
                Next

                UnitSelecter.SelectedIndex = 0
        End Select
    End Sub

    Private Sub AddTectDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)
        ListBox1.Items.Clear()
        ListBox1.Items.AddRange(REadtextfile("AddTectDialogList").ToArray)
        ListBox1.SelectedIndex = 0
        ListBox3.SelectedIndex = 0
    End Sub
End Class