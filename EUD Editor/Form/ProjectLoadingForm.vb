Imports System.ComponentModel
Imports System.IO

Public Class ProjectLoadingForm
    Private _worker As BackgroundWorker
    Public Buffer() As Byte

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        _worker = New BackgroundWorker()
        AddHandler _worker.DoWork, AddressOf WorkerDoWork
        AddHandler _worker.RunWorkerCompleted, AddressOf WorkerCompleted

        _worker.RunWorkerAsync()
    End Sub

    ' This is executed on a worker thread and will not make the dialog unresponsive.  If you want
    ' to interact with the dialog (like changing a progress bar or label), you need to use the
    ' worker's ReportProgress() method (see documentation for details)
    Private Sub WorkerDoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Dim maxValue As Integer
        For k = 0 To DatEditDATA.Count - 1
            For i = 0 To DatEditDATA(k).projectdata.Count - 1
                maxValue += 1
            Next
        Next




        ProgressBar1.Value = 0
        ProgressBar1.Maximum = maxValue
        ' Initialize encoder here
        Dim MemStream As MemoryStream = New MemoryStream(Buffer)
        Dim binread As BinaryReader = New BinaryReader(MemStream)

        '  ProjectLoadingForm.texts = "DatEdit 데이터 로딩중..."
        '데이터 로딩 시작
        Label1.Text = "DatEdit 데이터 읽는 중"
        For k = 0 To DatEditDATA.Count - 1
            For i = 0 To DatEditDATA(k).projectdata.Count - 1
                For j = 0 To DatEditDATA(k).projectdata(i).Count - 1
                    Dim Offsetname As String = DatEditDATA(k).typeName & "_" & DatEditDATA(k).keyDic.Keys.ToList(i)
                    Dim _size As Integer = DatEditDATA(k).keyINFO(i).realSize

                    Dim _offsetNum As Long = Val("&H" & ReadOffset(Offsetname)) + _size * j

                    If _offsetNum >= &H50C000 Then
                        MemStream.Position = _offsetNum - &H50C000


                        Dim tvalue As UInteger
                        Select Case DatEditDATA(k).keyINFO(i).Size
                            Case 1
                                tvalue = binread.ReadByte
                            Case 2
                                tvalue = binread.ReadUInt16
                            Case 4
                                tvalue = binread.ReadUInt32
                        End Select
                        'MsgBox("왓더21 " & DatEditDATA(k).data(0)(0))
                        DatEditDATA(k).WriteValueNum(i, j + DatEditDATA(k).keyINFO(i).VarStart, tvalue, True)
                        'Try
                        'Catch ex As Exception
                        '    MsgBox("왓더21 " & Offsetname & " " & i & " " & j + DatEditDATA(k).keyINFO(i).VarStart & " " & tvalue)
                        'End Try

                    End If


                Next
                ProgressBar1.Value += 1
            Next
        Next

        Label1.Text = "FireGraft 데이터 읽는 중"
        For k = 0 To ProjectUnitStatusFn1.Count - 1
            Dim _offsetNum As Long = Val("&H" & ReadOffset("FG_Debug")) + 12 * k
            MemStream.Position = _offsetNum - &H50C000


            ProjectDebugID(k) = binread.ReadUInt32 - DebugID(k)

            Dim tvalue As UInteger = binread.ReadUInt32
            ProjectUnitStatusFn1(k) = statusFn1.FindIndex(Function(p)
                                                              Return p = tvalue
                                                          End Function) - UnitStatusFn1(k)


            tvalue = binread.ReadUInt32
            ProjectUnitStatusFn2(k) = statusFn2.FindIndex(Function(p As UInteger)
                                                              Return p = tvalue
                                                          End Function) - UnitStatusFn2(k)
        Next


        For i = 0 To ProjectBtnUSE.Count - 1
            Dim isuse As Boolean = False

            Dim _offsetNum As Long = Val("&H" & ReadOffset("FG_BtnNum")) + 12 * i
            '버튼 겟수 파악
            MemStream.Position = _offsetNum - &H50C000

            Dim btncount As UInteger = binread.ReadUInt32
            Dim btnoffset As UInteger = binread.ReadUInt32

            If btnoffset <> 0 Then
                MemStream.Position = btnoffset - &H50C000

                ProjectBtnData(i).Clear()

                If BtnData(i).Count = btncount Then
                    isuse = False
                Else
                    isuse = True
                End If

                If btncount <> 0 Then
                    For j = 0 To btncount - 1
                        ProjectBtnData(i).Add(New SBtnDATA)
                        With ProjectBtnData(i)(j)
                            .pos = binread.ReadUInt16()
                            .icon = binread.ReadUInt16()
                            .con = binread.ReadUInt32()
                            .act = binread.ReadUInt32()
                            .conval = binread.ReadUInt16()
                            .actval = binread.ReadUInt16()
                            .enaStr = binread.ReadUInt16()
                            .disStr = binread.ReadUInt16()



                            If isuse = False Then
                                If BtnData(i)(j).pos <> .pos Or
                                        BtnData(i)(j).icon <> .icon Or
                                        BtnData(i)(j).con <> .con Or
                                        BtnData(i)(j).act <> .act Or
                                        BtnData(i)(j).conval <> .conval Or
                                        BtnData(i)(j).actval <> .actval Or
                                        BtnData(i)(j).enaStr <> .enaStr Or
                                        BtnData(i)(j).disStr <> .disStr Then
                                    isuse = True
                                End If
                            End If
                        End With

                        'binread.ReadUInt16()
                        'binread.ReadUInt32()
                        ' binread.ReadUInt32()
                        'binread.ReadUInt16()
                        'binread.ReadUInt16()
                        ' binread.ReadUInt16()
                        'binread.ReadUInt16()
                    Next
                End If
            End If





            'tempbtn.icon = extratext(2, pos, cliptext)
            'tempbtn.con = extratext(4, pos, cliptext)
            'tempbtn.act = extratext(4, pos, cliptext)
            'tempbtn.conval = extratext(2, pos, cliptext)
            'tempbtn.actval = extratext(2, pos, cliptext)
            'tempbtn.enaStr = extratext(2, pos, cliptext)
            'tempbtn.disStr = extratext(2, pos, cliptext)

            'ProjectBtnData(i)(0).icon


            If isuse = True Then
                ProjectBtnUSE(i) = True
            Else
                ProjectBtnUSE(i) = False
                ProjectBtnData(i).Clear()
            End If

        Next


        For i = 0 To ProjectRequireDataUSE.Count - 1
            For j = 0 To ProjectRequireDataUSE(i).Count - 1
                Dim isuse As Boolean = True
                Dim _offsetNum As Long
                Select Case i
                    Case 0
                        _offsetNum = Val("&H" & ReadOffset("FG_PReqUnit")) + 2 * j
                    Case 1
                        _offsetNum = Val("&H" & ReadOffset("FG_PReqUpg")) + 2 * j
                    Case 2
                        _offsetNum = Val("&H" & ReadOffset("FG_PReqTechUpg")) + 2 * j
                    Case 3
                        _offsetNum = Val("&H" & ReadOffset("FG_PReqTechUse")) + 2 * j
                    Case 4
                        _offsetNum = Val("&H" & ReadOffset("FG_PReqOrder")) + 2 * j
                End Select

                '위치 파악
                MemStream.Position = _offsetNum - &H50C000
                Dim pos As UInt16 = binread.ReadUInt16
                'MsgBox(pos)
                ProjectRequireData(i)(j).pos = pos
                If RequireData(i)(j).pos <> pos Then '일치 하지 않을 경우
                    isuse = False '일치할 경우' 기본값 이용안함
                Else
                    isuse = True
                End If

                If pos <> 0 Then 'pos에 관한 설정
                    Select Case i
                        Case 0
                            _offsetNum = Val("&H" & ReadOffset("FG_ReqUnit")) + 2 * pos
                        Case 1
                            _offsetNum = Val("&H" & ReadOffset("FG_ReqUpg")) + 2 * pos
                        Case 2
                            _offsetNum = Val("&H" & ReadOffset("FG_ReqTechUpg")) + 2 * pos
                        Case 3
                            _offsetNum = Val("&H" & ReadOffset("FG_ReqTechUse")) + 2 * pos
                        Case 4
                            _offsetNum = Val("&H" & ReadOffset("FG_ReqOrder")) + 2 * pos
                    End Select
                    MemStream.Position = _offsetNum - &H50C000

                    Dim opcode As UInt16 = binread.ReadUInt16
                    While opcode <> &HFFFF

                        ProjectRequireData(i)(j).Code.Add(opcode)

                        If isuse = True Then
                            If opcode <> RequireData(i)(j).Code(ProjectRequireData(i)(j).Code.Count - 1) Then
                                isuse = False  '일치할 경우' 기본값 이용안함
                            End If
                        End If
                        opcode = binread.ReadUInt16
                    End While
                End If


                If isuse = True Then
                    ProjectRequireDataUSE(i)(j) = 0 '기본값 사용하기
                Else
                    ProjectRequireDataUSE(i)(j) = 3
                End If
            Next
        Next


        'FireGraft시작
        'ReadOffset()
        '5193A4


        binread.Close()
        MemStream.Close()
    End Sub

    ' This is executed on the UI thread after the work is complete.  It's a good place to either
    ' close the dialog or indicate that the initialization is complete.  It's safe to work with
    ' controls from this event.
    Private Sub WorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class