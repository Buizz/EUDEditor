<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SCDBDataCreater
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.BtnDeath = New System.Windows.Forms.Button()
        Me.BtnLoc = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Location = New System.Drawing.Point(0, 2)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "플레이어"
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(55, 2)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(265, 16)
        Me.TextBox1.TabIndex = 1
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 76)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(314, 250)
        Me.DataGridView1.TabIndex = 14
        '
        'Column1
        '
        Me.Column1.HeaderText = "변수 명"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column1.Width = 130
        '
        'Column2
        '
        Me.Column2.HeaderText = "값"
        Me.Column2.Name = "Column2"
        Me.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column2.Width = 160
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(320, 329)
        Me.TableLayoutPanel1.TabIndex = 15
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.Panel1.Size = New System.Drawing.Size(320, 20)
        Me.Panel1.TabIndex = 16
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.Button3)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button2)
        Me.FlowLayoutPanel1.Controls.Add(Me.BtnDeath)
        Me.FlowLayoutPanel1.Controls.Add(Me.BtnLoc)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button1)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 20)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(320, 53)
        Me.FlowLayoutPanel1.TabIndex = 2
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.Control
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Button3.Image = Global.EUD_Editor.My.Resources.Resources.LoadFile
        Me.Button3.Location = New System.Drawing.Point(0, 0)
        Me.Button3.Margin = New System.Windows.Forms.Padding(0)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(64, 50)
        Me.Button3.TabIndex = 7
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.Control
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Button2.Image = Global.EUD_Editor.My.Resources.Resources.Save
        Me.Button2.Location = New System.Drawing.Point(64, 0)
        Me.Button2.Margin = New System.Windows.Forms.Padding(0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(64, 50)
        Me.Button2.TabIndex = 6
        Me.Button2.UseVisualStyleBackColor = False
        '
        'BtnDeath
        '
        Me.BtnDeath.BackColor = System.Drawing.SystemColors.Control
        Me.BtnDeath.FlatAppearance.BorderSize = 0
        Me.BtnDeath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDeath.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.BtnDeath.Location = New System.Drawing.Point(128, 0)
        Me.BtnDeath.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnDeath.Name = "BtnDeath"
        Me.BtnDeath.Size = New System.Drawing.Size(64, 50)
        Me.BtnDeath.TabIndex = 1
        Me.BtnDeath.Text = "데스 값"
        Me.BtnDeath.UseVisualStyleBackColor = False
        '
        'BtnLoc
        '
        Me.BtnLoc.BackColor = System.Drawing.SystemColors.Control
        Me.BtnLoc.FlatAppearance.BorderSize = 0
        Me.BtnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnLoc.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.BtnLoc.Location = New System.Drawing.Point(192, 0)
        Me.BtnLoc.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnLoc.Name = "BtnLoc"
        Me.BtnLoc.Size = New System.Drawing.Size(64, 50)
        Me.BtnLoc.TabIndex = 2
        Me.BtnLoc.Text = "로케이션"
        Me.BtnLoc.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Button1.Location = New System.Drawing.Point(256, 0)
        Me.Button1.Margin = New System.Windows.Forms.Padding(0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(64, 50)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "변수"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'SCDBDataCreater
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(320, 329)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SCDBDataCreater"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SCDBDataLoader"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents BtnDeath As Button
    Friend WithEvents BtnLoc As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
End Class
