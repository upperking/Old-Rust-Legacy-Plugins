
 function On_Command(Player, cmd, args){
 	if(cmd == "tester")
 	{
 		if(API.HasFlag(Player, "testerflag"))
 		{
 			Player.Notice("Yes");
 		}
 		else{
 			Player.Notice("no");
 		}
 	}
 }

