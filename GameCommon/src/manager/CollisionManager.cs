using System;
using System.Collections.Generic;
using GameCommon.service;
using Microsoft.Xna.Framework;

namespace GameCommon.manager
{
    public class CollisionsManager : GameService, ICollisionsManager
    {
        protected readonly List<ICollidable> m_Collidables = new List<ICollidable>();

        public CollisionsManager(Game i_Game)
            :
                base(i_Game, int.MaxValue)
        {
        }

        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(ICollisionsManager), this);
        }

        public void AddObjectToMonitor(ICollidable i_Collidable)
        {
            if (!this.m_Collidables.Contains(i_Collidable))
            {
                this.m_Collidables.Add(i_Collidable);
                i_Collidable.PositionChanged += collidable_PositionChanged;
                i_Collidable.Disposed += collidable_Disposed;
            }
        }

        private void collidable_PositionChanged(object i_Collidable)
        {
            if (i_Collidable is ICollidable)
            {// to be on the safe side :)
                CheckCollision(i_Collidable as ICollidable);
            }
        }

        private void collidable_Disposed(object sender, EventArgs e)
        {
            ICollidable collidable = sender as ICollidable;

            if (collidable != null
                &&
                this.m_Collidables.Contains(collidable))
            {
                collidable.PositionChanged -= collidable_PositionChanged;
                collidable.Disposed -= collidable_Disposed;
                m_Collidables.Remove(collidable);
            }
        }

        private void CheckCollision(ICollidable i_Source)
        {
            foreach (ICollidable target in m_Collidables)
            {
                if (i_Source != target && target.Visible)
                {
                    if (target.CheckCollision(i_Source))
                    {
                        target.Collided(i_Source);
                        i_Source.Collided(target);
                    }
                }
            }
        }
    }
}