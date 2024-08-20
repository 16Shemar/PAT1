Public Class Form1

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ' Get admin credentials from text boxes
        Dim AdminUsername As String = txtUsername.Text
        Dim AdminPassword As String = txtPassword.Text

        ' Hardcoded admin credentials for simplicity
        Dim ExpectedUsername As String = "admin"
        Dim ExpectedPassword As String = "12345"

        ' Validate password length and content
        If Not IsNumeric(AdminPassword) OrElse AdminPassword.Length <> 5 Then
            MsgBox("Password must be exactly 5 numeric characters", MsgBoxStyle.Exclamation, "Invalid Password")
            Return
        End If

        ' Check credentials
        If AdminUsername = ExpectedUsername AndAlso AdminPassword = ExpectedPassword Then
            ' Hide login form and show main form
            Me.Hide()
            Dim mainForm As New MainForm()
            mainForm.Show()
        Else
            MsgBox("Invalid Username or Password", MsgBoxStyle.RetryCancel, "Login Failed")
        End If
    End Sub
End Class
