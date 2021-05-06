using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static Maverick.PCF.Builder.Helper.Enum;

namespace Maverick.PCF.Builder.DataObjects
{
    public class Feature
    {
        public Feature()
        {
            Required = false;
        }

        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Required")]
        public bool Required { get; set; }
        public FeatureType Type { get; set; }

        [Browsable(false)]
        public bool IsValid { get; set; }

        public string IdentifyName(FeatureType type)
        {
            switch (type)
            {
                case FeatureType.CaptureAudio:
                    return "Device.captureAudio";
                case FeatureType.CaptureVideo:
                    return "Device.captureVideo";
                case FeatureType.CaptureImage:
                    return "Device.captureImage";
                case FeatureType.GetBarcode:
                    return "Device.getBarcodeValue";
                case FeatureType.GetCurrentPosition:
                    return "Device.getCurrentPosition";
                case FeatureType.PickFile:
                    return "Device.pickFile";
                case FeatureType.Utility:
                    return "Utility";
                case FeatureType.WebApi:
                    return "WebAPI";
                default:
                    return "";
            }
        }

        public FeatureType IdentifyType(string name)
        {
            switch (name)
            {
                case "Device.captureAudio":
                    Type = FeatureType.CaptureAudio;
                    break;
                case "Device.captureImage":
                    Type = FeatureType.CaptureImage;
                    break;
                case "Device.captureVideo":
                    Type = FeatureType.CaptureVideo;
                    break;
                case "Device.getBarcodeValue":
                    Type = FeatureType.GetBarcode;
                    break;
                case "Device.getCurrentPosition":
                    Type = FeatureType.GetCurrentPosition;
                    break;
                case "Device.pickFile":
                    Type = FeatureType.PickFile;
                    break;
                case "Utility":
                    Type = FeatureType.Utility;
                    break;
                case "WebAPI":
                    Type = FeatureType.WebApi;
                    break;
            }

            return Type;
        }
    }
}
