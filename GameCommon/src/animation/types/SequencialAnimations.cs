using System;
using GameCommon.comp;
using Infrastructure.ObjectModel.Animations;
using Microsoft.Xna.Framework;

namespace GameCommon.animation.types
{
    public class SequencialAnimations : AnimationsManager
    {
        public SequencialAnimations(
            string i_Name,
            TimeSpan i_AnimationLength,
            Sprite i_BoundSprite,
            params SpriteAnimation[] i_Animations)
            : base(i_Name, i_AnimationLength, i_BoundSprite, i_Animations)
        {}

        protected override void DoFrame(GameTime i_GameTime)
        {
            bool allFinished = true;
            foreach (SpriteAnimation animation in m_AnimationsList)
            {
                if (!animation.IsFinished)
                {
                    animation.Animate(i_GameTime);
                    allFinished = false;
                    break; // we are not going to call all our animations until this one is done
                }
            }

            if (allFinished)
            {
                this.IsFinished = true;
            }
        }
    }
}