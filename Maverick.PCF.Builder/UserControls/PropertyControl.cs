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
using Maverick.PCF.Builder.Helper;
using Maverick.PCF.Builder.Common;
using Enum = Maverick.PCF.Builder.Helper.Enum;
using Maverick.PCF.Builder.Forms;

namespace Maverick.PCF.Builder.UserControls
{
    public partial class PropertyControl : UserControl
    {
        PropertiesForm ParentControl;
        Dictionary<string, string> collection;
        List<SupportedDataTypes> dataTypes;
        List<System.Enum> usageTypes;
        List<TypeGroup> typeGroups;
        ControlManifestDetails manifestDetails;
        string persistedPropertyName = string.Empty;
        bool newProperty = false;

        public PropertyControl(PropertiesForm parent, Dictionary<string, string> attributes, ControlManifestDetails cmd)
        {
            InitializeComponent();
            collection = attributes;
            typeGroups = cmd.TypeGroups;
            manifestDetails = cmd;
            InitializeControls();
            ParentControl = parent;
        }

        #region Private Methods

        private void InitializeControls()
        {
            dataTypes = FetchSupportedDataTypes();
            usageTypes = Enum.GetEnumForDropDown(typeof(Enum.UsageType));

            ddUsage.DataSource = usageTypes;
            ddUsage.DisplayMember = "value";

            if (collection != null && collection.Count > 0)
            {
                Routine_EditControl();
                LoadControlValues();
            }
            else
            {
                Routine_NewControl();
            }
        }

        private void LoadControlValues()
        {
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    switch (item.Key)
                    {
                        case "Name":
                            txtPropertyName.Text = item.Value;
                            persistedPropertyName = txtPropertyName.Text;
                            break;
                        case "DisplayNameKey":
                            txtDisplayNameKey.Text = item.Value;
                            break;
                        case "DescriptionNameKey":
                            txtDescriptionKey.Text = item.Value;
                            break;
                        case "TypeOrTypeGroup":
                            ddOfType.SelectedValue = item.Value;
                            break;
                        case "Usage":
                            ddUsage.SelectedIndex = item.Value.ToLower() == "bound" ? 0 : 1;
                            break;
                        case "IsRequired":
                            chkRequired.Checked = bool.Parse(item.Value);
                            break;
                        case "IsUsingTypeGroup":
                            chkUseTypeGroup.Checked = bool.Parse(item.Value);
                            ToggleTypeDataset();
                            break;
                        default:
                            break;
                    }
                } 
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

        private void ToggleTypeDataset()
        {
            if (chkUseTypeGroup.Checked)
            {
                ddOfType.DataSource = typeGroups;
                ddOfType.DisplayMember = "Name";
                ddOfType.ValueMember = "Name";

                lblType.Text = "Type Group (Data Type)";
            }
            else
            {
                ddOfType.DataSource = dataTypes;
                ddOfType.DisplayMember = "DataTypeName";
                ddOfType.ValueMember = "DataTypeName";

                lblType.Text = "Type (Data Type)";
            }
        }

        private void Routine_NewControl()
        {
            btnUpdate.Text = "Create New Property";
            ToggleTypeDataset();
            newProperty = true;
        }

        private void Routine_EditControl()
        {
            btnUpdate.Text = "Update Property";
            ToggleTypeDataset();
            newProperty = false;
        }

        #endregion

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ControlProperty controlProperty = new ControlProperty();
            controlProperty.Name = txtPropertyName.Text;
            controlProperty.DisplayNameKey = txtDisplayNameKey.Text;
            controlProperty.DescriptionNameKey = txtDescriptionKey.Text;
            controlProperty.Usage = (Enum.UsageType)ddUsage.SelectedItem;
            controlProperty.IsRequired = chkRequired.Checked;
            controlProperty.IsUsingTypeGroup = chkUseTypeGroup.Checked;
            controlProperty.TypeOrTypeGroup = ddOfType.SelectedValue.ToString();

            ControlManifestHelper manifestHelper = new ControlManifestHelper();

            if (newProperty)
            {
                manifestHelper.CreateNewProperty(manifestDetails, controlProperty);
                Routine_EditControl();
            }
            else
            {
                manifestDetails = manifestHelper.UpdatePropertyDetails(persistedPropertyName, manifestDetails, controlProperty);
            }

            persistedPropertyName = controlProperty.Name;
            ParentControl.RefreshControlManifestDetails();
        }

        private void chkUseTypeGroup_CheckedChanged(object sender, EventArgs e)
        {
            ToggleTypeDataset();
        }

        private void ddOfType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPropertyNotes.Visible = false;
            if (!chkUseTypeGroup.Checked)
            {
                if (((SupportedDataTypes)ddOfType.SelectedItem).IsDeprecated)
                {
                    lblPropertyNotes.Visible = true;
                }
            }
        }
    }
}
