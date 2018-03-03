Public Class FileSettingForm
    Private Function getPureName(str As String) As String
        Return str.Split("\").Last
    End Function

    Private Sub FileSettingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView2.Rows.Clear()
        DataGridView2.Rows.Add("grpwire", dataDumper_grpwire, "비우기")
        DataGridView2.Rows.Add("tranwire", dataDumper_tranwire, "비우기")
        DataGridView2.Rows.Add("wirefram", dataDumper_wirefram, "비우기")
        DataGridView2.Rows.Add("cmdicons", dataDumper_cmdicons, "비우기")
        DataGridView2.Rows.Add("stat_txt", dataDumper_stat_txt, "비우기")
        DataGridView2.Rows.Add("AIscript", dataDumper_AIscript, "비우기")
        DataGridView2.Rows.Add("iscript", dataDumper_iscript, "비우기")


        DataGridView2.Rows.Add("arrow", grpinjector_arrow, "비우기")
        DataGridView2.Rows.Add("drag", grpinjector_drag, "비우기")
        DataGridView2.Rows.Add("illegal", grpinjector_illegal, "비우기")

        For i = 0 To Soundlist.Count - 1
            DataGridView2.Rows.Add("BGM " & i + 1, getPureName(Soundlist(i)), "")
        Next

        For i = 0 To GRPEditorDATA.Count - 1
            DataGridView2.Rows.Add("GRP " & i + 1, getPureName(GRPEditorDATA(i).Filename), "")
        Next

        'D:\Music\2018년 01월 10일 멜론 실시간 TOP 100\2018년 01월 10일 멜론 실시간 TOP 100\001. 장덕철 - 그날처럼.mp3

        'GRPEditorDATFilename0: unit\ terran \ BattleCr.grp
        'GRPEditorDATSafeFilename0: unit\ terran \ BattleCr.grp


        ' ProjectSet.saveStatus = False
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        ProjectSet.saveStatus = False
        If e.ColumnIndex = 2 Then
            Select Case e.RowIndex
                Case 0
                    dataDumper_grpwire = ""
                    dataDumper_grpwire_f = 0
                    DataGridView2.Rows(e.RowIndex).Cells(1).Value = ""
                Case 1
                    dataDumper_tranwire = ""
                    dataDumper_tranwire_f = 0
                    DataGridView2.Rows(e.RowIndex).Cells(1).Value = ""
                Case 2
                    dataDumper_wirefram = ""
                    dataDumper_wirefram_f = 0
                    DataGridView2.Rows(e.RowIndex).Cells(1).Value = ""
                Case 3
                    dataDumper_cmdicons = ""
                    dataDumper_cmdicons_f = 0
                    DataGridView2.Rows(e.RowIndex).Cells(1).Value = ""
                Case 4
                    dataDumper_stat_txt = ""
                    dataDumper_stat_txt_f = 0
                    DataGridView2.Rows(e.RowIndex).Cells(1).Value = ""
                Case 5
                    dataDumper_AIscript = ""
                    dataDumper_AIscript_f = 0
                    DataGridView2.Rows(e.RowIndex).Cells(1).Value = ""
                Case 6
                    dataDumper_iscript = ""
                    dataDumper_iscript_f = 0
                    DataGridView2.Rows(e.RowIndex).Cells(1).Value = ""
                Case 7
                    grpinjector_arrow = ""
                    DataGridView2.Rows(e.RowIndex).Cells(1).Value = ""
                Case 8
                    grpinjector_drag = ""
                    DataGridView2.Rows(e.RowIndex).Cells(1).Value = ""
                Case 9
                    grpinjector_illegal = ""
                    DataGridView2.Rows(e.RowIndex).Cells(1).Value = ""
            End Select
        End If
    End Sub
End Class