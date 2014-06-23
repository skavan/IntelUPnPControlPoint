Imports OpenSource.UPnP



Public Class MainForm
    Dim WithEvents disc As New UPnPDeviceManager.NetworkManager
    Delegate Sub delUpdateDeviceList(device As UPnPDevice, eventType As UPnPDeviceManager.eDeviceDiscoveryEvent)

    '// A Small Class to hold a Search Spec Record
    Class SearchAction

        Enum eSearchType
            devicePattern
            deviceIP
        End Enum

        Property FriendlyName As String
        Property Filter As String
        Property SearchType As eSearchType

        Sub New(sSpec As String, sFriendlyName As String, iSearchType As eSearchType)
            _Filter = sSpec
            _FriendlyName = sFriendlyName
            _SearchType = iSearchType
        End Sub

        Public Function GetString(delimiter As String) As String
            Return Me.Filter & delimiter & Me.FriendlyName & delimiter & Me.SearchType
        End Function

    End Class

#Region "GUI Triggered Events & Methods"

    '// Search for devices - either univeral, by upnp type - or a specific device description url
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        If cmbFilter.SelectedIndex <> -1 Then
            DeviceScan(cmbFilter.SelectedItem)
        End If
    End Sub

    '// the main device list was right clicked.
    Private Sub lstDevices_MouseUp(sender As Object, e As MouseEventArgs) Handles lstDevices.MouseUp
        PopupMenuHandler(sender, e)
    End Sub

    '// the selected/managed device list was right clicked.
    Private Sub lstManagedDevices_MouseUp(sender As Object, e As MouseEventArgs) Handles lstManagedDevices.MouseUp
        PopupMenuHandler(sender, e)
    End Sub

    '// A list device was clicked. Go get its service/device tree
    Private Sub lstDevices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDevices.SelectedIndexChanged
        If lstDevices.SelectedIndex <> -1 Then
            txtDetail.Text = ParseDeviceTree(lstDevices.SelectedItem)
        End If
    End Sub

    '// Handles the popup menu click event
    Private Sub mOnClick(sender As Object, e As EventArgs)
        HandleManagedDevices(sender)
    End Sub

#End Region

#Region "External Event Sinks"
    Private Sub disc_DeviceScanEvent(device As UPnPDevice, eventType As UPnPDeviceManager.eDeviceDiscoveryEvent) Handles disc.DeviceDiscoveryEvent
        MyBase.Invoke(New delUpdateDeviceList(AddressOf UpdateDeviceList), {device, eventType})
    End Sub
#End Region

#Region "Initialization & Cleanup"

    '// save settings and cleanup
    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Dim myCol As Collections.Specialized.StringCollection = My.Settings.SearchActions
        If myCol Is Nothing Then myCol = New Collections.Specialized.StringCollection
        For Each device As UPnPDevice In disc.ManagedDevices
            '// Create a specific device entry
            Dim searchAction1 As New SearchAction(device.LocationURL, device.FriendlyName, SearchAction.eSearchType.deviceIP)

            If Not myCol.Contains(searchAction1.GetString("|")) Then              '// ignore dupes
                'myCol.Add()
                myCol.Add(searchAction1.GetString("|"))
            End If
            '// Create a general upnp urn entry
            Dim searchAction2 As New SearchAction(device.DeviceURN, "*DEVICE TYPE: " & device.StandardDeviceType, SearchAction.eSearchType.devicePattern)
            If Not myCol.Contains(searchAction2.GetString("|")) Then              '// ignore dupes
                myCol.Add(searchAction2.GetString("|"))
            End If
        Next

        My.Settings.SearchActions = myCol
        My.Settings.Save()
        My.Settings.Reload()

        disc = Nothing
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Init()
    End Sub

    Private Sub Init()
        'Dim s1 As New SearchSpec("192.168.1.160", "SqueezeBox @ 192.168.1.160", SearchSpec.eSearchType.deviceIP)
        cmbFilter.Items.Clear()
        cmbFilter.DisplayMember = "FriendlyName"
        cmbFilter.Items.Add(New SearchAction("", " All UPnP Devices", SearchAction.eSearchType.devicePattern))
        'cmbFilter.Items.Add(New SearchAction("urn:schemas-upnp-org:device:MediaRenderer:1", "Media Renderers", SearchAction.eSearchType.devicePattern))
        My.Settings.Test = "Hello"
        If Not My.Settings.SearchActions Is Nothing Then
            For Each strAction As String In My.Settings.SearchActions
                Dim searchAction As New SearchAction(strAction.Split("|")(0), strAction.Split("|")(1), strAction.Split("|")(2))
                If Not cmbFilter.Items.Contains(searchAction) Then cmbFilter.Items.Add(searchAction)
            Next
        End If
        
        'cmbFilter.Items.Add(New SearchAction("http://192.168.1.126:9000/plugins/UPnP/MediaRenderer.xml?player=00:04:20:16:8d:51", "SqueezeBox @ 192.168.1.126", SearchAction.eSearchType.deviceIP))
        'cmbFilter.Items.Add(New SearchAction("http://192.168.1.167:1400/xml/device_description.xml", "SONOS @ 192.168.1.167", SearchAction.eSearchType.deviceIP))

        cmbFilter.Sorted = True
        cmbFilter.SelectedIndex = 0
        lstDevices.ContextMenuStrip = cMenu1
        lstManagedDevices.ContextMenuStrip = cMenu1

        lstDevices.DataSource = disc.AvailableDevices
        lstDevices.DisplayMember = "FriendlyName"
        lstManagedDevices.DataSource = disc.ManagedDevices
        lstManagedDevices.DisplayMember = "FriendlyName"
        'lstDevices.DisplayMember = "FriendlyName"
    End Sub
#End Region

#Region "Cross Thread GUI Updates"
    '// when we get a notification about a new device - lets process it!
    Private Sub UpdateDeviceList(device As UPnPDevice, eventType As UPnPDeviceManager.eDeviceDiscoveryEvent)
        Select Case eventType
            Case UPnPDeviceManager.eDeviceDiscoveryEvent.added
                lblstatus.Text = lstDevices.Items.Count & " Devices found."
                'lstDevices.Items.Add(device)
                'lstDevices.DisplayMember = "FriendlyName"

                'If lstDevices.DataSource Is Nothing Then
                '    lstDevices.DataSource = disc.AvailableDevices
                'End If
                'lstDevices.Refresh()
                'lstDevices.Items.Add(device)
            Case UPnPDeviceManager.eDeviceDiscoveryEvent.removed
                'add code
        End Select
    End Sub

#End Region

#Region "Private Methods"

    '// Launch a search based on the requirements contained within the SearchItem
    Private Sub DeviceScan(searchAction As SearchAction)
        Select Case searchAction.SearchType
            Case MainForm.SearchAction.eSearchType.deviceIP         '// Force find a specific IP Address
                disc.NetworkScan(searchAction.Filter, True)
            Case MainForm.SearchAction.eSearchType.devicePattern    '// Scan all UPnP Devices
                If searchAction.Filter = "" Then
                    disc.NetworkScan()
                Else                                                '// Scan for a matching urn
                    disc.NetworkScan(searchAction.Filter)
                End If
        End Select

    End Sub

    '// A small function to (a) make the right click select the item below and (b) Handle the setup of the popup menu
    Private Sub PopupMenuHandler(ByVal Control As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Dim ListBox1 As ListBox = Control
        If e.Button = MouseButtons.Right And Control.Items.Count > 0 Then
            Dim n As Integer = ListBox1.IndexFromPoint(e.X, e.Y)
            If n <> ListBox.NoMatches Then
                '// if we get a match - great - else use existing so long as its not -1
                ListBox1.SelectedIndex = n
            End If
            If ListBox1.SelectedIndex <> -1 Then
                PopupMenuContextHandler(Control, e)
            Else
                ListBox1.ContextMenuStrip.Close()
            End If
        Else
            ListBox1.ContextMenuStrip.Close()
        End If

    End Sub

    '// The routine that creates a dynamic popup menu
    Private Sub PopupMenuContextHandler(ByVal control As Object, ByVal e As MouseEventArgs)
        cMenu1.Items.Clear()
        Dim menuItem1 As ToolStripMenuItem = Nothing
        Dim menuItem2 As ToolStripMenuItem = Nothing

        If control.Name = lstDevices.Name Then
            menuItem1 = New ToolStripMenuItem("Add to Managed Devices List [" & control.SelectedItem.FriendlyName & "]", Nothing, AddressOf mOnClick, "AddManagedDevice")
            menuItem2 = New ToolStripMenuItem("Show Device Description [" & control.SelectedItem.FriendlyName & "]", Nothing, AddressOf mOnClick, "ShowXML")
        ElseIf control.Name = lstManagedDevices.Name Then
            menuItem1 = New ToolStripMenuItem("Remove from Managed Devices List [" & control.SelectedItem.FriendlyName & "]", Nothing, AddressOf mOnClick, "RemoveManagedDevice")
            menuItem2 = New ToolStripMenuItem("Show Device Description [" & control.SelectedItem.FriendlyName & "]", Nothing, AddressOf mOnClick, "ShowXMLManaged")
        End If
        cMenu1.Items.AddRange({menuItem1, menuItem2})
        control.ContextMenuStrip = cMenu1
        control.ContextMenuStrip.Show(control, New Point(e.X, e.Y))
    End Sub

    '// Move devices in and out of the Managed Device List and display the description XML in a browser
    Private Sub HandleManagedDevices(menuItem1 As ToolStripMenuItem)
        Select Case menuItem1.Name
            Case "AddManagedDevice"
                Dim device As UPnPDevice = lstDevices.SelectedItem
                disc.AddManagedDevice(device)

                'If Not lstManagedDevices.Items.Contains(device) Then
                '    lstManagedDevices.Items.Add(device)
                'End If
            Case "RemoveManagedDevice"
                Dim device As UPnPDevice = lstManagedDevices.SelectedItem
                disc.RemoveManagedDevice(device)
                'lstManagedDevices.Items.Remove(device)
            Case "ShowXML"
                Dim device As UPnPDevice = lstDevices.SelectedItem
                System.Diagnostics.Process.Start(device.LocationURL)
            Case "ShowXMLManaged"
                Dim device As UPnPDevice = lstManagedDevices.SelectedItem
                System.Diagnostics.Process.Start(device.LocationURL)
        End Select
    End Sub

#End Region




    Private Sub disc_ManagedDeviceEvent(device As UPnPDevice, managedDeviceEvent As UPnPDeviceManager.eManagedDeviceEvent) Handles disc.ManagedDeviceEvent
        Select Case managedDeviceEvent
            Case UPnPDeviceManager.eManagedDeviceEvent.addDevice
                lblstatus.Text = device.FriendlyName & " added to Managed Device List"
            Case UPnPDeviceManager.eManagedDeviceEvent.invalidDevice
                lblstatus.Text = device.FriendlyName & " is not a Manageable Device"
            Case UPnPDeviceManager.eManagedDeviceEvent.removeDevice
                lblstatus.Text = device.FriendlyName & " removed Managed Device List"

        End Select
    End Sub
End Class
