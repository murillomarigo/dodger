using System;
using Microsoft.Xna.Framework;

namespace Dodger.Desktop
{
    public class EnemyController : IUpdatableComponent
    {
        GameEntity entity;
        Vector2 position;
        Vector2 destination;
        int speed;

        public EnemyController(GameEntity entity, Vector2 origin, Vector2 destination, int speed)
        {
            this.entity = entity;
            this.position = origin;
            this.destination = destination;
            this.speed = speed;
        }

        public void Update(GameTime gameTime)
        {
            var direction = Vector2.Normalize(destination - position);

            destination += direction;

            position += direction * (float)gameTime.ElapsedGameTime.TotalSeconds * speed;

            if (Vector2.Distance(position, destination) >= 1)
                entity.SetPosition(position);
        }
    }
}
