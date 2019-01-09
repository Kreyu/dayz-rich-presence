using DZRichPresenceClient.Data;
using System;
using System.IO;

namespace DZRichPresenceClient.Misc
{
    class Logger
    {
        public static void LogException(Exception ex)
        {
            using (StreamWriter writer = new StreamWriter(GetErrorLogFilePath(), true))
            {
                writer.WriteLine("-----");
                writer.WriteLine("Date: " + DateTime.Now.ToString());
                writer.WriteLine();

                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("Message : " + ex.Message);
                    writer.WriteLine("StackTrace : " + ex.StackTrace);

                    ex = ex.InnerException;
                }
            }
        }

        public static string GetErrorLogFilePath()
        {
            return Path.Combine(Game.GetDataDirectory(), Config.ErrorLogFile);
        }
    }
}
