﻿namespace Maverick.PCF.Builder.Forms
{
    partial class ShowPreviewImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowPreviewImage));
            this.gboxPreviewImage = new System.Windows.Forms.GroupBox();
            this.pnlTypes = new System.Windows.Forms.Panel();
            this.lblTypes = new System.Windows.Forms.Label();
            this.btnChangePreviewImage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pboxPreviewImage = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ofdPreviewImage = new System.Windows.Forms.OpenFileDialog();
            this.gboxPreviewImage.SuspendLayout();
            this.pnlTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxPreviewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // gboxPreviewImage
            // 
            this.gboxPreviewImage.Controls.Add(this.pnlTypes);
            this.gboxPreviewImage.Controls.Add(this.btnChangePreviewImage);
            this.gboxPreviewImage.Controls.Add(this.label2);
            this.gboxPreviewImage.Controls.Add(this.pboxPreviewImage);
            this.gboxPreviewImage.Controls.Add(this.label3);
            this.gboxPreviewImage.Controls.Add(this.lblDescription);
            this.gboxPreviewImage.Controls.Add(this.lblDisplayName);
            this.gboxPreviewImage.Controls.Add(this.label7);
            this.gboxPreviewImage.Location = new System.Drawing.Point(12, 12);
            this.gboxPreviewImage.Name = "gboxPreviewImage";
            this.gboxPreviewImage.Size = new System.Drawing.Size(531, 263);
            this.gboxPreviewImage.TabIndex = 7;
            this.gboxPreviewImage.TabStop = false;
            this.gboxPreviewImage.Text = "Preview Image";
            // 
            // pnlTypes
            // 
            this.pnlTypes.AutoScroll = true;
            this.pnlTypes.Controls.Add(this.lblTypes);
            this.pnlTypes.Location = new System.Drawing.Point(79, 83);
            this.pnlTypes.Name = "pnlTypes";
            this.pnlTypes.Size = new System.Drawing.Size(206, 68);
            this.pnlTypes.TabIndex = 8;
            // 
            // lblTypes
            // 
            this.lblTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTypes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypes.Location = new System.Drawing.Point(0, 0);
            this.lblTypes.Name = "lblTypes";
            this.lblTypes.Size = new System.Drawing.Size(206, 68);
            this.lblTypes.TabIndex = 4;
            this.lblTypes.Text = "-";
            // 
            // btnChangePreviewImage
            // 
            this.btnChangePreviewImage.Location = new System.Drawing.Point(368, 200);
            this.btnChangePreviewImage.Name = "btnChangePreviewImage";
            this.btnChangePreviewImage.Size = new System.Drawing.Size(57, 23);
            this.btnChangePreviewImage.TabIndex = 7;
            this.btnChangePreviewImage.Text = "Update";
            this.btnChangePreviewImage.UseVisualStyleBackColor = true;
            this.btnChangePreviewImage.Click += new System.EventHandler(this.btnChangePreviewImage_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(468, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "This is how your control will display on the \"Add Control\" page in Model-driven a" +
    "pp";
            // 
            // pboxPreviewImage
            // 
            this.pboxPreviewImage.Image = ((System.Drawing.Image)(resources.GetObject("pboxPreviewImage.Image")));
            this.pboxPreviewImage.Location = new System.Drawing.Point(310, 59);
            this.pboxPreviewImage.Name = "pboxPreviewImage";
            this.pboxPreviewImage.Size = new System.Drawing.Size(170, 130);
            this.pboxPreviewImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxPreviewImage.TabIndex = 6;
            this.pboxPreviewImage.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Firebrick;
            this.label3.Location = new System.Drawing.Point(9, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Preview Image needs to be 17:13 ratio.";
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(9, 161);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(276, 62);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "-";
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayName.Location = new System.Drawing.Point(9, 59);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(40, 15);
            this.lblDisplayName.TabIndex = 2;
            this.lblDisplayName.Text = "label2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "Types:";
            // 
            // ofdPreviewImage
            // 
            this.ofdPreviewImage.Filter = "Image files|*.png;*.jpg;*.jpeg;*gif";
            this.ofdPreviewImage.Title = "Select new Preview Image";
            // 
            // ShowPreviewImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 287);
            this.Controls.Add(this.gboxPreviewImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ShowPreviewImage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Preview Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowPreviewImage_FormClosing);
            this.gboxPreviewImage.ResumeLayout(false);
            this.gboxPreviewImage.PerformLayout();
            this.pnlTypes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pboxPreviewImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gboxPreviewImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pboxPreviewImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.Label lblTypes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnChangePreviewImage;
        private System.Windows.Forms.OpenFileDialog ofdPreviewImage;
        private System.Windows.Forms.Panel pnlTypes;
    }
}