using System.Collections.Generic;
using GameCommon.manager;
using Microsoft.Xna.Framework;

namespace Intruders.comp.xna
{
    internal class XNAViewFactory : IViewFactory
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
            get { return (IInputManager) r_Game.Services.GetService(typeof(IInputManager)); }
        }

        public ISprite CreateSpriteComponent(string[] i_Assets)
        {
            return new SpriteComponent(i_Assets, r_Game);
        }

        public IFontComponent CreateFontComponent()
        {
            return new FontComponent(r_Game);
        }
    }
}