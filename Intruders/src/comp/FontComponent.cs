using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public class FontComponent : LoadableDrawableComponent, IFontComponent
    {
        private SpriteFont m_Font;
        private Color m_Color;
        private Vector2 m_Position;
        private string m_Text;
        private ILogic m_Logic;

        public FontComponent(Game game, int i_UpdateOrder): base(game, i_UpdateOrder)
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Font = Game.Content.Load<SpriteFont>(@"Fonts\ScoreFont");
            m_Logic.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sb.DrawString(m_Font, Text, Position, Color);
            base.Draw(gameTime);
        }

        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        public Vector2 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public int Height
        {
            get { return (int) m_Font.MeasureString(Text).Y; }
        }

        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public ILogic Logic
        {
            get { return m_Logic; }
            set { m_Logic = value; }
        }
    }
}