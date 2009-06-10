using System;
using System.Windows.Forms;
using GameCommon.manager;
using GameCommon.screen;
using Intruders.comp;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Keys=Microsoft.Xna.Framework.Input.Keys;

namespace Intruders.screen
{
    internal class PlayScreen : GameScreen
    {
        private readonly Ship r_BlueShip;
        private readonly XNAViewFactory r_Factory;
        private readonly GreenShip r_GreenShip;
        private readonly LivesMatrix r_Lives;
        private readonly EnemyMatrixLogic r_Monsters;
        private readonly MotherShip r_MotherShip;
        private readonly Random r_Random = new Random();
        private readonly ScoreDisplay r_Score;
        private bool m_GameOver;
        private readonly PauseScreen r_PauseScreen;
        private LevelScreen r_LevelScreen;

        public PlayScreen(Game i_Game) : base(i_Game)
        {
            r_Factory = new XNAViewFactory(Game, this);

            r_PauseScreen = new PauseScreen(i_Game);
            r_LevelScreen = new LevelScreen(i_Game);
            new CollisionsManager(Game);
            Add(new StaryBackground(Game, this));

            r_Score = new ScoreDisplay(r_Factory);

            r_BlueShip = new BlueShip(r_Factory);
            r_BlueShip.ShipHit += MarsIntruders_ShipHit;
            r_BlueShip.ScoreChanged += MarsIntruders_ScoreChanged;

            r_GreenShip = new GreenShip(r_Factory);
            r_GreenShip.ShipHit += MarsIntruders_ShipHit;
            r_GreenShip.ScoreChanged += MarsIntruders_ScoreChanged;

            r_Lives = new LivesMatrix(r_Factory, 3);

            Wall wall = new Wall(r_Factory);

            r_MotherShip = new MotherShip(r_Factory);

            r_Monsters = new EnemyMatrixLogic(r_Factory);
            r_Monsters.MatrixChanged += MarsIntruders_MatrixChanged;
        }

        private void MarsIntruders_ScoreChanged(object sender, EventArgs e)
        {
            r_Score.P1Score = r_BlueShip.Score;
            r_Score.P2Score = r_GreenShip.Score;
        }

        private void MarsIntruders_MatrixChanged(object sender, EventArgs e)
        {
            EnemyMatrixLogic eml = (EnemyMatrixLogic)sender;
            if(eml.GetLowerBound() >= r_BlueShip.Position.Y)
            {
                gameOver();
            }

            if(eml.GetNumberOfMonstersAlive() == 0)
            {
                r_Factory.PlayCue("LevelWin");
                m_GameOver = true;
            }
        }

        private void gameOver()
        {
            r_Factory.PlayCue("GameOver");
            m_GameOver = true;
        }

        private void MarsIntruders_ShipHit(object sender, EventArgs e)
        {
            r_Lives.GreenSouls = r_GreenShip.Souls;
            r_Lives.BlueSouls = r_BlueShip.Souls;
            if(r_GreenShip.Souls == 0 && r_BlueShip.Souls == 0)
            {
                gameOver();
            }
        }

        private void sailMotherShipIfPossible()
        {
            if(r_Random.Next(2000) == 1329 && !r_MotherShip.Alive)
            {
                r_MotherShip.Position = new Vector2(-r_MotherShip.Width, r_MotherShip.Height);
                r_MotherShip.Alive = true;
            }
        }

        private void DisplayScoreMessage()
        {
            string scoreMessage = string.Format(
                "Blue score: {0}\nGreen score: {1}\n",
                r_BlueShip.Score,
                r_GreenShip.Score);
            scoreMessage += string.Format("You are a {0}\n", isAWinner() ? "winner" : "loser, you died");

            MessageBox.Show(scoreMessage, "Game Over!");
        }

        private bool isAWinner()
        {
            return r_BlueShip.Souls > 0 && r_Monsters.GetNumberOfMonstersAlive() == 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(m_GameOver)
            {
                DisplayScoreMessage();
                Game.Exit();
            }

            sailMotherShipIfPossible(); 
            if (InputManager.KeyPressed(Keys.P))
            {
                ScreensManager.SetCurrentScreen(r_PauseScreen);
            }
        }

    }
}