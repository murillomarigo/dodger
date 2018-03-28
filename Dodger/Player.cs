using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dodger.Desktop
{
    public class Player : GameEntity
    {
        Rectangle screenBounds;

        public Player(Texture2D texture, Vector2 position, int screenWidth, int screenHeight)
            : base("Player", texture, position)
        {
            screenBounds = new Rectangle(0, 0, screenWidth, screenHeight);
            AddComponent(new PlayerController(this, 200));
            AddComponent(new BoundBox(this, (int)position.X, (int)position.Y, texture.Width, texture.Height));
        }

        public override void Update(GameTime gameTime)
        {
            KeepInBounds();
            var level = Level.Current;
            var enemies = level.Entities.Where(x => x.Name == "Enemy");
            var bound = (BoundBox)GetComponents().FirstOrDefault(x => x.GetType() == typeof(BoundBox));
            foreach (var e in enemies)
            {
                var enemyBound = (BoundBox)e.GetComponents().FirstOrDefault(x => x.GetType() == typeof(BoundBox));
                if(bound.Intersect(enemyBound)){
                    Console.WriteLine("Colidiu");
                    Destroy();
                }
            }
            base.Update(gameTime);
        }

        private void KeepInBounds()
        {
            position.Y = Math.Min(Math.Max(position.Y, 0), screenBounds.Height - texture.Height);
            position.X = Math.Min(Math.Max(position.X, 0), screenBounds.Width - texture.Width);
        }
    }
}
