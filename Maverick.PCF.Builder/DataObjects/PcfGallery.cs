using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maverick.PCF.Builder.DataObjects
{
    public class PcfGallery
    {
        public string title { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string image { get; set; }
        public string download { get; set; }
        public string author { get; set; }

        public Image ParsedImage { get; set; }
        public string ParsedControlName { get; set; }
    }

}
