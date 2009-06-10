using GameCommon.comp;
using GameCommon.screen;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intruders.screen
{
    internal class GameOverScreen : GameScreen
    {
        private readonly XNAViewFactory r_Factory;
        private readonly Sprite r_GameOver;
        private readonly IFontComponent r_Instructions;
        private readonly IFontComponent r_Ranks;

        public GameOverScreen(Game i_Game, int i_Green, int i_Blue) : base(i_Game)
        {
            r_Factory = new XNAViewFactory(i_Game, this);
            Add(new Background(i_Game));

            r_GameOver = new Sprite(@"Sprites\GameOver", i_Game);
            r_Ranks = r_Factory.CreateFontComponent(@"Fonts\PauseFont");
            r_Ranks.Text = "Green: " + i_Green + " Blue: " + i_Blue;
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

            r_GameOver.PositionOrigin = r_GameOver.SourceRectangleCenter;
            r_GameOver.RotationOrigin = r_GameOver.SourceRectangleCenter;
            r_GameOver.PositionOfOrigin = CenterOfViewPort;

            r_Ranks.PositionOfOrigin = new Vector2(
                CenterOfViewPort.X - r_Instructions.Width / 2,
                CenterOfViewPort.Y + r_GameOver.HeightAfterScale / 2);

            r_Instructions.PositionOfOrigin = new Vector2(
                CenterOfViewPort.X - r_Instructions.Width / 2,
                CenterOfViewPort.Y + r_GameOver.HeightAfterScale);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(InputManager.KeyPressed(Keys.Enter))
            {
                ExitScreen();
            }
            if(InputManager.KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }
            if(InputManager.KeyPressed(Keys.O))
            {
                ScreensManager.SetCurrentScreen(new MenuScreen(Game));
            }
        }
    }
}