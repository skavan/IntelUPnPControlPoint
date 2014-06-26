
Imports OpenSource.UPnP
Imports OpenSource.UPnP.AV

Public Class Player
    Private device As UPnPDevice
    Private mediaRenderer As UPnPDevice
    Public WithEvents avTransport As AV.CpAVTransport
    Private mediaServer As UPnPDevice
    Private renderingControl As UPnPService
    Private contentDirectory As UPnPService
    Private WithEvents svc As UPnPService
    Private WithEvents avTransportChangeState As UPnPStateVariable

    Sub New(Device As UPnPDevice)
        Me.device = Device
        Init()
    End Sub

    Sub Init()
        Dim s As UPnPService = GetService(device, "urn:upnp-org:serviceId:AVTransport")
        s.Subscribe(600, AddressOf svc_OnSubscribe)

        Dim avTransport As New AV.CpAVTransport(s)

        'avTransport = device.GetService("urn:upnp-org:serviceId:AVTransport")
    End Sub

    Private Sub HandleAVTransportEvent(sender As UPnPStateVariable, value As Object)
        Dim newState As Object = sender.Value
        'Me.ParseChangeXML(CStr(newState))
    End Sub
    

    Private Sub svc_OnSubscribe(service As UPnPService, SubscribeOK As Boolean) Handles svc.OnSubscribe
        If SubscribeOK Then
            avTransportChangeState = service.GetStateVariableObject("LastChange")
            Debug.Print("svc:" & service.ServiceID)
            AddHandler avTransportChangeState.OnModified, AddressOf GotData


            'lastChangeStateVariable.OnModified += New UPnPStateVariable.ModifiedHandler(Me.ChangeTriggered)


            'lastChangeStateVariable.OnModified += New UPnPStateVariable.ModifiedHandler(Me.HandleAVTransportEvent)
        End If

    End Sub
    Private Sub GotData(sender As UPnPStateVariable, NewValue As Object)
        Debug.Print("GOT DATA!!!")
    End Sub
    Private Sub avTransport_OnResult_GetPositionInfo(sender As CpAVTransport, InstanceID As UInteger, Track As UInteger, TrackDuration As String, TrackMetaData As String, TrackURI As String, RelTime As String, AbsTime As String, RelCount As Integer, AbsCount As Integer, e As UPnPInvokeException, _Tag As Object) Handles avTransport.OnResult_GetPositionInfo
        Debug.Print(TrackMetaData)
    End Sub

    Private Sub avTransport_OnStateVariable_LastChange(sender As CpAVTransport, NewValue As String) Handles avTransport.OnStateVariable_LastChange
        Debug.Print("NewValue:" & NewValue)
    End Sub

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

    Private Sub avTransportChangeState_OnModified(sender As UPnPStateVariable, NewValue As Object) Handles avTransportChangeState.OnModified
        Debug.Print("GOT DATA")
    End Sub
End Class
