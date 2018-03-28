using System;
using Microsoft.Xna.Framework;

namespace Dodger.Desktop
{
    public class BoundBox : IUpdatableComponent
    {
        GameEntity entity;
        Rectangle bounds;
        Rectangle Bounds => bounds;

        public BoundBox(GameEntity entity, int x, int y, int width, int height)
        {
            this.entity = entity;
            bounds = new Rectangle(x, y, width, height);
        }

        public void Update(GameTime gameTime)
        {
            bounds.X = (int)entity.Position.X;
            bounds.Y = (int)entity.Position.Y;
        }

        public bool Intersect(BoundBox boundBox)
        {
            if (this == boundBox) return false;

            return this.bounds.Intersects(boundBox.Bounds);
        }
    }
}
