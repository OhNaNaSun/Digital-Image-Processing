Public Class ImageClass
    Public Enum channel As Byte
        RGB = 3
    End Enum
    Private BMap As Bitmap ' Bitmap是VB.NET和C#中处理图像的基础类，用于处理由像素数据定义的图像的对象
    Private mImageName As String ' 记录打开图像的文件
    Private mImageType As Integer  '记录图像文件的类型    0：灰度图像；1：彩色图像（RGB）
    Private mPixels As Long, mSize As Long ' 记录图像的像素个数及存储数据的内存大小（以字节为单位）
    Private mWidth As Long, mHeight As Long ' 图像宽度、高度
    Private ImageB() As Byte ' 存储灰度图像数据，一维数组存储。对于8位图像，一个像素
    ' 即是一个字节。每行存储的字节数必须是4的整倍数，需要时添加适当字节。
    Private mFwidth As Long ' 内存中一行像素所占用的内存字节数 mFwidth = (mWidth+3)\4*4
    Private ImageC() As Byte ' 存储彩色图像数据，也是一个一维数组存储。对于24位图像，一个像素
    ' 即是三个字节。每行存储的字节数是像素个数的三倍。每行存储的字节数还需要确保是
    ' 4的整倍数，需要时添加适当字节。
    Private CSize As Long ' 彩色图像数据的总字节数
    Private Cpos() As Long ' 彩色图像数据每行首字节位置
    Private mCWidth As Long ' 彩色图像每行像素存储的字节数，mCWidth = (mWidth*3+3)\4*4
    Private mHist(255) As Long ' 统计图像的灰度直方图，彩色图像记录亮度
    Private rHist(255) As Long ' 红色直方图数据
    Private bHist(255) As Long ' 蓝色直方图数据
    Private gHist(255) As Long ' 绿色直方图数据
    Private isOpened As Boolean
    Private xView As Double, yView As Double
    Private xWmin As Double, yWmin As Double, xWmax As Double, yWmax As Double
    Private xWmin0 As Double, yWmin0 As Double, xWmax0 As Double, yWmax0 As Double
    Private mLastScale As Double
    ' 界面控件
    Private OldBMap(0) As Bitmap '存储以前的图像数据
    Private CurrentEdit As SByte = -1 '当前修改次数。用于计算撤销的次数
    Private mPanel As PictureBox ' 绘制图片的控件
    Private v(255) As Single '灰度直方图频率数据
    Public pBar As ToolStripProgressBar
    Public Event UndoEnabledChanged(ByVal Enabled As Boolean)
    Public Event ImageChanged()
    Public Event palettechanged()
    Public Event histchanged()

    ' 读取图像文件。实现图像对象由文件初始化的过程
    Public Function ReadImage(ByVal ImageName As String) As Integer
        If (Dir(ImageName) = "") Then     '
            Return 1
        End If
        If Not (BMap Is Nothing) Then
            BMap.Dispose()
        End If
        BMap = New Bitmap(ImageName)
        getBitMapData()
        xWmin = 0
        yWmin = 0
        xWmax = BMap.Width - 1    ' 取得pictureBox在容器中的宽度
        yWmax = BMap.Height - 1   ' 取得pictureBox在容器中的高度
        mImageName = ImageName
        isOpened = True
    End Function

    Public Sub zoomExtent(ByVal e As Drawing.Graphics)
        If Not isOpened Then Return
        xWmin = 0
        yWmin = 0
        xWmax = BMap.Width - 1    ' 取得pictureBox在容器中的宽度
        yWmax = BMap.Height - 1   ' 取得pictureBox在容器中的高度
        ZoomImage(e)
    End Sub

    Public Function ZoomImage(ByVal e As Drawing.Graphics, Optional ByVal flag As Integer = 0) As Integer
        Dim Vx As Double, Vy As Double, wX As Double, wY As Double
        Dim s As Double
        If BMap Is Nothing Then Return 1
        If Not isOpened Then Return 1

        Vx = xView     ' 计算视图（图版）的宽度(要求)
        Vy = yView     ' 计算视图（图版）的高度(要求)
        wX = xWmax - xWmin ' 计算图像中要求显示的拉框宽度
        wY = yWmax - yWmin ' 计算图像中要求显示的拉框高度
        If wX = 0 And wY = 0 Then ' 避免还没有打开图像就执行该函数
            Exit Function
        End If
        If (flag = 0) Or (mLastScale < 0.0000000001) Then  ' flag <> 0 使用原来的比例关系绘制图像
            s = wX / Vx             ' 宽度比例
            If s < wY / Vy Then     ' 与高度比例比较，取较大者
                s = wY / Vy
            End If
            mLastScale = s
        Else
            s = mLastScale
        End If
        '  注意： 主要由给定的xWmin,yWmin,xWmax,yWmax确定
        xWmin = xWmin + (wX - Vx * s) / 2.0#  ' 调整显示位置，保证图像居中显示
        xWmax = xWmin + Vx * s
        yWmin = yWmin + (wY - Vy * s) / 2.0#
        yWmax = yWmin + Vy * s

        Dim TargetRec(2) As PointF
        TargetRec(0).X = 0 ' 左上角
        TargetRec(0).Y = 0
        TargetRec(1).X = xView - 1 ' 右上角
        TargetRec(1).Y = 0
        TargetRec(2).X = 0 ' 左下角
        TargetRec(2).Y = yView - 1

        Dim xLeft As Integer = xWmin
        Dim xWidth As Integer = xWmax - xWmin + 1
        Dim yUpper As Integer = yWmin
        Dim yHeight As Integer = yWmax - yWmin + 1

        Dim srcRect As New Rectangle(xLeft, yUpper, xWidth, yHeight)
        Dim units As GraphicsUnit = GraphicsUnit.Pixel

        e.DrawImage(BMap, TargetRec, srcRect, units)

        xWmin0 = xWmin
        xWmax0 = xWmax
        yWmin0 = yWmin
        yWmax0 = yWmax
        ZoomImage = 0
    End Function

    Public Sub zoomInOut(Optional ByVal flag As Integer = 0, Optional ByVal MouseX As Single = -100.0, Optional ByVal MouseY As Single = -100.0)
        Dim winWidth As Double, winHeight As Double
        Dim winXc As Double, winYc As Double
        If Not isOpened Then Return
        ' 窗体中心
        winXc = (xWmax + xWmin) / 2.0
        winYc = (yWmax + yWmin) / 2.0
        If MouseX < -10.0 Then
            winXc = 0.0
            winYc = 0.0
        Else
            winXc = winXc - MouseX
            winYc = winYc - MouseY
        End If
        winWidth = xWmax - xWmin
        winHeight = yWmax - yWmin
        If (flag = 0) Then ' Zoom In
            '以窗体中心缩放
            'xWmin = X + x0 * 0.8 - wX * 0.4
            'xWmax = X + x0 * 0.8 + wX * 0.4
            'yWmin = Y + y0 * 0.8 - wY * 0.4
            'yWmax = Y + y0 * 0.8 + wY * 0.4
            ' 以鼠标位置为中心缩放
            xWmin = MouseX + winXc * 0.8 - winWidth * 0.4
            xWmax = MouseX + winXc * 0.8 + winWidth * 0.4
            yWmin = MouseY + winYc * 0.8 - winHeight * 0.4
            yWmax = MouseY + winYc * 0.8 + winHeight * 0.4
        ElseIf (flag = 1) Then ' Zoom Out
            ' 以窗体中心缩放
            'xWmin = X + x0 * 1.25 - wX * 0.625
            'xWmax = X + x0 * 1.25 + wX * 0.625
            'yWmin = Y + y0 * 1.25 - wY * 0.625
            'yWmax = Y + y0 * 1.25 + wY * 0.625
            ' 以下代码是解决以鼠标为缩放中心的问题
            xWmin = MouseX + winXc * 1.25 - winWidth * 0.625
            xWmax = MouseX + winXc * 1.25 + winWidth * 0.625
            yWmin = MouseY + winYc * 1.25 - winHeight * 0.625
            yWmax = MouseY + winYc * 1.25 + winHeight * 0.625
        End If
    End Sub

    Public Function Display(ByRef e As PictureBox, Optional ByVal flag As Integer = 0) As Integer
        Dim g As Graphics
        If Not isOpened Then Return 0
        If flag = 0 Then
            mPanel = e
            xView = e.Width - e.Margin.Right
            yView = e.Height - e.Margin.Bottom
            g = e.CreateGraphics
        Else
            xView = mPanel.Width - mPanel.Margin.Right
            yView = mPanel.Height - mPanel.Margin.Bottom
            g = mPanel.CreateGraphics
        End If
        xWmin = 0
        yWmin = 0
        xWmax = BMap.Width - 1    ' 取得pictureBox在容器中的宽度
        yWmax = BMap.Height - 1   ' 取得pictureBox在容器中的高度
        ZoomImage(g)
    End Function

    Private Function getBitMapData(Optional ByVal Undo As Boolean = False) As Boolean
        ' 从BitMap对象里获取图像数据
        ' 在实际处理程序中一般不采用拷贝备份的做法，可以通过获得的数据指针，直接操作就可以了。
        ' 过程是：锁定内存，获取数据的起始地址，根据图像类型操作数据，结束锁定
        If BMap Is Nothing Then Return False ' 图像对象必须存在，完成实例化
        Dim i As Integer
        Dim rect As New Rectangle(0, 0, BMap.Width, BMap.Height) ' 设置锁定图像范围的矩形
        Dim bmpData As System.Drawing.Imaging.BitmapData = BMap.LockBits(rect, _
            Drawing.Imaging.ImageLockMode.ReadOnly, BMap.PixelFormat) ' 锁定图像数据

        ' Get the address of the first line.
        Dim ptr As IntPtr = bmpData.Scan0 ' 获得图像数据的起始地址

        If BMap.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
            ' 如果图像是8位索引图像，256彩色，256灰度图像
            mWidth = BMap.Width     '获得图像宽度
            mHeight = BMap.Height   '获得图像高度
            mFwidth = ((mWidth + 3) \ 4) * 4 '由于图像数据是每行的记录字节数为4的整倍数，估作此调整计算
            ' mWidth + 3 > mFwidth >= mWidth
            mSize = mFwidth * mHeight  ' 图像数据的大小
            mPixels = mWidth * mHeight ' 图像总像素个数
            ReDim ImageB(mSize - 1) ' 定义一个一维数组，保存图像数据，用于图像数据操作
            System.Runtime.InteropServices.Marshal.Copy(ptr, ImageB, 0, mSize) ' 拷贝数据
            mImageType = 0
        ElseIf BMap.PixelFormat = Imaging.PixelFormat.Format24bppRgb Then
            mWidth = BMap.Width
            mHeight = BMap.Height
            mCWidth = ((mWidth * 3 + 3) \ 4) * 4
            CSize = mCWidth * mHeight
            mPixels = mWidth * mHeight
            ReDim ImageC(CSize - 1)
            ' Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, ImageC, 0, CSize)
            mImageType = 1
            ReDim Cpos(mHeight - 1)  ' Cpos数组纪录每行在ImageC中的起始位置
            For i = 0 To mHeight - 1
                Cpos(i) = i * mCWidth
            Next i
        End If
        hist()
        BMap.UnlockBits(bmpData) ' 解锁锁定的位图数据
        If Undo = False Then
            CurrentEdit = CurrentEdit + 1
            ReDim Preserve OldBMap(CurrentEdit)
            OldBMap(CurrentEdit) = BMap.Clone()
        End If
        RaiseEvent ImageChanged()
        RaiseEvent histchanged()
        Return True
    End Function

    Private Function putBitMapData() As Boolean
        If BMap Is Nothing Then Return False
        Dim rect As New Rectangle(0, 0, BMap.Width, BMap.Height)
        Dim bmpData As System.Drawing.Imaging.BitmapData = BMap.LockBits(rect, _
            Drawing.Imaging.ImageLockMode.WriteOnly, BMap.PixelFormat)

        ' Get the address of the first line.
        Dim ptr As IntPtr = bmpData.Scan0

        If BMap.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
            mSize = mFwidth * mHeight
            System.Runtime.InteropServices.Marshal.Copy(ImageB, 0, ptr, mSize) '负片或者镜像之后，ImageB的数据变了，通过putBitMapData()这个过程把数据变了的ImageB放到内存里，这块内存用于在窗体显示图像
        ElseIf BMap.PixelFormat = Imaging.PixelFormat.Format24bppRgb Then
            CSize = BMap.Width * BMap.Height * 3
            ' Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ImageC, 0, ptr, CSize)
        End If
        hist()
        ' Declare an array to hold the bytes of the bitmap.
        ' This code is specific to a bitmap with 24 bits per pixels.
        ' Unlock the bits.
        BMap.UnlockBits(bmpData)
        CurrentEdit = CurrentEdit + 1
        ReDim Preserve OldBMap(CurrentEdit)
        OldBMap(CurrentEdit) = BMap.Clone()
        RaiseEvent UndoEnabledChanged(True)
        RaiseEvent ImageChanged()
        RaiseEvent histchanged()
        Return True
    End Function

    Public Sub Undo(Optional ByVal Steps As Byte = 1)
        If Steps > CurrentEdit Then Return
        CurrentEdit = CurrentEdit - Steps
        BMap = OldBMap(CurrentEdit).Clone() '如果5撤销2，此时bmap=oldbmap(3).clone()是只取oldbmap中第四个元素么
        getBitMapData(True)
        If CurrentEdit = 0 Then RaiseEvent UndoEnabledChanged(False)
    End Sub
    Public Sub Negative() ' 图像的负片   了解图像数据的组织
        Dim pos As Long
        Dim i As Integer, j As Integer
        If BMap.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    pos = i * mFwidth + j
                    ImageB(pos) = 255 - ImageB(pos)
                Next j
            Next i
            putBitMapData()
        ElseIf BMap.PixelFormat = Imaging.PixelFormat.Format24bppRgb Then
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    pos = Cpos(i) + j * 3
                    ImageC(pos) = 255 - ImageC(pos)
                    ImageC(pos + 1) = 255 - ImageC(pos + 1)
                    ImageC(pos + 2) = 255 - ImageC(pos + 2)
                Next j
            Next i
            putBitMapData()
        End If
    End Sub

    Public Sub MirrorX() ' 图像的上下镜像   进一步了解图像数据的组织
        Dim pos As Long, pos1 As Long
        Dim i As Integer, j As Integer, g As Byte
        '需要实现数据的交换
        If BMap.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
            For i = 0 To mHeight \ 2 - 1
                ' 运行一半行数就可以了
                For j = 0 To mWidth - 1
                    pos = i * mFwidth + j  ' 必须使用mFwidth, 数组中数据为每行字节数为4的整倍数
                    pos1 = (mHeight - i - 1) * mFwidth + j
                    g = ImageB(pos)
                    ImageB(pos) = ImageB(pos1)
                    ImageB(pos1) = g
                Next j
            Next i
            putBitMapData()
        End If
    End Sub

    Public Sub MirrorY() ' 图像的左右镜像   进一步了解图像数据的组织
        Dim pos As Long, pos1 As Long
        Dim i As Integer, j As Integer, g As Byte
        '需要实现数据的交换
        If BMap.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
            For i = 0 To mHeight - 1
                For j = 0 To mWidth \ 2 - 1
                    ' 运行半行就可以了
                    pos = i * mFwidth + j  ' 必须使用mFwidth, 数组中数据为每行字节数为4的整倍数
                    pos1 = i * mFwidth + mWidth - j - 1    '为什么是mfwidth!!!!!!!!改了，应该是mwidth!!!
                    g = ImageB(pos)
                    ImageB(pos) = ImageB(pos1)
                    ImageB(pos1) = g
                Next j
            Next i
            putBitMapData()
        End If
    End Sub

    Public Sub MirrorO() ' 图像的中心镜像   进一步了解图像数据的组织
        Dim pos As Long, pos1 As Long
        Dim i As Integer, j As Integer, g As Byte
        '需要实现数据的交换
        If BMap.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
            For i = 0 To mHeight \ 2 - 1
                ' 运行一半行数就可以了
                For j = 0 To mWidth - 1
                    pos = i * mFwidth + j  ' 必须使用mFwidth, 数组中数据为每行字节数为4的整倍数
                    pos1 = (mHeight - i - 1) * mFwidth + mWidth - j - 1
                    g = ImageB(pos)
                    ImageB(pos) = ImageB(pos1)
                    ImageB(pos1) = g
                Next j
            Next i
            putBitMapData()
        End If
        ' 当行数为奇数时，处理中间一行时只要处理一半就够了，不然会被恢复原始形式,,,,,什么意思？？？！！！！
        ' 实际上，mirrorX和mirrorY也存在这个问题，实际上中间一行交换两次，恢复原始形式
        ' 好像少交换了一行或一列而已，感觉不到
    End Sub

    Public Function MapToImageX(ByVal x As Single) As Single
        Return x / xView * (xWmax0 - xWmin0) + xWmin0
    End Function

    Public Function MapToImageY(ByVal y As Single) As Single
        Return y / yView * (yWmax0 - yWmin0) + yWmin0
    End Function

    Public Property xWinMin() As Double
        Get
            Return xWmin
        End Get
        Set(ByVal value As Double)
            xWmin = value
        End Set
    End Property

    Public Property xWinMax() As Double
        Get
            Return xWmax
        End Get
        Set(ByVal value As Double)
            xWmax = value
        End Set
    End Property

    Public Property yWinMin() As Double
        Get
            Return yWmin
        End Get
        Set(ByVal value As Double)
            yWmin = value
        End Set
    End Property

    Public Property yWinMax() As Double
        Get
            Return yWmax
        End Get
        Set(ByVal value As Double)
            yWmax = value
        End Set
    End Property

    Public Property ViewX() As Double
        Get
            Return xView
        End Get
        Set(ByVal value As Double)
            xView = value
        End Set
    End Property

    Public Property ViewY() As Double
        Get
            Return yView
        End Get
        Set(ByVal value As Double)
            yView = value
        End Set
    End Property

    Public Function isEmpty() As Boolean
        Return isOpened
    End Function

    Public Function isAvailable() As Boolean
        If BMap Is Nothing Or Not isOpened Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Sub New()
        isOpened = False
    End Sub
    '二维变成一维，计算数组中值
    Private Function getmiddle(ByVal A(,) As Byte) As Byte
        Dim i, j As Byte
        Dim mv(A.Length - 1) As Byte
        For i = 0 To UBound(A, 1) '维数从1开始
            For j = 0 To UBound(A, 2)
                mv(i * A.GetLength(0) + j) = A(i, j) 'get维数从0开始
            Next
        Next
        Dim n As Integer = mv.Length
        Dim g As Byte
        For i = 0 To n \ 2
            For j = i To n - 1
                If mv(i) > mv(j) Then
                    g = mv(i)
                    mv(i) = mv(j)
                    mv(j) = g
                End If
            Next
        Next
        Return mv(n \ 2) 'mv(i-1)
    End Function
    Private Function getmean(ByVal A(,) As Byte) As Byte
        Dim i, j As Byte
        Dim sum As Integer = 0
        For i = 0 To UBound(A, 1) '维数从1开始
            For j = 0 To UBound(A, 2)
                sum += A(i, j)
            Next
        Next
        Return sum \ A.Length
    End Function
    Public Sub filter(ByVal H(,) As Single, Optional ByVal copyborder As Boolean = True) '系数矩阵,差别？？？？？
        Dim m As Byte = H.GetLength(0)
        Dim n As Byte = H.GetLength(1)
        Dim windows As Byte(,)
        Dim sum As Short
        Dim imagebcopy(mSize - 1) As Byte
        imagebcopy = ImageB.Clone
        For i As Integer = 0 To mHeight - 1
            For j As Integer = 0 To mWidth - 1
                windows = window(i, j, New Size(m, n), copyborder)
                sum = 0
                For l As Byte = 0 To m - 1
                    For k As Byte = 0 To n - 1
                        sum += H(l, k) * windows(l, k)
                        If sum < 0 Then sum = 0
                        If sum > 255 Then sum = 255
                    Next
                Next
                imagebcopy(i * mFwidth + j) = sum
            Next
        Next
        ImageB = imagebcopy
        putBitMapData()
    End Sub
    '对每个像素（i,j）得到size以便卷积处理，重点边缘操作
    Private Function window(ByVal i As Integer, ByVal j As Integer, ByVal size As Size, ByVal copyborder As Boolean) As Byte(,)
        Dim mrange(size.Width - 1, size.Height - 1) As Byte
        Dim m, n As Integer
        For m = 0 To size.Height - 1 '体会这个循环的诸多微妙之处
            Dim ci As Integer = i - size.Height \ 2 + m
            If ci < 0 Then
                If copyborder = True Then
                    ci = 0
                Else
                    For n = 0 To mWidth - 1 '对N 
                        mrange(m, n) = 0
                    Next
                    Continue For '要放这！！跳出for循环m+1,使其不会进行下面的for n 循环
                End If
                ' Continue For这个不能放在这！！！
            ElseIf ci > mHeight - 1 Then '前面用elseif 
                If copyborder = True Then
                    ci = mHeight - 1
                Else
                    For n = 0 To mWidth - 1
                        mrange(m, n) = 0
                    Next
                    Continue For
                End If
                'Continue For
            End If

            For n = 0 To size.Width - 1
                Dim cj As Integer = j - size.Width \ 2 + n
                If cj < 0 Then
                    If copyborder = True Then
                        cj = 0
                    Else
                        mrange(m, n) = 0
                        Continue For
                    End If
                    'Continue For
                ElseIf cj > mWidth - 1 Then '使用elseif
                    If copyborder = True Then
                        cj = mWidth - 1
                    Else
                        mrange(m, n) = 0
                        Continue For
                    End If
                End If
                mrange(m, n) = ImageB(ci * mWidth + cj)
            Next
        Next
        Return mrange
    End Function

    Public Sub medianfilter(Optional ByVal size As Byte = 3, Optional ByVal copyborder As Boolean = True)
        Dim imagebb(mSize - 1) As Byte '循环过程中要用到imageB中原来的像素值，不是修改后的
        For j As Integer = 0 To mWidth - 1
            For i = 0 To mHeight - 1
                imagebb(i * mFwidth + j) = getmiddle(window(i, j, New Size(size, size), copyborder))
            Next
        Next
        ImageB = imagebb
        putBitMapData()
    End Sub
    Public Sub meanfilter(Optional ByVal size As Byte = 3, Optional ByVal copyborder As Boolean = True)
        Dim imagebb(mSize - 1) As Byte
        For j As Integer = 0 To mWidth - 1
            For i = 0 To mHeight - 1
                imagebb(i * mFwidth + j) = getmean(window(i, j, New Size(size, size), copyborder))
            Next
        Next
        ImageB = imagebb
        putBitMapData()
    End Sub
    Public Sub hist(Optional ByRef text As String = Nothing)
        '统计灰度直方图数据
        Dim i As Long, j As Integer, pos As Long
        '8位索引图像
        If mImageType = 0 Then
            ' Dim n(255) As Integer 'n(i)为灰度为i的像素个数
            For i = 0 To 255
                mHist(i) = 0
            Next i
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    pos = mFwidth * i + j
                    mHist(ImageB(pos)) += 1
                Next j
            Next i
            For i = 0 To 255
                v(i) = mHist(i) / mPixels
            Next i
            '24位(真彩色)图像bgrbgr
            Dim max, min, h, avg, s As Double
            max = 0
            min = mPixels
            If text IsNot Nothing Then
                For i = 0 To 255
                    min = Math.Min(min, mHist(i))
                    max = Math.Max(max, mHist(i))
                    avg += i * mHist(i) / mPixels '图像的平均灰度
                    If mHist(i) <> 0 Then
                        h += -mHist(i) / mPixels * Math.Log(mHist(i) / mPixels) / Math.Log(2) '图像的熵

                    End If
                Next
                For i = 0 To 255
                    s += mHist(i) * (i - avg) ^ 2 / mPixels '标准差啊，就是算 (每个像素的灰度值 - 平均灰度) ^ 2，再求这些数的平均值
                Next
                max = max / mPixels
                min = min / mPixels
                s = Math.Sqrt(s)
                text = "最大值=" & Format(max, "0.00%") & Space(4) & "最小值=" & Format(min, "0.00%") & _
                Space(4) & "熵=" & Format(h, "0.00") & Space(4) & "平均值=" & Format(avg, "0.00") & _
                Space(4) & "标准差=" & Format(s, "0.00")
            End If
        ElseIf mImageType = 1 Then
            For i = 0 To 255
                mHist(i) = 0 '为什么弄成0
                rHist(i) = 0
                gHist(i) = 0
                bHist(i) = 0
            Next
            For i = 0 To mPixels - 1
                Dim m As Byte
                pos = Cpos(i \ mWidth) + 3 * (i Mod mWidth)
                bHist(ImageC(pos)) += 1
                gHist(ImageC(pos + 1)) += 1
                rHist(ImageC(pos + 2)) += 1
                m = 0.299 * ImageC(pos + 2) + 0.587 * ImageC(pos + 1) + 0.114 * ImageC(pos)
                mHist(m) += 1
            Next
            Dim max, min As Double
            max = 0
            min = mPixels
            If text IsNot Nothing Then
                For i = 0 To 255
                    min = Math.Min(min, mHist(i))
                    min = Math.Min(min, rHist(i))
                    min = Math.Min(min, gHist(i))
                    min = Math.Min(min, bHist(i))
                    max = Math.Max(max, mHist(i))
                    max = Math.Max(max, rHist(i))
                    max = Math.Max(max, gHist(i))
                    max = Math.Max(max, bHist(i))
                Next
                min = min / mPixels
                max = max / mPixels
                text = "最大值=" & Format(max, "0.00%") & Space(4) & "最小值=" & Format(min, "0.00%")
            End If
        End If
    End Sub
    'Public ReadOnly Property histogram() As Single()
    '    Get
    '        Return v
    '    End Get
    'End Property
    Public ReadOnly Property MaxUndo() As Byte
        Get
            Return CurrentEdit
        End Get
    End Property
    Public Sub lineartrans(ByVal a As Byte, ByVal b As Byte, ByVal c As Byte, ByVal d As Byte)
        Dim i As Integer
        For i = 0 To mSize - 1
            If ImageB(i) <= a Then
                ImageB(i) = c / a * ImageB(i)
            ElseIf ImageB(i) < b Then
                ImageB(i) = (d - c) / (b - a) * (ImageB(i) - a) + c
            Else
                ImageB(i) = (255 - d) / (255 - b) * (ImageB(i) - b) + d
            End If
        Next
        putBitMapData()
    End Sub
    Private Function co(ByVal pt As PointF, ByVal rect As Rectangle, ByVal ymax As Single)
        '将（i,v（i））转化为picturebox中的（x,y）,rect为图框
        Dim x As Single, y As Single
        x = pt.X * rect.Width / 255 + rect.X
        y = -pt.Y * rect.Height / ymax + rect.Height + rect.Y
        Return New PointF(x, y)
    End Function
    Public Sub drawhist(ByRef e As Graphics, ByVal rect As Rectangle) 'byref和byval什么区别？？？？？？
        Dim max As Single, pts(255) As PointF, i As Short
        e.Clear(Color.White)
        If mImageType = 0 Then
            For i = 0 To 255
                If max < v(i) Then max = v(i)
            Next
            e.DrawLine(Pens.Black, co(New Point(0, 0), rect, max), co(New Point(255, 0), rect, max))
            e.DrawLine(Pens.Black, co(New Point(0, 0), rect, max), co(New PointF(0, max), rect, max))
            For i = 0 To 255
                pts(i) = co(New PointF(i, v(i)), rect, max)
            Next
            e.DrawLines(Pens.Black, pts)
        ElseIf mImageType = 1 Then
            max = 0
            For i = 0 To 255
                max = Math.Max(rHist(i), max) '怎么做的
                max = Math.Max(gHist(i), max)
                max = Math.Max(bHist(i), max)
                max = Math.Max(mHist(i), max)
            Next

            For i = 0 To 255
                pts(i) = co(New PointF(i, rHist(i)), rect, max)
            Next
            e.DrawLines(Pens.Red, pts)
            For i = 0 To 255
                pts(i) = co(New PointF(i, gHist(i)), rect, max)
            Next
            e.DrawLines(Pens.Green, pts)
            For i = 0 To 255
                pts(i) = co(New PointF(i, bHist(i)), rect, max)
            Next
            e.DrawLines(Pens.Blue, pts)
            For i = 0 To 255
                pts(i) = co(New PointF(i, mHist(i)), rect, max)
            Next
            e.DrawLines(Pens.Black, pts)
            e.DrawLine(Pens.Black, co(New Point(0, 0), rect, max), co(New Point(255, 0), rect, max))
            e.DrawLine(Pens.Black, co(New Point(0, 0), rect, max), co(New PointF(0, max), rect, max))
        End If
    End Sub
    Public Function histequal() As Byte() '函数返回值类型         '直方图均衡化
        Dim s(255) As Single, sk(255) As Byte
        s(0) = v(0)
        For i = 1 To 255
            s(i) = s(i - 1) + v(i)
        Next
        For i = 0 To 255
            sk(i) = Math.Round(s(i) * 255)
        Next
        For i = 0 To mSize - 1
            ImageB(i) = sk(ImageB(i)) '这不是我几辈子也写不出来的东东么。。。
        Next
        putBitMapData()
        Return sk
    End Function
    '灰度化
    Public Sub togrey()
        '24位（真彩色图像）把它变为8位索引图像就可以灰度化了
        If mImageType = 1 Then
            mImageType = 0
            mFwidth = ((mWidth + 3) \ 4) * 4 '需要重新定义imageb（），8位与24位mheight,mwidth一样，mf的，msize不一样
            '我擦，*比\优先级高
            mSize = mFwidth * mHeight
            ReDim ImageB(mSize - 1) 'dim,redim使为0
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    Dim posb = i * mFwidth + j
                    Dim posc = Cpos(i) + 3 * j
                    ImageB(posb) = 0.299 * ImageC(posc + 2) + 0.587 * ImageC(posc + 1) + 0.114 * ImageC(posc)
                Next
            Next
            BMap.Dispose()
            BMap = New Bitmap(mWidth, mHeight, Imaging.PixelFormat.Format8bppIndexed)
        End If
        Dim newpalette As Imaging.ColorPalette
        newpalette = BMap.Palette
        For i = 0 To 255
            newpalette.Entries(i) = Color.FromArgb(i, i, i)
        Next
        BMap.Palette = newpalette
        putBitMapData()
    End Sub
    '加入椒盐噪声
    Public Sub noise()
        Randomize()
        Dim x, y As Integer
        For i = 0 To mPixels * 0.01
            x = Int(Rnd() * (mHeight - 1))
            y = Int(Rnd() * (mWidth - 1))
            ImageB(x * mWidth + y) = 0
        Next
        For i = 0 To mPixels * 0.01
            x = Int(Rnd() * (mHeight - 1))
            y = Int(Rnd() * (mWidth - 1))
            ImageB(x * mWidth + y) = 255
        Next
        putBitMapData()
    End Sub
    '调色板
    Public Property palette As Color()
        Get
            Return BMap.Palette.Entries
        End Get
        Set(ByVal value As Color())
            Dim newpalette As Imaging.ColorPalette = BMap.Palette
            For i = 0 To 255
                newpalette.Entries(i) = value(i)
            Next
            BMap.Palette = newpalette
            CurrentEdit = CurrentEdit + 1
            ReDim Preserve OldBMap(CurrentEdit)
            OldBMap(CurrentEdit) = BMap.Clone()
            RaiseEvent UndoEnabledChanged(True)
            RaiseEvent palettechanged() 'palette.palette使外部对这个事件做出动作，和public event写在同一个类中
        End Set
    End Property
    Public Sub geotrans(ByVal nmwidth As Integer, ByVal nmheight As Integer)
        Dim nmfwidth As Integer
        If mImageType = 0 Then nmfwidth = ((nmwidth + 3) \ 4) * 4
        If mImageType = 1 Then nmfwidth = ((nmwidth * 3 + 3) \ 4) * 4
        Dim k1 As Single = mFwidth / nmfwidth
        Dim k2 As Single = mHeight / nmheight
        Dim nimageb(nmfwidth * nmheight - 1) As Byte
        For i = 0 To nmheight - 1
            For j = 0 To nmfwidth - 1
                nimageb(i * nmfwidth + j) = ImageB((Math.Ceiling(k2 * (i + 1)) - 1) * mFwidth + Math.Ceiling(k1 * (j + 1)) - 1)
            Next
        Next
        mWidth = nmwidth
        mFwidth = nmfwidth
        mHeight = nmheight
        mSize = mWidth * mHeight
        ImageB = nimageb
        Dim bmapformat As Imaging.PixelFormat
        bmapformat = BMap.PixelFormat
        BMap.Dispose()
        BMap = New Bitmap(mWidth, mHeight, bmapformat)
        Dim newpalette As Imaging.ColorPalette
        newpalette = BMap.Palette
        For i As Short = 0 To 255
            newpalette.Entries(i) = Color.FromArgb(i, i, i)
        Next
        BMap.Palette = newpalette
        putBitMapData()
    End Sub
    Public Sub tograyscale(ByVal type As Short)
        ' Type 参数表述灰度化所采取的方法
        '    0 加权灰度化
        '    1 红色分量灰度化
        '    2 绿色分量灰度化
        '    3 蓝色分量灰度化
        mFwidth = ((mWidth + 3) \ 4) * 4
        ReDim ImageB(mHeight * mfwidth - 1)
        Dim posb As Integer
        Dim posc As Integer
        If mImageType = 1 Then
            mImageType = 0
            Select Case type
                Case 0
                    For i = 0 To mHeight - 1
                        For j = 0 To mWidth - 1
                            posb = i * mfwidth + j
                            posc = Cpos(i) + j * 3
                            ImageB(posb) = 0.299 * ImageC(posc + 2) + 0.587 * ImageC(posc + 1) + 0.114 * ImageC(posc)
                        Next
                    Next
                Case 1
                    For i = 0 To mHeight - 1
                        For j = 0 To mWidth - 1
                            posb = i * mfwidth + j
                            posc = Cpos(i) + j * 3
                            ImageB(posb) = ImageC(posc + 2)
                        Next
                    Next
                Case 2
                    For i = 0 To mHeight - 1
                        For j = 0 To mWidth - 1
                            posb = i * mfwidth + j
                            posc = Cpos(i) + j * 3
                            ImageB(posb) = ImageC(posc + 1)
                        Next
                    Next
                Case 3
                    For i = 0 To mHeight - 1
                        For j = 0 To mWidth - 1
                            posb = i * mfwidth + j
                            posc = Cpos(i) + j * 3
                            ImageB(posb) = ImageC(posc)
                        Next
                    Next
            End Select
        End If
        BMap.Dispose()
        BMap = New Bitmap(mWidth, mHeight, Imaging.PixelFormat.Format8bppIndexed)
        putBitMapData()
        commonpalette(0)
    End Sub
    Public Sub commonpalette(ByVal type As Short)
        Dim newpalette As Imaging.ColorPalette
        newpalette = BMap.Palette
        Select Case type
            Case 0
                For i = 0 To 255
                    newpalette.Entries(i) = Color.FromArgb(i, i, i)
                Next
            Case 1
                For i = 0 To 255
                    newpalette.Entries(i) = Color.FromArgb(i, 0, 0)
                Next
            Case 2
                For i = 0 To 255
                    newpalette.Entries(i) = Color.FromArgb(0, i, 0)
                Next
            Case 3
                For i = 0 To 255
                    newpalette.Entries(i) = Color.FromArgb(0, 0, i)
                Next
            Case 4
                Randomize()
                For i = 0 To 255
                    Dim R, G, B As Byte
                    R = CInt(Math.Floor(256 * Rnd()))
                    G = CInt(Math.Floor(256 * Rnd()))
                    B = CInt(Math.Floor(256 * Rnd()))
                    newpalette.Entries(i) = Color.FromArgb(R, G, B)
                Next
        End Select
        BMap.Palette = newpalette
    End Sub
    Public Sub channelchange(ByVal r As Short, ByVal g As Short, ByVal b As Short)
        '0,1,2
        Dim posb As Integer
        Dim nimagec(CSize - 1) As Byte
        For i = 0 To mHeight - 1
            For j = 0 To mWidth - 1
                posb = Cpos(i) + 3 * j
                nimagec(posb) = ImageC(posb + b)
                nimagec(posb + 1) = ImageC(posb + g)
                nimagec(posb + 2) = ImageC(posb + r)
            Next
        Next
        ImageC = nimagec
        putBitMapData()
    End Sub
    Public Function clone()
        Dim nmimage As New ImageClass
        'isOpened = True '什么意思
        nmimage.BMap = Me.BMap.Clone
        nmimage.getBitMapData()

        nmimage.isOpened = isOpened
        Return nmimage
    End Function
    Public Sub savefile()
        If BMap Is Nothing Then Return
        BMap.Save(mImageName, Imaging.ImageFormat.Bmp)
    End Sub
    Public Sub saveas(ByVal filename As String)
        If BMap Is Nothing Then Return
        BMap.Save(filename, Imaging.ImageFormat.Bmp)
        BMap.Dispose()
        BMap = New Bitmap(filename)
    End Sub
    Public Sub rotate(ByVal t As Single) '扩大画布？？？？？
        Dim nimageb(mSize - 1) As Byte
        t = t / 180 * Math.PI
        If mImageType = 0 Then
            For j = 0 To mHeight - 1
                For i = 0 To mWidth - 1
                    Dim m As Single
                    Dim n As Single
                    Dim u As Single
                    Dim v As Single
                    m = (i - mWidth / 2) * Math.Cos(t) - (j - mHeight / 2) * Math.Sin(t) + mWidth / 2
                    n = (i - mWidth / 2) * Math.Sin(t) + (j - mHeight / 2) * Math.Cos(t) + mHeight / 2
                    If m >= 0 And m <= mWidth - 2 And n >= 0 And n <= mHeight - 2 Then
                        u = m - Int(m)
                        v = n - Int(n)
                        m = Int(m)
                        n = Int(n)
                        nimageb(mFwidth * j + i) = (1 - u) * (1 - v) * ImageB(mFwidth * n + m) + (1 - u) * v * ImageB(mFwidth * n + m + 1) + u * (1 - v) * ImageB(mFwidth * (n + 1) + m) + u * v * ImageB(mFwidth * (n + 1) + m + 1)
                    End If
                Next
            Next
        End If
        ImageB = nimageb
        putBitMapData()
    End Sub
    Public Sub grad(ByVal m As Short)
        If mImageType = 0 Then
            Dim nimageb(mSize - 1) As Short 'nimageb有可能会超过255所以弄成short类型
            Dim pos, pos1, pos2, pos3 As Long
            For i = 0 To mSize - 1
                nimageb(i) = CShort(ImageB(i))
            Next
            Select Case m
                Case 1
                    For j As Short = 0 To mHeight - 2
                        For i As Short = 0 To mWidth - 2
                            pos = j * mFwidth + i
                            pos1 = (j + 1) * mFwidth + i
                            pos2 = j * mFwidth + i + 1
                            nimageb(pos) = Math.Abs(nimageb(pos1) - nimageb(pos)) + Math.Abs(nimageb(pos2) - nimageb(pos))
                        Next
                    Next
                Case 2
                    For j As Short = 0 To mHeight - 2
                        For i As Short = 0 To mWidth - 2
                            pos = j * mFwidth + i
                            pos1 = (j + 1) * mFwidth + i
                            pos2 = j * mFwidth + i + 1
                            nimageb(j * mFwidth + i) = Math.Max(Math.Abs(nimageb(pos1) - nimageb(pos)), Math.Abs(nimageb(pos2) - nimageb(pos)))
                        Next
                    Next
                Case 3
                    For j As Short = 0 To mHeight - 2
                        For i As Short = 0 To mWidth - 2
                            pos = j * mFwidth + i
                            pos1 = (j + 1) * mFwidth + i
                            pos2 = j * mFwidth + i + 1
                            nimageb(j * mFwidth + i) = Math.Sqrt((nimageb(pos1) - nimageb(pos)) ^ 2 + (nimageb(pos2) - nimageb(pos)) ^ 2)
                        Next
                    Next
                Case 4
                    For j As Short = 0 To mHeight - 2
                        For i As Short = 0 To mWidth - 2
                            pos = j * mFwidth + i
                            pos1 = (j + 1) * mFwidth + i
                            pos2 = j * mFwidth + i + 1
                            pos3 = (j + 1) * mFwidth + i + 1
                            nimageb(j * mFwidth + i) = Math.Abs(nimageb(pos3) - nimageb(pos)) + Math.Abs(nimageb(pos2) - nimageb(pos1))
                        Next
                    Next
                Case 5
                    For j As Short = 0 To mHeight - 2
                        For i As Short = 0 To mWidth - 2
                            pos = j * mFwidth + i
                            pos1 = (j + 1) * mFwidth + i
                            pos2 = j * mFwidth + i + 1
                            pos3 = (j + 1) * mFwidth + i + 1
                            nimageb(j * mFwidth + i) = Math.Max(Math.Abs(nimageb(pos3) - nimageb(pos)), Math.Abs(nimageb(pos2) - nimageb(pos1)))
                        Next
                    Next
                Case 6
                    For j As Short = 0 To mHeight - 2
                        For i As Short = 0 To mWidth - 2
                            pos = j * mFwidth + i
                            pos1 = (j + 1) * mFwidth + i
                            pos2 = j * mFwidth + i + 1
                            pos3 = (j + 1) * mFwidth + i + 1
                            nimageb(j * mFwidth + i) = Math.Sqrt((nimageb(pos3) - nimageb(pos)) ^ 2 + (nimageb(pos2) - nimageb(pos1)) ^ 2)
                        Next
                    Next
            End Select
            For i = 0 To mSize - 1
                If nimageb(i) > 255 Then
                    ImageB(i) = 255
                Else
                    ImageB(i) = nimageb(i)
                End If
            Next
            putBitMapData()
        End If
    End Sub
End Class
