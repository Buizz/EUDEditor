<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoadTileSetSingleForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoadTileSetSingleForm))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.PaintPal = New System.Windows.Forms.PictureBox()
        Me.VScrollBar2 = New System.Windows.Forms.VScrollBar()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.TabDefacult = New System.Windows.Forms.Button()
        Me.TabMove = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TabHeight = New System.Windows.Forms.TabControl()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PalletTimer = New System.Windows.Forms.Timer(Me.components)
        Me.OpenBMP = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.PaintPal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.TabMove.SuspendLayout()
        Me.TabHeight.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 274.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 351.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel5, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(749, 414)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.PaintPal, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.VScrollBar2, 1, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(274, 414)
        Me.TableLayoutPanel5.TabIndex = 5
        '
        'PaintPal
        '
        Me.PaintPal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PaintPal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PaintPal.Location = New System.Drawing.Point(0, 0)
        Me.PaintPal.Margin = New System.Windows.Forms.Padding(0)
        Me.PaintPal.Name = "PaintPal"
        Me.PaintPal.Size = New System.Drawing.Size(254, 414)
        Me.PaintPal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PaintPal.TabIndex = 4
        Me.PaintPal.TabStop = False
        '
        'VScrollBar2
        '
        Me.VScrollBar2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VScrollBar2.LargeChange = 32
        Me.VScrollBar2.Location = New System.Drawing.Point(254, 0)
        Me.VScrollBar2.Name = "VScrollBar2"
        Me.VScrollBar2.Size = New System.Drawing.Size(20, 414)
        Me.VScrollBar2.SmallChange = 32
        Me.VScrollBar2.TabIndex = 6
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox5)
        Me.Panel1.Controls.Add(Me.GroupBox4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(274, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(475, 414)
        Me.Panel1.TabIndex = 6
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.FlowLayoutPanel1)
        Me.GroupBox5.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(434, 69)
        Me.GroupBox5.TabIndex = 15
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "통행&&높이&&건설가능 설정 도구"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.TabDefacult)
        Me.FlowLayoutPanel1.Controls.Add(Me.TabMove)
        Me.FlowLayoutPanel1.Controls.Add(Me.TabHeight)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 19)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(428, 47)
        Me.FlowLayoutPanel1.TabIndex = 8
        '
        'TabDefacult
        '
        Me.TabDefacult.Location = New System.Drawing.Point(0, 0)
        Me.TabDefacult.Margin = New System.Windows.Forms.Padding(0)
        Me.TabDefacult.Name = "TabDefacult"
        Me.TabDefacult.Size = New System.Drawing.Size(67, 46)
        Me.TabDefacult.TabIndex = 1
        Me.TabDefacult.Text = "기본 값"
        Me.TabDefacult.UseVisualStyleBackColor = True
        '
        'TabMove
        '
        Me.TabMove.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.TabMove.Controls.Add(Me.TabPage1)
        Me.TabMove.Controls.Add(Me.TabPage2)
        Me.TabMove.Controls.Add(Me.TabPage3)
        Me.TabMove.Controls.Add(Me.TabPage4)
        Me.TabMove.ItemSize = New System.Drawing.Size(40, 40)
        Me.TabMove.Location = New System.Drawing.Point(70, 3)
        Me.TabMove.Multiline = True
        Me.TabMove.Name = "TabMove"
        Me.TabMove.Padding = New System.Drawing.Point(0, 0)
        Me.TabMove.SelectedIndex = 0
        Me.TabMove.Size = New System.Drawing.Size(194, 43)
        Me.TabMove.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 87)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(186, 0)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "통행가능"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 87)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(186, 0)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "건설가능"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 87)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(186, 0)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "통행불능"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Location = New System.Drawing.Point(4, 87)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(186, 0)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'TabHeight
        '
        Me.TabHeight.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.TabHeight.Controls.Add(Me.TabPage5)
        Me.TabHeight.Controls.Add(Me.TabPage6)
        Me.TabHeight.Controls.Add(Me.TabPage7)
        Me.TabHeight.Controls.Add(Me.TabPage8)
        Me.TabHeight.ItemSize = New System.Drawing.Size(40, 40)
        Me.TabHeight.Location = New System.Drawing.Point(270, 3)
        Me.TabHeight.Multiline = True
        Me.TabHeight.Name = "TabHeight"
        Me.TabHeight.Padding = New System.Drawing.Point(0, 0)
        Me.TabHeight.SelectedIndex = 0
        Me.TabHeight.Size = New System.Drawing.Size(155, 43)
        Me.TabHeight.TabIndex = 4
        '
        'TabPage5
        '
        Me.TabPage5.Location = New System.Drawing.Point(4, 87)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(147, 0)
        Me.TabPage5.TabIndex = 0
        Me.TabPage5.Text = "낮음"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'TabPage6
        '
        Me.TabPage6.Location = New System.Drawing.Point(4, 87)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(147, 0)
        Me.TabPage6.TabIndex = 1
        Me.TabPage6.Text = "중간"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'TabPage7
        '
        Me.TabPage7.Location = New System.Drawing.Point(4, 87)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(147, 0)
        Me.TabPage7.TabIndex = 2
        Me.TabPage7.Text = "높음"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'TabPage8
        '
        Me.TabPage8.Location = New System.Drawing.Point(4, 87)
        Me.TabPage8.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(147, 0)
        Me.TabPage8.TabIndex = 3
        Me.TabPage8.Text = " "
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.RadioButton1)
        Me.GroupBox4.Controls.Add(Me.Panel2)
        Me.GroupBox4.Controls.Add(Me.RadioButton2)
        Me.GroupBox4.Controls.Add(Me.GroupBox1)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 78)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(332, 319)
        Me.GroupBox4.TabIndex = 8
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "그래픽 정보"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(6, 22)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(117, 19)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.Text = "전체 타일셋 사용"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox6)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Location = New System.Drawing.Point(3, 156)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(318, 155)
        Me.Panel2.TabIndex = 7
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.RadioButton4)
        Me.GroupBox6.Controls.Add(Me.RadioButton3)
        Me.GroupBox6.Location = New System.Drawing.Point(4, 81)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(311, 57)
        Me.GroupBox6.TabIndex = 6
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "타입"
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Location = New System.Drawing.Point(89, 22)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(61, 19)
        Me.RadioButton4.TabIndex = 1
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "참조식"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(6, 22)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(61, 19)
        Me.RadioButton3.TabIndex = 0
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "대입식"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Button4)
        Me.GroupBox3.Controls.Add(Me.Button3)
        Me.GroupBox3.Controls.Add(Me.Button2)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(257, 78)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "파일"
        '
        'Button4
        '
        Me.Button4.Image = Global.EUD_Editor.My.Resources.Resources.Save
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button4.Location = New System.Drawing.Point(7, 22)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(60, 50)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "저장"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Image = Global.EUD_Editor.My.Resources.Resources.Delete
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button3.Location = New System.Drawing.Point(141, 22)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(60, 50)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "비우기"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = Global.EUD_Editor.My.Resources.Resources.Open
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button2.Location = New System.Drawing.Point(73, 22)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(60, 50)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "열기"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button2.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(6, 131)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(77, 19)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.Text = "부분 변경"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button5)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 47)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(311, 78)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "파일"
        '
        'Button5
        '
        Me.Button5.Image = Global.EUD_Editor.My.Resources.Resources.Delete
        Me.Button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button5.Location = New System.Drawing.Point(104, 21)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(97, 24)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "비우기"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(3, 49)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(300, 23)
        Me.TextBox1.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Image = Global.EUD_Editor.My.Resources.Resources.Open
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(3, 22)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(95, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "열기"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PalletTimer
        '
        Me.PalletTimer.Interval = 1
        '
        'OpenBMP
        '
        Me.OpenBMP.Filter = "indexingbitmap|*.bmp"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "indexingbitmap|*.bmp"
        '
        'LoadTileSetSingleForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 414)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "LoadTileSetSingleForm"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "TileSetManager"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        CType(Me.PaintPal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.TabMove.ResumeLayout(False)
        Me.TabHeight.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents PaintPal As PictureBox
    Friend WithEvents VScrollBar2 As VScrollBar
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents PalletTimer As Timer
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents TabMove As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabDefacult As Button
    Friend WithEvents TabHeight As TabControl
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabPage8 As TabPage
    Friend WithEvents OpenBMP As OpenFileDialog
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents RadioButton4 As RadioButton
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents Button5 As Button
End Class
