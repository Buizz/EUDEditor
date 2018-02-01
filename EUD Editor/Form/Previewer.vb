Imports FastColoredTextBoxNS


Public Class Previewer
    Public ispyfile As Boolean = True

    Friend WithEvents FCTB As New FastColoredTextBox
    Private Sub Previewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FCTB.Dock = DockStyle.Fill
        FCTB.Language = Language.CSharp
        FCTB.ReadOnly = True
        ' FCTB.Font = MyBase.Font

        'FCTB.ImeMode = ImeMode.Hangul
        MyBase.Controls.Add(FCTB)
        MyBase.Refresh()
    End Sub

End Class