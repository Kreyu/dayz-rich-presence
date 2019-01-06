using DZRichPresenceClient.Misc;
using System;
using System.Threading.Tasks;

namespace DZRichPresenceClient.RPC
{
    public static class Presence
    {
        private static bool Continue = true;
        private static bool Initialized = false;
        private static int StartTimestamp;

        public static void Start()
        {
            Task.Run((Action) delegate
            {
                while (Continue)
                {
                    UpdateTick();
                    System.Threading.Thread.Sleep(15000);
                }
            });
        }

        public static void Stop()
        {
            if (Initialized)
            {
                try
                {
                    RPC.Shutdown();
                }
                catch (Exception) { }
            }

            Initialized = false;
        }

        private static void UpdateTick()
        {
            if (!Game.IsRunning())
            {
                Stop();
                return;
            }

            if (!Initialized)
            {
                var Callbacks = new RPCBase.EventHandlers
                {
                    disconnectedCallback = DisconnectedCallback,
                };

                RPC.Initialize(Properties.Settings.Default.ApplicationId, ref Callbacks, true, null);

                StartTimestamp = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                Initialized = true;
            }

            PresenceData presenceData = Game.GetPresenceData();

            if (presenceData.IsValid())
            {
                var Status = new RPCBase.RichPresence
                {
                    largeImageKey = "dz-logo",
                    details = presenceData.status,
                    largeImageText = Properties.Settings.Default.LargeImageTexts.RandomElement(),
                    startTimestamp = StartTimestamp
                };

                RPC.UpdatePresence(ref Status);
            }

            RPC.RunCallbacks();
        }

        private static void DisconnectedCallback(int errorCode, string message)
        {
            Continue = false;
        }
    }
}
