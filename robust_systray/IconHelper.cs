using System.ComponentModel;
using Microsoft.Win32.SafeHandles;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

internal class IconHelper
{
    // There are many ways to load files! Here's one. I assume we'll need to add
    // additional parameters in the future.
    public static SafeFileHandle LoadIconFromFile(string path)
    {
        // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-loadimagew
        var safeIconHandle = PInvoke.LoadImage(
            NoReleaseSafeHandle.Null,
            path,
            GDI_IMAGE_TYPE.IMAGE_ICON,
            0,
            0,
            // TODO: LR_DEFAULTSIZE?
            IMAGE_FLAGS.LR_LOADFROMFILE
        );

        if (safeIconHandle.IsInvalid)
        {
            throw new Win32Exception();
        }

        return safeIconHandle;
    }

    // System icons, like IDI_APPLICATION, are integers, not valid strings.
    //
    // https://learn.microsoft.com/en-us/windows/win32/menurc/about-icons#icon-types
    public static HICON LoadSystemIcon(PCWSTR iconId)
    {
        // TODO: LoadIconMetric
        var icon = PInvoke.LoadIcon(HINSTANCE.Null, iconId);
        return icon;

        // TODO: maybe make a generic SafeHandle that doesn't type-erase?
        // return new NoReleaseSafeHandle(icon);
    }
}