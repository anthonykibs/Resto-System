Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.SqlServer
Imports System.Drawing.Drawing2D
Public Class EthnicLogin
    Dim user_type, Title As String
    Public login, login1 As String
    Public EmpID As String
    Dim strTitle As String = "ETHNIC -- Bar and Lounge"
    Dim con As New SqlConnection("Server=localhost; database=RestuarantBar; uid=sa; password=pass;")
    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try




            con.Open()
            Dim cmd As New SqlCommand("SELECT * FROM BarLogin WHERE UserName = '" & UsernameTextBox.Text & "' AND Password ='" & PasswordTextBox.Text & "'")
            cmd.Connection = con
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader
            With reader
                If .HasRows Then
                    .Read()
                    user_type = reader.Item("Previledges")
                    EmpID = reader.Item("EmpIdNo")
                    login1 = user_type
                    'login = reader.Item("EmpIdNo").ToString
                    MessageBox.Show("Welcome" + vbTab + login1, strTitle)
                    Form1.Show()
                    Form1.Text = Form1.Text & "   " & login1.ToUpper
                    Form1.lblLogTyp.Text = login1
                    Food_Snacks.lblLogTyp.Text = login1
                    changeLogin.username = UsernameTextBox.Text
                    changeLogin.login = user_type
                    Me.Hide()
                    UsernameTextBox.Clear()
                    PasswordTextBox.Clear()
                Else
                    MessageBox.Show("Wrong Username or Password", strTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation)
                    UsernameTextBox.Clear()
                    PasswordTextBox.Clear()
                End If
            End With
            reader.Close()

            Dim cmdn As New SqlCommand("SELECT * FROM Employees WHERE EmpIdNO = '" & EmpID & "'", con)

            Dim readern As SqlDataReader
            readern = cmdn.ExecuteReader
            While readern.Read
                login = readern.Item("FirstName") & " " & readern.Item("LastName")
            End While
            readern.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Application.Exit()
    End Sub

    Private Sub EthnicLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub
    Private Sub Ethnic_Load(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Dim rc As Rectangle = New Rectangle(0, 0, Me.Width, Me.Height)
        Dim brush As New LinearGradientBrush(rc, Color.White, Color.Black, LinearGradientMode.Vertical)
        e.Graphics.FillRectangle(brush, rc)
    End Sub
End Class
