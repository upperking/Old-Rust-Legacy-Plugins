using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using Fougerite.Events;

namespace PlayerDatabase
{
   public class Doc2
    {
        // Example

        public void OnEntityDestroy(DestroyEvent de)
        {
            Entity entity = de.Entity;
            Fougerite.Player attacker = (Fougerite.Player)de.Attacker;
            //Getting the entity ownername using PlayerDatabase + owner doesnt have to be online

            string ownername = PlayerDatabase.GetNameFromID(entity.OwnerID);
            attacker.Message(ownername + " is the owner of this structure");

        }
    }
}
