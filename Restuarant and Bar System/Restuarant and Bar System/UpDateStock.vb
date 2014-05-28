Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.SqlServer
Imports System.Drawing.Drawing2D
Public Class UpDateStock

    Dim con As New SqlConnection("Server=localhost; database=RestuarantBar; uid=sa; password=pass;")
    Dim BpriItem, BpriPac, Laststock, instock, currentstock As Integer
    Dim newItem As String
    Dim strTitle As String = "ETHNIC -- Bar and Lounge"
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim index As Integer = DataGridView4.CurrentCell.RowIndex
            DataGridView4.Rows.RemoveAt(index)
            SumUpCost()
        Catch ex As Exception
            MessageBox.Show(ex.Message, strTitle)
        End Try
    End Sub
    Public Function SumUpCost()
        Dim n As Integer = 0
        'Dim i As Integer = 0
        Dim total As Integer = 0
        While n < DataGridView4.Rows.Count
            total = total + DataGridView4.Rows(n).Cells(9).Value
            n = n + 1
        End While
        lblSubTot.Text = total
        Return Nothing
    End Function
    Public Sub reload()
        con.Open()
        Dim cmd As New SqlCommand("SELECT ItemName FROM StockItems1", con)
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader
        While reader.Read
            cboPuItemNem.Items.Add(reader.Item("ItemName"))
        End While
        reader.Close()

        Dim cmd1 As New SqlCommand("SELECT * FROM Category", con)
        Dim reader1 As SqlDataReader
        reader1 = cmd1.ExecuteReader
        While reader1.Read
            cboIteCateg.Items.Add(reader1.Item(0))
        End While
        reader1.Close()
        Dim cmd2 As New SqlCommand("SELECT * FROM Suppliers", con)
        Dim reader2 As SqlDataReader
        reader2 = cmd2.ExecuteReader
        While reader2.Read
            ComboBox1.Items.Add(reader2.Item(0))
        End While
        reader2.Close()
    End Sub
    Private Sub UpDateStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            reload()
        Catch ex As Exception
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try


    End Sub
    Private Sub txtItePack_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItePack.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False

    End Sub
    Private Sub txtNumPacks_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumPacks.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False

    End Sub
    Private Sub txtPriItem_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPriItem.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False

    End Sub
    Private Sub txtPriPack_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPriPack.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        If e.KeyChar = Chr(8) Then e.Handled = False

    End Sub
    Private Sub updateStock_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dim respo = MessageBox.Show("Are you sure you want to exit", strTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If respo = Windows.Forms.DialogResult.Yes Then

            Exit Sub
        ElseIf respo = Windows.Forms.DialogResult.No Then

            Dim x As New UpDateStock
            x.Show()
        End If
    End Sub

    Private Sub txtItePurPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumPacks.TextChanged
        If txtNumPacks.Text = "" Then
            Exit Sub
        End If
    End Sub

    Private Sub txtNumPacks_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtItePack.TextChanged
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtQtyPur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPriItem.TextChanged
        'transfer this to the save button
        
    End Sub

    Private Sub cboPuItemNem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPuItemNem.SelectedIndexChanged

        Try
            con.Open()
            Dim cmd As New SqlCommand("SELECT *  FROM StockItems1 WHERE ItemName = '" & cboPuItemNem.SelectedItem & "'", con)
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader
            While reader.Read
                BpriItem = reader.Item("BPricePerItem")
                BpriPac = reader.Item("BPricePerPack")
                txtPriItem.Text = reader.Item("BPricePerItem")
                txtPriPack.Text = reader.Item("BPricePerPack")
                cboIteCateg.Text = reader.Item("Category")
                currentstock = reader.Item("CurrentStock")
                ComboBox1.Text = reader.Item("SupplierName")
            End While
            reader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub
    ' Public flag As Boolean = False

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnter.Click
        Try
            ' If flag = False Then btnSave.Enabled = True
            ' If flag = True Then btnSNewIte.Enabled = True
            If Not cboPuItemNem.Items.Contains(cboPuItemNem.Text) Then
                Dim respo = MessageBox.Show("        Do wish to Save " & cboPuItemNem.Text.ToUpper & "  as new Item " & vbNewLine & "Press YESS to continue or NO to select from Item List ", strTitle, MessageBoxButtons.YesNo)
                If respo = DialogResult.Yes Then
                    ' flag = True
                    'btnSNewIte.Enabled = True
                    btnSave.Enabled = False
                    If Not cboIteCateg.Items.Contains(cboIteCateg.Text) Then

                        Dim resp = MessageBox.Show("        Do wish to Save " & cboIteCateg.Text.ToUpper & "  as new Item Category " & vbNewLine & "Press YESS to continue or NO to select category List ", strTitle, MessageBoxButtons.YesNo)
                        If resp = DialogResult.Yes Then
                            If cboIteCateg.Text = "Select Category" Or cboIteCateg.Text = "" Then Exit Sub
                            cboIteCateg.Items.Add(cboIteCateg.Text)
                            ' If con.State = ConnectionState.Closed Then con.Open()
                            'Dim cmd As New SqlCommand("INSERT INTO Category VALUES('" & cboIteCateg.Text & "')", con)
                            'cmd.ExecuteNonQuery()
                        Else
                            Exit Sub
                        End If
                    End If
                    If Not ComboBox1.Items.Contains(ComboBox1.Text) Then
                        Dim respo1 = MessageBox.Show("        Do wish to Save " & ComboBox1.Text.ToUpper & "  as new Supplier " & vbNewLine & "Press YESS to continue or NO to select from Supplier List ", strTitle, MessageBoxButtons.YesNo)
                        If respo1 = DialogResult.Yes Then
                            If ComboBox1.Text = "Select Supplier" Or ComboBox1.Text = "" Then Exit Sub
                            ComboBox1.Items.Add(ComboBox1.Text)

                            If con.State = ConnectionState.Closed Then con.Open()
                            Dim cmd As New SqlCommand("INSERT INTO Suppliers VALUES('" & ComboBox1.Text.Trim & "')", con)
                            cmd.ExecuteNonQuery()
                        Else
                            ComboBox1.DroppedDown = True
                            Exit Sub
                        End If
                    End If

                    Dim answer = InputBox("Please input Item Name", strTitle, cboPuItemNem.Text)

                    If answer <> "" And answer <> "Select Item" Then
                        cboPuItemNem.Items.Add(answer)
                        btnSNewIte.Enabled = True
                        If txtNumPacks.Text = "" Then
                            MessageBox.Show("Please enter number of Packs...!!!", strTitle)
                            txtNumPacks.Focus()
                            Exit Sub
                        Else
                            If txtItePack.Text = "" Then
                                MessageBox.Show("Please enter number of items per pack...!!!", strTitle)
                                txtItePack.Focus()

                                Exit Sub

                            Else
                                If txtPriPack.Text = "" Then
                                    MessageBox.Show("Please enter price per Packs...!!!", strTitle)
                                    txtPriPack.Focus()

                                    Exit Sub

                                Else
                                    If txtPriItem.Text = "" Then
                                        MessageBox.Show("Please enter price per Item", strTitle)
                                        txtPriItem.Focus()

                                        Exit Sub
                                    Else
                                        datagrid()
                                    End If
                                End If
                            End If

                        End If
                        ' End If

                        '==========================================================================
                        'check if user inputs new buying price per item 

                    Else
                        MessageBox.Show("Invalid item Name  : " & answer)
                        btnSave.Enabled = True
                        btnSNewIte.Enabled = False
                        Exit Sub
                    End If


                ElseIf respo = DialogResult.No Then
                    cboPuItemNem.Text = "Select Item"
                    cboPuItemNem.Focus()
                    cboPuItemNem.DroppedDown = True
                    ' flag = False
                    btnSave.Enabled = True
                    btnSNewIte.Enabled = False
                    Exit Sub
                End If
            Else
                If Not cboIteCateg.Items.Contains(cboIteCateg.Text) Then

                    Dim resp = MessageBox.Show("        Do wish to Save " & cboIteCateg.Text.ToUpper & "  as new Item Category " & vbNewLine & "Press YESS to continue or NO to select category List ", strTitle, MessageBoxButtons.YesNo)
                    If resp = DialogResult.Yes Then
                        If cboIteCateg.Text = "Select Category" Or cboIteCateg.Text = "" Then Exit Sub
                        cboIteCateg.Items.Add(cboIteCateg.Text)
                        ' If con.State = ConnectionState.Closed Then con.Open()
                        ' Dim cmd As New SqlCommand("INSERT INTO Category VALUES('" & cboIteCateg.Text & "')", con)
                        ' cmd.ExecuteNonQuery()
                    Else
                        cboIteCateg.DroppedDown = True
                        Exit Sub
                    End If
                End If

                If Not ComboBox1.Items.Contains(ComboBox1.Text) Then
                    Dim respo = MessageBox.Show("        Do wish to Save " & ComboBox1.Text.ToUpper & "  as new Supplier " & vbNewLine & "Press YESS to continue or NO to select from Supplier List ", strTitle, MessageBoxButtons.YesNo)
                    If respo = DialogResult.Yes Then
                        If ComboBox1.Text = "Select Supplier" Or ComboBox1.Text = "" Then Exit Sub
                        ComboBox1.Items.Add(ComboBox1.Text)

                        'If con.State = ConnectionState.Closed Then con.Open()
                        'Dim cmd As New SqlCommand("INSERT INTO Suppliers VALUES('" & ComboBox1.Text.Trim & "')", con)
                        ' cmd.ExecuteNonQuery()
                    Else
                        ComboBox1.DroppedDown = True
                        Exit Sub
                    End If
                Else
                    If txtNumPacks.Text = "" Or txtNumPacks.Text.Trim = "0" Then
                        MessageBox.Show("Please enter number of Packs...!!!")
                        txtNumPacks.Focus()
                        Exit Sub
                    Else
                        If txtItePack.Text = "" Or txtItePack.Text = "0" Then
                            MessageBox.Show("Please enter number of Items per pack")
                            txtItePack.Focus()
                            Exit Sub
                        Else
                            'check if user inputs new buying price per item 
                            If txtPriItem.Text = "" Or txtPriItem.Text = "0" Then
                                MessageBox.Show("Please enter price per Item", strTitle)
                                txtPriItem.Focus()
                                Exit Sub
                            Else
                                If CInt(txtPriItem.Text.Trim) <> BpriItem Then

                                    Dim resp = MessageBox.Show("Do you wish to update the price per Item to " & txtPriItem.Text & vbNewLine & " click Yess to continue or NO to use :" & BpriItem, strTitle, MessageBoxButtons.YesNo)
                                    If resp = DialogResult.Yes Then BpriItem = CInt(txtPriItem.Text)
                                    'End If
                                Else
                                    If txtPriPack.Text = "" Or txtPriPack.Text = "0" Then
                                        MessageBox.Show("Please enter price per pack", strTitle)
                                        txtPriPack.Focus()
                                        Exit Sub
                                    ElseIf CInt(txtPriPack.Text.Trim) <> BpriPac Then
                                        Dim rep = MessageBox.Show("  Do you wish to update the price per pack to?" & txtPriPack.Text & vbNewLine & "Press YESS to continue or CANCEL to use : " & BpriPac, strTitle, MessageBoxButtons.YesNo)
                                        If rep = DialogResult.Yes Then
                                            BpriPac = CInt(txtPriPack.Text)
                                            datagrid()
                                        Else
                                            datagrid()
                                        End If
                                    Else
                                        datagrid()


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
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub
    Public Sub datagrid1()
        Dim i As Integer = 0
        Dim nb As Integer = DataGridView4.RowCount - 1

        DataGridView4.Rows.Add()
        DataGridView4.Rows(nb).Cells(0).Value = DateTimePicker1.Value
        DataGridView4.Rows(nb).Cells(1).Value = cboPuItemNem.Text
        DataGridView4.Rows(nb).Cells(2).Value = cboIteCateg.Text
        DataGridView4.Rows(nb).Cells(3).Value = txtNumPacks.Text
        DataGridView4.Rows(nb).Cells(4).Value = txtItePack.Text
        DataGridView4.Rows(nb).Cells(5).Value = txtPriItem.Text
        DataGridView4.Rows(nb).Cells(6).Value = txtPriPack.Text
        DataGridView4.Rows(nb).Cells(7).Value = currentstock
        DataGridView4.Rows(nb).Cells(8).Value = CInt(txtItePack.Text) * CInt(txtNumPacks.Text)
        DataGridView4.Rows(nb).Cells(9).Value = CInt(txtNumPacks.Text) * CInt(txtPriPack.Text)
        DataGridView4.Rows(nb).Cells(10).Value = ComboBox1.Text
        DataGridView4.Rows(nb).Cells(11).Value = EthnicLogin.login
        DataGridView4.Rows(nb).Cells(12).Value = EthnicLogin.EmpID '  remeber to change this to  users names
        'DataGridView4.Rows(i).Cells(3).Value = DataGridView4.Rows(i).Cells(1).Value * DataGridView4.Rows(i).Cells(2).Value
        SumUpCost()
        txtItePack.Clear()
        txtNumPacks.Clear()
        txtPriItem.Clear()
        txtPriPack.Clear()
        cboPuItemNem.Text = "Select Item"
        cboIteCateg.Text = "Select Category"
        BpriItem = 0
        BpriPac = 0
    End Sub
    Public Sub datagrid()
        Dim i As Integer = 0
        Dim nb As Integer = DataGridView4.RowCount - 1

        '**************************Do the neccessary imcrements
        While i < nb
            If DataGridView4.Rows(i).Cells(1).Value = cboPuItemNem.SelectedItem Then
                DataGridView4.Rows(i).Cells(3).Value = CInt(DataGridView4.Rows(i).Cells(3).Value) + CInt(txtNumPacks.Text)
                DataGridView4.Rows(i).Cells(9).Value = DataGridView4.Rows(i).Cells(3).Value * DataGridView4.Rows(i).Cells(6).Value
                DataGridView4.Rows(i).Cells(8).Value = DataGridView4.Rows(i).Cells(4).Value * DataGridView4.Rows(i).Cells(3).Value
                SumUpCost()
                Exit Sub
            End If
            i = i + 1
        End While
        datagrid1()

    End Sub

    Private Sub btnSNewIte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNewIte.Click
        '=======================================================================
        'save to the purchases table
        'update the stock items table
        'update the purchasas transaction table
        Try
            con.Open()

            Dim dt As Date = DateTimePicker1.Value.Date & " " & TimeOfDay
            Dim itemName, category, supplier, authorBy As String
            Dim emploID, TotCost, TotStock, itePack, PriItem, PriPack, NumPacks As Integer

            itemName = DataGridView4.Rows(0).Cells(1).Value
            category = DataGridView4.Rows(0).Cells(2).Value
            NumPacks = DataGridView4.Rows(0).Cells(3).Value
            itePack = DataGridView4.Rows(0).Cells(4).Value
            PriItem = DataGridView4.Rows(0).Cells(5).Value
            PriPack = DataGridView4.Rows(0).Cells(6).Value
            currentstock = DataGridView4.Rows(0).Cells(7).Value
            TotStock = DataGridView4.Rows(0).Cells(8).Value
            TotCost = DataGridView4.Rows(0).Cells(9).Value
            supplier = DataGridView4.Rows(0).Cells(10).Value
            authorBy = DataGridView4.Rows(0).Cells(11).Value
            emploID = DataGridView4.Rows(0).Cells(12).Value

            Dim cmd1 As New SqlCommand("SELECT * FROM TransNoPur", con)
            Dim reader As SqlDataReader
            Dim transNo As Integer
            reader = cmd1.ExecuteReader
            While reader.Read
                transNo = reader.Item("TransNo") + 1

            End While


            reader.Close()

            Dim cdmu As New SqlCommand("INSERT INTO TransNoPur VALUES('" & transNo & "', '" & dt & "', '" & authorBy & "', '" & emploID & "')", con)
            cdmu.ExecuteNonQuery()

            Dim cmdP As New SqlCommand
            cmdP.CommandText = ("INSERT INTO PurchasesNew VALUES('" & dt & "','" & itemName & "','" & category & "', '" & NumPacks & "','" & itePack & "', '" & PriItem & "', '" & PriPack & "','" & currentstock & "', '" & TotStock & "','" & TotCost & "', '" & authorBy & "', '" & transNo & "', '" & emploID & "')")
            cmdP.Connection = con
            cmdP.ExecuteNonQuery()
            Dim cmd As New SqlCommand
            cmd.CommandText = "INSERT INTO StockItems1 VALUES('" & itemName & "', '" & category & "', '" & 0 & "', '" & 0 & "', '" & PriItem & "', '" & PriPack & "', '" & 0 & "', '" & TotStock & "', '" & TotStock & "','" & supplier & "')"
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            If Not ComboBox1.Items.Contains(ComboBox1.Text) Then
                Dim cmds As New SqlCommand("INSERT INTO Suppliers VALUES('" & supplier & "')", con)
                cmds.ExecuteNonQuery()
            End If
            If Not cboIteCateg.Items.Contains(ComboBox1.Text) Then
                Dim cmde As New SqlCommand("INSERT INTO Category VALUES('" & category & "')", con)
                cmde.ExecuteNonQuery()
            End If
            txtItePack.Clear()
            txtNumPacks.Clear()
            txtPriItem.Clear()
            txtPriPack.Clear()
            cboPuItemNem.Text = "Select Item"
            cboIteCateg.Text = "Select Category"
            DataGridView4.Rows.Clear()
            DataGridView4.RowCount = 1
            lblSubTot.Text = ""
            btnSave.Enabled = True
            btnSNewIte.Enabled = False
            MessageBox.Show("Saved Successfully", strTitle)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub btnRecItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRedItem.Click
        Try
            Dim rwCount As Integer = DataGridView4.RowCount - 1
            If rwCount = 0 Then
            Else
                Dim index As Integer = DataGridView4.CurrentCell.RowIndex
                DataGridView4.Rows(index).Cells(9).Value = CInt(DataGridView4.Rows(index).Cells(3).Value) * CInt(DataGridView4.Rows(index).Cells(6).Value)

                DataGridView4.Rows(index).Cells(8).Value = DataGridView4.Rows(index).Cells(4).Value * DataGridView4.Rows(index).Cells(3).Value
                SumUpCost()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        '========================================================================================
        'update the stoc items tabl
        'update the transactions table
        'save t0 the purchases table
        '========================================================================================

        Try
            con.Open()

            Dim dt As Date = DateTimePicker1.Value.Date & " " & TimeOfDay
            Dim itemName, category, supplier, authorBy As String
            Dim emploID, TotCost, TotStock, itePack, PriItem, PriPack, NumPacks As Integer

            Dim cmd1 As New SqlCommand("SELECT * FROM TransNoPur", con)
            Dim reader As SqlDataReader
            Dim transNo As Integer
            reader = cmd1.ExecuteReader
            While reader.Read
                transNo = reader.Item("TransNo") + 1

            End While
            reader.Close()

           

            Dim i As Integer = 0
            Dim n As Integer = DataGridView4.RowCount - 2
            While i <= n
                itemName = DataGridView4.Rows(i).Cells(1).Value
                category = DataGridView4.Rows(i).Cells(2).Value
                NumPacks = DataGridView4.Rows(i).Cells(3).Value
                itePack = DataGridView4.Rows(i).Cells(4).Value
                PriItem = DataGridView4.Rows(i).Cells(5).Value
                PriPack = DataGridView4.Rows(i).Cells(6).Value
                currentstock = DataGridView4.Rows(i).Cells(7).Value
                TotStock = DataGridView4.Rows(i).Cells(8).Value
                TotCost = DataGridView4.Rows(i).Cells(9).Value
                supplier = DataGridView4.Rows(i).Cells(10).Value
                authorBy = DataGridView4.Rows(i).Cells(11).Value
                emploID = DataGridView4.Rows(i).Cells(12).Value
                Dim cmdP As New SqlCommand
                cmdP.CommandText = ("INSERT INTO PurchasesNew VALUES('" & dt & "','" & itemName & "','" & category & "', '" & NumPacks & "','" & itePack & "', '" & PriItem & "', '" & PriPack & "','" & currentstock & "', '" & TotStock & "','" & TotCost & "', '" & authorBy & "', '" & transNo & "', '" & emploID & "')")
                cmdP.Connection = con
                cmdP.ExecuteNonQuery()
                i = i + 1
            End While
            i = 0
            authorBy = DataGridView4.Rows(i).Cells(11).Value
            Dim cdmu As New SqlCommand("INSERT INTO TransNoPur VALUES('" & transNo & "', '" & dt & "', '" & authorBy & "', '" & emploID & "')", con)
            cdmu.ExecuteNonQuery()
            Dim cmdp1, cmdup As New SqlCommand
            Dim dboNewsto As Integer

            While i <= n
                itemName = DataGridView4.Rows(i).Cells(1).Value.ToString.Trim
                TotStock = DataGridView4.Rows(i).Cells(8).Value

                cmdp1.CommandText = "SELECT CurrentStock FROM StockItems1 WHERE ItemName = '" & itemName & " '"
                cmdp1.Connection = con
                reader = cmdp1.ExecuteReader
                If reader.Read Then
                    currentstock = reader.Item("CurrentStock")
                End If
                reader.Close()

                dboNewsto = currentstock + TotStock

                cmdup.CommandText = "UPDATE StockItems1 SET CurrentStock = '" & dboNewsto & "',BPricePerItem = '" & PriItem & "', BPricePerPack = '" & PriPack & "', LastStock = '" & currentstock & "',InStock = '" & TotStock & "'  WHERE ItemName = '" & itemName & "' "
                cmdup.Connection = con
                cmdup.ExecuteNonQuery()
                i = i + 1
            End While
            txtItePack.Clear()
            txtNumPacks.Clear()
            txtPriItem.Clear()
            txtPriPack.Clear()
            cboPuItemNem.Text = "Select Item"
            cboIteCateg.Text = "Select Category"
            DataGridView4.Rows.Clear()
            lblSubTot.Text = "0.00"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub
End Class