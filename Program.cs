using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Accessibility;

WINEVENTPROC HandleWinEvent = (hWinEventHook, ev, hwnd, idObject, idChild, dwEventThread, dwmsEventTime) =>
{
    Console.WriteLine($"Event: {ev}");
};

Console.WriteLine("Registering event hook...");
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
// https://stackoverflow.com/a/12931599/788168
Console.WriteLine("Starting message loop...");
while (PInvoke.GetMessage(out var msg, HWND.Null, 0, 0))
{
    PInvoke.TranslateMessage(msg);
    PInvoke.DispatchMessage(msg);
}