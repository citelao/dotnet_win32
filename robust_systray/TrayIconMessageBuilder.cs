using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;
using Windows.Win32.UI.WindowsAndMessaging;

// TODO: public
internal class TrayIconMessageBuilder
{
    public Guid Guid;
    public HWND HWND;
    public string? Tooltip = null;
    public HICON Icon;

    public NOTIFYICONDATAW Build()
    {
        // Generate the flags for the notification.
        //
        // We always use GUID IDs for these tray icons, so specify by it
        // default.
        NOTIFY_ICON_DATA_FLAGS flags = NOTIFY_ICON_DATA_FLAGS.NIF_GUID;
        if (Tooltip != null)
        {
            flags |= NOTIFY_ICON_DATA_FLAGS.NIF_TIP | NOTIFY_ICON_DATA_FLAGS.NIF_SHOWTIP;
        }
        if (Icon != HICON.Null)
        {
            // TODO: handle updates that don't change the icon
            flags |= NOTIFY_ICON_DATA_FLAGS.NIF_ICON;
        }

        // https://github.com/microsoft/WindowsAppSDK/discussions/519
        // https://github.com/microsoft/WindowsAppSDK/issues/713
        // https://github.com/File-New-Project/EarTrumpet/blob/bd42f1e235386c35c0989df8f2af8aba951f1848/EarTrumpet/UI/Helpers/ShellNotifyIcon.cs#L18
        // https://learn.microsoft.com/en-us/windows/win32/shell/notification-area
        // https://learn.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-notifyicondataa
        // https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shell_notifyicona
        return new NOTIFYICONDATAW()
        {
            // Required. You need to include the size of this struct.
            cbSize = (uint)Marshal.SizeOf<NOTIFYICONDATAW>(),

            // Required. An HWND is required to register the icon with the system.
            // Window messages go there.
            hWnd = HWND,

            // Required. Indicates which of the other members contain valid data.
            // NIF_TIP and NIF_SHOWTIP are only required if you want to use szTip.
            uFlags = flags,

            // TODO
            uCallbackMessage = 0,

            // Required. The icon to display.
            // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-loadicona
            // https://learn.microsoft.com/en-us/windows/win32/menurc/about-icons
            hIcon = Icon,

            // Optional. You probably want a tooltip for your icon, though.
            szTip = Tooltip,

            // Required. A GUID to identify the icon. This should be persistent
            // across launches and unique to your app!
            //
            // We use GUID identifiers and not the alternative (HWND + a uint
            // ID) because HWNDs are not typically persistent across app
            // relaunches & reboots---so if you want Windows to remember if your
            // app has been pinned to the tray, you need the consistent GUID.
            // (TODO: validate completely).
            guidItem = Guid,

            Anonymous = new()
            {
                // Recommended. VERSION_4 has been present since Vista and gives your
                // app much richer window messages & more control over the icon tooltip.
                //
                // https://stackoverflow.com/q/41649303/788168
                uVersion = PInvoke.NOTIFYICON_VERSION_4
            }
        };
    }
}