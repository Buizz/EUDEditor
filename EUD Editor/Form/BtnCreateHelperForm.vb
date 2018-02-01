Public Class BtnCreateHelperForm

    Public isbtnset As Boolean = False
    Public dpos As Integer = 1
    Public dicon As Integer = 0
    Public dcon As Integer = &H4DEB40
    Public dact As Integer = &H423180
    Public dconval As Integer = 0
    Public dactval As Integer = 0
    Public denaStr As Integer = 1
    Public ddisStr As Integer = 1


    '빅 사이즈 396, 262
    '스몰 사이즈 181, 262

    Dim btntype As Integer
    Private Sub refreshGUI()
        Dim lastsel As Integer = ListBox2.SelectedIndex
        ListBox2.Items.Clear()


        '6
        '7
        '11
        '12
        '13
        '14
        '15
        '유니ㅛ


        '8
        '10
        '기술

        '9
        '업
        If btntype < 5 Then
            isbtnset = True
            GroupBox3.Enabled = False
            GroupBox2.Enabled = False
        Else
            GroupBox3.Enabled = True
            GroupBox2.Enabled = True
            isbtnset = False
        End If

        Select Case btntype
            Case 6 To 7
                ListBox2.Items.AddRange(CODE(DTYPE.units).ToArray)
                ListBox2.SelectedIndex = 0
            Case 11 To 12 '저그
                ListBox2.Items.AddRange(CODE(DTYPE.units).ToArray)
                ListBox2.SelectedIndex = 227
                ListBox2.SelectedIndex = 130
            Case 13 '테란
                ListBox2.Items.AddRange(CODE(DTYPE.units).ToArray)
                ListBox2.SelectedIndex = 227
                ListBox2.SelectedIndex = 106
            Case 14 '프로토스
                ListBox2.Items.AddRange(CODE(DTYPE.units).ToArray)
                ListBox2.SelectedIndex = 227
                ListBox2.SelectedIndex = 154
            Case 15 '에드온
                ListBox2.Items.AddRange(CODE(DTYPE.units).ToArray)
                ListBox2.SelectedIndex = 227
                ListBox2.SelectedIndex = 106

            Case 8, 10
                ListBox2.Items.AddRange(CODE(DTYPE.techdata).ToArray)
                ListBox2.SelectedIndex = 0
            Case 9
                ListBox2.Items.AddRange(CODE(DTYPE.upgrades).ToArray)
                ListBox2.SelectedIndex = 0
        End Select
    End Sub
    Private Sub btnreset()
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True
        Button6.Enabled = True
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = True

        Select Case dpos
            Case 1
                Button1.Enabled = False
            Case 2
                Button2.Enabled = False
            Case 3
                Button3.Enabled = False
            Case 4
                Button4.Enabled = False
            Case 5
                Button5.Enabled = False
            Case 6
                Button6.Enabled = False
            Case 7
                Button7.Enabled = False
            Case 8
                Button8.Enabled = False
            Case 9
                Button9.Enabled = False
        End Select

        ListBox2.Focus()
    End Sub

    Private Sub BtnCreateHelperForm_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        If Me.DialogResult = DialogResult.OK Then


            If ListBox2.SelectedIndex <> -1 Then
                dconval = ListBox2.SelectedIndex
                dactval = ListBox2.SelectedIndex
            End If
            Select Case ListBox1.SelectedIndex
                Case 0 '기본
                    Dim tempbtn As New SBtnDATA
                    tempbtn.pos = 1
                    tempbtn.icon = 228
                    tempbtn.con = &H4282D0
                    tempbtn.act = &H424440
                    tempbtn.conval = 0
                    tempbtn.actval = 0
                    tempbtn.enaStr = 664
                    tempbtn.disStr = 0


                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)

                    tempbtn = New SBtnDATA
                    tempbtn.pos = 2
                    tempbtn.icon = 229
                    tempbtn.con = &H4282D0
                    tempbtn.act = &H4233F0
                    tempbtn.conval = 0
                    tempbtn.actval = 0
                    tempbtn.enaStr = 665
                    tempbtn.disStr = 0

                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)

                    tempbtn = New SBtnDATA
                    tempbtn.pos = 3
                    tempbtn.icon = 230
                    tempbtn.con = &H428F30
                    tempbtn.act = &H424380
                    tempbtn.conval = 0
                    tempbtn.actval = 0
                    tempbtn.enaStr = 666
                    tempbtn.disStr = 0

                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)

                    tempbtn = New SBtnDATA
                    tempbtn.pos = 4
                    tempbtn.icon = 254
                    tempbtn.con = &H4282D0
                    tempbtn.act = &H424140
                    tempbtn.conval = 0
                    tempbtn.actval = 0
                    tempbtn.enaStr = 667
                    tempbtn.disStr = 0

                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)

                    tempbtn = New SBtnDATA
                    tempbtn.pos = 5
                    tempbtn.icon = 255
                    tempbtn.con = &H4282D0
                    tempbtn.act = &H423370
                    tempbtn.conval = 0
                    tempbtn.actval = 0
                    tempbtn.enaStr = 668
                    tempbtn.disStr = 0

                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)
                Case 1 '이동 가능 건물
                    Dim tempbtn As New SBtnDATA
                    tempbtn.pos = 1
                    tempbtn.icon = 228
                    tempbtn.con = &H428420
                    tempbtn.act = &H424440
                    tempbtn.conval = 0
                    tempbtn.actval = 0
                    tempbtn.enaStr = 664
                    tempbtn.disStr = 0


                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)

                    tempbtn = New SBtnDATA
                    tempbtn.pos = 2
                    tempbtn.icon = 229
                    tempbtn.con = &H428420
                    tempbtn.act = &H4233F0
                    tempbtn.conval = 0
                    tempbtn.actval = 0
                    tempbtn.enaStr = 665
                    tempbtn.disStr = 0

                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)

                    tempbtn = New SBtnDATA
                    tempbtn.pos = 9
                    tempbtn.icon = 283
                    tempbtn.con = &H4283F0
                    tempbtn.act = &H423C30
                    tempbtn.conval = 0
                    tempbtn.actval = FireGraftForm._OBJECTNUM
                    tempbtn.enaStr = 670
                    tempbtn.disStr = 0

                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)

                    tempbtn = New SBtnDATA
                    tempbtn.pos = 9
                    tempbtn.icon = 282
                    tempbtn.con = &H4287D0
                    tempbtn.act = &H423230
                    tempbtn.conval = 0
                    tempbtn.actval = 0
                    tempbtn.enaStr = 671
                    tempbtn.disStr = 0

                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)
                Case 2 '버러우
                    Dim tempbtn As New SBtnDATA
                    tempbtn.pos = 9
                    tempbtn.icon = 259
                    tempbtn.con = &H4290F0
                    tempbtn.act = &H4232B0
                    tempbtn.conval = 11
                    tempbtn.actval = 11
                    tempbtn.enaStr = 372
                    tempbtn.disStr = 382


                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)

                    tempbtn = New SBtnDATA
                    tempbtn.pos = 9
                    tempbtn.icon = 260
                    tempbtn.con = &H429070
                    tempbtn.act = &H423290
                    tempbtn.conval = 11
                    tempbtn.actval = 11
                    tempbtn.enaStr = 373
                    tempbtn.disStr = 0

                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)
                Case 3 '채취자
                    Dim tempbtn As New SBtnDATA
                    tempbtn.pos = 5
                    tempbtn.icon = 231
                    tempbtn.con = &H4284B0
                    tempbtn.act = &H423B70
                    tempbtn.conval = 0
                    tempbtn.actval = 0
                    tempbtn.enaStr = 675
                    tempbtn.disStr = 0


                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)

                    tempbtn = New SBtnDATA
                    tempbtn.pos = 6
                    tempbtn.icon = 233
                    tempbtn.con = &H428480
                    tempbtn.act = &H423760
                    tempbtn.conval = 0
                    tempbtn.actval = 0
                    tempbtn.enaStr = 676
                    tempbtn.disStr = 0

                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)
                Case 4 '운송수단
                    Dim tempbtn As New SBtnDATA
                    tempbtn.pos = 8
                    tempbtn.icon = 309
                    tempbtn.con = &H428FF0
                    tempbtn.act = &H423B40
                    tempbtn.conval = 0
                    tempbtn.actval = 0
                    tempbtn.enaStr = 683
                    tempbtn.disStr = 0


                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)

                    tempbtn = New SBtnDATA
                    tempbtn.pos = 9
                    tempbtn.icon = 312
                    tempbtn.con = &H428EA0
                    tempbtn.act = &H423B00
                    tempbtn.conval = 0
                    tempbtn.actval = 0
                    tempbtn.enaStr = 684
                    tempbtn.disStr = 0

                    ProjectBtnData(FireGraftForm._OBJECTNUM).Add(tempbtn)

                Case 5
                    dicon = 0
                    dcon = &H4DEB40
                    dact = &H423180
                    dconval = 0
                    dactval = 0
                    denaStr = 1
                    ddisStr = 1
                Case 6
                    dicon = ListBox2.SelectedIndex
                    dcon = &H428E60
                    dact = &H4234B0
                Case 7
                    dicon = ListBox2.SelectedIndex
                    dcon = &H428E60
                    dact = &H423790


                Case 8 '기술
                    dicon = DatEditDATA(DTYPE.techdata).ReadValue("Icon", ListBox2.SelectedIndex)
                    dcon = &H429500
                    dact = &H423350

                Case 9 '업글
                    dicon = DatEditDATA(DTYPE.upgrades).ReadValue("Icon", ListBox2.SelectedIndex)
                    dcon = &H429450
                    dact = &H423310
                Case 10 '기술
                    dicon = DatEditDATA(DTYPE.techdata).ReadValue("Icon", ListBox2.SelectedIndex)
                    dcon = &H4294E0
                    dact = &H423F70


                Case 11
                    dicon = ListBox2.SelectedIndex
                    dcon = &H428E60
                    dact = &H423860
                Case 12
                    dicon = ListBox2.SelectedIndex
                    dcon = &H428E60
                    dact = &H423C50
                Case 13
                    dicon = ListBox2.SelectedIndex
                    dcon = &H428E60
                    dact = &H423EB0
                Case 14
                    dicon = ListBox2.SelectedIndex
                    dcon = &H428E60
                    dact = &H423DD0
                Case 15
                    dicon = ListBox2.SelectedIndex
                    dcon = &H428E60
                    dact = &H423D10
            End Select
        End If


        GroupBox3.Enabled = False
        GroupBox2.Enabled = False
        'Me.Size = New Size(171, 252)
    End Sub
    Private Sub BtnCreateHelperForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.SelectedIndex = 0
        btnreset()
    End Sub


    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        btntype = ListBox1.SelectedIndex
        refreshGUI()
        btnreset()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dpos = 1
        btnreset()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        dpos = 2
        btnreset()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        dpos = 3
        btnreset()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        dpos = 4
        btnreset()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        dpos = 5
        btnreset()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        dpos = 6
        btnreset()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        dpos = 7
        btnreset()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        dpos = 8
        btnreset()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        dpos = 9
        btnreset()
    End Sub
End Class