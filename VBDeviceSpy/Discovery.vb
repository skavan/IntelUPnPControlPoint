Imports OpenSource.UPnP
Imports OpenSource.Utilities
Imports System.ComponentModel
Imports System.Threading

Namespace UPnPDeviceManager

    Public Enum eDeviceDiscoveryEvent
        added
        removed
        failed
    End Enum

    Public Enum eManagedDeviceEvent
        addDevice
        removeDevice
        invalidDevice
        incompleteDevice
    End Enum


    ''' <summary>
    ''' The Network Manager Class performs 3 important functions. (1) To Scan the Network for all or specific classes of device. (2) To try and force load a device and (3) To Create "ManagedDevices" including the concept of a LinkedDevice
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NetworkManager
        Public Event DeviceDiscoveryEvent(device As UPnPDevice, eventType As eDeviceDiscoveryEvent)
        Public Event ManagedDeviceEvent(device As UPnPDevice, managedDeviceEvent As eManagedDeviceEvent)
        Private threadingContext As SynchronizationContext = SynchronizationContext.Current

        Private scp As UPnPSmartControlPoint
        Private df As UPnPDeviceFactory
        Property AvailableDevices As New BindingList(Of UPnPDevice)
        Property ManagedDevices As New BindingList(Of UPnPDevice)

        Const HAS_AVTRANSPORT As Integer = 1
        Const HAS_CONTENTDIRECTORY As Integer = 2
        Const AVTRANSPORT = "urn:upnp-org:serviceId:AVTransport"
        Const CONTENTDIRECTORY = "urn:upnp-org:serviceId:ContentDirectory"

#Region "Device Scan Handling Events"

        '// This is a callback that is called when a new device is found on the network
        Private Sub HandleAddedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
            HandleDeviceChange(device, eManagedDeviceEvent.addDevice)
        End Sub

        '// This is a callback that is called when a device is removed from the network
        Private Sub HandleRemovedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
            HandleDeviceChange(device, eManagedDeviceEvent.removeDevice)
        End Sub

        '// This is a callback that is called when a Forced Add succeeds and a device is found
        Private Sub HandleForceAddDevice(sender As OpenSource.UPnP.UPnPDeviceFactory, device As OpenSource.UPnP.UPnPDevice, URL As System.Uri)
            HandleDeviceChange(device, eManagedDeviceEvent.addDevice)
        End Sub

        '// This is a callback that is called when a Forced Add FAILS
        Private Sub HandleForceAddFailed(sender As UPnPDeviceFactory, LocationUri As Uri, e As Exception, urn As String)
            RaiseEvent DeviceDiscoveryEvent(Nothing, eDeviceDiscoveryEvent.failed)
        End Sub

        '// this is a sub whose function is to put the add/remove device activity on the UI thread and then call ADD or REMOVE Available Device on that thread.
        Private Sub HandleDeviceChange(device As UPnPDevice, deviceAction As eManagedDeviceEvent)
            Select Case deviceAction
                Case eManagedDeviceEvent.addDevice
                    'Execute DoSomethingElse on the same thread that the current object was created on.
                    threadingContext.Post(AddressOf AddAvailableDevice, device)
                Case eManagedDeviceEvent.removeDevice
                    threadingContext.Post(AddressOf RemoveAvailableDevice, device)
            End Select
        End Sub

        '// This is where we actually Add a Device to the system, by placing it in the availabledevices list.
        Private Sub AddAvailableDevice(device As UPnPDevice)
            If Not AvailableDevices.Contains(device) Then
                AvailableDevices.Add(device)
                RaiseEvent DeviceDiscoveryEvent(device, eDeviceDiscoveryEvent.added)
            End If
        End Sub

        '// This is where we actually Remove a Device to the system, by taking it out of the availabledevices list.
        Private Sub RemoveAvailableDevice(device As UPnPDevice)
            '// to do
            If AvailableDevices.Contains(device) Then
                AvailableDevices.Remove(device)
                RaiseEvent DeviceDiscoveryEvent(device, eDeviceDiscoveryEvent.removed)
            End If
        End Sub

#End Region

#Region "Device Discovery"

        '// ResetSystem
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

        '// Try and force add a known device
        Private Sub ForceAddDevice(deviceDescriptionURL As String)
            Try
                Dim NetworkUri = New Uri(deviceDescriptionURL)
                Dim df As UPnPDeviceFactory = New UPnPDeviceFactory(NetworkUri, 1800, New UPnPDeviceFactory.UPnPDeviceHandler(AddressOf HandleForceAddDevice), New UPnPDeviceFactory.UPnPDeviceFailedHandler(AddressOf HandleForceAddFailed), Nothing, Nothing)
                'Dim df As UPnPDeviceFactory = New UPnPDeviceFactory(NetworkUri, 1800, New UPnPDeviceFactory.UPnPDeviceHandler(ForceDeviceOKSink), New UPnPDeviceFactory.UPnPDeviceFailedHandler(ForceDeviceFailSink), Nothing, Nothing)

            Catch ex_23 As Exception
                MessageBox.Show("Invalid URI!")
            End Try
        End Sub

#End Region

#Region "ManagedDevice Methods"

        '// public function to try and add a device to the manages device list
        Public Sub AddToManagedDevices(device As UPnPDevice)
            If Not device Is Nothing Then
                If Not ManagedDevices.Contains(device) Then
                    CheckValidManagedDevice(device)
                End If
            End If
        End Sub

        '// public function to remove a device to the manages device list
        Public Sub RemoveFromManagedDevices(device As UPnPDevice)
            If ManagedDevices.Contains(device) Then
                ManagedDevices.Remove(device)
                RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.removeDevice)
            End If
        End Sub

        '// Walks the tree of devices and services to find an AVTransport. Without it, it's an invalid device!
        '// If we find AVTransport in a child, we still return the parent.
        Private Sub CheckValidManagedDevice(device As UPnPDevice)

            If device Is Nothing Then
                RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.invalidDevice)
            End If

            '// The first thing to do is see if this is a complete media device, and ocntains a ContentDirectory And a Transport
            '// Let's use the .User field to hold the value
            If CheckForService(device, CONTENTDIRECTORY) Then device.User = HAS_CONTENTDIRECTORY
            If CheckForService(device, AVTRANSPORT) Then device.User = device.User + HAS_AVTRANSPORT

            Select Case device.User
                Case (HAS_CONTENTDIRECTORY + HAS_AVTRANSPORT)
                    '// we've found a complete device. Add it.
                    Dim topDevice As UPnPDevice = FindTopMostDevice(device)
                    If topDevice.UniqueDeviceName <> device.UniqueDeviceName Then
                        ForceAddDevice(topDevice.LocationURL)
                    End If
                    AddManagedDevice(topDevice, topDevice.FriendlyName)
                    RaiseEvent ManagedDeviceEvent(topDevice, eManagedDeviceEvent.addDevice)
                Case HAS_CONTENTDIRECTORY
                    '// find matching AVTransport
                    '// create child-parent
                    '// parent is always contentdir
                    Dim linkedDevice As UPnPDevice = FindSiblingDevice(device, AVTRANSPORT)
                    If Not linkedDevice Is Nothing Then
                        LinkChildDevice(device, linkedDevice)
                        AddManagedDevice(device, String.Format("{{{0}|{1}}}", device.FriendlyName, linkedDevice.FriendlyName))
                    Else
                        RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.incompleteDevice)
                    End If
                Case HAS_AVTRANSPORT
                    '// find matching ContentDirectory
                    '// create child-parent
                    '// parent is always contentdir
                    Dim linkedDevice As UPnPDevice = FindSiblingDevice(device, CONTENTDIRECTORY)
                    If Not linkedDevice Is Nothing Then
                        LinkChildDevice(linkedDevice, device)
                        AddManagedDevice(linkedDevice, String.Format("{{{0}|{1}}}", linkedDevice.FriendlyName, device.FriendlyName))
                    Else
                        RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.incompleteDevice)
                    End If
                Case Else
                    '//Invalid Device
                    RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.invalidDevice)
            End Select
        End Sub

        '// add a device to the managed device collection and setup the User2 variable with a descriptive name
        Private Sub AddManagedDevice(device As UPnPDevice, description As String)
            device.ManagedDeviceName = "MD:" & description
            If Not ManagedDevices.Contains(device) Then
                ManagedDevices.Add(device)
                RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.addDevice)
            End If
        End Sub

        '// given a device and a ServiceID, go find another device with the same ipaddress that has the target ServiceID in its tree.
        Private Function FindSiblingDevice(device As UPnPDevice, targetServiceID As String) As UPnPDevice
            For Each targetDevice As UPnPDevice In AvailableDevices
                If targetDevice.RemoteEndPoint.ToString = device.RemoteEndPoint.ToString Then
                    If targetDevice.UniqueDeviceName <> device.UniqueDeviceName Then
                        If CheckForService(targetDevice, targetServiceID) Then
                            Return targetDevice
                            Exit For
                        Else
                            '// we've found ourself! skip to next.
                        End If
                    End If
                End If
            Next
            Return Nothing
        End Function

        '// a utility routine that chacks for a serviceID within a device -- walking up and down the device tree looking at parents and children.
        Private Function CheckForService(device As UPnPDevice, serviceID As String) As Boolean
            If Not device.ParentDevice Is Nothing Then
                Return CheckForService(device.ParentDevice, serviceID)
            Else    '// now let's check for AVTransport somewhere in the tree
                For Each service As UPnPService In device.Services
                    If service.ServiceID = serviceID Then
                        Return True
                    End If
                Next
                For Each childDevice As UPnPDevice In device.EmbeddedDevices
                    For Each service As UPnPService In childDevice.Services
                        If service.ServiceID = serviceID Then
                            Return True
                        End If
                    Next
                Next

                Return False
            End If
        End Function

        Private Function FindTopMostDevice(device As UPnPDevice) As UPnPDevice
            If Not device.ParentDevice Is Nothing Then
                Return FindTopMostDevice(device.ParentDevice)
            Else
                Return device
            End If
        End Function

        '// add a device as a child to its parent
        Public Sub LinkChildDevice(ParentDevice As UPnPDevice, ChildDevice As UPnPDevice)
            ParentDevice.AddDevice(ChildDevice)
            ParentDevice.IsLinkedDevice = True
            ParentDevice.LinkedDeviceName = ChildDevice.UniqueDeviceName
            ChildDevice.LinkedDeviceName = ParentDevice.UniqueDeviceName
            ChildDevice.IsLinkedDevice = True
        End Sub

#End Region

        Protected Overrides Sub Finalize()
            If Not scp Is Nothing Then
                For Each device In scp.Devices
                    If Not device Is Nothing Then
                        scp.ForceDisposeDevice(device)
                    End If
                Next
                scp = Nothing
            End If
            EventLogger.Enabled = False
            MyBase.Finalize()
        End Sub

    End Class

End Namespace

