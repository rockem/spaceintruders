using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public class SpriteComponent : LoadableDrawableComponent, ISprite
    {
        private Texture2D m_TextureShip;
        private Vector2 m_Position;
        private Color m_Color = Color.White;
        private ISpriteLogic m_SpriteLogic;

        public SpriteComponent(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            m_TextureShip = Game.Content.Load<Texture2D>(m_SpriteLogic.GetImagePath());
            base.LoadContent();
            m_SpriteLogic.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            m_SpriteLogic.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = (SpriteBatch) Game.Services.GetService(typeof(SpriteBatch));
            sb.Draw(m_TextureShip, m_Position, m_Color);
            base.Draw(gameTime);
        }


        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public ISpriteLogic SpriteLogic
        {
            get { return m_SpriteLogic; }
            set { m_SpriteLogic = value; }
        }

        public int Width
        {
            get { return m_TextureShip.Width; }
        }

        public int Height
        {
            get { return m_TextureShip.Height; }
        }

        public Vector2 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }
    }
}