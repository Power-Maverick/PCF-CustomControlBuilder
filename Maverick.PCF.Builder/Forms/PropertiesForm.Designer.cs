
namespace Maverick.PCF.Builder.Forms
{
    partial class PropertiesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCloseBuild = new System.Windows.Forms.Button();
            this.gboxNodeProperties = new System.Windows.Forms.GroupBox();
            this.pnlPropertiesContainer = new System.Windows.Forms.Panel();
            this.tvProperties = new System.Windows.Forms.TreeView();
            this.contextMenuNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.gboxNodeProperties.SuspendLayout();
            this.contextMenuNode.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.btnCloseBuild);
            this.panel1.Controls.Add(this.gboxNodeProperties);
            this.panel1.Controls.Add(this.tvProperties);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 484);
            this.panel1.TabIndex = 0;
            // 
            // btnCloseBuild
            // 
            this.btnCloseBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCloseBuild.Location = new System.Drawing.Point(12, 443);
            this.btnCloseBuild.Name = "btnCloseBuild";
            this.btnCloseBuild.Size = new System.Drawing.Size(224, 23);
            this.btnCloseBuild.TabIndex = 1;
            this.btnCloseBuild.Text = "Close and Build";
            this.btnCloseBuild.UseVisualStyleBackColor = true;
            this.btnCloseBuild.TextChanged += new System.EventHandler(this.btnCloseBuild_TextChanged);
            this.btnCloseBuild.Click += new System.EventHandler(this.btnCloseBuild_Click);
            // 
            // gboxNodeProperties
            // 
            this.gboxNodeProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxNodeProperties.Controls.Add(this.pnlPropertiesContainer);
            this.gboxNodeProperties.Location = new System.Drawing.Point(242, 12);
            this.gboxNodeProperties.Name = "gboxNodeProperties";
            this.gboxNodeProperties.Size = new System.Drawing.Size(249, 457);
            this.gboxNodeProperties.TabIndex = 4;
            this.gboxNodeProperties.TabStop = false;
            this.gboxNodeProperties.Text = "Node Properties";
            // 
            // pnlPropertiesContainer
            // 
            this.pnlPropertiesContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPropertiesContainer.Location = new System.Drawing.Point(3, 16);
            this.pnlPropertiesContainer.Name = "pnlPropertiesContainer";
            this.pnlPropertiesContainer.Size = new System.Drawing.Size(243, 438);
            this.pnlPropertiesContainer.TabIndex = 0;
            // 
            // tvProperties
            // 
            this.tvProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvProperties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvProperties.HideSelection = false;
            this.tvProperties.Location = new System.Drawing.Point(12, 12);
            this.tvProperties.Name = "tvProperties";
            this.tvProperties.ShowNodeToolTips = true;
            this.tvProperties.Size = new System.Drawing.Size(224, 428);
            this.tvProperties.TabIndex = 3;
            this.tvProperties.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProperties_AfterSelect);
            this.tvProperties.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvProperties_NodeMouseClick);
            // 
            // contextMenuNode
            // 
            this.contextMenuNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.tsmiDelete});
            this.contextMenuNode.Name = "contextMenuNode";
            this.contextMenuNode.Size = new System.Drawing.Size(181, 70);
            this.contextMenuNode.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuNode_ItemClicked);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(180, 22);
            this.tsmiAdd.Tag = "morpheus";
            this.tsmiAdd.Text = "Add";
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(180, 22);
            this.tsmiDelete.Text = "Delete";
            // 
            // PropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 484);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PropertiesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Properties Management";
            this.panel1.ResumeLayout(false);
            this.gboxNodeProperties.ResumeLayout(false);
            this.contextMenuNode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tvProperties;
        private System.Windows.Forms.ContextMenuStrip contextMenuNode;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.GroupBox gboxNodeProperties;
        private System.Windows.Forms.Panel pnlPropertiesContainer;
        private System.Windows.Forms.Button btnCloseBuild;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
    }
}