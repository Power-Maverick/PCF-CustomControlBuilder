using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Maverick.PCF.Builder.DataObjects;
using Maverick.PCF.Builder.Helper;
using Maverick.PCF.Builder.UserControls;

namespace Maverick.PCF.Builder.Forms
{
    public partial class Templates : Form
    {
        #region Properties

        public PcfGallery SelectedTemplate { get; set; }
        public MainPluginControl ParentControl { get; set; }

        #endregion

        #region Cache Variables

        private List<PcfGallery> _lstPcfGallery;

        #endregion

        #region Private Variables

        private Thread _searchThread;
        private string _workingDir;
        private BackgroundWorker _worker;

        #endregion

        #region Custom Methods

        private Image DownloadImageFromUrl(string imageUrl)
        {
            Image image = null;

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                WebResponse webResponse = webRequest.GetResponse();
                System.IO.Stream stream = webResponse.GetResponseStream();
                image = Image.FromStream(stream);
                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return image;
        }

        private List<PcfGallery> ProcessTemplates()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(Properties.Settings.Default["PcfGalleryTemplateUri"].ToString());
                List<PcfGallery> lstPcfGallery = JsonHelper.FromJson<PcfGallery>(json);

                // Parse Image data from URL
                foreach (var galleryItem in lstPcfGallery)
                {
                    galleryItem.ParsedImage = DownloadImageFromUrl(galleryItem.image);
                }

                //TODO - verify if it is a good project or not

                return lstPcfGallery;
            }
        }

        private void LoadTemplates(object filter = null)
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += PerformLoadTemplates;
            _worker.RunWorkerAsync(filter);
        }

        private void PerformLoadTemplates(object worker, DoWorkEventArgs args)
        {
            object filter = args.Argument;

            Invoke(new Action(() =>
            {
                pnlTemplates.Controls.Clear();

                var filteredTemplates = (filter != null && filter.ToString().Length > 0
                ? _lstPcfGallery.Where(t => t.title.ToLower().Contains(filter.ToString().ToLower()) || t.author.ToLower().Contains(filter.ToString().ToLower()))
                : _lstPcfGallery).OrderBy(t => t.title).ToList();

                lblCount.Text = $"Count: {filteredTemplates.Count}";

                foreach (var pcfTemplate in filteredTemplates)
                {
                    Template template = new Template(
                        pcfTemplate.ParsedImage,
                        pcfTemplate.title,
                        pcfTemplate.author,
                        pcfTemplate.description,
                        pcfTemplate.link,
                        pcfTemplate.download,
                        _workingDir,
                        pcfTemplate.model_support,
                        pcfTemplate.canvas_support,
                        pcfTemplate.license_defined
                    );
                    template.Width = this.pnlTemplates.Width - 20;

                    pnlTemplates.Controls.Add(template);
                }

            }));
        }

        #endregion

        public Templates(string _workingDir)
        {
            InitializeComponent();
            
            this._workingDir = _workingDir;
        }

        private void Templates_Load(object sender, EventArgs e)
        {
            // Fetch data
            _lstPcfGallery = ProcessTemplates();
            LoadTemplates();

            txtSearch.AutoCompleteCustomSource.AddRange(_lstPcfGallery.Select(t => t.title).ToArray());
        }

        private void Templates_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.SelectedTemplate != null)
            {
                var parent = this.ParentControl as MainPluginControl;
                parent.SelectedTemplate = this.SelectedTemplate;
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            _searchThread?.Abort();
            _searchThread = new Thread(LoadTemplates);
            _searchThread.Start(txtSearch.Text);
        }


    }
}
