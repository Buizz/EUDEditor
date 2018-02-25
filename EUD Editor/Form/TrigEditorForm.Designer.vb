<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TrigEditorForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TrigEditorForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.파일FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.새로만들기NToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.열기OToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenCont = New System.Windows.Forms.ToolStripMenuItem()
        Me.파일로저장AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.프로젝트저장ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.FlowLayoutPanel3 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btn_NewFile = New System.Windows.Forms.Button()
        Me.btn_OpenFile = New System.Windows.Forms.Button()
        Me.Btn_OpenCont = New System.Windows.Forms.Button()
        Me.btn_Save = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.새로만들기NToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.조건ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.액션ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.대기하기ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.If문ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IfElse문ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.For문ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.While문ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.함수FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.함수ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.함수정의ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.인수ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.함수저장ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.함수불러오기ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.수정ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.잘라내기XToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.복사VToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.붙혀넣기CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.삭제DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FastColoredTextBox1 = New FastColoredTextBoxNS.FastColoredTextBox()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.FlowNew = New System.Windows.Forms.FlowLayoutPanel()
        Me.조건Btn = New System.Windows.Forms.Button()
        Me.액션Btn = New System.Windows.Forms.Button()
        Me.대기하기Btn = New System.Windows.Forms.Button()
        Me.함수Btn = New System.Windows.Forms.Button()
        Me.IfBtn = New System.Windows.Forms.Button()
        Me.IfElseBtn = New System.Windows.Forms.Button()
        Me.ForBtn = New System.Windows.Forms.Button()
        Me.WhileBtn = New System.Windows.Forms.Button()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.함수정의Btn = New System.Windows.Forms.Button()
        Me.인수Btn = New System.Windows.Forms.Button()
        Me.함수저장Btn = New System.Windows.Forms.Button()
        Me.함수불러오기Btn = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog2 = New System.Windows.Forms.SaveFileDialog()
        Me.MenuStrip1.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.FlowLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.FastColoredTextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.FlowNew.SuspendLayout()
        Me.FlowLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.파일FToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(755, 24)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '파일FToolStripMenuItem
        '
        Me.파일FToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.새로만들기NToolStripMenuItem, Me.열기OToolStripMenuItem, Me.OpenCont, Me.파일로저장AToolStripMenuItem, Me.ToolStripSeparator1, Me.프로젝트저장ToolStripMenuItem})
        Me.파일FToolStripMenuItem.Name = "파일FToolStripMenuItem"
        Me.파일FToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.파일FToolStripMenuItem.Text = "파일(&F)"
        '
        '새로만들기NToolStripMenuItem
        '
        Me.새로만들기NToolStripMenuItem.Image = Global.EUD_Editor.My.Resources.Resources.NewFile
        Me.새로만들기NToolStripMenuItem.Name = "새로만들기NToolStripMenuItem"
        Me.새로만들기NToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.새로만들기NToolStripMenuItem.Text = "새로 만들기(&N)"
        '
        '열기OToolStripMenuItem
        '
        Me.열기OToolStripMenuItem.Image = Global.EUD_Editor.My.Resources.Resources.Open
        Me.열기OToolStripMenuItem.Name = "열기OToolStripMenuItem"
        Me.열기OToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.열기OToolStripMenuItem.Text = "열기(&O)"
        '
        'OpenCont
        '
        Me.OpenCont.Image = Global.EUD_Editor.My.Resources.Resources.LoadFile
        Me.OpenCont.Name = "OpenCont"
        Me.OpenCont.Size = New System.Drawing.Size(206, 22)
        Me.OpenCont.Text = "불러오기"
        '
        '파일로저장AToolStripMenuItem
        '
        Me.파일로저장AToolStripMenuItem.Image = Global.EUD_Editor.My.Resources.Resources.Savefile
        Me.파일로저장AToolStripMenuItem.Name = "파일로저장AToolStripMenuItem"
        Me.파일로저장AToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.파일로저장AToolStripMenuItem.Text = "파일로 저장(&A)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(203, 6)
        '
        '프로젝트저장ToolStripMenuItem
        '
        Me.프로젝트저장ToolStripMenuItem.Image = Global.EUD_Editor.My.Resources.Resources.Save
        Me.프로젝트저장ToolStripMenuItem.Name = "프로젝트저장ToolStripMenuItem"
        Me.프로젝트저장ToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.프로젝트저장ToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.프로젝트저장ToolStripMenuItem.Text = "프로젝트 저장(&S)"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.FlowLayoutPanel3, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 24)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(755, 645)
        Me.TableLayoutPanel4.TabIndex = 7
        '
        'FlowLayoutPanel3
        '
        Me.FlowLayoutPanel3.BackColor = System.Drawing.SystemColors.Control
        Me.FlowLayoutPanel3.Controls.Add(Me.btn_NewFile)
        Me.FlowLayoutPanel3.Controls.Add(Me.btn_OpenFile)
        Me.FlowLayoutPanel3.Controls.Add(Me.Btn_OpenCont)
        Me.FlowLayoutPanel3.Controls.Add(Me.btn_Save)
        Me.FlowLayoutPanel3.Controls.Add(Me.Button4)
        Me.FlowLayoutPanel3.Controls.Add(Me.Button14)
        Me.FlowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowLayoutPanel3.Name = "FlowLayoutPanel3"
        Me.FlowLayoutPanel3.Size = New System.Drawing.Size(755, 43)
        Me.FlowLayoutPanel3.TabIndex = 7
        '
        'btn_NewFile
        '
        Me.btn_NewFile.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btn_NewFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_NewFile.Font = New System.Drawing.Font("맑은 고딕", 6.75!)
        Me.btn_NewFile.Image = Global.EUD_Editor.My.Resources.Resources.NewFile
        Me.btn_NewFile.Location = New System.Drawing.Point(1, 1)
        Me.btn_NewFile.Margin = New System.Windows.Forms.Padding(1)
        Me.btn_NewFile.Name = "btn_NewFile"
        Me.btn_NewFile.Size = New System.Drawing.Size(50, 40)
        Me.btn_NewFile.TabIndex = 1
        '
        'btn_OpenFile
        '
        Me.btn_OpenFile.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btn_OpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_OpenFile.Font = New System.Drawing.Font("맑은 고딕", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_OpenFile.Image = Global.EUD_Editor.My.Resources.Resources.Open
        Me.btn_OpenFile.Location = New System.Drawing.Point(53, 1)
        Me.btn_OpenFile.Margin = New System.Windows.Forms.Padding(1)
        Me.btn_OpenFile.Name = "btn_OpenFile"
        Me.btn_OpenFile.Size = New System.Drawing.Size(50, 40)
        Me.btn_OpenFile.TabIndex = 2
        '
        'Btn_OpenCont
        '
        Me.Btn_OpenCont.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.Btn_OpenCont.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_OpenCont.Font = New System.Drawing.Font("맑은 고딕", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Btn_OpenCont.Image = Global.EUD_Editor.My.Resources.Resources.LoadFile
        Me.Btn_OpenCont.Location = New System.Drawing.Point(105, 1)
        Me.Btn_OpenCont.Margin = New System.Windows.Forms.Padding(1)
        Me.Btn_OpenCont.Name = "Btn_OpenCont"
        Me.Btn_OpenCont.Size = New System.Drawing.Size(50, 40)
        Me.Btn_OpenCont.TabIndex = 18
        '
        'btn_Save
        '
        Me.btn_Save.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Save.Font = New System.Drawing.Font("맑은 고딕", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_Save.Image = Global.EUD_Editor.My.Resources.Resources.Savefile
        Me.btn_Save.Location = New System.Drawing.Point(157, 1)
        Me.btn_Save.Margin = New System.Windows.Forms.Padding(1)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(50, 40)
        Me.btn_Save.TabIndex = 3
        '
        'Button4
        '
        Me.Button4.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("맑은 고딕", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button4.Image = Global.EUD_Editor.My.Resources.Resources.Save
        Me.Button4.Location = New System.Drawing.Point(209, 1)
        Me.Button4.Margin = New System.Windows.Forms.Padding(1)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(50, 40)
        Me.Button4.TabIndex = 4
        '
        'Button14
        '
        Me.Button14.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Button14.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.Button14.Font = New System.Drawing.Font("맑은 고딕", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button14.Image = Global.EUD_Editor.My.Resources.Resources.Insert
        Me.Button14.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button14.Location = New System.Drawing.Point(265, 1)
        Me.Button14.Margin = New System.Windows.Forms.Padding(5, 1, 5, 1)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(48, 40)
        Me.Button14.TabIndex = 17
        Me.Button14.Text = "삽입(&W)"
        Me.Button14.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button14.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button14.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.SplitContainer1, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.FlowLayoutPanel1, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 43)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(755, 602)
        Me.TableLayoutPanel3.TabIndex = 6
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(135, 602)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "글로벌 변수"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ListBox1, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 19)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(129, 580)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Controls.Add(Me.Button3, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Button2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Button1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(129, 40)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'Button3
        '
        Me.Button3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button3.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("맑은 고딕", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button3.Image = Global.EUD_Editor.My.Resources.Resources.Delete
        Me.Button3.Location = New System.Drawing.Point(87, 1)
        Me.Button3.Margin = New System.Windows.Forms.Padding(1)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(41, 38)
        Me.Button3.TabIndex = 2
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("맑은 고딕", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button2.Image = Global.EUD_Editor.My.Resources.Resources.Clear
        Me.Button2.Location = New System.Drawing.Point(44, 1)
        Me.Button2.Margin = New System.Windows.Forms.Padding(1)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(41, 38)
        Me.Button2.TabIndex = 1
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("맑은 고딕", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button1.Image = Global.EUD_Editor.My.Resources.Resources.NewFile
        Me.Button1.Location = New System.Drawing.Point(1, 1)
        Me.Button1.Margin = New System.Windows.Forms.Padding(1)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(41, 38)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Location = New System.Drawing.Point(0, 40)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(129, 540)
        Me.ListBox1.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(200, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.FastColoredTextBox1)
        Me.SplitContainer1.Panel2MinSize = 5
        Me.SplitContainer1.Size = New System.Drawing.Size(552, 596)
        Me.SplitContainer1.SplitterDistance = 291
        Me.SplitContainer1.TabIndex = 5
        '
        'TreeView1
        '
        Me.TreeView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Font = New System.Drawing.Font("돋움", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TreeView1.ForeColor = System.Drawing.SystemColors.Window
        Me.TreeView1.HideSelection = False
        Me.TreeView1.Indent = 25
        Me.TreeView1.ItemHeight = 18
        Me.TreeView1.LineColor = System.Drawing.Color.DimGray
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Margin = New System.Windows.Forms.Padding(0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(291, 596)
        Me.TreeView1.TabIndex = 5
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.새로만들기NToolStripMenuItem1, Me.함수FToolStripMenuItem, Me.ToolStripSeparator4, Me.수정ToolStripMenuItem, Me.잘라내기XToolStripMenuItem, Me.복사VToolStripMenuItem, Me.붙혀넣기CToolStripMenuItem, Me.삭제DToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(156, 164)
        '
        '새로만들기NToolStripMenuItem1
        '
        Me.새로만들기NToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.조건ToolStripMenuItem, Me.ToolStripSeparator3, Me.액션ToolStripMenuItem, Me.대기하기ToolStripMenuItem, Me.If문ToolStripMenuItem, Me.IfElse문ToolStripMenuItem, Me.For문ToolStripMenuItem, Me.While문ToolStripMenuItem})
        Me.새로만들기NToolStripMenuItem1.Image = Global.EUD_Editor.My.Resources.Resources.NewFile
        Me.새로만들기NToolStripMenuItem1.Name = "새로만들기NToolStripMenuItem1"
        Me.새로만들기NToolStripMenuItem1.Size = New System.Drawing.Size(155, 22)
        Me.새로만들기NToolStripMenuItem1.Text = "새로 만들기(&N)"
        '
        '조건ToolStripMenuItem
        '
        Me.조건ToolStripMenuItem.Name = "조건ToolStripMenuItem"
        Me.조건ToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.조건ToolStripMenuItem.Text = "조건"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(139, 6)
        '
        '액션ToolStripMenuItem
        '
        Me.액션ToolStripMenuItem.Name = "액션ToolStripMenuItem"
        Me.액션ToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.액션ToolStripMenuItem.Text = "액션"
        '
        '대기하기ToolStripMenuItem
        '
        Me.대기하기ToolStripMenuItem.Name = "대기하기ToolStripMenuItem"
        Me.대기하기ToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.대기하기ToolStripMenuItem.Text = "대기하기"
        '
        'If문ToolStripMenuItem
        '
        Me.If문ToolStripMenuItem.Name = "If문ToolStripMenuItem"
        Me.If문ToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.If문ToolStripMenuItem.Text = "If문"
        '
        'IfElse문ToolStripMenuItem
        '
        Me.IfElse문ToolStripMenuItem.Name = "IfElse문ToolStripMenuItem"
        Me.IfElse문ToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.IfElse문ToolStripMenuItem.Text = "IfElse문"
        '
        'For문ToolStripMenuItem
        '
        Me.For문ToolStripMenuItem.Name = "For문ToolStripMenuItem"
        Me.For문ToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.For문ToolStripMenuItem.Text = "반복문(횟수)"
        '
        'While문ToolStripMenuItem
        '
        Me.While문ToolStripMenuItem.Name = "While문ToolStripMenuItem"
        Me.While문ToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.While문ToolStripMenuItem.Text = "반복문(조건)"
        '
        '함수FToolStripMenuItem
        '
        Me.함수FToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.함수ToolStripMenuItem, Me.함수정의ToolStripMenuItem, Me.인수ToolStripMenuItem, Me.ToolStripSeparator2, Me.함수저장ToolStripMenuItem, Me.함수불러오기ToolStripMenuItem})
        Me.함수FToolStripMenuItem.Name = "함수FToolStripMenuItem"
        Me.함수FToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.함수FToolStripMenuItem.Text = "함수(&F)"
        '
        '함수ToolStripMenuItem
        '
        Me.함수ToolStripMenuItem.Name = "함수ToolStripMenuItem"
        Me.함수ToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.함수ToolStripMenuItem.Text = "함수"
        '
        '함수정의ToolStripMenuItem
        '
        Me.함수정의ToolStripMenuItem.Name = "함수정의ToolStripMenuItem"
        Me.함수정의ToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.함수정의ToolStripMenuItem.Text = "함수 정의"
        '
        '인수ToolStripMenuItem
        '
        Me.인수ToolStripMenuItem.Name = "인수ToolStripMenuItem"
        Me.인수ToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.인수ToolStripMenuItem.Text = "인수 정의"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(147, 6)
        '
        '함수저장ToolStripMenuItem
        '
        Me.함수저장ToolStripMenuItem.Name = "함수저장ToolStripMenuItem"
        Me.함수저장ToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.함수저장ToolStripMenuItem.Text = "함수 저장"
        '
        '함수불러오기ToolStripMenuItem
        '
        Me.함수불러오기ToolStripMenuItem.Name = "함수불러오기ToolStripMenuItem"
        Me.함수불러오기ToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.함수불러오기ToolStripMenuItem.Text = "함수 불러오기"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(152, 6)
        '
        '수정ToolStripMenuItem
        '
        Me.수정ToolStripMenuItem.Image = Global.EUD_Editor.My.Resources.Resources.Clear
        Me.수정ToolStripMenuItem.Name = "수정ToolStripMenuItem"
        Me.수정ToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.수정ToolStripMenuItem.Text = "수정(Enter)"
        '
        '잘라내기XToolStripMenuItem
        '
        Me.잘라내기XToolStripMenuItem.Image = Global.EUD_Editor.My.Resources.Resources.Cut
        Me.잘라내기XToolStripMenuItem.Name = "잘라내기XToolStripMenuItem"
        Me.잘라내기XToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.잘라내기XToolStripMenuItem.Text = "잘라내기(&X)"
        '
        '복사VToolStripMenuItem
        '
        Me.복사VToolStripMenuItem.Image = Global.EUD_Editor.My.Resources.Resources.Copy
        Me.복사VToolStripMenuItem.Name = "복사VToolStripMenuItem"
        Me.복사VToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.복사VToolStripMenuItem.Text = "복사(&V)"
        '
        '붙혀넣기CToolStripMenuItem
        '
        Me.붙혀넣기CToolStripMenuItem.Image = Global.EUD_Editor.My.Resources.Resources.Paste
        Me.붙혀넣기CToolStripMenuItem.Name = "붙혀넣기CToolStripMenuItem"
        Me.붙혀넣기CToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.붙혀넣기CToolStripMenuItem.Text = "붙여넣기(&C)"
        '
        '삭제DToolStripMenuItem
        '
        Me.삭제DToolStripMenuItem.Image = Global.EUD_Editor.My.Resources.Resources.Cancle
        Me.삭제DToolStripMenuItem.Name = "삭제DToolStripMenuItem"
        Me.삭제DToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.삭제DToolStripMenuItem.Text = "삭제(&D)"
        '
        'FastColoredTextBox1
        '
        Me.FastColoredTextBox1.AutoCompleteBracketsList = New Char() {Global.Microsoft.VisualBasic.ChrW(40), Global.Microsoft.VisualBasic.ChrW(41), Global.Microsoft.VisualBasic.ChrW(123), Global.Microsoft.VisualBasic.ChrW(125), Global.Microsoft.VisualBasic.ChrW(91), Global.Microsoft.VisualBasic.ChrW(93), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(39), Global.Microsoft.VisualBasic.ChrW(39)}
        Me.FastColoredTextBox1.AutoIndentCharsPatterns = "" & Global.Microsoft.VisualBasic.ChrW(10) & "^\s*[\w\.]+(\s\w+)?\s*(?<range>=)\s*(?<range>[^;]+);" & Global.Microsoft.VisualBasic.ChrW(10)
        Me.FastColoredTextBox1.AutoScrollMinSize = New System.Drawing.Size(179, 14)
        Me.FastColoredTextBox1.BackBrush = Nothing
        Me.FastColoredTextBox1.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2
        Me.FastColoredTextBox1.CharHeight = 14
        Me.FastColoredTextBox1.CharWidth = 8
        Me.FastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.FastColoredTextBox1.DisabledColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.FastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FastColoredTextBox1.Font = New System.Drawing.Font("Courier New", 9.75!)
        Me.FastColoredTextBox1.IsReplaceMode = False
        Me.FastColoredTextBox1.Language = FastColoredTextBoxNS.Language.JS
        Me.FastColoredTextBox1.LeftBracket = Global.Microsoft.VisualBasic.ChrW(40)
        Me.FastColoredTextBox1.LeftBracket2 = Global.Microsoft.VisualBasic.ChrW(123)
        Me.FastColoredTextBox1.Location = New System.Drawing.Point(0, 0)
        Me.FastColoredTextBox1.Name = "FastColoredTextBox1"
        Me.FastColoredTextBox1.Paddings = New System.Windows.Forms.Padding(0)
        Me.FastColoredTextBox1.ReadOnly = True
        Me.FastColoredTextBox1.RightBracket = Global.Microsoft.VisualBasic.ChrW(41)
        Me.FastColoredTextBox1.RightBracket2 = Global.Microsoft.VisualBasic.ChrW(125)
        Me.FastColoredTextBox1.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.FastColoredTextBox1.ServiceColors = CType(resources.GetObject("FastColoredTextBox1.ServiceColors"), FastColoredTextBoxNS.ServiceColors)
        Me.FastColoredTextBox1.Size = New System.Drawing.Size(257, 596)
        Me.FastColoredTextBox1.TabIndex = 0
        Me.FastColoredTextBox1.Text = "FastColoredTextBox1"
        Me.FastColoredTextBox1.Zoom = 100
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.FlowNew)
        Me.FlowLayoutPanel1.Controls.Add(Me.FlowLayoutPanel2)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(135, 0)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(62, 602)
        Me.FlowLayoutPanel1.TabIndex = 6
        '
        'FlowNew
        '
        Me.FlowNew.AutoSize = True
        Me.FlowNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.FlowNew.Controls.Add(Me.조건Btn)
        Me.FlowNew.Controls.Add(Me.액션Btn)
        Me.FlowNew.Controls.Add(Me.대기하기Btn)
        Me.FlowNew.Controls.Add(Me.함수Btn)
        Me.FlowNew.Controls.Add(Me.IfBtn)
        Me.FlowNew.Controls.Add(Me.IfElseBtn)
        Me.FlowNew.Controls.Add(Me.ForBtn)
        Me.FlowNew.Controls.Add(Me.WhileBtn)
        Me.FlowNew.Location = New System.Drawing.Point(0, 0)
        Me.FlowNew.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowNew.Name = "FlowNew"
        Me.FlowNew.Size = New System.Drawing.Size(65, 304)
        Me.FlowNew.TabIndex = 21
        '
        '조건Btn
        '
        Me.조건Btn.BackColor = System.Drawing.SystemColors.Control
        Me.조건Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.조건Btn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.조건Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.조건Btn.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.조건Btn.Location = New System.Drawing.Point(1, 1)
        Me.조건Btn.Margin = New System.Windows.Forms.Padding(1)
        Me.조건Btn.Name = "조건Btn"
        Me.조건Btn.Size = New System.Drawing.Size(63, 36)
        Me.조건Btn.TabIndex = 19
        Me.조건Btn.Text = "조건(&C)"
        Me.조건Btn.UseVisualStyleBackColor = False
        '
        '액션Btn
        '
        Me.액션Btn.BackColor = System.Drawing.SystemColors.Control
        Me.액션Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.액션Btn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.액션Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.액션Btn.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.액션Btn.Location = New System.Drawing.Point(1, 39)
        Me.액션Btn.Margin = New System.Windows.Forms.Padding(1)
        Me.액션Btn.Name = "액션Btn"
        Me.액션Btn.Size = New System.Drawing.Size(63, 36)
        Me.액션Btn.TabIndex = 20
        Me.액션Btn.Text = "액션(&A)"
        Me.액션Btn.UseVisualStyleBackColor = False
        '
        '대기하기Btn
        '
        Me.대기하기Btn.BackColor = System.Drawing.SystemColors.Control
        Me.대기하기Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.대기하기Btn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.대기하기Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.대기하기Btn.Font = New System.Drawing.Font("맑은 고딕", 8.0!)
        Me.대기하기Btn.Location = New System.Drawing.Point(1, 77)
        Me.대기하기Btn.Margin = New System.Windows.Forms.Padding(1)
        Me.대기하기Btn.Name = "대기하기Btn"
        Me.대기하기Btn.Size = New System.Drawing.Size(63, 36)
        Me.대기하기Btn.TabIndex = 26
        Me.대기하기Btn.Text = "대기하기(&W)"
        Me.대기하기Btn.UseVisualStyleBackColor = False
        '
        '함수Btn
        '
        Me.함수Btn.BackColor = System.Drawing.SystemColors.Control
        Me.함수Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.함수Btn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.함수Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.함수Btn.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.함수Btn.Location = New System.Drawing.Point(1, 115)
        Me.함수Btn.Margin = New System.Windows.Forms.Padding(1)
        Me.함수Btn.Name = "함수Btn"
        Me.함수Btn.Size = New System.Drawing.Size(63, 36)
        Me.함수Btn.TabIndex = 21
        Me.함수Btn.Text = "함수(&F)"
        Me.함수Btn.UseVisualStyleBackColor = False
        '
        'IfBtn
        '
        Me.IfBtn.BackColor = System.Drawing.SystemColors.Control
        Me.IfBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.IfBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.IfBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IfBtn.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.IfBtn.Location = New System.Drawing.Point(1, 153)
        Me.IfBtn.Margin = New System.Windows.Forms.Padding(1)
        Me.IfBtn.Name = "IfBtn"
        Me.IfBtn.Size = New System.Drawing.Size(63, 36)
        Me.IfBtn.TabIndex = 22
        Me.IfBtn.Text = "IF(&I)"
        Me.IfBtn.UseVisualStyleBackColor = False
        '
        'IfElseBtn
        '
        Me.IfElseBtn.BackColor = System.Drawing.SystemColors.Control
        Me.IfElseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.IfElseBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.IfElseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IfElseBtn.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.IfElseBtn.Location = New System.Drawing.Point(1, 191)
        Me.IfElseBtn.Margin = New System.Windows.Forms.Padding(1)
        Me.IfElseBtn.Name = "IfElseBtn"
        Me.IfElseBtn.Size = New System.Drawing.Size(63, 36)
        Me.IfElseBtn.TabIndex = 23
        Me.IfElseBtn.Text = "IFlse(&E)"
        Me.IfElseBtn.UseVisualStyleBackColor = False
        '
        'ForBtn
        '
        Me.ForBtn.BackColor = System.Drawing.SystemColors.Control
        Me.ForBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ForBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.ForBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ForBtn.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.ForBtn.Location = New System.Drawing.Point(1, 229)
        Me.ForBtn.Margin = New System.Windows.Forms.Padding(1)
        Me.ForBtn.Name = "ForBtn"
        Me.ForBtn.Size = New System.Drawing.Size(63, 36)
        Me.ForBtn.TabIndex = 24
        Me.ForBtn.Text = "For(&R)"
        Me.ForBtn.UseVisualStyleBackColor = False
        '
        'WhileBtn
        '
        Me.WhileBtn.BackColor = System.Drawing.SystemColors.Control
        Me.WhileBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.WhileBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.WhileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.WhileBtn.Font = New System.Drawing.Font("맑은 고딕", 8.0!)
        Me.WhileBtn.Location = New System.Drawing.Point(1, 267)
        Me.WhileBtn.Margin = New System.Windows.Forms.Padding(1)
        Me.WhileBtn.Name = "WhileBtn"
        Me.WhileBtn.Size = New System.Drawing.Size(63, 36)
        Me.WhileBtn.TabIndex = 25
        Me.WhileBtn.Text = "While(&H)"
        Me.WhileBtn.UseVisualStyleBackColor = False
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.AutoSize = True
        Me.FlowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.FlowLayoutPanel2.Controls.Add(Me.함수정의Btn)
        Me.FlowLayoutPanel2.Controls.Add(Me.인수Btn)
        Me.FlowLayoutPanel2.Controls.Add(Me.함수저장Btn)
        Me.FlowLayoutPanel2.Controls.Add(Me.함수불러오기Btn)
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(0, 304)
        Me.FlowLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(65, 152)
        Me.FlowLayoutPanel2.TabIndex = 22
        '
        '함수정의Btn
        '
        Me.함수정의Btn.BackColor = System.Drawing.SystemColors.Control
        Me.함수정의Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.함수정의Btn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.함수정의Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.함수정의Btn.Font = New System.Drawing.Font("맑은 고딕", 8.0!)
        Me.함수정의Btn.Location = New System.Drawing.Point(1, 1)
        Me.함수정의Btn.Margin = New System.Windows.Forms.Padding(1)
        Me.함수정의Btn.Name = "함수정의Btn"
        Me.함수정의Btn.Size = New System.Drawing.Size(63, 36)
        Me.함수정의Btn.TabIndex = 23
        Me.함수정의Btn.Text = "새함수(&N)"
        Me.함수정의Btn.UseVisualStyleBackColor = False
        '
        '인수Btn
        '
        Me.인수Btn.BackColor = System.Drawing.SystemColors.Control
        Me.인수Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.인수Btn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.인수Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.인수Btn.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.인수Btn.Location = New System.Drawing.Point(1, 39)
        Me.인수Btn.Margin = New System.Windows.Forms.Padding(1)
        Me.인수Btn.Name = "인수Btn"
        Me.인수Btn.Size = New System.Drawing.Size(63, 36)
        Me.인수Btn.TabIndex = 22
        Me.인수Btn.Text = "인자(&T)"
        Me.인수Btn.UseVisualStyleBackColor = False
        '
        '함수저장Btn
        '
        Me.함수저장Btn.BackColor = System.Drawing.SystemColors.Control
        Me.함수저장Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.함수저장Btn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.함수저장Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.함수저장Btn.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.함수저장Btn.Location = New System.Drawing.Point(1, 77)
        Me.함수저장Btn.Margin = New System.Windows.Forms.Padding(1)
        Me.함수저장Btn.Name = "함수저장Btn"
        Me.함수저장Btn.Size = New System.Drawing.Size(63, 36)
        Me.함수저장Btn.TabIndex = 20
        Me.함수저장Btn.Text = "저장(&S)"
        Me.함수저장Btn.UseVisualStyleBackColor = False
        '
        '함수불러오기Btn
        '
        Me.함수불러오기Btn.BackColor = System.Drawing.SystemColors.Control
        Me.함수불러오기Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.함수불러오기Btn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.함수불러오기Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.함수불러오기Btn.Font = New System.Drawing.Font("맑은 고딕", 8.0!)
        Me.함수불러오기Btn.Location = New System.Drawing.Point(1, 115)
        Me.함수불러오기Btn.Margin = New System.Windows.Forms.Padding(1)
        Me.함수불러오기Btn.Name = "함수불러오기Btn"
        Me.함수불러오기Btn.Size = New System.Drawing.Size(63, 36)
        Me.함수불러오기Btn.TabIndex = 21
        Me.함수불러오기Btn.Text = "불러오기(&L)"
        Me.함수불러오기Btn.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 200
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Trigger파일|*.tf"
        Me.OpenFileDialog1.Tag = ""
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "Trigger파일|*.tf"
        Me.SaveFileDialog1.Tag = ""
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.Filter = "Funciton파일|*.tfn"
        Me.OpenFileDialog2.Multiselect = True
        Me.OpenFileDialog2.Tag = ""
        '
        'SaveFileDialog2
        '
        Me.SaveFileDialog2.Filter = "Funciton파일|*.tfn"
        Me.SaveFileDialog2.Tag = ""
        '
        'TrigEditorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(755, 669)
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MinimumSize = New System.Drawing.Size(347, 308)
        Me.Name = "TrigEditorForm"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "TriggerEditor"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.FlowLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.FastColoredTextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.FlowNew.ResumeLayout(False)
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents 파일FToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents FlowLayoutPanel3 As FlowLayoutPanel
    Friend WithEvents btn_NewFile As Button
    Friend WithEvents btn_OpenFile As Button
    Friend WithEvents btn_Save As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents 새로만들기NToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 열기OToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 프로젝트저장ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 파일로저장AToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents Button4 As Button
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents 새로만들기NToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents 복사VToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 붙혀넣기CToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 삭제DToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 잘라내기XToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 수정ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 조건ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 액션ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents For문ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents While문ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents If문ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IfElse문ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FastColoredTextBox1 As FastColoredTextBoxNS.FastColoredTextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Button14 As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents OpenFileDialog2 As OpenFileDialog
    Friend WithEvents SaveFileDialog2 As SaveFileDialog
    Friend WithEvents 함수FToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 함수정의ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 인수ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 함수ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 함수불러오기ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 함수저장ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents FlowNew As FlowLayoutPanel
    Friend WithEvents 조건Btn As Button
    Friend WithEvents 액션Btn As Button
    Friend WithEvents 함수Btn As Button
    Friend WithEvents IfBtn As Button
    Friend WithEvents IfElseBtn As Button
    Friend WithEvents ForBtn As Button
    Friend WithEvents WhileBtn As Button
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents 함수저장Btn As Button
    Friend WithEvents 함수불러오기Btn As Button
    Friend WithEvents 인수Btn As Button
    Friend WithEvents 함수정의Btn As Button
    Friend WithEvents 대기하기Btn As Button
    Friend WithEvents 대기하기ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Btn_OpenCont As Button
    Friend WithEvents OpenCont As ToolStripMenuItem
End Class
