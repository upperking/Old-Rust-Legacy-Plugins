function OnEntityDestroyed(DestroyEvent)
	local ownername = PlayerDatabase.GetNameFromID(DestroyEvent.Entity.OwnerID)
	DestroyEvent.Attacker.Message(ownername + " is the owner of this structure")
end
