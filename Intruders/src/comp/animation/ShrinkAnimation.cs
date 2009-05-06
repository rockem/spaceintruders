using System;
using Microsoft.Xna.Framework;

namespace Intruders.comp.animation
{
    public class ShrinkAnimation : Intruders.comp.animation.Animation
    {
        private const float k_ScaleFactor = 0.9f;
        private readonly TimeSpan r_ShrinkEffectTimeSpan = TimeSpan.FromSeconds(0.5f);

        private float m_ScaleFactor;

        private Vector2 m_OriginalPosition;

        public ShrinkAnimation(Component i_BoundSprite)
            : base(i_BoundSprite)
        {
            m_TimeLeft = r_ShrinkEffectTimeSpan;
            m_AnimationTime = r_ShrinkEffectTimeSpan;
            m_ScaleFactor = k_ScaleFactor;
        }

        public override void Reset()
        {
            base.Reset();

            //m_BoundSprite.Scale = 1;
            m_NeedToReset = false;
            /*m_OriginalPosition = m_BoundSprite.PositionOfOrigin;*/
        }

        protected override void Play(GameTime i_GameTime)
        {
            /*if(m_BoundSprite.Scale < 0)
            {
                m_IsFinished = true;
                m_TimeLeft = TimeSpan.Zero;
            }

            m_BoundSprite.Scale *= m_ScaleFactor;*/

            Vector2 curPosition = m_OriginalPosition;

           /* curPosition.X += (m_BoundSprite.WidthBeforeScale - m_BoundSprite.WidthAfterScale) / 2;
            curPosition.Y += (m_BoundSprite.HeightBeforeScale - m_BoundSprite.HeightAfterScale) / 2;

            m_BoundSprite.PositionOfOrigin = curPosition;*/
        }
    }
}