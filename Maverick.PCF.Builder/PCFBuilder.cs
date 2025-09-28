﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
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
using XrmToolBox.Extensibility.Args;
using System.Text.RegularExpressions;
using Maverick.PCF.Builder.SealedClasses;
using Maverick.PCF.Builder.ToolSettings;
using Maverick.PCF.Builder.Common;
using Enum = Maverick.PCF.Builder.Helper.Enum;
using SolutionDetails = Maverick.PCF.Builder.DataObjects.SolutionDetails;

namespace Maverick.PCF.Builder
{
    public partial class PCFBuilder : PluginControlBase, IGitHubPlugin, IHelpPlugin, IPayPalPlugin, IStatusBarMessenger
    {
        #region XrmToolBox settings
        private Settings pluginSettings;

        public string RepositoryName => "PCF-CustomControlBuilder";
        public string UserName => "Power-Maverick";
        public string HelpUrl => "https://github.com/Power-Maverick/PCF-CustomControlBuilder/blob/master/README.md";
        public string DonationDescription => "Keeps the ball rolling and motivates in making awesome tools. You will get free stickers; I will need your mailing address.";
        public string EmailAccount => "danz@techgeek.co.in";
        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;
        #endregion

        #region Public Properties and Enums

        public PcfGallery SelectedTemplate { get; set; }
        public bool CommandPromptInitialized { get; set; }
        public bool StatusCheckExecution { get; set; }
        public ProcessingStatus ExecutionStatus { get; set; }
        public bool ResxCheckExecution { get; set; }
        public bool BuildDeployExecution { get; set; }
        public bool ReloadDetails { get; set; }
        public string CurrentCommandOutput { get; set; }
        public bool ListProfileExecution { get; set; }
        public AuthProfileAction SelectedProfileAction { get; set; }
        public int SelectedProfileIndex { get; set; }
        public bool RefreshCurrentProfile { get; set; }
        public ControlManifestDetails ControlDetails { get; set; }
        public SolutionDetails DataverseSolutionDetails { get; set; }
        public List<LanguageCode> SelectedLcids { get; set; }
        public bool InitiatePCFProjectBuild { get; set; }

        public enum ProcessingStatus
        {
            Succeeded,
            Failed,
            NeedReview,
            Running,
            Undetermined,
            Complete
        }

        public enum AuthProfileAction
        {
            ShowDetails,
            SwitchCurrent,
            Delete
        }

        #endregion

        #region Private Variables & Constants

        private BackgroundWorker _mainPluginLocalWorker;
        private EntityCollection _solutionsCache;
        private EntityCollection _publishersCache;
        private BindingSource bindingSource = new BindingSource();
        private ControlManifestHelper ControlManifest = new ControlManifestHelper();
        private SolutionDetailsHelper Solution = new SolutionDetailsHelper();

        private const string TEMPLATE_LOCATION = "Templates";
        private const string SolutionFolderName = "Solution";

        #endregion

        #region Helper Functions

        private void InitCommandLine()
        {
            consoleControl.ShowDiagnostics = true;
            consoleControl.StartProcess("cmd", $"/K powershell");

            if (pluginSettings.CustomExecutionPolicy != string.Empty)
            {
                string[] commands = new string[] { Commands.Cmd.SetCustomExecutionPolicy(pluginSettings.CustomExecutionPolicy) };
                CommandLineHelper.RunCommand(commands);
            }            
        }

        private void RunCommandLine(params string[] commands)
        {
            // Make sure display is not corrupted when clicked anywhere on the console
            consoleControl.InternalRichTextBox.SelectionStart = consoleControl.InternalRichTextBox.Text.Length;
            consoleControl.InternalRichTextBox.SelectionLength = 0;

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

            if (commands.Contains(Commands.Pac.ListProfiles()))
            {
                ListProfileExecution = true;
            }
            else
            {
                ListProfileExecution = false;
            }

            // If any pac auth command found then refrsh the current auth profile details on form
            if (commands.Any(s => s.Contains("pac auth")))
            {
                _mainPluginLocalWorker = new BackgroundWorker();
                _mainPluginLocalWorker.DoWork += CheckDefaultAuthenticationProfile;
                _mainPluginLocalWorker.RunWorkerAsync();
            }

            foreach (var cmd in commands)
            {
                consoleControl.WriteInput(cmd, Color.White, false);
            }

            if (commands.Contains("yo pcf:resx"))
            {
                ResxCheckExecution = true;
            }

            //consoleControl.WriteInput("\r\n", Color.White, true);
            consoleControl.InternalRichTextBox.ScrollToCaret();
        }

        private void LogEvent(string eventName)
        {
            Telemetry.TrackEvent(eventName);
        }

        private void LogEventMetrics(string eventName, string metricName, double metric)
        {
            var metrics = new Dictionary<string, double>
            {
              { metricName, metric }
            };

            Telemetry.TrackEvent(eventName, null, metrics);
        }

        private void LogException(Exception ex)
        {
            Telemetry.TrackException(ex);
        }

        private void LoadMostRecentUsedLocations()
        {
            if (MostRecentlyUsedLocations.Instance.Items.Any())
            {
                var mrulList = MostRecentlyUsedLocations.Instance.Items.OrderByDescending(itm => itm.Date).ToList();
                bindingSource.DataSource = mrulList;
                dgvMRULocations.DataSource = bindingSource;
            }
        }

        private void ToggleMRULocationGrid()
        {
            dgvMRULocations.Visible = !dgvMRULocations.Visible;
            btnShowMRULocations.Text = dgvMRULocations.Visible ? "🔼" : "🔽";
        }

        private void BuildPCFProject()
        {
            string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{txtControlName.Text}");
            string npmCommand = Commands.Npm.RunBuild();

            RunCommandLine(cdWorkingDir, npmCommand);
        }

        #endregion

        #region Custom Functions

        private bool AreMainControlsPopulated()
        {
            lblErrors.Text = string.Empty;
            bool isWorkingFolderValid = true;

            isWorkingFolderValid = IsControlLocationPopulated(false);

            if (isWorkingFolderValid)
            {
                lblErrors.Text = string.Empty;
            }

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
            if (cboxAdditionalPackages.SelectedIndex == -1)
            {
                lblErrors.Text += "\nAdditional Package selection is required.";
            }

            bool isValid = isWorkingFolderValid && string.IsNullOrEmpty(lblErrors.Text);

            return isValid;
        }

        private bool IsControlLocationPopulated(bool clearError)
        {
            if (clearError)
            {
                lblErrors.Text = string.Empty;
            }

            bool isValid = true;
            if (string.IsNullOrEmpty(txtWorkingFolder.Text))
            {
                lblErrors.Text = "Control Location is required.";
                isValid = false;
            }

            if (isValid && clearError)
            {
                lblErrors.Text = string.Empty;
            }

            return isValid;
        }

        //private void SetupDockControls()
        //{
        //    pcfProjForm = new PCFProject(this, ControlDetails);
        //    pcfProjForm.Show(dockContainer, DockState.Document);
        //    solutionProjForm.Show(pcfProjForm.Pane, null);

        //    pcfProjForm.Activate();
        //}

        private bool AreSolutionDetailsPopulated()
        {
            bool isValid = true;
            lblErrors.Text = string.Empty;
            if (string.IsNullOrEmpty(txtSolutionFriendlyName.Text))
            {
                lblErrors.Text = "Solution Name is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(txtPublisherUniqueName.Text))
            {
                lblErrors.Text += "\nPublisher Name is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(txtPublisherPrefix.Text))
            {
                lblErrors.Text += "\nPublisher Prefix is required.";
                isValid = false;
            }
            else
            {
                /*
                 * The prefix must have at least 2 characters and start with a letter. 
                 * It cannot start with "mscrm".
                 * Max. Char. length: 9
                */
                if (Regex.Match(txtPublisherPrefix.Text, @"^[A-Za-z][A-Za-z0-9]{1,8}$").Success && !txtPublisherPrefix.Text.StartsWith("mscrm"))
                {
                    lblErrors.Text = string.Empty;
                }
                else
                {
                    lblErrors.Text += "\nIncorrect Publisher Prefix.";
                    isValid = false;
                }
            }

            return isValid;
        }

        private bool VerifyCodeFileExists(string directoryPath)
        {
            var resp = ControlManifest.CodeFileExists(directoryPath);
            if (!string.IsNullOrEmpty(resp))
            {
                SendMessageToStatusBar.Invoke(this, new StatusBarMessageEventArgs($"Code file found with name {resp}"));
            }

            return !string.IsNullOrEmpty(resp);
        }

        private void Routine_NewComponent()
        {
            txtNamespace.Clear();
            txtControlName.Clear();
            txtControlDisplayName.Clear();
            txtControlDescription.Clear();
            cboxControlType.SelectedIndex = -1;
            cboxTemplate.SelectedIndex = -1;
            cboxAdditionalPackages.SelectedIndex = -1;
            txtComponentVersion.Clear();

            txtSolutionName.Clear();
            txtSolutionFriendlyName.Clear();
            txtPublisherUniqueName.Clear();
            txtPublisherFriendlyName.Clear();
            txtPublisherPrefix.Clear();
            txtSolutionVersion.Clear();

            txtControlName.Enabled = true;
            txtNamespace.Enabled = true;
            txtControlDisplayName.Enabled = true;
            txtControlDescription.Enabled = true;
            cboxControlType.Enabled = true;
            cboxTemplate.Enabled = true;
            cboxAdditionalPackages.Enabled = true;
            txtSolutionName.Enabled = true;
            txtSolutionFriendlyName.Enabled = true;
            txtPublisherUniqueName.Enabled = true;
            txtPublisherFriendlyName.Enabled = true;
            txtPublisherPrefix.Enabled = true;
            btnCreateComponent.Enabled = true;
            btnCreateSolution.Enabled = true;
            btnRefreshDetails.Enabled = false;
            btnDeploy.Enabled = false;

            btnUpdateControlDetails.Enabled = false;
            btnManageProperties.Enabled = false;
            btnManageFeatures.Enabled = false;
            btnAddPreviewImage.Enabled = false;
            btnAddResxFile.Enabled = false;

            lblControlInitStatus.Text = Enum.InitializationStatus(false);
            lblControlInitStatus.ForeColor = Color.Firebrick;

            lblSolutionInitStatus.Text = Enum.InitializationStatus(false);
            lblSolutionInitStatus.ForeColor = Color.Firebrick;
        }

        private void Routine_EditComponent()
        {
            btnRefreshDetails.Enabled = true;

            if (!string.IsNullOrEmpty(txtControlName.Text))
            {
                txtControlName.Enabled = false;
                txtNamespace.Enabled = false;
                cboxControlType.Enabled = false;
                cboxTemplate.Enabled = false;
                cboxAdditionalPackages.Enabled = false;
                btnCreateComponent.Enabled = false;
                txtControlDisplayName.Enabled = false;
                txtControlDescription.Enabled = false;

                btnUpdateControlDetails.Enabled = true;
                btnManageProperties.Enabled = true;
                btnManageFeatures.Enabled = true;
                btnAddPreviewImage.Enabled = true;
                btnAddResxFile.Enabled = true;

                lblControlInitStatus.Text = Enum.InitializationStatus(true);
                lblControlInitStatus.ForeColor = Color.ForestGreen;
            }
        }

        private void Routine_SolutionFound()
        {
            if (!string.IsNullOrEmpty(txtSolutionName.Text))
            {
                txtSolutionFriendlyName.Enabled = false;
                txtSolutionName.Enabled = false;
                txtPublisherFriendlyName.Enabled = false;
                txtPublisherUniqueName.Enabled = false;
                txtPublisherFriendlyName.Enabled = false;
                txtPublisherPrefix.Enabled = false;
                btnCreateSolution.Enabled = false;
                cboxSolutions.Enabled = false;

                radSolutionTypeUnmanaged.Enabled = true;
                radSolutionTypeManaged.Enabled = true;
                radSolutionTypeBoth.Enabled = true;

                lblSolutionInitStatus.Text = Enum.InitializationStatus(true);
                lblSolutionInitStatus.ForeColor = Color.ForestGreen;
                btnDeploy.Enabled = true;
            }
        }

        private void Routine_SolutionNotFound(bool loadFromSettings = true, bool clearFields = true)
        {
            txtSolutionFriendlyName.Enabled = true;
            txtSolutionName.Enabled = true;
            txtPublisherUniqueName.Enabled = true;
            txtPublisherFriendlyName.Enabled = true;
            txtPublisherPrefix.Enabled = true;
            btnCreateSolution.Enabled = true;
            btnDeploy.Enabled = false;

            radSolutionTypeUnmanaged.Enabled = false;
            radSolutionTypeManaged.Enabled = false;
            radSolutionTypeBoth.Enabled = false;

            if (clearFields)
            {
                txtSolutionFriendlyName.Clear();
                txtSolutionName.Clear();
                txtPublisherUniqueName.Clear();
                txtPublisherFriendlyName.Clear();
                txtPublisherPrefix.Clear();
                txtSolutionVersion.Clear(); 
            }

            lblSolutionInitStatus.Text = Enum.InitializationStatus(false);
            lblSolutionInitStatus.ForeColor = Color.Firebrick;

            if (loadFromSettings)
            {
                TryLoadCDSDetailsFromSettings();
            }
        }

        private void Routine_ExistingSolution()
        {
            if (chkUseExistingSolution.Checked)
            {
                chkUseExistingPublisher.Checked = false;
                txtSolutionFriendlyName.Visible = false;
                txtSolutionName.Enabled = false;
                txtPublisherFriendlyName.Enabled = false;
                txtPublisherUniqueName.Enabled = false;
                txtPublisherPrefix.Enabled = false;
                cboxSolutions.Visible = true;
                //chkManagedSolution.Enabled = false;
                cboxPublishers.Visible = false;
                chkUseExistingPublisher.Visible = false;
                btnCreateSolution.Text = "Export and Add Control";

                radSolutionTypeUnmanaged.Enabled = false;
                radSolutionTypeManaged.Enabled = false;
                radSolutionTypeBoth.Enabled = false;
            }
            else
            {
                txtSolutionFriendlyName.Visible = true;
                txtSolutionName.Enabled = true;
                txtPublisherFriendlyName.Enabled = true;
                txtPublisherUniqueName.Enabled = true;
                txtPublisherPrefix.Enabled = true;
                cboxSolutions.Visible = false;
                //chkManagedSolution.Enabled = true;
                chkUseExistingPublisher.Visible = true;
                btnCreateSolution.Text = "Create and Add Control";

                radSolutionTypeUnmanaged.Enabled = true;
                radSolutionTypeManaged.Enabled = true;
                radSolutionTypeBoth.Enabled = true;
            }
        }

        private void Routine_ExistingPublisher()
        {
            if (chkUseExistingPublisher.Checked)
            {
                txtPublisherFriendlyName.Visible = false;
                txtPublisherUniqueName.Enabled = false;
                txtPublisherPrefix.Enabled = false;
                cboxPublishers.Visible = true;
            }
            else
            {
                txtPublisherFriendlyName.Visible = true;
                txtPublisherUniqueName.Enabled = true;
                txtPublisherPrefix.Enabled = true;
                cboxPublishers.Visible = false;
            }
        }

        private void TryLoadPCFDetailsFromSettings()
        {
            if (pluginSettings.AlwaysLoadNamespaceFromSettings)
            {
                txtNamespace.Text = pluginSettings.ControlNamespace;
            }
        }

        private void TryLoadCDSDetailsFromSettings()
        {
            if (pluginSettings.AlwaysLoadPublisherDetailsFromSettings)
            {
                txtPublisherUniqueName.Text = pluginSettings.PublisherName;
                txtPublisherPrefix.Text = pluginSettings.PublisherPrefix;
            }
        }

        private void IdentifyControlDetails()
        {
            try
            {
                var start = DateTime.Now;

                ControlDetails = ControlManifest.GetControlManifestDetails(txtWorkingFolder.Text);

                //_mainPluginLocalWorker = new BackgroundWorker();
                //_mainPluginLocalWorker.DoWork += IdentifyAdditionalPackage;
                //_mainPluginLocalWorker.RunWorkerAsync();

                IdentifyAdditionalPackage_New();

                if (!string.IsNullOrEmpty(ControlDetails.ControlName))
                {
                    IdentifySolutionDetails();
                }

                if (ControlDetails.FoundControlDetails)
                {
                    Routine_EditComponent();
                }
                else
                {
                    Routine_NewComponent();
                }

                var end = DateTime.Now;
                var duration = end - start;
                LogEventMetrics("IdentifyControlDetails", "ProcessingTime", duration.TotalMilliseconds);
            }
            catch (DirectoryNotFoundException dnex)
            {
                MessageBox.Show("Invalid directory. Could not retrieve existing PCF project and CDS solution project details.");
                Routine_NewComponent();
                LogException(dnex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while retrieving existing project details. Please report an issue on GitHub page.");
                Routine_NewComponent();
                LogException(ex);
            }
        }

        private void IdentifyAdditionalPackage(object worker, DoWorkEventArgs args)
        {
            // Identify additional package installs - for now only check for Fluent UI
            var packageName = Enum.AdditionalPackages().FirstOrDefault(p => p.Value == "Fluent UI").Key;
            string[] addPkgCommands = new string[] { Commands.Npm.CheckAdditionalPackage(packageName) };
            var addPkgOutput = CommandLineHelper.RunCommand(addPkgCommands);

            // scrub data
            addPkgOutput = addPkgOutput.Substring(addPkgOutput.IndexOf(addPkgCommands[0]) + addPkgCommands[0].Length);

            if (addPkgOutput.ToLower().Contains(packageName.ToLower()))
            {
                cboxAdditionalPackages.SelectedIndex = 1;
                ControlDetails.AdditionalPackageIndex = 1;
            }
        }

        private void IdentifyAdditionalPackage_New()
        {
            // Identify additional package installs - for now only check for Fluent UI
            var packageName = Enum.AdditionalPackages().FirstOrDefault(p => p.Value == "Fluent UI").Key;
            string[] addPkgCommands = new string[] { Commands.Npm.CheckAdditionalPackage(packageName) };
            var addPkgOutput = CommandLineHelper.RunCommand(addPkgCommands);

            // scrub data
            addPkgOutput = addPkgOutput.Substring(addPkgOutput.IndexOf(addPkgCommands[0]) + addPkgCommands[0].Length);

            if (addPkgOutput.ToLower().Contains(packageName.ToLower()))
            {
                cboxAdditionalPackages.SelectedIndex = 1;
                ControlDetails.AdditionalPackageIndex = 1;
            }
        }

        private void IdentifySolutionDetails(bool suppressErrors = false)
        {
            DataverseSolutionDetails = new SolutionDetails();
            try
            {
                var start = DateTime.Now;

                // Check if .cdsproj does not already exists
                string solutionFolderPath = GetCorrectSolutionDirectory();

                if (!string.IsNullOrEmpty(solutionFolderPath))
                {
                    var solutionDirs = Directory.GetDirectories(solutionFolderPath);

                    if (solutionDirs != null && solutionDirs.Count() > 0)
                    {
                        var filteredControlDirs = solutionDirs.ToList().Where(l => (!l.ToLower().EndsWith("generated")));

                        if (filteredControlDirs.Count() == 0)
                        {
                            Routine_SolutionNotFound();
                        }

                        foreach (var item in filteredControlDirs)
                        {
                            var cdsprojName = Path.GetFileName(item);
                            var cdsprojExists = File.Exists(item + "\\" + cdsprojName + ".cdsproj");
                            if (cdsprojExists)
                            {
                                DataverseSolutionDetails.ProjectFilePath = item + "\\" + cdsprojName + ".cdsproj";
                                var solutionFile = File.Exists(item + "\\Other\\Solution.xml") ? item + "\\Other\\Solution.xml" : item + "\\src\\Other\\Solution.xml";
                                DataverseSolutionDetails.SolutionXMLFilePath = solutionFile;

                                XmlDocument xmlDoc = new XmlDocument();
                                xmlDoc.Load(DataverseSolutionDetails.SolutionXMLFilePath);

                                XmlNode uniqueSolutionNameNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/UniqueName");
                                DataverseSolutionDetails.UniqueName = uniqueSolutionNameNode.InnerText;

                                XmlNode localizedSolutionNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/LocalizedNames/LocalizedName[@languagecode='1033']");
                                DataverseSolutionDetails.FriendlyName = localizedSolutionNode.Attributes["description"].Value;

                                XmlNode uniquePublisherNameNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/Publisher/UniqueName");
                                DataverseSolutionDetails.Publisher.UniqueName = uniquePublisherNameNode.InnerText;

                                XmlNode localizedPublisherNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/Publisher/LocalizedNames/LocalizedName[@languagecode='1033']");
                                DataverseSolutionDetails.Publisher.FriendlyName = localizedPublisherNode.Attributes["description"].Value;

                                XmlNode customizationPrefixNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/Publisher/CustomizationPrefix");
                                DataverseSolutionDetails.Publisher.Prefix = customizationPrefixNode.InnerText;

                                XmlNode versionNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/Version");
                                DataverseSolutionDetails.Version = versionNode.InnerText;

                                XmlDocument solutionXMLFile = new XmlDocument();
                                solutionXMLFile.Load(DataverseSolutionDetails.ProjectFilePath);

                                var childNodes = solutionXMLFile["Project"].ChildNodes;

                                foreach (XmlNode node in childNodes)
                                {
                                    if (node.Name == "PropertyGroup")
                                    {
                                        var pgChildnodes = node.ChildNodes;
                                        foreach (XmlNode pgChild in pgChildnodes)
                                        {
                                            if (pgChild.Name == "SolutionPackageType")
                                            {
                                                switch (pgChild.InnerText)
                                                {
                                                    case "Unmanaged":
                                                        DataverseSolutionDetails.PackageType = SolutionPackageType.Unmanaged;
                                                        break;
                                                    case "Managed":
                                                        DataverseSolutionDetails.PackageType = SolutionPackageType.Managed;
                                                        break;
                                                    case "Both":
                                                        DataverseSolutionDetails.PackageType = SolutionPackageType.Both;
                                                        break;
                                                    default:
                                                        DataverseSolutionDetails.PackageType = SolutionPackageType.Unmanaged;
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }

                                DataverseSolutionDetails.FoundSolutionDetails = true;

                                break;
                            }
                        }
                    }
                }

                PopulateSolutionDetails();
                if (DataverseSolutionDetails.FoundSolutionDetails)
                {
                    Routine_SolutionFound();
                }
                else
                {
                    Routine_SolutionNotFound();
                }
                var end = DateTime.Now;
                var duration = end - start;
                LogEventMetrics("IdentifySolutionDetails", "ProcessingTime", duration.TotalMilliseconds);
            }
            catch (DirectoryNotFoundException dnex)
            {
                if (!suppressErrors)
                {
                    MessageBox.Show("Invalid directory. Could not retrieve existing CDS solution project details.");
                    LogException(dnex);
                }

                Routine_SolutionNotFound();
            }
            catch (Exception ex)
            {
                if (!suppressErrors)
                {
                    MessageBox.Show("An error occured while retrieving existing project details. Please report an issue on GitHub page.");
                    LogException(ex);
                }

                Routine_SolutionNotFound();
            }
        }

        private void PopulateControlDetails()
        {
            txtNamespace.Text = ControlDetails.Namespace;
            txtControlName.Text = ControlDetails.ControlName;
            txtControlDisplayName.Text = ControlDetails.ControlDisplayName;
            txtControlDescription.Text = ControlDetails.ControlDescription;
            txtComponentVersion.Text = ControlDetails.Version;

            cboxControlType.SelectedIndex = ControlDetails.IsVirtual ? 1 : 0;
            cboxTemplate.SelectedIndex = ControlDetails.IsDatasetTemplate ? 1 : 0;

            if (!string.IsNullOrEmpty(ControlDetails.PreviewImagePath))
            {
                lblPreviewImageExists.Text = Enum.ResourceExists(true, Enum.ResourceType.PreviewImage);
                lblPreviewImageExists.ForeColor = Color.ForestGreen;
            }

            if (ControlDetails.ExistsCSS)
            {
                lblCssFileExists.Text = Enum.ResourceExists(true, Enum.ResourceType.CSS);
                lblCssFileExists.ForeColor = Color.ForestGreen;
            }

            if (ControlDetails.ExistsResx)
            {
                lblResxFileExists.Text = Enum.ResourceExists(true, Enum.ResourceType.RESX);
                lblResxFileExists.ForeColor = Color.ForestGreen;
            }
        }

        private void PopulateSolutionDetails()
        {
            txtSolutionName.Text = DataverseSolutionDetails.UniqueName;
            txtSolutionFriendlyName.Text = DataverseSolutionDetails.FriendlyName;
            txtPublisherUniqueName.Text = DataverseSolutionDetails.Publisher.UniqueName;
            txtPublisherFriendlyName.Text = DataverseSolutionDetails.Publisher.FriendlyName;
            txtPublisherPrefix.Text = DataverseSolutionDetails.Publisher.Prefix;
            txtSolutionVersion.Text = DataverseSolutionDetails.Version;

            switch (DataverseSolutionDetails.PackageType)
            {
                case SolutionPackageType.Unmanaged:
                    radSolutionTypeUnmanaged.Checked = true;
                    break;
                case SolutionPackageType.Managed:
                    radSolutionTypeManaged.Checked = true;
                    break;
                case SolutionPackageType.Both:
                    radSolutionTypeBoth.Checked = true;
                    break;
                default:
                    radSolutionTypeUnmanaged.Checked = true;
                    break;
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

        public void CheckPacVersion(object worker, DoWorkEventArgs args)
        {
            var start = DateTime.Now;

            string[] commands = new string[] { Commands.Pac.Check() };
            var output = CommandLineHelper.RunCommand(commands);

            StringHelper stringer = new StringHelper();
            PacVersionParsedDetails outputParsedPacDetails = stringer.ParsePacVersionOutput(output);

            if (!outputParsedPacDetails.CLINotFound)
            {
                if (!outputParsedPacDetails.UnableToDetectCLIVersion)
                {
                    if (outputParsedPacDetails.ContainsLatestVersionNotification &&
                        DialogResult.Yes == MessageBox.Show("New version of Power Apps CLI is available. Do you want to update it now?", "PCF Power Apps Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        string pacUpdateCLI = Commands.Pac.InstallLatest();
                        RunCommandLine(pacUpdateCLI);
                    }
                    lblPCFCLIVersionMsg.Text = "Power Apps CLI Version: " + outputParsedPacDetails.CurrentVersion;
                }
                else
                {
                    lblPCFCLIVersionMsg.Text = "Unable to detect Power Apps CLI version";
                }
            }
            else
            {
                lblPCFCLIVersionMsg.Text = "Power Apps CLI Not Detected";
                Invoke(new Action(() =>
                {
                    ShowErrorNotification("Power Apps CLI not detected on this machine. Please download it and restart the tool.", new Uri("https://aka.ms/PowerAppsCLI"));
                }));
                var downloadPACLIResult = MessageBox.Show("Power Apps CLI not detected on this machine. Do you want to download it?", "Power Apps CLI - Not Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (downloadPACLIResult == DialogResult.Yes)
                {
                    Process.Start("https://aka.ms/PowerAppsCLI");
                }
            }

            var end = DateTime.Now;
            var duration = end - start;
            LogEventMetrics("CheckPacVersion", "ProcessingTime", duration.TotalMilliseconds);
        }

        private void CheckNpmVersion(object worker, DoWorkEventArgs args)
        {
            var start = DateTime.Now;
            string[] commands = new string[] { Commands.Npm.Version() };
            var output = CommandLineHelper.RunCommand(commands);

            string currentNpmVersion = string.Empty;

            if (!string.IsNullOrEmpty(output) && output.ToLower().Contains("npm: "))
            {
                currentNpmVersion = output.Substring(output.IndexOf("npm: ") + 6, output.IndexOf(",", output.IndexOf("npm: ") + 6) - (output.IndexOf("npm: ") + 6) - 1);
                lblnpmVersionMsg.Text = "npm Version: " + currentNpmVersion.Trim();

                // Check if pcf-generator is installed
                string[] pcfGCommands = new string[] { Commands.Npm.CheckPcfGenerator() };
                var pcfGOutput = CommandLineHelper.RunCommand(pcfGCommands);

                // scrub data
                pcfGOutput = pcfGOutput.Substring(pcfGOutput.IndexOf(pcfGCommands[0]) + pcfGCommands[0].Length);

                if (pcfGOutput.ToLower().Contains("generator-pcf"))
                {
                    var pcfGVersionStr = pcfGOutput.Substring(pcfGOutput.IndexOf("generator-pcf@") + 14, 5);
                    Version pcfGInstalledVersion = Version.Parse(pcfGVersionStr);
                    Version pcfGMinimumVersion = Version.Parse("1.2.5");

                    if (pcfGInstalledVersion < pcfGMinimumVersion)
                    {
                        string[] updateCommands = new string[] { Commands.Npm.UpdatePcfGenerator() };
                        CommandLineHelper.RunCommand(updateCommands);

                        Invoke(new Action(() =>
                        {
                            ShowInfoNotification("PCF Generator was updated to latest version.", new Uri("https://www.npmjs.com/package/generator-pcf"));
                        }));
                    }
                }
                else
                {
                    string[] installCommands = new string[] { Commands.Npm.InstallYo(), Commands.Npm.InstallPcfGenerator() };
                    CommandLineHelper.RunCommand(installCommands);

                    Invoke(new Action(() =>
                    {
                        ShowInfoNotification("PCF Generator package was installed on this machine.", new Uri("https://www.npmjs.com/package/generator-pcf"));
                    }));
                }
            }
            else
            {
                lblnpmVersionMsg.Text = "npm Not Detected";
                Invoke(new Action(() =>
                {
                    ShowInfoNotification("npm not detected on this machine. Please download it.", new Uri("https://nodejs.org/en/"));
                }));
            }
            var end = DateTime.Now;
            var duration = end - start;
            LogEventMetrics("CheckNpmVersion", "ProcessingTime", duration.TotalMilliseconds);
        }

        private string FindMsBuildPath()
        {
            string msBuildPath = string.Empty;

            if (!string.IsNullOrEmpty(pluginSettings.MsBuildLocation))
            {
                msBuildPath = pluginSettings.MsBuildLocation;
            }
            else
            {
                string[] commands = new string[] { Commands.Cmd.SetCustomExecutionPolicyWrapped(pluginSettings.CustomExecutionPolicy), Commands.Cmd.FindMsBuild(), Commands.Cmd.DefaultExecutionPolicy() };
                var output = CommandLineHelper.RunCommand(commands);

                if (!string.IsNullOrEmpty(output) && output.ToLower().Contains("msbuild.ps1"))
                {
                    msBuildPath = output.Substring(output.IndexOf("msbuild.ps1")).Split('\r')[1].Trim();
                }

                if (msBuildPath.Equals("Unable to find msbuild", StringComparison.InvariantCultureIgnoreCase))
                {
                    MessageBox.Show(msBuildPath + ". Please install MsBuild or Visual Studio.", "MsBuild Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    msBuildPath = string.Empty;

                }

                pluginSettings.MsBuildLocation = msBuildPath;
            }

            return msBuildPath;
        }

        private void IncrementComponentVersion()
        {
            var start = DateTime.Now;
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
                                    var indexExists = VerifyCodeFileExists(item); //File.Exists(item + "\\" + "index.ts");
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
                    Telemetry.TrackException(dnex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while updating the version. Please report an issue on GitHub page.");
                    Telemetry.TrackException(ex);
                }
            }
            var end = DateTime.Now;
            var duration = end - start;
            LogEventMetrics("IncrementComponentVersion", "ProcessingTime", duration.TotalMilliseconds);
        }

        private void IncrementSolutionVersion()
        {
            var start = DateTime.Now;
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
                        string solutionFolderPath = GetCorrectSolutionDirectory();

                        if (!string.IsNullOrEmpty(solutionFolderPath))
                        {
                            var solutionDirs = Directory.GetDirectories(solutionFolderPath);

                            if (solutionDirs != null && solutionDirs.Count() > 0)
                            {
                                var filteredControlDirs = solutionDirs.ToList().Where(l => (!l.ToLower().EndsWith("generated")));
                                foreach (var item in filteredControlDirs)
                                {
                                    var cdsprojName = Path.GetFileName(item);
                                    var cdsprojExists = File.Exists(item + "\\" + cdsprojName + ".cdsproj");
                                    if (cdsprojExists)
                                    {
                                        txtSolutionName.Text = cdsprojName;
                                        var solutionFile = File.Exists(item + "\\Other\\Solution.xml") ? item + "\\Other\\Solution.xml" : item + "\\src\\Other\\Solution.xml";

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
                }
                catch (DirectoryNotFoundException dnex)
                {
                    MessageBox.Show("Invalid directory. Could not retrieve existing PCF project.");
                    Telemetry.TrackException(dnex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while updating the version. Please report an issue on GitHub page.");
                    Telemetry.TrackException(ex);
                }
            }
            var end = DateTime.Now;
            var duration = end - start;
            LogEventMetrics("IncrementSolutionVersion", "ProcessingTime", duration.TotalMilliseconds);
        }

        private void SanitizeSolutionDetails()
        {
            var start = DateTime.Now;
            if (chkIncrementSolutionVersion.Checked)
            {
                try
                {
                    var mainDirs = Directory.GetDirectories(txtWorkingFolder.Text);

                    if (!string.IsNullOrEmpty(txtControlName.Text))
                    {
                        // Check if .cdsproj does not already exists
                        string solutionFolderPath = GetCorrectSolutionDirectory();

                        if (!string.IsNullOrEmpty(solutionFolderPath))
                        {
                            var solutionDirs = Directory.GetDirectories(solutionFolderPath);

                            if (solutionDirs != null && solutionDirs.Count() > 0)
                            {
                                var filteredControlDirs = solutionDirs.ToList().Where(l => (!l.ToLower().EndsWith("generated")));
                                foreach (var item in filteredControlDirs)
                                {
                                    var cdsprojName = Path.GetFileName(item);
                                    var cdsprojExists = File.Exists(item + "\\" + cdsprojName + ".cdsproj");
                                    if (cdsprojExists)
                                    {
                                        var solutionFile = File.Exists(item + "\\Other\\Solution.xml") ? item + "\\Other\\Solution.xml" : item + "\\src\\Other\\Solution.xml";

                                        XmlDocument xmlDoc = new XmlDocument();
                                        xmlDoc.Load(solutionFile);

                                        // Solution Name Cleaning
                                        XmlNode uniqueSolutionNameNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/UniqueName");
                                        XmlNode localizedSolutionNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/LocalizedNames/LocalizedName[@languagecode='1033']");

                                        Regex rgx1 = new Regex(@"[^a-zA-Z0-9_]");
                                        uniqueSolutionNameNode.InnerText = rgx1.Replace(txtSolutionName.Text, "");

                                        if (localizedSolutionNode != null)
                                        {
                                            localizedSolutionNode.Attributes["description"].Value = txtSolutionFriendlyName.Text;
                                        }
                                        else
                                        {
                                            XmlNode temp = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/LocalizedNames");
                                            XmlNode tempLocalizedNode = xmlDoc.CreateNode(XmlNodeType.Element, "LocalizedName", "");
                                            XmlAttribute tempDescription = xmlDoc.CreateAttribute("description");
                                            tempDescription.Value = txtSolutionFriendlyName.Text;
                                            XmlAttribute tempLangCode = xmlDoc.CreateAttribute("languagecode");
                                            tempLangCode.Value = "1033";

                                            tempLocalizedNode.Attributes.Append(tempDescription);
                                            tempLocalizedNode.Attributes.Append(tempLangCode);

                                            temp.AppendChild(tempLocalizedNode);
                                        }

                                        // Publisher Cleaning
                                        XmlNode uniquePublisherNameNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/Publisher/UniqueName");
                                        XmlNode localizedPublisherNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/Publisher/LocalizedNames/LocalizedName[@languagecode='1033']");

                                        Regex rgx2 = new Regex(@"[^a-zA-Z0-9_]");
                                        uniquePublisherNameNode.InnerText = rgx2.Replace(txtPublisherUniqueName.Text, "").ToLower();

                                        if (localizedPublisherNode != null)
                                        {
                                            localizedPublisherNode.Attributes["description"].Value = txtPublisherFriendlyName.Text;
                                        }
                                        else
                                        {
                                            XmlNode temp = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/Publisher/LocalizedNames");
                                            XmlNode tempLocalizedNode = xmlDoc.CreateNode(XmlNodeType.Element, "LocalizedName", "");
                                            XmlAttribute tempDescription = xmlDoc.CreateAttribute("description");
                                            tempDescription.Value = txtPublisherFriendlyName.Text;
                                            XmlAttribute tempLangCode = xmlDoc.CreateAttribute("languagecode");
                                            tempLangCode.Value = "1033";

                                            tempLocalizedNode.Attributes.Append(tempDescription);
                                            tempLocalizedNode.Attributes.Append(tempLangCode);

                                            temp.AppendChild(tempLocalizedNode);
                                        }

                                        xmlDoc.Save(solutionFile);

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
                    Telemetry.TrackException(dnex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while updating the version. Please report an issue on GitHub page.");
                    Telemetry.TrackException(ex);
                }
            }
            var end = DateTime.Now;
            var duration = end - start;
            LogEventMetrics("IncrementSolutionVersion", "ProcessingTime", duration.TotalMilliseconds);
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
            string solutionFolder = string.Concat(GetCorrectSolutionDirectory(), $"\\{txtSolutionName.Text}");
            string solutionFileLocation = $"{solutionFolder}\\bin\\{(radReleaseTypeProd.Checked ? "release" : "debug")}\\{txtSolutionName.Text}.zip";

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
                    var start = DateTime.Now;

                    ImportSolutionRequest impSolReq = new ImportSolutionRequest()
                    {
                        CustomizationFile = fileBytes
                    };
                    args.Result = Service.Execute(impSolReq);

                    var end = DateTime.Now;
                    var duration = end - start;
                    LogEventMetrics("ImportSolutionInD365CE", "ProcessingTime", duration.TotalMilliseconds);
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
                    var start = DateTime.Now;

                    var publishRequest = new PublishAllXmlRequest();
                    args.Result = Service.Execute(publishRequest);

                    var end = DateTime.Now;
                    var duration = end - start;
                    LogEventMetrics("PublishAll", "ProcessingTime", duration.TotalMilliseconds);
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
            ExecutionStatus = status;
            lblStatus.Location = new Point(picRunning.Location.X, lblStatus.Location.Y);
            switch (status)
            {
                case ProcessingStatus.Succeeded:
                    lblStatus.Text = "Succeeded";
                    lblStatus.ForeColor = Color.LimeGreen;
                    picRunning.Visible = false;
                    break;
                case ProcessingStatus.Failed:
                    lblStatus.Text = "Failed";
                    lblStatus.ForeColor = Color.DarkRed;
                    picRunning.Visible = false;
                    break;
                case ProcessingStatus.NeedReview:
                    lblStatus.Text = "Need Review";
                    lblStatus.ForeColor = Color.DarkMagenta;
                    break;
                case ProcessingStatus.Running:
                    lblStatus.Text = "Running";
                    lblStatus.ForeColor = Color.DarkOrange;
                    picRunning.Visible = true;
                    lblStatus.Location = new Point(picRunning.Location.X + picRunning.Width + 10, lblStatus.Location.Y);
                    break;
                case ProcessingStatus.Undetermined:
                    lblStatus.Text = "Undetermined";
                    lblStatus.ForeColor = Color.Gray;
                    picRunning.Visible = false;
                    break;
                case ProcessingStatus.Complete:
                default:
                    lblStatus.Text = string.Empty;
                    lblStatus.ForeColor = Color.Black;
                    picRunning.Visible = false;
                    break;
            }
        }

        private void ParseProfileList(string output)
        {
            var start = DateTime.Now;

            try
            {
                if (output.ToLower().Contains("[1]"))
                {
                    // Split on \r\n
                    char[] mainDelimiterChars = { '[' };
                    string[] mainSplit = output.Split(mainDelimiterChars);

                    List<AuthenticationProfile> lstProfiles = new List<AuthenticationProfile>();

                    foreach (var list in mainSplit)
                    {
                        if (Regex.IsMatch(list, @"^\d"))
                        {
                            string[] innerSplit = list.Trim().Split(' ');
                            innerSplit = innerSplit.Where(isp => !string.IsNullOrEmpty(isp)).ToArray();

                            if (innerSplit.Length > 0)
                            {
                                AuthenticationProfile profile = new AuthenticationProfile();

                                if (innerSplit[1] == "*")
                                {
                                    Regex rgxIndex = new Regex("(\\[*\\]*)");
                                    profile.Index = int.Parse(rgxIndex.Replace(innerSplit[0].Trim(), ""));
                                    profile.EnvironmentType = innerSplit[2].Trim();
                                    profile.EnvironmentUrl = innerSplit[3].Trim();

                                    var username = innerSplit[5].Trim();
                                    profile.UserName = username.EndsWith("PS") ? username.Substring(0, username.Length - 2) : username;
                                    profile.IsCurrent = true;
                                }
                                else
                                {

                                    Regex rgxIndex = new Regex("(\\[*\\]*)");
                                    profile.Index = int.Parse(rgxIndex.Replace(innerSplit[0].Trim(), ""));
                                    profile.EnvironmentType = innerSplit[1].Trim();
                                    profile.EnvironmentUrl = innerSplit[2].Trim();

                                    var username = innerSplit[4].Trim();
                                    profile.UserName = username.EndsWith("PS") ? username.Substring(0, username.Length - 2) : username;
                                    profile.IsCurrent = false;
                                }

                                lstProfiles.Add(profile);
                            }
                        }
                    }

                    // Show List Form
                    AuthenticationProfileForm frmProfile = new AuthenticationProfileForm(lstProfiles);
                    frmProfile.StartPosition = FormStartPosition.CenterScreen;
                    frmProfile.ParentControl = this;
                    frmProfile.ShowDialog();

                    // Perform Delete or Switch if selected
                    switch (SelectedProfileAction)
                    {
                        case AuthProfileAction.SwitchCurrent:
                            string switchProfile = Commands.Pac.SwitchCurrentProfile(SelectedProfileIndex);
                            RunCommandLine(switchProfile);
                            break;
                        case AuthProfileAction.Delete:
                            string deleteProfile = Commands.Pac.DeleteProfile(SelectedProfileIndex);
                            RunCommandLine(deleteProfile);
                            break;
                        default:
                            break;
                    }
                }
                else if (output.ToLower().Contains("no profiles were found on this computer"))
                {
                    MessageBox.Show("No profiles were found on this computer.", "Profile List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                MessageBox.Show("An error occured while parsing the Authentication Profile list. Please report an issue on GitHub page.");
                Telemetry.TrackException(ex);
            }

            var end = DateTime.Now;
            var duration = end - start;
            LogEventMetrics("ParseProfileList", "ProcessingTime", duration.TotalMilliseconds);
        }

        private void CheckDefaultAuthenticationProfile(object worker, DoWorkEventArgs args)
        {
            string[] commands = new string[] { Commands.Pac.OrgDetails() };
            var output = CommandLineHelper.RunCommand(commands);

            if (!string.IsNullOrEmpty(output))
            {
                StringHelper helper = new StringHelper();
                lblCurrentProfile.Text = helper.ParseOrgDetails(output);
            }
        }

        private void LoadSolutions()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading Solutions... Please wait.",
                Work = (worker, args) =>
                {
                    var start = DateTime.Now;

                    args.Result = DataverseHelper.RetrieveSolutions(Service);

                    var end = DateTime.Now;
                    var duration = end - start;
                    LogEventMetrics("LoadSolutions", "ProcessingTime", duration.TotalMilliseconds);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var result = args.Result as EntityCollection;
                        if (result != null && result.Entities != null)
                        {
                            _solutionsCache = result;

                            var solutionListQuery = from entity in _solutionsCache.Entities
                                                    select (new EntityDetails
                                                    {
                                                        DisplayText = entity.GetAttributeValue<string>("friendlyname"),
                                                        MetaData = entity
                                                    });

                            var solutionList = solutionListQuery.ToList();
                            //solutionList.Add(new ComboListItem { DisplayText = "---Select---", MetaData = null });
                            solutionList.Sort((x, y) => string.Compare(x.DisplayText, y.DisplayText, StringComparison.Ordinal));

                            Routine_ExistingSolution();
                            cboxSolutions.DisplayMember = "DisplayText";
                            cboxSolutions.DataSource = solutionList;
                            cboxSolutions.DroppedDown = true;
                        }
                    }

                }
            });
        }

        private void LoadPublishers()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading Publishers... Please wait.",
                Work = (worker, args) =>
                {
                    var start = DateTime.Now;

                    args.Result = DataverseHelper.RetrievePublishers(Service);

                    var end = DateTime.Now;
                    var duration = end - start;
                    LogEventMetrics("LoadPublishers", "ProcessingTime", duration.TotalMilliseconds);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var result = args.Result as EntityCollection;
                        if (result != null && result.Entities != null)
                        {
                            _publishersCache = result;

                            var publisherListQuery = from entity in _publishersCache.Entities
                                                     select (new EntityDetails
                                                     {
                                                         DisplayText = entity.GetAttributeValue<string>("friendlyname"),
                                                         MetaData = entity
                                                     });

                            var publisherList = publisherListQuery.ToList();
                            //solutionList.Add(new ComboListItem { DisplayText = "---Select---", MetaData = null });
                            publisherList.Sort((x, y) => string.Compare(x.DisplayText, y.DisplayText, StringComparison.Ordinal));

                            Routine_ExistingPublisher();
                            cboxPublishers.DisplayMember = "DisplayText";
                            cboxPublishers.DataSource = publisherList;
                            cboxPublishers.DroppedDown = true;
                        }
                    }

                }
            });
        }

        private void ExportAndUnpackSolutions(string solutionName, string exportToPath)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Exporting Solution '{solutionName}'... Please wait.",
                Work = (worker, args) =>
                {
                    var start = DateTime.Now;

                    args.Result = DataverseHelper.ExportSolution(Service, solutionName);

                    var end = DateTime.Now;
                    var duration = end - start;
                    LogEventMetrics("ExportSolutions", "ProcessingTime", duration.TotalMilliseconds);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var result = args.Result as byte[];
                        string filename = solutionName + ".zip";
                        File.WriteAllBytes($"{exportToPath}\\{filename}", result);

                        // Unpack solution in the src folder
                        string unpack = Commands.SolutionPackager.UnpackSolution($"{exportToPath}\\{filename}", $"{exportToPath}\\src");
                        RunCommandLine(unpack);

                        // Initialize CDS Solution Project
                        string pacCommand_CreateSolution = Commands.Pac.SolutionInit(txtPublisherUniqueName.Text, txtPublisherPrefix.Text);
                        RunCommandLine(pacCommand_CreateSolution);

                        // Add component reference
                        string pacCommand_AddComponent = Commands.Pac.SolutionAddReference(txtWorkingFolder.Text);
                        RunCommandLine(pacCommand_AddComponent);
                    }
                }
            });
        }

        private string GetCorrectSolutionDirectory()
        {
            string v1Dir = $"{txtWorkingFolder.Text}\\{txtControlName.Text}";
            string v2Dir = $"{txtWorkingFolder.Text}\\Solutions";
            string v3Dir = $"{txtWorkingFolder.Text}\\{SolutionFolderName}";

            bool v1Exists = Directory.Exists(v1Dir);
            bool v2Exists = Directory.Exists(v2Dir);
            bool v3Exists = Directory.Exists(v3Dir);

            string correctSolutionDirectory = v3Exists ? v3Dir : v2Exists ? v2Dir : v1Dir;

            return correctSolutionDirectory;
        }

        private void CreateTemplateFiles(object worker, DoWorkEventArgs args)
        {
            string templateFolder = FindTemplateFolder();

            // Stylesheet
            string cssDir = $"{txtWorkingFolder.Text}\\{txtControlName.Text}\\css";
            if (!Directory.Exists(cssDir))
            {
                Directory.CreateDirectory(cssDir);
            }
            if (!File.Exists($"{cssDir}\\{txtControlName.Text}.css"))
            {
                File.Copy($"{templateFolder}\\sampleStyle.css", $"{cssDir}\\{txtControlName.Text}.css");
                FindAndReplaceTemplateFile($"{cssDir}\\{txtControlName.Text}.css");
                ControlDetails.ExistsCSS = true;
            }

            // Manifest
            string manifestDir = $"{txtWorkingFolder.Text}\\{txtControlName.Text}";
            if (File.Exists($"{manifestDir}\\ControlManifest.Input.xml"))
            {
                string manifestTemplate = "sampleManifestField.xml"; // Field is default
                if (cboxTemplate.SelectedIndex == 1)
                {
                    manifestTemplate = "sampleManifestDataset.xml";
                }
                File.Copy($"{templateFolder}\\{manifestTemplate}", $"{manifestDir}\\ControlManifest.Input.xml", true);
                FindAndReplaceTemplateFile($"{manifestDir}\\ControlManifest.Input.xml");
            }

            // Preview image
            string imgDir = $"{txtWorkingFolder.Text}\\{txtControlName.Text}\\img";
            if (!Directory.Exists(imgDir))
            {
                Directory.CreateDirectory(imgDir);
            }
            if (!File.Exists($"{imgDir}\\preview.png"))
            {
                File.Copy($"{templateFolder}\\samplePowerUp.png", $"{imgDir}\\preview.png");
                ControlDetails.PreviewImagePath = $"{imgDir}\\preview.png";
            }

            // Resx
            string resxDir = $"{txtWorkingFolder.Text}\\{txtControlName.Text}\\strings";
            if (!Directory.Exists(resxDir))
            {
                Directory.CreateDirectory(resxDir);
            }
            if (!File.Exists($"{resxDir}\\{txtControlName.Text}.resx"))
            {
                File.Copy($"{templateFolder}\\sampleLocalization.resx", $"{resxDir}\\{txtControlName.Text}.1033.resx");
                FindAndReplaceTemplateFile($"{resxDir}\\{txtControlName.Text}.1033.resx");
                ControlDetails.ExistsResx = true;
            }
            IdentifyControlDetails();
            ControlDetails = ControlManifest.UpdateControlDetails(ControlDetails, txtControlDisplayName.Text, txtControlDescription.Text);
            PopulateControlDetails();
        }

        private string FindTemplateFolder()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "\\" + TEMPLATE_LOCATION;
        }

        private void FindAndReplaceTemplateFile(string fullFilePath)
        {
            string replacedText = File.ReadAllText(fullFilePath);
            replacedText = replacedText.Replace("<%= controlNamespace %>", txtNamespace.Text);
            replacedText = replacedText.Replace("<%= controlName %>", txtControlName.Text);
            File.WriteAllText(fullFilePath, replacedText);
        }

        #endregion

        #region Constructors

        public PCFBuilder()
        {
            InitializeComponent();
            //var theme = new VS2015LightTheme();
            //dockContainer.Theme = theme;
            //dockContainer.Theme.Skin.DockPaneStripSkin.TextFont = Font;
        }

        #endregion

        #region Private Event Handlers

        private void PCFBuilder_Load(object sender, EventArgs e)
        {
            //ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));
            var start = DateTime.Now;

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out pluginSettings))
            {
                pluginSettings = new Settings();
                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                txtWorkingFolder.Text = pluginSettings.WorkingDirectoryLocation;
                LogInfo("Settings found and loaded");
            }

            Dictionary<string, string> additionalPkgSource = Enum.AdditionalPackages();
            cboxAdditionalPackages.DataSource = new BindingSource(additionalPkgSource, null);
            cboxAdditionalPackages.DisplayMember = "Value";
            cboxAdditionalPackages.ValueMember = "Key";
            picRunning.Image = Properties.Resources.running;

            _mainPluginLocalWorker = new BackgroundWorker();
            _mainPluginLocalWorker.DoWork += CheckPacVersion;
            _mainPluginLocalWorker.DoWork += CheckNpmVersion;
            _mainPluginLocalWorker.DoWork += CheckDefaultAuthenticationProfile;
            _mainPluginLocalWorker.RunWorkerAsync();

            StatusCheckExecution = false;
            ResxCheckExecution = false;
            BuildDeployExecution = false;
            ListProfileExecution = false;
            CurrentCommandOutput = string.Empty;

            var isValid = IsControlLocationPopulated(true);

            if (isValid)
            {
                CommandPromptInitialized = true;
                InitCommandLine();
                IdentifyControlDetails();
                PopulateControlDetails();
                Routine_EditComponent();
                LoadMostRecentUsedLocations();
            }
            else
            {
                CommandPromptInitialized = false;
            }

            var stop = DateTime.Now;
            var duration = stop - start;
            LogEventMetrics("Load", "LoadTime", duration.TotalMilliseconds);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            LogEvent("Close");
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
            var isValid = IsControlLocationPopulated(true);

            if (isValid)
            {
                if (!CommandPromptInitialized)
                {
                    InitCommandLine();
                }
                Routine_NewComponent();
                TryLoadPCFDetailsFromSettings();
                TryLoadCDSDetailsFromSettings();
            }
        }

        private void TsmNewPCFTemplate_Click(object sender, EventArgs e)
        {
            var isValid = IsControlLocationPopulated(true);

            if (isValid)
            {
                if (!CommandPromptInitialized)
                {
                    InitCommandLine();
                }

                var ctrlTemplates = new TemplatesForm(txtWorkingFolder.Text);
                ctrlTemplates.StartPosition = FormStartPosition.CenterScreen;
                ctrlTemplates.ParentControl = this;
                ctrlTemplates.ShowDialog();

                if (SelectedTemplate == null)
                {
                    return;
                }

                IdentifyControlDetails();
                PopulateControlDetails();

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
            var isValid = IsControlLocationPopulated(true);

            if (isValid)
            {
                if (!CommandPromptInitialized)
                {
                    InitCommandLine();
                }

                IdentifyControlDetails();
                PopulateControlDetails();
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
            System.Diagnostics.Process.Start("https://twitter.com/PCFBuilder");
        }

        private void TspSettings_Click(object sender, EventArgs e)
        {
            var ctrlSettingsForm = new SettingsForm(pluginSettings);
            ctrlSettingsForm.StartPosition = FormStartPosition.CenterScreen;
            ctrlSettingsForm.ShowDialog();
        }

        private void TxtWorkingFolder_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWorkingFolder.Text))
            {
                pluginSettings.WorkingDirectoryLocation = txtWorkingFolder.Text;
                MostRecentlyUsedLocations.Instance.Items.Add(new MostRecentlyUsedLocation(txtWorkingFolder.Text));
                MostRecentlyUsedLocations.Instance.Save();
            }
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

                RunCommandLine(cdWorkingDir, npmBuildCommand);

                Process.Start("cmd", $"/K {cdWorkingDir} && {npmTestCommand} && exit");
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

                var msbuild_filepath = FindMsBuildPath();

                string controlName = txtControlName.Text;
                var solutionPath = string.Concat(GetCorrectSolutionDirectory(), $"\\{txtSolutionName.Text}");

                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{controlName}");
                string npmBuildCommand = Commands.Npm.RunBuild();

                string cdMsBuildDir = Commands.Cmd.ChangeDirectory($"{msbuild_filepath}");
                string msbuild_restore = Commands.Msbuild.Restore(solutionPath);
                string msbuild_rebuild = radReleaseTypeProd.Checked ? Commands.Msbuild.RebuildRelease(solutionPath) : Commands.Msbuild.Rebuild(solutionPath);

                RunCommandLine(cdWorkingDir, npmBuildCommand, cdMsBuildDir, msbuild_restore, msbuild_rebuild);
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

                var msbuild_filepath = FindMsBuildPath();

                string controlName = txtControlName.Text;
                var solutionPath = string.Concat(GetCorrectSolutionDirectory(), $"\\{txtSolutionName.Text}");

                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}\\{controlName}");
                string npmBuildCommand = Commands.Npm.RunBuild();

                string cdMsBuildDir = Commands.Cmd.ChangeDirectory($"{msbuild_filepath}");
                string msbuild_rebuild = radReleaseTypeProd.Checked ? Commands.Msbuild.RebuildRelease(solutionPath) : Commands.Msbuild.Rebuild(solutionPath);

                BuildDeployExecution = true;
                RunCommandLine(cdWorkingDir, npmBuildCommand, cdMsBuildDir, msbuild_rebuild);
            }
        }

        private void BtnCreateComponent_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

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
                string additionalPackage = string.Empty;

                if (((KeyValuePair<string, string>)cboxAdditionalPackages.SelectedItem).Value != "None")
                {
                    switch (((KeyValuePair<string, string>)cboxAdditionalPackages.SelectedItem).Value)
                    {
                        case "Fluent UI":
                            additionalPackage = Commands.Npm.Install(((KeyValuePair<string, string>)cboxAdditionalPackages.SelectedItem).Key);
                            break;
                        default:
                            break;
                    }
                }

                ReloadDetails = true;
                if ((string)cboxControlType.SelectedItem == "Virtual")
                {
                    string pacCommandVirtual = Commands.Pac.PcfInitVirtual(txtNamespace.Text, txtControlName.Text, cboxTemplate.SelectedItem.ToString());
                    RunCommandLine(cdWorkingDir, pacCommandVirtual, npmCommand);
                    
                }
                else
                {
                    if (string.IsNullOrEmpty(additionalPackage))
                    {
                        RunCommandLine(cdWorkingDir, pacCommand, npmCommand);
                    }
                    else
                    {
                        RunCommandLine(cdWorkingDir, pacCommand, npmCommand, additionalPackage);
                    }
                }
                

                // Create VS Code Workspace file

            }
        }

        private void BtnOpenInVSCode_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                string vscodeopen = $"code \"{txtWorkingFolder.Text}\"";

                RunCommandLine(vscodeopen);
            }
        }

        private void BtnBuildComponent_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            if (areMainControlsValid)
            {
                IncrementComponentVersion();
                BuildPCFProject();
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
                //RunCommandLine(cdWorkingDir, npmCommand);
                Process.Start("cmd", $"/K {cdWorkingDir} && {npmCommand} && exit");
            }
        }

        private void BtnClearConsole_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("This will clear the console and logs will be lost. Do you want to proceed?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                consoleControl.ClearOutput();
                lblStatus.Text = string.Empty;
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
                var msbuild_filepath = FindMsBuildPath();

                IncrementSolutionVersion();

                string cdMsBuildDir = Commands.Cmd.ChangeDirectory($"{msbuild_filepath}");

                var projectPath = string.Concat(GetCorrectSolutionDirectory(), $"\\{txtSolutionName.Text}");
                string msbuild_restore = Commands.Msbuild.Restore(projectPath);
                string msbuild_build = radReleaseTypeProd.Checked ? Commands.Msbuild.RebuildRelease(projectPath) : Commands.Msbuild.Rebuild(projectPath);

                RunCommandLine(cdMsBuildDir, msbuild_restore, msbuild_build);
            }
        }

        private void BtnCreateSolution_Click(object sender, EventArgs e)
        {
            var isValid = AreSolutionDetailsPopulated();

            if (isValid)
            {
                lblErrors.Text = string.Empty;
                ReloadDetails = true;

                string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}");
                string mkdirSolutionFolder = Commands.Cmd.MakeDirectory(SolutionFolderName);
                string cdSolutionDir = Commands.Cmd.ChangeDirectory(SolutionFolderName);

                string mkdirDeploymentFolder = Commands.Cmd.MakeDirectory(txtSolutionName.Text);
                string cdDeploymentFolder = Commands.Cmd.ChangeDirectory($"{txtSolutionName.Text}");

                if (chkUseExistingSolution.Checked)
                {
                    RunCommandLine(cdWorkingDir, mkdirSolutionFolder, cdSolutionDir, mkdirDeploymentFolder, cdDeploymentFolder);

                    string outputDir = $"{txtWorkingFolder.Text}\\{SolutionFolderName}\\{txtSolutionName.Text}";
                    ExportAndUnpackSolutions(txtSolutionName.Text, outputDir);
                }
                else
                {
                    string pacCommand_CreateSolution = Commands.Pac.SolutionInit(txtPublisherUniqueName.Text, txtPublisherPrefix.Text);
                    string pacCommand_AddComponent = Commands.Pac.SolutionAddReference(txtWorkingFolder.Text);
                    RunCommandLine(cdWorkingDir, mkdirSolutionFolder, cdSolutionDir, mkdirDeploymentFolder, cdDeploymentFolder, pacCommand_CreateSolution, pacCommand_AddComponent);
                }
            }
        }

        private void BtnRefreshDetails_Click(object sender, EventArgs e)
        {
            var isValid = IsControlLocationPopulated(false);

            if (isValid)
            {
                IdentifyControlDetails();
                PopulateControlDetails();
                Routine_EditComponent();
            }
        }

        private void BtnOpenControlInExplorer_Click(object sender, EventArgs e)
        {
            var isValid = IsControlLocationPopulated(true);

            if (isValid)
            {
                Process.Start("explorer.exe", txtWorkingFolder.Text);
            }
        }

        private void ConsoleControl_OnConsoleOutput(object sender, ConsoleControl.ConsoleEventArgs args)
        {
            SetProcessingStatus(ProcessingStatus.Running);
            StatusCheckExecution = true;

            CurrentCommandOutput += args.Content.Trim();

            // This indicates end of execution
            if (args.Content.Trim().Contains("PS C:\\") && args.Content.Trim().EndsWith(">"))
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

                            if (CurrentCommandOutput.ToLower().Contains(Commands.Npm.RunBuild()) &&
                                (CurrentCommandOutput.ToLower().Contains("'pcf-scripts' is not recognized as an internal or external command")
                                    || CurrentCommandOutput.ToLower().Contains("error: cannot find module")))
                            {
                                var response = MessageBox.Show("Packages not installed. Do you want to install all dependent packages?", "Missing Package Dependencies", MessageBoxButtons.YesNo);
                                if (response.Equals(DialogResult.Yes))
                                {
                                    string npmInstall = Commands.Npm.Install();
                                    string npmBuild = Commands.Npm.RunBuild();
                                    RunCommandLine(npmInstall, npmBuild);
                                }
                            }
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
                        || CurrentCommandOutput.ToLower().Contains($"cds solution project with name '{txtSolutionName.Text.ToLower()}' created successfully")))
                {
                    if (CurrentCommandOutput.ToLower().Contains("the powerapps component framework project was successfully created"))
                    {
                        IdentifyControlDetails();

                        _mainPluginLocalWorker = new BackgroundWorker();
                        _mainPluginLocalWorker.DoWork += CreateTemplateFiles;
                        _mainPluginLocalWorker.RunWorkerAsync();
                    }

                    if (CurrentCommandOutput.ToLower().Contains($"cds solution project with name '{txtSolutionName.Text.ToLower()}' created successfully"))
                    {
                        if (!chkUseExistingSolution.Checked)
                        {
                            SanitizeSolutionDetails();
                        }
                        IdentifySolutionDetails();
                        Solution.UpdateSolutionPackageType(DataverseSolutionDetails);
                        PopulateSolutionDetails();
                    }

                    // Reset
                    ReloadDetails = false;
                }

                if (ListProfileExecution)
                {
                    ParseProfileList(CurrentCommandOutput);
                }

                if (ResxCheckExecution)
                {
                    // Assuming it was success - need better way
                    lblResxFileExists.Text = Enum.ResourceExists(true, Enum.ResourceType.RESX);
                    lblResxFileExists.ForeColor = Color.ForestGreen;
                }
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

        private void tsbAuthProfile_ButtonClick(object sender, EventArgs e)
        {
            tsbAuthProfile.ShowDropDown();
        }

        private void tsmCreateProfile_Click(object sender, EventArgs e)
        {
            var ctrlInputDialog = new InputDialog("Create Profile", "Enter CDS environment URL:");
            ctrlInputDialog.StartPosition = FormStartPosition.CenterScreen;
            if (DialogResult.OK == ctrlInputDialog.ShowDialog())
            {
                if (CommandPromptInitialized)
                {
                    string createProfile = Commands.Pac.CreateProfile(ctrlInputDialog.TextInputValue);
                    RunCommandLine(createProfile);
                }
                else
                {
                    lblErrors.Text = "Command Prompt not initialized. Wait for it to be initialized.";
                }
            }
        }

        private void tsmListProfiles_Click(object sender, EventArgs e)
        {
            if (CommandPromptInitialized)
            {
                string listProfile = Commands.Pac.ListProfiles();
                RunCommandLine(listProfile);
            }
            else
            {
                lblErrors.Text = "Command Prompt not initialized. Wait for it to be initialized.";
            }
        }

        private void linklblQuickDeployLearn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.microsoft.com/en-us/powerapps/developer/component-framework/import-custom-controls#deploying-code-components");
        }

        private void btnQuickDeploy_Click(object sender, EventArgs e)
        {
            var isValid = AreMainControlsPopulated();

            if (isValid)
            {
                string publisherPrefix = string.Empty;

                // Check if prefix exists from settings file
                if (pluginSettings.AlwaysLoadPublisherDetailsFromSettings && !string.IsNullOrEmpty(pluginSettings.PublisherPrefix))
                {
                    var result = MessageBox.Show($"Do you want to use prefix as '{pluginSettings.PublisherPrefix}' for Quick Deploy?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        var ctrlInputDialog = new InputDialog("Quick Deploy", "Enter your preferred Publisher Prefix:");
                        ctrlInputDialog.StartPosition = FormStartPosition.CenterScreen;
                        if (DialogResult.OK == ctrlInputDialog.ShowDialog())
                        {
                            publisherPrefix = ctrlInputDialog.TextInputValue;

                        }
                    }
                    if (result == DialogResult.Yes)
                    {
                        publisherPrefix = pluginSettings.PublisherPrefix;
                    }
                }
                else
                {
                    var ctrlInputDialog = new InputDialog("Quick Deploy", "Enter your preferred Publisher Prefix:");
                    ctrlInputDialog.StartPosition = FormStartPosition.CenterScreen;
                    if (DialogResult.OK == ctrlInputDialog.ShowDialog())
                    {
                        publisherPrefix = ctrlInputDialog.TextInputValue;

                    }
                }

                if (!string.IsNullOrEmpty(publisherPrefix))
                {
                    string cdWorkingDir = Commands.Cmd.ChangeDirectory($"{txtWorkingFolder.Text}");
                    string quickDeployCommand = Commands.Pac.DeployWithoutSolution(publisherPrefix);

                    RunCommandLine(cdWorkingDir, quickDeployCommand);
                }

            }
        }

        private void linklblPowerBiReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Properties.Settings.Default["AppInsightsPowerBiReport"].ToString());
        }

        private void chkUseExistingSolution_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseExistingSolution.Checked)
            {
                Routine_SolutionNotFound(false);
                ExecuteMethod(LoadSolutions);
                cboxSolutions.Enabled = true;
                radSolutionTypeUnmanaged.Checked = true;
            }
            else
            {
                Routine_ExistingSolution();
                IdentifySolutionDetails(true);
                Routine_EditComponent();
            }
        }

        private void cboxSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDetails selectedSolution = (EntityDetails)cboxSolutions.SelectedItem;

            txtSolutionName.Text = selectedSolution.MetaData.GetAttributeValue<string>("uniquename");
            txtSolutionFriendlyName.Text = selectedSolution.MetaData.GetAttributeValue<string>("friendlyname");
            txtPublisherUniqueName.Text = (selectedSolution.MetaData.GetAttributeValue<AliasedValue>("pub.uniquename")).Value.ToString();
            txtPublisherFriendlyName.Text = (selectedSolution.MetaData.GetAttributeValue<AliasedValue>("pub.friendlyname")).Value.ToString();
            txtPublisherPrefix.Text = (selectedSolution.MetaData.GetAttributeValue<AliasedValue>("pub.customizationprefix")).Value.ToString();
            txtSolutionVersion.Text = selectedSolution.MetaData.GetAttributeValue<string>("version");
        }

        private void PCFBuilder_OnCloseTool(object sender, EventArgs e)
        {
            consoleControl.Dispose();
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (detail != null)
            {
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private void btnShowMRULocations_Click(object sender, EventArgs e)
        {
            ToggleMRULocationGrid();
        }

        private void dgvMRULocations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtWorkingFolder.Text = dgvMRULocations.Rows[e.RowIndex].Cells[1].Value.ToString();
            ToggleMRULocationGrid();
            IdentifyControlDetails();
            PopulateControlDetails();
        }

        private void dgvMRULocations_Leave(object sender, EventArgs e)
        {
            ToggleMRULocationGrid();
        }

        private void btnAddPreviewImage_Click(object sender, EventArgs e)
        {
            var showPreviewImageForm = new ShowPreviewImage(ControlDetails);
            showPreviewImageForm.StartPosition = FormStartPosition.CenterScreen;
            showPreviewImageForm.ShowDialog();
        }

        private void btnAddResxFile_Click(object sender, EventArgs e)
        {
            var ctrlLangCodes = new LanguageCodeSelector();
            ctrlLangCodes.StartPosition = FormStartPosition.CenterScreen;
            ctrlLangCodes.ParentControl = this;
            ctrlLangCodes.ShowDialog();

            string controlDir = $"{txtWorkingFolder.Text}\\{txtControlName.Text}";
            string stringsDir = $"{controlDir}\\strings";

            if (SelectedLcids != null)
            {
                if (!Directory.Exists(stringsDir))
                {
                    Directory.CreateDirectory(stringsDir);
                }

                // As of now only one LCID an be added until PCF Generator supports multiple
                var lcid = SelectedLcids.First().Value;
                var areMainControlsValid = AreMainControlsPopulated();

                if (areMainControlsValid)
                {
                    string cdWorkingDir = Commands.Cmd.ChangeDirectory(txtWorkingFolder.Text);
                    string yoCommand = Commands.Yo.AddResxFile(txtControlName.Text, lcid.ToString());

                    RunCommandLine(cdWorkingDir, yoCommand);
                }
            }
        }

        private void txtPublisherUniqueName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '_')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPublisherFriendlyName_TextChanged(object sender, EventArgs e)
        {
            Regex rgx = new Regex(@"[^a-zA-Z0-9_]");
            txtPublisherUniqueName.Text = rgx.Replace(txtPublisherFriendlyName.Text.ToLower(), "");
        }

        private void txtPublisherPrefix_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
             * The prefix can contain only alphanumeric characters. 
             */
            if (Char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtSolutionFriendlyName_TextChanged(object sender, EventArgs e)
        {
            if (!DataverseSolutionDetails.FoundSolutionDetails)
            {
                txtSolutionName.Text = Regex.Replace(txtSolutionFriendlyName.Text, @"\s+", "");
            }
        }

        private void chkUseExistingPublisher_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseExistingPublisher.Checked)
            {
                Routine_SolutionNotFound(false, false);
                ExecuteMethod(LoadPublishers);
                cboxPublishers.Enabled = true;
            }
            else
            {
                Routine_ExistingPublisher();
                IdentifySolutionDetails(true);
                Routine_EditComponent();
            }
        }

        private void cboxPublishers_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDetails selectedPublisher = (EntityDetails)cboxPublishers.SelectedItem;

            txtPublisherUniqueName.Text = selectedPublisher.MetaData.GetAttributeValue<string>("uniquename");
            txtPublisherFriendlyName.Text = selectedPublisher.MetaData.GetAttributeValue<string>("friendlyname");
            txtPublisherPrefix.Text = selectedPublisher.MetaData.GetAttributeValue<string>("customizationprefix");
        }

        private void tspCreator_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/PCFBuilder");
        }

        private void txtControlDisplayName_TextChanged(object sender, EventArgs e)
        {
            if (!ControlDetails.FoundControlDetails)
            {
                Regex rgx = new Regex(@"[^a-zA-Z0-9_]");
                txtControlName.Text = rgx.Replace(txtControlDisplayName.Text, "");
            }
        }

        private void btnUpdateControlDetails_Click(object sender, EventArgs e)
        {
            var controlDetailsForm = new ControlDetailsForm(this);
            controlDetailsForm.StartPosition = FormStartPosition.CenterScreen;
            controlDetailsForm.ShowDialog();

            PopulateControlDetails();
        }

        private void SolutionPackageType_Changed(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            lblSolutionNote.Visible = false;
            if (radioButton.Checked)
            {
                switch (radioButton.Text)
                {
                    case "Unmanaged":
                        DataverseSolutionDetails.PackageType = SolutionPackageType.Unmanaged;
                        break;
                    case "Managed":
                        DataverseSolutionDetails.PackageType = SolutionPackageType.Managed;
                        break;
                    case "Both":
                        DataverseSolutionDetails.PackageType = SolutionPackageType.Both;
                        lblSolutionNote.Visible = true;
                        break;
                    default:
                        DataverseSolutionDetails.PackageType = SolutionPackageType.Unmanaged;
                        break;
                }
            }

            Solution.UpdateSolutionPackageType(DataverseSolutionDetails);
        }

        private void ReleaseType_Changed(object sender, EventArgs e)
        {
            RadioButton checkedRadioButton = pnlReleaseType.Controls
                                        .OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
            switch (checkedRadioButton.Text)
            {
                case "Dev":
                    DataverseSolutionDetails.ReleaseType = Release.Dev;
                    break;
                case "Production":
                    DataverseSolutionDetails.ReleaseType = Release.Prod;
                    break;
                default:
                    DataverseSolutionDetails.ReleaseType = Release.Dev;
                    break;
            }
        }
        
        private void btnOpenSolutionInExplorer_Click(object sender, EventArgs e)
        {
            var isValid = AreSolutionDetailsPopulated();

            if (isValid)
            {
                Process.Start("explorer.exe", string.Concat(GetCorrectSolutionDirectory(), $"\\{txtSolutionName.Text}"));
            }
        }

        private void btnManageProperties_Click(object sender, EventArgs e)
        {
            var propertiesForm = new PropertiesForm(this);
            propertiesForm.StartPosition = FormStartPosition.CenterScreen;
            propertiesForm.ShowDialog();

            ControlDetails = ControlManifest.GetControlManifestDetails(txtWorkingFolder.Text);
            if (InitiatePCFProjectBuild)
            {
                BuildPCFProject();
            }
        }

        private void btnManageFeatures_Click(object sender, EventArgs e)
        {
            var featuresForm = new FeaturesForm(this);
            featuresForm.StartPosition = FormStartPosition.CenterScreen;
            featuresForm.ShowDialog();

            ControlDetails = ControlManifest.GetControlManifestDetails(txtWorkingFolder.Text);
            if (InitiatePCFProjectBuild)
            {
                BuildPCFProject();
            }
        }

        private void cboxControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)cboxControlType.SelectedItem == "Virtual")
            {
                cboxAdditionalPackages.SelectedIndex = 1;
                cboxAdditionalPackages.Enabled = false;
            }
        }

        #endregion


    }
}