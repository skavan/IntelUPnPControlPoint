Imports OpenSource.UPnP
Imports VBDeviceSpy
Imports System.ComponentModel

Public Class frmController
    Private WithEvents logger As OpenSource.Utilities.EventLogger

    Property ManagedDevices As New BindingList(Of UPnPDevice)
    Private WithEvents player As Player

    Public Sub Init(managedDevices As BindingList(Of UPnPDevice))
        Me.ManagedDevices = managedDevices
        cmbFilter.DataSource = Me.ManagedDevices
        cmbFilter.DisplayMember = "ManagedDeviceName"
        cmbFilter.SelectedIndex = 1
        OpenSource.Utilities.EventLogger.Enabled = True
        Me.Show()
    End Sub

    Private Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        Dim device As UPnPDevice = cmbFilter.SelectedItem
        player = New Player(device)
    End Sub

    Private Sub logger_OnEvent(LogType As EventLogEntryType, origin As Object, StackTrace As String, LogMessage As String) Handles logger.OnEvent
        txtDetail.Text = txtDetail.Text & LogMessage & vbCrLf
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        player.avTransport.GetPositionInfo(0)
        Debug.Print(player.avTransport.CurrentTrackMetaData)
    End Sub
End Class