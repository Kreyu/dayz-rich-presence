class RichPresence
{
    protected string                    m_FileLocation = "$profile:rich_presence.json";
    protected ref map<string, string>   m_PresenceData;
    protected ref Timer                 m_Timer;

    void RichPresence()
    {
        m_PresenceData = Load();
    }

    void Initialize()
    {
        if (!m_Timer) {
            m_Timer = new Timer();
            m_Timer.Run(15, this, "Update", NULL, true);

            Print("<RichPresence> Initialized");
        }
    }

    void Update()
    {
        Save();
    }

    void Close()
    {
        m_Timer.Stop();
        SetStatus("");
        Save();
        
        Print("<RichPresence> Successfully closed");
    }

    void SetStatus(string status)
    {
        m_PresenceData.Set("status", status);

        Print("<RichPresence> New status: " + status);
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
