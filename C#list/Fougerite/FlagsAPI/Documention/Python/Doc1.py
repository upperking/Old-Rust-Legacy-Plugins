__title__ = 'example doc py'
__author__ = 'ice cold'
__version__ = '1.0'
import clr
clr.AddReferenceByPartialName("Fougerite")
import Fougerite

import sys
path = Util.GetRootFolder()
flagpath = sys.path.append(path + "\\Save\\FlagsAPI\\PlayerFlagdb.ini")

class Example:

    def On_PluginInit(self):
        flag = flagpath

    def On_Command(self, Player, cmd, args):
        if cmd == "tester":
            if self.Hasflag(Player, "testflager"):
                Player.Notice("yes")
            else:
                Player.Notice("no")

    def Hasflag(self, Player, str(flag)):
        brr = Plugin.CreateIni(path)
        if brr.ContainsSetting(Player.SteamID, str(flag)):
            return True
        return False