<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.components = New System.ComponentModel.Container()
        Me.Strip1 = New System.Windows.Forms.StatusStrip()
        Me.lblstatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.tabDevice = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnScan = New System.Windows.Forms.Button()
        Me.cmbFilter = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstDevices = New System.Windows.Forms.ListBox()
        Me.cMenu1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mAddToManagedDeviceList = New System.Windows.Forms.ToolStripMenuItem()
        Me.mShowDescriptionXML = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lstManagedDevices = New System.Windows.Forms.ListBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtDetail = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmbUtil = New System.Windows.Forms.ComboBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.tbControl = New System.Windows.Forms.TabControl()
        Me.Strip1.SuspendLayout()
        Me.tabDevice.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.cMenu1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.tbControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'Strip1
        '
        Me.Strip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblstatus})
        Me.Strip1.Location = New System.Drawing.Point(0, 917)
        Me.Strip1.Name = "Strip1"
        Me.Strip1.Size = New System.Drawing.Size(1232, 22)
        Me.Strip1.TabIndex = 1
        Me.Strip1.Text = "StatusStrip1"
        '
        'lblstatus
        '
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(1217, 17)
        Me.lblstatus.Spring = True
        Me.lblstatus.Text = "Status"
        Me.lblstatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1224, 879)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'tabDevice
        '
        Me.tabDevice.Controls.Add(Me.SplitContainer2)
        Me.tabDevice.Location = New System.Drawing.Point(4, 26)
        Me.tabDevice.Name = "tabDevice"
        Me.tabDevice.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDevice.Size = New System.Drawing.Size(1224, 887)
        Me.tabDevice.TabIndex = 0
        Me.tabDevice.Text = "UPnP Devices"
        Me.tabDevice.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox3)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer2.Size = New System.Drawing.Size(1218, 881)
        Me.SplitContainer2.SplitterDistance = 438
        Me.SplitContainer2.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer1.Size = New System.Drawing.Size(438, 881)
        Me.SplitContainer1.SplitterDistance = 670
        Me.SplitContainer1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnScan)
        Me.Panel1.Controls.Add(Me.cmbFilter)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(438, 52)
        Me.Panel1.TabIndex = 6
        '
        'btnScan
        '
        Me.btnScan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnScan.Location = New System.Drawing.Point(312, 6)
        Me.btnScan.Name = "btnScan"
        Me.btnScan.Size = New System.Drawing.Size(120, 40)
        Me.btnScan.TabIndex = 0
        Me.btnScan.Text = "Scan"
        Me.btnScan.UseVisualStyleBackColor = True
        '
        'cmbFilter
        '
        Me.cmbFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFilter.FormattingEnabled = True
        Me.cmbFilter.Location = New System.Drawing.Point(6, 8)
        Me.cmbFilter.Name = "cmbFilter"
        Me.cmbFilter.Size = New System.Drawing.Size(300, 25)
        Me.cmbFilter.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lstDevices)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 52)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox1.Size = New System.Drawing.Size(438, 618)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Available UPnP Devices:"
        '
        'lstDevices
        '
        Me.lstDevices.ContextMenuStrip = Me.cMenu1
        Me.lstDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstDevices.FormattingEnabled = True
        Me.lstDevices.ItemHeight = 17
        Me.lstDevices.Location = New System.Drawing.Point(6, 24)
        Me.lstDevices.Name = "lstDevices"
        Me.lstDevices.Size = New System.Drawing.Size(426, 588)
        Me.lstDevices.TabIndex = 2
        '
        'cMenu1
        '
        Me.cMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mAddToManagedDeviceList, Me.mShowDescriptionXML})
        Me.cMenu1.Name = "cMenu1"
        Me.cMenu1.ShowImageMargin = False
        Me.cMenu1.Size = New System.Drawing.Size(198, 48)
        '
        'mAddToManagedDeviceList
        '
        Me.mAddToManagedDeviceList.Name = "mAddToManagedDeviceList"
        Me.mAddToManagedDeviceList.Size = New System.Drawing.Size(197, 22)
        Me.mAddToManagedDeviceList.Text = "Add to Managed Device List"
        '
        'mShowDescriptionXML
        '
        Me.mShowDescriptionXML.Name = "mShowDescriptionXML"
        Me.mShowDescriptionXML.Size = New System.Drawing.Size(197, 22)
        Me.mShowDescriptionXML.Text = "Show Description XML"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lstManagedDevices)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox2.Size = New System.Drawing.Size(438, 207)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Managed UPnP Devices:"
        '
        'lstManagedDevices
        '
        Me.lstManagedDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstManagedDevices.FormattingEnabled = True
        Me.lstManagedDevices.ItemHeight = 17
        Me.lstManagedDevices.Location = New System.Drawing.Point(6, 24)
        Me.lstManagedDevices.Name = "lstManagedDevices"
        Me.lstManagedDevices.Size = New System.Drawing.Size(426, 177)
        Me.lstManagedDevices.TabIndex = 2
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtDetail)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 52)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(776, 829)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Query Results:"
        '
        'txtDetail
        '
        Me.txtDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDetail.Location = New System.Drawing.Point(3, 21)
        Me.txtDetail.Multiline = True
        Me.txtDetail.Name = "txtDetail"
        Me.txtDetail.ReadOnly = True
        Me.txtDetail.Size = New System.Drawing.Size(770, 805)
        Me.txtDetail.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cmbUtil)
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(776, 52)
        Me.Panel2.TabIndex = 7
        '
        'cmbUtil
        '
        Me.cmbUtil.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbUtil.FormattingEnabled = True
        Me.cmbUtil.Location = New System.Drawing.Point(6, 8)
        Me.cmbUtil.Name = "cmbUtil"
        Me.cmbUtil.Size = New System.Drawing.Size(638, 25)
        Me.cmbUtil.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(650, 6)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(120, 40)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "Scan"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'tbControl
        '
        Me.tbControl.Controls.Add(Me.tabDevice)
        Me.tbControl.Controls.Add(Me.TabPage2)
        Me.tbControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbControl.Location = New System.Drawing.Point(0, 0)
        Me.tbControl.Name = "tbControl"
        Me.tbControl.SelectedIndex = 0
        Me.tbControl.Size = New System.Drawing.Size(1232, 917)
        Me.tbControl.TabIndex = 2
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1232, 939)
        Me.Controls.Add(Me.tbControl)
        Me.Controls.Add(Me.Strip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "MainForm"
        Me.Text = "UPnP Control System"
        Me.Strip1.ResumeLayout(False)
        Me.Strip1.PerformLayout()
        Me.tabDevice.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.cMenu1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.tbControl.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Strip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblstatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents tabDevice As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents tbControl As System.Windows.Forms.TabControl
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lstDevices As System.Windows.Forms.ListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmbFilter As System.Windows.Forms.ComboBox
    Friend WithEvents btnScan As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstManagedDevices As System.Windows.Forms.ListBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmbUtil As System.Windows.Forms.ComboBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDetail As System.Windows.Forms.TextBox
    Friend WithEvents cMenu1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mAddToManagedDeviceList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mShowDescriptionXML As System.Windows.Forms.ToolStripMenuItem

End Class
