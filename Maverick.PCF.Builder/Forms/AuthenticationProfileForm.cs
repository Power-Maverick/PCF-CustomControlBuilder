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
    public partial class AuthenticationProfileForm : Form
    {
        #region Properties

        public PCFBuilder ParentControl { get; set; }

        #endregion

        public AuthenticationProfileForm()
        {
            InitializeComponent();
        }

        public AuthenticationProfileForm(List<AuthenticationProfile> profiles)
        {
            InitializeComponent();

            foreach (var item in profiles)
            {
                ListViewItem lvi = new ListViewItem(item.Index.ToString());
                lvi.SubItems.Add(item.IsCurrent ? "Yes" : "No");
                lvi.SubItems.Add(item.EnvironmentType);
                lvi.SubItems.Add(item.EnvironmentUrl);
                lvi.SubItems.Add(item.UserName);

                lstProfiles.Items.Add(lvi);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            var parent = this.ParentControl as PCFBuilder;
            parent.SelectedProfileAction = PCFBuilder.AuthProfileAction.ShowDetails;
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstProfiles.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this profile?\n*This will close current window and run the command in console.", "Profile Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var index = lstProfiles.SelectedItems[0].SubItems[0];

                    var parent = this.ParentControl as PCFBuilder;
                    parent.SelectedProfileAction = PCFBuilder.AuthProfileAction.Delete;
                    parent.SelectedProfileIndex = int.Parse(index.Text);

                    this.Close();
                } 
            }
        }

        private void btnSwitchCurrent_Click(object sender, EventArgs e)
        {
            if (lstProfiles.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to make this profile current?\n*This will close current window and run the command in console.", "Switch Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    var index = lstProfiles.SelectedItems[0].SubItems[0];

                    var parent = this.ParentControl as PCFBuilder;
                    parent.SelectedProfileAction = PCFBuilder.AuthProfileAction.SwitchCurrent;
                    parent.SelectedProfileIndex = int.Parse(index.Text);

                    this.Close();
                }
            }
        }
    }
}
