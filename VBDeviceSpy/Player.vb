
Imports OpenSource.UPnP
Imports System.Threading

'Imports OpenSource.UPnP.AV

Public Class Player
    Private device As UPnPDevice
    Private WithEvents mediaRenderer As UPnPDevice
    Public WithEvents avTransport As UPnPService
    'Private mediaServer As UPnPDevice
    'Private renderingControl As UPnPService
    'Private contentDirectory As UPnPService
    Public WithEvents svc As UPnPService
    Public isSubscribed As Boolean = False
    Private WithEvents avTransportChangeState As UPnPStateVariable

    Sub New(Device As UPnPDevice)
        Me.device = Device
        Init()
    End Sub

    Sub Init()
        If device.DeviceURN = "urn:schemas-upnp-org:device:MediaRenderer:1" Then
            mediaRenderer = device
        Else
            mediaRenderer = device.EmbeddedDevices.FirstOrDefault(Function(d) d.DeviceURN = "urn:schemas-upnp-org:device:MediaRenderer:1")
        End If
        avTransport = mediaRenderer.GetService("urn:upnp-org:serviceId:AVTransport")
        SubscribeToEvents()
        'Dim avTransport As New AV.CpAVTransport(s)


    End Sub

    Private Sub Subscribe()
        If isSubscribed Then                                      'YES

            'AddHandler avTransport.OnSubscribe, AddressOf OnSubscribe

            Dim d As New UPnPService.UPnPEventSubscribeHandler(AddressOf Me.OnSubscribe)
            AddHandler avTransport.OnSubscribe, d
            '(CType(obj, UPnPService)).OnSubscribe += New UPnPService.UPnPEventSubscribeHandler(Me.HandleSubscribe)
            Dim stateVariables As UPnPStateVariable() = avTransport.GetStateVariables()
            For i As Integer = 0 To stateVariables.Length - 1
                Dim V As UPnPStateVariable = stateVariables(i)
                AddHandler V.OnModified, New UPnPStateVariable.ModifiedHandler(AddressOf Me.HandleEvents)
            Next
            avTransport.Subscribe(600, Nothing)

        Else
            RemoveHandler avTransport.OnSubscribe, New UPnPService.UPnPEventSubscribeHandler(AddressOf Me.OnSubscribe)
            Dim stateVariables As UPnPStateVariable() = avTransport.GetStateVariables()
            For i As Integer = 0 To stateVariables.Length - 1
                Dim V As UPnPStateVariable = stateVariables(i)
                If V.SendEvent Then
                    RemoveHandler V.OnModified, New UPnPStateVariable.ModifiedHandler(AddressOf Me.HandleEvents)
                End If
            Next
            avTransport.UnSubscribe(Nothing)
        End If
    End Sub

    Sub OnSubscribe(service As UPnPService, subscribeok As Boolean)
        If subscribeok Then
            Debug.Print(avTransport.ServiceURN)
            Dim lastChangeStateVariable As UPnPStateVariable = service.GetStateVariableObject("LastChange")
            AddHandler lastChangeStateVariable.OnModified, AddressOf ChangeTriggered
            isSubscribed = True
            'lastChangeStateVariable.OnModified += New UPnPStateVariable.ModifiedHandler(Me.ChangeTriggered)
        Else
            Debug.Print("Subscribe Failed")
            isSubscribed = False
        End If
    End Sub

    Protected Sub HandleEvents(sender As UPnPStateVariable, EventValue As Object)
        Debug.Print(sender.OwningService.ParentDevice.FriendlyName + "/" + sender.OwningService.ServiceID)
        Dim ev As String = UPnPService.SerializeObjectInstance(EventValue)
        If ev = "" Then
            ev = "(Empty)"
        End If

        Debug.Print(ev)
        '      If MyBase.InvokeRequired Then
        'MyBase.Invoke(New UPnPStateVariable.ModifiedHandler(Me.HandleEvents), New Object()() = { sender, EventValue })
        '      Else
        '          Dim eventSource As String = sender.OwningService.ParentDevice.FriendlyName + "/" + sender.OwningService.ServiceID
        '          Dim eventValue As String = UPnPService.SerializeObjectInstance(eventValue)
        '          If eventValue = "" Then
        '              eventValue = "(Empty)"
        '          End If
        '          Dim now As DateTime = DateTime.Now
        'Dim i As ListViewItem = New ListViewItem(New String()() = { now.ToShortTimeString(), eventSource, sender.Name, eventValue })
        '          i.Tag = now
        '          Me.eventListView.Items.Insert(0, i)
        '          If Me.deviceTree.SelectedNode IsNot Nothing Then
        '              If Me.deviceTree.SelectedNode.Tag.[GetType]() Is GetType(UPnPStateVariable) Then
        '                  If (CType(Me.deviceTree.SelectedNode.Tag, UPnPStateVariable)).SendEvent Then
        '                      If Me.deviceTree.SelectedNode.Tag.GetHashCode() = sender.GetHashCode() Then
        '                          Me.SetListInfo(Me.deviceTree.SelectedNode.Tag)
        '                      End If
        '                  End If
        '              End If
        '          End If
        '          Dim fNode As TreeNode = Me.deviceTree.Nodes(0).FirstNode
        '          While fNode IsNot Nothing
        '              Me.ScanDeviceNode(fNode, sender.OwningService)
        '              fNode = fNode.NextNode
        '          End While
        '      End If
    End Sub


    Public Sub Shutdown()
        Subscribe()
        avTransport = Nothing
    End Sub
    Private Sub OnUnsubscribe(service As UPnPService, unsubscribeok As Boolean)
        Debug.Print("Unsubscribed")
    End Sub
    Private Sub SubscribeToEvents()
        Me.avTransport.Subscribe(600, AddressOf OnSubscribe)
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
