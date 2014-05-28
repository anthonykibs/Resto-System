Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.SqlServer
Imports System.Drawing.Drawing2D
Public Class Food_Snacks
    Public Price, Stotal As Integer
    Public wname As String
    Dim CPaIn As String
    Dim SalTr As Integer
    Dim CChange As String
    Dim strTitle As String = "ETHNIC -- Bar and Lounge"
    Dim con As New SqlConnection("Server=localhost; database=RestuarantBar; uid=sa; password=pass;")
    Private Sub SplitContainer1_Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWaiterNo.SelectedIndexChanged
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Employees WHERE EmpIdNo = '" & cboWaiterNo.SelectedItem & "'", con)
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader
            While reader.Read
                wname = reader.Item("FirstName") & " " & reader.Item("LastName")
            End While
            AcceptButton = btnEnter
            'MessageBox.Show(WName)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label1.Text = TimeOfDay
    End Sub

    Private Sub Food_Snacks_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Start()
        reload()

       
    End Sub
    Private Sub reload()
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Menu", con)
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader
            While reader.Read
                cboBeers.Items.Add(reader.Item("MenuItem").ToString)
            End While
            reader.Close()

            Dim cmdx As New SqlCommand("SELECT * FROM Employees WHERE Role = '" & "Waitress" & "' OR Role = '" & "Waiter" & "'", con)
            Dim readerx As SqlDataReader

            readerx = cmdx.ExecuteReader
            While readerx.Read
                cboWaiterNo.Items.Add(readerx.Item("EmpIdNo").ToString)

            End While
            readerx.Close()

            Dim cmdF As New SqlCommand("SELECT * FROM Menu", con)
            Dim readerf As SqlDataReader
            readerf = cmdF.ExecuteReader
            Dim fi As Integer
            While readerf.Read
                dgvMenuItems.Rows.Add()
                dgvMenuItems.Rows(fi).Cells(0).Value = readerf.Item(0)
                dgvMenuItems.Rows(fi).Cells(1).Value = readerf.Item(1)
                fi = fi + 1

            End While
            readerf.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub cboBeers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBeers.SelectedIndexChanged
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Menu WHERE MenuItem = '" & cboBeers.SelectedItem & "'", con)
            Dim reader As SqlDataReader

            reader = cmd.ExecuteReader
            While reader.Read
                Price = reader.Item(1)
            End While
            reader.Close()
            AcceptButton = btnEnter
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

    End Sub

    Private Sub dgvSaleSpec_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub btnBeer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnter.Click
        Try
            Dim dt As DateTime
            dt = DateTimePicker1.Value.Date & " " & TimeOfDay
            If Not cboBeers.Items.Contains(cboBeers.Text) Then
                MessageBox.Show(cboBeers.Text & " is not on a menu list", strTitle)
                cboBeers.DroppedDown = True
                Exit Sub
            Else
                If cboBeers.Text = "Select Menu Item" Then
                    MessageBox.Show("Please select atleast one menu item", strTitle)
                    cboBeers.Focus()
                    cboBeers.DroppedDown = True
                Else
                    If txtQtyBeers.Text = "" Or txtQtyBeers.Text = "0" Then
                        MessageBox.Show("Please select Number of Plates", strTitle)
                        txtQtyBeers.Focus()
                    Else
                        If Not cboWaiterNo.Items.Contains(cboWaiterNo.Text) Then
                            MessageBox.Show(cboWaiterNo.Text & "  is not a waiter", strTitle)
                            cboWaiterNo.DroppedDown = True
                            cboWaiterNo.Focus()
                        Else
                            Dim nb As Integer = DataGridView1.RowCount - 1
                            Dim i As Integer
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
                            DataGridView1.Rows(nb).Cells(1).Value = Price
                            DataGridView1.Rows(nb).Cells(2).Value = txtQtyBeers.Text
                            DataGridView1.Rows(nb).Cells(4).Value = dt
                            DataGridView1.Rows(nb).Cells(5).Value = cboWaiterNo.SelectedItem
                            DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(1).Value * DataGridView1.Rows(i).Cells(2).Value
                            SumUpCost()
                            txtQtyBeers.Clear()
                            cboBeers.Text = "Select Beer"
                            cboWaiterNo.Text = "Select Waiter #"
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function SumUpCost()
        Dim n As Integer = 0
        'Dim i As Integer = 0
        Dim total As Integer = 0
        While n < DataGridView1.Rows.Count
            total = total + DataGridView1.Rows(n).Cells(3).Value
            n = n + 1
        End While
        Stotal = total

        lblSubTot.Text = total
        Dim damount2 As Decimal = CType(total, Decimal) 'say you have enteBlack 1400.10345
        Me.lblSubTot.Text = String.Format("{0:n2}", damount2)
        Return Nothing
    End Function

    Private Sub btnRecItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecItem.Click
        Try
            Dim i As Integer
            Dim rwCount As Integer = DataGridView1.RowCount - 1
            If rwCount = 0 Then
            Else
                Dim index As Integer = DataGridView1.CurrentCell.RowIndex

                DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(index).Cells(1).Value * DataGridView1.Rows(i).Cells(2).Value
                SumUpCost()
                SumUpCost()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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

    Private Sub txtQtyBeers_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQtyBeers.TextChanged
        AcceptButton = btnEnter

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
                cashSpent = Stotal
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
    Private Sub txtCashPaid_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCashPaid.GotFocus
        txtCashPaid.Text = ""
    End Sub
    Private Sub txtCashPiad_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCashPaid.LostFocus
        If txtCashPaid.Text = "" Then
            txtCashPaid.Text = "Enter Cash Tendered"
        End If
    End Sub
    Private Sub cboBeers_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBeers.LostFocus
        If cboBeers.DroppedDown = True Or cboBeers.Text = "" Then
            cboBeers.DroppedDown = False
            cboBeers.Text = "Select Food Type"

        End If
    End Sub
    Private Sub cboWaiterNo_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If cboWaiterNo.Text = "Select Waiter #" Then
            cboWaiterNo.DroppedDown = True
        End If
    End Sub
    Private Sub cboWaiterNo_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If cboWaiterNo.DroppedDown = True Or cboWaiterNo.Text = "" Then
            cboWaiterNo.DroppedDown = False
            cboWaiterNo.Text = "Select Waiter #"

        End If
    End Sub
    Private Sub cboBeers_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBeers.GotFocus
        If cboBeers.Text = "Select Food Type" Then
            cboBeers.DroppedDown = True


        End If
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
                Dim Qty, UnitPrice, TotalAmout As Integer
                Dim dt As Date
                con.Open()
                Dim TSalNO
                cmdTraNo.CommandText = "SELECT * FROM TransNoFood"
                cmdTraNo.Connection = con
                readerTraNo = cmdTraNo.ExecuteReader
                'Dim dbTrasNO As Integer
                While readerTraNo.Read
                    TSalNO = readerTraNo.Item("FoodTransNo") + 1
                    SalTr = TSalNO
                End While
                readerTraNo.Close()
                Dim i As Integer = 0
                Dim n As Integer = DataGridView1.RowCount - 2
                While i <= n
                    itemName = DataGridView1.Rows(i).Cells(0).Value
                    Qty = DataGridView1.Rows(i).Cells(2).Value
                    UnitPrice = DataGridView1.Rows(i).Cells(1).Value
                    TotalAmout = DataGridView1.Rows(i).Cells(3).Value
                    dt = DataGridView1.Rows(i).Cells(4).Value
                    waiterNo = DataGridView1.Rows(i).Cells(5).Value
                    authoBY = EthnicLogin.login
                    cmdx.CommandText = "INSERT INTO Food VALUES('" & itemName & "','" & UnitPrice & "','" & Qty & "', '" & dt & "', '" & waiterNo & "', '" & TSalNO & "', '" & TotalAmout & "', '" & authoBY & "')"
                    cmdx.Connection = con
                    cmdx.ExecuteNonQuery()
                    i = i + 1


                End While
                i = 0
                '===============================================================================
               
                cmdTUpdate.CommandText = ("INSERT INTO TransNoFood VALUES('" & TSalNO & "', '" & DataGridView1.Rows(1).Cells(5).Value & "', '" & dt & "', '" & EthnicLogin.login & "')")
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

        Dim dt As DateTime = DateTimePicker1.Value.Date
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
        e.Graphics.DrawString("Number Of Plates", pfont, Brushes.Black, 400, 510)
        e.Graphics.DrawString("Ammount", pfont, Brushes.Black, 530, 510)
        '===========================================================================

        e.Graphics.DrawString("===========", pfont, Brushes.Black, 310, 520)
        e.Graphics.DrawString("==================", pfont, Brushes.Black, 400, 520)
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
            e.Graphics.DrawString(DataGridView1.Rows(n).Cells(2).Value, pfont, Brushes.Black, 420, i)
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
        e.Graphics.DrawString("You were served By : " & wname, pfont, Brushes.Black, 350, i + 78)
        '***********************************************************
                                            '*
        txtQtyBeers.Clear()                                       '*
        '*
        txtCashPaid.Clear()                                       '*
        txtCashPiad_LostFocus(sender, e)                          '*                            '*
        lblSubTot.Text = ""                                       '*
        lblCashChge.Text = "Change Refunded"                      '*
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub AddItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddItemsToolStripMenuItem.Click
        MenuItem.Show()
    End Sub

    Private Sub MenuToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuToolStripMenuItem.Click

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        dgvMenuItems.Rows.Clear()
        cboBeers.Items.Clear()
        cboWaiterNo.Items.Clear()
        reload()

    End Sub
End Class