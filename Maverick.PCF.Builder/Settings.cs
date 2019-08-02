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
        public string VisualStudioCommandPromptPath { get; set; }
        public string WorkingDirectoryLocation { get; set; }
        public bool DoNotShowWelcomeScreen { get; set; }
    }
}