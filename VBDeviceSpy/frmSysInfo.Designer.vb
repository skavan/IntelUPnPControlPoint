<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSysInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSysInfo))
        Me.Strip1 = New System.Windows.Forms.StatusStrip()
        Me.lblstatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.mainMenu = New System.Windows.Forms.MainMenu(Me.components)
        Me.menuItem1 = New System.Windows.Forms.MenuItem()
        Me.manuallyAddDeviceMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem15 = New System.Windows.Forms.MenuItem()
        Me.menuItem12 = New System.Windows.Forms.MenuItem()
        Me.menuItem9 = New System.Windows.Forms.MenuItem()
        Me.menuItem4 = New System.Windows.Forms.MenuItem()
        Me.menuItem13 = New System.Windows.Forms.MenuItem()
        Me.menuItem14 = New System.Windows.Forms.MenuItem()
        Me.menuItem2 = New System.Windows.Forms.MenuItem()
        Me.menuItem7 = New System.Windows.Forms.MenuItem()
        Me.rescanMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem19 = New System.Windows.Forms.MenuItem()
        Me.expandAllMenuItem = New System.Windows.Forms.MenuItem()
        Me.collapseAllMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem16 = New System.Windows.Forms.MenuItem()
        Me.menuItem3 = New System.Windows.Forms.MenuItem()
        Me.viewStatusbarMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem5 = New System.Windows.Forms.MenuItem()
        Me.helpMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem6 = New System.Windows.Forms.MenuItem()
        Me.menuItem10 = New System.Windows.Forms.MenuItem()
        Me.showDebugInfoMenuItem = New System.Windows.Forms.MenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.tbControl = New System.Windows.Forms.TabControl()
        Me.tabDevice = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.deviceTree = New System.Windows.Forms.TreeView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmbFilter = New System.Windows.Forms.ToolStripComboBox()
        Me.btnSearch = New System.Windows.Forms.ToolStripButton()
        Me.listInfo = New System.Windows.Forms.ListView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.columnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Strip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.tbControl.SuspendLayout()
        Me.tabDevice.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Strip1
        '
        Me.Strip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblstatus})
        Me.Strip1.Location = New System.Drawing.Point(0, 779)
        Me.Strip1.Name = "Strip1"
        Me.Strip1.Size = New System.Drawing.Size(1315, 30)
        Me.Strip1.TabIndex = 0
        Me.Strip1.Text = "StatusStrip1"
        '
        'lblstatus
        '
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(1300, 25)
        Me.lblstatus.Spring = True
        Me.lblstatus.Text = "Status"
        '
        'mainMenu
        '
        Me.mainMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuItem1, Me.menuItem7, Me.menuItem5})
        '
        'menuItem1
        '
        Me.menuItem1.Index = 0
        Me.menuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.manuallyAddDeviceMenuItem, Me.menuItem15, Me.menuItem12, Me.menuItem9, Me.menuItem4, Me.menuItem13, Me.menuItem14, Me.menuItem2})
        Me.menuItem1.Text = "&File"
        '
        'manuallyAddDeviceMenuItem
        '
        Me.manuallyAddDeviceMenuItem.Index = 0
        Me.manuallyAddDeviceMenuItem.Text = "Manually Add Device"
        '
        'menuItem15
        '
        Me.menuItem15.Index = 1
        Me.menuItem15.Text = "-"
        '
        'menuItem12
        '
        Me.menuItem12.Index = 2
        Me.menuItem12.Text = "Copy &information table to clipboard"
        '
        'menuItem9
        '
        Me.menuItem9.Index = 3
        Me.menuItem9.Text = "Copy &event log to clipboard"
        '
        'menuItem4
        '
        Me.menuItem4.Index = 4
        Me.menuItem4.Text = "-"
        '
        'menuItem13
        '
        Me.menuItem13.Index = 5
        Me.menuItem13.Text = "&Clear Event Log"
        '
        'menuItem14
        '
        Me.menuItem14.Index = 6
        Me.menuItem14.Text = "-"
        '
        'menuItem2
        '
        Me.menuItem2.Index = 7
        Me.menuItem2.Text = "E&xit"
        '
        'menuItem7
        '
        Me.menuItem7.Index = 1
        Me.menuItem7.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.rescanMenuItem, Me.menuItem19, Me.expandAllMenuItem, Me.collapseAllMenuItem, Me.menuItem16, Me.menuItem3, Me.viewStatusbarMenuItem})
        Me.menuItem7.Text = "&View"
        '
        'rescanMenuItem
        '
        Me.rescanMenuItem.Index = 0
        Me.rescanMenuItem.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.rescanMenuItem.Text = "Rescan network"
        '
        'menuItem19
        '
        Me.menuItem19.Index = 1
        Me.menuItem19.Text = "-"
        '
        'expandAllMenuItem
        '
        Me.expandAllMenuItem.Index = 2
        Me.expandAllMenuItem.Text = "&Expand all devices"
        '
        'collapseAllMenuItem
        '
        Me.collapseAllMenuItem.Index = 3
        Me.collapseAllMenuItem.Text = "&Collapse all devices"
        '
        'menuItem16
        '
        Me.menuItem16.Index = 4
        Me.menuItem16.Text = "-"
        '
        'menuItem3
        '
        Me.menuItem3.Index = 5
        Me.menuItem3.Text = "Event &log"
        '
        'viewStatusbarMenuItem
        '
        Me.viewStatusbarMenuItem.Checked = True
        Me.viewStatusbarMenuItem.Index = 6
        Me.viewStatusbarMenuItem.Text = "Status &bar"
        '
        'menuItem5
        '
        Me.menuItem5.Index = 2
        Me.menuItem5.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.helpMenuItem, Me.menuItem6, Me.menuItem10, Me.showDebugInfoMenuItem})
        Me.menuItem5.Text = "&Help"
        '
        'helpMenuItem
        '
        Me.helpMenuItem.Index = 0
        Me.helpMenuItem.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.helpMenuItem.Text = "&Help Topics"
        '
        'menuItem6
        '
        Me.menuItem6.Index = 1
        Me.menuItem6.Text = "&Check for updates"
        '
        'menuItem10
        '
        Me.menuItem10.Index = 2
        Me.menuItem10.Text = "-"
        '
        'showDebugInfoMenuItem
        '
        Me.showDebugInfoMenuItem.Index = 3
        Me.showDebugInfoMenuItem.Text = "&Show Debug Information"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbControl)
        Me.SplitContainer1.Size = New System.Drawing.Size(1315, 779)
        Me.SplitContainer1.SplitterDistance = 48
        Me.SplitContainer1.TabIndex = 1
        '
        'tbControl
        '
        Me.tbControl.Controls.Add(Me.tabDevice)
        Me.tbControl.Controls.Add(Me.TabPage2)
        Me.tbControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbControl.Location = New System.Drawing.Point(0, 0)
        Me.tbControl.Name = "tbControl"
        Me.tbControl.SelectedIndex = 0
        Me.tbControl.Size = New System.Drawing.Size(1315, 727)
        Me.tbControl.TabIndex = 0
        '
        'tabDevice
        '
        Me.tabDevice.Controls.Add(Me.SplitContainer2)
        Me.tabDevice.Location = New System.Drawing.Point(4, 37)
        Me.tabDevice.Name = "tabDevice"
        Me.tabDevice.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDevice.Size = New System.Drawing.Size(1307, 686)
        Me.tabDevice.TabIndex = 0
        Me.tabDevice.Text = "Device Scan"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.deviceTree)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ToolStrip1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.listInfo)
        Me.SplitContainer2.Size = New System.Drawing.Size(1301, 680)
        Me.SplitContainer2.SplitterDistance = 433
        Me.SplitContainer2.TabIndex = 0
        '
        'deviceTree
        '
        Me.deviceTree.BackColor = System.Drawing.Color.White
        Me.deviceTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.deviceTree.Indent = 19
        Me.deviceTree.ItemHeight = 16
        Me.deviceTree.Location = New System.Drawing.Point(0, 33)
        Me.deviceTree.Name = "deviceTree"
        Me.deviceTree.Size = New System.Drawing.Size(433, 647)
        Me.deviceTree.TabIndex = 14
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmbFilter, Me.btnSearch})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(433, 33)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmbFilter
        '
        Me.cmbFilter.AutoSize = False
        Me.cmbFilter.Name = "cmbFilter"
        Me.cmbFilter.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.cmbFilter.Size = New System.Drawing.Size(121, 33)
        '
        'btnSearch
        '
        Me.btnSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(68, 30)
        Me.btnSearch.Text = "Search"
        '
        'listInfo
        '
        Me.listInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listInfo.GridLines = True
        Me.listInfo.Location = New System.Drawing.Point(0, 0)
        Me.listInfo.Name = "listInfo"
        Me.listInfo.Size = New System.Drawing.Size(864, 680)
        Me.listInfo.TabIndex = 0
        Me.listInfo.UseCompatibleStateImageBehavior = False
        Me.listInfo.View = System.Windows.Forms.View.Details
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1307, 694)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'columnHeader1
        '
        Me.columnHeader1.Text = "Name"
        Me.columnHeader1.Width = 111
        '
        'columnHeader2
        '
        Me.columnHeader2.Text = "Value"
        Me.columnHeader2.Width = 350
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Name"
        Me.ColumnHeader3.Width = 111
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Value"
        Me.ColumnHeader4.Width = 350
        '
        'frmSysInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 28.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1315, 809)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Strip1)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Menu = Me.mainMenu
        Me.Name = "frmSysInfo"
        Me.Text = "frmSysInfo"
        Me.Strip1.ResumeLayout(False)
        Me.Strip1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.tbControl.ResumeLayout(False)
        Me.tabDevice.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Strip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblstatus As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents mainMenu As System.Windows.Forms.MainMenu
    Private WithEvents menuItem1 As System.Windows.Forms.MenuItem
    Private WithEvents manuallyAddDeviceMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem15 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem12 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem9 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem4 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem13 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem14 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem2 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem7 As System.Windows.Forms.MenuItem
    Private WithEvents rescanMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem19 As System.Windows.Forms.MenuItem
    Private WithEvents expandAllMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents collapseAllMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem16 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem3 As System.Windows.Forms.MenuItem
    Private WithEvents viewStatusbarMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem5 As System.Windows.Forms.MenuItem
    Private WithEvents helpMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem6 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem10 As System.Windows.Forms.MenuItem
    Private WithEvents showDebugInfoMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents tbControl As System.Windows.Forms.TabControl
    Friend WithEvents tabDevice As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmbFilter As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents btnSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Private WithEvents deviceTree As System.Windows.Forms.TreeView
    Private WithEvents columnHeader1 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listInfo As System.Windows.Forms.ListView
    Private WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Private WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
End Class
