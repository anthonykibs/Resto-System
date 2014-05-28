Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.SqlServer
Imports System.Drawing.Drawing2D
Public Class Damaged
    Dim strTitle As String = "ETHNIC -- Bar and Lounge"
    Dim dbBeer, dbWiSPi, dbSodWat, TransNoSales As Integer
    Dim con As New SqlConnection("Server=localhost; database=RestuarantBar; uid=sa; password=pass;")
    Private Sub Damaged_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            con.Open()
            '******************************************************************
            'add waiters number to the waiter number combo box
            Dim cmdw As New SqlCommand("SELECT EmpIdNo FROM Employees WHERE Role = '" & "Waiter" & "' OR Role= '" & "Waitress" & "'", con)


            Dim cat As String
            cat = "Soda"
            Dim cmd As New SqlCommand("SELECT * FROM StockItems1 WHERE Category = '" & cat.Trim & "'OR Category = '" & "Water" & "' OR Category = '" & "EnergyDrinks" & "' ", con) 'retrieve soft drinks
            Dim cmd1 As New SqlCommand("SELECT * FROM StockItems1 WHERE Category = '" & "Beer" & "'", con) 'retieve beer items add them to cboBeers
            Dim cmd2 As New SqlCommand("SELECT * FROM StockItems1 WHERE Category = '" & "Beer" & "' OR Category =  '" & "Spirits & Whisky" & "' OR Category = '" & "Wines" & "'", con) ' retrieve alcoholic items
            Dim cmd3 As New SqlCommand("SELECT * FROM StockItems1", con)
            Dim cmd4 As New SqlCommand("SELECT * FROM StockItems1 WHERE Category = '" & "Spirits & Whisky" & "' OR Category = '" & "Wines" & "' ", con)
            Dim cmd5 As New SqlCommand("SELECT * FROM SalesNew", con)
            Dim cdm6 As New SqlCommand("SELECT * FROM PurchasesNew", con)


            Dim reader, reader1, reader4 As SqlDataReader

            reader = cmd.ExecuteReader

            While reader.Read
                cboSodaWater.Items.Add(reader.Item("ItemName"))
            End While
            reader.Close()
            '********************************************************************
            reader1 = cmd1.ExecuteReader
            While reader1.Read ' add itmes to the Beer combo box
                cboBeers.Items.Add(reader1.Item("ItemName"))
            End While
            reader1.Close()
            '*********************************************************************
            
            '*********************************************************************

            '**********************************************************************
            reader4 = cmd4.ExecuteReader
            While reader4.Read
                cboWhiskeys.Items.Add(reader4.Item("ItemName"))
            End While
            reader4.Close()
            '*************************************************************************
        Catch ex As Exception
            MessageBox.Show("Connection to database failed" & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
        End Try
    End Sub

    Private Sub btnSoWat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSoWat.Click
        Try
            Dim dt As DateTime = DateTimePicker1.Value.Date & " " & TimeOfDay
            Dim cmd As New SqlCommand("INSERT INTO Damages VALUES('" & cboSodaWater.SelectedItem & "','" & txtQtySodWat.Text & "', '" & dt & "')", con)
            con.Open()
            cmd.ExecuteNonQuery()
            MessageBox.Show("Saved Successfully")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        
    End Sub

    Private Sub btnBeer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBeer.Click
        Try
            con.Open()
            Dim dt As DateTime = DateTimePicker1.Value.Date & " " & TimeOfDay
            Dim cmd As New SqlCommand("INSERT INTO Damages VALUES('" & cboBeers.SelectedItem & "', '" & txtQtyBeers.Text & "', '" & dt & "')", con)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Saved Successfully")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
       
    End Sub

    Private Sub btnWish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWish.Click
        Try
            con.Open()
            Dim dt As DateTime = DateTimePicker1.Value.Date & " " & TimeOfDay
            Dim cmd As New SqlCommand("INSERT INTO Damages VALUES('" & cboWhiskeys.SelectedItem & "', '" & txtQtyWhiskry.Text & "', '" & dt & "')", con)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Saved Successfully")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
       
    End Sub
End Class