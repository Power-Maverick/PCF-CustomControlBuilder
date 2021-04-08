using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Maverick.PCF.Builder.DataObjects;
using System.IO;

namespace Maverick.PCF.Builder.UserControls
{
    public partial class Template : UserControl
    {
        #region Properties

        public string WorkingDirLocation { get; set; }

        #endregion

        public Template()
        {
            InitializeComponent();
        }

        public Template(Image image, string controlName, string authorName, string description, string linkToPcfGallery, string downloadLink, string workingDirLocation, bool supportModelDrivenApp, bool supportCanvasApp, bool supportPortals, bool containsLicense)
        {
            InitializeComponent();

            imgPcfControl.Image = image;
            imgPcfControl.SizeMode = PictureBoxSizeMode.StretchImage;
            lblControlName.Text = controlName;
            lblAuthor.Text = authorName;
            lblDescription.Text = description;

            linklblPCFGallery.LinkClicked += (obj, e) =>
            {
                System.Diagnostics.Process.Start(linkToPcfGallery);
            };

            btnDownload.Click += (obj, e) =>
            {
                DownloadTemplate(downloadLink);
            };

            WorkingDirLocation = workingDirLocation;

            imgModelDriven.Visible = supportModelDrivenApp;
            imgCanvasApp.Visible = supportCanvasApp;
            imgPortals.Visible = supportPortals;
            imgLicense.Visible = containsLicense;
        }

        private void imgModelDriven_MouseHover(object sender, EventArgs e)
        {
            toolTipTemplate.SetToolTip(this.imgModelDriven, "Supports Model-driven apps");
        }

        private void imgCanvasApp_MouseHover(object sender, EventArgs e)
        {
            toolTipTemplate.SetToolTip(this.imgCanvasApp, "Supports Canvas apps");
        }

        private void imgLicense_MouseHover(object sender, EventArgs e)
        {
            toolTipTemplate.SetToolTip(this.imgLicense, "License defined for this template");
        }

        private void imgPortals_MouseHover(object sender, EventArgs e)
        {
            toolTipTemplate.SetToolTip(this.imgPortals, "Supports Portal apps");
        }

        #region Private Method

        private void DownloadTemplate(string downloadUrl)
        {
            try
            {
                string cdWorkingDir = $"cd {WorkingDirLocation}";
                string gitInit = $"git init";
                string gitAddOrigin = $"git remote add origin -f {downloadUrl}";
                string gitPull = $"git pull origin master";

                System.Diagnostics.Process.Start("cmd", $"{cdWorkingDir} && {gitInit} && {gitAddOrigin} && {gitPull} && exit");

                PcfGallery selectedTemplate = new PcfGallery();
                selectedTemplate.author = lblAuthor.Text;
                selectedTemplate.title = lblControlName.Text;
                selectedTemplate.download = downloadUrl;

                selectedTemplate.ParsedControlName = downloadUrl.Substring(downloadUrl.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1);

                // 4 level: control --> flowpanel --> table --> actual form
                var parent = this.Parent.Parent.Parent as Forms.TemplatesForm;
                parent.SelectedTemplate = selectedTemplate;

                // Wait for GitHub code to be downloaded
                int counter = 0;
                while (!File.Exists(WorkingDirLocation + "\\" + selectedTemplate.ParsedControlName + ".pcfproj"))
                {
                    if (counter > 10)
                    {
                        break;
                    }

                    System.Threading.Thread.Sleep(1000);
                    counter++;
                }

            ((Form)this.TopLevelControl).Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured during downloading the template. Please report an issue on GitHub.");
            }
        }

        #endregion

        
    }
}
