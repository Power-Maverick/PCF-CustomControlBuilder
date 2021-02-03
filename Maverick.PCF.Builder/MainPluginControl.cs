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
using XrmToolBox.Extensibility.Args;
using System.Text.RegularExpressions;
using Maverick.PCF.Builder.SealedClasses;
using Enum = Maverick.PCF.Builder.Helper.Enum;
using Maverick.PCF.Builder.ToolSettings;

namespace Maverick.PCF.Builder
{
    public partial class MainPluginControl : PluginControlBase, IGitHubPlugin, IHelpPlugin, IPayPalPlugin, IStatusBarMessenger
    {
        private Settings pluginSettings;

        public string RepositoryName => "PCF-CustomControlBuilder";
        public string UserName => "Power-Maverick";
        public string HelpUrl => "https://github.com/Power-Maverick/PCF-CustomControlBuilder/blob/master/README.md";
        public string DonationDescription => "Keeps the ball rolling and motivates in making awesome tools.";
        public string EmailAccount => "danz@techgeek.co.in";
        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        #region Properties, Constants and Enums

        private const string TEMPLATE_LOCATION = "Templates";

        public PcfGallery SelectedTemplate { get; set; }
        public bool CommandPromptInitialized { get; set; }
        public bool StatusCheckExecution { get; set; }
        public bool ResxCheckExecution { get; set; }
        public bool BuildDeployExecution { get; set; }
        public bool ReloadDetails { get; set; }
        public string CurrentCommandOutput { get; set; }
        public bool ListProfileExecution { get; set; }
        public AuthProfileAction SelectedProfileAction { get; set; }
        public int SelectedProfileIndex { get; set; }
        public bool RefreshCurrentProfile { get; set; }
        public ControlManifestDetails ControlDetails { get; set; }
        public List<LanguageCode> SelectedLcids { get; set; }

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
        private BindingSource bindingSource = new BindingSource();

        private const string SolutionFolderName = "Solution";

        #endregion

        #region Helper Functions

        private void InitCommandLine()
        {
            consoleControl.StartProcess("cmd", $"/K powershell");
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
                consoleControl.WriteInput(cmd, Color.White, true);
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

        #endregion

        #region Custom Functions

        private bool AreMainControlsPopulated()
        {
            lblErrors.Text = string.Empty;
            bool isWorkingFolderValid = true;

            isWorkingFolderValid = IsWorkingFolderPopulated(false);

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

            bool isValid = isWorkingFolderValid && string.IsNullOrEmpty(lblErrors.Text);

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
                if (Regex.Match(txtPublisherPrefix.Text, @"^\d*(?=.*[a-zA-Z]{2}).{1,9}$").Success && !txtPublisherPrefix.Text.StartsWith("mscrm"))
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

        private bool codeFileExists(string directoryPath)
        {
            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            FileInfo[] files = dir.GetFiles("*.ts");

            if (files.Length > 0)
            {
                SendMessageToStatusBar.Invoke(this, new StatusBarMessageEventArgs($"Code file found with name {files[0].Name}"));
            }

            return files.Length > 0 ? true : false;
        }

        private void Routine_NewComponent()
        {
            txtNamespace.Clear();
            txtControlName.Clear();
            cboxTemplate.SelectedIndex = -1;
            cboxAdditionalPackages.SelectedIndex = -1;
            txtComponentVersion.Clear();

            txtSolutionName.Clear();
            txtPublisherUniqueName.Clear();
            txtPublisherPrefix.Clear();
            txtSolutionVersion.Clear();

            txtControlName.Enabled = true;
            txtNamespace.Enabled = true;
            cboxTemplate.Enabled = true;
            cboxAdditionalPackages.Enabled = true;
            txtSolutionName.Enabled = true;
            txtPublisherUniqueName.Enabled = true;
            txtPublisherPrefix.Enabled = true;
            btnCreateComponent.Enabled = true;
            btnCreateSolution.Enabled = true;
            btnRefreshDetails.Enabled = false;
            btnDeploy.Enabled = false;

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
                cboxTemplate.Enabled = false;
                cboxAdditionalPackages.Enabled = false;
                btnCreateComponent.Enabled = false;

                lblControlInitStatus.Text = Enum.InitializationStatus(true);
                lblControlInitStatus.ForeColor = Color.ForestGreen;
            }

            if (!string.IsNullOrEmpty(txtSolutionName.Text))
            {
                txtSolutionFriendlyName.Enabled = false;
                txtSolutionName.Enabled = false;
                txtPublisherUniqueName.Enabled = false;
                txtPublisherPrefix.Enabled = false;
                btnCreateSolution.Enabled = false;
                cboxSolutions.Enabled = false;

                lblSolutionInitStatus.Text = Enum.InitializationStatus(true);
                lblSolutionInitStatus.ForeColor = Color.ForestGreen;
                btnDeploy.Enabled = true;
            }
        }

        private void Routine_SolutionNotFound(bool loadFromSettings = true)
        {
            txtSolutionFriendlyName.Enabled = true;
            txtSolutionName.Enabled = true;
            txtPublisherUniqueName.Enabled = true;
            txtPublisherFriendlyName.Enabled = true;
            txtPublisherPrefix.Enabled = true;
            btnCreateSolution.Enabled = true;
            btnDeploy.Enabled = false;

            txtSolutionFriendlyName.Clear();
            txtSolutionName.Clear();
            txtPublisherUniqueName.Clear();
            txtPublisherFriendlyName.Clear();
            txtPublisherPrefix.Clear();
            txtSolutionVersion.Clear();

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
                txtSolutionFriendlyName.Visible = false;
                //txtSolutionName.Visible = false;
                txtPublisherFriendlyName.Enabled = false;
                txtPublisherUniqueName.Enabled = false;
                txtPublisherPrefix.Enabled = false;
                cboxSolutions.Visible = true;
                chkManagedSolution.Enabled = false;
                btnCreateSolution.Text = "Export and Add Control";
            }
            else
            {
                txtSolutionFriendlyName.Visible = true;
                //txtSolutionName.Visible = true;
                txtPublisherFriendlyName.Enabled = true;
                txtPublisherUniqueName.Enabled = true;
                txtPublisherPrefix.Enabled = true;
                cboxSolutions.Visible = false;
                chkManagedSolution.Enabled = true;
                btnCreateSolution.Text = "Create and Add Control";
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
                bool loadEditRoutine = true;

                var start = DateTime.Now;
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
                                var indexExists = codeFileExists(item); //File.Exists(item + "\\" + "index.ts");
                                if (indexExists)
                                {
                                    txtControlName.Text = Path.GetFileName(item);
                                    var controlManifestFile = item + "\\" + "ControlManifest.Input.xml";
                                    XmlReader xmlReader = XmlReader.Create(controlManifestFile);
                                    bool readFirstProperty = false;
                                    bool readOfTypeGroup = false;
                                    string ofTypeGroupName = string.Empty;
                                    ControlDetails = new ControlManifestDetails();
                                    while (xmlReader.Read())
                                    {
                                        if (xmlReader.NodeType == XmlNodeType.Element)
                                        {
                                            switch (xmlReader.Name)
                                            {
                                                case "control":
                                                    txtNamespace.Text = xmlReader.GetAttribute("namespace");
                                                    txtComponentVersion.Text = xmlReader.GetAttribute("version");
                                                    ControlDetails.ControlDisplayName = xmlReader.GetAttribute("display-name-key");
                                                    ControlDetails.ControlDescription = xmlReader.GetAttribute("description-key");
                                                    ControlDetails.PreviewImagePath = $"{txtWorkingFolder.Text}\\{txtControlName.Text}\\{xmlReader.GetAttribute("preview-image")}";
                                                    if (!string.IsNullOrEmpty(ControlDetails.PreviewImagePath))
                                                    {
                                                        lblPreviewImageExists.Text = Enum.ResourceExists(true, Enum.ResourceType.PreviewImage);
                                                        lblPreviewImageExists.ForeColor = Color.ForestGreen;
                                                    }
                                                    break;
                                                case "property":
                                                    if ((xmlReader.GetAttribute("usage") == "bound") && !readFirstProperty)
                                                    {
                                                        readFirstProperty = true;
                                                        if (!string.IsNullOrEmpty(xmlReader.GetAttribute("of-type")))
                                                        {
                                                            ControlDetails.SupportedTypes.Add(xmlReader.GetAttribute("of-type"));
                                                        }
                                                        else if (!string.IsNullOrEmpty(xmlReader.GetAttribute("of-type-group")))
                                                        {
                                                            ofTypeGroupName = xmlReader.GetAttribute("of-type-group");
                                                        }
                                                    }
                                                    break;
                                                case "type-group":
                                                    if (xmlReader.GetAttribute("name") == ofTypeGroupName)
                                                    {
                                                        XmlReader xmlSubtreeReader = xmlReader.ReadSubtree();
                                                        while (xmlSubtreeReader.Read())
                                                        {
                                                            if (xmlSubtreeReader.NodeType == XmlNodeType.Element && xmlSubtreeReader.Name == "type")
                                                            {
                                                                xmlSubtreeReader.Read();
                                                                ControlDetails.SupportedTypes.Add(xmlSubtreeReader.ReadContentAsString());
                                                            }
                                                        }
                                                        xmlSubtreeReader.Close();
                                                    }
                                                    break;
                                                case "data-set":
                                                    cboxTemplate.SelectedIndex = 1;
                                                    break;
                                                case "css":
                                                    lblCssFileExists.Text = Enum.ResourceExists(true, Enum.ResourceType.CSS);
                                                    lblCssFileExists.ForeColor = Color.ForestGreen;
                                                    break;
                                                case "resx":
                                                    lblResxFileExists.Text = Enum.ResourceExists(true, Enum.ResourceType.RESX);
                                                    lblResxFileExists.ForeColor = Color.ForestGreen;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }

                                    }
                                    xmlReader.Close();
                                    if (cboxTemplate.SelectedIndex == -1)
                                    {
                                        cboxTemplate.SelectedIndex = 0;
                                    }
                                    break;
                                }

                            }
                            _mainPluginLocalWorker = new BackgroundWorker();
                            _mainPluginLocalWorker.DoWork += IdentifyAdditionalPackage;
                            _mainPluginLocalWorker.RunWorkerAsync();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Could not retrieve existing PCF project and CDS solution project details.");
                    loadEditRoutine = false;
                }

                if (!string.IsNullOrEmpty(txtControlName.Text))
                {
                    IdentifySolutionDetails();
                }

                if (loadEditRoutine)
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
            }
        }

        private void IdentifySolutionDetails()
        {
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
                                var solutionFile = File.Exists(item + "\\Other\\Solution.xml") ? item + "\\Other\\Solution.xml" : item + "\\src\\Other\\Solution.xml";

                                XmlDocument xmlDoc = new XmlDocument();
                                xmlDoc.Load(solutionFile);

                                XmlNode uniqueSolutionNameNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/UniqueName");
                                txtSolutionName.Text = uniqueSolutionNameNode.InnerText;

                                XmlNode localizedSolutionNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/LocalizedNames/LocalizedName[@languagecode='1033']");
                                txtSolutionFriendlyName.Text = localizedSolutionNode.Attributes["description"].Value;

                                XmlNode uniquePublisherNameNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/Publisher/UniqueName");
                                txtPublisherUniqueName.Text = uniquePublisherNameNode.InnerText;

                                XmlNode localizedPublisherNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/Publisher/LocalizedNames/LocalizedName[@languagecode='1033']");
                                txtPublisherFriendlyName.Text = localizedPublisherNode.Attributes["description"].Value;

                                XmlNode customizationPrefixNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/Publisher/CustomizationPrefix");
                                txtPublisherPrefix.Text = customizationPrefixNode.InnerText;

                                XmlNode versionNode = xmlDoc.SelectSingleNode("/ImportExportXml/SolutionManifest/Version");
                                txtSolutionVersion.Text = versionNode.InnerText;

                                /*XmlReader xmlReader = XmlReader.Create(solutionFile);
                                while (xmlReader.Read())
                                {
                                    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "UniqueName"))
                                    {
                                        var publisher = xmlReader.ReadElementContentAsString();
                                        txtPublisherUniqueName.Text = publisher;
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
                                */
                                break;
                            }
                            else
                            {
                                Routine_SolutionNotFound();
                            }
                        }
                    }
                    else
                    {
                        Routine_SolutionNotFound();
                    }
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
                MessageBox.Show("Invalid directory. Could not retrieve existing CDS solution project details.");
                Routine_SolutionNotFound();
                LogException(dnex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while retrieving existing project details. Please report an issue on GitHub page.");
                Routine_SolutionNotFound();
                LogException(ex);
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
            var start = DateTime.Now;

            string[] commands = new string[] { Commands.Pac.Check() };
            var output = CommandLineHelper.RunCommand(commands);

            string currentPacVersion = string.Empty;
            string latestPacVersion = string.Empty;

            if (!string.IsNullOrEmpty(output) && output.ToLower().Contains("version:"))
            {
                currentPacVersion = output.Substring(output.IndexOf("Version: ") + 8, output.IndexOf("+", output.IndexOf("Version: ") + 8) - (output.IndexOf("Version: ") + 8));

                //NOTE: A newer version of Microsoft.PowerApps.CLI has been found. Please run 'pac install latest' to install the latest version.
                if (output.ToLower().Contains("a newer version of microsoft.powerapps.cli has been found"))
                {
                    if (DialogResult.Yes == MessageBox.Show("New version of PCF CLI is available. Do you want to update it now?", "PCF CLI Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        string pacUpdateCLI = Commands.Pac.InstallLatest();
                        RunCommandLine(pacUpdateCLI);
                        currentPacVersion = latestPacVersion;
                    }
                }

                lblPCFCLIVersionMsg.Text = "Power Apps CLI Version: " + currentPacVersion.Trim();
            }
            else
            {
                lblPCFCLIVersionMsg.Text = "Power Apps CLI Not Detected";
                Invoke(new Action(() =>
                {
                    ShowErrorNotification("Power Apps CLI not detected on this machine. Please download it.", new Uri("https://aka.ms/PowerAppsCLI"));
                }));
                var downloadPACLIResult = MessageBox.Show("Power Apps CLI not detected on this machine. Do you want to download it?", "Power Apps CLI - Not Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (downloadPACLIResult == DialogResult.Yes)
                {
                    Process.Start("https://aka.ms/PowerAppsCLI");
                }
                else
                {
                    CloseTool();
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
                string[] commands = new string[] { Commands.Cmd.SetExecutionPolicyToUnrestricted(), Commands.Cmd.FindMsBuild(), Commands.Cmd.ResetExecutionPolicy() };
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
                                    var indexExists = codeFileExists(item); //File.Exists(item + "\\" + "index.ts");
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
            string solutionFileLocation = $"{solutionFolder}\\bin\\{(chkManagedSolution.Checked ? "release" : "debug")}\\{txtSolutionName.Text}.zip";

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

        private string ParseOrgDetails(string output)
        {
            string orgDetails = string.Empty;

            try
            {
                if (output.ToLower().Contains("organization information"))
                {
                    // Split on \r\n
                    char[] mainDelimiterChars = { '\r', '\n' };
                    string[] mainSplit = output.Split(mainDelimiterChars);

                    string url = string.Empty;
                    string username = string.Empty;

                    foreach (string list in mainSplit)
                    {
                        if (string.IsNullOrEmpty(list))
                        {
                            orgDetails += "\n";
                        }
                        else if (list.Contains("Org ID:") || list.Contains("Unique Name:") || list.Contains("Friendly Name:"))
                        {
                            string[] innerSplit = list.Trim().Split(':');

                            if (innerSplit.Length > 0)
                            {
                                orgDetails += innerSplit[0].Trim();
                                orgDetails += ":";
                                orgDetails += innerSplit[1].Trim();
                            }
                        }
                        else if (list.Contains("Org URL:"))
                        {
                            string[] innerSplit = list.Trim().Split(' ');

                            if (innerSplit.Length > 0)
                            {
                                orgDetails += "Org URL:";
                                orgDetails += innerSplit[innerSplit.Length - 1].Trim();
                            }
                        }
                        else if (list.Contains("User ID:"))
                        {
                            string[] innerSplit = list.Trim().Split(':');

                            if (innerSplit.Length > 0)
                            {
                                string[] userSplit = innerSplit[1].Trim().Split(' ');

                                if (userSplit.Length > 0)
                                {
                                    orgDetails += innerSplit[0].Trim();
                                    orgDetails += ":";
                                    orgDetails += userSplit[0].Trim();
                                }
                            }
                        }
                        else
                        {
                            orgDetails += list + "\n";
                        }
                    }

                }
                else if (output.ToLower().Contains("no profiles were found on this computer"))
                {
                    orgDetails = "No profiles were found on this computer. Please create a new profile.";
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                MessageBox.Show("An error occured while parsing the Org Details for Profile. Please report an issue on GitHub page.");
            }

            return orgDetails;
        }

        private void CheckDefaultAuthenticationProfile(object worker, DoWorkEventArgs args)
        {
            string[] commands = new string[] { Commands.Pac.OrgDetails() };
            var output = CommandLineHelper.RunCommand(commands);

            // Sometimes wen current org is change 'org who' command doesnt quickly respond with the change.
            // So check again to see if output changed?
            var second_output = CommandLineHelper.RunCommand(commands);
            if (!output.Trim().Equals(second_output.Trim(), StringComparison.InvariantCultureIgnoreCase))
            {
                output = second_output;
            }

            if (!string.IsNullOrEmpty(output) && output.ToLower().Contains("organization information"))
            {
                lblCurrentProfile.Text = ParseOrgDetails(output.Substring(output.IndexOf("Organization Information"), output.LastIndexOf("\r\n\r\n") - output.LastIndexOf("Organization Information")));
            }
            else if (output.ToLower().Contains("error: unable to login to dynamics crm"))
            {
                lblCurrentProfile.Text = "Unable to login into current profile. Please delete and re-authenticate.";
            }
            else if (output.ToLower().Contains("no profiles were found on this computer"))
            {
                lblCurrentProfile.Text = "No profiles found";
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
                                                    select (new SealedClasses.SolutionDetails
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
            }
            IdentifyControlDetails();
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

        public MainPluginControl()
        {
            InitializeComponent();
        }

        private void MainPluginControl_Load(object sender, EventArgs e)
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

            var isValid = IsWorkingFolderPopulated(true);

            if (isValid)
            {
                CommandPromptInitialized = true;
                InitCommandLine();
                IdentifyControlDetails();
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
            var isValid = IsWorkingFolderPopulated(true);

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
            var isValid = IsWorkingFolderPopulated(true);

            if (isValid)
            {
                if (!CommandPromptInitialized)
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
            var isValid = IsWorkingFolderPopulated(true);

            if (isValid)
            {
                if (!CommandPromptInitialized)
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
                string msbuild_rebuild = chkManagedSolution.Checked ? Commands.Msbuild.RebuildRelease(solutionPath) : Commands.Msbuild.Rebuild(solutionPath);

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
                string msbuild_rebuild = chkManagedSolution.Checked ? Commands.Msbuild.RebuildRelease(solutionPath) : Commands.Msbuild.Rebuild(solutionPath);

                BuildDeployExecution = true;
                RunCommandLine(cdWorkingDir, npmBuildCommand, cdMsBuildDir, msbuild_rebuild);
            }
        }

        private void BtnCreateComponent_Click(object sender, EventArgs e)
        {
            var areMainControlsValid = AreMainControlsPopulated();

            lblErrors.Text = string.Empty;

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
                if (string.IsNullOrEmpty(additionalPackage))
                {
                    RunCommandLine(cdWorkingDir, pacCommand, npmCommand);
                }
                else
                {
                    RunCommandLine(cdWorkingDir, pacCommand, npmCommand, additionalPackage);
                }

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
                string msbuild_build = chkManagedSolution.Checked ? Commands.Msbuild.BuildRelease(projectPath) : Commands.Msbuild.Build(projectPath);

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
            var isValid = IsWorkingFolderPopulated(false);

            if (isValid)
            {
                IdentifyControlDetails();
                Routine_EditComponent();
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
                    if (!chkUseExistingSolution.Checked)
                    {
                        SanitizeSolutionDetails();
                    }
                    IdentifyControlDetails();

                    if (CurrentCommandOutput.ToLower().Contains("the powerapps component framework project was successfully created"))
                    {
                        _mainPluginLocalWorker = new BackgroundWorker();
                        _mainPluginLocalWorker.DoWork += CreateTemplateFiles;
                        _mainPluginLocalWorker.RunWorkerAsync();
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
            }
            else
            {
                Routine_ExistingSolution();
                IdentifySolutionDetails();
                Routine_EditComponent();
            }
        }

        private void cboxSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            SealedClasses.SolutionDetails selectedSolution = (SealedClasses.SolutionDetails)cboxSolutions.SelectedItem;

            txtSolutionName.Text = selectedSolution.MetaData.GetAttributeValue<string>("uniquename");
            txtSolutionFriendlyName.Text = selectedSolution.MetaData.GetAttributeValue<string>("friendlyname");
            txtPublisherUniqueName.Text = (selectedSolution.MetaData.GetAttributeValue<AliasedValue>("pub.uniquename")).Value.ToString();
            txtPublisherFriendlyName.Text = (selectedSolution.MetaData.GetAttributeValue<AliasedValue>("pub.friendlyname")).Value.ToString();
            txtPublisherPrefix.Text = (selectedSolution.MetaData.GetAttributeValue<AliasedValue>("pub.customizationprefix")).Value.ToString();
            txtSolutionVersion.Text = selectedSolution.MetaData.GetAttributeValue<string>("version");
        }

        private void MainPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            consoleControl.Dispose();
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
        }

        private void dgvMRULocations_Leave(object sender, EventArgs e)
        {
            ToggleMRULocationGrid();
        }

        private void btnAddPreviewImage_Click(object sender, EventArgs e)
        {
            ControlDetails.WorkingFolderPath = txtWorkingFolder.Text;
            ControlDetails.ControlName = txtControlName.Text;

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
            txtSolutionName.Text = Regex.Replace(txtSolutionFriendlyName.Text, @"\s+", "");
        }
    }
}