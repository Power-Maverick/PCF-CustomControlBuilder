using Maverick.PCF.Builder.DataObjects;
using Maverick.PCF.Builder.Helper;
using Maverick.PCF.Builder.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maverick.PCF.Builder.Forms
{
    public partial class LanguageCodeSelector : Form
    {
        #region Properties

        public List<LanguageCode> SelectedLcids { get; set; }
        public PCFBuilder ParentControl { get; set; }

        #endregion

        #region Cache Variables

        private List<LanguageCode> _lstLangCodes;

        #endregion

        #region Private Variables

        private Thread _searchThread;
        private BackgroundWorker _worker;

        #endregion

        #region Custom Methods

        private List<LanguageCode> ProcessLanguageCodesFromJson()
        {
            List<LanguageCode> languageCodes = new List<LanguageCode>();
            var fullLangCodeFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "\\DataFiles\\lcid.json";

            using (StreamReader stream = File.OpenText(fullLangCodeFilePath))
            {
                languageCodes = JsonHelper.FromJson<LanguageCode>(stream.ReadToEnd());
            }

            return languageCodes;
        }

        private void LoadLangCodes(object filter = null)
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += PerformLoadLangCodes;
            _worker.RunWorkerAsync(filter);
        }

        private void PerformLoadLangCodes(object worker, DoWorkEventArgs args)
        {
            object filter = args.Argument;

            Invoke(new Action(() =>
            {
                pnlLangCodes.Controls.Clear();

                var filteredCodes = (filter != null && filter.ToString().Length > 0
                ? _lstLangCodes.Where(t => t.Name.ToLower().Contains(filter.ToString().ToLower()))
                : _lstLangCodes).OrderBy(t => t.Name).ToList();

                foreach (var lcid in filteredCodes)
                {
                    LcidControl control = new LcidControl(
                        lcid.Name,
                        lcid.LcidString,
                        lcid.Value.ToString()
                    );
                    //control.Width = this.pnlLangCodes.Width - 20;

                    pnlLangCodes.Controls.Add(control);
                }

            }));
        }

        #endregion

        public LanguageCodeSelector()
        {
            InitializeComponent();
        }

        private void LanguageCodeSelector_Load(object sender, EventArgs e)
        {
            // Fetch data
            _lstLangCodes = ProcessLanguageCodesFromJson();
            LoadLangCodes();

            SelectedLcids = new List<LanguageCode>();
            txtSearch.AutoCompleteCustomSource.AddRange(_lstLangCodes.Select(t => t.Name).ToArray());
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //_searchThread?.Abort();
            //_searchThread = new Thread(LoadLangCodes);
            //_searchThread.Start(txtSearch.Text);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadLangCodes(txtSearch.Text);
        }

        private void btnAddLangCodes_Click(object sender, EventArgs e)
        {
            var parent = this.ParentControl as PCFBuilder;
            parent.SelectedLcids = SelectedLcids;
            this.Close();
        }

        private void LanguageCodeSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.SelectedLcids != null)
            {
                var parent = this.ParentControl as PCFBuilder;
                parent.SelectedLcids = SelectedLcids;
            }
        }
    }
}
