Public Class ThemeSetForm
    Dim loadstatus As Boolean = False
    Private Sub ColorSet()
        PictureBox1.BackColor = ProgramSet.FORECOLOR

        PictureBox2.BackColor = ProgramSet.BACKCOLOR
        PictureBox3.BackColor = ProgramSet.CHANGECOLOR

        PictureBox4.BackColor = ProgramSet.LISTCOLOR


        loadstatus = True
        ComboBox1.SelectedIndex = 0
        If ProgramSet.FORECOLOR = Color.White And
        ProgramSet.BACKCOLOR = Color.Black And
        ProgramSet.CHANGECOLOR = Color.DarkCyan And
        ProgramSet.LISTCOLOR = Color.DarkGray Then
            ComboBox1.SelectedIndex = 1
        End If

        If ProgramSet.FORECOLOR = Color.Black And
        ProgramSet.BACKCOLOR = Color.White And
        ProgramSet.CHANGECOLOR = Color.PaleGreen And
        ProgramSet.LISTCOLOR = Color.LightGray Then
            ComboBox1.SelectedIndex = 2
        End If

        If ProgramSet.FORECOLOR = Color.White And
        ProgramSet.BACKCOLOR = Color.FromArgb(&HFF193333) And
        ProgramSet.CHANGECOLOR = Color.DarkSlateBlue And
        ProgramSet.LISTCOLOR = Color.FromArgb(&HFF538585) Then
            ComboBox1.SelectedIndex = 3
        End If ''FromArgb(&HFF4D9999)
        loadstatus = False
    End Sub
    Private Sub ThemeSetForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        ComboBox1.Items.Clear()
        ComboBox1.Items.AddRange(Lan.GetArray(Me.Name, "Combobox1"))
        ColorSet()
    End Sub
    '    사용자 정의
    'DatEdit 테마
    'EUD Editor 테마
    'EUD Editor2 테마

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ColorDialog1.ShowDialog()
        ProgramSet.FORECOLOR = ColorDialog1.Color
        ColorSet()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ColorDialog1.ShowDialog()
        ProgramSet.BACKCOLOR = ColorDialog1.Color
        ColorSet()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ColorDialog1.ShowDialog()
        ProgramSet.CHANGECOLOR = ColorDialog1.Color
        ColorSet()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ColorDialog1.ShowDialog()
        ProgramSet.LISTCOLOR = ColorDialog1.Color
        ColorSet()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If loadstatus = False Then
            Select Case ComboBox1.SelectedIndex
                Case 1
                    ProgramSet.FORECOLOR = Color.White

                    ProgramSet.BACKCOLOR = Color.Black
                    ProgramSet.CHANGECOLOR = Color.DarkCyan

                    ProgramSet.LISTCOLOR = Color.DarkGray
                Case 2
                    ProgramSet.FORECOLOR = Color.Black

                    ProgramSet.BACKCOLOR = Color.White
                    ProgramSet.CHANGECOLOR = Color.PaleGreen

                    ProgramSet.LISTCOLOR = Color.LightGray
                Case 3
                    ProgramSet.FORECOLOR = Color.White

                    ProgramSet.BACKCOLOR = Color.FromArgb(&HFF193333)
                    ProgramSet.CHANGECOLOR = Color.DarkSlateBlue

                    ProgramSet.LISTCOLOR = Color.FromArgb(&HFF538585) ''FromArgb(&HFF4D9999)
            End Select



            ColorSet()
        End If
    End Sub
End Class