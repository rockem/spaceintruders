using System;
using System.Collections.Generic;
using GameCommon.comp;
using Infrastructure.ObjectModel.Animations;
using Microsoft.Xna.Framework;

namespace GameCommon.animation
{
    public class AnimationsManager : SpriteAnimation
    {
        private readonly Dictionary<string, SpriteAnimation> m_AnimationsDictionary = 
            new Dictionary<string, SpriteAnimation>();

        protected readonly List<SpriteAnimation> m_AnimationsList = new List<SpriteAnimation>();

        // CTORs

        // CTOR: Me as an AnimationsMamager
        public AnimationsManager(Sprite i_BoundSprite)
            : this("AnimationsMamager", TimeSpan.Zero, i_BoundSprite, new SpriteAnimation[]{})
        {
            this.Enabled = false;
        }
        
        // CTOR: me as a ParallelAnimations animation:
        public AnimationsManager(
            string i_Name,
            TimeSpan i_AnimationLength,
            Sprite i_BoundSprite,
            params SpriteAnimation[] i_Animations)
            : base (i_Name, i_AnimationLength)
        {
            this.BoundSprite = i_BoundSprite;

            foreach (SpriteAnimation animation in i_Animations)
            {
                this.Add(animation);
            }
        }

        public void Add(SpriteAnimation i_Animation)
        {
            i_Animation.BoundSprite = this.BoundSprite;
            i_Animation.Enabled = true;
            m_AnimationsDictionary.Add(i_Animation.Name, i_Animation);
            m_AnimationsList.Add(i_Animation);
        }

        public void Remove(string i_AnimationName)
        {
            SpriteAnimation animationToRemove;
            m_AnimationsDictionary.TryGetValue(i_AnimationName, out animationToRemove);
            if (animationToRemove != null)
            {
                m_AnimationsDictionary.Remove(i_AnimationName);
                m_AnimationsList.Remove(animationToRemove);
            }
        }

        public SpriteAnimation this[string i_Name]
        {
            get
            {
                SpriteAnimation retVal = null;
                m_AnimationsDictionary.TryGetValue(i_Name, out retVal);
                return retVal;
            }            
        }

        public override void Restart()
        {
            base.Restart();

            foreach (SpriteAnimation animation in m_AnimationsList)
            {
                animation.Restart();
            }
        }

        public override void Restart(TimeSpan i_AnimationLength)
        {
            base.Restart(i_AnimationLength);

            foreach (SpriteAnimation animation in m_AnimationsList)
            {
                animation.Restart();
            }
        }

        public override void Reset()
        {
            base.Reset();

            foreach (SpriteAnimation animation in m_AnimationsList)
            {
                animation.Reset();
            }
        }

        public override void Reset(TimeSpan i_AnimationLength)
        {
            base.Reset(i_AnimationLength);

            foreach (SpriteAnimation animation in m_AnimationsList)
            {
                animation.Reset();
            }
        }

        protected override void CloneSpriteInfo()
        {
            base.CloneSpriteInfo();

            foreach (SpriteAnimation animation in m_AnimationsList)
            {
                animation.m_OriginalSpriteInfo = m_OriginalSpriteInfo;
            }
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            foreach (SpriteAnimation animation in m_AnimationsList)
            {
                animation.Animate(i_GameTime);
            }
        }
    }
}