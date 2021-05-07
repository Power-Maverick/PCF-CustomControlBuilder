
namespace Maverick.PCF.Builder.UserControls
{
    partial class PropertyControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.txtPropertyName = new System.Windows.Forms.TextBox();
            this.txtDisplayNameKey = new System.Windows.Forms.TextBox();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.txtDescriptionKey = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblUsage = new System.Windows.Forms.Label();
            this.ddOfType = new System.Windows.Forms.ComboBox();
            this.ddUsage = new System.Windows.Forms.ComboBox();
            this.chkRequired = new System.Windows.Forms.CheckBox();
            this.chkUseTypeGroup = new System.Windows.Forms.CheckBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblPropertyNotes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPropertyName.Location = new System.Drawing.Point(6, 28);
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(199, 20);
            this.txtPropertyName.TabIndex = 1;
            // 
            // txtDisplayNameKey
            // 
            this.txtDisplayNameKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDisplayNameKey.Location = new System.Drawing.Point(6, 69);
            this.txtDisplayNameKey.Name = "txtDisplayNameKey";
            this.txtDisplayNameKey.Size = new System.Drawing.Size(199, 20);
            this.txtDisplayNameKey.TabIndex = 3;
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(3, 53);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(99, 13);
            this.lblDisplayName.TabIndex = 2;
            this.lblDisplayName.Text = "Display Name (Key)";
            // 
            // txtDescriptionKey
            // 
            this.txtDescriptionKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescriptionKey.Location = new System.Drawing.Point(6, 110);
            this.txtDescriptionKey.Multiline = true;
            this.txtDescriptionKey.Name = "txtDescriptionKey";
            this.txtDescriptionKey.Size = new System.Drawing.Size(199, 80);
            this.txtDescriptionKey.TabIndex = 5;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(3, 94);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(87, 13);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description (Key)";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(3, 222);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(90, 13);
            this.lblType.TabIndex = 6;
            this.lblType.Text = "Type (Data Type)";
            // 
            // lblUsage
            // 
            this.lblUsage.AutoSize = true;
            this.lblUsage.Location = new System.Drawing.Point(3, 270);
            this.lblUsage.Name = "lblUsage";
            this.lblUsage.Size = new System.Drawing.Size(38, 13);
            this.lblUsage.TabIndex = 8;
            this.lblUsage.Text = "Usage";
            // 
            // ddOfType
            // 
            this.ddOfType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddOfType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddOfType.FormattingEnabled = true;
            this.ddOfType.Location = new System.Drawing.Point(6, 239);
            this.ddOfType.MaxDropDownItems = 4;
            this.ddOfType.Name = "ddOfType";
            this.ddOfType.Size = new System.Drawing.Size(199, 21);
            this.ddOfType.TabIndex = 12;
            this.ddOfType.SelectedIndexChanged += new System.EventHandler(this.ddOfType_SelectedIndexChanged);
            // 
            // ddUsage
            // 
            this.ddUsage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddUsage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddUsage.FormattingEnabled = true;
            this.ddUsage.Location = new System.Drawing.Point(6, 286);
            this.ddUsage.MaxDropDownItems = 4;
            this.ddUsage.Name = "ddUsage";
            this.ddUsage.Size = new System.Drawing.Size(199, 21);
            this.ddUsage.TabIndex = 13;
            // 
            // chkRequired
            // 
            this.chkRequired.AutoSize = true;
            this.chkRequired.Location = new System.Drawing.Point(3, 313);
            this.chkRequired.Name = "chkRequired";
            this.chkRequired.Size = new System.Drawing.Size(69, 17);
            this.chkRequired.TabIndex = 14;
            this.chkRequired.Text = "Required";
            this.chkRequired.UseVisualStyleBackColor = true;
            // 
            // chkUseTypeGroup
            // 
            this.chkUseTypeGroup.AutoSize = true;
            this.chkUseTypeGroup.Location = new System.Drawing.Point(6, 196);
            this.chkUseTypeGroup.Name = "chkUseTypeGroup";
            this.chkUseTypeGroup.Size = new System.Drawing.Size(104, 17);
            this.chkUseTypeGroup.TabIndex = 15;
            this.chkUseTypeGroup.Text = "Use Type Group";
            this.chkUseTypeGroup.UseVisualStyleBackColor = true;
            this.chkUseTypeGroup.CheckedChanged += new System.EventHandler(this.chkUseTypeGroup_CheckedChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(6, 412);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(199, 23);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "Update Property";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblPropertyNotes
            // 
            this.lblPropertyNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPropertyNotes.AutoEllipsis = true;
            this.lblPropertyNotes.ForeColor = System.Drawing.Color.BlueViolet;
            this.lblPropertyNotes.Location = new System.Drawing.Point(3, 344);
            this.lblPropertyNotes.Name = "lblPropertyNotes";
            this.lblPropertyNotes.Size = new System.Drawing.Size(202, 50);
            this.lblPropertyNotes.TabIndex = 17;
            this.lblPropertyNotes.Text = "*The selected \'Type\' is deprecated and will not work with newer version of Power " +
    "Apps CLI";
            this.lblPropertyNotes.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblPropertyNotes.Visible = false;
            // 
            // PropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPropertyNotes);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.chkUseTypeGroup);
            this.Controls.Add(this.chkRequired);
            this.Controls.Add(this.ddUsage);
            this.Controls.Add(this.ddOfType);
            this.Controls.Add(this.lblUsage);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtDescriptionKey);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDisplayNameKey);
            this.Controls.Add(this.lblDisplayName);
            this.Controls.Add(this.txtPropertyName);
            this.Controls.Add(this.lblName);
            this.Name = "PropertyControl";
            this.Size = new System.Drawing.Size(218, 448);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtPropertyName;
        private System.Windows.Forms.TextBox txtDisplayNameKey;
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.TextBox txtDescriptionKey;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblUsage;
        private System.Windows.Forms.ComboBox ddOfType;
        private System.Windows.Forms.ComboBox ddUsage;
        private System.Windows.Forms.CheckBox chkRequired;
        private System.Windows.Forms.CheckBox chkUseTypeGroup;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblPropertyNotes;
    }
}
