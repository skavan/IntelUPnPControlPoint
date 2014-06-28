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
        addedDevice
        addedLinkedDevice
        removedDevice
        invalidDevice
        incompleteDevice
    End Enum

    Public Enum eManagedDevicesAction
        addDevice
        addLinkedDevice
        removeDevice
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

        Public Const HAS_AVTRANSPORT As Integer = 1
        Public Const HAS_CONTENTDIRECTORY As Integer = 2
        Public Const AVTRANSPORT = "urn:upnp-org:serviceId:AVTransport"
        Public Const CONTENTDIRECTORY = "urn:upnp-org:serviceId:ContentDirectory"

#Region "Device Scan Handling Events"

        '// This is a callback that is called when a new device is found on the network
        Private Sub HandleAddedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
            HandleDeviceChange(device, eManagedDeviceEvent.addedDevice)
        End Sub

        '// This is a callback that is called when a device is removed from the network
        Private Sub HandleRemovedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
            HandleDeviceChange(device, eManagedDeviceEvent.removedDevice)
        End Sub

        '// This is a callback that is called when a Forced Add succeeds and a device is found
        Private Sub HandleForceAddDevice(sender As OpenSource.UPnP.UPnPDeviceFactory, device As OpenSource.UPnP.UPnPDevice, URL As System.Uri)
            HandleDeviceChange(device, eManagedDeviceEvent.addedDevice)
        End Sub

        '// This is a callback that is called when a Forced Add FAILS
        Private Sub HandleForceAddFailed(sender As UPnPDeviceFactory, LocationUri As Uri, e As Exception, urn As String)
            RaiseEvent DeviceDiscoveryEvent(Nothing, eDeviceDiscoveryEvent.failed)
        End Sub

        '// this is a sub whose function is to put the add/remove device activity on the UI thread and then call ADD or REMOVE Available Device on that thread.
        Private Sub HandleDeviceChange(device As UPnPDevice, deviceAction As eManagedDeviceEvent)
            Select Case deviceAction
                Case eManagedDeviceEvent.addedDevice
                    'Execute DoSomethingElse on the same thread that the current object was created on.
                    threadingContext.Post(AddressOf AddAvailableDevice, device)
                Case eManagedDeviceEvent.removedDevice
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

            If deviceDescriptionURL <> "" Then
                If bforceAddDevice Then
                    ForceAddDevice(deviceDescriptionURL)
                Else
                    NetworkScan(deviceDescriptionURL)
                End If
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

        Public Function isAvailableDevice(locationURL As String) As Boolean
            For Each device As UPnPDevice In AvailableDevices
                If device.LocationURL = locationURL Then
                    Return True
                End If
            Next
            Return False
        End Function


#End Region

#Region "ManagedDevice Methods"

        '// here's where we do a lot of clever stuff - from simply addive  acomplete device to the managed device list to
        '// adding an incomplete AVTransport device to creating a combo device and linking them and adding the linked device.
        Public Sub ManagedDevicesAction(device As UPnPDevice, Action As eManagedDevicesAction)
            Select Case Action
                Case eManagedDevicesAction.addDevice
                    Dim hasContentDirectory As Boolean = CheckForService(device, CONTENTDIRECTORY)
                    Select Case hasContentDirectory
                        Case True
                            Dim topDevice As UPnPDevice = FindTopMostDevice(device)
                            If topDevice.UniqueDeviceName <> device.UniqueDeviceName Then
                                If Not AvailableDevices.Contains(topDevice) Then
                                    ForceAddDevice(topDevice.LocationURL)
                                End If
                            End If
                            AddManagedDevice(topDevice, "COMPLETE DEVICE:" & topDevice.FriendlyName)
                            RaiseEvent ManagedDeviceEvent(topDevice, eManagedDeviceEvent.addedDevice)
                        Case False
                            AddManagedDevice(device, "TRANSPORT ONLY:" & device.FriendlyName)
                            RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.incompleteDevice)
                    End Select
                Case eManagedDevicesAction.addLinkedDevice
                    Dim linkedDevice As UPnPDevice = FindSiblingDevice(device, CONTENTDIRECTORY)
                    If Not linkedDevice Is Nothing Then
                        LinkChildDevice(device, linkedDevice)
                        AddManagedDevice(device, String.Format("LINKED DEVICE:{0}+{1}", device.FriendlyName, linkedDevice.FriendlyName))
                        RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.addedLinkedDevice)
                    Else
                        AddManagedDevice(device, "TRANSPORT ONLY:" & device.FriendlyName)
                        RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.incompleteDevice)
                    End If

                Case eManagedDevicesAction.removeDevice
                    If ManagedDevices.Contains(device) Then
                        ManagedDevices.Remove(device)                   '// Remove it from the managedlist
                        If device.IsLinkedDevice Then                    '// if its a linked device let's unlink, remove it and readd it.
                            UnlinkDevices(device)
                        Else

                        End If
                        RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.removedDevice)
                    End If


            End Select

        End Sub

        '// Walks the tree of devices and services to find an AVTransport. Without it, it's an invalid device!
        '// If we find AVTransport in a child, we still return the parent.
        'Private Sub OLDCheckValidManagedDevice(device As UPnPDevice)

        '    If device Is Nothing Then
        '        RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.invalidDevice)
        '    End If

        '    '// The first thing to do is see if this is a complete media device, and ocntains a ContentDirectory And a Transport
        '    '// Let's use the .User field to hold the value
        '    If CheckForService(device, CONTENTDIRECTORY) Then device.User = HAS_CONTENTDIRECTORY
        '    If CheckForService(device, AVTRANSPORT) Then device.User = device.User + HAS_AVTRANSPORT

        '    Select Case device.User
        '        Case (HAS_CONTENTDIRECTORY + HAS_AVTRANSPORT)
        '            '// we've found a complete device. Add it.
        '            Dim topDevice As UPnPDevice = FindTopMostDevice(device)
        '            If topDevice.UniqueDeviceName <> device.UniqueDeviceName Then
        '                ForceAddDevice(topDevice.LocationURL)
        '            End If
        '            AddManagedDevice(topDevice, topDevice.FriendlyName)
        '            RaiseEvent ManagedDeviceEvent(topDevice, eManagedDeviceEvent.addedDevice)
        '        Case HAS_CONTENTDIRECTORY
        '            '// find matching AVTransport
        '            '// create child-parent
        '            '// parent is always AVTransport - incase we have multiple transports feeding off a single CONTENT-DIR
        '            '// If we can't find a Transport - then EXIT with failure.
        '            Dim linkedDevice As UPnPDevice = FindSiblingDevice(device, AVTRANSPORT)
        '            If Not linkedDevice Is Nothing Then
        '                LinkChildDevice(device, linkedDevice)
        '                AddManagedDevice(device, String.Format("{{{0}|{1}}}", device.FriendlyName, linkedDevice.FriendlyName))
        '            Else
        '                RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.incompleteDevice)
        '            End If
        '        Case HAS_AVTRANSPORT
        '            '// find matching ContentDirectory
        '            '// create child-parent
        '            '// parent is always AVTransport - incase we have multiple transports feeding off a single CONTENT-DIR
        '            '// If we can't find a CONTENT-DIR, then Add the AVTransport as an Incomplete Device.
        '            Dim linkedDevice As UPnPDevice = FindSiblingDevice(device, CONTENTDIRECTORY)
        '            If Not linkedDevice Is Nothing Then
        '                LinkChildDevice(linkedDevice, device)
        '                AddManagedDevice(linkedDevice, String.Format("{{{0}|{1}}}", linkedDevice.FriendlyName, device.FriendlyName))
        '            Else
        '                AddManagedDevice(device, device.FriendlyName)
        '                RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.incompleteDevice)
        '            End If
        '        Case Else
        '            '//Invalid Device
        '            RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.invalidDevice)
        '    End Select
        'End Sub

        '// add a device to the managed device collection and setup the User2 variable with a descriptive name
        Private Sub AddManagedDevice(device As UPnPDevice, description As String)
            device.ManagedDeviceName = description
            If Not isManagedDevice(device.LocationURL) Then
                ManagedDevices.Add(device)
                RaiseEvent ManagedDeviceEvent(device, eManagedDeviceEvent.addedDevice)
            End If
        End Sub

        '// given a device and a ServiceID, go find another device with the same ipaddress that has the target ServiceID in its tree.
        Public Function FindSiblingDevice(device As UPnPDevice, targetServiceID As String) As UPnPDevice
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
        Public Function CheckForService(device As UPnPDevice, serviceID As String) As Boolean
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
        Private Sub LinkChildDevice(ParentDevice As UPnPDevice, ChildDevice As UPnPDevice)
            ParentDevice.AddDevice(ChildDevice)
            ParentDevice.IsLinkedDevice = True
            ParentDevice.LinkedLocationURL = ChildDevice.LocationURL
            ChildDevice.LinkedLocationURL = ParentDevice.LocationURL
            ChildDevice.IsLinkedDevice = True
        End Sub

        Private Sub UnlinkDevices(device As UPnPDevice)
            Dim childDevice As UPnPDevice
            Dim childURL As String = ""
            Dim parentURL As String = device.LocationURL                                            '// Get the location uri

            childDevice = getAvailableDevice(device.LinkedLocationURL)                              '// get the child device by LocationURL

            If childDevice IsNot Nothing Then                                                       '// if it exists, remove it, kill it
                childURL = childDevice.LocationURL
                If isAvailableDevice(childDevice.LocationURL) Then AvailableDevices.Remove(childDevice)
                Try
                    scp.ForceDisposeDevice(childDevice)                                             '// Kill the childdevice
                Catch ex As Exception
                End Try
            End If

            If isAvailableDevice(device.LocationURL) Then AvailableDevices.Remove(device)
            Try
                scp.ForceDisposeDevice(device)                                                  '//KIll the parent
            Catch ex As Exception

            End Try

            ForceAddDevice(parentURL)
            ForceAddDevice(childURL)


            'For Each childDevice In device.EmbeddedDevices
            '    If childDevice.LocationURL = device.LinkedLocationURL Then          '// find the linked device

            '        Dim childURL As String = childDevice.LocationURL
            '        If isAvailableDevice(childDevice.LocationURL) Then AvailableDevices.Remove(childDevice)
            '        'If AvailableDevices.Contains(childDevice) Then AvailableDevices.Remove(childDevice)
            '        'If AvailableDevices.Contains(device) Then AvailableDevices.Remove(device)
            '        Try
            '            scp.ForceDisposeDevice(childDevice)                                 '// Kill the childdevice
            '        Catch ex As Exception
            '        End Try
            '        'ForceAddDevice(parentURL)
            '        'ForceAddDevice(childURL)
            '        Exit For
            '    End If
            'Next


            


        End Sub

        Public Function isManagedDevice(locationURL As String) As Boolean
            For Each device As UPnPDevice In ManagedDevices
                If device.LocationURL = locationURL Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function getAvailableDevice(locationURL As String) As UPnPDevice
            For Each device As UPnPDevice In AvailableDevices
                If device.LocationURL = locationURL Then
                    Return device
                End If
            Next
            Return Nothing
        End Function
#End Region

        Protected Overrides Sub Finalize()
            If df IsNot Nothing Then
                df.Shutdown()
            End If
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

