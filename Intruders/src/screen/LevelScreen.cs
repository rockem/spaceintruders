using System;
using GameCommon.comp;
using GameCommon.screen;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.screen
{
    class LevelScreen : GameScreen 
    {
        private readonly XNAViewFactory r_Factory;
        private readonly IFontComponent m_LevelMessage;
        private int m_LevelNumber = 1;
        private TimeSpan m_TimeLeftForNextSecong;
        private readonly IFontComponent m_NumberMessage;
        private int m_SecondNumber = 1;

        public LevelScreen(Game i_Game) : base(i_Game)
        {
            IsModal = true;
            r_Factory = new XNAViewFactory(i_Game, this);
            Sprite background = new Background(i_Game);
            background.TintColor = Color.Gray;
            Add(background);
            m_LevelMessage = r_Factory.CreateFontComponent(@"Fonts\LevelFont");
            m_NumberMessage = r_Factory.CreateFontComponent(@"Fonts\LevelFont");
            
        }

        public void SetLevelNumber(int i_Level)
        {
            m_LevelNumber = i_Level;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            initLevelMessage();
            m_NumberMessage.Text = "" + m_SecondNumber;
            m_NumberMessage.PositionOfOrigin = new Vector2(
                CenterOfViewPort.X - m_NumberMessage.Width / 2,
                CenterOfViewPort.Y - m_NumberMessage.Height / 2 + m_LevelMessage.Height );

        }

        private void initLevelMessage()
        {
            m_LevelMessage.Text = "Level " + m_LevelNumber;
            m_LevelMessage.PositionOfOrigin = new Vector2(
                CenterOfViewPort.X - m_LevelMessage.Width / 2,
                CenterOfViewPort.Y - m_LevelMessage.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            m_TimeLeftForNextSecong -= gameTime.ElapsedGameTime;
            if(m_TimeLeftForNextSecong.TotalSeconds <= 0)
            {
                m_NumberMessage.Text = "" + m_SecondNumber;
                m_SecondNumber++;
                if (m_SecondNumber == 3)
                {
                    ExitScreen();
                }
                m_TimeLeftForNextSecong = TimeSpan.FromSeconds(3);
            }
        }
        
    }
}