Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.SqlServer
Imports System.Drawing.Drawing2D
Public Class Reports
    Dim strTitle As String = "ETHNIC -- Bar and Lounge"
    Dim con As New SqlConnection("Server=localhost; database=RestuarantBar; uid=sa; password=pass;")
    Private Sub Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            RadioButton1.Checked = True
            RadioButton3.Checked = True
            reload()

        Catch ex As Exception
            MessageBox.Show("Connection to database failed " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
        End Try
    End Sub
    Private Sub reload()
       
        con.Open()
        Dim cat As String
        cat = "Soda"
        
        Dim cmd3 As New SqlCommand("SELECT * FROM StockItems1", con)

        Dim cmd5 As New SqlCommand("SELECT * FROM SalesNew", con)
        Dim cdm6 As New SqlCommand("SELECT * FROM PurchasesNew", con)
       
        Dim reader3, reader5, reader6 As SqlDataReader

        Dim i2 As Integer
        reader3 = cmd3.ExecuteReader
        While reader3.Read
            dgvItemStock.Rows.Add()
            dgvItemStock.Rows(i2).Cells(0).Value = reader3.Item("ItemName")
            dgvItemStock.Rows(i2).Cells(1).Value = reader3.Item("Category")
            dgvItemStock.Rows(i2).Cells(2).Value = reader3.Item("CurrentStock")
            dgvItemStock.Rows(i2).Cells(3).Value = reader3.Item("BPricePerItem")
            dgvItemStock.Rows(i2).Cells(4).Value = reader3.Item("SPrice2")
            dgvItemStock.Rows(i2).Cells(5).Value = reader3.Item("SupplierName")
            i2 = i2 + 1
        End While
        reader3.Close()
        '**********************************************************************
       
        '**************************************************************************

        reader5 = cmd5.ExecuteReader
        Dim i3 As Integer
        While reader5.Read
            dgvSalesRep.Rows.Add()
            dgvSalesRep.Rows(i3).Cells(0).Value = reader5.Item("Date")
            dgvSalesRep.Rows(i3).Cells(1).Value = reader5.Item("ItemName")
            dgvSalesRep.Rows(i3).Cells(2).Value = reader5.Item("Quantity")
            dgvSalesRep.Rows(i3).Cells(3).Value = reader5.Item("UnitPrice")
            dgvSalesRep.Rows(i3).Cells(4).Value = reader5.Item("TotalAmmount")
            dgvSalesRep.Rows(i3).Cells(5).Value = reader5.Item("AuthorizedBy")
            dgvSalesRep.Rows(i3).Cells(6).Value = reader5.Item("TransNo")
            dgvSalesRep.Rows(i3).Cells(7).Value = reader5.Item("WaiterNo")
            'Sum up the total sales
            Dim x As Integer = 0
            Dim total As Integer = 0
            While x < dgvSalesRep.Rows.Count
                total = total + dgvSalesRep.Rows(x).Cells(4).Value
                lblTotalSal.Text = "TOTAL SALES  : " & total & " /="
                x = x + 1
            End While
            i3 = i3 + 1
        End While


        reader5.Close()
        '**********************************************************************************
        Dim i4 As Integer
        reader6 = cdm6.ExecuteReader
        While reader6.Read
            dgvPurcRep.Rows.Add()
            dgvPurcRep.Rows(i4).Cells(0).Value = reader6.Item("Date")
            dgvPurcRep.Rows(i4).Cells(1).Value = reader6.Item("ItemName")
            dgvPurcRep.Rows(i4).Cells(2).Value = reader6.Item("Category")
            dgvPurcRep.Rows(i4).Cells(3).Value = reader6.Item("NumPacks")
            dgvPurcRep.Rows(i4).Cells(4).Value = reader6.Item("ItemPerPack")
            dgvPurcRep.Rows(i4).Cells(5).Value = reader6.Item("BpricePerItem")
            dgvPurcRep.Rows(i4).Cells(6).Value = reader6.Item("BpricePack")
            dgvPurcRep.Rows(i4).Cells(7).Value = reader6.Item("CurStock")
            dgvPurcRep.Rows(i4).Cells(8).Value = reader6.Item("TotalStock")
            dgvPurcRep.Rows(i4).Cells(9).Value = reader6.Item("TotalCost")
            dgvPurcRep.Rows(i4).Cells(10).Value = reader6.Item("AuthorisedBy")
            dgvPurcRep.Rows(i4).Cells(11).Value = reader6.Item("Transaction #")
            dgvPurcRep.Rows(i4).Cells(12).Value = reader6.Item("EmpIdNo")


            'Sum up the total sales
            Dim x As Integer = 0
            Dim total As Integer = 0
            While x < dgvPurcRep.Rows.Count
                total = total + dgvPurcRep.Rows(x).Cells(9).Value
                lblTotPur.Text = "TOTAL PURCHASES  : " & total & " /="
                x = x + 1
            End While
            i4 = i4 + 1

        End While
        reader6.Close()

        Dim cmdf As New SqlCommand("SELECT * FROM FOOD", con)
        Dim readerf As SqlDataReader
        readerf = cmdf.ExecuteReader
        Dim id As Integer = 0
        While readerf.Read
            dgvfood.Rows.Add()
            dgvfood.Rows(id).Cells(0).Value = readerf.Item("Date")
            dgvfood.Rows(id).Cells(1).Value = readerf.Item("MenuItem")
            dgvfood.Rows(id).Cells(2).Value = readerf.Item("Price")
            dgvfood.Rows(id).Cells(3).Value = readerf.Item("Quantity")
            dgvfood.Rows(id).Cells(4).Value = readerf.Item("TotalCost")
            dgvfood.Rows(id).Cells(5).Value = readerf.Item("TransNo")
            dgvfood.Rows(id).Cells(6).Value = readerf.Item("WaiterNo")
            dgvfood.Rows(id).Cells(7).Value = readerf.Item("AuthorizedBy")


            Dim xl As Integer = 0
            Dim totall As Integer = 0
            While xl < dgvfood.Rows.Count
                totall = totall + dgvfood.Rows(xl).Cells(4).Value
                lblFd.Text = "TOTAL SALES  : " & totall & " /="
                xl = xl + 1
            End While
            id = id + 1
        End While
        readerf.Close()

    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            Label18.Text = "Start Date "

            DateTimePicker1.Enabled = True
        Else
            Label18.Text = "Enter date "

            DateTimePicker1.Enabled = False
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            DateTimePicker3.Enabled = True
            Label1.Text = "Start Date"

        Else
            DateTimePicker3.Enabled = False
            Label1.Text = "Enter Date"

        End If
    End Sub
    Public Sub dtgrid()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
       
        Try

            lblTotPur.Visible = False
            Dim dboDT, dboDTstrt, dboDTend As Date
            dboDT = DateTimePicker2.Value.Date
            dboDTstrt = DateTimePicker2.Value.Date
            dboDTend = DateTimePicker1.Value.Date
            Dim cmd, cmdx As New SqlCommand
            Dim reader, readerx As SqlDataReader
            Dim i As Integer
            If RadioButton3.Checked = True Then
                Dim respo = MessageBox.Show("Your search query is at for these dates : " & vbNewLine & vbTab & dboDTstrt & " and " & dboDTend, strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If respo = DialogResult.OK Then
                ElseIf respo = DialogResult.Cancel Then
                    Exit Sub
                End If
                dgvPurcRep.Visible = False
                dgvPurSpec.Rows.Clear()
                dgvPurSpec.Visible = True
                If con.State = ConnectionState.Closed Then con.Open()
                cmdx.CommandText = "SELECT * FROM PurchasesNew WHERE cast(CONVERT(varchar(8), Date, 112) AS datetime) >= '" & dboDTstrt & "' AND cast(CONVERT(varchar(8), Date, 112) AS datetime) <= '" & dboDTend & "'  ORDER BY DATE"
                cmdx.Connection = con
                readerx = cmdx.ExecuteReader
                If readerx.HasRows Then
                    While readerx.Read
                        dgvPurSpec.Rows.Add()
                        dgvPurSpec.Rows(i).Cells(0).Value = readerx.Item("Date")
                        dgvPurSpec.Rows(i).Cells(1).Value = readerx.Item("ItemName")
                        dgvPurSpec.Rows(i).Cells(2).Value = readerx.Item("Category")
                        dgvPurSpec.Rows(i).Cells(3).Value = readerx.Item("NumPacks")
                        dgvPurSpec.Rows(i).Cells(4).Value = readerx.Item("ItemPerPack")
                        dgvPurSpec.Rows(i).Cells(5).Value = readerx.Item("BpricePerItem")
                        dgvPurSpec.Rows(i).Cells(6).Value = readerx.Item("BpricePack")
                        dgvPurSpec.Rows(i).Cells(7).Value = readerx.Item("CurStock")
                        dgvPurSpec.Rows(i).Cells(8).Value = readerx.Item("TotalStock")
                        dgvPurSpec.Rows(i).Cells(9).Value = readerx.Item("TotalCost")
                        dgvPurSpec.Rows(i).Cells(10).Value = readerx.Item("AuthorisedBy")
                        dgvPurSpec.Rows(i).Cells(11).Value = readerx.Item("Transaction #")
                        dgvPurSpec.Rows(i).Cells(12).Value = readerx.Item("EmpIdNo")
                        i = i + 1
                    End While
                Else
                    Dim resps = MessageBox.Show("No results found for Search query Specific to these date :" & vbNewLine & vbTab & dboDTstrt & " and " & vbTab & dboDTend, strTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Information)
                    If resps = DialogResult.Retry Then
                        pnlDate.Visible = True
                        DateTimePicker2.Focus()
                        Exit Sub
                    Else
                        Exit Sub
                    End If

                End If

                Dim x As Integer = 0
                Dim total As Integer = 0
                While x < dgvPurSpec.Rows.Count
                    total = total + dgvPurSpec.Rows(x).Cells(9).Value
                    Dim damount2 As Decimal = CType(total, Decimal) 'say you have enteBlack 1400.10345
                    Dim X1 As String = String.Format("{0:n2}", damount2) 'the result after formating will look like 1,400.10
                    Labelt.Text = " Total Purchases : " & vbNewLine & X1 & "/= between : " & dboDTstrt & vbNewLine & " and " & dboDTend
                    x = x + 1
                    Labelt.Visible = True
                End While
                readerx.Close()
            ElseIf RadioButton4.Checked = True Then
                If con.State = ConnectionState.Closed Then con.Open()
                dgvPurSpec.Rows.Clear()
                dgvPurcRep.Visible = False
                dgvPurSpec.Visible = True
                Dim respo = MessageBox.Show("Your search query is at for this date : " & vbNewLine & vbTab & dboDT, strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If respo = DialogResult.OK Then
                ElseIf respo = DialogResult.Cancel Then
                    Exit Sub
                End If
                cmd.CommandText = "SELECT * FROM PurchasesNew WHERE cast(CONVERT(varchar(8), Date, 112) AS datetime) ='" & dboDT & "' "
                cmd.Connection = con
                reader = cmd.ExecuteReader

                If reader.HasRows Then
                    While reader.Read
                        dgvPurSpec.Rows.Add()
                        dgvPurSpec.Rows(i).Cells(0).Value = reader.Item("Date")
                        dgvPurSpec.Rows(i).Cells(1).Value = reader.Item("ItemName")
                        dgvPurSpec.Rows(i).Cells(2).Value = reader.Item("Category")
                        dgvPurSpec.Rows(i).Cells(3).Value = reader.Item("NumPacks")
                        dgvPurSpec.Rows(i).Cells(4).Value = reader.Item("ItemPerPack")
                        dgvPurSpec.Rows(i).Cells(5).Value = reader.Item("BpricePerItem")
                        dgvPurSpec.Rows(i).Cells(6).Value = reader.Item("BpricePack")
                        dgvPurSpec.Rows(i).Cells(7).Value = reader.Item("CurStock")
                        dgvPurSpec.Rows(i).Cells(8).Value = reader.Item("TotalStock")
                        dgvPurSpec.Rows(i).Cells(9).Value = reader.Item("TotalCost")
                        dgvPurSpec.Rows(i).Cells(10).Value = reader.Item("AuthorisedBy")
                        dgvPurSpec.Rows(i).Cells(11).Value = reader.Item("Transaction #")
                        dgvPurSpec.Rows(i).Cells(12).Value = reader.Item("EmpIdNo")
                        i = i + 1
                    End While
                Else
                    Dim resps = MessageBox.Show("No results found for Search query Specific to this date :" & vbNewLine & vbTab & dboDT, strTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Information)
                    If resps = DialogResult.Retry Then
                        pnlDate.Visible = True
                        DateTimePicker2.Focus()
                        Exit Sub
                    Else
                        Exit Sub
                    End If

                End If



                Dim x As Integer = 0
                Dim total As Integer = 0
                While x < dgvPurSpec.Rows.Count
                    total = total + dgvPurSpec.Rows(x).Cells(9).Value
                    Dim damount2 As Decimal = CType(total, Decimal) 'say you have enteBlack 1400.10345
                    Dim X1 As String = String.Format("{0:n2}", damount2) 'the result after formating will look like 1,400.10
                    Labelt.Text = " Total Purchases : " & vbNewLine & X1 & "/= as at : " & dboDT
                    x = x + 1
                    Labelt.Visible = True
                End While
                reader.Close()

            End If
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
            dgvSalesRep.Visible = True
            dgvSaleSpec.Visible = False
            Label11.Visible = False
            lblTotalSal.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try

            lblTotalSal.Visible = False
            Dim dboDT, dbStart, dbEnd As Date
            dboDT = DateTimePicker4.Value.Date
            dbStart = DateTimePicker4.Value.Date
            dbEnd = DateTimePicker3.Value.Date
            Dim cmd, cmdx As New SqlCommand
            Dim reader, readerx As SqlDataReader
            Dim i As Integer
            con.Open()
            If RadioButton2.Checked = True Then
                dgvSaleSpec.Visible = True
                dgvSalesRep.Visible = False
                dgvSaleSpec.Rows.Clear()
                Dim respo = MessageBox.Show("Your search query is at for this date:" & vbNewLine & dboDT, strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If respo = DialogResult.OK Then

                ElseIf respo = DialogResult.Cancel Then
                    Exit Sub
                End If
                cmd.CommandText = "SELECT * FROM SalesNew WHERE cast(CONVERT(varchar(8), Date, 112) AS datetime) = '" & dboDT & "'"
                cmd.Connection = con
                reader = cmd.ExecuteReader

                If reader.HasRows Then
                    While reader.Read
                        dgvSaleSpec.Rows.Add()
                        dgvSaleSpec.Rows(i).Cells(0).Value = reader.Item("Date")
                        dgvSaleSpec.Rows(i).Cells(1).Value = reader.Item("ItemName")
                        dgvSaleSpec.Rows(i).Cells(2).Value = reader.Item("Quantity")
                        dgvSaleSpec.Rows(i).Cells(3).Value = reader.Item("UnitPrice")
                        dgvSaleSpec.Rows(i).Cells(4).Value = reader.Item("TotalAmmount")
                        dgvSaleSpec.Rows(i).Cells(5).Value = reader.Item("AuthorizedBy")
                        dgvSaleSpec.Rows(i).Cells(6).Value = reader.Item("TransNo")
                        dgvSaleSpec.Rows(i).Cells(7).Value = reader.Item("WaiterNo")
                        i = i + 1


                    End While

                Else
                    Dim resps = MessageBox.Show("No results found for search query Specific to this date" & vbNewLine & vbTab & dboDT, strTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Information)
                    If resps = DialogResult.Retry Then
                        pnlDate.Visible = True
                        DateTimePicker2.Focus()
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If

                Dim x As Integer = 0
                Dim total As Integer = 0
                While x < dgvSaleSpec.Rows.Count
                    total = total + dgvSaleSpec.Rows(x).Cells(4).Value
                    x = x + 1
                    Dim damount2 As Decimal = CType(total, Decimal) 'say you have enteBlack 1400.10345
                    Dim X1 As String = String.Format("{0:n2}", damount2) 'the result after formating will look like 1,400.10
                    Label11.Text = " Total Sales : " & X1 & "/= as at : " & dboDT
                    Label11.Visible = True
                End While


                Label11.Visible = True
                reader.Close()
            ElseIf RadioButton1.Checked = True Then

                dgvSaleSpec.Visible = True
                dgvSalesRep.Visible = False
                dgvSaleSpec.Rows.Clear()
                Dim respo = MessageBox.Show("Your search query is at for these date:" & vbNewLine & dbStart & " and " & dbEnd, strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If respo = DialogResult.OK Then

                ElseIf respo = DialogResult.Cancel Then
                    Exit Sub
                End If
                cmdx.CommandText = "SELECT * FROM SalesNew WHERE cast(CONVERT(varchar(8), Date, 112) AS datetime) >= '" & dbStart & "' AND cast(CONVERT(varchar(8), Date, 112) AS datetime) <= '" & dbEnd & "' ORDER BY DATE "
                cmdx.Connection = con
                readerx = cmdx.ExecuteReader

                If readerx.HasRows Then
                    While readerx.Read
                        dgvSaleSpec.Rows.Add()
                        dgvSaleSpec.Rows(i).Cells(0).Value = readerx.Item("Date")
                        dgvSaleSpec.Rows(i).Cells(1).Value = readerx.Item("ItemName")
                        dgvSaleSpec.Rows(i).Cells(2).Value = readerx.Item("Quantity")
                        dgvSaleSpec.Rows(i).Cells(3).Value = readerx.Item("UnitPrice")
                        dgvSaleSpec.Rows(i).Cells(4).Value = readerx.Item("TotalAmmount")
                        dgvSaleSpec.Rows(i).Cells(5).Value = readerx.Item("AuthorizedBy")
                        dgvSaleSpec.Rows(i).Cells(6).Value = readerx.Item("TransNo")
                        dgvSaleSpec.Rows(i).Cells(7).Value = readerx.Item("WaiterNo")
                        i = i + 1


                    End While

                Else
                    Dim resps = MessageBox.Show("No results found for search query Specific to this date" & vbNewLine & vbTab & dbStart & " and " & dbEnd, strTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Information)
                    If resps = DialogResult.Retry Then
                        pnlDate.Visible = True
                        DateTimePicker2.Focus()
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If

                Dim x As Integer = 0
                Dim total As Integer = 0
                While x < dgvSaleSpec.Rows.Count
                    total = total + dgvSaleSpec.Rows(x).Cells(4).Value
                    x = x + 1
                    Dim damount2 As Decimal = CType(total, Decimal) 'say you have enteBlack 1400.10345
                    Dim X1 As String = String.Format("{0:n2}", damount2) 'the result after formating will look like 1,400.10
                    Label11.Text = " Total Sales : " & X1 & "/= Between : " & dbStart & vbNewLine & " and " & dbEnd
                    Label11.Visible = True
                End While
                Label11.Visible = True
                readerx.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            lblTotPur.Visible = True
            Labelt.Visible = False
            dgvPurcRep.Visible = True
            dgvPurSpec.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadioButton6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton6.CheckedChanged
        If RadioButton6.Checked = True Then
            Label3.Text = "Start Date "

            DateTimePicker5.Enabled = True
        Else
            Label3.Text = "Enter date "

            DateTimePicker5.Enabled = False
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click

        Try

            lblFd.Visible = False
            Dim dboDT, dboDTstrt, dboDTend As Date
            dboDT = DateTimePicker6.Value.Date
            dboDTstrt = DateTimePicker6.Value.Date
            dboDTend = DateTimePicker5.Value.Date
            Dim cmd, cmdx As New SqlCommand
            Dim reader, readerx As SqlDataReader
            Dim i As Integer
            If RadioButton6.Checked = True Then
                Dim respo = MessageBox.Show("Your search query is at for these dates : " & vbNewLine & vbTab & dboDTstrt & " and " & dboDTend, strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If respo = DialogResult.OK Then
                ElseIf respo = DialogResult.Cancel Then
                    Exit Sub
                End If
                dgvfood.Visible = False
                dgvFoodSPec.Rows.Clear()
                dgvFoodSPec.Visible = True
                If con.State = ConnectionState.Closed Then con.Open()
                cmdx.CommandText = "SELECT * FROM Food WHERE cast(CONVERT(varchar(8), Date, 112) AS datetime) >= '" & dboDTstrt & "' AND cast(CONVERT(varchar(8), Date, 112) AS datetime) <= '" & dboDTend & "'  ORDER BY DATE"
                cmdx.Connection = con
                readerx = cmdx.ExecuteReader
                If readerx.HasRows Then
                    While readerx.Read
                        dgvFoodSPec.Rows.Add()
                        dgvFoodSPec.Rows(i).Cells(0).Value = readerx.Item("Date")
                        dgvFoodSPec.Rows(i).Cells(1).Value = readerx.Item("MenuItem")
                        dgvFoodSPec.Rows(i).Cells(2).Value = readerx.Item("Price")
                        dgvFoodSPec.Rows(i).Cells(3).Value = readerx.Item("Quantity")
                        dgvFoodSPec.Rows(i).Cells(4).Value = readerx.Item("TotalCost")
                        dgvFoodSPec.Rows(i).Cells(5).Value = readerx.Item("TransNo")
                        dgvFoodSPec.Rows(i).Cells(6).Value = readerx.Item("WaiterNo")
                        dgvFoodSPec.Rows(i).Cells(7).Value = readerx.Item("AuthorizedBy")
                        i = i + 1
                    End While
                Else
                    Dim resps = MessageBox.Show("No results found for Search query Specific to these date :" & vbNewLine & vbTab & dboDTstrt & " and " & vbTab & dboDTend, strTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Information)
                    If resps = DialogResult.Retry Then
                        pnlDate.Visible = True
                        DateTimePicker6.Focus()
                        Exit Sub
                    Else
                        Exit Sub
                    End If

                End If

                Dim x As Integer = 0
                Dim total As Integer = 0
                While x < dgvFoodSPec.Rows.Count
                    total = total + dgvFoodSPec.Rows(x).Cells(4).Value
                    Dim damount2 As Decimal = CType(total, Decimal) 'say you have enteBlack 1400.10345
                    Dim X1 As String = String.Format("{0:n2}", damount2) 'the result after formating will look like 1,400.10
                    lblFo.Text = " Total Food Sales : " & vbNewLine & X1 & "/= between : " & dboDTstrt & vbNewLine & " and " & dboDTend
                    x = x + 1
                    lblFo.Visible = True
                End While
                readerx.Close()
            ElseIf RadioButton5.Checked = True Then
                If con.State = ConnectionState.Closed Then con.Open()
                dgvFoodSPec.Rows.Clear()
                dgvfood.Visible = False
                dgvFoodSPec.Visible = True
                Dim respo = MessageBox.Show("Your search query is at for this date : " & vbNewLine & vbTab & dboDT, strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If respo = DialogResult.OK Then
                ElseIf respo = DialogResult.Cancel Then
                    Exit Sub
                End If
                cmd.CommandText = "SELECT * FROM Food WHERE cast(CONVERT(varchar(8), Date, 112) AS datetime) ='" & dboDT & "' "
                cmd.Connection = con
                reader = cmd.ExecuteReader

                If reader.HasRows Then
                    While reader.Read
                        dgvFoodSPec.Rows.Add()
                        dgvFoodSPec.Rows(i).Cells(0).Value = reader.Item("Date")
                        dgvFoodSPec.Rows(i).Cells(1).Value = reader.Item("MenuItem")
                        dgvFoodSPec.Rows(i).Cells(2).Value = reader.Item("Price")
                        dgvFoodSPec.Rows(i).Cells(3).Value = reader.Item("Quantity")
                        dgvFoodSPec.Rows(i).Cells(4).Value = reader.Item("TotalCost")
                        dgvFoodSPec.Rows(i).Cells(5).Value = reader.Item("TransNo")
                        dgvFoodSPec.Rows(i).Cells(6).Value = reader.Item("WaiterNo")
                        dgvFoodSPec.Rows(i).Cells(7).Value = reader.Item("AuthorizedBy")
                        i = i + 1
                    End While
                Else
                    Dim resps = MessageBox.Show("No results found for Search query Specific to this date :" & vbNewLine & vbTab & dboDT, strTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Information)
                    If resps = DialogResult.Retry Then
                        pnlDate.Visible = True
                        DateTimePicker6.Focus()
                        Exit Sub
                    Else
                        Exit Sub
                    End If

                End If



                Dim x As Integer = 0
                Dim total As Integer = 0
                While x < dgvFoodSPec.Rows.Count
                    total = total + dgvFoodSPec.Rows(x).Cells(4).Value
                    Dim damount2 As Decimal = CType(total, Decimal) 'say you have enteBlack 1400.10345
                    Dim X1 As String = String.Format("{0:n2}", damount2) 'the result after formating will look like 1,400.10
                    lblFo.Text = " Total Food Sales : " & vbNewLine & X1 & "/= as at : " & dboDT
                    x = x + 1
                    lblFo.Visible = True
                End While
                reader.Close()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            dgvfood.Visible = True
            dgvFoodSPec.Visible = False
            lblFo.Visible = False
            lblFd.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        dgvfood.Rows.Clear()
        dgvFoodSPec.Rows.Clear()
        dgvItemStock.Rows.Clear()
        dgvPurcRep.Rows.Clear()
        dgvPurSpec.Rows.Clear()
        dgvSaleSpec.Rows.Clear()
        dgvSalesRep.Rows.Clear()
        Reports_Load(sender, e)
    End Sub

    Private Sub dgvfood_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvfood.CellContentClick

    End Sub

    Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub DateTimePicker6_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker6.ValueChanged

    End Sub

    Private Sub DateTimePicker5_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker5.ValueChanged

    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub RadioButton5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton5.CheckedChanged

    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub lblFd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFd.Click

    End Sub

    Private Sub lblFo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFo.Click

    End Sub

    Private Sub dgvFoodSPec_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFoodSPec.CellContentClick

    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub dgvItemStock_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemStock.CellContentClick

    End Sub

    Private Sub TabPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage4.Click

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub

    Private Sub dgvPurSpec_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPurSpec.CellContentClick

    End Sub

    Private Sub dgvPurcRep_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPurcRep.CellContentClick

    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged

    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub Label17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label17.Click

    End Sub

    Private Sub Label18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label18.Click

    End Sub

    Private Sub pnlDate_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlDate.Paint

    End Sub

    Private Sub lblTotPur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTotPur.Click

    End Sub

    Private Sub Labelt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Labelt.Click

    End Sub

    Private Sub dgvSaleSpec_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSaleSpec.CellContentClick

    End Sub

    Private Sub dgvSalesRep_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSalesRep.CellContentClick

    End Sub

    Private Sub DateTimePicker4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker4.ValueChanged

    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker3.ValueChanged

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Label11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label11.Click

    End Sub

    Private Sub lblTotalSal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTotalSal.Click

    End Sub

    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub TabPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage3.Click

    End Sub


    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintPurch.PrintPage
        Dim fntAdress As New Font("Comic Sans MS", 10, FontStyle.Regular)
        Dim fntHeader As New Font("Calibri", 20, FontStyle.Bold)
        Dim fntBodyText As New Font("Calibri", 12, FontStyle.Regular)
        Dim fntHeaderText As New Font("Calibri", 13, FontStyle.Bold)


        e.Graphics.DrawString("ETHNIC BAR AND LOUNGE", fntHeader, Brushes.Black, 150, 0)
        e.Graphics.DrawString("GENERATED PURCHASES REPORT", New Font("calibri", 18, FontStyle.Bold), Brushes.Black, 150, 30)
        e.Graphics.DrawLine(Pens.Black, 5, 140, 850, 140)
        e.Graphics.DrawString("Date", fntHeaderText, Brushes.Black, 10, 107)
        e.Graphics.DrawString("Item", fntHeaderText, Brushes.Black, 170, 107)
        e.Graphics.DrawString("Name", fntHeaderText, Brushes.Black, 170, 120)
        e.Graphics.DrawString("Qty", fntHeaderText, Brushes.Black, 250, 107)
        e.Graphics.DrawString("Qty /", fntHeaderText, Brushes.Black, 300, 107)
        e.Graphics.DrawString("Pack", fntHeaderText, Brushes.Black, 300, 120)
        e.Graphics.DrawString("Price", fntHeaderText, Brushes.Black, 380, 107)
        e.Graphics.DrawString("Per Pack", fntHeaderText, Brushes.Black, 380, 120)
        e.Graphics.DrawString("Current", fntHeaderText, Brushes.Black, 480, 107)
        e.Graphics.DrawString("Stock", fntHeaderText, Brushes.Black, 480, 120)
        e.Graphics.DrawString("Total", fntHeaderText, Brushes.Black, 580, 107)
        e.Graphics.DrawString("Stock", fntHeaderText, Brushes.Black, 580, 120)
        e.Graphics.DrawString("Total", fntHeaderText, Brushes.Black, 580, 107)
        e.Graphics.DrawString("Cost", fntHeaderText, Brushes.Black, 680, 120)
        e.Graphics.DrawString("Authorized By", fntHeaderText, Brushes.Black, 780, 107)
        e.Graphics.DrawString("Transaction #", fntHeaderText, Brushes.Black, 780, 107)
        'if radiobutoon1.checked = true then 
        Dim rowcount As Integer = dgvPurcRep.Rows.Count - 1
        Dim i As Integer = 139
        Dim x1 = 10
        Dim x2 = 700
        Dim y1 = 155
        Dim n As Integer = 0

        While n < rowcount
            'e.Graphics.DrawRectangle(Pens.Black, New Rectangle(5, i - 5, 770, 35))
            ' e.Graphics.DrawRectangle(Pens.Black, New Rectangle(5, i - 5, 770, 35))
            e.Graphics.DrawString(dgvPurcRep.Rows(n).Cells(0).Value, fntBodyText, Brushes.Black, 10, i)
            e.Graphics.DrawString(dgvPurcRep.Rows(n).Cells(1).Value, fntBodyText, Brushes.Black, 170, i)

            e.Graphics.DrawString(dgvPurcRep.Rows(n).Cells(3).Value, fntBodyText, Brushes.Black, 260, i)
            e.Graphics.DrawString(dgvPurcRep.Rows(n).Cells(4).Value, fntBodyText, Brushes.Black, 300, i)
            e.Graphics.DrawString(dgvPurcRep.Rows(n).Cells(5).Value, fntBodyText, Brushes.Black, 400, i)
            e.Graphics.DrawString(dgvPurcRep.Rows(n).Cells(6).Value, fntBodyText, Brushes.Black, 500, i)
            e.Graphics.DrawString(dgvPurcRep.Rows(n).Cells(7).Value, fntBodyText, Brushes.Black, 600, i)
            e.Graphics.DrawString(dgvPurcRep.Rows(n).Cells(8).Value, fntBodyText, Brushes.Black, 700, i)
            e.Graphics.DrawString(dgvPurcRep.Rows(n).Cells(9).Value, fntBodyText, Brushes.Black, 800, i)
            i = i + 35

           
            n = n + 1
        End While

        'elseif radiobuttn3.checked true then

    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        PrintPurch.Print()

    End Sub
End Class