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
            this.label3 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.gbMultiMon.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGroupingMode
            // 
            this.lblGroupingMode.AutoSize = true;
            this.lblGroupingMode.Location = new System.Drawing.Point(8, 15);
            this.lblGroupingMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGroupingMode.Name = "lblGroupingMode";
            this.lblGroupingMode.Size = new System.Drawing.Size(107, 16);
            this.lblGroupingMode.TabIndex = 0;
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
            this.cbxGroupingMode.Location = new System.Drawing.Point(124, 11);
            this.cbxGroupingMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxGroupingMode.Name = "cbxGroupingMode";
            this.cbxGroupingMode.Size = new System.Drawing.Size(298, 24);
            this.cbxGroupingMode.TabIndex = 1;
            // 
            // cbShowLabels
            // 
            this.cbShowLabels.AutoSize = true;
            this.cbShowLabels.Location = new System.Drawing.Point(12, 50);
            this.cbShowLabels.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbShowLabels.Name = "cbShowLabels";
            this.cbShowLabels.Size = new System.Drawing.Size(196, 20);
            this.cbShowLabels.TabIndex = 2;
            this.cbShowLabels.Text = "Show labels on taskbar &pins";
            this.cbShowLabels.UseVisualStyleBackColor = true;
            // 
            // gbMultiMon
            // 
            this.gbMultiMon.Controls.Add(this.cbMultiMon);
            this.gbMultiMon.Controls.Add(this.cbxSecondaryMonitorsGroupingMode);
            this.gbMultiMon.Controls.Add(this.label3);
            this.gbMultiMon.ForeColor = System.Drawing.Color.White;
            this.gbMultiMon.Location = new System.Drawing.Point(12, 89);
            this.gbMultiMon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbMultiMon.Name = "gbMultiMon";
            this.gbMultiMon.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbMultiMon.Size = new System.Drawing.Size(421, 122);
            this.gbMultiMon.TabIndex = 4;
            this.gbMultiMon.TabStop = false;
            this.gbMultiMon.Text = "Multiple displays";
            // 
            // cbMultiMon
            // 
            this.cbMultiMon.AutoSize = true;
            this.cbMultiMon.Location = new System.Drawing.Point(11, 26);
            this.cbMultiMon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbMultiMon.Name = "cbMultiMon";
            this.cbMultiMon.Size = new System.Drawing.Size(199, 20);
            this.cbMultiMon.TabIndex = 3;
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
            this.cbxSecondaryMonitorsGroupingMode.Location = new System.Drawing.Point(11, 80);
            this.cbxSecondaryMonitorsGroupingMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxSecondaryMonitorsGroupingMode.Name = "cbxSecondaryMonitorsGroupingMode";
            this.cbxSecondaryMonitorsGroupingMode.Size = new System.Drawing.Size(399, 24);
            this.cbxSecondaryMonitorsGroupingMode.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Buttons on &other taskbars:";
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnApply.Location = new System.Drawing.Point(340, 224);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(94, 29);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(445, 268);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.gbMultiMon);
            this.Controls.Add(this.cbShowLabels);
            this.Controls.Add(this.cbxGroupingMode);
            this.Controls.Add(this.lblGroupingMode);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckBox cbMultiMon;
    }
}

