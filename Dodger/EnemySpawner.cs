using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dodger.Desktop
{
    public static class EnemySpawner
    {
        public static Enemy Spawn(Texture2D texture, int screenWidth, int screenHeight)
        {
            var level = Level.Current;
            var random = new Random();

            var origin = new Vector2(random.Next(screenWidth - texture.Width), -texture.Height * 2);

            var player = level.Entities.FirstOrDefault(x => x.Name == "Player");

            Vector2 destination;
            if (player == null)
                destination = new Vector2(origin.X, screenHeight + texture.Height);
            else
                destination = player.Position;

            var enemy = new Enemy(texture, origin, destination, 100);

            return enemy;
        }
    }
}
