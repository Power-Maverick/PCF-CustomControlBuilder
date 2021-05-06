using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maverick.PCF.Builder.Helper
{
    public class Enum
    {
        private const string CHECK_MARK = "✔";
        private const string CROSS_MARK = "❌";

        public enum ResourceType
        {
            PreviewImage,
            CSS,
            RESX
        }

        public enum UsageType
        {
            bound,
            input
        }

        public enum FeatureType
        {
            CaptureAudio,
            CaptureVideo,
            CaptureImage,
            GetBarcode,
            GetCurrentPosition,
            PickFile,
            Utility,
            WebApi
        }

        public static Dictionary<string, string> AdditionalPackages()
        {
            Dictionary<string, string> packages = new Dictionary<string, string>();
            packages.Add("", "None");
            packages.Add("@fluentui/react", "Fluent UI");

            return packages;
        }

        public static string InitializationStatus(bool isInitialized)
        {
            return isInitialized ? $"{CHECK_MARK} Initialized" : $"{CROSS_MARK} Not Initialized";
        }

        public static string ResourceExists(bool exists, ResourceType type)
        {
            string initialMark;
            if (exists)
            {
                initialMark = CHECK_MARK;
            }
            else
            {
                initialMark = CROSS_MARK + " No";
            }

            switch (type)
            {
                case ResourceType.PreviewImage:
                    return $"{initialMark} Preview Image";
                case ResourceType.CSS:
                    return $"{initialMark} CSS file";
                case ResourceType.RESX:
                    return $"{initialMark} RESX file";
                default:
                    return $"{initialMark} Unknown Type";
            }
        }

        public static List<System.Enum> GetEnumForDropDown(Type enumType)
        {
           return System.Enum.GetValues(enumType)
                                .Cast<System.Enum>()
                                .ToList();
        }
    }
}
