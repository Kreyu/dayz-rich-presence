using DZRichPresenceClient.Data;
using DZRichPresenceClient.Exceptions;
using DZRichPresenceClient.RPC;
using System;
using System.Diagnostics;
using System.IO;
using TinyJson;

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

                    presenceData = json.FromJson<PresenceData>();

                    if (presenceData == null || presenceData.status == null)
                    {
                        throw new JsonReaderException();
                    }
                }
            }
            catch (Exception exception)
            {
                if (exception is FileNotFoundException || exception is JsonReaderException)
                {
                    presenceData = PresenceData.GetDefaultData();

                    using (TextWriter writer = new StreamWriter(jsonPath, false))
                    {
                        writer.Write(presenceData.ToJson());
                    }
                }
            }

            return presenceData;
        }
    }
}
