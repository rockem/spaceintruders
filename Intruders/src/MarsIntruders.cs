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
        private readonly Ship r_Ship;
        private bool m_GameOver;
        private Random i_Random = new Random();
        private MotherShip r_MotherShip;


        public MarsIntruders()
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IViewFactory factory = new XNAViewFactory(this);

            new InputManager(this);
            new CollisionsManager(this);
            new BackgroundComponent(this);

            r_Ship = new Ship(factory);
            r_MotherShip = new MotherShip(factory);
            r_Ship.ShipHit += MarsIntruders_ShipHit;
            r_Monsters = new EnemyMatrixLogic(factory);
            r_Monsters.MatrixChanged += MarsIntruders_MatrixChanged;
        }

        private void MarsIntruders_MatrixChanged(object sender, EventArgs e)
        {
            EnemyMatrixLogic eml = (EnemyMatrixLogic) sender;
            if (eml.GetLowerBound() >= r_Ship.Position.Y || eml.GetNumberOfMonstersAlive() == 0)
            {
                m_GameOver = true;

            }
        }

        private void MarsIntruders_ShipHit(object sender, EventArgs e)
        {
            if(((Ship)sender).Souls == 0)
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
            r_Monsters.Initialize();
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
            base.Update(gameTime);
        }

        private void sailMotherShipIfPossible()
        {
            if(i_Random.Next(2000) == 1329 && !r_MotherShip.Alive)
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
            string scoreMessage = string.Format("Your score: {0}\n", r_Ship.Score);
            scoreMessage += string.Format("You are a {0}\n", isAWinner() ? "winner" : "loser, you died");

            MessageBox.Show(scoreMessage, "Game Over!");
        }

        private bool isAWinner()
        {
            return r_Ship.Souls > 0 && r_Monsters.GetNumberOfMonstersAlive() == 0;
        }
    }
}