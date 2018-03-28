using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Dodger.Desktop
{
    public class PlayerController : IUpdatableComponent
    {
        GameEntity entity;
        int speed;
        public PlayerController(GameEntity entity, int speed)
        {
            this.entity = entity;
            this.speed = speed;
        }

        public void Update(GameTime gameTime)
        {
            var current = entity.Position;
            var destination = current;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                destination.Y = current.Y - speed
                                   * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                destination.Y = current.Y + speed
                                   * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                destination.X = current.X - speed
                                   * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                destination.X = current.X + speed
                                   * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            entity.SetPosition(destination.X, destination.Y);
        }
    }
}
