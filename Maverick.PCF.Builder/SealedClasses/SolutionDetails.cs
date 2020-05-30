using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Maverick.PCF.Builder.SealedClasses
{
    public sealed class SolutionDetails
    {
        public string DisplayText { get; set; }
        public Entity MetaData { get; set; }
    }
}
