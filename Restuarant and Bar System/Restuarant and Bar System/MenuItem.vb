Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.SqlServer
Imports System.Drawing.Drawing2D
Public Class MenuItem
    Dim strTitle As String = "ETHNIC -- Bar and Lounge"
    Dim con As New SqlConnection("Server=localhost; database=RestuarantBar; uid=sa; password=pass;")
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            pnlChngPr.Location = New Point(551, 27)
            pnlNeMenu.Visible = True
            Button1.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ChangePricesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangePricesToolStripMenuItem.Click
        Try
            pnlChngPr.Location = New Point(0, 27)
            pnlNeMenu.Visible = False
            Button1.Visible = True
        Catch ex As Exception

        End Try
    End Sub
    Private Sub textbox3_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False
        If e.KeyChar = Chr(32) Then e.Handled = False 'Allow Space Bar
        If e.KeyChar = "." Then e.Handled = False 'Allow Full Stop
    End Sub
    Private Sub textbox1_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False
    End Sub
    Private Sub textbox2_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
       
        Try
            If TextBox3.Text = "" Then
                MessageBox.Show("Please enter Menu item Name", strTitle)
                TextBox3.Focus()
                Exit Sub
            Else
                If TextBox1.Text = "" Then
                    MessageBox.Show("Please enter price for Item", strTitle)
                    TextBox1.Focus()
                    Exit Sub
                Else
                    Dim x As Integer
                    x = Val(TextBox1.Text)
                    If con.State = ConnectionState.Closed Then con.Open()
                    Dim cmd As New SqlCommand("INSERT INTO Menu VALUES('" & TextBox3.Text.ToString.Trim & "', '" & x & "')", con)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Item saved Successfully", strTitle)
                    TextBox3.Clear()
                    TextBox1.Clear()
                    Food_Snacks.Refresh()




                End If
            End If
           
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub MenuItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        reload()

    End Sub
    Public Sub reload()
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Menu", con)
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader
            While reader.Read
                ComboBox2.Items.Add(reader.Item(0))
            End While
            reader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            If Not ComboBox2.Items.Contains(ComboBox2.Text) Then
                MessageBox.Show("Please Item is not apart of the Menu list " & ComboBox2.Text, strTitle)
                ComboBox2.Focus()
                ComboBox2.DroppedDown = True
                Exit Sub
            Else
                If TextBox2.Text = "" Then
                    MessageBox.Show("Please enter new price for Item  " & ComboBox2.Text, strTitle)
                    TextBox2.Focus()
                    Exit Sub
                Else
                    If con.State = ConnectionState.Closed Then con.Open()
                    Dim cmd As New SqlCommand("UPDATE Menu SET PricePerItem = '" & CInt(TextBox2.Text) & "' WHERE MenuItem = '" & ComboBox2.Text & "'", con)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Successfully changed", strTitle)
                    TextBox2.Clear()
                    ComboBox2.Text = "Select Item"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        ComboBox2.Items.Clear()
        reload()

    End Sub
End Class