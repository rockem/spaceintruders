using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    class BackgroundComponent : SpriteComponent 
    {
        private Texture2D m_TextureBackground;

        public BackgroundComponent(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            m_TextureBackground = Game.Content.Load<Texture2D>(@"Sprites\Background1");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = new SpriteBatch(Game.GraphicsDevice);
            sb.Begin();
            sb.Draw(m_TextureBackground, Vector2.Zero, Color.Brown);
            base.Draw(gameTime);
            sb.End();
        }
    }
}