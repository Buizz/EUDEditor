Public Class GRPForm
    Dim Grp As New GRP

    Private Sub GRPForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)
        ProjectSet.saveStatus = False

        Size = New Size(221, 438)

        ListBox1.Items.Clear()

        For i = 0 To GRPEditorDATA.Count - 1
            ListBox1.Items.Add(GRPEditorDATA(i).SafeFilename)
        Next


        resetUsinglist()
    End Sub

    Private Sub resetUsinglist()
        ListBox2.Items.Clear()

        For i = 0 To GRPEditorUsingDATA.Count - 1
            If GRPEditorUsingDATA(i) <> "" Then
                ListBox2.Items.Add("Image " & Format(i, "0000") & ", " & GRPEditorUsingDATA(i))
            End If
        Next
    End Sub


    Private Function GetListNum() As Integer
        If ListBox1.SelectedIndex <> -1 Then
            For i = 0 To GRPEditorDATA.Count - 1
                If GRPEditorDATA(i).SafeFilename = ListBox1.SelectedItem Then
                    Return i
                End If
            Next
        End If
        Return 0
    End Function

    Private Sub InFile_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim dialog As DialogResult
        GRPForm_ListForm.Location = Me.Location
        dialog = GRPForm_ListForm.ShowDialog()
        If (dialog = DialogResult.OK) Or (GRPForm_ListForm.returnvalue >= 0) Then


            Dim GrpD As New GRPDATA
            GrpD.IsExternal = False


            'GRPForm_ListForm.returnvalue
            GrpD.Filename = "unit\" & CODE(DTYPE.grpfile)(GRPForm_ListForm.returnvalue).Replace("<0>", "") '"unit\neutral\civilian.grp"
            GrpD.SafeFilename = "unit\" & CODE(DTYPE.grpfile)(GRPForm_ListForm.returnvalue).Replace("<0>", "") '"unit\neutral\civilian.grp"
            GrpD.Remapping = 0
            GrpD.Palett = 4
            GrpD.usingimage = New List(Of Integer)



            If ListBox1.SelectedIndex <> -1 Then
                For i = 0 To GRPEditorDATA.Count - 1
                    If GRPEditorDATA(i).SafeFilename = GrpD.SafeFilename Then
                        MsgBox("이미 같은 파일이 들어가 있습니다!", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)

                        Exit Sub
                    End If
                Next
            End If



            ListBox1.Items.Add(GrpD.SafeFilename)
            GRPEditorDATA.Add(GrpD)



            ListBox1.SelectedIndex = ListBox1.Items.Count - 1
        End If
    End Sub

    Private Sub Import_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dialog As DialogResult
        dialog = OpenFileDialog1.ShowDialog


        If dialog = DialogResult.OK Then
            Dim GrpD As New GRPDATA
            GrpD.IsExternal = True
            GrpD.Filename = OpenFileDialog1.FileName
            GrpD.SafeFilename = OpenFileDialog1.SafeFileName
            GrpD.Remapping = 0
            GrpD.Palett = 4
            GrpD.usingimage = New List(Of Integer)


            If ListBox1.SelectedIndex <> -1 Then
                For i = 0 To GRPEditorDATA.Count - 1
                    If GRPEditorDATA(i).SafeFilename = GrpD.SafeFilename Then
                        MsgBox("이미 같은 파일이 들어가 있습니다!", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)

                        Exit Sub
                    End If
                Next
            End If

            If CheckGRPFile(GrpD.Filename) = False Then
                Exit Sub
            End If


            ListBox1.Items.Add(GrpD.SafeFilename)
                GRPEditorDATA.Add(GrpD)



                ListBox1.SelectedIndex = ListBox1.Items.Count - 1
            End If
    End Sub

    Private Sub Delete_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim index As Integer = ListBox1.SelectedIndex

        Dim num As Integer = GetListNum()

        If index <> -1 Then
            For i = 0 To GRPEditorUsingindexDATA.Count - 1
                If GRPEditorUsingindexDATA(i) = GRPEditorDATA(num).Filename Then
                    GRPEditorUsingindexDATA(i) = ""
                End If
            Next


            ListBox1.Items.RemoveAt((index))

            For i = 0 To GRPEditorDATA(num).usingimage.Count - 1
                GRPEditorUsingDATA(GRPEditorDATA(num).usingimage(i)) = ""
            Next

            GRPEditorDATA.RemoveAt(num)


            If ListBox1.Items.Count > index Then
                ListBox1.SelectedIndex = index
                Panel1.Enabled = True
                Size = New Size(806, 438)
            Else
                If ListBox1.Items.Count > 0 Then
                    ListBox1.SelectedIndex = ListBox1.Items.Count - 1
                    Panel1.Enabled = True
                    Size = New Size(806, 438)
                Else

                    Panel1.Enabled = False
                    Size = New Size(221, 438)
                    PictureBox8.Image = PictureBox8.InitialImage
                End If
            End If
        End If

        resetUsinglist()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim num As Integer = GetListNum()
        Dim mpq As New SFMpq

        If ListBox1.SelectedIndex <> -1 Then
            Panel1.Enabled = True
            Button2.Enabled = True
            Size = New Size(806, 438)

            Grp.Reset()

            ComboBox1.SelectedIndex = GRPEditorDATA(num).Palett
            ComboBox20.SelectedIndex = GRPEditorDATA(num).Remapping
            If GRPEditorDATA(num).IsExternal = True Then
                Grp.LoadGRP(GRPEditorDATA(num).Filename)
            Else
                Grp.LoadGRP(mpq.ReaddatFile(GRPEditorDATA(num).Filename))
            End If


            ListBox3.Items.Clear()
            listbox3index.Clear()

            For k = 0 To GRPEditorDATA(num).usingimage.Count - 1
                ListBox3.Items.Add(CODE(DTYPE.images)(GRPEditorDATA(num).usingimage(k)))
                listbox3index.Add(GRPEditorDATA(num).usingimage(k))
            Next





            ListBox4.Items.Clear()

            GroupBox1.Text = "GRP List  Count : " & Grp.framecount
            For j = 0 To Grp.framecount - 1
                ListBox4.Items.Add("Frame " & j)
                If j = 0 Then
                    ListBox4.SelectedIndex = 0
                End If
            Next
        Else
            Panel1.Enabled = False
            Size = New Size(221, 438)
            Button2.Enabled = False
        End If

    End Sub

    Private Sub Listbox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox4.SelectedIndexChanged
        Try
            If ComboBox20.SelectedIndex = 0 Then
                Grp.LoadPalette(ComboBox1.SelectedIndex)
            Else
                Grp.LoadPalette(ComboBox20.SelectedIndex + 8)
            End If
            Grp.DrawToPictureBox(PictureBox8, ListBox4.SelectedIndices(0), 0)
        Catch ex As Exception

        End Try
    End Sub



    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            If ComboBox20.SelectedIndex = 0 Then
                Grp.LoadPalette(ComboBox1.SelectedIndex)
            Else
                Grp.LoadPalette(ComboBox20.SelectedIndex + 8)
            End If
            Grp.DrawToPictureBox(PictureBox8, ListBox4.SelectedIndices(0), 0)
        Catch ex As Exception

        End Try

        Dim num As Integer = GetListNum()
        GRPEditorDATA(num).Palett = ComboBox1.SelectedIndex
    End Sub

    Private Sub ComboBox20_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox20.SelectedIndexChanged
        Try
            If ComboBox20.SelectedIndex = 0 Then
                Grp.LoadPalette(ComboBox1.SelectedIndex)
            Else
                Grp.LoadPalette(ComboBox20.SelectedIndex + 8)
            End If
            Grp.DrawToPictureBox(PictureBox8, ListBox4.SelectedIndices(0), 0)
        Catch ex As Exception

        End Try

        Dim num As Integer = GetListNum()
        GRPEditorDATA(num).Remapping = ComboBox20.SelectedIndex
    End Sub



    Private Sub AddFile_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim num As Integer = GetListNum()
        Dim dialog As DialogResult

        GRPFormUseGRP.grpcount = Grp.framecount


        dialog = GRPFormUseGRP.ShowDialog()
        If dialog = DialogResult.OK Or GRPFormUseGRP.returnvalue >= 0 Then

            Dim value As Integer = GRPFormUseGRP.returnvalue
            GRPEditorUsingDATA(value) = GRPEditorDATA(num).Filename

            ListBox3.Items.Add(CODE(DTYPE.images)(value))
            listbox3index.Add(value)

            GRPEditorDATA(num).usingimage.Add(value)
            resetUsinglist()
        End If
    End Sub

    Private listbox3index As New List(Of Integer)
    Private Sub DeleteFile_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim num As Integer = ListBox3.SelectedIndex
        Dim num2 As Integer = GetListNum()

        If ListBox3.SelectedIndex <> -1 Then
            For i = 0 To GRPEditorDATA(num2).usingimage.Count - 1
                If listbox3index(num) = GRPEditorDATA(num2).usingimage(i) Then
                    GRPEditorUsingDATA(GRPEditorDATA(num2).usingimage(i)) = ""
                    ListBox3.Items.RemoveAt(num)
                    listbox3index.RemoveAt(num)
                    GRPEditorDATA(num2).usingimage.RemoveAt(i)
                    resetUsinglist()
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim num As Integer = GetListNum()

        GRPFormUseIndex.grpname = GRPEditorDATA(num).Filename
        GRPFormUseIndex.ShowDialog()

    End Sub
End Class