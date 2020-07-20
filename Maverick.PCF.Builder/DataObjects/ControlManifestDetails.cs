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
            SupportedTypes = new List<string>();
        }

        public string WorkingFolderPath { get; set; }
        public string ControlName { get; set; }
        public string ControlDisplayName { get; set; }
        public string ControlDescription { get; set; }
        public List<string> SupportedTypes { get; set; }
        public string PreviewImagePath { get; set; }
    }
}
