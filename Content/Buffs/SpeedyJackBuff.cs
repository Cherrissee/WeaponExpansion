using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace WeaponExpansion.Content.Buffs
{
    internal class SpeedyJackBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.5f;
            player.runAcceleration += 0.125f;
            player.maxRunSpeed += 0.5f;
        }
    }
}
