using FrostingCfg.NativeMethods;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace FrostingCfg
{
    internal static class ExtensionCfg
    {
        private const string ExtensionDllName = "frosting.dll";
        private const string ExtensionRegKey = @"SOFTWARE\Classes\Drive\shellex\FolderExtensions\{DED1CA7E-D1CE-4DEC-AFC0-FFEE4DEBA7E5}";

        internal static bool IsInstalled()
        {
            var rKey = Registry.CurrentUser.OpenSubKey(ExtensionRegKey);
            if (rKey != null)
            {
                rKey.Dispose();
                return true;
            }
            return false;
        }

        internal static int Register()
        {
            return RunRegsvr32(false);
        }

        internal static int Unregister()
        {
            return RunRegsvr32(true);
        }

        internal static string GetErrorDescription(int hResult)
        {
            var w32ex = new Win32Exception(hResult);
            return w32ex.Message;
        }

        internal static void RestartExplorer()
        {
            var trayWnd = User32.FindWindowW("Shell_TrayWnd", null);
            User32.GetWindowThreadProcessId(trayWnd, out int explorerPid);
            var explorerProcess = Process.GetProcessById(explorerPid);
            User32.PostMessageW(trayWnd, 0x5B4, IntPtr.Zero, IntPtr.Zero);
            explorerProcess.WaitForExit();
            using (var p = new Process())
            {
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.WorkingDirectory = Environment.GetEnvironmentVariable("WINDIR");
                p.Start();
            }
        }

        private static int RunRegsvr32(bool unregister)
        {
            using (var p = new Process())
            {
                p.StartInfo.FileName = "regsvr32.exe";
                var args = unregister ? "/u " : string.Empty;
                args += "/s " + ExtensionDllName;
                p.StartInfo.Arguments = args;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
                return p.ExitCode;
            }
        }
    }
}
