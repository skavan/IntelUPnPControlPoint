Imports OpenSource.UPnP
Imports OpenSource.Utilities


Public Class frmSysInfo


    Public Delegate Sub UpdateTreeDelegate(node As TreeNode)
    Private SubscribeTime As Integer = 300
    Protected scp As UPnPSmartControlPoint
    Protected UPnpRoot As TreeNode = New TreeNode("Devices", 0, 0)
    Private ForceDeviceList As Hashtable = New Hashtable()
    Private statusBar As StatusBar
    Private splitter1 As Splitter
    Private treeImageList As ImageList
    'Private deviceTree As TreeView
    'Private listInfo As ListView

#Region "Init & Cleanup"

    Sub InitSystem()
        Dim args As String() = {"/DEBUG", ""}

        Me.deviceTree.Nodes.Add(Me.UPnpRoot)
        For i As Integer = 0 To args.Length - 1
            Dim parm As String = args(i)
            If parm.ToUpper() = "/DEBUG" Then
                InstanceTracker.Enabled = True
                EventLogger.Enabled = True
                EventLogger.ShowAll = True
                InstanceTracker.Display()
            End If
            If parm.ToUpper().StartsWith("/ST:") Then
                Dim p As DText = New DText()
                p.ATTRMARK = ":"
                p(0) = parm
                Try
                    Me.SubscribeTime = Integer.Parse(p(2))
                Catch ex_DA As Exception
                End Try
            End If
        Next
        Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf HandleAddedDevice))
        AddHandler Me.scp.OnRemovedDevice, New UPnPSmartControlPoint.DeviceHandler(AddressOf HandleRemovedDevice)

        'Me.scp.OnRemovedDevice += New UPnPSmartControlPoint.DeviceHandler(AddressOf HandleRemovedDevice)

    End Sub

    Sub CleanUp()
        EventLogger.Enabled = False
        scp = Nothing
        'If Me.components IsNot Nothing Then
        '    Me.components.Dispose()
        'End If
    End Sub

#End Region
    
    Protected Sub HandleCreate(device As UPnPDevice, URL As Uri)
        Dim TempList As SortedList = New SortedList()
        Dim Parent As TreeNode = New TreeNode(device.FriendlyName, 1, 1)
        Parent.Tag = device

        For cid As Integer = 0 To device.Services.Length - 1
            Dim Child As TreeNode = New TreeNode(device.Services(cid).ServiceURN, 2, 2)
            Child.Tag = device.Services(cid)
            Dim stateVarNode As TreeNode = New TreeNode("State variables", 6, 6)
            Child.Nodes.Add(stateVarNode)
            Dim varList As UPnPStateVariable() = device.Services(cid).GetStateVariables()
            TempList.Clear()
            Dim array As UPnPStateVariable() = varList
            For i As Integer = 0 To array.Length - 1
                Dim var As UPnPStateVariable = array(i)
                Dim varNode As TreeNode = New TreeNode(var.Name, 5, 5)
                varNode.Tag = var
                TempList.Add(var.Name, varNode)
            Next
            Dim sve As IDictionaryEnumerator = TempList.GetEnumerator()
            While sve.MoveNext()
                stateVarNode.Nodes.Add(CType(sve.Value, TreeNode))
            End While
            TempList.Clear()
            Dim actions As UPnPAction() = device.Services(cid).GetActions()
            For i As Integer = 0 To actions.Length - 1
                Dim action As UPnPAction = actions(i)
                Dim argsstr As String = ""
                Dim argumentList As UPnPArgument() = action.ArgumentList
                For j As Integer = 0 To argumentList.Length - 1
                    Dim arg As UPnPArgument = argumentList(j)
                    If Not arg.IsReturnValue Then
                        If argsstr <> "" Then
                            argsstr += ", "
                        End If
                        argsstr = argsstr + arg.RelatedStateVar.ValueType + " " + arg.Name
                    End If
                Next
                Dim methodNode As TreeNode = New TreeNode(action.Name + "(" + argsstr + ")", 4, 4)
                methodNode.Tag = action
                TempList.Add(action.Name, methodNode)
            Next
            Dim ide As IDictionaryEnumerator = TempList.GetEnumerator()
            While ide.MoveNext()
                Child.Nodes.Add(CType(ide.Value, TreeNode))
            End While
            Parent.Nodes.Add(Child)
        Next
        For cid As Integer = 0 To device.EmbeddedDevices.Length - 1
            Dim Child As TreeNode = Me.ProcessEmbeddedDevice(device.EmbeddedDevices(cid))
            Child.Tag = device.EmbeddedDevices(cid)
            Parent.Nodes.Add(Child)
        Next
        'Dim args As Object() = New Object()() {Parent}
        MyBase.Invoke(New UpdateTreeDelegate(AddressOf Me.HandleTreeUpdate), Parent)
    End Sub

    Protected Function ProcessEmbeddedDevice(device As UPnPDevice) As TreeNode
        Dim TempList As SortedList = New SortedList()
        Dim Parent As TreeNode = New TreeNode(device.FriendlyName, 1, 1)
        Parent.Tag = device
        For cid As Integer = 0 To device.Services.Length - 1
            Dim Child As TreeNode = New TreeNode(device.Services(cid).ServiceURN, 2, 2)
            Child.Tag = device.Services(cid)
            Dim stateVarNode As TreeNode = New TreeNode("State variables", 6, 6)
            Child.Nodes.Add(stateVarNode)
            Dim varList As UPnPStateVariable() = device.Services(cid).GetStateVariables()
            TempList.Clear()
            Dim array As UPnPStateVariable() = varList
            For i As Integer = 0 To array.Length - 1
                Dim var As UPnPStateVariable = array(i)
                Dim varNode As TreeNode = New TreeNode(var.Name, 5, 5)
                varNode.Tag = var
                TempList.Add(var.Name, varNode)
            Next
            Dim sve As IDictionaryEnumerator = TempList.GetEnumerator()
            While sve.MoveNext()
                stateVarNode.Nodes.Add(CType(sve.Value, TreeNode))
            End While
            TempList.Clear()
            Dim actions As UPnPAction() = device.Services(cid).GetActions()
            For i As Integer = 0 To actions.Length - 1
                Dim action As UPnPAction = actions(i)
                Dim argsstr As String = ""
                Dim argumentList As UPnPArgument() = action.ArgumentList
                For j As Integer = 0 To argumentList.Length - 1
                    Dim arg As UPnPArgument = argumentList(j)
                    If Not arg.IsReturnValue Then
                        If argsstr <> "" Then
                            argsstr += ", "
                        End If
                        argsstr = argsstr + arg.RelatedStateVar.ValueType + " " + arg.Name
                    End If
                Next
                Dim methodNode As TreeNode = New TreeNode(action.Name + "(" + argsstr + ")", 4, 4)
                methodNode.Tag = action
                TempList.Add(action.Name, methodNode)
            Next
            Dim ide As IDictionaryEnumerator = TempList.GetEnumerator()
            While ide.MoveNext()
                Child.Nodes.Add(CType(ide.Value, TreeNode))
            End While
            Parent.Nodes.Add(Child)
        Next
        For cid As Integer = 0 To device.EmbeddedDevices.Length - 1
            Dim Child As TreeNode = Me.ProcessEmbeddedDevice(device.EmbeddedDevices(cid))
            Child.Tag = device.EmbeddedDevices(cid)
            Parent.Nodes.Add(Child)
        Next
        Return Parent
    End Function

    Protected Sub HandleTreeUpdate(node As TreeNode)
        If Me.UPnpRoot.Nodes.Count = 0 Then
            Me.UPnpRoot.Nodes.Add(node)
        Else
            For i As Integer = 0 To Me.UPnpRoot.Nodes.Count - 1
                If Me.UPnpRoot.Nodes(i).Text.CompareTo(node.Text) > 0 Then
                    Me.UPnpRoot.Nodes.Insert(i, node)
                    Exit For
                End If
                If i = Me.UPnpRoot.Nodes.Count - 1 Then
                    Me.UPnpRoot.Nodes.Add(node)
                    Exit For
                End If
            Next
        End If
        Me.UPnpRoot.Expand()
    End Sub

    Protected Sub HandleExpiredDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
        Dim cnt As Integer = Me.UPnpRoot.Nodes.Count
        For x As Integer = 0 To cnt - 1
            If Me.UPnpRoot.Nodes(x).Tag.GetHashCode() = device.GetHashCode() Then
                Me.UPnpRoot.Nodes.RemoveAt(x)
                Exit For
            End If
        Next
        MessageBox.Show("Expired: " + device.FriendlyName)
    End Sub

    Protected Sub HandleRemovedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
        MyBase.Invoke(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleRemovedDeviceEx), {sender, device})
    End Sub

    Protected Sub HandleRemovedDeviceEx(sender As UPnPSmartControlPoint, device As UPnPDevice)
        Dim TempList As ArrayList = New ArrayList()
        Dim en As IEnumerator = Me.UPnpRoot.Nodes.GetEnumerator()
        While en.MoveNext()
            Dim tn As TreeNode = CType(en.Current, TreeNode)
            If (CType(tn.Tag, UPnPDevice)).UniqueDeviceName = device.UniqueDeviceName Then
                TempList.Add(tn)
            End If
        End While
        For x As Integer = 0 To TempList.Count - 1
            Dim i As TreeNode = CType(TempList(x), TreeNode)
            Me.CleanTags(i)
            Me.UPnpRoot.Nodes.Remove(i)
        Next
    End Sub

    Private Sub CleanTags(n As TreeNode)
        n.Tag = Nothing
        For Each sn As TreeNode In n.Nodes
            Me.CleanTags(sn)
        Next
    End Sub

    Protected Sub HandleAddedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
        Me.HandleCreate(device, device.BaseURL)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        InitSystem()
    End Sub

    Private Sub frmSysInfo_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CleanUp()
    End Sub

    Protected Sub SetListInfo(infoObject As Object)
        Dim Items As ArrayList = New ArrayList()
        If infoObject Is Nothing Then
            Items.Add(New ListViewItem(New String() {"Product name", "Device Spy"}))
            Items.Add(New ListViewItem(New String() {"Version", AutoUpdate.VersionString}))
        Else
            If infoObject.[GetType]() Is GetType(UPnPDevice) Then
                Dim d As UPnPDevice = CType(infoObject, UPnPDevice)
                Items.Add(New ListViewItem(New String() {"Friendly name", d.FriendlyName}))
                Items.Add(New ListViewItem(New String() {"Unique device name", d.UniqueDeviceName}))
                Items.Add(New ListViewItem(New String() {"Has presentation", d.HasPresentation.ToString()}))
                Items.Add(New ListViewItem(New String() {"Manufacturer", d.Manufacturer}))
                Items.Add(New ListViewItem(New String() {"Manufacturer URL", d.ManufacturerURL}))
                Items.Add(New ListViewItem(New String() {"Model description", d.ModelDescription}))
                Items.Add(New ListViewItem(New String() {"Model name", d.ModelName}))
                Items.Add(New ListViewItem(New String() {"Model number", d.ModelNumber}))
                If d.ModelURL IsNot Nothing Then
                    Items.Add(New ListViewItem(New String() {"Model URL", d.ModelURL.ToString()}))
                End If
                Items.Add(New ListViewItem(New String() {"Product code", d.ProductCode}))
                Items.Add(New ListViewItem(New String() {"Proprietary type", d.ProprietaryDeviceType}))
                Items.Add(New ListViewItem(New String() {"Serial number", d.SerialNumber}))
                Items.Add(New ListViewItem(New String() {"Services", d.Services.Length.ToString()}))
                Items.Add(New ListViewItem(New String() {"Embedded devices", d.EmbeddedDevices.Length.ToString()}))
                Items.Add(New ListViewItem(New String() {"Base URL", d.BaseURL.ToString()}))
                Items.Add(New ListViewItem(New String() {"Device URN", d.DeviceURN}))
                Items.Add(New ListViewItem(New String() {"Expiration timeout", d.ExpirationTimeout.ToString()}))
                Items.Add(New ListViewItem(New String() {"Version", d.Major.ToString() + "." + d.Minor.ToString()}))
                Items.Add(New ListViewItem(New String() {"Remote endpoint", d.RemoteEndPoint.ToString()}))
                Items.Add(New ListViewItem(New String() {"Standard type", d.StandardDeviceType}))
                If d.Icon IsNot Nothing Then
                    Items.Add(New ListViewItem(New String() {"Device icon", String.Concat(New Object() {"Present, ", d.Icon.Width, "x", d.Icon.Height})}))
                Else
                    Items.Add(New ListViewItem(New String() {"Device icon", "None"}))
                End If
                If d.InterfaceToHost IsNot Nothing Then
                    Items.Add(New ListViewItem(New String() {"Interface to host", d.InterfaceToHost.ToString()}))
                Else
                    Items.Add(New ListViewItem(New String() {"Interface to host", "(Embedded device)"}))
                End If
                Dim deviceURL As String = ""
                Try
                    If d.PresentationURL <> Nothing Then
                        If d.PresentationURL.StartsWith("/") Then
                            deviceURL = String.Concat(New String() {"http://", d.RemoteEndPoint.Address.ToString(), ":", d.RemoteEndPoint.Port.ToString(), d.PresentationURL})
                        Else
                            If Not d.PresentationURL.ToUpper().StartsWith("HTTP://") Then
                                deviceURL = String.Concat(New String() {"http://", d.RemoteEndPoint.Address.ToString(), ":", d.RemoteEndPoint.Port.ToString(), "/", d.PresentationURL})
                            Else
                                deviceURL = d.PresentationURL
                            End If
                        End If
                    End If
                Catch
                End Try
                Items.Add(New ListViewItem(New String() {"Presentation URL", deviceURL}))
            Else
                If infoObject.[GetType]() Is GetType(UPnPService) Then
                    Dim s As UPnPService = CType(infoObject, UPnPService)
                    Items.Add(New ListViewItem(New String() {"Parent UDN", s.ParentDevice.DeviceURN}))
                    Items.Add(New ListViewItem(New String() {"Version", s.Major.ToString() + "." + s.Minor.ToString()}))
                    Items.Add(New ListViewItem(New String() {"Methods", s.Actions.Count.ToString()}))
                    Items.Add(New ListViewItem(New String() {"State variables", s.GetStateVariables().Length.ToString()}))
                    Items.Add(New ListViewItem(New String() {"Service ID", s.ServiceID}))
                    Items.Add(New ListViewItem(New String() {"Service URL", CStr(New UPnPDebugObject(s).GetField("SCPDURL"))}))
                    Dim deviceURL As String = Nothing
                    Try
                        If s.ParentDevice.PresentationURL <> Nothing Then
                            If s.ParentDevice.PresentationURL.ToLower().StartsWith("http://") OrElse s.ParentDevice.PresentationURL.ToLower().StartsWith("https://") Then
                                deviceURL = s.ParentDevice.PresentationURL
                            Else
                                If s.ParentDevice.PresentationURL.StartsWith("/") Then
                                    deviceURL = String.Concat(New String() {"http://", s.ParentDevice.RemoteEndPoint.Address.ToString(), ":", s.ParentDevice.RemoteEndPoint.Port.ToString(), s.ParentDevice.PresentationURL})
                                Else
                                    deviceURL = String.Concat(New String() {"http://", s.ParentDevice.RemoteEndPoint.Address.ToString(), ":", s.ParentDevice.RemoteEndPoint.Port.ToString(), "/", s.ParentDevice.PresentationURL})
                                End If
                            End If
                        End If
                    Catch
                    End Try
                    If deviceURL <> Nothing Then
                        Items.Add(New ListViewItem(New String() {"Parent presentation URL", deviceURL}))
                    End If
                Else
                    If infoObject.[GetType]() Is GetType(UPnPAction) Then
                        Me.listInfo.Sorting = SortOrder.None
                        Dim a As UPnPAction = CType(infoObject, UPnPAction)
                        Items.Add(New ListViewItem(New String() {"Action name", a.Name}))
                        If Not a.HasReturnValue Then
                            Items.Add(New ListViewItem(New String() {"Return argument", "<none>"}))
                        Else
                            Items.Add(New ListViewItem(New String() {"Return argument ASV", a.GetRetArg().RelatedStateVar.Name}))
                            Items.Add(New ListViewItem(New String() {"Return Type", a.GetRetArg().RelatedStateVar.ValueType}))
                        End If
                        Dim argnum As Integer = 1
                        Dim argumentList As UPnPArgument() = a.ArgumentList
                        For i As Integer = 0 To argumentList.Length - 1
                            Dim arg As UPnPArgument = argumentList(i)
                            If Not arg.IsReturnValue Then
                                Dim dataType As String = arg.DataType
                                If dataType Is Nothing OrElse dataType = "" Then
                                End If
                                Items.Add(New ListViewItem(New String() {"Argument " + argnum, "(" + arg.RelatedStateVar.ValueType + ") " + arg.Name}))
                                Items.Add(New ListViewItem(New String() {"Argument " + argnum + " ASV", arg.RelatedStateVar.Name}))
                                argnum += 1
                            End If
                        Next
                    Else
                        If infoObject.[GetType]() Is GetType(UPnPStateVariable) Then
                            Dim var As UPnPStateVariable = CType(infoObject, UPnPStateVariable)
                            Items.Add(New ListViewItem(New String() {"Variable name", var.Name}))
                            Items.Add(New ListViewItem(New String() {"Evented", var.SendEvent.ToString()}))
                            Items.Add(New ListViewItem(New String() {"Data type", var.ValueType}))
                            Try
                                Items.Add(New ListViewItem(New String() {"Last known value", var.Value.ToString()}))
                            Catch ex_C88 As Exception
                                Items.Add(New ListViewItem(New String() {"Last known value", "<unknown>"}))
                            End Try
                            If var.Minimum IsNot Nothing AndAlso var.Maximum Is Nothing Then
                                If var.[Step] IsNot Nothing Then
                                    Items.Add(New ListViewItem(New String() {"Value range", "Not below " + var.Minimum.ToString() + ", Step " + var.[Step].ToString()}))
                                Else
                                    Items.Add(New ListViewItem(New String() {"Value range", "Not below " + var.Minimum.ToString()}))
                                End If
                            Else
                                If var.Minimum Is Nothing AndAlso var.Maximum IsNot Nothing Then
                                    If var.[Step] IsNot Nothing Then
                                        Items.Add(New ListViewItem(New String() {"Value range", "Not above " + var.Maximum.ToString() + ", Step " + var.[Step].ToString()}))
                                    Else
                                        Items.Add(New ListViewItem(New String() {"Value range", "Not above " + var.Maximum.ToString()}))
                                    End If
                                Else
                                    If var.Minimum IsNot Nothing OrElse var.Maximum IsNot Nothing Then
                                        If var.[Step] IsNot Nothing Then
                                            Items.Add(New ListViewItem(New String() {"Value range", String.Concat(New String() {"From ", var.Minimum.ToString(), " to ", var.Maximum.ToString(), ", Step ", var.[Step].ToString()})}))
                                        Else
                                            Items.Add(New ListViewItem(New String() {"Value range", "From " + var.Minimum.ToString() + " to " + var.Maximum.ToString()}))
                                        End If
                                    End If
                                End If
                            End If
                            If var.AllowedStringValues IsNot Nothing AndAlso var.AllowedStringValues.Length > 0 Then
                                Dim AllowedValues As String = ""
                                Dim allowedStringValues As String() = var.AllowedStringValues
                                For i As Integer = 0 To allowedStringValues.Length - 1
                                    Dim x As String = allowedStringValues(i)
                                    If AllowedValues <> "" Then
                                        AllowedValues += ", "
                                    End If
                                    AllowedValues += x
                                Next
                                Items.Add(New ListViewItem(New String() {"Allowed values", AllowedValues}))
                            End If
                            If var.DefaultValue IsNot Nothing Then
                                Items.Add(New ListViewItem(New String() {"Default value", UPnPService.SerializeObjectInstance(var.DefaultValue)}))
                            End If
                        End If
                    End If
                End If
            End If
        End If
        Me.listInfo.Sorting = SortOrder.Ascending
        Me.listInfo.Items.Clear()
        Me.listInfo.Items.AddRange(CType(Items.ToArray(GetType(ListViewItem)), ListViewItem()))
    End Sub

    Private Sub OnSelectedItem(sender As Object, e As TreeViewEventArgs) Handles deviceTree.AfterSelect
        Dim node As TreeNode = Me.deviceTree.SelectedNode
        Me.SetListInfo(node.Tag)
    End Sub
End Class