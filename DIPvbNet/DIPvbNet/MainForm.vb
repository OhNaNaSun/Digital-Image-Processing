Public Class MainForm
    Public WithEvents mImage As New ImageClass
    Dim ImageName As String ' 记录当前处理图像的路径
    Dim isZoomAll As Boolean
    Private RubberColor As Color, NeedZoomOut As Boolean
    Private mx0 As Single, my0 As Single, mox As Single, moy As Single ' 这些变量在鼠标拉窗口时记录鼠标位置
    Private cmx As Single, cmy As Single ', w As Integer, h As Integer
    Private picMX0 As Single, picMY0 As Single
    Private isZoom As Boolean
    Private winX0 As Single, winY0 As Single
    Dim theRectangle As New Rectangle(New Point(0, 0), New Size(0, 0))
    Dim startPoint As Point

    Private Sub 打开图像ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 打开图像ToolStripMenuItem.Click
        If mImage Is Nothing Or Not mImage.isEmpty Then
            mImage.ReadImage(Application.StartupPath & "\lena.bmp")
        Else
            OpenFileDialog1.ShowDialog()
            If OpenFileDialog1.FileName = "" Or Dir(OpenFileDialog1.FileName) = "" Then
                Return
            Else
                mImage.ReadImage(OpenFileDialog1.FileName)
            End If
        End If
        Panel.Refresh()
    End Sub

    Private Sub Panel_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel.DoubleClick
        isZoomAll = True
        If mImage.isAvailable Then Panel.Refresh()
        isZoomAll = False
    End Sub

    Private Sub Panel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel.MouseDown
        If ((e.Button = Windows.Forms.MouseButtons.Left) And ((Control.ModifierKeys And Keys.Shift) = Keys.Shift)) Then
            picMX0 = e.X  ' 保存鼠标位置
            picMY0 = e.Y
            Panel.Cursor = Cursors.Cross
            isZoom = True
        End If
        ' by using the PointToScreen method to convert form coordinates to
        ' screen coordinates.
        Dim sControl As Control = CType(sender, Control)
        ' Calculate the startPoint by using the PointToScreen 
        ' method.
        startPoint = sControl.PointToScreen(New Point(e.X, e.Y))
    End Sub

    Private Sub Panel_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel.MouseMove
        Dim i As Integer, j As Integer
        If Not mImage.isAvailable Then Exit Sub
        If mImage.ViewX > 0 And mImage.ViewY > 0 Then
            j = mImage.MapToImageX(e.X)
            i = mImage.MapToImageY(e.Y)
            ' ToolStripStatusLabel1.Text = "鼠标在：" & j.ToString("0") & "," & i.ToString("0") & "," & mImage.getGrey(i, j)
        End If
        If (e.Button = Windows.Forms.MouseButtons.Left) And isZoom Then
            ControlPaint.DrawReversibleFrame(theRectangle, RubberColor, FrameStyle.Thick)
            ' Calculate the endpoint and dimensions for the new rectangle, 
            ' again using the PointToScreen method.
            Dim endPoint As Point = CType(sender, Control).PointToScreen(New Point(e.X, e.Y))
            Dim sWidth As Integer = endPoint.X - startPoint.X
            Dim sHeight As Integer = endPoint.Y - startPoint.Y
            theRectangle = New Rectangle(startPoint.X, startPoint.Y, sWidth, sHeight)

            ' Draw the new rectangle by calling DrawReversibleFrame again.  
            ControlPaint.DrawReversibleFrame(theRectangle, RubberColor, FrameStyle.Thick)
        End If
    End Sub

    Private Sub Panel_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel.MouseUp
        If (e.Button = Windows.Forms.MouseButtons.Right) Then
            If ((Control.ModifierKeys And Keys.Shift) = Keys.Shift) Then
                mImage.zoomInOut(1, mImage.MapToImageX(e.X), mImage.MapToImageY(e.Y))
            Else
                mImage.zoomInOut(0, mImage.MapToImageX(e.X), mImage.MapToImageY(e.Y))
            End If
            Panel.Refresh()
        ElseIf (e.Button = Windows.Forms.MouseButtons.Left) And isZoom Then
            ControlPaint.DrawReversibleFrame(theRectangle, RubberColor, FrameStyle.Thick)
            ' Reset the rectangle.
            theRectangle = New Rectangle(0, 0, 0, 0)

            mx0 = picMX0
            my0 = picMY0
            mox = e.X
            moy = e.Y
            Dim xx0 As Single = IIf(mx0 > mox, mox, mx0)
            Dim yy0 As Single = IIf(my0 > moy, moy, my0)
            Dim xx1 As Single = IIf(mx0 > mox, mx0, mox)
            Dim yy1 As Single = IIf(my0 > moy, my0, moy)
            If Math.Abs(xx1 - xx0) > 5.0 Or Math.Abs(yy1 - yy0) > 5.0 Then
                mImage.xWinMin = mImage.MapToImageX(xx0)
                mImage.yWinMin = mImage.MapToImageY(yy0)
                mImage.xWinMax = mImage.MapToImageX(xx1)
                mImage.yWinMax = mImage.MapToImageY(yy1)
                '利用Refresh()调用paint, 而不是mImage.ZoomImage(g)
                Panel.Refresh()
            Else
                mImage.zoomInOut(0, mImage.MapToImageX(e.X), mImage.MapToImageY(e.Y))
                Panel.Refresh()
            End If
        End If
        Panel.Cursor = Cursors.Default
        isZoom = False

    End Sub

    Private Sub Panel_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel.Paint
        If Not mImage.isAvailable Then Return
        mImage.ViewX = Panel.Width - Panel.Margin.Right
        mImage.ViewY = Panel.Height - Panel.Margin.Bottom
        e.Graphics.Clear(Color.White)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.None
        e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        If (isZoomAll) Then
            mImage.zoomExtent(e.Graphics)
        Else
            mImage.ZoomImage(e.Graphics, IIf(NeedZoomOut, 0, 1))
            NeedZoomOut = True
        End If
    End Sub

    Private Sub MainForm_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        isZoomAll = False
        isZoom = False
        RubberColor = Color.Cyan
        NeedZoomOut = True
        ImageName = Application.StartupPath & "\lena.bmp"
        If Dir(ImageName) = "" Then Exit Sub
        mImage.ReadImage(ImageName)
    End Sub

    Private Sub MainForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Panel.Refresh()
    End Sub

    Private Sub 负片ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 负片ToolStripMenuItem.Click
        mImage.Negative()
        Panel.Refresh()
    End Sub

    Private Sub 中心镜像ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 中心镜像ToolStripMenuItem.Click
        mImage.MirrorX()
        mImage.MirrorY()
        'mImage.MirrorO()
        Panel.Refresh()
    End Sub

    Private Sub 水平镜像ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 水平镜像ToolStripMenuItem.Click
        mImage.MirrorX()
        Panel.Refresh()
    End Sub

    Private Sub 垂直镜像ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 垂直镜像ToolStripMenuItem.Click
        mImage.MirrorY()
        Panel.Refresh()
    End Sub

    Private Sub 文件ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 文件ToolStripMenuItem.Click

    End Sub

    Private Sub 编辑ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 编辑ToolStripMenuItem.Click

    End Sub

    Private Sub 灰度直方图ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 灰度直方图ToolStripMenuItem.Click
        mImage.hist(histogram.ToolStripStatusLabel1.Text)
        histogram.Show()
    End Sub

    Private Sub 线性变换ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 线性变换ToolStripMenuItem.Click
        LinearTrans.Show()
        LinearTrans.Focus()
    End Sub

    Private Sub 直方图均衡化ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 直方图均衡化ToolStripMenuItem.Click
        histequal.Show()
        histequal.Focus()
    End Sub

    Private Sub 撤销ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 撤销ToolStripMenuItem.Click
        mImage.Undo()
    End Sub

    Private Sub mImage_ImageChanged() Handles mImage.ImageChanged
        Panel.Refresh()
    End Sub

    Private Sub mImage_palettechanged() Handles mImage.palettechanged
        Panel.Refresh()

    End Sub

    Private Sub mImage_UndoEnabledChanged(ByVal Enabled As Boolean) Handles mImage.UndoEnabledChanged
        撤销ToolStripMenuItem.Enabled = Enabled '改变撤销按钮是否可用
    End Sub

    Private Sub 调色板ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 调色板ToolStripMenuItem.Click
        Palette.ShowDialog()
    End Sub

    Private Sub 灰度化ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 灰度化ToolStripMenuItem.Click
        mImage.togrey()
    End Sub

    Private Sub 加入椒盐噪声ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 加入椒盐噪声ToolStripMenuItem.Click
        mImage.noise()
    End Sub

    Private Sub 中值滤波ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 中值滤波ToolStripMenuItem.Click
        mImage.medianfilter(3, True)
    End Sub

    Private Sub 均值滤波ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 均值滤波ToolStripMenuItem.Click
        mImage.meanfilter(3, True)
    End Sub

    Private Sub 滤波ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 滤波ToolStripMenuItem.Click
        filter.ShowDialog()
    End Sub

    Private Sub 几何变换ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 几何变换ToolStripMenuItem.Click
        geotrans.Show()
    End Sub

    Private Sub 通道ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 通道ToolStripMenuItem.Click
        channel.Show()
    End Sub
    Private Sub 另存为ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 另存为ToolStripMenuItem.Click
        If SaveFileDialog1.showdialog() = Windows.Forms.DialogResult.OK Then
            mImage.saveas(SaveFileDialog1.FileName)
        End If
    End Sub

    Private Sub 旋转ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 旋转ToolStripMenuItem.Click
        mImage.rotate(Val(InputBox("请输入旋转角度")))
    End Sub

    Private Sub 一阶梯度ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 一阶梯度ToolStripMenuItem.Click
        grad.ShowDialog()
    End Sub
End Class
