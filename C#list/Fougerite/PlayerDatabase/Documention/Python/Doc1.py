__title__ = 'Doc1'
__author__ = 'ice cold'

import clr
clr.AddReferenceByPartialName("Fougerite", "PlayerDatabase")
import Fougerite
#better do a try/catch so if it returns a log when the server hasnt the PlayerDatabase plugin installed
try:
	import PlayerDatabase
except:
	Util.Log("Failed to find PlayerDatabase...")

class Doc1:

	def OnEntityDestroyed(self, DestroyEvent):
		ownername = PlayerDatabase.GetNameFromID(DestroyEvent.Entity.OwnerID)
		DestroyEvent.Attacker.Message(ownername + " is the owner of this structure")
