using System;
using System.Collections.Generic;
using System.Text;

namespace Maverick.PCF.Builder.DataObjects
{
    public class OrganizationInfo
    {
        public enum Status
        {
            Found,
            NotFound,
            Error
        }

        public OrganizationInfo()
        {
            ParseStatus = Status.NotFound;
        }

        public Status ParseStatus { get; set; }
        public string OrgId { get; set; }
        public string UniqueName { get; set; }
        public string FriendlyName { get; set; }
        public string OrgUrl { get; set; }
        public string UserId { get; set; }

    }
}
