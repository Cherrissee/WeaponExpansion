using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponExpansion.Content.Projectiles;

namespace WeaponExpansion.Content.Weapons
{
    internal class Annihilator : ModItem 
    {
        public float hitRadius = 140f;

        public override void SetDefaults()
        {
            Item.width = 32;                                                                                                         
            Item.height = 32;

            // Combat Properties
            Item.damage = 1;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 6f;
            Item.autoReuse = false;
            Item.scale = 0.85f;

            // Other Values
            Item.value = 100;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.DD2_LightningBugZap;
        }

        public override bool? UseItem(Player player)
        {
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.type == ModContent.ProjectileType<ExplosiveOrb>())
                {
                    Explode(proj);
                }
            }
            return base.UseItem(player);
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

            

            // Now check for enemies
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly)
                {
                    float distance = Vector2.Distance(projectile.position, npc.position);
                    if (distance < hitRadius)
                    {
                        // Apply damage to the NPC
                        NPC.HitInfo damageDealt = npc.CalculateHitInfo(projectile.damage, projectile.direction, true, projectile.knockBack, DamageClass.Ranged);

                        npc.StrikeNPC(damageDealt, fromNet : true);
                        NetMessage.SendStrikeNPC(npc, damageDealt);
                    }
                }
            }

            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, position: projectile.position);

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
