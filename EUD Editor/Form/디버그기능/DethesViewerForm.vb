Imports System.IO

Public Class DethesViewerForm
    Dim chagneUnitCount As UInt16


    Dim ischange(227) As Boolean

    Private Sub CheckValue()
        Dim offset As UInteger = &H58A364


        Dim bytes() As Byte = WinAPI.ReadValue(offset, 228 * 48)

        Dim memoryStr As New MemoryStream(bytes)
        Dim binaryReader As New BinaryReader(memoryStr)

        Dim changedUnit As UInt16 = 0
        For i = 0 To 227 'ListBox1.Items.Count - 1
            memoryStr.Position = i * 48


            ischange(i) = False
            For k = 0 To 11
                If binaryReader.ReadUInt32() <> 0 Then
                    ischange(i) = True
                    changedUnit += 1
                    Exit For
                End If
            Next
        Next
        If changedUnit <> chagneUnitCount Then
            chagneUnitCount = changedUnit
            LoadList()
        End If


        binaryReader.Close()
        memoryStr.Close()
    End Sub
    Private Sub CheckValueDirect()
        Dim offset As UInteger = &H58A364


        Dim bytes() As Byte = WinAPI.ReadValue(offset, 228 * 48)

        Dim memoryStr As New MemoryStream(bytes)
        Dim binaryReader As New BinaryReader(memoryStr)


        For i = 0 To ListBox1.Items.Count - 1
            memoryStr.Position = ListBox1.Items(i)(LITEM.index) * 48


            ListBox1.Items(i)(LITEM.ischange) = False
            For k = 0 To 11
                If binaryReader.ReadUInt32() <> 0 Then
                    ListBox1.Items(i)(LITEM.ischange) = True
                    Exit For
                End If
            Next
        Next



        binaryReader.Close()
        memoryStr.Close()
    End Sub




    Private Sub ColorReset()
        NumericUpDown1.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown1.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown2.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown2.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown3.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown3.ForeColor = ProgramSet.FORECOLOR


        NumericUpDown4.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown4.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown5.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown5.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown6.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown6.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown7.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown7.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown8.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown8.ForeColor = ProgramSet.FORECOLOR


        NumericUpDown9.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown9.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown10.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown10.ForeColor = ProgramSet.FORECOLOR


        NumericUpDown11.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown11.ForeColor = ProgramSet.FORECOLOR
        NumericUpDown12.BackColor = ProgramSet.BACKCOLOR
        NumericUpDown12.ForeColor = ProgramSet.FORECOLOR
    End Sub

    Private Sub LoadData()
        '58A364
        Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48


        Dim bytes() As Byte = WinAPI.ReadValue(offset, 48)

        Dim memoryStr As New MemoryStream(bytes)
        Dim binaryReader As New BinaryReader(memoryStr)


        If NumericUpDown1.Focused = False Then
            NumericUpDown1.Value = binaryReader.ReadUInt32()
            NumericUpDown1.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown1.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown1.BackColor = ProgramSet.FORECOLOR
            NumericUpDown1.ForeColor = ProgramSet.BACKCOLOR
            binaryReader.ReadUInt32()
        End If

        If NumericUpDown2.Focused = False Then
            NumericUpDown2.Value = binaryReader.ReadUInt32()
            NumericUpDown2.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown2.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown2.BackColor = ProgramSet.FORECOLOR
            NumericUpDown2.ForeColor = ProgramSet.BACKCOLOR
            binaryReader.ReadUInt32()
        End If

        If NumericUpDown3.Focused = False Then
            NumericUpDown3.Value = binaryReader.ReadUInt32()
            NumericUpDown3.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown3.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown3.BackColor = ProgramSet.FORECOLOR
            NumericUpDown3.ForeColor = ProgramSet.BACKCOLOR
            binaryReader.ReadUInt32()
        End If

        If NumericUpDown4.Focused = False Then
            NumericUpDown4.Value = binaryReader.ReadUInt32()
            NumericUpDown4.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown4.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown4.BackColor = ProgramSet.FORECOLOR
            NumericUpDown4.ForeColor = ProgramSet.BACKCOLOR
            binaryReader.ReadUInt32()
        End If

        If NumericUpDown5.Focused = False Then
            NumericUpDown5.Value = binaryReader.ReadUInt32()
            NumericUpDown5.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown5.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown5.BackColor = ProgramSet.FORECOLOR
            NumericUpDown5.ForeColor = ProgramSet.BACKCOLOR
            binaryReader.ReadUInt32()
        End If

        If NumericUpDown6.Focused = False Then
            NumericUpDown6.Value = binaryReader.ReadUInt32()
            NumericUpDown6.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown6.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown6.BackColor = ProgramSet.FORECOLOR
            NumericUpDown6.ForeColor = ProgramSet.BACKCOLOR
            binaryReader.ReadUInt32()
        End If

        If NumericUpDown7.Focused = False Then
            NumericUpDown7.Value = binaryReader.ReadUInt32()
            NumericUpDown7.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown7.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown7.BackColor = ProgramSet.FORECOLOR
            NumericUpDown7.ForeColor = ProgramSet.BACKCOLOR
            binaryReader.ReadUInt32()
        End If

        If NumericUpDown8.Focused = False Then
            NumericUpDown8.Value = binaryReader.ReadUInt32()
            NumericUpDown8.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown8.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown8.BackColor = ProgramSet.FORECOLOR
            NumericUpDown8.ForeColor = ProgramSet.BACKCOLOR
            binaryReader.ReadUInt32()
        End If

        If NumericUpDown9.Focused = False Then
            NumericUpDown9.Value = binaryReader.ReadUInt32()
            NumericUpDown9.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown9.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown9.BackColor = ProgramSet.FORECOLOR
            NumericUpDown9.ForeColor = ProgramSet.BACKCOLOR
            binaryReader.ReadUInt32()
        End If

        If NumericUpDown10.Focused = False Then
            NumericUpDown10.Value = binaryReader.ReadUInt32()
            NumericUpDown10.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown10.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown10.BackColor = ProgramSet.FORECOLOR
            NumericUpDown10.ForeColor = ProgramSet.BACKCOLOR
            binaryReader.ReadUInt32()
        End If

        If NumericUpDown11.Focused = False Then
            NumericUpDown11.Value = binaryReader.ReadUInt32()
            NumericUpDown11.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown11.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown11.BackColor = ProgramSet.FORECOLOR
            NumericUpDown11.ForeColor = ProgramSet.BACKCOLOR
            binaryReader.ReadUInt32()
        End If

        If NumericUpDown12.Focused = False Then
            NumericUpDown12.Value = binaryReader.ReadUInt32()
            NumericUpDown12.BackColor = ProgramSet.BACKCOLOR
            NumericUpDown12.ForeColor = ProgramSet.FORECOLOR
        Else
            NumericUpDown12.BackColor = ProgramSet.FORECOLOR
            NumericUpDown12.ForeColor = ProgramSet.BACKCOLOR
            binaryReader.ReadUInt32()
        End If


        binaryReader.Close()
        memoryStr.Close()
    End Sub

    Private Sub ListBox1_DrawItem(ByVal sender As Object,
ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles ListBox1.DrawItem

        If (e.Index < 0) Then Exit Sub

        Dim myBrush As Brush
        myBrush = Brushes.White

        If ListBox1.Items(e.Index)(LITEM.ischange) = True Then
            myBrush = Brushes.IndianRed
        End If



        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            e = New DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State Xor DrawItemState.Selected, e.ForeColor,
        Color.DarkRed)
        End If


        e.DrawBackground()


        e.Graphics.DrawString(ListBox1.Items(e.Index)(LITEM.Name),
        e.Font, myBrush, e.Bounds, StringFormat.GenericDefault)


        e.DrawFocusRectangle()

    End Sub

    Private Sub PaletDraw()
        ListView1.BeginUpdate()
        ListView1.Items.Clear()
        Dim flingyNum, SpriteNum, ImageNum As Integer
        Dim size As Integer = ListBox1.Items.Count - 1
        For i = 0 To size
            Dim index As Integer = ListBox1.Items(i)(LITEM.index)

            ListView1.Items.Add("")
            Dim itemindex As Integer = ListView1.Items.Count - 1
            flingyNum = DatEditDATA(DTYPE.units).ReadValue("Graphics", index)
            SpriteNum = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", flingyNum)
            ImageNum = DatEditDATA(DTYPE.sprites).ReadValue("Image File", SpriteNum)

            ListView1.LargeImageList = DatEditForm.IMAGELIST
            ListView1.Items(itemindex).ImageIndex = ImageNum
            ListView1.Items(itemindex).Tag = index
        Next
        ListView1.EndUpdate()


        'ListView1.Clear()
        'ListView1.Items.Add(New ListView.ListViewItemCollection())
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        LoadList()
        PaletDraw()
    End Sub

    Private LastSize As Integer
    Private Sub PaletteBtn(sender As Object, e As EventArgs) Handles Button5.Click
        If SplitContainer1.SplitterDistance = 24 Then
            SplitContainer1.Panel1MinSize = 93
            SplitContainer1.IsSplitterFixed = False
            SplitContainer1.SplitterDistance = LastSize '244
            Button5.Text = "접기"
        Else
            LastSize = SplitContainer1.SplitterDistance
            SplitContainer1.Panel1MinSize = 24
            SplitContainer1.IsSplitterFixed = True
            SplitContainer1.SplitterDistance = 24
            Button5.Text = "펼치기"
        End If
    End Sub
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.KeyUp
        LISTFILTER = TextBox2.Text

        LoadList()
        PaletDraw()
    End Sub

    Private Sub SELECTLIST(index As Integer)
        ListBox1.SelectedIndex = -1

        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(i)(LITEM.index) = index Then
                ListBox1.SelectedIndex = i
                _OBJECTNUM = index
                Exit Sub
            End If
        Next


        If ListBox1.SelectedIndex = -1 Then
            If ListBox1.Items.Count <> 0 Then
                ListBox1.SelectedIndex = 0
                _OBJECTNUM = ListBox1.Items(0)(LITEM.index)
                Exit Sub
            End If
        End If

        _OBJECTNUM = 0
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex <> -1 Then
            _OBJECTNUM = ListBox1.SelectedItem(LITEM.index)

            LoadData()
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.Click, ListView1.ItemSelectionChanged
        Try
            SELECTLIST(ListView1.SelectedItems(0).Tag)
        Catch ex As Exception
        End Try
    End Sub

    Enum LITEM
        ischange = 0
        index = 1
        Name = 2
    End Enum

    Dim LISTFILTER As String
    Dim _OBJECTNUM As Integer
    Private Sub LoadList()
        Dim lastSELECT As Integer = _OBJECTNUM


        ListBox1.BeginUpdate()

        ListBox1.Items.Clear()

        CheckValue()

        For i = 0 To CODE(DTYPE.units).Count - 1
            Dim list(2) As String

            Dim temp As String = CODE(DTYPE.units)(i)
            If DatEditDATA(DTYPE.units).ReadValue("Unit Map String", i) = 0 Then
                list(LITEM.Name) = temp
            Else
                Try
                    list(LITEM.Name) = ProjectSet.CHKSTRING(-1 + DatEditDATA(DTYPE.units).ReadValue("Unit Map String", i)) & " (" & temp & ")" 'ProjectSet.UNITSTR(index)
                Catch ex As Exception
                    list(LITEM.Name) = temp
                End Try

            End If
            list(LITEM.index) = i
            list(LITEM.ischange) = ischange(i)
            list(LITEM.Name) = "[" & Format(i, "000") & "]- " & list(LITEM.Name)




            Dim stra, strb As String
            stra = list(LITEM.Name).ToLower
            If LISTFILTER <> "" Then
                strb = LISTFILTER.ToLower
            Else
                strb = ""
            End If


            If CheckBox5.Checked = True Then
                If list(LITEM.ischange) = True And InStr(stra, strb) <> 0 Then
                    ListBox1.Items.Add(list)
                End If
            Else
                If InStr(stra, strb) <> 0 Then
                    ListBox1.Items.Add(list)
                End If
            End If
        Next


        SELECTLIST(lastSELECT)





        ListBox1.EndUpdate()
    End Sub

    Private Sub DethesViewerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
        LoadList()
        PaletDraw()
        ColorReset()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        CheckValueDirect()
        CheckValue()
        LoadData()
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If NumericUpDown1.Focused Then
            Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48

            WinAPI.Write(offset, CUInt(NumericUpDown1.Value))
        End If
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        If NumericUpDown2.Focused Then
            Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48 + 4

            WinAPI.Write(offset, CUInt(NumericUpDown2.Value))
        End If
    End Sub

    Private Sub NumericUpDown3_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown3.ValueChanged
        If NumericUpDown3.Focused Then
            Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48 + 8

            WinAPI.Write(offset, CUInt(NumericUpDown3.Value))
        End If
    End Sub

    Private Sub NumericUpDown4_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown4.ValueChanged
        If NumericUpDown4.Focused Then
            Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48 + 12

            WinAPI.Write(offset, CUInt(NumericUpDown4.Value))
        End If
    End Sub

    Private Sub NumericUpDown5_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown5.ValueChanged
        If NumericUpDown5.Focused Then
            Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48 + 16

            WinAPI.Write(offset, CUInt(NumericUpDown5.Value))
        End If
    End Sub

    Private Sub NumericUpDown6_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown6.ValueChanged
        If NumericUpDown6.Focused Then
            Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48 + 20

            WinAPI.Write(offset, CUInt(NumericUpDown6.Value))
        End If
    End Sub

    Private Sub NumericUpDown7_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown7.ValueChanged
        If NumericUpDown7.Focused Then
            Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48 + 24

            WinAPI.Write(offset, CUInt(NumericUpDown7.Value))
        End If
    End Sub

    Private Sub NumericUpDown8_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown8.ValueChanged
        If NumericUpDown8.Focused Then
            Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48 + 28

            WinAPI.Write(offset, CUInt(NumericUpDown8.Value))
        End If
    End Sub

    Private Sub NumericUpDown9_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown9.ValueChanged
        If NumericUpDown9.Focused Then
            Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48 + 32

            WinAPI.Write(offset, CUInt(NumericUpDown9.Value))
        End If
    End Sub

    Private Sub NumericUpDown10_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown10.ValueChanged
        If NumericUpDown10.Focused Then
            Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48 + 36

            WinAPI.Write(offset, CUInt(NumericUpDown10.Value))
        End If
    End Sub

    Private Sub NumericUpDown11_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown11.ValueChanged
        If NumericUpDown11.Focused Then
            Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48 + 40

            WinAPI.Write(offset, CUInt(NumericUpDown11.Value))
        End If
    End Sub

    Private Sub NumericUpDown12_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown12.ValueChanged
        If NumericUpDown12.Focused Then
            Dim offset As UInteger = &H58A364 + _OBJECTNUM * 48 + 44

            WinAPI.Write(offset, CUInt(NumericUpDown12.Value))
        End If
    End Sub
End Class