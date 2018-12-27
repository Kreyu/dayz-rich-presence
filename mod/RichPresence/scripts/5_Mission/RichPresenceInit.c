modded class MissionBase
{
    void ~MissionBase()
    {
        if (IsEligible()) {
            GetRichPresence().Close();
        }
    }

    bool IsEligible()
    {
        return GetGame().IsClient() || !GetGame().IsMultiplayer();
    }
}

modded class MissionGameplay
{
    override void OnInit()
    {
        super.OnInit();

        if (IsEligible()) {
            GetRichPresence().SetStatus(RichPresenceStatus.IN_GAME);
        }
    }
}

modded class MissionMainMenu
{
	override void OnInit()
	{
        super.OnInit();

        if (IsEligible()) {
            GetRichPresence().SetStatus(RichPresenceStatus.IN_MENU);
        }
	}
}
