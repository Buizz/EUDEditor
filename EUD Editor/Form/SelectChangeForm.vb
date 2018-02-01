Public Class SelectChangeForm
    Private Sub SelectChangeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Rows.Clear()

        For i = 0 To DatEditDATA.Count - 1
            For k = 0 To DatEditDATA(i).projectdata.Count - 1
                For p = 0 To DatEditDATA(i).projectdata(k).Count - 1
                    If DatEditDATA(i).projectdata(k)(p) <> 0 Then
                        Try 'keyINFO(k).VarStart)
                            Dim listviewitem As New ListViewItem
                            'DatEditDAT(i).typeName.ToUpper

                            Dim value As Long = DatEditDATA(i).projectdata(k)(p) + DatEditDATA(i).mapdata(k)(p) + DatEditDATA(i).data(k)(p)



                            'MsgBox(CODE(i)(p) & ", " & DatEditDATA(i).projectdata(k)(p))
                            'MsgBox(DatEditDATA(i).ReadValue(DatEditDATA(i).keyDic.Keys(k), p))
                            DataGridView1.Rows.Add(
                                        DatEditDATA(i).typeName.ToUpper,
                                        DatEditDATA(i).keyDic.Keys(k),
                                        CODE(i)(p + DatEditDATA(i).keyINFO(k).VarStart),
                                        value)


                        Catch ex As Exception
                            MsgBox(i & ", " & k & ", " & p)
                        End Try

                        DataGridView1.Item(3, DataGridView1.Rows.Count - 1).Tag = i & "," & k & "," & p

                    End If
                Next
            Next
        Next
    End Sub
    Private Sub SelectChangeForm_Close(sender As Object, e As EventArgs) Handles MyBase.Closing
        DataGridView1.EndEdit()
    End Sub


    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim dataset() As String = DataGridView1.Item(e.ColumnIndex, e.RowIndex).Tag.ToString.Split(",")

        Dim i, k, p As Integer
        i = dataset(0)
        k = dataset(1)
        p = dataset(2) + DatEditDATA(i).keyINFO(k).VarStart

        Try
            DatEditDATA(i).WriteValue(DatEditDATA(i).keyDic.Keys(k), p, DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value)
            'MsgBox(DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value & ", " & DataGridView1.Item(e.ColumnIndex, e.RowIndex).Tag)
        Catch ex As Exception
            DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = DatEditDATA(i).ReadValue(DatEditDATA(i).keyDic.Keys(k), p)
        End Try
    End Sub


    Private Sub DataGridView1_UserDeletingRow(sender As Object, e As EventArgs) Handles DataGridView1.UserDeletingRow
        Dim rowindex As Integer = DataGridView1.SelectedRows.Item(DataGridView1.SelectedRows.Count - 1).Index


        Dim dataset() As String = DataGridView1.Item(3, rowindex).Tag.ToString.Split(",")

        Dim i, k, p As Integer
        i = dataset(0)
        k = dataset(1)
        p = dataset(2)

        DatEditDATA(i).projectdata(k)(p) = 0

        'DatEditDAT(i).WriteValue(DatEditDAT(i).keyDic.Keys(k), p, 0)
    End Sub
End Class