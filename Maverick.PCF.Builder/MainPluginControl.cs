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

        #region Properties

        public PcfGallery SelectedTemplate { get; set; }

        #endregion

        #region Internal Properties

        internal string VisualStudioBatchFilePath { get; set; }

        #endregion

        #region Custom Functions

        private void DeployNewCustomControl()
        {
            string deploymentFolder = $"{txtWorkingFolder.Text}\\{txtControlName.Text}\\{txtDeploymentFolder.Text}\\bin\\debug\\{txtDeploymentFolder.Text}.zip";
            byte[] fileBytes = File.ReadAllBytes(deploymentFolder);

            ImportSolutionInD365CE(fileBytes, txtDeploymentFolder.Text);
        }

        private void DeployExistingCustomControl()
        {
            string deploymentFolder = $"{txtWorkingFolder.Text}\\{txtExistsControlName.Text}\\{txtExistsDeployFolderName.Text}\\bin\\debug\\{txtExistsDeployFolderName.Text}.zip";
            byte[] fileBytes = File.ReadAllBytes(deploymentFolder);

            ImportSolutionInD365CE(fileBytes, txtExistsDeployFolderName.Text);
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

        private bool AreMainControlsPopulated()
        {
            lblErrors.Text = string.Empty;
            bool isWorkingFolderValid = true;
            bool isVSPromptLocationValid = true;

            isWorkingFolderValid = IsWorkingFolderPopulated(false);
            isVSPromptLocationValid = IsVSCommandPromptLocationPopulated(false);

            if (isWorkingFolderValid && isVSPromptLocationValid)
            {
                lblErrors.Text = string.Empty;
            }

            bool isValid = isWorkingFolderValid && isVSPromptLocationValid;

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

        private bool IsVSCommandPromptLocationPopulated(bool clearError)
        {
            if (clearError)
            {
                lblErrors.Text = string.Empty;
            }

            bool isValid = true;
            if (string.IsNullOrEmpty(txtVSPromptLoc.Text))
            {
                lblErrors.Text += "\nVisual Studio Developer Command Prompt executable location is required.";
                isValid = false;
            }
            if (!string.IsNullOrEmpty(txtVSPromptLoc.Text) && !txtVSPromptLoc.Text.EndsWith("VsDevCmd.bat"))
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
                                    txtExistsControlName.Text = Path.GetFileName(item);
                                    var controlManifestFile = item + "\\" + "ControlManifest.Input.xml";
                                    XmlReader xmlReader = XmlReader.Create(controlManifestFile);
                                    while (xmlReader.Read())
                                    {
                                        if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "control"))
                                        {
                                            var control_namespace = xmlReader.GetAttribute("namespace");
                                            txtExistNamespace.Text = control_namespace;
                                        }
                                    }
                                    xmlReader.Close();
                                    break;
                                }

                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(txtExistsControlName.Text))
                {
                    // Check if .cdsproj does not already exists
                    var controlDirs = Directory.GetDirectories(txtWorkingFolder.Text + "\\" + txtExistsControlName.Text);
                    if (controlDirs != null && controlDirs.Count() > 0)
                    {
                        var filteredControlDirs = controlDirs.ToList().Where(l => (!l.ToLower().EndsWith("generated")));
                        foreach (var item in filteredControlDirs)
                        {
                            var cdsprojName = Path.GetFileName(item);
                            var cdsprojExists = File.Exists(item + "\\" + cdsprojName + ".cdsproj");
                            if (cdsprojExists)
                            {
                                txtExistsDeployFolderName.Text = cdsprojName;
                                var solutionFile = item + "\\Other\\Solution.xml";
                                XmlReader xmlReader = XmlReader.Create(solutionFile);
                                while (xmlReader.Read())
                                {
                                    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "UniqueName"))
                                    {
                                        var publisher = xmlReader.ReadElementContentAsString();
                                        txtExistsPublisherName.Text = publisher;
                                    }
                                    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "CustomizationPrefix"))
                                    {
                                        var prefix = xmlReader.ReadElementContentAsString();
                                        txtExistsPublisherPrefix.Text = prefix;
                                    }
                                }
                                xmlReader.Close();
                                break;
                            }
                        }
                    }
                }
            }
            catch (DirectoryNotFoundException dnex)
            {
                MessageBox.Show("Invalid directory. Could not retrieve existing PCF project and CDS solution project details.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while retrieving existing project details. Please report an issue on GitHub page.");
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

        private bool SaveSettings()
        {
            var ok = true;

            if (!string.IsNullOrEmpty(txtWorkingFolder.Text) && !(pluginSettings.WorkingDirectoryLocation.Equals(txtWorkingFolder.Text)))
            {
                var result = MessageBox.Show("Do you want to save your working folder location?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
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

        private void RunCommandHelper(bool async, string commandsToShow, params string[] commands)
        {
            if (async)
            {
                WorkAsync(new WorkAsyncInfo
                {
                    Message = $"Running {commandsToShow} commands. Please wait.",
                    Work = (worker, args) =>
                    {
                        var output = CommandLineHelper.RunCommand(VisualStudioBatchFilePath, commands);
                        args.Result = output;
                    },
                    PostWorkCallBack = (args) =>
                    {
                        txtCommandPrompt.Clear();
                        txtCommandPrompt.AppendText((string)args.Result);
                        txtCommandPrompt.ScrollToCaret();

                        if (commands.Contains(Commands.Npm.RunBuild()) || commands.Contains(Commands.Msbuild.Rebuild()))
                        {
                            lblBuildStatus.Text = "Undetermined";
                            lblBuildStatus.ForeColor = Color.Gray;

                            if (((string)args.Result).ToLower().Contains("succeeded"))
                            {
                                lblBuildStatus.Text = "Succeeded";
                                lblBuildStatus.ForeColor = Color.LimeGreen;
                            }

                            if (((string)args.Result).ToLower().Contains("failed"))
                            {
                                lblBuildStatus.Text = "Failed";
                                lblBuildStatus.ForeColor = Color.DarkRed;
                            }
                        }
                    }
                });
            }
            else
            {
                CommandLineHelper.RunCommand(VisualStudioBatchFilePath, commands);
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
                txtVSPromptLoc.Text = pluginSettings.VisualStudioCommandPromptPath;
                txtWorkingFolder.Text = pluginSettings.WorkingDirectoryLocation;
                LogInfo("Settings found and loaded");
            }

            lblPCFInfo.Visible = false;
            webBrowserPCFInfo.Visible = false;

            gboxNewControl.Visible = false;
            gboxEditControl.Visible = false;
            cboxInfoSelection.SelectedIndex = 0;
            cboxInfoSelection.Visible = false;
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

        private void btnNewPcfRunPacCmd_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            lblRunPacError.Text = string.Empty;
            if (string.IsNullOrEmpty(txtNamespace.Text))
            {
                lblRunPacError.Text = "Namespace is required.";
            }
            if (string.IsNullOrEmpty(txtControlName.Text))
            {
                lblRunPacError.Text += "\nControl Name is required.";
            }
            if (cmbTemplate.SelectedIndex == -1)
            {
                lblRunPacError.Text += "\nTemplate selection is required.";
            }

            if (areMainControlsValid && !string.IsNullOrEmpty(txtNamespace.Text) && !string.IsNullOrEmpty(txtControlName.Text) && cmbTemplate.SelectedIndex >= 0)
            {
                // Check if .pcfproj does not already exists
                var folderName = Path.GetFileName(txtWorkingFolder.Text);
                if (IsPcfProjectAlreadyExists(folderName))
                {
                    MessageBox.Show("PCF Project already exists in this directory. Please select another folder location or use \"Edit existing PCF Control\" page.");
                    return;
                }

                lblRunPacError.Text = string.Empty;

                string cdWorkingDir = Commands.Cmd.ChangeDirectory(txtWorkingFolder.Text);
                string pacCommand = Commands.Pac.PcfInit(txtNamespace.Text, txtControlName.Text, cmbTemplate.SelectedItem.ToString());
                string npmCommand = Commands.Npm.Install();

                //Process.Start("cmd", "/K \"C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Enterprise\\Common7\\Tools\\VsDevCmd.bat\" && dir");
                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {pacCommand} && {npmCommand}");

                RunCommandHelper(true, "pacInit, npmInstall", cdWorkingDir, pacCommand, npmCommand);
            }
        }

        private void btnVSPromptLoc_Click(object sender, EventArgs e)
        {
            if (selectVSDevFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtVSPromptLoc.Text = selectVSDevFileDialog.FileName;
                pluginSettings.VisualStudioCommandPromptPath = txtVSPromptLoc.Text;
                VisualStudioBatchFilePath = txtVSPromptLoc.Text;
            }
        }

        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            var isValid = IsWorkingFolderPopulated(true);

            if (isValid)
            {
                Process.Start("explorer.exe", txtWorkingFolder.Text);
                lblDevelopComments.Text = $"Navigate inside {txtControlName.Text} folder.\nEdit \"ControlManifest.Input.xml\".\nDevelop your control.";
            }
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtControlName.Text}");
                string npmCommand = Commands.Npm.RunBuild();

                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {npmCommand}");
                RunCommandHelper(true, "npmBuild", cdWorkingDir, npmCommand);
            }
        }

        private void btnTestProject_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtControlName.Text}");
                string npmCommand = Commands.Npm.Start();

                // Using RunCommandHelper is causing issues.
                Process.Start("cmd", $"/K \"{VisualStudioBatchFilePath}\" && {cdWorkingDir} && {npmCommand}");
            }
        }

        private void btnCreatePackage_Click(object sender, EventArgs e)
        {
            lblDeploymentError.Text = string.Empty;
            if (string.IsNullOrEmpty(txtDeploymentFolder.Text))
            {
                lblDeploymentError.Text = "Deployment Folder Name is required.";
            }
            if (string.IsNullOrEmpty(txtPublisherName.Text))
            {
                lblDeploymentError.Text += "\nPublisher Name is required.";
            }
            if (string.IsNullOrEmpty(txtPublisherPrefix.Text))
            {
                lblDeploymentError.Text += "\nPublisher Prefix is required.";
            }

            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid && !string.IsNullOrEmpty(txtDeploymentFolder.Text) && !string.IsNullOrEmpty(txtPublisherName.Text) && !string.IsNullOrEmpty(txtPublisherPrefix.Text))
            {
                lblDeploymentError.Text = string.Empty;

                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtControlName.Text}");
                string mkdirDeploymentFolder = Commands.Cmd.MakeDirectory(txtDeploymentFolder.Text);
                string cdDeploymentFolder = Commands.Cmd.ChangeDirectory(txtDeploymentFolder.Text);
                string pacCommand_CreateSolution = Commands.Pac.SolutionInit(txtPublisherName.Text, txtPublisherPrefix.Text);
                string pacCommand_AddComponent = Commands.Pac.SolutionAddReference(txtWorkingFolder.Text);
                string msbuild_restore = Commands.Msbuild.Restore();
                string msbuild = Commands.Msbuild.Build();

                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {mkdirDeploymentFolder} && {cdDeploymentFolder} && {pacCommand_CreateSolution} && {pacCommand_AddComponent} && {msbuild_restore} && {msbuild}");
                RunCommandHelper(true, "pacCreateSolution, pacAddComponent, msRestore, msBuild", cdWorkingDir, mkdirDeploymentFolder, cdDeploymentFolder, pacCommand_CreateSolution, pacCommand_AddComponent, msbuild_restore, msbuild);
            }
        }

        private void btnDeploy_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                // The ExecuteMethod method handles connecting to an
                // organization if XrmToolBox is not yet connected
                ExecuteMethod(DeployNewCustomControl);
            }
        }

        private void tsbEditControl_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                lblPCFInfo.Visible = true;
                webBrowserPCFInfo.Visible = true;
                cboxInfoSelection.Visible = true;

                gboxNewControl.Visible = false;
                gboxEditControl.Visible = true;
                flowMainRight.Visible = true;

                IdentifyControlDetails();
            }
        }

        private void btnExistsOpenProject_Click(object sender, EventArgs e)
        {
            var isValid = IsWorkingFolderPopulated(true);

            if (isValid)
            {
                Process.Start("explorer.exe", txtWorkingFolder.Text);
                lblExistsDevelopComments.Text = $"Navigate inside {txtControlName.Text} folder.\nEdit \"ControlManifest.Input.xml\" by incrementing the version.\nEdit your control.";
            }
        }

        private void btnExistsBuild_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtExistsControlName.Text}");
                string npmCommand = Commands.Npm.RunBuild();

                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {npmCommand}");
                RunCommandHelper(true, "npmBuild", cdWorkingDir, npmCommand);
            }
        }

        private void btnExistsTest_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtExistsControlName.Text}");
                string npmCommand = Commands.Npm.Start();

                // Using RunCommandHelper is causing issues.
                Process.Start("cmd", $"/K \"{VisualStudioBatchFilePath}\" && {cdWorkingDir} && {npmCommand}");
            }
        }

        private void btnExistsCreateSolution_Click(object sender, EventArgs e)
        {
            lblExistsDeploymentError.Text = string.Empty;
            if (string.IsNullOrEmpty(txtExistsDeployFolderName.Text))
            {
                lblExistsDeploymentError.Text = "Deployment Folder Name is required.";
            }
            if (string.IsNullOrEmpty(txtExistsPublisherName.Text))
            {
                lblExistsDeploymentError.Text += "\nPublisher Name is required.";
            }
            if (string.IsNullOrEmpty(txtExistsPublisherPrefix.Text))
            {
                lblExistsDeploymentError.Text += "\nPublisher Prefix is required.";
            }

            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid && !string.IsNullOrEmpty(txtExistsDeployFolderName.Text) && !string.IsNullOrEmpty(txtExistsPublisherName.Text) && !string.IsNullOrEmpty(txtExistsPublisherPrefix.Text))
            {
                // Check if .cdsproj does not already exists
                var folderName = Path.GetFileName(txtWorkingFolder.Text + "\\" + txtExistsControlName.Text + "\\" + txtExistsDeployFolderName.Text);
                var cdsprojAlreadyExists = File.Exists(txtWorkingFolder.Text + "\\" + txtExistsControlName.Text + "\\" + txtExistsDeployFolderName.Text + "\\" + folderName + ".cdsproj");

                if (cdsprojAlreadyExists)
                {
                    btnExistsCreateSolution.Enabled = false;
                    cboxDeploymentFolderExists.Checked = true;
                    MessageBox.Show("Deployment project (.cdsproj) file already exists. Use the same project to build, deploy and update the custom control.");
                    return;
                }

                lblExistsDeploymentError.Text = string.Empty;

                string cdWorkingDir = $"cd {txtWorkingFolder.Text}\\{txtExistsControlName.Text}";

                if (cboxDeploymentFolderExists.Checked)
                {
                    // Delete existing folder and re-create it

                    string rmdirDeploymentFolder = Commands.Cmd.RemoveDirectory(txtExistsDeployFolderName.Text); ;
                    string mkdirDeploymentFolder = Commands.Cmd.MakeDirectory(txtExistsDeployFolderName.Text);
                    string cdDeploymentFolder = Commands.Cmd.ChangeDirectory(txtExistsDeployFolderName.Text);
                    string pacCommand_CreateSolution = Commands.Pac.SolutionInit(txtExistsPublisherName.Text, txtExistsPublisherPrefix.Text);
                    string pacCommand_AddComponent = Commands.Pac.SolutionAddReference(txtWorkingFolder.Text);

                    //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {rmdirDeploymentFolder} && {mkdirDeploymentFolder} && {cdDeploymentFolder} && {pacCommand_CreateSolution} && {pacCommand_AddComponent}");
                    RunCommandHelper(true, "pacCreateSolution, pacAddComponent", cdWorkingDir, rmdirDeploymentFolder, mkdirDeploymentFolder, cdDeploymentFolder, pacCommand_CreateSolution, pacCommand_AddComponent);
                }
                else
                {
                    // No folder exists; create a folder

                    string mkdirDeploymentFolder = Commands.Cmd.MakeDirectory(txtExistsDeployFolderName.Text);
                    string cdDeploymentFolder = Commands.Cmd.ChangeDirectory(txtExistsDeployFolderName.Text);
                    string pacCommand_CreateSolution = Commands.Pac.SolutionInit(txtExistsPublisherName.Text, txtExistsPublisherPrefix.Text);
                    string pacCommand_AddComponent = Commands.Pac.SolutionAddReference(txtWorkingFolder.Text);

                    //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {mkdirDeploymentFolder} && {cdDeploymentFolder} && {pacCommand_CreateSolution} && {pacCommand_AddComponent}");
                    RunCommandHelper(true, "pacCreateSolution, pacAddComponent", cdWorkingDir, mkdirDeploymentFolder, cdDeploymentFolder, pacCommand_CreateSolution, pacCommand_AddComponent);
                }
            }
        }

        private void cboxDeploymentFolderExists_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxDeploymentFolderExists.Checked)
            {
                lblExistsCreateSolutionWarning.Text = "*Use the same solution package to update the custom control.";
                btnExistsCreateSolution.Enabled = false;
            }
            else
            {
                lblExistsCreateSolutionWarning.Text = "*This will create the folder for you. Do not create deployment folder";
                btnExistsCreateSolution.Enabled = true;
            }
        }

        private void btnExistsDeployToD365_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            lblExistsDeploymentError.Text = string.Empty;
            if (string.IsNullOrEmpty(txtExistsDeployFolderName.Text))
            {
                lblExistsDeploymentError.Text = "Deployment Folder Name is required.";
            }

            if (areMainControlsValid && !string.IsNullOrEmpty(txtExistsDeployFolderName.Text))
            {
                // The ExecuteMethod method handles connecting to an
                // organization if XrmToolBox is not yet connected
                ExecuteMethod(DeployExistingCustomControl);
            }

        }

        private void powerAppsCLIOverviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://docs.microsoft.com/en-us/powerapps/developer/component-framework/overview");
        }

        private void tspmDownloadPowerAppsCLI_Click(object sender, EventArgs e)
        {
            Process.Start("https://aka.ms/PowerAppsCLI");
        }

        private void tspmUpdatePowerAppsCLI_Click(object sender, EventArgs e)
        {
            var isValid = IsVSCommandPromptLocationPopulated(true);

            if (isValid)
            {
                string pacUpdateCLI = Commands.Pac.InstallLatest();

                Process.Start("cmd", $"/K \"{VisualStudioBatchFilePath}\" && {pacUpdateCLI}");
                //RunCommandHelper(true, pacUpdateCLI);
            }
        }

        private void btnOpenInVSCode_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                string vscodeopen = $"code {txtWorkingFolder.Text}\\{txtControlName.Text}";

                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {vscodeopen} && exit");
                RunCommandHelper(false, "VSCode Open", vscodeopen);
            }
        }

        private void btnExistsOpenInVSCode_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                string vscodeopen = $"code {txtWorkingFolder.Text}\\{txtExistsControlName.Text}";

                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {vscodeopen} && exit");
                RunCommandHelper(false, "VSCode Open", vscodeopen);
            }
        }

        private void btnBuildDeploymentProject_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            lblExistsDeploymentError.Text = string.Empty;
            if (string.IsNullOrEmpty(txtExistsDeployFolderName.Text))
            {
                lblExistsDeploymentError.Text = "Deployment Folder Name is required.";
            }

            if (areMainControlsValid && !string.IsNullOrEmpty(txtExistsDeployFolderName.Text))
            {
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtExistsControlName.Text}");

                string cdDeploymentFolder = Commands.Cmd.ChangeDirectory(txtExistsDeployFolderName.Text);
                string msbuild_restore = Commands.Msbuild.Restore();
                string msbuild_rebuild = Commands.Msbuild.Rebuild();

                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {cdDeploymentFolder} && {msbuild_restore} && {msbuild_rebuild}");
                RunCommandHelper(true, "msRestore, msRebuild", cdWorkingDir, cdDeploymentFolder, msbuild_restore, msbuild_rebuild);
            }
        }

        private void btnEditCDSProjectFile_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            lblExistsDeploymentError.Text = string.Empty;
            if (string.IsNullOrEmpty(txtExistsDeployFolderName.Text))
            {
                lblExistsDeploymentError.Text = "Deployment Folder Name is required.";
            }

            if (isValid && !string.IsNullOrEmpty(txtExistsDeployFolderName.Text))
            {
                string vscodeopen = $"code {txtWorkingFolder.Text}\\{txtExistsControlName.Text}\\{txtExistsDeployFolderName.Text}";

                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {vscodeopen} && exit");
                RunCommandHelper(false, "VSCode Open", vscodeopen);
            }
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

        private void cboxInfoSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = cboxInfoSelection.SelectedItem;
            string browserURL = "https://aka.ms/PCFDemos"; // Default
            switch (selection)
            {
                case "Demos":
                    browserURL = "https://aka.ms/PCFDemos ";
                    break;
                case "Blogs":
                    browserURL = "https://aka.ms/PCFBlog";
                    break;
                case "Forums":
                    browserURL = "https://aka.ms/PCFForum";
                    break;
                case "Ideas":
                    browserURL = "https://aka.ms/PCFIdeas";
                    break;
            }

            webBrowserPCFInfo.Url = new Uri(browserURL);
        }

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
                lblPCFInfo.Visible = true;
                webBrowserPCFInfo.Visible = true;
                cboxInfoSelection.Visible = true;

                gboxNewControl.Visible = true;
                gboxEditControl.Visible = false;
                flowMainRight.Visible = true;
            }
        }

        private void TsmNewPCFTemplate_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                var ctrlTemplates = new Templates(txtVSPromptLoc.Text, txtWorkingFolder.Text);
                ctrlTemplates.StartPosition = FormStartPosition.CenterScreen;
                ctrlTemplates.ParentControl = this;
                ctrlTemplates.ShowDialog();

                if (SelectedTemplate == null)
                {
                    return;
                }

                IdentifyControlDetails();

                if (string.IsNullOrEmpty(txtExistsControlName.Text))
                {
                    MessageBox.Show("Not able to parse the control from the template.");

                    lblPCFInfo.Visible = true;
                    webBrowserPCFInfo.Visible = true;
                    cboxInfoSelection.Visible = true;

                    gboxNewControl.Visible = true;
                    gboxEditControl.Visible = false;
                    flowMainRight.Visible = true;

                    return;
                }

                lblPCFInfo.Visible = true;
                webBrowserPCFInfo.Visible = true;
                cboxInfoSelection.Visible = true;

                gboxNewControl.Visible = false;
                gboxEditControl.Visible = true;
                flowMainRight.Visible = true;

                // Run npm install - as the solution is downloaded form GitHub
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtControlName.Text}");
                string npmCommand = Commands.Npm.Install();
                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {npmCommand} && exit");

                RunCommandHelper(true, "npmInstall", cdWorkingDir, npmCommand);
            }
        }

        private void TsbNewPCFMenu_ButtonClick(object sender, EventArgs e)
        {
            tsbNewPCFMenu.ShowDropDown();
        }

        private void BtnExistingRefreshDetails_Click(object sender, EventArgs e)
        {
            IdentifyControlDetails();
        }

        public override void ClosingPlugin(PluginCloseInfo info)
        {
            if (!SaveSettings())
            {
                info.Cancel = true;
                return;
            }
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

        }

        private void TxtVSPromptLoc_TextChanged(object sender, EventArgs e)
        {
            pluginSettings.VisualStudioCommandPromptPath = txtVSPromptLoc.Text;
            VisualStudioBatchFilePath = txtVSPromptLoc.Text;
        }

        private void BtnBuildAndTest_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtExistsControlName.Text}");
                string npmBuildCommand = Commands.Npm.RunBuild();
                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {npmCommand}");
                RunCommandHelper(true, "npmBuild", cdWorkingDir, npmBuildCommand);

                string npmTestCommand = Commands.Npm.Start();
                // Using RunCommandHelper is causing issues.
                Process.Start("cmd", $"/K \"{VisualStudioBatchFilePath}\" && {cdWorkingDir} && {npmTestCommand}");
            }
        }

        private void BtnBuildAllProjects_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            lblExistsDeploymentError.Text = string.Empty;
            if (string.IsNullOrEmpty(txtExistsDeployFolderName.Text))
            {
                lblExistsDeploymentError.Text = "Deployment Folder Name is required.";
            }

            if (areMainControlsValid && !string.IsNullOrEmpty(txtExistsDeployFolderName.Text))
            {
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtExistsControlName.Text}");
                string npmBuildCommand = Commands.Npm.RunBuild();

                string cdDeploymentFolder = Commands.Cmd.ChangeDirectory(txtExistsDeployFolderName.Text);
                string msbuild_restore = Commands.Msbuild.Restore();
                string msbuild_rebuild = Commands.Msbuild.Rebuild();

                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {cdDeploymentFolder} && {msbuild_restore} && {msbuild_rebuild}");
                RunCommandHelper(true, "npmBuild, msRestore, msRebuild", cdWorkingDir, npmBuildCommand, cdDeploymentFolder, msbuild_restore, msbuild_rebuild);
            }
        }

        private void BtnBuildAndDeployAll_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            lblExistsDeploymentError.Text = string.Empty;
            if (string.IsNullOrEmpty(txtExistsDeployFolderName.Text))
            {
                lblExistsDeploymentError.Text = "Deployment Folder Name is required.";
            }

            if (areMainControlsValid && !string.IsNullOrEmpty(txtExistsDeployFolderName.Text))
            {
                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtExistsControlName.Text}");
                string npmBuildCommand = Commands.Npm.RunBuild();

                string cdDeploymentFolder = Commands.Cmd.ChangeDirectory(txtExistsDeployFolderName.Text);
                string msbuild_restore = Commands.Msbuild.Restore();
                string msbuild_rebuild = Commands.Msbuild.Rebuild();

                //Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {cdDeploymentFolder} && {msbuild_restore} && {msbuild_rebuild}");
                RunCommandHelper(true, "npmBuild, msRestore, msRebuild", cdWorkingDir, npmBuildCommand, cdDeploymentFolder, msbuild_restore, msbuild_rebuild);

                // The ExecuteMethod method handles connecting to an
                // organization if XrmToolBox is not yet connected
                ExecuteMethod(DeployExistingCustomControl);
            }
        }
    }
}