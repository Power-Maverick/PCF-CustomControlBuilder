using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maverick.PCF.Builder.DataObjects
{
    public class AuthenticationProfile
    {
        public int Index { get; set; }
        public bool IsCurrent { get; set; }
        public string EnvironmentType { get; set; }
        public string EnvironmentUrl { get; set; }
        public string UserName { get; set; }

    }
}
