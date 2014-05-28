<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Users
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ViewEmployessToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlUser = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgvUsers = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlEmp = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgvEmp = New System.Windows.Forms.DataGridView()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.MenuStrip1.SuspendLayout()
        Me.pnlUser.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEmp.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewEmployessToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(531, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ViewEmployessToolStripMenuItem
        '
        Me.ViewEmployessToolStripMenuItem.Name = "ViewEmployessToolStripMenuItem"
        Me.ViewEmployessToolStripMenuItem.Size = New System.Drawing.Size(103, 20)
        Me.ViewEmployessToolStripMenuItem.Text = "View Employess"
        '
        'pnlUser
        '
        Me.pnlUser.Controls.Add(Me.Panel1)
        Me.pnlUser.Controls.Add(Me.GroupBox1)
        Me.pnlUser.Location = New System.Drawing.Point(7, 27)
        Me.pnlUser.Name = "pnlUser"
        Me.pnlUser.Size = New System.Drawing.Size(505, 486)
        Me.pnlUser.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Location = New System.Drawing.Point(316, 433)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(165, 41)
        Me.Panel1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(32, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(97, 30)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "DELETE"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvUsers)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(502, 417)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SYSTEM USERS"
        '
        'dgvUsers
        '
        Me.dgvUsers.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvUsers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4})
        Me.dgvUsers.Location = New System.Drawing.Point(6, 12)
        Me.dgvUsers.Name = "dgvUsers"
        Me.dgvUsers.RowHeadersVisible = False
        Me.dgvUsers.Size = New System.Drawing.Size(490, 399)
        Me.dgvUsers.TabIndex = 1
        '
        'Column1
        '
        Me.Column1.HeaderText = "User Name"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 125
        '
        'Column2
        '
        Me.Column2.HeaderText = "Password"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 120
        '
        'Column3
        '
        Me.Column3.HeaderText = "Postion"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 120
        '
        'Column4
        '
        Me.Column4.HeaderText = "Emplyee ID"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 120
        '
        'pnlEmp
        '
        Me.pnlEmp.Controls.Add(Me.Panel3)
        Me.pnlEmp.Controls.Add(Me.GroupBox2)
        Me.pnlEmp.Location = New System.Drawing.Point(548, 27)
        Me.pnlEmp.Name = "pnlEmp"
        Me.pnlEmp.Size = New System.Drawing.Size(505, 486)
        Me.pnlEmp.TabIndex = 2
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.Button2)
        Me.Panel3.Location = New System.Drawing.Point(328, 438)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(165, 41)
        Me.Panel3.TabIndex = 2
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(32, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(97, 30)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "DELETE"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvEmp)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(497, 432)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "EMPLOYEES"
        '
        'dgvEmp
        '
        Me.dgvEmp.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvEmp.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.Column9})
        Me.dgvEmp.Location = New System.Drawing.Point(6, 19)
        Me.dgvEmp.Name = "dgvEmp"
        Me.dgvEmp.RowHeadersVisible = False
        Me.dgvEmp.Size = New System.Drawing.Size(484, 407)
        Me.dgvEmp.TabIndex = 0
        '
        'Column5
        '
        Me.Column5.HeaderText = "Employee ID"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "First Name"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "Last Name"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "User Name"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "Position"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(509, 207)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(15, 61)
        Me.btnBack.TabIndex = 1
        Me.btnBack.Text = ">>"
        Me.btnBack.UseVisualStyleBackColor = True
        Me.btnBack.Visible = False
        '
        'Users
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 513)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.pnlEmp)
        Me.Controls.Add(Me.pnlUser)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(547, 551)
        Me.Name = "Users"
        Me.Text = "Users"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.pnlUser.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEmp.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvEmp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents pnlUser As System.Windows.Forms.Panel
    Friend WithEvents pnlEmp As System.Windows.Forms.Panel
    Friend WithEvents dgvUsers As System.Windows.Forms.DataGridView
    Friend WithEvents dgvEmp As System.Windows.Forms.DataGridView
    Friend WithEvents ViewEmployessToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnBack As System.Windows.Forms.Button
End Class
