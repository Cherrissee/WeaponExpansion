using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponExpansion.NPCs
{
    [AutoloadHead]
    public class Talon : ModNPC
    {
        public override void SetDefaults()
        {
            //NPC STATS
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 20;
            NPC.height = 20;
            NPC.aiStyle = 7;
            NPC.defense = 200;
            NPC.lifeMax = 200;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 0;
            NPCID.Sets.AttackFrameCount[NPC.type] = 1;
            NPCID.Sets.DangerDetectRange[NPC.type] = 750;
            NPCID.Sets.AttackType[NPC.type] = 1;
            NPCID.Sets.AttackTime[NPC.type] = 30;
            NPCID.Sets.AttackAverageChance[NPC.type] = 10;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;
            AnimationType = 22;

        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            for (var i = 0; i < 255; i++)
            {
                Player player = Main.player[i];
                foreach (Item item in player.inventory)
                {
                    if (item.type == ItemID.MagicQuiver)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                "Diddy",
                "Gassy",
                "Jonkler",
                "Bhey"
            };
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Shop";
            button2 = "Button2";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            Main.NewText("chat button clicked");
            if (firstButton)
            {
                shopName = "Shop";  
            }

        }

        public override void AddShops()
        {
            NPCShop shop = new(Type);

            shop.Add(ItemID.WoodenArrow);
            shop.Add(ItemID.DemonBow);


            shop.Register();
            setItemPrice(shop, ItemID.DemonBow, Item.buyPrice(gold: 1));
            setItemPrice(shop, ItemID.WoodenArrow, Item.buyPrice(gold: 1));

        }

        private void setItemPrice(NPCShop shop, int itemID, int price)
        {
            NPCShop.Entry entry = shop.GetEntry(itemID);
            entry.Item.shopCustomPrice = price;
        }

        public override string GetChat()
        {
            NPC.FindFirstNPC(ModContent.NPCType<Talon>());
            switch (Main.rand.Next(3))
            {
                case 0:
                    return "I hate Magic in general so get out of my face, if you use one.";
                case 1:
                    return "Incredible Gassy !!";
                case 2:
                    return "Deadlock, that game huh? Is good I recommend !!";
                default:
                    return "If you are being silly why not scram?";
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 1000;
            knockback = 2f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 2;
            randExtraCooldown = 10;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.WoodenArrowFriendly;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 20f;
        }

        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ItemID.GoldBow, 1, false, 0, false, false);
        }



    }

}
