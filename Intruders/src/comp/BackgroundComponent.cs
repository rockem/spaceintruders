using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    internal class BackgroundComponent : LoadableDrawableComponent
    {
        private Texture2D m_TextureBackground;
        private readonly StarComponent[,] r_Stars = new StarComponent[2, 50];
        private readonly Random r_Random = new Random();
        private TimeSpan m_TimeToSwitch = TimeSpan.FromSeconds(1);
        private int m_CurrentSet;

        public BackgroundComponent(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            for(int i = 0; i < 50; i++)
            {
                r_Stars[0, i] = new StarComponent(Game);
                r_Stars[1, i] = new StarComponent(Game);
            }
            base.Initialize();
        }


        protected override void LoadContent()
        {
            m_TextureBackground = Game.Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            positionStars();
            base.LoadContent();
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
            } else
            {
                m_CurrentSet = 0;
            }
        }

        private void positionStars()
        {
            for(int i = 0; i < 50; i++)
            {
                r_Stars[m_CurrentSet, i].Position = getRandomPlace();
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
            while (retrievedColor[0] != Color.Black)
            {
                int place = r_Random.Next(m_TextureBackground.Width * 500);
                x = place % m_TextureBackground.Width;
                y = place / m_TextureBackground.Width;
                m_TextureBackground.GetData(0, new Rectangle((int) x, (int) y, 1, 1), retrievedColor, 0, 1);
            }
            return new Vector2(x, y);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = (SpriteBatch) Game.Services.GetService(typeof(SpriteBatch));
            sb.Draw(m_TextureBackground, Vector2.Zero, Color.Brown);
            base.Draw(gameTime);
        }
    }
}