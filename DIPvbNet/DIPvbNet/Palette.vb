'24位（fw）
Public Class Palette
    Private mimage As ImageClass = MainForm.mImage
    Private buttons(255) As Button
    Private palette As Color()

    Private Sub Palette_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load 'load是第一次打开时，关闭再打开不会load
        Dim topleft As New Point(10, 10)
        Dim size As New Size(30, 30)
        For i = 0 To 255
            Dim row As Short = i \ 16
            Dim column As Short = i Mod 16
            buttons(i) = New Button
            buttons(i).Size = size
            buttons(i).Location = New Point((10 + size.Width * column), (10 + size.Height * row))
            AddHandler buttons(i).Click, AddressOf buttons_click '使动作与事件联系起来
            Me.Controls.Add(buttons(i))
        Next
    End Sub
    Private Sub buttons_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Dim i As Short
        For i = 0 To 255
            If buttons(i) Is sender Then Exit For
        Next
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            change_palette(ColorDialog1.Color, i)
        End If
        mimage.palette = palette
    End Sub
    Private Sub change_palette(ByVal color As Color, ByVal i As Short)
        buttons(i).BackColor = color
        palette(i) = color
        ToolTip1.SetToolTip(buttons(i), "index=" & i & " " & palette(i).ToString)
    End Sub

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button1.Click
        Dim newcolor As Color
        For i = 0 To 255
            newcolor = Color.FromArgb(Not palette(i).R, Not palette(i).G, Not palette(i).B)
            change_palette(newcolor, i)
        Next
        mimage.palette = palette
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim newcolor As Color
        For i = 0 To 255
            newcolor = Color.FromArgb(255, 255, i)
            change_palette(newcolor, i)
        Next
        mimage.palette = palette
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim newcolor As Color
        For i = 0 To 255
            newcolor = Color.FromArgb(i, i, 255)
            change_palette(newcolor, i)
        Next
        mimage.palette = palette
    End Sub

    Private Sub Palette_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Visible = True Then
            palette = mimage.palette
            For i = 0 To 255
                buttons(i) = Me.Controls(i + 4)
                buttons(i).BackColor = palette(i)
                ToolTip1.SetToolTip(buttons(i), "index=" & i & " " & palette(i).ToString)
            Next
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim newcolor As Color
        Randomize()
        For i = 0 To 255
            newcolor = Color.FromArgb(Rnd() * 255, Rnd() * 255, Rnd() * 255)
            change_palette(newcolor, i)
        Next
        mimage.palette = palette
    End Sub
End Class