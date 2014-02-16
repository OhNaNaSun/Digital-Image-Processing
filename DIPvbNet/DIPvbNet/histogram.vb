
Public Class histogram
    Private WithEvents mimage As ImageClass = MainForm.mImage 'withevents????
    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        Dim h As Integer = PictureBox1.Height
        Dim w As Integer = PictureBox1.Width
        Me.Text = MainForm.OpenFileDialog1.SafeFileName & "直方图"
        mimage.drawhist(e.Graphics, New Rectangle(10, 10, w - 20, h - 20))
        ' mImage.DrawHist(e.Graphics, PictureBox1.DisplayRectangle)
    End Sub

    Private Sub PictureBox1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.SizeChanged
        PictureBox1.Refresh()
    End Sub

    Private Sub mimage_histchanged() Handles mimage.histchanged
        If Me.Visible = True Then
            mimage.hist(ToolStripStatusLabel1.Text)
            PictureBox1.Refresh()
        End If
    End Sub
End Class