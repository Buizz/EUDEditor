<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SCDBLoginForm
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(55, 6)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(171, 23)
        Me.TextBox1.TabIndex = 0
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(56, 35)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(170, 23)
        Me.TextBox2.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "제작자"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "맵이름"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(95, 64)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(131, 23)
        Me.NumericUpDown1.TabIndex = 4
        Me.NumericUpDown1.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "데이터 전송량"
        '
        'SCDBLoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(238, 94)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SCDBLoginForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SCDBSetting"
        Me.TopMost = True
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents Label3 As Label
End Class
