using System.Collections.Generic;
using GameCommon.manager;
using GameCommon.manager.xna;
using Intruders.comp;
using Intruders.comp.xna;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MarsIntruders : Game
    {
        private readonly EnemyMatrixLogic r_Monsters;
        private SpriteBatch r_SpriteBatch;

        public MarsIntruders()
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IViewFactory factory = new XNAViewFactory(this);

            new InputManager(this);
            new CollisionsManager(this);
            new BackgroundComponent(this);

            new Ship(factory);
            r_Monsters = new EnemyMatrixLogic(factory);
        }

        protected override void Initialize()
        {
            r_SpriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), r_SpriteBatch);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            r_Monsters.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            r_Monsters.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            r_SpriteBatch.Begin();
            base.Draw(gameTime);
            r_SpriteBatch.End();
        }
    }
}