Imports OpenSource.UPnP

Module modUtils
    Public Function ParseDeviceTree(device As UPnPDevice) As String
        Dim tree As String
        tree = device.FriendlyName & " | " & device.LocationURL & vbCrLf
        tree += ScanDevice(device, 0)
        Return tree
    End Function

    Private Function ScanDevice(device As UPnPDevice, tabLevel As Integer) As String
        Dim szTree As String
        For Each service As UPnPService In device.Services
            szTree += AddService(service, tabLevel)
        Next
        For Each childDevice As UPnPDevice In device.EmbeddedDevices
            szTree += AddDevice(childDevice, tabLevel)
            szTree += ScanDevice(childDevice, tabLevel + 1)
        Next
        Return szTree
    End Function

    Private Function AddDevice(device As UPnPDevice, tabLevel As Integer) As String
        Dim szDevice As String = Indent(tabLevel) & device.FriendlyName & vbCrLf
        Return szDevice
    End Function

    Private Function AddService(service As UPnPService, tabLevel As Integer) As String
        Dim szService As String = Indent(tabLevel) & service.ServiceURN & vbCrLf        
        Return szService
    End Function

    Private Function Indent(level As Integer)
        Dim szIndent As String
        For i = 0 To level
            szIndent += vbTab
        Next
        Return szIndent
    End Function
End Module
