
namespace Maverick.PCF.Builder.Forms
{
    partial class FeaturesForm
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
            this.chkCaptureAudio = new System.Windows.Forms.CheckBox();
            this.chkCaptureImage = new System.Windows.Forms.CheckBox();
            this.chkCaptureVideo = new System.Windows.Forms.CheckBox();
            this.chkGetBarcodeValue = new System.Windows.Forms.CheckBox();
            this.chkGetCurrentPosition = new System.Windows.Forms.CheckBox();
            this.chkPickFile = new System.Windows.Forms.CheckBox();
            this.chkUtility = new System.Windows.Forms.CheckBox();
            this.chkWebApi = new System.Windows.Forms.CheckBox();
            this.btnCloseBuild = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkCaptureAudio
            // 
            this.chkCaptureAudio.AutoSize = true;
            this.chkCaptureAudio.Location = new System.Drawing.Point(55, 11);
            this.chkCaptureAudio.Name = "chkCaptureAudio";
            this.chkCaptureAudio.Size = new System.Drawing.Size(126, 17);
            this.chkCaptureAudio.TabIndex = 1;
            this.chkCaptureAudio.Text = "Device.captureAudio";
            this.chkCaptureAudio.UseVisualStyleBackColor = true;
            this.chkCaptureAudio.CheckedChanged += new System.EventHandler(this.FeatureCheckbox_CheckedChanged);
            // 
            // chkCaptureImage
            // 
            this.chkCaptureImage.AutoSize = true;
            this.chkCaptureImage.Location = new System.Drawing.Point(55, 34);
            this.chkCaptureImage.Name = "chkCaptureImage";
            this.chkCaptureImage.Size = new System.Drawing.Size(128, 17);
            this.chkCaptureImage.TabIndex = 2;
            this.chkCaptureImage.Text = "Device.captureImage";
            this.chkCaptureImage.UseVisualStyleBackColor = true;
            this.chkCaptureImage.CheckedChanged += new System.EventHandler(this.FeatureCheckbox_CheckedChanged);
            // 
            // chkCaptureVideo
            // 
            this.chkCaptureVideo.AutoSize = true;
            this.chkCaptureVideo.Location = new System.Drawing.Point(55, 57);
            this.chkCaptureVideo.Name = "chkCaptureVideo";
            this.chkCaptureVideo.Size = new System.Drawing.Size(126, 17);
            this.chkCaptureVideo.TabIndex = 3;
            this.chkCaptureVideo.Text = "Device.captureVideo";
            this.chkCaptureVideo.UseVisualStyleBackColor = true;
            this.chkCaptureVideo.CheckedChanged += new System.EventHandler(this.FeatureCheckbox_CheckedChanged);
            // 
            // chkGetBarcodeValue
            // 
            this.chkGetBarcodeValue.AutoSize = true;
            this.chkGetBarcodeValue.Location = new System.Drawing.Point(55, 80);
            this.chkGetBarcodeValue.Name = "chkGetBarcodeValue";
            this.chkGetBarcodeValue.Size = new System.Drawing.Size(145, 17);
            this.chkGetBarcodeValue.TabIndex = 4;
            this.chkGetBarcodeValue.Text = "Device.getBarcodeValue";
            this.chkGetBarcodeValue.UseVisualStyleBackColor = true;
            this.chkGetBarcodeValue.CheckedChanged += new System.EventHandler(this.FeatureCheckbox_CheckedChanged);
            // 
            // chkGetCurrentPosition
            // 
            this.chkGetCurrentPosition.AutoSize = true;
            this.chkGetCurrentPosition.Location = new System.Drawing.Point(55, 103);
            this.chkGetCurrentPosition.Name = "chkGetCurrentPosition";
            this.chkGetCurrentPosition.Size = new System.Drawing.Size(149, 17);
            this.chkGetCurrentPosition.TabIndex = 5;
            this.chkGetCurrentPosition.Text = "Device.getCurrentPosition";
            this.chkGetCurrentPosition.UseVisualStyleBackColor = true;
            this.chkGetCurrentPosition.CheckedChanged += new System.EventHandler(this.FeatureCheckbox_CheckedChanged);
            // 
            // chkPickFile
            // 
            this.chkPickFile.AutoSize = true;
            this.chkPickFile.Location = new System.Drawing.Point(55, 126);
            this.chkPickFile.Name = "chkPickFile";
            this.chkPickFile.Size = new System.Drawing.Size(99, 17);
            this.chkPickFile.TabIndex = 6;
            this.chkPickFile.Text = "Device.pickFile";
            this.chkPickFile.UseVisualStyleBackColor = true;
            this.chkPickFile.CheckedChanged += new System.EventHandler(this.FeatureCheckbox_CheckedChanged);
            // 
            // chkUtility
            // 
            this.chkUtility.AutoSize = true;
            this.chkUtility.Location = new System.Drawing.Point(55, 149);
            this.chkUtility.Name = "chkUtility";
            this.chkUtility.Size = new System.Drawing.Size(51, 17);
            this.chkUtility.TabIndex = 7;
            this.chkUtility.Text = "Utility";
            this.chkUtility.UseVisualStyleBackColor = true;
            this.chkUtility.CheckedChanged += new System.EventHandler(this.FeatureCheckbox_CheckedChanged);
            // 
            // chkWebApi
            // 
            this.chkWebApi.AutoSize = true;
            this.chkWebApi.Location = new System.Drawing.Point(55, 172);
            this.chkWebApi.Name = "chkWebApi";
            this.chkWebApi.Size = new System.Drawing.Size(66, 17);
            this.chkWebApi.TabIndex = 8;
            this.chkWebApi.Text = "WebAPI";
            this.chkWebApi.UseVisualStyleBackColor = true;
            this.chkWebApi.CheckedChanged += new System.EventHandler(this.FeatureCheckbox_CheckedChanged);
            // 
            // btnCloseBuild
            // 
            this.btnCloseBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCloseBuild.Location = new System.Drawing.Point(49, 205);
            this.btnCloseBuild.Name = "btnCloseBuild";
            this.btnCloseBuild.Size = new System.Drawing.Size(155, 23);
            this.btnCloseBuild.TabIndex = 9;
            this.btnCloseBuild.Text = "Close and Build";
            this.btnCloseBuild.UseVisualStyleBackColor = true;
            this.btnCloseBuild.Click += new System.EventHandler(this.btnCloseBuild_Click);
            // 
            // FeaturesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 240);
            this.Controls.Add(this.btnCloseBuild);
            this.Controls.Add(this.chkWebApi);
            this.Controls.Add(this.chkUtility);
            this.Controls.Add(this.chkPickFile);
            this.Controls.Add(this.chkGetCurrentPosition);
            this.Controls.Add(this.chkGetBarcodeValue);
            this.Controls.Add(this.chkCaptureVideo);
            this.Controls.Add(this.chkCaptureImage);
            this.Controls.Add(this.chkCaptureAudio);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FeaturesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Features";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkCaptureAudio;
        private System.Windows.Forms.CheckBox chkCaptureImage;
        private System.Windows.Forms.CheckBox chkCaptureVideo;
        private System.Windows.Forms.CheckBox chkGetBarcodeValue;
        private System.Windows.Forms.CheckBox chkGetCurrentPosition;
        private System.Windows.Forms.CheckBox chkPickFile;
        private System.Windows.Forms.CheckBox chkUtility;
        private System.Windows.Forms.CheckBox chkWebApi;
        private System.Windows.Forms.Button btnCloseBuild;
    }
}