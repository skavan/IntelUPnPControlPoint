﻿Imports OpenSource.UPnP
Imports OpenSource.Utilities
Imports System.ComponentModel
Imports System.Threading

Namespace UPnPDeviceManager

    Public Enum eDeviceDiscoveryEvent
        added
        removed
        failed
    End Enum

    Enum eHandleDevice
        addDevice
        removeDevice
    End Enum

    Public Class NetworkManager
        Public Event DeviceDiscoveryEvent(device As UPnPDevice, eventType As eDeviceDiscoveryEvent)
        Private threadingContext As SynchronizationContext = SynchronizationContext.Current

        Private scp As UPnPSmartControlPoint
        Private df As UPnPDeviceFactory
        Property AvailableDevices As New BindingList(Of UPnPDevice)
        Property ManagedDevices As New BindingList(Of UPnPDevice)

#Region "Device Scan Handling Events"

        Private Sub HandleAddedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
            HandleDeviceChange(device, eHandleDevice.addDevice)
        End Sub

        Private Sub HandleRemovedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
            HandleDeviceChange(device, eHandleDevice.removeDevice)
        End Sub

        Private Sub HandleForceAddDevice(sender As OpenSource.UPnP.UPnPDeviceFactory, device As OpenSource.UPnP.UPnPDevice, URL As System.Uri)
            HandleDeviceChange(device, eHandleDevice.addDevice)
        End Sub

        Private Sub HandleForceAddFailed(sender As UPnPDeviceFactory, LocationUri As Uri, e As Exception, urn As String)
            RaiseEvent DeviceDiscoveryEvent(Nothing, eDeviceDiscoveryEvent.failed)
        End Sub


#End Region

#Region "Public Methods"

        Private Sub ResetSystem()
            Me.scp = Nothing
            AvailableDevices.Clear()
            'ManagedDevices.Clear()
        End Sub

        '// Scan Network for all devices
        Public Sub NetworkScan()
            ResetSystem()
            Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf HandleAddedDevice))
            AddHandler Me.scp.OnRemovedDevice, New UPnPSmartControlPoint.DeviceHandler(AddressOf HandleRemovedDevice)
        End Sub

        '// Scan Network for a class of device
        Public Sub NetworkScan(filter As String)
            ResetSystem()
            Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf HandleAddedDevice), filter)
        End Sub

        '// Scan Network for a Specific Device
        Public Sub NetworkScan(deviceDescriptionURL As String, bforceAddDevice As Boolean)
            If bforceAddDevice Then
                ForceAddDevice(deviceDescriptionURL)
            Else
                NetworkScan(deviceDescriptionURL)
            End If
        End Sub

        Private Sub ForceAddDevice(deviceDescriptionURL As String)
            Try
                Dim NetworkUri = New Uri(deviceDescriptionURL)
                Dim df As UPnPDeviceFactory = New UPnPDeviceFactory(NetworkUri, 1800, New UPnPDeviceFactory.UPnPDeviceHandler(AddressOf HandleForceAddDevice), New UPnPDeviceFactory.UPnPDeviceFailedHandler(AddressOf HandleForceAddFailed), Nothing, Nothing)
                'Dim df As UPnPDeviceFactory = New UPnPDeviceFactory(NetworkUri, 1800, New UPnPDeviceFactory.UPnPDeviceHandler(ForceDeviceOKSink), New UPnPDeviceFactory.UPnPDeviceFailedHandler(ForceDeviceFailSink), Nothing, Nothing)

            Catch ex_23 As Exception
                MessageBox.Show("Invalid URI!")
            End Try
        End Sub

        Public Sub AddManagedDevice(device As UPnPDevice)
            If Not ManagedDevices.Contains(device) Then
                ManagedDevices.Add(device)
            End If
        End Sub

        Public Sub RemoveManagedDevice(device As UPnPDevice)
            If ManagedDevices.Contains(device) Then
                ManagedDevices.Remove(device)
            End If
        End Sub

#End Region

#Region "Private Methods"
        Private Sub AddAvailableDevice(device As UPnPDevice)
            If Not AvailableDevices.Contains(device) Then
                AvailableDevices.Add(device)
                RaiseEvent DeviceDiscoveryEvent(device, eDeviceDiscoveryEvent.added)
            End If
        End Sub
        Private Sub RemoveAvailableDevice(device As UPnPDevice)
            '// to do
            If AvailableDevices.Contains(device) Then
                AvailableDevices.Remove(device)
                RaiseEvent DeviceDiscoveryEvent(device, eDeviceDiscoveryEvent.removed)
            End If
        End Sub
        '// this is a sub whose function is to put the add/remove device activity on the UI thread
        Private Sub HandleDeviceChange(device As UPnPDevice, deviceAction As eHandleDevice)
            Select Case deviceAction
                Case eHandleDevice.addDevice
                    'Execute DoSomethingElse on the same thread that the current object was created on.
                    threadingContext.Post(AddressOf AddAvailableDevice, device)
                Case eHandleDevice.removeDevice
                    threadingContext.Post(AddressOf RemoveAvailableDevice, device)
            End Select
        End Sub
#End Region

        Protected Overrides Sub Finalize()
            EventLogger.Enabled = False
            scp = Nothing
            MyBase.Finalize()
        End Sub

    End Class

End Namespace

