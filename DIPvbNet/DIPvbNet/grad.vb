Public Class grad
    Dim mimage As ImageClass = MainForm.mImage
    Private Sub Button_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles Button1.Click, Button2.Click, Button3.Click, Button4.Click, Button5.Click, Button6.Click
        mimage.grad(Val(sender.Name(6)))
        Close()
    End Sub
End Class