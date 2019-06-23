namespace Maverick.PCF.Builder
{
    partial class MyPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyPluginControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNewPCF = new System.Windows.Forms.ToolStripButton();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabNewControl = new System.Windows.Forms.TabPage();
            this.lblDeploymentError = new System.Windows.Forms.Label();
            this.lblRunPacError = new System.Windows.Forms.Label();
            this.linkLblBlog = new System.Windows.Forms.LinkLabel();
            this.txtPublisherPrefix = new System.Windows.Forms.TextBox();
            this.lblPublisherPrefix = new System.Windows.Forms.Label();
            this.txtPublisherName = new System.Windows.Forms.TextBox();
            this.lblPublisherName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeploymentFolder = new System.Windows.Forms.TextBox();
            this.lblDeploymentFolder = new System.Windows.Forms.Label();
            this.btnCreatePackage = new System.Windows.Forms.Button();
            this.lblCreatePackage = new System.Windows.Forms.Label();
            this.btnTestProject = new System.Windows.Forms.Button();
            this.lblTestProject = new System.Windows.Forms.Label();
            this.btnBuild = new System.Windows.Forms.Button();
            this.lblBuild = new System.Windows.Forms.Label();
            this.lblDevelopComments = new System.Windows.Forms.Label();
            this.btnOpenProject = new System.Windows.Forms.Button();
            this.lblOpenProject = new System.Windows.Forms.Label();
            this.cmbTemplate = new System.Windows.Forms.ComboBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.txtControlName = new System.Windows.Forms.TextBox();
            this.lblControlName = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.lblNewPcfRunPacCmd = new System.Windows.Forms.Label();
            this.btnNewPcfRunPacCmd = new System.Windows.Forms.Button();
            this.lblWorkingDir = new System.Windows.Forms.Label();
            this.workingFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtWorkingFolder = new System.Windows.Forms.TextBox();
            this.btnWorkingFolderSelector = new System.Windows.Forms.Button();
            this.lblVSPromptLoc = new System.Windows.Forms.Label();
            this.txtVSPromptLoc = new System.Windows.Forms.TextBox();
            this.btnVSPromptLoc = new System.Windows.Forms.Button();
            this.lblErrors = new System.Windows.Forms.Label();
            this.lblDeploy = new System.Windows.Forms.Label();
            this.btnDeploy = new System.Windows.Forms.Button();
            this.toolStripMenu.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabNewControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.tsbNewPCF});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(1127, 31);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(28, 28);
            this.tsbClose.ToolTipText = "Close tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbNewPCF
            // 
            this.tsbNewPCF.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewPCF.Image")));
            this.tsbNewPCF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewPCF.Name = "tsbNewPCF";
            this.tsbNewPCF.Size = new System.Drawing.Size(126, 28);
            this.tsbNewPCF.Text = "New PCF Control";
            this.tsbNewPCF.ToolTipText = "Create New PCF Control";
            this.tsbNewPCF.Click += new System.EventHandler(this.tsbNewPCF_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabNewControl);
            this.tabControl.Location = new System.Drawing.Point(0, 91);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1127, 745);
            this.tabControl.TabIndex = 5;
            // 
            // tabNewControl
            // 
            this.tabNewControl.Controls.Add(this.btnDeploy);
            this.tabNewControl.Controls.Add(this.lblDeploy);
            this.tabNewControl.Controls.Add(this.lblDeploymentError);
            this.tabNewControl.Controls.Add(this.lblRunPacError);
            this.tabNewControl.Controls.Add(this.linkLblBlog);
            this.tabNewControl.Controls.Add(this.txtPublisherPrefix);
            this.tabNewControl.Controls.Add(this.lblPublisherPrefix);
            this.tabNewControl.Controls.Add(this.txtPublisherName);
            this.tabNewControl.Controls.Add(this.lblPublisherName);
            this.tabNewControl.Controls.Add(this.label1);
            this.tabNewControl.Controls.Add(this.txtDeploymentFolder);
            this.tabNewControl.Controls.Add(this.lblDeploymentFolder);
            this.tabNewControl.Controls.Add(this.btnCreatePackage);
            this.tabNewControl.Controls.Add(this.lblCreatePackage);
            this.tabNewControl.Controls.Add(this.btnTestProject);
            this.tabNewControl.Controls.Add(this.lblTestProject);
            this.tabNewControl.Controls.Add(this.btnBuild);
            this.tabNewControl.Controls.Add(this.lblBuild);
            this.tabNewControl.Controls.Add(this.lblDevelopComments);
            this.tabNewControl.Controls.Add(this.btnOpenProject);
            this.tabNewControl.Controls.Add(this.lblOpenProject);
            this.tabNewControl.Controls.Add(this.cmbTemplate);
            this.tabNewControl.Controls.Add(this.lblTemplate);
            this.tabNewControl.Controls.Add(this.txtControlName);
            this.tabNewControl.Controls.Add(this.lblControlName);
            this.tabNewControl.Controls.Add(this.txtNamespace);
            this.tabNewControl.Controls.Add(this.lblNamespace);
            this.tabNewControl.Controls.Add(this.lblNewPcfRunPacCmd);
            this.tabNewControl.Controls.Add(this.btnNewPcfRunPacCmd);
            this.tabNewControl.Location = new System.Drawing.Point(4, 22);
            this.tabNewControl.Name = "tabNewControl";
            this.tabNewControl.Padding = new System.Windows.Forms.Padding(3);
            this.tabNewControl.Size = new System.Drawing.Size(1119, 719);
            this.tabNewControl.TabIndex = 0;
            this.tabNewControl.Text = "New PCF Control";
            this.tabNewControl.UseVisualStyleBackColor = true;
            // 
            // lblDeploymentError
            // 
            this.lblDeploymentError.AutoSize = true;
            this.lblDeploymentError.ForeColor = System.Drawing.Color.Maroon;
            this.lblDeploymentError.Location = new System.Drawing.Point(214, 550);
            this.lblDeploymentError.Name = "lblDeploymentError";
            this.lblDeploymentError.Size = new System.Drawing.Size(0, 13);
            this.lblDeploymentError.TabIndex = 27;
            // 
            // lblRunPacError
            // 
            this.lblRunPacError.AutoSize = true;
            this.lblRunPacError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblRunPacError.Location = new System.Drawing.Point(203, 132);
            this.lblRunPacError.Name = "lblRunPacError";
            this.lblRunPacError.Size = new System.Drawing.Size(0, 13);
            this.lblRunPacError.TabIndex = 26;
            // 
            // linkLblBlog
            // 
            this.linkLblBlog.AutoSize = true;
            this.linkLblBlog.Location = new System.Drawing.Point(197, 218);
            this.linkLblBlog.Name = "linkLblBlog";
            this.linkLblBlog.Size = new System.Drawing.Size(52, 13);
            this.linkLblBlog.TabIndex = 25;
            this.linkLblBlog.TabStop = true;
            this.linkLblBlog.Text = "More Info";
            this.linkLblBlog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblBlog_LinkClicked);
            // 
            // txtPublisherPrefix
            // 
            this.txtPublisherPrefix.Location = new System.Drawing.Point(175, 509);
            this.txtPublisherPrefix.Name = "txtPublisherPrefix";
            this.txtPublisherPrefix.Size = new System.Drawing.Size(118, 20);
            this.txtPublisherPrefix.TabIndex = 24;
            // 
            // lblPublisherPrefix
            // 
            this.lblPublisherPrefix.AutoSize = true;
            this.lblPublisherPrefix.Location = new System.Drawing.Point(48, 512);
            this.lblPublisherPrefix.Name = "lblPublisherPrefix";
            this.lblPublisherPrefix.Size = new System.Drawing.Size(79, 13);
            this.lblPublisherPrefix.TabIndex = 23;
            this.lblPublisherPrefix.Text = "Publisher Prefix";
            // 
            // txtPublisherName
            // 
            this.txtPublisherName.Location = new System.Drawing.Point(175, 483);
            this.txtPublisherName.Name = "txtPublisherName";
            this.txtPublisherName.Size = new System.Drawing.Size(118, 20);
            this.txtPublisherName.TabIndex = 22;
            // 
            // lblPublisherName
            // 
            this.lblPublisherName.AutoSize = true;
            this.lblPublisherName.Location = new System.Drawing.Point(48, 486);
            this.lblPublisherName.Name = "lblPublisherName";
            this.lblPublisherName.Size = new System.Drawing.Size(81, 13);
            this.lblPublisherName.TabIndex = 21;
            this.lblPublisherName.Text = "Publisher Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(300, 460);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "*This will create the folder for you. Do not create deployment folder";
            // 
            // txtDeploymentFolder
            // 
            this.txtDeploymentFolder.Location = new System.Drawing.Point(175, 457);
            this.txtDeploymentFolder.Name = "txtDeploymentFolder";
            this.txtDeploymentFolder.Size = new System.Drawing.Size(118, 20);
            this.txtDeploymentFolder.TabIndex = 19;
            // 
            // lblDeploymentFolder
            // 
            this.lblDeploymentFolder.AutoSize = true;
            this.lblDeploymentFolder.Location = new System.Drawing.Point(48, 460);
            this.lblDeploymentFolder.Name = "lblDeploymentFolder";
            this.lblDeploymentFolder.Size = new System.Drawing.Size(121, 13);
            this.lblDeploymentFolder.TabIndex = 18;
            this.lblDeploymentFolder.Text = "Deployment folder name";
            // 
            // btnCreatePackage
            // 
            this.btnCreatePackage.Location = new System.Drawing.Point(51, 545);
            this.btnCreatePackage.Name = "btnCreatePackage";
            this.btnCreatePackage.Size = new System.Drawing.Size(143, 23);
            this.btnCreatePackage.TabIndex = 17;
            this.btnCreatePackage.Text = "Create Solution Package";
            this.btnCreatePackage.UseVisualStyleBackColor = true;
            this.btnCreatePackage.Click += new System.EventHandler(this.btnCreatePackage_Click);
            // 
            // lblCreatePackage
            // 
            this.lblCreatePackage.AutoSize = true;
            this.lblCreatePackage.Location = new System.Drawing.Point(21, 437);
            this.lblCreatePackage.Name = "lblCreatePackage";
            this.lblCreatePackage.Size = new System.Drawing.Size(180, 13);
            this.lblCreatePackage.TabIndex = 16;
            this.lblCreatePackage.Text = "5. Create D365 CE solution package";
            // 
            // btnTestProject
            // 
            this.btnTestProject.Location = new System.Drawing.Point(51, 388);
            this.btnTestProject.Name = "btnTestProject";
            this.btnTestProject.Size = new System.Drawing.Size(143, 23);
            this.btnTestProject.TabIndex = 15;
            this.btnTestProject.Text = "Test project";
            this.btnTestProject.UseVisualStyleBackColor = true;
            this.btnTestProject.Click += new System.EventHandler(this.btnTestProject_Click);
            // 
            // lblTestProject
            // 
            this.lblTestProject.AutoSize = true;
            this.lblTestProject.Location = new System.Drawing.Point(18, 354);
            this.lblTestProject.Name = "lblTestProject";
            this.lblTestProject.Size = new System.Drawing.Size(181, 13);
            this.lblTestProject.TabIndex = 14;
            this.lblTestProject.Text = "4. Test your custom control (optional)";
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(51, 308);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(143, 23);
            this.btnBuild.TabIndex = 13;
            this.btnBuild.Text = "Build project";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // lblBuild
            // 
            this.lblBuild.AutoSize = true;
            this.lblBuild.Location = new System.Drawing.Point(15, 270);
            this.lblBuild.Name = "lblBuild";
            this.lblBuild.Size = new System.Drawing.Size(77, 13);
            this.lblBuild.TabIndex = 12;
            this.lblBuild.Text = "3. Build project";
            // 
            // lblDevelopComments
            // 
            this.lblDevelopComments.AutoSize = true;
            this.lblDevelopComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevelopComments.Location = new System.Drawing.Point(300, 218);
            this.lblDevelopComments.Name = "lblDevelopComments";
            this.lblDevelopComments.Size = new System.Drawing.Size(0, 13);
            this.lblDevelopComments.TabIndex = 10;
            // 
            // btnOpenProject
            // 
            this.btnOpenProject.Location = new System.Drawing.Point(51, 213);
            this.btnOpenProject.Name = "btnOpenProject";
            this.btnOpenProject.Size = new System.Drawing.Size(143, 23);
            this.btnOpenProject.TabIndex = 9;
            this.btnOpenProject.Text = "Open Project Directory";
            this.btnOpenProject.UseVisualStyleBackColor = true;
            this.btnOpenProject.Click += new System.EventHandler(this.btnOpenProject_Click);
            // 
            // lblOpenProject
            // 
            this.lblOpenProject.AutoSize = true;
            this.lblOpenProject.Location = new System.Drawing.Point(15, 178);
            this.lblOpenProject.Name = "lblOpenProject";
            this.lblOpenProject.Size = new System.Drawing.Size(223, 13);
            this.lblOpenProject.TabIndex = 8;
            this.lblOpenProject.Text = "2. Open your project and develop your control";
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.FormattingEnabled = true;
            this.cmbTemplate.Items.AddRange(new object[] {
            "Field",
            "Dataset"});
            this.cmbTemplate.Location = new System.Drawing.Point(125, 86);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(175, 21);
            this.cmbTemplate.TabIndex = 7;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(48, 89);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(51, 13);
            this.lblTemplate.TabIndex = 6;
            this.lblTemplate.Text = "Template";
            // 
            // txtControlName
            // 
            this.txtControlName.Location = new System.Drawing.Point(125, 59);
            this.txtControlName.Name = "txtControlName";
            this.txtControlName.Size = new System.Drawing.Size(175, 20);
            this.txtControlName.TabIndex = 5;
            // 
            // lblControlName
            // 
            this.lblControlName.AutoSize = true;
            this.lblControlName.Location = new System.Drawing.Point(48, 62);
            this.lblControlName.Name = "lblControlName";
            this.lblControlName.Size = new System.Drawing.Size(71, 13);
            this.lblControlName.TabIndex = 4;
            this.lblControlName.Text = "Control Name";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(125, 33);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(175, 20);
            this.txtNamespace.TabIndex = 3;
            // 
            // lblNamespace
            // 
            this.lblNamespace.AutoSize = true;
            this.lblNamespace.Location = new System.Drawing.Point(48, 36);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(64, 13);
            this.lblNamespace.TabIndex = 2;
            this.lblNamespace.Text = "Namespace";
            // 
            // lblNewPcfRunPacCmd
            // 
            this.lblNewPcfRunPacCmd.AutoSize = true;
            this.lblNewPcfRunPacCmd.Location = new System.Drawing.Point(15, 14);
            this.lblNewPcfRunPacCmd.Name = "lblNewPcfRunPacCmd";
            this.lblNewPcfRunPacCmd.Size = new System.Drawing.Size(278, 13);
            this.lblNewPcfRunPacCmd.TabIndex = 1;
            this.lblNewPcfRunPacCmd.Text = "1. Run \"pac\" command to create new component project";
            // 
            // btnNewPcfRunPacCmd
            // 
            this.btnNewPcfRunPacCmd.Location = new System.Drawing.Point(51, 127);
            this.btnNewPcfRunPacCmd.Name = "btnNewPcfRunPacCmd";
            this.btnNewPcfRunPacCmd.Size = new System.Drawing.Size(143, 23);
            this.btnNewPcfRunPacCmd.TabIndex = 0;
            this.btnNewPcfRunPacCmd.Text = "Run \"pac\" command";
            this.btnNewPcfRunPacCmd.UseVisualStyleBackColor = true;
            this.btnNewPcfRunPacCmd.Click += new System.EventHandler(this.btnNewPcfRunPacCmd_Click);
            // 
            // lblWorkingDir
            // 
            this.lblWorkingDir.AutoSize = true;
            this.lblWorkingDir.Location = new System.Drawing.Point(1, 41);
            this.lblWorkingDir.Name = "lblWorkingDir";
            this.lblWorkingDir.Size = new System.Drawing.Size(143, 13);
            this.lblWorkingDir.TabIndex = 6;
            this.lblWorkingDir.Text = "Select your working directory";
            // 
            // txtWorkingFolder
            // 
            this.txtWorkingFolder.Location = new System.Drawing.Point(204, 38);
            this.txtWorkingFolder.Name = "txtWorkingFolder";
            this.txtWorkingFolder.Size = new System.Drawing.Size(305, 20);
            this.txtWorkingFolder.TabIndex = 7;
            // 
            // btnWorkingFolderSelector
            // 
            this.btnWorkingFolderSelector.Location = new System.Drawing.Point(515, 38);
            this.btnWorkingFolderSelector.Name = "btnWorkingFolderSelector";
            this.btnWorkingFolderSelector.Size = new System.Drawing.Size(27, 20);
            this.btnWorkingFolderSelector.TabIndex = 8;
            this.btnWorkingFolderSelector.Text = "...";
            this.btnWorkingFolderSelector.UseVisualStyleBackColor = true;
            this.btnWorkingFolderSelector.Click += new System.EventHandler(this.btnWorkingFolderSelector_Click);
            // 
            // lblVSPromptLoc
            // 
            this.lblVSPromptLoc.AutoSize = true;
            this.lblVSPromptLoc.Location = new System.Drawing.Point(3, 68);
            this.lblVSPromptLoc.Name = "lblVSPromptLoc";
            this.lblVSPromptLoc.Size = new System.Drawing.Size(195, 13);
            this.lblVSPromptLoc.TabIndex = 9;
            this.lblVSPromptLoc.Text = "VS developer command prompt location";
            // 
            // txtVSPromptLoc
            // 
            this.txtVSPromptLoc.Location = new System.Drawing.Point(204, 65);
            this.txtVSPromptLoc.Name = "txtVSPromptLoc";
            this.txtVSPromptLoc.Size = new System.Drawing.Size(305, 20);
            this.txtVSPromptLoc.TabIndex = 10;
            // 
            // btnVSPromptLoc
            // 
            this.btnVSPromptLoc.Location = new System.Drawing.Point(515, 65);
            this.btnVSPromptLoc.Name = "btnVSPromptLoc";
            this.btnVSPromptLoc.Size = new System.Drawing.Size(27, 20);
            this.btnVSPromptLoc.TabIndex = 11;
            this.btnVSPromptLoc.Text = "...";
            this.btnVSPromptLoc.UseVisualStyleBackColor = true;
            this.btnVSPromptLoc.Click += new System.EventHandler(this.btnVSPromptLoc_Click);
            // 
            // lblErrors
            // 
            this.lblErrors.AutoSize = true;
            this.lblErrors.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblErrors.Location = new System.Drawing.Point(593, 41);
            this.lblErrors.Name = "lblErrors";
            this.lblErrors.Size = new System.Drawing.Size(0, 13);
            this.lblErrors.TabIndex = 12;
            // 
            // lblDeploy
            // 
            this.lblDeploy.AutoSize = true;
            this.lblDeploy.Location = new System.Drawing.Point(21, 603);
            this.lblDeploy.Name = "lblDeploy";
            this.lblDeploy.Size = new System.Drawing.Size(184, 13);
            this.lblDeploy.TabIndex = 28;
            this.lblDeploy.Text = "6. Deploy Custom Control to D365 CE";
            // 
            // btnDeploy
            // 
            this.btnDeploy.Location = new System.Drawing.Point(51, 635);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(143, 23);
            this.btnDeploy.TabIndex = 29;
            this.btnDeploy.Text = "Deploy to D365 CE";
            this.btnDeploy.UseVisualStyleBackColor = true;
            this.btnDeploy.Click += new System.EventHandler(this.btnDeploy_Click);
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblErrors);
            this.Controls.Add(this.btnVSPromptLoc);
            this.Controls.Add(this.txtVSPromptLoc);
            this.Controls.Add(this.lblVSPromptLoc);
            this.Controls.Add(this.btnWorkingFolderSelector);
            this.Controls.Add(this.txtWorkingFolder);
            this.Controls.Add(this.lblWorkingDir);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(1127, 839);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabNewControl.ResumeLayout(false);
            this.tabNewControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabNewControl;
        private System.Windows.Forms.ToolStripButton tsbNewPCF;
        private System.Windows.Forms.Label lblWorkingDir;
        private System.Windows.Forms.FolderBrowserDialog workingFolderBrowserDialog;
        private System.Windows.Forms.TextBox txtWorkingFolder;
        private System.Windows.Forms.Button btnWorkingFolderSelector;
        private System.Windows.Forms.Label lblNewPcfRunPacCmd;
        private System.Windows.Forms.Button btnNewPcfRunPacCmd;
        private System.Windows.Forms.Label lblVSPromptLoc;
        private System.Windows.Forms.TextBox txtVSPromptLoc;
        private System.Windows.Forms.Button btnVSPromptLoc;
        private System.Windows.Forms.TextBox txtControlName;
        private System.Windows.Forms.Label lblControlName;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.ComboBox cmbTemplate;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.Button btnOpenProject;
        private System.Windows.Forms.Label lblOpenProject;
        private System.Windows.Forms.Label lblDevelopComments;
        private System.Windows.Forms.Label lblBuild;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Button btnTestProject;
        private System.Windows.Forms.Label lblTestProject;
        private System.Windows.Forms.Button btnCreatePackage;
        private System.Windows.Forms.Label lblCreatePackage;
        private System.Windows.Forms.Label lblDeploymentFolder;
        private System.Windows.Forms.TextBox txtDeploymentFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPublisherPrefix;
        private System.Windows.Forms.Label lblPublisherPrefix;
        private System.Windows.Forms.TextBox txtPublisherName;
        private System.Windows.Forms.Label lblPublisherName;
        private System.Windows.Forms.LinkLabel linkLblBlog;
        private System.Windows.Forms.Label lblErrors;
        private System.Windows.Forms.Label lblRunPacError;
        private System.Windows.Forms.Label lblDeploymentError;
        private System.Windows.Forms.Label lblDeploy;
        private System.Windows.Forms.Button btnDeploy;
    }
}
