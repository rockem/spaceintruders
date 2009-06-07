using System;
using Microsoft.Xna.Framework;

namespace Intruders.comp.animation
{
    public class RotateAnimation : Animation
    {
        private const float m_RotationAngle = MathHelper.PiOver4;
        private readonly TimeSpan r_FadeEffectTimeSpan = TimeSpan.FromSeconds(0.25f);

        public RotateAnimation(SpriteComponent i_BoundSprite)
            : base(i_BoundSprite)
        {
            m_TimeLeft = r_FadeEffectTimeSpan;
            m_AnimationTime = r_FadeEffectTimeSpan;
        }

        public override void Initialize()
        {
            base.Initialize();

            m_BoundSprite.RotationOrigin =
                new Vector2(
                    (m_BoundSprite as SpriteComponent).WidthAfterScale / 2,
                    (m_BoundSprite as SpriteComponent).HeightAfterScale / 2);
        }

        public override void Reset()
        {
            base.Reset();

            m_BoundSprite.Rotation = 0;
        }

        protected override void Play(GameTime i_GameTime)
        {
            m_BoundSprite.Rotation += m_RotationAngle;
        }
    }
}