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

            DataGridView2.Rows.Add("BGM " & i + 1, getPureName(Soundlist(i)), "비우기")
        Next

        'D:\Music\2018년 01월 10일 멜론 실시간 TOP 100\2018년 01월 10일 멜론 실시간 TOP 100\001. 장덕철 - 그날처럼.mp3

        'GRPEditorDATFilename0: unit\ terran \ BattleCr.grp
        'GRPEditorDATSafeFilename0: unit\ terran \ BattleCr.grp
    End Sub
End Class