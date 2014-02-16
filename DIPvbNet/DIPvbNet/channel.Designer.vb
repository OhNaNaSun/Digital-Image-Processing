<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class channel
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
        Me.components = New System.ComponentModel.Container()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RRGGBBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RRGBBGToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RGGRBBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RGGBBRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RBGGBRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RBGRBGToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.PictureBox1.Location = New System.Drawing.Point(12, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 270)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.ContextMenuStrip = Me.ContextMenuStrip1
        Me.PictureBox2.Location = New System.Drawing.Point(291, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(270, 270)
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.ContextMenuStrip = Me.ContextMenuStrip1
        Me.PictureBox3.Location = New System.Drawing.Point(12, 276)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(270, 270)
        Me.PictureBox3.TabIndex = 2
        Me.PictureBox3.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.ContextMenuStrip = Me.ContextMenuStrip1
        Me.PictureBox4.Location = New System.Drawing.Point(291, 276)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(270, 270)
        Me.PictureBox4.TabIndex = 3
        Me.PictureBox4.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RRGGBBToolStripMenuItem, Me.RRGBBGToolStripMenuItem, Me.RGGRBBToolStripMenuItem, Me.RGGBBRToolStripMenuItem, Me.RBGGBRToolStripMenuItem, Me.RBGRBGToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(175, 136)
        '
        'RRGGBBToolStripMenuItem
        '
        Me.RRGGBBToolStripMenuItem.Name = "RRGGBBToolStripMenuItem"
        Me.RRGGBBToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.RRGGBBToolStripMenuItem.Text = "R->R,G->G,B->B"
        '
        'RRGBBGToolStripMenuItem
        '
        Me.RRGBBGToolStripMenuItem.Name = "RRGBBGToolStripMenuItem"
        Me.RRGBBGToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.RRGBBGToolStripMenuItem.Text = "R->R,G->B,B->G"
        '
        'RGGRBBToolStripMenuItem
        '
        Me.RGGRBBToolStripMenuItem.Name = "RGGRBBToolStripMenuItem"
        Me.RGGRBBToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.RGGRBBToolStripMenuItem.Text = "R->G,G->R,B->B"
        '
        'RGGBBRToolStripMenuItem
        '
        Me.RGGBBRToolStripMenuItem.Name = "RGGBBRToolStripMenuItem"
        Me.RGGBBRToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.RGGBBRToolStripMenuItem.Text = "R->G,G->B,B->R"
        '
        'RBGGBRToolStripMenuItem
        '
        Me.RBGGBRToolStripMenuItem.Name = "RBGGBRToolStripMenuItem"
        Me.RBGGBRToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.RBGGBRToolStripMenuItem.Text = "R->B,G->G,B->R"
        '
        'RBGRBGToolStripMenuItem
        '
        Me.RBGRBGToolStripMenuItem.Name = "RBGRBGToolStripMenuItem"
        Me.RBGRBGToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.RBGRBGToolStripMenuItem.Text = "R->B,G->R,B->G"
        '
        'channel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(576, 555)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "channel"
        Me.Text = "channel"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RRGGBBToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RRGBBGToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RGGRBBToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RGGBBRToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBGGBRToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBGRBGToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
