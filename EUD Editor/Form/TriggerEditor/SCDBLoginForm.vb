Public Class SCDBLoginForm
    Dim islogin As Boolean


    Dim loginSize As New Size(254, 209)
    Dim logoutSize As New Size(186, 143)

    Private Sub SCDBLoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = SCDBMaker
        TextBox2.Text = SCDBMapName

        NumericUpDown1.Value = SCDBDataSize
        ' Me.DialogResult = DialogResult.Yes
    End Sub
    Private Sub SCDBLoginForm_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        SCDBMaker = TextBox1.Text
        SCDBMapName = TextBox2.Text

        SCDBDataSize = NumericUpDown1.Value
    End Sub


End Class