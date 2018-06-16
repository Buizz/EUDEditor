Public Class ListControl

    Public Event ItemClick(sender As Object, Index As Integer)
    Public Event ItemDoubleClick(sender As Object)
    Public Function Items(Index As Integer) As ListControlItem
        Return flpListBox.Controls(Index)
    End Function

    Public Sub Add(Trigger As Element)
        Dim c As New ListControlItem
        With c
            '' Assign an auto generated name
            .Name = "item" & flpListBox.Controls.Count + 1

            .Margin = New Padding(0)
            .Trigger = Trigger



            '
            '' set properties
            '.Song = Song
            '.Artist = Artist
            '.Album = Album
            '.Image = SongImage
            '.Lenght = Rating
        End With
        ' To check when the selection is changed
        AddHandler c.SelectionChanged, AddressOf SelectionChanged
        AddHandler c.Click, AddressOf ItemClicked
        AddHandler c.DoubleClick, AddressOf ItemDoubleClicked
        '
        flpListBox.Controls.Add(c)
        SetupAnchors()
    End Sub

    Public Sub Remove(Index As Integer)
        Dim c As ListControlItem = flpListBox.Controls(Index)
        Remove(c.Name)  ' call the below sub
    End Sub

    Public Sub Remove(name As String)
        ' grab which control is being removed
        Dim c As ListControlItem = flpListBox.Controls(name)
        flpListBox.Controls.Remove(c)
        ' remove the event hook
        RemoveHandler c.SelectionChanged, AddressOf SelectionChanged
        RemoveHandler c.Click, AddressOf ItemClicked
        RemoveHandler c.DoubleClick, AddressOf ItemDoubleClicked
        ' now dispose off properly
        c.Dispose()
        SetupAnchors()
    End Sub

    Public Sub Clear()
        Do
            If flpListBox.Controls.Count = 0 Then Exit Do
            Dim c As ListControlItem = flpListBox.Controls(0)
            flpListBox.Controls.Remove(c)
            ' remove the event hook
            RemoveHandler c.SelectionChanged, AddressOf SelectionChanged
            RemoveHandler c.Click, AddressOf ItemClicked
            RemoveHandler c.DoubleClick, AddressOf ItemDoubleClicked
            ' now dispose off properly
            c.Dispose()
        Loop
        mLastSelected = Nothing
    End Sub

    Public ReadOnly Property Count() As Integer
        Get
            Return flpListBox.Controls.Count
        End Get
    End Property

    Public Property SelectedIndex() As Integer
        Get
            For i = 0 To flpListBox.Controls.Count - 1
                Dim c As ListControlItem = flpListBox.Controls(i)
                If c.Selected = True Then
                    'Me.VScroll = False
                    Return i
                End If
            Next
            Return -1
        End Get
        Set(value As Integer)
            For i = 0 To flpListBox.Controls.Count - 1
                Dim c As ListControlItem = flpListBox.Controls(i)
                If value = i Then
                    c.Selected = True
                    flpListBox.ScrollControlIntoView(c)
                    mLastSelected = flpListBox.Controls(i)
                Else
                    c.Selected = False
                End If
            Next
        End Set
    End Property

    Private Sub SetupAnchors()
        If flpListBox.Controls.Count > 0 Then

            For i = 0 To flpListBox.Controls.Count - 1
                Dim c As Control = flpListBox.Controls(i)

                If i = 0 Then
                    ' Its the first control, all subsequent controls follow 
                    ' the anchor behavior of this control.
                    c.Anchor = AnchorStyles.Left + AnchorStyles.Top
                    c.Width = flpListBox.Width - SystemInformation.VerticalScrollBarWidth

                Else
                    ' It is not the first control. Set its anchor to
                    ' copy the width of the first control in the list.
                    c.Anchor = AnchorStyles.Left + AnchorStyles.Right

                End If

            Next

        End If
    End Sub

    Private Sub flpListBox_Resize(sender As Object, e As System.EventArgs) Handles flpListBox.Resize
        If flpListBox.Controls.Count Then
            flpListBox.Controls(0).Width = flpListBox.Width - SystemInformation.VerticalScrollBarWidth
        End If
    End Sub

    Dim mLastSelected As ListControlItem = Nothing
    Private Sub SelectionChanged(sender As Object)
        If mLastSelected IsNot Nothing Then
            mLastSelected.Selected = False
        End If
        mLastSelected = sender
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean
        Select Case keyData
            Case Keys.Up
                Debug.Print("Up")
                If SelectedIndex > 0 Then
                    SelectedIndex = SelectedIndex - 1
                End If

                Return True ' <-- If you want to suppress default handling of arrow keys

            Case Keys.Down
                Debug.Print("Down")
                If SelectedIndex < Count - 1 Then
                    SelectedIndex = SelectedIndex + 1
                End If

                Return True ' <-- If you want to suppress default handling of arrow keys

        End Select
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function



    Private Sub ItemClicked(sender As Object, e As System.EventArgs)
        RaiseEvent ItemClick(Me, flpListBox.Controls.IndexOfKey(sender.name))
    End Sub
    Private Sub ItemDoubleClicked(sender As Object, e As System.EventArgs)
        RaiseEvent ItemDoubleClick(Me)
    End Sub


End Class
