using System;
using System.IO;
using System.Xml.Serialization;

namespace ClockWidget
{
    public class Settings
    {
        public string SelectedThemeName { get; set; }
        public string SelectedAccentName { get; set; }
        public string FinishTime { get; set; }
        public int FontSize { get; set; }
        public string FontFamily { get; set; }
        public string Note { get; set; }
    }

    public static class SettingsLogic
    {
        private static readonly string SettingsPath = $"{AppDomain.CurrentDomain.BaseDirectory}settings.xml";

        public static void SaveSettings(Settings settings)
        {
            if (File.Exists(SettingsPath))
                File.Delete(SettingsPath);

            using (FileStream stream = new FileStream(SettingsPath, FileMode.OpenOrCreate))
            {
                var serializer = new XmlSerializer(typeof(Settings));
                serializer.Serialize(stream, settings);
            }
        }

        public static Settings GetSettings()
        {
            if (!File.Exists(SettingsPath))
                return SettingsLogic.GetDefaultSettings();

            using (FileStream stream = new FileStream(SettingsPath, FileMode.OpenOrCreate))
            {
                var serializer = new XmlSerializer(typeof(Settings));
                return serializer.Deserialize(stream) as Settings;
            }
        }

        private static Settings GetDefaultSettings() => new Settings()
        {
            SelectedAccentName = "Cyan",
            SelectedThemeName = "BaseLight",
            FontSize = 36,
            FinishTime = "16:40:00",
            FontFamily = "Arial",
            Note = string.Empty

        };
    }
}