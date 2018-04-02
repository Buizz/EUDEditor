<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RawStringsForm
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.EasyCompletionComboBox1 = New SergeUtils.EasyCompletionComboBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UnitSelecter = New SergeUtils.EasyCompletionComboBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.EasyCompletionComboBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(207, 270)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'EasyCompletionComboBox1
        '
        Me.EasyCompletionComboBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EasyCompletionComboBox1.FormattingEnabled = True
        Me.EasyCompletionComboBox1.IntegralHeight = False
        Me.EasyCompletionComboBox1.Items.AddRange(New Object() {"Offset", "Read", "Write"})
        Me.EasyCompletionComboBox1.Location = New System.Drawing.Point(0, 0)
        Me.EasyCompletionComboBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.EasyCompletionComboBox1.MatchingMethod = SergeUtils.StringMatchingMethod.UseWildcards
        Me.EasyCompletionComboBox1.MaxDropDownItems = 16
        Me.EasyCompletionComboBox1.Name = "EasyCompletionComboBox1"
        Me.EasyCompletionComboBox1.Size = New System.Drawing.Size(207, 23)
        Me.EasyCompletionComboBox1.TabIndex = 23
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Button5, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Button6, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 230)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(207, 40)
        Me.TableLayoutPanel3.TabIndex = 18
        '
        'Button5
        '
        Me.Button5.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button5.Image = Global.EUD_Editor.My.Resources.Resources.Okay
        Me.Button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button5.Location = New System.Drawing.Point(0, 0)
        Me.Button5.Margin = New System.Windows.Forms.Padding(0)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(103, 40)
        Me.Button5.TabIndex = 13
        Me.Button5.Text = "확인"
        Me.Button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button6.Image = Global.EUD_Editor.My.Resources.Resources.Cancle
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button6.Location = New System.Drawing.Point(103, 0)
        Me.Button6.Margin = New System.Windows.Forms.Padding(0)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(104, 40)
        Me.Button6.TabIndex = 14
        Me.Button6.Text = "취소"
        Me.Button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button6.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel4.Controls.Add(Me.ListBox3, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.ListBox1, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 23)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(207, 184)
        Me.TableLayoutPanel4.TabIndex = 4
        '
        'ListBox3
        '
        Me.ListBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.ItemHeight = 15
        Me.ListBox3.Items.AddRange(New Object() {"Player 1", "Player 2", "Player 3", "Player 4", "Player 5", "Player 6", "Player 7", "Player 8", "C Player"})
        Me.ListBox3.Location = New System.Drawing.Point(138, 0)
        Me.ListBox3.Margin = New System.Windows.Forms.Padding(0)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(69, 184)
        Me.ListBox3.TabIndex = 3
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Items.AddRange(New Object() {"킬", "데스", "스코어", "자원", "인구수", "업그레이드", "업그레이드", "테크", "테크", "밝기", "게임 경과 시간", "카운트 다운 타이머"})
        Me.ListBox1.Location = New System.Drawing.Point(0, 0)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(138, 184)
        Me.ListBox1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.UnitSelecter)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 207)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(207, 23)
        Me.Panel1.TabIndex = 19
        '
        'UnitSelecter
        '
        Me.UnitSelecter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UnitSelecter.FormattingEnabled = True
        Me.UnitSelecter.IntegralHeight = False
        Me.UnitSelecter.Location = New System.Drawing.Point(0, 0)
        Me.UnitSelecter.Margin = New System.Windows.Forms.Padding(0)
        Me.UnitSelecter.MatchingMethod = SergeUtils.StringMatchingMethod.UseWildcards
        Me.UnitSelecter.MaxDropDownItems = 16
        Me.UnitSelecter.Name = "UnitSelecter"
        Me.UnitSelecter.Size = New System.Drawing.Size(207, 23)
        Me.UnitSelecter.TabIndex = 21
        '
        'RawStringsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(207, 270)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RawStringsForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Extra Code"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents ListBox3 As ListBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents UnitSelecter As SergeUtils.EasyCompletionComboBox
    Friend WithEvents EasyCompletionComboBox1 As SergeUtils.EasyCompletionComboBox
End Class
