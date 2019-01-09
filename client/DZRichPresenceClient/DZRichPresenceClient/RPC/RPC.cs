using DZRichPresenceClient.Misc;
using System;

namespace DZRichPresenceClient.RPC
{
    public class RPC : RPCBase
    {
        public new static void Initialize(string applicationId, ref EventHandlers handlers, bool autoRegister, string optionalSteamId)
        {
            try
            {
                RPCBase.Initialize(applicationId, ref handlers, autoRegister, optionalSteamId);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public new static void UpdatePresence(ref RichPresence presence)
        {
            try
            {
                RPCBase.UpdatePresence(ref presence);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public new static void Respond(string userid, ReplyValue reply)
        {
            try
            {
                RPCBase.Respond(userid, reply);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public new static void RunCallbacks()
        {
            try
            {
                RPCBase.RunCallbacks();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public new static void UpdateConnection()
        {
            try
            {
                RPCBase.UpdateConnection();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public new static void Shutdown()
        {
            try
            {
                RPCBase.Shutdown();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private static void LogException(Exception ex)
        {
            Logger.LogException(ex);
        }
    }
}
