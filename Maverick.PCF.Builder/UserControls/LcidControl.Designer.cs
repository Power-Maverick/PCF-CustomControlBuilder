namespace Maverick.PCF.Builder.UserControls
{
    partial class LcidControl
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
            this.lblLcidString = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(111, 33);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblName.Click += new System.EventHandler(this.Label_Click);
            // 
            // lblLcidString
            // 
            this.lblLcidString.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLcidString.Location = new System.Drawing.Point(0, 33);
            this.lblLcidString.Name = "lblLcidString";
            this.lblLcidString.Size = new System.Drawing.Size(111, 26);
            this.lblLcidString.TabIndex = 1;
            this.lblLcidString.Text = "label2";
            this.lblLcidString.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLcidString.Click += new System.EventHandler(this.Label_Click);
            // 
            // lblCode
            // 
            this.lblCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.Location = new System.Drawing.Point(0, 59);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(111, 21);
            this.lblCode.TabIndex = 2;
            this.lblCode.Text = "label3";
            this.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCode.Click += new System.EventHandler(this.Label_Click);
            // 
            // LcidControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lblLcidString);
            this.Controls.Add(this.lblName);
            this.Name = "LcidControl";
            this.Size = new System.Drawing.Size(110, 88);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblLcidString;
        private System.Windows.Forms.Label lblCode;
    }
}
