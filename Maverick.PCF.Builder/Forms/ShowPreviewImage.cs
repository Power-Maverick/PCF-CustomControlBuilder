using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Maverick.PCF.Builder.DataObjects;

namespace Maverick.PCF.Builder.Forms
{
    public partial class ShowPreviewImage : Form
    {
        public MainPluginControl ParentControl { get; set; }

        private ControlManifestDetails _controlDetails;

        public ShowPreviewImage(ControlManifestDetails details)
        {
            InitializeComponent();

            _controlDetails = details;
            lblDisplayName.Text = _controlDetails.ControlDisplayName;
            lblDescription.Text = _controlDetails.ControlDescription;
            lblTypes.Text = String.Join(",\n", _controlDetails.SupportedTypes);

            pboxPreviewImage.ImageLocation = _controlDetails.PreviewImagePath;
        }

        private void btnChangePreviewImage_Click(object sender, EventArgs e)
        {
            if (ofdPreviewImage.ShowDialog() == DialogResult.OK)
            {
                var fileName = Path.GetFileName(_controlDetails.PreviewImagePath);
                var filePath = $"{_controlDetails.WorkingFolderPath}\\{_controlDetails.ControlName}\\img";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory($"{_controlDetails.WorkingFolderPath}\\{_controlDetails.ControlName}\\img");
                }

                File.Copy(ofdPreviewImage.FileName, $"{filePath}\\{fileName}", true);
                pboxPreviewImage.ImageLocation = $"{filePath}\\{fileName}";
               
            }
        }
    }
}
