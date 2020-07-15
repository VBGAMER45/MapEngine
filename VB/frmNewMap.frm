VERSION 5.00
Begin VB.Form frmNewMap 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "   New Map"
   ClientHeight    =   3090
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   4680
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3090
   ScaleWidth      =   4680
   StartUpPosition =   1  'CenterOwner
   Begin VB.Frame Frame1 
      Caption         =   "Map Size"
      Height          =   1875
      Left            =   120
      TabIndex        =   14
      Top             =   540
      Width           =   4335
      Begin VB.TextBox txtMapSizeCustomY 
         Height          =   285
         Left            =   2400
         TabIndex        =   9
         Text            =   "0"
         Top             =   1440
         Width           =   555
      End
      Begin VB.TextBox txtMapSizeCustomX 
         Height          =   285
         Left            =   1500
         TabIndex        =   8
         Text            =   "0"
         Top             =   1440
         Width           =   555
      End
      Begin VB.OptionButton optMapSizeCustom 
         Caption         =   "Custom"
         Height          =   195
         Left            =   240
         TabIndex        =   7
         Top             =   1440
         Width           =   975
      End
      Begin VB.OptionButton optMapSizeHuge 
         Caption         =   "Huge (200x200)"
         Height          =   195
         Left            =   2160
         TabIndex        =   6
         Top             =   960
         Width           =   1455
      End
      Begin VB.OptionButton optMapSizeExtraLarge 
         Caption         =   "Extra Large (140x140)"
         Height          =   195
         Left            =   2160
         TabIndex        =   5
         Top             =   660
         Width           =   1935
      End
      Begin VB.OptionButton optMapSizeLarge 
         Caption         =   "Large (100x100)"
         Height          =   195
         Left            =   2160
         TabIndex        =   4
         Top             =   360
         Width           =   1455
      End
      Begin VB.OptionButton optMapSizeMedium 
         Caption         =   "Medium (40x40)"
         Height          =   195
         Left            =   240
         TabIndex        =   3
         Top             =   960
         Width           =   1455
      End
      Begin VB.OptionButton optMapSizeSmall 
         Caption         =   "Small (20x20)"
         Height          =   195
         Left            =   240
         TabIndex        =   2
         Top             =   660
         Width           =   1455
      End
      Begin VB.OptionButton optMapSizeTiny 
         Caption         =   "Tiny (10x10)"
         Height          =   195
         Left            =   240
         TabIndex        =   1
         Top             =   360
         Width           =   1455
      End
      Begin VB.Label Label2 
         Caption         =   "X"
         Height          =   195
         Left            =   2160
         TabIndex        =   13
         Top             =   1500
         Width           =   315
      End
   End
   Begin VB.TextBox txtMapName 
      Height          =   315
      Left            =   1020
      MaxLength       =   200
      TabIndex        =   0
      Top             =   120
      Width           =   3435
   End
   Begin VB.CommandButton cmdOK 
      Caption         =   "&OK"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   2040
      TabIndex        =   10
      Top             =   2520
      Width           =   1215
   End
   Begin VB.CommandButton cmdCancel 
      Caption         =   "&Cancel"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   3360
      TabIndex        =   11
      Top             =   2520
      Width           =   1215
   End
   Begin VB.Label Label1 
      Caption         =   "Map Name:"
      Height          =   195
      Left            =   60
      TabIndex        =   12
      Top             =   180
      Width           =   915
   End
End
Attribute VB_Name = "frmNewMap"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmdCancel_Click()
    Unload Me
End Sub

Private Sub cmdOK_Click()
    If txtMapName.Text = vbNullString Then
        MsgBox "You need to enter a map name."
    Else
        If Me.optMapSizeTiny.Value = True Then
            ReDim Map(10, 10)
        End If
        If Me.optMapSizeSmall.Value = True Then
            ReDim Map(20, 20)
        End If
        If Me.optMapSizeMedium.Value = True Then
            ReDim Map(40, 40)
        End If
        If Me.optMapSizeLarge.Value = True Then
            ReDim Map(100, 100)
        End If
        If Me.optMapSizeExtraLarge.Value = True Then
            ReDim Map(140, 140)
        End If
        If Me.optMapSizeHuge.Value = True Then
            ReDim Map(200, 200)
        End If
        
        
        If Me.optMapSizeCustom.Value = True Then
            ReDim Map(txtMapSizeCustomX.Text, txtMapSizeCustomY.Text)
        End If
        
        strMapName = txtMapName.Text
        'Set the max of the scroll bars according to the map size
        frmMain.hsMap.Max = UBound(Map, 1) - 1
        frmMain.vsMap.Max = UBound(Map, 2) - 1
        Unload Me
    End If
End Sub

Private Sub txtMapSizeCustomX_Change()
    If IsNumeric(txtMapSizeCustomX.Text) Then txtMapSizeCustomX.Text = 0
End Sub

Private Sub txtMapSizeCustomY_Change()
    If IsNumeric(txtMapSizeCustomY.Text) Then txtMapSizeCustomY.Text = 0
End Sub
