namespace DZRichPresenceClient
{
    public class PresenceData
    {
        public string status;

        public bool IsValid()
        {
            return status.Length > 0 && status.Length <= 128;
        }

        public static PresenceData GetDefaultData()
        {
            return new PresenceData()
            {
                status = string.Empty
            };
        }
    }
}
