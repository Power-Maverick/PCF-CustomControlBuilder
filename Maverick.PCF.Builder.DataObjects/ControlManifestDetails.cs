using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maverick.PCF.Builder.DataObjects
{
    public class ControlManifestDetails
    {
        public ControlManifestDetails()
        {
            // Atleast one value will exists
            TypeGroups = new List<TypeGroup>();
            IsDatasetTemplate = false;
            ExistsCSS = false;
            ExistsResx = false;
            PreviewImagePath = null;
            Properties = new List<ControlProperty>();
        }

        public string WorkingFolderPath { get; set; }
        public string ManifestFilePath { get; set; }
        public bool FoundControlDetails { get; set; }
        public string Namespace { get; set; }
        public string ControlName { get; set; }
        public string ControlDisplayName { get; set; }
        public string ControlDescription { get; set; }
        public List<TypeGroup> TypeGroups { get; set; }
        public string PreviewImagePath { get; set; }
        public string Version { get; set; }
        public bool IsDatasetTemplate { get; set; }
        public bool ExistsCSS { get; set; }
        public bool ExistsResx { get; set; }
        public int AdditionalPackageIndex { get; set; }
        public List<ControlProperty> Properties { get; set; }
    }
}
