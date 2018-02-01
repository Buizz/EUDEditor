<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FunctionForm
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.SelectFunctionCbb = New SergeUtils.EasyCompletionComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnSelector = New System.Windows.Forms.ListBox()
        Me.UnitBtnSelecter = New SergeUtils.EasyCompletionComboBox()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.CheckedListBox2 = New System.Windows.Forms.CheckedListBox()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown5 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown6 = New System.Windows.Forms.NumericUpDown()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.EasyCompletionComboBox1 = New SergeUtils.EasyCompletionComboBox()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.EasyCompletionComboBox2 = New SergeUtils.EasyCompletionComboBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TabControl3 = New System.Windows.Forms.TabControl()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TabControl1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TabControl3, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 117.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(334, 361)
        Me.TableLayoutPanel1.TabIndex = 16
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Button5, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Button6, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 331)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(334, 30)
        Me.TableLayoutPanel3.TabIndex = 16
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
        Me.Button5.Size = New System.Drawing.Size(167, 30)
        Me.Button5.TabIndex = 13
        Me.Button5.Text = "확인"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button6.Image = Global.EUD_Editor.My.Resources.Resources.Cancle
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button6.Location = New System.Drawing.Point(167, 0)
        Me.Button6.Margin = New System.Windows.Forms.Padding(0)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(167, 30)
        Me.Button6.TabIndex = 14
        Me.Button6.Text = "취소"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.FlowLayoutPanel1, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.SelectFunctionCbb, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(334, 117)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 27)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(334, 90)
        Me.FlowLayoutPanel1.TabIndex = 7
        '
        'SelectFunctionCbb
        '
        Me.SelectFunctionCbb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SelectFunctionCbb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SelectFunctionCbb.FormattingEnabled = True
        Me.SelectFunctionCbb.IntegralHeight = False
        Me.SelectFunctionCbb.Location = New System.Drawing.Point(3, 3)
        Me.SelectFunctionCbb.MatchingMethod = SergeUtils.StringMatchingMethod.UseWildcards
        Me.SelectFunctionCbb.MaxDropDownItems = 16
        Me.SelectFunctionCbb.Name = "SelectFunctionCbb"
        Me.SelectFunctionCbb.Size = New System.Drawing.Size(328, 23)
        Me.SelectFunctionCbb.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 147)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(334, 184)
        Me.TabControl1.TabIndex = 17
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel8)
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel7)
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel6)
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel4)
        Me.TabPage1.Controls.Add(Me.ListBox1)
        Me.TabPage1.Controls.Add(Me.CheckedListBox1)
        Me.TabPage1.Controls.Add(Me.EasyCompletionComboBox1)
        Me.TabPage1.Controls.Add(Me.NumericUpDown1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(326, 153)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "추천"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 1
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.TextBox3, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.Button2, 0, 1)
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(128, 14)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 2
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(35, 65)
        Me.TableLayoutPanel8.TabIndex = 12
        Me.TableLayoutPanel8.Visible = False
        '
        'TextBox3
        '
        Me.TextBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox3.Location = New System.Drawing.Point(0, 0)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(35, 35)
        Me.TextBox3.TabIndex = 6
        '
        'Button2
        '
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button2.Location = New System.Drawing.Point(0, 35)
        Me.Button2.Margin = New System.Windows.Forms.Padding(0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(35, 30)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "새 창으로 열기"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 1
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.BtnSelector, 0, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.UnitBtnSelecter, 0, 0)
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(8, 6)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 2
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(105, 73)
        Me.TableLayoutPanel7.TabIndex = 11
        Me.TableLayoutPanel7.Visible = False
        '
        'BtnSelector
        '
        Me.BtnSelector.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnSelector.FormattingEnabled = True
        Me.BtnSelector.ItemHeight = 15
        Me.BtnSelector.Location = New System.Drawing.Point(0, 23)
        Me.BtnSelector.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnSelector.Name = "BtnSelector"
        Me.BtnSelector.Size = New System.Drawing.Size(105, 50)
        Me.BtnSelector.TabIndex = 7
        '
        'UnitBtnSelecter
        '
        Me.UnitBtnSelecter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UnitBtnSelecter.FormattingEnabled = True
        Me.UnitBtnSelecter.IntegralHeight = False
        Me.UnitBtnSelecter.Location = New System.Drawing.Point(0, 0)
        Me.UnitBtnSelecter.Margin = New System.Windows.Forms.Padding(0)
        Me.UnitBtnSelecter.MatchingMethod = SergeUtils.StringMatchingMethod.UseWildcards
        Me.UnitBtnSelecter.MaxDropDownItems = 16
        Me.UnitBtnSelecter.Name = "UnitBtnSelecter"
        Me.UnitBtnSelecter.Size = New System.Drawing.Size(105, 23)
        Me.UnitBtnSelecter.TabIndex = 6
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.Button1, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.TextBox1, 0, 0)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(283, 101)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(31, 43)
        Me.TableLayoutPanel6.TabIndex = 9
        Me.TableLayoutPanel6.Visible = False
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.Location = New System.Drawing.Point(0, 13)
        Me.Button1.Margin = New System.Windows.Forms.Padding(0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(31, 30)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "새 창으로 열기"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(31, 13)
        Me.TextBox1.TabIndex = 6
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.CheckedListBox2, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel5, 0, 1)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 99)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(38, 53)
        Me.TableLayoutPanel4.TabIndex = 7
        '
        'CheckedListBox2
        '
        Me.CheckedListBox2.CheckOnClick = True
        Me.CheckedListBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckedListBox2.FormattingEnabled = True
        Me.CheckedListBox2.Items.AddRange(New Object() {"clocked", "burrowed", "intransit", "hallucinated", "invincible"})
        Me.CheckedListBox2.Location = New System.Drawing.Point(0, 0)
        Me.CheckedListBox2.Margin = New System.Windows.Forms.Padding(0)
        Me.CheckedListBox2.MultiColumn = True
        Me.CheckedListBox2.Name = "CheckedListBox2"
        Me.CheckedListBox2.Size = New System.Drawing.Size(38, 26)
        Me.CheckedListBox2.TabIndex = 0
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 7
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Label2, 2, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Label3, 4, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.Label5, 2, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.NumericUpDown2, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.NumericUpDown3, 3, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.NumericUpDown4, 5, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.NumericUpDown5, 1, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.NumericUpDown6, 3, 1)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 26)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(38, 27)
        Me.TableLayoutPanel5.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(49, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "hitpoint"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(113, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(38, 19)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "shield"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(223, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Label3.Size = New System.Drawing.Size(43, 19)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "energy"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Label4.Size = New System.Drawing.Size(52, 3)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "resource"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(113, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Label5.Size = New System.Drawing.Size(44, 3)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "hanger"
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumericUpDown2.Location = New System.Drawing.Point(60, 0)
        Me.NumericUpDown2.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(50, 23)
        Me.NumericUpDown2.TabIndex = 5
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumericUpDown3.Location = New System.Drawing.Point(170, 0)
        Me.NumericUpDown3.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(50, 23)
        Me.NumericUpDown3.TabIndex = 6
        '
        'NumericUpDown4
        '
        Me.NumericUpDown4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumericUpDown4.Location = New System.Drawing.Point(280, 0)
        Me.NumericUpDown4.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDown4.Name = "NumericUpDown4"
        Me.NumericUpDown4.Size = New System.Drawing.Size(50, 23)
        Me.NumericUpDown4.TabIndex = 7
        '
        'NumericUpDown5
        '
        Me.NumericUpDown5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumericUpDown5.Location = New System.Drawing.Point(60, 24)
        Me.NumericUpDown5.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDown5.Maximum = New Decimal(New Integer() {50000, 0, 0, 0})
        Me.NumericUpDown5.Name = "NumericUpDown5"
        Me.NumericUpDown5.Size = New System.Drawing.Size(50, 23)
        Me.NumericUpDown5.TabIndex = 8
        '
        'NumericUpDown6
        '
        Me.NumericUpDown6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumericUpDown6.Location = New System.Drawing.Point(170, 24)
        Me.NumericUpDown6.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDown6.Name = "NumericUpDown6"
        Me.NumericUpDown6.Size = New System.Drawing.Size(50, 23)
        Me.NumericUpDown6.TabIndex = 9
        '
        'ListBox1
        '
        Me.ListBox1.ColumnWidth = 200
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Location = New System.Drawing.Point(212, 80)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(57, 34)
        Me.ListBox1.TabIndex = 2
        Me.ListBox1.Visible = False
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.CheckOnClick = True
        Me.CheckedListBox1.ColumnWidth = 200
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(212, 124)
        Me.CheckedListBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(69, 22)
        Me.CheckedListBox1.TabIndex = 4
        Me.CheckedListBox1.Visible = False
        '
        'EasyCompletionComboBox1
        '
        Me.EasyCompletionComboBox1.FormattingEnabled = True
        Me.EasyCompletionComboBox1.IntegralHeight = False
        Me.EasyCompletionComboBox1.Location = New System.Drawing.Point(282, 29)
        Me.EasyCompletionComboBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.EasyCompletionComboBox1.MatchingMethod = SergeUtils.StringMatchingMethod.UseWildcards
        Me.EasyCompletionComboBox1.MaxDropDownItems = 16
        Me.EasyCompletionComboBox1.Name = "EasyCompletionComboBox1"
        Me.EasyCompletionComboBox1.Size = New System.Drawing.Size(32, 23)
        Me.EasyCompletionComboBox1.TabIndex = 5
        Me.EasyCompletionComboBox1.Visible = False
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(269, 3)
        Me.NumericUpDown1.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {-1, 0, 0, -2147483648})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(45, 23)
        Me.NumericUpDown1.TabIndex = 0
        Me.NumericUpDown1.Visible = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.EasyCompletionComboBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(326, 153)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "변수"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'EasyCompletionComboBox2
        '
        Me.EasyCompletionComboBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.EasyCompletionComboBox2.FormattingEnabled = True
        Me.EasyCompletionComboBox2.IntegralHeight = False
        Me.EasyCompletionComboBox2.Location = New System.Drawing.Point(3, 3)
        Me.EasyCompletionComboBox2.MatchingMethod = SergeUtils.StringMatchingMethod.UseWildcards
        Me.EasyCompletionComboBox2.MaxDropDownItems = 16
        Me.EasyCompletionComboBox2.Name = "EasyCompletionComboBox2"
        Me.EasyCompletionComboBox2.Size = New System.Drawing.Size(320, 23)
        Me.EasyCompletionComboBox2.TabIndex = 6
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TextBox2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 27)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(326, 153)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "직접 입력"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox2.Location = New System.Drawing.Point(0, 0)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(326, 153)
        Me.TextBox2.TabIndex = 1
        '
        'TabControl3
        '
        Me.TabControl3.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.TabControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl3.Location = New System.Drawing.Point(0, 117)
        Me.TabControl3.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.Size = New System.Drawing.Size(334, 30)
        Me.TabControl3.TabIndex = 18
        '
        'FunctionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button6
        Me.ClientSize = New System.Drawing.Size(334, 361)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FunctionForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "함수"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents SelectFunctionCbb As SergeUtils.EasyCompletionComboBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents CheckedListBox2 As CheckedListBox
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents NumericUpDown3 As NumericUpDown
    Friend WithEvents NumericUpDown4 As NumericUpDown
    Friend WithEvents NumericUpDown5 As NumericUpDown
    Friend WithEvents NumericUpDown6 As NumericUpDown
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents EasyCompletionComboBox1 As SergeUtils.EasyCompletionComboBox
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents EasyCompletionComboBox2 As SergeUtils.EasyCompletionComboBox
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TabControl3 As TabControl
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents Button1 As Button
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents BtnSelector As ListBox
    Friend WithEvents UnitBtnSelecter As SergeUtils.EasyCompletionComboBox
End Class
