using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maverick.PCF.Builder.DataObjects
{
    public class SupportedDataTypes
    {
        [JsonProperty("DataTypeName")]
        public string DataTypeName { get; set; }
       
    }
}
