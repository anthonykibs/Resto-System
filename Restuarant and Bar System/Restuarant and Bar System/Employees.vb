Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.SqlServer
Imports System.Drawing.Drawing2D
Public Class Employees
    Dim con As New SqlConnection("Server=localhost; database=RestuarantBar; uid=sa; password=pass;")
    Dim strTitle As String = "ETHNIC -- Bar and Lounge"
    Public Priv As Boolean
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Try
            If TextBox3.Text = "" Then
                LinkLabel2.Visible = False
            Else
                LinkLabel2.Visible = True
                LinkLabel2.Text = "Check Availability"
                LinkLabel2.LinkColor = Color.Blue
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub textbox3_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False
    End Sub
    Private Sub textbox1_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False
    End Sub
    Private Sub textbox2_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not Char.IsLetter(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False
    End Sub
    Private Sub textbox5_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If Not Char.IsLetter(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Try
            If TextBox1.Text = "" Then
                LinkLabel1.Visible = False

            Else
                LinkLabel1.LinkColor = Color.Blue
                LinkLabel1.Visible = True
                LinkLabel1.Text = "Check Availability"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked

        Try

            If con.State = ConnectionState.Closed Then con.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Employees WHERE EmpIdNo = '" & TextBox1.Text & "'", con)
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                LinkLabel1.Text = "User number Exists"
                LinkLabel1.LinkColor = Color.Red
            Else
                LinkLabel1.Text = "Not existent"
                LinkLabel1.LinkColor = Color.Green
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try

            If con.State = ConnectionState.Closed Then con.Open()
            If TextBox3.Text = "" Then
                MessageBox.Show("Please insert User Number", strTitle)
                Exit Sub
            Else
                Dim cmd As New SqlCommand("SELECT * FROM Employees WHERE UserName = '" & TextBox3.Text & "'", con)
                Dim reader As SqlDataReader
                reader = cmd.ExecuteReader
                If reader.HasRows Then
                    LinkLabel2.Text = "UserName Exists"
                    LinkLabel2.LinkColor = Color.Red
                Else
                    LinkLabel2.Text = "Not existent"
                    LinkLabel2.LinkColor = Color.Green
                End If
            End If
           
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox5.Clear()
        ComboBox1.Text = "Select Employee Type"
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Not ComboBox1.Items.Contains(ComboBox1.Text) Then
                MessageBox.Show("Please " + ComboBox1.Text + " is not applicable select from the list", strTitle)
                ComboBox1.Focus()
                ComboBox1.DroppedDown = True
            Else
                If TextBox1.Text = "" Then
                    MessageBox.Show("Please Provive Employee User number", strTitle)
                    TextBox1.Focus()
                    Exit Sub
                Else
                    If con.State = ConnectionState.Closed Then con.Open()
                    Dim cmd2 As New SqlCommand("SELECT * FROM Employees WHERE EmpIdNo = '" & TextBox1.Text.Trim & "'", con)
                    Dim reader2 As SqlDataReader
                    reader2 = cmd2.ExecuteReader
                    If reader2.HasRows Then
                        MessageBox.Show("Please User Number Exists try another one", strTitle)
                        Exit Sub
                    Else

                        reader2.Close()
                        If con.State = ConnectionState.Closed Then con.Open()
                        Dim cmd1 As New SqlCommand("SELECT * FROM Employees WHERE UserName = '" & TextBox3.Text.Trim & "'", con)
                        Dim reader As SqlDataReader
                        reader = cmd1.ExecuteReader
                        If reader.HasRows Then
                            MessageBox.Show("Please Username Exist try another one", strTitle)
                        Else
                            reader.Close()
                            If TextBox5.Text = "" Then
                                MessageBox.Show("Please Provide Employee First Name", strTitle)
                                TextBox5.Focus()
                                Exit Sub
                            Else
                                If TextBox2.Text = "" Then
                                    MessageBox.Show("Please provide employee last name", strTitle)
                                    TextBox2.Focus()
                                    Exit Sub
                                Else
                                    If CheckBox1.Checked = True Then

                                        If TextBox3.Text = "" Then
                                            MessageBox.Show("Please provide Username for " & ComboBox1.Text, strTitle)
                                            Exit Sub
                                        Else
                                            If TextBox4.Text = "" Then
                                                MessageBox.Show("Please assign password to " & ComboBox1.Text, strTitle)
                                                Exit Sub
                                            Else
                                                If con.State = ConnectionState.Closed Then con.Open()
                                                Dim cmd As New SqlCommand("INSERT INTO Employees VALUES('" & TextBox1.Text & "','" & TextBox5.Text & "', '" & TextBox2.Text & "' , '" & TextBox3.Text & "', '" & ComboBox1.Text & "')", con)
                                                cmd.ExecuteNonQuery()

                                                '**********************************************************
                                                Dim cmdl As New SqlCommand("INSERT INTO BarLogin VALUES('" & TextBox3.Text.Trim & "', '" & TextBox4.Text.Trim & "', '" & ComboBox1.Text & "', '" & TextBox1.Text.Trim & "')", con)
                                                cmdl.ExecuteNonQuery()
                                                MessageBox.Show("Successfully saved", strTitle)
                                                TextBox1.Clear()
                                                TextBox2.Clear()
                                                TextBox3.Clear()
                                                TextBox4.Clear()
                                                TextBox5.Clear()
                                                Panel2.Visible = True
                                                ComboBox1.Text = "Select Employee Type"
                                            End If
                                        End If

                                    Else
                                        If con.State = ConnectionState.Closed Then con.Open()
                                        Dim cmd As New SqlCommand("INSERT INTO Employees VALUES('" & TextBox1.Text & "','" & TextBox5.Text & "', '" & TextBox2.Text & "' , '" & "***" & "', '" & ComboBox1.Text & "')", con)
                                        cmd.ExecuteNonQuery()
                                        MessageBox.Show("Successfully saved", strTitle)
                                        TextBox1.Clear()
                                        TextBox2.Clear()
                                        TextBox3.Clear()
                                        TextBox4.Clear()
                                        TextBox5.Clear()
                                        Panel2.Visible = True
                                        ComboBox1.Text = "Select Employee Type"
                                    End If
                                End If

                            End If
                        End If

                        End If

                    End If
                End If
              
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "Waiter" Or ComboBox1.SelectedItem = "Waitress" Then
            Panel2.Visible = False
            Priv = False
            Panel3.Visible = False
            CheckBox1.Visible = False

        Else
            Panel2.Visible = True
            Panel3.Visible = True
            CheckBox1.Visible = True
            Priv = True
        End If
    End Sub

    Private Sub Employees_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Priv = True
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox3.Enabled = True
            TextBox4.Enabled = True
        Else
            TextBox3.Enabled = False
            TextBox4.Enabled = False
        End If
      
    End Sub
End Class