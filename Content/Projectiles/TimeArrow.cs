using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace WeaponExpansion.Content.Projectiles
{
    internal class TimeArrow : ModProjectile
    {
        private float timer, maxTime = 8;

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 32;
            Projectile.height = 14;
            Projectile.penetrate = 1;
        }

        public override void AI()
        {
            timer += 1;
            if (timer > maxTime)
            {
                timer = 0;

                // Create a random offset within a specified range
                float offsetX = Main.rand.Next(-20, 21);
                float offsetY = Main.rand.Next(-20, 21);

                // Create the new position with the offset
                Vector2 newPosition = Projectile.position + new Vector2(offsetX, offsetY);

                // Spawn the projectile with the new position
                Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                    newPosition,
                    Vector2.Zero,
                    ModContent.ProjectileType<ExplosiveOrb>(),
                    Projectile.damage * 4,
                    Projectile.knockBack,
                    Projectile.owner);



            }
        }
    }
}
