using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RGuard;

namespace Ragnarok.Plugins.ShopSystem
{
    public class ShopGUI : MonoBehaviour
    {
        public Texture2D dollar;

        public static bool show;

        public static int money;

        public Rect startrect = new Rect(400f, 150f, 850f, 800f);

        public void Start()
        {
            byte[] dollarb = System.IO.File.ReadAllBytes(RGuard.RGuard.instance.datadir + "Images\\DollarSign.png");

            dollar = new Texture2D(64, 64, TextureFormat.RGB24, false);
            dollar.LoadImage(dollarb);

            ItemDataBlock paper = DatablockDictionary.GetByName("Paper");
            paper.iconTex = dollar;
        }
        public void OnGUI()
        {
            if (ShopGUI.show)
            {
                GUI.backgroundColor = Color.grey;
                this.startrect = GUI.Window(2, this.startrect, new GUI.WindowFunction(this.DoMyWindow), "<b> Shop:  " + ShopGUI.money + " € </b>");
            }
        }

        private void DoMyWindow(int id)
        {
            GUI.backgroundColor = Color.red;
            GUI.Label(new Rect(10f, 15, 200, 20f), "<b> <color=#76ee00> Building Supplies </color> </b>");
            var woodplanks = GUI.Button(new Rect(10f, 40f, 180f, 20f), "<color=#6495ed>1x Wood Planks = 6 € </color>");
            var woodwall = GUI.Button(new Rect(10f, 65f, 180, 20f), "<color=#6495ed>1x Wood Wall = 15 € </color>");
            var woodfoundation = GUI.Button(new Rect(10f, 90f, 180f, 20f), "<color=#6495ed>1x Wood Foundation = 25 € </color>");
            var woodpillar = GUI.Button(new Rect(10f, 115f, 180f, 20f), "<color=#6495ed>1x Wood Pillar = 20 € </color>");
            var woodceiling = GUI.Button(new Rect(10f, 140f, 180f, 20f), "<color=#6495ed>1x Wood Ceiling = 30 €</color>");
            var wooddoorway = GUI.Button(new Rect(10f, 165, 180, 20), "<color=#6495ed>1x Wood Doorway = 25 € </color>");
            var woodramp = GUI.Button(new Rect(10f, 190, 180f, 20f), "<color=#6495ed>1x Wood Ramp = 35 € </color>");
            var wooddoor = GUI.Button(new Rect(10f, 215, 180, 20), "<color=#6495ed>1x Wooden Door = 15 € </color>");
            var woodshelter = GUI.Button(new Rect(10f, 240, 180f, 20f), "<color=#6495ed>1x Wood Shelter = 12 € </color>");
            var campfire = GUI.Button(new Rect(10f, 265, 180f, 20f), "<color=#6495ed>1x Camp Fire = 14 € </color>");
            var furnace = GUI.Button(new Rect(10f, 290f, 180f, 20f), "<color=#6495ed>1x Furnace = 50 € </color>");
            var bed = GUI.Button(new Rect(10f, 315f, 180f, 20f), "<color=#6495ed>1x Bed = 80 € </color>");
            var woodstoragebox = GUI.Button(new Rect(10f, 340, 180f, 20f), "<color=#6495ed>1x Wood Storage Box = 75 € </color>");
            var woodbarricade = GUI.Button(new Rect(10f, 365, 180f, 20f), "<color=#6495ed>1x Wood Barricade = 7 € </color>");
            var sleepingbag = GUI.Button(new Rect(10f, 390f, 180f, 20f), "<color=#6495ed>1x Sleeping Bag = 18 € </color>");
            var workbench = GUI.Button(new Rect(10f, 415f, 180f, 20f), "<color=#6495ed>1x Workbench = 22 € </color>");
            var spikewall = GUI.Button(new Rect(10f, 440f, 180f, 20f), "<color=#6495ed>1x Spike Wall = 38 € </color>");
            var largespikewall = GUI.Button(new Rect(10f, 465f, 180f, 20f), "<color=#6495ed>1x Large Spike Wall = 60 € </color>");
            var woodgate = GUI.Button(new Rect(10f, 490f, 180f, 20f), "<color=#6495ed>1x Wood Gate = 90 € </color>");
            var woodgateway = GUI.Button(new Rect(10f, 515f, 180f, 20f), "<color=#6495ed>1x Wood Gateway = 120 € </color>");
            var metalpillar = GUI.Button(new Rect(10f, 540f, 180f, 20f), "<color=#6495ed>1x Metal Pillar = 115 € </color>");
            var metalwall = GUI.Button(new Rect(10f, 565, 180f, 20f), "<color=#6495ed>1x Metal Wall = 125 € </color>");
            var metalfoundation = GUI.Button(new Rect(10f, 590f, 180f, 20f), "<color=#6495ed>1x Metal Foundation = 120 € </color>");
            var metalceiling = GUI.Button(new Rect(10f, 615f, 180f, 20f), "<color=#6495ed>1x Metal Ceiling = 140 € </color>");
            var metaldoorway = GUI.Button(new Rect(10f, 640f, 180f, 20f), "<color=#6495ed>1x Metal Doorway = 125 € </color>");
            var metalramp = GUI.Button(new Rect(10f, 665f, 180f, 20f), "<color=#6495ed>1x Metal Ramp = 135 € </color>");
            var metaldoor = GUI.Button(new Rect(10f, 690f, 180f, 20f), "<color=#6495ed>1x Metal Door = 125 € </color>");

            var close = GUI.Button(new Rect(10f, 725, 150, 60f), "<b> Close </b>");

            GUI.Label(new Rect(220f, 15, 200, 20f), "<b> <color=#76ee00> Weapons </color> </b>");
            var m4 = GUI.Button(new Rect(220f, 40f, 180f, 20f), "<color=#ff7f24>1x M4 = 470 € </color>");
            var bolt = GUI.Button(new Rect(220f, 65f, 180f, 20f), "<color=#ff7f24>1x Bolt Action Rifle = 680 € </color>");
            var P250 = GUI.Button(new Rect(220f, 90f, 180f, 20f), "<color=#ff7f24>1x P250 = 360 € </color>");
            var shotgun = GUI.Button(new Rect(220f, 115f, 180f, 20f), "<color=#ff7f24>1x Shotgun = 415 € </color>");
            var pipeshotgun = GUI.Button(new Rect(220, 140f, 180f, 20f), "<color=#ff7f24>1x Pipe Shotgun = 80 € </color>");
            var mp5 = GUI.Button(new Rect(220f, 165f, 180f, 20f), "<color=#ff7f24>1x MP5A4 = 387 € </color>");
            var pistol = GUI.Button(new Rect(220, 190f, 180f, 20f), "<color=#ff7f24>1x 9mm Pistol = 280 € </color>");

            GUI.Label(new Rect(440f, 15f, 200f, 20f), "<b> <color=#76ee00> Armor </color> </b>");
            var kevlarhelmet = GUI.Button(new Rect(440f, 40f, 180f, 20f), "<color=#bcee68>1x Kevlar Helmet = 740 € </color>");
            var kevlarvest = GUI.Button(new Rect(440f, 65f, 180f, 20f), "<color=#bcee68>1x Kevlar Vest = 780 € </color>");
            var kevlarpants = GUI.Button(new Rect(440f, 90f, 180f, 20f), "<color=#bcee68>1x Kevlar Pants = 740 € </color>");
            var kelvarboots = GUI.Button(new Rect(440f, 115, 180f, 20f), "<color=#bcee68>1x Kevlar Boots = 720 € </color>");

            var leatherhelmet = GUI.Button(new Rect(440f, 140f, 180f, 20f), "<color=#bcee68>1x Leather Helmet = 534 € </color>");
            var leathervest = GUI.Button(new Rect(440f, 165f, 180f, 20f), "<color=#bcee68>1x Leather Vest = 545 € </color>");
            var leatherpants = GUI.Button(new Rect(440f, 190f, 180f, 20f), "<color=#bcee68>1x Leather Pants = 538 € </color>");
            var leatherboots = GUI.Button(new Rect(440f, 215f, 180f, 20f), "<color=#bcee68>1x Leather Boots = 530 € </color>");

            var clothhelmet = GUI.Button(new Rect(440f, 240f, 180f, 20f), "<color=#bcee68>1x Cloth Helmet = 235 € </color>");
            var clothvest = GUI.Button(new Rect(440f, 265f, 180f, 20f), "<color=#bcee68>1x Cloth Vest = 245 € </color>");
            var clothpants = GUI.Button(new Rect(440f, 290f, 180f, 20f), "<color=#bcee68>1x Cloth Pants = 220 € </color>");
            var clothboots = GUI.Button(new Rect(440f, 315f, 180f, 20f), "<color=#bcee68>1x Cloth Boots = 225 € </color>");

            GUI.Label(new Rect(660f, 15f, 200f, 20f), "<b> <color=#76ee00> Extra </color> </b>");
            var researchkit = GUI.Button(new Rect(660, 40f, 180f, 20f), "<color=#ff1493>1x Research Kit = 380 € </color>");
            var c4 = GUI.Button(new Rect(660f, 65f, 180f, 20f), "<color=#ff1493>1x Explosive Charge = 920 € </color>");
            var grenade = GUI.Button(new Rect(660f, 90f, 180f, 20f), "<color=#ff1493>1x F1 Grenade = 734 € </color>");
            var flare = GUI.Button(new Rect(660f, 115f, 180f, 20f), "<color=#ff1493>1x Flare = 545 € </color>");

            if (flare) { RGuard.RGuard.instance.SendMessageToServer("Flare,545"); }
            if (grenade) { RGuard.RGuard.instance.SendMessageToServer("F1 Grenade,734"); }
            if (c4) { RGuard.RGuard.instance.SendMessageToServer("Explosive Charge,920"); }
            if (researchkit) { RGuard.RGuard.instance.SendMessageToServer("Research Kit 1,380"); }
            if (clothboots) { RGuard.RGuard.instance.SendMessageToServer("Cloth Boots,235"); }
            if (clothpants) { RGuard.RGuard.instance.SendMessageToServer("Cloth Pants,245"); }
            if (clothvest) { RGuard.RGuard.instance.SendMessageToServer("Cloth Vest,220"); }
            if (clothhelmet) { RGuard.RGuard.instance.SendMessageToServer("Cloth Helmet,225"); }
            if (leatherboots) { RGuard.RGuard.instance.SendMessageToServer("Leather Boots,530"); }
            if (leatherpants) { RGuard.RGuard.instance.SendMessageToServer("Leather Pants,538"); }
            if (leathervest) { RGuard.RGuard.instance.SendMessageToServer("Leather Vest,545"); }
            if (leatherhelmet) { RGuard.RGuard.instance.SendMessageToServer("Leather Helmet,534"); }
            if (kelvarboots) { RGuard.RGuard.instance.SendMessageToServer("Kevlar Boots,720"); }
            if (kevlarpants) { RGuard.RGuard.instance.SendMessageToServer("Kevlar Pants,740"); }
            if (kevlarhelmet) { RGuard.RGuard.instance.SendMessageToServer("Kevlar Helmet,740"); }
            if (kevlarvest) { RGuard.RGuard.instance.SendMessageToServer("Kevlar Vest,780"); }
            if (pistol) { RGuard.RGuard.instance.SendMessageToServer("9mm Pistol,280"); }
            if (mp5) { RGuard.RGuard.instance.SendMessageToServer("MP5A4,387"); }
            if (pipeshotgun) { RGuard.RGuard.instance.SendMessageToServer("Pipe Shotgun,80"); }
            if (shotgun) { RGuard.RGuard.instance.SendMessageToServer("Shotgun,415"); }
            if (P250) { RGuard.RGuard.instance.SendMessageToServer("P250,360"); }
            if (bolt) { RGuard.RGuard.instance.SendMessageToServer("Bolt Action Rifle,680"); }
            if (m4) { RGuard.RGuard.instance.SendMessageToServer("M4,470"); }
            if (metaldoor) { RGuard.RGuard.instance.SendMessageToServer("Metal Door,125"); }
            if (metalramp) { RGuard.RGuard.instance.SendMessageToServer("Metal Ramp,135"); }
            if (metaldoorway) { RGuard.RGuard.instance.SendMessageToServer("Metal Doorway,125"); }
            if (metalceiling) { RGuard.RGuard.instance.SendMessageToServer("Metal Ceiling,140"); }
            if (metalfoundation) { RGuard.RGuard.instance.SendMessageToServer("Metal Foundation,120"); }
            if (metalwall) { RGuard.RGuard.instance.SendMessageToServer("Metal Wall,125"); }
            if (metalpillar) { RGuard.RGuard.instance.SendMessageToServer("Metal Pillar,115"); }
            if (woodgateway) { RGuard.RGuard.instance.SendMessageToServer("Wood Gateway,120"); }
            if (woodgate) { RGuard.RGuard.instance.SendMessageToServer("Wood Gate,90"); }
            if (largespikewall) { RGuard.RGuard.instance.SendMessageToServer("Large Spike Wall,60"); }
            if (spikewall) { RGuard.RGuard.instance.SendMessageToServer("Spike Wall,38"); }
            if (workbench) { RGuard.RGuard.instance.SendMessageToServer("Workbench,22"); }
            if (sleepingbag) { RGuard.RGuard.instance.SendMessageToServer("Sleeping Bag,17"); }
            if (woodplanks) { RGuard.RGuard.instance.SendMessageToServer("Wood Planks,6"); }
            if (woodwall) { RGuard.RGuard.instance.SendMessageToServer("Wood Wall,15"); }
            if (woodfoundation) { RGuard.RGuard.instance.SendMessageToServer("Wood Foundation,25"); }
            if (woodpillar) { RGuard.RGuard.instance.SendMessageToServer("Wood Pillar,20"); }
            if (woodceiling) { RGuard.RGuard.instance.SendMessageToServer("Wood Ceiling,30"); }
            if (wooddoorway) { RGuard.RGuard.instance.SendMessageToServer("Wood Doorway,20"); }
            if (woodramp) { RGuard.RGuard.instance.SendMessageToServer("Wood Ramp,35"); }
            if (wooddoor) { RGuard.RGuard.instance.SendMessageToServer("Wooden Door,15"); }
            if (woodshelter) { RGuard.RGuard.instance.SendMessageToServer("Wood Shelter,12"); }
            if (campfire) { RGuard.RGuard.instance.SendMessageToServer("Camp Fire,14"); }
            if (furnace) { RGuard.RGuard.instance.SendMessageToServer("Furnace,50"); }
            if (bed) { RGuard.RGuard.instance.SendMessageToServer("Bed,80"); }
            if (woodstoragebox) { RGuard.RGuard.instance.SendMessageToServer("Wood Storage Box,75"); }
            if (woodbarricade) { RGuard.RGuard.instance.SendMessageToServer("Wood Barricade,7"); }

            if (close) { show = false; }
            GUI.DragWindow(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
        }
    }
}
