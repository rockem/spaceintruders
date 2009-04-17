using System.Collections.Generic;
using GameCommon.manager;
using Intruders.comp;

namespace ItrudersUT.comp.mock
{
    class MockViewFactory : IViewFactory
    {
        private readonly int r_Width;
        private readonly int r_Height;
        private readonly int r_SpriteWidth;
        private readonly int r_SpriteHeight;
        public readonly List<ISprite> r_Sprites = new List<ISprite>();
        public IInputManager m_InputManager;

        public MockViewFactory(int i_Width, int i_Height, int i_SpriteWidth, int i_SpriteHeight)
        {
            r_Width = i_Width;
            r_Height = i_Height;
            r_SpriteWidth = i_SpriteWidth;
            r_SpriteHeight = i_SpriteHeight;
        }

        public int ViewHeight
        {
            get { return r_Height; }
        }

        public int ViewWidth
        {
            get { return r_Width; }
        }

        public IInputManager InputManager
        {
            get { return m_InputManager; }
        }

        public ISprite CreateSpriteComponent()
        {
            ISprite s = new MockSprite(r_SpriteWidth, r_SpriteHeight);
            r_Sprites.Add(s);
            return s;
        }
    }
}