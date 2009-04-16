using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public class SpriteComponent : LoadableDrawableComponent, ISprite
    {
        private Texture2D m_TextureShip;
        private ISpriteLogic r_SpriteLogic;
        private Vector2 m_Position;
        private Color m_Color = Color.White;

        public SpriteComponent(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            m_TextureShip = Game.Content.Load<Texture2D>(r_SpriteLogic.GetImagePath());
            base.LoadContent();
            r_SpriteLogic.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            r_SpriteLogic.Update(gameTime);
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

        public void setComponentLogic(ISpriteLogic i_Logic)
        {
            r_SpriteLogic = i_Logic;
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