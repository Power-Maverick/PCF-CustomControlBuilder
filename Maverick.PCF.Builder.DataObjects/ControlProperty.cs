using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static Maverick.PCF.Builder.Helper.Enum;

namespace Maverick.PCF.Builder.DataObjects
{
    public class ControlProperty
    {
        public ControlProperty()
        {
            IsUsingTypeGroup = false;
        }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Display Name Key")]
        public string DisplayNameKey { get; set; }

        [DisplayName("Description Name Key")]
        public string DescriptionNameKey { get; set; }

        [DisplayName("Type or Type Group")]
        public string TypeOrTypeGroup { get; set; }

        [Browsable(false)]
        public bool IsUsingTypeGroup { get; set; }

        [DisplayName("Usage")]
        public UsageType Usage { get; set; }

        [DisplayName("Is Required")]
        public bool IsRequired { get; set; }
        
        [Browsable(false)]
        public bool IsValid { get; set; }
    }
}
