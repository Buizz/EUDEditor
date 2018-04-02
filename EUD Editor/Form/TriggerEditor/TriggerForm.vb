Public Class TriggerForm
    Public MainTrigger As Element

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
        Next

        ListBox2.Items.Clear()
        For i = 0 To MainTrigger.GetElements(1).GetElementsCount - 1
            ListBox2.Items.Add(MainTrigger.GetElements(1).GetElements(i).GetText)
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

    Private Sub Conbtnrefreash()
        If ListBox1.SelectedIndex = -1 Then
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
        Else
            With MainTrigger.GetElements(0).GetElements(ListBox1.SelectedIndex)
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
            If ListBox1.SelectedIndex = 0 Then
                Button5.Enabled = False
            Else
                Button5.Enabled = True
            End If


            If ListBox1.SelectedIndex = ListBox1.Items.Count - 1 Then
                Button6.Enabled = False
            Else
                Button6.Enabled = True
            End If
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Conbtnrefreash()
    End Sub

    Private Sub ConCopy(sender As Object, e As EventArgs) Handles Button3.Click
        With MainTrigger.GetElements(0)
            MainTrigger.GetElements(0).AddElements(ListBox1.SelectedIndex, .GetElements(ListBox1.SelectedIndex).Clone())
        End With

        ListBox1.Items.Insert(ListBox1.SelectedIndex, MainTrigger.GetElements(0).GetElements(ListBox1.SelectedIndex).GetText)
    End Sub

    Private Sub ConDelete(sender As Object, e As EventArgs) Handles Button4.Click
        Dim lastSelect As Integer = ListBox1.SelectedIndex
        MainTrigger.GetElements(0).GetElements(ListBox1.SelectedIndex).Delete()

        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        If lastSelect < ListBox1.Items.Count - 1 Then
            ListBox1.SelectedIndex = lastSelect
        Else
            ListBox1.SelectedIndex = ListBox1.Items.Count - 1
        End If
    End Sub

    Private Sub ConUp(sender As Object, e As EventArgs) Handles Button5.Click
        With MainTrigger.GetElements(0)
            MainTrigger.GetElements(0).AddElements(ListBox1.SelectedIndex - 1, .GetElements(ListBox1.SelectedIndex).Clone())
            .GetElements(ListBox1.SelectedIndex + 1).Delete()
        End With

        Dim temp As String = ListBox1.Items(ListBox1.SelectedIndex)

        ListBox1.Items(ListBox1.SelectedIndex) = ListBox1.Items(ListBox1.SelectedIndex - 1)
        ListBox1.Items(ListBox1.SelectedIndex - 1) = temp
        ListBox1.SelectedIndex -= 1
    End Sub

    Private Sub ConDown(sender As Object, e As EventArgs) Handles Button6.Click
        With MainTrigger.GetElements(0)
            MainTrigger.GetElements(0).AddElements(ListBox1.SelectedIndex + 2, .GetElements(ListBox1.SelectedIndex).Clone())
            .GetElements(ListBox1.SelectedIndex).Delete()
        End With

        Dim temp As String = ListBox1.Items(ListBox1.SelectedIndex)

        ListBox1.Items(ListBox1.SelectedIndex) = ListBox1.Items(ListBox1.SelectedIndex + 1)
        ListBox1.Items(ListBox1.SelectedIndex + 1) = temp
        ListBox1.SelectedIndex += 1
    End Sub


    Private Sub ConNew(sender As Object, e As EventArgs) Handles Button1.Click
        Dim _selectElement As Element = MainTrigger.GetElements(0)
        CondictionForm._varele = _selectElement
        CondictionForm.isNewCon = True

        CondictionForm._ele = New Element(_selectElement, ElementType.조건, 0)



        If CondictionForm.ShowDialog() = DialogResult.OK Then
            _selectElement.AddElements(ListBox1.SelectedIndex + 1, CondictionForm._ele)

            ListBox1.Items.Insert(ListBox1.SelectedIndex + 1, CondictionForm._ele.GetText)
            ListBox1.SelectedIndex += 1
        End If
    End Sub

    Private Sub ConFunc(sender As Object, e As EventArgs) Handles Button17.Click
        FunctionForm.FunEle = New Element(Nothing, ElementType.함수, {"Name"})
        Dim _selectElement As Element = MainTrigger.GetElements(0)
        My.Forms.FunctionForm.isNew = True
        FunctionForm._varele = _selectElement
        If FunctionForm.ShowDialog() = DialogResult.OK Then
            _selectElement.AddElements(ListBox1.SelectedIndex + 1, FunctionForm.FunEle)

            ListBox1.Items.Insert(ListBox1.SelectedIndex + 1, FunctionForm.FunEle.GetText)
            ListBox1.SelectedIndex += 1
        End If
    End Sub

    Private Sub ConEdit()
        Dim _selectElement As Element = MainTrigger.GetElements(0).GetElements(ListBox1.SelectedIndex)
        CondictionForm._varele = _selectElement
        CondictionForm.isNewCon = False

        CondictionForm._ele = MainTrigger.GetElements(0).GetElements(ListBox1.SelectedIndex).Clone(Nothing)



        If CondictionForm.ShowDialog() = DialogResult.OK Then
            _selectElement.con = CondictionForm._ele.con
            _selectElement.SetValue(CondictionForm._ele.Values.ToArray)

            ListBox1.Items(ListBox1.SelectedIndex) = CondictionForm._ele.GetText
        End If
    End Sub
    Private Sub funcEdit(colum As Byte)
        Dim _selectElement As Element
        If colum = 0 Then
            _selectElement = MainTrigger.GetElements(colum).GetElements(ListBox1.SelectedIndex)
        Else
            _selectElement = MainTrigger.GetElements(colum).GetElements(ListBox2.SelectedIndex)
        End If



        My.Forms.FunctionForm.FunEle = _selectElement.Clone(Nothing)
        My.Forms.FunctionForm._varele = _selectElement
        My.Forms.FunctionForm.isNew = False
        If My.Forms.FunctionForm.ShowDialog = DialogResult.OK Then
            _selectElement.SetValue(My.Forms.FunctionForm.FunEle.Values.ToArray)
            If colum = 0 Then
                ListBox1.Items(ListBox1.SelectedIndex) = _selectElement.GetText
            Else
                ListBox2.Items(ListBox2.SelectedIndex) = _selectElement.GetText
            End If
        End If
    End Sub

    Private Sub ConEditbtn(sender As Object, e As EventArgs) Handles Button2.Click
        With MainTrigger.GetElements(0).GetElements(ListBox1.SelectedIndex)
            Select Case .GetTypeV
                Case ElementType.조건
                    ConEdit()
                Case ElementType.함수
                    funcEdit(0)
            End Select
        End With


    End Sub



    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As EventArgs) Handles ListBox1.MouseDoubleClick
        If ListBox1.SelectedIndex <> -1 Then
            With MainTrigger.GetElements(0).GetElements(ListBox1.SelectedIndex)
                Select Case .GetTypeV
                    Case ElementType.조건
                        ConEdit()
                    Case ElementType.함수
                        funcEdit(0)
                End Select
            End With
        End If
    End Sub


    Private Sub ListBox2_MouseDoubleClick(sender As Object, e As EventArgs) Handles ListBox2.MouseDoubleClick
        If ListBox2.SelectedIndex <> -1 Then
            With MainTrigger.GetElements(1).GetElements(ListBox2.SelectedIndex)
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



    Private Sub Actbtnrefreash()
        If ListBox2.SelectedIndex = -1 Then
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

        Else
            With MainTrigger.GetElements(1).GetElements(ListBox2.SelectedIndex)
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
            If ListBox2.SelectedIndex = 0 Then
                Button11.Enabled = False
            Else
                Button11.Enabled = True
            End If


            If ListBox2.SelectedIndex = ListBox2.Items.Count - 1 Then
                Button12.Enabled = False
            Else
                Button12.Enabled = True
            End If
        End If
    End Sub
    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        Actbtnrefreash()
    End Sub

    Private Sub ActCopy(sender As Object, e As EventArgs) Handles Button9.Click
        With MainTrigger.GetElements(1)
            MainTrigger.GetElements(1).AddElements(ListBox2.SelectedIndex, .GetElements(ListBox2.SelectedIndex).Clone())
        End With

        ListBox2.Items.Insert(ListBox2.SelectedIndex, MainTrigger.GetElements(1).GetElements(ListBox2.SelectedIndex).GetText)
    End Sub

    Private Sub ActDelete(sender As Object, e As EventArgs) Handles Button10.Click
        Dim lastSelect As Integer = ListBox2.SelectedIndex
        MainTrigger.GetElements(1).GetElements(ListBox2.SelectedIndex).Delete()

        ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
        If lastSelect < ListBox2.Items.Count - 1 Then
            ListBox2.SelectedIndex = lastSelect
        Else
            ListBox2.SelectedIndex = ListBox2.Items.Count - 1
        End If
    End Sub

    Private Sub ActUp(sender As Object, e As EventArgs) Handles Button11.Click
        With MainTrigger.GetElements(1)
            MainTrigger.GetElements(1).AddElements(ListBox2.SelectedIndex - 1, .GetElements(ListBox2.SelectedIndex).Clone())
            .GetElements(ListBox2.SelectedIndex + 1).Delete()
        End With

        Dim temp As String = ListBox2.Items(ListBox2.SelectedIndex)

        ListBox2.Items(ListBox2.SelectedIndex) = ListBox2.Items(ListBox2.SelectedIndex - 1)
        ListBox2.Items(ListBox2.SelectedIndex - 1) = temp
        ListBox2.SelectedIndex -= 1
    End Sub

    Private Sub ActDown(sender As Object, e As EventArgs) Handles Button12.Click
        With MainTrigger.GetElements(1)
            MainTrigger.GetElements(1).AddElements(ListBox2.SelectedIndex + 2, .GetElements(ListBox2.SelectedIndex).Clone())
            .GetElements(ListBox2.SelectedIndex).Delete()
        End With

        Dim temp As String = ListBox2.Items(ListBox2.SelectedIndex)

        ListBox2.Items(ListBox2.SelectedIndex) = ListBox2.Items(ListBox2.SelectedIndex + 1)
        ListBox2.Items(ListBox2.SelectedIndex + 1) = temp
        ListBox2.SelectedIndex += 1
    End Sub


    Private Sub ActNew(sender As Object, e As EventArgs) Handles Button7.Click
        Dim _selectElement As Element = MainTrigger.GetElements(1)
        ActionForm._varele = _selectElement
        ActionForm.isNewAct = True

        ActionForm._ele = New Element(_selectElement, ElementType.액션, 0)



        If ActionForm.ShowDialog() = DialogResult.OK Then
            _selectElement.AddElements(ListBox2.SelectedIndex + 1, ActionForm._ele)

            ListBox2.Items.Insert(ListBox2.SelectedIndex + 1, ActionForm._ele.GetText)
            ListBox2.SelectedIndex += 1
        End If
    End Sub


    Private Sub ActEdit()
        Dim _selectElement As Element = MainTrigger.GetElements(1).GetElements(ListBox2.SelectedIndex)
        ActionForm._varele = _selectElement
        ActionForm.isNewAct = False

        ActionForm._ele = MainTrigger.GetElements(1).GetElements(ListBox2.SelectedIndex).Clone(Nothing)



        If ActionForm.ShowDialog() = DialogResult.OK Then
            _selectElement.act = ActionForm._ele.act
            _selectElement.SetValue(ActionForm._ele.Values.ToArray)

            ListBox2.Items(ListBox2.SelectedIndex) = ActionForm._ele.GetText
        End If
    End Sub


    Private Sub ActEditbtn(sender As Object, e As EventArgs) Handles Button8.Click
        With MainTrigger.GetElements(1).GetElements(ListBox2.SelectedIndex)
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
        FunctionForm.FunEle = New Element(Nothing, ElementType.함수, {"Name"})
        Dim _selectElement As Element = MainTrigger.GetElements(1)
        My.Forms.FunctionForm.isNew = True
        FunctionForm._varele = _selectElement
        If FunctionForm.ShowDialog() = DialogResult.OK Then
            _selectElement.AddElements(ListBox2.SelectedIndex + 1, FunctionForm.FunEle)

            ListBox2.Items.Insert(ListBox2.SelectedIndex + 1, FunctionForm.FunEle.GetText)
            ListBox2.SelectedIndex += 1
        End If
    End Sub
    Private Sub AddWait(isnew As Boolean)
        If isnew Then
            Dim _selectElement As Element = MainTrigger.GetElements(1)
            My.Forms.WaitDailog.NumericUpDown1.Value = 0
            If WaitDailog.ShowDialog = DialogResult.OK Then
                Dim waitele As Element = New Element(Nothing, ElementType.Wait, {WaitDailog.NumericUpDown1.Value})
                _selectElement.AddElements(ListBox2.SelectedIndex + 1, waitele)

                ListBox2.Items.Insert(ListBox2.SelectedIndex + 1, waitele.GetText)
                ListBox2.SelectedIndex += 1
            End If
        Else
            Dim _selectElement As Element = MainTrigger.GetElements(1).GetElements(ListBox2.SelectedIndex)
            My.Forms.WaitDailog.NumericUpDown1.Value = _selectElement.Values(0)
            If WaitDailog.ShowDialog = DialogResult.OK Then
                _selectElement.SetValue({WaitDailog.NumericUpDown1.Value})

                ListBox2.Items(ListBox2.SelectedIndex) = _selectElement.GetText
            End If
        End If
    End Sub
    Private Sub ActWaitbtn(sender As Object, e As EventArgs) Handles Button16.Click
        AddWait(True)
    End Sub
End Class