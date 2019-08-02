namespace Maverick.PCF.Builder.UserControls
{
    partial class Template
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Template));
            this.imgPcfControl = new System.Windows.Forms.PictureBox();
            this.lblControlName = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.linklblPCFGallery = new System.Windows.Forms.LinkLabel();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblDownload = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgPcfControl)).BeginInit();
            this.SuspendLayout();
            // 
            // imgPcfControl
            // 
            this.imgPcfControl.Location = new System.Drawing.Point(3, 3);
            this.imgPcfControl.Name = "imgPcfControl";
            this.imgPcfControl.Size = new System.Drawing.Size(140, 140);
            this.imgPcfControl.TabIndex = 0;
            this.imgPcfControl.TabStop = false;
            // 
            // lblControlName
            // 
            this.lblControlName.AutoSize = true;
            this.lblControlName.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlName.Location = new System.Drawing.Point(149, 3);
            this.lblControlName.Name = "lblControlName";
            this.lblControlName.Size = new System.Drawing.Size(137, 30);
            this.lblControlName.TabIndex = 1;
            this.lblControlName.Text = "ControlName";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthor.Location = new System.Drawing.Point(151, 39);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(58, 21);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Author";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(152, 68);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(389, 61);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description";
            // 
            // linklblPCFGallery
            // 
            this.linklblPCFGallery.AutoSize = true;
            this.linklblPCFGallery.Location = new System.Drawing.Point(151, 130);
            this.linklblPCFGallery.Name = "linklblPCFGallery";
            this.linklblPCFGallery.Size = new System.Drawing.Size(152, 13);
            this.linklblPCFGallery.TabIndex = 4;
            this.linklblPCFGallery.TabStop = true;
            this.linklblPCFGallery.Text = "Link To Control on PCF Gallery";
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.Image")));
            this.btnDownload.Location = new System.Drawing.Point(600, 39);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(60, 60);
            this.btnDownload.TabIndex = 5;
            this.btnDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDownload.UseVisualStyleBackColor = true;
            // 
            // lblDownload
            // 
            this.lblDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDownload.Location = new System.Drawing.Point(600, 106);
            this.lblDownload.Name = "lblDownload";
            this.lblDownload.Size = new System.Drawing.Size(60, 23);
            this.lblDownload.TabIndex = 6;
            this.lblDownload.Text = "Download";
            this.lblDownload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Template
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Controls.Add(this.lblDownload);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.linklblPCFGallery);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblControlName);
            this.Controls.Add(this.imgPcfControl);
            this.Name = "Template";
            this.Size = new System.Drawing.Size(680, 146);
            ((System.ComponentModel.ISupportInitialize)(this.imgPcfControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgPcfControl;
        private System.Windows.Forms.Label lblControlName;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.LinkLabel linklblPCFGallery;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lblDownload;
    }
}
