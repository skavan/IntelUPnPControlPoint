
Imports OpenSource.UPnP
Imports OpenSource.UPnP.AV

Public Class Player
    Private device As UPnPDevice
    Private WithEvents mediaRenderer As UPnPDevice
    Public WithEvents avTransport As UPnPService
    Private mediaServer As UPnPDevice
    Private renderingControl As UPnPService
    Private contentDirectory As UPnPService
    Public WithEvents svc As UPnPService
    Private WithEvents avTransportChangeState As UPnPStateVariable

    Sub New(Device As UPnPDevice)
        Me.device = Device
        Init()
    End Sub

    Sub Init()
        'Dim s As UPnPService = GetService(device, "urn:upnp-org:serviceId:AVTransport")
        's.Subscribe(600, AddressOf svc_OnSubscribe)
        'svc = GetService(device, "urn:upnp-org:serviceId:AVTransport")
        'svc.Subscribe(600, AddressOf svc_OnSubscribe)
        'mediaRenderer = device.GetDevices()
        If device.DeviceURN = "urn:schemas-upnp-org:device:MediaRenderer:1" Then
            mediaRenderer = device
        Else
            mediaRenderer = device.EmbeddedDevices.FirstOrDefault(Function(d) d.DeviceURN = "urn:schemas-upnp-org:device:MediaRenderer:1")
        End If
        avTransport = mediaRenderer.GetService("urn:upnp-org:serviceId:AVTransport")
        SubscribeToEvents()
        'Dim avTransport As New AV.CpAVTransport(s)


    End Sub


    Private Sub SubscribeToEvents()
        Me.avTransport.Subscribe(600, Sub(service As UPnPService, subscribeok As Boolean)
                                          If subscribeok Then
                                              Debug.Print(avTransport.ServiceURN)
                                              Dim lastChangeStateVariable As UPnPStateVariable = service.GetStateVariableObject("LastChange")
                                              AddHandler lastChangeStateVariable.OnModified, AddressOf ChangeTriggered
                                              'lastChangeStateVariable.OnModified += New UPnPStateVariable.ModifiedHandler(Me.ChangeTriggered)
                                          End If
                                      End Sub)
    End Sub

    Private Sub ChangeTriggered(sender As UPnPStateVariable, value As Object)
        Dim newState As Object = sender.Value
        Debug.Print("Change Triggered")
        'Me.ParseChangeXML(CStr(newState))
    End Sub

    Private Sub HandleAVTransportEvent(sender As UPnPStateVariable, value As Object)
        Dim newState As Object = sender.Value
        'Me.ParseChangeXML(CStr(newState))
    End Sub
    

    Private Sub svc_OnSubscribe(service As UPnPService, SubscribeOK As Boolean) Handles svc.OnSubscribe
        If SubscribeOK Then

            Debug.Print("svc:" & service.ServiceID)
            'AddHandler service.GetStateVariableObject("LastChange").OnModified(), AddressOf GotData

            avTransportChangeState = service.GetStateVariableObject("LastChange")
            AddHandler avTransportChangeState.OnModified, AddressOf GotData

            '        			{
            '        If (!subscribeok) Then
            '		return;

            '	var lastChangeStateVariable = service.GetStateVariableObject("LastChange");
            '	lastChangeStateVariable.OnModified += ChangeTriggered;
            '});

            'lastChangeStateVariable.OnModified += New UPnPStateVariable.ModifiedHandler(Me.ChangeTriggered)


            'lastChangeStateVariable.OnModified += New UPnPStateVariable.ModifiedHandler(Me.HandleAVTransportEvent)
        End If

    End Sub
    Private Sub GotData(sender As UPnPStateVariable, NewValue As Object)
        Debug.Print("GOT DATA!!!")
    End Sub
    'Private Sub avTransport_OnResult_GetPositionInfo(sender As CpAVTransport, InstanceID As UInteger, Track As UInteger, TrackDuration As String, TrackMetaData As String, TrackURI As String, RelTime As String, AbsTime As String, RelCount As Integer, AbsCount As Integer, e As UPnPInvokeException, _Tag As Object) Handles avTransport.OnResult_GetPositionInfo
    '    Debug.Print(TrackMetaData)
    'End Sub

    'Private Sub avTransport_OnStateVariable_LastChange(sender As CpAVTransport, NewValue As String) Handles avTransport.OnStateVariable_LastChange
    '    Debug.Print("NewValue:" & NewValue)
    'End Sub

    '// a utility routine that chacks for a serviceID within a device -- walking up and down the device tree looking at parents and children.
    Public Function GetService(device As UPnPDevice, serviceID As String) As UPnPService
        If Not device.ParentDevice Is Nothing Then
            Return GetService(device.ParentDevice, serviceID)
        Else    '// now let's check for AVTransport somewhere in the tree
            For Each service As UPnPService In device.Services
                If service.ServiceID = serviceID Then
                    Return service
                End If
            Next
            For Each childDevice As UPnPDevice In device.EmbeddedDevices
                For Each service As UPnPService In childDevice.Services
                    If service.ServiceID = serviceID Then
                        Return service
                    End If
                Next
            Next

            Return Nothing
        End If
    End Function

    'Private Sub avTransportChangeState_OnModified(sender As UPnPStateVariable, NewValue As Object) Handles avTransportChangeState.OnModified
    '    Debug.Print("GOT DATA")
    'End Sub

    Private Sub svc_OnSubscriptionAdded(sender As UPnPService) Handles svc.OnSubscriptionAdded
        Debug.Print("Subscription Added")
    End Sub

    Private Sub svc_OnUPnPEvent(sender As UPnPService, SEQ As Long) Handles svc.OnUPnPEvent
        Debug.Print("CHANGE!")
    End Sub

    Private Sub avTransport_OnSubscribe(sender As UPnPService, SubscribeOK As Boolean) Handles avTransport.OnSubscribe
        Debug.Print("We are here")
    End Sub

    Private Sub avTransport_OnSubscriptionAdded(sender As UPnPService) Handles avTransport.OnSubscriptionAdded
        Debug.Print("We are here")
    End Sub

    Private Sub avTransport_OnUPnPEvent(sender As UPnPService, SEQ As Long) Handles avTransport.OnUPnPEvent
        Debug.Print("We are here")
    End Sub
End Class
