
namespace Maverick.PCF.Builder.Forms
{
    partial class ControlDetailsForm
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
            this.txtControlDisplayName = new System.Windows.Forms.TextBox();
            this.txtControlDescription = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblControlDescription = new System.Windows.Forms.Label();
            this.lblControlDisplayName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnChangeControlDetails = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtControlDisplayName
            // 
            this.txtControlDisplayName.Location = new System.Drawing.Point(92, 12);
            this.txtControlDisplayName.Name = "txtControlDisplayName";
            this.txtControlDisplayName.Size = new System.Drawing.Size(173, 20);
            this.txtControlDisplayName.TabIndex = 0;
            // 
            // txtControlDescription
            // 
            this.txtControlDescription.Location = new System.Drawing.Point(92, 38);
            this.txtControlDescription.Multiline = true;
            this.txtControlDescription.Name = "txtControlDescription";
            this.txtControlDescription.Size = new System.Drawing.Size(173, 74);
            this.txtControlDescription.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.96407F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.03593F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(559, 195);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Details";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(282, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "Change Details";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblControlDescription);
            this.panel1.Controls.Add(this.lblControlDisplayName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 154);
            this.panel1.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Description:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Display Name:";
            // 
            // lblControlDescription
            // 
            this.lblControlDescription.AutoSize = true;
            this.lblControlDescription.Location = new System.Drawing.Point(87, 41);
            this.lblControlDescription.Name = "lblControlDescription";
            this.lblControlDescription.Size = new System.Drawing.Size(40, 13);
            this.lblControlDescription.TabIndex = 1;
            this.lblControlDescription.Text = "Control";
            // 
            // lblControlDisplayName
            // 
            this.lblControlDisplayName.AutoSize = true;
            this.lblControlDisplayName.Location = new System.Drawing.Point(87, 15);
            this.lblControlDisplayName.Name = "lblControlDisplayName";
            this.lblControlDisplayName.Size = new System.Drawing.Size(41, 13);
            this.lblControlDisplayName.TabIndex = 0;
            this.lblControlDisplayName.Text = "Display";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnChangeControlDetails);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtControlDescription);
            this.panel2.Controls.Add(this.txtControlDisplayName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(282, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(274, 154);
            this.panel2.TabIndex = 3;
            // 
            // btnChangeControlDetails
            // 
            this.btnChangeControlDetails.Location = new System.Drawing.Point(139, 118);
            this.btnChangeControlDetails.Name = "btnChangeControlDetails";
            this.btnChangeControlDetails.Size = new System.Drawing.Size(126, 23);
            this.btnChangeControlDetails.TabIndex = 6;
            this.btnChangeControlDetails.Text = "Change Control Details";
            this.btnChangeControlDetails.UseVisualStyleBackColor = true;
            this.btnChangeControlDetails.Click += new System.EventHandler(this.btnChangeControlDetails_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Description:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Display Name:";
            // 
            // ControlDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 195);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ControlDetailsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control Manifest Details";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtControlDisplayName;
        private System.Windows.Forms.TextBox txtControlDescription;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblControlDescription;
        private System.Windows.Forms.Label lblControlDisplayName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnChangeControlDetails;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}