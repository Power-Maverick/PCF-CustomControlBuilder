using Maverick.PCF.Builder.Common;
using Maverick.PCF.Builder.DataObjects;
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
    public partial class ControlDetailsForm : Form
    {
        public PCFBuilder ParentControl { get; set; }

        public ControlDetailsForm(PCFBuilder parent)
        {
            InitializeComponent();

            ParentControl = parent;
            lblControlDisplayName.Text = ParentControl.ControlDetails.ControlDisplayName;
            lblControlDescription.Text = ParentControl.ControlDetails.ControlDescription;

        }

        private void btnChangeControlDetails_Click(object sender, EventArgs e)
        {
            ControlManifestHelper cmHelper = new ControlManifestHelper();
            ParentControl.ControlDetails = cmHelper.UpdateControlDetails(ParentControl.ControlDetails, txtControlDisplayName.Text, txtControlDescription.Text);

            if (MessageBox.Show("Control Manifest file is updated with new control details.", "Update Control Details", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
