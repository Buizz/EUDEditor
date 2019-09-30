Public Class UnitSelecter
    Public SCDBLocationData As SCDBDataCreater.SCDBUnitData

    Private Sub UnitSelecter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Rows.Clear()

        For i = 0 To SCDBForm.units.Count - 1
            DataGridView1.Rows.Add(i, SCDBForm.units(i), CStr(SCDBLocationData.GetUnitCount(i)))
        Next
    End Sub


    Private Sub UnitSelecter_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        DataGridView1.Columns(0).Width = 32
        DataGridView1.Columns(1).Width = (Me.Size.Width - 40) / 2 - 15
        DataGridView1.Columns(2).Width = (Me.Size.Width - 40) / 2 - 15
    End Sub

    Private Sub UnitSelecter_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        DataGridView1.EndEdit()

        For i = 0 To SCDBForm.units.Count - 1
            Try
                SCDBLocationData.SetUnitCount(i, DataGridView1.Item(2, i).Value)
            Catch ex As Exception

            End Try
        Next
    End Sub
End Class