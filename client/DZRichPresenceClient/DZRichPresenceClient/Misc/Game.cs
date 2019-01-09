using DZRichPresenceClient.Data;
using DZRichPresenceClient.RPC;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace DZRichPresenceClient.Misc
{
    public static class Game
    {
        public static bool IsRunning()
        {
            return Process.GetProcessesByName(Config.GameProcessName).Length > 0;
        }

        public static string GetDataDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Config.GameDataFolderName);
        }

        public static string GetPresenceFilePath()
        {
            return Path.Combine(GetDataDirectory(), Config.PresenceDataFile);
        }

        public static PresenceData GetPresenceData()
        {
            PresenceData presenceData = PresenceData.GetDefaultData();

            string jsonPath = GetPresenceFilePath();

            try
            {
                using (StreamReader reader = new StreamReader(jsonPath))
                {
                    string json = reader.ReadToEnd();

                    presenceData = JsonConvert.DeserializeObject<PresenceData>(json);
                }
            }
            catch (Exception exception)
            {
                if (exception is FileNotFoundException || exception is JsonReaderException)
                {
                    presenceData = PresenceData.GetDefaultData();

                    using (TextWriter writer = new StreamWriter(jsonPath, false))
                    {
                        writer.Write(JsonConvert.SerializeObject(presenceData));
                    }
                }
            }

            return presenceData;
        }
    }
}
