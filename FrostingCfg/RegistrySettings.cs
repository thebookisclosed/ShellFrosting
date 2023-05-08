using Microsoft.Win32;

namespace FrostingCfg
{
    internal static class RegistrySettings
    {
        internal static SettingBag Bag = new SettingBag();

        internal static void Refresh()
        {
            using (var rKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced"))
            {
                var regObj = rKey.GetValue("TaskbarShowLabels");
                if (regObj != null)
                    Bag.TaskbarShowLabels = (int)regObj == 1;
                else
                    Bag.TaskbarShowLabels = false;

                regObj = rKey.GetValue("MMTaskbarEnabled");
                if (regObj != null)
                    Bag.MMTaskbarEnabled = (int)regObj == 1;
                else
                    Bag.MMTaskbarEnabled = false;

                regObj = rKey.GetValue("TaskbarGlomLevel");
                if (regObj != null)
                    Bag.TaskbarGlomLevel = UnboxCappedInt(regObj, 2, 0);
                else
                    Bag.TaskbarGlomLevel = 0;

                regObj = rKey.GetValue("MMTaskbarGlomLevel");
                if (regObj != null)
                    Bag.MMTaskbarGlomLevel = UnboxCappedInt(regObj, 2, 0);
                else
                    Bag.MMTaskbarGlomLevel = 0;
            }
        }

        internal static int UnboxCappedInt(object boxedInt, int cap, int fallback)
        {
            var unboxedU = (uint)(int)boxedInt;
            if (unboxedU > cap)
                return fallback;
            else
                return (int)unboxedU;
        }

        internal static void Update(SettingBag newSettings)
        {
            using (var rKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true))
            {
                if (newSettings.TaskbarShowLabels != Bag.TaskbarShowLabels)
                    rKey.SetValue("TaskbarShowLabels", newSettings.TaskbarShowLabels ? 1 : 0, RegistryValueKind.DWord);
                if (newSettings.MMTaskbarEnabled != Bag.MMTaskbarEnabled)
                    rKey.SetValue("MMTaskbarEnabled", newSettings.MMTaskbarEnabled ? 1 : 0, RegistryValueKind.DWord);
                if (newSettings.TaskbarGlomLevel != Bag.TaskbarGlomLevel)
                    rKey.SetValue("TaskbarGlomLevel", newSettings.TaskbarGlomLevel, RegistryValueKind.DWord);
                if (newSettings.MMTaskbarGlomLevel != Bag.MMTaskbarGlomLevel)
                    rKey.SetValue("MMTaskbarGlomLevel", newSettings.MMTaskbarGlomLevel, RegistryValueKind.DWord);
            }
            Bag = newSettings;
        }
    }

    internal class SettingBag
    {
        internal bool TaskbarShowLabels;
        internal bool MMTaskbarEnabled;
        internal int TaskbarGlomLevel;
        internal int MMTaskbarGlomLevel;
    }
}
