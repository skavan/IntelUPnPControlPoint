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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.Strip1 = New System.Windows.Forms.StatusStrip()
        Me.lblstatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.tabDevice = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lstDevices = New System.Windows.Forms.ListBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmbFilter = New System.Windows.Forms.ToolStripComboBox()
        Me.btnSearch = New System.Windows.Forms.ToolStripButton()
        Me.txtDetail = New System.Windows.Forms.TextBox()
        Me.tbControl = New System.Windows.Forms.TabControl()
        Me.Strip1.SuspendLayout()
        Me.tabDevice.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.tbControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'Strip1
        '
        Me.Strip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblstatus})
        Me.Strip1.Location = New System.Drawing.Point(0, 737)
        Me.Strip1.Name = "Strip1"
        Me.Strip1.Size = New System.Drawing.Size(1232, 30)
        Me.Strip1.TabIndex = 1
        Me.Strip1.Text = "StatusStrip1"
        '
        'lblstatus
        '
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(1217, 25)
        Me.lblstatus.Spring = True
        Me.lblstatus.Text = "Status"
        Me.lblstatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 37)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1224, 696)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'tabDevice
        '
        Me.tabDevice.Controls.Add(Me.SplitContainer2)
        Me.tabDevice.Location = New System.Drawing.Point(4, 37)
        Me.tabDevice.Name = "tabDevice"
        Me.tabDevice.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDevice.Size = New System.Drawing.Size(1224, 696)
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lstDevices)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ToolStrip1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtDetail)
        Me.SplitContainer2.Size = New System.Drawing.Size(1218, 690)
        Me.SplitContainer2.SplitterDistance = 405
        Me.SplitContainer2.TabIndex = 0
        '
        'lstDevices
        '
        Me.lstDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstDevices.FormattingEnabled = True
        Me.lstDevices.ItemHeight = 28
        Me.lstDevices.Location = New System.Drawing.Point(0, 33)
        Me.lstDevices.Name = "lstDevices"
        Me.lstDevices.Size = New System.Drawing.Size(405, 657)
        Me.lstDevices.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmbFilter, Me.btnSearch})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(405, 33)
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
        'txtDetail
        '
        Me.txtDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDetail.Location = New System.Drawing.Point(0, 0)
        Me.txtDetail.Multiline = True
        Me.txtDetail.Name = "txtDetail"
        Me.txtDetail.ReadOnly = True
        Me.txtDetail.Size = New System.Drawing.Size(809, 690)
        Me.txtDetail.TabIndex = 0
        '
        'tbControl
        '
        Me.tbControl.Controls.Add(Me.tabDevice)
        Me.tbControl.Controls.Add(Me.TabPage2)
        Me.tbControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbControl.Location = New System.Drawing.Point(0, 0)
        Me.tbControl.Name = "tbControl"
        Me.tbControl.SelectedIndex = 0
        Me.tbControl.Size = New System.Drawing.Size(1232, 737)
        Me.tbControl.TabIndex = 2
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 28.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1232, 767)
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
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.tbControl.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Strip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblstatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents tabDevice As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmbFilter As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents btnSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtDetail As System.Windows.Forms.TextBox
    Friend WithEvents tbControl As System.Windows.Forms.TabControl
    Friend WithEvents lstDevices As System.Windows.Forms.ListBox

End Class
