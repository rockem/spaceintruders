using GameCommon.manager;
using Microsoft.Xna.Framework;

namespace Intruders.comp.xna
{
    class XNAViewFactory : IViewFactory 
    {
        private readonly Game r_Game;

        public XNAViewFactory(Game i_Game)
        {
            r_Game = i_Game;    
        }

        public int ViewHeight
        {
            get { return r_Game.GraphicsDevice.Viewport.Height; }
        }

        public int ViewWidth
        {
            get { return r_Game.GraphicsDevice.Viewport.Width; }
        }

        public IInputManager InputManager
        {
            get { return (IInputManager)r_Game.Services.GetService(typeof(IInputManager)); }
        }

        public ISprite CreateSpriteComponent()
        {
            return new SpriteComponent(r_Game);
        }
    }
}