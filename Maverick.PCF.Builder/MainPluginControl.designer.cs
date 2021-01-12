namespace Maverick.PCF.Builder
{
    partial class MainPluginControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPluginControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNewPCFMenu = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmNewPCFBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNewPCFTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbEditControl = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAuthProfile = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmCreateProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmListProfiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tspGallery = new System.Windows.Forms.ToolStripButton();
            this.tspSampleControls = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspCLIHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.tspmDownloadPowerAppsCLI = new System.Windows.Forms.ToolStripMenuItem();
            this.tspmUpdatePowerAppsCLI = new System.Windows.Forms.ToolStripMenuItem();
            this.tspHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.tspmPCFOverview = new System.Windows.Forms.ToolStripMenuItem();
            this.tspmMSDocs = new System.Windows.Forms.ToolStripMenuItem();
            this.tspmPCFLimitations = new System.Windows.Forms.ToolStripMenuItem();
            this.tspMoreLinks = new System.Windows.Forms.ToolStripDropDownButton();
            this.tspmDemos = new System.Windows.Forms.ToolStripMenuItem();
            this.tspmBlogs = new System.Windows.Forms.ToolStripMenuItem();
            this.tspmForums = new System.Windows.Forms.ToolStripMenuItem();
            this.tspmIdeas = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tspSettings = new System.Windows.Forms.ToolStripButton();
            this.workingFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.gboxQuickAction = new System.Windows.Forms.GroupBox();
            this.btnBuildAndDeployAll = new System.Windows.Forms.Button();
            this.btnBuildAllProjects = new System.Windows.Forms.Button();
            this.btnBuildAndTest = new System.Windows.Forms.Button();
            this.lblPCFCLIVersionMsg = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.picRunning = new System.Windows.Forms.PictureBox();
            this.grpBoxAuthProfileDetails = new System.Windows.Forms.GroupBox();
            this.lblCurrentProfile = new System.Windows.Forms.Label();
            this.dgvMRULocations = new System.Windows.Forms.DataGridView();
            this.FolderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnShowMRULocations = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.linklblPowerBiReport = new System.Windows.Forms.LinkLabel();
            this.lblnpmVersionMsg = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClearConsole = new System.Windows.Forms.Button();
            this.btnRefreshDetails = new System.Windows.Forms.Button();
            this.lblErrors = new System.Windows.Forms.Label();
            this.gboxCommandPrompt = new System.Windows.Forms.GroupBox();
            this.consoleControl = new ConsoleControl.ConsoleControl();
            this.grpBoxSolutionDetails = new System.Windows.Forms.GroupBox();
            this.cboxSolutions = new System.Windows.Forms.ComboBox();
            this.chkUseExistingSolution = new System.Windows.Forms.CheckBox();
            this.lblSolutionInitStatus = new System.Windows.Forms.Label();
            this.chkManagedSolution = new System.Windows.Forms.CheckBox();
            this.chkIncrementSolutionVersion = new System.Windows.Forms.CheckBox();
            this.btnBuildSolution = new System.Windows.Forms.Button();
            this.txtSolutionVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateSolution = new System.Windows.Forms.Button();
            this.txtPublisherPrefix = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDeploy = new System.Windows.Forms.Button();
            this.txtPublisherName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSolutionName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.grpBoxComponentDetails = new System.Windows.Forms.GroupBox();
            this.lblResxFileExists = new System.Windows.Forms.Label();
            this.lblCssFileExists = new System.Windows.Forms.Label();
            this.lblPreviewImageExists = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cboxAdditionalPackages = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnAddResxFile = new System.Windows.Forms.Button();
            this.btnAddPreviewImage = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.lblControlInitStatus = new System.Windows.Forms.Label();
            this.linklblQuickDeployLearn = new System.Windows.Forms.LinkLabel();
            this.btnQuickDeploy = new System.Windows.Forms.Button();
            this.btnOpenControlInExplorer = new System.Windows.Forms.Button();
            this.chkIncrementComponentVersion = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtComponentVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.chkNoWatch = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTestComponent = new System.Windows.Forms.Button();
            this.txtControlName = new System.Windows.Forms.TextBox();
            this.btnBuildComponent = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOpenInVSCode = new System.Windows.Forms.Button();
            this.btnCreateComponent = new System.Windows.Forms.Button();
            this.cboxTemplate = new System.Windows.Forms.ComboBox();
            this.txtWorkingFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnWorkingFolderSelector = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.linklblCreator = new System.Windows.Forms.LinkLabel();
            this.toolStripMenu.SuspendLayout();
            this.gboxQuickAction.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRunning)).BeginInit();
            this.grpBoxAuthProfileDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMRULocations)).BeginInit();
            this.gboxCommandPrompt.SuspendLayout();
            this.grpBoxSolutionDetails.SuspendLayout();
            this.grpBoxComponentDetails.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.tsbNewPCFMenu,
            this.tsbEditControl,
            this.toolStripSeparator2,
            this.tsbAuthProfile,
            this.toolStripSeparator4,
            this.tspGallery,
            this.tspSampleControls,
            this.toolStripSeparator1,
            this.tspCLIHelp,
            this.tspHelp,
            this.tspMoreLinks,
            this.toolStripSeparator3,
            this.tspSettings});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(1409, 31);
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
            // tsbNewPCFMenu
            // 
            this.tsbNewPCFMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmNewPCFBlank,
            this.tsmNewPCFTemplate});
            this.tsbNewPCFMenu.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewPCFMenu.Image")));
            this.tsbNewPCFMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewPCFMenu.Name = "tsbNewPCFMenu";
            this.tsbNewPCFMenu.Size = new System.Drawing.Size(138, 28);
            this.tsbNewPCFMenu.Text = "New PCF Control";
            this.tsbNewPCFMenu.ButtonClick += new System.EventHandler(this.TsbNewPCFMenu_ButtonClick);
            // 
            // tsmNewPCFBlank
            // 
            this.tsmNewPCFBlank.Name = "tsmNewPCFBlank";
            this.tsmNewPCFBlank.Size = new System.Drawing.Size(178, 22);
            this.tsmNewPCFBlank.Text = "New from Blank";
            this.tsmNewPCFBlank.Click += new System.EventHandler(this.TsmNewPCFBlank_Click);
            // 
            // tsmNewPCFTemplate
            // 
            this.tsmNewPCFTemplate.Name = "tsmNewPCFTemplate";
            this.tsmNewPCFTemplate.Size = new System.Drawing.Size(178, 22);
            this.tsmNewPCFTemplate.Text = "New from Template";
            this.tsmNewPCFTemplate.Click += new System.EventHandler(this.TsmNewPCFTemplate_Click);
            // 
            // tsbEditControl
            // 
            this.tsbEditControl.Image = ((System.Drawing.Image)(resources.GetObject("tsbEditControl.Image")));
            this.tsbEditControl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditControl.Name = "tsbEditControl";
            this.tsbEditControl.Size = new System.Drawing.Size(120, 28);
            this.tsbEditControl.Text = "Edit PCF control";
            this.tsbEditControl.Click += new System.EventHandler(this.tsbEditControl_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbAuthProfile
            // 
            this.tsbAuthProfile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCreateProfile,
            this.tsmListProfiles});
            this.tsbAuthProfile.Image = ((System.Drawing.Image)(resources.GetObject("tsbAuthProfile.Image")));
            this.tsbAuthProfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAuthProfile.Name = "tsbAuthProfile";
            this.tsbAuthProfile.Size = new System.Drawing.Size(168, 28);
            this.tsbAuthProfile.Text = "Authentication Profiles";
            this.tsbAuthProfile.ButtonClick += new System.EventHandler(this.tsbAuthProfile_ButtonClick);
            // 
            // tsmCreateProfile
            // 
            this.tsmCreateProfile.Name = "tsmCreateProfile";
            this.tsmCreateProfile.Size = new System.Drawing.Size(145, 22);
            this.tsmCreateProfile.Text = "Create Profile";
            this.tsmCreateProfile.Click += new System.EventHandler(this.tsmCreateProfile_Click);
            // 
            // tsmListProfiles
            // 
            this.tsmListProfiles.Name = "tsmListProfiles";
            this.tsmListProfiles.Size = new System.Drawing.Size(145, 22);
            this.tsmListProfiles.Text = "List Profiles";
            this.tsmListProfiles.Click += new System.EventHandler(this.tsmListProfiles_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // tspGallery
            // 
            this.tspGallery.Image = ((System.Drawing.Image)(resources.GetObject("tspGallery.Image")));
            this.tspGallery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspGallery.Name = "tspGallery";
            this.tspGallery.Size = new System.Drawing.Size(95, 28);
            this.tspGallery.Text = "PCF Gallery";
            this.tspGallery.Click += new System.EventHandler(this.tspGallery_Click);
            // 
            // tspSampleControls
            // 
            this.tspSampleControls.Image = ((System.Drawing.Image)(resources.GetObject("tspSampleControls.Image")));
            this.tspSampleControls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSampleControls.Name = "tspSampleControls";
            this.tspSampleControls.Size = new System.Drawing.Size(122, 28);
            this.tspSampleControls.Text = "Sample Controls";
            this.tspSampleControls.Click += new System.EventHandler(this.tspSampleControls_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tspCLIHelp
            // 
            this.tspCLIHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspmDownloadPowerAppsCLI,
            this.tspmUpdatePowerAppsCLI});
            this.tspCLIHelp.Image = ((System.Drawing.Image)(resources.GetObject("tspCLIHelp.Image")));
            this.tspCLIHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspCLIHelp.Name = "tspCLIHelp";
            this.tspCLIHelp.Size = new System.Drawing.Size(124, 28);
            this.tspCLIHelp.Text = "PowerApps CLI";
            this.tspCLIHelp.ToolTipText = "PowerApps CLI";
            // 
            // tspmDownloadPowerAppsCLI
            // 
            this.tspmDownloadPowerAppsCLI.Image = ((System.Drawing.Image)(resources.GetObject("tspmDownloadPowerAppsCLI.Image")));
            this.tspmDownloadPowerAppsCLI.Name = "tspmDownloadPowerAppsCLI";
            this.tspmDownloadPowerAppsCLI.Size = new System.Drawing.Size(211, 22);
            this.tspmDownloadPowerAppsCLI.Text = "Download PowerApps CLI";
            this.tspmDownloadPowerAppsCLI.Click += new System.EventHandler(this.tspmDownloadPowerAppsCLI_Click);
            // 
            // tspmUpdatePowerAppsCLI
            // 
            this.tspmUpdatePowerAppsCLI.Image = ((System.Drawing.Image)(resources.GetObject("tspmUpdatePowerAppsCLI.Image")));
            this.tspmUpdatePowerAppsCLI.Name = "tspmUpdatePowerAppsCLI";
            this.tspmUpdatePowerAppsCLI.Size = new System.Drawing.Size(211, 22);
            this.tspmUpdatePowerAppsCLI.Text = "Update PowerApps CLI";
            this.tspmUpdatePowerAppsCLI.Click += new System.EventHandler(this.tspmUpdatePowerAppsCLI_Click);
            // 
            // tspHelp
            // 
            this.tspHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspmPCFOverview,
            this.tspmMSDocs,
            this.tspmPCFLimitations});
            this.tspHelp.Image = ((System.Drawing.Image)(resources.GetObject("tspHelp.Image")));
            this.tspHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspHelp.Name = "tspHelp";
            this.tspHelp.Size = new System.Drawing.Size(69, 28);
            this.tspHelp.Text = "Help";
            // 
            // tspmPCFOverview
            // 
            this.tspmPCFOverview.Image = ((System.Drawing.Image)(resources.GetObject("tspmPCFOverview.Image")));
            this.tspmPCFOverview.Name = "tspmPCFOverview";
            this.tspmPCFOverview.Size = new System.Drawing.Size(289, 22);
            this.tspmPCFOverview.Text = "PowerApps Component Framework";
            this.tspmPCFOverview.Click += new System.EventHandler(this.tspmPCFOverview_Click);
            // 
            // tspmMSDocs
            // 
            this.tspmMSDocs.Image = ((System.Drawing.Image)(resources.GetObject("tspmMSDocs.Image")));
            this.tspmMSDocs.Name = "tspmMSDocs";
            this.tspmMSDocs.Size = new System.Drawing.Size(289, 22);
            this.tspmMSDocs.Text = "Microsoft Docs for Custom Components";
            this.tspmMSDocs.Click += new System.EventHandler(this.tspmMSDocs_Click);
            // 
            // tspmPCFLimitations
            // 
            this.tspmPCFLimitations.Image = ((System.Drawing.Image)(resources.GetObject("tspmPCFLimitations.Image")));
            this.tspmPCFLimitations.Name = "tspmPCFLimitations";
            this.tspmPCFLimitations.Size = new System.Drawing.Size(289, 22);
            this.tspmPCFLimitations.Text = "PCF Limitations";
            this.tspmPCFLimitations.Click += new System.EventHandler(this.tspmPCFLimitations_Click);
            // 
            // tspMoreLinks
            // 
            this.tspMoreLinks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspmDemos,
            this.tspmBlogs,
            this.tspmForums,
            this.tspmIdeas});
            this.tspMoreLinks.Image = ((System.Drawing.Image)(resources.GetObject("tspMoreLinks.Image")));
            this.tspMoreLinks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspMoreLinks.Name = "tspMoreLinks";
            this.tspMoreLinks.Size = new System.Drawing.Size(102, 28);
            this.tspMoreLinks.Text = "More Links";
            // 
            // tspmDemos
            // 
            this.tspmDemos.Image = ((System.Drawing.Image)(resources.GetObject("tspmDemos.Image")));
            this.tspmDemos.Name = "tspmDemos";
            this.tspmDemos.Size = new System.Drawing.Size(114, 22);
            this.tspmDemos.Text = "Demos";
            this.tspmDemos.Click += new System.EventHandler(this.TspmDemos_Click);
            // 
            // tspmBlogs
            // 
            this.tspmBlogs.Image = ((System.Drawing.Image)(resources.GetObject("tspmBlogs.Image")));
            this.tspmBlogs.Name = "tspmBlogs";
            this.tspmBlogs.Size = new System.Drawing.Size(114, 22);
            this.tspmBlogs.Text = "Blogs";
            this.tspmBlogs.Click += new System.EventHandler(this.TspmBlogs_Click);
            // 
            // tspmForums
            // 
            this.tspmForums.Image = ((System.Drawing.Image)(resources.GetObject("tspmForums.Image")));
            this.tspmForums.Name = "tspmForums";
            this.tspmForums.Size = new System.Drawing.Size(114, 22);
            this.tspmForums.Text = "Forums";
            this.tspmForums.Click += new System.EventHandler(this.TspmForums_Click);
            // 
            // tspmIdeas
            // 
            this.tspmIdeas.Image = ((System.Drawing.Image)(resources.GetObject("tspmIdeas.Image")));
            this.tspmIdeas.Name = "tspmIdeas";
            this.tspmIdeas.Size = new System.Drawing.Size(114, 22);
            this.tspmIdeas.Text = "Ideas";
            this.tspmIdeas.Click += new System.EventHandler(this.TspmIdeas_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // tspSettings
            // 
            this.tspSettings.Image = ((System.Drawing.Image)(resources.GetObject("tspSettings.Image")));
            this.tspSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSettings.Name = "tspSettings";
            this.tspSettings.Size = new System.Drawing.Size(77, 28);
            this.tspSettings.Text = "Settings";
            this.tspSettings.Click += new System.EventHandler(this.TspSettings_Click);
            // 
            // gboxQuickAction
            // 
            this.gboxQuickAction.Controls.Add(this.btnBuildAndDeployAll);
            this.gboxQuickAction.Controls.Add(this.btnBuildAllProjects);
            this.gboxQuickAction.Controls.Add(this.btnBuildAndTest);
            this.gboxQuickAction.Location = new System.Drawing.Point(529, 8);
            this.gboxQuickAction.Name = "gboxQuickAction";
            this.gboxQuickAction.Size = new System.Drawing.Size(453, 57);
            this.gboxQuickAction.TabIndex = 19;
            this.gboxQuickAction.TabStop = false;
            this.gboxQuickAction.Text = "Quick Actions";
            // 
            // btnBuildAndDeployAll
            // 
            this.btnBuildAndDeployAll.Location = new System.Drawing.Point(304, 19);
            this.btnBuildAndDeployAll.Name = "btnBuildAndDeployAll";
            this.btnBuildAndDeployAll.Size = new System.Drawing.Size(143, 23);
            this.btnBuildAndDeployAll.TabIndex = 37;
            this.btnBuildAndDeployAll.Text = "Build All and Deploy";
            this.btnBuildAndDeployAll.UseVisualStyleBackColor = true;
            this.btnBuildAndDeployAll.Click += new System.EventHandler(this.BtnBuildAndDeployAll_Click);
            // 
            // btnBuildAllProjects
            // 
            this.btnBuildAllProjects.Location = new System.Drawing.Point(155, 19);
            this.btnBuildAllProjects.Name = "btnBuildAllProjects";
            this.btnBuildAllProjects.Size = new System.Drawing.Size(143, 23);
            this.btnBuildAllProjects.TabIndex = 36;
            this.btnBuildAllProjects.Text = "Build All Projects";
            this.btnBuildAllProjects.UseVisualStyleBackColor = true;
            this.btnBuildAllProjects.Click += new System.EventHandler(this.BtnBuildAllProjects_Click);
            // 
            // btnBuildAndTest
            // 
            this.btnBuildAndTest.Location = new System.Drawing.Point(6, 19);
            this.btnBuildAndTest.Name = "btnBuildAndTest";
            this.btnBuildAndTest.Size = new System.Drawing.Size(143, 23);
            this.btnBuildAndTest.TabIndex = 35;
            this.btnBuildAndTest.Text = "Build and Test Control";
            this.btnBuildAndTest.UseVisualStyleBackColor = true;
            this.btnBuildAndTest.Click += new System.EventHandler(this.BtnBuildAndTest_Click);
            // 
            // lblPCFCLIVersionMsg
            // 
            this.lblPCFCLIVersionMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPCFCLIVersionMsg.Location = new System.Drawing.Point(1232, 0);
            this.lblPCFCLIVersionMsg.Name = "lblPCFCLIVersionMsg";
            this.lblPCFCLIVersionMsg.Size = new System.Drawing.Size(174, 23);
            this.lblPCFCLIVersionMsg.TabIndex = 21;
            this.lblPCFCLIVersionMsg.Text = "Detecting Power Apps CLI version.";
            this.lblPCFCLIVersionMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.picRunning);
            this.pnlMain.Controls.Add(this.grpBoxAuthProfileDetails);
            this.pnlMain.Controls.Add(this.dgvMRULocations);
            this.pnlMain.Controls.Add(this.btnShowMRULocations);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.linklblPowerBiReport);
            this.pnlMain.Controls.Add(this.lblnpmVersionMsg);
            this.pnlMain.Controls.Add(this.lblStatus);
            this.pnlMain.Controls.Add(this.btnClearConsole);
            this.pnlMain.Controls.Add(this.btnRefreshDetails);
            this.pnlMain.Controls.Add(this.lblErrors);
            this.pnlMain.Controls.Add(this.lblPCFCLIVersionMsg);
            this.pnlMain.Controls.Add(this.gboxQuickAction);
            this.pnlMain.Controls.Add(this.gboxCommandPrompt);
            this.pnlMain.Controls.Add(this.grpBoxSolutionDetails);
            this.pnlMain.Controls.Add(this.grpBoxComponentDetails);
            this.pnlMain.Controls.Add(this.txtWorkingFolder);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.btnWorkingFolderSelector);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 31);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1409, 750);
            this.pnlMain.TabIndex = 22;
            // 
            // picRunning
            // 
            this.picRunning.Location = new System.Drawing.Point(531, 68);
            this.picRunning.Name = "picRunning";
            this.picRunning.Size = new System.Drawing.Size(32, 32);
            this.picRunning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRunning.TabIndex = 37;
            this.picRunning.TabStop = false;
            this.picRunning.Visible = false;
            // 
            // grpBoxAuthProfileDetails
            // 
            this.grpBoxAuthProfileDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpBoxAuthProfileDetails.Controls.Add(this.lblCurrentProfile);
            this.grpBoxAuthProfileDetails.Location = new System.Drawing.Point(19, 552);
            this.grpBoxAuthProfileDetails.Name = "grpBoxAuthProfileDetails";
            this.grpBoxAuthProfileDetails.Size = new System.Drawing.Size(494, 133);
            this.grpBoxAuthProfileDetails.TabIndex = 36;
            this.grpBoxAuthProfileDetails.TabStop = false;
            this.grpBoxAuthProfileDetails.Text = "Current Authentication Profile";
            // 
            // lblCurrentProfile
            // 
            this.lblCurrentProfile.Location = new System.Drawing.Point(6, 16);
            this.lblCurrentProfile.Name = "lblCurrentProfile";
            this.lblCurrentProfile.Size = new System.Drawing.Size(483, 114);
            this.lblCurrentProfile.TabIndex = 29;
            this.lblCurrentProfile.Text = "Loading....";
            // 
            // dgvMRULocations
            // 
            this.dgvMRULocations.AllowUserToAddRows = false;
            this.dgvMRULocations.AllowUserToDeleteRows = false;
            this.dgvMRULocations.AllowUserToResizeRows = false;
            this.dgvMRULocations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMRULocations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FolderName,
            this.Date,
            this.Location});
            this.dgvMRULocations.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMRULocations.Location = new System.Drawing.Point(115, 42);
            this.dgvMRULocations.MultiSelect = false;
            this.dgvMRULocations.Name = "dgvMRULocations";
            this.dgvMRULocations.ReadOnly = true;
            this.dgvMRULocations.RowHeadersVisible = false;
            this.dgvMRULocations.Size = new System.Drawing.Size(344, 150);
            this.dgvMRULocations.TabIndex = 34;
            this.dgvMRULocations.Visible = false;
            this.dgvMRULocations.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMRULocations_CellClick);
            this.dgvMRULocations.Leave += new System.EventHandler(this.dgvMRULocations_Leave);
            // 
            // FolderName
            // 
            this.FolderName.DataPropertyName = "FolderName";
            this.FolderName.HeaderText = "Folder Name";
            this.FolderName.Name = "FolderName";
            this.FolderName.ReadOnly = true;
            this.FolderName.Width = 150;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "Date";
            this.Date.HeaderText = "Last Used";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 110;
            // 
            // Location
            // 
            this.Location.DataPropertyName = "Location";
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            this.Location.Width = 200;
            // 
            // btnShowMRULocations
            // 
            this.btnShowMRULocations.Location = new System.Drawing.Point(433, 22);
            this.btnShowMRULocations.Name = "btnShowMRULocations";
            this.btnShowMRULocations.Size = new System.Drawing.Size(27, 20);
            this.btnShowMRULocations.TabIndex = 35;
            this.btnShowMRULocations.Text = "🔽";
            this.toolTip.SetToolTip(this.btnShowMRULocations, "Show Most Recent Used Location");
            this.btnShowMRULocations.UseVisualStyleBackColor = true;
            this.btnShowMRULocations.Click += new System.EventHandler(this.btnShowMRULocations_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1157, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(221, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "*This will not terminate any running processes";
            // 
            // linklblPowerBiReport
            // 
            this.linklblPowerBiReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linklblPowerBiReport.AutoSize = true;
            this.linklblPowerBiReport.Location = new System.Drawing.Point(1133, 694);
            this.linklblPowerBiReport.Name = "linklblPowerBiReport";
            this.linklblPowerBiReport.Size = new System.Drawing.Size(242, 13);
            this.linklblPowerBiReport.TabIndex = 31;
            this.linklblPowerBiReport.TabStop = true;
            this.linklblPowerBiReport.Text = "Check out the report on how the tool is performing";
            this.linklblPowerBiReport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblPowerBiReport_LinkClicked);
            // 
            // lblnpmVersionMsg
            // 
            this.lblnpmVersionMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblnpmVersionMsg.Location = new System.Drawing.Point(1256, 23);
            this.lblnpmVersionMsg.Name = "lblnpmVersionMsg";
            this.lblnpmVersionMsg.Size = new System.Drawing.Size(150, 23);
            this.lblnpmVersionMsg.TabIndex = 27;
            this.lblnpmVersionMsg.Text = "Detecting npm version";
            this.lblnpmVersionMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(569, 72);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(156, 23);
            this.lblStatus.TabIndex = 26;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClearConsole
            // 
            this.btnClearConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearConsole.Location = new System.Drawing.Point(1285, 61);
            this.btnClearConsole.Name = "btnClearConsole";
            this.btnClearConsole.Size = new System.Drawing.Size(93, 23);
            this.btnClearConsole.TabIndex = 1;
            this.btnClearConsole.Text = "Clear Console";
            this.btnClearConsole.UseVisualStyleBackColor = true;
            this.btnClearConsole.Click += new System.EventHandler(this.BtnClearConsole_Click);
            // 
            // btnRefreshDetails
            // 
            this.btnRefreshDetails.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshDetails.Image")));
            this.btnRefreshDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefreshDetails.Location = new System.Drawing.Point(115, 49);
            this.btnRefreshDetails.Name = "btnRefreshDetails";
            this.btnRefreshDetails.Size = new System.Drawing.Size(107, 23);
            this.btnRefreshDetails.TabIndex = 22;
            this.btnRefreshDetails.Text = "Reload Details";
            this.btnRefreshDetails.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefreshDetails.UseVisualStyleBackColor = true;
            this.btnRefreshDetails.Click += new System.EventHandler(this.BtnRefreshDetails_Click);
            // 
            // lblErrors
            // 
            this.lblErrors.ForeColor = System.Drawing.Color.Red;
            this.lblErrors.Location = new System.Drawing.Point(310, 49);
            this.lblErrors.Name = "lblErrors";
            this.lblErrors.Size = new System.Drawing.Size(203, 51);
            this.lblErrors.TabIndex = 21;
            this.lblErrors.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // gboxCommandPrompt
            // 
            this.gboxCommandPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxCommandPrompt.Controls.Add(this.consoleControl);
            this.gboxCommandPrompt.Location = new System.Drawing.Point(529, 102);
            this.gboxCommandPrompt.Name = "gboxCommandPrompt";
            this.gboxCommandPrompt.Size = new System.Drawing.Size(849, 586);
            this.gboxCommandPrompt.TabIndex = 19;
            this.gboxCommandPrompt.TabStop = false;
            this.gboxCommandPrompt.Text = "Command Prompt";
            // 
            // consoleControl
            // 
            this.consoleControl.AutoScroll = true;
            this.consoleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleControl.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleControl.IsInputEnabled = false;
            this.consoleControl.Location = new System.Drawing.Point(3, 16);
            this.consoleControl.Name = "consoleControl";
            this.consoleControl.SendKeyboardCommandsToProcess = true;
            this.consoleControl.ShowDiagnostics = false;
            this.consoleControl.Size = new System.Drawing.Size(843, 567);
            this.consoleControl.TabIndex = 0;
            this.consoleControl.OnConsoleOutput += new ConsoleControl.ConsoleEventHandler(this.ConsoleControl_OnConsoleOutput);
            // 
            // grpBoxSolutionDetails
            // 
            this.grpBoxSolutionDetails.Controls.Add(this.cboxSolutions);
            this.grpBoxSolutionDetails.Controls.Add(this.chkUseExistingSolution);
            this.grpBoxSolutionDetails.Controls.Add(this.lblSolutionInitStatus);
            this.grpBoxSolutionDetails.Controls.Add(this.chkManagedSolution);
            this.grpBoxSolutionDetails.Controls.Add(this.chkIncrementSolutionVersion);
            this.grpBoxSolutionDetails.Controls.Add(this.btnBuildSolution);
            this.grpBoxSolutionDetails.Controls.Add(this.txtSolutionVersion);
            this.grpBoxSolutionDetails.Controls.Add(this.label2);
            this.grpBoxSolutionDetails.Controls.Add(this.btnCreateSolution);
            this.grpBoxSolutionDetails.Controls.Add(this.txtPublisherPrefix);
            this.grpBoxSolutionDetails.Controls.Add(this.label7);
            this.grpBoxSolutionDetails.Controls.Add(this.btnDeploy);
            this.grpBoxSolutionDetails.Controls.Add(this.txtPublisherName);
            this.grpBoxSolutionDetails.Controls.Add(this.label8);
            this.grpBoxSolutionDetails.Controls.Add(this.txtSolutionName);
            this.grpBoxSolutionDetails.Controls.Add(this.label9);
            this.grpBoxSolutionDetails.Location = new System.Drawing.Point(19, 379);
            this.grpBoxSolutionDetails.Name = "grpBoxSolutionDetails";
            this.grpBoxSolutionDetails.Size = new System.Drawing.Size(494, 167);
            this.grpBoxSolutionDetails.TabIndex = 18;
            this.grpBoxSolutionDetails.TabStop = false;
            this.grpBoxSolutionDetails.Text = "CDS Solution Details";
            // 
            // cboxSolutions
            // 
            this.cboxSolutions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxSolutions.FormattingEnabled = true;
            this.cboxSolutions.Items.AddRange(new object[] {
            "Field",
            "Dataset"});
            this.cboxSolutions.Location = new System.Drawing.Point(110, 56);
            this.cboxSolutions.Name = "cboxSolutions";
            this.cboxSolutions.Size = new System.Drawing.Size(175, 21);
            this.cboxSolutions.TabIndex = 30;
            this.cboxSolutions.Visible = false;
            this.cboxSolutions.SelectedIndexChanged += new System.EventHandler(this.cboxSolutions_SelectedIndexChanged);
            // 
            // chkUseExistingSolution
            // 
            this.chkUseExistingSolution.AutoSize = true;
            this.chkUseExistingSolution.Location = new System.Drawing.Point(110, 33);
            this.chkUseExistingSolution.Name = "chkUseExistingSolution";
            this.chkUseExistingSolution.Size = new System.Drawing.Size(125, 17);
            this.chkUseExistingSolution.TabIndex = 37;
            this.chkUseExistingSolution.Text = "Use Existing Solution";
            this.chkUseExistingSolution.UseVisualStyleBackColor = true;
            this.chkUseExistingSolution.CheckedChanged += new System.EventHandler(this.chkUseExistingSolution_CheckedChanged);
            // 
            // lblSolutionInitStatus
            // 
            this.lblSolutionInitStatus.AutoSize = true;
            this.lblSolutionInitStatus.ForeColor = System.Drawing.Color.Firebrick;
            this.lblSolutionInitStatus.Location = new System.Drawing.Point(6, 21);
            this.lblSolutionInitStatus.Name = "lblSolutionInitStatus";
            this.lblSolutionInitStatus.Size = new System.Drawing.Size(85, 13);
            this.lblSolutionInitStatus.TabIndex = 30;
            this.lblSolutionInitStatus.Text = "❌ Not Initialized";
            // 
            // chkManagedSolution
            // 
            this.chkManagedSolution.AutoSize = true;
            this.chkManagedSolution.Location = new System.Drawing.Point(413, 81);
            this.chkManagedSolution.Name = "chkManagedSolution";
            this.chkManagedSolution.Size = new System.Drawing.Size(71, 17);
            this.chkManagedSolution.TabIndex = 36;
            this.chkManagedSolution.Text = "Managed";
            this.chkManagedSolution.UseVisualStyleBackColor = true;
            // 
            // chkIncrementSolutionVersion
            // 
            this.chkIncrementSolutionVersion.AutoSize = true;
            this.chkIncrementSolutionVersion.Checked = true;
            this.chkIncrementSolutionVersion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncrementSolutionVersion.Location = new System.Drawing.Point(168, 136);
            this.chkIncrementSolutionVersion.Name = "chkIncrementSolutionVersion";
            this.chkIncrementSolutionVersion.Size = new System.Drawing.Size(111, 17);
            this.chkIncrementSolutionVersion.TabIndex = 35;
            this.chkIncrementSolutionVersion.Text = "Increment Version";
            this.chkIncrementSolutionVersion.UseVisualStyleBackColor = true;
            // 
            // btnBuildSolution
            // 
            this.btnBuildSolution.Location = new System.Drawing.Point(340, 77);
            this.btnBuildSolution.Name = "btnBuildSolution";
            this.btnBuildSolution.Size = new System.Drawing.Size(67, 23);
            this.btnBuildSolution.TabIndex = 34;
            this.btnBuildSolution.Text = "Build";
            this.btnBuildSolution.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuildSolution.UseVisualStyleBackColor = true;
            this.btnBuildSolution.Click += new System.EventHandler(this.BtnBuildSolution_Click);
            // 
            // txtSolutionVersion
            // 
            this.txtSolutionVersion.Location = new System.Drawing.Point(110, 134);
            this.txtSolutionVersion.Name = "txtSolutionVersion";
            this.txtSolutionVersion.ReadOnly = true;
            this.txtSolutionVersion.Size = new System.Drawing.Size(50, 20);
            this.txtSolutionVersion.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Version";
            // 
            // btnCreateSolution
            // 
            this.btnCreateSolution.Location = new System.Drawing.Point(340, 48);
            this.btnCreateSolution.Name = "btnCreateSolution";
            this.btnCreateSolution.Size = new System.Drawing.Size(148, 23);
            this.btnCreateSolution.TabIndex = 19;
            this.btnCreateSolution.Text = "Create and Add Control";
            this.btnCreateSolution.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreateSolution.UseVisualStyleBackColor = true;
            this.btnCreateSolution.Click += new System.EventHandler(this.BtnCreateSolution_Click);
            // 
            // txtPublisherPrefix
            // 
            this.txtPublisherPrefix.Location = new System.Drawing.Point(110, 108);
            this.txtPublisherPrefix.Name = "txtPublisherPrefix";
            this.txtPublisherPrefix.Size = new System.Drawing.Size(175, 20);
            this.txtPublisherPrefix.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Publisher Prefix";
            // 
            // btnDeploy
            // 
            this.btnDeploy.Enabled = false;
            this.btnDeploy.Location = new System.Drawing.Point(340, 106);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(148, 23);
            this.btnDeploy.TabIndex = 20;
            this.btnDeploy.Text = "Deploy";
            this.btnDeploy.UseVisualStyleBackColor = true;
            this.btnDeploy.Click += new System.EventHandler(this.BtnDeploy_Click);
            // 
            // txtPublisherName
            // 
            this.txtPublisherName.Location = new System.Drawing.Point(110, 82);
            this.txtPublisherName.Name = "txtPublisherName";
            this.txtPublisherName.Size = new System.Drawing.Size(175, 20);
            this.txtPublisherName.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Publisher Name";
            // 
            // txtSolutionName
            // 
            this.txtSolutionName.Location = new System.Drawing.Point(110, 56);
            this.txtSolutionName.Name = "txtSolutionName";
            this.txtSolutionName.Size = new System.Drawing.Size(175, 20);
            this.txtSolutionName.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Solution Name";
            // 
            // grpBoxComponentDetails
            // 
            this.grpBoxComponentDetails.Controls.Add(this.lblResxFileExists);
            this.grpBoxComponentDetails.Controls.Add(this.lblCssFileExists);
            this.grpBoxComponentDetails.Controls.Add(this.lblPreviewImageExists);
            this.grpBoxComponentDetails.Controls.Add(this.label14);
            this.grpBoxComponentDetails.Controls.Add(this.cboxAdditionalPackages);
            this.grpBoxComponentDetails.Controls.Add(this.tableLayoutPanel1);
            this.grpBoxComponentDetails.Controls.Add(this.label11);
            this.grpBoxComponentDetails.Controls.Add(this.lblControlInitStatus);
            this.grpBoxComponentDetails.Controls.Add(this.linklblQuickDeployLearn);
            this.grpBoxComponentDetails.Controls.Add(this.btnQuickDeploy);
            this.grpBoxComponentDetails.Controls.Add(this.btnOpenControlInExplorer);
            this.grpBoxComponentDetails.Controls.Add(this.chkIncrementComponentVersion);
            this.grpBoxComponentDetails.Controls.Add(this.label1);
            this.grpBoxComponentDetails.Controls.Add(this.txtComponentVersion);
            this.grpBoxComponentDetails.Controls.Add(this.label4);
            this.grpBoxComponentDetails.Controls.Add(this.txtNamespace);
            this.grpBoxComponentDetails.Controls.Add(this.chkNoWatch);
            this.grpBoxComponentDetails.Controls.Add(this.label5);
            this.grpBoxComponentDetails.Controls.Add(this.btnTestComponent);
            this.grpBoxComponentDetails.Controls.Add(this.txtControlName);
            this.grpBoxComponentDetails.Controls.Add(this.btnBuildComponent);
            this.grpBoxComponentDetails.Controls.Add(this.label6);
            this.grpBoxComponentDetails.Controls.Add(this.btnOpenInVSCode);
            this.grpBoxComponentDetails.Controls.Add(this.btnCreateComponent);
            this.grpBoxComponentDetails.Controls.Add(this.cboxTemplate);
            this.grpBoxComponentDetails.Location = new System.Drawing.Point(19, 102);
            this.grpBoxComponentDetails.Name = "grpBoxComponentDetails";
            this.grpBoxComponentDetails.Size = new System.Drawing.Size(494, 271);
            this.grpBoxComponentDetails.TabIndex = 12;
            this.grpBoxComponentDetails.TabStop = false;
            this.grpBoxComponentDetails.Text = "PCF Component Details";
            // 
            // lblResxFileExists
            // 
            this.lblResxFileExists.ForeColor = System.Drawing.Color.Firebrick;
            this.lblResxFileExists.Location = new System.Drawing.Point(340, 236);
            this.lblResxFileExists.Name = "lblResxFileExists";
            this.lblResxFileExists.Size = new System.Drawing.Size(148, 16);
            this.lblResxFileExists.TabIndex = 40;
            this.lblResxFileExists.Text = "❌ No RESX file";
            this.lblResxFileExists.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCssFileExists
            // 
            this.lblCssFileExists.ForeColor = System.Drawing.Color.Firebrick;
            this.lblCssFileExists.Location = new System.Drawing.Point(340, 214);
            this.lblCssFileExists.Name = "lblCssFileExists";
            this.lblCssFileExists.Size = new System.Drawing.Size(148, 16);
            this.lblCssFileExists.TabIndex = 39;
            this.lblCssFileExists.Text = "❌ No CSS file";
            this.lblCssFileExists.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPreviewImageExists
            // 
            this.lblPreviewImageExists.ForeColor = System.Drawing.Color.Firebrick;
            this.lblPreviewImageExists.Location = new System.Drawing.Point(340, 194);
            this.lblPreviewImageExists.Name = "lblPreviewImageExists";
            this.lblPreviewImageExists.Size = new System.Drawing.Size(148, 16);
            this.lblPreviewImageExists.TabIndex = 38;
            this.lblPreviewImageExists.Text = "❌ No Preview Image";
            this.lblPreviewImageExists.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 130);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 26);
            this.label14.TabIndex = 36;
            this.label14.Text = "Additional\r\nPackages";
            // 
            // cboxAdditionalPackages
            // 
            this.cboxAdditionalPackages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxAdditionalPackages.FormattingEnabled = true;
            this.cboxAdditionalPackages.Location = new System.Drawing.Point(110, 127);
            this.cboxAdditionalPackages.Name = "cboxAdditionalPackages";
            this.cboxAdditionalPackages.Size = new System.Drawing.Size(175, 21);
            this.cboxAdditionalPackages.TabIndex = 37;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.84932F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.15068F));
            this.tableLayoutPanel1.Controls.Add(this.label13, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAddResxFile, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddPreviewImage, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(110, 191);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.0625F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.9375F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(134, 64);
            this.tableLayoutPanel1.TabIndex = 35;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(83, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 12);
            this.label13.TabIndex = 34;
            this.label13.Text = "RESX file";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 12);
            this.label12.TabIndex = 33;
            this.label12.Text = "Preview Image";
            // 
            // btnAddResxFile
            // 
            this.btnAddResxFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddResxFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddResxFile.BackgroundImage")));
            this.btnAddResxFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddResxFile.FlatAppearance.BorderSize = 0;
            this.btnAddResxFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddResxFile.Location = new System.Drawing.Point(89, 3);
            this.btnAddResxFile.Name = "btnAddResxFile";
            this.btnAddResxFile.Size = new System.Drawing.Size(32, 32);
            this.btnAddResxFile.TabIndex = 32;
            this.toolTip.SetToolTip(this.btnAddResxFile, "Add RESX file");
            this.btnAddResxFile.UseVisualStyleBackColor = true;
            this.btnAddResxFile.Click += new System.EventHandler(this.btnAddResxFile_Click);
            // 
            // btnAddPreviewImage
            // 
            this.btnAddPreviewImage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddPreviewImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddPreviewImage.BackgroundImage")));
            this.btnAddPreviewImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddPreviewImage.FlatAppearance.BorderSize = 0;
            this.btnAddPreviewImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPreviewImage.Location = new System.Drawing.Point(22, 3);
            this.btnAddPreviewImage.Name = "btnAddPreviewImage";
            this.btnAddPreviewImage.Size = new System.Drawing.Size(32, 32);
            this.btnAddPreviewImage.TabIndex = 30;
            this.toolTip.SetToolTip(this.btnAddPreviewImage, "Add Preview Image");
            this.btnAddPreviewImage.UseVisualStyleBackColor = true;
            this.btnAddPreviewImage.Click += new System.EventHandler(this.btnAddPreviewImage_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 209);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Action Buttons";
            // 
            // lblControlInitStatus
            // 
            this.lblControlInitStatus.AutoSize = true;
            this.lblControlInitStatus.ForeColor = System.Drawing.Color.Firebrick;
            this.lblControlInitStatus.Location = new System.Drawing.Point(6, 20);
            this.lblControlInitStatus.Name = "lblControlInitStatus";
            this.lblControlInitStatus.Size = new System.Drawing.Size(85, 13);
            this.lblControlInitStatus.TabIndex = 29;
            this.lblControlInitStatus.Text = "❌ Not Initialized";
            // 
            // linklblQuickDeployLearn
            // 
            this.linklblQuickDeployLearn.AutoSize = true;
            this.linklblQuickDeployLearn.Location = new System.Drawing.Point(331, 171);
            this.linklblQuickDeployLearn.Name = "linklblQuickDeployLearn";
            this.linklblQuickDeployLearn.Size = new System.Drawing.Size(157, 13);
            this.linklblQuickDeployLearn.TabIndex = 28;
            this.linklblQuickDeployLearn.TabStop = true;
            this.linklblQuickDeployLearn.Text = "Learn more about Quick Deploy";
            this.linklblQuickDeployLearn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblQuickDeployLearn_LinkClicked);
            // 
            // btnQuickDeploy
            // 
            this.btnQuickDeploy.Location = new System.Drawing.Point(340, 145);
            this.btnQuickDeploy.Name = "btnQuickDeploy";
            this.btnQuickDeploy.Size = new System.Drawing.Size(148, 23);
            this.btnQuickDeploy.TabIndex = 27;
            this.btnQuickDeploy.Text = "Quick Deploy (Testing only)";
            this.btnQuickDeploy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.btnQuickDeploy, "This uses \'pac pcf push\' command");
            this.btnQuickDeploy.UseVisualStyleBackColor = true;
            this.btnQuickDeploy.Click += new System.EventHandler(this.btnQuickDeploy_Click);
            // 
            // btnOpenControlInExplorer
            // 
            this.btnOpenControlInExplorer.FlatAppearance.BorderSize = 0;
            this.btnOpenControlInExplorer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOpenControlInExplorer.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenControlInExplorer.Image")));
            this.btnOpenControlInExplorer.Location = new System.Drawing.Point(291, 73);
            this.btnOpenControlInExplorer.Name = "btnOpenControlInExplorer";
            this.btnOpenControlInExplorer.Size = new System.Drawing.Size(27, 20);
            this.btnOpenControlInExplorer.TabIndex = 26;
            this.btnOpenControlInExplorer.UseVisualStyleBackColor = true;
            this.btnOpenControlInExplorer.Click += new System.EventHandler(this.BtnOpenControlInExplorer_Click);
            // 
            // chkIncrementComponentVersion
            // 
            this.chkIncrementComponentVersion.AutoSize = true;
            this.chkIncrementComponentVersion.Checked = true;
            this.chkIncrementComponentVersion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncrementComponentVersion.Location = new System.Drawing.Point(166, 167);
            this.chkIncrementComponentVersion.Name = "chkIncrementComponentVersion";
            this.chkIncrementComponentVersion.Size = new System.Drawing.Size(111, 17);
            this.chkIncrementComponentVersion.TabIndex = 22;
            this.chkIncrementComponentVersion.Text = "Increment Version";
            this.chkIncrementComponentVersion.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Version";
            // 
            // txtComponentVersion
            // 
            this.txtComponentVersion.Location = new System.Drawing.Point(110, 165);
            this.txtComponentVersion.Name = "txtComponentVersion";
            this.txtComponentVersion.ReadOnly = true;
            this.txtComponentVersion.Size = new System.Drawing.Size(50, 20);
            this.txtComponentVersion.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Namespace";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(110, 47);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(175, 20);
            this.txtNamespace.TabIndex = 9;
            // 
            // chkNoWatch
            // 
            this.chkNoWatch.AutoSize = true;
            this.chkNoWatch.Location = new System.Drawing.Point(413, 120);
            this.chkNoWatch.Name = "chkNoWatch";
            this.chkNoWatch.Size = new System.Drawing.Size(75, 17);
            this.chkNoWatch.TabIndex = 17;
            this.chkNoWatch.Text = "No Watch";
            this.chkNoWatch.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Control Name";
            // 
            // btnTestComponent
            // 
            this.btnTestComponent.Location = new System.Drawing.Point(340, 116);
            this.btnTestComponent.Name = "btnTestComponent";
            this.btnTestComponent.Size = new System.Drawing.Size(67, 23);
            this.btnTestComponent.TabIndex = 16;
            this.btnTestComponent.Text = "Test";
            this.btnTestComponent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTestComponent.UseVisualStyleBackColor = true;
            this.btnTestComponent.Click += new System.EventHandler(this.BtnTestComponent_Click);
            // 
            // txtControlName
            // 
            this.txtControlName.Location = new System.Drawing.Point(110, 73);
            this.txtControlName.Name = "txtControlName";
            this.txtControlName.Size = new System.Drawing.Size(175, 20);
            this.txtControlName.TabIndex = 11;
            // 
            // btnBuildComponent
            // 
            this.btnBuildComponent.Location = new System.Drawing.Point(340, 87);
            this.btnBuildComponent.Name = "btnBuildComponent";
            this.btnBuildComponent.Size = new System.Drawing.Size(148, 23);
            this.btnBuildComponent.TabIndex = 15;
            this.btnBuildComponent.Text = "Build";
            this.btnBuildComponent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuildComponent.UseVisualStyleBackColor = true;
            this.btnBuildComponent.Click += new System.EventHandler(this.BtnBuildComponent_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Template";
            // 
            // btnOpenInVSCode
            // 
            this.btnOpenInVSCode.Location = new System.Drawing.Point(340, 58);
            this.btnOpenInVSCode.Name = "btnOpenInVSCode";
            this.btnOpenInVSCode.Size = new System.Drawing.Size(148, 23);
            this.btnOpenInVSCode.TabIndex = 14;
            this.btnOpenInVSCode.Text = "Open in VS Code";
            this.btnOpenInVSCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenInVSCode.UseVisualStyleBackColor = true;
            this.btnOpenInVSCode.Click += new System.EventHandler(this.BtnOpenInVSCode_Click);
            // 
            // btnCreateComponent
            // 
            this.btnCreateComponent.Location = new System.Drawing.Point(340, 29);
            this.btnCreateComponent.Name = "btnCreateComponent";
            this.btnCreateComponent.Size = new System.Drawing.Size(148, 23);
            this.btnCreateComponent.TabIndex = 13;
            this.btnCreateComponent.Text = "Create";
            this.btnCreateComponent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreateComponent.UseVisualStyleBackColor = true;
            this.btnCreateComponent.Click += new System.EventHandler(this.BtnCreateComponent_Click);
            // 
            // cboxTemplate
            // 
            this.cboxTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxTemplate.FormattingEnabled = true;
            this.cboxTemplate.Items.AddRange(new object[] {
            "Field",
            "Dataset"});
            this.cboxTemplate.Location = new System.Drawing.Point(110, 100);
            this.cboxTemplate.Name = "cboxTemplate";
            this.cboxTemplate.Size = new System.Drawing.Size(175, 21);
            this.cboxTemplate.TabIndex = 13;
            // 
            // txtWorkingFolder
            // 
            this.txtWorkingFolder.Location = new System.Drawing.Point(115, 23);
            this.txtWorkingFolder.Name = "txtWorkingFolder";
            this.txtWorkingFolder.Size = new System.Drawing.Size(320, 20);
            this.txtWorkingFolder.TabIndex = 10;
            this.txtWorkingFolder.TextChanged += new System.EventHandler(this.TxtWorkingFolder_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Control Location";
            // 
            // btnWorkingFolderSelector
            // 
            this.btnWorkingFolderSelector.Location = new System.Drawing.Point(466, 23);
            this.btnWorkingFolderSelector.Name = "btnWorkingFolderSelector";
            this.btnWorkingFolderSelector.Size = new System.Drawing.Size(27, 20);
            this.btnWorkingFolderSelector.TabIndex = 11;
            this.btnWorkingFolderSelector.Text = "...";
            this.btnWorkingFolderSelector.UseVisualStyleBackColor = true;
            this.btnWorkingFolderSelector.Click += new System.EventHandler(this.btnWorkingFolderSelector_Click);
            // 
            // linklblCreator
            // 
            this.linklblCreator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linklblCreator.Image = ((System.Drawing.Image)(resources.GetObject("linklblCreator.Image")));
            this.linklblCreator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linklblCreator.Location = new System.Drawing.Point(1246, 0);
            this.linklblCreator.Name = "linklblCreator";
            this.linklblCreator.Size = new System.Drawing.Size(160, 23);
            this.linklblCreator.TabIndex = 19;
            this.linklblCreator.TabStop = true;
            this.linklblCreator.Text = "by Danish (Power Maverick)";
            this.linklblCreator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linklblCreator.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinklblCreator_LinkClicked);
            // 
            // MainPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.linklblCreator);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MainPluginControl";
            this.Size = new System.Drawing.Size(1409, 781);
            this.OnCloseTool += new System.EventHandler(this.MainPluginControl_OnCloseTool);
            this.Load += new System.EventHandler(this.MainPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.gboxQuickAction.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRunning)).EndInit();
            this.grpBoxAuthProfileDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMRULocations)).EndInit();
            this.gboxCommandPrompt.ResumeLayout(false);
            this.grpBoxSolutionDetails.ResumeLayout(false);
            this.grpBoxSolutionDetails.PerformLayout();
            this.grpBoxComponentDetails.ResumeLayout(false);
            this.grpBoxComponentDetails.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.FolderBrowserDialog workingFolderBrowserDialog;
        private System.Windows.Forms.ToolStripButton tsbEditControl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tspCLIHelp;
        private System.Windows.Forms.ToolStripMenuItem tspmDownloadPowerAppsCLI;
        private System.Windows.Forms.ToolStripMenuItem tspmUpdatePowerAppsCLI;
        private System.Windows.Forms.ToolStripDropDownButton tspHelp;
        private System.Windows.Forms.ToolStripMenuItem tspmMSDocs;
        private System.Windows.Forms.ToolStripMenuItem tspmPCFLimitations;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tspGallery;
        private System.Windows.Forms.ToolStripMenuItem tspmPCFOverview;
        private System.Windows.Forms.ToolStripButton tspSampleControls;
        private System.Windows.Forms.ToolStripSplitButton tsbNewPCFMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmNewPCFBlank;
        private System.Windows.Forms.ToolStripMenuItem tsmNewPCFTemplate;
        private System.Windows.Forms.LinkLabel linklblCreator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tspSettings;
        private System.Windows.Forms.GroupBox gboxQuickAction;
        private System.Windows.Forms.Button btnBuildAndDeployAll;
        private System.Windows.Forms.Button btnBuildAllProjects;
        private System.Windows.Forms.Button btnBuildAndTest;
        private System.Windows.Forms.Label lblPCFCLIVersionMsg;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnDeploy;
        private System.Windows.Forms.GroupBox gboxCommandPrompt;
        private System.Windows.Forms.GroupBox grpBoxSolutionDetails;
        private System.Windows.Forms.Button btnCreateSolution;
        private System.Windows.Forms.TextBox txtPublisherPrefix;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPublisherName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSolutionName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grpBoxComponentDetails;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.CheckBox chkNoWatch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTestComponent;
        private System.Windows.Forms.TextBox txtControlName;
        private System.Windows.Forms.Button btnBuildComponent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOpenInVSCode;
        private System.Windows.Forms.Button btnCreateComponent;
        private System.Windows.Forms.ComboBox cboxTemplate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWorkingFolder;
        private System.Windows.Forms.Button btnWorkingFolderSelector;
        private ConsoleControl.ConsoleControl consoleControl;
        private System.Windows.Forms.Button btnClearConsole;
        private System.Windows.Forms.Label lblErrors;
        private System.Windows.Forms.TextBox txtSolutionVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtComponentVersion;
        private System.Windows.Forms.CheckBox chkIncrementComponentVersion;
        private System.Windows.Forms.Button btnBuildSolution;
        private System.Windows.Forms.CheckBox chkIncrementSolutionVersion;
        private System.Windows.Forms.CheckBox chkManagedSolution;
        private System.Windows.Forms.Button btnRefreshDetails;
        private System.Windows.Forms.Button btnOpenControlInExplorer;
        private System.Windows.Forms.ToolStripDropDownButton tspMoreLinks;
        private System.Windows.Forms.ToolStripMenuItem tspmDemos;
        private System.Windows.Forms.ToolStripMenuItem tspmBlogs;
        private System.Windows.Forms.ToolStripMenuItem tspmForums;
        private System.Windows.Forms.ToolStripMenuItem tspmIdeas;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblnpmVersionMsg;
        private System.Windows.Forms.ToolStripSplitButton tsbAuthProfile;
        private System.Windows.Forms.ToolStripMenuItem tsmCreateProfile;
        private System.Windows.Forms.ToolStripMenuItem tsmListProfiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Button btnQuickDeploy;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.LinkLabel linklblQuickDeployLearn;
        private System.Windows.Forms.Label lblCurrentProfile;
        private System.Windows.Forms.LinkLabel linklblPowerBiReport;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSolutionInitStatus;
        private System.Windows.Forms.Label lblControlInitStatus;
        private System.Windows.Forms.CheckBox chkUseExistingSolution;
        private System.Windows.Forms.ComboBox cboxSolutions;
        private System.Windows.Forms.DataGridView dgvMRULocations;
        private System.Windows.Forms.Button btnShowMRULocations;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.GroupBox grpBoxAuthProfileDetails;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnAddPreviewImage;
        private System.Windows.Forms.Button btnAddResxFile;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboxAdditionalPackages;
        private System.Windows.Forms.Label lblResxFileExists;
        private System.Windows.Forms.Label lblCssFileExists;
        private System.Windows.Forms.Label lblPreviewImageExists;
        private System.Windows.Forms.PictureBox picRunning;
    }
}
