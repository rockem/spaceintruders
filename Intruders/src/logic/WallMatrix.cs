using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    internal class WallMatrix : Logic
    {
        private const int k_NumberOfWalls = 4;
        private readonly Wall[] r_Walls = new Wall[k_NumberOfWalls];
        private int m_StartLeftPosision;
        private int m_Velocity = 100;

        public WallMatrix(IViewFactory i_Factory) : base(i_Factory)
        {
            for(int i = 0; i < k_NumberOfWalls; i++)
            {
                r_Walls[i] = new Wall(ViewFactory);
            }

            // CreateView(ViewFactory.CreateComponent());
        }

        public override void Update(GameTime i_GameTime)
        {
            if(matrixTouchBounds())
            {
                m_Velocity *= -1;
            }

            for(int i = 0; i < k_NumberOfWalls; i++)
            {
                r_Walls[i].Position = new Vector2(
                    r_Walls[i].Position.X + (m_Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds),
                    r_Walls[i].Position.Y);
            }
        }

        private bool matrixTouchBounds()
        {
            return r_Walls[0].Position.X >= m_StartLeftPosision + (r_Walls[0].Width / 2) ||
                   r_Walls[0].Position.X <= m_StartLeftPosision - (r_Walls[0].Width / 2);
        }

        public override void Initialize()
        {
            m_StartLeftPosision = (ViewFactory.ViewWidth / 2) - (matrixWidth() / 2);
            for(int i = 0; i < k_NumberOfWalls; i++)
            {
                r_Walls[i].Position = new Vector2(
                    m_StartLeftPosision + ((float)(r_Walls[0].Width + (r_Walls[0].Width * 1.5)) * i),
                    ViewFactory.ViewHeight - 64 - (r_Walls[0].Height * 2));
            }
        }

        private int matrixWidth()
        {
            return (int)((r_Walls[0].Width * k_NumberOfWalls) + (r_Walls[0].Width * 1.5 * (k_NumberOfWalls - 1)));
        }
    }
}