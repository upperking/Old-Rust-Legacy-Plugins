__title__ = 'example doc py'
__author__ = 'ice cold'
__version__ = '1.0'
import clr
clr.AddReferenceByPartialName("Fougerite")
import Fougerite
try:
    clr.AddRefrenceByPartialName("FlagsAPI")
    import FlagsAPI
exept:
    Util.Log("Failed to import FlagsAPI.dll")
        
class Example:
    def On_Command(self, Player, cmd, args):
        if cmd == "tester":
            if API.HasFlag(Player, "testflager"):
                Player.Notice("yes")
            else:
                Player.Notice("no")
