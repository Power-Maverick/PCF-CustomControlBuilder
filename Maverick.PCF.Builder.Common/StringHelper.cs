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

        public string ParseOrgDetails(string output)
        {
            string parsedOrgDetails = string.Empty;
            var extractedDetails = ExtractOrgDetails(output);

            parsedOrgDetails += "\n";

            switch (extractedDetails.ParseStatus)
            {
                case OrganizationInfo.Status.Found:
                    parsedOrgDetails += "Org URL:           " + extractedDetails.OrgUrl + "\n";
                    parsedOrgDetails += "User Id:            " + extractedDetails.UserId + "\n";
                    parsedOrgDetails += "Friendly Name:  " + extractedDetails.FriendlyName + "\n";
                    parsedOrgDetails += "Unique Name:   " + extractedDetails.UniqueName + "\n";
                    parsedOrgDetails += "Org Id:               " + extractedDetails.OrgId + "\n";
                    break;
                
                case OrganizationInfo.Status.Error:
                    parsedOrgDetails += "Unable to login into current profile. Please delete and re-authenticate.";
                    break;
                case OrganizationInfo.Status.NotFound:
                default:
                    parsedOrgDetails += "No profiles were found on this machine.";
                    break;
            }

            return parsedOrgDetails;
        }

        public OrganizationInfo ExtractOrgDetails(string output)
        {
            OrganizationInfo info = new OrganizationInfo();

            try
            {
                if (output.ToLower().Contains("organization information"))
                {
                    // Split on \r\n
                    char[] mainDelimiterChars = { '\r', '\n' };
                    string[] mainSplit = output.Split(mainDelimiterChars);

                    string url = string.Empty;
                    string username = string.Empty;

                    foreach (string list in mainSplit)
                    {
                        if (list.Contains("Friendly Name:"))
                        {
                            string[] innerSplit = list.Trim().Split(':');

                            if (innerSplit.Length > 0)
                            {
                                info.FriendlyName = innerSplit[1].Trim();
                            }
                        }
                        if (list.Contains("Org URL:"))
                        {
                            string[] innerSplit = list.Trim().Split(' ');

                            if (innerSplit.Length > 0)
                            {
                                info.OrgUrl = innerSplit[innerSplit.Length - 1].Trim();
                            }
                        }
                        if (list.Contains("User ID:"))
                        {
                            string[] innerSplit = list.Trim().Split(':');

                            if (innerSplit.Length > 0)
                            {
                                string[] userSplit = innerSplit[1].Trim().Split(' ');

                                if (userSplit.Length > 0)
                                {
                                    info.UserId = userSplit[0].Trim();
                                }
                            }
                        }
                        if (list.Contains("Org ID:"))
                        {
                            string[] innerSplit = list.Trim().Split(':');

                            if (innerSplit.Length > 0)
                            {
                                info.OrgId = innerSplit[1].Trim();
                            }
                        }
                        if (list.Contains("Unique Name:"))
                        {
                            string[] innerSplit = list.Trim().Split(':');

                            if (innerSplit.Length > 0)
                            {
                                info.UniqueName = innerSplit[1].Trim();
                            }
                        }
                    }
                    info.ParseStatus = OrganizationInfo.Status.Found;
                }
                else if (output.ToLower().Contains("no profiles were found on this computer"))
                {
                    info.ParseStatus = OrganizationInfo.Status.NotFound;
                }
                else if (output.ToLower().Contains("error: unable to login"))
                {
                    info.ParseStatus = OrganizationInfo.Status.Error;
                }
            }
            catch (Exception ex)
            {
                info.ParseStatus = OrganizationInfo.Status.Error;
            }

            return info;
        }
    }
}
