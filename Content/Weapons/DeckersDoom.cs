using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponExpansion.Content.Projectiles;

namespace WeaponExpansion.Content.Weapons
{
    internal class DeckersDoom : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 32;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 300;

            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.shootSpeed = 20;
            Item.ChangePlayerDirectionOnShoot = true;
            Item.shoot = ModContent.ProjectileType<DeckersDoomProj>();
        }
    }
}
