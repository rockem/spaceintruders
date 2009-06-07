using System;
using System.Collections.Generic;
using System.Text;
using GameCommon.comp;
using GameCommon.screen;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace GameCommon.manager
{
    public class ScreensMananger : CompositeDrawableComponent<GameScreen>, IScreensMananger
    {
        public ScreensMananger(Game i_Game)
            : base(i_Game)
        {
            i_Game.Components.Add(this);
        }

        private Stack<GameScreen> m_ScreensStack = new Stack<GameScreen>();

        public GameScreen ActiveScreen
        {
            get
            {
                return m_ScreensStack.Count > 0 ? m_ScreensStack.Peek() : null;
            }
        }

        public void SetCurrentScreen(GameScreen i_GameScreen)
        {
            Push(i_GameScreen);

            i_GameScreen.Activate();
        }

        public void Push(GameScreen i_GameScreen)
        {
            i_GameScreen.ScreensManager = this;

            if (!this.Contains(i_GameScreen))
            {
                this.Add(i_GameScreen);

                i_GameScreen.Closed += Screen_Closed;
            }

            if (ActiveScreen != i_GameScreen)
            {
                if (ActiveScreen != null)
                {
                    i_GameScreen.PreviousScreen = ActiveScreen;

                    ActiveScreen.Deactivate();
                }
            }

            if (ActiveScreen != i_GameScreen)
            {
                m_ScreensStack.Push(i_GameScreen);
            }

            i_GameScreen.DrawOrder = m_ScreensStack.Count;
        }

        private void Screen_Closed(object sender, EventArgs e)
        {
            Pop(sender as GameScreen);
            Remove(sender as GameScreen);
        }

        private void Pop(GameScreen i_GameScreen)
        {
            m_ScreensStack.Pop();

            if (m_ScreensStack.Count > 0)
            {
                ActiveScreen.Activate();
            }
        }

        private new bool Remove(GameScreen i_Screen)
        {
            return base.Remove(i_Screen);
        }

        private new void Add(GameScreen i_Component)
        {
            base.Add(i_Component);
        }

        protected override void OnComponentRemoved(GameComponentEventArgs<GameScreen> e)
        {
            base.OnComponentRemoved(e);

            e.GameComponent.Closed -= Screen_Closed;

            if (m_ScreensStack.Count == 0)
            {
                Game.Exit();
            }
        }

        public override void Initialize()
        {
            Game.Services.AddService(typeof(IScreensMananger), this);

            base.Initialize();
        }
    }
}