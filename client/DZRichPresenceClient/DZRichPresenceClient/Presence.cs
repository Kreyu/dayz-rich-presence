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
        /// <summary>
        /// Determines if the Discord RPC update process loop should be running. 
        /// </summary>
        private static bool Continue = true;

        /// <summary>
        /// Starts the Discord RPC update process.
        /// </summary>
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

        /// <summary>
        /// Stops the Discord RPC update process.
        /// </summary>
        public static void Stop()
        {
            DiscordRpc.Shutdown();
        }

        /// <summary>
        /// Reads json file and deserializes to the presence data.
        /// Applies a fallback if file is not found or found invalid.
        /// </summary>
        /// <returns>
        /// Presence data from the json file.
        /// </returns>
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

        /// <summary>
        /// Callback to run when Discord RPC is ready.
        /// </summary>
        private static void ReadyCallback()
        {
            Console.WriteLine("Discord::Ready()");
        }

        /// <summary>
        /// Callback to run when Discord RPC got disconnected.
        /// </summary>
        private static void DisconnectedCallback(int errorCode, string message)
        {
            Console.WriteLine("Discord::Disconnect({0}, {1})", errorCode, message);
            Continue = false;
        }

        /// <summary>
        /// Callback to run when Discord RPC got error.
        /// </summary>
        private static void ErrorCallback(int errorCode, string message)
        {
            Console.WriteLine("Discord::Error({0}, {1})", errorCode, message);
        }
    }

    public class PresenceData
    {
        public string status;

        /// <summary>
        /// Determines if current instance data is valid.
        /// </summary>
        /// <returns>
        /// A validation status.
        /// </returns>
        public bool IsValid()
        {
            return status.Length > 0 && status.Length <= 128;
        }

        /// <summary>
        /// Returns new instance of self with default presence data.
        /// </summary>
        /// <returns>
        /// New instance of self with default data.
        /// </returns>
        public static PresenceData GetDefaultData()
        {
            return new PresenceData()
            {
                status = string.Empty
            };
        }
    }
}
