using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace WeaponExpansion
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class WeaponExpansion : Mod
	{
		public static void Log(String msg)
		{
			Main.NewText(msg, 255, 120, 10);
		}

        public static void SpawnCircularDust(Vector2 position, int radius, int dustType, int numDust)
		{
			for( int i = 0; i < numDust; i++)
			{
				double angle = (Math.PI * 2) * i / numDust;

				float dustX = position.X + (float)(radius * Math.Cos(angle));
				float dustY = position.Y + (float)(radius * Math.Sin(angle));

				Vector2 dustPosition = new Vector2(dustX, dustY);
				Dust dust = Dust.NewDustPerfect(dustPosition, dustType);

				dust.noGravity = true;
				dust.velocity *= 0f;
			}
		}

		public static void spawnSmokeDust(Vector2 position, float spreadSpeed, int dustType, int numDust)
		{
			for(int i = 0;i < numDust;i++)
			{
				double angle = (Math.PI * 2) * i / numDust;
				
				float xVel = MathF.Cos((float)angle);
				float yVel = MathF.Sin((float)angle);

				Vector2 dustDirection = new Vector2(xVel, yVel);
				dustDirection.Normalize();

				Dust dust = Dust.NewDustPerfect(position, dustType, Velocity : dustDirection);
				dust.velocity *= spreadSpeed;
				dust.noGravity= true;
			}
		}
    }
}
