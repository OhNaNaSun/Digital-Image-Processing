'24位（真彩色）图像无调色板
Public Class histequal
    Private mimage As ImageClass = MainForm.mImage
    Private sk() As Byte
    Private Current As Byte = mimage.MaxUndo 'Current初始化
    Private Sub PictureBox2_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox2.Paint
        Dim h As Integer = PictureBox2.Height
        Dim w As Integer = PictureBox2.Width
        mimage.drawhist(e.Graphics, New Rectangle(10, 10, w - 20, h - 20))
        sk = mimage.histequal
        PictureBox1.Refresh() '绘制转换函数
        PictureBox3.Refresh() '绘制均衡化后的直方图
    End Sub
    Private Sub PictureBox3_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox3.Paint
        Dim h As Integer = PictureBox3.Height
        Dim w As Integer = PictureBox3.Width
        e.Graphics.RotateTransform(-90) '将坐标系逆时针旋转90度
        e.Graphics.TranslateTransform(-h, 0)
        mimage.drawhist(e.Graphics, New Rectangle(10, 10, h - 20, w - 20))
    End Sub
    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        Dim h As Integer = PictureBox1.Height
        Dim w As Integer = PictureBox1.Width
        Dim pts(255) As PointF
        Dim i As Integer
        Dim max As Integer
        e.Graphics.DrawLine(Pens.Black, New Point(10, h - 10), New Point(w - 10, h - 10))
        e.Graphics.DrawLine(Pens.Black, New Point(10, h - 10), New Point(10, 10))
        For i = 0 To 255
            If max < sk(i) Then max = sk(i)
        Next
        For i = 0 To 255
            pts(i) = New PointF((w - 20) / 255 * i + 10, h - 10 - (h - 20) / max * sk(i))
        Next
        e.Graphics.DrawLines(Pens.Blue, pts)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Current = mimage.MaxUndo
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub histequal_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        mimage.Undo(mimage.MaxUndo - Current) 'button1_click和button2_click都会触发这个
    End Sub
End Class