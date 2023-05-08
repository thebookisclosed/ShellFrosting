using FrostingCfg.NativeMethods;
using System;
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
            cbShowLabels.HandleCreated += OnHandleCreated;
            cbMultiMon.HandleCreated += OnHandleCreated;
            cbxGroupingMode.HandleCreated += OnHandleCreatedCFD;
            cbxSecondaryMonitorsGroupingMode.HandleCreated += OnHandleCreatedCFD;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RegistrySettings.Refresh();
            var sb = RegistrySettings.Bag;
            cbShowLabels.Checked = sb.TaskbarShowLabels;
            cbMultiMon.Checked = sb.MMTaskbarEnabled;
            cbxGroupingMode.SelectedIndex = sb.TaskbarGlomLevel;
            cbxSecondaryMonitorsGroupingMode.SelectedIndex = sb.MMTaskbarGlomLevel;
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
