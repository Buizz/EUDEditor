Public Class SetMPQForm
    Private Sub SetMPQForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refreshform()
    End Sub

    Private Sub refreshform()

        TextBox1.Text = ProgramSet.DatMPQDirec(0)
        TextBox2.Text = ProgramSet.DatMPQDirec(1)
        TextBox3.Text = ProgramSet.DatMPQDirec(2)
        TextBox4.Text = ProgramSet.DatMPQDirec(3)
        If CheckFileExist(ProgramSet.DatMPQDirec(0)) = False Then
            Panel1.Enabled = False
        End If
        If CheckFileExist(ProgramSet.DatMPQDirec(1)) = False Then
            Panel2.Enabled = False
        End If
        If CheckFileExist(ProgramSet.DatMPQDirec(2)) = False Then
            Panel3.Enabled = False
        End If
        If CheckFileExist(ProgramSet.DatMPQDirec(3)) = False Then
            Panel4.Enabled = False
        End If

        If Panel1.Enabled = False And Panel2.Enabled = False And Panel3.Enabled = False And Panel4.Enabled = False Then
            Button5.Enabled = True
        Else
            Button5.Enabled = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dialog As DialogResult

        OpenFileDialog1.Filter = "MPQ 파일|Patch_rt.mpq|모든 MPQ 파일|*.mpq"
        dialog = OpenFileDialog1.ShowDialog
        If dialog = DialogResult.OK Then
            ProgramSet.DatMPQDirec(0) = OpenFileDialog1.FileName
        End If
        refreshform()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dialog As DialogResult

        OpenFileDialog1.Filter = "MPQ 파일|BrooDat.mpq|모든 MPQ 파일|*.mpq"
        dialog = OpenFileDialog1.ShowDialog
        If dialog = DialogResult.OK Then
            ProgramSet.DatMPQDirec(1) = OpenFileDialog1.FileName
        End If
        refreshform()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim dialog As DialogResult

        OpenFileDialog1.Filter = "MPQ 파일|BroodWar.mpq|모든 MPQ 파일|*.mpq"
        dialog = OpenFileDialog1.ShowDialog
        If dialog = DialogResult.OK Then
            ProgramSet.DatMPQDirec(2) = OpenFileDialog1.FileName
        End If
        refreshform()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim dialog As DialogResult

        OpenFileDialog1.Filter = "MPQ 파일|StarDat.mpq|모든 MPQ 파일|*.mpq"
        dialog = OpenFileDialog1.ShowDialog
        If dialog = DialogResult.OK Then
            ProgramSet.DatMPQDirec(3) = OpenFileDialog1.FileName
        End If
        refreshform()
    End Sub
End Class