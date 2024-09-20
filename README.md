# .NET + Win32 examples

These self-contained example apps present the minimum required code to call
different Win32 APIs from .NET code, using modern techniques like
[CsWin32](https://github.com/microsoft/CsWin32). 

## Demos

* **SetWinEventHook** (`/window_events/`): listen to foreground changes across the OS
* **CreateWindowEx** (`/simple_window/`): create a simple window
* **Shell_NotifyIcon** (`/simple_systray/`): create a simple systray icon

## Build & run

```pwsh
dotnet build

# Run with the project name:
dotnet run --project .\simple_window\simple_window.csproj
# or
cd .\simple_window
dotnet run
```

## See also

* MSDN: [Using Messages and Message Queues](https://learn.microsoft.com/en-us/windows/win32/winmsg/using-messages-and-message-queues)
* MSDN: [About Window Procedures](https://learn.microsoft.com/en-us/windows/win32/winmsg/about-window-procedures)
* MSDN: [`SetWinEventHook`](https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwineventhook)
* MSDN: [`GetMessage`](https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmessage)
* [CsWin32 example that spawns an HWND](https://github.com/microsoft/CsWin32/blob/99ddd314ea359d3a97afa82c735b6a25eb25ea32/test/WinRTInteropTest/Program.cs)
