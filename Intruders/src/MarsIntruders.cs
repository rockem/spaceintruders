using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intruders
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MarsIntruders : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D m_TextureEnemy1;
        private Texture2D m_TextureBackground;
        private Texture2D m_TextureShip;

        public MarsIntruders()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            m_TextureEnemy1 = Content.Load<Texture2D>(@"Sprites\Enemy01");
            m_TextureBackground = createTextureFrom(@"Sprites\Background1");
            m_TextureShip = createTextureFrom(@"Sprites\Ship");
            

            // TODO: use this.Content to load your game content here
        }

        private Texture2D createTextureFrom(string i_Path)
        {
            return Content.Load<Texture2D>(i_Path);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(m_TextureBackground, Vector2.Zero, Color.Brown);
            drawEnemyMatrix();
            drawShip();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void drawShip()
        {
            Vector2 shipPosition = new Vector2(0, GraphicsDevice.Viewport.Height - m_TextureShip.Height / 2 - 30);
            spriteBatch.Draw(m_TextureShip, shipPosition, Color.White);
        }

        private void drawEnemyMatrix()
        {
            drawEnemyRow(0, Color.Pink);
            drawEnemyRow(1, Color.LightBlue);
            drawEnemyRow(2, Color.LightBlue);
            drawEnemyRow(3, Color.Yellow);
            drawEnemyRow(4, Color.Yellow);
        }

        private void drawEnemyRow(int i_Row, Color i_Color)
        {
            Vector2 currentPosition = new Vector2();
            int enemyHeight = m_TextureEnemy1.Height;
            currentPosition.Y = (float)(enemyHeight * 3 + (i_Row * (enemyHeight + enemyHeight * 0.6)));
            for (int i = 0; i < 9; i++)
            {
                int enemyWidth = m_TextureEnemy1.Width;
                currentPosition.X = (float)(i * (enemyWidth + enemyWidth * 0.6));
                spriteBatch.Draw(m_TextureEnemy1, currentPosition, i_Color);
            }
        }
    }
}
