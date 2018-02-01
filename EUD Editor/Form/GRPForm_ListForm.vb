Public Class GRPForm_ListForm
    Public returnvalue As Integer
    Public listnumber As Integer


    Private LISTFILTER As String

    Private LastSize As Integer
    Private Sub PaletteBtn(sender As Object, e As EventArgs) Handles Button5.Click
        If SplitContainer1.SplitterDistance = 24 Then
            SplitContainer1.Panel1MinSize = 93
            SplitContainer1.IsSplitterFixed = False
            SplitContainer1.SplitterDistance = LastSize '244
            Button5.Text = Lan.GetText(Me.Name, "Fold")
        Else
            LastSize = SplitContainer1.SplitterDistance
            SplitContainer1.Panel1MinSize = 24
            SplitContainer1.IsSplitterFixed = True
            SplitContainer1.SplitterDistance = 24
            Button5.Text = Lan.GetText(Me.Name, "UnFold")
        End If
    End Sub

    Private Sub GRPForm_ListForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)
        ListDraw()
        PaletDraw()
        Timer1.Enabled = True
    End Sub


    Private Sub ListBox1_DrawItem(ByVal sender As Object,
 ByVal e As System.Windows.Forms.DrawItemEventArgs) _
 Handles ListBox1.DrawItem
        If ListBox1.SelectedIndex <> -1 Then
            ' Draw the background of the ListBox control for each item.
            e.DrawBackground()

            ' Define the default color of the brush as black.
            Dim myBrush As Brush

            ' Determine the color of the brush to draw each item based on   
            ' the index of the item to draw.
            myBrush = Brushes.White
            'rect.Height -= 1
            If ListBox1.Items(e.Index)(2) = 1 Then
                'ToolStripStatusLabel1.Text = e.Index
                myBrush = Brushes.IndianRed
            End If



            ' Draw the current item text based on the current 
            ' Font and the custom brush settings.
            e.Graphics.DrawString(ListBox1.Items(e.Index)(0).ToString,
        e.Font, myBrush, e.Bounds, StringFormat.GenericDefault)

            ' If the ListBox has focus, draw a focus rectangle around  _ 
            ' the selected item.
            e.DrawFocusRectangle()
        End If

    End Sub
    Private Sub ListDraw()
        ListBox1.BeginUpdate()

        Dim listNum As Integer = listnumber
        Dim index As Integer = 0

        ListBox1.Items.Clear()

        For i = 0 To CODE(listNum).Count - 1
            index = i

            Dim temp, stra, strb As String
            Dim temp2(2) As String

            temp = CODE(listNum)(i)
            If temp <> "None" Then
                temp2(0) = temp
                temp2(1) = index
                temp2(2) = DatEditDATA(listNum).CheckChangeAll(i)


                temp2(0) = "[" & Format(i, "000") & "]- " & temp2(0)


                stra = temp2(0).ToLower
                If LISTFILTER <> "" Then
                    strb = LISTFILTER.ToLower
                Else
                    strb = ""
                End If
                If InStr(stra, strb) <> 0 Then
                    If CheckBox5.Checked = False Then
                        ListBox1.Items.Add(temp2)
                    Else
                        If temp2(2) = 1 Then
                            ListBox1.Items.Add(temp2)
                        End If
                    End If
                End If
            End If
        Next

        If ListBox1.Items.Count <> 0 Then
            ListBox1.SelectedIndex = 0
        End If


        ListBox1.EndUpdate()
    End Sub


    Private Sub PaletDraw()
        ListView1.BeginUpdate()
        ListView1.Items.Clear()
        Dim flingyNum, SpriteNum, ImageNum As Integer
        Dim size As Integer = ListBox1.Items.Count - 1

        For i = 0 To size
            Dim index As Integer = ListBox1.Items(i)(1)

            ListView1.Items.Add("")
            Dim itemindex As Integer = ListView1.Items.Count - 1
            Select Case listnumber
                Case DTYPE.units
                    flingyNum = DatEditDATA(DTYPE.units).ReadValue("Graphics", index)
                    SpriteNum = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", flingyNum)
                    ImageNum = DatEditDATA(DTYPE.sprites).ReadValue("Image File", SpriteNum)

                    ListView1.LargeImageList = DatEditForm.IMAGELIST
                    ListView1.Items(itemindex).ImageIndex = ImageNum
                Case DTYPE.weapons
                    ListView1.LargeImageList = DatEditForm.ICONILIST
                    Try
                        ListView1.Items(itemindex).ImageIndex = DatEditDATA(DTYPE.weapons).ReadValue("Icon", index)
                    Catch ex As Exception
                        ListView1.Items(itemindex).ImageIndex = 4
                    End Try
                Case DTYPE.flingy
                    SpriteNum = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", index)
                    ImageNum = DatEditDATA(DTYPE.sprites).ReadValue("Image File", SpriteNum)
                    ListView1.LargeImageList = DatEditForm.IMAGELIST
                    ListView1.Items(itemindex).ImageIndex = ImageNum
                Case DTYPE.sprites
                    ImageNum = DatEditDATA(DTYPE.sprites).ReadValue("Image File", index)
                    ListView1.LargeImageList = DatEditForm.IMAGELIST
                    ListView1.Items(itemindex).ImageIndex = ImageNum
                Case DTYPE.images
                    ListView1.LargeImageList = DatEditForm.IMAGELIST
                    ListView1.Items(itemindex).ImageIndex = index
                Case DTYPE.upgrades
                    ListView1.LargeImageList = DatEditForm.ICONILIST
                    Try
                        ListView1.Items(itemindex).ImageIndex = DatEditDATA(DTYPE.upgrades).ReadValue("Icon", index)
                    Catch ex As Exception
                        ListView1.Items(itemindex).ImageIndex = 4
                    End Try
                Case DTYPE.techdata
                    ListView1.LargeImageList = DatEditForm.ICONILIST
                    Try
                        ListView1.Items(itemindex).ImageIndex = DatEditDATA(DTYPE.techdata).ReadValue("Icon", index)
                    Catch ex As Exception
                        ListView1.Items(itemindex).ImageIndex = 4
                    End Try
                Case DTYPE.orders
                    ListView1.LargeImageList = DatEditForm.ICONILIST
                    Try
                        ListView1.Items(itemindex).ImageIndex = DatEditDATA(DTYPE.orders).ReadValue("Highlight", index)
                        If DatEditDATA(DTYPE.orders).ReadValue("Highlight", index) > 390 Then
                            ListView1.Items(itemindex).ImageIndex = 4
                        End If
                    Catch ex As Exception
                        ListView1.Items(itemindex).ImageIndex = 4
                    End Try
            End Select
            ListView1.Items(itemindex).Tag = index
        Next
        ListView1.EndUpdate()
        'ListView1.Clear()
        'ListView1.Items.Add(New ListView.ListViewItemCollection())
    End Sub

    Private Sub SELECTLIST(index As Integer)
        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(i)(1) = index Then
                ListBox1.SelectedIndex = i
            End If
        Next

    End Sub
    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.Click, ListView1.ItemSelectionChanged
        Try
            SELECTLIST(ListView1.SelectedItems(0).Tag)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Search_TextChanged(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyUp
        LISTFILTER = TextBox2.Text
        ListDraw()
        PaletDraw()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        listnumber = 0 '0
        ListDraw()
        PaletDraw()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        listnumber = 2  '2
        ListDraw()
        PaletDraw()
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        listnumber = 3  '3
        ListDraw()
        PaletDraw()
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        listnumber = 4  '4
        ListDraw()
        PaletDraw()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.Items.Count <> 0 Then

            Dim fristValue As UInteger = ListBox1.SelectedItem(1)
            Select Case listnumber
                Case 0
                    Dim unitgrp As Integer = DatEditDATA(DTYPE.units).ReadValue("Graphics", fristValue)
                    Dim sprite As Integer = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", unitgrp)
                    Dim imagefile As Integer = DatEditDATA(DTYPE.sprites).ReadValue("Image File", sprite)
                    returnvalue = DatEditDATA(DTYPE.images).ReadValue("GRP File", imagefile)

                Case 2
                    Dim sprite As Integer = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", fristValue)
                    Dim imagefile As Integer = DatEditDATA(DTYPE.sprites).ReadValue("Image File", sprite)
                    returnvalue = DatEditDATA(DTYPE.images).ReadValue("GRP File", imagefile)

                Case 3
                    Dim imagefile As Integer = DatEditDATA(DTYPE.sprites).ReadValue("Image File", fristValue)
                    returnvalue = DatEditDATA(DTYPE.images).ReadValue("GRP File", imagefile)

                Case 4
                    returnvalue = DatEditDATA(DTYPE.images).ReadValue("GRP File", fristValue)

            End Select
        End If
    End Sub


    Private Sub drawUnitGRP()
        If ListBox1.Items.Count <> 0 And returnvalue >= 0 Then


            Dim mpq As New SFMpq

            Dim ICONGRP As New GRP
            Dim remapping, imagevalue As Integer
            Dim fristValue As UInteger = ListBox1.SelectedItem(1)

            Select Case listnumber
                Case 0
                    Dim unitgrp As Integer = DatEditDATA(DTYPE.units).ReadValue("Graphics", fristValue)
                    Dim sprite As Integer = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", unitgrp)
                    imagevalue = DatEditDATA(DTYPE.sprites).ReadValue("Image File", sprite)

                Case 2
                    Dim sprite As Integer = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", fristValue)
                    imagevalue = DatEditDATA(DTYPE.sprites).ReadValue("Image File", sprite)

                Case 3
                    imagevalue = DatEditDATA(DTYPE.sprites).ReadValue("Image File", fristValue)

                Case 4
                    imagevalue = fristValue

            End Select
            remapping = DatEditDATA(DTYPE.images).ReadValue("Remapping", imagevalue)



            Select Case remapping
                Case 0
                    ICONGRP.LoadPalette(PalettType.install)
                Case 1
                    ICONGRP.LoadPalette(PalettType.ofire)
                Case 2
                    ICONGRP.LoadPalette(PalettType.gfire)
                Case 3
                    ICONGRP.LoadPalette(PalettType.bfire)
                Case 4
                    ICONGRP.LoadPalette(PalettType.bexpl)
            End Select



            ICONGRP.LoadGRP(mpq.ReaddatFile("unit\" & CODE(DTYPE.grpfile)(returnvalue).Replace("<0>", ""))) 'unit\protoss\dragoo

            If remapping = 0 Then
                ICONGRP.DrawToPictureBox(PictureBox8, frameNum, 12)
            Else
                ICONGRP.DrawToPictureBox(PictureBox8, frameNum, 0)
            End If



        End If
    End Sub

    Private frameNum As UInteger
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        drawUnitGRP()
        If frameNum < &HFFFFFFFE& Then
            frameNum += 1
        Else
            frameNum = 0
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        returnvalue = -1
        Close()
    End Sub


    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Close()
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        Close()
    End Sub
End Class