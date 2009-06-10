using System.Collections.Generic;
using GameCommon.input;
using GameCommon.screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Intruders.comp
{
    internal class XNAViewFactory : IViewFactory
    {
        private readonly Game r_Game;
        private readonly GameScreen r_Screen;
        private int m_Order = 1;
        private SoundBank m_SoundBank;
        private List<IGameComponent> r_Components = new List<IGameComponent>();

        public XNAViewFactory(Game i_Game, GameScreen i_Screen)
        {
            r_Game = i_Game;
            r_Screen = i_Screen;
        }

        #region IViewFactory Members

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

        public ISprite CreateSpriteComponent(string i_AssetName)
        {
            SpriteComponent component = new SpriteComponent(i_AssetName, r_Game);
            r_Screen.Add(component);
            return component;
        }

        public IFontComponent CreateFontComponent(string i_Font)
        {
            FontComponent component = new FontComponent(i_Font, r_Game, m_Order++);
            r_Screen.Add(component);
            return component;
        }

        public IComponent CreateComponent(string i_AssetName)
        {
            Component component = new Component(i_AssetName, r_Game, m_Order++);
            r_Screen.Add(component);
            return component;
        }

        public void PlayCue(string cue)
        {
            ((SoundManager)r_Game.Services.GetService(typeof(SoundManager))).PlayCue(cue);
            // m_SoundBank.GetCue(cue).Play();
        }

        #endregion

        public void SetSoundBank(SoundBank i_SoundBank)
        {
            m_SoundBank = i_SoundBank;
        }
    }
}