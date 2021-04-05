using Maverick.PCF.Builder.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maverick.PCF.Builder.Common;

namespace Maverick.PCF.Builder.Tests
{
    [TestClass]
    public class SolutionDetailsHelperTests
    {
        [TestMethod]
        public void UpdateSolutionPackageType_Managed_Test()
        {
            SolutionDetails solutionDetails = new SolutionDetails();
            solutionDetails.FoundSolutionDetails = true;
            solutionDetails.PackageType = SolutionPackageType.Managed;
            solutionDetails.ProjectFilePath = @"C:\PowerMeUpExamples\TestNewDesign\Solution\TestingSolution\TestingSolution.cdsproj";


            SolutionDetailsHelper helper = new SolutionDetailsHelper();
            helper.UpdateSolutionPackageType(solutionDetails);
        }

        [TestMethod]
        public void UpdateSolutionPackageType_Unmanaged_Test()
        {
            SolutionDetails solutionDetails = new SolutionDetails();
            solutionDetails.FoundSolutionDetails = true;
            solutionDetails.PackageType = SolutionPackageType.Unmanaged;
            solutionDetails.ProjectFilePath = @"C:\PowerMeUpExamples\TestNewDesign\Solution\TestingSolution\TestingSolution.cdsproj";


            SolutionDetailsHelper helper = new SolutionDetailsHelper();
            helper.UpdateSolutionPackageType(solutionDetails);
        }

        [TestMethod]
        public void UpdateSolutionPackageType_Both_Test()
        {
            SolutionDetails solutionDetails = new SolutionDetails();
            solutionDetails.FoundSolutionDetails = true;
            solutionDetails.PackageType = SolutionPackageType.Both;
            solutionDetails.ProjectFilePath = @"C:\PowerMeUpExamples\TestNewDesign\Solution\TestingSolution\TestingSolution.cdsproj";


            SolutionDetailsHelper helper = new SolutionDetailsHelper();
            helper.UpdateSolutionPackageType(solutionDetails);
        }
    }
}
