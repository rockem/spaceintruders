using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    public class SpriteAtt
    {
        private Vector2 m_Position;
        private Color m_Color;

        public Vector2 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }
    }
}