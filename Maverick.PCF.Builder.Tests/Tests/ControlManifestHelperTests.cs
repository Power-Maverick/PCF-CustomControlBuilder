using Maverick.PCF.Builder.Common;
using Maverick.PCF.Builder.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Maverick.PCF.Builder.Tests
{
    [TestClass]
    public class ControlManifestHelperTests
    {
        [TestMethod]
        public void FetchProperties_Test()
        {
            ControlManifestDetails details = new ControlManifestDetails();
            details.WorkingFolderPath = @"C:\PowerMeUpExamples\TestNewDesign";
            details.ControlName = "PCFTest";
            details.ControlDescription = "Testing";
            details.ManifestFilePath = $"{details.WorkingFolderPath}\\{details.ControlName}\\ControlManifest.Input.xml";
            details.FoundControlDetails = true;

#if DEBUG
            ControlManifestHelper helper = new ControlManifestHelper();
            helper.FetchProperties(details); 
#endif
        }
    }
}
