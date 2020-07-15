VERSION 5.00
Begin VB.Form frmTools 
   AutoRedraw      =   -1  'True
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Tools"
   ClientHeight    =   8280
   ClientLeft      =   45
   ClientTop       =   285
   ClientWidth     =   2265
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   552
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   151
   ShowInTaskbar   =   0   'False
   Begin VB.ComboBox cboTileType 
      Height          =   315
      ItemData        =   "frmTools.frx":0000
      Left            =   120
      List            =   "frmTools.frx":000A
      TabIndex        =   16
      Text            =   "walkable"
      Top             =   6480
      Width           =   1935
   End
   Begin VB.Frame FrameMiniMap 
      Caption         =   "MiniMap"
      Height          =   1335
      Left            =   150
      TabIndex        =   13
      Top             =   4680
      Width           =   1890
      Begin VB.PictureBox MiniMap 
         AutoRedraw      =   -1  'True
         BackColor       =   &H00000000&
         BorderStyle     =   0  'None
         Height          =   975
         Left            =   240
         ScaleHeight     =   65
         ScaleMode       =   3  'Pixel
         ScaleWidth      =   91
         TabIndex        =   14
         Top             =   240
         Width           =   1370
         Begin VB.Shape Shape1 
            BorderColor     =   &H0000FFFF&
            Height          =   248
            Left            =   0
            Top             =   0
            Width           =   330
         End
      End
   End
   Begin VB.Frame FrameSelectedTool 
      Caption         =   "Selected Tool"
      Height          =   615
      Left            =   120
      TabIndex        =   11
      Top             =   2880
      Width           =   2055
      Begin VB.Label lblSelectedTool 
         Alignment       =   2  'Center
         Height          =   255
         Left            =   120
         TabIndex        =   12
         Top             =   240
         Width           =   1815
      End
   End
   Begin VB.Timer tmrGetInfo 
      Interval        =   50
      Left            =   0
      Top             =   1920
   End
   Begin VB.Frame FrameSelectedTile 
      Caption         =   "Selected Tile"
      Height          =   855
      Left            =   480
      TabIndex        =   2
      Top             =   1950
      Width           =   1215
      Begin VB.PictureBox picSelected 
         Appearance      =   0  'Flat
         AutoRedraw      =   -1  'True
         BackColor       =   &H00C0C0C0&
         BorderStyle     =   0  'None
         ForeColor       =   &H80000008&
         Height          =   480
         Left            =   360
         ScaleHeight     =   32
         ScaleMode       =   3  'Pixel
         ScaleWidth      =   32
         TabIndex        =   3
         Top             =   240
         Width           =   480
      End
   End
   Begin VB.PictureBox picDisplay 
      Appearance      =   0  'Flat
      AutoRedraw      =   -1  'True
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   1440
      Left            =   240
      ScaleHeight     =   96
      ScaleMode       =   3  'Pixel
      ScaleWidth      =   128
      TabIndex        =   0
      Top             =   240
      Width           =   1920
   End
   Begin VB.Frame FrameChooseTile 
      Caption         =   " Choose Tile"
      Height          =   1935
      Left            =   0
      TabIndex        =   1
      Top             =   0
      Width           =   2295
      Begin VB.VScrollBar vsTiles 
         Height          =   1575
         Left            =   0
         TabIndex        =   15
         Top             =   120
         Width           =   255
      End
      Begin VB.PictureBox picStorage 
         AutoRedraw      =   -1  'True
         AutoSize        =   -1  'True
         Height          =   180
         Left            =   120
         ScaleHeight     =   8
         ScaleMode       =   3  'Pixel
         ScaleWidth      =   16
         TabIndex        =   10
         Top             =   0
         Visible         =   0   'False
         Width           =   300
      End
      Begin VB.HScrollBar hsTiles 
         Height          =   255
         Left            =   0
         Max             =   2
         TabIndex        =   9
         Top             =   1680
         Width           =   2175
      End
   End
   Begin VB.Frame FrameTileY 
      Caption         =   "Tile Y"
      Height          =   615
      Left            =   1200
      TabIndex        =   6
      Top             =   3840
      Width           =   735
      Begin VB.Label lblTileY 
         Height          =   255
         Left            =   120
         TabIndex        =   8
         Top             =   240
         Width           =   495
      End
   End
   Begin VB.Frame FrameTileCords 
      Caption         =   "Tile Cordinates"
      Height          =   975
      Left            =   120
      TabIndex        =   4
      Top             =   3600
      Width           =   1935
      Begin VB.Frame FrameTileX 
         Caption         =   "Tile X"
         Height          =   615
         Left            =   120
         TabIndex        =   5
         Top             =   240
         Width           =   735
         Begin VB.Label lblTileX 
            Height          =   255
            Left            =   120
            TabIndex        =   7
            Top             =   240
            Width           =   495
         End
      End
   End
   Begin VB.Label lblTileType 
      Caption         =   "TileType:"
      Height          =   255
      Left            =   120
      TabIndex        =   17
      Top             =   6240
      Width           =   1815
   End
End
Attribute VB_Name = "frmTools"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Sub Form_Load()
    picStorage.Picture = LoadPicture(App.Path & "\Tiles.bmp")
    
    'Blt the first tile to the picselected box
    BitBlt picSelected.hDC, 0, 0, 32, 32, picStorage.hDC, 0, 0, SRCCOPY
    picSelected.Refresh
    
    'Let the scrollbars max value = to how many tiles there are
    hsTiles.Value = 0
    hsTiles.Max = picStorage.ScaleWidth \ 32 - 4
    vsTiles.Value = 0
    vsTiles.Max = picStorage.ScaleHeight \ 32 - 3
    'In case tiles.bmp width is too small change the max to 0
    If hsTiles.Max < 0 Then hsTiles.Max = 0
    Call RedrawTile
End Sub

Private Sub hsTiles_Change()
    Call RedrawTile
End Sub

Private Sub hsTiles_Scroll()
   Call RedrawTile
End Sub

Private Sub MiniMap_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
    'This will check to make sure box doesnt go off screen it also moves box
    Dim tHScroll As Long, tVScroll As Long
    If Button = 1 Then
        tHScroll = X
        tVScroll = Y
        If tHScroll < 0 Then tHScroll = 0
        If tVScroll < 0 Then tVScroll = 0
        If tHScroll + Shape1.Width > MiniMap.ScaleWidth Then tHScroll = MiniMap.ScaleWidth - Shape1.Width
        If tVScroll + Shape1.Height > MiniMap.ScaleHeight Then tVScroll = MiniMap.ScaleHeight - Shape1.Height
        HScroll = tHScroll
        VScroll = tVScroll
    End If
End Sub

Private Sub MiniMap_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
    'This will check to make sure box doesnt go off screen it also moves box
    Dim tHScroll As Long, tVScroll As Long
    If Button = 1 Then
        tHScroll = X
        tVScroll = Y
        If tHScroll < 0 Then tHScroll = 0
        If tVScroll < 0 Then tVScroll = 0
        If tHScroll + Shape1.Width > MiniMap.ScaleWidth Then tHScroll = MiniMap.ScaleWidth - Shape1.Width
        If tVScroll + Shape1.Height > MiniMap.ScaleHeight Then tVScroll = MiniMap.ScaleHeight - Shape1.Height
        HScroll = tHScroll
        VScroll = tVScroll
    End If
End Sub

Private Sub picDisplay_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
    'If user presses left M. Button. then blit the pic to the Select Pic Box
    If Button = 1 Then
        BitBlt picSelected.hDC, 0, 0, 32, 32, picDisplay.hDC, Snap(X, 32), Snap(Y, 32), SRCCOPY
        picSelected.Refresh
        TempTileX = Snap(X, 32) + hsTiles.Value * 32
        TempTileY = Snap(Y, 32) + vsTiles.Value * 32
    End If
End Sub

Private Sub RedrawTile()
'An algorithnm that will blit tiles to the picDisplay box according to the scroll bars values
    Dim TempX As Long, TempY As Long
    Dim X As Long, Y As Long
    picDisplay.Cls
    TempX = 0
    TempY = 0
    'For Y = 0 To 64 Step 32
    For Y = vsTiles.Value * 32 To vsTiles.Value * 32 + 64 Step 32
        For X = hsTiles.Value * 32 To hsTiles.Value * 32 + 96 Step 32
            BitBlt picDisplay.hDC, TempX, TempY, 32, 32, picStorage.hDC, X, Y, SRCCOPY
            'Because X will try to blit offscreen
            TempX = TempX + 32
            
        Next X
        TempY = TempY + 32
    TempX = 0
    Next Y
    
    picDisplay.Refresh
End Sub
Private Sub RefreshMiniMap()

    Dim X As Long, Y As Long
    MiniMap.Cls
    For Y = 0 To UBound(Map, 2)
        For X = 0 To UBound(Map, 1)
            If Map(X, Y).TileType = 1 Then
                BitBlt MiniMap.hDC, X, Y, 1.5, 1.2, picStorage.hDC, Map(X, Y).TileX + 2, Map(X, Y).TileY + 2, SRCCOPY
      
            End If
        Next X
    Next Y
       MiniMap.Refresh
       RefreshMini = False
End Sub

Private Sub tmrGetInfo_Timer()
    'Refresh minimap if needed
    If RefreshMini = True Then RefreshMiniMap
    
    'Refresh yellow box
    Shape1.Left = HScroll
    Shape1.Top = VScroll
    
    'Make the two forms windowstates the same
    frmTools.WindowState = frmMain.WindowState
    
    'Dont move left pos of tools if start is minimized
    If frmMain.WindowState <> 1 Then
        frmTools.Left = frmMain.Left + frmMain.Width
        frmTools.Top = frmMain.Top
    End If
    

    'Show the scroll bars values
    lblTileX.Caption = (Snap(CurX, 32) \ 32) + HScroll
    lblTileY.Caption = (Snap(CurY, 32) \ 32) + VScroll
    
    
    'Show what tool we are using (only 1 for now)
    lblSelectedTool.Caption = SelectedTool
End Sub

Private Sub vsTiles_Change()
    Call RedrawTile
End Sub

Private Sub vsTiles_Scroll()
    Call RedrawTile
End Sub
