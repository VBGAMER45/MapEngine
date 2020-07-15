Option Strict Off
Option Explicit On 

Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D

Friend Class frmTools
    Inherits System.Windows.Forms.Form
    Dim picDisplayGFX As Graphics
    Dim picSelectedGFX As Graphics
    Dim MiniMapGFX As Graphics
    Dim bTileSet As Image = New Bitmap("Tiles.bmp")
#Region "Windows Form Designer generated code "
	Public Sub New()
		MyBase.New()
		If m_vb6FormDefInstance Is Nothing Then
			If m_InitializingDefInstance Then
				m_vb6FormDefInstance = Me
			Else
				Try 
					'For the start-up form, the first instance created is the default instance.
					If System.Reflection.Assembly.GetExecutingAssembly.EntryPoint.DeclaringType Is Me.GetType Then
						m_vb6FormDefInstance = Me
					End If
				Catch
				End Try
			End If
		End If
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents cboTileType As System.Windows.Forms.ComboBox
	Public WithEvents Shape1 As System.Windows.Forms.Label
	Public WithEvents MiniMap As System.Windows.Forms.Panel
	Public WithEvents FrameMiniMap As System.Windows.Forms.GroupBox
	Public WithEvents lblSelectedTool As System.Windows.Forms.Label
	Public WithEvents FrameSelectedTool As System.Windows.Forms.GroupBox
	Public WithEvents tmrGetInfo As System.Windows.Forms.Timer
	Public WithEvents picSelected As System.Windows.Forms.PictureBox
	Public WithEvents FrameSelectedTile As System.Windows.Forms.GroupBox
	Public WithEvents picDisplay As System.Windows.Forms.PictureBox
	Public WithEvents vsTiles As System.Windows.Forms.VScrollBar
	Public WithEvents picStorage As System.Windows.Forms.PictureBox
	Public WithEvents hsTiles As System.Windows.Forms.HScrollBar
	Public WithEvents FrameChooseTile As System.Windows.Forms.GroupBox
	Public WithEvents lblTileY As System.Windows.Forms.Label
	Public WithEvents FrameTileY As System.Windows.Forms.GroupBox
	Public WithEvents lblTileX As System.Windows.Forms.Label
	Public WithEvents FrameTileX As System.Windows.Forms.GroupBox
	Public WithEvents FrameTileCords As System.Windows.Forms.GroupBox
	Public WithEvents lblTileType As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cboTileType = New System.Windows.Forms.ComboBox
        Me.FrameMiniMap = New System.Windows.Forms.GroupBox
        Me.MiniMap = New System.Windows.Forms.Panel
        Me.Shape1 = New System.Windows.Forms.Label
        Me.FrameSelectedTool = New System.Windows.Forms.GroupBox
        Me.lblSelectedTool = New System.Windows.Forms.Label
        Me.tmrGetInfo = New System.Windows.Forms.Timer(Me.components)
        Me.FrameSelectedTile = New System.Windows.Forms.GroupBox
        Me.picSelected = New System.Windows.Forms.PictureBox
        Me.picDisplay = New System.Windows.Forms.PictureBox
        Me.FrameChooseTile = New System.Windows.Forms.GroupBox
        Me.vsTiles = New System.Windows.Forms.VScrollBar
        Me.picStorage = New System.Windows.Forms.PictureBox
        Me.hsTiles = New System.Windows.Forms.HScrollBar
        Me.FrameTileY = New System.Windows.Forms.GroupBox
        Me.lblTileY = New System.Windows.Forms.Label
        Me.FrameTileCords = New System.Windows.Forms.GroupBox
        Me.FrameTileX = New System.Windows.Forms.GroupBox
        Me.lblTileX = New System.Windows.Forms.Label
        Me.lblTileType = New System.Windows.Forms.Label
        Me.FrameMiniMap.SuspendLayout()
        Me.MiniMap.SuspendLayout()
        Me.FrameSelectedTool.SuspendLayout()
        Me.FrameSelectedTile.SuspendLayout()
        Me.FrameChooseTile.SuspendLayout()
        Me.FrameTileY.SuspendLayout()
        Me.FrameTileCords.SuspendLayout()
        Me.FrameTileX.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboTileType
        '
        Me.cboTileType.BackColor = System.Drawing.SystemColors.Window
        Me.cboTileType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTileType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTileType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTileType.Items.AddRange(New Object() {"walkable", "nonwalkable"})
        Me.cboTileType.Location = New System.Drawing.Point(8, 432)
        Me.cboTileType.Name = "cboTileType"
        Me.cboTileType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTileType.Size = New System.Drawing.Size(129, 22)
        Me.cboTileType.TabIndex = 16
        Me.cboTileType.Text = "walkable"
        '
        'FrameMiniMap
        '
        Me.FrameMiniMap.BackColor = System.Drawing.SystemColors.Control
        Me.FrameMiniMap.Controls.Add(Me.MiniMap)
        Me.FrameMiniMap.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameMiniMap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameMiniMap.Location = New System.Drawing.Point(10, 312)
        Me.FrameMiniMap.Name = "FrameMiniMap"
        Me.FrameMiniMap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameMiniMap.Size = New System.Drawing.Size(126, 89)
        Me.FrameMiniMap.TabIndex = 13
        Me.FrameMiniMap.TabStop = False
        Me.FrameMiniMap.Text = "MiniMap"
        '
        'MiniMap
        '
        Me.MiniMap.BackColor = System.Drawing.Color.Black
        Me.MiniMap.Controls.Add(Me.Shape1)
        Me.MiniMap.Cursor = System.Windows.Forms.Cursors.Default
        Me.MiniMap.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MiniMap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.MiniMap.Location = New System.Drawing.Point(16, 16)
        Me.MiniMap.Name = "MiniMap"
        Me.MiniMap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MiniMap.Size = New System.Drawing.Size(92, 65)
        Me.MiniMap.TabIndex = 14
        Me.MiniMap.TabStop = True
        '
        'Shape1
        '
        Me.Shape1.BackColor = System.Drawing.Color.Transparent
        Me.Shape1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Shape1.Location = New System.Drawing.Point(0, 0)
        Me.Shape1.Name = "Shape1"
        Me.Shape1.Size = New System.Drawing.Size(22, 17)
        Me.Shape1.TabIndex = 0
        '
        'FrameSelectedTool
        '
        Me.FrameSelectedTool.BackColor = System.Drawing.SystemColors.Control
        Me.FrameSelectedTool.Controls.Add(Me.lblSelectedTool)
        Me.FrameSelectedTool.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameSelectedTool.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameSelectedTool.Location = New System.Drawing.Point(8, 192)
        Me.FrameSelectedTool.Name = "FrameSelectedTool"
        Me.FrameSelectedTool.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameSelectedTool.Size = New System.Drawing.Size(137, 41)
        Me.FrameSelectedTool.TabIndex = 11
        Me.FrameSelectedTool.TabStop = False
        Me.FrameSelectedTool.Text = "Selected Tool"
        '
        'lblSelectedTool
        '
        Me.lblSelectedTool.BackColor = System.Drawing.SystemColors.Control
        Me.lblSelectedTool.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSelectedTool.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedTool.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSelectedTool.Location = New System.Drawing.Point(8, 16)
        Me.lblSelectedTool.Name = "lblSelectedTool"
        Me.lblSelectedTool.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSelectedTool.Size = New System.Drawing.Size(121, 17)
        Me.lblSelectedTool.TabIndex = 12
        Me.lblSelectedTool.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tmrGetInfo
        '
        Me.tmrGetInfo.Enabled = True
        Me.tmrGetInfo.Interval = 50
        '
        'FrameSelectedTile
        '
        Me.FrameSelectedTile.BackColor = System.Drawing.SystemColors.Control
        Me.FrameSelectedTile.Controls.Add(Me.picSelected)
        Me.FrameSelectedTile.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameSelectedTile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameSelectedTile.Location = New System.Drawing.Point(32, 130)
        Me.FrameSelectedTile.Name = "FrameSelectedTile"
        Me.FrameSelectedTile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameSelectedTile.Size = New System.Drawing.Size(81, 57)
        Me.FrameSelectedTile.TabIndex = 2
        Me.FrameSelectedTile.TabStop = False
        Me.FrameSelectedTile.Text = "Selected Tile"
        '
        'picSelected
        '
        Me.picSelected.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
        Me.picSelected.Cursor = System.Windows.Forms.Cursors.Default
        Me.picSelected.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picSelected.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picSelected.Location = New System.Drawing.Point(24, 16)
        Me.picSelected.Name = "picSelected"
        Me.picSelected.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picSelected.Size = New System.Drawing.Size(32, 32)
        Me.picSelected.TabIndex = 3
        Me.picSelected.TabStop = False
        '
        'picDisplay
        '
        Me.picDisplay.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
        Me.picDisplay.Cursor = System.Windows.Forms.Cursors.Default
        Me.picDisplay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picDisplay.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picDisplay.Location = New System.Drawing.Point(16, 16)
        Me.picDisplay.Name = "picDisplay"
        Me.picDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picDisplay.Size = New System.Drawing.Size(128, 96)
        Me.picDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picDisplay.TabIndex = 0
        Me.picDisplay.TabStop = False
        '
        'FrameChooseTile
        '
        Me.FrameChooseTile.BackColor = System.Drawing.SystemColors.Control
        Me.FrameChooseTile.Controls.Add(Me.vsTiles)
        Me.FrameChooseTile.Controls.Add(Me.picStorage)
        Me.FrameChooseTile.Controls.Add(Me.hsTiles)
        Me.FrameChooseTile.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameChooseTile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameChooseTile.Location = New System.Drawing.Point(0, 0)
        Me.FrameChooseTile.Name = "FrameChooseTile"
        Me.FrameChooseTile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameChooseTile.Size = New System.Drawing.Size(153, 129)
        Me.FrameChooseTile.TabIndex = 1
        Me.FrameChooseTile.TabStop = False
        Me.FrameChooseTile.Text = " Choose Tile"
        '
        'vsTiles
        '
        Me.vsTiles.Cursor = System.Windows.Forms.Cursors.Default
        Me.vsTiles.LargeChange = 1
        Me.vsTiles.Location = New System.Drawing.Point(0, 8)
        Me.vsTiles.Maximum = 32767
        Me.vsTiles.Name = "vsTiles"
        Me.vsTiles.Size = New System.Drawing.Size(17, 105)
        Me.vsTiles.TabIndex = 15
        Me.vsTiles.TabStop = True
        '
        'picStorage
        '
        Me.picStorage.BackColor = System.Drawing.SystemColors.Control
        Me.picStorage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picStorage.Cursor = System.Windows.Forms.Cursors.Default
        Me.picStorage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picStorage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.picStorage.Location = New System.Drawing.Point(8, 0)
        Me.picStorage.Name = "picStorage"
        Me.picStorage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picStorage.Size = New System.Drawing.Size(20, 12)
        Me.picStorage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picStorage.TabIndex = 10
        Me.picStorage.TabStop = False
        Me.picStorage.Visible = False
        '
        'hsTiles
        '
        Me.hsTiles.Cursor = System.Windows.Forms.Cursors.Default
        Me.hsTiles.LargeChange = 1
        Me.hsTiles.Location = New System.Drawing.Point(0, 112)
        Me.hsTiles.Maximum = 2
        Me.hsTiles.Name = "hsTiles"
        Me.hsTiles.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hsTiles.Size = New System.Drawing.Size(145, 17)
        Me.hsTiles.TabIndex = 9
        Me.hsTiles.TabStop = True
        '
        'FrameTileY
        '
        Me.FrameTileY.BackColor = System.Drawing.SystemColors.Control
        Me.FrameTileY.Controls.Add(Me.lblTileY)
        Me.FrameTileY.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameTileY.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameTileY.Location = New System.Drawing.Point(80, 256)
        Me.FrameTileY.Name = "FrameTileY"
        Me.FrameTileY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameTileY.Size = New System.Drawing.Size(49, 41)
        Me.FrameTileY.TabIndex = 6
        Me.FrameTileY.TabStop = False
        Me.FrameTileY.Text = "Tile Y"
        '
        'lblTileY
        '
        Me.lblTileY.BackColor = System.Drawing.SystemColors.Control
        Me.lblTileY.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTileY.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTileY.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTileY.Location = New System.Drawing.Point(8, 16)
        Me.lblTileY.Name = "lblTileY"
        Me.lblTileY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTileY.Size = New System.Drawing.Size(33, 17)
        Me.lblTileY.TabIndex = 8
        '
        'FrameTileCords
        '
        Me.FrameTileCords.BackColor = System.Drawing.SystemColors.Control
        Me.FrameTileCords.Controls.Add(Me.FrameTileX)
        Me.FrameTileCords.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameTileCords.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameTileCords.Location = New System.Drawing.Point(8, 240)
        Me.FrameTileCords.Name = "FrameTileCords"
        Me.FrameTileCords.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameTileCords.Size = New System.Drawing.Size(129, 65)
        Me.FrameTileCords.TabIndex = 4
        Me.FrameTileCords.TabStop = False
        Me.FrameTileCords.Text = "Tile Cordinates"
        '
        'FrameTileX
        '
        Me.FrameTileX.BackColor = System.Drawing.SystemColors.Control
        Me.FrameTileX.Controls.Add(Me.lblTileX)
        Me.FrameTileX.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameTileX.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameTileX.Location = New System.Drawing.Point(8, 16)
        Me.FrameTileX.Name = "FrameTileX"
        Me.FrameTileX.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameTileX.Size = New System.Drawing.Size(49, 41)
        Me.FrameTileX.TabIndex = 5
        Me.FrameTileX.TabStop = False
        Me.FrameTileX.Text = "Tile X"
        '
        'lblTileX
        '
        Me.lblTileX.BackColor = System.Drawing.SystemColors.Control
        Me.lblTileX.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTileX.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTileX.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTileX.Location = New System.Drawing.Point(8, 16)
        Me.lblTileX.Name = "lblTileX"
        Me.lblTileX.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTileX.Size = New System.Drawing.Size(33, 17)
        Me.lblTileX.TabIndex = 7
        '
        'lblTileType
        '
        Me.lblTileType.BackColor = System.Drawing.SystemColors.Control
        Me.lblTileType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTileType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTileType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTileType.Location = New System.Drawing.Point(8, 416)
        Me.lblTileType.Name = "lblTileType"
        Me.lblTileType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTileType.Size = New System.Drawing.Size(121, 17)
        Me.lblTileType.TabIndex = 17
        Me.lblTileType.Text = "TileType:"
        '
        'frmTools
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(151, 552)
        Me.ControlBox = False
        Me.Controls.Add(Me.cboTileType)
        Me.Controls.Add(Me.FrameMiniMap)
        Me.Controls.Add(Me.FrameSelectedTool)
        Me.Controls.Add(Me.FrameSelectedTile)
        Me.Controls.Add(Me.picDisplay)
        Me.Controls.Add(Me.FrameChooseTile)
        Me.Controls.Add(Me.FrameTileY)
        Me.Controls.Add(Me.FrameTileCords)
        Me.Controls.Add(Me.lblTileType)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Location = New System.Drawing.Point(3, 19)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTools"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Tools"
        Me.FrameMiniMap.ResumeLayout(False)
        Me.MiniMap.ResumeLayout(False)
        Me.FrameSelectedTool.ResumeLayout(False)
        Me.FrameSelectedTile.ResumeLayout(False)
        Me.FrameChooseTile.ResumeLayout(False)
        Me.FrameTileY.ResumeLayout(False)
        Me.FrameTileCords.ResumeLayout(False)
        Me.FrameTileX.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region 
#Region "Upgrade Support "
	Private Shared m_vb6FormDefInstance As frmTools
	Private Shared m_InitializingDefInstance As Boolean
	Public Shared Property DefInstance() As frmTools
		Get
			If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then
				m_InitializingDefInstance = True
				m_vb6FormDefInstance = New frmTools()
				m_InitializingDefInstance = False
			End If
			DefInstance = m_vb6FormDefInstance
		End Get
		Set
			m_vb6FormDefInstance = Value
		End Set
	End Property
#End Region 
	Private Sub frmTools_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Me.Show()
        'picDisplayGFX = picDisplay.CreateGraphics()
        picDisplayGFX = Graphics.FromHwnd(picDisplay.Handle)
        picSelectedGFX = picSelected.CreateGraphics()
        MiniMapGFX = MiniMap.CreateGraphics



        'picStorage.Image = System.Drawing.Image.FromFile(VB6.GetPath & "\Tiles.bmp")

		'Blt the first tile to the picselected box
		'UPGRADE_ISSUE: PictureBox property picStorage.hDC was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
		'UPGRADE_ISSUE: PictureBox property picSelected.hDC was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'BitBlt(picSelected.hDC, 0, 0, 32, 32, picStorage.hDC, 0, 0, SRCCOPY)
        Dim SelectedRect As Rectangle
        SelectedRect = New Rectangle(0, 0, 32, 32)
        picSelectedGFX.DrawImage(bTileSet, SelectedRect)
        'picSelectedGFX.DrawImageUnscaled(bTileSet, 0, 0, 32, 32)
        'picSelected.Refresh()
        'picSelected.Invalidate()
		'Let the scrollbars max value = to how many tiles there are
		hsTiles.Value = 0
        hsTiles.Maximum = (bTileSet.Width \ 32 - 4 + hsTiles.LargeChange - 1)
		vsTiles.Value = 0
        vsTiles.Maximum = (bTileSet.Height \ 32 - 3 + vsTiles.LargeChange - 1)
		'In case tiles.bmp width is too small change the max to 0
		If (hsTiles.Maximum - hsTiles.LargeChange + 1) < 0 Then hsTiles.Maximum = (0 + hsTiles.LargeChange - 1)
		Call RedrawTile()
	End Sub
	
	'UPGRADE_NOTE: hsTiles.Change was changed from an event to a procedure. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2010"'
	'UPGRADE_WARNING: HScrollBar event hsTiles.Change has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
	Private Sub hsTiles_Change(ByVal newScrollValue As Integer)
		Call RedrawTile()
	End Sub
	
	'UPGRADE_NOTE: hsTiles.Scroll was changed from an event to a procedure. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2010"'
	Private Sub hsTiles_Scroll_Renamed(ByVal newScrollValue As Integer)
		Call RedrawTile()
	End Sub
	
	Private Sub MiniMap_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles MiniMap.MouseDown
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = eventArgs.X
		Dim Y As Single = eventArgs.Y
		'This will check to make sure box doesnt go off screen it also moves box
		Dim tHScroll, tVScroll As Integer
		If Button = 1 Then
			tHScroll = X
			tVScroll = Y
			If tHScroll < 0 Then tHScroll = 0
			If tVScroll < 0 Then tVScroll = 0
			If tHScroll + Shape1.Width > MiniMap.ClientRectangle.Width Then tHScroll = MiniMap.ClientRectangle.Width - Shape1.Width
			If tVScroll + Shape1.Height > MiniMap.ClientRectangle.Height Then tVScroll = MiniMap.ClientRectangle.Height - Shape1.Height
			HScroll = tHScroll
			VScroll = tVScroll
		End If
	End Sub
	
	Private Sub MiniMap_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles MiniMap.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = eventArgs.X
		Dim Y As Single = eventArgs.Y
		'This will check to make sure box doesnt go off screen it also moves box
		Dim tHScroll, tVScroll As Integer
		If Button = 1 Then
			tHScroll = X
			tVScroll = Y
			If tHScroll < 0 Then tHScroll = 0
			If tVScroll < 0 Then tVScroll = 0
			If tHScroll + Shape1.Width > MiniMap.ClientRectangle.Width Then tHScroll = MiniMap.ClientRectangle.Width - Shape1.Width
			If tVScroll + Shape1.Height > MiniMap.ClientRectangle.Height Then tVScroll = MiniMap.ClientRectangle.Height - Shape1.Height
			HScroll = tHScroll
			VScroll = tVScroll
		End If
	End Sub
	
	Private Sub picDisplay_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles picDisplay.MouseDown
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = eventArgs.X
		Dim Y As Single = eventArgs.Y
		'If user presses left M. Button. then blit the pic to the Select Pic Box
		If Button = 1 Then
			'UPGRADE_ISSUE: PictureBox property picDisplay.hDC was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
			'UPGRADE_ISSUE: PictureBox property picSelected.hDC was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
            'BitBlt(picSelected.hDC, 0, 0, 32, 32, picDisplay.hDC, Snap(X, 32), Snap(Y, 32), SRCCOPY)

            'picSelected.Refresh()
            TempTileX = Snap(X, 32) + hsTiles.Value * 32
            TempTileY = Snap(Y, 32) + vsTiles.Value * 32
            Dim t As Rectangle = New Rectangle(TempTileX, TempTileY, 32, 32)
            picSelectedGFX.DrawImage(bTileSet, 0, 0, t, GraphicsUnit.Pixel)


        End If
	End Sub
	
    Private Sub RedrawTile()

        'An algorithnm that will blit tiles to the picDisplay box according to the scroll bars values
        Dim TempX, TempY As Integer
        Dim X, Y As Integer
        'UPGRADE_ISSUE: PictureBox method picDisplay.Cls was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'picDisplay.Cls()
        picDisplayGFX.Clear(System.Drawing.Color.Gray)
        TempX = 0
        TempY = 0
        'For Y = 0 To 64 Step 32
        Dim t As Rectangle, t2 As Rectangle
        For Y = vsTiles.Value * 32 To vsTiles.Value * 32 + 64 Step 32
            For X = hsTiles.Value * 32 To hsTiles.Value * 32 + 96 Step 32
                'UPGRADE_ISSUE: PictureBox property picStorage.hDC was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
                'UPGRADE_ISSUE: PictureBox property picDisplay.hDC was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
                'picDisplayGFX.DrawImage(bTileSet, TempX, TempY, 32, 32)
                't = New Rectangle(TempX, TempY, 32, 32)
                t2 = New Rectangle(X, Y, 32, 32)
                'picDisplayGFX.DrawImage(bTileSet, t, t2, GraphicsUnit.Pixel)
                picDisplayGFX.DrawImage(bTileSet, TempX, TempY, t2, GraphicsUnit.Pixel)

                'BitBlt(GetDC(picDisplay.Handle), TempX, TempY, 32, 32, picStorage.hDC, X, Y, SRCCOPY)
                'Because X will try t,o blit offscreen
                TempX = TempX + 32

            Next X
            TempY = TempY + 32
            TempX = 0
        Next Y

        ' picDisplay.Refresh()
    End Sub
    Private Sub RefreshMiniMap()

        Dim X, Y As Integer
        'UPGRADE_ISSUE: PictureBox method MiniMap.Cls was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'MiniMap.Cls()
        MiniMapGFX.Clear(System.Drawing.Color.Gray)
        For Y = 0 To UBound(Map, 2)
            For X = 0 To UBound(Map, 1)
                If Map(X, Y).TileType = 1 Then
                    'UPGRADE_ISSUE: PictureBox property picStorage.hDC was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
                    'UPGRADE_ISSUE: PictureBox property MiniMap.hDC was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'

                    ''BitBlt(MiniMap.hDC, X, Y, 1.5, 1.2, picStorage.hDC, Map(X, Y).TileX + 2, Map(X, Y).TileY + 2, SRCCOPY)
                    ''MiniMapgfx.DrawImageUnscaled(
                End If
            Next X
        Next Y
        'MiniMap.Refresh()
        RefreshMini = False
    End Sub

    Private Sub tmrGetInfo_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmrGetInfo.Tick
        'Refresh minimap if needed
        If RefreshMini = True Then RefreshMiniMap()

        'Refresh yellow box
        Shape1.Left = HScroll
        Shape1.Top = VScroll

        'Make the two forms windowstates the same
        frmTools.DefInstance.WindowState = frmMain.DefInstance.WindowState

        'Dont move left pos of tools if start is minimized
        If frmMain.DefInstance.WindowState <> 1 Then
            frmTools.DefInstance.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(frmMain.DefInstance.Left) + VB6.PixelsToTwipsX(frmMain.DefInstance.Width))
            frmTools.DefInstance.Top = frmMain.DefInstance.Top
        End If


        'Show the scroll bars values
        lblTileX.Text = CStr((Snap(CurX, 32) \ 32) + HScroll)
        lblTileY.Text = CStr((Snap(CurY, 32) \ 32) + VScroll)


        'Show what tool we are using (only 1 for now)
        lblSelectedTool.Text = SelectedTool
    End Sub

    'UPGRADE_NOTE: vsTiles.Change was changed from an event to a procedure. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2010"'
    'UPGRADE_WARNING: VScrollBar event vsTiles.Change has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    Private Sub vsTiles_Change(ByVal newScrollValue As Integer)
        Call RedrawTile()
    End Sub

    'UPGRADE_NOTE: vsTiles.Scroll was changed from an event to a procedure. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2010"'
    Private Sub vsTiles_Scroll_Renamed(ByVal newScrollValue As Integer)
        Call RedrawTile()
    End Sub
    Private Sub hsTiles_Scroll(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.ScrollEventArgs) Handles hsTiles.Scroll
        Select Case eventArgs.Type
            Case System.Windows.Forms.ScrollEventType.ThumbTrack
                hsTiles_Scroll_Renamed(eventArgs.NewValue)
            Case System.Windows.Forms.ScrollEventType.EndScroll
                hsTiles_Change(eventArgs.NewValue)
        End Select
    End Sub
    Private Sub vsTiles_Scroll(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.ScrollEventArgs) Handles vsTiles.Scroll
        Select Case eventArgs.Type
            Case System.Windows.Forms.ScrollEventType.ThumbTrack
                vsTiles_Scroll_Renamed(eventArgs.NewValue)
            Case System.Windows.Forms.ScrollEventType.EndScroll
                vsTiles_Change(eventArgs.NewValue)
        End Select
    End Sub

End Class
