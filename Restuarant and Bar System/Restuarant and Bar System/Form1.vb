Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.SqlServer
Imports System.Drawing.Drawing2D
Public Class Form1
    Dim strTitle As String = "ETHNIC -- Bar and Lounge"
    Dim dbBeer, dbWiSPi, dbSodWat, SalTr As Integer
    Dim CPaIn As String
    Dim CChange As String
    Dim con As New SqlConnection("Server=localhost; database=RestuarantBar; uid=sa; password=pass;")

    Private Sub DataGridView3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    
    Private Sub reload()
        If EthnicLogin.login1 = "Retailer"  Then
            MUpdate.Enabled = False
        End If
        If con.State = ConnectionState.Closed Then con.Open()
        Dim cat As String
        cat = "Soda"
        Dim cmd As New SqlCommand("SELECT * FROM StockItems1 WHERE Category = '" & cat.Trim & "'OR Category = '" & "Water" & "' OR Category = '" & "EnergyDrinks" & "' ", con) 'retrieve soft drinks
        Dim cmd1 As New SqlCommand("SELECT * FROM StockItems1 WHERE Category = '" & "Beer" & "'", con) 'retieve beer items add them to cboBeers
        Dim cmd2 As New SqlCommand("SELECT * FROM StockItems1 WHERE Category = '" & "Beer" & "' OR Category =  '" & "Spirits & Whisky" & "' OR Category = '" & "Wines" & "'", con) ' retrieve alcoholic items
        Dim cmd3 As New SqlCommand("SELECT * FROM StockItems1", con)
        Dim cmd4 As New SqlCommand("SELECT * FROM StockItems1 WHERE Category = '" & "Spirits & Whisky" & "' OR Category = '" & "Wines" & "' ", con)
        Dim cmd5 As New SqlCommand("SELECT * FROM SalesNew", con)
        Dim cdm6 As New SqlCommand("SELECT * FROM PurchasesNew", con)
        Dim cmd7 As New SqlCommand("SELECT * FROM Employees WHERE Role = '" & "Waitress" & "' OR Role = '" & "Waiter" & "'", con)

        Dim reader, reader1, reader2, reader3, reader4, reader5, reader6, reader7 As SqlDataReader
        reader7 = cmd7.ExecuteReader

        While reader7.Read
            cboWaiterNo.Items.Add(reader7.Item("EmpIdNo").ToString)
        End While
        reader7.Close()

        reader = cmd.ExecuteReader
        Dim n As Integer = DataGridView3.RowCount
        Dim i As Integer
        While reader.Read


            DataGridView3.Rows.Add()
            DataGridView3.Rows(i).Cells(0).Value = reader.Item("ItemName")
            DataGridView3.Rows(i).Cells(1).Value = reader.Item("CurrentStock")
            DataGridView3.Rows(i).Cells(2).Value = reader.Item("SPrice2")
            i = i + 1
            Label15.Text = "Types Of Soft Drinks and Energizers : " & DataGridView3.RowCount - 1
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
        reader2 = cmd2.ExecuteReader
        Dim i1 As Integer
        While reader2.Read
            DataGridView2.Rows.Add()
            DataGridView2.Rows(i1).Cells(0).Value = reader2.Item("ItemName")
            DataGridView2.Rows(i1).Cells(1).Value = reader2.Item("CurrentStock")
            DataGridView2.Rows(i1).Cells(2).Value = reader2.Item("SPrice2")
            i1 = i1 + 1
            Label16.Text = "Types Of Alcoholic Drinks : " & DataGridView2.RowCount - 1
        End While
        reader2.Close()
        '*********************************************************************
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
        reader4 = cmd4.ExecuteReader
        While reader4.Read
            cboWhiskeys.Items.Add(reader4.Item("ItemName"))
        End While
        reader4.Close()
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
        Timer1.Start()
        rdbSalRep.Checked = True
        DateTimePicker2.Visible = False
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
           
            reload()

            '******************************************************************
            'add waiters number to the waiter number combo box

        Catch ex As Exception
            MessageBox.Show("Connection to database failed " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
        End Try

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lblTime.Text = TimeOfDay
    End Sub

    Private Sub rdbSalRep_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSalRep.CheckedChanged
        Try
            If rdbSalRep.Checked = True Then
                dgvSalesRep.Visible = True
                dgvPurcRep.Visible = False
                dgvItemStock.Visible = False
                pnlDate.Visible = False
                DateTimePicker2.Visible = False
                btnDgvItRefresh.Visible = False
                btnRefPurRe.Visible = False
                dgvPurSpec.Visible = False
                dgvSaleSpec.Visible = False
                btnretry.Visible = False
                BtnRefSaleRe.Visible = True
                lblTotalSal.Visible = True
                lblTotPur.Visible = False
                Label11.Visible = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbPurDat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPurDat.CheckedChanged
        Try
            If rdbPurDat.Checked = True Then
                dgvItemStock.Visible = False
                dgvPurcRep.Visible = True
                dgvSalesRep.Visible = False
                pnlDate.Visible = False
                DateTimePicker2.Visible = False
                btnDgvItRefresh.Visible = False
                btnRefPurRe.Visible = True
                dgvPurSpec.Visible = False
                dgvSaleSpec.Visible = False
                BtnRefSaleRe.Visible = False
                btnretry.Visible = False
                lblTotalSal.Visible = False
                lblTotPur.Visible = True
                Label11.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Form1_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        EthnicLogin.Show()
    End Sub

    Private Sub rdbSpecDat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSpecDat.CheckedChanged
        Try
            If rdbSpecDat.Checked = True Then
                If RadioButton1.Checked = True Then
                    dgvItemStock.Visible = False
                    dgvPurcRep.Visible = False
                    pnlDate.Visible = True
                    dgvSalesRep.Visible = False
                    pnlDate.Visible = True
                    dgvSaleSpec.Visible = True
                    DateTimePicker2.Visible = True
                    btnDgvItRefresh.Visible = False
                    btnRefPurRe.Visible = False
                    dgvPurSpec.Visible = False
                    BtnRefSaleRe.Visible = False
                    Label11.Visible = False
                    lblTotalSal.Visible = False
                    lblTotPur.Visible = False

                ElseIf RadioButton2.Checked = True Then
                    dgvItemStock.Visible = False
                    dgvPurcRep.Visible = False

                    dgvSalesRep.Visible = False
                    pnlDate.Visible = True
                    dgvSaleSpec.Visible = False
                    DateTimePicker2.Visible = True
                    btnDgvItRefresh.Visible = False
                    btnRefPurRe.Visible = False
                    dgvPurSpec.Visible = True
                    BtnRefSaleRe.Visible = False
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbItems_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbItems.CheckedChanged
        Try
            If rdbItems.Checked = True Then
                dgvItemStock.Visible = True
                dgvPurcRep.Visible = False
                dgvSalesRep.Visible = False
                pnlDate.Visible = False
                DateTimePicker2.Visible = False
                btnDgvItRefresh.Visible = True
                btnRefPurRe.Visible = False
                dgvPurSpec.Visible = False
                dgvSaleSpec.Visible = False
                BtnRefSaleRe.Visible = False
                btnretry.Visible = False
                lblTotalSal.Visible = False
                lblTotPur.Visible = False
                Label11.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public dboName As String
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            Label11.Text = ""
            dgvSaleSpec.Rows.Clear()
            dgvSaleSpec.Visible = True
            dgvPurSpec.Visible = False

            Label18.Text = "Select Date"

            lblTotalSal.Text = "TOTAL SaLES : "
            lblTotPur.Visible = False
            Label11.Visible = True
           
        ElseIf RadioButton2.Checked = True Then
            Label11.Text = ""
            dgvPurSpec.Rows.Clear()
            dgvPurSpec.Visible = True
            dgvSaleSpec.Visible = False

            Label18.Text = "Select Date"

            lblTotalSal.Visible = False
            lblTotPur.Text = "TOTAL PURCHASES : "
       
        End If
    End Sub
    Private Sub cboBeers_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBeers.GotFocus
        If cboBeers.Text = "Select Beer" Then
            cboBeers.DroppedDown = True


        End If
    End Sub
    Private Sub cbowhiskeys_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWhiskeys.GotFocus
        If cboWhiskeys.Text = "Select Whiskey" Then
            cboWhiskeys.DroppedDown = True
        End If
    End Sub
    Private Sub cboWaiterNo_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If cboWaiterNo.Text = "Select Waiter #" Then
            cboWaiterNo.DroppedDown = True
        End If
    End Sub

    Private Sub cboSodWat_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSodaWater.GotFocus
        If cboSodaWater.Text = "Select Soft Drink" Then
            cboSodaWater.DroppedDown = True
        End If
    End Sub
    Private Sub txtCashPaid_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCashPaid.GotFocus
        txtCashPaid.Text = ""
    End Sub
    Private Sub cboBeers_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBeers.LostFocus
        If cboBeers.DroppedDown = True Or cboBeers.Text = "" Then
            cboBeers.DroppedDown = False
            cboBeers.Text = "Select Beer"

        End If
    End Sub
    Private Sub cboWhiskeys_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWhiskeys.LostFocus
        If cboWhiskeys.DroppedDown = True Or cboWhiskeys.Text = "" Then
            cboWhiskeys.DroppedDown = False
            cboWhiskeys.Text = "Select Whiskey"

        End If
    End Sub
    Private Sub cboWaiterNo_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If cboWaiterNo.DroppedDown = True Or cboWaiterNo.Text = "" Then
            cboWaiterNo.DroppedDown = False
            cboWaiterNo.Text = "Select Waiter #"

        End If
    End Sub
    Private Sub cboSodWat_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSodaWater.LostFocus
        If cboSodaWater.DroppedDown = True Or cboSodaWater.Text = "" Then
            cboSodaWater.DroppedDown = False
            cboSodaWater.Text = "Select Soft Drink"

        End If
    End Sub
    Private Sub txtCashPiad_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCashPaid.LostFocus
        If txtCashPaid.Text = "" Then
            txtCashPaid.Text = "Enter Cash Tendered"
        End If
    End Sub
    Public Function NumberToString(ByVal num_str As String) As String ', Optional ByVal use_us_group_names As Boolean = True
        ' Get the appropiate group names.
        Dim groups() As String
        'If use_us_group_names Then
        groups = New String() {"", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion", "Vigintillion"}
        'Else
        ' groups = New String() {"", "thousand", "million", "milliard", "billion", "1000 billion", "trillion", "1000 trillion", "quadrillion", "1000 quadrillion", "quintillion", "1000 quintillion", "sextillion", "1000 sextillion", "septillion", "1000 septillion", "octillion", "1000 octillion", "nonillion", "1000 nonillion", "decillion", "1000 decillion"}
        'End If

        ' Clean the string a bit.
        ' Remove "$", ",", leading zeros, and
        ' anything after a decimal point.
        Const CURRENCY As String = "/="
        Const SEPARATOR As String = ","
        Const DECIMAL_POINT As String = "."
        Const SPACE As String = " "
        num_str = num_str.Replace(CURRENCY, "").Replace(SEPARATOR, "").Replace(SPACE, "")  ' Eliminate a few xters.
        num_str = num_str.TrimStart(New Char() {"0"c})
        Dim pos As Integer = num_str.IndexOf(DECIMAL_POINT)
        If pos = 0 Then
            Return "Zero"
        ElseIf pos > 0 Then
            num_str = num_str.Substring(0, pos)
        End If

        ' See how many groups there will be.
        Dim num_groups As Integer = (num_str.Length + 2) \ 3

        ' Pad so length is a multiple of 3.
        num_str = num_str.PadLeft(num_groups * 3, " "c)

        ' Process the groups, largest first.
        Dim result As String = ""
        Dim group_num As Integer
        For group_num = num_groups - 1 To 0 Step -1
            ' Get the next three digits.
            Dim group_str As String = num_str.Substring(0, 3)

            num_str = num_str.Substring(3)
            Dim group_value As Integer = CInt(group_str)

            ' Convert the group into words.
            If group_value > 0 Then
                If group_num >= groups.Length Then
                    result &= GroupToWords(group_value) & _
                        " ?, "
                Else
                    result &= GroupToWords(group_value) & _
                        " " & groups(group_num) & ", "
                End If
            End If
        Next group_num

        ' Remove the trailing ", ".
        If result.EndsWith(", ") Then
            result = result.Substring(0, result.Length - 2)
        End If

        Return result.Trim()
    End Function
    ' Convert a number between 0 and 999 into words.
    Private Function GroupToWords(ByVal num As Integer) As String
        Static one_to_nineteen() As String = {"Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
        Static multiples_of_ten() As String = {"Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}

        ' If the number is 0, return an empty string.
        If num = 0 Then Return ""

        ' Handle the hundreds digit.
        Dim digit As Integer
        Dim result As String = ""
        If num > 99 Then
            digit = num \ 100
            num = num Mod 100
            result = one_to_nineteen(digit) & " Hundred"
        End If

        ' If num = 0, we have hundreds only.
        If num = 0 Then Return result.Trim()

        ' See if the rest is less than 20.
        If num < 20 Then
            ' Look up the correct name.
            result &= " " & one_to_nineteen(num)
        Else
            ' Handle the tens digit.
            digit = num \ 10
            num = num Mod 10
            result &= " " & multiples_of_ten(digit - 2)

            ' Handle the final digit.
            If num > 0 Then
                result &= " " & one_to_nineteen(num)
            End If
        End If

        Return result.Trim()
    End Function
    Dim STotal As Integer
    Public Function SumUpCost()
        Dim n As Integer = 0
        'Dim i As Integer = 0
        Dim total As Integer = 0
        While n < DataGridView1.Rows.Count
            total = total + DataGridView1.Rows(n).Cells(3).Value
            n = n + 1
        End While
        STotal = total

        lblSubTot.Text = total
        Dim damount2 As Decimal = CType(total, Decimal) 'say you have enteBlack 1400.10345
        Me.lblSubTot.Text = String.Format("{0:n2}", damount2)
        Return Nothing
    End Function

    Private Sub txtTotAmot_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotAmot.TextChanged
        Try
            Const DECIMAL_POINT As String = ("." & "##")
            Dim amount As String

            amount = txtTotAmot.Text.Replace(DECIMAL_POINT, "")

            txtTotAmtInWords.Text = NumberToString(amount) + " Shillings "
        Catch ex As Exception
            ' MsgBox(ex.Message, 0 + 48, "ERROR ! ! ")
            MsgBox("Invalid input", 0 + 48, "ERROR ! ! ")
            txtTotAmot.Focus()

        End Try
        Dim damount2 As Decimal = CType(txtTotAmot.Text, Decimal) 'say you have enteBlack 1400.10345
        Me.txtTotAmot.Text = String.Format("{0:n2}", damount2) 'the result after formating will look like 1,400.10
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Try
            Const DECIMAL_POINT As String = ("." & "##")
            Dim amount As String
            amount = lblAmotPaidIn.Text.Replace(DECIMAL_POINT, "")

            txtCasPaInWords.Text = NumberToString(amount) + " Shillings "
        Catch ex As Exception
            ' MsgBox(ex.Message, 0 + 48, "ERROR ! ! ")
            MsgBox("Invalid input", 0 + 48, "ERROR ! ! ")
            txtCashPaid.Focus()

        End Try
        'Dim damount2 As Decimal = CType(txtCashPaid.Text, Decimal) 'say you have enteBlack 1400.10345
        'Me.TextBox1.Text = String.Format("{0:n2}", damount2) 'the result after formating will look like 1,400.10

    End Sub

    Private Sub txtCashPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCashPaid.TextChanged
        Try
            If txtCashPaid.Text = "" Then
                lblAmotPaidIn.Text = "0.00"
                TextBox1.Text = lblAmotPaidIn.Text
                Exit Sub
            Else
                Dim damount2 As Decimal = CType(txtCashPaid.Text, Decimal) 'say you have enteBlack 1400.10345
                Me.lblAmotPaidIn.Text = String.Format("{0:n2}", damount2) 'the result after formating will look like 1,400.10
                TextBox1.Text = lblAmotPaidIn.Text
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub txtQtyBeers_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQtyBeers.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False

    End Sub
    Private Sub txtQtySodWat_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQtySodWat.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False

    End Sub
    Private Sub txtQtyWhiskry_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQtyWhiskry.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False
    End Sub
    Private Sub txtCashPaIn_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCashPaid.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False

    End Sub

    Private Sub btnAdCat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDgvItRefresh.Click
        Try

            dgvItemStock.Rows.Clear()

            Dim cmd3 As New SqlCommand("SELECT * FROM StockItems1", con)
            con.Open()

            Dim reader3 As SqlDataReader

            '*********************************************************************
            '****************************************************
            Dim i2 As Integer
            reader3 = cmd3.ExecuteReader
            While reader3.Read
                dgvItemStock.Rows.Add()
                dgvItemStock.Rows.Add()
                dgvItemStock.Rows(i2).Cells(0).Value = reader3.Item("ItemName")
                dgvItemStock.Rows(i2).Cells(1).Value = reader3.Item("Category")
                dgvItemStock.Rows(i2).Cells(2).Value = reader3.Item("CurrentStock")
                dgvItemStock.Rows(i2).Cells(3).Value = reader3.Item("BPricePerItem")
                dgvItemStock.Rows(i2).Cells(4).Value = reader3.Item("SPrice2")
                dgvItemStock.Rows(i2).Cells(5).Value = reader3.Item("SupplierName")
                i2 = i2 + 1
            End While
            Dim n As Integer = dgvItemStock.RowCount - 1
            Label11.Text = n.ToString & vbTab & "Item Categories in Stock"
        Catch ex As Exception
            MessageBox.Show("Connection to database failed" & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
        End Try
    End Sub

    Private Sub btnRefSpecRe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnRefPurRe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefPurRe.Click
        Try
            dgvPurcRep.Rows.Clear()

            Dim cdm6 As New SqlCommand("SELECT * FROM PurchasesNew", con)

            con.Open()

            Dim reader6 As SqlDataReader

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

        Catch ex As Exception
            MessageBox.Show("Connection to database failed" & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
        End Try
    End Sub

    Private Sub BtnRefSaleRe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefSaleRe.Click
        Try
            dgvSalesRep.Rows.Clear()

            Dim cmd5 As New SqlCommand("SELECT * FROM SalesNew", con)
            con.Open()

            Dim reader5 As SqlDataReader

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
        Catch ex As Exception
            MessageBox.Show("Connection to database failed" & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try

            DataGridView2.Rows.Clear()
            Dim cmd2 As New SqlCommand("SELECT * FROM StockItems1 WHERE Category = '" & "Beer" & "' OR Category =  '" & "Spirits & Whisky" & "' OR Category = '" & "Wines" & "'", con) ' retrieve alcoholic items

            con.Open()

            Dim reader2 As SqlDataReader



            '*********************************************************************
            reader2 = cmd2.ExecuteReader
            Dim i1 As Integer
            While reader2.Read
                DataGridView2.Rows.Add()
                DataGridView2.Rows(i1).Cells(0).Value = reader2.Item("ItemName")
                DataGridView2.Rows(i1).Cells(1).Value = reader2.Item("CurrentStock")
                DataGridView2.Rows(i1).Cells(2).Value = reader2.Item("SPrice2")
                Label16.Text = "Types Of Alcoholic Drinks : " & DataGridView2.RowCount - 1
                i1 = i1 + 1
            End While
            reader2.Close()
            '*********************************************************************

        Catch ex As Exception
            MessageBox.Show("Connection to database failed" & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
        End Try
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            DataGridView3.Rows.Clear()
            cboSodaWater.Items.Clear()
            Dim cat As String
            cat = "Soda"
            Dim cmd As New SqlCommand("SELECT * FROM StockItems1 WHERE Category = '" & cat.Trim & "'OR Category = '" & "Water" & "' OR Category = '" & "EnergyDrinks" & "' ", con) 'retrieve soft drinks

            con.Open()

            Dim reader As SqlDataReader

            reader = cmd.ExecuteReader
            Dim n As Integer = DataGridView3.RowCount
            Dim i As Integer
            While reader.Read
                DataGridView3.Rows.Add()
                DataGridView3.Rows(i).Cells(0).Value = reader.Item("ItemName")
                DataGridView3.Rows(i).Cells(1).Value = reader.Item("CurrentStock")
                DataGridView3.Rows(i).Cells(2).Value = reader.Item("SPrice2")
                i = i + 1
                Label15.Text = "Types Of Soft Drinks and Energizers : " & DataGridView3.RowCount - 1
                cboSodaWater.Items.Add(reader.Item("ItemName"))
            End While
            reader.Close()


        Catch ex As Exception
            MessageBox.Show("Connection to database failed" & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim dboDT As Date
            dboDT = DateTimePicker2.Value.Date
            Dim cmd As New SqlCommand
            Dim reader As SqlDataReader
            Dim i As Integer
            con.Open()

            If RadioButton1.Checked = True Then
                dgvSaleSpec.Rows.Clear()
                Dim respo = MessageBox.Show("Your search query is at for this date:" & vbNewLine & dboDT, strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If respo = DialogResult.OK Then
                    btnretry.Visible = True
                    pnlDate.Visible = False
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
                    Label11.Text = "Total Sales:" & X1 & "/= as at:" & dboDT
                End While


                Label11.Visible = True
                reader.Close()
            ElseIf RadioButton2.Checked = True Then

                dgvPurSpec.Rows.Clear()
                Dim respo = MessageBox.Show("Your search query is at for this date : " & vbNewLine & vbTab & dboDT, strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If respo = DialogResult.OK Then
                    btnretry.Visible = True
                    pnlDate.Visible = False
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
                    Label11.Text = "Total Purchases:" & X1 & "/= as at:" & dboDT
                    x = x + 1
                End While
                reader.Close()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, strTitle)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
        End Try


    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton1.Checked = True Then
            dgvSaleSpec.Rows.Clear()
            dgvSaleSpec.Visible = True
            dgvPurSpec.Visible = False

            Label18.Text = "Select Date"

        ElseIf RadioButton2.Checked = True Then
            dgvPurSpec.Rows.Clear()
            dgvPurSpec.Visible = True
            dgvSaleSpec.Visible = False

            Label18.Text = "Select Date"

            Label11.Visible = True
           

        End If
    End Sub
    Dim Price, i As Integer
    Private Sub cboBeers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBeers.SelectedIndexChanged
        Try

            '============================================================
            'Read form the products database and retrieve the Qty of the select item

            Dim cmdP As New SqlCommand("SELECT CurrentStock FROM StockItems1 WHERE ItemName = '" & cboBeers.SelectedItem & "'", con)

            'STOCK ITEMS TABLE
            Dim cmd As New SqlCommand("SELECT SPrice2, CurrentStock  FROM StockItems1 WHERE ItemName = '" & Me.cboBeers.SelectedItem & "'", con)
            Dim Reader As SqlDataReader
            con.Open()
            Reader = cmd.ExecuteReader

            While Reader.Read
                Price = Reader.Item("SPrice2")
                dbBeer = Reader.Item("CurrentStock")


            End While
            Reader.Close()


        Catch ex As Exception
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub cboWhiskeys_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWhiskeys.SelectedIndexChanged
        Try
            'STOCK ITEMS TABLE


            Dim cmd As New SqlCommand("SELECT SPrice2,CurrentStock FROM StockItems1 WHERE ItemName = '" & Me.cboWhiskeys.SelectedItem & "'", con)
            Dim Reader As SqlDataReader
            con.Open()
            Reader = cmd.ExecuteReader

            While Reader.Read
                Price = Reader.Item("SPrice2")
                dbWiSPi = Reader.Item("CurrentStock")
            End While
            Reader.Close()
        Catch ex As Exception
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

    End Sub

    Private Sub cboSodaWater_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSodaWater.SelectedIndexChanged
        Try
            'STOCK ITEMS TABLE


            Dim cmd As New SqlCommand("SELECT * FROM StockItems1 WHERE ItemName = '" & Me.cboSodaWater.SelectedItem & "'", con)
            Dim Reader As SqlDataReader
            con.Open()
            Reader = cmd.ExecuteReader

            While Reader.Read
                Price = Reader.Item("SPrice2")
                dbSodWat = Reader.Item("CurrentStock")

            End While

            Reader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

    End Sub

   

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        rdbSalRep.Checked = True
    End Sub

    Private Sub btnretry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnretry.Click
        pnlDate.Visible = True
    End Sub

    Private Sub btnSoWat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSoWat.Click
        Try

            If Not cboSodaWater.Items.Contains(cboSodaWater.Text) Then
                MessageBox.Show(cboSodaWater.Text & " is not apart of the stock Items", strTitle)
                cboSodaWater.DroppedDown = True
            Else
                Dim dT As DateTime = DateTimePicker3.Value.Date & " " & TimeOfDay
                Dim i As Integer = 0

                If cboSodaWater.Text = "Select Soft Drink" Then
                    Dim resp = MessageBox.Show("Please Select atleast a Drink ", strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                    If resp = Windows.Forms.DialogResult.OK Then
                        cboSodaWater.Focus()
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                ElseIf cboSodaWater.Text <> "Select Soft Drink" Then
                    If cboSodaWater.Text <> "Select Soft Drink" And txtQtySodWat.Text <> "" And txtQtySodWat.Text <> "0" Then
                        If Not cboWaiterNo.Items.Contains(cboWaiterNo.Text) Then
                            MessageBox.Show(cboWaiterNo.Text & " is not apart of the WAITERS / WAITRESSES", strTitle)
                            cboWaiterNo.DroppedDown = True
                            cboWaiterNo.Focus()
                        Else
                            Dim nb As Integer = DataGridView1.RowCount - 1

                            '**************************Do the neccessary imcrements
                            While i < nb
                                If DataGridView1.Rows(i).Cells(0).Value = cboSodaWater.SelectedItem Then
                                    DataGridView1.Rows(i).Cells(1).Value = CInt(DataGridView1.Rows(i).Cells(1).Value) + CInt(txtQtySodWat.Text)
                                    DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(1).Value * DataGridView1.Rows(i).Cells(2).Value
                                    SumUpCost()
                                    Exit Sub
                                End If
                                i = i + 1
                            End While
                            DataGridView1.Rows.Add()
                            DataGridView1.Rows(nb).Cells(0).Value = cboSodaWater.SelectedItem
                            DataGridView1.Rows(nb).Cells(1).Value = txtQtySodWat.Text
                            DataGridView1.Rows(nb).Cells(2).Value = Price
                            DataGridView1.Rows(nb).Cells(5).Value = dT
                            DataGridView1.Rows(nb).Cells(4).Value = cboWaiterNo.SelectedItem
                            DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(1).Value * DataGridView1.Rows(i).Cells(2).Value
                            SumUpCost()
                            txtQtySodWat.Clear()
                            cboSodaWater.Text = "Select Soft Drink"
                            cboWaiterNo.Text = "Select Waiter #"
                        End If
                    Else
                        Dim resps = MessageBox.Show("Please enter qauntity of " & cboSodaWater.Text, strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                        If resps = DialogResult.OK Then
                            txtQtySodWat.Focus()
                            Exit Sub
                        ElseIf resps = DialogResult.Cancel Then
                            Exit Sub
                        End If
                    End If



                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnWish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWish.Click
        '****************************************************
        'create a function to add items to the dtagrid view
        '============================================================
        Try

            If Not cboWhiskeys.Items.Contains(cboWhiskeys.Text) Then
                MessageBox.Show(cboWhiskeys.Text & " is not apart of the stock Items", strTitle)
                cboWhiskeys.DroppedDown = True
            Else
                Dim dT As Date = DateTimePicker3.Value.Date & " " & TimeOfDay

                Dim i As Integer = 0

                If cboWhiskeys.Text = "Select Whiskey" Then
                    Dim resp = MessageBox.Show("Please Select atleast a Drink", strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                    If resp = Windows.Forms.DialogResult.OK Then
                        cboWhiskeys.Focus()

                        Exit Sub
                    Else
                        Exit Sub
                    End If
                ElseIf cboWhiskeys.Text <> "Select Whiskey" Then
                    If cboWhiskeys.Text <> "Select Whiskey" And txtQtyWhiskry.Text <> "" And txtQtyWhiskry.Text <> "0" Then
                        If Not cboWaiterNo.Items.Contains(cboWaiterNo.Text) Then
                            MessageBox.Show(cboWaiterNo.Text & " is not apart of the WAITERS", strTitle)
                            cboWaiterNo.DroppedDown = True
                            cboWaiterNo.Focus()
                        Else
                            Dim nb As Integer = DataGridView1.RowCount - 1

                            '**************************Do the neccessary imcrements
                            While i < nb
                                If DataGridView1.Rows(i).Cells(0).Value = cboWhiskeys.SelectedItem Then
                                    DataGridView1.Rows(i).Cells(1).Value = CInt(DataGridView1.Rows(i).Cells(1).Value) + CInt(txtQtyWhiskry.Text)
                                    DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(1).Value * DataGridView1.Rows(i).Cells(2).Value
                                    SumUpCost()
                                    Exit Sub

                                End If
                                i = i + 1
                            End While
                            DataGridView1.Rows.Add()
                            DataGridView1.Rows(nb).Cells(0).Value = cboWhiskeys.SelectedItem
                            DataGridView1.Rows(nb).Cells(1).Value = txtQtyWhiskry.Text
                            DataGridView1.Rows(nb).Cells(2).Value = Price
                            DataGridView1.Rows(nb).Cells(5).Value = dT
                            DataGridView1.Rows(nb).Cells(4).Value = cboWaiterNo.SelectedItem
                            DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(1).Value * DataGridView1.Rows(i).Cells(2).Value
                            SumUpCost()
                            txtQtyWhiskry.Clear()
                            cboWhiskeys.Text = " Select Whiskey"

                            cboWaiterNo.Text = "Select Waiter #"
                        End If
                    Else
                        Dim resps = MessageBox.Show("Please enter qauntity of " & cboWhiskeys.Text, strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                        If resps = DialogResult.OK Then
                            txtQtyWhiskry.Focus()
                            Exit Sub
                        ElseIf resps = DialogResult.Cancel Then
                            Exit Sub
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBeer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBeer.Click
        '****************************************************
        'create a function to add items to the dtagrid view
        '============================================================

        Try
            If Not cboBeers.Items.Contains(cboBeers.Text) Then
                MessageBox.Show(cboBeers.Text & " is not apart of the stock Items", strTitle)
                cboBeers.DroppedDown = True
            Else

                Dim dT As Date = DateTimePicker3.Value.Date & " " & TimeOfDay

                Dim i As Integer = 0

                If cboBeers.Text = "Select Beer" Then
                    Dim resp = MessageBox.Show("Please Select atleast a Drink", strTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                    If resp = Windows.Forms.DialogResult.OK Then
                        cboBeers.Focus()
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                ElseIf cboBeers.Text <> "Select Beer" Then
                    If cboBeers.Text <> "Select Beer" And txtQtyBeers.Text <> "" And txtQtyBeers.Text <> "0" Then
                        If Not cboWaiterNo.Items.Contains(cboWaiterNo.Text) Then
                            MessageBox.Show(cboWaiterNo.Text & " is not apart of the WAITERS", strTitle)
                            cboWaiterNo.DroppedDown = True
                            cboWaiterNo.Focus()

                        Else
                            Dim nb As Integer = DataGridView1.RowCount - 1

                            '**************************Do the neccessary imcrements
                            While i < nb
                                If DataGridView1.Rows(i).Cells(0).Value = cboBeers.SelectedItem Then
                                    DataGridView1.Rows(i).Cells(1).Value = CInt(DataGridView1.Rows(i).Cells(1).Value) + CInt(txtQtyBeers.Text)
                                    DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(1).Value * DataGridView1.Rows(i).Cells(2).Value
                                    SumUpCost()
                                    Exit Sub
                                End If
                                i = i + 1
                            End While
                            DataGridView1.Rows.Add()
                            DataGridView1.Rows(nb).Cells(0).Value = cboBeers.SelectedItem
                            DataGridView1.Rows(nb).Cells(1).Value = txtQtyBeers.Text
                            DataGridView1.Rows(nb).Cells(2).Value = Price
                            DataGridView1.Rows(nb).Cells(5).Value = dT
                            DataGridView1.Rows(nb).Cells(4).Value = cboWaiterNo.SelectedItem
                            DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(1).Value * DataGridView1.Rows(i).Cells(2).Value
                            SumUpCost()
                            txtQtyBeers.Clear()
                            cboBeers.Text = "Select Beer"
                            cboWaiterNo.Text = "Select Waiter #"
                        End If
                    Else
                        Dim resp = MessageBox.Show("Please enter qauntity of " & cboBeers.Text, strTitle)
                        If resp = DialogResult.OK Then
                            txtQtyBeers.Focus()
                            Exit Sub
                        End If

                    End If
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtQtyBeers_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQtyBeers.TextChanged
        Try

            If txtQtyBeers.Text = "" Then
                Exit Sub
            Else
                If Not cboBeers.Items.Contains(cboBeers.Text) Then
                    txtQtyBeers.Text = ""
                    Dim resp = MessageBox.Show("Please First select the Drink", strTitle)
                    If resp = DialogResult.OK Then

                        cboBeers.Focus()
                        cboBeers.DroppedDown = True

                        Exit Sub
                    End If
                Else
                    If CInt(txtQtyBeers.Text) > dbBeer Then
                        txtQtyBeers.Focus()
                        Dim dif As Integer = Val(CInt(txtQtyBeers.Text) - dbBeer)
                        Dialog1.Text = strTitle
                        Dialog1.lblMsg.Text = cboBeers.SelectedItem & " is less by : " & dif
                        Dialog1.Show()
                        txtQtyBeers.Clear()
                    Else
                        AcceptButton = btnBeer

                    End If
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtQtyWhiskry_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQtyWhiskry.TextChanged
        Try
            If txtQtyWhiskry.Text = "" Then
                Exit Sub

            Else
                If Not cboWhiskeys.Items.Contains(cboWhiskeys.Text) Then
                    Dim respo = MessageBox.Show("Please First select the Drink", strTitle)
                    If respo = DialogResult.OK Then
                        txtQtyWhiskry.Text = ""
                        cboWhiskeys.Focus()
                        cboWhiskeys.DroppedDown = True
                        Exit Sub
                    End If
                Else
                    If CInt(txtQtyWhiskry.Text) > dbWiSPi Then
                        Dim dif As Integer = CInt(txtQtyWhiskry.Text) - dbWiSPi
                        txtQtyWhiskry.Clear()
                        Dialog1.Show()
                        Dialog1.Text = strTitle
                        Dialog1.lblMsg.Text = cboWhiskeys.SelectedItem & " is less by : " & dif
                        txtQtyWhiskry.Focus()

                    Else
                        AcceptButton = btnWish
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub txtQtySodWat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQtySodWat.TextChanged
        Try
            If txtQtySodWat.Text = "" Then
                Exit Sub
            Else
                If Not cboSodaWater.Items.Contains(cboSodaWater.Text) Then
                    MessageBox.Show("Please First select the Drink", strTitle)
                    cboSodaWater.Focus()
                    cboSodaWater.DroppedDown = True
                    txtQtySodWat.Clear()

                    Exit Sub
                Else
                    If CInt(txtQtySodWat.Text) > dbSodWat Then
                        Dim dif As Integer = CInt(txtQtySodWat.Text) - dbSodWat
                        txtQtySodWat.Clear()
                        Dialog1.Show()
                        Dialog1.Text = strTitle
                        Dialog1.lblMsg.Text = cboSodaWater.SelectedItem & " is less by : " & dif
                        txtQtySodWat.Focus()

                        Exit Sub
                    Else
                        AcceptButton = btnSoWat
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCompute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompute.Click
        Try
            Dim rowcount As Integer = DataGridView1.RowCount - 1

            If rowcount = 0 Then
                MessageBox.Show("Please Input Items to Sale", strTitle)
                cboBeers.Focus()
                Exit Sub
            Else

                If lblAmotPaidIn.Text = "Amount Paid In" Or lblAmotPaidIn.Text = "0.00" Then
                    Dim respo = MessageBox.Show("Please Input tendered Cash", strTitle)
                    If respo = DialogResult.OK Then
                        txtCashPaid.Focus()
                        txtCashPaid.Clear()
                        Exit Sub
                    End If

                End If
                Dim cashPaid, cashSpent As Integer
                cashPaid = CInt(txtCashPaid.Text)
                cashSpent = STotal
                If cashSpent > cashPaid Then
                    btnPrint.Enabled = False
                    Dim defeciet As Double = cashSpent - cashPaid

                    Dim damount2 As Decimal = CType(defeciet, Decimal) 'say you have enteBlack 1400.10345
                    Dim def As String = String.Format("{0:n2}", damount2) 'the result after formating will look like 1,400.10

                    Dialog1.Show()
                    Dialog1.lblMsg.Text = "Money tendered" & vbNewLine & "is less by: " & def & " UgShs"
                    'txtCashPaid.Focus()
                    Exit Sub

                Else
                    btnPrint.Enabled = True
                    Dim bal As Double = cashPaid - cashSpent
                    Dim bal2 As Decimal = CType(bal, Decimal)

                    lblCashChge.Text = String.Format("{0:n2}", bal2)


                    Dialog1.lblMsg.Text = "   CHANGE   :    " & lblCashChge.Text & "  UgShs" & vbNewLine + vbTab & " Save and Print"

                    CPaIn = lblAmotPaidIn.Text
                    CChange = lblCashChge.Text
                    If Dialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                        btnPrint.PerformClick()
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try


    End Sub

    Private Sub Splitter1_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles Splitter1.SplitterMoved

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try

            Dim rowcount As Integer = DataGridView1.RowCount

            If (rowcount = 1) Then
                MessageBox.Show("You can not attempt to make sales when Item List is Empty", strTitle, MessageBoxButtons.OK)
                cboBeers.Focus()
                Exit Sub
            Else

               
                '=================================================================================================
                'Connec to the databse and make the neccesary updates
                'update the sales table 
                Dim cmdx, cmdp, cmdup, cmdTraNo, cmdTUpdate As New SqlCommand
                Dim reader, readerTraNo As SqlDataReader
                Dim itemName, authoBY, waiterNo As String
                Dim Qty, UnitPrice, TotalAmout, dboCurr, dboNewSto As Integer
                Dim dt As Date
                con.Open()
                Dim TSalNO
                cmdTraNo.CommandText = "SELECT * FROM TransNoSales"
                cmdTraNo.Connection = con
                readerTraNo = cmdTraNo.ExecuteReader
                'Dim dbTrasNO As Integer
                While readerTraNo.Read
                    TSalNO = readerTraNo.Item("TransNo") + 1
                    SalTr = TSalNO
                End While
                readerTraNo.Close()
                Dim i As Integer = 0
                Dim n As Integer = DataGridView1.RowCount - 2
                While i <= n
                    itemName = DataGridView1.Rows(i).Cells(0).Value
                    Qty = DataGridView1.Rows(i).Cells(1).Value
                    UnitPrice = DataGridView1.Rows(i).Cells(2).Value
                    TotalAmout = DataGridView1.Rows(i).Cells(3).Value
                    dt = DataGridView1.Rows(i).Cells(5).Value
                    waiterNo = DataGridView1.Rows(i).Cells(4).Value
                    authoBY = EthnicLogin.login
                    cmdx.CommandText = "INSERT INTO SalesNew VALUES('" & dt & "','" & itemName & "','" & Qty & "', '" & UnitPrice & "', '" & TotalAmout & "', '" & authoBY & "', '" & TSalNO & "', '" & waiterNo & "','" & 0 & "')"
                    cmdx.Connection = con
                    cmdx.ExecuteNonQuery()
                    i = i + 1
                   

                End While
                i = 0
                '===============================================================================
                'read from PRODUCTS DATABASE and check
                While i <= n
                    itemName = DataGridView1.Rows(i).Cells(0).Value.ToString.Trim
                    Qty = DataGridView1.Rows(i).Cells(1).Value

                    cmdp.CommandText = "SELECT CurrentStock FROM StockItems1 WHERE ItemName = '" & itemName & " '"
                    cmdp.Connection = con
                    reader = cmdp.ExecuteReader
                    If reader.Read Then
                        dboCurr = reader.Item("CurrentStock")
                    End If
                    reader.Close()

                    dboNewSto = dboCurr - Qty

                    cmdup.CommandText = "UPDATE StockItems1 SET CurrentStock = '" & dboNewSto & "' WHERE ItemName = '" & itemName.Trim & "' "
                    cmdup.Connection = con
                    cmdup.ExecuteNonQuery()
                    i = i + 1
                End While
                cmdTUpdate.CommandText = ("INSERT INTO TransNoSales VALUES('" & TSalNO & "', '" & DataGridView1.Rows(1).Cells(4).Value & "', '" & dt & "', '" & EthnicLogin.login & "')")
                cmdTUpdate.Connection = con
                cmdTUpdate.ExecuteNonQuery()
                PrintDocument1.Print()
                'PrintPreviewDialog1.Document = PrintDocument1

                'PrintPreviewDialog1.ShowDialog()
                '
            End If
           
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

    End Sub

    Private Sub Panel6_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub txtQtyPur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnRecItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecItem.Click

        Try
            Dim rwCount As Integer = DataGridView1.RowCount - 1
            If rwCount = 0 Then
            Else
                Dim index As Integer = DataGridView1.CurrentCell.RowIndex

                DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(index).Cells(1).Value * DataGridView1.Rows(i).Cells(2).Value

                SumUpCost()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Panel13_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel13.Paint

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub

    Private Sub FoodAndSnacksToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MUpdate.Click


    End Sub

    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub lblWhiskeys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblWhiskeys.Click

    End Sub

    Private Sub lblBeers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBeers.Click

    End Sub

    Private Sub lblSodWat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblSodWat.Click

    End Sub

    Private Sub cboWaiterNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        con.Open()
        Dim cmdw As New SqlCommand("SELECT * FROM Employees WHERE Role ='" & cboWaiterNo.SelectedItem & "'", con)
        Dim readerw As SqlDataReader
        readerw = cmdw.ExecuteReader
        If readerw.HasRows Then
        Else
            Exit Sub
        End If
        ' Dim waiterNo As Integer
        ' Dim waiterNem As String
        ' While readerw.HasRows
        'waiterNo = readerw.Item("EmpIdNo")
        'cboWaiterNo.Items.Add(waiterNo)
        ' End While
    End Sub

    Private Sub lblTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTime.Click

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim rwCount As Integer = DataGridView1.RowCount - 1
            If rwCount = 0 Then
            Else
                Dim index As Integer = DataGridView1.CurrentCell.RowIndex
                DataGridView1.Rows.RemoveAt(index)
                SumUpCost()
            End If
            
        Catch ex As Exception
            MessageBox.Show(ex.Message, strTitle)
        End Try
    End Sub

    Private Sub SoftDrinksToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftDrinksToolStripMenuItem1.Click

        Damaged.Panel13.Enabled = False
        Damaged.Panel14.Enabled = False
        Damaged.btnBeer.Enabled = False
        Damaged.btnWish.Enabled = False
        Damaged.Panel15.Enabled = True
        Damaged.btnSoWat.Enabled = True
        Damaged.cboSodaWater.Focus()
        Damaged.Show()
    End Sub

    Private Sub SpiritsAndWinesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpiritsAndWinesToolStripMenuItem.Click
        Damaged.Panel13.Enabled = False
        Damaged.Panel15.Enabled = False
        Damaged.btnBeer.Enabled = False
        Damaged.btnSoWat.Enabled = False
        Damaged.Panel14.Enabled = True
        Damaged.btnWish.Enabled = True
        Damaged.cboWhiskeys.Focus()
        Damaged.Show()
    End Sub

    Private Sub BeersToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeersToolStripMenuItem1.Click
        Damaged.Panel15.Enabled = False
        Damaged.Panel13.Enabled = True
        Damaged.Panel14.Enabled = False
        Damaged.btnWish.Enabled = False
        Damaged.btnSoWat.Enabled = False
        Damaged.btnBeer.Enabled = True
        Damaged.cboBeers.Focus()
        Damaged.Show()
    End Sub

    Private Sub FoodAndSnacksToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FoodAndSnacksToolStripMenuItem1.Click
        Food_Snacks.Show()
    End Sub

    Private Sub ChangeSalesPriceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SalesPrice.Show()
    End Sub

  
    Private Sub Panel14_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel14.Paint

    End Sub

    Private Sub StockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockToolStripMenuItem.Click
        UpDateStock.Show()
    End Sub

    Private Sub SetPricesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetPricesToolStripMenuItem.Click
        SalesPrice.Show()
    End Sub

    Private Sub dgvItemStock_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemStock.CellContentClick

    End Sub

   

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
              Dim Lmargin, Rmargin, Tmargin, Bmargin As Integer
        With PrintDocument1.DefaultPageSettings.Margins
            Lmargin = .Left
            Rmargin = .Right
            Tmargin = .Top
            Bmargin = .Bottom
        End With
        Dim printWidth, printHieght As Integer

        With PrintDocument1.DefaultPageSettings.PaperSize
            printWidth = .Width - Lmargin - Rmargin
            printHieght = .Height - Tmargin - Bmargin

        End With
        Dim r As Rectangle
        r = New Rectangle(Lmargin, Tmargin, printWidth, printHieght)
        e.Graphics.DrawRectangle(Pens.Red, r)
        Dim pfont As Font
        pfont = New Font("Arial", 7)
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        e.Graphics.DrawString("ETHNIC -- BAR AND LOUNGE", pfont, Brushes.Black, 360, 435)
        Dim rec2 As New Rectangle(300, 455, 270, 11)
        e.Graphics.DrawRectangle(Pens.Black, rec2)
        e.Graphics.DrawString("TAX RECEIPT / INVOICE", pfont, Brushes.Black, 370, 455)
        Dim rec As New Rectangle(285, 410, 300, 310)
        e.Graphics.DrawRectangle(Pens.SlateBlue, rec)

        Dim dt As DateTime = DateTimePicker2.Value.Date
        Dim Time As DateTime = TimeOfDay
        '===============================================================================
        '===============================================================================
        e.Graphics.DrawString("Transaction #:", pfont, Brushes.Black, 310, 475)
        e.Graphics.DrawString(SalTr, pfont, Brushes.Black, 420, 475)
        e.Graphics.DrawString("Date:", pfont, Brushes.Black, 310, 485)
        e.Graphics.DrawString(dt, pfont, Brushes.Black, 340, 485)
        e.Graphics.DrawString("Cashier: ", pfont, Brushes.Black, 310, 495)
        e.Graphics.DrawString(EthnicLogin.EmpID, pfont, Brushes.Black, 350, 495)
        e.Graphics.DrawString("Time", pfont, Brushes.Black, 420, 485)
        e.Graphics.DrawString(Time, pfont, Brushes.Black, 450, 485)
        '*****************************************************************************
        '============================================================================
        e.Graphics.DrawString("Item Name", pfont, Brushes.Black, 310, 510)
        e.Graphics.DrawString("Quantity", pfont, Brushes.Black, 400, 510)
        e.Graphics.DrawString("Ammount", pfont, Brushes.Black, 530, 510)
        '===========================================================================

        e.Graphics.DrawString("===========", pfont, Brushes.Black, 310, 520)
        e.Graphics.DrawString("================", pfont, Brushes.Black, 400, 520)
        e.Graphics.DrawString("===========", pfont, Brushes.Black, 510, 520)
        '============================================================================
        ''print out from the datagrid to be place here/ items 
        '
        Dim rowcount As Integer = DataGridView1.Rows.Count - 1

        Dim i As Integer = 530
        Dim n As Integer = 0
        Dim tot As String = lblSubTot.Text
        While n < rowcount
            e.Graphics.DrawString(DataGridView1.Rows(n).Cells(0).Value, pfont, Brushes.Black, 310, i)
            e.Graphics.DrawString(DataGridView1.Rows(n).Cells(1).Value, pfont, Brushes.Black, 420, i)
            e.Graphics.DrawString(DataGridView1.Rows(n).Cells(3).Value, pfont, Brushes.Black, 530, i)
            n = n + 1
            i = i + 10
        End While
        '===============================================================================
        e.Graphics.DrawString("=============", pfont, Brushes.Black, 490, i)
        e.Graphics.DrawString("Total", pfont, Brushes.Black, 490, i + 7)
        e.Graphics.DrawString(lblSubTot.Text, pfont, Brushes.Black, 520, i + 7)
        e.Graphics.DrawString("CASH Tendered ", pfont, Brushes.Black, 440, i + 25)
        e.Graphics.DrawString(CPaIn, pfont, Brushes.Black, 520, i + 25)
        e.Graphics.DrawString("Change CASH ", pfont, Brushes.Black, 450, i + 35)
        e.Graphics.DrawString(CChange, pfont, Brushes.Black, 520, i + 35)
        e.Graphics.DrawString("VAT Total :", pfont, Brushes.Black, 420, i + 53)
        e.Graphics.DrawString(18 & " %" + "UgShs : " & 510, pfont, Brushes.Black, 470, i + 53)


        e.Graphics.DrawString("Thank you For coming at :ETHNIC -- BAR AND LOUNGE ", pfont, Brushes.Black, 310, i + 68)
        e.Graphics.DrawString("You were served By : " & WName, pfont, Brushes.Black, 350, i + 78)
        '***********************************************************
        cboBeers.Text = "Select Beer"                             '*
        cboWhiskeys.Text = "Select Whiskey"                       '*
        cboSodaWater.Text = "Select Soft Drinks"                  '*
        txtQtySodWat.Clear()                                      '*
        txtQtyBeers.Clear()                                       '*
        txtQtyWhiskry.Clear()                                     '*
        txtCashPaid.Clear()                                       '*
        txtCashPiad_LostFocus(sender, e)                          '*
        txtTotAmot.Text = "0.00"                                  '*
        lblSubTot.Text = ""                                       '*
        lblCashChge.Text = "Change Refunded"                      '*
        DataGridView1.Rows.Clear()                                '*
    End Sub

    Private Sub PrintPreviewDialog1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPreviewDialog1.Load

    End Sub
    Dim WName As String
    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub cboWaiterNo_SelectedIndexChanged_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWaiterNo.SelectedIndexChanged
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Employees WHERE EmpIdNo = '" & cboWaiterNo.SelectedItem & "'", con)
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader
            While reader.Read
                WName = reader.Item("FirstName") & " " & reader.Item("LastName")
            End While
            'MessageBox.Show(WName)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub btnForward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForward.Click
        Try
            Dim x As Integer
            While x < 10000
                Me.Size = New Size(1365, 775)
                x = x + 1
            End While
            btnForward.Visible = False
            btnHide.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHide.Click
        Try
            Dim x As Integer
            While x < 10000
                x = x + 1
                Me.Size = New Size(933, 766)
            End While
            btnForward.Visible = True
            btnHide.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub EmployeesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmployeesToolStripMenuItem.Click
        Employees.Show()
    End Sub

    Private Sub ChangeLoginToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeLoginToolStripMenuItem.Click
        changeLogin.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub SalesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesToolStripMenuItem.Click
        Reports.Show()
    End Sub

    Private Sub MenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub ViewSystemUsersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewSystemUsersToolStripMenuItem.Click
        Users.Show()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.Show()
    End Sub

    Private Sub SetPrinterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetPrinterToolStripMenuItem.Click
        PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
        End If
    End Sub

    Private Sub REFRESHToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles REFRESHToolStripMenuItem.Click

        Try
            dgvPurcRep.Rows.Clear()
            dgvSaleSpec.Rows.Clear()
            dgvPurSpec.Rows.Clear()
            dgvSalesRep.Rows.Clear()
            cboBeers.Items.Clear()
            cboWaiterNo.Items.Clear()
            cboSodaWater.Items.Clear()
            cboWhiskeys.Items.Clear()
            DataGridView2.Rows.Clear()
            DataGridView3.Rows.Clear()
            dgvItemStock.Rows.Clear()
            reload()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

    End Sub
End Class
