﻿Imports OpenSource.UPnP

Public Class MainForm
    Dim WithEvents disc As New UPnPDeviceManager.NetworkManager
    Delegate Sub delUpdateDeviceList(device As UPnPDevice, eventType As UPnPDeviceManager.eDeviceDiscoveryEvent)


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
        If lstDevices.SelectedIndex > -1 Then
            If lstDevices.Items.Count > 0 Then
                txtDetail.Text = ParseDeviceTree(lstDevices.SelectedItem)
            End If

        End If
    End Sub

    '// Handles the popup menu click event
    Private Sub mOnClick(sender As Object, e As EventArgs)
        HandleManagedDevices(sender)
    End Sub

#End Region

#Region "External Event Sinks"

    '// Raised from the devcie discovery process. Normally on a non-GUI thread, hence the MyBase.Invoke.
    Private Sub disc_DeviceScanEvent(device As UPnPDevice, eventType As UPnPDeviceManager.eDeviceDiscoveryEvent) Handles disc.DeviceDiscoveryEvent
        MyBase.Invoke(New delUpdateDeviceList(AddressOf UpdateDeviceList), {device, eventType})
    End Sub

    '// Raised when we try and create a "Managed Device"
    Private Sub disc_ManagedDeviceEvent(device As UPnPDevice, managedDeviceEvent As UPnPDeviceManager.eManagedDeviceEvent) Handles disc.ManagedDeviceEvent
        Select Case managedDeviceEvent
            Case UPnPDeviceManager.eManagedDeviceEvent.addDevice
                lblstatus.Text = device.FriendlyName & " added to Managed Device List"
            Case UPnPDeviceManager.eManagedDeviceEvent.invalidDevice
                lblstatus.Text = device.FriendlyName & " is not a Manageable Device"
            Case UPnPDeviceManager.eManagedDeviceEvent.removeDevice
                lblstatus.Text = device.FriendlyName & " removed Managed Device List"
            Case UPnPDeviceManager.eManagedDeviceEvent.incompleteDevice
                lblstatus.Text = device.FriendlyName & " incomplete Managed Device List"
        End Select
    End Sub

#End Region

#Region "Initialization & Closing"

    '// save settings and cleanup
    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Dim myCol As SearchActions = My.Settings.SearchActions
        If myCol Is Nothing Then myCol = New SearchActions
        Dim mySavedDevices As New SavedDevices
        'If mySavedDevices Is Nothing Then mySavedDevices = New SavedDevices


        For Each device As UPnPDevice In disc.ManagedDevices
            '// Create a specific device entry shortcut
            Dim searchAction1 As New SearchAction(device.LocationURL, device.FriendlyName, SearchAction.eSearchType.deviceIP)
            If Not myCol.Contains(searchAction1) Then myCol.Add(searchAction1)

            ''// Create a general upnp urn entry
            'Dim searchAction2 As New SearchAction(device.DeviceURN, "*DEVICE TYPE: " & device.StandardDeviceType, SearchAction.eSearchType.devicePattern)
            'If Not myCol.Contains(searchAction2) Then myCol.Add(searchAction2)

            '// Create a saved device
            Dim savedDevice As SavedDevice
            If device.IsLinkedDevice Then
                savedDevice = New SavedDevice(device.ManagedDeviceName, device.UniqueDeviceName, True, device.LinkedDeviceName)
            Else
                savedDevice = New SavedDevice(device.ManagedDeviceName, device.UniqueDeviceName, False, "")
            End If
            If Not mySavedDevices.Contains(savedDevice) Then
                mySavedDevices.Add(savedDevice)
            End If


        Next
        My.Settings.SavedDevices = mySavedDevices
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
        cmbFilter.Items.Add(New SearchAction("urn:schemas-upnp-org:device:MediaRenderer:1", "*DEVICE TYPE: Media Renderers", SearchAction.eSearchType.devicePattern))
        cmbFilter.Items.Add(New SearchAction("urn:schemas-upnp-org:device:MediaServer:1", "*DEVICE TYPE: Media Servers", SearchAction.eSearchType.devicePattern))
        cmbFilter.Items.Add(New SearchAction("urn:schemas-upnp-org:device:ZonePlayer:1", "*DEVICE TYPE: Zone Players", SearchAction.eSearchType.devicePattern))

        Dim searchActions As SearchActions = My.Settings.SearchActions

        If Not searchActions Is Nothing Then
            For Each searchAction In My.Settings.SearchActions
                If Not cmbFilter.Items.Contains(searchAction) Then cmbFilter.Items.Add(searchAction)
            Next
        End If

        'cmbFilter.Items.Add(New SearchAction("http://192.168.1.126:9000/plugins/UPnP/MediaRenderer.xml?player=00:04:20:16:8d:51", "SqueezeBox @ 192.168.1.126", SearchAction.eSearchType.deviceIP))
        'cmbFilter.Items.Add(New SearchAction("http://192.168.1.167:1400/xml/device_description.xml", "SONOS @ 192.168.1.167", SearchAction.eSearchType.deviceIP))

        cmbFilter.Sorted = True
        cmbFilter.SelectedIndex = 0
        lstDevices.ContextMenuStrip = cMenu1
        lstManagedDevices.ContextMenuStrip = cMenu1

    End Sub
#End Region

#Region "Cross Thread GUI Updates"
    '// when we get a notification about a new device - lets process it!
    Private Sub UpdateDeviceList(device As UPnPDevice, eventType As UPnPDeviceManager.eDeviceDiscoveryEvent)
        Select Case eventType
            Case UPnPDeviceManager.eDeviceDiscoveryEvent.added
                lblstatus.Text = lstDevices.Items.Count & " Devices found."
                If lstDevices.DataSource Is Nothing Then
                    lstDevices.DataSource = disc.AvailableDevices
                    'lstDevices.DisplayMember = "FriendlyName"
                End If

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
            Case VBDeviceSpy.SearchAction.eSearchType.deviceIP         '// Force find a specific IP Address
                disc.NetworkScan(searchAction.Filter, True)
            Case VBDeviceSpy.SearchAction.eSearchType.devicePattern    '// Scan all UPnP Devices
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
                disc.AddToManagedDevices(device)
                If lstManagedDevices.DataSource Is Nothing Then
                    lstManagedDevices.DataSource = disc.ManagedDevices
                    lstManagedDevices.DisplayMember = "ManagedDeviceName"
                End If

            Case "RemoveManagedDevice"
                Dim device As UPnPDevice = lstManagedDevices.SelectedItem
                disc.RemoveFromManagedDevices(device)
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



End Class
