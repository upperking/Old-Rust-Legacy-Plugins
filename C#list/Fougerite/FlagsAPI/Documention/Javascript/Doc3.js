function Hasflag(Player, flag){
 	var brr = Plugin.CreateIni(flag);
 	if(brr.ContainsSetting(brr)){
 		return true;		
 	}
 	return false;
 }