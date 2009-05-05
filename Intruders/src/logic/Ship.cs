using System;
using System.Collections.Generic;
using GameCommon.manager;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intruders.logic
{
    public class Ship : SpriteLogic
    {
        private readonly List<Bullet> r_Bullets = new List<Bullet>();
        private const int k_MaxNumOfBullets = 3;
        private readonly TimeSpan r_MinTimeBetweenBullets = TimeSpan.FromSeconds(0.5f);
        private TimeSpan m_TimeLeftForNextShoot;
        private const int k_BulletVelocity = -200;
        private const int k_PxForSecond = 120;
        private int m_RemainingSouls = 3;

        public event EventHandler ShipHit;

        public event EventHandler ScoreChanged;

        public Ship(IViewFactory i_Factory) : base(i_Factory)
        {
            Type = eSpriteType.Ship;
        }

        public int Souls
        {
            get
            {
                return m_RemainingSouls;
            }
        }

        public override void Update(GameTime time)
        {
            int velocity = 0;
            float currentX = ((ISprite)View).Position.X;
            m_TimeLeftForNextShoot -= time.ElapsedGameTime;

            if(inputFire())
            {
                if(m_TimeLeftForNextShoot.TotalSeconds <= 0)
                {
                    shootBullet();
                    m_TimeLeftForNextShoot = r_MinTimeBetweenBullets;
                }
            }

            if(inputRight())
            {
                velocity = k_PxForSecond;
            }
            if(inputLeft())
            {
                velocity = -k_PxForSecond;
            }

            currentX += velocity * (float) time.ElapsedGameTime.TotalSeconds;
            if (useMouseForMovement())
            {
                currentX += ViewFactory.InputManager.MousePositionDelta.X;
            }
            currentX = MathHelper.Clamp(currentX, 0, ViewFactory.ViewWidth - ((ISprite)View).Width);
            ((ISprite)View).Position = new Vector2(currentX, ((ISprite)View).Position.Y);
        }

        protected virtual bool inputFire()
        {
            return ViewFactory.InputManager.KeyPressed(Keys.Enter) ||
                   ViewFactory.InputManager.ButtonPressed(eInputButtons.Left);
        }

        protected virtual bool inputLeft()
        {
            return ViewFactory.InputManager.KeyHeld(Keys.Left);
        }

        protected virtual bool inputRight()
        {
            return ViewFactory.InputManager.KeyHeld(Keys.Right);
        }

        protected virtual bool useMouseForMovement()
        {
            return false;
        }

        private void shootBullet()
        {
            Bullet bullet = findAvailableBullet();
            if(bullet != null)
            {
                bullet.Position = new Vector2(Position.X + (float) Width / 2 - (float) bullet.Width / 2,
                                              Position.Y - bullet.Height);
                bullet.Alive = true;
            }
        }

        private Bullet findAvailableBullet()
        {
            foreach(Bullet bullet in r_Bullets)
            {
                if(!bullet.Alive)
                {
                    return bullet;
                }
            }
            return null;
        }

        public override void Initialize()
        {
            initPosition();
            buildBulletsArray();
        }

        protected virtual void initPosition()
        {
            float currentY = ViewFactory.ViewHeight - ((ISprite)View).Height / 2 - 30;
            ((ISprite)View).Position = new Vector2(0, currentY);
        }

        private void buildBulletsArray()
        {
            for(int i = 0; i < k_MaxNumOfBullets; i++)
            {
                r_Bullets.Add(createBullet());
            }
        }

        private Bullet createBullet()
        {
            Bullet bullet = new Bullet(ViewFactory, eSpriteType.Bullet);
            bullet.BulletHit += Ship_BulletHit;
            bullet.YVelocity = k_BulletVelocity;
            return bullet;
        }

        private void Ship_BulletHit(object sender, EventArgs e)
        {
            setNewScore(Score + ((Bullet) sender).Score);
        }

        private void setNewScore(int score)
        {
            Score = score < 0 ? 0 : score;
            ScoreChanged(this, EventArgs.Empty);
        }

        public override void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
            if (i_SpriteLogic.Type == eSpriteType.Bomb)
            {
                initPosition();
                m_RemainingSouls--;
                setNewScore(Score - 2000);
                ShipHit(this, EventArgs.Empty);
                if(m_RemainingSouls ==0)
                {
                    Alive = false;
                }

            }
        }
    }
}