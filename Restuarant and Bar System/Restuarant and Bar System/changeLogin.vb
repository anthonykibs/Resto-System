Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.SqlServer
Imports System.Drawing.Drawing2D

Public Class changeLogin
    Dim strTitle As String = "ETHNIC -- Bar and Lounge"
    Dim con As New SqlConnection("Server=localhost; database=RestuarantBar; uid=sa; password=pass;")
    Public username, login As String
    Private Sub changeLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If login <> "Administrator" Then
            ConfigureToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub btnForward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForward.Click

        Try
            Dim x As Integer
            While x < 1000
                Panel5.Location = New Point(504, 21)
                x = x + 1

            End While


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text = "" Then
                MessageBox.Show("Please input password", strTitle)
                TextBox1.Focus()
                Exit Sub
            Else
                If TextBox3.Text = "" Then
                    MessageBox.Show("Please confirm password", strTitle)
                    TextBox3.Focus()
                    Exit Sub
                Else
                    If con.State = ConnectionState.Closed Then con.Open()
                    If TextBox1.Text.Trim = TextBox3.Text Then
                        Dim cmd As New SqlCommand("UPDATE BarLogin SET Password = '" & TextBox3.Text.Trim & "' WHERE UserName = '" & username & "' ", con)
                        cmd.ExecuteNonQuery()
                        TextBox1.Clear()
                        TextBox3.Clear()
                        MessageBox.Show("Password changed successfully", strTitle)
                    Else

                        MessageBox.Show("Password Mism=smatch..!!", strTitle)
                        TextBox1.Clear()
                        TextBox1.Focus()
                        TextBox3.Clear()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub AnotherAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnotherAccountToolStripMenuItem.Click
        Try
            Dim x As Integer
            While x < 1000
                Panel5.Location = New Point(145, 21)
                x = x + 1
            End While


        Catch ex As Exception

        End Try
    End Sub

    Private Sub MenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim username1 As String = TextBox8.Text.Trim
        Try
            If TextBox8.Text = "" Then
                MessageBox.Show("Please provide username", strTitle)
                TextBox8.Focus()
                Exit Sub
            Else
                If TextBox2.Text = "" Then
                    MessageBox.Show("Please enter Old password", strTitle)
                    TextBox2.Focus()
                    Exit Sub
                Else
                    If TextBox7.Text = "" Then
                        MessageBox.Show("Please enter New password", strTitle)
                        TextBox7.Focus()
                        Exit Sub
                    Else
                        If TextBox5.Text = "" Then
                            MessageBox.Show("Please confirm new password", strTitle)
                            TextBox5.Focus()
                            Exit Sub
                        Else
                            If TextBox5.Text <> TextBox7.Text Then
                                MessageBox.Show("Passwords do not match", strTitle)
                                Exit Sub
                            Else
                                If con.State = ConnectionState.Closed Then con.Open()
                                Dim cmdc As New SqlCommand("SELECT * FROM BarLogin WHERE UserName = '" & TextBox8.Text.Trim & "'", con)
                                Dim reader As SqlDataReader

                                reader = cmdc.ExecuteReader
                                If reader.HasRows Then

                                    Dim cmd As New SqlCommand("UPDATE BarLogin SET Password = '" & TextBox5.Text.Trim & "' WHERE UserName = '" & TextBox8.Text.Trim & "'", con)
                                    cmd.ExecuteNonQuery()
                                    MessageBox.Show("Password Changed Successfully", strTitle)
                                    TextBox8.Clear()
                                    TextBox7.Clear()
                                    TextBox5.Clear()
                                    TextBox2.Clear()
                                Else
                                    MessageBox.Show("Error --- username does not exist", strTitle)
                                    TextBox8.Clear()
                                    TextBox7.Clear()
                                    TextBox5.Clear()
                                    TextBox2.Clear()
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

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text = "" Then
            Label3.Visible = False
            Exit Sub
        Else
            Label3.Visible = True
            If TextBox5.Text = TextBox7.Text Then
                Label3.Text = "   Match"
                Label3.ForeColor = Color.Green
            Else
                Label3.Text = "MisMatch"
                Label3.ForeColor = Color.Red
            End If
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If TextBox3.Text = "" Then
            Label5.Visible = False
            Exit Sub
        Else
            If TextBox1.Text = TextBox3.Text Then
                Label5.Visible = True
                Label5.Text = "   Match"
                Label5.ForeColor = Color.Green
            Else
                Label5.Visible = True
                Label5.Text = "MisMatch"
                Label5.ForeColor = Color.Red
            End If
        End If
    End Sub

    Private Sub ConfigureToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigureToolStripMenuItem.Click

    End Sub
End Class