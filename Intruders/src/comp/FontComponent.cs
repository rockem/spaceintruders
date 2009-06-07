using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public class FontComponent : Component, IFontComponent
    {
        private SpriteFont m_Font;
        private Vector2 m_Position;
        private string m_Text;

        public FontComponent(Game game, int i_UpdateOrder) : base(null, game, i_UpdateOrder)
        {
        }

        #region IFontComponent Members

        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        public int Height
        {
            get { return (int)m_Font.MeasureString(Text).Y; }
        }

        #endregion

        protected override void LoadContent()
        {
            m_Font = Game.Content.Load<SpriteFont>(@"Fonts\ScoreFont");
            base.LoadContent();
        }

        protected override void InitBounds()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            //SpriteBatch sb = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            SpriteBatch.DrawString(m_Font, Text, PositionOfOrigin, Color);
            base.Draw(gameTime);
        }
    }
}