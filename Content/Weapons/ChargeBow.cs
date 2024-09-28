using Microsoft.Xna.Framework;
using Terraria;
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
            Item.damage = 400;
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
            Item.shootSpeed = 200;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
        }

        public override void HoldItem(Player player)
        {
            if (Main.mouseRight && Main.mouseRightRelease)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if(proj.active && proj.type == ModContent.ProjectileType<ExplosiveOrb>())
                    {
                        Explode(proj);
                    }
                }
            }
        }

        private void Explode(Projectile projectile)
        {
            // Create explosion effect
            for (int i = 0; i <= 3; i++)
            {
                int projID = Projectile.NewProjectile(projectile.GetSource_FromThis(),
                projectile.position,
                Vector2.Zero,
                ProjectileID.StardustGuardianExplosion,
                0,
                0,
                projectile.owner);

                // Adjust timeLeft for the projectiles
                Main.projectile[projID].timeLeft = 120; // Despawns after 1 second
            }

            float hitRadius = 130f;

            // Now check for enemies
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly)
                {
                    float distance = Vector2.Distance(projectile.position, npc.position);
                    if (distance < hitRadius)
                    {
                        // Apply damage to the NPC
                        npc.StrikeNPC(npc.CalculateHitInfo(
                            projectile.damage, projectile.direction, true, projectile.knockBack, DamageClass.Ranged));
                        //WeaponExpansion.DrawDebugCircle(projectile.Center, hitRadius, DustID.ToxicBubble);
                    }
                }
            }

            // Kill this projectile
            projectile.Kill();
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
