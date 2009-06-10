using System;
using GameCommon.screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    internal class StaryBackground : Background
    {
        private readonly Random r_Random = new Random();
        private readonly GameScreen r_Screen;
        private readonly StarComponent[,] r_Stars = new StarComponent[2,50];
        private int m_CurrentSet;
        private TimeSpan m_TimeToSwitch = TimeSpan.FromSeconds(1);

        public StaryBackground(Game game, GameScreen i_Screen)
            : base(game)
        {
            r_Screen = i_Screen;
        }

        public override void Initialize()
        {
            for(int i = 0; i < 50; i++)
            {
                r_Stars[0, i] = new StarComponent(Game);
                r_Screen.Add(r_Stars[0, i]);
                r_Stars[1, i] = new StarComponent(Game);
                r_Screen.Add(r_Stars[1, i]);
            }

            base.Initialize();
            positionStars();
        }

        public override void Update(GameTime gameTime)
        {
            m_TimeToSwitch -= gameTime.ElapsedGameTime;
            if(m_TimeToSwitch <= gameTime.ElapsedGameTime)
            {
                getNextSet();
                positionStars();
                m_TimeToSwitch = TimeSpan.FromSeconds(1);
            }

            base.Update(gameTime);
        }

        private void getNextSet()
        {
            if(m_CurrentSet == 0)
            {
                m_CurrentSet = 1;
            }
            else
            {
                m_CurrentSet = 0;
            }
        }

        private void positionStars()
        {
            for(int i = 0; i < 50; i++)
            {
                r_Stars[m_CurrentSet, i].PositionOfOrigin = getRandomPlace();
                r_Stars[m_CurrentSet, i].Scale = r_Random.Next(6) + 1;
                r_Stars[m_CurrentSet, i].Alive = true;
            }
        }

        private Vector2 getRandomPlace()
        {
            Color[] retrievedColor = new Color[1];
            retrievedColor[0] = Color.White;
            float x = 0;
            float y = 0;
            while(retrievedColor[0] != Color.Black)
            {
                int place = r_Random.Next(Texture.Width * 500);
                x = place % Texture.Width;
                y = place / Texture.Width;
                Texture.GetData(0, new Rectangle((int)x, (int)y, 1, 1), retrievedColor, 0, 1);
            }

            return new Vector2(x, y);
        }
    }
}