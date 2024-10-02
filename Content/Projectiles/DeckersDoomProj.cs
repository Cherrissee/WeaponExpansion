using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponExpansion.Content.Weapons;

namespace WeaponExpansion.Content.Projectiles
{

    internal class DeckersDoomProj : ModProjectile
    {

        private CardType rollEffect = CardType.None;
        private enum CardType
        {
            CriticalGambit, HealingAce, SpeedyJack, None
        }


        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 22;
            Projectile.height = 32;
            Projectile.penetrate = 1;
            Projectile.scale = 0.65f;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation =  MathF.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.5f;

            int roll = Main.rand.Next(1, 7);
            if(roll == 6)
            {
                rollEffect = CardType.CriticalGambit;
            }
            else if(roll == 4)
            {
                rollEffect = CardType.HealingAce;
            }
            else if(roll == 1)
            {
                rollEffect = CardType.SpeedyJack;
            }

        }

        public override bool PreDraw(ref Color lightColor)
        {
            if(rollEffect == CardType.None)
            {
                return true;
            }


            int dustEffect = 0;
            if(rollEffect == CardType.CriticalGambit)
            {
                dustEffect = DustID.SolarFlare;
            }
            else if(rollEffect == CardType.HealingAce)
            {
                dustEffect = DustID.PoisonStaff;
            }
            else
            {
                dustEffect = DustID.UltraBrightTorch;
            }


            for (int i = 0; i < 4; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, dustEffect);
                dust.velocity *= 0.3f;
                dust.noGravity = true;
            }
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(rollEffect == CardType.CriticalGambit)
            {
                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    Vector2.Zero,
                    ProjectileID.DaybreakExplosion,
                    Projectile.damage,
                    25,
                    Projectile.owner
                );
            }
            else if (rollEffect == CardType.HealingAce)
            {
                float explosionRadius = 150;
                for(int i = 50; i <= explosionRadius; i += 50)
                {
                    WeaponExpansion.SpawnCircularDust(Projectile.position, i, DustID.Firework_Green, i / 2);
                }

                // Heal the owner by 50
                Main.player[Projectile.owner].Heal(50);

                // But heal all the players inside the explosion by 80
                foreach(Player p in Main.ActivePlayers)
                {
                    float distance = Vector2.Distance(Projectile.position, p.position);
                    if(!p.dead && distance < explosionRadius)
                    {
                        p.Heal(80);
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {

            int dustEffect = DustID.Ash;
            if (rollEffect == CardType.CriticalGambit)
            {
                dustEffect = DustID.SolarFlare;
            }
            else if (rollEffect == CardType.HealingAce)
            {
                dustEffect = DustID.PoisonStaff;
            }
            else
            {
                dustEffect = DustID.UltraBrightTorch;
            }


            WeaponExpansion.spawnSmokeDust(Projectile.position, 2.5f, dustEffect, 60);
        }

    }
}
