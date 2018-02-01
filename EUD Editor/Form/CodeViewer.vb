Imports System.IO

Public Class CodeViewer

    Public mode As String

    Public ObjectName As String
    Public ObjectNum As Integer
    Public ObjectTab As Integer
    Public BtnCount As Integer


    Public Value As Integer = 0
    Public listNum As Integer = 1


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

    Private LISTFILTER As String
    Private Sub ListDraw()
        ListBox1.BeginUpdate()


        Dim index As Integer = 0


        ListBox1.Items.Clear()

        For i = 0 To CODE(listNum).Count - 1
            index = i

            Dim temp, stra, strb As String
            Dim temp2(2) As String

            temp = CODE(listNum)(i)
            If temp <> "None" Then
                If listNum = DTYPE.units Then
                    If DatEditDATA(DTYPE.units).ReadValue("Unit Map String", index) = 0 Then
                        temp2(0) = temp
                    Else
                        Try
                            temp2(0) = ProjectSet.CHKSTRING(-1 + DatEditDATA(DTYPE.units).ReadValue("Unit Map String", i)) & " (" & temp & ")" 'ProjectSet.UNITSTR(index)
                        Catch ex As Exception
                            temp2(0) = "FailToLoadString" 'ProjectSet.UNITSTR(index)
                        End Try

                    End If
                    temp2(1) = index
                    temp2(2) = DatEditDATA(DTYPE.units).CheckChangeAll(i)
                Else
                    temp2(0) = temp
                    temp2(1) = index
                    If listNum < 12 Then
                        temp2(2) = DatEditDATA(listNum).CheckChangeAll(i)
                    End If
                End If
                temp2(0) = "[" & Format(i, "000") & "]- " & temp2(0)


                'Select Case TAB_INDEX
                '    Case DTYPE.sfxdata
                '        Dim value As Integer = DatEditDATA(DTYPE.sfxdata).ReadValue("Sound File", i)

                '        If ListBox6.Items.Contains("sound\" & ComboBox53.Items(value).ToString.ToLower) Then

                '            temp2(2) = 1
                '        End If
                '    Case DTYPE.images
                '        If GRPEditorUsingDATA(i) <> "" Then
                '            temp2(2) = 1
                '        End If
                'End Select


                stra = temp2(0).ToLower
                If LISTFILTER <> "" Then
                    strb = LISTFILTER.ToLower
                Else
                    strb = ""
                End If
                If InStr(stra, strb) <> 0 Then
                    ListBox1.Items.Add(temp2)
                End If
            End If
        Next

        'SELECTLIST(_OBJECTNUM)
        If ListBox1.SelectedIndex = -1 And ListBox1.Items.Count <> 0 Then
            ListBox1.SelectedIndex = 0
        End If



        ListBox1.EndUpdate()
    End Sub


    Private Sub LoadListviewFromFile(ByRef Listview As ListView, filename As String)
        filename = My.Application.Info.DirectoryPath & "\Data\" & filename
        Dim file As FileStream = New FileStream(filename, FileMode.Open, FileAccess.Read)
        Dim stream As StreamReader = New StreamReader(file, System.Text.Encoding.Default)

        Listview.Items.Clear()
        While (stream.EndOfStream = False)
            Listview.Items.Add(stream.ReadLine)
        End While

        stream.Close()
        file.Close()
    End Sub


    Private Sub CodeViewer_LostFocus(sender As Object, e As EventArgs) Handles Me.Deactivate
        Close()
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        Close()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Close()
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


    Private Sub PaletDraw()
        ListView1.BeginUpdate()
        ListView1.Items.Clear()
        Dim flingyNum, SpriteNum, ImageNum As Integer
        Dim size As Integer = ListBox1.Items.Count - 1
        If listNum = DTYPE.weapons Or listNum = DTYPE.techdata Or
            listNum = DTYPE.orders Then
            size -= 1
        End If
        For i = 0 To size
            Dim index As Integer = ListBox1.Items(i)(1)

            ListView1.Items.Add("")
            Dim itemindex As Integer = ListView1.Items.Count - 1
            Select Case listNum
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
                Case DTYPE.icon
                    ListView1.LargeImageList = DatEditForm.ICONILIST
                    Try
                        ListView1.Items(itemindex).ImageIndex = index
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

    Private LoadStatus As Boolean = True
    Private Sub CodeViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        LoadStatus = False
        ListDraw()
        PaletDraw()

        LoadStatus = True
        SELECTLIST(Value)

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

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If LoadStatus Then
            ListSelected()
        End If


    End Sub
    Private Sub ListSelected()
        If ListBox1.Items.Count <> 0 Then
            Value = ListBox1.SelectedItem(1)

            If mode = "Dat" Then
                DatEditDATA(ObjectTab).WriteValue(ObjectName, ObjectNum, Value)
                DatEditForm.LoadData()
            ElseIf mode = "Fire" Then
                ProjectBtnData(ObjectNum)(BtnCount).icon = Value
                FireGraftForm.TextBox5.Text = Value
            ElseIf mode = "BtnSet" Then

                BtnSettingForm.TextBox5.Text = Value
            End If
        Else
            Value = 0
        End If
    End Sub

    Private Sub Search_TextChanged(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyUp
        LISTFILTER = TextBox2.Text
        ListDraw()
        PaletDraw()
    End Sub

End Class