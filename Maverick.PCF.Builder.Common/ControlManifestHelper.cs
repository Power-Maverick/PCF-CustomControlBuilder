using Maverick.PCF.Builder.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Maverick.PCF.Builder.Common
{
    public class ControlManifestHelper
    {
        public ControlManifestDetails UpdateControlDetails(ControlManifestDetails controlDetails, string displayName = "", string description = "")
        {
            XmlDocument manifestFile = new XmlDocument();
            manifestFile.Load(controlDetails.ManifestFilePath);

            XmlNode controlNode = manifestFile.SelectSingleNode("/manifest/control");
            XmlAttribute attrDisplayNameKey = controlNode.Attributes["display-name-key"];
            XmlAttribute attrDescriptionKey = controlNode.Attributes["description-key"];

            // Update
            if (!string.IsNullOrEmpty(displayName))
            {
                attrDisplayNameKey.Value = displayName;
                controlDetails.ControlDisplayName = displayName;
            }

            if (!string.IsNullOrEmpty(description))
            {
                attrDescriptionKey.Value = description;
                controlDetails.ControlDescription = description;
            }

            manifestFile.Save(controlDetails.ManifestFilePath);

            return controlDetails;
        }
    }
}
