Imports System.IO
Imports System.Text



Public Class PluginTriggerAddForm
    Public TriggerString As String

    Private Sub PluginTriggerAddForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()

        If TriggerString <> "" Then
            ListBox1.Enabled = False

            Dim triggertext() As String = {}
            For i = 0 To ListBox1.Items.Count - 1
                If ListBox1.Items(i).ToString.Split("(")(0) = TriggerString.Split("(")(0) Then
                    triggertext = ListBox1.Items(i).ToString.Split(",")
                    ListBox1.SelectedIndex = i
                    Exit For
                End If
            Next


            Dim temptext() As String = TriggerString.Split(",")

            DataGridView1.Rows.Clear()
            If InStr(TriggerString, "()") = 0 Then


                For i = 0 To temptext.Count - 1
                    If i = 0 Then
                        DataGridView1.Rows.Add(triggertext(i).Split("(")(1).Replace(")", ""), temptext(i).Split("(")(1).Replace(")", ""))
                    Else
                        DataGridView1.Rows.Add(triggertext(i).Trim.Replace(")", ""), temptext(i).Trim.Replace(")", ""))
                    End If

                Next
            End If
            DataGridView1.Height = DataGridView1.RowTemplate.Height * (DataGridView1.Rows.Count + 1) + 2
            ' 
        Else
            ListBox1.Enabled = True
        End If
        If ListBox1.SelectedIndex = -1 Then
            Button5.Enabled = False
        Else
            Button5.Enabled = True
        End If
    End Sub

    Private Sub LoadData()
        Dim filename As String = My.Application.Info.DirectoryPath & "\Data\trigeditplus\stockcond.lua"

        Dim filestream As New FileStream(filename, FileMode.Open)
        Dim streamreader As New StreamReader(filestream)

        Dim startoffset, endoffset As Integer


        Dim temptext As String
        Dim tempBuilder As New StringBuilder
        tempBuilder.Append(streamreader.ReadToEnd)


        While InStr(tempBuilder.ToString, "function") <> 0
            startoffset = InStr(tempBuilder.ToString, "function")
            endoffset = InStr(Mid(tempBuilder.ToString, startoffset), vbCrLf)
            temptext = Mid(tempBuilder.ToString, startoffset, endoffset).Trim

            tempBuilder.Remove(1, startoffset)

            ListBox1.Items.Add(temptext.Replace("function ", ""))
        End While




        streamreader.Close()
        filestream.Close()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If TriggerString = "" Then
            Dim temptext() As String = ListBox1.SelectedItem.ToString.Split(",")

            DataGridView1.Rows.Clear()
            If InStr(ListBox1.SelectedItem.ToString, "()") = 0 Then

                For i = 0 To temptext.Count - 1
                    If i = 0 Then
                        DataGridView1.Rows.Add(temptext(i).Split("(")(1).Replace(")", ""), "0")
                    Else
                        DataGridView1.Rows.Add(temptext(i).Trim.Replace(")", ""), "0")
                    End If

                Next
            End If
            DataGridView1.Height = DataGridView1.RowTemplate.Height * (DataGridView1.Rows.Count + 1) + 2
            ' 
        End If
        If ListBox1.SelectedIndex = -1 Then
            Button5.Enabled = False
        Else
            Button5.Enabled = True
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TriggerString = ListBox1.SelectedItem.ToString.Split("(")(0)

        TriggerString = TriggerString & "("
        For i = 0 To DataGridView1.Rows.Count - 1
            TriggerString = TriggerString & DataGridView1.Item(1, i).Value & ", "
        Next
        If DataGridView1.Rows.Count > 0 Then
            TriggerString = Mid(TriggerString, 1, TriggerString.Length - 2)
        End If
        TriggerString = TriggerString & ")"
    End Sub
End Class