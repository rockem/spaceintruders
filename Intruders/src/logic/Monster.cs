using System;
using System.Collections.Generic;
using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    internal class Monster : SpriteLogic
    {
        private readonly TimeSpan r_DieDuration = TimeSpan.FromSeconds(0.5f);
        private TimeSpan m_TimeLeftToDie;
        private bool m_Dying;

        public event EventHandler MonsterHit;

        public Monster(IViewFactory i_Factory) : base(i_Factory)
        {
        }

        public override void Update(GameTime i_GameTime)
        {
            m_TimeLeftToDie -= i_GameTime.ElapsedGameTime;
            if(m_TimeLeftToDie.TotalSeconds > 0)
            {
                m_TimeLeftToDie -= i_GameTime.ElapsedGameTime;
                getSprite().Scale *= 0.9f;
                getSprite().Position = new Vector2(Position.X + Width * 0.04f,
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
            base.Update(i_GameTime);
        }

        public override void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
            m_TimeLeftToDie = r_DieDuration;
            m_Dying = true;
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