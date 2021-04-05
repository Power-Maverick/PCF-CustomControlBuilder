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
using Maverick.PCF.Builder.Extensions;
using System.Xml;
using System.Reflection;
using Maverick.PCF.Builder.Helper;
using Maverick.PCF.Builder.UserControls;
using Maverick.PCF.Builder.DataObjects;

namespace Maverick.PCF.Builder.Forms
{
    public partial class PropertiesForm : Form
    {
        public PCFBuilder ParentControl { get; set; }

        #region Private Variables

        private string nodeGroupPropertiesName = "properties";
        private string nodeGroupTypeGroupsName = "type-groups";
        private string nodePropertyName = "property";
        private string nodeTypeGroupName = "type-group";
        private string nodeTypeName = "type";
        private bool needsPCFProjectBuild = false;
        #endregion

        #region Private Methods

        private void LoadTreeView()
        {
            tvProperties.Nodes.Clear();
            TreeNode propertiesTreeNode = CreateTreeNode(nodeGroupPropertiesName);
            TreeNode typeGroupsTreeNode = CreateTreeNode(nodeGroupTypeGroupsName);
            tvProperties.Nodes.Add(propertiesTreeNode);
            tvProperties.Nodes.Add(typeGroupsTreeNode);

            foreach (var item in ParentControl.ControlDetails.Properties)
            {
                TreeNode propertyTreeNode = CreateTreeNode(item);
                propertiesTreeNode.Nodes.Add(propertyTreeNode);
            }

            foreach (var item in ParentControl.ControlDetails.TypeGroups)
            {
                TreeNode typeGroupTreeNode = CreateTreeNode(item);
                Dictionary<string, string> specialAttributes = new Dictionary<string, string>();
                specialAttributes.Add(nodeGroupTypeGroupsName, item.Name);

                foreach (var type in item.Types)
                {
                    typeGroupTreeNode.Nodes.Add(CreateTreeNode(type, nodeTypeName, specialAttributes));
                }

                typeGroupsTreeNode.Nodes.Add(typeGroupTreeNode);
            }

            tvProperties.ExpandAll();
        }

        private TreeNode CreateTreeNode<T>(T item, string definedType = "", Dictionary<string, string> specialAttributes = null)
        {
            TreeNode node = new TreeNode();

            if (typeof(T).Name.ToLower().Equals("string"))
            {
                Dictionary<string, string> attributes = new Dictionary<string, string>();

                node.Name = string.IsNullOrEmpty(definedType) ? item.ToString() : definedType;
                node.Text = item.ToString();

                if (specialAttributes != null)
                {
                    foreach (var attr in specialAttributes)
                    {
                        attributes.Add(attr.Key, attr.Value);
                    }
                }

                attributes.Add(definedType, item.ToString());
                node.Tag = attributes;

            }
            else
            {
                var name = typeof(T).GetProperty("Name").GetValue(item);
                var type = typeof(T).Name.ToLower() == "controlproperty" ? nodePropertyName : nodeTypeGroupName;

                node.Name = type;
                node.Text = $"{name}";
                node.Tag = PreparePropertyAttributes(item);
            }

            return node;
        }

        private Dictionary<string, string> PreparePropertyAttributes<T>(T item)
        {
            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties();

            Dictionary<string, string> attributes = new Dictionary<string, string>();

            Array.Sort(
                propertyInfos,
                delegate (PropertyInfo p1, PropertyInfo p2)
                {
                    return p1.Name.CompareTo(p2.Name);
                }
            );
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.Name.ToLower().Equals("types"))
                {
                    attributes.Add(propertyInfo.Name, string.Join(";", ((List<string>)propertyInfo.GetValue(item))));
                }
                else
                {
                    attributes.Add(propertyInfo.Name, propertyInfo.GetValue(item)?.ToString());
                }
            }

            return attributes;
        }

        private void HandleNodeSelection(TreeNode node)
        {
            if (tvProperties.SelectedNode != node)
            {
                tvProperties.SelectedNode = node;
            }
            LoadContextMenuForTreeNode(node);

            UserControl ctrl = null;
            pnlPropertiesContainer.Controls.Clear();
            if (node != null)
            {
                var attributes = (Dictionary<string, string>)node.Tag;

                switch (node.Name.ToLower())
                {
                    case "property":
                        ctrl = new PropertyControl(this, attributes, ParentControl.ControlDetails);
                        break;
                    case "type-group":
                        ctrl = new TypeGroupControl(this, attributes, ParentControl.ControlDetails);
                        break;
                    case "type":
                        ctrl = new TypeControl(this, attributes, ParentControl.ControlDetails);
                        break;
                    default:
                        break;
                }
                node.ContextMenuStrip = this.contextMenuNode;
            }
            if (ctrl != null)
            {
                pnlPropertiesContainer.Controls.Add(ctrl);
                InformProjectNeedsBuild();
                ctrl.BringToFront();
            }
        }

        private void LoadContextMenuForTreeNode(TreeNode node)
        {
            if (node != null)
            {
                switch (node.Name.ToLower())
                {
                    case "properties":
                        tsmiAdd.Visible = true;
                        tsmiAdd.Text = "Add Property";
                        break;
                    case "type-groups":
                        tsmiAdd.Visible = true;
                        tsmiAdd.Text = "Add Type Group";
                        break;
                    case "type-group":
                        tsmiAdd.Visible = true;
                        tsmiAdd.Text = "Add Type";
                        break;
                    case "type":
                    case "property":
                        tsmiAdd.Visible = false;
                        break;
                    default:
                        tsmiAdd.Visible = false;
                        break;
                }
                tsmiAdd.Tag = node.Tag;
                node.ContextMenuStrip = this.contextMenuNode;
            }
        }

        private void HandleNodeMenuClick(string clickedNode, object tag)
        {
            UserControl ctrl = null;
            pnlPropertiesContainer.Controls.Clear();
            switch (clickedNode)
            {
                case "property":
                    ctrl = new PropertyControl(this, null, ParentControl.ControlDetails);
                    break;
                case "type-group":
                    ctrl = new TypeGroupControl(this, null, ParentControl.ControlDetails);
                    break;
                case "type":
                    ctrl = new TypeControl(this, null, ParentControl.ControlDetails, tag);
                    break;
                default:
                    break;
            }
            if (ctrl != null)
            {
                pnlPropertiesContainer.Controls.Add(ctrl);
                InformProjectNeedsBuild();
                ctrl.BringToFront();
            }
        }

        private void InformProjectNeedsBuild()
        {
            btnCloseBuild.Text = "Close and Build";
            needsPCFProjectBuild = true;
        }

        #endregion

        #region Constructor

        public PropertiesForm(PCFBuilder parent)
        {
            InitializeComponent();

            ParentControl = parent;
            btnCloseBuild.Text = "Close";
            LoadTreeView();
        }

        #endregion

        #region Event Handlers

        private void tvProperties_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                HandleNodeSelection(e.Node);
            }
        }

        private void tvProperties_AfterSelect(object sender, TreeViewEventArgs e)
        {
            HandleNodeSelection(e.Node);
        }

        private void contextMenuNode_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string nodeClickedName = string.Empty;

            switch (e.ClickedItem.Text.ToLower())
            {
                case string a when a.Contains("property"):
                    nodeClickedName = nodePropertyName;
                    break;
                case string a when a.Contains("type group"):
                    nodeClickedName = nodeTypeGroupName;
                    break;
                case string a when a.Contains("type"):
                    nodeClickedName = nodeTypeName;
                    break;
                default:
                    nodeClickedName = e.ClickedItem.Tag?.ToString();
                    break;
            }

            HandleNodeMenuClick(nodeClickedName, e.ClickedItem.Tag);
        }

        private void btnCloseBuild_Click(object sender, EventArgs e)
        {
            var parent = this.ParentControl as PCFBuilder;
            parent.InitiatePCFProjectBuild = needsPCFProjectBuild;
            this.Close();
        }

        private void btnCloseBuild_TextChanged(object sender, EventArgs e)
        {
            if (btnCloseBuild.Text.ToLower().Equals("close"))
            {
                needsPCFProjectBuild = false;
            }
            else
            {
                needsPCFProjectBuild = true;
            }
        }

        #endregion

        #region Public Methods

        public void RefreshControlManifestDetails()
        {
            ControlManifestHelper helper = new ControlManifestHelper();
            ParentControl.ControlDetails = helper.GetControlManifestDetails(ParentControl.ControlDetails.WorkingFolderPath);

            LoadTreeView();
        }

        #endregion
    }
}
