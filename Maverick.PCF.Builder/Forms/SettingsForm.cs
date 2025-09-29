using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Maverick.PCF.Builder.Forms
{
    public partial class SettingsForm : Form
    {
        private Settings pluginSettings;

        public SettingsForm()
        {
            InitializeComponent();
        }

        public SettingsForm(Settings localSettings)
        {
            InitializeComponent();
            pluginSettings = localSettings;

            txtSetWorkingFolder.Text = pluginSettings.WorkingDirectoryLocation;
            txtMsBuildPath.Text = pluginSettings.MsBuildLocation;
            txtCustomExecutionPolicy.Text = pluginSettings.CustomExecutionPolicy ?? string.Empty;
            txtControlNamespace.Text = pluginSettings.ControlNamespace;
            chkboxLoadNamespace.Checked = pluginSettings.AlwaysLoadNamespaceFromSettings;
            txtPublisherName.Text = pluginSettings.PublisherName;
            txtPublisherPrefix.Text = pluginSettings.PublisherPrefix;
            chkboxLoadPublisherDetails.Checked = pluginSettings.AlwaysLoadPublisherDetailsFromSettings;
        }

        private void BtnWorkingFolderSelector_Click(object sender, EventArgs e)
        {
            if (workingFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtSetWorkingFolder.Text = workingFolderBrowserDialog.SelectedPath;
            }
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            pluginSettings.WorkingDirectoryLocation = txtSetWorkingFolder.Text;
            pluginSettings.MsBuildLocation = txtMsBuildPath.Text;
            pluginSettings.CustomExecutionPolicy = txtCustomExecutionPolicy.Text;

            pluginSettings.ControlNamespace = txtControlNamespace.Text;
            pluginSettings.AlwaysLoadNamespaceFromSettings = chkboxLoadNamespace.Checked;

            pluginSettings.PublisherName = txtPublisherName.Text;
            pluginSettings.PublisherPrefix = txtPublisherPrefix.Text;
            pluginSettings.AlwaysLoadPublisherDetailsFromSettings = chkboxLoadPublisherDetails.Checked;

            SettingsManager.Instance.Save(GetType(), pluginSettings);

            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtSetWorkingFolder.Text = string.Empty;
            pluginSettings.WorkingDirectoryLocation = string.Empty;
            pluginSettings.MsBuildLocation = string.Empty;
            pluginSettings.CustomExecutionPolicy = string.Empty;

            pluginSettings.ControlNamespace = string.Empty;
            pluginSettings.AlwaysLoadNamespaceFromSettings = true;

            pluginSettings.PublisherName = string.Empty;
            pluginSettings.PublisherPrefix = string.Empty;
            pluginSettings.AlwaysLoadPublisherDetailsFromSettings = true;

            SettingsManager.Instance.Save(GetType(), pluginSettings);

            this.Close();
        }
    }
}
