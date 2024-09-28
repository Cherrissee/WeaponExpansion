
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponExpansion.Content.Projectiles;

namespace WeaponExpansion.Content.Weapons
{
    internal class ChargeBow : ModItem 
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 32;

            // Combat Properties
            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 6f;
            Item.autoReuse = true;

            // Other Values
            Item.ChangePlayerDirectionOnShoot = true;
            Item.value = 100;
            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 120;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
        }

        public override void OnConsumeAmmo(Item ammo, Player player)
        {
            WeaponExpansion.Log("Ammo used: " + ammo.Name);
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
