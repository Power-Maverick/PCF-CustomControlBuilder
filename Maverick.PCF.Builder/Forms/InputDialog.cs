using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maverick.PCF.Builder.Forms
{
    public partial class InputDialog : Form
    {
        public string TextInputValue { get; set; }

        public InputDialog()
        {
            InitializeComponent();
        }

        public InputDialog(string title, string prompt)
        {
            InitializeComponent();

            this.Text = title;
            lblPrompt.Text = prompt;
        }

        private void btnInputOk_Click(object sender, EventArgs e)
        {
            TextInputValue = txtInputValue.Text;
        }
    }
}
