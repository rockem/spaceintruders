using System;
using GameCommon.comp;
using GameCommon.screen;
using Infrastructure.ObjectModel.Animations.ConcreteAnimations;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intruders.screen
{
    internal class WelcomeScreen : GameScreen
    {
        private readonly XNAViewFactory r_Factory;
        private readonly Sprite r_WelcomeMessage;
        private readonly IFontComponent r_Instructions;

        public WelcomeScreen(Game i_Game) : base(i_Game)
        {
            r_Factory = new XNAViewFactory(i_Game, this);
            Add(new Background(Game));
            r_WelcomeMessage = new Sprite(@"Sprites\MarsIntruders", i_Game);
            Add(r_WelcomeMessage);
            r_Instructions = createInstructions();
        }

        private IFontComponent createInstructions()
        {
            IFontComponent instructions = r_Factory.CreateFontComponent(@"Fonts\PauseFont");
            instructions.Text = "Enter - Start  |  Esc - Exit  |  O - Main Menu";
            return instructions;
        }

        public override void Initialize()
        {
            base.Initialize();
            initWelcomeMessage();

            r_Instructions.PositionOfOrigin = new Vector2(
                CenterOfViewPort.X - r_Instructions.Width / 2, 
                CenterOfViewPort.Y + r_WelcomeMessage.HeightAfterScale / 2);
        }

        private void initWelcomeMessage()
        {
            r_WelcomeMessage.Animations.Add(new PulseAnimation("Pulse", TimeSpan.Zero, 1.03f, 0.7f));
            r_WelcomeMessage.Animations.Enabled = true;
            r_WelcomeMessage.PositionOrigin = r_WelcomeMessage.SourceRectangleCenter;
            r_WelcomeMessage.RotationOrigin = r_WelcomeMessage.SourceRectangleCenter;
            r_WelcomeMessage.PositionOfOrigin = CenterOfViewPort;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.Enter))
            {
                ExitScreen();
            }
            if(InputManager.KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }
        }
    }
}