namespace Maverick.PCF.Builder.Forms
{
    partial class InputDialog
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
            this.btnInputCancel = new System.Windows.Forms.Button();
            this.btnInputOk = new System.Windows.Forms.Button();
            this.txtInputValue = new System.Windows.Forms.TextBox();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnInputCancel
            // 
            this.btnInputCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnInputCancel.Location = new System.Drawing.Point(280, 52);
            this.btnInputCancel.Name = "btnInputCancel";
            this.btnInputCancel.Size = new System.Drawing.Size(75, 23);
            this.btnInputCancel.TabIndex = 7;
            this.btnInputCancel.Text = "Cancel";
            this.btnInputCancel.UseVisualStyleBackColor = true;
            // 
            // btnInputOk
            // 
            this.btnInputOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnInputOk.Location = new System.Drawing.Point(199, 52);
            this.btnInputOk.Name = "btnInputOk";
            this.btnInputOk.Size = new System.Drawing.Size(75, 23);
            this.btnInputOk.TabIndex = 6;
            this.btnInputOk.Text = "OK";
            this.btnInputOk.UseVisualStyleBackColor = true;
            this.btnInputOk.Click += new System.EventHandler(this.btnInputOk_Click);
            // 
            // txtInputValue
            // 
            this.txtInputValue.Location = new System.Drawing.Point(13, 26);
            this.txtInputValue.Name = "txtInputValue";
            this.txtInputValue.Size = new System.Drawing.Size(342, 20);
            this.txtInputValue.TabIndex = 5;
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Location = new System.Drawing.Point(13, 10);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(0, 13);
            this.lblPrompt.TabIndex = 4;
            // 
            // InputDialog
            // 
            this.AcceptButton = this.btnInputOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnInputCancel;
            this.ClientSize = new System.Drawing.Size(368, 88);
            this.Controls.Add(this.btnInputCancel);
            this.Controls.Add(this.btnInputOk);
            this.Controls.Add(this.txtInputValue);
            this.Controls.Add(this.lblPrompt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InputDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InputDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInputCancel;
        private System.Windows.Forms.Button btnInputOk;
        private System.Windows.Forms.TextBox txtInputValue;
        private System.Windows.Forms.Label lblPrompt;
    }
}