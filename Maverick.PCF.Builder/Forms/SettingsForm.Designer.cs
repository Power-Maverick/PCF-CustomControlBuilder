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
            this.chkboxLoadPublisherDetails = new System.Windows.Forms.CheckBox();
            this.chkboxLoadNamespace = new System.Windows.Forms.CheckBox();
            this.txtControlNamespace = new System.Windows.Forms.TextBox();
            this.lblControlNamespace = new System.Windows.Forms.Label();
            this.txtPublisherPrefix = new System.Windows.Forms.TextBox();
            this.lblPublisherPrefix = new System.Windows.Forms.Label();
            this.txtPublisherName = new System.Windows.Forms.TextBox();
            this.lblPublisherName = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnWorkingFolderSelector = new System.Windows.Forms.Button();
            this.txtSetWorkingFolder = new System.Windows.Forms.TextBox();
            this.lblSetWorkingFolder = new System.Windows.Forms.Label();
            this.workingFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.selectVSDevFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.txtMsBuildPath = new System.Windows.Forms.TextBox();
            this.lblMsBuildPath = new System.Windows.Forms.Label();
            this.gboxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // gboxSettings
            // 
            this.gboxSettings.Controls.Add(this.txtMsBuildPath);
            this.gboxSettings.Controls.Add(this.lblMsBuildPath);
            this.gboxSettings.Controls.Add(this.chkboxLoadPublisherDetails);
            this.gboxSettings.Controls.Add(this.chkboxLoadNamespace);
            this.gboxSettings.Controls.Add(this.txtControlNamespace);
            this.gboxSettings.Controls.Add(this.lblControlNamespace);
            this.gboxSettings.Controls.Add(this.txtPublisherPrefix);
            this.gboxSettings.Controls.Add(this.lblPublisherPrefix);
            this.gboxSettings.Controls.Add(this.txtPublisherName);
            this.gboxSettings.Controls.Add(this.lblPublisherName);
            this.gboxSettings.Controls.Add(this.btnReset);
            this.gboxSettings.Controls.Add(this.btnCancel);
            this.gboxSettings.Controls.Add(this.btnSave);
            this.gboxSettings.Controls.Add(this.btnWorkingFolderSelector);
            this.gboxSettings.Controls.Add(this.txtSetWorkingFolder);
            this.gboxSettings.Controls.Add(this.lblSetWorkingFolder);
            this.gboxSettings.Location = new System.Drawing.Point(12, 12);
            this.gboxSettings.Name = "gboxSettings";
            this.gboxSettings.Size = new System.Drawing.Size(532, 304);
            this.gboxSettings.TabIndex = 0;
            this.gboxSettings.TabStop = false;
            this.gboxSettings.Text = "Settings";
            // 
            // chkboxLoadPublisherDetails
            // 
            this.chkboxLoadPublisherDetails.AutoSize = true;
            this.chkboxLoadPublisherDetails.Location = new System.Drawing.Point(172, 206);
            this.chkboxLoadPublisherDetails.Name = "chkboxLoadPublisherDetails";
            this.chkboxLoadPublisherDetails.Size = new System.Drawing.Size(231, 17);
            this.chkboxLoadPublisherDetails.TabIndex = 24;
            this.chkboxLoadPublisherDetails.Text = "Always Load Publisher Details from Settings";
            this.chkboxLoadPublisherDetails.UseVisualStyleBackColor = true;
            // 
            // chkboxLoadNamespace
            // 
            this.chkboxLoadNamespace.AutoSize = true;
            this.chkboxLoadNamespace.Location = new System.Drawing.Point(172, 132);
            this.chkboxLoadNamespace.Name = "chkboxLoadNamespace";
            this.chkboxLoadNamespace.Size = new System.Drawing.Size(210, 17);
            this.chkboxLoadNamespace.TabIndex = 23;
            this.chkboxLoadNamespace.Text = "Always Load Namespace from Settings";
            this.chkboxLoadNamespace.UseVisualStyleBackColor = true;
            // 
            // txtControlNamespace
            // 
            this.txtControlNamespace.Location = new System.Drawing.Point(172, 106);
            this.txtControlNamespace.Name = "txtControlNamespace";
            this.txtControlNamespace.Size = new System.Drawing.Size(345, 20);
            this.txtControlNamespace.TabIndex = 22;
            // 
            // lblControlNamespace
            // 
            this.lblControlNamespace.AutoSize = true;
            this.lblControlNamespace.Location = new System.Drawing.Point(12, 109);
            this.lblControlNamespace.Name = "lblControlNamespace";
            this.lblControlNamespace.Size = new System.Drawing.Size(64, 13);
            this.lblControlNamespace.TabIndex = 21;
            this.lblControlNamespace.Text = "Namespace";
            // 
            // txtPublisherPrefix
            // 
            this.txtPublisherPrefix.Location = new System.Drawing.Point(172, 180);
            this.txtPublisherPrefix.Name = "txtPublisherPrefix";
            this.txtPublisherPrefix.Size = new System.Drawing.Size(345, 20);
            this.txtPublisherPrefix.TabIndex = 20;
            // 
            // lblPublisherPrefix
            // 
            this.lblPublisherPrefix.AutoSize = true;
            this.lblPublisherPrefix.Location = new System.Drawing.Point(12, 183);
            this.lblPublisherPrefix.Name = "lblPublisherPrefix";
            this.lblPublisherPrefix.Size = new System.Drawing.Size(79, 13);
            this.lblPublisherPrefix.TabIndex = 19;
            this.lblPublisherPrefix.Text = "Publisher Prefix";
            // 
            // txtPublisherName
            // 
            this.txtPublisherName.Location = new System.Drawing.Point(172, 154);
            this.txtPublisherName.Name = "txtPublisherName";
            this.txtPublisherName.Size = new System.Drawing.Size(345, 20);
            this.txtPublisherName.TabIndex = 18;
            // 
            // lblPublisherName
            // 
            this.lblPublisherName.AutoSize = true;
            this.lblPublisherName.Location = new System.Drawing.Point(12, 157);
            this.lblPublisherName.Name = "lblPublisherName";
            this.lblPublisherName.Size = new System.Drawing.Size(81, 13);
            this.lblPublisherName.TabIndex = 17;
            this.lblPublisherName.Text = "Publisher Name";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(15, 265);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(109, 23);
            this.btnReset.TabIndex = 16;
            this.btnReset.Text = "Reset and Close";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(442, 265);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(361, 265);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
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
            this.lblSetWorkingFolder.Size = new System.Drawing.Size(136, 13);
            this.lblSetWorkingFolder.TabIndex = 0;
            this.lblSetWorkingFolder.Text = "Working Directory Full Path";
            // 
            // selectVSDevFileDialog
            // 
            this.selectVSDevFileDialog.FileName = "VsDevCmd.bat";
            this.selectVSDevFileDialog.Filter = "Batch files (*.bat)|*.bat";
            this.selectVSDevFileDialog.InitialDirectory = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Enterprise\\Common7\\Tools";
            // 
            // txtMsBuildPath
            // 
            this.txtMsBuildPath.Location = new System.Drawing.Point(172, 58);
            this.txtMsBuildPath.Name = "txtMsBuildPath";
            this.txtMsBuildPath.Size = new System.Drawing.Size(345, 20);
            this.txtMsBuildPath.TabIndex = 26;
            // 
            // lblMsBuildPath
            // 
            this.lblMsBuildPath.AutoSize = true;
            this.lblMsBuildPath.Location = new System.Drawing.Point(12, 61);
            this.lblMsBuildPath.Name = "lblMsBuildPath";
            this.lblMsBuildPath.Size = new System.Drawing.Size(74, 13);
            this.lblMsBuildPath.TabIndex = 25;
            this.lblMsBuildPath.Text = "MS Build Path";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 328);
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
        private System.Windows.Forms.TextBox txtSetWorkingFolder;
        private System.Windows.Forms.Label lblSetWorkingFolder;
        private System.Windows.Forms.Button btnWorkingFolderSelector;
        private System.Windows.Forms.FolderBrowserDialog workingFolderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog selectVSDevFileDialog;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtPublisherPrefix;
        private System.Windows.Forms.Label lblPublisherPrefix;
        private System.Windows.Forms.TextBox txtPublisherName;
        private System.Windows.Forms.Label lblPublisherName;
        private System.Windows.Forms.TextBox txtControlNamespace;
        private System.Windows.Forms.Label lblControlNamespace;
        private System.Windows.Forms.CheckBox chkboxLoadPublisherDetails;
        private System.Windows.Forms.CheckBox chkboxLoadNamespace;
        private System.Windows.Forms.TextBox txtMsBuildPath;
        private System.Windows.Forms.Label lblMsBuildPath;
    }
}