using System;
using System.Collections.Generic;
using System.Text;
using Intruders.logic;
using Microsoft.Xna.Framework;

namespace Intruders.comp.animation
{
    public abstract class Animation
    {
        public event EventHandler AnimationFinished;

        protected TimeSpan m_AnimationTime;
        protected TimeSpan m_TimeLeft;
        private bool m_Enabled;
        protected bool m_IsFinished;
        protected bool m_IsInitiated;
        protected bool m_NeedToReset;
        protected Component m_BoundSprite;

        public bool IsFinished
        {
            get
            {
                return this.m_IsFinished;
            }

            protected set
            {
                if(value != m_IsFinished)
                {
                    m_IsFinished = value;

                    if(m_IsFinished == true)
                    {
                        OnAnimationFinished();
                    }
                }
            }
        }

        public bool Enabled
        {
            get
            {
                return m_Enabled;
            }
            set
            {
                m_Enabled = value;
            }
        }

        protected Animation(Component i_BoundSprite)
        {
            m_BoundSprite = i_BoundSprite;
            m_IsInitiated = false;
            m_NeedToReset = true;
        }

        public virtual void Initialize()
        {
            m_IsInitiated = true;
            Reset();
        }

        public void Animate(GameTime i_GameTime)
        {
            if(m_Enabled && !m_IsFinished)
            {
                if(!m_IsInitiated)
                {
                    Initialize();
                }

                if(!m_IsFinished)
                {
                    m_TimeLeft -= i_GameTime.ElapsedGameTime;

                    if(m_TimeLeft.TotalSeconds <= 0)
                    {
                        Enabled = false;
                        IsFinished = true;
                    }
                    else
                    {
                        Play(i_GameTime);
                    }
                }
            }
        }

        public virtual void Reset()
        {
            m_TimeLeft = m_AnimationTime;
            IsFinished = false;
        }

        private void OnAnimationFinished()
        {
            if(m_NeedToReset)
            {
                Reset();
            }

            if(AnimationFinished != null)
            {
                AnimationFinished(this, EventArgs.Empty);
            }
        }

        protected abstract void Play(GameTime i_GameTime);
    }
}