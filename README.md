# .NET window events watcher

This example app shows the minimum required code to call `SetWinEventHook` from
.NET code. This demo listens for foreground-changed events across the OS.

## Build & run

```pwsh
dotnet run
# ctrl-c to exit
```

## See also

* [CsWin32 example that spawns an HWND](https://github.com/microsoft/CsWin32/blob/99ddd314ea359d3a97afa82c735b6a25eb25ea32/test/WinRTInteropTest/Program.cs)
