using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using WeaponExpansion.Content.Projectiles;

namespace WeaponExpansion.Content.Weapons
{
    internal class Timebow : ModItem
    {
        private int arrowCount = 0;
        private int fastSpeed = 12;
        private int fasterSpeed = 5;

        public override void SetDefaults()
        {

            Item.width = 42;
            Item.height = 84;
            Item.damage = 700;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = false;
            Item.channel = false;
            Item.knockBack = 5f;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.autoReuse = true;
            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Arrow;

            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(platinum : 20);

        }

        public override void HoldItem(Player player)
        {
            if (Main.mouseLeft && Main.mouseLeftRelease)
            {
                arrowCount = 0;
                Item.useTime = 20;
                Item.useAnimation = 20;
            }
        }


        public override bool? UseItem(Player player)
        {
            arrowCount += 1;
            return null;
        }



        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] <= 0;

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.DemonBow).
                AddIngredient(ItemID.DirtBlock).
                AddTile(ItemID.IronAnvil).
                Register();

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (arrowCount > 10)
            {
                // WeaponExpansion.Log("arrowcount " + arrowCount);
                Item.useTime = fasterSpeed;
                Item.useAnimation = fasterSpeed;

            }
            else if (arrowCount > 5 )
            {
                Item.useTime = fastSpeed;
                Item.useAnimation = fastSpeed;
            }
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<TimeArrow>();
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

    }
}
