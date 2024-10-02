using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection.Metadata;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponExpansion.Content.Projectiles
{
    internal class LightbreakArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 32;
            Projectile.height = 14;
            Projectile.penetrate = 1;

            // Setup some variables for the trails
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Drawing extra dust trails
            Dust dust = Dust.NewDustDirect(Projectile.position, 12, 12, DustID.Vortex);
            dust.velocity = Projectile.velocity * 0.1f;
            dust.noGravity = true;
            dust.alpha = 100;


            // Get the texture first
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Loop through all the previous position this Projectile from start to death
            for (int i = 0; i < Projectile.oldPos.Length; i++)

            {
                // Calculate the opacity of the trail that will be rendered in the game
                Color color = lightColor * ((Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                color.A = (byte)(color.A * 0.3f);

                // Calculate the position where to draw the Trail
                Vector2 drawPosition = Projectile.oldPos[i] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);

                // Draw that trail, pluh
                Main.spriteBatch.Draw(
                    texture,
                    drawPosition,
                    null,
                    color,
                    Projectile.rotation,
                    texture.Size() / 2f,
                    Projectile.scale,
                    SpriteEffects.None,
                    0f);
            }

            // Turn this on, to render the original projectile
            return true;
        }

        public override void AI()
        {
            // Cool light I guess
            Lighting.AddLight(Projectile.position, 1f, 0.9f, 0.1f); // Blue-ish light
            Projectile.velocity = Vector2.Multiply(Projectile.velocity, 1.025f);
        }

        public override void OnKill(int timeLeft)
        { 

            Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                Projectile.position,
                Vector2.Zero,
                ModContent.ProjectileType<ExplosiveOrb>(),
                Projectile.damage,
                Projectile.knockBack,
                Projectile.owner);


            float angle = 0;
            for (int i = 0; i <= 120; i++)
            {
                angle += 1f * 0.1f;

                Vector2 direction;
                direction.X = Projectile.Center.X * (float)Math.Cos(angle);
                direction.Y = Projectile.Center.Y  * (float)Math.Sin(angle);

                Dust dust = Dust.NewDustDirect(Projectile.position, 18, 18, DustID.Vortex);
                dust.velocity = Vector2.Normalize(direction) * 4f;
                dust.noGravity = true;
            }
        }
    }
}
