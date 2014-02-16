Public Class LinearTrans
    Private mimage As ImageClass = MainForm.mImage
    Private Current As Byte = mimage.MaxUndo


    Private Sub PictureBox2_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox2.Paint
        Dim h As Integer = PictureBox2.Height
        Dim w As Integer = PictureBox2.Width
        mimage.drawhist(e.Graphics, New Rectangle(10, 10, w - 20, h - 20))
    End Sub

    Private Sub PictureBox1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseClick
        If e.Button <> Windows.Forms.MouseButtons.Left Then Return
        Dim h As Integer = PictureBox1.Height
        Dim w As Integer = PictureBox1.Width
        Dim click As New PointF
        click.X = 255 / (w - 20) * (e.X - 10)
        click.Y = 255 / (h - 20) * (h - e.Y - 10)
        Dim distA As Single, distB As Single
        distA = (TextBox1.Text - click.X) ^ 2 + (TextBox3.Text - click.Y) ^ 2
        distB = (TextBox2.Text - click.X) ^ 2 + (TextBox4.Text - click.Y) ^ 2
        If distA < distB Then
            TextBox1.Text = Int(click.X)
            check(TextBox1, False)
            TextBox3.Text = Int(click.Y)
            check(TextBox3, False)
        Else
            TextBox2.Text = Int(click.X)
            check(TextBox2, False)
            TextBox4.Text = Int(click.Y)
            check(TextBox4, False)
        End If
        trans(Val(TextBox1.Text), Val(TextBox2.Text), Val(TextBox3.Text), Val(TextBox4.Text))
    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        Dim h As Integer = PictureBox1.Height
        Dim w As Integer = PictureBox1.Width
        e.Graphics.Clear(Color.White)
        e.Graphics.DrawLine(Pens.Black, New Point(10, h - 10), New Point(w - 10, h - 10))
        e.Graphics.DrawLine(Pens.Black, New Point(10, h - 10), New Point(10, 10))
        Dim pts(3) As PointF
        pts(0) = New PointF(10, h - 10)
        pts(1) = New PointF(TextBox1.Text * (w - 20) / 255 + 10, h - 10 - TextBox3.Text * (h - 20) / 255)
        pts(2) = New PointF(TextBox2.Text * (w - 20) / 255 + 10, h - 10 - TextBox4.Text * (h - 20) / 255)
        pts(3) = New PointF(w - 10, 10)
        e.Graphics.DrawLines(Pens.Blue, pts)
    End Sub

    Private Sub PictureBox3_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox3.Paint

        Dim h As Integer = PictureBox3.Height
        Dim w As Integer = PictureBox3.Width
        e.Graphics.RotateTransform(-90) '将坐标系逆时针旋转90度
        e.Graphics.TranslateTransform(-h, 0)
        mimage.drawhist(e.Graphics, New Rectangle(10, 10, h - 20, w - 20))
    End Sub


    Private Sub trans(ByVal a As Byte, ByVal b As Byte, ByVal c As Byte, ByVal d As Byte)
        PictureBox1.Refresh()
        mimage.Undo(mimage.MaxUndo - Current)
        mimage.lineartrans(a, b, c, d)
        MainForm.Panel.Refresh()
        PictureBox3.Refresh()
    End Sub
    Private Sub check(ByVal sender As Object, Optional ByVal msg As Boolean = True)
        Dim a As Short, b As Short, c As Short, d As Short
        Dim txt As String = "" '做什么用的？？？？？
        Dim correct As Boolean = False '做什么用的？？？？？
        a = Val(TextBox1.Text)
        b = Val(TextBox2.Text)
        c = Val(TextBox3.Text)
        d = Val(TextBox4.Text)
        If (a <= 0) Then
            txt = "a,b必须满足条件：0<a<b<255.已插入最接近的数字。"
            TextBox1.Text = 1
            correct = True
        End If
        If (a >= b) Then
            txt = "a,b必须满足条件：0<a<b<255.已插入最接近的数字。"
            If sender Is TextBox1 Then
                TextBox1.Text = b - 1
            Else
                TextBox2.Text = a + 1
            End If
            correct = True
        End If
        If (b >= 255) Then
            txt = "a,b必须满足条件：0<a<b<255.已插入最接近的数字。"
            TextBox2.Text = 254
            correct = True
        End If
        If (c < 0) Then
            txt = "c,d必须满足条件：0<=c<d<=255.已插入最接近的数字。"
            TextBox3.Text = 0
            correct = True
        End If
        If (c >= d) Then
            txt = "c,d必须满足条件：0<=c<d<=255.已插入最接近的数字。"
            If sender Is TextBox3 Then
                TextBox3.Text = d - 1
            Else
                TextBox4.Text = c + 1
            End If
            correct = True
        End If
        If (d > 255) Then
            txt = "c,d必须满足条件：0<=c<d<=255.已插入最接近的数字。"
            TextBox4.Text = 255
            correct = True
        End If
    End Sub
    Private Sub TextBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave, TextBox2.Leave, TextBox3.Leave, TextBox4.Leave
        If IsNumeric(sender.Text) = False Then
            MsgBox("须输入0-255间的数字", MsgBoxStyle.Critical, "错误")
            sender.focus()
            Return
        End If
        check(sender)
        trans(Val(TextBox1.Text), Val(TextBox2.Text), Val(TextBox3.Text), Val(TextBox4.Text))
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Current = mimage.MaxUndo
        PictureBox2.Refresh()
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub LinearTrans_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        mimage.Undo(mimage.MaxUndo - Current)
    End Sub
End Class