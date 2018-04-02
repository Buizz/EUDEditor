Public Class FuncManagerDialog
    Dim loading As Boolean = False
    Private Sub FuncManagerDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loading = False
        CheckedListBox1.Items.Clear()

        Dim FuncNames As New List(Of String)
        For i = 0 To functions.GetElementsCount - 1
            FuncNames.Add(functions.GetElements(i).Values(0))
        Next



        For Each _file As String In IO.Directory.GetFiles(My.Application.Info.DirectoryPath & "\TEFunction")
            Dim filenames As String = _file.Split("\").Last.Split(".").First
            CheckedListBox1.Items.Add(filenames)
            If FuncNames.IndexOf(filenames) <> -1 Then
                CheckedListBox1.SetItemChecked(CheckedListBox1.Items.Count - 1, True)
            End If
        Next
        loading= true
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        If loading Then
            If e.NewValue Then
                '파일 불러오기
                FuncLoadFile(CheckedListBox1.Items(e.Index))
            Else
                FuncDeleteFile(CheckedListBox1.Items(e.Index))
            End If
        End If
    End Sub
End Class