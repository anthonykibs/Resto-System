
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.SqlServer
Imports System.Drawing.Drawing2D

Public Class Users
    Dim strTitle As String = "ETHNIC -- Bar and Lounge"
    Dim con As New SqlConnection("Server=localhost; database=RestuarantBar; uid=sa; password=pass;")
    Private Sub Users_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadUsers()

    End Sub
    Public Sub LoadUsers()
        Try
            dgvEmp.Rows.Clear()
            dgvUsers.Rows.Clear()

            If con.State = ConnectionState.Closed Then con.Open()
            Dim cmd, cmd1 As New SqlCommand
            Dim reader, reader1 As SqlDataReader
            cmd.CommandText = "SELECT * FROM Employees"
            cmd1.CommandText = "SELECT * FROM BarLogin"
            cmd.Connection = con
            cmd1.Connection = con
            reader = cmd1.ExecuteReader


            Dim i, i1 As Integer
            While reader.Read
                dgvUsers.Rows.Add()
                dgvUsers.Rows(i).Cells(0).Value = reader.Item(0)
                dgvUsers.Rows(i).Cells(1).Value = reader.Item(1)
                dgvUsers.Rows(i).Cells(2).Value = reader.Item(2)
                dgvUsers.Rows(i).Cells(3).Value = reader.Item(3)
                i = i + 1
            End While
            reader.Close()
            reader1 = cmd.ExecuteReader
            While reader1.Read
                dgvEmp.Rows.Add()
                dgvEmp.Rows(i1).Cells(0).Value = reader1.Item(0)
                dgvEmp.Rows(i1).Cells(1).Value = reader1.Item(1)
                dgvEmp.Rows(i1).Cells(2).Value = reader1.Item(2)
                dgvEmp.Rows(i1).Cells(3).Value = reader1.Item(3)
                dgvEmp.Rows(i1).Cells(4).Value = reader1.Item(4)


                i1 = i1 + 1
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Dim i As Integer = 0
            While i < 1000
                pnlEmp.Location = New Point(548, 27)
                i = i + 1
            End While
            btnBack.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ViewEmployessToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewEmployessToolStripMenuItem.Click
        Try
            Dim i As Integer = 0
            While i < 1000
                pnlEmp.Location = New Point(7, 27)
                i = i + 1
            End While
            btnBack.Visible = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            Dim index As Integer = dgvUsers.CurrentCell.RowIndex
            Dim empId As String = dgvUsers.Rows(index).Cells(3).Value
            If con.State = ConnectionState.Closed Then con.Open()
            Dim cmd As New SqlCommand("DELETE FROM BarLogin WHERE EmpIdNo = '" & empId & "'", con)
            cmd.ExecuteNonQuery()
            con.Close()
            LoadUsers()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            Dim index As Integer = dgvEmp.CurrentCell.RowIndex
            Dim empId As String = dgvEmp.Rows(index).Cells(0).Value
            If con.State = ConnectionState.Closed Then con.Open()
            Dim cmd As New SqlCommand("DELETE FROM Employees WHERE EmpIdNo = '" & empId & "'", con)
            cmd.ExecuteNonQuery()
            Dim cmdx As New SqlCommand("DELETE FROM BarLogin WHERE EmpIdNo = '" & empId & "'", con)
            cmdx.ExecuteNonQuery()
            con.Close()
            MessageBox.Show("Successfully Deleted")
            LoadUsers()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
End Class