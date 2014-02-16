
Public Class geotrans
    Private mimage As ImageClass = MainForm.mImage

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mimage.geotrans(TextBox1.Text, TextBox2.Text)
    End Sub

    Private Sub TextBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave, TextBox2.Leave
        If IsNumeric(sender.text) Then
            Dim value As Integer = sender.text
            If value > 0 Then
                Button1.Enabled = True
            Else
                Button1.Enabled = False
            End If
        End If
    End Sub
End Class