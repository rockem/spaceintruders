using System;
using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    public class Bullet : SpriteLogic
    {
        private float m_NumberOfSteps;
        private bool m_InsideWall;
        private Vector2 m_SavePos;

        public Bullet(IViewFactory i_Factory, eSpriteType i_Type) : base(i_Factory)
        {
            Alive = false;
            Type = i_Type;
            BulletHit += Bullet_Dummy;
        }

        public override void Initialize()
        {
            base.Initialize();
            m_NumberOfSteps = (float)(Height * 0.75);
        }

        public event EventHandler BulletHit;

        private void Bullet_Dummy(object sender, EventArgs e)
        {
        }

        public override void Update(GameTime i_GameTime)
        {
            Position = new Vector2(Position.X, Position.Y + (YVelocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds));
            if(Position.Y <= 0)
            {
                Alive = false;
            }

            base.Update(i_GameTime);
        }

        public override void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
            if((Type == eSpriteType.Bullet && i_SpriteLogic.Type == eSpriteType.Monster) ||
               (Type == eSpriteType.Bullet && i_SpriteLogic.Type == eSpriteType.Bomb))
            {
                Alive = false;
                Score = i_SpriteLogic.Score;
                BulletHit(this, EventArgs.Empty);
            }

            if(i_SpriteLogic.Type == eSpriteType.Wall)
            {
                if (!m_InsideWall)
                {
                    m_SavePos = Position;
                    m_InsideWall = true;
                } else
                {
                    m_NumberOfSteps -= Math.Abs(Position.Y - m_SavePos.Y);
                    if(m_NumberOfSteps <= 0)
                    {
                        die(i_SpriteLogic);
                    }
                }

            }
        }

        private void die(ISpriteLogic i_SpriteLogic)
        {
            Alive = false;
            Score = i_SpriteLogic.Score;
            BulletHit(this, EventArgs.Empty);
        }

        protected override void CreateAssets()
        {
            Assets = new Asset("Sprites\\Bullet");
        }
    }
}