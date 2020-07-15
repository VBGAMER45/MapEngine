VERSION 5.00
Begin VB.Form frmAbout 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "About Map Editor"
   ClientHeight    =   1635
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4515
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1635
   ScaleWidth      =   4515
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton cmdClose 
      Caption         =   "&Close"
      Default         =   -1  'True
      Height          =   375
      Left            =   1560
      TabIndex        =   1
      Top             =   1080
      Width           =   1215
   End
   Begin VB.Label lblUrl 
      BackColor       =   &H8000000A&
      Caption         =   "http://www.tileengines.com"
      Height          =   255
      Left            =   240
      TabIndex        =   2
      Top             =   480
      Width           =   3495
   End
   Begin VB.Label lblAbout 
      Caption         =   "By vbgamer45 "
      Height          =   255
      Left            =   240
      TabIndex        =   0
      Top             =   240
      Width           =   3255
   End
End
Attribute VB_Name = "frmAbout"
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

Private Sub cmdClose_Click()
    Unload Me
End Sub
