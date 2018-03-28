using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dodger.Desktop
{
    public class Level
    {
        public static Level Current { get; private set; }

        public List<GameEntity> Entities;

        Texture2D enemyTexture;
        Texture2D playerTexture;

        int screenWidth;
        public int ScreenWidth => screenWidth;
        int screenHeight;
        public int ScreenHeight => screenHeight;

        double spawnDelaySeconds = 1f;

        public Level()
        {
            Entities = new List<GameEntity>();
            Current = this;
        }

        public void Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            playerTexture = content.Load<Texture2D>("player");
            enemyTexture = content.Load<Texture2D>("enemy");

            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            Reset();


        }

        double countElapsedTime = 0f;
        public void Update(GameTime gameTime)
        {
            foreach (var e in Entities)
            {
                e.Update(gameTime);
            }
            for (int i = 0; i < Entities.Count; i++)
            {
                var e = Entities[i];
                if (e.IsDestroyed)
                {
                    Entities[i] = null;
                }
            }
            Entities = Entities.Where(x => x != null).ToList();
            if (countElapsedTime >= spawnDelaySeconds)
            {
                countElapsedTime = 0;
                Entities.Add(EnemySpawner.Spawn(enemyTexture,
                                            screenWidth,
                                            screenHeight));
            }
            countElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var e in Entities)
            {
                e.Draw(spriteBatch);
            }
        }

        public void Reset()
        {
            Entities.Clear();

            var player = new Player(playerTexture,
                                    new Vector2(screenWidth / 2, screenHeight / 2),
                                    screenWidth,
                                    screenHeight);
            player.IsCentered = true;

            Entities.Add(player);
        }

    }
}
