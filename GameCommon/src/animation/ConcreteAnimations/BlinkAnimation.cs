using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animations.ConcreteAnimations
{
    public class BlinkAnimation : SpriteAnimation
    {
        private TimeSpan m_BlinkLength;
        private TimeSpan m_TimeLeftForNextBlink;
        private bool m_IsVisible = true;

        // CTORs
        public BlinkAnimation(TimeSpan i_BlinkLength, TimeSpan i_AnimationLength)
            : base("Blink", i_AnimationLength)
        {
            this.m_BlinkLength = i_BlinkLength;
            this.m_TimeLeftForNextBlink = i_BlinkLength;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            m_TimeLeftForNextBlink -= i_GameTime.ElapsedGameTime;
            if (m_TimeLeftForNextBlink.TotalSeconds < 0)
            {
                // we have elapsed, so blink
                m_IsVisible = !m_IsVisible;
                m_TimeLeftForNextBlink = m_BlinkLength;
            }

            this.BoundSprite.Visible = m_IsVisible;
        }

        public override void Reset(TimeSpan i_AnimationLength)
        {
            base.Reset(i_AnimationLength);

            this.BoundSprite.Visible = m_OriginalSpriteInfo.Visible;
        }
    }
}
