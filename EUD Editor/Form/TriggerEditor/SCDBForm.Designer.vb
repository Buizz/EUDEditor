<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SCDBForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SCDBForm))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.BtnSetting = New System.Windows.Forms.Button()
        Me.BtnDeath = New System.Windows.Forms.Button()
        Me.BtnLoc = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ComboBox1 = New SergeUtils.EasyCompletionComboBox()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.EasyCompletionComboBox1 = New SergeUtils.EasyCompletionComboBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ListBox3, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(248, 345)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.BtnSetting)
        Me.FlowLayoutPanel1.Controls.Add(Me.BtnDeath)
        Me.FlowLayoutPanel1.Controls.Add(Me.BtnLoc)
        Me.FlowLayoutPanel1.Controls.Add(Me.BtnDelete)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(248, 50)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        'BtnSetting
        '
        Me.BtnSetting.BackColor = System.Drawing.SystemColors.Control
        Me.BtnSetting.FlatAppearance.BorderSize = 0
        Me.BtnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSetting.Image = Global.EUD_Editor.My.Resources.Resources.Setting
        Me.BtnSetting.Location = New System.Drawing.Point(0, 0)
        Me.BtnSetting.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnSetting.Name = "BtnSetting"
        Me.BtnSetting.Size = New System.Drawing.Size(55, 50)
        Me.BtnSetting.TabIndex = 0
        Me.BtnSetting.UseVisualStyleBackColor = False
        '
        'BtnDeath
        '
        Me.BtnDeath.BackColor = System.Drawing.SystemColors.Control
        Me.BtnDeath.FlatAppearance.BorderSize = 0
        Me.BtnDeath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDeath.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.BtnDeath.Location = New System.Drawing.Point(55, 0)
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
        Me.BtnLoc.Location = New System.Drawing.Point(119, 0)
        Me.BtnLoc.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnLoc.Name = "BtnLoc"
        Me.BtnLoc.Size = New System.Drawing.Size(64, 50)
        Me.BtnLoc.TabIndex = 2
        Me.BtnLoc.Text = "로케이션"
        Me.BtnLoc.UseVisualStyleBackColor = False
        '
        'BtnDelete
        '
        Me.BtnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.BtnDelete.Enabled = False
        Me.BtnDelete.FlatAppearance.BorderSize = 0
        Me.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDelete.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.BtnDelete.Location = New System.Drawing.Point(183, 0)
        Me.BtnDelete.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(64, 50)
        Me.BtnDelete.TabIndex = 4
        Me.BtnDelete.Text = "삭제"
        Me.BtnDelete.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TableLayoutPanel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 50)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(248, 33)
        Me.Panel1.TabIndex = 4
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Button2, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(248, 33)
        Me.TableLayoutPanel2.TabIndex = 3
        '
        'Button2
        '
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button2.Location = New System.Drawing.Point(182, 2)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(64, 29)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "추가"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.IntegralHeight = False
        Me.ComboBox1.Location = New System.Drawing.Point(3, 5)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.ComboBox1.MatchingMethod = SergeUtils.StringMatchingMethod.UseWildcards
        Me.ComboBox1.MaxDropDownItems = 16
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(174, 23)
        Me.ComboBox1.TabIndex = 1
        Me.ComboBox1.TabStop = False
        '
        'ListBox3
        '
        Me.ListBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.ItemHeight = 15
        Me.ListBox3.Location = New System.Drawing.Point(3, 86)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(242, 256)
        Me.ListBox3.TabIndex = 5
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.EasyCompletionComboBox1, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.ComboBox1, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(180, 33)
        Me.TableLayoutPanel3.TabIndex = 3
        '
        'EasyCompletionComboBox1
        '
        Me.EasyCompletionComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EasyCompletionComboBox1.FormattingEnabled = True
        Me.EasyCompletionComboBox1.IntegralHeight = False
        Me.EasyCompletionComboBox1.Location = New System.Drawing.Point(3, 38)
        Me.EasyCompletionComboBox1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.EasyCompletionComboBox1.MatchingMethod = SergeUtils.StringMatchingMethod.UseWildcards
        Me.EasyCompletionComboBox1.MaxDropDownItems = 16
        Me.EasyCompletionComboBox1.Name = "EasyCompletionComboBox1"
        Me.EasyCompletionComboBox1.Size = New System.Drawing.Size(174, 23)
        Me.EasyCompletionComboBox1.TabIndex = 2
        Me.EasyCompletionComboBox1.TabStop = False
        '
        'SCDBForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(248, 345)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MinimumSize = New System.Drawing.Size(264, 384)
        Me.Name = "SCDBForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SCDataBase"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents BtnSetting As Button
    Friend WithEvents BtnDeath As Button
    Friend WithEvents BtnLoc As Button
    Friend WithEvents BtnDelete As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ComboBox1 As SergeUtils.EasyCompletionComboBox
    Friend WithEvents Button2 As Button
    Friend WithEvents ListBox3 As ListBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents EasyCompletionComboBox1 As SergeUtils.EasyCompletionComboBox
End Class
