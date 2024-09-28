using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
            Projectile.height = 16;
            Projectile.penetrate = 1;

            // Setup some variables for the trails
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Drawing the Trail

            // Get the texture first
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Loop through all the previous position this Projectile from start to death
            for (int i = 0; i < Projectile.oldPos.Length; i++)

            {
                // Calculate the opacity of the trail that will be rendered in the game
                Color color = lightColor * ((Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                color.A = (byte)(color.A * 0.5f);

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
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                Projectile.position,
                Vector2.Zero,
                ModContent.ProjectileType<ExplosiveOrb>(),
                Projectile.damage * 2,
                Projectile.knockBack,
                Projectile.owner);

            Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                Projectile.position,
                Vector2.Zero,
                ProjectileID.DaybreakExplosion,
                0,
                0,
                Projectile.owner);
        }
    }
}
