using Maverick.PCF.Builder.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Maverick.PCF.Builder.Common
{
    public class SolutionDetailsHelper
    {
        public void UpdateSolutionPackageType(SolutionDetails solutionDetails)
        {
            XmlDocument solutionXMLFile = new XmlDocument();
            solutionXMLFile.Load(solutionDetails.ProjectFilePath);

            var childNodes = solutionXMLFile["Project"].ChildNodes;
            bool nodeFound = false;

            foreach (XmlNode node in childNodes)
            {
                if (node.Name == "PropertyGroup")
                {
                    var pgChildnodes = node.ChildNodes;
                    foreach (XmlNode pgChild in pgChildnodes)
                    {
                        if (pgChild.Name == "SolutionPackageType")
                        {
                            pgChild.InnerText = solutionDetails.PackageType.ToString();
                            nodeFound = true;
                        }
                    }
                }
            }

            if (!nodeFound)
            {
                XmlNode parentNode = solutionXMLFile["Project"];

                XmlNode newPGNode = parentNode.AppendChild(solutionXMLFile.CreateNode(XmlNodeType.Element, "PropertyGroup", parentNode.NamespaceURI));

                XmlNode newSPTNode = solutionXMLFile.CreateNode(XmlNodeType.Element, "SolutionPackageType", parentNode.NamespaceURI);
                newSPTNode.InnerText = solutionDetails.PackageType.ToString();
                newPGNode.AppendChild(newSPTNode);

            }

            solutionXMLFile.Save(solutionDetails.ProjectFilePath);
        }
    }
}
