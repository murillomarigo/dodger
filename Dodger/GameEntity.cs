using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dodger.Desktop
{
    public abstract class GameEntity
    {
        public string Name { get; private set; }
        protected Vector2 position;
        public Vector2 Position => position;
        public bool IsCentered { get; internal set; }
        public bool WillDestroy { get; private set; }
        public bool IsDestroyed { get; private set; }

        protected Texture2D texture;

        List<IComponent> components;

        public IReadOnlyList<IComponent> GetComponents()
        {
            return components;
        }

        public GameEntity(string name, Texture2D texture, Vector2 position)
        {
            this.Name = name;
            this.texture = texture;
            this.position = position;
            this.components = new List<IComponent>();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (IComponent c in components)
            {
                if (c is IUpdatableComponent)
                {
                    var gt = gameTime;
                    ((IUpdatableComponent)c).Update(gt);
                }
            }
            if (WillDestroy)
            {
                for (int i = 0; i < components.Count; i++)
                {
                    components[i] = null;
                }
                components.Clear();
                IsDestroyed = true;
                return;
            }
            if (IsDestroyed) return;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsDestroyed || WillDestroy) return;

            if (IsCentered)
                spriteBatch.Draw(texture, position,
                                 null,
                                 Color.White,
                                 0,
                                 new Vector2(texture.Width / 2, texture.Height / 2),
                                 Vector2.One,
                                 SpriteEffects.None,
                                 0);
            else
                spriteBatch.Draw(texture, position, Color.White);
        }

        public void SetPosition(Vector2 postion)
        {
            SetPosition(postion.X, postion.Y);
        }

        public void SetPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        public void AddComponent(IComponent component)
        {
            components.Add(component);
        }

        public void Destroy()
        {
            WillDestroy = true;
        }
    }
}
