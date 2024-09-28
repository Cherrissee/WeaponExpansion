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

        public static void DrawDebugCircle(Vector2 center, float radius, int dustType)
        {
            int numDust = 100;
            for (int i = 0; i < numDust; i++)
            {
                // Angle around the circle (in radians)
                float angle = MathHelper.ToRadians(360f * i / numDust);

                // Calculate the position of the dust particle on the circle
                Vector2 position = center + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * radius;

                // Spawn dust at the calculated position
                Dust dust = Dust.NewDustPerfect(position, dustType, Vector2.Zero, 0, Color.White, 1f);
                dust.velocity = Vector2.Zero;
            }
        }
    }
}
