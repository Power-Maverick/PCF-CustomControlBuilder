namespace Maverick.PCF.Builder.Forms
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
            this.label2 = new System.Windows.Forms.Label();
            this.pboxPreviewImage = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.lblTypes = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnChangePreviewImage = new System.Windows.Forms.Button();
            this.ofdPreviewImage = new System.Windows.Forms.OpenFileDialog();
            this.pnlTypes = new System.Windows.Forms.Panel();
            this.gboxPreviewImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxPreviewImage)).BeginInit();
            this.pnlTypes.SuspendLayout();
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
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(511, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Preview Image only works for Model-driven apps. Controls with preview images migh" +
    "t break in Canvas apps";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(10, 156);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(12, 15);
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
            // lblTypes
            // 
            this.lblTypes.AutoSize = true;
            this.lblTypes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypes.Location = new System.Drawing.Point(3, 0);
            this.lblTypes.Name = "lblTypes";
            this.lblTypes.Size = new System.Drawing.Size(12, 15);
            this.lblTypes.TabIndex = 4;
            this.lblTypes.Text = "-";
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
            // ofdPreviewImage
            // 
            this.ofdPreviewImage.Filter = "Image files|*.png;*.jpg;*.jpeg;*gif";
            this.ofdPreviewImage.Title = "Select new Preview Image";
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
            // ShowPreviewImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 287);
            this.Controls.Add(this.gboxPreviewImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ShowPreviewImage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ShowPreviewImage";
            this.gboxPreviewImage.ResumeLayout(false);
            this.gboxPreviewImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxPreviewImage)).EndInit();
            this.pnlTypes.ResumeLayout(false);
            this.pnlTypes.PerformLayout();
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