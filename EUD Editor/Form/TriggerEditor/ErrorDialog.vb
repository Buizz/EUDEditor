Imports System.Text
Imports System.Text.RegularExpressions

Public Class ErrorDialog
    Private Sub ErrorDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
    End Sub

    Private Sub ErrorParse(text As String)
        Dim returnstr As New StringBuilder
        DataGridView1.Rows.Clear()
        '일단 찾아
        Dim regex As New Regex("Module ""TriggerEditor"" Line (\d*) : (.*)")
        Dim matchcoll As MatchCollection = regex.Matches(text)

        For i = 0 To matchcoll.Count - 1
            Dim linecount As ULong = matchcoll.Item(i).Groups.Item(1).Value
            Dim ErrorType As String = matchcoll.Item(i).Groups.Item(2).Value

            'returnstr.AppendLine(matchcoll.Item(i).Groups.Item(1).Value & " " & matchcoll.Item(i).Groups.Item(2).Value & vbCrLf)
            Try
                returnstr.AppendLine(DebugDic(linecount).GetText)
                DebugDic(linecount).CTreeNode.BackColor = Color.Red


                DataGridView1.Rows.Add({linecount, ErrorType})
                DataGridView1.Rows.Item(DataGridView1.Rows.Count - 1).Tag = DebugDic(linecount).CTreeNode
            Catch ex As Exception

            End Try


        Next


    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Hide()
        If TEErrorText <> "" Or TEErrorText2 <> "" Then
            ErrorParse(TEErrorText)
            Me.Show()
            Me.Size = New Size(200, 200)
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        Try
            Dim node As TreeNode = CType(DataGridView1.SelectedRows(0).Tag, TreeNode)
            node.TreeView.SelectedNode = node
        Catch ex As Exception
            'DataGridView1.Rows.RemoveAt(DataGridView1.SelectedRows(0).Index)
        End Try
    End Sub
End Class