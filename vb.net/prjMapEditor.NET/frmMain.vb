Option Strict Off
Option Explicit On 
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text


Friend Class frmMain
    Inherits System.Windows.Forms.Form
    Dim frmMainGFX As Graphics
    Dim MainFont As New Font("Arial", 12)
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
            Static fTerminateCalled As Boolean
            If Not fTerminateCalled Then
                Form_Terminate_Renamed()
                fTerminateCalled = True
            End If
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents tmrChangeValue As System.Windows.Forms.Timer
    Public WithEvents picTiles As System.Windows.Forms.PictureBox
    Public WithEvents Picture1 As System.Windows.Forms.PictureBox
    Public WithEvents vsMap As System.Windows.Forms.VScrollBar
    Public WithEvents hsMap As System.Windows.Forms.HScrollBar
    Public WithEvents Shape1 As System.Windows.Forms.Label
    Public WithEvents mnuFileNewMap As System.Windows.Forms.MenuItem
    Public WithEvents mnuFileSave As System.Windows.Forms.MenuItem
    Public WithEvents mnuFileLoad As System.Windows.Forms.MenuItem
    Public WithEvents mnuFileExportBMP As System.Windows.Forms.MenuItem
    Public WithEvents mnuFileSep1 As System.Windows.Forms.MenuItem
    Public WithEvents mnuFileExit As System.Windows.Forms.MenuItem
    Public WithEvents mnuFile As System.Windows.Forms.MenuItem
    Public WithEvents mnuMapClearScreen As System.Windows.Forms.MenuItem
    Public WithEvents mnuMapFillScreen As System.Windows.Forms.MenuItem
    Public WithEvents mnuMapTileType As System.Windows.Forms.MenuItem
    Public WithEvents mnuMap As System.Windows.Forms.MenuItem
    Public WithEvents mnuToolsNormalPaint As System.Windows.Forms.MenuItem
    Public WithEvents mnuTools As System.Windows.Forms.MenuItem
    Public WithEvents mnuWindowsCascade As System.Windows.Forms.MenuItem
    Public WithEvents mnuWindows As System.Windows.Forms.MenuItem
    Public WithEvents mnuHelpAbout As System.Windows.Forms.MenuItem
    Public WithEvents mnuHelp As System.Windows.Forms.MenuItem
    Public MainMenu1 As System.Windows.Forms.MainMenu
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMain))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.tmrChangeValue = New System.Windows.Forms.Timer(Me.components)
        Me.picTiles = New System.Windows.Forms.PictureBox
        Me.Picture1 = New System.Windows.Forms.PictureBox
        Me.vsMap = New System.Windows.Forms.VScrollBar
        Me.hsMap = New System.Windows.Forms.HScrollBar
        Me.Shape1 = New System.Windows.Forms.Label
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.mnuFile = New System.Windows.Forms.MenuItem
        Me.mnuFileNewMap = New System.Windows.Forms.MenuItem
        Me.mnuFileSave = New System.Windows.Forms.MenuItem
        Me.mnuFileLoad = New System.Windows.Forms.MenuItem
        Me.mnuFileExportBMP = New System.Windows.Forms.MenuItem
        Me.mnuFileSep1 = New System.Windows.Forms.MenuItem
        Me.mnuFileExit = New System.Windows.Forms.MenuItem
        Me.mnuMap = New System.Windows.Forms.MenuItem
        Me.mnuMapClearScreen = New System.Windows.Forms.MenuItem
        Me.mnuMapFillScreen = New System.Windows.Forms.MenuItem
        Me.mnuMapTileType = New System.Windows.Forms.MenuItem
        Me.mnuTools = New System.Windows.Forms.MenuItem
        Me.mnuToolsNormalPaint = New System.Windows.Forms.MenuItem
        Me.mnuWindows = New System.Windows.Forms.MenuItem
        Me.mnuWindowsCascade = New System.Windows.Forms.MenuItem
        Me.mnuHelp = New System.Windows.Forms.MenuItem
        Me.mnuHelpAbout = New System.Windows.Forms.MenuItem
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.SuspendLayout()
        '
        'tmrChangeValue
        '
        Me.tmrChangeValue.Enabled = True
        Me.tmrChangeValue.Interval = 20
        '
        'picTiles
        '
        Me.picTiles.BackColor = System.Drawing.SystemColors.Control
        Me.picTiles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picTiles.Cursor = System.Windows.Forms.Cursors.Default
        Me.picTiles.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picTiles.ForeColor = System.Drawing.SystemColors.ControlText
        Me.picTiles.Location = New System.Drawing.Point(0, 0)
        Me.picTiles.Name = "picTiles"
        Me.picTiles.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picTiles.Size = New System.Drawing.Size(132, 36)
        Me.picTiles.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picTiles.TabIndex = 3
        Me.picTiles.TabStop = False
        Me.picTiles.Visible = False
        '
        'Picture1
        '
        Me.Picture1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
        Me.Picture1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Picture1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Picture1.Location = New System.Drawing.Point(624, 512)
        Me.Picture1.Name = "Picture1"
        Me.Picture1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture1.Size = New System.Drawing.Size(17, 17)
        Me.Picture1.TabIndex = 2
        Me.Picture1.TabStop = False
        '
        'vsMap
        '
        Me.vsMap.Cursor = System.Windows.Forms.Cursors.Default
        Me.vsMap.LargeChange = 1
        Me.vsMap.Location = New System.Drawing.Point(624, 0)
        Me.vsMap.Maximum = 1
        Me.vsMap.Name = "vsMap"
        Me.vsMap.Size = New System.Drawing.Size(17, 513)
        Me.vsMap.TabIndex = 1
        Me.vsMap.TabStop = True
        '
        'hsMap
        '
        Me.hsMap.Cursor = System.Windows.Forms.Cursors.Default
        Me.hsMap.LargeChange = 1
        Me.hsMap.Location = New System.Drawing.Point(0, 512)
        Me.hsMap.Maximum = 1
        Me.hsMap.Name = "hsMap"
        Me.hsMap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hsMap.Size = New System.Drawing.Size(625, 17)
        Me.hsMap.TabIndex = 0
        Me.hsMap.TabStop = True
        '
        'Shape1
        '
        Me.Shape1.BackColor = System.Drawing.Color.Transparent
        Me.Shape1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Shape1.Location = New System.Drawing.Point(136, 8)
        Me.Shape1.Name = "Shape1"
        Me.Shape1.Size = New System.Drawing.Size(32, 32)
        Me.Shape1.TabIndex = 4
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile, Me.mnuMap, Me.mnuTools, Me.mnuWindows, Me.mnuHelp})
        '
        'mnuFile
        '
        Me.mnuFile.Index = 0
        Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileNewMap, Me.mnuFileSave, Me.mnuFileLoad, Me.mnuFileExportBMP, Me.mnuFileSep1, Me.mnuFileExit})
        Me.mnuFile.Text = "&File"
        '
        'mnuFileNewMap
        '
        Me.mnuFileNewMap.Index = 0
        Me.mnuFileNewMap.Text = "New Map"
        '
        'mnuFileSave
        '
        Me.mnuFileSave.Index = 1
        Me.mnuFileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.mnuFileSave.Text = "&Save"
        '
        'mnuFileLoad
        '
        Me.mnuFileLoad.Index = 2
        Me.mnuFileLoad.Shortcut = System.Windows.Forms.Shortcut.CtrlL
        Me.mnuFileLoad.Text = "&Load"
        '
        'mnuFileExportBMP
        '
        Me.mnuFileExportBMP.Index = 3
        Me.mnuFileExportBMP.Shortcut = System.Windows.Forms.Shortcut.CtrlE
        Me.mnuFileExportBMP.Text = "&Export to BMP"
        '
        'mnuFileSep1
        '
        Me.mnuFileSep1.Index = 4
        Me.mnuFileSep1.Text = "-"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Index = 5
        Me.mnuFileExit.Shortcut = System.Windows.Forms.Shortcut.CtrlX
        Me.mnuFileExit.Text = "E&xit"
        '
        'mnuMap
        '
        Me.mnuMap.Index = 1
        Me.mnuMap.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuMapClearScreen, Me.mnuMapFillScreen, Me.mnuMapTileType})
        Me.mnuMap.Text = "&Map"
        '
        'mnuMapClearScreen
        '
        Me.mnuMapClearScreen.Index = 0
        Me.mnuMapClearScreen.Shortcut = System.Windows.Forms.Shortcut.CtrlR
        Me.mnuMapClearScreen.Text = "Clear Sc&reen"
        '
        'mnuMapFillScreen
        '
        Me.mnuMapFillScreen.Index = 1
        Me.mnuMapFillScreen.Shortcut = System.Windows.Forms.Shortcut.CtrlF
        Me.mnuMapFillScreen.Text = "&Fill Screen w/Selected Tile"
        '
        'mnuMapTileType
        '
        Me.mnuMapTileType.Checked = True
        Me.mnuMapTileType.Index = 2
        Me.mnuMapTileType.Text = "&View Tile Type"
        '
        'mnuTools
        '
        Me.mnuTools.Index = 2
        Me.mnuTools.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuToolsNormalPaint})
        Me.mnuTools.Text = "&Tools"
        '
        'mnuToolsNormalPaint
        '
        Me.mnuToolsNormalPaint.Checked = True
        Me.mnuToolsNormalPaint.Index = 0
        Me.mnuToolsNormalPaint.Shortcut = System.Windows.Forms.Shortcut.CtrlN
        Me.mnuToolsNormalPaint.Text = "&Normal Paint"
        '
        'mnuWindows
        '
        Me.mnuWindows.Index = 3
        Me.mnuWindows.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuWindowsCascade})
        Me.mnuWindows.Text = "&Windows"
        '
        'mnuWindowsCascade
        '
        Me.mnuWindowsCascade.Index = 0
        Me.mnuWindowsCascade.Shortcut = System.Windows.Forms.Shortcut.CtrlC
        Me.mnuWindowsCascade.Text = "&Cascade"
        '
        'mnuHelp
        '
        Me.mnuHelp.Index = 4
        Me.mnuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuHelpAbout})
        Me.mnuHelp.Text = "&Help"
        '
        'mnuHelpAbout
        '
        Me.mnuHelpAbout.Index = 0
        Me.mnuHelpAbout.Text = "&About"
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(640, 528)
        Me.Controls.Add(Me.picTiles)
        Me.Controls.Add(Me.Picture1)
        Me.Controls.Add(Me.vsMap)
        Me.Controls.Add(Me.hsMap)
        Me.Controls.Add(Me.Shape1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(11, 30)
        Me.MaximizeBox = False
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Map Editor 1.0 by vbgamer45 http://www.visualbasiczone.com"
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support "
    Private Shared m_vb6FormDefInstance As frmMain
    Private Shared m_InitializingDefInstance As Boolean
    Public Shared Property DefInstance() As frmMain
        Get
            If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then
                m_InitializingDefInstance = True
                m_vb6FormDefInstance = New frmMain
                m_InitializingDefInstance = False
            End If
            DefInstance = m_vb6FormDefInstance
        End Get
        Set(ByVal Value As frmMain)
            m_vb6FormDefInstance = Value
        End Set
    End Property
#End Region

    Private Sub frmMain_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim X As Single = eventArgs.X
        Dim Y As Single = eventArgs.Y

        'UPGRADE_ISSUE: Form method frmMain.PopupMenu was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        ''If Button = 2     Then PopupMenu(mnuMap)

        'If button pressed paint tile
        If Button = eventArgs.Button.Left And SelectedTool = "Normal Paint" Then
            PaintTile(X, Y)
            'UPGRADE_ISSUE: Shape property Shape1.BorderColor was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
            ''Shape1.BorderColor = &HFFFFFF
        End If

        'frmMain.DefInstance.Refresh()

    End Sub

    Private Sub frmMain_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim X As Single = eventArgs.X
        Dim Y As Single = eventArgs.Y

        'Tell the TileX and TileY where they are
        CurX = X
        CurY = Y

        'If mouse moved move square thingy

        Shape1.Left = Snap(X, 32)
        Shape1.Top = Snap(Y, 32)


        'If button pressed paint tile
        If eventArgs.Button.Left And SelectedTool = "Normal Paint" Then
            PaintTile(X, Y)
            'UPGRADE_ISSUE: Shape property Shape1.BorderColor was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
            ''Shape1.BorderColor = &HFFFFFF
        End If

    End Sub

    Private Sub frmMain_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim X As Single = eventArgs.X
        Dim Y As Single = eventArgs.Y
        'UPGRADE_ISSUE: Shape property Shape1.BorderColor was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        ''If Button = 1 Then Shape1.BorderColor = &HFF0000
    End Sub

    Private Sub LoadDefault()
        Call Cascade() 'cascade the windows

        ReDim Map(23 * 4, 16 * 4) 'Set the ubound of the map array

        'Set the max of the scroll bars according to the map size
        hsMap.Maximum = (23 * (4 - 1) + hsMap.LargeChange - 1)
        vsMap.Maximum = (16 * (4 - 1) + vsMap.LargeChange - 1)

    End Sub
    Private Sub frmMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        ' Me.SetStyle(ControlStyles.DoubleBuffer, True)

        frmMainGFX = Me.CreateGraphics


        bShowTileType = True
        picTiles.Image = System.Drawing.Image.FromFile(VB6.GetPath & "\Tiles.bmp")
        SelectedTool = "Normal Paint"
        Call LoadDefault()
        Call Cascade()
    End Sub

    Private Sub PaintTile(ByVal X As Single, ByVal Y As Single)

        On Error GoTo errHandle
        Dim TempX, TempY As Integer

        'If X <= frmMain.DefInstance.ClientRectangle.Width And X >= 0 And Y <= frmMain.DefInstance.ClientRectangle.Height And Y >= 0 Then

        'UPGRADE_ISSUE: PictureBox property picTiles.hDC was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'UPGRADE_ISSUE: Form property frmMain.hDC was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        ''BitBlt(frmMain.DefInstance.hDC, Snap(X, 32), Snap(Y, 32), 32, 32, picTiles.hDC, TempTileX, TempTileY, SRCCOPY)

        'Holds data for where the info should be saved to the map array
        TempX = Snap(X, 32) \ 32 + hsMap.Value
        TempY = Snap(Y, 32) \ 32 + vsMap.Value
        Dim t2 As Rectangle
        t2 = New Rectangle(TempTileX, TempTileY, 32, 32)
        'picDisplayGFX.DrawImage(bTileSet, t, t2, GraphicsUnit.Pixel)
        'MsgBox(CStr(X))
        frmMainGFX.DrawImage(bTileSet, Snap(X, 32) + hsMap.Value, Snap(Y, 32) + vsMap.Value, t2, GraphicsUnit.Pixel)

        'Save map info to the map array
        Map(TempX, TempY).TileX = TempTileX
        Map(TempX, TempY).TileY = TempTileY
        Map(TempX, TempY).TileType = GetTileType()
        If bShowTileType = True Then
            'UPGRADE_ISSUE: Form property frmMain.CurrentX was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'

            'Me.CurrentX = Snap(X, 32)
            'UPGRADE_ISSUE: Form property frmMain.CurrentY was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
            'Me.CurrentY = Snap(Y, 32)
            'UPGRADE_ISSUE: Form method frmMain.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'

            'Me.print(CStr(Map(TempX, TempY).TileType))
            frmMainGFX.DrawString(CStr(Map(TempX, TempY).TileType), MainFont, Brushes.Black, Snap2(X, 32), Snap2(Y, 32))
        End If

        RefreshMini = True
        'End If
        Exit Sub
errHandle:
        MsgBox("Error_frmMain.PaintTile : " & Err.Number & " " & Err.Description)
    End Sub

    Private Sub Cascade()
        'Cascade the windows
        frmTools.DefInstance.Show()
        frmTools.DefInstance.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(frmMain.DefInstance.Width) + 1)
        frmTools.DefInstance.Top = 0
        frmMain.DefInstance.Show()
        frmMain.DefInstance.Top = 0
        frmMain.DefInstance.Left = 0
    End Sub

    'UPGRADE_NOTE: Form_Terminate was upgraded to Form_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    'UPGRADE_WARNING: frmMain event Form.Terminate has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    Private Sub Form_Terminate_Renamed()
        Call Terminate()
    End Sub

    'UPGRADE_WARNING: Form event frmMain.Unload has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    Private Sub frmMain_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Closed
        Call Terminate()
    End Sub

    Private Sub Terminate()
        ' frmTools.DefInstance.Close()
        'frmMain.DefInstance.Close()
        End
    End Sub

    Private Sub RedrawMap()
        Dim X, Y As Integer
        'Draw the map Algorithm
        'UPGRADE_ISSUE: Form method frmMain.Cls was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'frmMain.Cls()
        frmMainGFX.Clear(System.Drawing.Color.Gray)
        frmMain.DefInstance.ForeColor = System.Drawing.Color.White
        Dim t2 As Rectangle
        For Y = vsMap.Value To vsMap.Value + 16
            If Y > UBound(Map, 2) Then Exit For
            For X = hsMap.Value To hsMap.Value + 23
                If X > UBound(Map, 1) Then Exit For
                If Map(X, Y).TileType > 0 Then 'If the tiletype is anything but 0
                    'UPGRADE_ISSUE: PictureBox property picTiles.hDC was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
                    'UPGRADE_ISSUE: Form property frmMain.hDC was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
                    '' BitBlt(frmMain.DefInstance.hDC, Snap2(X, 32) - Snap2((hsMap.Value), 32), Snap2(Y, 32) - Snap2((vsMap.Value), 32), 32, 32, picTiles.hDC, Map(X, Y).TileX, Map(X, Y).TileY, SRCCOPY)

                    t2 = New Rectangle(Map(X, Y).TileX, Map(X, Y).TileY, 32, 32)
                    frmMainGFX.DrawImage(bTileSet, Snap2(X, 32) - Snap2((hsMap.Value), 32), Snap2(Y, 32) - Snap2((vsMap.Value), 32), t2, GraphicsUnit.Pixel)

                    If bShowTileType = True Then
                        'UPGRADE_ISSUE: Form property frmMain.CurrentX was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
                        'Me.CurrentX = Snap2(X, 32) - Snap2((hsMap.Value), 32)
                        'UPGRADE_ISSUE: Form property frmMain.CurrentY was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
                        'Me.CurrentY = Snap2(Y, 32) - Snap2((vsMap.Value), 32)
                        'UPGRADE_ISSUE: Form method frmMain.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
                        frmMainGFX.DrawString(CStr(Map(X, Y).TileType), MainFont, Brushes.Black, Snap2(X, 32) - Snap2((hsMap.Value), 32), Snap2(Y, 32) - Snap2((vsMap.Value), 32))
                        'Me.Print(CStr(Map(X, Y).TileType))
                    End If
                End If
            Next X
        Next Y

        '  frmMain.DefInstance.Refresh()
        RefreshMini = True
    End Sub

    'UPGRADE_NOTE: hsMap.Change was changed from an event to a procedure. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2010"'
    'UPGRADE_WARNING: HScrollBar event hsMap.Change has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    Private Sub hsMap_Change(ByVal newScrollValue As Integer)
        HScroll = newScrollValue
        Call RedrawMap()
    End Sub

    'UPGRADE_NOTE: hsMap.Scroll was changed from an event to a procedure. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2010"'
    Private Sub hsMap_Scroll_Renamed(ByVal newScrollValue As Integer)
        HScroll = newScrollValue
        Call RedrawMap()
    End Sub

    Public Sub mnuFileExit_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileExit.Popup
        mnuFileExit_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuFileExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileExit.Click
        Call Terminate()
    End Sub

    Public Sub mnuFileExportBMP_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileExportBMP.Popup
        mnuFileExportBMP_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuFileExportBMP_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileExportBMP.Click
        SaveFileDialog.FileName = ""
        SaveFileDialog.Title = "Save as BMP"
        SaveFileDialog.ShowDialog()
        If SaveFileDialog.FileName = vbNullString Then Exit Sub

        'frmMainGFX.Sa()
        'UPGRADE_WARNING: SavePicture was upgraded to System.Drawing.Image.Save and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        ''frmMain.DefInstance.Image.Save(CD1.FileName) 'Export the Start.hdc to a BMP file

    End Sub

    Public Sub mnuFileLoad_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileLoad.Popup
        mnuFileLoad_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuFileLoad_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileLoad.Click
        OpenFileDialog.FileName = ""
        OpenFileDialog.Title = "Load Map"
        OpenFileDialog.ShowDialog()
        If OpenFileDialog.FileName = vbNullString Then Exit Sub
        Call LoadMap(OpenFileDialog.FileName)
    End Sub

    Public Sub mnuFileNewMap_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileNewMap.Popup
        mnuFileNewMap_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuFileNewMap_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileNewMap.Click
        Call LoadDefault()
        Call RedrawMap()
    End Sub

    Public Sub mnuFileSave_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileSave.Popup
        mnuFileSave_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuFileSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileSave.Click
        SaveFileDialog.FileName = ""
        SaveFileDialog.Title = "Save Map"
        SaveFileDialog.ShowDialog()
        If SaveFileDialog.FileName = vbNullString Then Exit Sub
        Call SaveMap(SaveFileDialog.FileName)
    End Sub

    Public Sub mnuMapClearScreen_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuMapClearScreen.Popup
        mnuMapClearScreen_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuMapClearScreen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuMapClearScreen.Click
        Call EraseAll()
    End Sub

    Public Sub mnuMapFillScreen_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuMapFillScreen.Popup
        mnuMapFillScreen_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuMapFillScreen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuMapFillScreen.Click
        'This fills the screen with the selected tile
        Dim X, Y As Integer
        For Y = 0 To UBound(Map, 2)
            For X = 0 To UBound(Map, 1)
                Map(X, Y).TileType = GetTileType()
                Map(X, Y).TileX = TempTileX
                Map(X, Y).TileY = TempTileY
            Next X
        Next Y
        Call RedrawMap()
    End Sub

    Public Sub mnuMapTileType_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuMapTileType.Popup
        mnuMapTileType_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuMapTileType_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuMapTileType.Click
        If mnuMapTileType.Checked = True Then
            bShowTileType = False
            mnuMapTileType.Checked = False
            Call RedrawMap()
        Else
            bShowTileType = True
            mnuMapTileType.Checked = True
            Call RedrawMap()
        End If
    End Sub

    Public Sub mnuToolsNormalPaint_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuToolsNormalPaint.Popup
        mnuToolsNormalPaint_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuToolsNormalPaint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuToolsNormalPaint.Click
        SelectedTool = "Normal Paint"
    End Sub

    Public Sub mnuWindowsCascade_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuWindowsCascade.Popup
        mnuWindowsCascade_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuWindowsCascade_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuWindowsCascade.Click
        Call Cascade()
    End Sub

    Private Sub tmrChangeValue_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmrChangeValue.Tick
        'If the two values arent equal set them equal
        If hsMap.Value <> HScroll Or vsMap.Value <> VScroll Then
            hsMap.Value = HScroll
            vsMap.Value = VScroll
        End If
    End Sub
    Private Sub SaveMap(ByVal strFilename As String)


        On Error GoTo NoFile 'If file doesnt save properly show error
        Dim F As Integer
        'UPGRADE_NOTE: Width was upgraded to Width_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        'UPGRADE_NOTE: Height was upgraded to Height_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Dim Height_Renamed, Width_Renamed As Short
        F = FreeFile()

        Width_Renamed = UBound(Map, 1)
        Height_Renamed = UBound(Map, 2)

        FileOpen(F, strFilename, OpenMode.Binary, OpenAccess.Write, OpenShare.LockWrite)
        'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        FilePut(F, Width_Renamed)
        'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        FilePut(F, Height_Renamed)
        'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        FilePut(F, Map)
        FileClose(F)

        Call RedrawMap()
        MsgBox("The Map was succesfully saved", MsgBoxStyle.OKOnly, "Map Editor 1.0")


NoFile:
        RedrawMap()
        MsgBox("Error_frmMain_SaveMap: " & Err.Number & " " & Err.Description)

    End Sub

    Private Sub LoadMap(ByVal strFilename As String)

        On Error GoTo NoFile 'If file doesnt exist dont open it
        Dim F As Integer
        'UPGRADE_NOTE: Width was upgraded to Width_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        'UPGRADE_NOTE: Height was upgraded to Height_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Dim Height_Renamed, Width_Renamed As Short
        F = FreeFile()
        FileOpen(F, strFilename, OpenMode.Binary, OpenAccess.Read, OpenShare.LockRead)
        Call EraseAll()
        'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        FileGet(F, Width_Renamed)
        'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        FileGet(F, Height_Renamed)
        ReDim Map(Width_Renamed, Height_Renamed)
        'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        FileGet(F, Map)
        FileClose(F)


        Call RedrawMap()
        MsgBox("The Map was succesfully loaded", MsgBoxStyle.OKOnly, "Map Editor 1.0")
        Exit Sub

NoFile:
        RedrawMap()
        MsgBox("Error_frmMain_LoadMap: " & Err.Number & " " & Err.Description)

    End Sub

    Private Sub EraseAll()
        'Erase the map
        Dim X, Y As Integer
        For Y = 0 To UBound(Map, 2)
            For X = 0 To UBound(Map, 1)

                Map(X, Y).TileType = 0
                Map(X, Y).TileX = 0
                Map(X, Y).TileY = 0

            Next X
        Next Y
        Call RedrawMap()
    End Sub

    'UPGRADE_NOTE: vsMap.Change was changed from an event to a procedure. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2010"'
    'UPGRADE_WARNING: VScrollBar event vsMap.Change has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    Private Sub vsMap_Change(ByVal newScrollValue As Integer)
        VScroll = newScrollValue
        Call RedrawMap()
    End Sub

    'UPGRADE_NOTE: vsMap.Scroll was changed from an event to a procedure. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2010"'
    Private Sub vsMap_Scroll_Renamed(ByVal newScrollValue As Integer)
        VScroll = newScrollValue
        Call RedrawMap()
    End Sub
    Private Function GetTileType() As Short
        If frmTools.DefInstance.cboTileType.Text = "walkable" Then
            'Tile is walkable
            GetTileType = 1
        Else
            'Tile is nonwalkable
            GetTileType = 2
        End If

    End Function
    Private Sub hsMap_Scroll(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.ScrollEventArgs) Handles hsMap.Scroll
        Select Case eventArgs.Type
            Case System.Windows.Forms.ScrollEventType.ThumbTrack
                hsMap_Scroll_Renamed(eventArgs.NewValue)
            Case System.Windows.Forms.ScrollEventType.EndScroll
                hsMap_Change(eventArgs.NewValue)
        End Select
    End Sub
    Private Sub vsMap_Scroll(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.ScrollEventArgs) Handles vsMap.Scroll
        Select Case eventArgs.Type
            Case System.Windows.Forms.ScrollEventType.ThumbTrack
                vsMap_Scroll_Renamed(eventArgs.NewValue)
            Case System.Windows.Forms.ScrollEventType.EndScroll
                vsMap_Change(eventArgs.NewValue)
        End Select
    End Sub
End Class