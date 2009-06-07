using System;
using Intruders.comp.animation;
using Microsoft.Xna.Framework;

namespace Intruders.comp
{
    internal class StarComponent : Component
    {
        private readonly FadeInAnimation m_FadingInAnimation;
        private bool m_Alive;
        private FadeOutAnimation m_FadingOutAnimation;
        private TimeSpan m_TimeTillFade = TimeSpan.FromSeconds(1);

        public StarComponent(Game game) : base(@"Sprites\Star", game)
        {
            m_FadingInAnimation = new FadeInAnimation(this);
        }

        public bool Alive
        {
            get { return m_Alive; }
            set
            {
                m_Alive = value;
                Enabled = m_Alive;
                Visible = m_Alive;
                if(m_Alive)
                {
                    m_FadingInAnimation.Enabled = true;
                    m_TimeTillFade = TimeSpan.FromSeconds(1);
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            Alive = false;
            m_FadingOutAnimation = new FadeOutAnimation(this);
            m_FadingOutAnimation.AnimationFinished += FadingAnimation_AnimationFinished;
        }

        public override void Update(GameTime gameTime)
        {
            // base.Update(gameTime);
            m_TimeTillFade -= gameTime.ElapsedGameTime;
            if(m_TimeTillFade.TotalSeconds <= 0)
            {
                m_FadingOutAnimation.Enabled = true;
            }

            m_FadingOutAnimation.Animate(gameTime);
            m_FadingInAnimation.Animate(gameTime);
        }

        private void FadingAnimation_AnimationFinished(object sender, EventArgs e)
        {
            Alive = false;
        }
    }
}