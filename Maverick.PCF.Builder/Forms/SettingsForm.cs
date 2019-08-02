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

            txtSetVSCmdLoc.Text = pluginSettings.VisualStudioCommandPromptPath;
            txtSetWorkingFolder.Text = pluginSettings.WorkingDirectoryLocation;
        }

        private void BtnWorkingFolderSelector_Click(object sender, EventArgs e)
        {
            if (workingFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtSetWorkingFolder.Text = workingFolderBrowserDialog.SelectedPath;
            }
        }

        private void BtnVSPromptLoc_Click(object sender, EventArgs e)
        {
            if (selectVSDevFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtSetVSCmdLoc.Text = selectVSDevFileDialog.FileName;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            pluginSettings.WorkingDirectoryLocation = txtSetWorkingFolder.Text;
            pluginSettings.VisualStudioCommandPromptPath = txtSetVSCmdLoc.Text;
            SettingsManager.Instance.Save(GetType(), pluginSettings);

            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtSetVSCmdLoc.Text = string.Empty;
            txtSetWorkingFolder.Text = string.Empty;
            pluginSettings.WorkingDirectoryLocation = string.Empty;
            pluginSettings.VisualStudioCommandPromptPath = string.Empty;
            SettingsManager.Instance.Save(GetType(), pluginSettings);

            this.Close();
        }
    }
}
