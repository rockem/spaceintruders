using System;
using System.Windows.Forms;
using GameCommon.manager;
using GameCommon.manager.xna;
using Intruders.comp;
using Intruders.comp.xna;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MarsIntruders : Game
    {
        private readonly EnemyMatrixLogic r_Monsters;
        private SpriteBatch r_SpriteBatch;
        private readonly Ship r_BlueShip;
        private bool m_GameOver;
        private readonly Random r_Random = new Random();
        private readonly MotherShip r_MotherShip;
        private readonly WallMatrix r_Walls;
        private readonly GreenShip r_GreenShip;
        private readonly LivesMatrix r_Lives;

        public MarsIntruders()
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IViewFactory factory = new XNAViewFactory(this);

            new InputManager(this);
            new CollisionsManager(this);
            new BackgroundComponent(this);

            r_BlueShip = new BlueShip(factory);
            r_BlueShip.ShipHit += MarsIntruders_ShipHit;
            r_GreenShip = new GreenShip(factory);
            r_GreenShip.ShipHit += MarsIntruders_ShipHit;
            r_Lives = new LivesMatrix(factory, 3);
            r_Walls = new WallMatrix(factory);
            r_MotherShip = new MotherShip(factory);
            r_Monsters = new EnemyMatrixLogic(factory);
            r_Monsters.MatrixChanged += MarsIntruders_MatrixChanged;
        }

        private void MarsIntruders_MatrixChanged(object sender, EventArgs e)
        {
            EnemyMatrixLogic eml = (EnemyMatrixLogic) sender;
            if (eml.GetLowerBound() >= r_BlueShip.Position.Y || eml.GetNumberOfMonstersAlive() == 0)
            {
                m_GameOver = true;
            }
        }

        private void MarsIntruders_ShipHit(object sender, EventArgs e)
        {
            r_Lives.GreenSouls = r_GreenShip.Souls;
            r_Lives.BlueSouls = r_BlueShip.Souls;
            if(r_GreenShip.Souls == 0 && r_BlueShip.Souls == 0)
            {
                m_GameOver = true;
            }
        }

        protected override void Initialize()
        {
            r_SpriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), r_SpriteBatch);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            r_Lives.Initialize();
            r_Monsters.Initialize();
            r_Walls.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if(m_GameOver)
            {
                DisplayScoreMessage();
                Exit();
            }
            sailMotherShipIfPossible();
            r_Monsters.Update(gameTime);
            r_Walls.Update(gameTime);
            base.Update(gameTime);
        }

        private void sailMotherShipIfPossible()
        {
            if(r_Random.Next(2000) == 1329 && !r_MotherShip.Alive)
            {
                r_MotherShip.Position = new Vector2(-r_MotherShip.Width, r_MotherShip.Height);
                r_MotherShip.Alive = true;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            r_SpriteBatch.Begin();
            base.Draw(gameTime);
            r_SpriteBatch.End();
        }

        private void DisplayScoreMessage()
        {
            string scoreMessage = string.Format("Blue score: {0}\nGreen score: {1}", r_BlueShip.Score, r_GreenShip.Score);
            scoreMessage += string.Format("You are a {0}\n", isAWinner() ? "winner" : "loser, you died");

            MessageBox.Show(scoreMessage, "Game Over!");
        }

        private bool isAWinner()
        {
            return r_BlueShip.Souls > 0 && r_Monsters.GetNumberOfMonstersAlive() == 0;
        }
    }
}