using System;
using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    internal class Monster : SpriteLogic
    {
        private readonly TimeSpan r_DieDuration = TimeSpan.FromSeconds(0.5f);
        private readonly TimeSpan r_MinTimeBetweenBullets = TimeSpan.FromSeconds(0.5f);
        private TimeSpan m_TimeLeftToNextBullet;
        private TimeSpan m_TimeLeftToDie;
        private readonly Bullet r_Bullet;
        private bool m_Dying;
        private readonly Random r_Random;

        public event EventHandler MonsterHit;

        public Monster(IViewFactory i_Factory) : base(i_Factory)
        {
            r_Bullet = new Bullet(ViewFactory, eSpriteType.Bomb);
            r_Bullet.YVelocity = 200;
            r_Random = new Random(GetHashCode());
            Type = eSpriteType.Monster;
        }

        public override void Update(GameTime i_GameTime)
        {
            m_TimeLeftToDie -= i_GameTime.ElapsedGameTime;
            if(m_TimeLeftToDie.TotalSeconds > 0)
            {
                m_TimeLeftToDie -= i_GameTime.ElapsedGameTime;
                ((ISprite)View).Scale *= 0.9f;
                ((ISprite)View).Position = new Vector2(Position.X + Width * 0.04f,
                                                   Position.Y + Height * 0.04f);
            }
            else
            {
                if(m_Dying)
                {
                    Alive = false;
                    MonsterHit(this, EventArgs.Empty);
                }
            }
            int num = r_Random.Next(5000);
            m_TimeLeftToNextBullet -= i_GameTime.ElapsedGameTime;
            if(m_TimeLeftToNextBullet.TotalSeconds <= 0 && num == 287 && !r_Bullet.Alive)
            {
                shootBullet();
                m_TimeLeftToNextBullet = r_MinTimeBetweenBullets;
            }
            
            base.Update(i_GameTime);
        }

        private void shootBullet()
        {
            ViewFactory.PlayCue("EnemyShot");
            r_Bullet.Position = new Vector2(Position.X + (float)Width / 2, Position.Y + Height);
            r_Bullet.Alive = true;
        }

        public override void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
            ISpriteLogic logic = i_SpriteLogic;
            if (logic.Type == eSpriteType.Bullet)
            {
                PlayKillCue();
                m_TimeLeftToDie = r_DieDuration;
                m_Dying = true;
            }
        }

        protected virtual void PlayKillCue()
        {
            throw new NotImplementedException();
        }


        public void SwitchLook()
        {
            if(CurrentAsset == 0)
            {
                CurrentAsset = 1;
            }
            else
            {
                CurrentAsset = 0;
            }
        }
    }
}