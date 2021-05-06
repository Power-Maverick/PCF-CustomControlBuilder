using Maverick.PCF.Builder.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maverick.PCF.Builder.Forms
{
    public partial class FeaturesForm : Form
    {
        public PCFBuilder ParentControl { get; set; }
        ControlManifestHelper manifestHelper = new ControlManifestHelper();

        #region Private Methods

        private void LoadFeatures()
        {
            foreach (var item in ParentControl.ControlDetails.Features)
            {
                switch (item.Type)
                {
                    case Helper.Enum.FeatureType.CaptureAudio:
                        chkCaptureAudio.Checked = true;
                        break;
                    case Helper.Enum.FeatureType.CaptureVideo:
                        chkCaptureVideo.Checked = true;
                        break;
                    case Helper.Enum.FeatureType.CaptureImage:
                        chkCaptureImage.Checked = true;
                        break;
                    case Helper.Enum.FeatureType.GetBarcode:
                        chkGetBarcodeValue.Checked = true;
                        break;
                    case Helper.Enum.FeatureType.GetCurrentPosition:
                        chkGetCurrentPosition.Checked = true;
                        break;
                    case Helper.Enum.FeatureType.PickFile:
                        chkPickFile.Checked = true;
                        break;
                    case Helper.Enum.FeatureType.Utility:
                        chkUtility.Checked = true;
                        break;
                    case Helper.Enum.FeatureType.WebApi:
                        chkWebApi.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Constructor

        public FeaturesForm(PCFBuilder parent)
        {
            InitializeComponent();

            ParentControl = parent;
            LoadFeatures();
        }

        #endregion

        #region Event Handlers

        private void btnCloseBuild_Click(object sender, EventArgs e)
        {
            var parent = this.ParentControl as PCFBuilder;
            parent.InitiatePCFProjectBuild = true;
            this.Close();
        }

        private void FeatureCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkbox = (CheckBox)sender;

            if (chkbox.Checked)
            {
                manifestHelper.AddFeature(ParentControl.ControlDetails, chkbox.Text);
            }
            else
            {
                manifestHelper.RemoveFeature(ParentControl.ControlDetails, chkbox.Text);
            }
        }

        #endregion

    }
}
