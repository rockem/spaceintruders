using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public class FontComponent : Component, IFontComponent
    {
        private SpriteFont m_Font;
        private string m_Text;
        private readonly string r_FontName;

        public FontComponent(string i_FontName, Game i_Game, int i_UpdateOrder) : base(null, i_Game, i_UpdateOrder)
        {
            r_FontName = i_FontName;
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

        public int Width
        {
            get { return (int)m_Font.MeasureString(Text).X; }
        }

        #endregion

        protected override void LoadContent()
        {
            m_Font = Game.Content.Load<SpriteFont>(r_FontName);
            base.LoadContent();
        }

        protected override void InitBounds()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            //SpriteBatch sb = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            SpriteBatch.DrawString(m_Font, Text, PositionOfOrigin, TintColor);
            base.Draw(gameTime);
        }
    }
}