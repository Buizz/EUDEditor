Imports System.IO

Public Class MPQForm_ListForm
    Public Listtype As Integer
    Public ListValue As String = "None"
    Public okay As Boolean = True
    Private Sub MPQForm_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        My.Computer.Audio.Stop()
    End Sub
    Private Sub MPQForm_ListForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim filenames() As String
        Dim tempnames As New List(Of String)
        tempnames.AddRange({"music\pdefeat.wav", "music\prdyroom.wav", "music\protoss1.wav",
            "music\protoss2.wav", "music\protoss3.wav", "music\pvict.wav", "music\radiofreezerg.wav",
            "music\tdefeat.wav", "music\terran1.wav", "music\terran2.wav", "music\terran3.wav",
            "music\title.wav", "music\trdyroom.wav", "music\tvict.wav",
            "music\zdefeat.wav", "music\zerg1.wav", "music\zerg2.wav", "music\zerg3.wav",
            "music\zrdyroom.wav", "music\zvict.wav"})



        TreeView1.Nodes.Clear()

        Select Case Listtype
            Case 0 'wav
                tempnames.AddRange(Readtblfile(My.Application.Info.DirectoryPath & "\Data\" & "sfxdata.tbl"))
                filenames = tempnames.ToArray
                'filenames.
            Case 1 'smk
                Dim filmstream As New FileStream(My.Application.Info.DirectoryPath & "\Data\" & "Portdatalist.txt", FileMode.Open)
                Dim streamreader As New StreamReader(filmstream)
                Dim templist As New List(Of String)

                While (streamreader.EndOfStream = False)
                    Dim temp As String = streamreader.ReadLine()
                    templist.Add(Mid(temp, InStr(temp, "\") + 1))
                End While
                filenames = templist.ToArray 'readtblfile(My.Application.Info.DirectoryPath & "\Data\" & "portdata.tbl")

                streamreader.Close()
                filmstream.Close()
            Case Else
                Dim filmstream As New FileStream(My.Application.Info.DirectoryPath & "\Data\" & "binfilelist.txt", FileMode.Open)
                Dim streamreader As New StreamReader(filmstream)
                Dim templist As New List(Of String)

                While (streamreader.EndOfStream = False)
                    Dim temp As String = streamreader.ReadLine()
                    templist.Add(Mid(temp, InStr(temp, "\") + 1))
                End While
                filenames = templist.ToArray 'readtblfile(My.Application.Info.DirectoryPath & "\Data\" & "portdata.tbl")

                streamreader.Close()
                filmstream.Close()
        End Select
        For i = 0 To filenames.Count - 1
            Dim filename As String = filenames(i)
            Dim namedty As String
            Dim namedname As String = ""
            Dim names As New List(Of String)
            Dim index0, index1, index2, index3 As Integer
            Dim temptext As String = ""
            If filename <> "" Then
                filename = filename.ToLower
                namedty = filename.Trim
                While InStr(namedty, "\") <> 0
                    namedname = Mid(namedty, 1, InStr(namedty, "\") - 1)
                    namedty = Mid(namedty, InStr(namedty, "\") + 1)
                    names.Add(namedname)
                    'MsgBox(namedname & ", " & namedty)
                End While
                names.Add(namedty)
                'MsgBox(names(0) & names(1) & names(2))


                Select Case Listtype
                    Case 0 'wav
                        Select Case names.Count
                            Case 1
                                If TreeView1.Nodes.ContainsKey(names(0)) Then
                                    index1 = TreeView1.Nodes.IndexOfKey(names(0))
                                Else
                                    TreeView1.Nodes.Add(names(0), names(0))
                                    index1 = TreeView1.Nodes.IndexOfKey(names(0))
                                End If
                            Case 2
                                If TreeView1.Nodes.ContainsKey(names(0)) Then
                                    index1 = TreeView1.Nodes.IndexOfKey(names(0))
                                Else
                                    TreeView1.Nodes.Add(names(0), names(0))
                                    index1 = TreeView1.Nodes.IndexOfKey(names(0))
                                End If


                                If TreeView1.Nodes(index1).Nodes.ContainsKey(names(1)) Then
                                    index1 = TreeView1.Nodes(index1).Nodes.IndexOfKey(names(1))
                                Else
                                    TreeView1.Nodes(index1).Nodes.Add(names(1), names(1))
                                    index1 = TreeView1.Nodes(index1).Nodes.IndexOfKey(names(1))
                                End If
                            Case 3
                                If TreeView1.Nodes.ContainsKey(names(0)) Then
                                    index1 = TreeView1.Nodes.IndexOfKey(names(0))
                                Else
                                    TreeView1.Nodes.Add(names(0), names(0))
                                    index1 = TreeView1.Nodes.IndexOfKey(names(0))
                                End If


                                If TreeView1.Nodes(index1).Nodes.ContainsKey(names(1)) Then
                                    index2 = TreeView1.Nodes(index1).Nodes.IndexOfKey(names(1))
                                Else
                                    TreeView1.Nodes(index1).Nodes.Add(names(1), names(1))
                                    index2 = TreeView1.Nodes(index1).Nodes.IndexOfKey(names(1))
                                End If


                                If TreeView1.Nodes(index1).Nodes(index2).Nodes.ContainsKey(names(2)) Then
                                    index3 = TreeView1.Nodes(index1).Nodes(index2).Nodes.IndexOfKey(names(2))
                                Else
                                    TreeView1.Nodes(index1).Nodes(index2).Nodes.Add(names(2), names(2))
                                    index3 = TreeView1.Nodes(index1).Nodes(index2).Nodes.IndexOfKey(names(2))
                                End If
                        End Select
                    Case 1 'smk
                        temptext = Mid(names(0), 1, 1)
                        Select Case temptext
                            Case "n"
                                temptext = "Neutral"
                            Case "p"
                                temptext = "Protoss"
                            Case "t"
                                temptext = "Terran"
                            Case "u"
                                temptext = "Etc"
                            Case "z"
                                temptext = "Zerg"
                            Case "s"
                                temptext = "Static"
                        End Select

                        If TreeView1.Nodes.ContainsKey(temptext) Then
                            index0 = TreeView1.Nodes.IndexOfKey(temptext)
                        Else
                            TreeView1.Nodes.Add(temptext, temptext)
                            index0 = TreeView1.Nodes.IndexOfKey(temptext)
                        End If


                        Select Case names.Count
                            Case 1
                                If TreeView1.Nodes.ContainsKey(names(0)) Then
                                    index1 = TreeView1.Nodes.IndexOfKey(names(0))
                                Else
                                    TreeView1.Nodes.Add(names(0), names(0))
                                    index1 = TreeView1.Nodes.IndexOfKey(names(0))
                                End If
                            Case 2
                                If TreeView1.Nodes(index0).Nodes.ContainsKey(names(0)) Then
                                    index1 = TreeView1.Nodes(index0).Nodes.IndexOfKey(names(0))
                                Else
                                    TreeView1.Nodes(index0).Nodes.Add(names(0), names(0))
                                    index1 = TreeView1.Nodes(index0).Nodes.IndexOfKey(names(0))
                                End If


                                If TreeView1.Nodes(index0).Nodes(index1).Nodes.ContainsKey(names(1)) Then
                                    index1 = TreeView1.Nodes(index0).Nodes(index1).Nodes.IndexOfKey(names(1))
                                Else
                                    TreeView1.Nodes(index0).Nodes(index1).Nodes.Add(names(1), names(1))
                                    index1 = TreeView1.Nodes(index0).Nodes(index1).Nodes.IndexOfKey(names(1))
                                End If
                            Case 3
                                If TreeView1.Nodes(index0).Nodes.ContainsKey(names(0)) Then
                                    index1 = TreeView1.Nodes(index0).Nodes.IndexOfKey(names(0))
                                Else
                                    TreeView1.Nodes(index0).Nodes.Add(names(0), names(0))
                                    index1 = TreeView1.Nodes(index0).Nodes.IndexOfKey(names(0))
                                End If


                                If TreeView1.Nodes(index0).Nodes(index1).Nodes.ContainsKey(names(1)) Then
                                    index2 = TreeView1.Nodes(index0).Nodes(index1).Nodes.IndexOfKey(names(1))
                                Else
                                    TreeView1.Nodes(index0).Nodes(index1).Nodes.Add(names(1), names(1))
                                    index2 = TreeView1.Nodes(index0).Nodes(index1).Nodes.IndexOfKey(names(1))
                                End If


                                If TreeView1.Nodes(index0).Nodes(index1).Nodes(index2).Nodes.ContainsKey(names(2)) Then
                                    index3 = TreeView1.Nodes(index0).Nodes(index1).Nodes(index2).Nodes.IndexOfKey(names(2))
                                Else
                                    TreeView1.Nodes(index0).Nodes(index1).Nodes(index2).Nodes.Add(names(2), names(2))
                                    index3 = TreeView1.Nodes(index0).Nodes(index1).Nodes(index2).Nodes.IndexOfKey(names(2))
                                End If
                        End Select
                    Case Else
                        If TreeView1.Nodes.ContainsKey(names(0)) Then
                            index1 = TreeView1.Nodes.IndexOfKey(names(0))
                        Else
                            TreeView1.Nodes.Add(names(0), names(0))
                            index1 = TreeView1.Nodes.IndexOfKey(names(0))
                        End If
                End Select





            End If
            'TreeView1.Nodes.Add(filenames(i))
        Next
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim mpq As New SFMpq
        ListValue = e.Node.FullPath
        If e.Node.Nodes.Count = 0 Then
            Select Case Listtype
                Case 0 'wav
                    Try
                        If InStr(e.Node.FullPath, "music") = 1 Then
                            My.Computer.Audio.Play(mpq.ReaddatFile(ListValue), AudioPlayMode.Background)
                        Else
                            ListValue = "sound\" & ListValue
                            'MsgBox(mpq.ReaddatFile(ListValue).Count)  
                            My.Computer.Audio.Play(mpq.ReaddatFile(ListValue), AudioPlayMode.Background)
                        End If
                    Catch ex As Exception
                        MsgBox("웨이브 재생에 실패했습니다.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)

                    End Try
                Case 1 'smk
                    ListValue = "portrait\" & Mid(ListValue, InStr(ListValue, "\") + 1)
                Case Else
                    ListValue = "rez\" & ListValue
            End Select
        End If

    End Sub

    Private Sub TreeView1_Double(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        ListValue = e.Node.FullPath
        Select Case Listtype
            Case 0 'wav
                If InStr(e.Node.FullPath, "music") = 1 Then
                Else
                    ListValue = "sound\" & ListValue
                End If
            Case 1 'smk
                ListValue = "portrait\" & Mid(ListValue, InStr(ListValue, "\") + 1)
            Case Else
                ListValue = "rez\" & ListValue
        End Select

        If e.Node.Nodes.Count = 0 Then
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ListValue = TreeView1.SelectedNode.FullPath
        Select Case Listtype
            Case 0 'wav
                If InStr(TreeView1.SelectedNode.FullPath, "music") = 1 Then
                Else
                    ListValue = "sound\" & ListValue
                End If
            Case 1 'smk
                ListValue = "portrait\" & Mid(ListValue, InStr(ListValue, "\") + 1)
            Case Else
                ListValue = "rez\" & ListValue
        End Select

        If TreeView1.SelectedNode.Nodes.Count = 0 Then
            Me.Close()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        okay = False
        Close()
    End Sub
End Class