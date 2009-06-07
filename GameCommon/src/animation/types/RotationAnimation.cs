using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animations.ConcreteAnimations
{
    public class RotationAnimation : SpriteAnimation
    {
        public TimeSpan m_FullRotationLength;

        // CTORs
        public RotationAnimation(TimeSpan i_FullRotationLength, TimeSpan i_AnimationLength)
            : base("Rotation", i_AnimationLength)
        {
            m_FullRotationLength = i_FullRotationLength;
        }

        public override void Reset(TimeSpan i_AnimationLength)
        {
            base.Reset(i_AnimationLength);

            this.BoundSprite.Rotation = m_OriginalSpriteInfo.Rotation;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            float elapsedPercentage = (float)((i_GameTime.TotalGameTime.TotalMilliseconds % m_FullRotationLength.TotalMilliseconds) / m_FullRotationLength.TotalMilliseconds);
            float rotation = (MathHelper.TwoPi * elapsedPercentage);

            this.BoundSprite.Rotation = rotation;
        }
    }
}
