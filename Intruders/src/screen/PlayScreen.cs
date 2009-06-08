using System;
using System.Windows.Forms;
using GameCommon.manager;
using Infrastructure.ObjectModel.Screens;
using Intruders.comp;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
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
        private AudioEngine m_AudioEngine;
        private bool m_GameOver;
        private Cue m_Music;
        private SoundBank m_SoundBank;
        private WaveBank m_WaveBank;
        private SpriteBatch r_SpriteBatch;
        private PauseScreen r_PauseScreen;

        public PlayScreen(Game i_Game) : base(i_Game)
        {
            r_Factory = new XNAViewFactory(Game, this);

            r_PauseScreen = new PauseScreen(i_Game);
            new CollisionsManager(Game);
            BackgroundComponent bg = new BackgroundComponent(Game, this);
            Add(bg);

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

        protected override void LoadContent()
        {
            m_AudioEngine = new AudioEngine(@"Content\Audio\GameSounds.xgs");
            m_WaveBank = new WaveBank(m_AudioEngine, @"Content\Audio\Wave Bank.xwb");
            m_SoundBank = new SoundBank(m_AudioEngine, @"Content\Audio\Sound Bank.xsb");
            r_Factory.SetSoundBank(m_SoundBank);

            m_Music = m_SoundBank.GetCue("BGMusic");

            m_Music.Play();

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            m_AudioEngine.Dispose();
            m_WaveBank.Dispose();
            m_SoundBank.Dispose();
            base.UnloadContent();
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
            base.Update(gameTime);
        }

    }
}