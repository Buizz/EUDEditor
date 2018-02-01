Public Class FiregraftRepAddForm
    Public Opcode As UInt16
    Public Value As UInt16
    Public ishavevalue As Boolean

    Private Sub FiregraftRepAddForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        Opcode = 0
        Value = 0

        ComboBox11.SelectedIndex = Opcode

        ishavevalue = False
        ListBox1.Visible = False
    End Sub
    Private Sub ResetList()
        Dim selindex As Integer = ListBox1.SelectedIndex

        Dim lastindexitems As String = ""
        If ListBox1.SelectedIndex <> -1 Then
            lastindexitems = ListBox1.Items(ListBox1.SelectedIndex)

        End If

        Dim filiter As String = TextBox1.Text.ToLower

        ListBox1.BeginUpdate()
        ListBox1.Items.Clear()

        If Opcode = 36 Then

            For i = 0 To CODE(DTYPE.techdata).Count - 2
                If InStr(CODE(DTYPE.techdata)(i).ToLower, filiter) <> 0 Or filiter = "" Then
                    ListBox1.Items.Add("[" & Format(i, "000") & "]" & CODE(DTYPE.techdata)(i))
                End If
            Next
        Else
            For i = 0 To CODE(DTYPE.units).Count - 1
                If InStr(CODE(DTYPE.units)(i).ToLower, filiter) <> 0 Or filiter = "" Then
                    ListBox1.Items.Add("[" & Format(i, "000") & "]" & CODE(DTYPE.units)(i))
                End If
            Next
        End If


        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(i) = lastindexitems Then
                ListBox1.SelectedIndex = i
                Exit For
            End If
        Next


        ListBox1.EndUpdate()
    End Sub

    Private Sub ComboBox11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox11.SelectedIndexChanged
        Opcode = ComboBox11.SelectedIndex
        Select Case Opcode
            Case 1, 2, 3, 36, 38
                ishavevalue = True
                ListBox1.Visible = True
                ResetList()
            Case Else
                ishavevalue = False
                ListBox1.Visible = False
        End Select

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex = -1 Then
            Value = 0
        Else
            Value = Mid(ListBox1.Items(ListBox1.SelectedIndex), 2, 3)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ResetList()
    End Sub
End Class