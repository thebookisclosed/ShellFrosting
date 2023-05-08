using FrostingCfg.NativeMethods;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FrostingCfg
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            HandleCreated += MainForm_HandleCreated;
            InitializeComponent();
            btnApply.HandleCreated += OnHandleCreated;
            btnInstall.HandleCreated += OnHandleCreated;

            cbShowLabels.HandleCreated += OnHandleCreated;
            cbMultiMon.HandleCreated += OnHandleCreated;
            cbxGroupingMode.HandleCreated += OnHandleCreatedCFD;
            cbxSecondaryMonitorsGroupingMode.HandleCreated += OnHandleCreatedCFD;

            const int IDI_APPLICATION = 32512;
            var ourHandle = Kernel32.GetModuleHandleW(null);
            var hIcon = User32.LoadIconW(ourHandle, new IntPtr(IDI_APPLICATION));
            Icon = Icon.FromHandle(hIcon);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateExtensionStatus();
        }

        private bool UpdateExtensionStatus()
        {
            var isInstalled = ExtensionCfg.IsInstalled();
            if (!isInstalled)
            {
                btnApply.Enabled = false;
                lblExStatus.ForeColor = Color.FromArgb(255, 240, 0);
                lblExStatus.Text = "Not present";
                btnInstall.Text = "&Install";
                btnInstall.Tag = false;
            }
            else
            {
                btnApply.Enabled = true;
                lblExStatus.ForeColor = Color.FromArgb(69, 212, 0);
                lblExStatus.Text = "Installed";
                btnInstall.Text = "Un&install";
                btnInstall.Tag = true;
            }
            RefreshSettings();
            return isInstalled;
        }

        private void RefreshSettings()
        {
            RegistrySettings.Refresh();
            var sb = RegistrySettings.Bag;
            cbShowLabels.Checked = sb.TaskbarShowLabels;
            cbMultiMon.Checked = sb.MMTaskbarEnabled;
            cbxGroupingMode.SelectedIndex = sb.TaskbarGlomLevel;
            cbxSecondaryMonitorsGroupingMode.SelectedIndex = sb.MMTaskbarGlomLevel;
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            var ynResult = MessageBox.Show("Configuring the extension requires a restart of File Explorer.\nDo you wish to continue?",
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ynResult != DialogResult.Yes)
                return;

            var shouldUninstall = (bool)btnInstall.Tag;
            var regResult = shouldUninstall ? ExtensionCfg.Unregister() : ExtensionCfg.Register();
            
            if (regResult != 0)
            {
                var errorString = string.Format("Extension configuration failed\n\n{0}\n(Error {1})",
                    ExtensionCfg.GetErrorDescription(regResult),
                    regResult);
                MessageBox.Show(errorString, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ExtensionCfg.RestartExplorer();
            UpdateExtensionStatus();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var ns = new SettingBag()
            {
                TaskbarShowLabels = cbShowLabels.Checked,
                MMTaskbarEnabled = cbMultiMon.Checked,
                TaskbarGlomLevel = cbxGroupingMode.SelectedIndex,
                MMTaskbarGlomLevel = cbxSecondaryMonitorsGroupingMode.SelectedIndex
            };
            RegistrySettings.Update(ns);
        }

        private unsafe void MainForm_HandleCreated(object sender, EventArgs e)
        {
            UxTheme.SetPreferredAppMode(1);
            const int WCA_USEDARKMODECOLORS = 26;
            int enabled = 1;
            var attribs = new WINDOWCOMPOSITIONATTRIBDATA()
            {
                Attrib = WCA_USEDARKMODECOLORS,
                pvData = &enabled,
                cbData = sizeof(int)
            };
            User32.SetWindowCompositionAttribute(Handle, &attribs);
            OnHandleCreated(sender, e);
        }

        private void OnHandleCreated(object sender, EventArgs e)
        {
            MakeDark((Control)sender, "DarkMode_Explorer");
        }

        private void OnHandleCreatedCFD(object sender, EventArgs e)
        {
            MakeDark((Control)sender, "DarkMode_CFD");
        }

        private unsafe void MakeDark(Control sender, string pszSubAppName)
        {
            const int WM_THEMECHANGED = 0x31A;
            var handle = sender.Handle;

            UxTheme.AllowDarkModeForWindow(handle, true);
            UxTheme.SetWindowTheme(handle, pszSubAppName, null);
            User32.SendMessageW(handle, WM_THEMECHANGED, new IntPtr(0xFFFFFFFC), new IntPtr(3));
        }
    }
}
