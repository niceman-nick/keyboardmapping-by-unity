using UnityEngine;
using System;
using System.Runtime.InteropServices;
using Application = UnityEngine.Application;
using WindowsNotifyIcon = System.Windows.Forms.NotifyIcon;
using ContextMenu = System.Windows.Forms.ContextMenu;
using WindowstrayMenuItem = System.Windows.Forms.MenuItem;
using Windowstrayicon = System.Drawing.Icon;

public class WinSetting_v042 : MonoBehaviour
{
    #region Win函数常量
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

    [DllImport("Dwmapi.dll")]
    static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    [DllImport("user32.dll")]
    public static extern bool SetCapture();
    [DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rect);

    [DllImport("user32.dll")]
    private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int width, int height, bool repaint);

    private const int WS_POPUP = 0x800000;
    private const int GWL_EXSTYLE = -20;
    private const int GWL_STYLE = -16;
    private const int WS_EX_LAYERED = 0x00080000;
    private const int WS_BORDER = 0x00800000;
    private const int WS_CAPTION = 0x00C00000;
    private const int SWP_SHOWWINDOW = 0x0040;
    private const int LWA_COLORKEY = 0x00000001;
    private const int LWA_ALPHA = 0x00000002;
    private const int WS_EX_TRANSPARENT = 0x20;
    private const int SWP_NOSIZE = 0x0001;
    private const int SWP_NOMOVE = 0x0002;
    private const int WS_EX_TOOLWINDOW = 0x00000080;
    //
    const int WM_NCLBUTTONDOWN = 0x00A1;
    const int HTCAPTION = 0x02;
    #endregion
    //
    public int 窗口宽度 = 600;
    public int 窗口高度 = 400;

    int intExTemp;

    int winz = -1;
    //
    [HideInInspector]
    public bool 是否置顶 = true;
    //string 托盘菜单置顶提示;
    [HideInInspector]
    public bool 是否穿透窗口 = false;

    public GameObject UI控制器;

    WindowsNotifyIcon trayIcon;
    WindowstrayMenuItem 置顶MenuItem;
    WindowstrayMenuItem 锁定MenuItem;

    IntPtr hwnd;

    Vector2 MosePosition;
    Vector2 LastMosePosition;

    bool hasFocus;

    // Use this for initialization
    void Awake()
    {
        hwnd = GetActiveWindow();
        SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED);//去边框并且透明
        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_BORDER & ~WS_CAPTION);//去掉边框
        var margins = new MARGINS() { cxLeftWidth = -1 };//窗口透明
        DwmExtendFrameIntoClientArea(hwnd, ref margins);//窗口透明
        SetWindowPos(hwnd, -1, 0, 0, 窗口宽度, 窗口高度, SWP_SHOWWINDOW | SWP_NOMOVE );//置顶
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        系统托盘();
    }
    void Update()
    {
        intExTemp = GetWindowLong(hwnd, GWL_EXSTYLE);

        MosePosition = Input.mousePosition;

        
        if(hasFocus==true&& 是否穿透窗口 == false)
        {
            if (Vector2.Distance(MosePosition, LastMosePosition) != 0 && Input.GetMouseButton(0))//检测鼠标是否移动，没有移动就不要执行拖动窗口，否则按钮不起作用
            {
                ReleaseCapture();
                SendMessage(hwnd, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        
    }
    void LateUpdate()
    {
        LastMosePosition = Input.mousePosition;
    }
    public void 是否置顶判断()
    {
        是否置顶 = !是否置顶;
        if (是否置顶 == true)
        {
            winz = -1;
            置顶MenuItem.Text = "置顶✓";
        }
        else
        {
            winz = -2;
            置顶MenuItem.Text = "置顶";
        }
        SetWindowPos(hwnd, winz, 0, 0, 窗口宽度, 窗口高度, SWP_SHOWWINDOW | SWP_NOMOVE| SWP_NOSIZE);//置顶
    }
    public void 是否穿透判断()
    {
        是否穿透窗口 = !是否穿透窗口;
        if (是否穿透窗口 == true)
        {
            intExTemp = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, intExTemp | WS_EX_TRANSPARENT | WS_EX_LAYERED);
            锁定MenuItem.Text = "锁定✓";
        }
        else
        {
            intExTemp &= ~WS_EX_TRANSPARENT;
            SetWindowLong(hwnd, GWL_EXSTYLE, intExTemp);
            锁定MenuItem.Text = "锁定";
        }
    }

    public void 系统托盘()//参考GPT
    {
        // 创建 NotifyIcon 对象
        trayIcon = new WindowsNotifyIcon();

        // 设置托盘图标的图标
        trayIcon.Icon = new Windowstrayicon(Application.dataPath + "/icon.ico");// 自定义图标的路径

        // 设置托盘图标可见
        trayIcon.Visible = true;

        置顶MenuItem = new WindowstrayMenuItem("置顶✓", 托盘是否置顶判断);
        锁定MenuItem = new WindowstrayMenuItem("锁定", 托盘是否穿透判断);

        // 创建右键菜单
        trayIcon.ContextMenu = new ContextMenu(new WindowstrayMenuItem[]
        {
            new ("风格切换",风格切换),
            置顶MenuItem,
            锁定MenuItem,
            new("大",窗口尺寸_大),
            new("中",窗口尺寸_中),
            new("小",窗口尺寸_小),
            new("使用说明",打开帮助文档),
            new ("退出", 托盘退出程序)
        });
        trayIcon.DoubleClick += 双击托盘显示窗口;
    }
    public void 双击托盘显示窗口(object sender, EventArgs e)
    {
        ShowWindow(hwnd, 1);
        SetForegroundWindow(hwnd);
    }
    public void 风格切换(object sender, EventArgs e)
    {
        UI控制器.GetComponent<UI控制_v042>().风格切换();
    }

    public void 托盘是否置顶判断(object sender, EventArgs e)
    {
        是否置顶判断();
    }
    public void 托盘是否穿透判断(object sender, EventArgs e)
    {
        是否穿透判断();
    }
    public void 窗口尺寸_大(object sender, EventArgs e)
    {
        窗口宽度 = 720;
        窗口高度 = 480;
        SetWindowPos(hwnd, winz, 0, 0, 窗口宽度, 窗口高度, SWP_SHOWWINDOW | SWP_NOMOVE );//置顶
    }
    public void 窗口尺寸_中(object sender, EventArgs e)
    {
        窗口宽度 = 600;
        窗口高度 = 400;
        SetWindowPos(hwnd, winz, 0, 0, 窗口宽度, 窗口高度, SWP_SHOWWINDOW | SWP_NOMOVE );//置顶
    }
    public void 窗口尺寸_小(object sender, EventArgs e)
    {
        窗口宽度 = 480;
        窗口高度 = 320;
        SetWindowPos(hwnd, winz, 0, 0, 窗口宽度, 窗口高度, SWP_SHOWWINDOW | SWP_NOMOVE );//置顶
    }

    public void 打开帮助文档(object sender, EventArgs e)
    {
        Application.OpenURL("Readme.txt");
    }
    public void 托盘退出程序(object sender, EventArgs e)
    {
        Application.Quit();
    }

    private void OnApplicationFocus(bool focus)
    {
        hasFocus = focus;
    }

    private void OnApplicationQuit()
    {
        if (trayIcon != null)
        {
            trayIcon.Dispose();
        }
    }
}