using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Accessibility;

Console.WriteLine("Hello, World!");

WINEVENTPROC HandleWinEvent = (hWinEventHook, ev, hwnd, idObject, idChild, dwEventThread, dwmsEventTime) =>
{
    Console.WriteLine($"Event: {ev}");
};

var hook = PInvoke.SetWinEventHook(
    PInvoke.EVENT_SYSTEM_FOREGROUND,
    PInvoke.EVENT_SYSTEM_FOREGROUND,
    HMODULE.Null,
    HandleWinEvent,
    0, // all processes
    0, // all threads
    PInvoke.WINEVENT_OUTOFCONTEXT
);

// Start a message loop
while (PInvoke.GetMessage(out var msg, HWND.Null, 0, 0))
{
    PInvoke.TranslateMessage(msg);
    PInvoke.DispatchMessage(msg);
}