Imports OpenSource.UPnP



Public Class MainForm
    Dim WithEvents disc As New UPnPDeviceManager.NetworkManager
    Delegate Sub delUpdateDeviceList(device As UPnPDevice, eventType As UPnPDeviceManager.eDeviceDiscoveryEvent)

    Class SearchSpec
        Enum eSearchType
            devicePattern
            deviceIP
        End Enum
        Property FriendlyName As String
        Property SearchSpec As String
        Property SearchType As eSearchType
        Sub New(sSpec As String, sFriendlyName As String, iSearchType As eSearchType)
            _SearchSpec = sSpec
            _FriendlyName = sFriendlyName
            _SearchType = iSearchType
        End Sub
    End Class

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        If cmbFilter.SelectedIndex <> -1 Then
            Dim searchAction As SearchSpec = cmbFilter.SelectedItem
            Select Case searchAction.SearchType
                Case MainForm.SearchSpec.eSearchType.deviceIP
                    disc.NetworkScan(searchAction.SearchSpec, True)
                Case MainForm.SearchSpec.eSearchType.devicePattern
                    If searchAction.SearchSpec = "" Then
                        disc.NetworkScan()
                    Else
                        disc.NetworkScan(searchAction.SearchSpec)
                    End If
            End Select
        End If


    End Sub

    Private Sub disc_DeviceScanEvent(device As UPnPDevice, eventType As UPnPDeviceManager.eDeviceDiscoveryEvent) Handles disc.DeviceDiscoveryEvent
        MyBase.Invoke(New delUpdateDeviceList(AddressOf UpdateDeviceList), {device, eventType})
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Init()
    End Sub

    Private Sub Init()

        'Dim s1 As New SearchSpec("192.168.1.160", "SqueezeBox @ 192.168.1.160", SearchSpec.eSearchType.deviceIP)
        cmbFilter.Items.Clear()
        cmbFilter.DisplayMember = "FriendlyName"
        cmbFilter.Items.Add(New SearchSpec("http://192.168.1.126:9000/plugins/UPnP/MediaRenderer.xml?player=00:04:20:16:8d:51", "SqueezeBox @ 192.168.1.126", SearchSpec.eSearchType.deviceIP))
        cmbFilter.Items.Add(New SearchSpec("http://192.168.1.167:1400/xml/device_description.xml", "SONOS @ 192.168.1.167", SearchSpec.eSearchType.deviceIP))
        cmbFilter.Items.Add(New SearchSpec("", "All UPnP Devices", SearchSpec.eSearchType.devicePattern))
        cmbFilter.Items.Add(New SearchSpec("urn:schemas-upnp-org:device:MediaRenderer:1", "Media Renderers", SearchSpec.eSearchType.devicePattern))
        cmbFilter.SelectedIndex = 2


        'lstDevices.DataSource = disc.AvailableDevices
        'lstDevices.DisplayMember = "FriendlyName"
        'lstDevices.DisplayMember = "FriendlyName"
    End Sub

    Private Sub UpdateDeviceList(device As UPnPDevice, eventType As UPnPDeviceManager.eDeviceDiscoveryEvent)
        Select Case eventType
            Case UPnPDeviceManager.eDeviceDiscoveryEvent.added
                lstDevices.Items.Add(device)
                lstDevices.DisplayMember = "FriendlyName"
                'If lstDevices.DataSource Is Nothing Then
                '    lstDevices.DataSource = disc.AvailableDevices
                'End If
                'lstDevices.Refresh()
                'lstDevices.Items.Add(device)
            Case UPnPDeviceManager.eDeviceDiscoveryEvent.removed
                'add code
        End Select
    End Sub

    Private Sub lstDevices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDevices.SelectedIndexChanged
        If lstDevices.SelectedIndex <> -1 Then

            txtDetail.Text = DeviceTree(lstDevices.SelectedItem)
        End If
    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs)
        Debug.Print(disc.AvailableDevices.Count)
        lstDevices.DataSource = disc.AvailableDevices
        lstDevices.DisplayMember = "FriendlyName"

    End Sub
End Class
