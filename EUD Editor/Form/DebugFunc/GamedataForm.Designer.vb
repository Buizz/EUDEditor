<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GamedataForm
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 168)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "게임 속도"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 197)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "반응 속도"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "맵 파일"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 15)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "맵 제목"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(255, 127)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(114, 19)
        Me.CheckBox1.TabIndex = 18
        Me.CheckBox1.Text = "멀티플레이 모드"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(255, 152)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(78, 19)
        Me.CheckBox2.TabIndex = 5
        Me.CheckBox2.Text = "게임 퍼즈"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(255, 177)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(78, 19)
        Me.CheckBox3.TabIndex = 6
        Me.CheckBox3.Text = "명령 허용"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(255, 202)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(90, 19)
        Me.CheckBox4.TabIndex = 7
        Me.CheckBox4.Text = "트리거 실행"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 15)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "경과 시간"
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(82, 268)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(118, 19)
        Me.CheckBox5.TabIndex = 9
        Me.CheckBox5.Text = "게임 타이머 실행"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 241)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 15)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "게임 타이머"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(31, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 15)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "방 제목"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(82, 122)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {2147483647, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.ReadOnly = True
        Me.NumericUpDown1.Size = New System.Drawing.Size(149, 23)
        Me.NumericUpDown1.TabIndex = 12
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Location = New System.Drawing.Point(82, 239)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {2147483647, 0, 0, 0})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(149, 23)
        Me.NumericUpDown2.TabIndex = 13
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"L1", "L2", "L3", "L4", "L5"})
        Me.ComboBox1.Location = New System.Drawing.Point(82, 194)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(83, 23)
        Me.ComboBox1.TabIndex = 14
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"기본속도", "X1", "X2", "X3", "X4", "X5"})
        Me.ComboBox2.Location = New System.Drawing.Point(82, 165)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(83, 23)
        Me.ComboBox2.TabIndex = 15
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.Location = New System.Drawing.Point(171, 165)
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(60, 23)
        Me.NumericUpDown3.TabIndex = 16
        '
        'NumericUpDown4
        '
        Me.NumericUpDown4.Location = New System.Drawing.Point(171, 194)
        Me.NumericUpDown4.Name = "NumericUpDown4"
        Me.NumericUpDown4.Size = New System.Drawing.Size(60, 23)
        Me.NumericUpDown4.TabIndex = 17
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(82, 6)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(100, 23)
        Me.TextBox1.TabIndex = 0
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(82, 51)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(132, 23)
        Me.TextBox2.TabIndex = 19
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(82, 80)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(291, 23)
        Me.TextBox3.TabIndex = 20
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 50
        '
        'GamedataForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(399, 296)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.NumericUpDown4)
        Me.Controls.Add(Me.NumericUpDown3)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.NumericUpDown2)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.CheckBox5)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CheckBox4)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GamedataForm"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GameData"
        Me.TopMost = True
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents NumericUpDown3 As NumericUpDown
    Friend WithEvents NumericUpDown4 As NumericUpDown
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Timer1 As Timer
End Class
