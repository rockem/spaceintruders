using GameCommon.manager;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    internal class Wall : SpriteLogic
    {
        private int m_StartLeftPosision;
        private int m_Velocity = 50;

        public Wall(IViewFactory i_Factory) : base(i_Factory)
        {
            Type = eSpriteType.Wall;
        }

        protected override void CreateAssets()
        {
            Assets = new Asset("Sprites\\Barrier_374x32");
        }

        public override void Initialize()
        {
            m_StartLeftPosision = ViewFactory.ViewWidth / 2 - Width / 2;
            Position = new Vector2(m_StartLeftPosision, ViewFactory.ViewHeight - 64 - Height * 2);
            
        }

        public override void Update(GameTime i_GameTime)
        {
            if (matrixTouchBounds())
            {
                m_Velocity *= -1;
            }

             Position = new Vector2(
                 Position.X + m_Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds,
                 Position.Y);
            
        }

        private bool matrixTouchBounds()
        {
            return Position.X >= m_StartLeftPosision + 44 / 2 ||
                   Position.X <= m_StartLeftPosision - 44 / 2;
        }

        public override void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
            ViewFactory.PlayCue("BarrierHit");
            Color[] pixel = (View as ICollidable2D).GetPixelArray();
            pixel[30] = Color.White;
            (View as SpriteComponent).Pixels = pixel;
            if(i_SpriteLogic.Type == eSpriteType.Monster)
            {
                Alive = false;
            }
        }
    }
}