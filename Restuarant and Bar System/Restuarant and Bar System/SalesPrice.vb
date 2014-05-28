Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.SqlServer
Imports System.Drawing.Drawing2D
Public Class SalesPrice
    Dim con As New SqlConnection("Server=localhost; database=RestuarantBar; uid=sa; password=pass;")
    Dim strTitle As String = "ETHNIC -- Bar and Lounge"
    Private Sub SalesPrice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            con.Open()
            Dim cmd As New SqlCommand("SELECT ItemName FROM StockItems1", con)
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader
            While reader.Read
                ComboBox1.Items.Add(reader.Item("ItemName"))
            End While
            reader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
       
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            Dim cmd As New SqlCommand("SELECT SalesPrice1,SPrice2 FROM StockItems1 WHERE ItemName = '" & ComboBox1.Text & "'", con)
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader
            While reader.Read
                TextBox1.Text = reader.Item("SalesPrice1")
                TextBox2.Text = reader.Item("SPrice2")
            End While
            reader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        
        Try
            If con.State = ConnectionState.Closed Then con.Open()

            Dim cmd As New SqlCommand("UPDATE StockItems1 SET SalesPrice1 = '" & CInt(TextBox3.Text) & "' WHERE ItemName = '" & ComboBox1.Text & "'", con)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Price set successfully", strTitle)
            TextBox3.Clear()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            Dim cmd As New SqlCommand("UPDATE StockItems1 SET SPrice2 = '" & CInt(TextBox4.Text) & "' WHERE ItemName = '" & ComboBox1.Text & "'", con)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Price set successfully", strTitle)
            TextBox4.Clear()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class