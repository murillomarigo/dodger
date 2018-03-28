using System;
using Microsoft.Xna.Framework;

namespace Dodger.Desktop
{
    public interface IUpdatableComponent : IComponent
    {
        void Update(GameTime gameTime);
    }
}
