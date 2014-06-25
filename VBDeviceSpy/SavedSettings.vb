﻿Imports System.Collections
Imports System.Configuration




'// A Small Class to hold a Search Spec Record
<SettingsSerializeAs(SettingsSerializeAs.Xml)>
Public Class SearchActions
    Inherits System.Collections.ObjectModel.Collection(Of SearchAction)
End Class

'// The serializable SearchSpec Class
Public Class SearchAction

    Enum eSearchType
        devicePattern
        deviceIP
    End Enum

    Public Property FriendlyName As String
    Public Property Filter As String
    Public Property SearchType As eSearchType

    '// only used for serialization purposes
    Sub New()
    End Sub

    Sub New(sSpec As String, sFriendlyName As String, iSearchType As eSearchType)
        _Filter = sSpec
        _FriendlyName = sFriendlyName
        _SearchType = iSearchType
    End Sub

    Public Function GetString(delimiter As String) As String
        Return Me.Filter & delimiter & Me.FriendlyName & delimiter & Me.SearchType
    End Function

End Class

'// A Small Class to hold the collection of saved devices
<SettingsSerializeAs(SettingsSerializeAs.Xml)>
Public Class SavedDevices
    Inherits System.Collections.ObjectModel.Collection(Of SavedDevice)
End Class

Public Class SavedDevice
    Public Property DisplayName As String
    Public Property UniqueDeviceName As String
    Public Property IsLinkedDevice As Boolean
    Public Property LinkedDeviceName As String
    Public Property DeviceCode As String

    Sub New()
    End Sub

    Sub New(displayName As String, uniqueDeviceName As String, isLinkedDevice As Boolean, linkedDevice As String)
        Init(displayName, uniqueDeviceName, isLinkedDevice, linkedDevice)
    End Sub

    Sub New(displayName As String, uniqueDeviceName As String, isLinkedDevice As Boolean, linkedDevice As String, deviceCode As String)
        Me.DeviceCode = deviceCode
        Init(displayName, uniqueDeviceName, isLinkedDevice, linkedDevice)
    End Sub

    Private Sub Init(displayName As String, uniqueDeviceName As String, isLinkedDevice As Boolean, linkedDevice As String)
        Me.DisplayName = displayName
        Me.UniqueDeviceName = uniqueDeviceName
        Me.IsLinkedDevice = isLinkedDevice
        Me.LinkedDeviceName = linkedDevice
    End Sub
End Class