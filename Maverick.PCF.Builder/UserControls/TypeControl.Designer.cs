
namespace Maverick.PCF.Builder.UserControls
{
    partial class TypeControl
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
            this.ddType = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblParent = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ddType
            // 
            this.ddType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddType.FormattingEnabled = true;
            this.ddType.Location = new System.Drawing.Point(6, 27);
            this.ddType.Name = "ddType";
            this.ddType.Size = new System.Drawing.Size(184, 21);
            this.ddType.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 11);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(90, 13);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Type (Data Type)";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(6, 68);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(184, 23);
            this.btnUpdate.TabIndex = 17;
            this.btnUpdate.Text = "Update Type (Data Type)";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblParent
            // 
            this.lblParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblParent.AutoSize = true;
            this.lblParent.Location = new System.Drawing.Point(3, 139);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(22, 13);
            this.lblParent.TabIndex = 19;
            this.lblParent.Text = "TG";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Parent TypeGroup:";
            // 
            // TypeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblParent);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.ddType);
            this.Name = "TypeControl";
            this.Size = new System.Drawing.Size(222, 493);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.Label label1;
    }
}
