
 function On_Command(Player, cmd, args){
 	if(cmd == "tester")
 	{
 		if(FlagsAPI.flag.HasFlag(Player, "testerflag"))
 		{
 			Player.Notice("Yes");
 		}
 		else{
 			Player.Notice("no");
 		}
 	}
 }

