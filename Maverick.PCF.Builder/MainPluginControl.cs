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

namespace Maverick.PCF.Builder
{
    public partial class MainPluginControl : PluginControlBase, IGitHubPlugin, IHelpPlugin
    {
        private Settings mySettings;

        public string RepositoryName => "PCF-CustomControlBuilder";

        public string UserName => "Danz-maveRICK";

        public string HelpUrl => "https://github.com/Danz-maveRICK/PCF-CustomControlBuilder/blob/master/README.md";

        public MainPluginControl()
        {
            InitializeComponent();
        }

        private void MainPluginControl_Load(object sender, EventArgs e)
        {
            //ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();
                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
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

        ///// <summary>
        ///// This event occurs when the plugin is closed
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        //{
        //    // Before leaving, save the settings
        //    SettingsManager.Instance.Save(GetType(), mySettings);
        //}

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
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
                var pcfprojAlreadyExists = File.Exists(txtWorkingFolder.Text + "\\" + folderName + ".pcfproj");

                if (pcfprojAlreadyExists)
                {
                    MessageBox.Show("PCF Project already exists in this directory. Please select another folder location or use \"Edit existing PCF Control\" page.");
                    return;
                }

                lblRunPacError.Text = string.Empty;

                string vsPromptLocation = txtVSPromptLoc.Text;
                string cdWorkingDir = $"cd {txtWorkingFolder.Text}";
                string pacCommand = $"pac pcf init --namespace {txtNamespace.Text} --name {txtControlName.Text} --template {cmbTemplate.SelectedItem.ToString().ToLower()}";
                string npmCommand = $"npm install";

                //Process.Start("cmd", "/K \"C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Enterprise\\Common7\\Tools\\VsDevCmd.bat\" && dir");
                Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {pacCommand} && {npmCommand}");
            }
        }

        private void btnVSPromptLoc_Click(object sender, EventArgs e)
        {
            if (selectVSDevFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtVSPromptLoc.Text = selectVSDevFileDialog.FileName;
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
                string vsPromptLocation = txtVSPromptLoc.Text;
                string cdWorkingDir = $"cd {txtWorkingFolder.Text}\\{txtControlName.Text}";
                string npmCommand = $"npm run build";

                Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {npmCommand}");
            }
        }

        private void btnTestProject_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                string vsPromptLocation = txtVSPromptLoc.Text;
                string cdWorkingDir = $"cd {txtWorkingFolder.Text}\\{txtControlName.Text}";
                string npmCommand = $"npm start";

                Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {npmCommand}");
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

                string vsPromptLocation = txtVSPromptLoc.Text;
                string cdWorkingDir = $"cd {txtWorkingFolder.Text}\\{txtControlName.Text}";
                string mkdirDeploymentFolder = $"mkdir {txtDeploymentFolder.Text}";
                string cdDeploymentFolder = $"cd {txtDeploymentFolder.Text}";
                string pacCommand_CreateSolution = $"pac solution init --publisherName {txtPublisherName.Text} --customizationPrefix {txtPublisherPrefix.Text}";
                string pacCommand_AddComponent = $"pac solution add-reference --path {txtWorkingFolder.Text}";
                string msbuild_restore = "msbuild /t:restore";
                string msbuild = "msbuild ";

                Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {mkdirDeploymentFolder} && {cdDeploymentFolder} && {pacCommand_CreateSolution} && {pacCommand_AddComponent} && {msbuild_restore} && {msbuild}");
            }
        }

        private void tsbNewPCF_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                lblPCFInfo.Visible = true;
                webBrowserPCFInfo.Visible = true;
                cboxInfoSelection.Visible = true;

                gboxNewControl.Visible = true;
                gboxEditControl.Visible = false;
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

                try
                {
                    // Check if .pcfproj does not already exists
                    var workingFolderName = Path.GetFileName(txtWorkingFolder.Text);
                    var pcfprojAlreadyExists = File.Exists(txtWorkingFolder.Text + "\\" + workingFolderName + ".pcfproj");
                    if (pcfprojAlreadyExists)
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
                                    break;
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
                string vsPromptLocation = txtVSPromptLoc.Text;
                string cdWorkingDir = $"cd {txtWorkingFolder.Text}\\{txtExistsControlName.Text}";
                string npmCommand = $"npm run build";

                Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {npmCommand}");
            }
        }

        private void btnExistsTest_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                string vsPromptLocation = txtVSPromptLoc.Text;
                string cdWorkingDir = $"cd {txtWorkingFolder.Text}\\{txtExistsControlName.Text}";
                string npmCommand = $"npm start";

                Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {npmCommand}");
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

                string vsPromptLocation = txtVSPromptLoc.Text;
                string cdWorkingDir = $"cd {txtWorkingFolder.Text}\\{txtExistsControlName.Text}";

                if (cboxDeploymentFolderExists.Checked)
                {
                    // Delete existing folder and re-create it

                    string rmdirDeploymentFolder = $"rmdir /s {txtExistsDeployFolderName.Text}";
                    string mkdirDeploymentFolder = $"mkdir {txtExistsDeployFolderName.Text}";
                    string cdDeploymentFolder = $"cd {txtExistsDeployFolderName.Text}";
                    string pacCommand_CreateSolution = $"pac solution init --publisherName {txtExistsPublisherName.Text} --customizationPrefix {txtExistsPublisherPrefix.Text}";
                    string pacCommand_AddComponent = $"pac solution add-reference --path {txtWorkingFolder.Text}";

                    Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {rmdirDeploymentFolder} && {mkdirDeploymentFolder} && {cdDeploymentFolder} && {pacCommand_CreateSolution} && {pacCommand_AddComponent}");
                }
                else
                {
                    // No folder exists; create a folder

                    string mkdirDeploymentFolder = $"mkdir {txtExistsDeployFolderName.Text}";
                    string cdDeploymentFolder = $"cd {txtExistsDeployFolderName.Text}";
                    string pacCommand_CreateSolution = $"pac solution init --publisherName {txtExistsPublisherName.Text} --customizationPrefix {txtExistsPublisherPrefix.Text}";
                    string pacCommand_AddComponent = $"pac solution add-reference --path {txtWorkingFolder.Text}";

                    Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {mkdirDeploymentFolder} && {cdDeploymentFolder} && {pacCommand_CreateSolution} && {pacCommand_AddComponent}");
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
                string vsPromptLocation = txtVSPromptLoc.Text;
                string pacUpdateCLI = $"pac install latest";

                Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {pacUpdateCLI}");
            }
        }

        private void btnOpenInVSCode_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                string vsPromptLocation = txtVSPromptLoc.Text;
                string vscodeopen = $"code {txtWorkingFolder.Text}\\{txtControlName.Text}";

                Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {vscodeopen} && exit");
            }
        }

        private void btnExistsOpenInVSCode_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                string vsPromptLocation = txtVSPromptLoc.Text;
                string vscodeopen = $"code {txtWorkingFolder.Text}\\{txtExistsControlName.Text}";

                Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {vscodeopen} && exit");
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
                string vsPromptLocation = txtVSPromptLoc.Text;
                string cdWorkingDir = $"cd {txtWorkingFolder.Text}\\{txtExistsControlName.Text}";

                string cdDeploymentFolder = $"cd {txtExistsDeployFolderName.Text}";
                string msbuild_restore = "msbuild /t:restore";
                string msbuild_rebuild = "msbuild /t:rebuild";

                Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {cdDeploymentFolder} && {msbuild_restore} && {msbuild_rebuild}");
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
                string vsPromptLocation = txtVSPromptLoc.Text;
                string vscodeopen = $"code {txtWorkingFolder.Text}\\{txtExistsControlName.Text}\\{txtExistsDeployFolderName.Text}";

                Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {vscodeopen} && exit");
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


        #endregion


    }
}