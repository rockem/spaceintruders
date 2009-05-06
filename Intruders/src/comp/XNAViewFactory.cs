using GameCommon.manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Intruders.comp
{
    internal class XNAViewFactory : IViewFactory
    {
        private readonly Game r_Game;
        private int m_Order = 1;
        private SoundBank m_SoundBank;

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

        public ISprite CreateSpriteComponent(Asset i_Asset)
        {
            return new SpriteComponent(i_Asset, r_Game, m_Order++);
        }

        public IFontComponent CreateFontComponent()
        {
            return new FontComponent(r_Game, m_Order++);
        }

        public IComponent CreateComponent()
        {
            return new Component(r_Game, m_Order++);
        }

        public void PlayCue(string cue)
        {
            m_SoundBank.GetCue(cue).Play();
        }

        public void SetSoundBank(SoundBank i_SoundBank)
        {
            m_SoundBank = i_SoundBank;
        } 
    }
}