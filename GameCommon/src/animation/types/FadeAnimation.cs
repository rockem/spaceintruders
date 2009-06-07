using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animations.ConcreteAnimations
{
    public class FadeAnimation : SpriteAnimation
    {
        protected float m_TargetOpacity;
        public float TargetOpacity
        {
            get { return m_TargetOpacity; }
            set { m_TargetOpacity = value; }
        }

        protected float m_FadeTime;
        public float FadeTime
        {
            get { return m_FadeTime; }
            set { m_FadeTime = value; }
        }

        protected bool m_Loop;
        public bool Loop
        {
            get { return m_Loop; }
            set { m_Loop = value; }
        }

        private float m_Speed;
        private bool m_FadingOut;
        private float m_CurrentTarget;
        private float m_SourceOpacity;
        private float m_DeltaOpacity;

        public FadeAnimation(string i_Name, TimeSpan i_AnimationLength, float i_TargetOpacity, float i_FadeTime, bool i_Loop)
            : base(i_Name, i_AnimationLength)
        {
            m_TargetOpacity = i_TargetOpacity;
            m_Loop = i_Loop;
            m_FadeTime = i_FadeTime;
        }

        public override void Reset(TimeSpan i_AnimationLength)
        {
            base.Reset(i_AnimationLength);

            this.BoundSprite.Opacity = m_OriginalSpriteInfo.Opacity;

            m_SourceOpacity = m_OriginalSpriteInfo.Opacity;
            m_CurrentTarget = m_TargetOpacity;
            m_DeltaOpacity = m_CurrentTarget - m_SourceOpacity;
            m_FadingOut = m_DeltaOpacity < 0;
            m_Speed = m_DeltaOpacity / m_FadeTime;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            float totalSeconds = (float)i_GameTime.ElapsedGameTime.TotalSeconds;

            if ((m_FadingOut && this.BoundSprite.Opacity > m_CurrentTarget)
                ||
                (!m_FadingOut && this.BoundSprite.Opacity < m_CurrentTarget))
            {
                this.BoundSprite.Opacity += totalSeconds * m_Speed;
            }
            else if(m_Loop)
            {
                this.BoundSprite.Opacity = m_CurrentTarget;
                m_FadingOut = !m_FadingOut;
                m_CurrentTarget = m_SourceOpacity;
                m_SourceOpacity = this.BoundSprite.Opacity;
                m_DeltaOpacity = m_CurrentTarget - m_SourceOpacity;
                m_Speed = m_DeltaOpacity / m_FadeTime;
            }
            else
            {
                this.IsFinished = true;
            }
        }
    }
}
