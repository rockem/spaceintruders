using GameCommon.manager;
using Intruders.comp;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItrudersUT.comp.mock
{
    class MockSprite : ISprite
    {
        public IInputManager m_InputManager;
        private Vector2 m_Position;
        private readonly int r_Width;
        private readonly int r_Height;
        private ISpriteLogic m_SpriteLogic;
        private bool m_Enabled = true;
        private bool m_Visible = true;

        public MockSprite(int i_Width, int i_Height)
        {
            r_Width = i_Width;
            r_Height = i_Height;
        }

        public bool Enabled
        {
            get { return m_Enabled; }
            set { m_Enabled = value; }
        }

        public bool Visible
        {
            get { return m_Visible; }
            set { m_Visible = value; }
        }

        public int Width
        {
            get { return r_Width; }
        }

        public int Height
        {
            get { return r_Height; }
        }

        public Vector2 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public Color Color
        {
            get { return Color.White; }
            set { }
        }

        public ISpriteLogic SpriteLogic
        {
            get { return m_SpriteLogic; }
            set { m_SpriteLogic = value; }
        }

        public void setComponentLogic(ISpriteLogic i_Logic)
        {
            
        }
    }
}