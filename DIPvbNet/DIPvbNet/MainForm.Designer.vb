<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.文件ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.打开图像ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.另存为ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.编辑ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.撤销ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.负片ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.水平镜像ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.中心镜像ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.垂直镜像ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.灰度直方图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.线性变换ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.直方图均衡化ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.调色板ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.灰度化ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.加入椒盐噪声ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.中值滤波ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.均值滤波ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.滤波ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.几何变换ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.通道ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.旋转ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.一阶梯度ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Panel = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.Panel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.文件ToolStripMenuItem, Me.编辑ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(766, 25)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '文件ToolStripMenuItem
        '
        Me.文件ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.打开图像ToolStripMenuItem, Me.另存为ToolStripMenuItem})
        Me.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem"
        Me.文件ToolStripMenuItem.Size = New System.Drawing.Size(44, 21)
        Me.文件ToolStripMenuItem.Text = "文件"
        '
        '打开图像ToolStripMenuItem
        '
        Me.打开图像ToolStripMenuItem.Name = "打开图像ToolStripMenuItem"
        Me.打开图像ToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.打开图像ToolStripMenuItem.Text = "打开"
        '
        '另存为ToolStripMenuItem
        '
        Me.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem"
        Me.另存为ToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.另存为ToolStripMenuItem.Text = "另存为"
        '
        '编辑ToolStripMenuItem
        '
        Me.编辑ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.撤销ToolStripMenuItem, Me.负片ToolStripMenuItem, Me.水平镜像ToolStripMenuItem, Me.中心镜像ToolStripMenuItem, Me.垂直镜像ToolStripMenuItem, Me.灰度直方图ToolStripMenuItem, Me.线性变换ToolStripMenuItem, Me.直方图均衡化ToolStripMenuItem, Me.调色板ToolStripMenuItem, Me.灰度化ToolStripMenuItem, Me.加入椒盐噪声ToolStripMenuItem, Me.中值滤波ToolStripMenuItem, Me.均值滤波ToolStripMenuItem, Me.滤波ToolStripMenuItem, Me.几何变换ToolStripMenuItem, Me.通道ToolStripMenuItem, Me.旋转ToolStripMenuItem, Me.一阶梯度ToolStripMenuItem})
        Me.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem"
        Me.编辑ToolStripMenuItem.Size = New System.Drawing.Size(44, 21)
        Me.编辑ToolStripMenuItem.Text = "编辑"
        '
        '撤销ToolStripMenuItem
        '
        Me.撤销ToolStripMenuItem.Enabled = False
        Me.撤销ToolStripMenuItem.Name = "撤销ToolStripMenuItem"
        Me.撤销ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.撤销ToolStripMenuItem.Text = "撤销"
        '
        '负片ToolStripMenuItem
        '
        Me.负片ToolStripMenuItem.Name = "负片ToolStripMenuItem"
        Me.负片ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.负片ToolStripMenuItem.Text = "负片"
        '
        '水平镜像ToolStripMenuItem
        '
        Me.水平镜像ToolStripMenuItem.Name = "水平镜像ToolStripMenuItem"
        Me.水平镜像ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.水平镜像ToolStripMenuItem.Text = "水平镜像"
        '
        '中心镜像ToolStripMenuItem
        '
        Me.中心镜像ToolStripMenuItem.Name = "中心镜像ToolStripMenuItem"
        Me.中心镜像ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.中心镜像ToolStripMenuItem.Text = "中心镜像"
        '
        '垂直镜像ToolStripMenuItem
        '
        Me.垂直镜像ToolStripMenuItem.Name = "垂直镜像ToolStripMenuItem"
        Me.垂直镜像ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.垂直镜像ToolStripMenuItem.Text = "垂直镜像"
        '
        '灰度直方图ToolStripMenuItem
        '
        Me.灰度直方图ToolStripMenuItem.Name = "灰度直方图ToolStripMenuItem"
        Me.灰度直方图ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.灰度直方图ToolStripMenuItem.Text = "直方图"
        '
        '线性变换ToolStripMenuItem
        '
        Me.线性变换ToolStripMenuItem.Name = "线性变换ToolStripMenuItem"
        Me.线性变换ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.线性变换ToolStripMenuItem.Text = "线性变换"
        '
        '直方图均衡化ToolStripMenuItem
        '
        Me.直方图均衡化ToolStripMenuItem.Name = "直方图均衡化ToolStripMenuItem"
        Me.直方图均衡化ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.直方图均衡化ToolStripMenuItem.Text = "直方图均衡化"
        '
        '调色板ToolStripMenuItem
        '
        Me.调色板ToolStripMenuItem.Name = "调色板ToolStripMenuItem"
        Me.调色板ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.调色板ToolStripMenuItem.Text = "调色板"
        '
        '灰度化ToolStripMenuItem
        '
        Me.灰度化ToolStripMenuItem.Name = "灰度化ToolStripMenuItem"
        Me.灰度化ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.灰度化ToolStripMenuItem.Text = "灰度化"
        '
        '加入椒盐噪声ToolStripMenuItem
        '
        Me.加入椒盐噪声ToolStripMenuItem.Name = "加入椒盐噪声ToolStripMenuItem"
        Me.加入椒盐噪声ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.加入椒盐噪声ToolStripMenuItem.Text = "加入椒盐噪声"
        '
        '中值滤波ToolStripMenuItem
        '
        Me.中值滤波ToolStripMenuItem.Name = "中值滤波ToolStripMenuItem"
        Me.中值滤波ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.中值滤波ToolStripMenuItem.Text = "中值滤波"
        '
        '均值滤波ToolStripMenuItem
        '
        Me.均值滤波ToolStripMenuItem.Name = "均值滤波ToolStripMenuItem"
        Me.均值滤波ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.均值滤波ToolStripMenuItem.Text = "均值滤波"
        '
        '滤波ToolStripMenuItem
        '
        Me.滤波ToolStripMenuItem.Name = "滤波ToolStripMenuItem"
        Me.滤波ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.滤波ToolStripMenuItem.Text = "滤波"
        '
        '几何变换ToolStripMenuItem
        '
        Me.几何变换ToolStripMenuItem.Name = "几何变换ToolStripMenuItem"
        Me.几何变换ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.几何变换ToolStripMenuItem.Text = "缩放"
        '
        '通道ToolStripMenuItem
        '
        Me.通道ToolStripMenuItem.Name = "通道ToolStripMenuItem"
        Me.通道ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.通道ToolStripMenuItem.Text = "通道"
        '
        '旋转ToolStripMenuItem
        '
        Me.旋转ToolStripMenuItem.Name = "旋转ToolStripMenuItem"
        Me.旋转ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.旋转ToolStripMenuItem.Text = "旋转"
        '
        '一阶梯度ToolStripMenuItem
        '
        Me.一阶梯度ToolStripMenuItem.Name = "一阶梯度ToolStripMenuItem"
        Me.一阶梯度ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.一阶梯度ToolStripMenuItem.Text = "一阶梯度"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 25)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(766, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 521)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(766, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "bmp文件|*.bmp"
        '
        'Panel
        '
        Me.Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel.Location = New System.Drawing.Point(0, 50)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(766, 471)
        Me.Panel.TabIndex = 5
        Me.Panel.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(766, 543)
        Me.Controls.Add(Me.Panel)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainForm"
        Me.Text = "数字图像处理基本功能"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.Panel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 文件ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 打开图像ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 编辑ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 负片ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 水平镜像ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 中心镜像ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents 垂直镜像ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents 灰度直方图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 线性变换ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 直方图均衡化ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel As System.Windows.Forms.PictureBox
    Friend WithEvents 撤销ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 调色板ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 灰度化ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 加入椒盐噪声ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 中值滤波ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 均值滤波ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 滤波ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 几何变换ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 通道ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 另存为ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents 旋转ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 一阶梯度ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
