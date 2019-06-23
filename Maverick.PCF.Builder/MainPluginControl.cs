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

namespace Maverick.PCF.Builder
{
    public partial class MainPluginControl : PluginControlBase
    {
        private Settings mySettings;

        public MainPluginControl()
        {
            InitializeComponent();
            //lblLinkBlog.Links.Add(0, lblLinkBlog.Text.Length, "http://bit.ly/2Kdqu0q");
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
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

            tabControl.Visible = false;
            lblPCFInfo.Visible = false;
            webBrowserPCFInfo.Visible = false;
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

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

            if (!string.IsNullOrEmpty(txtNamespace.Text) && !string.IsNullOrEmpty(txtControlName.Text) && cmbTemplate.SelectedIndex >= 0)
            {
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
            Process.Start("explorer.exe", txtWorkingFolder.Text);
            lblDevelopComments.Text = $"Navigate inside {txtControlName.Text} folder.\nEdit \"ControlManifest.Input.xml\".\nDevelop your control.";
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            string vsPromptLocation = txtVSPromptLoc.Text;
            string cdWorkingDir = $"cd {txtWorkingFolder.Text}\\{txtControlName.Text}";
            string npmCommand = $"npm run build";

            Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {npmCommand}");
        }

        private void btnTestProject_Click(object sender, EventArgs e)
        {
            string vsPromptLocation = txtVSPromptLoc.Text;
            string cdWorkingDir = $"cd {txtWorkingFolder.Text}\\{txtControlName.Text}";
            string npmCommand = $"npm start";

            Process.Start("cmd", $"/K \"{vsPromptLocation}\" && {cdWorkingDir} && {npmCommand}");
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

            if (!string.IsNullOrEmpty(txtDeploymentFolder.Text) && !string.IsNullOrEmpty(txtPublisherName.Text) && !string.IsNullOrEmpty(txtPublisherPrefix.Text))
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
            lblErrors.Text = string.Empty;
            if (string.IsNullOrEmpty(txtWorkingFolder.Text))
            {
                lblErrors.Text = "Working folder is required.";
            }
            if (string.IsNullOrEmpty(txtVSPromptLoc.Text))
            {
                lblErrors.Text += "\nVisual Studio Developer Command Prompt executable location is required.";
            }

            if (!string.IsNullOrEmpty(txtWorkingFolder.Text) && !string.IsNullOrEmpty(txtVSPromptLoc.Text))
            {
                tabControl.Visible = true;

                lblPCFInfo.Visible = true;
                webBrowserPCFInfo.Visible = true;

                lblErrors.Text = string.Empty;
            }
        }

        private void btnDeploy_Click(object sender, EventArgs e)
        {
            // The ExecuteMethod method handles connecting to an
            // organization if XrmToolBox is not yet connected
            ExecuteMethod(DeployCustomControl);
        }

        private void DeployCustomControl()
        {
            string deploymentFolder = $"{txtWorkingFolder.Text}\\{txtControlName.Text}\\{txtDeploymentFolder.Text}\\bin\\debug\\{txtDeploymentFolder.Text}.zip";
            byte[] fileBytes = File.ReadAllBytes(deploymentFolder);


            WorkAsync(new WorkAsyncInfo
            {
                Message = "Deploying custom control solution to Dynamics 365",
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
                    var result = args.Result as OrganizationResponse;
                    if (result != null)
                    {
                        MessageBox.Show($"Solution {txtDeploymentFolder.Text} deployed to Dynamics 365 CE");
                    }
                }
            });
        }
    }
}