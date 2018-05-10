bool Hasflag(Fougerite.Player pl, string flag)
{
	var id = pl.SteamID;
    if (ini.ContainsSetting(id, flag))
     {
        return true;
    }
    return false;
}