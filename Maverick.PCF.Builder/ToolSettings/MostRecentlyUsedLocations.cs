using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Maverick.PCF.Builder.Helper;

namespace Maverick.PCF.Builder.ToolSettings
{
    [XmlRoot(ElementName = "MostRecentlyUsedLocations", Namespace = "PCFBuilder")]
    [XmlInclude(typeof(MostRecentlyUsedLocation))]
    public class MostRecentlyUsedLocations
    {
        private const string SETTING_FOLDER = "Settings";
        private const string MRUL_FILENAME = "PCFBuilder.MRUL.xml";

        private static MostRecentlyUsedLocations instance;

        private MostRecentlyUsedLocations()
        {
        }

        public static MostRecentlyUsedLocations Instance
        {
            get
            {
                if (instance == null)
                {
                    if (!Load(out instance, out var message))
                    {
                        throw new Exception(message);
                    }
                }

                return instance;
            }
        }

        [XmlElement("MostRecentlyUsedItem")]
        public List<MostRecentlyUsedLocation> Items { get; set; } = new List<MostRecentlyUsedLocation>();

        public void Save()
        {
            var settingFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "\\" + SETTING_FOLDER;
            if (!Directory.Exists(settingFolder))
            {
                Directory.CreateDirectory(settingFolder);
            }

            // De-Dup and keep latest
            Items = Items
                    .OrderByDescending(item => item.Date)
                    .GroupBy(item => item.Location)
                    .Select(g => g.OrderByDescending(o => o.Date).First())
                    .Where(d => !string.IsNullOrEmpty(d.Location))
                    .Take(5)
                    .ToList();

            var settingsFile = Path.Combine(settingFolder, MRUL_FILENAME);

            XmlHelper.ToXmlFile(Instance, settingsFile);
        }

        private static bool Load(out MostRecentlyUsedLocations mrul, out string errorMessage)
        {
            errorMessage = string.Empty;
            var settingFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "\\" + SETTING_FOLDER;

            if (!Directory.Exists(settingFolder))
            {
                Directory.CreateDirectory(settingFolder);
            }

            var mrulFile = Path.Combine(settingFolder, MRUL_FILENAME);

            if (File.Exists(mrulFile))
            {
                try
                {
                    mrul = XmlHelper.FromXmlFile<MostRecentlyUsedLocations>(mrulFile);

                    return true;
                }
                catch (Exception error)
                {
                    errorMessage = error.Message;
                    mrul = new MostRecentlyUsedLocations();
                    return false;
                }
            }

            mrul = new MostRecentlyUsedLocations();
            return true;
        }
    }

    public class MostRecentlyUsedLocation
    {
        public string FolderName { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        
        public MostRecentlyUsedLocation()
        {

        }

        public MostRecentlyUsedLocation(string location)
        {
            FolderName = Path.GetFileName(location);
            Location = location;
            Date = DateTime.Now;
        }
    }
}
