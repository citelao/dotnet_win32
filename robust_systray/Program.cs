using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;
using Windows.Win32.UI.WindowsAndMessaging;
using Windows.Win32.Graphics.Gdi;


// Heavily inspired by https://github.com/microsoft/CsWin32/blob/99ddd314ea359d3a97afa82c735b6a25eb25ea32/test/WinRTInteropTest/Program.cs

const string WindowClassName = "SimpleSystrayWindow";

// Use the variable style function here to get easy access to the full function
// signature. This function isn't used directly.
// WNDPROC intermediateWndProc = (hwnd, msg, wParam, lParam) =>
// {
//     return WndProc(hwnd, msg, wParam, lParam);
// };

unsafe
{
    fixed (char* pClassName = WindowClassName)
    {
        var wndClass = new WNDCLASSEXW
        {
            // You need to include the size of this struct.
            cbSize = (uint)Marshal.SizeOf<WNDCLASSEXW>(),

            // Seems to work without this, but it seems sketchy to leave it out.
            hInstance = PInvoke.GetModuleHandle(default(PCWSTR)),

            // Required to actually run your WndProc (and the app will crash if
            // null).
            lpfnWndProc = WndProc,

            // Required to identify the window class in CreateWindowEx.
            lpszClassName = pClassName,
        };

        // We ignore the returned class atom & use the class name directly.
        // https://devblogs.microsoft.com/oldnewthing/20080501-00/?p=22503
        PInvoke.RegisterClassEx(wndClass);
    }
}

HWND hwnd;
unsafe
{
    hwnd = PInvoke.CreateWindowEx(
        0, // dwExStyle
        WindowClassName,
        "Hello, Windows!", // lpWindowName
        0, // dwStyle
        0, // x
        0, // y
        640, // nWidth
        480, // nHeight
        HWND.Null, // hWndParent
        NoReleaseSafeHandle.Null, // hMenu
        NoReleaseSafeHandle.Null, // hInstance
        null // lpParam
    );
}

var guid = Guid.Parse("bc540dbe-f04e-4c1c-a5a0-01b32095b04c");
using var icon = IconHelper.LoadIconFromFile("assets/simple_icon.ico"); 
// var windowMessage = PInvoke.RegisterWindowMessage($"TrayIconWindowMessage-{guid}");
const uint windowMessage = PInvoke.WM_USER + 1;
var trayIcon = new TrayIcon(guid, hwnd, windowMessage)
{
    Tooltip = "Hello, Windows!",
    Icon = (HICON)icon.DangerousGetHandle(),
};

Console.WriteLine("Starting message loop...");
Console.WriteLine("Press Ctrl-C to exit.");

while (PInvoke.GetMessage(out var msg, HWND.Null, 0, 0))
{
    PInvoke.TranslateMessage(msg);
    PInvoke.DispatchMessage(msg);
}

static LRESULT WndProc(HWND hwnd, uint msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg)
    {
        case PInvoke.WM_CLOSE:
            PInvoke.DestroyWindow(hwnd);
            break;

        case PInvoke.WM_DESTROY:
            PInvoke.PostQuitMessage(0);
            break;

        case windowMessage:
            Console.WriteLine($"Tray: {hwnd} {msg} {wParam} {lParam}");
            break;

        default:
            return PInvoke.DefWindowProc(hwnd, msg, wParam, lParam);
    }

    return new LRESULT(0);
}