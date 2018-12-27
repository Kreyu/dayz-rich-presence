class RichPresenceStatus
{
    static const int NONE = 0
    static const int IN_MENU = 1;
    static const int IN_GAME = 2;

    ref TStringArray m_Descriptions = new TStringArray();

    void RichPresenceStatus()
    {
        m_Descriptions.InsertAt("", NONE);
        m_Descriptions.InsertAt("In menu", IN_MENU);
        m_Descriptions.InsertAt("In game", IN_GAME);
    }

    string GetDescription(int status)
    {
        return m_Descriptions.Get(status);
    }
}

static ref RichPresenceStatus g_RichPresenceStatus;
static ref RichPresenceStatus GetRichPresenceStatus()
{
    if (!g_RichPresenceStatus) {
        g_RichPresenceStatus = new RichPresenceStatus();
    }

    return g_RichPresenceStatus;
}