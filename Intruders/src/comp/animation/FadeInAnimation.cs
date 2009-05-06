using System;
using Microsoft.Xna.Framework;

namespace Intruders.comp.animation
{
    public class FadeInAnimation : Animation
    {
        private readonly TimeSpan r_FadeEffectTimeSpan = TimeSpan.FromSeconds(0.1f);

        public FadeInAnimation(Component i_BoundSprite) : base(i_BoundSprite)
        {
            m_TimeLeft = r_FadeEffectTimeSpan;
            m_AnimationTime = r_FadeEffectTimeSpan;
            m_BoundSprite.Opacity = 100;
        }

        public override void Reset()
        {
            base.Reset();

            m_BoundSprite.Opacity = 100;
        }

        protected override void Play(GameTime i_GameTime)
        {
            m_BoundSprite.Opacity = (float)(m_AnimationTime.TotalSeconds / m_TimeLeft.TotalSeconds) * 100;
        }
    }
}