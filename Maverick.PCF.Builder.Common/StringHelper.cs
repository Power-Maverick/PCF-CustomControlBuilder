using System;
using System.Collections.Generic;
using System.Text;
using Maverick.PCF.Builder.DataObjects;

namespace Maverick.PCF.Builder.Common
{
    public class StringHelper
    {
        public PacVersionParsedDetails ParsePacVersionOutput(string output)
        {
            PacVersionParsedDetails details = new PacVersionParsedDetails();

            if (!string.IsNullOrEmpty(output) && output.ToLower().Contains("microsoft powerapps cli"))
            {
                if (output.IndexOf("Version: ") > 0)
                {
                    details.CurrentVersion = output.Substring(output.IndexOf("Version: ") + 8, output.IndexOf("+", output.IndexOf("Version: ") + 8) - (output.IndexOf("Version: ") + 8)).Trim();

                    //NOTE: A newer version of Microsoft.PowerApps.CLI has been found. Please run 'pac install latest' to install the latest version.
                    if (output.ToLower().Contains("a newer version of microsoft.powerapps.cli has been found"))
                    {
                        details.ContainsLatestVersionNotification = true;
                    }
                }
                else
                {
                    details.UnableToDetectCLIVersion = true;
                }
                    
            }
            else
            {
                details.CLINotFound = true;
            }

            return details;
        }
    }
}
