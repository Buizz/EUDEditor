Public Class SCDBLoginForm
    Dim islogin As Boolean


    Dim loginSize As New Size(254, 209)
    Dim logoutSize As New Size(186, 143)

    Private Sub SCDBLoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SCDBGetuserData()

        CheckBox1.Checked = My.Settings.Remember
        CheckBox2.Checked = My.Settings.AutoLogin
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = Hex(ProgramSet.SCDBSerial * 256 + ProjectSet.SCDBSerial)
        NumericUpDown1.Value = ProjectSet.SCDBSerial

        Label4.Visible = False
        Label5.Visible = False

        If ProjectSet.scdbLoingStatus Then
            Me.Size = logoutSize
            Panel1.Visible = False
            Panel2.Visible = True
        Else
            Me.Size = loginSize
            Panel1.Visible = True
            Panel2.Visible = False
        End If


        If My.Settings.Remember Then
            TextBox1.Text = My.Settings.ID
            TextBox2.Text = My.Settings.Password
        End If

        If My.Settings.AutoLogin And ProjectSet.scdbLoingStatus = False Then
            Login()
        End If

        ' Me.DialogResult = DialogResult.Yes
    End Sub
    Private Sub SCDBLoginForm_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        My.Settings.Save()

        If My.Settings.Remember Then
            My.Settings.ID = TextBox1.Text
            My.Settings.Password = TextBox2.Text
        End If

        If islogin = False Then
            Me.DialogResult = DialogResult.No
        End If
    End Sub


    Private Sub Login()
        If SCDBCheckID(TextBox1.Text) Then '아이디 확인
            Label4.Visible = False
            If SCDBCheckPW(TextBox1.Text, TextBox2.Text) Then '비밀번호 확인
                ProgramSet.SCDBSerial = "&H" & SCDBGetSerial(TextBox1.Text, TextBox2.Text)

                Label5.Visible = False
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
                islogin = True
                Me.DialogResult = DialogResult.Yes
                ProjectSet.scdbLoingStatus = True
            Else
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Hand)
                Label5.Visible = True
            End If
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Hand)
            Label4.Visible = True
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Login()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        My.Settings.Remember = CheckBox1.Checked
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        My.Settings.AutoLogin = CheckBox2.Checked
        CheckBox1.Checked = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        islogin = False
        Me.Size = loginSize
        Panel1.Visible = True
        Panel2.Visible = False
        ProjectSet.scdbLoingStatus = False
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Label4.Visible = False
        Label5.Visible = False
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Label4.Visible = False
        Label5.Visible = False
    End Sub


    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        ProjectSet.SCDBSerial = NumericUpDown1.Value
        TextBox3.Text = Hex(ProgramSet.SCDBSerial * 256 + ProjectSet.SCDBSerial)
    End Sub
End Class