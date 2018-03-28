using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dodger.Desktop
{
    public class Enemy : GameEntity
    {
        byte state = 0;
        public Enemy(Texture2D texture, Vector2 position, Vector2 destionation, int speed)
            : base("Enemy", texture, position)
        {
            AddComponent(new EnemyController(this, position, destionation, speed));
            AddComponent(new BoundBox(this, (int)position.X, (int)position.Y, texture.Width, texture.Height));
        }

        public override void Update(GameTime gameTime)
        {
            var level = Level.Current;
            if (state == 0
               && position.X >= 0 && position.X <= level.ScreenWidth
                && position.Y >= 0 && position.Y <= level.ScreenHeight)
            {
                state = 1;
            }
            else if (state == 1 
                     && (( position.X < 0 || position.X > level.ScreenWidth)
                         || (position.Y < 0 || position.Y > level.ScreenHeight)))
            {
                Destroy();
            }
            base.Update(gameTime);
        }
    }
}
