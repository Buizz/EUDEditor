Public Class WaitDailog
    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.TextChanged, NumericUpDown1.ValueChanged
        If NumericUpDown1.Focused = True Then
            NumericUpDown2.Value = NumericUpDown1.Value * 42
        End If
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.TextChanged, NumericUpDown2.ValueChanged
        If NumericUpDown2.Focused = True Then
            NumericUpDown1.Value = Math.Floor(NumericUpDown2.Value / 42)
        End If
    End Sub

    Private Sub WaitDailog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NumericUpDown2.Value = NumericUpDown1.Value * 42
    End Sub
End Class