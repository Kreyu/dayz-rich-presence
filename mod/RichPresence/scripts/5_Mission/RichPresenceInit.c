modded class MissionBase
{
    void MissionBase()
    {
        if (IsEligible()) {
            GetRichPresence().Initialize();
        }
    }

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
            GetRichPresence().SetStatus("In game");
        }
    }
}

modded class MissionMainMenu
{
	override void OnInit()
	{
        super.OnInit();

        if (IsEligible()) {
            GetRichPresence().SetStatus("In main menu");
        }
	}
}
