using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponExpansion.Content.Projectiles
{
    public class LightbreakArrowItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 20; // The damage this arrow deals
            Item.DamageType = DamageClass.Ranged; // Ranged damage
            Item.width = 14; // Item texture width
            Item.height = 32; // Item texture height
            Item.maxStack = 999; // Maximum stack size
            Item.consumable = true; // Ammunition is consumable
            Item.knockBack = 3.5f; // Knockback
            Item.value = 10; // Item value in coins
            Item.rare = ItemRarityID.Green; // Rarity of the item
            Item.shoot = ModContent.ProjectileType<Projectiles.LightbreakArrow>(); // The projectile that this arrow shoots
            Item.shootSpeed = 120f; // The speed of the projectile
            Item.ammo = AmmoID.Arrow; // Defines that this item is an arrow-type ammo
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.StoneBlock, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
