Public Class filter
    Private textbox(8, 8) As TextBox
    Private mimage As ImageClass = MainForm.mImage
    Private Sub filter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If IsNothing(textbox(0, 0)) Then '使第一次load时不重复添加控件
            Dim w = 20
            Dim h = 23
            For i As Byte = 0 To 8
                For j As Byte = 0 To 8
                    textbox(i, j) = New TextBox
                    With textbox(i, j)
                        .Size = New Size(w, h)
                        .Location = New Point((1 + w * j), (30 + h * i))
                        .TextAlign = HorizontalAlignment.Center
                        .Visible = False
                    End With
                    Controls.Add(textbox(i, j))
                Next
            Next
            textbox(0, 0).Visible = True
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim m As Byte = NumericUpDown1.Value
        Dim n As Byte = NumericUpDown2.Value
        Dim h(m - 1, n - 1) As Single
        For i As Byte = 0 To m - 1
            For j As Byte = 0 To n - 1
                If IsNumeric(textbox(i, j).Text) = False Then
                    MsgBox("只能输入数字！", MsgBoxStyle.Critical, "错误")
                    Return
                End If
                h(i, j) = Val(textbox(i, j).Text) / NumericUpDown3.Value
            Next
        Next
        mimage.filter(h, True)
    End Sub

    Private Sub NumericUpDown_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged, NumericUpDown2.ValueChanged
        If Me.Created = False Then Return
        If CheckBox1.Checked = True Then NumericUpDown2.Value = NumericUpDown1.Value
        Dim m As Byte = NumericUpDown1.Value
        Dim n As Byte = NumericUpDown2.Value
        For i As Byte = 0 To 8
            For j As Byte = 0 To 8
                If i <= m - 1 And j <= n - 1 Then
                    textbox(i, j).Visible = True
                Else
                    textbox(i, j).Visible = False
                End If
            Next
        Next
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        NumericUpDown2.Enabled = Not CheckBox1.Checked
        If CheckBox1.Checked = True Then
            NumericUpDown2.Value = NumericUpDown1.Value
        End If
    End Sub
End Class