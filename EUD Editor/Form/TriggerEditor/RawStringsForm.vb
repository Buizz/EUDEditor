Public Class RawStringsForm
    Public returnvalue As String

    Private Enum ListDef
        Deaths = 1
        Kills = 0
        Score = 2
        Resources = 3
        Suppley = 4
        UpgradeC = 5
        UpgradeMax = 6
        TechC = 7
        TechMax = 8
        Lighting = 9
        GameTime = 10
        CounterTimer = 11
    End Enum



    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        DialogResult = DialogResult.OK
    End Sub
    Private Sub RawStringsForm_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        Select Case EasyCompletionComboBox1.SelectedIndex
            Case 0 'offset
                returnvalue = GetOffset()
            Case 1 'read
                Select Case ListBox1.SelectedIndex
                    Case ListDef.UpgradeC, ListDef.UpgradeMax, ListDef.TechC, ListDef.TechMax
                        returnvalue = "bread(" & GetOffset() & ")"
                    Case Else
                        returnvalue = "dwread_epd(EPD(" & GetOffset() & "))"
                End Select
            Case 2 'write
                Select Case ListBox1.SelectedIndex
                    Case ListDef.UpgradeC, ListDef.UpgradeMax, ListDef.TechC, ListDef.TechMax
                        returnvalue = "bwrite(" & GetOffset() & ")"
                    Case Else
                        returnvalue = "dwwrite_epd(EPD(" & GetOffset() & "))"
                End Select
        End Select
    End Sub

    Private Function GetOffset() As String
        Dim offset As String = ""
        Dim player As String = ListBox3.SelectedIndex
        If player = 8 Then
            player = "getcurpl()"
        End If
        Select Case ListBox1.SelectedIndex
            Case ListDef.Deaths
                offset = "0x58A364 + 48 * " & UnitSelecter.SelectedIndex & " + 4 * " & player
            Case ListDef.Kills
                offset = "0x5878A4 + 48 * " & UnitSelecter.SelectedIndex & " + 4 * " & player
            Case ListDef.Score
                If UnitSelecter.SelectedIndex = 18 Then
                    offset = "0x5822F4 + 4 * " & player
                Else
                    offset = "0x581DE4 + 48 * " & UnitSelecter.SelectedIndex & " + 4 * " & player
                End If
            Case ListDef.Resources
                offset = "0x57F0F0 + 48 * " & UnitSelecter.SelectedIndex & " + 4 * " & player
            Case ListDef.Suppley
                offset = "0x582144 + 48 * " & UnitSelecter.SelectedIndex & " + 4 * " & player
            Case ListDef.UpgradeC
                If UnitSelecter.SelectedIndex < 46 Then
                    offset = "0x58D2B0 + " & UnitSelecter.SelectedIndex & " + 46 * " & player
                Else
                    offset = "0x58F32C + " & UnitSelecter.SelectedIndex & " + 15 * " & player
                End If
            Case ListDef.UpgradeMax
                If UnitSelecter.SelectedIndex < 46 Then
                    offset = "0x58D088 + " & UnitSelecter.SelectedIndex & " + 46 * " & player
                Else
                    offset = "0x58F140 + " & UnitSelecter.SelectedIndex & " + 15 * " & player
                End If
            Case ListDef.TechC
                If UnitSelecter.SelectedIndex < 24 Then
                    offset = "0x58CF44 + " & UnitSelecter.SelectedIndex & " + 24 * " & player
                Else
                    offset = "0x58F140 + " & UnitSelecter.SelectedIndex & " + 20 * " & player
                End If
            Case ListDef.TechMax
                If UnitSelecter.SelectedIndex < 24 Then
                    offset = "0x58CE24 + " & UnitSelecter.SelectedIndex & " + 24 * " & player
                Else
                    offset = "0x58F050 + " & UnitSelecter.SelectedIndex & " + 20 * " & player
                End If
            Case ListDef.Lighting
                offset = "0x657A9C"
            Case ListDef.GameTime
                offset = "0x58D6F8"
            Case ListDef.CounterTimer
                offset = "0x58D6F4"
        End Select

        Return offset
    End Function

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Select Case ListBox1.SelectedIndex
            Case ListDef.Deaths, ListDef.Kills, ListDef.Score, ListDef.Resources, ListDef.Suppley, ListDef.UpgradeC, ListDef.UpgradeMax, ListDef.TechC, ListDef.TechMax
                ListBox3.Enabled = True
                TableLayoutPanel4.ColumnStyles(0).Width = 60
                TableLayoutPanel4.ColumnStyles(1).Width = 30

                UnitSelecter.Dock = DockStyle.Fill
                UnitSelecter.Visible = True
            Case ListDef.Lighting, ListDef.GameTime, ListDef.CounterTimer
                ListBox3.Enabled = False
                TableLayoutPanel4.ColumnStyles(0).Width = 100
                TableLayoutPanel4.ColumnStyles(1).Width = 0

                UnitSelecter.Dock = DockStyle.Fill
                UnitSelecter.Visible = False
        End Select

        UnitSelecter.Enabled = False

        UnitSelecter.Items.Clear()
        UnitSelecter.ResetText()
        Select Case ListBox1.SelectedIndex
            Case ListDef.Deaths, ListDef.Kills
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
            Case ListDef.Score
                UnitSelecter.Enabled = True
                UnitSelecter.Items.AddRange(Readtextfile("ScoreOffset").ToArray)
                UnitSelecter.SelectedIndex = 0
            Case ListDef.Resources
                UnitSelecter.Enabled = True
                UnitSelecter.Items.Add("Ore")
                UnitSelecter.Items.Add("Gas")
                UnitSelecter.SelectedIndex = 0
            Case ListDef.Suppley
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
            Case ListDef.UpgradeC, ListDef.UpgradeMax
                UnitSelecter.Enabled = True
                UnitSelecter.Items.Clear()
                UnitSelecter.Items.AddRange(CODE(5).ToArray)
                If UnitSelecter.Items.Count <> 0 Then
                    UnitSelecter.SelectedIndex = 0
                End If
            Case ListDef.TechC, ListDef.TechMax
                UnitSelecter.Enabled = True
                UnitSelecter.Items.Clear()
                UnitSelecter.Items.AddRange(CODE(6).ToArray)
                If UnitSelecter.Items.Count <> 0 Then
                    UnitSelecter.SelectedIndex = 0
                End If
        End Select
    End Sub

    Private Sub RawStringsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EasyCompletionComboBox1.SelectedIndex = 0
        ListBox1.Items.Clear()
        ListBox1.Items.AddRange(Readtextfile("RawStringsList").ToArray)
        ListBox1.SelectedIndex = 0
        ListBox3.SelectedIndex = 0
    End Sub
End Class