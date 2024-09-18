using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Accessibility;

// Use the "variable" style function definition to validate the function
// signature. You could use a regular function as long as it takes the correct
// arguments.
WINEVENTPROC HandleWinEvent = (hWinEventHook, ev, hwnd, idObject, idChild, dwEventThread, dwmsEventTime) =>
{
    Console.WriteLine($"Event: {ev} hwnd: {hwnd} idObject: {idObject} idChild: {idChild} dwEventThread: {dwEventThread} dwmsEventTime: {dwmsEventTime}");
};

// CsWin32 generates 2 overloads for functions like this: one that takes a raw
// HMODULE for the 3rd arg (and returns a raw HWINEVENTHOOK) and one that takes
// a SafeHandle instead (and returns an UnhookWinEventSafeHandle).
//
// We're using the latter overload here because the result is IDisposable and
// can be cleaned up automatically for us. It's more idiomatic C#.

// Unfortunately, this causes a bit of type-erasure, since the 3rd arg is now a
// `SafeHandle`, which is an abstract class, rather than a specific handle to an
// HMODULE. We're not passing anything real in here, so we create a
// `NoReleaseSafeHandle` to pass in an empty value of `0`.
//
// See `NoReleaseSafeHandle` for an example that uses a non-empty value.
Console.WriteLine("Registering event hook...");
using var hook = PInvoke.SetWinEventHook(
    PInvoke.EVENT_SYSTEM_FOREGROUND,
    PInvoke.EVENT_SYSTEM_FOREGROUND,
    new NoReleaseSafeHandle(0),
    HandleWinEvent,
    0, // all processes
    0, // all threads
    PInvoke.WINEVENT_OUTOFCONTEXT
);

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