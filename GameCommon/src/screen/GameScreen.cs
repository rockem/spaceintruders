using System;
using System.Collections.Generic;
using System.Text;
using GameCommon.comp;
using GameCommon.input;
using GameCommon.screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Infrastructure.ObjectModel.Screens
{
    public abstract class GameScreen
        : CompositeDrawableComponent<IGameComponent>
    {
        //CTOR:
        public GameScreen(Game i_Game)
            : base(i_Game)
        {
            this.Enabled = false;
            this.Visible = false;
        }

        //PROPS:
        protected IScreensMananger m_ScreensManager;
        public IScreensMananger ScreensManager
        {
            get { return m_ScreensManager; }
            set { m_ScreensManager = value; }
        }

        protected bool m_IsModal = true;
        public bool IsModal // background screen should not be updated
        {
            get { return m_IsModal; }
            set { m_IsModal = value; }
        }

        protected bool m_IsOverlayed;
        public bool IsOverlayed // background screen should be drawn
        {
            get { return m_IsOverlayed; }
            set { m_IsOverlayed = value; }
        }

        protected GameScreen m_PreviousScreen;
        public GameScreen PreviousScreen
        {
            get { return m_PreviousScreen; }
            set { m_PreviousScreen = value; }
        }

        protected bool m_HasFocus;
        public bool HasFocus // should handle input
        {
            get { return m_HasFocus; }
            set { m_HasFocus = value; }
        }

        protected float m_BlackTintAlpha = 0;
        public float BlackTintAlpha
        {
            get { return m_BlackTintAlpha; }
            set
            {
                if (m_BlackTintAlpha < 0 || m_BlackTintAlpha > 1)
                {
                    throw new ArgumentException("value must be between 0 and 1", "BackgroundDarkness");
                }

                m_BlackTintAlpha = value;
            }
        }

        private IInputManager m_InputManager;
        private IInputManager m_DummyInputManager = new DummyInputManager();

        public IInputManager InputManager
        {
            get { return this.HasFocus ? m_InputManager : m_DummyInputManager; }
        }

        public override void Initialize()
        {
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            if (m_InputManager == null)
            {
                m_InputManager = m_DummyInputManager;
            }

            base.Initialize();
        }

        public void Activate()
        {
            this.Enabled = true;
            this.Visible = true;
            this.HasFocus = true;

            OnActivated();
        }

        protected virtual void OnActivated()
        {
            if (PreviousScreen != null)
            {
                PreviousScreen.HasFocus = !this.HasFocus;
            }
        }

        protected void ExitScreen()
        {
            Deactivate();
            OnClosed();
        }

        public event EventHandler Closed;
        protected virtual void OnClosed()
        {
            if (Closed != null)
            {
                Closed(this, EventArgs.Empty);
            }
        }

        public void Deactivate()
        {
            this.Enabled = false;
            this.Visible = false;
            this.HasFocus = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (PreviousScreen != null && !this.IsModal)
            {
                PreviousScreen.Update(gameTime);
            }

            base.Update(gameTime);
        }

        #region Faded Background Support

        Texture2D m_GradientTexture;
        Texture2D m_BlankTexture;

        protected override void LoadContent()
        {
            base.LoadContent();

            m_GradientTexture = this.ContentManager.Load<Texture2D>(@"Screens\gradient");
            m_BlankTexture = this.ContentManager.Load<Texture2D>(@"Screens\blank");
        }

        public override void Draw(GameTime gameTime)
        {
            if (PreviousScreen != null
                && IsOverlayed)
            {
                PreviousScreen.Draw(gameTime);

                if (BlackTintAlpha > 0 || UseGradientBackground)
                {
                    FadeBackBufferToBlack((byte)(m_BlackTintAlpha * byte.MaxValue));
                }
            }

            base.Draw(gameTime);
        }

        protected bool m_UseGradientBackground = false;

        public bool UseGradientBackground
        {
            get { return m_UseGradientBackground; }
            set { m_UseGradientBackground = value; }
        }

        public void FadeBackBufferToBlack(byte i_Alpha)
        {
            Viewport viewport = this.GraphicsDevice.Viewport;

            Texture2D background = UseGradientBackground ? m_GradientTexture : m_BlankTexture;

            SpriteBatch.Begin();
            SpriteBatch.Draw(background,
                             new Rectangle(0, 0, viewport.Width, viewport.Height),
                             new Color(0, 0, 0, i_Alpha));
            SpriteBatch.End();
        }
        #endregion Faded Background Support
    }
}
