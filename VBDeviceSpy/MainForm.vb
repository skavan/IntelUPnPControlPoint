Imports OpenSource.UPnP



Public Class MainForm
    Dim WithEvents disc As New MyUPnP.Discovery
    Delegate Sub delUpdateDeviceList(device As UPnPDevice, eventType As MyUPnP.eDeviceDiscoveryEvent)

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        disc.Scan()
    End Sub

    Private Sub disc_DeviceScanEvent(device As UPnPDevice, eventType As MyUPnP.eDeviceDiscoveryEvent) Handles disc.DeviceScanEvent
        MyBase.Invoke(New delUpdateDeviceList(AddressOf UpdateDeviceList), {device, eventType})
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Init()
    End Sub

    Private Sub Init()
        lstDevices.DisplayMember = "FriendlyName"
    End Sub

    Private Sub UpdateDeviceList(device As UPnPDevice, eventType As MyUPnP.eDeviceDiscoveryEvent)
        Select Case eventType
            Case MyUPnP.eDeviceDiscoveryEvent.found
                lstDevices.Items.Add(device)
            Case MyUPnP.eDeviceDiscoveryEvent.removed
                'add code
        End Select
    End Sub

    Private Sub lstDevices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDevices.SelectedIndexChanged
        If lstDevices.SelectedIndex <> -1 Then

            txtDetail.Text = DeviceTree(lstDevices.SelectedItem)
        End If
    End Sub
End Class
