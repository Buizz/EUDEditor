Public Class TriggerForm
    Public MainTrigger As Element


    Private Property GetListbox1Selectedindex As Integer
        Get
            If ListBox1.SelectedIndices.Count <> 0 Then
                Return ListBox1.SelectedIndices(0)
            Else
                Return -1
            End If
        End Get
        Set(value As Integer)
            If ListBox1.Items.Count <> 0 Then
                ListBox1.SelectedItems.Clear()
                ListBox1.Items(value).Selected = True
            End If
        End Set
    End Property

    Private Property GetListbox2Selectedindex As Integer
        Get
            If ListBox2.SelectedIndices.Count <> 0 Then
                Return ListBox2.SelectedIndices(0)
            Else
                Return -1
            End If
        End Get
        Set(value As Integer)
            If ListBox2.Items.Count <> 0 Then
                ListBox2.SelectedItems.Clear()
                ListBox2.Items(value).Selected = True
            End If
        End Set
    End Property



    Private Sub TriggerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        TabControl1.SelectedIndex = 0

        Dim playerflag As Integer = MainTrigger.Values(0)
        For i = 0 To 12
            If (playerflag And Math.Pow(2, i)) > 0 Then
                CheckedListBox1.SetItemChecked(i, True)
            Else
                CheckedListBox1.SetItemChecked(i, False)
            End If
        Next
        If playerflag = 0 Then
            Button13.Enabled = False
        Else
            Button13.Enabled = True
        End If




        ListBox1.Items.Clear()
        For i = 0 To MainTrigger.GetElements(0).GetElementsCount - 1
            ListBox1.Items.Add(MainTrigger.GetElements(0).GetElements(i).GetText)
            ListBox1.Items(i).Checked = Not (MainTrigger.GetElements(0).GetElements(i).isdisalbe)
        Next

        ListBox2.Items.Clear()
        For i = 0 To MainTrigger.GetElements(1).GetElementsCount - 1
            ListBox2.Items.Add(MainTrigger.GetElements(1).GetElements(i).GetText)
            ListBox2.Items(i).Checked = Not (MainTrigger.GetElements(1).GetElements(i).isdisalbe)
        Next
        Conbtnrefreash()
        Actbtnrefreash()
    End Sub


    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        Dim value As Integer = 0
        For i = 0 To 12
            If CheckedListBox1.GetItemChecked(i) Then
                value += Math.Pow(2, i)
            End If
        Next
        If value = 0 Then
            Button13.Enabled = False
        Else
            Button13.Enabled = True
        End If
        MainTrigger.Values(0) = value
    End Sub



    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.Focused Then
            Conbtnrefreash()
        End If
    End Sub

    '"Button1" "Cond(C)",
    '"Button17": "Func(F)",
    '"Button2": "Edit(E)",
    '"Button3": "Copy(C)",
    '"Button4": "Delete(Delete)",
    '"Button5": "Move Up(U)",
    '"Button6": "Move Down(O)",
    Private Sub ListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox1.KeyDown
        Select Case e.KeyCode
            Case Keys.N
                If Button1.Enabled = True Then
                    ConNew()
                End If
            Case Keys.F
                If Button17.Enabled = True Then
                    ConFunc()
                End If
            Case Keys.E
                If Button2.Enabled = True Then
                    With MainTrigger.GetElements(0).GetElements(GetListbox1Selectedindex)
                        Select Case .GetTypeV
                            Case ElementType.조건
                                ConEdit()
                            Case ElementType.함수
                                funcEdit(0)
                        End Select
                    End With
                End If
            Case Keys.C
                If Button3.Enabled = True Then
                    COnCopy()
                End If
            Case Keys.D
                If Button4.Enabled = True Then
                    ConDelete()
                End If
            Case Keys.U
                If Button5.Enabled = True Then
                    ConUp()
                End If
            Case Keys.O
                If Button6.Enabled = True Then
                    ConDown()
                End If
        End Select
    End Sub

    Private Sub ConCopybtn(sender As Object, e As EventArgs) Handles Button3.Click
        COnCopy()
    End Sub
    Private Sub ConDeletebtn(sender As Object, e As EventArgs) Handles Button4.Click
        ConDelete()
    End Sub

    Private Sub ConUpbtn(sender As Object, e As EventArgs) Handles Button5.Click
        ConUp()
    End Sub

    Private Sub ConDownbtn(sender As Object, e As EventArgs) Handles Button6.Click
        ConDown()
    End Sub

    Private Sub ConNewbtm(sender As Object, e As EventArgs) Handles Button1.Click
        ConNew()
    End Sub

    Private Sub ConFuncbtn(sender As Object, e As EventArgs) Handles Button17.Click
        ConFunc()
    End Sub

    Private Sub ConEditbtn(sender As Object, e As EventArgs) Handles Button2.Click
        With MainTrigger.GetElements(0).GetElements(GetListbox1Selectedindex)
            Select Case .GetTypeV
                Case ElementType.조건
                    ConEdit()
                Case ElementType.함수
                    funcEdit(0)
            End Select
        End With
    End Sub


    Private Sub COnCopy()
        For i = 0 To ListBox1.SelectedIndices.Count - 1
            With MainTrigger.GetElements(0)
                MainTrigger.GetElements(0).AddElements(ListBox1.SelectedIndices(ListBox1.SelectedIndices.Count - 1) + i + 1, .GetElements(ListBox1.SelectedIndices(i)).Clone())
            End With

            ListBox1.Items.Insert(ListBox1.SelectedIndices(ListBox1.SelectedIndices.Count - 1) + i + 1, MainTrigger.GetElements(0).GetElements(ListBox1.SelectedIndices(i)).GetText)
            ListBox1.Items(ListBox1.SelectedIndices(ListBox1.SelectedIndices.Count - 1) + i + 1).Checked = True
        Next


        ListBox1.Select()
        Conbtnrefreash()
    End Sub


    Private Sub ConDelete()
        Dim lastSelect As Integer = GetListbox1Selectedindex()
        While ListBox1.SelectedItems.Count <> 0
            MainTrigger.GetElements(0).GetElements(GetListbox1Selectedindex).Delete()

            ListBox1.Items.RemoveAt(GetListbox1Selectedindex)
        End While
        If lastSelect < ListBox1.Items.Count - 1 Then
            GetListbox1Selectedindex = lastSelect
        Else
            GetListbox1Selectedindex = ListBox1.Items.Count - 1
        End If
        ListBox1.Select()
        Conbtnrefreash()
    End Sub

    Private Sub ConUp()
        Dim templist As New List(Of Integer)

        For i = 0 To ListBox1.SelectedIndices.Count - 1
            templist.Add(ListBox1.SelectedIndices(i))
            With MainTrigger.GetElements(0)
                MainTrigger.GetElements(0).AddElements(ListBox1.SelectedIndices(i) - 1, .GetElements(ListBox1.SelectedIndices(i)).Clone())
                .GetElements(ListBox1.SelectedIndices(i) + 1).Delete()
            End With

            Dim temp As String = ListBox1.Items(ListBox1.SelectedIndices(i)).Text

            ListBox1.Items(ListBox1.SelectedIndices(i)).Text = ListBox1.Items(ListBox1.SelectedIndices(i) - 1).Text
            ListBox1.Items(ListBox1.SelectedIndices(i) - 1).Text = temp
            ' ListBox1.SelectedIndices(i) -= 1


        Next




        For i = 0 To templist.Count - 1
            ListBox1.Items(templist(i) - 1).Selected = True
            ListBox1.Items(templist(i)).Selected = False
        Next

        ListBox1.Select()
        Conbtnrefreash()
    End Sub

    Private Sub ConDown()
        Dim templist As New List(Of Integer)

        For i = ListBox1.SelectedIndices.Count - 1 To 0 Step -1
            templist.Add(ListBox1.SelectedIndices(i))
            With MainTrigger.GetElements(0)
                MainTrigger.GetElements(0).AddElements(ListBox1.SelectedIndices(i) + 2, .GetElements(ListBox1.SelectedIndices(i)).Clone())
                .GetElements(ListBox1.SelectedIndices(i)).Delete()
            End With

            Dim temp As String = ListBox1.Items(ListBox1.SelectedIndices(i)).Text

            ListBox1.Items(ListBox1.SelectedIndices(i)).Text = ListBox1.Items(ListBox1.SelectedIndices(i) + 1).Text
            ListBox1.Items(ListBox1.SelectedIndices(i) + 1).Text = temp
            ' ListBox1.SelectedIndices(i) -= 1
        Next

        For i = 0 To templist.Count - 1
            ListBox1.Items(templist(i) + 1).Selected = True
            ListBox1.Items(templist(i)).Selected = False
        Next


        ListBox1.Select()
        Conbtnrefreash()
    End Sub

    Private Sub ConNew()
        Dim _selectElement As Element = MainTrigger.GetElements(0)
        CondictionForm._varele = _selectElement
        CondictionForm.isNewCon = True

        CondictionForm._ele = New Element(_selectElement, ElementType.조건, 0)



        If CondictionForm.ShowDialog() = DialogResult.OK Then
            _selectElement.AddElements(GetListbox1Selectedindex() + 1, CondictionForm._ele)

            ListBox1.Items.Insert(GetListbox1Selectedindex() + 1, CondictionForm._ele.GetText)
            ListBox1.Items(GetListbox1Selectedindex + 1).Checked = True
            GetListbox1Selectedindex += 1
        End If
    End Sub

    Private Sub ConFunc()
        FunctionForm.FunEle = New Element(Nothing, ElementType.함수, {"Name"})
        Dim _selectElement As Element = MainTrigger.GetElements(0)
        My.Forms.FunctionForm.isNew = True
        FunctionForm._varele = _selectElement
        If FunctionForm.ShowDialog() = DialogResult.OK Then
            _selectElement.AddElements(GetListbox1Selectedindex() + 1, FunctionForm.FunEle)

            ListBox1.Items.Insert(GetListbox1Selectedindex() + 1, FunctionForm.FunEle.GetText)
            GetListbox1Selectedindex += 1
        End If
    End Sub

    Private Sub ConEdit()
        Dim _selectElement As Element = MainTrigger.GetElements(0).GetElements(GetListbox1Selectedindex)
        CondictionForm._varele = _selectElement
        CondictionForm.isNewCon = False

        CondictionForm._ele = MainTrigger.GetElements(0).GetElements(GetListbox1Selectedindex).Clone(Nothing)



        If CondictionForm.ShowDialog() = DialogResult.OK Then
            _selectElement.con = CondictionForm._ele.con
            _selectElement.SetValue(CondictionForm._ele.Values.ToArray)

            ListBox1.Items(GetListbox1Selectedindex).Text = CondictionForm._ele.GetText
        End If
    End Sub
    Private Sub funcEdit(colum As Byte)
        Dim _selectElement As Element
        If colum = 0 Then
            _selectElement = MainTrigger.GetElements(colum).GetElements(GetListbox1Selectedindex)
        Else
            _selectElement = MainTrigger.GetElements(colum).GetElements(GetListbox2Selectedindex)
        End If



        My.Forms.FunctionForm.FunEle = _selectElement.Clone(Nothing)
        My.Forms.FunctionForm._varele = _selectElement
        My.Forms.FunctionForm.isNew = False
        If My.Forms.FunctionForm.ShowDialog = DialogResult.OK Then
            _selectElement.SetValue(My.Forms.FunctionForm.FunEle.Values.ToArray)
            If colum = 0 Then
                ListBox1.Items(GetListbox1Selectedindex).Text = _selectElement.GetText
            Else
                ListBox2.Items(GetListbox2Selectedindex).Text = _selectElement.GetText
            End If
        End If
    End Sub


    Private Sub Conbtnrefreash()
        If GetListbox1Selectedindex() = -1 Then
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            If functions.GetElementsCount <> 0 Then
                Button17.Enabled = True
            Else
                Button17.Enabled = False
            End If
        ElseIf GetListbox1Selectedindex <= MainTrigger.GetElements(0).GetElementsCount Then
            With MainTrigger.GetElements(0).GetElements(GetListbox1Selectedindex)
                If .GetTypeV = ElementType.조건 Or .GetTypeV = ElementType.함수 Then
                    Button2.Enabled = True
                Else
                    Button2.Enabled = False
                End If
            End With

            Button3.Enabled = True


            If functions.GetElementsCount <> 0 Then
                Button17.Enabled = True
            Else
                Button17.Enabled = False
            End If


            Button4.Enabled = True
            If GetListbox1Selectedindex() = 0 Then
                Button5.Enabled = False
            Else
                Button5.Enabled = True
            End If


            If ListBox1.SelectedIndices(ListBox1.SelectedIndices.Count - 1) = ListBox1.Items.Count - 1 Then
                Button6.Enabled = False
            Else
                Button6.Enabled = True
            End If
        End If
    End Sub

    Private Sub Actbtnrefreash()
        If GetListbox2Selectedindex = -1 Then
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
            Button11.Enabled = False
            Button12.Enabled = False
            If functions.GetElementsCount <> 0 Then
                Button15.Enabled = True
            Else
                Button15.Enabled = False
            End If

        ElseIf GetListbox2Selectedindex <= MainTrigger.GetElements(1).GetElementsCount Then
            With MainTrigger.GetElements(1).GetElements(GetListbox2Selectedindex)
                If .GetTypeV = ElementType.액션 Or .GetTypeV = ElementType.Wait Or .GetTypeV = ElementType.함수 Then
                    Button8.Enabled = True
                Else
                    Button8.Enabled = False
                End If
            End With


            Button9.Enabled = True


            If functions.GetElementsCount <> 0 Then
                Button15.Enabled = True
            Else
                Button15.Enabled = False
            End If

            Button10.Enabled = True
            If GetListbox2Selectedindex = 0 Then
                Button11.Enabled = False
            Else
                Button11.Enabled = True
            End If


            If ListBox2.SelectedIndices(ListBox2.SelectedIndices.Count - 1) = ListBox2.Items.Count - 1 Then
                Button12.Enabled = False
            Else
                Button12.Enabled = True
            End If
        End If
    End Sub
    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.Focused Then
            Actbtnrefreash()
        End If
    End Sub


    Private Sub ListBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox2.KeyDown
        Select Case e.KeyCode
            Case Keys.N
                If Button7.Enabled = True Then
                    ActNew()
                End If
            Case Keys.F
                If Button15.Enabled = True Then
                    ActFunc()
                End If
            Case Keys.E
                If Button8.Enabled = True Then
                    With MainTrigger.GetElements(1).GetElements(GetListbox2Selectedindex)
                        Select Case .GetTypeV
                            Case ElementType.액션
                                ActEdit()
                            Case ElementType.Wait
                                AddWait(False)
                            Case ElementType.함수
                                funcEdit(1)
                        End Select
                    End With
                End If
            Case Keys.C
                If Button9.Enabled = True Then
                    ActCopy()
                End If
            Case Keys.D
                If Button10.Enabled = True Then
                    ActDelete()
                End If
            Case Keys.U
                If Button11.Enabled = True Then
                    ActUp()
                End If
            Case Keys.O
                If Button12.Enabled = True Then
                    ActDown()
                End If
            Case Keys.W
                If Button16.Enabled = True Then
                    AddWait(True)
                End If
        End Select
    End Sub

    Private Sub ActCopybtn(sender As Object, e As EventArgs) Handles Button9.Click
        ActCopy()
    End Sub

    Private Sub ActDeletebtn(sender As Object, e As EventArgs) Handles Button10.Click
        ActDelete()
    End Sub

    Private Sub ActUpbtn(sender As Object, e As EventArgs) Handles Button11.Click
        ActUp()
    End Sub

    Private Sub ActDownbtn(sender As Object, e As EventArgs) Handles Button12.Click
        ActDown()
    End Sub


    Private Sub ActNewbtn(sender As Object, e As EventArgs) Handles Button7.Click
        ActNew()
    End Sub

    Private Sub ActWaitbtn(sender As Object, e As EventArgs) Handles Button16.Click
        AddWait(True)
    End Sub

    Private Sub ActEditbtn(sender As Object, e As EventArgs) Handles Button8.Click
        With MainTrigger.GetElements(1).GetElements(GetListbox2Selectedindex)
            Select Case .GetTypeV
                Case ElementType.액션
                    ActEdit()
                Case ElementType.Wait
                    AddWait(False)
                Case ElementType.함수
                    funcEdit(1)
            End Select
        End With
    End Sub

    Private Sub ActFunc(sender As Object, e As EventArgs) Handles Button15.Click
        ActFunc()
    End Sub







    Private Sub ActCopy()
        For i = 0 To ListBox2.SelectedIndices.Count - 1
            With MainTrigger.GetElements(1)
                MainTrigger.GetElements(1).AddElements(ListBox2.SelectedIndices(ListBox2.SelectedIndices.Count - 1) + i + 1, .GetElements(ListBox2.SelectedIndices(i)).Clone())
            End With

            ListBox2.Items.Insert(ListBox2.SelectedIndices(ListBox2.SelectedIndices.Count - 1) + i + 1, MainTrigger.GetElements(1).GetElements(ListBox2.SelectedIndices(i)).GetText)
            ListBox2.Items(ListBox2.SelectedIndices(ListBox2.SelectedIndices.Count - 1) + i + 1).Checked = True
        Next


        ListBox2.Select()
        Actbtnrefreash()
    End Sub

    Private Sub ActDelete()
        Dim lastSelect As Integer = GetListbox2Selectedindex()
        While ListBox2.SelectedItems.Count <> 0
            MainTrigger.GetElements(1).GetElements(GetListbox2Selectedindex).Delete()

            ListBox2.Items.RemoveAt(GetListbox2Selectedindex)
        End While
        If lastSelect < ListBox2.Items.Count - 1 Then
            GetListbox2Selectedindex = lastSelect
        Else
            GetListbox2Selectedindex = ListBox2.Items.Count - 1
        End If
        ListBox2.Select()
        Actbtnrefreash()
    End Sub

    Private Sub ActUp()
        Dim templist As New List(Of Integer)
        For i = 0 To ListBox2.SelectedIndices.Count - 1
            templist.Add(ListBox2.SelectedIndices(i))

            With MainTrigger.GetElements(1)
                MainTrigger.GetElements(1).AddElements(ListBox2.SelectedIndices(i) - 1, .GetElements(ListBox2.SelectedIndices(i)).Clone())
                .GetElements(ListBox2.SelectedIndices(i) + 1).Delete()
            End With

            Dim temp As String = ListBox2.Items(ListBox2.SelectedIndices(i)).Text

            ListBox2.Items(ListBox2.SelectedIndices(i)).Text = ListBox2.Items(ListBox2.SelectedIndices(i) - 1).Text
            ListBox2.Items(ListBox2.SelectedIndices(i) - 1).Text = temp
            ' ListBox1.SelectedIndices(i) -= 1
        Next

        For i = 0 To templist.Count - 1
            ListBox2.Items(templist(i) - 1).Selected = True
            ListBox2.Items(templist(i)).Selected = False
        Next


        ListBox2.Select()
        Actbtnrefreash()
    End Sub

    Private Sub ActDown()
        Dim templist As New List(Of Integer)
        For i = ListBox2.SelectedIndices.Count - 1 To 0 Step -1
            templist.Add(ListBox2.SelectedIndices(i))
            With MainTrigger.GetElements(1)
                MainTrigger.GetElements(1).AddElements(ListBox2.SelectedIndices(i) + 2, .GetElements(ListBox2.SelectedIndices(i)).Clone())
                .GetElements(ListBox2.SelectedIndices(i)).Delete()
            End With

            Dim temp As String = ListBox2.Items(ListBox2.SelectedIndices(i)).Text

            ListBox2.Items(ListBox2.SelectedIndices(i)).Text = ListBox2.Items(ListBox2.SelectedIndices(i) + 1).Text
            ListBox2.Items(ListBox2.SelectedIndices(i) + 1).Text = temp
            ' ListBox1.SelectedIndices(i) -= 1
        Next

        For i = 0 To templist.Count - 1
            ListBox2.Items(templist(i) + 1).Selected = True
            ListBox2.Items(templist(i)).Selected = False
        Next


        ListBox2.Select()
        Actbtnrefreash()
    End Sub


    Private Sub ActNew()
        Dim _selectElement As Element = MainTrigger.GetElements(1)
        ActionForm._varele = _selectElement
        ActionForm.isNewAct = True

        ActionForm._ele = New Element(_selectElement, ElementType.액션, 0)



        If ActionForm.ShowDialog() = DialogResult.OK Then
            _selectElement.AddElements(GetListbox2Selectedindex + 1, ActionForm._ele)

            ListBox2.Items.Insert(GetListbox2Selectedindex + 1, ActionForm._ele.GetText)
            ListBox2.Items(GetListbox2Selectedindex + 1).Checked = True
            GetListbox2Selectedindex += 1
        End If
    End Sub


    Private Sub ActFunc()
        FunctionForm.FunEle = New Element(Nothing, ElementType.함수, {"Name"})
        Dim _selectElement As Element = MainTrigger.GetElements(1)
        My.Forms.FunctionForm.isNew = True
        FunctionForm._varele = _selectElement
        If FunctionForm.ShowDialog() = DialogResult.OK Then
            _selectElement.AddElements(GetListbox2Selectedindex + 1, FunctionForm.FunEle)

            ListBox2.Items.Insert(GetListbox2Selectedindex + 1, FunctionForm.FunEle.GetText)
            ListBox2.Items(GetListbox2Selectedindex + 1).Checked = True
            GetListbox2Selectedindex += 1
        End If
    End Sub


    Private Sub ActEdit()
        Dim _selectElement As Element = MainTrigger.GetElements(1).GetElements(GetListbox2Selectedindex)
        ActionForm._varele = _selectElement
        ActionForm.isNewAct = False

        ActionForm._ele = MainTrigger.GetElements(1).GetElements(GetListbox2Selectedindex).Clone(Nothing)



        If ActionForm.ShowDialog() = DialogResult.OK Then
            _selectElement.act = ActionForm._ele.act
            _selectElement.SetValue(ActionForm._ele.Values.ToArray)

            ListBox2.Items(GetListbox2Selectedindex).Text = ActionForm._ele.GetText
        End If
    End Sub







    Private Sub AddWait(isnew As Boolean)
        If isnew Then
            Dim _selectElement As Element = MainTrigger.GetElements(1)
            My.Forms.WaitDailog.NumericUpDown1.Value = 0
            If WaitDailog.ShowDialog = DialogResult.OK Then
                Dim waitele As Element = New Element(Nothing, ElementType.Wait, {WaitDailog.NumericUpDown1.Value})
                _selectElement.AddElements(GetListbox2Selectedindex + 1, waitele)

                ListBox2.Items.Insert(GetListbox2Selectedindex + 1, waitele.GetText)
                ListBox2.Items(GetListbox2Selectedindex + 1).Checked = True
                GetListbox2Selectedindex += 1
            End If
        Else
            Dim _selectElement As Element = MainTrigger.GetElements(1).GetElements(GetListbox2Selectedindex)
            My.Forms.WaitDailog.NumericUpDown1.Value = _selectElement.Values(0)
            If WaitDailog.ShowDialog = DialogResult.OK Then
                _selectElement.SetValue({WaitDailog.NumericUpDown1.Value})

                ListBox2.Items(GetListbox2Selectedindex).Text = _selectElement.GetText
            End If
        End If
    End Sub




    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        ListBox1.SelectedItems(0).Checked = Not ListBox1.SelectedItems(0).Checked

        If GetListbox1Selectedindex() <> -1 Then
            With MainTrigger.GetElements(0).GetElements(GetListbox1Selectedindex)
                Select Case .GetTypeV
                    Case ElementType.조건
                        ConEdit()
                    Case ElementType.함수
                        funcEdit(0)
                End Select
            End With
        End If
    End Sub


    Private Sub ListBox2_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox2.MouseDoubleClick
        ListBox2.SelectedItems(0).Checked = Not ListBox2.SelectedItems(0).Checked


        If GetListbox2Selectedindex <> -1 Then
            With MainTrigger.GetElements(1).GetElements(GetListbox2Selectedindex)
                Select Case .GetTypeV
                    Case ElementType.액션
                        ActEdit()
                    Case ElementType.Wait
                        AddWait(False)
                    Case ElementType.함수
                        funcEdit(1)
                End Select
            End With
        End If
    End Sub


    Private Sub ListBox1_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles ListBox1.ItemChecked
        MainTrigger.GetElements(0).GetElements(e.Item.Index).isdisalbe = Not e.Item.Checked
    End Sub

    Private Sub ListBox2_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles ListBox2.ItemChecked
        MainTrigger.GetElements(1).GetElements(e.Item.Index).isdisalbe = Not e.Item.Checked
    End Sub
End Class