//**********************************************************************
// Map Editor for C by vbgamer45
// Be sure to check out
// http://www.tileengines.com
//***********************************************************************
//Updates:
//Removed creating scrollbars. Now the style just added to main window
//Added scrolling with keyboard.
//Removed DrawBitmapSize just use DrawBitmap now.
//Items now show in the combo box.

//Known bugs aka my TODO list

//Crashing Saving File
//Finish New Map Dialog
//Flickers way too much

//Include Files
#include "stdafx.h"
#include "COMMDLG.H" //Used for Open and Save Common Dialogs
#include "resource.h"
#include "winuser.h"
#include "SHELLAPI.H" //Used for ShellExecute

//Constants
#define MAX_LOADSTRING 100
#define MAP_WIDTH 92
#define MAP_HEIGHT 64

struct MapSizeData{
	int MapWidth; // = 92;
	int MapHeight; //= 64;
} MapSize;
// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];            // The title bar text
TCHAR szMapName[MAX_LOADSTRING];           //Used to store Map Name
//Variables for Controls on the main window
HDC hDCfrmMain;
//frmTools Dialog Handles
HWND hfrmTools; //Handle to frmTools Dialog
HWND hpicDisplay; //Handle to the tile set picutre.
HWND hpicMiniMap; //Handle to the mini Map
HWND hpicSelected; //Handle to the pic selected
HWND hToolsHscroll;
HWND hToolsVscroll;
//frmTools variables
HANDLE hToolsTiles=NULL; 
HDC hDCpicDisplay=NULL;
HDC hDCpicSelected=NULL;
HDC hDCpicMiniMap=NULL; 

WNDPROC fnOldSrc; //Subclass for getting IDC_PICDisplay Messages

//Map Stuff							
bool bMapSaved = true; //Is the Map Saved?
bool bMapLoaded = false; //Is the Map Loaded?
bool bMapViewTileType =true; //Show the TileType?
bool bRefreshMini = true; //Refresh the Mini map?

//Main Map data structure
struct MapData{
	int TileX;
	int TileY;
	int TileType;
} Map[92][64];

int vScrollPos=0, hScrollPos=0; //Scoll bar Position values
int hToolsScrollPos=0; //Handle to Tile select Scrollbar
int TempTileX=0, TempTileY=0; //Current Tile Select X and Y posistion
int iCurX=0, iCurY=0; //Current Cursor Tile X and Tile Y position

// Foward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK	frmAbout(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK	frmNewMap(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK	frmTools(HWND, UINT, WPARAM, LPARAM);
void RedrawTiles();
void AdjustWindows(HWND hWnd);
int Snap(int Cordinate, int Dimension);
int Snap2(int Cordinate, int Dimension);
void PaintTile(int x, int y);
int GetTileType();
void EraseAll();
void FillScreen();
void RedrawMap();
void DrawBitmap(HDC hdc, HANDLE hBitmap, int SourceX, int SourceY, int DestX, int DestY, int iWidth, int iHeight);
LRESULT CALLBACK picDisplayProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);
void RedrawMiniMap();


int APIENTRY WinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPSTR     lpCmdLine,
                     int       nCmdShow)
{

	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_MAPEDITOR, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow)) 
		return FALSE;

	hAccelTable = LoadAccelerators(hInstance, (LPCTSTR)IDC_MAPEDITOR);

	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0)) 
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg)) 
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}


	return msg.wParam;
}

//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
//  COMMENTS:
//
//    This function and its usage is only necessary if you want this code
//    to be compatible with Win32 systems prior to the 'RegisterClassEx'
//    function that was added to Windows 95. It is important to call this function
//    so that the application will get 'well formed' small icons associated
//    with it.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;


	wcex.cbSize = sizeof(WNDCLASSEX); 

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= (WNDPROC)WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= hInstance;
	wcex.hIcon			= LoadIcon(hInstance, (LPCTSTR)IDI_MAPEDITOR);
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= (LPCSTR)IDC_MAPEDITOR;
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, (LPCTSTR)IDI_SMALL);

	return RegisterClassEx(&wcex);
}

//
//   FUNCTION: InitInstance(HANDLE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   HWND hWnd;

   hInst = hInstance; // Store instance handle in our global variable

   hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW | WS_VSCROLL | WS_HSCROLL,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);

   if (!hWnd)
      return FALSE;

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

//
//  FUNCTION: WndProc(HWND, unsigned, WORD, LONG)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_COMMAND	- process the application menu
//  WM_PAINT	- Paint the main window
//  WM_DESTROY	- post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;

	switch (message) 
	{
		case WM_CREATE:  //On creation of window

			//Set up the Scrollbars
			SetScrollRange(hWnd,SB_HORZ,0, 23*3,true);
			SetScrollPos(hWnd,SB_HORZ,0,true);
			SetScrollRange(hWnd,SB_VERT,0, 16*3,true);
			SetScrollPos(hWnd,SB_VERT,0,true);

			//Save the Handle to Device Context
			hDCfrmMain=GetDC(hWnd);
		    //Show The Tools Dialog
			hfrmTools=CreateDialog(hInst, (LPCTSTR)IDD_TOOLS, hWnd, (DLGPROC)frmTools);
			ShowWindow(hfrmTools,SW_SHOW);

		
			break;
		case WM_SIZE: //On window resized
			//Adjust the windows positions
			AdjustWindows(hWnd);
			break;

		case WM_COMMAND: // On Menu Click
			wmId    = LOWORD(wParam); 
			wmEvent = HIWORD(wParam); 
			// Parse the menu selections:
			switch (wmId)
			{
				case IDM_ABOUT: // On Menu About Click
				   DialogBox(hInst, (LPCTSTR)IDD_ABOUTBOX, hWnd, (DLGPROC)frmAbout);
				   break;
				case IDM_FILENEW: // On Menu New Click
				   DialogBox(hInst, (LPCTSTR)IDD_NEWMAP, hWnd, (DLGPROC)frmNewMap);
				   break;
				case IDM_FILEOPEN:
				{
	 
				    //Setup OPEN FILE Dialog
					long	lErrMsg = 0;
					OPENFILENAME ofn;
					char szFileName[MAX_PATH] = "";

					ZeroMemory(&ofn, sizeof(ofn)); //Needed to setup this structure

					ofn.lStructSize = sizeof(ofn);
					ofn.hwndOwner = hWnd;
					ofn.lpstrFilter = "Map Files (*.map)\0*.map\0All Files (*.*)\0*.*\0";
					ofn.lpstrFile = szFileName;
					ofn.nMaxFile = MAX_PATH;
					ofn.Flags = OFN_EXPLORER | OFN_FILEMUSTEXIST | OFN_HIDEREADONLY;
					ofn.lpstrDefExt = "map";

					if(GetOpenFileName(&ofn))
					{
						// Do something usefull with the filename stored in szFileName 
						HANDLE iFileHandle;
						iFileHandle=CreateFile(ofn.lpstrFile,GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE, 0,OPEN_EXISTING, 0, 0);
						//Get Map Height and Width

						//Get Map Info
						CloseHandle(iFileHandle);
					}
				   else
				   {
					  //What error was returned?
					   //TCHAR		sErrMsg[256];
					  //l//ErrMsg = CommDlgExtendedError();
					  //wsprintf(sErrMsg,TEXT("Error %d returned from GetOpenFileName()"), lErrMsg);
					  //MessageBox(NULL, sErrMsg, TEXT("OpenDialog"), MB_OK);
				   }
								
				   break; 
				}
				case IDM_FILESAVE: //On File Save Click
				{
					//Setup the Save Common Dialog
					OPENFILENAME ofn;
					char szFileName[MAX_PATH] = "";

					ZeroMemory(&ofn, sizeof(ofn)); //Needed for this structure

					ofn.lStructSize = sizeof(ofn);
					ofn.hwndOwner = hWnd;
					ofn.lpstrFilter = "Map Files (*.map)\0*.map\0All Files (*.*)\0*.*\0";
					ofn.lpstrFile = szFileName;
					ofn.nMaxFile = MAX_PATH;
					ofn.Flags = OFN_EXPLORER;
					ofn.lpstrDefExt = "map";

					if(GetSaveFileName(&ofn))
					{
						HANDLE iFileHandle;
						MapSize.MapHeight=64;
						MapSize.MapWidth =92;
						// Do something usefull with the filename stored in szFileName 
						iFileHandle=CreateFile(ofn.lpstrFile,GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, 0, CREATE_ALWAYS, 0, 0);
						//Write the Height and Width
						WriteFile(iFileHandle, &MapSize, sizeof(MapSize),NULL, 0);
						//Write the MapInfo
						WriteFile(iFileHandle, &Map, sizeof(Map),NULL, 0);

						CloseHandle(iFileHandle);
					}


					break;
				}
				case IDM_MAPVIEWFILETYPE: //On Menu View File Type Click
					{
						if(bMapViewTileType==true)
						{
							CheckMenuItem(GetMenu(hWnd),IDM_MAPVIEWFILETYPE,MF_UNCHECKED);
							bMapViewTileType=false;
						}
						else
						{
							CheckMenuItem(GetMenu(hWnd),IDM_MAPVIEWFILETYPE,MF_CHECKED);
							bMapViewTileType=true;
						}
						break;
					}
				case IDM_MAPCLEARSCREEN:
					{
						EraseAll(); //Erase all the information in Map
						RedrawMap(); 
						break;
					}
				case IDM_MAPFILLSCREEN:
					{
						FillScreen();
						RedrawMap();
						break;
					}
				case IDM_EXIT:
				   DestroyWindow(hWnd);
				   break;
				default:
				   return DefWindowProc(hWnd, message, wParam, lParam);
			}
			break;
		case WM_CONTEXTMENU: //On right click on the main window
			{
				
				int xPos = LOWORD(lParam); //Get the X Cord
				int yPos HIWORD(lParam); //Get the Y Cord
				//Create Popup Menu
				HMENU hPopupMenu;
				hPopupMenu=CreatePopupMenu();
				AppendMenu(hPopupMenu,MF_STRING | MF_POPUP,IDM_MAPCLEARSCREEN,"Clear Sc&reen");
				AppendMenu(hPopupMenu,MF_STRING | MF_POPUP,IDM_MAPFILLSCREEN,"&Fill Screen w/Selected Tile");
				TrackPopupMenu(hPopupMenu,TPM_RIGHTALIGN,xPos,yPos,0,hWnd,NULL);
				break;
			}

		case WM_LBUTTONDOWN:
			{

				int xPos = LOWORD(lParam);
				int yPos = HIWORD(lParam);
				
				//Paint the Tile
				PaintTile(xPos,yPos);
				RECT rt;
				GetClientRect(hWnd, &rt);
				InvalidateRect(hWnd,&rt,true);
				break;
			}
		case WM_MOUSEMOVE:
			{

				int xPos = LOWORD(lParam);
				int yPos = HIWORD(lParam);
				iCurX = Snap(xPos,32);
				iCurY = Snap(yPos,32); 
				xPos=(Snap(xPos,32) / 32) + hScrollPos;
				yPos=(Snap(yPos,32) / 32) + vScrollPos;
                 
				TCHAR szPos[MAX_LOADSTRING];
				_itoa(xPos,szPos,10);
				SetDlgItemText(hfrmTools,IDC_TILEX,szPos);
				_itoa(yPos,szPos,10);
				SetDlgItemText(hfrmTools,IDC_TILEY,szPos);
			 
				//Is Mouse Down? If so draw
				if(wParam==MK_LBUTTON)
					PaintTile(iCurX,iCurY);

			
				//Invaildate the main window to get it redrawn
				RECT rt;
				GetClientRect(hWnd, &rt);
				InvalidateRect(hWnd,&rt,true);
   
				break;
			}
		case WM_MOVE: //On main window moved
			AdjustWindows(hWnd);
			break;
		case WM_VSCROLL: //On vertical Scroll bar
			{
				switch(LOWORD(wParam))
					{
						case SB_PAGEDOWN:

						case SB_LINEDOWN:
							vScrollPos=min(16*3,vScrollPos+1);
							break;
						case SB_PAGEUP:
							
						case SB_LINEUP:
							vScrollPos=max(0,vScrollPos-1);
							break;
						case SB_TOP:
							vScrollPos=0;
							break;
						case SB_BOTTOM:
							vScrollPos = (16*3);
							break;
						case SB_THUMBPOSITION:
						case SB_THUMBTRACK:
							vScrollPos=HIWORD(wParam);
							break;

						default:
							break;
				}

				SetScrollPos(hWnd, SB_VERT, vScrollPos,TRUE);
				RECT rt;
				GetClientRect(hWnd, &rt);
				InvalidateRect(hWnd,&rt,true);
				break;
			}
		case WM_HSCROLL: //On Horizontal Scrollbar
			{
				switch(LOWORD(wParam))
					{
						case SB_PAGEDOWN:

						case SB_LINEDOWN:
							hScrollPos=min(23*3,hScrollPos+1);
							break;
						case SB_PAGEUP:
							
						case SB_LINEUP:
							hScrollPos=max(0,hScrollPos-1);
							break;
						case SB_TOP:
							hScrollPos=0;
							break;
						case SB_BOTTOM:
							hScrollPos = (23*3);
							break;
						case SB_THUMBPOSITION:
						case SB_THUMBTRACK:
							hScrollPos=HIWORD(wParam);
							break;

						default:
							break;
				}

				SetScrollPos(hWnd, SB_HORZ, hScrollPos,TRUE);
				RECT rt;
				GetClientRect(hWnd, &rt);
				InvalidateRect(hWnd,&rt,true);
				break;
			}
		case WM_KEYDOWN: //On key down
			switch (wParam)
			{
			case VK_HOME:
				SendMessage(hWnd,WM_VSCROLL, SB_TOP, 0l);
				break;
			case VK_END:
				SendMessage(hWnd,WM_VSCROLL, SB_BOTTOM, 0l);
				break;
			case VK_UP:
				SendMessage(hWnd,WM_VSCROLL, SB_LINEUP, 0l);
				break;
			case VK_DOWN:
				SendMessage(hWnd,WM_VSCROLL, SB_LINEDOWN, 0l);
				break;
			case VK_PRIOR:
				SendMessage(hWnd,WM_VSCROLL, SB_PAGEUP, 0l);
				break;
			case VK_NEXT:
				SendMessage(hWnd,WM_VSCROLL, SB_PAGEDOWN, 0l);
				break;
			case VK_LEFT:
				SendMessage(hWnd,WM_HSCROLL, SB_LINEUP, 0l);
				break;
			case VK_RIGHT:
				SendMessage(hWnd,WM_HSCROLL, SB_LINEDOWN, 0l);
				break;

			default:
				break;
			}
		case WM_PAINT: //On Paint
			hdc = BeginPaint(hWnd, &ps);
			Rectangle(hdc, iCurX, iCurY, iCurX+32, iCurY+32);
			RedrawMap();
			EndPaint(hWnd, &ps);
			UpdateWindow(hWnd);
			break;
		case WM_DESTROY: //On Main Window destroyed
			//Delete Objects and free Memory
			DeleteObject(hDCpicDisplay);
			DeleteObject(hDCpicSelected);
			
			PostQuitMessage(0);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
   }
   return 0;
}

// Mesage handler for about box.
LRESULT CALLBACK frmAbout(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
		case WM_INITDIALOG: 
				return TRUE;

		case WM_LBUTTONDOWN: //Launch the internet explorer with my site
			ShellExecute(hDlg,NULL, "http://www.tileengines.com", NULL, "C:\\",SW_SHOWNORMAL); 
	
			break;

		case WM_COMMAND:
			if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL) 
			{
				EndDialog(hDlg, LOWORD(wParam));
				return TRUE;
			}
			break;
	}
    return FALSE;
}

// Mesage handler for newmap box.
LRESULT CALLBACK frmNewMap(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
TCHAR cMapName[100];	

	switch (message)
	{
		case WM_INITDIALOG:
			{
				//Setup the Dialog
				SetDlgItemText(hDlg,IDC_TXTX,"0");
				SetDlgItemText(hDlg,IDC_TXTY,"0");
				CheckDlgButton(hDlg,IDC_OPTTINY,1);
				
				return TRUE;
			}

		case WM_COMMAND:

			if (LOWORD(wParam) == IDC_OK) 
			{
				//Check Mapname
				GetDlgItemText(hDlg,IDC_TXTMAPNAME, cMapName,100);
				if(strlen(cMapName)==0)
				{
					MessageBox(hDlg,"You need to enter a map name!","Error!",0);
					break;
				}
				else
				{
					//szMapName="";
					ZeroMemory(&szMapName, sizeof(szMapName));
					strcat(szMapName,cMapName);
				}
				//	szMapName=cMapName;

				if(IsDlgButtonChecked(hDlg,IDC_OPTTINY)==1)
				{
					//create a new map 10x10

				}
				if(IsDlgButtonChecked(hDlg,IDC_OPTSMALL)==1)
				{
					//create small map 20x20
				}
				if(IsDlgButtonChecked(hDlg,IDC_OPTMEDIUM)==1)
				{
					//create medium map 40x40
				}
				if(IsDlgButtonChecked(hDlg,IDC_OPTLARGE)==1)
				{
					//create large map 100x100
				}
				if(IsDlgButtonChecked(hDlg,IDC_OPTEXTRALARGE)==1)
				{
					//create extra large map 140x140	
				}
				if(IsDlgButtonChecked(hDlg,IDC_OPTHUGE)==1)
				{
					//create huge map 200x200
				}

				//Check Width and Height
				TCHAR szWidth[5];
				TCHAR szHeight[5];
				GetDlgItemText(hDlg,IDC_TXTX, szWidth,5);
				GetDlgItemText(hDlg, IDC_TXTY, szHeight,5);

				if(IsDlgButtonChecked(hDlg,IDC_OPTCUSTOM)==1)
				{

					if(strlen(szWidth)==0)
					{
						MessageBox(hDlg,"Missing Width size","Error!",0);
						break;
					}
					if(strlen(szHeight)==0)
					{
						MessageBox(hDlg,"Missing Height size","Error!",0);
						break;
					}
				}


				MessageBox(hDlg,szMapName,"Wow",0);
				EndDialog(hDlg, LOWORD(wParam));
				return TRUE;
			}

			if (LOWORD(wParam) == IDC_CANCEL) 
			{
				EndDialog(hDlg, LOWORD(wParam));
				return TRUE;
			}
			break;
	}
    return FALSE;
}
// Mesage handler for Tools Window
LRESULT CALLBACK frmTools(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	PAINTSTRUCT ps;
	switch (message)
	{
		case WM_INITDIALOG:
			{
				SetDlgItemText(hDlg,IDC_CBOTILETYPE,"walkable");
				SetDlgItemText(hDlg,IDC_TILEX,"0");
				SetDlgItemText(hDlg,IDC_TILEY,"0");
				//Load tiles
				
			    hToolsTiles=LoadImage(hInst,"tiles.bmp",IMAGE_BITMAP,128,96,LR_LOADFROMFILE);
				hDCpicDisplay = GetDC(GetDlgItem(hDlg,IDC_PICDISPLAY));
				hDCpicSelected = GetDC(GetDlgItem(hDlg,IDC_PICSELECTED));
				hDCpicMiniMap=GetDC(GetDlgItem(hDlg,IDC_PICMINIMAP));
				//Setup the function subclass for the tiles box
			    fnOldSrc = (WNDPROC) SetWindowLong(GetDlgItem(hDlg,IDC_PICDISPLAY), GWL_WNDPROC, (LONG) picDisplayProc);
			
				//Add strings to the the combo box
				
				SendMessage(GetDlgItem(hDlg, IDC_CBOTILETYPE), CB_ADDSTRING, 0, (LPARAM) "walkable");
				SendMessage(GetDlgItem(hDlg, IDC_CBOTILETYPE), CB_ADDSTRING, 1, (LPARAM) "nonwalkable");
				SendMessage(GetDlgItem(hDlg, IDC_CBOTILETYPE), WM_SETREDRAW, TRUE,0);

				//Save IDC_HSTILES HWND and do scrolling
				hToolsHscroll=GetDlgItem(hDlg,IDC_HSTILES);
				
				SetScrollRange(hToolsHscroll,SB_HORZ,0,(128 / 32) -4,true);
				SetScrollPos(hToolsHscroll,SB_HORZ,0,true);
				//SetWindowPos(GetDlgItem(hDlg, IDC_CBOTILETYPE),NULL,10,457,131,21,0);
				UpdateWindow(hToolsHscroll);
			    
				if(hToolsTiles==0)
					MessageBox(hDlg,"Error loading bitmap","LoadImage Error",MB_ICONINFORMATION);

				if(hDCpicDisplay==0)
					MessageBox(hDlg,"Error getting dc","GetDC Error",MB_ICONINFORMATION);

				RedrawTiles();
				bRefreshMini=true;
				return TRUE;
			}
		case WM_PAINT:
			{
				HDC hdc;
				hdc=BeginPaint(hDlg,&ps);
				RedrawTiles();
				if(bRefreshMini==true)
					RedrawMiniMap();
				EndPaint(hDlg,&ps);

			}
			break;
		case WM_HSCROLL:
			{
				switch(LOWORD(wParam))
					{
						case SB_PAGEDOWN:

						case SB_LINEDOWN:
							hToolsScrollPos=min((128 / 32) -4,hToolsScrollPos+1);
							break;
						case SB_PAGEUP:
							
						case SB_LINEUP:
							hToolsScrollPos=max(0,hToolsScrollPos-1);
							break;
						case SB_TOP:
							hToolsScrollPos=0;
							break;
						case SB_BOTTOM:
							hToolsScrollPos = (128 /32) -4;
							break;
						case SB_THUMBPOSITION:
						case SB_THUMBTRACK:
							hToolsScrollPos=HIWORD(wParam);
							break;

						default:
							break;
				}

				SetScrollPos(hToolsHscroll, SB_CTL, hToolsScrollPos,TRUE);
				RedrawTiles();
				break;
			}

		case WM_COMMAND:

			break;
	}
    return FALSE;
}

void RedrawTiles()
{
//Purpose: To redraw tiles in the tileset
	
	//Draw selected Tile
	DrawBitmap(hDCpicSelected,hToolsTiles,TempTileX,TempTileY,0,0,32,32);
	
	//Draw the tileset
	int TempX=0, TempY=0;
	for(int y=(0*32); y <= 64 ; y+=32)
	{
		for(int x=(0*32);x <= 96 ; x+=32)
		{
			DrawBitmap(hDCpicDisplay,hToolsTiles,x,y, TempX, TempY,32,32);
			TempX+=32;
		}
		TempY+=32;
		TempX=0;
	}

}
void AdjustWindows(HWND hWnd)
{
	//Purpose: To adjust the windows to their proper positions
	SetWindowPos(hWnd,0,0,0,650,550,SWP_SHOWWINDOW);
	RECT rt;
	GetClientRect(hWnd, &rt);
	SetWindowPos(hfrmTools,0,rt.right+20,0,300, rt.bottom + 62, SWP_SHOWWINDOW);
}
int Snap(int Cordinate, int Dimension)
{
	//Purpose: Small algorithm that snaps to grid sort of
	//Paramters: an integer containg the Cordinate, integer containg the Dimension
	//Returns - an integer
	return (Cordinate / Dimension) * Dimension;
}
int Snap2(int Cordinate, int Dimension)
{
	//Purpose: Small algorithm that takes a number and converts it into a pixel cord.
	//Parameters: an integer containg the Cordinate, integer containg the Dimension
	//Returns - an integer
	return (Cordinate * Dimension);
}
void PaintTile(int x, int y)
{
//Purpose: To paint a tile given the x and y cordinates
//Parameters: integer contating the x cordinate, integer containing the y cordinate
	int TempX, TempY;
	TCHAR szBuffer[1]; //Buffer that holds the tiletype
	
	//Draw the selected tile to the point on the screen
	DrawBitmap(hDCfrmMain,hToolsTiles,TempTileX,TempTileY, Snap(x,32),Snap(y,32),32,32);

	//Get the tile cordinates
	TempX = Snap(x, 32) / 32 + hScrollPos;
	TempY = Snap(y, 32) / 32 + vScrollPos;

	//Save the tile infomation into the map structure
	Map[TempX][TempY].TileX=TempTileX;
	Map[TempX][TempY].TileY=TempTileY;
	Map[TempX][TempY].TileType=GetTileType();

	//Print TileTypeInfo
	if(bMapViewTileType==true)
	{
		SetBkMode(hDCfrmMain,TRANSPARENT); //Set Text Mode to Transparent
		itoa(Map[TempX][TempY].TileType,szBuffer,10); //Convert Integer to string
		TextOut(hDCfrmMain,Snap2(x,32),Snap2(y,32),szBuffer,1); //Draw the tiletype to screen
	
	}
				
	//Refresh the mini map
	bRefreshMini=true;
	RedrawMiniMap();
}
int GetTileType()
{
//Purpose: To Get The text from IDC_CBOTILETYPE and check what is selected
//Returns: an int containting the tiletype
	TCHAR szcboText[20];
	GetDlgItemText(hfrmTools,IDC_CBOTILETYPE,szcboText,20);
	if(strcmp(szcboText,"walkable")==0)
		return 1;
	else 
		return 2;

}
void EraseAll()
{
	//Purpose: To Erase all map information used for clear screen

   int x=0, y=0;
   for(y=0; y < MAP_HEIGHT -1 ; y++)
		for(x=0; x < MAP_WIDTH -1; x++)
		{
			Map[x][y].TileType = 0;
			Map[x][y].TileX = 0;
			Map[x][y].TileY = 0;
		}


	RedrawMiniMap();
}
void FillScreen()
{
   //Purpose: To fill the screen with the selected tile.
   
   int x=0, y=0;
   for(y=0; y < MAP_HEIGHT -1; y++)
		for(x=0; x < MAP_WIDTH -1; x++)
		{
			Map[x][y].TileType = GetTileType();
			Map[x][y].TileX = TempTileX;
			Map[x][y].TileY = TempTileY;
		}

   RedrawMiniMap();
}
void RedrawMap()
{
	TCHAR szBuffer[1]; //Buffer to hold the tiletype
	//Set the Text mode to TRANSPARENT
	 SetBkMode(hDCfrmMain,TRANSPARENT);

   for(int y=vScrollPos; y < MAP_HEIGHT -1; y++)
		for(int x=hScrollPos; x < MAP_WIDTH -1; x++)
			if(Map[x][y].TileType != 0)
			{
				DrawBitmap(hDCfrmMain,hToolsTiles,Map[x][y].TileX, Map[x][y].TileY, Snap2(x,32) - Snap2(hScrollPos,32),Snap2(y,32) - Snap2(vScrollPos,32),32,32);	
			
				if(bMapViewTileType==true)
				{
					itoa(Map[x][y].TileType,szBuffer,10);
					//Draw the tiletype in the corner of the tile
					TextOut(hDCfrmMain,Snap2(x,32) - Snap2(hScrollPos,32),Snap2(y,32) - Snap2(vScrollPos,32),szBuffer,1);
					
				}

			}


}
void DrawBitmap(HDC hdc, HANDLE hBitmap, int SourceX, int SourceY, int DestX, int DestY, int iWidth, int iHeight)
{
//Purpose: To draw Bitmap's a little easier
	HDC hdcMem;

	hdcMem = CreateCompatibleDC(hdc);
	SelectObject(hdcMem, hBitmap);

	BitBlt(hdc, DestX, DestY, iWidth, iHeight, hdcMem, SourceX, SourceY, SRCCOPY);

	//Free up the memory
	DeleteDC(hdcMem);

}

//Subclass for the Tileset - IDC_PICDISPLAY
LRESULT CALLBACK picDisplayProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
		case WM_PAINT:
			RedrawTiles();
			break;
		case WM_LBUTTONDOWN:
			int xPos = LOWORD(lParam);
			int yPos = HIWORD(lParam);
			//Hold the selected tile cords
			TempTileX= Snap(xPos,32);
			TempTileY = Snap(yPos,32);
			//Draw the Selected Tile
			DrawBitmap(hDCpicSelected,hToolsTiles,TempTileX,TempTileY,0,0,32,32);
			break;
	}
return CallWindowProcA(fnOldSrc, hWnd, message, wParam, lParam);
}
void RedrawMiniMap()
{
	//Purpose: To Redraw the mini map
	for (int y=0; y < MAP_HEIGHT -1; y++)
		for(int x=0; x < MAP_WIDTH -1; x++)
			if(Map[x][y].TileType !=0)
				DrawBitmap(hDCpicMiniMap,hToolsTiles, Map[x][y].TileX, Map[x][y].TileY, x, y, 2, 1);


	bRefreshMini=false;

}
