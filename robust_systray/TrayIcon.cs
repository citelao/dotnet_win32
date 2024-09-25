using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;

internal class TrayIcon
{
    private string _tooltip = string.Empty;
    public string Tooltip {
        set { SetTooltip(value); }
        get { return _tooltip; }
    }

    public readonly Guid Guid;
    public readonly HWND OwnerHwnd;

    public TrayIcon(Guid guid, HWND ownerHwnd)
    {
        Guid = guid;
        OwnerHwnd = ownerHwnd;

        var notificationIconData = (new TrayIconMessageBuilder()
        {
            HWND = ownerHwnd,
            Guid = guid,
            Tooltip = _tooltip,
            Icon = PInvoke.LoadIcon(HINSTANCE.Null, PInvoke.IDI_APPLICATION)
        }).Build();
        if (!PInvoke.Shell_NotifyIcon(NOTIFY_ICON_MESSAGE.NIM_ADD, notificationIconData))
        {
            throw new Exception("Failed to add icon to the notification area.");
        }
        if(!PInvoke.Shell_NotifyIcon(NOTIFY_ICON_MESSAGE.NIM_SETVERSION, notificationIconData))
        {
            throw new Exception("Failed to set version of icon in the notification area.");
        }
    }

    private void SetTooltip(string newTip)
    {
        var notificationIconData = new NOTIFYICONDATAW
        {
            cbSize = (uint)Marshal.SizeOf<NOTIFYICONDATAW>(),
            hWnd = OwnerHwnd,
            uFlags = NOTIFY_ICON_DATA_FLAGS.NIF_TIP | NOTIFY_ICON_DATA_FLAGS.NIF_SHOWTIP | NOTIFY_ICON_DATA_FLAGS.NIF_GUID,
            szTip = newTip,
            guidItem = Guid,
        };
        if (!PInvoke.Shell_NotifyIcon(NOTIFY_ICON_MESSAGE.NIM_MODIFY, notificationIconData))
        {
            throw new Exception("Failed to modify icon in the notification area.");
        }
        _tooltip = newTip;
    }
}