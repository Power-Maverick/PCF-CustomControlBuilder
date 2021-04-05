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
    public partial class TypeControl : UserControl
    {
        PropertiesForm ParentControl;
        Dictionary<string, string> collection;
        Dictionary<string, string> tag;
        List<SupportedDataTypes> dataTypes;
        ControlManifestDetails manifestDetails;
        string persistedTypeName = string.Empty;
        bool newType = false;

        public TypeControl(PropertiesForm parent, Dictionary<string, string> attributes, ControlManifestDetails cmd, object t = null)
        {
            ParentControl = parent;
            collection = attributes;
            manifestDetails = cmd;
            tag = (Dictionary<string, string>)t;
            InitializeComponent();
            InitializeControls();
        }

        #region Private Methods

        private void InitializeControls()
        {
            dataTypes = FetchSupportedDataTypes();

            ddType.DataSource = dataTypes;
            ddType.DisplayMember = "DataTypeName";
            ddType.ValueMember = "DataTypeName";

            if (collection != null)
            {
                LoadControlValues();
                Routine_EditControl();
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
                        case "type":
                            ddType.SelectedValue = item.Value;
                            persistedTypeName = item.Value;
                            break;
                        case "type-groups":
                            lblParent.Text = item.Value;
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

        private void Routine_NewControl()
        {
            btnUpdate.Text = "Add New Type under Type Group";
            lblParent.Text = tag["Name"];
            newType = true;
        }

        private void Routine_EditControl()
        {
            btnUpdate.Text = "Update Type (Data Type)";
            newType = false;
        }

        #endregion

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ControlManifestHelper manifestHelper = new ControlManifestHelper();

            if (newType)
            {
                manifestHelper.AddNewTypeInTypeGroup(manifestDetails, lblParent.Text, ddType.SelectedValue.ToString());
            }
            else
            {
                manifestHelper.UpdateTypeInTypeGroup(lblParent.Text, persistedTypeName, ddType.SelectedValue.ToString(), manifestDetails);
            }

            persistedTypeName = ddType.SelectedValue.ToString();
            ParentControl.RefreshControlManifestDetails();
        }
    }
}
