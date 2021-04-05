
namespace Maverick.PCF.Builder.UserControls
{
    partial class TypeGroupControl
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
            this.txtTypeGroupName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.gboxDetails = new System.Windows.Forms.GroupBox();
            this.lblTypes = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblReferences = new System.Windows.Forms.Label();
            this.lblReferenceCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.ddType = new System.Windows.Forms.ComboBox();
            this.gboxDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTypeGroupName
            // 
            this.txtTypeGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTypeGroupName.Location = new System.Drawing.Point(9, 28);
            this.txtTypeGroupName.Name = "txtTypeGroupName";
            this.txtTypeGroupName.Size = new System.Drawing.Size(199, 20);
            this.txtTypeGroupName.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(94, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Type Group Name";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(9, 76);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(199, 23);
            this.btnUpdate.TabIndex = 17;
            this.btnUpdate.Text = "Update Type Group Name";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.Location = new System.Drawing.Point(6, 25);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(187, 34);
            this.lblMessage.TabIndex = 18;
            this.lblMessage.Text = "Updating the TypeGroup name will also update all it\'s references";
            // 
            // gboxDetails
            // 
            this.gboxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxDetails.Controls.Add(this.lblTypes);
            this.gboxDetails.Controls.Add(this.label2);
            this.gboxDetails.Controls.Add(this.lblReferences);
            this.gboxDetails.Controls.Add(this.lblReferenceCount);
            this.gboxDetails.Controls.Add(this.label1);
            this.gboxDetails.Controls.Add(this.lblMessage);
            this.gboxDetails.Location = new System.Drawing.Point(9, 104);
            this.gboxDetails.Name = "gboxDetails";
            this.gboxDetails.Size = new System.Drawing.Size(199, 329);
            this.gboxDetails.TabIndex = 19;
            this.gboxDetails.TabStop = false;
            this.gboxDetails.Text = "Insights";
            // 
            // lblTypes
            // 
            this.lblTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTypes.Location = new System.Drawing.Point(6, 240);
            this.lblTypes.Name = "lblTypes";
            this.lblTypes.Size = new System.Drawing.Size(187, 86);
            this.lblTypes.TabIndex = 23;
            this.lblTypes.Text = "1 Types (max. 6)\r\n2\r\n3\r\n4\r\n5\r\n6";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Types under this TypeGroup:";
            // 
            // lblReferences
            // 
            this.lblReferences.Location = new System.Drawing.Point(6, 116);
            this.lblReferences.Name = "lblReferences";
            this.lblReferences.Size = new System.Drawing.Size(187, 87);
            this.lblReferences.TabIndex = 20;
            this.lblReferences.Text = "1 Property Ref (max. 6)\r\n2\r\n3\r\n4\r\n5\r\n6";
            // 
            // lblReferenceCount
            // 
            this.lblReferenceCount.AutoSize = true;
            this.lblReferenceCount.Location = new System.Drawing.Point(6, 94);
            this.lblReferenceCount.Name = "lblReferenceCount";
            this.lblReferenceCount.Size = new System.Drawing.Size(53, 13);
            this.lblReferenceCount.TabIndex = 21;
            this.lblReferenceCount.Text = "Count = 0";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(6, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 35);
            this.label1.TabIndex = 20;
            this.label1.Text = "Properties using this TypeGroup as a reference:";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(6, 60);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(126, 13);
            this.lblType.TabIndex = 21;
            this.lblType.Text = "Initial Type of TypeGroup";
            // 
            // ddType
            // 
            this.ddType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddType.FormattingEnabled = true;
            this.ddType.Location = new System.Drawing.Point(9, 76);
            this.ddType.Name = "ddType";
            this.ddType.Size = new System.Drawing.Size(199, 21);
            this.ddType.TabIndex = 20;
            // 
            // TypeGroupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.ddType);
            this.Controls.Add(this.gboxDetails);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtTypeGroupName);
            this.Controls.Add(this.lblName);
            this.Name = "TypeGroupControl";
            this.Size = new System.Drawing.Size(226, 449);
            this.gboxDetails.ResumeLayout(false);
            this.gboxDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTypeGroupName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.GroupBox gboxDetails;
        private System.Windows.Forms.Label lblReferenceCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTypes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblReferences;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox ddType;
    }
}
