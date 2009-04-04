using GameCommon.manager.xna;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MarsIntruders : Game
    {

        public MarsIntruders()
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            new InputManager(this);
            new BackgroundComponent(this);
            new ShipComponent(this);
            new EnemyMatrix(this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch sb = new SpriteBatch(GraphicsDevice);
            sb.Begin();
            base.Draw(gameTime);
            sb.End();
        }
    }
}
