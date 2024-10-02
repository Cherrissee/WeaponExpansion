using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponExpansion.Content.Projectiles;

namespace WeaponExpansion.Content.Items.Arrows
{
    public class LightbreakArrowItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;           // The damage this arrow deals
            Item.DamageType = DamageClass.Ranged; // Ranged damage
            Item.width = 14;            // Item texture width
            Item.height = 32;           // Item texture height
            Item.maxStack = 9999;        // Maximum stack size
            Item.consumable = true;     // Ammunition is consumable
            Item.knockBack = 3.5f;      // Knockback
            Item.value = 10;            // Item value in coins
            Item.rare = ItemRarityID.Green; // Rarity of the item
            Item.shoot = ModContent.ProjectileType<LightbreakArrow>(); // The projectile that this arrow shoots
            Item.shootSpeed = 160;     // The speed of the projectile
            Item.ammo = AmmoID.Arrow;   // Defines that this item is an arrow-type ammo
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.StoneBlock, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }



        /*
         *  Create Mod: RailGunner from Risk Of Rain2
         *  
         *  It's a gun, this sniper will create a mark on each Enemy that is active on screen
         *  the mark will be randomly positioned, as long as it's inside the sprite of the enemy
         *  
         *  When this sniper hits the mark on that enemy, the mark will deal damage to that
         *  enemy with 100% crit chance, the mark will not be spawn for 2 seconds
         *  
         *  The mark will also only appear on scope mode, when scope is not on, the mark will not be active
         * 
         *  This gun also replaces all bullets into a unique one, a bullet that is non-craftable / obtainable
         *  This bullet is laser, traveling at light speed, with a lot of trail to simulate "Laser"
         */
    }
}