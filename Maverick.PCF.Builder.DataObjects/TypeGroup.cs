using System;
using System.Collections.Generic;
using System.Text;

namespace Maverick.PCF.Builder.DataObjects
{
    public class TypeGroup
    {
        public TypeGroup()
        {
            Types = new List<string>();
        }
        public string Name { get; set; }
        public List<string> Types { get; set; }

    }
}
