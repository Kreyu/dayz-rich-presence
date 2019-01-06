using System;
using System.Threading.Tasks;
using DiscordRpcNet;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace DZRichPresenceClient
{
    static class Presence
    {
        private static bool Continue = true;
        private static bool Initialized = false;
        private static int StartTimestamp;

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
                while (Continue)
                {
                    if (IsGameRunning())
                    {
                        if (!Initialized) Initialize();

                        PresenceData presenceData = GetPresenceData();

                        if (presenceData.IsValid())
                        {
                            var Status = new DiscordRpc.RichPresence
                            {
                                largeImageKey = "dz-logo",
                                details = presenceData.status,
                                largeImageText = largeImageTexts[random.Next(largeImageTexts.Count)],
                                startTimestamp = StartTimestamp
                            };

                            DiscordRpc.UpdatePresence(ref Status);
                        }

                        try { 
                            DiscordRpc.RunCallbacks();
                        } catch (Exception) {}
                    }
                    else if (Initialized) Stop();

                    System.Threading.Thread.Sleep(15000);
                }
            });
        }

        public static void Stop()
        {
            DiscordRpc.Shutdown();

            Initialized = false;
        }

        private static void Initialize()
        {
            var Callbacks = new DiscordRpc.EventHandlers
            {
                readyCallback = ReadyCallback,
                disconnectedCallback = DisconnectedCallback,
                errorCallback = ErrorCallback
            };

            DiscordRpc.Initialize(Properties.Settings.Default.ApplicationId, ref Callbacks, true, null);

            StartTimestamp = (int) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            Initialized = true;
        }

        private static PresenceData GetPresenceData()
        {
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string jsonPath = Path.Combine(localAppDataPath, "DayZ", Properties.Settings.Default.PresenceDataFile);

            PresenceData presenceData = PresenceData.GetDefaultData();

            try {
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

        private static bool IsGameRunning()
        {
            return Process.GetProcessesByName("DayZ_x64").Length > 0;
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
}
