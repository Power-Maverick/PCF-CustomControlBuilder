using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maverick.PCF.Builder.UserControls
{
    public partial class LcidControl : UserControl
    {
        public LcidControl(string name, string lcidString, string code)
        {
            InitializeComponent();

            lblName.Text = name;
            lblLcidString.Text = lcidString;
            lblCode.Text = code;

        }

        /// <summary>
        /// Invokes on all label click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Click(object sender, EventArgs e)
        {
            //chkSelected.Checked = !chkSelected.Checked;
            var parent = this.Parent.Parent.Parent as Forms.LanguageCodeSelector;
            parent.SelectedLcids.Add(new DataObjects.LanguageCode() { LcidString = lblLcidString.Text, Name = lblName.Text, Value = int.Parse(lblCode.Text) });

            ((Form)this.TopLevelControl).Close();
        }
    }
}
