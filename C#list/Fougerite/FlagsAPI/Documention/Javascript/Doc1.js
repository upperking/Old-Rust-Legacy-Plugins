 var path = require('path');
 var flagpath = path.basename('\\Save\\FlagsAPI\\PlayerFlagdb.ini');

 function On_PluginInit() {
 	var flag = flagpath;
 }
 function On_Command(Player, cmd, args){
 	if(cmd == "tester")
 	{
 		if(Hasflag(Player, "testerflag"))
 		{
 			Player.Notice("Yes");
 		}
 		else{
 			Player.Notice("no");
 		}
 	}
 }
 function Hasflag(Player, flag){
 	var brr = Plugin.CreateIni(flag);
 	if(brr.ContainsSetting(brr)){
 		return true;		
 	}
 	return false;
 }

