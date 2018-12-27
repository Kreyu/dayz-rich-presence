class RichPresence
{
    protected string                    m_FileLocation = "$profile:rich_presence.json";
    protected int                       m_Status = RichPresenceStatus.NONE;
    protected ref map<string, string>   m_PresenceData;

    void RichPresence()
    {
        m_PresenceData = Load();
    }

    void Close()
    {
        SetStatus(RichPresenceStatus.NONE);
        
        Print("<RichPresence> Successfully closed");
    }

    void SetStatus(int status)
    {
        string description = GetRichPresenceStatus().GetDescription(status);

        if (status == RichPresenceStatus.IN_GAME) {
            string playerName = "";
            GetGame().GetPlayerName(playerName);

            if (playerName.Length() > 0) {
                description = description + " | " + playerName;
            }
        }

        m_Status = status;
        m_PresenceData.Set("status", description);
        Save();

        Print("<RichPresence> New status: " + status + " (" + description + ")");
    }

    map<string, string> Load()
    {
        ref map<string, string> data = new map<string, string>();

        if (FileExist(m_FileLocation)) {
            JsonFileLoader<map<string, string>>.JsonLoadFile(m_FileLocation, data);
        } else {
            JsonFileLoader<map<string, string>>.JsonSaveFile(m_FileLocation, data);
        }

        Print("<RichPresence> Presence data loaded");

        return data;
    }

    void Save()
    {
        JsonFileLoader<map<string, string>>.JsonSaveFile(m_FileLocation, m_PresenceData);

        Print("<RichPresence> Presence data saved");
    }
}

static ref RichPresence g_RichPresence;
static ref RichPresence GetRichPresence()
{
    if (!g_RichPresence) {
        g_RichPresence = new RichPresence();
    }

    return g_RichPresence;
}
