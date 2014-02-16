Public Class channel
    ' Private mimage As ImageClass = MainForm.mImage
    Dim mimage As ImageClass
    Dim rmimage As ImageClass
    Dim gmimage As ImageClass
    Dim bmimage As ImageClass
    Private Sub channel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mimage = MainForm.mImage.clone
        rmimage = mimage.clone
        rmimage.tograyscale(1)
        gmimage = mimage.clone
        gmimage.tograyscale(2)
        bmimage = mimage.clone
        bmimage.tograyscale(3)
    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        mimage.ViewX = PictureBox1.Width - PictureBox1.Margin.Right
        mimage.ViewY = PictureBox1.Height - PictureBox1.Margin.Bottom
        mimage.zoomExtent(e.Graphics)
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Static c As Boolean '默认是faulse
        c = Not c
        If c = True Then
            rmimage.commonpalette(1)
        Else
            rmimage.commonpalette(0)
        End If
        PictureBox2.Refresh()
    End Sub

    Private Sub PictureBox2_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox2.Paint
        rmimage.ViewX = PictureBox2.Width - PictureBox2.Margin.Right
        rmimage.ViewY = PictureBox2.Height - PictureBox2.Margin.Bottom
        rmimage.zoomExtent(e.Graphics)
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        Static c As Boolean
        c = Not c
        If c = True Then
            gmimage.commonpalette(2)
        Else
            gmimage.commonpalette(0)
        End If
        PictureBox3.Refresh()
    End Sub
    Private Sub PictureBox3_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox3.Paint
        gmimage.ViewX = PictureBox3.Width - PictureBox3.Margin.Right
        gmimage.ViewY = PictureBox3.Height - PictureBox3.Margin.Bottom
        gmimage.zoomExtent(e.Graphics)
    End Sub

    Private Sub PictureBox4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        Static c As Boolean
        c = Not c
        If c = True Then
            bmimage.commonpalette(3)
        Else
            bmimage.commonpalette(0)
        End If
        PictureBox4.Refresh()
    End Sub
    Private Sub PictureBox4_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox4.Paint
        bmimage.ViewX = PictureBox4.Width - PictureBox4.Margin.Right
        bmimage.ViewY = PictureBox4.Height - PictureBox4.Margin.Bottom
        bmimage.zoomExtent(e.Graphics)
    End Sub

    Private Sub RRGGBBToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RRGGBBToolStripMenuItem.Click
        mimage.channelchange(0, 1, 2)
        PictureBox1.Refresh()
    End Sub

    Private Sub RRGBBGToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RRGBBGToolStripMenuItem.Click
        mimage.channelchange(2, 0, 1)
        PictureBox1.Refresh()
    End Sub

    Private Sub RBGGBRToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBGGBRToolStripMenuItem.Click
        mimage.channelchange(0, 1, 2)
        PictureBox1.Refresh()
    End Sub

    Private Sub RBGRBGToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBGRBGToolStripMenuItem.Click
        mimage.channelchange(0, 2, 1)
        PictureBox1.Refresh()
    End Sub

    Private Sub RGGRBBToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RGGRBBToolStripMenuItem.Click
        mimage.channelchange(1, 2, 0)
        PictureBox1.Refresh()
    End Sub

    Private Sub RGGBBRToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RGGBBRToolStripMenuItem.Click
        mimage.channelchange(1, 0, 2)
        PictureBox1.Refresh()
    End Sub
End Class