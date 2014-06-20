Imports OpenSource.UPnP
Imports OpenSource.Utilities

Namespace MyUPnP
    Public Enum eDeviceDiscoveryEvent
        found
        removed
    End Enum
    Public Class Discovery
        Public Event DeviceScanEvent(device As UPnPDevice, eventType As eDeviceDiscoveryEvent)
        Private scp As UPnPSmartControlPoint

        Public Sub Scan()
            Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf HandleAddedDevice))
            AddHandler Me.scp.OnRemovedDevice, New UPnPSmartControlPoint.DeviceHandler(AddressOf HandleRemovedDevice)
        End Sub

#Region "Device Scan Events"

        Private Sub HandleAddedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
            RaiseEvent DeviceScanEvent(device, eDeviceDiscoveryEvent.found)
        End Sub

        Private Sub HandleRemovedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
            RaiseEvent DeviceScanEvent(device, eDeviceDiscoveryEvent.removed)
        End Sub

#End Region

#Region "Public Methods"

#End Region

        Protected Overrides Sub Finalize()
            EventLogger.Enabled = False
            scp = Nothing
            MyBase.Finalize()
        End Sub
    End Class


    Public Class Devices

    End Class
End Namespace

