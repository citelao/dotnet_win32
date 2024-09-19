using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

HWND hwnd;
unsafe
{
    hwnd = PInvoke.CreateWindowEx(
        0, // dwExStyle
        "STATIC", // lpClassName
        "Hello, Windows!", // lpWindowName
        WINDOW_STYLE.WS_OVERLAPPEDWINDOW, // dwStyle
        0, // x
        0, // y
        640, // nWidth
        480, // nHeight
        HWND.Null, // hWndParent
        new NoReleaseSafeHandle(0), // hMenu
        new NoReleaseSafeHandle(0), // hInstance
        null // lpParam
    );
}

PInvoke.ShowWindow(hwnd, SHOW_WINDOW_CMD.SW_NORMAL);
PInvoke.UpdateWindow(hwnd);

// Start a message loop to pump messages, or the hook won't be called.
// https://stackoverflow.com/a/12931599/788168
Console.WriteLine("Starting message loop...");
Console.WriteLine("Press Ctrl-C to exit.");
while (PInvoke.GetMessage(out var msg, HWND.Null, 0, 0))
{
    PInvoke.TranslateMessage(msg);
    PInvoke.DispatchMessage(msg);
}

// Adapted directly from:
// https://github.com/microsoft/CsWin32/blob/99ddd314ea359d3a97afa82c735b6a25eb25ea32/test/WinRTInteropTest/Program.cs#L144
class NoReleaseSafeHandle : SafeHandle
{
    public NoReleaseSafeHandle(int value)
        : base(IntPtr.Zero, true)
    {
        this.SetHandle(new IntPtr(value));
    }

    public override bool IsInvalid => throw new NotImplementedException();

    protected override bool ReleaseHandle()
    {
        return true;
    }
}