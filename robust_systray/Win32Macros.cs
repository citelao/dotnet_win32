using Windows.Win32.Foundation;

// TODO: public
internal static class Win32Macros
{
    // https://learn.microsoft.com/en-us/windows/win32/winmsg/loword
    public static nint LOWORD(nint wParam)
    {
        return wParam & 0xFFFF;
    }

    // https://learn.microsoft.com/en-us/windows/win32/winmsg/hiword
    public static nint HIWORD(nint wParam)
    {
        return wParam >> 16;
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/windowsx/nf-windowsx-get_x_lparam
    public static short GET_X_LPARAM(nint lParam)
    {
        return (short)lParam;
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/windowsx/nf-windowsx-get_y_lparam
    public static short GET_Y_LPARAM(nint lParam)
    {
        // https://github.com/microsoft/CsWin32/issues/1194
        return (short)(lParam >> 16);
    }
}