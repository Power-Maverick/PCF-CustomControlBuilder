namespace Maverick.PCF.Builder.Forms
{
    partial class SettingsForm
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
            this.gboxSettings = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnVSPromptLoc = new System.Windows.Forms.Button();
            this.btnWorkingFolderSelector = new System.Windows.Forms.Button();
            this.txtSetVSCmdLoc = new System.Windows.Forms.TextBox();
            this.lblSetVSCmdLoc = new System.Windows.Forms.Label();
            this.txtSetWorkingFolder = new System.Windows.Forms.TextBox();
            this.lblSetWorkingFolder = new System.Windows.Forms.Label();
            this.workingFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.selectVSDevFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnReset = new System.Windows.Forms.Button();
            this.gboxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // gboxSettings
            // 
            this.gboxSettings.Controls.Add(this.btnReset);
            this.gboxSettings.Controls.Add(this.btnCancel);
            this.gboxSettings.Controls.Add(this.btnSave);
            this.gboxSettings.Controls.Add(this.btnVSPromptLoc);
            this.gboxSettings.Controls.Add(this.btnWorkingFolderSelector);
            this.gboxSettings.Controls.Add(this.txtSetVSCmdLoc);
            this.gboxSettings.Controls.Add(this.lblSetVSCmdLoc);
            this.gboxSettings.Controls.Add(this.txtSetWorkingFolder);
            this.gboxSettings.Controls.Add(this.lblSetWorkingFolder);
            this.gboxSettings.Location = new System.Drawing.Point(12, 12);
            this.gboxSettings.Name = "gboxSettings";
            this.gboxSettings.Size = new System.Drawing.Size(532, 132);
            this.gboxSettings.TabIndex = 0;
            this.gboxSettings.TabStop = false;
            this.gboxSettings.Text = "Settings";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(442, 99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(361, 99);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnVSPromptLoc
            // 
            this.btnVSPromptLoc.Location = new System.Drawing.Point(490, 58);
            this.btnVSPromptLoc.Name = "btnVSPromptLoc";
            this.btnVSPromptLoc.Size = new System.Drawing.Size(27, 20);
            this.btnVSPromptLoc.TabIndex = 13;
            this.btnVSPromptLoc.Text = "...";
            this.btnVSPromptLoc.UseVisualStyleBackColor = true;
            this.btnVSPromptLoc.Click += new System.EventHandler(this.BtnVSPromptLoc_Click);
            // 
            // btnWorkingFolderSelector
            // 
            this.btnWorkingFolderSelector.Location = new System.Drawing.Point(490, 32);
            this.btnWorkingFolderSelector.Name = "btnWorkingFolderSelector";
            this.btnWorkingFolderSelector.Size = new System.Drawing.Size(27, 20);
            this.btnWorkingFolderSelector.TabIndex = 12;
            this.btnWorkingFolderSelector.Text = "...";
            this.btnWorkingFolderSelector.UseVisualStyleBackColor = true;
            this.btnWorkingFolderSelector.Click += new System.EventHandler(this.BtnWorkingFolderSelector_Click);
            // 
            // txtSetVSCmdLoc
            // 
            this.txtSetVSCmdLoc.Location = new System.Drawing.Point(172, 58);
            this.txtSetVSCmdLoc.Name = "txtSetVSCmdLoc";
            this.txtSetVSCmdLoc.Size = new System.Drawing.Size(312, 20);
            this.txtSetVSCmdLoc.TabIndex = 3;
            // 
            // lblSetVSCmdLoc
            // 
            this.lblSetVSCmdLoc.AutoSize = true;
            this.lblSetVSCmdLoc.Location = new System.Drawing.Point(12, 61);
            this.lblSetVSCmdLoc.Name = "lblSetVSCmdLoc";
            this.lblSetVSCmdLoc.Size = new System.Drawing.Size(154, 13);
            this.lblSetVSCmdLoc.TabIndex = 2;
            this.lblSetVSCmdLoc.Text = "VS Command Prompt Location:";
            // 
            // txtSetWorkingFolder
            // 
            this.txtSetWorkingFolder.Location = new System.Drawing.Point(172, 32);
            this.txtSetWorkingFolder.Name = "txtSetWorkingFolder";
            this.txtSetWorkingFolder.Size = new System.Drawing.Size(312, 20);
            this.txtSetWorkingFolder.TabIndex = 1;
            // 
            // lblSetWorkingFolder
            // 
            this.lblSetWorkingFolder.AutoSize = true;
            this.lblSetWorkingFolder.Location = new System.Drawing.Point(12, 35);
            this.lblSetWorkingFolder.Name = "lblSetWorkingFolder";
            this.lblSetWorkingFolder.Size = new System.Drawing.Size(139, 13);
            this.lblSetWorkingFolder.TabIndex = 0;
            this.lblSetWorkingFolder.Text = "Working Directory Full Path:";
            // 
            // selectVSDevFileDialog
            // 
            this.selectVSDevFileDialog.FileName = "VsDevCmd.bat";
            this.selectVSDevFileDialog.Filter = "Batch files (*.bat)|*.bat";
            this.selectVSDevFileDialog.InitialDirectory = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Enterprise\\Common7\\Tools";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(15, 99);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(109, 23);
            this.btnReset.TabIndex = 16;
            this.btnReset.Text = "Reset and Close";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 156);
            this.Controls.Add(this.gboxSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.gboxSettings.ResumeLayout(false);
            this.gboxSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gboxSettings;
        private System.Windows.Forms.TextBox txtSetVSCmdLoc;
        private System.Windows.Forms.Label lblSetVSCmdLoc;
        private System.Windows.Forms.TextBox txtSetWorkingFolder;
        private System.Windows.Forms.Label lblSetWorkingFolder;
        private System.Windows.Forms.Button btnVSPromptLoc;
        private System.Windows.Forms.Button btnWorkingFolderSelector;
        private System.Windows.Forms.FolderBrowserDialog workingFolderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog selectVSDevFileDialog;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
    }
}