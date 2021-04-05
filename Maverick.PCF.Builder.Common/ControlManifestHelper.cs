using Maverick.PCF.Builder.DataObjects;
using Maverick.PCF.Builder.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Enum = Maverick.PCF.Builder.Helper.Enum;

namespace Maverick.PCF.Builder.Common
{
    public class ControlManifestHelper
    {
        public ControlManifestDetails GetControlManifestDetails(string controlFolder)
        {
            ControlManifestDetails ControlDetails = new ControlManifestDetails();

            ControlDetails.FoundControlDetails = false;

            var start = DateTime.Now;
            var mainDirs = Directory.GetDirectories(controlFolder);
            ControlDetails.WorkingFolderPath = controlFolder;

            if (mainDirs != null && mainDirs.Count() > 0)
            {
                // Check if .pcfproj does not already exists
                var filteredPcfProject = mainDirs.ToList().Where(l => (!l.ToLower().EndsWith(".pcfproj")));
                if (filteredPcfProject != null && filteredPcfProject.Count() > 0)
                {
                    var pcfDirs = Directory.GetDirectories(ControlDetails.WorkingFolderPath);
                    if (pcfDirs != null && pcfDirs.Count() > 0)
                    {
                        var filteredPcfDirs = pcfDirs.ToList().Where(l => (!l.ToLower().EndsWith("node_modules")) && (!l.ToLower().EndsWith("obj")) && (!l.ToLower().EndsWith("out")));
                        foreach (var currentDir in filteredPcfDirs)
                        {
                            var indexExists = CodeFileExists(currentDir);
                            if (!string.IsNullOrEmpty(indexExists))
                            {
                                ControlDetails.FoundControlDetails = true;

                                ControlDetails.ControlName = Path.GetFileName(currentDir);
                                var controlManifestFile = currentDir + "\\" + "ControlManifest.Input.xml";
                                ControlDetails.ManifestFilePath = controlManifestFile;
                                XmlReader xmlReader = XmlReader.Create(ControlDetails.ManifestFilePath);

                                while (xmlReader.Read())
                                {
                                    if (xmlReader.NodeType == XmlNodeType.Element)
                                    {
                                        switch (xmlReader.Name)
                                        {
                                            case "control":
                                                ControlDetails.Namespace = xmlReader.GetAttribute("namespace");
                                                ControlDetails.Version = xmlReader.GetAttribute("version");
                                                ControlDetails.ControlDisplayName = xmlReader.GetAttribute("display-name-key");
                                                ControlDetails.ControlDescription = xmlReader.GetAttribute("description-key");

                                                if (xmlReader.GetAttribute("preview-image") != null)
                                                {
                                                    var sanitizedPreviewImageRelativePath = xmlReader.GetAttribute("preview-image").Replace("/", "\\");
                                                    ControlDetails.PreviewImagePath = $"{currentDir}\\{sanitizedPreviewImageRelativePath}";
                                                }

                                                break;
                                            case "data-set":
                                                ControlDetails.IsDatasetTemplate = true;
                                                break;
                                            case "css":
                                                ControlDetails.ExistsCSS = true;
                                                break;
                                            case "resx":
                                                ControlDetails.ExistsResx = true;
                                                break;
                                            default:
                                                break;
                                        }
                                    }

                                }
                                xmlReader.Close();
                                ControlManifestHelper manifestHelper = new ControlManifestHelper();
                                ControlDetails = manifestHelper.FetchProperties(ControlDetails);
                                ControlDetails = manifestHelper.FetchTypeGroups(ControlDetails);
                                break;
                            }

                        }

                    }
                }
            }
            else
            {
                //MessageBox.Show("Could not retrieve existing PCF project and CDS solution project details.");
                ControlDetails.FoundControlDetails = false;
            }

            return ControlDetails;
        }

        public string CodeFileExists(string directoryPath)
        {
            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            FileInfo[] files = dir.GetFiles("*.ts");

            return files.Length > 0 ? files[0].Name : null;
        }

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

        public ControlManifestDetails FetchProperties(ControlManifestDetails controlDetails)
        {
            XmlDocument manifestFile = new XmlDocument();
            manifestFile.Load(controlDetails.ManifestFilePath);

            XmlNodeList propertyNodes = manifestFile.SelectNodes("/manifest/control/property");
            foreach (XmlNode node in propertyNodes)
            {
                ControlProperty property = new ControlProperty();

                try
                {
                    property.Name = (node.Attributes["name"]?.Value) ?? string.Empty;
                    property.DisplayNameKey = (node.Attributes["display-name-key"]?.Value) ?? string.Empty;
                    property.DescriptionNameKey = (node.Attributes["description-key"]?.Value) ?? string.Empty;
                    property.TypeOrTypeGroup = (node.Attributes["of-type"]?.Value) ?? string.Empty;
                    property.IsRequired = bool.Parse(node.Attributes["required"]?.Value ?? "false");
                    property.Usage = (node.Attributes["usage"]?.Value ?? "bound") == "bound" ? Enum.UsageType.bound : Enum.UsageType.input;

                    // Check of of-type-group
                    if (string.IsNullOrEmpty(property.TypeOrTypeGroup))
                    {
                        property.TypeOrTypeGroup = (node.Attributes["of-type-group"]?.Value) ?? string.Empty;
                        property.IsUsingTypeGroup = true;
                    }

                    property.IsValid = true;
                }
                catch (Exception ex)
                {
                    property.IsValid = false;
                }

                controlDetails.Properties.Add(property);
            }

            return controlDetails;
        }

        public ControlManifestDetails FetchTypeGroups(ControlManifestDetails controlDetails)
        {
            XmlDocument manifestFile = new XmlDocument();
            manifestFile.Load(controlDetails.ManifestFilePath);

            XmlNodeList tgNodes = manifestFile.SelectNodes("/manifest/control/type-group");
            foreach (XmlNode node in tgNodes)
            {
                TypeGroup typegroup = new TypeGroup();

                try
                {
                    typegroup.Name = (node.Attributes["name"]?.Value) ?? string.Empty;

                    XmlNodeList typeNodes = manifestFile.SelectNodes($"manifest/control/type-group[@name='{typegroup.Name}']/type");
                    foreach (XmlNode type in typeNodes)
                    {
                        typegroup.Types.Add(type.InnerText);
                    }

                    controlDetails.TypeGroups.Add(typegroup);
                }
                catch (Exception ex)
                {
                }
            }

            return controlDetails;
        }

        public ControlManifestDetails UpdatePropertyDetails(string propertyName, ControlManifestDetails controlDetails, ControlProperty property)
        {
            XmlDocument manifestFile = new XmlDocument();
            manifestFile.Load(controlDetails.ManifestFilePath);

            XmlNode propertyNode = manifestFile.SelectSingleNode($"/manifest/control/property[@name='{propertyName}']");
            XmlAttribute attrName = propertyNode.Attributes["name"];
            XmlAttribute attrDisplayNameKey = propertyNode.Attributes["display-name-key"];
            XmlAttribute attrDescriptionKey = propertyNode.Attributes["description-key"];
            XmlAttribute attrUsage = propertyNode.Attributes["usage"];
            XmlAttribute attrRequired = propertyNode.Attributes["required"];
            XmlAttribute attrOfType = propertyNode.Attributes["of-type"];
            XmlAttribute attrOfTypeGroup = propertyNode.Attributes["of-type-group"];

            // Update
            if (!string.IsNullOrEmpty(property.Name) && attrName != null)
            {
                attrName.Value = property.Name;
            }
            if (!string.IsNullOrEmpty(property.DisplayNameKey) && attrDisplayNameKey != null)
            {
                attrDisplayNameKey.Value = property.DisplayNameKey;
            }
            if (!string.IsNullOrEmpty(property.DescriptionNameKey) && attrDescriptionKey != null)
            {
                attrDescriptionKey.Value = property.DescriptionNameKey;
            }
            if (attrUsage != null)
            {
                attrUsage.Value = property.Usage.ToString();
            }
            if (attrRequired != null)
            {
                attrRequired.Value = property.IsRequired ? "true" : "false";
            }
            if (attrOfType != null)
            {
                if (property.IsUsingTypeGroup)
                {
                    // Change of-type ot of-type-group
                    XmlAttribute newattr = manifestFile.CreateNewAttribute("of-type-group", property.TypeOrTypeGroup);
                    propertyNode.Attributes.Append(newattr);
                    propertyNode.Attributes.Remove(attrOfType);
                }
                else
                {
                    attrOfType.Value = property.TypeOrTypeGroup;
                }
            }
            if (attrOfTypeGroup != null)
            {
                if (property.IsUsingTypeGroup)
                {
                    attrOfTypeGroup.Value = property.TypeOrTypeGroup;
                }
                else
                {
                    // Change of-type ot of-type-group
                    XmlAttribute newattr = manifestFile.CreateNewAttribute("of-type", property.TypeOrTypeGroup);
                    propertyNode.Attributes.Append(newattr);
                    propertyNode.Attributes.Remove(attrOfTypeGroup);
                }
            }

            // Update Control Manifest Details
            var updateProp = controlDetails.Properties.FirstOrDefault(p => p.Name == propertyName);
            updateProp = property;

            manifestFile.Save(controlDetails.ManifestFilePath);

            return controlDetails;
        }

        public void UpdateTypeGroupName(string currentTypeGroupName, string newTypeGroupName, ControlManifestDetails controlDetails)
        {
            XmlDocument manifestFile = new XmlDocument();
            manifestFile.Load(controlDetails.ManifestFilePath);

            XmlNode typeGroupNode = manifestFile.SelectSingleNode($"manifest/control/type-group[@name='{currentTypeGroupName}']");
            XmlAttribute attrName = typeGroupNode.Attributes["name"];

            // Update
            if (!string.IsNullOrEmpty(newTypeGroupName) && attrName != null)
            {
                attrName.Value = newTypeGroupName;
            }

            // Update References
            XmlNodeList propertyNodes = manifestFile.SelectNodes($"/manifest/control/property[@of-type-group='{currentTypeGroupName}']");
            foreach (XmlNode node in propertyNodes)
            {
                XmlAttribute attrOfTypeGroup = node.Attributes["of-type-group"];
                attrOfTypeGroup.Value = newTypeGroupName;
            }

            manifestFile.Save(controlDetails.ManifestFilePath);

        }

        public void UpdateTypeInTypeGroup(string currentTypeGroupName, string currentType, string newType, ControlManifestDetails controlDetails)
        {
            XmlDocument manifestFile = new XmlDocument();
            manifestFile.Load(controlDetails.ManifestFilePath);

            XmlNodeList typeNodes = manifestFile.SelectNodes($"manifest/control/type-group[@name='{currentTypeGroupName}']/type");
            foreach (XmlNode type in typeNodes)
            {
                if (type.InnerText == currentType)
                {
                    type.InnerText = newType;
                }
            }

            manifestFile.Save(controlDetails.ManifestFilePath);
        }

        public void CreateNewProperty(ControlManifestDetails controlDetails, ControlProperty property)
        {
            XmlDocument manifestFile = new XmlDocument();
            manifestFile.Load(controlDetails.ManifestFilePath);

            XmlNode controlNode = manifestFile.SelectSingleNode($"/manifest/control");

            XmlElement propertyNode = manifestFile.CreateElement("property");
            if (!string.IsNullOrEmpty(property.Name))
            {
                propertyNode.SetAttribute("name", property.Name);
            }
            if (!string.IsNullOrEmpty(property.DisplayNameKey))
            {
                propertyNode.SetAttribute("display-name-key", property.DisplayNameKey);
            }
            if (!string.IsNullOrEmpty(property.DescriptionNameKey))
            {
                propertyNode.SetAttribute("description-key", property.DescriptionNameKey);
            }
            propertyNode.SetAttribute("usage", property.Usage.ToString());
            propertyNode.SetAttribute("required", property.IsRequired ? "true" : "false");
            if (property.IsUsingTypeGroup)
            {
                propertyNode.SetAttribute("of-type-group", property.TypeOrTypeGroup);
            }
            else
            {
                propertyNode.SetAttribute("of-type", property.TypeOrTypeGroup);
            }
            controlNode.AppendChild(propertyNode);

            manifestFile.Save(controlDetails.ManifestFilePath);
        }

        public void CreateNewTypeGroup(ControlManifestDetails controlDetails, string typeGroupName, string initialType)
        {
            XmlDocument manifestFile = new XmlDocument();
            manifestFile.Load(controlDetails.ManifestFilePath);

            XmlNode controlNode = manifestFile.SelectSingleNode($"/manifest/control");

            XmlElement tgNode = manifestFile.CreateElement("type-group");
            tgNode.SetAttribute("name", typeGroupName);

            XmlElement typeNode = manifestFile.CreateElement("type");
            typeNode.InnerText = initialType;
            tgNode.AppendChild(typeNode);

            controlNode.AppendChild(tgNode);
            manifestFile.Save(controlDetails.ManifestFilePath);
        }

        public void AddNewTypeInTypeGroup(ControlManifestDetails controlDetails, string typeGroupName, string typeName)
        {
            XmlDocument manifestFile = new XmlDocument();
            manifestFile.Load(controlDetails.ManifestFilePath);

            XmlNode tgNode = manifestFile.SelectSingleNode($"manifest/control/type-group[@name='{typeGroupName}']");

            XmlElement typeNode = manifestFile.CreateElement("type");
            typeNode.InnerText = typeName;
            tgNode.AppendChild(typeNode);

            manifestFile.Save(controlDetails.ManifestFilePath);
        }
    }
}
