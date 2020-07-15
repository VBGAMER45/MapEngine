Option Strict Off
Option Explicit On
Module modMap
	'API to Blit a picture to the screen
	Public Declare Function BitBlt Lib "gdi32" (ByVal hDestDC As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As Integer, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer
	Public Const SRCAND As Integer = &H8800C6
	Public Const SRCCOPY As Integer = &HCC0020
	Public Const SRCPAINT As Integer = &HEE0086
	
	'Map Stuff
	Public HScroll, VScroll As Short ' Tells the Tools Form the map editors scrollbar positions
	Public CurX, CurY As Single
	
	Public Structure MapData
		Dim TileX As Short
		Dim TileY As Short
		Dim TileType As Short
	End Structure
	
    Public Map(92, 64) As MapData 'Stores map info
	Public MapLayer As Short 'What layer are we on?
	Public TempTileX, TempTileY As Short 'Which tile is currently selected
	Public SelectedTool As String 'Which tool are we using?
	Public RefreshMini As Boolean 'Refresh minimap when needed
	Public MapSaved As Boolean
	Public strMapName As String
	Public bShowTileType As Boolean
	
    Public Function Snap(ByRef Cordinate As Int16, ByRef Dimension As Short) As Short
        Snap = (Cordinate \ Dimension) * Dimension 'Small algorithm that snaps to grid sort of
    End Function

    Public Function Snap2(ByRef Cordinate As Int16, ByRef Dimension As Short) As Short
        Snap2 = (Cordinate * Dimension) 'Small algorithm that takes a number and converts it into a pixel cord.
    End Function
End Module