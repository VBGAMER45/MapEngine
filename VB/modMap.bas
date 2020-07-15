Attribute VB_Name = "modMap"
'*********************************
'Map Editor Example in VB6
'Website:
'http://www.tileengines.com
'*********************************
Option Explicit
'API to Blit a picture to the screen
Public Declare Function BitBlt Lib "gdi32" (ByVal hDestDC As Long, ByVal X As Long, ByVal Y As Long, ByVal nWidth As Long, ByVal nHeight As Long, ByVal hSrcDC As Long, ByVal xSrc As Long, ByVal ySrc As Long, ByVal dwRop As Long) As Long
Public Const SRCCOPY As Long = &HCC0020 'Constant used for BitBlt to copy

'Map Stuff
Public HScroll As Integer, VScroll As Integer ' Tells the Tools Form the map editors scrollbar positions
'Holds where the shape current x,y cords
Public CurX As Single, CurY As Single

'Mapdata structure
Private Type MapData
    TileX As Integer 'Holds the x cord in tiles.bmp
    TileY As Integer 'Holds the y cord in tiles.bmp
    TileType As Byte 'Holds wether the tile is walkable or not
End Type

Public Map() As MapData 'Stores map info
Public TempTileX As Integer, TempTileY As Integer 'Which tile is currently selected
Public SelectedTool As String 'Which tool are we using?
Public RefreshMini As Boolean 'Refresh minimap when needed
Public bShowTileType As Boolean 'Show the tile information?

Public Function Snap(Cordinate As Variant, Dimension As Integer) As Integer
    Snap = (Cordinate \ Dimension) * Dimension 'Small algorithm that snaps to grid sort of
End Function

Public Function Snap2(Cordinate As Variant, Dimension As Integer) As Integer
    Snap2 = (Cordinate * Dimension) 'Small algorithm that takes a number and converts it into a pixel cord.
End Function
