using System;
using System.Collections.Generic;
using System.Text;

namespace Maverick.PCF.Builder.DataObjects
{
    public enum SolutionPackageType
    {
        Unmanaged,
        Managed,
        Both
    }

    public enum Release
    {
        Dev,
        Prod
    }

    public class SolutionDetails
    {
        public SolutionDetails()
        {
            Publisher = new PublisherDetails();
            PackageType = SolutionPackageType.Unmanaged;
            ReleaseType = Release.Dev;
        }

        public string UniqueName { get; set; }
        public string FriendlyName { get; set; }
        public string Version { get; set; }
        public PublisherDetails Publisher { get; set; }
        public SolutionPackageType PackageType { get; set; }
        public Release ReleaseType { get; set; }
    }
}
