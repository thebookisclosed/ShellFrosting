using System;
using System.Runtime.InteropServices;

namespace FrostingCfg.NativeMethods
{
    internal static class UxTheme
    {
        [DllImport("uxtheme.dll", EntryPoint = "#133")]
        internal static extern int AllowDarkModeForWindow(IntPtr hWnd, [MarshalAs(UnmanagedType.I1)] bool bAllow);

        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        internal static extern int SetPreferredAppMode(int dwMode);

        [DllImport("uxtheme.dll", EntryPoint = "#191", CharSet = CharSet.Unicode)]
        internal static extern int SetWindowTheme(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszSubAppName, [MarshalAs(UnmanagedType.LPWStr)] string pszSubIdList);
    }

    internal static class User32
    {
        [DllImport("user32.dll", ExactSpelling = true)]
        internal static unsafe extern int SetWindowCompositionAttribute(IntPtr hWnd, WINDOWCOMPOSITIONATTRIBDATA* pAttrData);

        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern int SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct WINDOWCOMPOSITIONATTRIBDATA
    {
        internal int Attrib;
        internal int* pvData;
        internal int cbData;
    }
}
