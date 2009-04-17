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

        public Ship(IViewFactory i_Factory) : base(i_Factory)
        {
        }

        public override string GetImagePath()
        {
            return "Sprites\\Ship";
        }

        public override void Update(GameTime time)
        {
            int velocity = 0;
            float currentX = getSprite().Position.X;
            m_TimeLeftForNextShoot -= time.ElapsedGameTime;

            if (inputFire())
            {
                if(m_TimeLeftForNextShoot.TotalSeconds <= 0)
                {
                    shootBullet();
                    m_TimeLeftForNextShoot = r_MinTimeBetweenBullets;
                }
            }

            if (inputRight())
            {
                velocity = k_PxForSecond;
            }
            if (inputLeft())
            {
                velocity = -k_PxForSecond;
            }

            currentX += velocity * (float)time.ElapsedGameTime.TotalSeconds;
            currentX += ViewFactory.InputManager.MousePositionDelta.X;
            currentX = MathHelper.Clamp(currentX, 0, ViewFactory.ViewWidth - getSprite().Width);
            getSprite().Position = new Vector2(currentX, getSprite().Position.Y);
        }

        private bool inputFire()
        {
            return ViewFactory.InputManager.KeyPressed(Keys.Enter) || 
                ViewFactory.InputManager.ButtonPressed(eInputButtons.Left);
        }

        private bool inputLeft()
        {
            return ViewFactory.InputManager.KeyHeld(Keys.Left);
        }

        private bool inputRight()
        {
            return ViewFactory.InputManager.KeyHeld(Keys.Right);
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
            float currentY = ViewFactory.ViewHeight - getSprite().Height / 2 - 30;
            getSprite().Position = new Vector2(0, currentY);
            buildBulletsArray();
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
            Bullet bullet = new Bullet(ViewFactory);
            bullet.YVelocity = k_BulletVelocity;
            bullet.Alive = false;
            return bullet;
        }
    }
}