Imports System.Drawing.Drawing2D

Public Class ListControlItem

    Public Event SelectionChanged(sender As Object)

    Friend WithEvents tmrMouseLeave As New System.Windows.Forms.Timer With {.Interval = 10}

#Region "Properties"
    Dim mTrigger As Element = Nothing
    Public Property Trigger() As Element
        Get
            Return mTrigger
        End Get
        Set(ByVal value As Element)
            mTrigger = value
            Refresh()
        End Set
    End Property

    Private mSelected As Boolean
    Public Property Selected() As Boolean
        Get
            Return mSelected
        End Get
        Set(ByVal value As Boolean)
            mSelected = value
            Refresh()
        End Set
    End Property

    Dim mLenght As Integer = 150
    Public Property Lenght() As Integer
        Get
            Return mLenght
        End Get
        Set(ByVal value As Integer)
            mLenght = value
            Refresh()
        End Set
    End Property

#End Region

#Region "Mouse coding"

    Private Enum MouseCapture
        Outside
        Inside
    End Enum
    Private Enum ButtonState
        ButtonUp
        ButtonDown
        Disabled
    End Enum
    Dim bState As ButtonState
    Dim bMouse As MouseCapture



    Private Sub ListControlItem_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        If Selected = False Then
            Selected = True
            RaiseEvent SelectionChanged(Me)
        End If
    End Sub

    Private Sub metroRadioGroup_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown ', rdButton.MouseDown
        bState = ButtonState.ButtonDown
        Refresh()
    End Sub

    Private Sub metroRadioGroup_MouseEnter(sender As Object, e As System.EventArgs) Handles Me.MouseEnter
        bMouse = MouseCapture.Inside
        tmrMouseLeave.Start()
        Refresh()
    End Sub

    Private Sub metroRadioGroup_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp ', rdButton.MouseUp
        bState = ButtonState.ButtonUp
        Refresh()
    End Sub

    Private Sub tmrMouseLeave_Tick(sender As System.Object, e As System.EventArgs) Handles tmrMouseLeave.Tick
        Try
            Dim scrPT = Control.MousePosition
            Dim ctlPT As Point = Me.PointToClient(scrPT)
            '
            If ctlPT.X < 0 Or ctlPT.Y < 0 Or ctlPT.X > Me.Width Or ctlPT.Y > Me.Height Then
                ' Stop timer
                tmrMouseLeave.Stop()
                bMouse = MouseCapture.Outside
                Refresh()
            Else
                bMouse = MouseCapture.Inside
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Painting"

    Private Sub Paint_DrawBackground(gfx As Graphics)
        Me.Height = mLenght
        Dim rect As New Rectangle(0, 0, Me.Width - 1, Me.Height)

        '/// Build a rounded rectangle
        Dim p As New GraphicsPath
        Const Roundness = 1
        p.StartFigure()
        p.AddArc(New Rectangle(rect.Left, rect.Top, Roundness, Roundness), 180, 90)
        p.AddLine(rect.Left + Roundness, 0, rect.Right - Roundness, 0)
        p.AddArc(New Rectangle(rect.Right - Roundness, 0, Roundness, Roundness), -90, 90)
        p.AddLine(rect.Right, Roundness, rect.Right, rect.Bottom - Roundness)
        p.AddArc(New Rectangle(rect.Right - Roundness, rect.Bottom - Roundness, Roundness, Roundness), 0, 90)
        p.AddLine(rect.Right - Roundness, rect.Bottom, rect.Left + Roundness, rect.Bottom)
        p.AddArc(New Rectangle(rect.Left, rect.Height - Roundness, Roundness, Roundness), 90, 90)
        p.CloseFigure()


        '/// Draw the background ///
        Dim ColorScheme As Color() = Nothing
        Dim brdr As SolidBrush = Nothing

        If bState = ButtonState.Disabled Then
            ' normal
            brdr = ColorSchemes.DisabledBorder
            ColorScheme = ColorSchemes.DisabledAllColor
        Else
            If mSelected Then
                ' Selected
                brdr = ColorSchemes.SelectedBorder

                If bState = ButtonState.ButtonUp And bMouse = MouseCapture.Outside Then
                    ' normal
                    ColorScheme = ColorSchemes.SelectedNormal

                ElseIf bState = ButtonState.ButtonUp And bMouse = MouseCapture.Inside Then
                    '  hover 
                    ColorScheme = ColorSchemes.SelectedHover

                ElseIf bState = ButtonState.ButtonDown And bMouse = MouseCapture.Outside Then
                    ' no one cares!
                    Exit Sub
                ElseIf bState = ButtonState.ButtonDown And bMouse = MouseCapture.Inside Then
                    ' pressed
                    ColorScheme = ColorSchemes.SelectedPressed
                End If

            Else
                ' Not selected
                brdr = ColorSchemes.UnSelectedBorder

                If bState = ButtonState.ButtonUp And bMouse = MouseCapture.Outside Then
                    ' normal
                    brdr = ColorSchemes.DisabledBorder
                    ColorScheme = ColorSchemes.UnSelectedNormal

                ElseIf bState = ButtonState.ButtonUp And bMouse = MouseCapture.Inside Then
                    '  hover 
                    ColorScheme = ColorSchemes.UnSelectedHover

                ElseIf bState = ButtonState.ButtonDown And bMouse = MouseCapture.Outside Then
                    ' no one cares!
                    Exit Sub
                ElseIf bState = ButtonState.ButtonDown And bMouse = MouseCapture.Inside Then
                    ' pressed
                    ColorScheme = ColorSchemes.UnSelectedPressed
                End If

            End If
        End If

        ' Draw
        Dim b As LinearGradientBrush = New LinearGradientBrush(rect, Color.White, Color.Black, LinearGradientMode.Vertical)
        Dim blend As ColorBlend = New ColorBlend
        blend.Colors = ColorScheme
        blend.Positions = New Single() {0.0F, 0.1, 0.9F, 0.95F, 1.0F}
        b.InterpolationColors = blend
        gfx.FillPath(b, p)

        '// Draw border
        gfx.DrawPath(New Pen(brdr), p)

        '// Draw bottom border if Normal state (not hovered)
        If bMouse = MouseCapture.Outside Then
            rect = New Rectangle(rect.Left, Me.Height, rect.Width, 1)
            b = New LinearGradientBrush(rect, Color.Blue, Color.Yellow, LinearGradientMode.Horizontal)
            blend = New ColorBlend
            blend.Colors = New Color() {Color.White, Color.LightGray, Color.White}
            blend.Positions = New Single() {0.0F, 0.5F, 1.0F}
            b.InterpolationColors = blend
            '
            gfx.FillRectangle(b, rect)
        End If
    End Sub

    Private Const lineheight As Byte = 15
    Private Const MAX_LINE As Byte = 15
    Private Sub Paint_DrawButton(gfx As Graphics)
        Dim linecount As Byte = 0


        Dim fnt As Font = Nothing
        Dim sz As SizeF = Nothing
        Dim layoutRect As RectangleF
        Dim SF As New StringFormat With {.Trimming = StringTrimming.EllipsisCharacter}
        Dim workingRect As New Rectangle(40, 0, Me.Width, Me.Height)


        Dim LinePixel As Integer = 0


        Dim isCommentTrigger As Boolean = False
        Dim CommentMsg As String = ""

        '주석이 있는지 체크
        For Each act As Element In Trigger.GetElements(1).GetElementList
            If act.GetTypeV = ElementType.액션 Then
                If act.act.Name = "Comment" Then
                    CommentMsg = act.Values(0)
                    isCommentTrigger = True
                End If
            End If
        Next


        fnt = Me.Font
        sz = gfx.MeasureString("DisableTrigger ======", fnt)
        layoutRect = New RectangleF(20, LinePixel, workingRect.Width, sz.Height)
        If Trigger.isdisalbe Then
            gfx.DrawString("DisableTrigger ======", fnt, Brushes.DarkRed, layoutRect, SF)
        Else
            gfx.DrawString("Enable Trigger ======", fnt, Brushes.Green, layoutRect, SF)
        End If
        LinePixel += lineheight
        linecount += 1
        CheckBox1.Checked = Not Trigger.isdisalbe

        If isCommentTrigger Then
            fnt = Me.Font
            sz = gfx.MeasureString(CommentMsg, fnt)
            layoutRect = New RectangleF(10, LinePixel, workingRect.Width, sz.Height)
            gfx.DrawString(CommentMsg, fnt, Brushes.Red, layoutRect, SF)
            LinePixel += lineheight
            linecount += 1
        Else
            ' condiction ment
            fnt = Me.Font
            sz = gfx.MeasureString("CONDITIONS:", fnt)
            layoutRect = New RectangleF(10, LinePixel, workingRect.Width, sz.Height)
            gfx.DrawString("CONDITIONS:", fnt, Brushes.Red, layoutRect, SF)
            LinePixel += lineheight
            linecount += 1


            For Each cond As Element In Trigger.GetElements(0).GetElementList
                If linecount < MAX_LINE Then
                    fnt = Me.Font
                    Dim text As String = cond.GetText
                    Dim _Brush As Brush = Brushes.Black
                    If cond.isdisalbe Then
                        text = "[DISABLE] " & text
                        _Brush = Brushes.DeepPink
                    End If
                    sz = gfx.MeasureString(text, fnt)
                    layoutRect = New RectangleF(15, LinePixel, workingRect.Width, sz.Height)
                    gfx.DrawString(text, fnt, _Brush, layoutRect, SF)
                    LinePixel += lineheight
                    linecount += 1
                End If
            Next

            If linecount < MAX_LINE Then
                fnt = Me.Font
                sz = gfx.MeasureString("ACTIONS:", fnt)
                layoutRect = New RectangleF(10, LinePixel, workingRect.Width, sz.Height)
                gfx.DrawString("ACTIONS:", fnt, Brushes.Red, layoutRect, SF)
                LinePixel += lineheight
                linecount += 1
            End If

            For Each act As Element In Trigger.GetElements(1).GetElementList
                If linecount < MAX_LINE Then
                    Dim text As String = act.GetText
                    Dim _Brush As Brush = Brushes.Black
                    If act.isdisalbe Then
                        text = "[DISABLE] " & text
                        _Brush = Brushes.DeepPink
                    End If
                    fnt = Me.Font
                    sz = gfx.MeasureString(text, fnt)
                    layoutRect = New RectangleF(15, LinePixel, workingRect.Width, sz.Height)
                    gfx.DrawString(text, fnt, _Brush, layoutRect, SF)
                    LinePixel += lineheight
                    linecount += 1
                End If
            Next

            If linecount >= MAX_LINE Then
                fnt = Me.Font
                sz = gfx.MeasureString("(MORE)", fnt)
                layoutRect = New RectangleF(workingRect.Width / 2, LinePixel, workingRect.Width, sz.Height)
                gfx.DrawString("(MORE)", fnt, Brushes.Black, layoutRect, SF)
                LinePixel += lineheight
                linecount += 1
            End If
        End If


        If Me.Height <> LinePixel + 10 Then
            Me.Height = LinePixel + 10
            mLenght = LinePixel + 10
            Refresh()
        End If




        '' Draw song name
        'fnt = Me.Font
        'sz = gfx.MeasureString(mSong, fnt)
        'layoutRect = New RectangleF(40, 0, workingRect.Width, sz.Height)
        'gfx.DrawString(mSong, fnt, Brushes.Black, layoutRect, SF)

        '' Draw artist name
        'fnt = Me.Font
        'sz = gfx.MeasureString(mArtist, fnt)
        'layoutRect = New RectangleF(42, 30, workingRect.Width, sz.Height)
        'gfx.DrawString(mArtist, fnt, Brushes.Black, layoutRect, SF)

        '' Draw album name
        'fnt = Me.Font
        'sz = gfx.MeasureString(mAlbum, fnt)
        'layoutRect = New RectangleF(42, 49, workingRect.Width, sz.Height)
        'gfx.DrawString(mAlbum, fnt, Brushes.Black, layoutRect, SF)

        '' Album Image
        'If mImage IsNot Nothing Then
        '    gfx.DrawImage(mImage, New Point(7, 7))
        'Else
        '    gfx.DrawImage(ImageList1.Images(0), New Point(7, 7))
        'End If

    End Sub

    Private Sub PaintEvent(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim gfx = e.Graphics
        '
        Paint_DrawBackground(gfx)
        Paint_DrawButton(gfx)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Trigger.isdisalbe = Not CheckBox1.Checked
    End Sub


#End Region

End Class
