 def Hasflag(self, Player, str(flag)):
        brr = Plugin.CreateIni(path)
        if brr.ContainsSetting(Player.SteamID, str(flag)):
            return True
        return False