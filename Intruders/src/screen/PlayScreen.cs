using System;
using System.Windows.Forms;
using GameCommon.screen;
using Intruders.comp;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Keys=Microsoft.Xna.Framework.Input.Keys;

namespace Intruders.screen
{
    internal class PlayScreen : GameScreen
    {
        private readonly XNAViewFactory r_Factory;
        private readonly PauseScreen r_PauseScreen;
        private readonly Random r_Random = new Random();
        private Ship m_BlueShip;
        private bool m_GameOver;
        private GreenShip m_GreenShip;
        private LivesMatrix m_Lives;
        private EnemyMatrixLogic m_Monsters;
        private MotherShip m_MotherShip;
        private ScoreDisplay m_Score;
        private Wall m_Wall;

        public PlayScreen(Game i_Game) : base(i_Game)
        {
            r_Factory = new XNAViewFactory(Game, this);

            r_PauseScreen = new PauseScreen(i_Game);

            init(6);
        }

        private void init(int i_Columns)
        {
            Clear();
            Add(new StaryBackground(Game, this));
            m_Score = new ScoreDisplay(r_Factory);

            m_BlueShip = new BlueShip(r_Factory);
            m_BlueShip.ShipHit += MarsIntruders_ShipHit;
            m_BlueShip.ScoreChanged += MarsIntruders_ScoreChanged;

            m_GreenShip = new GreenShip(r_Factory);
            m_GreenShip.ShipHit += MarsIntruders_ShipHit;
            m_GreenShip.ScoreChanged += MarsIntruders_ScoreChanged;

            m_Lives = new LivesMatrix(r_Factory, 3);

            m_Wall = new Wall(r_Factory);

            m_MotherShip = new MotherShip(r_Factory);

            m_Monsters = new EnemyMatrixLogic(r_Factory, i_Columns);
            m_Monsters.MatrixChanged += MarsIntruders_MatrixChanged;
        }


        public override void Initialize()
        {
            base.Initialize();
            if(GameOptions.GetInstance().NumberOfPlayers == 1)
            {
                Remove((IGameComponent)m_GreenShip.View);
            }
            if(GameOptions.GetInstance().CurrentLevelNumber % 5 == 1)
            {
                m_Wall.Velocity = 0;
            }
            if(GameOptions.GetInstance().CurrentLevelNumber % 5 == 2)
            {
                m_Wall.Velocity = 40;
            }
            if(GameOptions.GetInstance().CurrentLevelNumber % 5 == 3)
            {
                m_Wall.Velocity = (int)(40 + 40 * 0.15);
            }
            if(GameOptions.GetInstance().CurrentLevelNumber % 5 == 4)
            {
                m_Wall.Velocity = (int)(40 + 40 * 0.20);
            }
        }

        private void MarsIntruders_ScoreChanged(object sender, EventArgs e)
        {
            m_Score.P1Score = m_BlueShip.Score;
            m_Score.P2Score = m_GreenShip.Score;
        }

        private void MarsIntruders_MatrixChanged(object sender, EventArgs e)
        {
            EnemyMatrixLogic eml = (EnemyMatrixLogic)sender;
            if(eml.GetLowerBound() >= m_BlueShip.Position.Y)
            {
                gameOver();
            }

            if(eml.GetNumberOfMonstersAlive() == 0)
            {
                r_Factory.PlayCue("LevelWin");
                GameOptions.GetInstance().CurrentLevelNumber++;
                ExitScreen();
                init(GameOptions.GetInstance().CurrentLevelNumber + 6);
            }
        }

        private void gameOver()
        {
            r_Factory.PlayCue("GameOver");
            m_GameOver = true;
        }

        private void MarsIntruders_ShipHit(object sender, EventArgs e)
        {
            m_Lives.GreenSouls = m_GreenShip.Souls;
            m_Lives.BlueSouls = m_BlueShip.Souls;
            if(m_GreenShip.Souls == 0 && m_BlueShip.Souls == 0)
            {
                gameOver();
            }
        }

        private void sailMotherShipIfPossible()
        {
            if(r_Random.Next(2000) == 1329 && !m_MotherShip.Alive)
            {
                m_MotherShip.Position = new Vector2(-m_MotherShip.Width, m_MotherShip.Height);
                m_MotherShip.Alive = true;
            }
        }

        private void DisplayScoreMessage()
        {
            string scoreMessage = string.Format(
                "Blue score: {0}\nGreen score: {1}\n",
                m_BlueShip.Score,
                m_GreenShip.Score);
            scoreMessage += string.Format("You are a {0}\n", isAWinner() ? "winner" : "loser, you died");

            MessageBox.Show(scoreMessage, "Game Over!");
        }

        private bool isAWinner()
        {
            return m_BlueShip.Souls > 0 && m_Monsters.GetNumberOfMonstersAlive() == 0;
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
            if(InputManager.KeyPressed(Keys.P))
            {
                ScreensManager.SetCurrentScreen(r_PauseScreen);
            }
        }
    }
}