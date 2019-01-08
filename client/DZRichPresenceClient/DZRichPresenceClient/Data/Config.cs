using System;
using System.Diagnostics;
using System.Reflection;

namespace DZRichPresenceClient.Data
{
    public static class Config
    {
        public static string ApplicationVersion { get => GetCurrentVersion(); }
        public static string ApplicationName = "DayZ Rich Presence";
        public static string ApplicationId = "527063225661915136";

        public static string RepositoryUrl = "https://github.com/Kreyu/dayz-rich-presence";
        public static string RepositoryId = "dayz-rich-presence";
        public static string RepositoryOwner = "Kreyu";
        public static string RepositoryReleasesUrl = "https://github.com/Kreyu/dayz-rich-presence/releases";

        public static string PresenceDataFile = "rich_presence.json";
        public static string ErrorLogFile = "rich_presence_client.log";

        public static string GameProcessName = "DayZ_x64";
        public static string GameDataFolderName = "DayZ";

        public static string ModWorkshopUrl = "https://steamcommunity.com/sharedfiles/filedetails/?id=1605419390";

        public static int TickDelay = 15000;

        private static string GetCurrentVersion()
        {
            return new Version(FileVersionInfo.GetVersionInfo(Assembly.GetCallingAssembly().Location).ProductVersion).ToString();
        }
    }
}
