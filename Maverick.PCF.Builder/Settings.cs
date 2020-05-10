using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maverick.PCF.Builder
{
    /// <summary>
    /// This class can help you to store settings for your plugin
    /// </summary>
    /// <remarks>
    /// This class must be XML serializable
    /// </remarks>
    public class Settings
    {
        public Settings()
        {
            AlwaysLoadNamespaceFromSettings = true;
            AlwaysLoadPublisherDetailsFromSettings = true;
        }

        public string WorkingDirectoryLocation { get; set; }
        public string MsBuildLocation { get; set; }
        public string ControlNamespace { get; set; }
        public bool AlwaysLoadNamespaceFromSettings { get; set; }
        public string PublisherName { get; set; }
        public string PublisherPrefix { get; set; }
        public bool AlwaysLoadPublisherDetailsFromSettings { get; set; }
    }
}