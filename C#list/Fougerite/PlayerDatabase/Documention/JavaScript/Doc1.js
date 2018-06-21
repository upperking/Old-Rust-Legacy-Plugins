function OnEntityDestroyed(DestroyEvent){
	String ownername = PlayerDatabase.GetNameFromID(DestroyEvent.Entity.OwnerID)
	DestroyEvent.Attacker.Message(ownername + " is the owner of this structure")
}