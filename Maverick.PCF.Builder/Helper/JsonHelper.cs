using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Maverick.PCF.Builder.Helper
{
    public static class JsonHelper
    {
        public static List<T> FromJson<T>(string json) => JsonConvert.DeserializeObject<List<T>>(json, Converter.SerializerSettings);

        public static string ToJson<T>(this List<T> self) => JsonConvert.SerializeObject(self, Converter.SerializerSettings);

        internal static class Converter
        {
            public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = System.Globalization.DateTimeStyles.AssumeUniversal }
            },
            };
        }
    }
}
