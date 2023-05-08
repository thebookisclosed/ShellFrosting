namespace FrostingCfg
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblGroupingMode = new System.Windows.Forms.Label();
            this.cbxGroupingMode = new System.Windows.Forms.ComboBox();
            this.cbShowLabels = new System.Windows.Forms.CheckBox();
            this.gbMultiMon = new System.Windows.Forms.GroupBox();
            this.cbMultiMon = new System.Windows.Forms.CheckBox();
            this.cbxSecondaryMonitorsGroupingMode = new System.Windows.Forms.ComboBox();
            this.lblMMGroupingMode = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblExtension = new System.Windows.Forms.Label();
            this.lblExStatus = new System.Windows.Forms.Label();
            this.btnInstall = new System.Windows.Forms.Button();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.lleSupport = new FrostingCfg.LinkLabelEx();
            this.gbMultiMon.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGroupingMode
            // 
            this.lblGroupingMode.AutoSize = true;
            this.lblGroupingMode.Location = new System.Drawing.Point(6, 53);
            this.lblGroupingMode.Name = "lblGroupingMode";
            this.lblGroupingMode.Size = new System.Drawing.Size(87, 13);
            this.lblGroupingMode.TabIndex = 4;
            this.lblGroupingMode.Text = "Taskbar &buttons:";
            // 
            // cbxGroupingMode
            // 
            this.cbxGroupingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGroupingMode.FormattingEnabled = true;
            this.cbxGroupingMode.Items.AddRange(new object[] {
            "Always combine, hide labels",
            "Combine when taskbar is full",
            "Never combine"});
            this.cbxGroupingMode.Location = new System.Drawing.Point(99, 50);
            this.cbxGroupingMode.Name = "cbxGroupingMode";
            this.cbxGroupingMode.Size = new System.Drawing.Size(239, 21);
            this.cbxGroupingMode.TabIndex = 5;
            // 
            // cbShowLabels
            // 
            this.cbShowLabels.AutoSize = true;
            this.cbShowLabels.Location = new System.Drawing.Point(9, 81);
            this.cbShowLabels.Name = "cbShowLabels";
            this.cbShowLabels.Size = new System.Drawing.Size(158, 17);
            this.cbShowLabels.TabIndex = 6;
            this.cbShowLabels.Text = "Show labels on taskbar &pins";
            this.cbShowLabels.UseVisualStyleBackColor = true;
            // 
            // gbMultiMon
            // 
            this.gbMultiMon.Controls.Add(this.cbMultiMon);
            this.gbMultiMon.Controls.Add(this.cbxSecondaryMonitorsGroupingMode);
            this.gbMultiMon.Controls.Add(this.lblMMGroupingMode);
            this.gbMultiMon.ForeColor = System.Drawing.Color.White;
            this.gbMultiMon.Location = new System.Drawing.Point(9, 112);
            this.gbMultiMon.Name = "gbMultiMon";
            this.gbMultiMon.Size = new System.Drawing.Size(337, 98);
            this.gbMultiMon.TabIndex = 7;
            this.gbMultiMon.TabStop = false;
            this.gbMultiMon.Text = "Multiple displays";
            // 
            // cbMultiMon
            // 
            this.cbMultiMon.AutoSize = true;
            this.cbMultiMon.Location = new System.Drawing.Point(9, 21);
            this.cbMultiMon.Name = "cbMultiMon";
            this.cbMultiMon.Size = new System.Drawing.Size(159, 17);
            this.cbMultiMon.TabIndex = 0;
            this.cbMultiMon.Text = "&Show taskbar on all displays";
            this.cbMultiMon.UseVisualStyleBackColor = true;
            // 
            // cbxSecondaryMonitorsGroupingMode
            // 
            this.cbxSecondaryMonitorsGroupingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSecondaryMonitorsGroupingMode.FormattingEnabled = true;
            this.cbxSecondaryMonitorsGroupingMode.Items.AddRange(new object[] {
            "Always combine, hide labels",
            "Combine when taskbar is full",
            "Never combine"});
            this.cbxSecondaryMonitorsGroupingMode.Location = new System.Drawing.Point(9, 64);
            this.cbxSecondaryMonitorsGroupingMode.Name = "cbxSecondaryMonitorsGroupingMode";
            this.cbxSecondaryMonitorsGroupingMode.Size = new System.Drawing.Size(320, 21);
            this.cbxSecondaryMonitorsGroupingMode.TabIndex = 2;
            // 
            // lblMMGroupingMode
            // 
            this.lblMMGroupingMode.AutoSize = true;
            this.lblMMGroupingMode.Location = new System.Drawing.Point(6, 44);
            this.lblMMGroupingMode.Name = "lblMMGroupingMode";
            this.lblMMGroupingMode.Size = new System.Drawing.Size(131, 13);
            this.lblMMGroupingMode.TabIndex = 1;
            this.lblMMGroupingMode.Text = "Buttons on &other taskbars:";
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnApply.Location = new System.Drawing.Point(271, 220);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Location = new System.Drawing.Point(6, 12);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(82, 13);
            this.lblExtension.TabIndex = 0;
            this.lblExtension.Text = "Extension state:";
            // 
            // lblExStatus
            // 
            this.lblExStatus.AutoSize = true;
            this.lblExStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(0)))));
            this.lblExStatus.Location = new System.Drawing.Point(96, 12);
            this.lblExStatus.Name = "lblExStatus";
            this.lblExStatus.Size = new System.Drawing.Size(62, 13);
            this.lblExStatus.TabIndex = 1;
            this.lblExStatus.Text = "Not present";
            // 
            // btnInstall
            // 
            this.btnInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInstall.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnInstall.Location = new System.Drawing.Point(271, 7);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 2;
            this.btnInstall.Text = "&Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // pnlDivider
            // 
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.pnlDivider.Location = new System.Drawing.Point(9, 39);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(337, 1);
            this.pnlDivider.TabIndex = 3;
            // 
            // lleSupport
            // 
            this.lleSupport.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(194)))), ((int)(((byte)(255)))));
            this.lleSupport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lleSupport.AutoSize = true;
            this.lleSupport.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.lleSupport.Location = new System.Drawing.Point(6, 225);
            this.lleSupport.Name = "lleSupport";
            this.lleSupport.Size = new System.Drawing.Size(202, 13);
            this.lleSupport.TabIndex = 9;
            this.lleSupport.TabStop = true;
            this.lleSupport.Text = "Like the project? You can support it here.";
            this.lleSupport.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.lleSupport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lleSupport_LinkClicked);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(356, 253);
            this.Controls.Add(this.lleSupport);
            this.Controls.Add(this.gbMultiMon);
            this.Controls.Add(this.pnlDivider);
            this.Controls.Add(this.lblGroupingMode);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.cbxGroupingMode);
            this.Controls.Add(this.lblExStatus);
            this.Controls.Add(this.cbShowLabels);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblExtension);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Frosting Configuration";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbMultiMon.ResumeLayout(false);
            this.gbMultiMon.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGroupingMode;
        private System.Windows.Forms.ComboBox cbxGroupingMode;
        private System.Windows.Forms.CheckBox cbShowLabels;
        private System.Windows.Forms.GroupBox gbMultiMon;
        private System.Windows.Forms.ComboBox cbxSecondaryMonitorsGroupingMode;
        private System.Windows.Forms.Label lblMMGroupingMode;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckBox cbMultiMon;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.Label lblExStatus;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Panel pnlDivider;
        private LinkLabelEx lleSupport;
    }
}

