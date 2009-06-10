using GameCommon.comp;
using GameCommon.screen;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intruders.screen
{
    class PauseScreen : GameScreen
    {
        private readonly XNAViewFactory r_Factory;
        private readonly Sprite r_PauseMessage;
        private readonly IFontComponent r_ResumeMessage;

        public PauseScreen(Game i_Game)
            : base(i_Game)
        {
            r_Factory = new XNAViewFactory(i_Game, this);
            IsModal = true;
            IsOverlayed = true;
            UseGradientBackground = true;
            BlackTintAlpha = 0.60f;

            r_PauseMessage = new Sprite(@"Sprites\Pause", Game);
            r_ResumeMessage = r_Factory.CreateFontComponent(@"Fonts\PauseFont");
            r_ResumeMessage.Text = "Press R To Resume";
            Add(r_PauseMessage);
        }

        public override void Initialize()
        {
            base.Initialize();
            initPauseMessage();
            initResumeMessage();
        }

        private void initResumeMessage()
        {
            r_ResumeMessage.PositionOfOrigin = new Vector2(
                CenterOfViewPort.X - r_ResumeMessage.Width / 2, 
                CenterOfViewPort.Y + r_PauseMessage.HeightAfterScale / 2);
            r_ResumeMessage.TintColor = Color.Gray;
        }

        private void initPauseMessage()
        {
            r_PauseMessage.PositionOrigin = r_PauseMessage.SourceRectangleCenter;
            r_PauseMessage.RotationOrigin = r_PauseMessage.SourceRectangleCenter;
            r_PauseMessage.PositionOfOrigin = CenterOfViewPort;
        }


        public override void Update(GameTime gameTime)
        {
            if (r_Factory.InputManager.KeyPressed(Keys.R))
            {
                ExitScreen();
            }
        }
    }
}