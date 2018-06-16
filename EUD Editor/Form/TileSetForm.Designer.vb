<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TileSetForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TileSetForm))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.MiniMap = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.VScrollBar2 = New System.Windows.Forms.VScrollBar()
        Me.PaintPal = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Map = New System.Windows.Forms.PictureBox()
        Me.HScrollBar1 = New System.Windows.Forms.HScrollBar()
        Me.VScrollBar1 = New System.Windows.Forms.VScrollBar()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton15 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton11 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton12 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ColorCycle = New System.Windows.Forms.Timer(Me.components)
        Me.PalletTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.파일FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.저장SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.편집EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.실행취소UToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.다시실행RToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.MiniMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.PaintPal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.Map, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 274.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1310, 663)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.MiniMap, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel5, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 128.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(274, 663)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'MiniMap
        '
        Me.MiniMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.MiniMap.Location = New System.Drawing.Point(64, 0)
        Me.MiniMap.Margin = New System.Windows.Forms.Padding(64, 0, 0, 0)
        Me.MiniMap.Name = "MiniMap"
        Me.MiniMap.Size = New System.Drawing.Size(128, 128)
        Me.MiniMap.TabIndex = 3
        Me.MiniMap.TabStop = False
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.VScrollBar2, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.PaintPal, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 128)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(274, 535)
        Me.TableLayoutPanel5.TabIndex = 4
        '
        'VScrollBar2
        '
        Me.VScrollBar2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VScrollBar2.LargeChange = 32
        Me.VScrollBar2.Location = New System.Drawing.Point(254, 0)
        Me.VScrollBar2.Name = "VScrollBar2"
        Me.VScrollBar2.Size = New System.Drawing.Size(20, 535)
        Me.VScrollBar2.SmallChange = 32
        Me.VScrollBar2.TabIndex = 6
        '
        'PaintPal
        '
        Me.PaintPal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PaintPal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PaintPal.Location = New System.Drawing.Point(0, 0)
        Me.PaintPal.Margin = New System.Windows.Forms.Padding(0)
        Me.PaintPal.Name = "PaintPal"
        Me.PaintPal.Size = New System.Drawing.Size(254, 535)
        Me.PaintPal.TabIndex = 4
        Me.PaintPal.TabStop = False
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(274, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1036, 663)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.HScrollBar1, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.VScrollBar1, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 48)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1036, 615)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Map)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1010, 584)
        Me.Panel2.TabIndex = 2
        '
        'Map
        '
        Me.Map.Location = New System.Drawing.Point(0, 0)
        Me.Map.Margin = New System.Windows.Forms.Padding(0)
        Me.Map.Name = "Map"
        Me.Map.Size = New System.Drawing.Size(2000, 2000)
        Me.Map.TabIndex = 3
        Me.Map.TabStop = False
        '
        'HScrollBar1
        '
        Me.HScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HScrollBar1.Location = New System.Drawing.Point(0, 590)
        Me.HScrollBar1.Name = "HScrollBar1"
        Me.HScrollBar1.Size = New System.Drawing.Size(1016, 25)
        Me.HScrollBar1.TabIndex = 4
        '
        'VScrollBar1
        '
        Me.VScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VScrollBar1.Location = New System.Drawing.Point(1016, 0)
        Me.VScrollBar1.Name = "VScrollBar1"
        Me.VScrollBar1.Size = New System.Drawing.Size(20, 590)
        Me.VScrollBar1.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.FlowLayoutPanel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1036, 48)
        Me.Panel1.TabIndex = 2
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.ToolStrip1)
        Me.FlowLayoutPanel1.Controls.Add(Me.ProgressBar1)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(1036, 48)
        Me.FlowLayoutPanel1.TabIndex = 9
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(25, 25)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripButton3, Me.ToolStripSeparator1, Me.ToolStripButton15, Me.ToolStripSeparator5, Me.ToolStripButton11, Me.ToolStripButton12, Me.ToolStripSeparator2, Me.ToolStripButton4, Me.ToolStripSeparator4, Me.ToolStripButton5, Me.ToolStripButton6, Me.ToolStripButton7, Me.ToolStripSeparator3, Me.ToolStripButton8, Me.ToolStripButton9, Me.ToolStripButton10})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(668, 47)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = Global.EUD_Editor.My.Resources.Resources.Save
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(35, 44)
        Me.ToolStripButton1.Text = "저장"
        Me.ToolStripButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Enabled = False
        Me.ToolStripButton2.Image = Global.EUD_Editor.My.Resources.Resources.MostRecentlyUsed
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(59, 44)
        Me.ToolStripButton2.Text = "실행취소"
        Me.ToolStripButton2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Enabled = False
        Me.ToolStripButton3.Image = Global.EUD_Editor.My.Resources.Resources.MostRecentlyRedo
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(59, 44)
        Me.ToolStripButton3.Text = "다시실행"
        Me.ToolStripButton3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 47)
        '
        'ToolStripButton15
        '
        Me.ToolStripButton15.CheckOnClick = True
        Me.ToolStripButton15.Image = CType(resources.GetObject("ToolStripButton15.Image"), System.Drawing.Image)
        Me.ToolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton15.Name = "ToolStripButton15"
        Me.ToolStripButton15.Size = New System.Drawing.Size(59, 44)
        Me.ToolStripButton15.Text = "리마스터"
        Me.ToolStripButton15.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton15.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 47)
        '
        'ToolStripButton11
        '
        Me.ToolStripButton11.Image = CType(resources.GetObject("ToolStripButton11.Image"), System.Drawing.Image)
        Me.ToolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton11.Name = "ToolStripButton11"
        Me.ToolStripButton11.Size = New System.Drawing.Size(47, 44)
        Me.ToolStripButton11.Text = "팔레트"
        Me.ToolStripButton11.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton12
        '
        Me.ToolStripButton12.Image = CType(resources.GetObject("ToolStripButton12.Image"), System.Drawing.Image)
        Me.ToolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton12.Name = "ToolStripButton12"
        Me.ToolStripButton12.Size = New System.Drawing.Size(59, 44)
        Me.ToolStripButton12.Text = "지형복사"
        Me.ToolStripButton12.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 47)
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(35, 44)
        Me.ToolStripButton4.Text = "기본"
        Me.ToolStripButton4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 47)
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(59, 44)
        Me.ToolStripButton5.Text = "통행가능"
        Me.ToolStripButton5.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(59, 44)
        Me.ToolStripButton6.Text = "건설가능"
        Me.ToolStripButton6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.Image = CType(resources.GetObject("ToolStripButton7.Image"), System.Drawing.Image)
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(59, 44)
        Me.ToolStripButton7.Text = "통행불능"
        Me.ToolStripButton7.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 47)
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.Image = CType(resources.GetObject("ToolStripButton8.Image"), System.Drawing.Image)
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(35, 44)
        Me.ToolStripButton8.Text = "낮음"
        Me.ToolStripButton8.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton9
        '
        Me.ToolStripButton9.Image = CType(resources.GetObject("ToolStripButton9.Image"), System.Drawing.Image)
        Me.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton9.Name = "ToolStripButton9"
        Me.ToolStripButton9.Size = New System.Drawing.Size(35, 44)
        Me.ToolStripButton9.Text = "중간"
        Me.ToolStripButton9.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton10
        '
        Me.ToolStripButton10.Image = CType(resources.GetObject("ToolStripButton10.Image"), System.Drawing.Image)
        Me.ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton10.Name = "ToolStripButton10"
        Me.ToolStripButton10.Size = New System.Drawing.Size(35, 44)
        Me.ToolStripButton10.Text = "높음"
        Me.ToolStripButton10.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(671, 3)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(201, 26)
        Me.ProgressBar1.TabIndex = 1
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'ColorCycle
        '
        Me.ColorCycle.Enabled = True
        Me.ColorCycle.Interval = 150
        '
        'PalletTimer
        '
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.파일FToolStripMenuItem, Me.편집EToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(905, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        '파일FToolStripMenuItem
        '
        Me.파일FToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.저장SToolStripMenuItem})
        Me.파일FToolStripMenuItem.Name = "파일FToolStripMenuItem"
        Me.파일FToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.파일FToolStripMenuItem.Text = "파일(&F)"
        '
        '저장SToolStripMenuItem
        '
        Me.저장SToolStripMenuItem.Name = "저장SToolStripMenuItem"
        Me.저장SToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.저장SToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.저장SToolStripMenuItem.Text = "저장(&S)"
        '
        '편집EToolStripMenuItem
        '
        Me.편집EToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.실행취소UToolStripMenuItem, Me.다시실행RToolStripMenuItem})
        Me.편집EToolStripMenuItem.Name = "편집EToolStripMenuItem"
        Me.편집EToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.편집EToolStripMenuItem.Text = "편집(&E)"
        '
        '실행취소UToolStripMenuItem
        '
        Me.실행취소UToolStripMenuItem.Name = "실행취소UToolStripMenuItem"
        Me.실행취소UToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.실행취소UToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.실행취소UToolStripMenuItem.Text = "실행 취소(&U)"
        '
        '다시실행RToolStripMenuItem
        '
        Me.다시실행RToolStripMenuItem.Name = "다시실행RToolStripMenuItem"
        Me.다시실행RToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
        Me.다시실행RToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.다시실행RToolStripMenuItem.Text = "다시 실행(&R)"
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 10
        '
        'TileSetForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1310, 663)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "TileSetForm"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "TileSet"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.MiniMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel5.ResumeLayout(False)
        CType(Me.PaintPal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.Map, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents HScrollBar1 As HScrollBar
    Friend WithEvents VScrollBar1 As VScrollBar
    Friend WithEvents MiniMap As PictureBox
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents ColorCycle As Timer
    Friend WithEvents PaintPal As PictureBox
    Friend WithEvents PalletTimer As Timer
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripButton4 As ToolStripButton
    Friend WithEvents ToolStripButton5 As ToolStripButton
    Friend WithEvents ToolStripButton6 As ToolStripButton
    Friend WithEvents ToolStripButton7 As ToolStripButton
    Friend WithEvents ToolStripButton8 As ToolStripButton
    Friend WithEvents ToolStripButton9 As ToolStripButton
    Friend WithEvents ToolStripButton10 As ToolStripButton
    Friend WithEvents ToolStripButton11 As ToolStripButton
    Friend WithEvents ToolStripButton12 As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents 파일FToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 저장SToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 편집EToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 실행취소UToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 다시실행RToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Timer2 As Timer
    Friend WithEvents ToolStripButton15 As ToolStripButton
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents VScrollBar2 As VScrollBar
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Map As PictureBox
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
