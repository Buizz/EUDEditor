<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DethesViewerForm
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
        Me.components = New System.ComponentModel.Container()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.NumericUpDown12 = New System.Windows.Forms.NumericUpDown()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.NumericUpDown11 = New System.Windows.Forms.NumericUpDown()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.NumericUpDown10 = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.NumericUpDown9 = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.NumericUpDown8 = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.NumericUpDown7 = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NumericUpDown6 = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.NumericUpDown5 = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.NumericUpDown12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 227.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 436.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(448, 368)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.FlowLayoutPanel1, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.SplitContainer1, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(227, 368)
        Me.TableLayoutPanel2.TabIndex = 7
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.Label27)
        Me.FlowLayoutPanel1.Controls.Add(Me.TextBox2)
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox5)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 340)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(227, 28)
        Me.FlowLayoutPanel1.TabIndex = 4
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(3, 7)
        Me.Label27.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(31, 15)
        Me.Label27.TabIndex = 3
        Me.Label27.Text = "필터"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.WindowText
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.Window
        Me.TextBox2.Location = New System.Drawing.Point(40, 3)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(128, 23)
        Me.TextBox2.TabIndex = 2
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(174, 3)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(50, 19)
        Me.CheckBox5.TabIndex = 5
        Me.CheckBox5.Text = "수정"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel3)
        Me.SplitContainer1.Panel1MinSize = 93
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ListBox1)
        Me.SplitContainer1.Size = New System.Drawing.Size(227, 340)
        Me.SplitContainer1.SplitterDistance = 152
        Me.SplitContainer1.TabIndex = 5
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Button5, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(227, 152)
        Me.TableLayoutPanel3.TabIndex = 14
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ListView1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 25)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(227, 127)
        Me.Panel1.TabIndex = 15
        '
        'ListView1
        '
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(227, 127)
        Me.ListView1.TabIndex = 14
        Me.ListView1.TileSize = New System.Drawing.Size(40, 38)
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Tile
        '
        'Button5
        '
        Me.Button5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button5.Location = New System.Drawing.Point(0, 0)
        Me.Button5.Margin = New System.Windows.Forms.Padding(0)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(227, 25)
        Me.Button5.TabIndex = 15
        Me.Button5.Text = "접기"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.SystemColors.WindowText
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ListBox1.Font = New System.Drawing.Font("맑은 고딕", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ListBox1.ForeColor = System.Drawing.SystemColors.Window
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(0, 0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(227, 184)
        Me.ListBox1.TabIndex = 7
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.NumericUpDown12)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.NumericUpDown11)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.NumericUpDown10)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.NumericUpDown9)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.NumericUpDown8)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.NumericUpDown7)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.NumericUpDown6)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.NumericUpDown5)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.NumericUpDown4)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.NumericUpDown3)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.NumericUpDown2)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.NumericUpDown1)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(227, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(436, 368)
        Me.Panel2.TabIndex = 8
        '
        'NumericUpDown12
        '
        Me.NumericUpDown12.Location = New System.Drawing.Point(70, 327)
        Me.NumericUpDown12.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown12.Name = "NumericUpDown12"
        Me.NumericUpDown12.Size = New System.Drawing.Size(140, 23)
        Me.NumericUpDown12.TabIndex = 23
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(14, 329)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 15)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "Player 12"
        '
        'NumericUpDown11
        '
        Me.NumericUpDown11.Location = New System.Drawing.Point(70, 298)
        Me.NumericUpDown11.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown11.Name = "NumericUpDown11"
        Me.NumericUpDown11.Size = New System.Drawing.Size(140, 23)
        Me.NumericUpDown11.TabIndex = 21
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(14, 300)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(57, 15)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Player 11"
        '
        'NumericUpDown10
        '
        Me.NumericUpDown10.Location = New System.Drawing.Point(70, 269)
        Me.NumericUpDown10.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown10.Name = "NumericUpDown10"
        Me.NumericUpDown10.Size = New System.Drawing.Size(140, 23)
        Me.NumericUpDown10.TabIndex = 19
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(14, 271)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(57, 15)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Player 10"
        '
        'NumericUpDown9
        '
        Me.NumericUpDown9.Location = New System.Drawing.Point(70, 240)
        Me.NumericUpDown9.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown9.Name = "NumericUpDown9"
        Me.NumericUpDown9.Size = New System.Drawing.Size(140, 23)
        Me.NumericUpDown9.TabIndex = 17
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(14, 242)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 15)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Player 9"
        '
        'NumericUpDown8
        '
        Me.NumericUpDown8.Location = New System.Drawing.Point(70, 211)
        Me.NumericUpDown8.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown8.Name = "NumericUpDown8"
        Me.NumericUpDown8.Size = New System.Drawing.Size(140, 23)
        Me.NumericUpDown8.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 213)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 15)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Player 8"
        '
        'NumericUpDown7
        '
        Me.NumericUpDown7.Location = New System.Drawing.Point(70, 182)
        Me.NumericUpDown7.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown7.Name = "NumericUpDown7"
        Me.NumericUpDown7.Size = New System.Drawing.Size(140, 23)
        Me.NumericUpDown7.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 184)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 15)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Player 7"
        '
        'NumericUpDown6
        '
        Me.NumericUpDown6.Location = New System.Drawing.Point(70, 153)
        Me.NumericUpDown6.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown6.Name = "NumericUpDown6"
        Me.NumericUpDown6.Size = New System.Drawing.Size(140, 23)
        Me.NumericUpDown6.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 155)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 15)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Player 6"
        '
        'NumericUpDown5
        '
        Me.NumericUpDown5.Location = New System.Drawing.Point(70, 124)
        Me.NumericUpDown5.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown5.Name = "NumericUpDown5"
        Me.NumericUpDown5.Size = New System.Drawing.Size(140, 23)
        Me.NumericUpDown5.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 15)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Player 5"
        '
        'NumericUpDown4
        '
        Me.NumericUpDown4.Location = New System.Drawing.Point(70, 95)
        Me.NumericUpDown4.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown4.Name = "NumericUpDown4"
        Me.NumericUpDown4.Size = New System.Drawing.Size(140, 23)
        Me.NumericUpDown4.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Player 4"
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.Location = New System.Drawing.Point(70, 66)
        Me.NumericUpDown3.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(140, 23)
        Me.NumericUpDown3.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Player 3"
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Location = New System.Drawing.Point(70, 37)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(140, 23)
        Me.NumericUpDown2.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Player 2"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(70, 8)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(140, 23)
        Me.NumericUpDown1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Player 1"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 20
        '
        'DethesViewerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(448, 368)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MinimumSize = New System.Drawing.Size(464, 407)
        Me.Name = "DethesViewerForm"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DeathesViewer"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.NumericUpDown12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents Label27 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Button5 As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents NumericUpDown12 As NumericUpDown
    Friend WithEvents Label12 As Label
    Friend WithEvents NumericUpDown11 As NumericUpDown
    Friend WithEvents Label11 As Label
    Friend WithEvents NumericUpDown10 As NumericUpDown
    Friend WithEvents Label10 As Label
    Friend WithEvents NumericUpDown9 As NumericUpDown
    Friend WithEvents Label9 As Label
    Friend WithEvents NumericUpDown8 As NumericUpDown
    Friend WithEvents Label8 As Label
    Friend WithEvents NumericUpDown7 As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents NumericUpDown6 As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents NumericUpDown5 As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents NumericUpDown4 As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents NumericUpDown3 As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents Timer1 As Timer
End Class
