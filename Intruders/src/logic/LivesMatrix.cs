using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    internal class LivesMatrix : Logic
    {
        private readonly SmallBlueShip[] r_BlueSouls;
        private readonly SmallGreenShip[] r_GreenSouls;
        private readonly int r_Souls;
        private int m_BlueSouls;
        private int m_GreenSouls;

        public LivesMatrix(IViewFactory i_Factory, int i_Souls) : base(i_Factory)
        {
            r_Souls = i_Souls;
            m_BlueSouls = r_Souls;
            m_GreenSouls = r_Souls;
            r_BlueSouls = new SmallBlueShip[m_BlueSouls];
            r_GreenSouls = new SmallGreenShip[m_GreenSouls];
            for(int i = 0; i < r_Souls; i++)
            {
                r_BlueSouls[i] = new SmallBlueShip(ViewFactory);
                r_GreenSouls[i] = new SmallGreenShip(ViewFactory);
            }

            CreateView(ViewFactory.CreateComponent());
        }

        public int GreenSouls
        {
            set
            {
                m_GreenSouls = value;
                for(int i = r_Souls - 1; i >= m_GreenSouls; i--)
                {
                    r_GreenSouls[i].Alive = false;
                }
            }
        }

        public int BlueSouls
        {
            set
            {
                m_BlueSouls = value;
                for(int i = r_Souls - 1; i >= m_BlueSouls; i--)
                {
                    r_BlueSouls[i].Alive = false;
                }
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            positionSouls();
        }

        public override void Initialize()
        {
            positionSouls();
        }

        private void positionSouls()
        {
            for(int i = 0; i < m_BlueSouls; i++)
            {
                r_BlueSouls[i].Position = new Vector2(ViewFactory.ViewWidth - (r_BlueSouls[0].Width * (i + 1)), 0);
            }

            for(int i = 0; i < m_GreenSouls; i++)
            {
                r_GreenSouls[i].Position = new Vector2(
                    ViewFactory.ViewWidth - (r_GreenSouls[0].Width * (i + 1)), 
                    r_BlueSouls[0].Height + 1);
            }
        }
    }
}