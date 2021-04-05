using Maverick.PCF.Builder.Common;
using Maverick.PCF.Builder.DataObjects;
using Maverick.PCF.Builder.Forms;
using Maverick.PCF.Builder.Helper;
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

namespace Maverick.PCF.Builder.UserControls
{
    public partial class TypeGroupControl : UserControl
    {
        PropertiesForm ParentControl;
        Dictionary<string, string> collection;
        List<SupportedDataTypes> dataTypes;
        ControlManifestDetails manifestDetails;
        string persistedTypeGroupName = string.Empty;
        bool newTypeGroup = false;

        public TypeGroupControl(PropertiesForm parent, Dictionary<string, string> attributes, ControlManifestDetails cmd)
        {
            InitializeComponent();
            collection = attributes;
            manifestDetails = cmd;
            InitializeControls();
            ParentControl = parent;
        }

        #region Private Methods

        private void InitializeControls()
        {
            if (collection != null)
            {
                txtTypeGroupName.Text = collection["Name"];
                persistedTypeGroupName = txtTypeGroupName.Text;

                var propTG = manifestDetails.Properties.Where(p => p.TypeOrTypeGroup == txtTypeGroupName.Text);
                lblReferenceCount.Text = $"Count = {propTG.Count()}";
                int counter = 0;
                lblReferences.Text = string.Empty;
                foreach (ControlProperty cp in propTG)
                {
                    if (counter < 5)
                    {
                        lblReferences.Text += cp.Name + "\n";
                    }
                    else
                    {
                        break;
                    }
                    counter++;
                }
                if (propTG.Count() > 5)
                {
                    lblReferences.Text += $"{propTG.Count() - 5} more...";
                }

                var arrTypes = collection["Types"].Split(';');
                counter = 0;
                lblTypes.Text = string.Empty;
                foreach (string t in arrTypes)
                {
                    if (counter < 5)
                    {
                        lblTypes.Text += t + "\n";
                    }
                    else
                    {
                        break;
                    }
                    counter++;
                }
                if (arrTypes.Length > 5)
                {
                    lblTypes.Text += $"{arrTypes.Length - 5} more...";
                }
                Routine_EditControl();
            }
            else
            {
                Routine_NewControl();
            }
        }

        private List<SupportedDataTypes> FetchSupportedDataTypes()
        {
            List<SupportedDataTypes> dataTypes = new List<SupportedDataTypes>();
            var fullDataTypeFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "\\DataFiles\\SupportedDataTypes.json";

            using (StreamReader stream = File.OpenText(fullDataTypeFilePath))
            {
                dataTypes = JsonHelper.FromJson<SupportedDataTypes>(stream.ReadToEnd());
            }

            return dataTypes;
        }

        private void Routine_NewControl()
        {
            btnUpdate.Text = "Create New Type Group";
            gboxDetails.Visible = false;
            btnUpdate.Location = new Point(9, 128);
            lblType.Visible = true;
            ddType.Visible = true;
            dataTypes = FetchSupportedDataTypes();
            ddType.DataSource = dataTypes;
            ddType.DisplayMember = "DataTypeName";
            ddType.ValueMember = "DataTypeName";
            lblReferences.Text = string.Empty;
            lblTypes.Text = string.Empty;

            newTypeGroup = true;
        }

        private void Routine_EditControl()
        {
            btnUpdate.Text = "Update Type Group Name";
            gboxDetails.Visible = true;
            btnUpdate.Location = new Point(9, 64);
            lblType.Visible = false;
            ddType.Visible = false;
            newTypeGroup = false;
        }

        #endregion

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ControlManifestHelper manifestHelper = new ControlManifestHelper();

            if (newTypeGroup)
            {
                manifestHelper.CreateNewTypeGroup(manifestDetails, txtTypeGroupName.Text, ddType.SelectedValue.ToString());
                Routine_EditControl();
            }
            else
            {
                manifestHelper.UpdateTypeGroupName(persistedTypeGroupName, txtTypeGroupName.Text, manifestDetails);
            }
            
            persistedTypeGroupName = txtTypeGroupName.Text;
            ParentControl.RefreshControlManifestDetails();
        }
    }
}
