using System;
using System.Threading.Tasks;
using DiscordRpcNet;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.IO;

namespace DZRichPresenceClient
{
    class Presence
    {
        private static bool Continue = true;

        public static void Start()
        {
            Random random = new Random();

            StringCollection largeImageTexts = Properties.Settings.Default.LargeImageTexts;

            var Callbacks = new DiscordRpc.EventHandlers
            {
                readyCallback = ReadyCallback,
                disconnectedCallback = DisconnectedCallback,
                errorCallback = ErrorCallback
            };

            Task.Run((Action) delegate
            {
                DiscordRpc.Initialize(Properties.Settings.Default.ApplicationId, ref Callbacks, true, null);

                var Status = new DiscordRpc.RichPresence
                {
                    largeImageKey = "dz-logo",
                    startTimestamp = (int) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds,
                };

                while (Continue)
                {
                    PresenceData presenceData = GetPresenceData();

                    if (presenceData.IsValid())
                    {
                        Status.details = presenceData.status;
                        Status.largeImageText = largeImageTexts[random.Next(largeImageTexts.Count)];

                        DiscordRpc.UpdatePresence(ref Status);
                    }

                    DiscordRpc.RunCallbacks();
                    System.Threading.Thread.Sleep(15000);
                }
            });
        }

        public static void Stop()
        {
            DiscordRpc.Shutdown();
        }

        private static PresenceData GetPresenceData()
        {
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string jsonPath = Path.Combine(localAppDataPath, "DayZ", "rich_presence.json");

            using (StreamReader r = new StreamReader(jsonPath))
            {
                string json = r.ReadToEnd();
                PresenceData items = JsonConvert.DeserializeObject<PresenceData>(json);

                return items;
            }
        }

        private static void ReadyCallback()
        {
            Console.WriteLine("Discord::Ready()");
        }

        private static void DisconnectedCallback(int errorCode, string message)
        {
            Console.WriteLine("Discord::Disconnect({0}, {1})", errorCode, message);
            Continue = false;
        }

        private static void ErrorCallback(int errorCode, string message)
        {
            Console.WriteLine("Discord::Error({0}, {1})", errorCode, message);
        }
    }

    public class PresenceData
    {
        public string status;

        public bool IsValid()
        {
            return status.Length > 0 && status.Length <= 128;
        }
    }
}
