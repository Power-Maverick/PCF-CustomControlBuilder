using System;
using System.Collections.Generic;
using System.Text;

namespace Maverick.PCF.Builder.DataObjects
{
    public class PacVersionParsedDetails
    {
        public PacVersionParsedDetails()
        {
            ContainsLatestVersionNotification = false;
            CLINotFound = false;
            UnableToDetectCLIVersion = false;
        }

        public string CurrentVersion { get; set; }
        public bool ContainsLatestVersionNotification { get; set; }
        public bool CLINotFound { get; set; }
        public bool UnableToDetectCLIVersion { get; set; }

    }
}
