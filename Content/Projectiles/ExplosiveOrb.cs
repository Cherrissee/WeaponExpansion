using Microsoft.Xna.Framework;
using System;
using System.Reflection.Metadata;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponExpansion.Content.Projectiles
{
    internal class ExplosiveOrb : ModProjectile
    {

        private float angle = 0f;
        private float speed = 1.2f;
        private NPC target = null;

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = false; // It should be hostile to enemies; this is a landmine
            Projectile.hostile = false;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = -1; // Infinite penetration, to not despawn on hit
            Projectile.tileCollide = false;
            Projectile.timeLeft = 1200; // Lasts for 20 seconds
            Projectile.damage = 300;
        }

        public override void AI()
        {

            if (target != null)
            {
                if (target.life <= 0)
                {
                    target = null;
                    return;
                }
                // Increment the angle based on speed
                angle += speed * 0.1f; // Adjust for frame rate

                // Calculate new position based on the enemy's position
                Projectile.position.X = target.Center.X + ((target.width / 2) + 50) * (float)Math.Cos(angle) - (Projectile.width / 2);
                Projectile.position.Y = target.Center.Y + ((target.height / 2) + 50) * (float)Math.Sin(angle) - (Projectile.height / 2);
            }
            else
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly)
                    {
                        float distance = Vector2.Distance(Projectile.position, npc.position);
                        if (distance < 45)
                        {
                            target = npc;
                        }
                    }
                }
            }

            Lighting.AddLight(Projectile.position, 0.1f, 0.5f, 1f);
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, 16, 16, DustID.HallowSpray);
                dust.velocity *= 0.2f; // Slower particle movement
            }
        }
    }
}
