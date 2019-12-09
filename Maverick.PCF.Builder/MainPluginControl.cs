using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using System.Diagnostics;
using System.IO;
using Microsoft.Crm.Sdk.Messages;
using XrmToolBox.Extensibility.Interfaces;
using System.Xml;
using Maverick.PCF.Builder.Forms;
using Maverick.PCF.Builder.DataObjects;
using Maverick.PCF.Builder.Helper;

namespace Maverick.PCF.Builder
{
    public partial class MainPluginControl : PluginControlBase, IGitHubPlugin, IHelpPlugin, IPayPalPlugin
    {
        private Settings pluginSettings;

        public string RepositoryName => "PCF-CustomControlBuilder";

        public string UserName => "Danz-maveRICK";

        public string HelpUrl => "https://github.com/Danz-maveRICK/PCF-CustomControlBuilder/blob/master/README.md";

        public string DonationDescription => "Keeps the ball rolling and motivates in making awesome tools.";

        public string EmailAccount => "danz@techgeek.co.in";

        #region Properties and Enums

        public PcfGallery SelectedTemplate { get; set; }
        public bool CommandPromptInitialied { get; set; }
        public bool StatusCheckExecution { get; set; }
        public bool BuildDeployExecution { get; set; }
        public bool ReloadDetails { get; set; }
        public string CurrentCommandOutput { get; set; }

        public enum ProcessingStatus
        {
            Succeeded,
            Failed,
            NeedReview,
            Running,
            Undetermined,
            Complete
        }

        #endregion

        #region Private Variables

        private BackgroundWorker _mainPluginLocalWorker;

        #endregion

        #region Helper Functions

        private void InitCommandLine()
        {
            consoleControl.StartProcess("cmd", $"/K \"{txtVSCmdPrompt.Text}\"");
        }

        private void RunCommandLine(params string[] commands)
        {
            CurrentCommandOutput = string.Empty;
            if (commands.Contains(Commands.Npm.RunBuild())
                || commands.Contains(Commands.Msbuild.Rebuild())
                || commands.Contains(Commands.Msbuild.BuildRelease())
                || commands.Contains(Commands.Msbuild.RebuildRelease())
                || commands.Contains(Commands.Npm.Start())
                || commands.Contains(Commands.Npm.StartWatch()))
            {
                StatusCheckExecution = true;
            }

            foreach (var cmd in commands)
            {
                consoleControl.WriteInput(cmd, Color.White, true);
            }

            consoleControl.InternalRichTextBox.ScrollToCaret();

        }

        #endregion

        #region Custom Functions

        private bool AreMainControlsPopulated()
        {
            lblErrors.Text = string.Empty;
            bool isWorkingFolderValid = true;
            bool isVSCmdPromptValid = true;

            isVSCmdPromptValid = IsVSCommandPromptLocationPopulated(true);
            isWorkingFolderValid = IsWorkingFolderPopulated(false);

            if (isWorkingFolderValid && isVSCmdPromptValid)
            {
                lblErrors.Text = string.Empty;
            }

            bool isValid = isWorkingFolderValid && isVSCmdPromptValid;

            return isValid;
        }

        private bool IsVSCommandPromptLocationPopulated(bool clearError)
        {
            if (clearError)
            {
                lblErrors.Text = string.Empty;
            }

            bool isValid = true;
            if (string.IsNullOrEmpty(txtVSCmdPrompt.Text))
            {
                lblErrors.Text += "\nVisual Studio Developer Command Prompt executable location is required.";
                isValid = false;
            }
            if (!string.IsNullOrEmpty(txtVSCmdPrompt.Text) && !txtVSCmdPrompt.Text.EndsWith("VsDevCmd.bat"))
            {
                lblErrors.Text += "\nSelect a proper Visual Studio Developer Command Prompt executable ending in \"VsDevCmd.bat\".";
                isValid = false;
            }

            if (isValid && clearError)
            {
                lblErrors.Text = string.Empty;
            }

            return isValid;
        }

        private bool IsWorkingFolderPopulated(bool clearError)
        {
            if (clearError)
            {
                lblErrors.Text = string.Empty;
            }

            bool isValid = true;
            if (string.IsNullOrEmpty(txtWorkingFolder.Text))
            {
                lblErrors.Text = "Working folder is required.";
                isValid = false;
            }

            if (isValid && clearError)
            {
                lblErrors.Text = string.Empty;
            }

            return isValid;
        }

        private bool AreSolutionDetailsPopulated()
        {
            bool isValid = true;
            lblErrors.Text = string.Empty;
            if (string.IsNullOrEmpty(txtSolutionName.Text))
            {
                lblErrors.Text = "Deployment Folder Name is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(txtPublisherName.Text))
            {
                lblErrors.Text += "\nPublisher Name is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(txtPublisherPrefix.Text))
            {
                lblErrors.Text += "\nPublisher Prefix is required.";
                isValid = false;
            }

            return isValid;
        }

        private void Routine_NewComponent()
        {
            txtNamespace.Clear();
            txtControlName.Clear();
            cboxTemplate.SelectedIndex = -1;
            txtComponentVersion.Clear();

            txtSolutionName.Clear();
            txtPublisherName.Clear();
            txtPublisherPrefix.Clear();
            txtSolutionVersion.Clear();

            cboxTemplate.Enabled = true;
            btnCreateComponent.Enabled = true;
            btnCreateSolution.Enabled = true;
            btnRefreshDetails.Enabled = false;
        }

        private void Routine_EditComponent()
        {
            btnRefreshDetails.Enabled = true;

            if (!string.IsNullOrEmpty(txtControlName.Text))
            {
                cboxTemplate.Text = "N/A";
                cboxTemplate.Enabled = false;
                btnCreateComponent.Enabled = false;
            }

            if (!string.IsNullOrEmpty(txtSolutionName.Text))
            {
                btnCreateSolution.Enabled = false;
            }
        }

        private void IdentifyControlDetails()
        {
            try
            {
                var mainDirs = Directory.GetDirectories(txtWorkingFolder.Text);
                if (mainDirs != null && mainDirs.Count() > 0)
                {
                    // Check if .pcfproj does not already exists
                    var filteredPcfProject = mainDirs.ToList().Where(l => (!l.ToLower().EndsWith(".pcfproj")));
                    if (filteredPcfProject != null && filteredPcfProject.Count() > 0)
                    {
                        var pcfDirs = Directory.GetDirectories(txtWorkingFolder.Text);
                        if (pcfDirs != null && pcfDirs.Count() > 0)
                        {
                            var filteredPcfDirs = pcfDirs.ToList().Where(l => (!l.ToLower().EndsWith("node_modules")) && (!l.ToLower().EndsWith("obj")) && (!l.ToLower().EndsWith("out")));
                            foreach (var item in filteredPcfDirs)
                            {
                                var indexExists = File.Exists(item + "\\" + "index.ts");
                                if (indexExists)
                                {
                                    txtControlName.Text = Path.GetFileName(item);
                                    var controlManifestFile = item + "\\" + "ControlManifest.Input.xml";
                                    XmlReader xmlReader = XmlReader.Create(controlManifestFile);
                                    while (xmlReader.Read())
                                    {
                                        if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "control"))
                                        {
                                            var control_namespace = xmlReader.GetAttribute("namespace");
                                            txtNamespace.Text = control_namespace;

                                            var control_version = xmlReader.GetAttribute("version");
                                            txtComponentVersion.Text = control_version;
                                        }
                                    }
                                    xmlReader.Close();
                                    break;
                                }

                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(txtControlName.Text))
                {
                    // Check if .cdsproj does not already exists
                    var controlDirs = Directory.GetDirectories(txtWorkingFolder.Text + "\\" + txtControlName.Text);
                    if (controlDirs != null && controlDirs.Count() > 0)
                    {
                        var filteredControlDirs = controlDirs.ToList().Where(l => (!l.ToLower().EndsWith("generated")));
                        foreach (var item in filteredControlDirs)
                        {
                            var cdsprojName = Path.GetFileName(item);
                            var cdsprojExists = File.Exists(item + "\\" + cdsprojName + ".cdsproj");
                            if (cdsprojExists)
                            {
                                txtSolutionName.Text = cdsprojName;
                                var solutionFile = item + "\\Other\\Solution.xml";
                                XmlReader xmlReader = XmlReader.Create(solutionFile);
                                while (xmlReader.Read())
                                {
                                    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "UniqueName"))
                                    {
                                        var publisher = xmlReader.ReadElementContentAsString();
                                        txtPublisherName.Text = publisher;
                                    }
                                    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "CustomizationPrefix"))
                                    {
                                        var prefix = xmlReader.ReadElementContentAsString();
                                        txtPublisherPrefix.Text = prefix;
                                    }
                                    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Version"))
                                    {
                                        var version = xmlReader.ReadElementContentAsString();
                                        txtSolutionVersion.Text = version;
                                    }
                                }
                                xmlReader.Close();
                                break;
                            }
                        }
                    }
                }

                Routine_EditComponent();
            }
            catch (DirectoryNotFoundException dnex)
            {
                MessageBox.Show("Invalid directory. Could not retrieve existing PCF project and CDS solution project details.");
                Routine_NewComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while retrieving existing project details. Please report an issue on GitHub page.");
                Routine_NewComponent();
            }
        }

        private bool IsPcfProjectAlreadyExists(string controlName)
        {
            var pcfprojAlreadyExists = File.Exists(txtWorkingFolder.Text + "\\" + controlName + ".pcfproj");

            if (pcfprojAlreadyExists)
            {
                return true;
            }

            return false;
        }

        private void CheckPacVersion(object worker, DoWorkEventArgs args)
        {
            string[] commands = new string[] { Commands.Pac.Check() };
            var output = CommandLineHelper.RunCommand(commands);

            string currentPacVersion = string.Empty;
            string latestPacVersion = string.Empty;

            if (!string.IsNullOrEmpty(output) && output.ToLower().Contains("version"))
            {
                currentPacVersion = output.Substring(output.IndexOf("Version: ") + 8, output.IndexOf("+", output.IndexOf("Version: ") + 8) - (output.IndexOf("Version: ") + 8));

                commands = new string[] { Commands.Pac.Use() };
                var output2 = CommandLineHelper.RunCommand(commands);
                if (!string.IsNullOrEmpty(output2) && output2.ToLower().Contains("latest"))
                {
                    var tempString = output2.Split('\r').Where(a => a.ToLower().Contains("(latest)")).FirstOrDefault().Trim();
                    latestPacVersion = tempString.Substring(0, tempString.IndexOf(" (Latest)"));

                    if (!currentPacVersion.Trim().Equals(latestPacVersion.Trim()))
                    {
                        if (DialogResult.Yes == MessageBox.Show("New version of PCF CLI is available. Do you want to update it now?", "PCF CLI Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            string pacUpdateCLI = Commands.Pac.InstallLatest();
                            RunCommandLine(pacUpdateCLI);
                            currentPacVersion = latestPacVersion;
                        }
                    }
                }

                lblPCFCLIVersionMsg.Text = "PCF CLI Version: " + currentPacVersion.Trim();
            }
            else
            {
                lblPCFCLIVersionMsg.Text = "PCF CLI Not Detected";
            }
        }

        private void CheckNpmVersion(object worker, DoWorkEventArgs args)
        {
            string[] commands = new string[] { Commands.Npm.Version() };
            var output = CommandLineHelper.RunCommand(commands);

            string currentNpmVersion = string.Empty;

            if (!string.IsNullOrEmpty(output) && output.ToLower().Contains("npm: "))
            {
                currentNpmVersion = output.Substring(output.IndexOf("npm: ") + 6, output.IndexOf(",", output.IndexOf("npm: ") + 6) - (output.IndexOf("npm: ") + 6) - 1);
                lblnpmVersionMsg.Text = "npm Version: " + currentNpmVersion.Trim();
            }
            else
            {
                lblnpmVersionMsg.Text = "npm Not Detected";
                ShowInfoNotification("npm not detected on this machine. Please download it.", new Uri("https://nodejs.org/en/"));
            }
            
        }

        private void IncrementComponentVersion()
        {
            if (chkIncrementComponentVersion.Checked)
            {
                try
                {
                    var mainDirs = Directory.GetDirectories(txtWorkingFolder.Text);
                    var currentversion = Version.Parse(txtComponentVersion.Text);
                    var newversion = new Version(currentversion.Major, currentversion.Minor, currentversion.Build + 1);
                    if (mainDirs != null && mainDirs.Count() > 0)
                    {
                        // Check if .pcfproj does not already exists
                        var filteredPcfProject = mainDirs.ToList().Where(l => (!l.ToLower().EndsWith(".pcfproj")));
                        if (filteredPcfProject != null && filteredPcfProject.Count() > 0)
                        {
                            var pcfDirs = Directory.GetDirectories(txtWorkingFolder.Text);
                            if (pcfDirs != null && pcfDirs.Count() > 0)
                            {
                                var filteredPcfDirs = pcfDirs.ToList().Where(l => (!l.ToLower().EndsWith("node_modules")) && (!l.ToLower().EndsWith("obj")) && (!l.ToLower().EndsWith("out")));
                                foreach (var item in filteredPcfDirs)
                                {
                                    var indexExists = File.Exists(item + "\\" + "index.ts");
                                    if (indexExists)
                                    {
                                        txtControlName.Text = Path.GetFileName(item);
                                        var controlManifestFile = item + "\\" + "ControlManifest.Input.xml";

                                        XmlDocument xmlDoc = new XmlDocument();
                                        xmlDoc.Load(controlManifestFile);
                                        XmlNode node = xmlDoc.SelectSingleNode("/manifest/control");
                                        node.Attributes["version"].Value = newversion.ToString();
                                        xmlDoc.Save(controlManifestFile);
                                        txtComponentVersion.Text = newversion.ToString();

                                        break;
                                    }

                                }
                            }
                        }
                    }
                }
                catch (DirectoryNotFoundException dnex)
                {
                    MessageBox.Show("Invalid directory. Could not retrieve existing PCF project.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while updating the version. Please report an issue on GitHub page.");
                }
            }
        }

        private void IncrementSolutionVersion()
        {
            if (chkIncrementSolutionVersion.Checked)
            {
                try
                {
                    var mainDirs = Directory.GetDirectories(txtWorkingFolder.Text);
                    var currentversion = Version.Parse(txtSolutionVersion.Text);
                    var newversion = new Version(currentversion.Major, currentversion.Minor, currentversion.Build + 1);

                    if (!string.IsNullOrEmpty(txtControlName.Text))
                    {
                        // Check if .cdsproj does not already exists
                        var controlDirs = Directory.GetDirectories(txtWorkingFolder.Text + "\\" + txtControlName.Text);
                        if (controlDirs != null && controlDirs.Count() > 0)
                        {
                            var filteredControlDirs = controlDirs.ToList().Where(l => (!l.ToLower().EndsWith("generated")));
                            foreach (var item in filteredControlDirs)
                            {
                                var cdsprojName = Path.GetFileName(item);
                                var cdsprojExists = File.Exists(item + "\\" + cdsprojName + ".cdsproj");
                                if (cdsprojExists)
                                {
                                    txtSolutionName.Text = cdsprojName;
                                    var solutionFile = item + "\\Other\\Solution.xml";

                                    XmlDocument xmlDoc = new XmlDocument();
                                    xmlDoc.Load(solutionFile);
                                    XmlNode node = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/Version");
                                    node.InnerText = newversion.ToString();
                                    xmlDoc.Save(solutionFile);
                                    txtSolutionVersion.Text = newversion.ToString();

                                    break;
                                }
                            }
                        }
                    }
                }
                catch (DirectoryNotFoundException dnex)
                {
                    MessageBox.Show("Invalid directory. Could not retrieve existing PCF project.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while updating the version. Please report an issue on GitHub page.");
                }
            }
        }

        private bool SaveSettings()
        {
            var ok = true;

            if (!string.IsNullOrEmpty(txtWorkingFolder.Text) && !(pluginSettings.WorkingDirectoryLocation.Equals(txtWorkingFolder.Text)))
            {
                var result = MessageBox.Show("Do you want to save your folder location?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    ok = false;
                }
                if (result == DialogResult.Yes)
                {
                    pluginSettings.WorkingDirectoryLocation = txtWorkingFolder.Text;
                }
            }

            SettingsManager.Instance.Save(GetType(), pluginSettings);

            return ok;
        }

        private void DeploySolution()
        {
            string solutionFileLocation = chkManagedSolution.Checked ? $"{txtWorkingFolder.Text}\\{txtControlName.Text}\\{txtSolutionName.Text}\\bin\\release\\{txtSolutionName.Text}.zip" : $"{txtWorkingFolder.Text}\\{txtControlName.Text}\\{txtSolutionName.Text}\\bin\\debug\\{txtSolutionName.Text}.zip";

            string deploymentFolder = solutionFileLocation;
            byte[] fileBytes = File.ReadAllBytes(deploymentFolder);

            ImportSolutionInD365CE(fileBytes, txtSolutionName.Text);
        }

        private void ImportSolutionInD365CE(byte[] fileBytes, string deploymentFolderName)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Importing solution package to CDS",
                Work = (worker, args) =>
                {
                    ImportSolutionRequest impSolReq = new ImportSolutionRequest()
                    {
                        CustomizationFile = fileBytes
                    };
                    args.Result = Service.Execute(impSolReq);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var result = args.Result as OrganizationResponse;
                        if (result != null)
                        {
                            PublishAll(deploymentFolderName);
                        }
                    }

                }
            });
        }

        private void PublishAll(string deploymentFolderName)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Publishing",
                Work = (worker, args) =>
                {
                    var publishRequest = new PublishAllXmlRequest();
                    args.Result = Service.Execute(publishRequest);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var result = args.Result as OrganizationResponse;
                        if (result != null)
                        {
                            MessageBox.Show($"Solution {deploymentFolderName} deployed to Dynamics 365 CE");
                        }
                    }

                }
            });
        }

        private void SetProcessingStatus(ProcessingStatus status)
        {
            switch (status)
            {
                case ProcessingStatus.Succeeded:
                    lblStatus.Text = "Succeeded";
                    lblStatus.ForeColor = Color.LimeGreen;
                    break;
                case ProcessingStatus.Failed:
                    lblStatus.Text = "Failed";
                    lblStatus.ForeColor = Color.DarkRed;
                    break;
                case ProcessingStatus.NeedReview:
                    lblStatus.Text = "Need Review";
                    lblStatus.ForeColor = Color.DarkMagenta;
                    break;
                case ProcessingStatus.Running:
                    lblStatus.Text = "Running";
                    lblStatus.ForeColor = Color.DarkOrange;
                    break;
                case ProcessingStatus.Undetermined:
                    lblStatus.Text = "Undetermined";
                    lblStatus.ForeColor = Color.Gray;
                    break;
                case ProcessingStatus.Complete:
                default:
                    lblStatus.Text = string.Empty;
                    lblStatus.ForeColor = Color.Black;
                    break;
            }
        }

        #endregion

        public MainPluginControl()
        {
            InitializeComponent();
        }

        private void MainPluginControl_Load(object sender, EventArgs e)
        {
            //ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out pluginSettings))
            {
                pluginSettings = new Settings();
                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                txtVSCmdPrompt.Text = pluginSettings.VisualStudioCommandPromptPath;
                txtWorkingFolder.Text = pluginSettings.WorkingDirectoryLocation;
                LogInfo("Settings found and loaded");
            }

            _mainPluginLocalWorker = new BackgroundWorker();
            _mainPluginLocalWorker.DoWork += CheckPacVersion;
            _mainPluginLocalWorker.DoWork += CheckNpmVersion;
            _mainPluginLocalWorker.RunWorkerAsync();

            StatusCheckExecution = false;
            BuildDeployExecution = false;
            CurrentCommandOutput = string.Empty;

            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                CommandPromptInitialied = true;
                InitCommandLine();
                IdentifyControlDetails();
                Routine_EditComponent();
            }
            else
            {
                CommandPromptInitialied = false;
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }


        private void btnWorkingFolderSelector_Click(object sender, EventArgs e)
        {
            if (workingFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtWorkingFolder.Text = workingFolderBrowserDialog.SelectedPath;
            }
        }

        private void tspmDownloadPowerAppsCLI_Click(object sender, EventArgs e)
        {
            Process.Start("https://aka.ms/PowerAppsCLI");
        }

        private void tspmUpdatePowerAppsCLI_Click(object sender, EventArgs e)
        {
            string pacUpdateCLI = Commands.Pac.InstallLatest();
            RunCommandLine(pacUpdateCLI);

            _mainPluginLocalWorker = new BackgroundWorker();
            _mainPluginLocalWorker.DoWork += CheckPacVersion;
            _mainPluginLocalWorker.RunWorkerAsync();
        }

        private void tspmMSDocs_Click(object sender, EventArgs e)
        {
            Process.Start("https://docs.microsoft.com/en-us/powerapps/developer/component-framework/create-custom-controls-using-pcf");
        }

        private void tspmPCFLimitations_Click(object sender, EventArgs e)
        {
            Process.Start("https://docs.microsoft.com/en-us/powerapps/developer/component-framework/limitations");
        }

        private void tspGallery_Click(object sender, EventArgs e)
        {
            Process.Start("https://pcf.gallery/");
        }

        /*
        private void cboxInfoSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            webBrowserPCFInfo.Url = new Uri(GetBrowserUrl());
        }
        */

        private void tspmPCFOverview_Click(object sender, EventArgs e)
        {
            Process.Start("https://docs.microsoft.com/en-us/powerapps/developer/component-framework/overview");
        }

        private void tspSampleControls_Click(object sender, EventArgs e)
        {
            Process.Start("https://aka.ms/PCFSampleControls");
        }

        private void TsmNewPCFBlank_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                if (!CommandPromptInitialied)
                {
                    InitCommandLine();
                }
                Routine_NewComponent();
            }
        }

        private void TsmNewPCFTemplate_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                if (!CommandPromptInitialied)
                {
                    InitCommandLine();
                }

                var ctrlTemplates = new Templates(txtWorkingFolder.Text);
                ctrlTemplates.StartPosition = FormStartPosition.CenterScreen;
                ctrlTemplates.ParentControl = this;
                ctrlTemplates.ShowDialog();

                if (SelectedTemplate == null)
                {
                    return;
                }

                IdentifyControlDetails();

                if (string.IsNullOrEmpty(txtControlName.Text))
                {
                    MessageBox.Show("Not able to parse the control from the template.");

                    Routine_NewComponent();

                    return;
                }

                Routine_EditComponent();

                // Run npm install - as the solution is downloaded form GitHub
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtControlName.Text}");
                string npmCommand = Commands.Npm.Install();
                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {npmCommand} && exit");

                RunCommandLine(cdWorkingDir, npmCommand);
            }
        }

        private void tsbEditControl_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                if (!CommandPromptInitialied)
                {
                    InitCommandLine();
                }

                IdentifyControlDetails();
                Routine_EditComponent();
            }
        }

        private void TsbNewPCFMenu_ButtonClick(object sender, EventArgs e)
        {
            tsbNewPCFMenu.ShowDropDown();
        }

        public override void ClosingPlugin(PluginCloseInfo info)
        {
            if (!SaveSettings())
            {
                info.Cancel = true;
                return;
            }
            consoleControl.Dispose();
        }

        private void LinklblCreator_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/DanzMaverick");
        }

        private void TspSettings_Click(object sender, EventArgs e)
        {
            var ctrlSettingsForm = new SettingsForm(pluginSettings);
            ctrlSettingsForm.StartPosition = FormStartPosition.CenterScreen;
            ctrlSettingsForm.ShowDialog();
        }

        private void TxtWorkingFolder_TextChanged(object sender, EventArgs e)
        {
            pluginSettings.WorkingDirectoryLocation = txtWorkingFolder.Text;

            //if (txtWorkingFolder.Text.Contains(" "))
            //{
            //    ShowInfoNotification("There is a known issue with a space in the Control location while adding it to the Solution", new Uri("https://github.com/Danz-maveRICK/PCF-CustomControlBuilder/issues/6"));
            //}
            //else
            //{
            //    HideNotification();
            //}
        }

        private void TxtVSPromptLoc_TextChanged(object sender, EventArgs e)
        {
            pluginSettings.VisualStudioCommandPromptPath = txtVSCmdPrompt.Text;
        }

        private void BtnBuildAndTest_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                IncrementComponentVersion();
                string controlName = txtControlName.Text;
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{controlName}");
                string npmBuildCommand = Commands.Npm.RunBuild();
                string npmTestCommand = chkNoWatch.Checked ? Commands.Npm.Start() : Commands.Npm.StartWatch();

                RunCommandLine(cdWorkingDir, npmBuildCommand, npmTestCommand);
            }
        }

        private void BtnBuildAllProjects_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            lblErrors.Text = string.Empty;
            if (string.IsNullOrEmpty(txtSolutionName.Text))
            {
                lblErrors.Text = "Deployment Folder Name is required.";
            }

            if (areMainControlsValid && !string.IsNullOrEmpty(txtSolutionName.Text))
            {
                IncrementComponentVersion();
                IncrementSolutionVersion();

                string controlName = txtControlName.Text;
                string deployFolderName = txtSolutionName.Text;

                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{controlName}");
                string npmBuildCommand = Commands.Npm.RunBuild();

                string cdDeploymentFolder = Commands.Cmd.ChangeDirectory(deployFolderName);
                string msbuild_restore = Commands.Msbuild.Restore();
                string msbuild_rebuild = chkManagedSolution.Checked ? Commands.Msbuild.RebuildRelease() : Commands.Msbuild.Rebuild();

                RunCommandLine(cdWorkingDir, npmBuildCommand, cdDeploymentFolder, msbuild_restore, msbuild_rebuild);
            }
        }

        private void BtnBuildAndDeployAll_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            lblErrors.Text = string.Empty;
            if (string.IsNullOrEmpty(txtSolutionName.Text))
            {
                lblErrors.Text = "Deployment Folder Name is required.";
            }

            if (areMainControlsValid && !string.IsNullOrEmpty(txtSolutionName.Text))
            {
                IncrementComponentVersion();
                IncrementSolutionVersion();

                string controlName = txtControlName.Text;
                string deployFolderName = txtSolutionName.Text;

                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{controlName}");
                string npmBuildCommand = Commands.Npm.RunBuild();

                string cdDeploymentFolder = Commands.Cmd.ChangeDirectory(deployFolderName);
                string msbuild_rebuild = chkManagedSolution.Checked ? Commands.Msbuild.RebuildRelease() : Commands.Msbuild.Rebuild();

                BuildDeployExecution = true;
                RunCommandLine(cdWorkingDir, npmBuildCommand, cdDeploymentFolder, msbuild_rebuild);
            }
        }

        private void BtnCreateComponent_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            lblErrors.Text = string.Empty;
            if (string.IsNullOrEmpty(txtNamespace.Text))
            {
                lblErrors.Text = "Namespace is required.";
            }
            if (string.IsNullOrEmpty(txtControlName.Text))
            {
                lblErrors.Text += "\nControl Name is required.";
            }
            if (cboxTemplate.SelectedIndex == -1)
            {
                lblErrors.Text += "\nTemplate selection is required.";
            }

            if (areMainControlsValid && !string.IsNullOrEmpty(txtNamespace.Text) && !string.IsNullOrEmpty(txtControlName.Text) && cboxTemplate.SelectedIndex >= 0)
            {
                // Check if .pcfproj does not already exists
                var folderName = Path.GetFileName(txtWorkingFolder.Text);
                if (IsPcfProjectAlreadyExists(folderName))
                {
                    MessageBox.Show("PCF Project already exists in this directory. Please select another folder location or use \"Edit existing PCF Control\" page.");
                    return;
                }

                lblErrors.Text = string.Empty;

                string cdWorkingDir = Commands.Cmd.ChangeDirectory(txtWorkingFolder.Text);
                string pacCommand = Commands.Pac.PcfInit(txtNamespace.Text, txtControlName.Text, cboxTemplate.SelectedItem.ToString());
                string npmCommand = Commands.Npm.Install();

                ReloadDetails = true;
                RunCommandLine(cdWorkingDir, pacCommand, npmCommand);
            }
        }

        private void BtnOpenInVSCode_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                string vscodeopen = $"code \"{txtWorkingFolder.Text}\\{txtControlName.Text}\"";

                RunCommandLine(vscodeopen);
            }
        }

        private void BtnBuildComponent_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                IncrementComponentVersion();
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtControlName.Text}");
                string npmCommand = Commands.Npm.RunBuild();

                RunCommandLine(cdWorkingDir, npmCommand);
            }
        }

        private void BtnTestComponent_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtControlName.Text}");
                string npmCommand = chkNoWatch.Checked ? Commands.Npm.Start() : Commands.Npm.StartWatch();

                //Process.Start("cmd", $"/K \"{txtVSCmdPrompt.Text}\" && {cdWorkingDir} && {npmCommand}");
                RunCommandLine(cdWorkingDir, npmCommand);
            }
        }

        private void BtnTerminateProcess_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("This will stop the running process and reset the console.  Do you want to proceed?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                //consoleControl.StopProcess();
                //consoleControl.ProcessInterface.Process.Kill();
                consoleControl.ProcessInterface.Process.Close();
                //consoleControl.ProcessInterface.Process.CancelOutputRead();
                try
                {
                    InitCommandLine();
                }
                catch (Exception)
                {
                    // This is intentional error throwing
                    consoleControl.ClearOutput();
                    RunCommandLine("Console Reset. Ready for further commands.");
                    SetProcessingStatus(ProcessingStatus.Complete);
                }
            }
        }

        private void BtnBuildSolution_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            lblErrors.Text = string.Empty;
            if (string.IsNullOrEmpty(txtSolutionName.Text))
            {
                lblErrors.Text = "Solution Name is required.";
            }

            if (areMainControlsValid && !string.IsNullOrEmpty(txtSolutionName.Text))
            {
                IncrementSolutionVersion();

                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtControlName.Text}");

                string cdDeploymentFolder = Commands.Cmd.ChangeDirectory(txtSolutionName.Text);
                string msbuild_restore = Commands.Msbuild.Restore();
                string msbuild_rebuild = chkManagedSolution.Checked ? Commands.Msbuild.RebuildRelease() : Commands.Msbuild.Rebuild();

                RunCommandLine(cdWorkingDir, cdDeploymentFolder, msbuild_restore, msbuild_rebuild);
            }
        }

        private void BtnCreateSolution_Click(object sender, EventArgs e)
        {
            var isValid = AreSolutionDetailsPopulated();

            if (isValid)
            {
                lblErrors.Text = string.Empty;

                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtControlName.Text}");
                string mkdirDeploymentFolder = Commands.Cmd.MakeDirectory(txtSolutionName.Text);
                string cdDeploymentFolder = Commands.Cmd.ChangeDirectory(txtSolutionName.Text);
                string pacCommand_CreateSolution = Commands.Pac.SolutionInit(txtPublisherName.Text, txtPublisherPrefix.Text);
                //string pacCommand_AddComponent = Commands.Pac.SolutionAddReference(txtWorkingFolder.Text);
                //string msbuild_restore = Commands.Msbuild.Restore();
                //string msbuild = Commands.Msbuild.Build();

                ReloadDetails = true;
                RunCommandLine(cdWorkingDir, mkdirDeploymentFolder, cdDeploymentFolder, pacCommand_CreateSolution);
            }
        }

        private void BtnAddComponentToSolution_Click(object sender, EventArgs e)
        {
            var isValid = AreSolutionDetailsPopulated();

            if (isValid)
            {
                lblErrors.Text = string.Empty;

                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtControlName.Text}");
                //string mkdirDeploymentFolder = Commands.Cmd.MakeDirectory(txtSolutionName.Text);
                string cdDeploymentFolder = Commands.Cmd.ChangeDirectory(txtSolutionName.Text);
                //string pacCommand_CreateSolution = Commands.Pac.SolutionInit(txtPublisherName.Text, txtPublisherPrefix.Text);
                string pacCommand_AddComponent = Commands.Pac.SolutionAddReference(txtWorkingFolder.Text);
                //string msbuild_restore = Commands.Msbuild.Restore();
                //string msbuild = Commands.Msbuild.Build();

                RunCommandLine(cdWorkingDir, cdDeploymentFolder, pacCommand_AddComponent);
            }
        }

        private void BtnRefreshDetails_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                IdentifyControlDetails();
                Routine_EditComponent();
            }
        }

        private void BtnVSCmdPrompt_Click(object sender, EventArgs e)
        {
            if (selectVSDevFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtVSCmdPrompt.Text = selectVSDevFileDialog.FileName;
                pluginSettings.VisualStudioCommandPromptPath = txtVSCmdPrompt.Text;
                //VisualStudioBatchFilePath = txtVSCmdPrompt.Text;
            }
        }

        private void BtnOpenControlInExplorer_Click(object sender, EventArgs e)
        {
            var isValid = IsWorkingFolderPopulated(true);

            if (isValid)
            {
                Process.Start("explorer.exe", txtWorkingFolder.Text);
            }
        }

        private void ConsoleControl_OnConsoleOutput(object sender, ConsoleControl.ConsoleEventArgs args)
        {
            SetProcessingStatus(ProcessingStatus.Running);
            StatusCheckExecution = true;

            CurrentCommandOutput += args.Content;

            // This indicates end of execution
            if (args.Content.Contains("C:\\") && args.Content.EndsWith(">"))
            {
                SetProcessingStatus(ProcessingStatus.Complete);

                if (StatusCheckExecution)
                {
                    if (CurrentCommandOutput.ToLower().Contains("succeeded"))
                    {
                        SetProcessingStatus(ProcessingStatus.Succeeded);
                    }
                    if (CurrentCommandOutput.ToLower().Contains("failed"))
                    {
                        if (CurrentCommandOutput.ToLower().Contains("npm install"))
                        {
                            SetProcessingStatus(ProcessingStatus.NeedReview);
                        }
                        else
                        {
                            SetProcessingStatus(ProcessingStatus.Failed);
                        }
                    }

                    StatusCheckExecution = false;
                }

                if (BuildDeployExecution && lblStatus.Text.ToLower().Equals("succeeded") && CurrentCommandOutput.ToLower().Contains("done building project"))
                {
                    // The ExecuteMethod method handles connecting to an
                    // organization if XrmToolBox is not yet connected
                    ExecuteMethod(DeploySolution);

                    // Reset
                    BuildDeployExecution = false;
                }

                if (ReloadDetails
                    && (CurrentCommandOutput.ToLower().Contains("the powerapps component framework project was successfully created")
                        || CurrentCommandOutput.ToLower().Contains("cds solution files were successfully created")))
                {
                    IdentifyControlDetails();

                    // Reset
                    ReloadDetails = false;
                }


                //if (async)
                //{
                //    WorkAsync(new WorkAsyncInfo
                //    {
                //        Message = $"Running {commandsToShow} commands. Please wait.",
                //        Work = (worker, args) =>
                //        {
                //            var output = CommandLineHelper.RunCommand(VisualStudioBatchFilePath, commands);
                //            args.Result = output;
                //        },
                //        PostWorkCallBack = (args) =>
                //        {
                //            //txtCommandPrompt.Clear();
                //            //txtCommandPrompt.AppendText((string)args.Result);
                //            //txtCommandPrompt.ScrollToCaret();

                //            //if (commands.Contains(Commands.Npm.RunBuild()) || commands.Contains(Commands.Msbuild.Rebuild()))
                //            //{
                //            //    lblBuildStatus.Text = "Undetermined";
                //            //    lblBuildStatus.ForeColor = Color.Gray;

                //            //    if (((string)args.Result).ToLower().Contains("succeeded"))
                //            //    {
                //            //        lblBuildStatus.Text = "Succeeded";
                //            //        lblBuildStatus.ForeColor = Color.LimeGreen;
                //            //    }

                //            //    if (((string)args.Result).ToLower().Contains("failed"))
                //            //    {
                //            //        lblBuildStatus.Text = "Failed";
                //            //        lblBuildStatus.ForeColor = Color.DarkRed;
                //            //    }
                //            //}

                //            //if (commandsToShow.Equals("npmBuild, msRestore, msRebuild") && ((string)args.Result).ToLower().Contains("succeeded"))
                //            //{
                //            //    // The ExecuteMethod method handles connecting to an
                //            //    // organization if XrmToolBox is not yet connected
                //            //    ExecuteMethod(DeployExistingCustomControl);
                //            //}
                //        }
                //    });
                //}
                //else
                //{
                //    //CommandLineHelper.RunCommand(VisualStudioBatchFilePath, commands);
                //}
            }

            consoleControl.InternalRichTextBox.ScrollToCaret();
        }

        private void BtnDeploy_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            lblErrors.Text = string.Empty;
            if (string.IsNullOrEmpty(txtSolutionName.Text))
            {
                lblErrors.Text = "Deployment Folder Name is required.";
            }

            if (areMainControlsValid && !string.IsNullOrEmpty(txtSolutionName.Text))
            {
                // The ExecuteMethod method handles connecting to an
                // organization if XrmToolBox is not yet connected
                ExecuteMethod(DeploySolution);
            }
        }

        private void TspmDemos_Click(object sender, EventArgs e)
        {
            Process.Start("https://aka.ms/PCFDemos");
        }

        private void TspmBlogs_Click(object sender, EventArgs e)
        {
            Process.Start("https://aka.ms/PCFBlog");
        }

        private void TspmForums_Click(object sender, EventArgs e)
        {
            Process.Start("https://aka.ms/PCFForum");
        }

        private void TspmIdeas_Click(object sender, EventArgs e)
        {
            Process.Start("https://aka.ms/PCFIdeas");
        }

        private void LblStatus_TextChanged(object sender, EventArgs e)
        {
            if (lblStatus.Text.ToLower().Equals("running"))
            {
                btnTerminateProcess.Enabled = true;
            }
            else
            {
                btnTerminateProcess.Enabled = false;
            }
        }

        /*
        private void BtnTestWithWatch_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder1.Text}\\{txtExistsControlName.Text}");
                string npmCommand = Commands.Npm.StartWatch();

                // Using RunCommandHelper is causing issues.
                Process.Start("cmd", $"/K \"{VisualStudioBatchFilePath}\" && {cdWorkingDir} && {npmCommand}");
            }
        }

        private void BtnTestNewWithWatch_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder1.Text}\\{txtControlName.Text}");
                string npmCommand = Commands.Npm.StartWatch();

                // Using RunCommandHelper is causing issues.
                Process.Start("cmd", $"/K \"{VisualStudioBatchFilePath}\" && {cdWorkingDir} && {npmCommand}");
            }
        }

        private void LinklblInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(GetBrowserUrl());
        }
        */
    }
}