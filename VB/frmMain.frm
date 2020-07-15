VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form frmMain 
   AutoRedraw      =   -1  'True
   Caption         =   "Map Editor 1.0 by vbgamer45 http://www.tileengines.com"
   ClientHeight    =   7920
   ClientLeft      =   165
   ClientTop       =   450
   ClientWidth     =   9600
   ForeColor       =   &H00FFFFFF&
   Icon            =   "frmMain.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   528
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   640
   Begin MSComDlg.CommonDialog CD1 
      Left            =   1200
      Top             =   1680
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.Timer tmrChangeValue 
      Interval        =   20
      Left            =   2160
      Top             =   2280
   End
   Begin VB.PictureBox picTiles 
      AutoRedraw      =   -1  'True
      AutoSize        =   -1  'True
      Height          =   540
      Left            =   0
      ScaleHeight     =   32
      ScaleMode       =   3  'Pixel
      ScaleWidth      =   128
      TabIndex        =   3
      Top             =   0
      Visible         =   0   'False
      Width           =   1980
   End
   Begin VB.PictureBox Picture1 
      Appearance      =   0  'Flat
      BackColor       =   &H00C0C0C0&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   255
      Left            =   9360
      ScaleHeight     =   255
      ScaleWidth      =   255
      TabIndex        =   2
      Top             =   7680
      Width           =   255
   End
   Begin VB.VScrollBar vsMap 
      Height          =   7695
      Left            =   9360
      Max             =   1
      TabIndex        =   1
      Top             =   0
      Width           =   255
   End
   Begin VB.HScrollBar hsMap 
      Height          =   255
      Left            =   0
      Max             =   1
      TabIndex        =   0
      Top             =   7680
      Width           =   9375
   End
   Begin VB.Shape shpSelector 
      BorderColor     =   &H00FF0000&
      BorderWidth     =   2
      FillColor       =   &H00FF0000&
      Height          =   480
      Left            =   2040
      Top             =   120
      Width           =   480
   End
   Begin VB.Menu mnuFile 
      Caption         =   "&File"
      Begin VB.Menu mnuFileNewMap 
         Caption         =   "&New Map"
         Shortcut        =   ^N
      End
      Begin VB.Menu mnuFileSave 
         Caption         =   "&Save"
         Shortcut        =   ^S
      End
      Begin VB.Menu mnuFileLoad 
         Caption         =   "&Load"
         Shortcut        =   ^L
      End
      Begin VB.Menu mnuFileSep1 
         Caption         =   "-"
      End
      Begin VB.Menu mnuFileExit 
         Caption         =   "E&xit"
         Shortcut        =   ^X
      End
   End
   Begin VB.Menu mnuMap 
      Caption         =   "&Map"
      Begin VB.Menu mnuMapClearScreen 
         Caption         =   "Clear Sc&reen"
         Shortcut        =   ^R
      End
      Begin VB.Menu mnuMapFillScreen 
         Caption         =   "&Fill Screen w/Selected Tile"
         Shortcut        =   ^F
      End
      Begin VB.Menu mnuMapTileType 
         Caption         =   "&View Tile Type"
         Checked         =   -1  'True
      End
   End
   Begin VB.Menu mnuTools 
      Caption         =   "&Tools"
      Begin VB.Menu mnuToolsNormalPaint 
         Caption         =   "&Normal Paint"
         Checked         =   -1  'True
         Shortcut        =   ^P
      End
   End
   Begin VB.Menu mnuWindows 
      Caption         =   "&Windows"
      Begin VB.Menu mnuWindowsCascade 
         Caption         =   "&Cascade"
         Shortcut        =   ^C
      End
   End
   Begin VB.Menu mnuHelp 
      Caption         =   "&Help"
      Begin VB.Menu mnuHelpAbout 
         Caption         =   "&About"
      End
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'*********************************
'Map Editor Example in VB6
'Website:
'http://www.tileengines.com
'*********************************
Option Explicit

Private Sub Form_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
    'If right button mouse click show the popup menu
    If Button = vbRightButton Then PopupMenu mnuMap
    
    'If button pressed paint tile
    If Button = vbLeftButton And SelectedTool = "Normal Paint" Then
       'Paint the current tile
       Call PaintTile(X, Y)
       'Change the shapes backcolor to white
       shpSelector.BorderColor = &HFFFFFF
    End If
    'Refresh the screen
    frmMain.Refresh
    
End Sub

Private Sub Form_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
    
    'Tell the TileX and TileY where they are
    CurX = X
    CurY = Y
    
    'If mouse moved move square tile selector
    shpSelector.Left = Snap(X, 32)
    shpSelector.Top = Snap(Y, 32)
    
    'If left mouse button then call paint tile
    If Button = vbLeftButton And SelectedTool = "Normal Paint" Then
       'Call Paint Tile
       Call PaintTile(X, Y)
       'Changes shape color to white
       shpSelector.BorderColor = &HFFFFFF
    End If
End Sub

Private Sub Form_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
    'if left mouse button pressed then change the color back to blue
    If Button = 1 Then shpSelector.BorderColor = &HFF0000
End Sub

Private Sub LoadDefault()
    Call Cascade 'cascade the windows
    
    ReDim Map(23 * 4, 16 * 4) 'Set the ubound of the map array
    
    'Set the max of the scroll bars according to the map size
    hsMap.Max = 23 * (4 - 1)
    vsMap.Max = 16 * (4 - 1)
    
End Sub
Private Sub Form_Load()
On Error GoTo errHandle 'Error handling if file doesn't exist
    'Set Show Tile Information equal true
    bShowTileType = True
    'Load tiles
    picTiles.Picture = LoadPicture(App.Path & "\Tiles.bmp")
    'Set the selected tool to normal paint
    SelectedTool = "Normal Paint"
    'Load Default map information
    Call LoadDefault
    'Setup the window positions
    Call Cascade
Exit Sub
errHandle:
    MsgBox "Error_Form_Load: " & Err.Description
End Sub

Private Sub PaintTile(ByVal X As Single, ByVal Y As Single)
On Error GoTo errHandle:
    'Hold the tile x,y cords
    Dim TempX As Long, TempY As Long
    
    'Check if x,y is within the map
    If X <= frmMain.ScaleWidth And X >= 0 And Y <= frmMain.ScaleHeight And Y >= 0 Then
        
        'Paints the tile to the screen
        BitBlt frmMain.hDC, Snap(X, 32), Snap(Y, 32), 32, 32, picTiles.hDC, TempTileX, TempTileY, SRCCOPY
        
        'Gets the current X,Y cords
        TempX = Snap(X, 32) \ 32 + hsMap.Value
        TempY = Snap(Y, 32) \ 32 + vsMap.Value
        
        'Save map info to the map array
        Map(TempX, TempY).TileX = TempTileX
        Map(TempX, TempY).TileY = TempTileY
        Map(TempX, TempY).TileType = GetTileType
        
        'If show tile information equals true then draw the tiletype
        If bShowTileType = True Then
            Me.CurrentX = Snap(X, 32)
            Me.CurrentY = Snap(Y, 32)
            Me.Print Map(TempX, TempY).TileType
        End If
        'Set refreshmini flag to true
        RefreshMini = True
    End If
Exit Sub
errHandle:
    MsgBox "Error_frmMain.PaintTile : " & Err.Number & " " & Err.Description
End Sub

Private Sub Cascade()
    'Setup each forms correct position
    frmTools.Show
    frmTools.Left = frmMain.Width + 1
    frmTools.Top = 0
    
    frmMain.Show
    frmMain.Top = 0
    frmMain.Left = 0
End Sub

Private Sub Form_Terminate()
    'Call the Terminate function
    Call Terminate
End Sub

Private Sub Form_Unload(Cancel As Integer)
    'Call the Terminate function
    Call Terminate
End Sub

Private Sub Terminate()
    'Unload the forms then end
    Unload frmTools
    Unload frmMain
    End
End Sub

Private Sub RedrawMap()
'Redraw Map Function
    Dim X As Long, Y As Long
    'Clear the screen
    frmMain.Cls
    'Set the Forecolor to white (Sets the text color)
    frmMain.ForeColor = vbWhite
    For Y = vsMap.Value To vsMap.Value + 16
        If Y > UBound(Map, 2) Then Exit For
        For X = hsMap.Value To hsMap.Value + 23
            If X > UBound(Map, 1) Then Exit For
            If Map(X, Y).TileType > 0 Then 'If the tiletype is anything but 0
                BitBlt frmMain.hDC, Snap2(X, 32) - Snap2(hsMap.Value, 32), Snap2(Y, 32) - Snap2(vsMap.Value, 32), 32, 32, picTiles.hDC, Map(X, Y).TileX, Map(X, Y).TileY, SRCCOPY
                'Print the tiletype
                If bShowTileType = True Then
                    Me.CurrentX = Snap2(X, 32) - Snap2(hsMap.Value, 32)
                    Me.CurrentY = Snap2(Y, 32) - Snap2(vsMap.Value, 32)
                    Me.Print Map(X, Y).TileType
                End If
            End If
        Next X
    Next Y
    'Refresh the screen
    frmMain.Refresh
    RefreshMini = True
End Sub
Private Sub hsMap_Change()
    'Hold the current Horizontal Scroll Position
    HScroll = hsMap.Value
    Call RedrawMap
End Sub

Private Sub hsMap_Scroll()
    'Hold the current Horizontal Scroll Position
    HScroll = hsMap.Value
    Call RedrawMap
End Sub

Private Sub mnuFileExit_Click()
    Call Terminate
End Sub
Private Sub mnuFileLoad_Click()
    'Set the filename to null
    CD1.FileName = ""
    'Set the Dialog title
    CD1.DialogTitle = "Load Map"
    'Show only files with extension .map
    CD1.Filter = "*.map|*.map"
    'Show the open dialog
    CD1.ShowOpen
    'Check if a file was selected
    If CD1.FileName = vbNullString Then Exit Sub
    'Process the map
    Call LoadMap(CD1.FileName)
End Sub

Private Sub mnuFileNewMap_Click()
    'Load the Default map options
    Call LoadDefault
    'Redraw the map
    Call RedrawMap
End Sub

Private Sub mnuFileSave_Click()
    'Set the filename to null
    CD1.FileName = ""
    CD1.DialogTitle = "Save Map"
    CD1.Filter = "*.map|*.map"
    CD1.ShowSave
    If CD1.FileName = vbNullString Then Exit Sub
    Call SaveMap(CD1.FileName)
End Sub

Private Sub mnuHelpAbout_Click()
    frmAbout.Show vbModal, Me
End Sub

Private Sub mnuMapClearScreen_Click()
    Call EraseAll
End Sub

Private Sub mnuMapFillScreen_Click()
    'This fills the screen with the selected tile
    Dim X As Long, Y As Long
        For Y = 0 To UBound(Map, 2)
            For X = 0 To UBound(Map, 1)
                Map(X, Y).TileType = GetTileType
                Map(X, Y).TileX = TempTileX
                Map(X, Y).TileY = TempTileY
            Next X
        Next Y
    Call RedrawMap
End Sub

Private Sub mnuMapTileType_Click()
'Set the current tiletype
    If mnuMapTileType.Checked = True Then
        bShowTileType = False
        mnuMapTileType.Checked = False
        Call RedrawMap
    Else
        bShowTileType = True
        mnuMapTileType.Checked = True
        Call RedrawMap
    End If
End Sub

Private Sub mnuToolsNormalPaint_Click()
    'Set the selected tool to Normal
    SelectedTool = "Normal Paint"
End Sub

Private Sub mnuWindowsCascade_Click()
    Call Cascade
End Sub

Private Sub tmrChangeValue_Timer()
    'If the two values arent equal set them equal
    If hsMap.Value <> HScroll Or vsMap.Value <> VScroll Then
        hsMap.Value = HScroll
        vsMap.Value = VScroll
    End If
End Sub
Private Sub SaveMap(ByVal strFilename As String)

    On Error GoTo NoFile: 'If file doesnt save properly show error
    Dim F As Long 'Holds the file handle
    Dim Height As Integer, Width As Integer 'Hold the width and height of the map
    F = FreeFile 'Get a free file handle
    Width = UBound(Map, 1) 'Get the width of the map
    Height = UBound(Map, 2) 'Get the height of the map
    Open strFilename For Binary Access Write Lock Write As #F
        Put #F, , Width 'Store the width
        Put #F, , Height 'Store the height
        Put #F, , Map 'Store the entire map
    Close #F
    'Redraw map
    Call RedrawMap
    MsgBox "The Map was succesfully saved", vbOKOnly, "Map Editor 1.0"
  
    Exit Sub
NoFile:
    RedrawMap
    MsgBox "Error_frmMain_SaveMap: " & Err.Number & " " & Err.Description

End Sub

Private Sub LoadMap(ByVal strFilename As String)
    On Error GoTo NoFile: 'If file doesnt exist dont open it
    Dim F As Long 'Holds file handle
    Dim Height As Integer, Width As Integer
    F = FreeFile 'Gets a free file handle
    Open strFilename For Binary Access Read Lock Read As #F
        Call EraseAll 'Earse the current map information
        Get #F, , Width 'Get the map width
        Get #F, , Height ' Get the map height
        ReDim Map(Width, Height) 'Resize the map
        Get #F, , Map 'Get the map
    Close #F
    
    
    Call RedrawMap
    MsgBox "The Map was succesfully loaded", vbOKOnly, "Map Editor 1.0"
    Exit Sub
    
NoFile:
    RedrawMap
    MsgBox "Error_frmMain_LoadMap: " & Err.Number & " " & Err.Description

End Sub

Private Sub EraseAll()
    'Erase the map
    Dim X As Long, Y As Long
    'Setup all the elements in the array to zero
    For Y = 0 To UBound(Map, 2)
        For X = 0 To UBound(Map, 1)
            Map(X, Y).TileType = 0
            Map(X, Y).TileX = 0
            Map(X, Y).TileY = 0
        Next X
    Next Y
    'Redraw the map
    Call RedrawMap
End Sub

Private Sub vsMap_Change()
    'On value change update the map
    VScroll = vsMap.Value
    Call RedrawMap
End Sub

Private Sub vsMap_Scroll()
    'On scroll update map
    VScroll = vsMap.Value
    Call RedrawMap
End Sub
Private Function GetTileType() As Integer
    If frmTools.cboTileType.Text = "walkable" Then
        'Tile is walkable
        GetTileType = 1
    Else
        'Tile is nonwalkable
        GetTileType = 2
    End If
    
End Function
